using System;
using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("Usage", "CA1721")]
    public static unsafe class Type<T>
    {
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
        ///     Registered type hooks.
        /// </summary>
        public static TypeHooks<T>? TypeHooks { get; set; }

        /// <summary>
        ///     Sets type hooks for the type.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="typeHooks"></param>
        public static void SetTypeHooks(ecs_world_t* world, TypeHooks<T> typeHooks)
        {
            TypeHooks = typeHooks;

            if (IsRegistered(world))
                RegisterLifeCycleActions(world);
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
                Ecs.Assert(RawId == entity, $"{nameof(ECS_INCONSISTENT_COMPONENT_ID)} {GetTypeName()}");
                Ecs.Assert(allowTag == AllowTag, nameof(ECS_INVALID_PARAMETER));
            }

            NativeLayout(out int size, out int alignment, allowTag);

            Size = size;
            Alignment = alignment;
            ResetCount = FlecsInternal.ResetCount;
            RawId = entity;
            AllowTag = allowTag;
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
                Ecs.Assert(world != null, $"{nameof(ECS_COMPONENT_NOT_REGISTERED)} {name}");
            else
                Ecs.Assert(id == 0 || RawId == id, nameof(ECS_INCONSISTENT_COMPONENT_ID));

            if (IsRegistered(world))
            {
                Ecs.Assert(RawId != 0 && ecs_exists(world, RawId) == 1, nameof(ECS_INTERNAL_ERROR));
                return RawId;
            }

            Init(RawId != 0 ? RawId : id, allowTag);

            Ecs.Assert(id == 0 || RawId == id, nameof(ECS_INTERNAL_ERROR));

            string symbol = id == 0 ? GetSymbolName() : NativeString.GetString(ecs_get_symbol(world, id));

            Type type = typeof(T);
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeTypeName = (NativeString)GetTypeName();
            using NativeString nativeSymbolName = (NativeString)symbol;

            RawId = FlecsInternal.ComponentRegisterExplicit(
                world, RawId, id,
                nativeName, nativeTypeName, nativeSymbolName,
                Size, Alignment,
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
                Ecs.Assert(RawId != 0, nameof(ECS_INTERNAL_ERROR));
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
                typeHooksDesc.ctor = BindingContext<T>.DefaultManagedCtorPointer;
                typeHooksDesc.dtor = BindingContext<T>.DefaultManagedDtorPointer;
                typeHooksDesc.move = BindingContext<T>.DefaultManagedMovePointer;
                typeHooksDesc.copy = BindingContext<T>.DefaultManagedCopyPointer;
            }

            if (TypeHooks == null)
                goto SetHooks;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                typeHooksDesc.ctor = TypeHooks.Ctor == null ? IntPtr.Zero : BindingContext<T>.ManagedCtorPointer;
                typeHooksDesc.dtor = TypeHooks.Dtor == null ? IntPtr.Zero : BindingContext<T>.ManagedDtorPointer;
                typeHooksDesc.move = TypeHooks.Move == null ? IntPtr.Zero : BindingContext<T>.ManagedMovePointer;
                typeHooksDesc.copy = TypeHooks.Copy == null ? IntPtr.Zero : BindingContext<T>.ManagedCopyPointer;
            }
            else
            {
                typeHooksDesc.ctor = TypeHooks.Ctor == null ? IntPtr.Zero : BindingContext<T>.UnmanagedCtorPointer;
                typeHooksDesc.dtor = TypeHooks.Dtor == null ? IntPtr.Zero : BindingContext<T>.UnmanagedDtorPointer;
                typeHooksDesc.move = TypeHooks.Move == null ? IntPtr.Zero : BindingContext<T>.UnmanagedMovePointer;
                typeHooksDesc.copy = TypeHooks.Copy == null ? IntPtr.Zero : BindingContext<T>.UnmanagedCopyPointer;
            }

            BindingContext.TypeHooksContext* bindingContext = Memory.AllocZeroed<BindingContext.TypeHooksContext>(1);
            BindingContext.SetCallback(ref bindingContext->Ctor, TypeHooks.Ctor, false);
            BindingContext.SetCallback(ref bindingContext->Dtor, TypeHooks.Dtor, false);
            BindingContext.SetCallback(ref bindingContext->Move, TypeHooks.Move, false);
            BindingContext.SetCallback(ref bindingContext->Copy, TypeHooks.Copy, false);
            BindingContext.SetCallback(ref bindingContext->OnAdd, TypeHooks.OnAdd, false);
            BindingContext.SetCallback(ref bindingContext->OnSet, TypeHooks.OnSet, false);
            BindingContext.SetCallback(ref bindingContext->OnRemove, TypeHooks.OnRemove, false);
            BindingContext.SetCallback(ref bindingContext->ContextFree, TypeHooks.ContextFree);

            typeHooksDesc.on_add = TypeHooks.OnAdd == null ? IntPtr.Zero : BindingContext<T>.OnAddHookPointer;
            typeHooksDesc.on_set = TypeHooks.OnSet == null ? IntPtr.Zero : BindingContext<T>.OnSetHookPointer;
            typeHooksDesc.on_remove = TypeHooks.OnRemove == null ? IntPtr.Zero : BindingContext<T>.OnRemoveHookPointer;
            typeHooksDesc.ctx = TypeHooks.Context;
            typeHooksDesc.ctx_free = bindingContext->ContextFree.Function;
            typeHooksDesc.binding_ctx = bindingContext;
            typeHooksDesc.binding_ctx_free = BindingContext.TypeHooksContextFreePointer;

            SetHooks:
            if (typeHooksDesc != default)
                ecs_set_hooks_id(world, RawId, &typeHooksDesc);
        }

        /// <summary>
        ///     Gets the size of a type.
        /// </summary>
        /// <returns></returns>
        public static int GetSize()
        {
            Ecs.Assert(RawId != 0, nameof(ECS_INTERNAL_ERROR));
            return Size;
        }

        /// <summary>
        ///     Gets the alignment of a type.
        /// </summary>
        /// <returns></returns>
        public static int GetAlignment()
        {
            Ecs.Assert(RawId != 0, nameof(ECS_INTERNAL_ERROR));
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

            string symbolName = SymbolName ?? GetSymbolName();
            return TypeName = "::" + symbolName;
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

            // File-local types are prefixed with a file name + GUID.
            if (FlecsInternal.StripFileLocalTypeNameGuid)
            {
                int start = 0;
                bool skip = false;

                StringBuilder stringBuilder = new StringBuilder();

                for (int current = 0; current < csName.Length;)
                {
                    char c = csName[current];

                    if (skip && c == '_')
                    {
                        skip = false;
                        start = current + 2;
                    }
                    else if (!skip && c == '<')
                    {
                        skip = true;
                        stringBuilder.Append(csName.AsSpan(start, current - start));
                        current = csName.IndexOf('>', current) + 1;
                        continue;
                    }

                    current++;
                }

                stringBuilder.Append(csName.AsSpan(start));
                csName = stringBuilder.ToString();
            }

            {
                csName = csName
                    .Replace(nativeClass, string.Empty, StringComparison.Ordinal) // Strip namespace from binding types
                    .Replace('+', '.')
                    .Replace('[', '<')
                    .Replace(']', '>');

                int start = 0;
                int current = 0;
                bool skip = false;

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
        }

        /// <summary>
        ///     Sets the type's symbol name.
        /// </summary>
        public static void SetSymbolName(string symbolName)
        {
            SymbolName = symbolName;
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
        ///     Calculates the size of the type.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="alignment"></param>
        /// <param name="allowTag"></param>
        [SuppressMessage("Usage", "CA1508")]
        public static void NativeLayout(out int size, out int alignment, bool allowTag = true)
        {
            Type type = typeof(T);
            StructLayoutAttribute attribute = type.StructLayoutAttribute!;

            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                size = sizeof(GCHandle);
                alignment = Type<GCHandle>.AlignOf();
                return;
            }

            if (attribute.Value == LayoutKind.Explicit)
            {
                size = attribute.Size == 0 ? sizeof(T) : attribute.Size;
                alignment = attribute.Pack == 0 ? AlignOf() : attribute.Pack;
            }
            else
            {
                size = sizeof(T);
                alignment = AlignOf();
            }

            if (!allowTag)
                return;

            if (RuntimeFeature.IsDynamicCodeSupported)
            {
                FieldInfo[] fields =
                    type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                size = fields.Length == 0 ? 0 : size;
                alignment = size == 0 ? 0 : alignment;
            }
            else if (sizeof(T) == 1)
            {
                // Structs containing zero non-static fields will always return true when compared using .Equals().
                // Test for tags by changing the underlying byte and checking equality.
                // If the structs always return true, it's likely that the struct is a tag.

                T aInstance = default!;
                T bInstance = default!;

                for (byte i = 0; i < 16; i++)
                {
                    *(byte*)&bInstance = i;
                    if (!Equals(aInstance, bInstance))
                        return;
                }

                size = 0;
                alignment = 0;
            }
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
