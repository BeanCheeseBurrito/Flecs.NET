using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public static class Iterable
{
    public static string GenerateExtensions(int i, Type type)
    {
        string typeParameters = Generator.TypeParameters[i];

        IEnumerable<string> iterators = Generator.CallbacksIterAndEachAndFind.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates the <see cref="{{type}}"/> using the provided .{{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            public {{Generator.GetInvokerReturnType(callback)}} {{Generator.GetInvokerName(callback)}}<{{typeParameters}}>({{Generator.GetCallbackType(i, callback)}} callback) {{Generator.GetCallbackConstraints(i, callback)}}
            {
                {{Generator.GetInvokerReturn(callback)}}Invoker.{{Generator.GetInvokerName(callback)}}<{{type}}, {{typeParameters}}>(ref this, callback);
            }
        """);

        return $$"""
        using System;

        namespace Flecs.NET.Core;

        public unsafe partial struct {{type}}
        {
        {{string.Join(Separator.DoubleNewLine, iterators)}}
        }
        """;
    }
}
