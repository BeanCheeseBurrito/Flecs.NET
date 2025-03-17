using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;
using Type = Flecs.NET.Codegen.Helpers.Type;

namespace Flecs.NET.Codegen.Generators;

public class Invoker : GeneratorBase
{
    public override void Generate()
    {
        AddSource("Run/T0.g.cs", GenerateRunInvokers(Generator.GetRunCallbacks()));

        for (int i = -1; i < Generator.GenericCount; i++)
        {
            AddSource($"Iter/T{i + 1}.g.cs", GenerateIterInvokers(Generator.GetIterCallbacks(i), i));
            AddSource($"Each/T{i + 1}.g.cs", GenerateEachInvokers(Generator.GetEachCallbacks(i), i));
            AddSource($"Find/T{i + 1}.g.cs", GenerateFindInvokers(Generator.GetFindCallbacks(i), i));
        }

        for (int i = -1; i < 1; i++)
        {
            AddSource($"Observe/T{i + 1}.g.cs", GenerateObserveInvokers(Generator.GetObserveCallbacks(i), i));
        }

        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"Read/T{i + 1}.g.cs", GenerateFetchInvokers([Callback.ReadRefCallbackDelegate], i));
            AddSource($"Write/T{i + 1}.g.cs", GenerateFetchInvokers([Callback.WriteRefCallbackDelegate], i));
            AddSource($"Insert/T{i + 1}.g.cs", GenerateFetchInvokers([Callback.InsertRefCallbackDelegate], i));
        }
    }

    private static string GenerateRunInvokers(Callback[] callbacks)
    {
        IEnumerable<string> invokers = callbacks.Select((Callback callback) => $$"""
            internal unsafe struct {{Generator.GetCallbackName(callback)}} : IRunInvoker
            {
                public static void Invoke(Iter it, InvokerCallback callback)
                {
                    {{Generator.GetCallbackType(callback)}} invoke = ({{Generator.GetCallbackType(callback)}})callback;
                    invoke({{Generator.GetCallbackArguments(callback)}});
                }
                
                public static void Invoke(JobState state)
                {
                    {{Generator.GetTypeName(Type.Invoker)}}.Run<WorkerIterable, {{Generator.GetCallbackName(callback)}}>(ref state.Worker, state.Callback);
                }
                
                public static void Callback(Iter it)
                {
                    it.Callback();
                }
            }
            """);

        return $$"""
            using System;

            namespace Flecs.NET.Core.Invokers;

            {{string.Join(Separator.DoubleNewLine, invokers)}}
            """;
    }

    private static string GenerateIterInvokers(Callback[] callbacks, int i = -1)
    {
        IEnumerable<string> invokers = callbacks.Select((Callback callback) => $$"""
            internal unsafe struct {{Generator.GetCallbackName(callback, i)}} : IIterInvoker
            {
                public static void Invoke(Iter it, InvokerCallback callback)
                {
                    {{Generator.GetCallbackType(callback, i)}} invoke = ({{Generator.GetCallbackType(callback, i)}})callback;
                    invoke({{Generator.GetCallbackArguments(callback, i)}});
                }
                
                public static void Invoke(JobState state)
                {
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Iter<WorkerIterable, {{Generator.GetCallbackName(callback, i)}}>(ref state.Worker, state.Callback);
                }
            }
            """);

        return $$"""
            using System;

            namespace Flecs.NET.Core.Invokers;

            {{string.Join(Separator.DoubleNewLine, invokers)}}
            """;
    }

    private static string GenerateEachInvokers(Callback[] callbacks, int i = -1)
    {
        IEnumerable<string> invokers = callbacks.Select((Callback callback) => $$"""
            internal unsafe struct {{Generator.GetCallbackName(callback, i)}} : IEachInvoker
            {
                public static void Invoke<TFieldGetter>(in Fields fields, int count, InvokerCallback callback) where TFieldGetter : IFieldGetter
                {
                    {{Generator.GetCallbackType(callback, i)}} invoke = ({{Generator.GetCallbackType(callback, i)}})callback;
                    for (int i = 0; i < count; i++)
                        invoke({{Generator.GetCallbackArguments(callback, i)}});
                }
                
                public static void Invoke(JobState state)
                {
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Each<WorkerIterable, {{Generator.GetCallbackName(callback, i)}}>(ref state.Worker, state.Callback);
                }
            }
            """);

        return $$"""
            using System;
            using Flecs.NET.Utilities;
            
            namespace Flecs.NET.Core.Invokers;
            
            {{string.Join(Separator.DoubleNewLine, invokers)}}
            """;
    }

    private static string GenerateFindInvokers(Callback[] callbacks, int i = -1)
    {
        IEnumerable<string> invokers = callbacks.Select((Callback callback) => $$"""
            internal unsafe struct {{Generator.GetCallbackName(callback, i)}} : IFindInvoker
            {
                public static Entity Invoke<TFieldGetter>(in Fields fields, int count, InvokerCallback callback) where TFieldGetter : IFieldGetter
                {
                    {{Generator.GetCallbackType(callback, i)}} invoke = ({{Generator.GetCallbackType(callback, i)}})callback;
                    
                    for (int i = 0; i < count; i++)
                    {
                        if (invoke({{Generator.GetCallbackArguments(callback, i)}}))
                            return new Entity(fields.Iter->world, fields.Iter->entities[i]);
                    }
                    
                    return default;
                }
            }
            """);

        return $$"""
            using System;
            using Flecs.NET.Utilities;

            namespace Flecs.NET.Core.Invokers;

            {{string.Join(Separator.DoubleNewLine, invokers)}}
            """;
    }

    private static string GenerateObserveInvokers(Callback[] callbacks, int i = -1)
    {
        IEnumerable<string> invokers = callbacks.Select((Callback callback) => $$"""
            internal unsafe struct {{Generator.GetCallbackName(callback, i)}} : IObserveInvoker
            {
                public static void Invoke(Iter it, InvokerCallback callback)
                {
                    {{Generator.GetCallbackType(callback, i)}} invoke = ({{Generator.GetCallbackType(callback, i)}})callback;
                    invoke({{Generator.GetCallbackArguments(callback, i)}});
                }
            }
            """);

        return $$"""
            using System;

            namespace Flecs.NET.Core.Invokers;

            {{string.Join(Separator.DoubleNewLine, invokers)}}
            """;
    }

    private static string GenerateFetchInvokers(Callback[] callbacks, int i)
    {
        IEnumerable<string> invokers = callbacks.Select((Callback callback) => $$"""
            internal unsafe struct {{Generator.GetCallbackName(callback, i)}} : I{{Generator.GetInvokerName(callback)}}Invoker
            {
                public static void Invoke(void** pointers, InvokerCallback callback)
                {
                    {{Generator.GetCallbackType(callback, i)}} invoke = ({{Generator.GetCallbackType(callback, i)}})callback;
                    invoke({{Generator.GetCallbackArguments(callback, i)}});
                }
            }
            """);

        return $$"""
            using System;

            namespace Flecs.NET.Core.Invokers;

            {{string.Join(Separator.DoubleNewLine, invokers)}}
            """;
    }
}
