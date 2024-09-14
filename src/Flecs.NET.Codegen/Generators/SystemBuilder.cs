using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class SystemBuilder : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            Generator.AddSource(postContext, "SystemBuilder.QueryBuilder.g.cs", QueryBuilder.GenerateExtensions(Type.SystemBuilder));

            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"SystemBuilder/T{i + 1}.g.cs", GenerateSystemBuilder(i));
                Generator.AddSource(postContext, $"SystemBuilder.QueryBuilder/T{i + 1}.g.cs", QueryBuilder.GenerateExtensions(Type.SystemBuilder, i));
                Generator.AddSource(postContext, $"SystemBuilder.NodeBuilder/T{i + 1}.g.cs", NodeBuilder.GenerateExtensions(Type.SystemBuilder, Type.System, i));
            }
        });
    }

    private static string GenerateSystemBuilder(int i)
    {
        return $$"""
        #nullable enable
        
        using System;
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;
        
        /// <summary>
        ///     A type-safe wrapper over <see cref="SystemBuilder"/> that takes {{i + 1}} type arguments.
        /// </summary>
        /// {{Generator.XmlTypeParameters[i]}}
        public unsafe partial struct {{Generator.GetTypeName(Type.SystemBuilder, i)}} : IDisposable, IEquatable<{{Generator.GetTypeName(Type.SystemBuilder, i)}}>, IQueryBuilder<{{Generator.GetTypeName(Type.SystemBuilder, i)}}, {{Generator.GetTypeName(Type.System, i)}}>
        {
            private SystemBuilder _systemBuilder;
        
            /// <inheritdoc cref="SystemBuilder.World"/>
            public ref ecs_world_t* World => ref _systemBuilder.World;
        
            /// <inheritdoc cref="SystemBuilder.Desc"/>
            public ref ecs_system_desc_t Desc => ref _systemBuilder.Desc;
        
            /// <inheritdoc cref="SystemBuilder.QueryBuilder"/>
            public ref QueryBuilder QueryBuilder => ref _systemBuilder.QueryBuilder;
        
            /// <summary>
            ///     Creates a system builder with the provided system builder.
            /// </summary>
            /// <param name="systemBuilder">The system builder.</param>
            public SystemBuilder(SystemBuilder systemBuilder)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _systemBuilder = systemBuilder;
            }
        
            /// <inheritdoc cref="SystemBuilder(ecs_world_t*)"/>
            public SystemBuilder(ecs_world_t* world)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _systemBuilder = new SystemBuilder(world){{Generator.WithChain[i]}};
            }
        
            /// <inheritdoc cref="SystemBuilder(ecs_world_t*, string)"/>
            public SystemBuilder(ecs_world_t* world, string name)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _systemBuilder = new SystemBuilder(world, name){{Generator.WithChain[i]}};
            }
        
            /// <inheritdoc cref="SystemBuilder.Dispose()"/>
            public void Dispose()
            {
                _systemBuilder.Dispose();
            }
        
            /// <inheritdoc cref="SystemBuilder.Kind(ulong)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Kind(ulong phase)
            {
                _systemBuilder.Kind(phase);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Kind{T}()"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Kind<T>()
            {
                _systemBuilder.Kind<T>();
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Kind{T}(T)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Kind<T>(T value) where T : Enum
            {
                _systemBuilder.Kind(value);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.MultiThreaded(bool)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} MultiThreaded(bool value = true)
            {
                _systemBuilder.MultiThreaded();
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Immediate(bool)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Immediate(bool value = true)
            {
                _systemBuilder.Immediate(value);
                return ref this;
            }
            
            /// <inheritdoc cref="SystemBuilder.Interval(float)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Interval(float interval)
            {
                _systemBuilder.Interval(interval);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Rate(ulong, int)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Rate(ulong tickSource, int rate)
            {
                _systemBuilder.Rate(tickSource, rate);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Rate(int)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Rate(int rate)
            {
                _systemBuilder.Rate(rate);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.TickSource(ulong)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} TickSource(ulong tickSource)
            {
                _systemBuilder.TickSource(tickSource);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.TickSource{T}()"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} TickSource<T>()
            {
                _systemBuilder.TickSource<T>();
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Ctx"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Ctx(void* ctx)
            {
                _systemBuilder.Ctx(ctx);
                return ref this;
            }
            
            /// <inheritdoc cref="SystemBuilder.Run(System.Action)"/>
            public {{Generator.GetTypeName(Type.System, i)}} Run(Action callback)
            {
                return new {{Generator.GetTypeName(Type.System, i)}}(_systemBuilder.Run(callback));
            }
            
            /// <inheritdoc cref="SystemBuilder.Run(System.Action)"/>
            public {{Generator.GetTypeName(Type.System, i)}} Run(delegate*<void> callback)
            {
                return new {{Generator.GetTypeName(Type.System, i)}}(_systemBuilder.Run(callback));
            }
        
            /// <inheritdoc cref="SystemBuilder.Run(Ecs.RunDelegateCallback)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Run(Ecs.RunDelegateCallback callback)
            {
                _systemBuilder.Run(callback);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Run(Ecs.RunDelegateCallback)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Run(delegate*<Iter, Action<Iter>, void> callback)
            {
                _systemBuilder.Run(callback);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Run(Ecs.RunPointerCallback)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Run(Ecs.RunPointerCallback callback)
            {
                _systemBuilder.Run(callback);
                return ref this;
            }
        
            /// <inheritdoc cref="SystemBuilder.Run(Ecs.RunPointerCallback)"/>
            public ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} Run(delegate*<Iter, delegate*<Iter, void>, void> callback)
            {
                _systemBuilder.Run(callback);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} SetCallback<T>(T callback, IntPtr invoker) where T : Delegate
            {
                _systemBuilder.SetCallback(callback, invoker);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} SetCallback(IntPtr callback, IntPtr invoker)
            {
                _systemBuilder.SetCallback(callback, invoker);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} SetRun<T>(T callback, IntPtr invoker) where T : Delegate
            {
                _systemBuilder.SetRun(callback, invoker);
                return ref this;
            }
        
            internal ref {{Generator.GetTypeName(Type.SystemBuilder, i)}} SetRun(IntPtr callback, IntPtr invoker)
            {
                _systemBuilder.SetRun(callback, invoker);
                return ref this;
            }
        
            internal {{Generator.GetTypeName(Type.System, i)}} Build()
            {
                return new {{Generator.GetTypeName(Type.System, i)}}(_systemBuilder.Build());
            }
            
            {{Generator.GetTypeName(Type.System, i)}} IQueryBuilder<{{Generator.GetTypeName(Type.SystemBuilder, i)}}, {{Generator.GetTypeName(Type.System, i)}}>.Build()
            {
                return Build();
            }
        
            /// <inheritdoc cref="SystemBuilder.Equals(SystemBuilder)"/>
            public bool Equals({{Generator.GetTypeName(Type.SystemBuilder, i)}} other)
            {
                return _systemBuilder == other._systemBuilder;
            }
        
            /// <inheritdoc cref="SystemBuilder.Equals(object)"/>
            public override bool Equals(object? obj)
            {
                return obj is {{Generator.GetTypeName(Type.SystemBuilder, i)}} other && Equals(other);
            }
        
            /// <inheritdoc cref="SystemBuilder.GetHashCode()"/>
            public override int GetHashCode()
            {
                return HashCode.Combine(Desc.GetHashCode(), QueryBuilder.GetHashCode());
            }
        
            /// <inheritdoc cref="SystemBuilder.op_Equality"/>
            public static bool operator ==({{Generator.GetTypeName(Type.SystemBuilder, i)}} left, {{Generator.GetTypeName(Type.SystemBuilder, i)}} right)
            {
                return left.Equals(right);
            }
        
            /// <inheritdoc cref="SystemBuilder.op_Inequality"/>
            public static bool operator !=({{Generator.GetTypeName(Type.SystemBuilder, i)}} left, {{Generator.GetTypeName(Type.SystemBuilder, i)}} right)
            {
                return !(left == right);
            }
        }
        """;
    }
}
