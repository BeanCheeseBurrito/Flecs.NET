using System;
using System.Runtime.InteropServices;
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
        ///     Determines whether or not to strip the GUID prefix the from beginning of file-local type names when
        ///     registering components. This is will cause name clashing if file-local types in different files
        ///     have the same name. This is primarily used in Flecs.NET.Examples to reduce output noise.
        /// </summary>
        public static bool StripFileLocalTypeNameGuid { get; set; }

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

        internal static ulong ComponentRegister(
            ecs_world_t* world,
            ulong id,
            byte* name,
            byte* symbol,
            int size,
            int alignment,
            byte implicitName,
            byte* existingOut)

        {
            byte existing = Macros.False;
            ulong prevScope = ecs_set_scope(world, 0);
            ulong ent;

            if (id != 0)
            {
                ent = id;
            }
            else
            {
                ent = ecs_lookup_path_w_sep(world, 0, name,
                    BindingContext.DefaultSeparator, BindingContext.DefaultRootSeparator, Macros.False);
                existing = Macros.Bool(ent != 0 && ecs_has_id(world, ent, FLECS_IDEcsComponentID_) == Macros.True);
            }

            ecs_set_scope(world, prevScope);

            if (ent != 0)
            {
                EcsComponent* component = (EcsComponent*)ecs_get_id(world, ent, FLECS_IDEcsComponentID_);

                if (component != null)
                {
                    byte* sym = ecs_get_symbol(world, ent);
                    if (sym != null && !Utils.StringEqual(sym, symbol))
                    {
                        byte* typePath = ecs_get_path_w_sep(world, 0, ent, BindingContext.DefaultSeparator, null);

                        if (!Utils.StringEqual(typePath, symbol) ||
                            component->size != size ||
                            component->alignment != alignment)
                        {
                            string? managedName = Marshal.PtrToStringAnsi((IntPtr)name);
                            string? managedSym = Marshal.PtrToStringAnsi((IntPtr)sym);
                            string? managedSymbol = Marshal.PtrToStringAnsi((IntPtr)symbol);
                            Ecs.Error(
                                $"Component with name '{managedName}' is already registered for type '{managedSym}' (trying to register for type {managedSymbol}");
                        }

                        Macros.OsFree(typePath);
                    }
                    else if (sym == null)
                    {
                        ecs_set_symbol(world, ent, symbol);
                    }
                }
            }
            else if (implicitName == Macros.False)
            {
                ent = ecs_lookup_symbol(world, symbol, Macros.False, Macros.False);
                Ecs.Assert(ent == 0 || ent == id, nameof(ECS_INCONSISTENT_COMPONENT_ID));
            }

            if (existingOut != null)
                *existingOut = existing;

            return ent;
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
                        existingName = ecs_get_path_w_sep(world, 0, id,
                            BindingContext.DefaultSeparator, BindingContext.DefaultRootSeparator);
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
                if (ecs_is_valid(world, id) == Macros.False || ecs_get_name(world, id) == null)
                    name = ecs_cpp_trim_module(world, typeName);
            }

            ulong entity;
            if (isComponent == Macros.True || size != 0)
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
                Ecs.Assert(entity != 0, nameof(ECS_INVALID_OPERATION));

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
                Ecs.Assert(entity != 0, nameof(ECS_INVALID_OPERATION));
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

            Ecs.Assert(entity != 0, nameof(ECS_INTERNAL_ERROR));
            Ecs.Assert(staticId == 0 || staticId == entity, nameof(ECS_INTERNAL_ERROR));

            if (existingName != null)
                Macros.OsFree(existingName);

            return entity;
        }

        internal static void ComponentValidate(
            ecs_world_t* world,
            ulong id,
            byte* name,
            byte* symbol,
            int size,
            int alignment,
            byte implicitName)
        {
            if (ecs_is_valid(world, id) == Macros.True && ecs_get_name(world, id) != null)
            {
                if (implicitName == Macros.False && id >= EcsFirstUserComponentId)
                {
#if DEBUG
                    byte* path = ecs_get_path_w_sep(world, 0, id, BindingContext.DefaultRootSeparator, null);

                    if (!Utils.StringEqual(path, name))
                    {
                        string? managedName = Marshal.PtrToStringAnsi((IntPtr)name);
                        string? managedPath = Marshal.PtrToStringAnsi((IntPtr)path);
                        Ecs.Error($"Component '{managedName}' already registered with name '{managedPath}'");
                    }

                    Macros.OsFree(path);
#endif
                }

                if (symbol != null)
                {
                    byte* existingSymbol = ecs_get_symbol(world, id);

                    if (existingSymbol != null)
                        if (!Utils.StringEqual(symbol, existingSymbol))
                        {
                            string? managedName = Marshal.PtrToStringAnsi((IntPtr)name);
                            string? managedSymbol = Marshal.PtrToStringAnsi((IntPtr)symbol);
                            string? managedExistingSymbol = Marshal.PtrToStringAnsi((IntPtr)existingSymbol);

                            Ecs.Error(
                                $"Component '{managedName}' with symbol '{managedSymbol}' already registered with symbol '{managedExistingSymbol}'");
                        }
                }
            }
            else
            {
                if (ecs_is_alive(world, id) == Macros.False)
                    ecs_make_alive(world, id);

                ecs_add_path_w_sep(world, id, 0, name,
                    BindingContext.DefaultSeparator, BindingContext.DefaultRootSeparator);
            }

            ecs_component_desc_t componentDesc = new ecs_component_desc_t
            {
                entity = id,
                type = new ecs_type_info_t
                {
                    size = size,
                    alignment = alignment
                }
            };

            ulong ent = ecs_component_init(world, &componentDesc);
            Ecs.Assert(ent == id, nameof(ECS_INTERNAL_ERROR));
        }
    }
}
