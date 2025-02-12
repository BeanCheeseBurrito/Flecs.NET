using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public static class NodeBuilder
{
    public static string GenerateExtensions(Type builderType, Type returnType, int i)
    {
        IEnumerable<string> iterators = Generator.CallbacksRunAndIterAndEach.Select((Callback callback) => $$"""
                /// <summary>
                ///     Creates <see cref="{{returnType}}"/> with the provided .{{Generator.GetInvokerName(callback)}} callback.
                /// </summary>
                /// <param name="callback">The callback.</param>
                public {{Generator.GetTypeName(returnType, i)}} {{Generator.GetInvokerName(callback)}}({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertSparseTypes(World, {{(Generator.GetCallbackIsIter(callback) ? "false" : "true")}});
                    return {{(Generator.GetCallbackIsRun(callback) ? "SetRun" : "SetCallback")}}(callback, (delegate*<ecs_iter_t*, void>)&Functions.{{callback}}<{{Generator.TypeParameters[i]}}>).Build();
                }
            """);

        return $$"""
            using System;
            using Flecs.NET.Core.BindingContext;
            
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            public unsafe partial struct {{Generator.GetTypeName(builderType, i)}}
            {
            {{string.Join(Separator.DoubleNewLine, iterators)}}
            }
            """;
    }
}
