using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class RoutineBuilder : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            Generator.AddSource(postContext, "RoutineBuilder.QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(Type.RoutineBuilder));

            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"RoutineBuilder/T{i + 1}.g.cs", GenerateRoutineBuilder(i));
                Generator.AddSource(postContext, $"RoutineBuilder.QueryBuilder/T{i + 1}.g.cs", QueryBuilder.GenerateExtensions(Type.RoutineBuilder, i));
                Generator.AddSource(postContext, $"RoutineBuilder.NodeBuilder/T{i + 1}.g.cs", NodeBuilder.GenerateExtensions(Type.RoutineBuilder, Type.Routine, i));
            }
        });
    }

    private static string GenerateRoutineBuilder(int i)
    {
        return $$"""
        #nullable enable
        
        using System;
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;
        
        /// <summary>
        ///     A type-safe wrapper over <see cref="RoutineBuilder"/> that takes {{i + 1}} type arguments.
        /// </summary>
        /// {{Generator.XmlTypeParameters[i]}}
        public unsafe partial struct {{Generator.GetTypeName(Type.RoutineBuilder, i)}} : IDisposable, IEquatable<{{Generator.GetTypeName(Type.RoutineBuilder, i)}}>, IQueryBuilder<{{Generator.GetTypeName(Type.RoutineBuilder, i)}}, {{Generator.GetTypeName(Type.Routine, i)}}>
        {
            private RoutineBuilder _routineBuilder;
        
            /// <inheritdoc cref="RoutineBuilder.World"/>
            public ref ecs_world_t* World => ref _routineBuilder.World;
        
            /// <inheritdoc cref="RoutineBuilder.Desc"/>
            public ref ecs_system_desc_t Desc => ref _routineBuilder.Desc;
        
            /// <inheritdoc cref="RoutineBuilder.QueryBuilder"/>
            public ref QueryBuilder QueryBuilder => ref _routineBuilder.QueryBuilder;
        
            /// <summary>
            ///     Creates a routine builder with the provided routine builder.
            /// </summary>
            /// <param name="routineBuilder">The routine builder.</param>
            public RoutineBuilder(RoutineBuilder routineBuilder)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _routineBuilder = routineBuilder;
            }
        
            /// <inheritdoc cref="RoutineBuilder(ecs_world_t*)"/>
            public RoutineBuilder(ecs_world_t* world)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _routineBuilder = new RoutineBuilder(world){{Generator.WithChain[i]}};
            }
        
            /// <inheritdoc cref="RoutineBuilder(ecs_world_t*, string)"/>
            public RoutineBuilder(ecs_world_t* world, string name)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _routineBuilder = new RoutineBuilder(world, name){{Generator.WithChain[i]}};
            }
        
            /// <inheritdoc cref="RoutineBuilder.Dispose()"/>
            public void Dispose()
            {
                _routineBuilder.Dispose();
            }
        
            /// <inheritdoc cref="RoutineBuilder.Kind(ulong)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Kind(ulong phase)
            {
                _routineBuilder.Kind(phase);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Kind{T}()"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Kind<T>()
            {
                _routineBuilder.Kind<T>();
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Kind{T}(T)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Kind<T>(T value) where T : Enum
            {
                _routineBuilder.Kind(value);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.MultiThreaded(bool)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} MultiThreaded(bool value = true)
            {
                _routineBuilder.MultiThreaded();
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Immediate(bool)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Immediate(bool value = true)
            {
                _routineBuilder.Immediate(value);
                return ref this;
            }
            
            /// <inheritdoc cref="RoutineBuilder.Interval(float)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Interval(float interval)
            {
                _routineBuilder.Interval(interval);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Rate(ulong, int)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Rate(ulong tickSource, int rate)
            {
                _routineBuilder.Rate(tickSource, rate);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Rate(int)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Rate(int rate)
            {
                _routineBuilder.Rate(rate);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.TickSource(ulong)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} TickSource(ulong tickSource)
            {
                _routineBuilder.TickSource(tickSource);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.TickSource{T}()"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} TickSource<T>()
            {
                _routineBuilder.TickSource<T>();
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Ctx"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Ctx(void* ctx)
            {
                _routineBuilder.Ctx(ctx);
                return ref this;
            }
            
            /// <inheritdoc cref="RoutineBuilder.Run(System.Action)"/>
            public {{Generator.GetTypeName(Type.Routine, i)}} Run(Action callback)
            {
                return new {{Generator.GetTypeName(Type.Routine, i)}}(_routineBuilder.Run(callback));
            }
            
            /// <inheritdoc cref="RoutineBuilder.Run(System.Action)"/>
            public {{Generator.GetTypeName(Type.Routine, i)}} Run(delegate*<void> callback)
            {
                return new {{Generator.GetTypeName(Type.Routine, i)}}(_routineBuilder.Run(callback));
            }
        
            /// <inheritdoc cref="RoutineBuilder.Run(Ecs.RunDelegateCallback)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Run(Ecs.RunDelegateCallback callback)
            {
                _routineBuilder.Run(callback);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Run(Ecs.RunDelegateCallback)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Run(delegate*<Iter, Action<Iter>, void> callback)
            {
                _routineBuilder.Run(callback);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Run(Ecs.RunPointerCallback)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Run(Ecs.RunPointerCallback callback)
            {
                _routineBuilder.Run(callback);
                return ref this;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Run(Ecs.RunPointerCallback)"/>
            public ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} Run(delegate*<Iter, delegate*<Iter, void>, void> callback)
            {
                _routineBuilder.Run(callback);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} SetCallback<T>(T callback, IntPtr invoker) where T : Delegate
            {
                _routineBuilder.SetCallback(callback, invoker);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} SetCallback(IntPtr callback, IntPtr invoker)
            {
                _routineBuilder.SetCallback(callback, invoker);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} SetRun<T>(T callback, IntPtr invoker) where T : Delegate
            {
                _routineBuilder.SetRun(callback, invoker);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.RoutineBuilder, i)}} SetRun(IntPtr callback, IntPtr invoker)
            {
                _routineBuilder.SetRun(callback, invoker);
                return ref this;
            }
        
            internal {{Generator.GetTypeName(Type.Routine, i)}} Build()
            {
                return new {{Generator.GetTypeName(Type.Routine, i)}}(_routineBuilder.Build());
            }
            
            {{Generator.GetTypeName(Type.Routine, i)}} IQueryBuilder<{{Generator.GetTypeName(Type.RoutineBuilder, i)}}, {{Generator.GetTypeName(Type.Routine, i)}}>.Build()
            {
                return Build();
            }
        
            /// <inheritdoc cref="RoutineBuilder.Equals(RoutineBuilder)"/>
            public bool Equals({{Generator.GetTypeName(Type.RoutineBuilder, i)}} other)
            {
                return _routineBuilder == other._routineBuilder;
            }
        
            /// <inheritdoc cref="RoutineBuilder.Equals(object)"/>
            public override bool Equals(object? obj)
            {
                return obj is {{Generator.GetTypeName(Type.RoutineBuilder, i)}} other && Equals(other);
            }
        
            /// <inheritdoc cref="RoutineBuilder.GetHashCode()"/>
            public override int GetHashCode()
            {
                return HashCode.Combine(Desc.GetHashCode(), QueryBuilder.GetHashCode());
            }
        
            /// <inheritdoc cref="RoutineBuilder.op_Equality"/>
            public static bool operator ==({{Generator.GetTypeName(Type.RoutineBuilder, i)}} left, {{Generator.GetTypeName(Type.RoutineBuilder, i)}} right)
            {
                return left.Equals(right);
            }
        
            /// <inheritdoc cref="RoutineBuilder.op_Inequality"/>
            public static bool operator !=({{Generator.GetTypeName(Type.RoutineBuilder, i)}} left, {{Generator.GetTypeName(Type.RoutineBuilder, i)}} right)
            {
                return !(left == right);
            }
        }
        """;
    }
}
