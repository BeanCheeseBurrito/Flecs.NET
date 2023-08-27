using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Flecs.NET.Bindings;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Static class that registers and stores information about types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static unsafe class Type<T>
    {
        /// <summary>
        ///     Registered type hooks.
        /// </summary>
        public static TypeHooks? Hooks;

        /// <summary>
        ///     The raw id of the type.
        /// </summary>
        public static ulong RawId { get; private set; }

        /// <summary>
        ///     The size of the type.
        /// </summary>
        public static int Size { get; private set; }

        /// <summary>
        ///     The alignment of the type.
        /// </summary>
        public static int Alignment { get; private set; }

        /// <summary>
        ///     The reset count of the type.
        /// </summary>
        public static int ResetCount { get; private set; }

        /// <summary>
        ///     Whether or not the type is an alias.
        /// </summary>
        public static bool IsAlias { get; private set; }

        /// <summary>
        ///     Whether or not the type can be registered as a tag.
        /// </summary>
        public static bool AllowTag { get; private set; } = true;

        /// <summary>
        ///     The type name of the type.
        /// </summary>
        public static string? TypeName { get; private set; }

        /// <summary>
        ///     The symbol name of the type.
        /// </summary>
        public static string? SymbolName { get; private set; }

        /// <summary>
        ///     Sets type hooks for the type.
        /// </summary>
        /// <param name="typeHooks"></param>
        public static void SetTypeHooks(TypeHooks typeHooks)
        {
            Hooks = typeHooks;
        }

        /// <summary>
        ///     Tests if the type is registered.
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public static bool IsRegistered(ecs_world_t* world)
        {
            if (ResetCount != FlecsInternal.ResetCount)
                Reset();

            if (RawId == 0)
                return false;

            return world == null || ecs_exists(world, RawId) != 0;
        }

        /// <summary>
        ///     Inits a type.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="allowTag"></param>
        public static void Init(ulong entity, bool allowTag = true)
        {
            if (RawId != 0)
            {
                Assert.True(RawId == entity, $"{nameof(ECS_INCONSISTENT_COMPONENT_ID)} {GetTypeName()}");
                Assert.True(allowTag == AllowTag, nameof(ECS_INVALID_PARAMETER));
            }

            Type type = typeof(T);
            StructLayoutAttribute attribute = type.StructLayoutAttribute!;

            ResetCount = FlecsInternal.ResetCount;
            RawId = entity;
            AllowTag = allowTag;
            Size = RuntimeHelpers.IsReferenceOrContainsReferences<T>() ? sizeof(IntPtr) : sizeof(T);
            Alignment = RuntimeHelpers.IsReferenceOrContainsReferences<T>()
                ? sizeof(IntPtr)
                : attribute.Value == LayoutKind.Explicit
                    ? attribute.Pack
                    : AlignOf();

            if (!allowTag || RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                return;

            if (RuntimeFeature.IsDynamicCodeSupported)
            {
                FieldInfo[] fields =
                    type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Size = fields.Length == 0 ? 0 : Size;
                Alignment = Size == 0 ? 0 : Alignment;
            }
            else
            {
                // TODO: Reimplement NativeAOT support after move to .NET Standard 2.1
            }
        }

        /// <summary>
        ///     Registers a type and returns it's id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="allowTag"></param>
        /// <param name="id"></param>
        /// <param name="isComponent"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        public static ulong IdExplicit(ecs_world_t* world, string? name = null, bool allowTag = true,
            ulong id = default, bool isComponent = true, bool* existing = null)
        {
            if (RawId == 0)
                Assert.True(world != null, $"{nameof(ECS_COMPONENT_NOT_REGISTERED)} {name}");

            Assert.True(id == 0 || RawId == id, nameof(ECS_INCONSISTENT_COMPONENT_ID));

            if (IsRegistered(world))
            {
                Assert.True(RawId != 0 && ecs_exists(world, RawId) == 1, nameof(ECS_INTERNAL_ERROR));
                return RawId;
            }

            Init(RawId != 0 ? RawId : id, allowTag);

            Assert.True(id == 0 || RawId == id, nameof(ECS_INTERNAL_ERROR));

            string symbol = id == 0 ? GetSymbolName() : NativeString.GetString(ecs_get_symbol(world, id));

            Type type = typeof(T);
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeTypeName = (NativeString)GetTypeName();
            using NativeString nativeSymbolName = (NativeString)symbol;

            RawId = ecs_cpp_component_register_explicit(
                world, RawId, id,
                nativeName, nativeTypeName, nativeSymbolName,
                (IntPtr)Size, (IntPtr)Alignment,
                Macros.Bool(isComponent), (byte*)existing
            );

            if (type.IsEnum)
                EnumType<T>.Init(world, RawId);

            return RawId;
        }

        /// <summary>
        ///     Registers a type and returns it's id.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        /// <param name="allowTag"></param>
        /// <returns></returns>
        public static ulong Id(ecs_world_t* world, string? name = null, bool allowTag = true)
        {
            if (IsRegistered(world))
            {
                Assert.True(RawId != 0, nameof(ECS_INTERNAL_ERROR));
                return RawId;
            }

            ulong prevScope = default;
            ulong prevWith = default;

            if (world != null)
            {
                prevScope = ecs_set_scope(world, 0);
                prevWith = ecs_set_with(world, 0);
            }

            bool existing = false;
            IdExplicit(world, name, allowTag, 0, true, &existing);

            if (GetSize() != 0 && !existing)
                RegisterLifeCycleActions(world);

            if (prevWith != 0)
                ecs_set_with(world, prevWith);

            if (prevScope != 0)
                ecs_set_scope(world, prevScope);

            return RawId;
        }

        /// <summary>
        ///     Registers type hooks.
        /// </summary>
        /// <param name="world"></param>
        public static void RegisterLifeCycleActions(ecs_world_t* world)
        {
            ecs_type_hooks_t typeHooksDesc = default;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                typeHooksDesc.ctor = TypeHooks.GcHandleCtorPointer;
                typeHooksDesc.dtor = TypeHooks.GcHandleDtorPointer;
                typeHooksDesc.move = TypeHooks.GcHandleMovePointer;
                typeHooksDesc.copy = TypeHooks.GcHandleCopyPointer;

                if (Hooks == null)
                    ecs_set_hooks_id(world, RawId, &typeHooksDesc);
            }

            if (Hooks == null)
                return;

            BindingContext.TypeHooksContext* bindingContext = Memory.AllocZeroed<BindingContext.TypeHooksContext>(1);
            bindingContext->OnAdd = BindingContext.AllocCallback(Hooks.OnAdd);
            bindingContext->OnSet = BindingContext.AllocCallback(Hooks.OnSet);
            bindingContext->OnRemove = BindingContext.AllocCallback(Hooks.OnRemove);

            typeHooksDesc.on_add = bindingContext->OnAdd.Function;
            typeHooksDesc.on_set = bindingContext->OnSet.Function;
            typeHooksDesc.on_remove = bindingContext->OnRemove.Function;
            typeHooksDesc.binding_ctx = bindingContext;
            typeHooksDesc.binding_ctx_free = BindingContext.TypeHooksContextFreePointer;

            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                bindingContext->Ctor = BindingContext.AllocCallback(Hooks.Ctor);
                bindingContext->Dtor = BindingContext.AllocCallback(Hooks.Dtor);
                bindingContext->Move = BindingContext.AllocCallback(Hooks.Move);
                bindingContext->Copy = BindingContext.AllocCallback(Hooks.Copy);
                bindingContext->ContextFree = BindingContext.AllocCallback(Hooks.ContextFree);

                typeHooksDesc.ctor = TypeHooks.NormalCtorPointer;
                typeHooksDesc.dtor = TypeHooks.NormalDtorPointer;
                typeHooksDesc.move = TypeHooks.NormalMovePointer;
                typeHooksDesc.copy = TypeHooks.NormalCopyPointer;
                typeHooksDesc.ctx = Hooks.Context;
                typeHooksDesc.ctx_free = bindingContext->ContextFree.Function;
            }

            ecs_set_hooks_id(world, RawId, &typeHooksDesc);
        }

        /// <summary>
        ///     Gets the size of a type.
        /// </summary>
        /// <returns></returns>
        public static int GetSize()
        {
            Assert.True(RawId != 0, nameof(ECS_INTERNAL_ERROR));
            return Size;
        }

        /// <summary>
        ///     Gets the alignment of a type.
        /// </summary>
        /// <returns></returns>
        public static int GetAlignment()
        {
            Assert.True(RawId != 0, nameof(ECS_INTERNAL_ERROR));
            return Alignment;
        }

        /// <summary>
        ///     Gets the type name.
        /// </summary>
        /// <returns></returns>
        public static string GetTypeName()
        {
            if (TypeName != null)
                return TypeName;

            string symbol = SymbolName ?? GetSymbolName();
            return TypeName = symbol.Replace(".", "::", StringComparison.Ordinal);
        }

        /// <summary>
        ///     Gets the symbol name of a type.
        /// </summary>
        /// <returns></returns>
        public static string GetSymbolName()
        {
            if (SymbolName != null)
                return SymbolName;

            string csName = typeof(T).ToString();
            string nativeClass = $"{nameof(Flecs)}.{nameof(NET)}.{nameof(Bindings)}.{nameof(Native)}+";

            if (csName.StartsWith(nativeClass, StringComparison.Ordinal))
                IsAlias = true;

            csName = csName
                .Replace(nativeClass, string.Empty,
                    StringComparison.Ordinal) // Types from the bindings don't use namespaces
                .Replace('+', '.')
                .Replace('[', '<')
                .Replace(']', '>');

            int start = 0;
            int current = 0;
            bool skip = false; // If a tilde is encountered, skip over next characters until a '<' or '.' is encountered

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in csName)
            {
                if (skip && (c == '<' || c == '.'))
                {
                    start = current;
                    skip = false;
                }
                else if (!skip && c == '`')
                {
                    stringBuilder.Append(csName.AsSpan(start, current - start));
                    skip = true;
                }

                current++;
            }

            stringBuilder.Append(csName.AsSpan(start));
            SymbolName = stringBuilder.ToString();

            return SymbolName;
        }

        /// <summary>
        ///     Resets a types information.
        /// </summary>
        public static void Reset()
        {
            RawId = 0;
            Size = 0;
            Alignment = 0;
            AllowTag = true;
        }

        /// <summary>
        ///     Calculates the alignment of a type.
        /// </summary>
        /// <returns></returns>
        public static int AlignOf()
        {
            return sizeof(AlignOfHelper) - sizeof(T);
        }

        private readonly struct AlignOfHelper
        {
            private readonly byte Dummy;
            private readonly T Data;

            public AlignOfHelper(byte dummy, T data)
            {
                Dummy = dummy;
                Data = data;
                _ = Dummy;
                _ = Data;
            }
        }
    }
}
