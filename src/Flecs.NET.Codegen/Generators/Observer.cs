using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Observer : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"Observer/T{i + 1}.g.cs", GenerateObserver(i));
        }
    }

    private static string GenerateObserver(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="Observer"/> that takes 16 type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe struct {{Generator.GetTypeName(Type.Observer, i)}} : IEquatable<{{Generator.GetTypeName(Type.Observer, i)}}>, IDisposable
            {
                private Observer _observer;
            
                /// <inheritdoc cref="Observer.Entity"/>
                public ref Entity Entity => ref _observer.Entity;
            
                /// <inheritdoc cref="Observer.Id"/>
                public ref Id Id => ref _observer.Id;
            
                /// <inheritdoc cref="Observer.World"/>
                public ref ecs_world_t* World => ref _observer.World;
                
                /// <summary>
                ///     Creates an observer with the provided observer.
                /// </summary>
                /// <param name="observer">The observer.</param>
                public Observer(Observer observer)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                    _observer = observer;
                }
            
                /// <inheritdoc cref="Observer(ecs_world_t*, ulong)"/>
                public Observer(ecs_world_t* world, ulong entity)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                    _observer = new Observer(world, entity);
                }
            
                /// <inheritdoc cref="Observer(Core.Entity)"/>
                public Observer(Entity entity)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                    _observer = new Observer(entity);
                }
            
                /// <inheritdoc cref="Observer.Dispose"/>
                public void Dispose()
                {
                    _observer.Dispose();
                }
            
                /// <inheritdoc cref="Observer.Destruct"/>
                public void Destruct()
                {
                    _observer.Destruct();
                }
            
                ///
                public void Ctx(void* ctx)
                {
                    _observer.Ctx(ctx);
                }
            
                /// <inheritdoc cref="Observer.Ctx()"/>
                public void* Ctx()
                {
                    return _observer.Ctx();
                }
            
                /// <inheritdoc cref="Observer.Ctx{T}()"/>
                public T* Ctx<T>() where T : unmanaged
                {
                    return _observer.Ctx<T>();
                }
            
                /// <inheritdoc cref="Observer.Query()"/>
                public {{Generator.GetTypeName(Type.Query, i)}} Query()
                {
                    return new {{Generator.GetTypeName(Type.Query, i)}}(_observer.Query());
                }
            
                /// <inheritdoc cref="Observer.ToUInt64"/>
                public static implicit operator ulong({{Generator.GetTypeName(Type.Observer, i)}} observer)
                {
                    return ToUInt64(observer);
                }
            
                /// <inheritdoc cref="Observer.ToId"/>
                public static implicit operator Id({{Generator.GetTypeName(Type.Observer, i)}} observer)
                {
                    return ToId(observer);
                }
            
                /// <inheritdoc cref="Observer.ToEntity"/>
                public static implicit operator Entity({{Generator.GetTypeName(Type.Observer, i)}} observer)
                {
                    return ToEntity(observer);
                }
            
                /// <inheritdoc cref="Observer.ToUInt64"/>
                public static ulong ToUInt64({{Generator.GetTypeName(Type.Observer, i)}} observer)
                {
                    return observer.Entity;
                }
            
                /// <inheritdoc cref="Observer.ToId"/>
                public static Id ToId({{Generator.GetTypeName(Type.Observer, i)}} observer)
                {
                    return observer.Id;
                }
            
                /// <inheritdoc cref="Observer.ToEntity"/>
                public static Entity ToEntity({{Generator.GetTypeName(Type.Observer, i)}} observer)
                {
                    return observer.Entity;
                }
            
                /// <inheritdoc cref="Observer.Equals(Observer)"/>
                public bool Equals({{Generator.GetTypeName(Type.Observer, i)}} other)
                {
                    return _observer == other._observer;
                }
            
                /// <inheritdoc cref="Observer.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.Observer, i)}} other && Equals(other);
                }
            
                /// <inheritdoc cref="Observer.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _observer.GetHashCode();
                }
            
                /// <inheritdoc cref="Observer.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.Observer, i)}} left, {{Generator.GetTypeName(Type.Observer, i)}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="Observer.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.Observer, i)}} left, {{Generator.GetTypeName(Type.Observer, i)}} right)
                {
                    return !(left == right);
                }
            
                /// <inheritdoc cref="Observer.ToString"/>
                public override string ToString()
                {
                    return _observer.ToString();
                }
            }
            """;
    }
}
