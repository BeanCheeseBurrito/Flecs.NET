using System.Runtime.InteropServices;

namespace Flecs.NET.Bindings
{
    public static unsafe partial class Native
    {
    #if NET5_0_OR_GREATER
        [SuppressGCTransition]
    #endif
        [DllImport(BindgenInternal.DllImportPath, EntryPoint = "ecs_get_binding_ctx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void* ecs_get_binding_ctx_fast(ecs_world_t* world);
    }
}
