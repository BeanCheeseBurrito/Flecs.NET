using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class Invoker : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
            {
                // Iterator Invokers
                Generator.AddSource(postContext, $"Iter/Iterator/T{i + 1}.g.cs", GenerateIterIteratorInvokers(i));
                Generator.AddSource(postContext, $"Each/Iterator/T{i + 1}.g.cs", GenerateEachIteratorInvokers(i));
                Generator.AddSource(postContext, $"Find/Iterator/T{i + 1}.g.cs", GenerateFindIteratorInvokers(i));

                // Iterable Invokers
                Generator.AddSource(postContext, $"Iter/Iterable/T{i + 1}.g.cs", GenerateIterIterableInvokers(i));
                Generator.AddSource(postContext, $"Each/Iterable/T{i + 1}.g.cs", GenerateEachIterableInvokers(i));
                Generator.AddSource(postContext, $"Find/Iterable/T{i + 1}.g.cs", GenerateFindIterableInvokers(i));

                // Fetch Component Invokers
                Generator.AddSource(postContext, $"FetchComponents/T{i + 1}.g.cs", GenerateFetchComponentInvokers(i));
            }
        });
    }

    private static string GenerateIterIteratorInvokers(int i)
    {
        IEnumerable<string> invokers = Generator.CallbacksIter.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over an Iter object using the provided .Iter callback.
            /// </summary>
            /// <param name="it">The iter object.</param>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            public static void Iter<{{Generator.TypeParameters[i]}}>(Iter it, {{Generator.GetCallbackType(callback, i)}} callback)
            {
                Ecs.TableLock(it);
                callback(it, {{Generator.GetCallbackArguments(callback, i)}});
                Ecs.TableUnlock(it);
            }
        """);

        return $$"""
        using System;
        using static Flecs.NET.Bindings.flecs;

        namespace Flecs.NET.Core;
        
        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, invokers)}}
        }
        """;
    }

    private static string GenerateEachIteratorInvokers(int i)
    {
        IEnumerable<string> invokers = Generator.CallbacksEach.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over an Iter object using the provided .Each callback.
            /// </summary>
            /// <param name="it">The iter object.</param>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            public static void Each<{{Generator.TypeParameters[i]}}>(Iter it, {{Generator.GetCallbackType(callback, i)}} callback)
            {
                {{Generator.GetCallbackCountVariable(callback)}}
                
                {{Generator.FieldDataVariables[i]}}
                IterationTechnique flags = it.GetIterationTechnique({{i + 1}});
                    
                Ecs.TableLock(it);
                
                if ({{Generator.ContainsReferenceTypes[i]}})
                {
                    if (flags == IterationTechnique.None)
                        {{IterationTechnique.Managed}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Shared)
                        {{IterationTechnique.SharedManaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Sparse)
                        {{IterationTechnique.SparseManaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == (IterationTechnique.Sparse | IterationTechnique.Shared))
                        {{IterationTechnique.SparseSharedManaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                }
                else
                {
                   if (flags == IterationTechnique.None)
                        {{IterationTechnique.Unmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Shared)
                        {{IterationTechnique.SharedUnmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Sparse)
                        {{IterationTechnique.SparseUnmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == (IterationTechnique.Sparse | IterationTechnique.Shared))
                        {{IterationTechnique.SparseSharedUnmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                }
                    
                Ecs.TableUnlock(it);
                
                return;
                
        {{GenerateEachInvokerIterators(callback, i)}}
            }
        """);

        return $$"""
        using System;
        using System.Runtime.CompilerServices;
        using Flecs.NET.Utilities;
        using static Flecs.NET.Bindings.flecs;

        namespace Flecs.NET.Core;
        
        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, invokers)}}
        }
        """;
    }

    private static string GenerateFindIteratorInvokers(int i)
    {
        IEnumerable<string> invokers = Generator.CallbacksFind.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over an Iter object using the provided .Find callback.
            /// </summary>
            /// <param name="it">The iter object.</param>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            public static Entity Find<{{Generator.TypeParameters[i]}}>(Iter it, {{Generator.GetCallbackType(callback, i)}} callback)
            {
                {{Generator.GetCallbackCountVariable(callback)}}
                
                {{Generator.FieldDataVariables[i]}}
                IterationTechnique flags = it.GetIterationTechnique({{i + 1}});
                    
                Ecs.TableLock(it);
                
                Entity result = default;
                
                if ({{Generator.ContainsReferenceTypes[i]}})
                {
                    if (flags == IterationTechnique.None)
                        result = {{IterationTechnique.Managed}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Shared)
                        result = {{IterationTechnique.SharedManaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Sparse)
                        result = {{IterationTechnique.SparseManaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == (IterationTechnique.Sparse | IterationTechnique.Shared))
                        result = {{IterationTechnique.SparseSharedManaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                }
                else
                {
                   if (flags == IterationTechnique.None)
                        result = {{IterationTechnique.Unmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Shared)
                        result = {{IterationTechnique.SharedUnmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == IterationTechnique.Sparse)
                        result = {{IterationTechnique.SparseUnmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                    else if (flags == (IterationTechnique.Sparse | IterationTechnique.Shared))
                        result = {{IterationTechnique.SparseSharedUnmanaged}}(it, count, callback, {{Generator.FieldDataRefs[i]}});
                }
                    
                Ecs.TableUnlock(it);
                
                return result;
                
        {{GenerateFindInvokerIterators(callback, i)}}
            }
        """);

        return $$"""
        using System;
        using System.Runtime.CompilerServices;
        using Flecs.NET.Utilities;
        using static Flecs.NET.Bindings.flecs;

        namespace Flecs.NET.Core;
        
        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, invokers)}}
        }
        """;
    }

    private static string GenerateEachInvokerIterators(Callback callback, int i)
    {
        IEnumerable<string> invokerIterators = Enum.GetValues(typeof(IterationTechnique)).Cast<IterationTechnique>().Select((IterationTechnique iterationTechnique) => $$"""
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                static void {{iterationTechnique}}(Iter it, int count, {{Generator.GetCallbackType(callback, i)}} callback, {{Generator.FieldDataParameters[i]}})
                {
                    for (int i = 0; i < count; i++)
                        callback({{Generator.GetCallbackArguments(callback, iterationTechnique, i)}});
                }
        """);

        return string.Join(Separator.DoubleNewLine, invokerIterators);
    }

    private static string GenerateFindInvokerIterators(Callback callback, int i)
    {
        IEnumerable<string> invokerIterators = Enum.GetValues(typeof(IterationTechnique)).Cast<IterationTechnique>().Select((IterationTechnique iterationTechnique) => $$"""
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                static Entity {{iterationTechnique}}(Iter it, int count, {{Generator.GetCallbackType(callback, i)}} callback, {{Generator.FieldDataParameters[i]}})
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (callback({{Generator.GetCallbackArguments(callback, iterationTechnique, i)}}))
                            return new Entity(it.Handle->world, it.Handle->entities[i]);
                    }
                        
                    return default;
                }
        """);

        return string.Join(Separator.DoubleNewLine, invokerIterators);
    }

    private static string GenerateIterIterableInvokers(int i)
    {
        IEnumerable<string> invokers = Generator.CallbacksIter.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over an IIterableBase object using the provided .{{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="iterable">The iterable object.</param>
            /// <param name="callback">The callback.</param>
            /// <typeparam name="T">The iterable type.</typeparam>
            /// {{Generator.XmlTypeParameters[i]}}
            public static void {{Generator.GetInvokerName(callback)}}<T, {{Generator.TypeParameters[i]}}>(ref T iterable, {{Generator.GetCallbackType(callback, i)}} callback)
                where T : unmanaged, IIterableBase
            {
                ecs_iter_t iter = iterable.GetIter();
                while (iterable.GetNext(&iter))
                    {{Generator.GetInvokerName(callback)}}(&iter, callback);
            }
        """);

        return $$"""
        using System;
        using static Flecs.NET.Bindings.flecs;

        namespace Flecs.NET.Core;
        
        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, invokers)}}
        }
        """;
    }

    private static string GenerateEachIterableInvokers(int i)
    {
        IEnumerable<string> invokers = Generator.CallbacksEach.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over an IIterableBase object using the provided .{{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="iterable">The iterable object.</param>
            /// <param name="callback">The callback.</param>
            /// <typeparam name="T">The iterable type.</typeparam>
            /// {{Generator.XmlTypeParameters[i]}}
            public static void {{Generator.GetInvokerName(callback)}}<T, {{Generator.TypeParameters[i]}}>(ref T iterable, {{Generator.GetCallbackType(callback, i)}} callback)
                where T : unmanaged, IIterableBase
            {
                ecs_iter_t iter = iterable.GetIter();
                while (iterable.GetNext(&iter))
                    {{Generator.GetInvokerName(callback)}}(&iter, callback);
            }
        """);

        return $$"""
        using System;
        using static Flecs.NET.Bindings.flecs;

        namespace Flecs.NET.Core;
        
        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, invokers)}}
        }
        """;
    }

    private static string GenerateFindIterableInvokers(int i)
    {
        IEnumerable<string> invokers = Generator.CallbacksFind.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over an IIterableBase object using the provided .{{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="iterable">The iterable object.</param>
            /// <param name="callback">The callback.</param>
            /// <typeparam name="T">The iterable type.</typeparam>
            /// {{Generator.XmlTypeParameters[i]}}
            public static Entity {{Generator.GetInvokerName(callback)}}<T, {{Generator.TypeParameters[i]}}>(ref T iterable, {{Generator.GetCallbackType(callback, i)}} callback)
                where T : unmanaged, IIterableBase
            {
                Entity result = default;
        
                ecs_iter_t iter = iterable.GetIter();
                while (result == 0 && iterable.GetNext(&iter))
                    result = {{Generator.GetInvokerName(callback)}}(&iter, callback);
                    
                if (result != 0)
                    ecs_iter_fini(&iter);
                
                return result;
            }
        """);

        return $$"""
        using System;
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;

        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, invokers)}}
        }
        """;
    }

    [SuppressMessage("Globalization", "CA1304:Specify CultureInfo")]
    [SuppressMessage("Globalization", "CA1311:Specify a culture or use an invariant version")]
    public static string GenerateFetchComponentInvokers(int i)
    {
        IEnumerable<string> readAndWrite = Generator.CallbacksReadAndWrite.Select((Callback callback) => $$"""
            /// <summary>
            ///     Invokes the provided {{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="world">The world.</param>
            /// <param name="entity">The entity.</param>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            /// <returns>True if the entity has the specified components.</returns>
            public static bool {{Generator.GetInvokerName(callback)}}<{{Generator.TypeParameters[i]}}>(ecs_world_t* world, ulong entity, {{Generator.GetCallbackType(callback, i)}} callback)
            {
                ecs_record_t* record = ecs_{{Generator.GetInvokerName(callback).ToLower()}}_begin(world, entity);

                if (record == null)
                    return false;

                ecs_table_t *table = record->table;

                if (table == null)
                    return false;

                void** pointers = stackalloc void*[{{i + 1}}];
                bool hasComponents = Ecs.GetPointers<{{Generator.TypeParameters[i]}}>(world, entity, record, table, pointers);

                if (hasComponents)
                    callback({{Generator.GetCallbackArguments(callback, i)}});

                ecs_{{Generator.GetInvokerName(callback).ToLower()}}_end(record);

                return hasComponents;
            }
        """);

        IEnumerable<string> insert = Generator.CallbacksInsert.Select((Callback callback) => $$"""
            /// <summary>
            ///     Invokes the provided {{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="world">The world.</param>
            /// <param name="entity">The entity.</param>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            /// <returns></returns>
            public static bool Insert<{{Generator.TypeParameters[i]}}>(World world, ulong entity, {{Generator.GetCallbackType(callback, i)}} callback)
            {
                {{Generator.IdsArray[i]}}
                void** pointers = stackalloc void*[{{i + 1}}];
                
                ecs_table_t* table = null;
            
                if (!world.IsDeferred())
                {
                    Ecs.Assert(!world.IsStage(), nameof(ECS_INVALID_PARAMETER));
            
                    ecs_record_t* record = ecs_record_find(world, entity);
                    
                    if (record != null)
                        table = record->table;
            
                    ecs_table_t* prev = table;
                    ecs_table_t* next = null;
                    
                    ulong* added = stackalloc ulong[{{i + 1}}];
                    int addedCount = 0;
                    
                    for (int i = 0; i < {{i + 1}}; i++)
                    {
                        next = ecs_table_add_id(world, prev, ids[i]);
                        
                        if (prev != next)
                            added[addedCount++] = ids[i];
                            
                        prev = next;
                    }
        
                    if (table != next)
                    {
                        ecs_type_t type = default;
                        type.array = added;
                        type.count = addedCount;
                        ecs_commit(world, entity, record, next, &type, null);
                        table = next;
                    }
            
                    if (!Ecs.GetPointers<{{Generator.TypeParameters[i]}}>(world, entity, record, table, pointers))
                        Ecs.Error(nameof(ECS_INTERNAL_ERROR));
            
                    Ecs.TableLock(world, table);
                }
                else
                {
                    Ecs.EnsurePointers<{{Generator.TypeParameters[i]}}>(world, entity, pointers);
                }
            
                callback({{Generator.GetCallbackArguments(callback, i)}});
            
                if (!world.IsDeferred())
                    Ecs.TableUnlock(world, table);
            
                {{Generator.ModifiedChain[i]}}
            
                return true;
            }
        """);

        return $$"""
        using System;
        using Flecs.NET.Utilities;
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;
        
        public static unsafe partial class Invoker
        {
        {{string.Join(Separator.DoubleNewLine, readAndWrite.Concat(insert))}}
        }
        """;
    }
}
