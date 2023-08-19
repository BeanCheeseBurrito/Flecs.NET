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
    public static unsafe class Type<T>
    {
        public static TypeHooks? Hooks;
        public static ulong RawId { get; private set; }
        public static int Size { get; private set; }
        public static int Alignment { get; private set; }
        public static int ResetCount { get; private set; }
        public static bool IsAlias { get; private set; }
        public static bool AllowTag { get; private set; } = true;

        public static string? TypeName { get; private set; }
        public static string? SymbolName { get; private set; }

        public static void SetTypeHooks(TypeHooks typeHooks)
        {
            Hooks = typeHooks;
        }

        public static bool IsRegistered(ecs_world_t* world)
        {
            if (ResetCount != FlecsInternal.ResetCount)
                Reset();

            if (RawId == 0)
                return false;

            return world == null || ecs_exists(world, RawId) != 0;
        }

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

            if (!allowTag)
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
                (ulong)Size, (ulong)Alignment,
                Macros.Bool(isComponent), (byte*)existing
            );

            if (type.IsEnum)
                EnumType<T>.Init(world, RawId);

            return RawId;
        }

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

            BindingContext.TypeHooks* bindingContext = Memory.AllocZeroed<BindingContext.TypeHooks>(1);
            bindingContext->OnAdd = BindingContext.AllocCallback(Hooks.OnAdd);
            bindingContext->OnSet = BindingContext.AllocCallback(Hooks.OnSet);
            bindingContext->OnRemove = BindingContext.AllocCallback(Hooks.OnRemove);

            typeHooksDesc.on_add = bindingContext->OnAdd.Function;
            typeHooksDesc.on_set = bindingContext->OnSet.Function;
            typeHooksDesc.on_remove = bindingContext->OnRemove.Function;
            typeHooksDesc.binding_ctx = bindingContext;
            typeHooksDesc.binding_ctx_free = BindingContext.FreeTypeHooksPointer;

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

        public static int GetSize()
        {
            Assert.True(RawId != 0, nameof(ECS_INTERNAL_ERROR));
            return Size;
        }

        public static int GetAlignment()
        {
            Assert.True(RawId != 0, nameof(ECS_INTERNAL_ERROR));
            return Alignment;
        }

        public static string GetTypeName()
        {
            if (TypeName != null)
                return TypeName;

            string symbol = SymbolName ?? GetSymbolName();
            return TypeName = symbol.Replace(".", "::", StringComparison.Ordinal);
        }

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

        public static void Reset()
        {
            RawId = 0;
            Size = 0;
            Alignment = 0;
            AllowTag = true;
        }

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
