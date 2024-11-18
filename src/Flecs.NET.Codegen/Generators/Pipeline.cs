using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Pipeline : GeneratorBase
{
    public override void Generate()
    {
        AddSource($"Pipeline.Id.g.cs", Id.GenerateExtensions(Type.Pipeline));
        AddSource($"Pipeline.Entity.g.cs", Entity.GenerateExtensions(Type.Pipeline));

        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"Pipeline/T{i + 1}.g.cs", GeneratePipeline(i));
            AddSource($"Pipeline.Id/T{i + 1}.g.cs", Id.GenerateExtensions(Type.Pipeline, i));
            AddSource($"Pipeline.Entity/T{i + 1}.g.cs", Entity.GenerateExtensions(Type.Pipeline, i));
        }
    }

    private static string GeneratePipeline(int i)
    {
        string typeName = Generator.GetTypeName(Type.Pipeline, i);

        return $$"""
            #nullable enable

            using System;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="Pipeline"/> that takes {{i + 1}} type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{typeName}} : IEquatable<{{typeName}}>, IEntity<{{typeName}}>
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
                public static implicit operator ulong({{typeName}} pipeline)
                {
                    return ToUInt64(pipeline);
                }
            
                /// <inheritdoc cref="Pipeline.ToId(Pipeline)"/>
                public static implicit operator Id({{typeName}} pipeline)
                {
                    return ToId(pipeline);
                }
            
                /// <inheritdoc cref="Pipeline.ToEntity(Pipeline)"/>
                public static implicit operator Entity({{typeName}} pipeline)
                {
                    return ToEntity(pipeline);
                }
            
                /// <inheritdoc cref="Pipeline.ToUInt64(Pipeline)"/>
                public static ulong ToUInt64({{typeName}} pipeline)
                {
                    return pipeline.Entity;
                }
            
                /// <inheritdoc cref="Pipeline.ToId(Pipeline)"/>
                public static Id ToId({{typeName}} pipeline)
                {
                    return pipeline.Id;
                }
            
                /// <inheritdoc cref="Pipeline.ToEntity(Pipeline)"/>
                public static Entity ToEntity({{typeName}} pipeline)
                {
                    return pipeline.Entity;
                }
            
                /// <inheritdoc cref="Pipeline.ToString()"/>
                public override string ToString()
                {
                    return Entity.ToString();
                }
            
                /// <inheritdoc cref="Pipeline.Equals(Pipeline)"/>
                public bool Equals({{typeName}} other)
                {
                    return Entity == other.Entity;
                }
            
                /// <inheritdoc cref="Pipeline.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{typeName}} other && Equals(other);
                }
            
                /// <inheritdoc cref="Pipeline.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return Entity.GetHashCode();
                }
            
                /// <inheritdoc cref="Pipeline.op_Equality"/>
                public static bool operator ==({{typeName}} left, {{typeName}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="Pipeline.op_Inequality"/>
                public static bool operator !=({{typeName}} left, {{typeName}} right)
                {
                    return !(left == right);
                }
            }
            """;
    }
}
