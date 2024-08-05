using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class World : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"Builders/T{i + 1}.g.cs", GenerateBuilders(i));
                Generator.AddSource(postContext, $"Each/T{i + 1}.g.cs", GenerateEach(i));
            }
        });
    }

    private static string GenerateBuilders(int count)
    {
        string typeParameters = Generator.TypeParameters[count];
        string withChain = Generator.WithChain[count];

        return $$"""
        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        
        namespace Flecs.NET.Core;
        
        public unsafe partial struct World
        {
            public AlertBuilder AlertBuilder<{{typeParameters}}>()
            {
                return new AlertBuilder(Handle){{withChain}};
            }

            public AlertBuilder AlertBuilder<{{typeParameters}}>(string name)
            {
                return new AlertBuilder(Handle, name){{withChain}};
            }

            public AlertBuilder AlertBuilder<{{typeParameters}}>(ulong entity)
            {
                return new AlertBuilder(Handle, entity){{withChain}};
            }

            public Alert Alert<{{typeParameters}}>()
            {
                return AlertBuilder<{{typeParameters}}>().Build();
            }

            public Alert Alert<{{typeParameters}}>(string name)
            {
                return AlertBuilder<{{typeParameters}}>(name).Build();
            }

            public Alert Alert<{{typeParameters}}>(ulong entity)
            {
                return AlertBuilder<{{typeParameters}}>(entity).Build();
            }

            public QueryBuilder QueryBuilder<{{typeParameters}}>()
            {
                return new QueryBuilder(Handle){{withChain}};
            }

            public QueryBuilder QueryBuilder<{{typeParameters}}>(string name)
            {
                return new QueryBuilder(Handle, name){{withChain}};
            }

            public QueryBuilder QueryBuilder<{{typeParameters}}>(ulong entity)
            {
                return new QueryBuilder(Handle, entity){{withChain}};
            }

            public Query Query<{{typeParameters}}>()
            {
                return QueryBuilder<{{typeParameters}}>().Build();
            }

            public Query Query<{{typeParameters}}>(string name)
            {
                return QueryBuilder<{{typeParameters}}>(name).Build();
            }

            public Query Query<{{typeParameters}}>(ulong entity)
            {
                return QueryBuilder<{{typeParameters}}>(entity).Build();
            }

            public RoutineBuilder Routine<{{typeParameters}}>()
            {
                return new RoutineBuilder(Handle){{withChain}};
            }

            public RoutineBuilder Routine<{{typeParameters}}>(string name)
            {
                return new RoutineBuilder(Handle, name){{withChain}};
            }

            public ObserverBuilder Observer<{{typeParameters}}>()
            {
                return new ObserverBuilder(Handle){{withChain}};
            }

            public ObserverBuilder Observer<{{typeParameters}}>(string name)
            {
                return new ObserverBuilder(Handle, name){{withChain}};
            }
        }
        
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        """;
    }

    private static string GenerateEach(int i)
    {
        IEnumerable<string> functions = Generator.CallbacksEach.Select((Callback callback) => $$"""
            /// <summary>
            ///     Iterates over the world using the provided .Each callback.
            /// </summary>
            /// <param name="callback">The callback.</param>
            /// {{Generator.XmlTypeParameters[i]}}
            public void {{Generator.GetInvokerName(callback)}}<{{Generator.TypeParameters[i]}}>({{Generator.GetCallbackType(i, callback)}} callback) {{Generator.GetCallbackConstraints(i, callback)}}
            {
                using Query query = Query<{{Generator.TypeParameters[i]}}>();
                query.{{Generator.GetInvokerName(callback)}}(callback);   
            }
        """);

        return $$"""
        namespace Flecs.NET.Core;
                     
        public unsafe partial struct World
        {
        {{string.Join(Separator.DoubleNewLine, functions)}}
        }
        """;
    }
}
