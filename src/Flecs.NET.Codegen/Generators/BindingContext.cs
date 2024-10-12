using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class BindingContext : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"Functions/T{i}.g.cs", GenerateFunctions(i));
                Generator.AddSource(postContext, $"Pointers/T{i}.g.cs", GeneratePointers(i));
            }
        });
    }

    public static string GenerateFunctions(int i)
    {
        IEnumerable<string> functions = Generator.CallbacksRunAndIterAndEach.Select((Callback callback) => $$"""
            internal static void {{callback}}(ecs_iter_t* iter)
            {
                {{(Generator.GetCallbackIsRun(callback) ? "RunContext" : "IteratorContext")}}* context = ({{(Generator.GetCallbackIsRun(callback) ? "RunContext" : "IteratorContext")}}*)iter->{{(Generator.GetCallbackIsRun(callback) ? "run_ctx" : "callback_ctx")}};
                Invoker.{{Generator.GetInvokerName(callback)}}(iter, ({{Generator.GetCallbackType(callback, i)}}){{(Generator.GetCallbackIsDelegate(callback) ? "context->Callback.Delegate.Target!" : "context->Callback.Pointer")}});
            }
        """);

        return $$"""
        using System;
        
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core.BindingContext;

        internal static unsafe partial class Functions<{{Generator.TypeParameters[i]}}>
        {
        {{string.Join(Separator.DoubleNewLine, functions)}}
        }
        """;
    }

    public static string GeneratePointers(int i)
    {
        IEnumerable<string> pointers = Generator.CallbacksRunAndIterAndEach.Select((Callback callback) => $$"""
            internal static readonly nint {{callback}} = (nint)(delegate* <ecs_iter_t*, void>)&Functions<{{Generator.TypeParameters[i]}}>.{{callback}};
        """);

        return $$"""
        using System;
        
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core.BindingContext;

        internal static unsafe partial class Pointers<{{Generator.TypeParameters[i]}}>
        {
        {{string.Join(Separator.DoubleNewLine, pointers)}}
        }
        """;
    }
}
