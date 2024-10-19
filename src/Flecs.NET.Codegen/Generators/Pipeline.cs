using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Pipeline : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"Pipeline/T{i + 1}.g.cs", GeneratePipeline(i));
        }
    }

    private static string GeneratePipeline(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="Pipeline"/> that takes {{i + 1}} type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{Generator.GetTypeName(Type.Pipeline, i)}} : IEquatable<{{Generator.GetTypeName(Type.Pipeline, i)}}>, IEntity
            {
                private Pipeline _pipeline;
            
                /// <inheritdoc cref="Pipeline.Entity"/>
                public ref Entity Entity => ref _pipeline.Entity;
            
                /// <inheritdoc cref="Pipeline.Id"/>
                public ref Id Id => ref _pipeline.Id;
            
                /// <inheritdoc cref="Pipeline.World"/>
                public ref ecs_world_t* World => ref _pipeline.World;
                
                /// <summary>
                ///     Creates a new pipeline with the provided pipeline.
                /// </summary>
                /// <param name="pipeline">The pipeline.</param>
                public Pipeline(Pipeline pipeline)
                {
                    _pipeline = pipeline;
                }
            
                /// <inheritdoc cref="Pipeline(ecs_world_t*, ulong)"/>
                public Pipeline(ecs_world_t* world, ulong entity = 0)
                {
                    _pipeline = new Pipeline(world, entity);
                }
            
                /// <inheritdoc cref="Pipeline(Core.Entity)"/>
                public Pipeline(Entity entity)
                {
                   _pipeline = new Pipeline(entity);
                }
            
                /// <inheritdoc cref="Pipeline.ToUInt64(Pipeline)"/>
                public static implicit operator ulong({{Generator.GetTypeName(Type.Pipeline, i)}} pipeline)
                {
                    return ToUInt64(pipeline);
                }
            
                /// <inheritdoc cref="Pipeline.ToId(Pipeline)"/>
                public static implicit operator Id({{Generator.GetTypeName(Type.Pipeline, i)}} pipeline)
                {
                    return ToId(pipeline);
                }
            
                /// <inheritdoc cref="Pipeline.ToEntity(Pipeline)"/>
                public static implicit operator Entity({{Generator.GetTypeName(Type.Pipeline, i)}} pipeline)
                {
                    return ToEntity(pipeline);
                }
            
                /// <inheritdoc cref="Pipeline.ToUInt64(Pipeline)"/>
                public static ulong ToUInt64({{Generator.GetTypeName(Type.Pipeline, i)}} pipeline)
                {
                    return pipeline.Entity;
                }
            
                /// <inheritdoc cref="Pipeline.ToId(Pipeline)"/>
                public static Id ToId({{Generator.GetTypeName(Type.Pipeline, i)}} pipeline)
                {
                    return pipeline.Id;
                }
            
                /// <inheritdoc cref="Pipeline.ToEntity(Pipeline)"/>
                public static Entity ToEntity({{Generator.GetTypeName(Type.Pipeline, i)}} pipeline)
                {
                    return pipeline.Entity;
                }
            
                /// <inheritdoc cref="Pipeline.ToString()"/>
                public override string ToString()
                {
                    return Entity.ToString();
                }
            
                /// <inheritdoc cref="Pipeline.Equals(Pipeline)"/>
                public bool Equals({{Generator.GetTypeName(Type.Pipeline, i)}} other)
                {
                    return Entity == other.Entity;
                }
            
                /// <inheritdoc cref="Pipeline.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.Pipeline, i)}} other && Equals(other);
                }
            
                /// <inheritdoc cref="Pipeline.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return Entity.GetHashCode();
                }
            
                /// <inheritdoc cref="Pipeline.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.Pipeline, i)}} left, {{Generator.GetTypeName(Type.Pipeline, i)}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="Pipeline.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.Pipeline, i)}} left, {{Generator.GetTypeName(Type.Pipeline, i)}} right)
                {
                    return !(left == right);
                }
            }
            """;
    }
}
