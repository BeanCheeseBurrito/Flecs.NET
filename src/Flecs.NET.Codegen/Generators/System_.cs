using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class System_ : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"System/T{i + 1}.g.cs", GenerateSystem(i));
            }
        });
    }

    private static string GenerateSystem(int i)
    {
        string systemTypeName = Generator.GetTypeName(Type.System, i);

        return $$"""
        #nullable enable
        
        using System;
        using static Flecs.NET.Bindings.flecs;
        
        namespace Flecs.NET.Core;
        
        /// <summary>
        ///     A type-safe wrapper around <see cref="System"/> that takes {{i + 1}} type arguments.
        /// </summary>
        /// {{Generator.XmlTypeParameters[i]}}
        public unsafe struct {{systemTypeName}} : IEquatable<{{systemTypeName}}>, IEntity
        {
            private System_ _system;
        
            /// <inheritdoc cref="System_.Entity"/>
            public ref Entity Entity => ref _system.Entity;
        
            /// <inheritdoc cref="System_.Id"/>
            public ref Id Id => ref _system.Id;
        
            /// <inheritdoc cref="System_.World"/>
            public ref ecs_world_t* World => ref _system.World;
        
            /// <summary>
            ///     Creates a new system with the provided system.
            /// </summary>
            /// <param name="system">The system.</param>
            public System(System_ system)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _system = system;
            }
        
            /// <inheritdoc cref="System_(ecs_world_t*, ulong)"/>
            public System(ecs_world_t* world, ulong entity)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _system = new System_(world, entity);
            }
        
            /// <inheritdoc cref="System_(Core.Entity)"/>
            public System(Entity entity)
            {
                {{Generator.GetTypeName(Type.TypeHelper, i)}}.AssertNoTags();
                _system = new System_(entity);
            }
        
            /// <inheritdoc cref="System_.Destruct()"/>
            public void Destruct()
            {
                _system.Destruct();
            }
        
            ///
            public void Ctx(void* ctx)
            {
                _system.Ctx(ctx);
            }
        
            /// <inheritdoc cref="System_.Ctx()"/>
            public void* Ctx()
            {
                return _system.Ctx();
            }
        
            /// <inheritdoc cref="System_.Query()"/>
            public Query<T0> Query()
            {
                return new Query<T0>(ecs_system_get(World, Entity)->query);
            }
        
            /// <inheritdoc cref="System_.Run(float)"/>
            public void Run(float deltaTime = 0)
            {
                _system.Run(deltaTime);
            }
        
            /// <inheritdoc cref="System_.RunWithParam"/>
            public void RunWithParam(float deltaTime = 0, void* param = null)
            {
                _system.RunWithParam(deltaTime, param);
            }
        
            /// <inheritdoc cref="System_.RunWorker"/>
            public void RunWorker(int stageCurrent, int stageCount, float deltaTime = 0)
            {
                _system.RunWorker(stageCurrent, stageCount, deltaTime);
            }
        
            /// <inheritdoc cref="System_.RunWorkerWithParam"/>
            public void RunWorkerWithParam(int stageCurrent, int stageCount, float deltaTime = 0, void* param = null)
            {
                _system.RunWorkerWithParam(stageCurrent, stageCount, deltaTime, param);
            }
        
            /// <inheritdoc cref="System_.Interval(float)"/>
            public void Interval(float interval)
            {
                _system.Interval(interval);
            }
        
            /// <inheritdoc cref="System_.Interval()"/>
            public float Interval()
            {
                return _system.Interval();
            }
        
            /// <inheritdoc cref="System_.Timeout(float)"/>
            public void Timeout(float timeout)
            {
                _system.Timeout(timeout);
            }
        
            /// <inheritdoc cref="System_.Timeout()"/>
            public float Timeout()
            {
                return _system.Timeout();
            }
        
            /// <inheritdoc cref="System_.Rate(int)"/>
            public void Rate(int rate)
            {
                _system.Rate(rate);
            }
        
            /// <inheritdoc cref="System_.Start()"/>
            public void Start()
            {
                _system.Start();
            }
        
            /// <inheritdoc cref="System_.StopTimer()"/>
            public void StopTimer()
            {
                _system.StopTimer();
            }
        
            /// <inheritdoc cref="System_.SetTickSource(ulong)"/>
            public void SetTickSource(ulong entity)
            {
                _system.SetTickSource(entity);
            }
        
            /// <inheritdoc cref="System_.SetTickSource(TimerEntity)"/>
            public void SetTickSource(TimerEntity timerEntity)
            {
                _system.SetTickSource(timerEntity);
            }
        
            /// <inheritdoc cref="System_.SetTickSource{T}()"/>
            public void SetTickSource<T>()
            {
                _system.SetTickSource<T>();
            }
        
            /// <inheritdoc cref="System_.ToUInt64"/>
            public static implicit operator ulong({{systemTypeName}} system)
            {
                return ToUInt64(system);
            }
        
            /// <inheritdoc cref="System_.ToId"/>
            public static implicit operator Id({{systemTypeName}} system)
            {
                return ToId(system);
            }
        
            /// <inheritdoc cref="System_.ToEntity"/>
            public static implicit operator Entity({{systemTypeName}} system)
            {
                return ToEntity(system);
            }
        
            /// <inheritdoc cref="System_.ToUInt64"/>
            public static ulong ToUInt64({{systemTypeName}} system)
            {
                return system.Entity;
            }
        
            /// <inheritdoc cref="System_.ToId"/>
            public static Id ToId({{systemTypeName}} system)
            {
                return system.Id;
            }
        
            /// <inheritdoc cref="System_.ToEntity"/>
            public static Entity ToEntity({{systemTypeName}} system)
            {
                return system.Entity;
            }
        
            /// <inheritdoc cref="System_.ToString()"/>
            public override string ToString()
            {
                return _system.ToString();
            }
        
            /// <inheritdoc cref="System_.Equals(System_)"/>
            public bool Equals({{systemTypeName}} other)
            {
                return _system == other._system;
            }
        
            /// <inheritdoc cref="System_.Equals(object)"/>
            public override bool Equals(object? obj)
            {
                return obj is {{systemTypeName}} system && Equals(system);
            }
        
            /// <inheritdoc cref="System_.GetHashCode()"/>
            public override int GetHashCode()
            {
                return _system.GetHashCode();
            }
        
            /// <inheritdoc cref="System_.op_Equality"/>
            public static bool operator ==({{systemTypeName}} left, {{systemTypeName}} right)
            {
                return left.Equals(right);
            }
        
            /// <inheritdoc cref="System_.op_Inequality"/>
            public static bool operator !=({{systemTypeName}} left, {{systemTypeName}} right)
            {
                return !(left == right);
            }
        }
        """;
    }
}
