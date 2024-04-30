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
                    {GenerateIterIterableExtensions()}
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
                public static unsafe partial class Ecs 
                {{
                    {GenerateDelegates()}
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
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldTypeArgs = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string refTypeArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.Append($@"
                    {GenerateQueryCallbackFunctions($"Iter<{typeParams}>", $"Ecs.IterCallback<{typeParams}>", $"delegate*<Iter, {fieldTypeArgs}, void>", "ecs_query_next")}
                    {GenerateQueryCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachCallback<{typeParams}>", $"delegate*<{refTypeArgs}, void>", "ecs_query_next_instanced")} 
                    {GenerateQueryCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachEntityCallback<{typeParams}>", $"delegate*<Entity, {refTypeArgs}, void>", "ecs_query_next_instanced")} 
                    {GenerateQueryCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refTypeArgs}, void>", "ecs_query_next_instanced")}
                    {GenerateQueryFindCallbackFunctions(typeParams, $"Ecs.FindCallback<{typeParams}>", $"delegate*<{refTypeArgs}, bool>")}
                    {GenerateQueryFindCallbackFunctions(typeParams, $"Ecs.FindEntityCallback<{typeParams}>", $"delegate*<Entity, {refTypeArgs}, bool>")}
                    {GenerateQueryFindCallbackFunctions(typeParams, $"Ecs.FindIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refTypeArgs}, bool>")}
                ");
            }

            return $@"
                public unsafe partial struct Query
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateIterIterableExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldTypeArgs = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string refTypeArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.Append($@"
                    {GenerateIterIterableCallbackFunctions($"Iter<{typeParams}>", $"Ecs.IterCallback<{typeParams}>", $"delegate*<Iter, {fieldTypeArgs}, void>", "ecs_query_next")}
                    {GenerateIterIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachCallback<{typeParams}>", $"delegate*<{refTypeArgs}, void>", "ecs_query_next_instanced")} 
                    {GenerateIterIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachEntityCallback<{typeParams}>", $"delegate*<Entity, {refTypeArgs}, void>", "ecs_query_next_instanced")} 
                    {GenerateIterIterableCallbackFunctions($"Each<{typeParams}>", $"Ecs.EachIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refTypeArgs}, void>", "ecs_query_next_instanced")}
                    {GenerateIterIterableFindCallbackFunctions(typeParams, $"Ecs.FindCallback<{typeParams}>", $"delegate*<{refTypeArgs}, bool>")}
                    {GenerateIterIterableFindCallbackFunctions(typeParams, $"Ecs.FindEntityCallback<{typeParams}>", $"delegate*<Entity, {refTypeArgs}, bool>")}
                    {GenerateIterIterableFindCallbackFunctions(typeParams, $"Ecs.FindIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refTypeArgs}, bool>")}
                ");
            }

            return $@"
                public unsafe partial struct IterIterable
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateObserverExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldTypeArgs = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string refTypeArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public Observer Iter<{typeParams}>(Ecs.IterCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachIndexCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                #if NET5_0_OR_GREATER
                    public Observer Iter<{typeParams}>(delegate*<Iter, {fieldTypeArgs}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(delegate*<{refTypeArgs}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(delegate*<Entity, {refTypeArgs}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(delegate*<Iter, int, {refTypeArgs}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}
                #endif
                ");
            }

            return $@"
                public unsafe partial struct ObserverBuilder
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
                string fieldTypeArgs = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string refTypeArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public Routine Iter<{typeParams}>(Ecs.IterCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachIndexCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                #if NET5_0_OR_GREATER
                    public Routine Iter<{typeParams}>(delegate*<Iter, {fieldTypeArgs}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(delegate*<{refTypeArgs}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(delegate*<Entity, {refTypeArgs}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(delegate*<Iter, int, {refTypeArgs}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}
                #endif
                ");
            }

            return $@"
                public unsafe partial struct RoutineBuilder
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
                string fieldTypeArgs = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string refTypeArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    internal static unsafe partial class BindingContext<{typeParams}>
                    {{
                        #if NET5_0_OR_GREATER
                        {GenerateBindingContextPointers("IterCallback")}
                        {GenerateBindingContextPointers("EachCallback")}
                        {GenerateBindingContextPointers("EachEntityCallback")}
                        {GenerateBindingContextPointers("EachIndexCallback")}
                        #else
                        {GenerateBindingContextDelegates("IterCallback")}
                        {GenerateBindingContextDelegates("EachCallback")}
                        {GenerateBindingContextDelegates("EachEntityCallback")}
                        {GenerateBindingContextDelegates("EachIndexCallback")}
                        #endif
                        {GenerateBindingContextCallbacks("IterCallback", $"Ecs.IterCallback<{typeParams}>", $"delegate*<Iter, {fieldTypeArgs}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("EachCallback", $"Ecs.EachCallback<{typeParams}>", $"delegate*<{refTypeArgs}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachEntityCallback", $"Ecs.EachEntityCallback<{typeParams}>", $"delegate*<Entity, {refTypeArgs}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachIndexCallback", $"Ecs.EachIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refTypeArgs}, void>", "Each")} 
                    }}
                ");
            }

            return str.ToString();
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

        private static string GenerateDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}> field{index}");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                string inParams = ConcatString(i + 1, ", ", index => $"in T{index} comp{index}");

                str.AppendLine($"public delegate void IterCallback<{typeParams}>(Iter it, {fieldParams});");
                str.AppendLine($"public delegate void EachCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate void EachEntityCallback<{typeParams}>(Entity entity, {refParams});");
                str.AppendLine($"public delegate void EachIndexCallback<{typeParams}>(Iter it, int i, {refParams});");

                str.AppendLine($"public delegate bool FindCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate bool FindEntityCallback<{typeParams}>(Entity entity, {refParams});");
                str.AppendLine($"public delegate bool FindIndexCallback<{typeParams}>(Iter it, int i, {refParams});");

                str.AppendLine($"public delegate void InvokeReadCallback<{typeParams}>({inParams});");
                str.AppendLine($"public delegate void InvokeWriteCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate void InvokeEnsureCallback<{typeParams}>({refParams});");
            }

            return str.ToString();
        }

        private static string GenerateIterInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string functionPointerParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string callbackArgs = ConcatString(i + 1, ", ", index => $"it.Field<T{index}>({index})");

                string methodBody = $@"
                    Macros.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {callbackArgs});
                    Macros.TableUnlock(iter->world, iter->table);
                ";

                str.AppendLine($@"
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {functionPointerParams}, void> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

                string functionPointerParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string isSelfBools = ConcatString(i + 1, "\n",
                    index => $"int t{index}IsSelf = (iter->sources == null || iter->sources[{index}] == 0) ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"void* t{index}Pointer = iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(t{index}Pointer, i * t{index}IsSelf)");

                string methodBody = $@"
                    iter->flags |= EcsIterCppEach;

                    Macros.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 ? 1 : iter->count;
                    
                    {typeAssertions}
                    {isSelfBools}
                    {pointers}

                    for (int i = 0; i < count; i++)
                        callback({callbackArgs});

                    Macros.TableUnlock(iter->world, iter->table);
                ";

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<{functionPointerParams}, void> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

                string functionPointerParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string isSelfBools = ConcatString(i + 1, "\n",
                    index => $"int t{index}IsSelf = (iter->sources == null || iter->sources[{index}] == 0) ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"void* t{index}Pointer = iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(t{index}Pointer, i * t{index}IsSelf)");

                string methodBody = $@"
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
                ";

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {functionPointerParams}, void> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

                string functionPointerParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string isSelfBools = ConcatString(i + 1, "\n",
                    index => $"int t{index}IsSelf = (iter->sources == null || iter->sources[{index}] == 0) ? 1 : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"void* t{index}Pointer = iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(t{index}Pointer, i * t{index}IsSelf)");

                string methodBody = $@"
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
                ";

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {functionPointerParams}, void> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

                string functionPointerParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                string methodBody = $@"
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
                ";

                str.AppendLine($@"
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}
                #if NET5_0_OR_GREATER
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<{functionPointerParams}, bool> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

                string functionPointerParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                string methodBody = $@"
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
                ";

                str.AppendLine($@"
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindEntityCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {functionPointerParams}, bool> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

                string functionPointerParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string typeAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(iter->ptrs[{index}], i)");

                string methodBody = $@"
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
                ";

                str.AppendLine($@"
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindIndexCallback<{typeParams}> callback)
                    {{
                        {methodBody}
                    }}
                #if NET5_0_OR_GREATER
                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {functionPointerParams}, bool> callback)
                    {{
                        {methodBody}
                    }}
                #endif
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

        private static string GenerateBindingContextPointers(string callbackName)
        {
            return $@"
                internal static readonly IntPtr {callbackName}Pointer =
                    (IntPtr)(delegate* <ecs_iter_t*, void>)&{callbackName};
            ";
        }

        private static string GenerateBindingContextDelegates(string functionName)
        {
            return $@"
                internal static readonly IntPtr {functionName}Pointer =
                    Marshal.GetFunctionPointerForDelegate({functionName}Reference = {functionName});
                private static readonly Ecs.IterAction {functionName}Reference;
            ";
        }

        private static string GenerateBindingContextCallbacks(
            string callbackName,
            string delegateName,
            string functionPointerName,
            string invokerName)
        {
            return $@"
                internal static void {callbackName}(ecs_iter_t* iter)
                {{
                    BindingContext.RunIterContext* context = (BindingContext.RunIterContext*)iter->binding_ctx;
            #if NET5_0_OR_GREATER
                    if (context->Iterator.GcHandle == default)
                    {{
                        Invoker.{invokerName}(iter, ({functionPointerName})context->Iterator.Function);
                        return;
                    }}
            #endif
                    {delegateName} callback = ({delegateName})context->Iterator.GcHandle.Target!;
                    Invoker.{invokerName}(iter, callback);
                }}
            ";
        }

        private static string GenerateQueryCallbackFunctions(
            string functionName,
            string delegateName,
            string functionPointerName,
            string nextName)
        {
            return $@"
                public void {functionName}({delegateName} callback)
                {{
                    {functionName}(World, callback);
                }}

                public void {functionName}(Entity e, {delegateName} callback)
                {{
                    {functionName}(e.World, callback);
                }}

                public void {functionName}(Iter it, {delegateName} callback)
                {{
                    {functionName}(it.World(), callback);
                }}

                public void {functionName}(World world, {delegateName} callback)
                {{
                    ecs_iter_t iter = ecs_query_iter(world, Handle);
                    while ({nextName}(&iter) == 1)
                        Invoker.{functionName}(&iter, callback);
                }}

                #if NET5_0_OR_GREATER
                public void {functionName}({functionPointerName} callback)
                {{
                    {functionName}(World, callback);
                }}

                public void {functionName}(Entity e, {functionPointerName} callback)
                {{
                    {functionName}(e.World, callback);
                }}

                public void {functionName}(Iter it, {functionPointerName} callback)
                {{
                    {functionName}(it.World(), callback);
                }}

                public void {functionName}(World world, {functionPointerName} callback)
                {{
                    ecs_iter_t iter = ecs_query_iter(world, Handle);
                    while ({nextName}(&iter) == 1)
                        Invoker.{functionName}(&iter, callback);
                }}
                #endif
            ";
        }

        private static string GenerateQueryFindCallbackFunctions(string typeParams, string delegateName, string functionPointerName)
        {
            string methodBody = $@"
                ecs_iter_t iter = ecs_query_iter(World, Handle);
                Entity result = default;

                while (result == 0 && ecs_query_next_instanced(&iter) == 1)
                    result = Invoker.Find(&iter, callback);
                
                if (result != 0)
                    ecs_iter_fini(&iter);

                return result;
            ";

            return $@"
                public Entity Find<{typeParams}>({delegateName} callback)
                {{
                    {methodBody}
                }}
            #if NET5_0_OR_GREATER
                public Entity Find<{typeParams}>({functionPointerName} callback)
                {{
                    {methodBody}
                }}
            #endif
            ";
        }

        private static string GenerateIterIterableFindCallbackFunctions(string typeParams, string delegateName, string functionPointerName)
        {
            string methodBody = $@"
                ecs_iter_t iter = _iter;
                Entity result = default;

                while (result == 0 && ecs_query_next_instanced(&iter) == 1)
                    result = Invoker.Find(&iter, callback);
                
                if (result != 0)
                    ecs_iter_fini(&iter);

                return result;
            ";

            return $@"
                public Entity Find<{typeParams}>({delegateName} callback)
                {{
                    {methodBody}
                }}
            #if NET5_0_OR_GREATER
                public Entity Find<{typeParams}>({functionPointerName} callback)
                {{
                    {methodBody}
                }}
            #endif
            ";
        }

        private static string GenerateIterIterableCallbackFunctions(
            string functionName,
            string delegateName,
            string functionPointerName,
            string nextName)
        {
            return $@"
                public void {functionName}({delegateName} callback)
                {{
                    {functionName}(_iter.world, callback);
                }}

                public void {functionName}(Entity e, {delegateName} callback)
                {{
                    {functionName}(e.World, callback);
                }}

                public void {functionName}(Iter it, {delegateName} callback)
                {{
                    {functionName}(it.World(), callback);
                }}

                public void {functionName}(World world, {delegateName} callback)
                {{
                    ecs_iter_t iter = _iter;
                    iter.world = world;
                    while ({nextName}(&iter) == 1)
                        Invoker.{functionName}(&iter, callback);
                }}

                #if NET5_0_OR_GREATER
                public void {functionName}({functionPointerName} callback)
                {{
                    {functionName}(_iter.world, callback);
                }}

                public void {functionName}(Entity e, {functionPointerName} callback)
                {{
                    {functionName}(e.World, callback);
                }}

                public void {functionName}(Iter it, {functionPointerName} callback)
                {{
                    {functionName}(it.World(), callback);
                }}

                public void {functionName}(World world, {functionPointerName} callback)
                {{
                    ecs_iter_t iter = _iter;
                    iter.world = world;
                    while ({nextName}(&iter) == 1)
                        Invoker.{functionName}(&iter, callback);
                }}
                #endif
            ";
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
