#if !NET5_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for holding flecs internals.
    /// </summary>
    public static unsafe class FlecsInternal
    {
        internal static IntPtr OsAbortNative;

        /// <summary>
        ///     Tests whether or not the os api is initialized.
        /// </summary>
        public static bool IsOsApiOverridden { get; private set; }


        /// <summary>
        ///     Current reset count.
        /// </summary>
        public static int ResetCount { get; private set; }

        /// <summary>
        ///     Override the default os api abort function to log C# stack traces.
        /// </summary>
        public static void OverrideOsAbort()
        {
            if (IsOsApiOverridden)
                return;

            ecs_os_set_api_defaults();
            ecs_os_api_t currentApi = ecs_os_get_api();
            OsAbortNative = currentApi.abort_;

            ecs_os_api.abort_ = BindingContext.OsApiAbortPointer;

            IsOsApiOverridden = true;
        }

        /// <summary>
        ///     Resets all type ids.
        /// </summary>
        public static void Reset()
        {
            ResetCount++;
        }

        internal static ulong ComponentRegisterExplicit(
            ecs_world_t* world,
            ulong staticId,
            ulong id,
            byte* name,
            byte* typeName,
            byte* symbol,
            int size,
            int alignment,
            byte isComponent,
            byte* existingOut)
        {
            byte* existingName = null;

            if (existingOut != null)
                *existingOut = Macros.False;

            if (id == 0)
            {
                if (name == null)
                {
                    id = ecs_lookup_symbol(world, symbol, Macros.False, Macros.False);
                    if (id != 0)
                    {
                        existingName = ecs_get_path_w_sep(world, 0, id, BindingContext.DefaultSeparator,
                            BindingContext.DefaultRootSeparator);
                        name = existingName;

                        if (existingOut != null)
                            *existingOut = Macros.True;
                    }
                    else
                    {
                        name = ecs_cpp_trim_module(world, typeName);
                    }
                }
            }
            else
            {
                if (ecs_is_valid(world, id) == 0 || ecs_get_name(world, id) == null)
                    name = ecs_cpp_trim_module(world, typeName);
            }

            ulong entity;
            if (isComponent == 1 || size != 0)
            {
                ecs_entity_desc_t entityDesc = new ecs_entity_desc_t
                {
                    id = staticId,
                    name = name,
                    sep = BindingContext.DefaultSeparator,
                    root_sep = BindingContext.DefaultRootSeparator,
                    symbol = symbol,
                    use_low_id = Macros.True
                };

                entity = ecs_entity_init(world, &entityDesc);
                Assert.True(entity != 0, nameof(ECS_INVALID_OPERATION));

                ecs_component_desc_t componentDesc = new ecs_component_desc_t
                {
                    entity = entity,
                    type = new ecs_type_info_t
                    {
                        size = size,
                        alignment = alignment
                    }
                };

                entity = ecs_component_init(world, &componentDesc);
                Assert.True(entity != 0, nameof(ECS_INVALID_OPERATION));
            }
            else
            {
                ecs_entity_desc_t entityDesc = new ecs_entity_desc_t
                {
                    id = staticId,
                    name = name,
                    sep = BindingContext.DefaultSeparator,
                    root_sep = BindingContext.DefaultRootSeparator,
                    symbol = symbol,
                    use_low_id = Macros.True
                };

                entity = ecs_entity_init(world, &entityDesc);
            }

            Assert.True(entity != 0, nameof(ECS_INTERNAL_ERROR));
            Assert.True(staticId == 0 || staticId == entity, nameof(ECS_INTERNAL_ERROR));

            if (existingName != null)
            {
#if NET5_0_OR_GREATER
                ((delegate* unmanaged[Cdecl]<IntPtr, void>)ecs_os_api.free_)((IntPtr)existingName);
#else
                Marshal.GetDelegateForFunctionPointer<Ecs.Free>(ecs_os_api.free_)((IntPtr)existingName);
#endif
            }

            return entity;
        }
    }
}
