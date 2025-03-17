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

        for (int i = -1; i < Generator.GenericCount; i++)
        {
            AddSource($"Observer.Id/T{i + 1}.g.cs", Id.GenerateExtensions(Type.Observer, i));
            AddSource($"Observer.Entity/T{i + 1}.g.cs", Entity.GenerateExtensions(Type.Observer, i));
            AddSource($"Observer.Entity.Observe/T{i + 1}.g.cs", Entity.GenerateObserveFunctions(Type.Observer, i));
        }
    }

    private static string GenerateObserver(int i)
    {
        string typeName = Generator.GetTypeName(Type.Observer, i);
        return $$"""
            #nullable enable

            using System;
            using Flecs.NET.Utilities;
            
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="Observer"/> that takes 16 type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{typeName}} : IDisposable, IEquatable<{{typeName}}>, IEntity<{{typeName}}>
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
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertNoTags();
                    _observer = observer;
                }
            
                /// <inheritdoc cref="Observer(ecs_world_t*, ulong)"/>
                public Observer(ecs_world_t* world, ulong entity)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertNoTags();
                    _observer = new Observer(world, entity);
                }
            
                /// <inheritdoc cref="Observer(Core.Entity)"/>
                public Observer(Entity entity)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertNoTags();
                    _observer = new Observer(entity);
                }
            
                /// <inheritdoc cref="Observer.Dispose"/>
                public void Dispose()
                {
                    _observer.Dispose();
                }
            
                /// <inheritdoc cref="Observer.Ctx{T}(T)"/>
                public void Ctx<T>(T value)
                {
                    _observer.Ctx(ref value);
                }
                
                /// <inheritdoc cref="Observer.Ctx{T}(T, Ecs.UserContextFinish{T})"/>
                public void Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
                {
                    _observer.Ctx(ref value, callback);
                }
                
                /// <inheritdoc cref="Observer.Ctx{T}(T, Ecs.UserContextFinish{T})"/>
                public void Ctx<T>(T value, delegate*<ref T, void> callback)
                {
                    _observer.Ctx(ref value, callback);
                }
                
                /// <inheritdoc cref="Observer.Ctx{T}(ref T)"/>
                public void Ctx<T>(ref T value)
                {
                    _observer.Ctx(ref value);
                }
                
                /// <inheritdoc cref="Observer.Ctx{T}(ref T, Ecs.UserContextFinish{T})"/>
                public void Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
                {
                    _observer.Ctx(ref value, callback);
                }
                
                /// <inheritdoc cref="Observer.Ctx{T}(ref T, Ecs.UserContextFinish{T})"/>
                public void Ctx<T>(ref T value, delegate*<ref T, void> callback)
                {
                    _observer.Ctx(ref value, callback);
                }
                
                /// <inheritdoc cref="Observer.Ctx{T}()"/>
                public ref T Ctx<T>()
                {
                    return ref _observer.Ctx<T>();
                }
            
                /// <inheritdoc cref="Observer.Query()"/>
                public {{Generator.GetTypeName(Type.Query, i)}} Query()
                {
                    return new {{Generator.GetTypeName(Type.Query, i)}}(_observer.Query());
                }
            
                /// <inheritdoc cref="Observer.ToUInt64"/>
                public static implicit operator ulong({{typeName}} observer)
                {
                    return ToUInt64(observer);
                }
            
                /// <inheritdoc cref="Observer.ToId"/>
                public static implicit operator Id({{typeName}} observer)
                {
                    return ToId(observer);
                }
            
                /// <inheritdoc cref="Observer.ToEntity(Observer)"/>
                public static implicit operator Entity({{typeName}} observer)
                {
                    return ToEntity(observer);
                }
            
                /// <inheritdoc cref="Observer.ToUInt64"/>
                public static ulong ToUInt64({{typeName}} observer)
                {
                    return observer.Entity;
                }
            
                /// <inheritdoc cref="Observer.ToId"/>
                public static Id ToId({{typeName}} observer)
                {
                    return observer.Id;
                }
            
                /// <inheritdoc cref="Observer.ToEntity(Observer)"/>
                public static Entity ToEntity({{typeName}} observer)
                {
                    return observer.Entity;
                }
            
                /// <inheritdoc cref="Observer.Equals(Observer)"/>
                public bool Equals({{typeName}} other)
                {
                    return _observer == other._observer;
                }
            
                /// <inheritdoc cref="Observer.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{typeName}} other && Equals(other);
                }
            
                /// <inheritdoc cref="Observer.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _observer.GetHashCode();
                }
            
                /// <inheritdoc cref="Observer.op_Equality"/>
                public static bool operator ==({{typeName}} left, {{typeName}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="Observer.op_Inequality"/>
                public static bool operator !=({{typeName}} left, {{typeName}} right)
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
