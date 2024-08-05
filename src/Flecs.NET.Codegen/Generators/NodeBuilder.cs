using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public static class NodeBuilder
{
    public static string GenerateExtensions(int i, Type builderType, Type returnType)
    {
        IEnumerable<string> iterators = Generator.CallbacksIterAndEach.Select((Callback callback) => $$"""
            /// <summary>
            ///     Creates <see cref="{{returnType}}"/> with the provided .{{Generator.GetInvokerName(callback)}} callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            public {{returnType}} {{Generator.GetInvokerName(callback)}}<{{Generator.TypeParameters[i]}}>({{Generator.GetCallbackType(i, callback)}} callback) {{Generator.GetCallbackConstraints(i, callback)}}
            {
                return SetCallback({{(Generator.GetCallbackIsDelegate(callback) ? string.Empty : "(IntPtr)")}}callback, Pointers<{{Generator.TypeParameters[i]}}>.{{callback}}).Build();
            }
        """);

        return $$"""
        using System;
        using Flecs.NET.Core.BindingContext;

        namespace Flecs.NET.Core;

        public unsafe partial struct {{builderType}}
        {
        {{string.Join(Separator.DoubleNewLine, iterators)}}
        }
        """;
    }
}
