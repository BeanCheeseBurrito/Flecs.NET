using System;
using System.Runtime.CompilerServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct World : IDisposable, IEquatable<World>
    {
        public ecs_world_t* Handle { get; private set; }

        public World(ecs_world_t* handle)
        {
            Handle = handle;
        }

        public static World Create()
        {
            return new World(ecs_init());
        }

        public void Dispose()
        {
            if (Handle == null)
                return;

            _ = ecs_fini(Handle);
            Handle = null;
        }

        public bool Progress()
        {
            return ecs_progress(Handle, 0) == 1;
        }

        public ulong Import<T>() where T : IFlecsModule, new()
        {
            return Module.Import<T>(this);
        }

        public bool DeferBegin()
        {
            return ecs_defer_begin(Handle) == 1;
        }

        public bool DeferEnd()
        {
            return ecs_defer_end(Handle) == 1;
        }

        public void Defer(Action func)
        {
            ecs_defer_begin(Handle);
            func();
            ecs_defer_end(Handle);
        }

        private ref World SetInternal<T>(ulong id, ref T component)
        {
            bool isRef = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            int size = isRef ? sizeof(IntPtr) : sizeof(T);

            fixed (void* data = &component)
            {
                IntPtr ptr = default;
                ecs_set_id(Handle, id, id, (ulong)size, isRef ? Managed.AllocGcHandle(&ptr, ref component) : data);
                return ref this;
            }
        }

        public ref World Set<T>(T component)
        {
            return ref SetInternal(Type<T>.Id(Handle), ref component);
        }

        public ref World Set<T>(ref T component)
        {
            return ref SetInternal(Type<T>.Id(Handle), ref component);
        }

        public Id Id<T>()
        {
            return new Id(Handle, Type<T>.Id(Handle));
        }

        public Entity Entity()
        {
            return new Entity(Handle);
        }

        public Entity Entity(ulong id)
        {
            return new Entity(Handle, id);
        }

        public Entity Entity(string name)
        {
            return new Entity(Handle, name);
        }

        public Entity Entity<T>(string name)
        {
            return new Entity(Handle, Type<T>.IdExplicit(Handle, name, true, 0, false));
        }

        public Entity Entity<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return new Entity(Handle, EnumType<TEnum>.Id(enumMember, Handle));
        }

        public Entity Prefab()
        {
            return new Entity(Handle).Add(EcsPrefab);
        }

        public Component<T> Component<T>()
        {
            return new Component<T>(Handle);
        }

        public Entity SetScope(ulong scope)
        {
            return new Entity(ecs_set_scope(Handle, scope));
        }

        public FilterBuilder FilterBuilder()
        {
            return new FilterBuilder(Handle);
        }

        public QueryBuilder QueryBuilder()
        {
            return new QueryBuilder(Handle);
        }

        public ObserverBuilder ObserverBuilder()
        {
            return new ObserverBuilder(Handle);
        }

        public RoutineBuilder RoutineBuilder()
        {
            return new RoutineBuilder(Handle);
        }

        public Filter Filter(FilterBuilder filterBuilder)
        {
            return new Filter(Handle, filterBuilder);
        }

        public Query Query(FilterBuilder filterBuilder = default, QueryBuilder queryBuilder = default)
        {
            return new Query(Handle, filterBuilder, queryBuilder);
        }

        public Observer Observer(string name = "", FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.IterCallback? callback = null)
        {
            return new Observer(Handle, name, filterBuilder, observerBuilder, callback);
        }

        public Observer Observer(ulong entity)
        {
            return new Observer(Handle, entity);
        }

        public Routine Routine(string name = "", FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default,
            RoutineBuilder routineBuilder = default, Ecs.IterCallback? callback = null)
        {
            return new Routine(Handle, name, filterBuilder, queryBuilder, routineBuilder, callback);
        }

        public static implicit operator ecs_world_t*(World world)
        {
            return world.Handle;
        }

        public ecs_world_t* To()
        {
            return Handle;
        }

        public bool Equals(World other)
        {
            return Handle == other.Handle;
        }

        public override bool Equals(object obj)
        {
            return obj is World world && Equals(world);
        }

        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        public static bool operator ==(World left, World right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(World left, World right)
        {
            return !(left == right);
        }
    }
}