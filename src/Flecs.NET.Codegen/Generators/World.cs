using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class World : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"Builders/T{i + 1}.g.cs", GenerateBuilders(i));
            AddSource($"Each/T{i + 1}.g.cs", GenerateEach(i));
        }
    }

    private static string GenerateBuilders(int i)
    {
        return $$"""
            #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

            namespace Flecs.NET.Core;

            public unsafe partial struct World
            {
                public {{Generator.GetTypeName(Type.AlertBuilder)}} {{Generator.GetTypeName(Type.AlertBuilder, i)}}()
                {
                    return new AlertBuilder(Handle){{Generator.WithChain[i]}};
                }
            
                public {{Generator.GetTypeName(Type.AlertBuilder)}} {{Generator.GetTypeName(Type.AlertBuilder, i)}}(string name)
                {
                    return new AlertBuilder(Handle, name){{Generator.WithChain[i]}};
                }
            
                public {{Generator.GetTypeName(Type.AlertBuilder)}} {{Generator.GetTypeName(Type.AlertBuilder, i)}}(ulong entity)
                {
                    return new AlertBuilder(Handle, entity){{Generator.WithChain[i]}};
                }
            
                public {{Generator.GetTypeName(Type.Alert)}} {{Generator.GetTypeName(Type.Alert, i)}}()
                {
                    return {{Generator.GetTypeName(Type.AlertBuilder, i)}}().Build();
                }
            
                public {{Generator.GetTypeName(Type.Alert)}} {{Generator.GetTypeName(Type.Alert, i)}}(string name)
                {
                    return {{Generator.GetTypeName(Type.AlertBuilder, i)}}(name).Build();
                }
            
                public {{Generator.GetTypeName(Type.Alert)}} {{Generator.GetTypeName(Type.Alert, i)}}(ulong entity)
                {
                    return {{Generator.GetTypeName(Type.AlertBuilder, i)}}(entity).Build();
                }
                
                public {{Generator.GetTypeName(Type.QueryBuilder, i)}} {{Generator.GetTypeName(Type.QueryBuilder, i)}}()
                {
                    return new {{Generator.GetTypeName(Type.QueryBuilder, i)}}(Handle);
                }
            
                public {{Generator.GetTypeName(Type.QueryBuilder, i)}} {{Generator.GetTypeName(Type.QueryBuilder, i)}}(string name)
                {
                    return new {{Generator.GetTypeName(Type.QueryBuilder, i)}}(Handle, name);
                }
            
                public {{Generator.GetTypeName(Type.QueryBuilder, i)}} {{Generator.GetTypeName(Type.QueryBuilder, i)}}(ulong entity)
                {
                    return new {{Generator.GetTypeName(Type.QueryBuilder, i)}}(Handle, entity);
                }
            
                public {{Generator.GetTypeName(Type.Query, i)}} {{Generator.GetTypeName(Type.Query, i)}}()
                {
                    return new {{Generator.GetTypeName(Type.QueryBuilder, i)}}(Handle).Build();
                }
            
                public {{Generator.GetTypeName(Type.Query, i)}} {{Generator.GetTypeName(Type.Query, i)}}(string name)
                {
                    return new {{Generator.GetTypeName(Type.QueryBuilder, i)}}(Handle, name).Build();
                }
            
                public {{Generator.GetTypeName(Type.Query, i)}} {{Generator.GetTypeName(Type.Query, i)}}(ulong entity)
                {
                    return new {{Generator.GetTypeName(Type.QueryBuilder, i)}}(Handle, entity).Build();
                }
            
                public {{Generator.GetTypeName(Type.SystemBuilder, i)}} {{Generator.GetTypeName(Type.System, i)}}()
                {
                    return new {{Generator.GetTypeName(Type.SystemBuilder, i)}}(Handle);
                }
            
                public {{Generator.GetTypeName(Type.SystemBuilder, i)}} {{Generator.GetTypeName(Type.System, i)}}(string name)
                {
                    return new {{Generator.GetTypeName(Type.SystemBuilder, i)}}(Handle, name);
                }
            
                public {{Generator.GetTypeName(Type.ObserverBuilder, i)}} {{Generator.GetTypeName(Type.Observer, i)}}()
                {
                    return new {{Generator.GetTypeName(Type.ObserverBuilder, i)}}(Handle);
                }
            
                public {{Generator.GetTypeName(Type.ObserverBuilder, i)}} {{Generator.GetTypeName(Type.Observer, i)}}(string name)
                {
                    return new {{Generator.GetTypeName(Type.ObserverBuilder, i)}}(Handle, name);
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
                public void {{Generator.GetInvokerName(callback)}}<{{Generator.TypeParameters[i]}}>({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    using Query<{{Generator.TypeParameters[i]}}> query = Query<{{Generator.TypeParameters[i]}}>();
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
