using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class BindingContext : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"Functions/T{i}.g.cs", GenerateFunctions(i));
        }
    }

    public static string GenerateFunctions(int i)
    {
        IEnumerable<string> functions = Generator.CallbacksRunAndIterAndEach.Select((Callback callback) => $$"""
                internal static void {{callback}}<{{Generator.TypeParameters[i]}}>(ecs_iter_t* iter)
                {
                    {{(Generator.GetCallbackIsRun(callback) ? "RunContext" : "IteratorContext")}}* context = ({{(Generator.GetCallbackIsRun(callback) ? "RunContext" : "IteratorContext")}}*)iter->{{(Generator.GetCallbackIsRun(callback) ? "run_ctx" : "callback_ctx")}};
                    Invoker.{{Generator.GetInvokerName(callback)}}(iter, ({{Generator.GetCallbackType(callback, i)}}){{(Generator.GetCallbackIsDelegate(callback) ? "context->Callback.Delegate.Target!" : "context->Callback.Pointer")}});
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
