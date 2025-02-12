#pragma warning disable CS8981

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Flecs.NET")]

namespace Flecs.NET.Bindings
{
    public static unsafe partial class flecs
    {
        [SuppressGCTransition]
        [DllImport(BindgenInternal.DllImportPath, EntryPoint = "ecs_get_binding_ctx")]
        internal static extern void* ecs_get_binding_ctx_fast(ecs_world_t* world);
    }
}

#pragma warning restore CS8981
