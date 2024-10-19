using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class PipelineBuilder : GeneratorBase
{
    public override void Generate()
    {
        AddSource("PipelineBuilder.QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(Type.PipelineBuilder));

        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"PipelineBuilder/T{i + 1}.g.cs", GeneratePipelineBuilder(i));
            AddSource($"PipelineBuilder.QueryBuilder/T{i + 1}.g.cs",
                QueryBuilder.GenerateExtensions(Type.PipelineBuilder, i));
        }
    }

    private static string GeneratePipelineBuilder(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="PipelineBuilder"/> that takes {{i + 1}} type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{Generator.GetTypeName(Type.PipelineBuilder, i)}} : IEquatable<{{Generator.GetTypeName(Type.PipelineBuilder, i)}}>, IQueryBuilder<{{Generator.GetTypeName(Type.PipelineBuilder, i)}}, {{Generator.GetTypeName(Type.Pipeline, i)}}>
            {
                private PipelineBuilder _pipelineBuilder;
            
                /// <inheritdoc cref="PipelineBuilder.World"/>
                public ref ecs_world_t* World => ref _pipelineBuilder.World;
                
                /// <inheritdoc cref="PipelineBuilder.Desc"/>
                public ref ecs_pipeline_desc_t Desc => ref _pipelineBuilder.Desc;
                
                /// <inheritdoc cref="PipelineBuilder.QueryBuilder"/>
                public ref QueryBuilder QueryBuilder => ref _pipelineBuilder.QueryBuilder;
            
                /// <inheritdoc cref="PipelineBuilder(ecs_world_t*)"/>
                public PipelineBuilder(ecs_world_t* world)
                {
                    _pipelineBuilder = new PipelineBuilder(world){{Generator.WithChain[i]}};
                }
            
                /// <inheritdoc cref="PipelineBuilder(ecs_world_t*, ulong)"/>
                public PipelineBuilder(ecs_world_t* world, ulong entity)
                {
                    _pipelineBuilder = new PipelineBuilder(world, entity){{Generator.WithChain[i]}};
                }
            
                /// <inheritdoc cref="PipelineBuilder(ecs_world_t*, string)"/>
                public PipelineBuilder(ecs_world_t* world, string name)
                {
                    _pipelineBuilder = new PipelineBuilder(world, name){{Generator.WithChain[i]}};
                }
            
                /// <inheritdoc cref="PipelineBuilder.Build()"/>
                public {{Generator.GetTypeName(Type.Pipeline, i)}} Build()
                {
                    return new {{Generator.GetTypeName(Type.Pipeline, i)}}(_pipelineBuilder.Build());
                }
                
                /// <inheritdoc cref="PipelineBuilder.Equals(PipelineBuilder)"/>
                public bool Equals({{Generator.GetTypeName(Type.PipelineBuilder, i)}} other)
                {
                    return _pipelineBuilder == other._pipelineBuilder;
                }
            
                /// <inheritdoc cref="PipelineBuilder.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.PipelineBuilder, i)}}  other && Equals(other);
                }
            
                /// <inheritdoc cref="PipelineBuilder.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _pipelineBuilder.GetHashCode();
                }
            
                /// <inheritdoc cref="PipelineBuilder.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.PipelineBuilder, i)}} left, {{Generator.GetTypeName(Type.PipelineBuilder, i)}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="PipelineBuilder.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.PipelineBuilder, i)}} left, {{Generator.GetTypeName(Type.PipelineBuilder, i)}} right)
                {
                    return !(left == right);
                }
            }
            """;
    }
}
