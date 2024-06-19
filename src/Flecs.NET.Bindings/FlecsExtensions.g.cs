#pragma warning disable CS8981

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Flecs.NET")]

namespace Flecs.NET.Bindings
{
    public static unsafe partial class flecs
    {
    #if NET5_0_OR_GREATER
        [SuppressGCTransition]
    #endif
        [DllImport(BindgenInternal.DllImportPath, EntryPoint = "ecs_get_binding_ctx", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void* ecs_get_binding_ctx_fast(ecs_world_t* world);

        // TODO: Added manually because bindgen can't handle this yet. Fix during bindgen rewrite.
        public partial struct ecs_script_vars_t
        {
            public struct ecs_stack_t { }
        }
    }
}

#pragma warning restore CS8981
