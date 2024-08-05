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
                Generator.AddSource(postContext, $"Functions/Functions.T{i}.g.cs", GenerateFunctions(i));
                Generator.AddSource(postContext, $"Pointers/Pointers.T{i}.g.cs", GeneratePointers(i));
            }
        });
    }

    public static string GenerateFunctions(int i)
    {
        IEnumerable<string> functions = Generator.CallbacksIterAndEach.Select((Callback callback) => $$"""
            internal static void {{callback}}(ecs_iter_t* iter)
            {
                IteratorContext* context = (IteratorContext*)iter->callback_ctx;
                Invoker.{{Generator.GetInvokerName(callback)}}(iter, ({{Generator.GetCallbackType(i, callback)}}){{(Generator.GetCallbackIsDelegate(callback) ? "context->Callback.GcHandle.Target!" : "context->Callback.Pointer")}});
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
        IEnumerable<string> pointers = Generator.CallbacksIterAndEach.Select((Callback callback) => $$"""
            internal static readonly IntPtr {{callback}} = (IntPtr)(delegate* <ecs_iter_t*, void>)&Functions<{{Generator.TypeParameters[i]}}>.{{callback}};
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
