using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Flecs.NET.Polyfill;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe partial class TypeHooks
    {
        public ManagedCtor? Ctor { get; set; }
        public ManagedDtor? Dtor { get; set; }
        public ManagedMove? Move { get; set; }
        public ManagedCopy? Copy { get; set; }
        public Ecs.IterAction? OnAdd { get; set; }
        public Ecs.IterAction? OnSet { get; set; }
        public Ecs.IterAction? OnRemove { get; set; }
        public Ecs.ContextFree? ContextFree { get; set; }
        public void* Context { get; set; }
    }

    public unsafe partial class TypeHooks
    {
#if NET5_0_OR_GREATER
        internal static readonly IntPtr NormalCtorPointer =
            (IntPtr)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&NormalCtor;

        internal static readonly IntPtr NormalDtorPointer =
            (IntPtr)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&NormalDtor;

        internal static readonly IntPtr NormalMovePointer =
            (IntPtr)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&NormalMove;

        internal static readonly IntPtr NormalCopyPointer =
            (IntPtr)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&NormalCopy;

        internal static readonly IntPtr GcHandleCtorPointer =
            (IntPtr)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&GcHandleCtor;

        internal static readonly IntPtr GcHandleDtorPointer =
            (IntPtr)(delegate* unmanaged<void*, int, ecs_type_info_t*, void>)&GcHandleDtor;

        internal static readonly IntPtr GcHandleMovePointer =
            (IntPtr)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&GcHandleMove;

        internal static readonly IntPtr GcHandleCopyPointer =
            (IntPtr)(delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>)&GcHandleCopy;
#else
        internal static readonly IntPtr NormalCtorPointer;
        internal static readonly IntPtr NormalDtorPointer;
        internal static readonly IntPtr NormalMovePointer;
        internal static readonly IntPtr NormalCopyPointer;

        internal static readonly IntPtr GcHandleCtorPointer;
        internal static readonly IntPtr GcHandleDtorPointer;
        internal static readonly IntPtr GcHandleMovePointer;
        internal static readonly IntPtr GcHandleCopyPointer;

        private static readonly UnmanagedCtor NormalCtorReference = NormalCtor;
        private static readonly UnmanagedDtor NormalDtorReference = NormalDtor;
        private static readonly UnmanagedMove NormalMoveReference = NormalMove;
        private static readonly UnmanagedCopy NormalCopyReference = NormalCopy;

        private static readonly UnmanagedCtor GcHandleCtorReference = GcHandleCtor;
        private static readonly UnmanagedDtor GcHandleDtorReference = GcHandleDtor;
        private static readonly UnmanagedMove GcHandleMoveReference = GcHandleMove;
        private static readonly UnmanagedCopy GcHandleCopyReference = GcHandleCopy;

        [SuppressMessage("Usage", "CA1810")]
        static TypeHooks()
        {
            NormalCtorPointer = Marshal.GetFunctionPointerForDelegate(NormalCtorReference);
            NormalDtorPointer = Marshal.GetFunctionPointerForDelegate(NormalDtorReference);
            NormalMovePointer = Marshal.GetFunctionPointerForDelegate(NormalMoveReference);
            NormalCopyPointer = Marshal.GetFunctionPointerForDelegate(NormalCopyReference);

            GcHandleCtorPointer = Marshal.GetFunctionPointerForDelegate(GcHandleCtorReference);
            GcHandleDtorPointer = Marshal.GetFunctionPointerForDelegate(GcHandleDtorReference);
            GcHandleMovePointer = Marshal.GetFunctionPointerForDelegate(GcHandleMoveReference);
            GcHandleCopyPointer = Marshal.GetFunctionPointerForDelegate(GcHandleCopyReference);
        }
#endif

        [UnmanagedCallersOnly]
        private static void NormalCtor(void* data, int count, ecs_type_info_t* typeInfo)
        {
            CtorData ctorData = new CtorData(data, typeInfo);
            BindingContext.TypeHooks* context = (BindingContext.TypeHooks*)typeInfo->hooks.binding_ctx;

            if (context->Ctor.Function == IntPtr.Zero)
                return;

            ManagedCtor callback = Marshal.GetDelegateForFunctionPointer<ManagedCtor>(context->Ctor.Function);

            for (int i = 0; i < count; i++)
            {
                ctorData.Data = (void*)((IntPtr)data + typeInfo->size);
                callback(ref ctorData);
            }
        }

        [UnmanagedCallersOnly]
        private static void NormalDtor(void* data, int count, ecs_type_info_t* typeInfo)
        {
            DtorData dtorData = new DtorData(data, typeInfo);
            BindingContext.TypeHooks* context = (BindingContext.TypeHooks*)typeInfo->hooks.binding_ctx;

            if (context->Dtor.Function == IntPtr.Zero)
                return;

            ManagedDtor callback = Marshal.GetDelegateForFunctionPointer<ManagedDtor>(context->Dtor.Function);

            for (int i = 0; i < count; i++)
            {
                dtorData.Data = (void*)((IntPtr)data + typeInfo->size);
                callback(ref dtorData);
            }
        }

        [UnmanagedCallersOnly]
        private static void NormalMove(void* src, void* dst, int count, ecs_type_info_t* typeInfo)
        {
            MoveData moveData = new MoveData(src, dst, typeInfo);
            BindingContext.TypeHooks* context = (BindingContext.TypeHooks*)typeInfo->hooks.binding_ctx;

            if (context->Move.Function == IntPtr.Zero)
                return;

            ManagedMove callback = Marshal.GetDelegateForFunctionPointer<ManagedMove>(context->Move.Function);

            for (int i = 0; i < count; i++)
            {
                moveData.SrcData = (void*)((IntPtr)src + typeInfo->size);
                moveData.DstData = (void*)((IntPtr)dst + typeInfo->size);
                callback(ref moveData);
            }
        }

        [UnmanagedCallersOnly]
        private static void NormalCopy(void* src, void* dst, int count, ecs_type_info_t* typeInfo)
        {
            CopyData copyData = new CopyData(src, dst, typeInfo);
            BindingContext.TypeHooks* context = (BindingContext.TypeHooks*)typeInfo->hooks.binding_ctx;

            if (context->Copy.Function == IntPtr.Zero)
                return;

            ManagedCopy callback = Marshal.GetDelegateForFunctionPointer<ManagedCopy>(context->Copy.Function);

            for (int i = 0; i < count; i++)
            {
                copyData.SrcData = (void*)((IntPtr)src + typeInfo->size);
                copyData.DstData = (void*)((IntPtr)dst + typeInfo->size);
                callback(ref copyData);
            }
        }

        [UnmanagedCallersOnly]
        private static void GcHandleCtor(void* data, int count, ecs_type_info_t* typeInfo)
        {
            IntPtr* handles = (IntPtr*)data;

            for (int i = 0; i < count; i++)
                handles[i] = IntPtr.Zero;
        }

        [UnmanagedCallersOnly]
        private static void GcHandleDtor(void* data, int count, ecs_type_info_t* typeInfo)
        {
            IntPtr* handles = (IntPtr*)data;

            for (int i = 0; i < count; i++)
                Managed.FreeGcHandle(handles[i]);
        }

        [UnmanagedCallersOnly]
        private static void GcHandleMove(void* dst, void* src, int count, ecs_type_info_t* typeInfo)
        {
            IntPtr* handlesDst = (IntPtr*)dst;
            IntPtr* handlesSrc = (IntPtr*)src;

            for (int i = 0; i < count; i++)
            {
                Managed.FreeGcHandle(handlesDst[i]);
                handlesDst[i] = handlesSrc[i];
                handlesSrc[i] = IntPtr.Zero;
            }
        }

        [UnmanagedCallersOnly]
        private static void GcHandleCopy(void* dst, void* src, int count, ecs_type_info_t* typeInfo)
        {
            IntPtr* handlesDst = (IntPtr*)dst;
            IntPtr* handlesSrc = (IntPtr*)src;

            for (int i = 0; i < count; i++)
            {
                Managed.FreeGcHandle(handlesDst[i]);
                handlesDst[i] = handlesSrc[i];
            }
        }

        private delegate void UnmanagedCtor(void* data, int count, ecs_type_info_t* typeInfo);

        private delegate void UnmanagedDtor(void* data, int count, ecs_type_info_t* typeInfo);

        private delegate void UnmanagedMove(void* dst, void* src, int count, ecs_type_info_t* typeInfo);

        private delegate void UnmanagedCopy(void* dst, void* src, int count, ecs_type_info_t* typeInfo);

        public delegate void ManagedCtor(ref CtorData ctorData);

        public delegate void ManagedDtor(ref DtorData dtorData);

        public delegate void ManagedMove(ref MoveData ctorData);

        public delegate void ManagedCopy(ref CopyData copyData);
    }

    public unsafe struct CtorData
    {
        public void* Data { get; internal set; }
        public TypeInfo TypeInfo { get; }

        public CtorData(void* data, ecs_type_info_t* typeInfo)
        {
            Data = data;
            TypeInfo = new TypeInfo(typeInfo);
        }

        public readonly ref T Get<T>()
        {
            return ref Managed.GetTypeRef<T>(Data);
        }
    }

    public unsafe struct DtorData
    {
        public void* Data { get; internal set; }
        public TypeInfo TypeInfo { get; }

        public DtorData(void* data, ecs_type_info_t* typeInfo)
        {
            Data = data;
            TypeInfo = new TypeInfo(typeInfo);
        }

        public readonly ref T Get<T>()
        {
            return ref Managed.GetTypeRef<T>(Data);
        }
    }

    public unsafe struct MoveData
    {
        public void* SrcData { get; internal set; }
        public void* DstData { get; internal set; }

        public TypeInfo TypeInfo { get; }

        public MoveData(void* src, void* dst, ecs_type_info_t* typeInfo)
        {
            SrcData = src;
            DstData = dst;
            TypeInfo = new TypeInfo(typeInfo);
        }

        public readonly ref T Src<T>()
        {
            return ref Managed.GetTypeRef<T>(SrcData);
        }

        public readonly ref T Dst<T>()
        {
            return ref Managed.GetTypeRef<T>(DstData);
        }
    }

    public unsafe struct CopyData
    {
        public void* SrcData { get; internal set; }
        public void* DstData { get; internal set; }

        public TypeInfo TypeInfo { get; }

        public CopyData(void* src, void* dst, ecs_type_info_t* typeInfo)
        {
            SrcData = src;
            DstData = dst;
            TypeInfo = new TypeInfo(typeInfo);
        }

        public readonly ref T Src<T>()
        {
            return ref Managed.GetTypeRef<T>(SrcData);
        }

        public readonly ref T Dst<T>()
        {
            return ref Managed.GetTypeRef<T>(DstData);
        }
    }
}
