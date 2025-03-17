using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class BindingContext : GeneratorBase
{
    public override void Generate()
    {
        for (int i = -1; i < Generator.GenericCount; i++)
        {
            AddSource($"Run/T{i + 1}.g.cs", GenerateRunFunctions(Generator.GetRunCallbacks(), i));
            AddSource($"Iter/T{i + 1}.g.cs", GenerateIteratorFunctions(Generator.GetIterCallbacks(i), i));
            AddSource($"Each/T{i + 1}.g.cs", GenerateIteratorFunctions(Generator.GetEachCallbacks(i), i));
        }

        for (int i = -1; i < 1; i++)
        {
            AddSource($"Observe/T{i + 1}.g.cs", GenerateIteratorFunctions(Generator.GetObserveCallbacks(i), i));
        }
    }

    public static string GenerateRunFunctions(Callback[] callbacks, int i)
    {
        IEnumerable<string> functions = callbacks.Select((Callback callback) => $$"""
                internal static void {{Generator.GetCallbackName(callback, i)}}(ecs_iter_t* iter)
                {
                    RunContext* context = (RunContext*)iter->run_ctx;
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Run<{{Generator.GetCallbackName(callback)}}>(iter, ({{Generator.GetCallbackType(callback)}})context->Callback);
                }
            """);

        return $$"""
            using System;

            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core.BindingContext;

            internal static unsafe partial class Functions
            {
            {{string.Join(Separator.DoubleNewLine, functions)}}
            }
            """;
    }

    public static string GenerateIteratorFunctions(Callback[] callbacks, int i)
    {
        IEnumerable<string> functions = callbacks.Select((Callback callback) => $$"""
                internal static void {{Generator.GetCallbackName(callback, i)}}(ecs_iter_t* iter)
                {
                    IteratorContext* context = (IteratorContext*)iter->callback_ctx;
                    {{Generator.GetTypeName(Type.Invoker, i)}}.{{Generator.GetInvokerName(callback)}}<{{Generator.GetCallbackName(callback, i)}}>(iter, ({{Generator.GetCallbackType(callback, i)}})context->Callback);
                }
            """);

        return $$"""
            using System;

            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core.BindingContext;

            internal static unsafe partial class Functions
            {
            {{string.Join(Separator.DoubleNewLine, functions)}}
            }
            """;
    }
}
