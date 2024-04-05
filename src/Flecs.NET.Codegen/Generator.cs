using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Flecs.NET.Codegen
{
    [Generator]
    public class Generator : IIncrementalGenerator
    {
        private const int GenericCount = 16;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
            {
                postContext.AddSource("Flecs.NET.g.cs", CodeFormatter.Format(Generate()));
            });
        }

        private static string Generate()
        {
            return $@"
                #pragma warning disable 1591
                #nullable enable              

                using System;
                using System.Runtime.CompilerServices;
                using System.Runtime.InteropServices;
                using Flecs.NET.Utilities; 
                using static Flecs.NET.Bindings.Native;

                namespace Flecs.NET.Core 
                {{
                    {GenerateWorldExtensions()}
                    {GenerateEntityExtensions()}
                    {GenerateEcsExtensions()}
                    {GenerateInvokerExtensions()}
                    {GenerateBindingContextExtensions()}
                    {GenerateQueryExtensions()}
                    {GenerateObserverExtensions()}
                    {GenerateRoutineExtensions()}
                }}
                
                #pragma warning restore 1591
            ";
        }

        private static string GenerateWorldExtensions()
        {
            return $@"
                public unsafe partial struct World
                {{
                    {GenerateIterableFactoryExtensions()}
                    {GenerateWorldEachCallbackFunctions()}
                    {GenerateWorldEachEntityCallbackFunction()}
                }}
            ";
        }

        private static string GenerateEntityExtensions()
        {
            return $@"
                public unsafe partial struct Entity
                {{
                    {GenerateEntityReadCallbacks()}
                    {GenerateEntityWriteCallbacks()} 
                    {GenerateEntityEnsureCallbacks()}
                }}
            ";
        }

        private static string GenerateEcsExtensions()
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
                    {GenerateInvokeReadCallbackDelegates()}
                    {GenerateInvokeWriteCallbackDelegates()}
                    {GenerateInvokeEnsureCallbackDelegates()}
                }}
            ";
        }

        private static string GenerateInvokerExtensions()
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
                    {GenerateGetPointers()}
                    {GenerateEnsurePointers()}
                    {GenerateReadInvokers()}
                    {GenerateWriteInvokers()}
                    {GenerateEnsureInvokers()}
                }}
            ";
        }

        private static string GenerateQueryExtensions()
        {
            return $@"
                public unsafe partial struct Query
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

        private static string GenerateObserverExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public Observer Iter<{typeParams}>(Ecs.IterCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.ObserverIterPointer, false);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.ObserverEachPointer, false);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.ObserverEachEntityPointer, false);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachIndexCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.ObserverEachIndexPointer, false);
                    }}
                ");
            }

            return $@"
                public partial struct ObserverBuilder
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateRoutineExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public Routine Iter<{typeParams}>(Ecs.IterCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.RoutineIterPointer, false);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.RoutineEachPointer, false);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.RoutineEachEntityPointer, false);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachIndexCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.RoutineEachIndexPointer, false);
                    }}
                ");
            }

            return $@"
                public partial struct RoutineBuilder
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateBindingContextExtensions()
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

        private static string GenerateIterableFactoryExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string termBuilders = ConcatString(i + 1, "\n", index => $".With<T{index}>()");
                str.AppendLine($@"
                    public AlertBuilder AlertBuilder<{typeParams}>(string? name = null)
                    {{
                        return new AlertBuilder(Handle, name){termBuilders};
                    }}

                    public Alert Alert<{typeParams}>(string? name = null)
                    {{
                        return AlertBuilder<{typeParams}>(name).Build();
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>(string? name = null)
                    {{
                        return new QueryBuilder(Handle, name){termBuilders};
                    }}

                    public Query Query<{typeParams}>(string? name = null)
                    {{
                        return QueryBuilder<{typeParams}>(name).Build();
                    }}

                    public RoutineBuilder Routine<{typeParams}>(string? name = null)
                    {{
                        return new RoutineBuilder(Handle, name){termBuilders};
                    }}

                    public ObserverBuilder Observer<{typeParams}>(string? name = null)
                    {{
                        return new ObserverBuilder(Handle, name){termBuilders};
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWorldEachCallbackFunctions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public void Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);   
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWorldEachEntityCallbackFunction()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public void Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityReadCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public bool Read<{typeParams}>(Ecs.InvokeReadCallback<{typeParams}> callback)
                    {{
                        return Invoker.InvokeRead(World, Id, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityWriteCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public bool Write<{typeParams}>(Ecs.InvokeWriteCallback<{typeParams}> callback)
                    {{
                        return Invoker.InvokeWrite(World, Id, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityEnsureCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public ref Entity Ensure<{typeParams}>(Ecs.InvokeEnsureCallback<{typeParams}> callback)
                    {{
                        Invoker.InvokeEnsure(World, Id, callback);
                        return ref this;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateIterCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"Field<T{index}> field{index}");
                str.AppendLine($"public delegate void IterCallback<{typeParams}>(Iter it, {funcParams});");
            }

            return str.ToString();
        }

        private static string GenerateEachCallbackDelegates()
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

        private static string GenerateEachEntityCallbackDelegates()
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

        private static string GenerateEachIndexCallbackDelegates()
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

        private static string GenerateFindCallbackDelegates()
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

        private static string GenerateFindEntityCallbackDelegates()
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

        private static string GenerateFindIndexCallbackDelegates()
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

        private static string GenerateInvokeReadCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"in T{index} comp{index}");
                str.AppendLine($"public delegate void InvokeReadCallback<{typeParams}>({funcParams});");
            }

            return str.ToString();
        }

        private static string GenerateInvokeWriteCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate void InvokeWriteCallback<{typeParams}>({funcParams});");
            }

            return str.ToString();
        }

        private static string GenerateInvokeEnsureCallbackDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string funcParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                str.AppendLine($"public delegate void InvokeEnsureCallback<{typeParams}>({funcParams});");
            }

            return str.ToString();
        }

        private static string GenerateIterInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string callbackArgs = ConcatString(i + 1, ", ", index => $"it.Field<T{index}>({index})");

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

        private static string GenerateEachInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string isSelfBools = ConcatString(i + 1, "\n",
                    index => $"int t{index}IsSelf = (iter->sources == null || iter->sources[{index}] == 0) ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"void* t{index}Pointer = iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(t{index}Pointer, i * t{index}IsSelf)");

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        iter->flags |= EcsIterCppEach;

                        Macros.TableLock(iter->world, iter->table);

                        int count = iter->count == 0 ? 1 : iter->count;
                        
                        {typeAssertions}
                        {isSelfBools}
                        {pointers}

                        for (int i = 0; i < count; i++)
                            callback({callbackArgs});

                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEachEntityInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string isSelfBools = ConcatString(i + 1, "\n",
                    index => $"int t{index}IsSelf = (iter->sources == null || iter->sources[{index}] == 0) ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"void* t{index}Pointer = iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(t{index}Pointer, i * t{index}IsSelf)");

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        iter->flags |= EcsIterCppEach;

                        ecs_world_t* world = iter->world;
                        int count = iter->count;

                        Ecs.Assert(count > 0, ""No entities returned, use Each() without the entity argument instead."");
                        {typeAssertions}
                        {isSelfBools}
                        {pointers}

                        Macros.TableLock(iter->world, iter->table);

                        for (int i = 0; i < count; i++)
                            callback(new Entity(world, iter->entities[i]), {callbackArgs});

                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEachIndexInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string isSelfBools = ConcatString(i + 1, "\n",
                    index => $"int t{index}IsSelf = (iter->sources == null || iter->sources[{index}] == 0) ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"void* t{index}Pointer = iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(t{index}Pointer, i * t{index}IsSelf)");

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        iter->flags |= EcsIterCppEach;

                        int count = iter->count == 0 ? 1 : iter->count;

                        Iter it = new Iter(iter);

                        {typeAssertions}
                        {isSelfBools}
                        {pointers}

                        Macros.TableLock(iter->world, iter->table);

                        for (int i = 0; i < count; i++)
                            callback(it, i, {callbackArgs});

                        Macros.TableUnlock(iter->world, iter->table);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateFindInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

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

        private static string GenerateFindEntityInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

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

        private static string GenerateFindIndexInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

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

        private static string GenerateGetPointers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string columnIndexes = ConcatString(i + 1, "\n",
                    index => $"int t{index} = ecs_table_get_column_index(realWorld, table, Type<T{index}>.Id(world));");

                string condition = ConcatString(i + 1, " || ",
                    index => $"t{index} == -1");

                string typeIds = ConcatString(i + 1, "\n",
                    index => $"ptrs[{index}] = ecs_record_get_column(r, t{index}, default);");

                str.AppendLine($@"
                    internal static bool GetPointers<{typeParams}>(ecs_world_t* world, ecs_record_t* r, ecs_table_t* table, void** ptrs)
                    {{
                        Ecs.Assert(table != null, nameof(ECS_INTERNAL_ERROR));

                        if (ecs_table_column_count(table) == 0)
                            return false;

                        ecs_world_t* realWorld = ecs_get_world(world);

                        {columnIndexes}

                        if ({condition})
                            return false;

                        {typeIds}

                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEnsurePointers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeIds = ConcatString(i + 1, "\n",
                    index => $"ptrs[{index}] = ecs_ensure_id(world, e, Type<T{index}>.Id(world));");

                str.AppendLine($@"
                    internal static bool EnsurePointers<{typeParams}>(ecs_world_t* world, ulong e, void** ptrs)
                    {{
                        {typeIds}
                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateReadInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"in Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                str.AppendLine($@"
                    internal static bool InvokeRead<{typeParams}>(ecs_world_t* world, ulong e, Ecs.InvokeReadCallback<{typeParams}> callback)
                    {{
                        ecs_record_t* r = ecs_read_begin(world, e);

                        if (r == null)
                            return false;

                        ecs_table_t *table = r->table;

                        if (table == null)
                            return false;

                        void** ptrs = stackalloc void*[{i + 1}];
                        bool hasComponents = GetPointers<{typeParams}>(world, r, table, ptrs);

                        if (hasComponents)
                            callback({callbackArgs});

                        ecs_read_end(r);

                        return hasComponents;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWriteInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                str.AppendLine($@"
                    internal static bool InvokeWrite<{typeParams}>(ecs_world_t* world, ulong e, Ecs.InvokeWriteCallback<{typeParams}> callback)
                    {{
                        ecs_record_t* r = ecs_write_begin(world, e);

                        if (r == null)
                            return false;

                        ecs_table_t *table = r->table;

                        if (table == null)
                            return false;

                        void** ptrs = stackalloc void*[{i + 1}];
                        bool hasComponents = GetPointers<{typeParams}>(world, r, table, ptrs);

                        if (hasComponents)
                            callback({callbackArgs});

                        ecs_write_end(r);

                        return hasComponents;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEnsureInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string addedIds = ConcatString(i + 1, "\n", index => $@"
                    next = ecs_table_add_id(world, prev, Type<T{index}>.Id(world));
                    if (prev != next) added[elem++] = Type<T{index}>.Id(world);
                    prev = next;
                ");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                string modified = ConcatString(i + 1, "\n",
                    index => $"ecs_modified_id(world, id, Type<T{index}>.Id(world));");

                str.AppendLine($@"
                    internal static bool InvokeEnsure<{typeParams}>(ecs_world_t* world, ulong id, Ecs.InvokeEnsureCallback<{typeParams}> callback)
                    {{
                        World w = new World(world);

                        void** ptrs = stackalloc void*[{i + 1}];
                        ecs_table_t* table = null;

                        if (!w.IsDeferred())
                        {{
                            Ecs.Assert(!w.IsStage(), nameof(ECS_INVALID_PARAMETER));

                            ecs_record_t* r = ecs_record_find(world, id);
                            if (r != null)
                                table = r->table;

                            ecs_table_t* prev = table;
                            ecs_table_t* next;
                            int elem = 0;
                            ulong* added = stackalloc ulong[{i + 1}];

                            {addedIds}

                            if (table != next)
                            {{
                                ecs_type_t ids = default;
                                ids.array = added;
                                ids.count = elem;
                                ecs_commit(world, id, r, next, &ids, null);
                                table = next;
                            }}

                            if (!GetPointers<{typeParams}>(w, r, table, ptrs))
                                Ecs.Error(nameof(ECS_INTERNAL_ERROR));

                            Macros.TableLock(world, table);
                        }}
                        else
                        {{
                            EnsurePointers<{typeParams}>(world, id, ptrs);
                        }}

                        callback({callbackArgs});

                        if (!w.IsDeferred())
                            Macros.TableUnlock(world, table);

                        {modified}

                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateBindingContextPointers(int index, string callbackName)
        {
            return $@"
                internal static readonly IntPtr {callbackName}Pointer =
                    (IntPtr)(delegate* <ecs_iter_t*, void>)&BindingContext.{callbackName}<{GenerateTypeParams(index + 1)}>;
            ";
        }

        private static string GenerateBindingContextDelegates(int index, string functionName)
        {
            return $@"
                internal static readonly IntPtr {functionName}Pointer =
                    Marshal.GetFunctionPointerForDelegate({functionName}Reference = BindingContext.{functionName}<{GenerateTypeParams(index + 1)}>);
                private static readonly Ecs.IterAction {functionName}Reference;
            ";
        }

        private static string GenerateBindingContextCallbacks(string typeName, string callbackName, string delegateName,
            string invokerName)
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

        private static string GenerateCallbackFunctions(string functionName, string delegateName, string iterName,
            string nextName)
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

        private static string GenerateFindCallbackFunctions(string delegateName, string iterName, string nextName)
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

        private static string ConcatString(int count, string separator, Func<int, string> callback)
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

        private static string GenerateTypeParams(int num)
        {
            return ConcatString(num, ", ", index => $"T{index}");
        }
    }
}
