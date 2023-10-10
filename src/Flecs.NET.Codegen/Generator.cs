using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Flecs.NET.Codegen
{
    [Generator]
    public class Generator : IIncrementalGenerator
    {
        public const int GenericCount = 16;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
            {
                postContext.AddSource("Flecs.NET.g.cs", CodeFormatter.Format(Generate()));
            });
        }

        public static string Generate()
        {
            return $@"
                #pragma warning disable 1591
                #nullable enable              

                using System;
                using System.Runtime.InteropServices;
                using Flecs.NET.Utilities; 
                using static Flecs.NET.Bindings.Native;

                namespace Flecs.NET.Core 
                {{
                    {GenerateWorldExtensions()}
                    {GenerateEcsExtensions()}
                    {GenerateInvokerExtensions()}
                    {GenerateBindingContextExtensions()}
                    {GenerateFilterExtensions()}
                    {GenerateQueryExtensions()}
                    {GenerateRuleExtensions()}
                }}
                
                #pragma warning restore 1591
            ";
        }

        public static string GenerateWorldExtensions()
        {
            return $@"
                public unsafe partial struct World : IDisposable, IEquatable<World>
                {{
                    {GenerateFilterBuilderFactoryExtensions()}

                    {GenerateWorldEachCallbackFunctions()}
                    {GenerateWorldEachEntityCallbackFunction()}

                    {GenerateRoutineFactoryExtensions("IterCallback", "RoutineIter")}
                    {GenerateRoutineFactoryExtensions("EachCallback", "RoutineEach")}
                    {GenerateRoutineFactoryExtensions("EachEntityCallback", "RoutineEachEntity")}
                    {GenerateRoutineFactoryExtensions("EachIndexCallback", "RoutineEachIndex")}

                    {GenerateObserverFactoryExtensions("IterCallback", "ObserverIter")}
                    {GenerateObserverFactoryExtensions("EachCallback", "ObserverEach")}
                    {GenerateObserverFactoryExtensions("EachEntityCallback", "ObserverEachEntity")}
                    {GenerateObserverFactoryExtensions("EachIndexCallback", "ObserverEachIndex")}
                }}
            ";
        }

        public static string GenerateEcsExtensions()
        {
            return $@"
                public static partial class Ecs 
                {{
                    {GenerateIterCallbackDelegates()}
                    {GenerateEachCallbackDelegates()}
                    {GenerateEachEntityCallbackDelegates()}
                    {GenerateEachIndexCallbackDelegates()}
                    {GenerateFindCallbackDelegates()}
                    {GenerateFindEntityCallbackDelegates()}
                    {GenerateFindIndexCallbackDelegates()}
                }}
            ";
        }

        public static string GenerateInvokerExtensions()
        {
            return $@"
                public static unsafe partial class Invoker 
                {{
                    {GenerateIterInvokers()}
                    {GenerateEachInvokers()}
                    {GenerateEachEntityInvokers()}
                    {GenerateEachIndexInvokers()}
                    {GenerateFindInvokers()}
                    {GenerateFindEntityInvokers()}
                    {GenerateFindIndexInvokers()}
                }}
            ";
        }

        public static string GenerateFilterExtensions()
        {
            return $@"
                public unsafe partial struct Filter : IDisposable
                {{
                    {GenerateCallbackFunctions("Iter", "IterCallback", "ecs_filter_iter", "ecs_filter_next")}
                    {GenerateCallbackFunctions("Each", "EachCallback", "ecs_filter_iter", "ecs_filter_next_instanced")} 
                    {GenerateCallbackFunctions("Each", "EachEntityCallback", "ecs_filter_iter", "ecs_filter_next_instanced")} 
                    {GenerateCallbackFunctions("Each", "EachIndexCallback", "ecs_filter_iter", "ecs_filter_next_instanced")} 
                    {GenerateFindCallbackFunctions("FindCallback", "ecs_filter_iter", "ecs_filter_next_instanced")}
                    {GenerateFindCallbackFunctions("FindEntityCallback", "ecs_filter_iter", "ecs_filter_next_instanced")}
                    {GenerateFindCallbackFunctions("FindIndexCallback", "ecs_filter_iter", "ecs_filter_next_instanced")}
                }}
            ";
        }

        public static string GenerateQueryExtensions()
        {
            return $@"
                public unsafe partial struct Query : IDisposable
                {{
                    {GenerateCallbackFunctions("Iter", "IterCallback", "ecs_query_iter", "ecs_query_next")}
                    {GenerateCallbackFunctions("Each", "EachCallback", "ecs_query_iter", "ecs_query_next_instanced")} 
                    {GenerateCallbackFunctions("Each", "EachEntityCallback", "ecs_query_iter", "ecs_query_next_instanced")} 
                    {GenerateCallbackFunctions("Each", "EachIndexCallback", "ecs_query_iter", "ecs_query_next_instanced")}
                    {GenerateFindCallbackFunctions("FindCallback", "ecs_query_iter", "ecs_query_next_instanced")}
                    {GenerateFindCallbackFunctions("FindEntityCallback", "ecs_query_iter", "ecs_query_next_instanced")}
                    {GenerateFindCallbackFunctions("FindIndexCallback", "ecs_query_iter", "ecs_query_next_instanced")}
                }}
            ";
        }

        public static string GenerateRuleExtensions()
        {
            return $@"
                public unsafe partial struct Rule : IDisposable
                {{
                    {GenerateCallbackFunctions("Iter", "IterCallback", "ecs_rule_iter", "ecs_rule_next")}
                    {GenerateCallbackFunctions("Each", "EachCallback", "ecs_rule_iter", "ecs_rule_next_instanced")} 
                    {GenerateCallbackFunctions("Each", "EachEntityCallback", "ecs_rule_iter", "ecs_rule_next_instanced")} 
                    {GenerateCallbackFunctions("Each", "EachIndexCallback", "ecs_rule_iter", "ecs_rule_next_instanced")}
                    {GenerateFindCallbackFunctions("FindCallback", "ecs_rule_iter", "ecs_rule_next_instanced")}
                    {GenerateFindCallbackFunctions("FindEntityCallback", "ecs_rule_iter", "ecs_rule_next_instanced")}
                    {GenerateFindCallbackFunctions("FindIndexCallback", "ecs_rule_iter", "ecs_rule_next_instanced")}
                }}
            ";
        }

        public static string GenerateBindingContextExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    internal static unsafe partial class BindingContext<{typeParams}>
                    {{
                        #if NET5_0_OR_GREATER
                        {GenerateBindingContextPointers(i, "RoutineIter")}
                        {GenerateBindingContextPointers(i, "RoutineEach")}
                        {GenerateBindingContextPointers(i, "RoutineEachEntity")}
                        {GenerateBindingContextPointers(i, "RoutineEachIndex")}
                        {GenerateBindingContextPointers(i, "ObserverIter")}
                        {GenerateBindingContextPointers(i, "ObserverEach")}
                        {GenerateBindingContextPointers(i, "ObserverEachEntity")}
                        {GenerateBindingContextPointers(i, "ObserverEachIndex")}
                        #else
                        {GenerateBindingContextDelegates(i, "RoutineIter")}
                        {GenerateBindingContextDelegates(i, "RoutineEach")}
                        {GenerateBindingContextDelegates(i, "RoutineEachEntity")}
                        {GenerateBindingContextDelegates(i, "RoutineEachIndex")}
                        {GenerateBindingContextDelegates(i, "ObserverIter")}
                        {GenerateBindingContextDelegates(i, "ObserverEach")}
                        {GenerateBindingContextDelegates(i, "ObserverEachEntity")}
                        {GenerateBindingContextDelegates(i, "ObserverEachIndex")}
                        #endif
                    }}
                ");
            }

            return $@"
                public static unsafe partial class BindingContext
                {{
                    {GenerateBindingContextCallbacks("Routine", "RoutineIter", "IterCallback", "Iter")}
                    {GenerateBindingContextCallbacks("Routine", "RoutineEach", "EachCallback", "Each")}
                    {GenerateBindingContextCallbacks("Routine", "RoutineEachEntity", "EachEntityCallback", "Each")}
                    {GenerateBindingContextCallbacks("Routine", "RoutineEachIndex", "EachIndexCallback", "Each")}
                    {GenerateBindingContextCallbacks("Observer", "ObserverIter", "IterCallback", "Iter")}
                    {GenerateBindingContextCallbacks("Observer", "ObserverEach", "EachCallback", "Each")}
                    {GenerateBindingContextCallbacks("Observer", "ObserverEachEntity", "EachEntityCallback", "Each")}
                    {GenerateBindingContextCallbacks("Observer", "ObserverEachIndex", "EachIndexCallback", "Each")}
                }}

                {str}
            ";
        }

        public static string GenerateFilterBuilderFactoryExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string termBuilders = ConcatString(i + 1, "\n", index => $".Term<T{index}>()");
                str.AppendLine($@"
                    public FilterBuilder FilterBuilder<{typeParams}>()
                    {{
                        return new FilterBuilder(Handle){termBuilders};
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateWorldEachCallbackFunctions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public void Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        using Filter filter = Filter(FilterBuilder<{typeParams}>());
                        filter.Each(callback);   
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateWorldEachEntityCallbackFunction()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public void Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        using Filter filter = Filter(FilterBuilder<{typeParams}>());
                        filter.Each(callback);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateObserverFactoryExtensions(string delegateName, string callbackName)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public Observer Observer<{typeParams}>(
                        FilterBuilder filter = default,
                        ObserverBuilder observer = default,
                        Ecs.{delegateName}<{typeParams}>? callback = null,
                        string name = """")
                    {{
                        return new Observer().InitObserver(
                            false,
                            BindingContext<{typeParams}>.{callbackName}Pointer,
                            ref callback,
                            ref Handle,
                            ref filter,
                            ref observer,
                            ref name
                        );
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateRoutineFactoryExtensions(string delegateName, string callbackName)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public Routine Routine<{typeParams}>(
                        FilterBuilder filter = default,
                        QueryBuilder query = default,
                        RoutineBuilder routine = default,
                        Ecs.{delegateName}<{typeParams}>? callback = null,
                        string name = """")
                    {{
                        return new Routine().InitRoutine(
                            false,
                            BindingContext<{typeParams}>.{callbackName}Pointer,
                            ref callback,
                            ref Handle,
                            ref filter,
                            ref query,
                            ref routine,
                            ref name
                        );
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateIterCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"Column<T{index}> comp{index}");
                str.AppendLine($"public delegate void IterCallback<{typeParams}>(Iter it, {funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateEachCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate void EachCallback<{typeParams}>({funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateEachEntityCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate void EachEntityCallback<{typeParams}>(Entity entity, {funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateEachIndexCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate void EachIndexCallback<{typeParams}>(Iter it, int i, {funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateFindCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate bool FindCallback<{typeParams}>({funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateFindEntityCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate bool FindEntityCallback<{typeParams}>(Entity entity, {funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateFindIndexCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate bool FindIndexCallback<{typeParams}>(Iter it, int i, {funcParams});");
            }

            return str.ToString();
        }

        public static string GenerateIterInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string callbackArgs = ConcatString(i + 1, ", ", index => $"it.Field<T{index}>({index + 1})");

                str.AppendLine($@"
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterCallback<{typeParams}> callback)
                    {{
                        Macros.TableLock(iter->world, iter->table);
                        Iter it = new Iter(iter);
                        callback(it, {callbackArgs});
                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateEachInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeAssertions = ConcatString(i + 1, "\n", index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index + 1});");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        Macros.TableLock(iter->world, iter->table);

                        int count = iter->count == 0 ? 1 : iter->count;
                        
                        {typeAssertions}

                        for (int i = 0; i < count; i++)
                            callback({callbackArgs});

                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateEachEntityInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeAssertions = ConcatString(i + 1, "\n", index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index + 1});");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        ecs_world_t* world = iter->world;
                        int count = iter->count;

                        Ecs.Assert(count > 0, ""No entities returned, use Each() without the entity argument instead."");
                        {typeAssertions}

                        Macros.TableLock(iter->world, iter->table);

                        for (int i = 0; i < count; i++)
                            callback(new Entity(world, iter->entities[i]), {callbackArgs});

                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateEachIndexInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeAssertions = ConcatString(i + 1, "\n", index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index + 1});");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        int count = iter->count == 0 ? 1 : iter->count;

                        Iter it = new Iter(iter);

                        {typeAssertions}

                        Macros.TableLock(iter->world, iter->table);

                        for (int i = 0; i < count; i++)
                            callback(it, i, {callbackArgs});

                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateFindInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeAssertions = ConcatString(i + 1, "\n", index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index + 1});");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                str.AppendLine($@"
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindCallback<{typeParams}> callback)
                    {{
                        Macros.TableLock(iter->world, iter->table);

                        int count = iter->count == 0 ? 1 : iter->count;
                        Iter it = new Iter(iter);
                        Entity result = default;

                        {typeAssertions}

                        for (int i = 0; i < count; i++)
                        {{
                            if (!callback({callbackArgs}))
                                continue;

                            result = new Entity(iter->world, iter->entities[i]);
                            break;
                        }}

                        Macros.TableUnlock(iter->world, iter->table);

                        return result;
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateFindEntityInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeAssertions = ConcatString(i + 1, "\n", index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index + 1});");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                str.AppendLine($@"
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindEntityCallback<{typeParams}> callback)
                    {{
                        Macros.TableLock(iter->world, iter->table);

                        int count = iter->count;
                        ecs_world_t *world = iter->world;
                        Entity result = default;

                        Ecs.Assert(count > 0, ""No entities returned, use Find() without Entity argument"");
                        {typeAssertions}

                        for (int i = 0; i < count; i++)
                        {{
                            if (!callback(new Entity(world, iter->entities[i]), {callbackArgs}))
                                continue;

                            result = new Entity(world, iter->entities[i]);
                            break;
                        }}

                        Macros.TableUnlock(iter->world, iter->table);

                        return result;
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateFindIndexInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeAssertions = ConcatString(i + 1, "\n", index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index + 1});");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                str.AppendLine($@"
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindIndexCallback<{typeParams}> callback)
                    {{
                        Macros.TableLock(iter->world, iter->table);

                        int count = iter->count == 0 ? 1 : iter->count;
                        Iter it = new Iter(iter);
                        Entity result = default;

                        {typeAssertions}

                        for (int i = 0; i < count; i++)
                        {{
                            if (!callback(it, i, {callbackArgs}))
                                continue;

                            result = new Entity(iter->world, iter->entities[i]);
                            break;
                        }}

                        Macros.TableUnlock(iter->world, iter->table);

                        return result;
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateBindingContextPointers(int index, string callbackName)
        {
            return $@"
                internal static readonly IntPtr {callbackName}Pointer =
                    (IntPtr)(delegate* <ecs_iter_t*, void>)&BindingContext.{callbackName}<{GenerateTypeParams(index + 1)}>;
            ";
        }

        public static string GenerateBindingContextDelegates(int index, string functionName)
        {
            return $@"
                internal static readonly IntPtr {functionName}Pointer =
                    Marshal.GetFunctionPointerForDelegate({functionName}Reference = BindingContext.{functionName}<{GenerateTypeParams(index + 1)}>);
                private static readonly Ecs.IterAction {functionName}Reference;
            ";
        }

        public static string GenerateBindingContextCallbacks(string typeName, string callbackName, string delegateName, string invokerName)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    internal static void {callbackName}<{typeParams}>(ecs_iter_t* iter)
                    {{
                        {typeName}Context* context = ({typeName}Context*)iter->binding_ctx;
                        Ecs.{delegateName}<{typeParams}> callback = (Ecs.{delegateName}<{typeParams}>)context->Iterator.GcHandle.Target!;
                        Invoker.{invokerName}(iter, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateCallbackFunctions(string functionName, string delegateName, string iterName, string nextName)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public void {functionName}<{typeParams}>(Ecs.{delegateName}<{typeParams}> callback)
                    {{
                        ecs_iter_t iter = {iterName}(World, Handle);
                        while ({nextName}(&iter) == 1)
                            Invoker.{functionName}(&iter, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        public static string GenerateFindCallbackFunctions(string delegateName, string iterName, string nextName)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public Entity Find<{typeParams}>(Ecs.{delegateName}<{typeParams}> callback)
                    {{
                        ecs_iter_t iter = {iterName}(World, Handle);
                        Entity result = default;

                        while (result == 0 && {nextName}(&iter) == 1)
                            result = Invoker.Find(&iter, callback);
                        
                        if (result != 0)
                            ecs_iter_fini(&iter);

                        return result;
                    }}
                ");
            }

            return str.ToString();
        }

        public static string ConcatString(int count, string separator, Func<int, string> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                str.Append(callback(i));
                if (i < count - 1)
                    str.Append(separator);
            }

            return str.ToString();
        }

        public static string GenerateTypeParams(int num)
        {
            return ConcatString(num, ", ", index => $"T{index}");
        }
    }
}
