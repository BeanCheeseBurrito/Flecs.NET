using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class ObserverBuilder : GeneratorBase
{
    public override void Generate()
    {
        AddSource("ObserverBuilder.QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(Type.ObserverBuilder));

        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"ObserverBuilder/T{i + 1}.g.cs", GenerateObserverBuilder(i));
            AddSource($"ObserverBuilder.QueryBuilder/T{i + 1}.g.cs",
                QueryBuilder.GenerateExtensions(Type.ObserverBuilder, i));
            AddSource($"ObserverBuilder.NodeBuilder/T{i + 1}.g.cs",
                NodeBuilder.GenerateExtensions(Type.ObserverBuilder, Type.Observer, i));
        }
    }

    private static string GenerateObserverBuilder(int i)
    {
        return $$"""
            #nullable enable

            using System;
            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     A type-safe wrapper around <see cref="ObserverBuilder"/> that takes {{i + 1}} type arguments.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe partial struct {{Generator.GetTypeName(Type.ObserverBuilder, i)}} : IDisposable, IEquatable<{{Generator.GetTypeName(Type.ObserverBuilder, i)}}>, IQueryBuilder<{{Generator.GetTypeName(Type.ObserverBuilder, i)}}, {{Generator.GetTypeName(Type.Observer, i)}}>
            {
                private ObserverBuilder _observerBuilder;
            
                /// <inheritdoc cref="ObserverBuilder.World"/>
                public ref ecs_world_t* World => ref _observerBuilder.World;
            
                /// <inheritdoc cref="ObserverBuilder.Desc"/>
                public ref ecs_observer_desc_t Desc => ref _observerBuilder.Desc;
            
                /// <inheritdoc cref="ObserverBuilder.QueryBuilder"/>
                public ref QueryBuilder QueryBuilder => ref _observerBuilder.QueryBuilder;
            
                /// <summary>
                ///     Creates an observer builder with the provided observer builder.
                /// </summary>
                /// <param name="observerBuilder">The observer builder.</param>
                public ObserverBuilder(ObserverBuilder observerBuilder)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                    _observerBuilder = observerBuilder;
                }
            
                /// <inheritdoc cref="ObserverBuilder(ecs_world_t*)"/>
                public ObserverBuilder(ecs_world_t* world)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                    _observerBuilder = new ObserverBuilder(world){{Generator.WithChain[i]}};
                }
            
                /// <inheritdoc cref="ObserverBuilder(ecs_world_t*, string)"/>
                public ObserverBuilder(ecs_world_t* world, string name)
                {
                    {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                    _observerBuilder = new ObserverBuilder(world, name){{Generator.WithChain[i]}};
                }
            
                /// <inheritdoc cref="ObserverBuilder.Dispose()"/>
                public void Dispose()
                {
                    _observerBuilder.Dispose();
                }
            
                /// <inheritdoc cref="ObserverBuilder.Event(ulong)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Event(ulong @event)
                {
                    _observerBuilder.Event(@event);
                    return ref this;
                }
            
                /// <inheritdoc cref="ObserverBuilder.Event{T}()"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Event<T>()
                {
                    _observerBuilder.Event<T>();
                    return ref this;
                }
            
                /// <inheritdoc cref="ObserverBuilder.YieldExisting(bool)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} YieldExisting(bool value = true)
                {
                    _observerBuilder.YieldExisting(value);
                    return ref this;
                }
            
                /// <inheritdoc cref="ObserverBuilder.Ctx{T}(T)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Ctx<T>(T value)
                {
                    _observerBuilder.Ctx(ref value);
                    return ref this;
                }
                
                /// <inheritdoc cref="ObserverBuilder.Ctx{T}(T, Ecs.UserContextFinish{T})"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Ctx<T>(T value, Ecs.UserContextFinish<T> callback)
                {
                    _observerBuilder.Ctx(ref value, callback);
                    return ref this;
                }
                
                /// <inheritdoc cref="ObserverBuilder.Ctx{T}(T, Ecs.UserContextFinish{T})"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Ctx<T>(T value, delegate*<ref T, void> callback)
                {
                    _observerBuilder.Ctx(ref value, callback);
                    return ref this;
                }
                
                /// <inheritdoc cref="ObserverBuilder.Ctx{T}(ref T)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Ctx<T>(ref T value)
                {
                    _observerBuilder.Ctx(ref value);
                    return ref this;
                }
                
                /// <inheritdoc cref="ObserverBuilder.Ctx{T}(ref T, Ecs.UserContextFinish{T})"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Ctx<T>(ref T value, Ecs.UserContextFinish<T> callback)
                {
                    _observerBuilder.Ctx(ref value, callback);
                    return ref this;
                }
                
                /// <inheritdoc cref="ObserverBuilder.Ctx{T}(ref T, Ecs.UserContextFinish{T})"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Ctx<T>(ref T value, delegate*<ref T, void> callback)
                {
                    _observerBuilder.Ctx(ref value, callback);
                    return ref this;
                }
                
                /// <inheritdoc cref="ObserverBuilder.Run(System.Action)"/>
                public {{Generator.GetTypeName(Type.Observer, i)}} Run(Action callback)
                {
                    return new {{Generator.GetTypeName(Type.Observer, i)}}(_observerBuilder.Run(callback));
                }
                
                /// <inheritdoc cref="ObserverBuilder.Run(System.Action)"/>
                public {{Generator.GetTypeName(Type.Observer, i)}} Run(delegate*<void> callback)
                {
                    return new {{Generator.GetTypeName(Type.Observer, i)}}(_observerBuilder.Run(callback));
                }
            
                /// <inheritdoc cref="ObserverBuilder.Run(Ecs.RunDelegateCallback)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Run(Ecs.RunDelegateCallback callback)
                {
                    _observerBuilder.Run(callback);
                    return ref this;
                }
            
                /// <inheritdoc cref="ObserverBuilder.Run(Ecs.RunDelegateCallback)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Run(delegate*<Iter, Action<Iter>, void> callback)
                {
                    _observerBuilder.Run(callback);
                    return ref this;
                }
            
                /// <inheritdoc cref="ObserverBuilder.Run(Ecs.RunPointerCallback)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Run(Ecs.RunPointerCallback callback)
                {
                    _observerBuilder.Run(callback);
                    return ref this;
                }
            
                /// <inheritdoc cref="ObserverBuilder.Run(Ecs.RunPointerCallback)"/>
                public ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} Run(delegate*<Iter, delegate*<Iter, void>, void> callback)
                {
                    _observerBuilder.Run(callback);
                    return ref this;
                }
            
                private ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} SetCallback<T>(T callback, void* invoker) where T : Delegate
                {
                    _observerBuilder.SetCallback(callback, invoker);
                    return ref this;
                }
            
                private ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} SetCallback(void* callback, void* invoker)
                {
                    _observerBuilder.SetCallback(callback, invoker);
                    return ref this;
                }
            
                private ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} SetRun<T>(T callback, void* invoker) where T : Delegate
                {
                    _observerBuilder.SetRun(callback, invoker);
                    return ref this;
                }
            
                private ref {{Generator.GetTypeName(Type.ObserverBuilder, i)}} SetRun(void* callback, void* invoker)
                {
                    _observerBuilder.SetRun(callback, invoker);
                    return ref this;
                }
            
                private {{Generator.GetTypeName(Type.Observer, i)}} Build()
                {
                    return new {{Generator.GetTypeName(Type.Observer, i)}}(_observerBuilder.Build());
                }
            
                {{Generator.GetTypeName(Type.Observer, i)}} IQueryBuilder<{{Generator.GetTypeName(Type.ObserverBuilder, i)}}, {{Generator.GetTypeName(Type.Observer, i)}}>.Build()
                {
                    return Build();
                }
            
                /// <inheritdoc cref="ObserverBuilder.Equals(ObserverBuilder)"/>
                public bool Equals({{Generator.GetTypeName(Type.ObserverBuilder, i)}} other)
                {
                    return _observerBuilder == other._observerBuilder;
                }
            
                /// <inheritdoc cref="ObserverBuilder.Equals(object)"/>
                public override bool Equals(object? obj)
                {
                    return obj is {{Generator.GetTypeName(Type.ObserverBuilder, i)}} other && Equals(other);
                }
            
                /// <inheritdoc cref="ObserverBuilder.GetHashCode()"/>
                public override int GetHashCode()
                {
                    return _observerBuilder.GetHashCode();
                }
            
                /// <inheritdoc cref="ObserverBuilder.op_Equality"/>
                public static bool operator ==({{Generator.GetTypeName(Type.ObserverBuilder, i)}} left, {{Generator.GetTypeName(Type.ObserverBuilder, i)}} right)
                {
                    return left.Equals(right);
                }
            
                /// <inheritdoc cref="ObserverBuilder.op_Inequality"/>
                public static bool operator !=({{Generator.GetTypeName(Type.ObserverBuilder, i)}} left, {{Generator.GetTypeName(Type.ObserverBuilder, i)}} right)
                {
                    return !(left == right);
                }
            }
            """;
    }
}
