using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public static class NodeBuilder
{
    public static string GenerateExtensions(Type builderType, Type returnType, int i)
    {
        string GenerateComment(Callback callback)
        {
            return $$"""
                    /// <summary>
                    ///     Creates <see cref="{{returnType}}"/> with the provided .{{Generator.GetInvokerName(callback)}} callback.
                    /// </summary>
                    /// <param name="callback">The callback.</param>
                """;
        }

        IEnumerable<string> run = Generator.GetRunCallbacks().Select((Callback callback) => Generator.RunCallbackBuilds(callback)
            ?
            $$"""
            {{GenerateComment(callback)}}
                public {{Generator.GetTypeName(returnType, i)}} {{Generator.GetInvokerName(callback)}}({{Generator.GetCallbackType(callback)}} callback)
                {
                    return SetRun(callback, &Functions.{{Generator.GetCallbackName(callback, i)}}).Build();
                }
            """
            :
            $$"""
            {{GenerateComment(callback)}}
                public ref {{Generator.GetTypeName(builderType, i)}} {{Generator.GetInvokerName(callback)}}({{Generator.GetCallbackType(callback)}} callback)
                {
                    return ref SetRun(callback, &Functions.{{Generator.GetCallbackName(callback, i)}});
                }
            """);

        IEnumerable<string> iter = Generator.GetIterCallbacks(i).Select((Callback callback) => $$"""
            {{GenerateComment(callback)}}
                public {{Generator.GetTypeName(returnType, i)}} Iter({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertSparseTypes(World, true);
                    return SetCallback(callback, &Functions.{{Generator.GetCallbackName(callback, i)}}).Build();
                }
            """);

        IEnumerable<string> each = Generator.GetEachCallbacks(i).Select((Callback callback) => $$"""
            {{GenerateComment(callback)}}
                public {{Generator.GetTypeName(returnType, i)}} Each({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    return SetCallback(callback, &Functions.{{Generator.GetCallbackName(callback, i)}}).Build();
                }
            """);

        return $$"""
            using System;
            using Flecs.NET.Core.BindingContext;
            using Flecs.NET.Utilities;
            
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            public unsafe partial struct {{Generator.GetTypeName(builderType, i)}}
            {
            {{string.Join(Separator.DoubleNewLine, run.Concat(iter).Concat(each))}}
            }
            """;
    }
}
