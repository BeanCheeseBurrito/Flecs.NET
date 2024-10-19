using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class WorkerIterable : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"WorkerIterable/T{i + 1}.g.cs", GenerateWorkerIterable(i));
            AddSource($"WorkerIterable.IIterable/T{i + 1}.g.cs",
                IIterable.GenerateExtensions(Type.WorkerIterable, i));
        }
    }

    private static string GenerateWorkerIterable(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using System.Runtime.CompilerServices;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <inheritdoc cref="IterIterable"/>
            /// {{Generator.XmlTypeParameters[i]}}
            public partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : IEquatable<{{Generator.GetTypeName(Type.WorkerIterable, i)}}>
            {
                private WorkerIterable _workerIterable;
            
                /// <summary>
                ///     Creates a worker iterable.
                /// </summary>
                /// <param name="workerIterable">The worker iterable.</param>
                public WorkerIterable(WorkerIterable workerIterable)
                {
                    _workerIterable = workerIterable;
                }
            
                /// <inheritdoc cref="WorkerIterable(ecs_iter_t, int, int)"/>
                public WorkerIterable(ecs_iter_t iter, int index, int count)
                {
                    _workerIterable = new WorkerIterable(iter, index, count);
                }
                
                /// <inheritdoc cref="WorkerIterable.Equals(WorkerIterable)"/>
                public bool Equals({{Generator.GetTypeName(Type.WorkerIterable, i)}} other)
                {
                    return _workerIterable.Equals(other._workerIterable);
                }
                
                /// <inheritdoc cref="WorkerIterable.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.WorkerIterable, i)}} other && Equals(other);
                }
                
                /// <inheritdoc cref="WorkerIterable.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _workerIterable.GetHashCode();
                }
                
                /// <inheritdoc cref="WorkerIterable.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.WorkerIterable, i)}} left, {{Generator.GetTypeName(Type.WorkerIterable, i)}} right)
                {
                    return left.Equals(right);
                }
                
                /// <inheritdoc cref="WorkerIterable.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.WorkerIterable, i)}} left, {{Generator.GetTypeName(Type.WorkerIterable, i)}} right)
                {
                    return !(left == right);
                }
            }

            // IIterableBase Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : IIterableBase
            {
                /// <inheritdoc cref="WorkerIterable.World"/>
                public ref ecs_world_t* World => ref _workerIterable.World;
                
                /// <inheritdoc cref="WorkerIterable.GetIter(ecs_world_t*)"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public ecs_iter_t GetIter(ecs_world_t* world = null)
                {
                    return _workerIterable.GetIter(world);
                }
                
                /// <inheritdoc cref="WorkerIterable.GetNext(ecs_iter_t*)"/>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool GetNext(ecs_iter_t* it)
                {
                    return _workerIterable.GetNext(it);
                }
            }

            // {{Generator.GetTypeName(Type.IIterable, i)}} Interface
            public unsafe partial struct {{Generator.GetTypeName(Type.WorkerIterable, i)}} : {{Generator.GetTypeName(Type.IIterable, i)}}
            {
                /// <inheritdoc cref="WorkerIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit)
                {
                    return new {{Generator.GetTypeName(Type.PageIterable, i)}}(_workerIterable.Page(offset, limit));
                }
                
                /// <inheritdoc cref="WorkerIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count)
                {
                    return new {{Generator.GetTypeName(Type.WorkerIterable, i)}}(_workerIterable.Worker(index, count));
                }
                
                /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default)
                {
                    return new(_workerIterable.Iter(world));
                }
                
                /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it)
                {
                    return new(_workerIterable.Iter(it));
                }
                
                /// <inheritdoc cref="WorkerIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity)
                {
                    return new(_workerIterable.Iter(entity));
                }
                
                /// <inheritdoc cref="WorkerIterable.Count()"/>
                public int Count()
                {
                    return _workerIterable.Count();
                }
                
                /// <inheritdoc cref="WorkerIterable.IsTrue()"/>
                public bool IsTrue()
                {
                    return _workerIterable.IsTrue();
                }
                
                /// <inheritdoc cref="WorkerIterable.First()"/>
                public Entity First()
                {
                    return _workerIterable.First();
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(int, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value)
                {
                    return new(_workerIterable.SetVar(varId, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value)
                {
                    return new(_workerIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, ecs_table_t*)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value)
                {
                    return new(_workerIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, ecs_table_range_t)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value)
                {
                    return new(_workerIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetVar(string, Table)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value)
                {
                    return new(_workerIterable.SetVar(name, value));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetGroup(ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId)
                {
                    return new(_workerIterable.SetGroup(groupId));
                }
                
                /// <inheritdoc cref="WorkerIterable.SetGroup{T}()"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>()
                {
                    return new(_workerIterable.SetGroup<T>()); 
                }
            }
            """;
    }
}
