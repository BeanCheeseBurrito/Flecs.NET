using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class Entity : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
                Generator.AddSource(postContext, $"ComponentCallbacks/T{i + 1}.g.cs", GenerateExtensions(i, Type.Entity));
        });
    }

    public static string GenerateExtensions(int i, Type type)
    {
        IEnumerable<string> readAndWrite = Generator.CallbacksReadAndWrite.Select((Callback callback) => $$"""
            /// <summary>
            ///     {{Generator.GetInvokerName(callback)}} {{i + 1}} components using the provided callback. <br/><br/>
            /// 
            ///     This operation accepts a callback with as arguments the components to
            ///     retrieve. The callback will only be invoked when the entity has all
            ///     the components. <br/><br/>
            ///     
            ///     This operation is faster than individually calling get for each component
            ///     as it only obtains entity metadata once.  <br/><br/>
            ///     
            ///     While the callback is invoked the table in which the components are
            ///     stored is locked, which prevents mutations that could cause invalidation
            ///     of the component references. Note that this is not an actual lock: 
            ///     invalid access causes a runtime panic and so it is still up to the 
            ///     application to ensure access is protected.  <br/><br/>
            /// </summary>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters}}
            /// <returns>True if the entity has the specified components.</returns>
            public bool {{Generator.GetInvokerName(callback)}}<{{Generator.TypeParameters[i]}}>({{Generator.GetCallbackType(i, callback)}} callback)
            {
                return Invoker.{{Generator.GetInvokerName(callback)}}(World, Id, callback);
            }
        """);

        IEnumerable<string> insert = Generator.CallbacksInsert.Select((Callback callback) => $$"""
            /// <summary>
            ///     Ensures {{i + 1}} components using the provided callback.<br/><br/>
            /// 
            ///     This operation accepts a callback with as arguments the components to
            ///     set. If the entity does not have all of the provided components, they
            ///     will be added. <br/><br/>
            ///
            ///     This operation is faster than individually calling ensure for each component
            ///     as it only obtains entity metadata once. When this operation is called
            ///     while deferred, its performance is equivalent to that of calling ensure
            ///     for each component separately. <br/><br/>
            ///
            ///     The operation will invoke modified for each component after the callback
            ///     has been invoked.
            /// </summary>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters}}
            /// <returns>Reference to self.</returns>
            public ref {{type}} {{Generator.GetInvokerName(callback)}}<{{Generator.TypeParameters[i]}}>({{Generator.GetCallbackType(i, callback)}} callback)
            {
                Invoker.{{Generator.GetInvokerName(callback)}}(World, Id, callback);
                return ref this;
            }
        """);

        return $$"""
        using System;

        namespace Flecs.NET.Core;

        public unsafe partial struct {{type}}
        {
        {{string.Join(Separator.DoubleNewLine, readAndWrite.Concat(insert))}}
        }
        """;
    }
}
