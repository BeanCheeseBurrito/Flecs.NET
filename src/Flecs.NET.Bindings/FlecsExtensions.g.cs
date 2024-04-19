using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Flecs.NET")]

namespace Flecs.NET.Bindings
{
    public static unsafe partial class Native
    {
    #if NET5_0_OR_GREATER
        [SuppressGCTransition]
    #endif
        [DllImport(BindgenInternal.DllImportPath, EntryPoint = "ecs_get_binding_ctx", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void* ecs_get_binding_ctx_fast(ecs_world_t* world);

        // Temporary hack until the suspend/resume functions are made public.
        [DllImport(BindgenInternal.DllImportPath, EntryPoint = "flecs_suspend_readonly", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ecs_world_t* flecs_suspend_readonly(ecs_world_t* world, ecs_suspend_readonly_state_t* state);

        [DllImport(BindgenInternal.DllImportPath, EntryPoint = "flecs_resume_readonly", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void flecs_resume_readonly(ecs_world_t* world, ecs_suspend_readonly_state_t* state);

        // Larger size just in case it changes.
        [StructLayout(LayoutKind.Explicit, Size = 96 * 2)]
        internal struct ecs_suspend_readonly_state_t { }
    }
}
