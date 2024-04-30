using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_query_t.
    /// </summary>
    public unsafe partial struct Query : IEquatable<Query>, IDisposable
    {
        private ecs_world_t* _world;
        private ecs_query_t* _handle;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_query_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a query from a world and handle.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="query"></param>
        public Query(ecs_world_t* world, ecs_query_t* query = null)
        {
            _world = world;
            _handle = query;
        }

        /// <summary>
        ///     Creates a query from a handle.
        /// </summary>
        /// <param name="query"></param>
        public Query(ecs_query_t* query)
        {
            _world = query->world;
            _handle = query;
        }

        /// <summary>
        ///     Disposes query.
        /// </summary>
        public void Dispose()
        {
            Destruct();
        }

        /// <summary>
        ///     Destructs query and cleans up resources.
        /// </summary>
        public void Destruct()
        {
            if (Handle == null)
                return;

            ecs_query_fini(Handle);
            World = null;
            Handle = null;
        }

        /// <summary>
        ///     Returns the entity associated with the query.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, Handle->entity);
        }

        /// <summary>
        ///     Returns whether the query data changed since the last iteration.
        /// </summary>
        /// <returns></returns>
        public bool Changed()
        {
            return Macros.Bool(ecs_query_changed(Handle));
        }

        /// <summary>
        ///     Get info for group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ecs_query_group_info_t* GroupInfo(ulong groupId)
        {
            return ecs_query_get_group_info(Handle, groupId);
        }

        /// <summary>
        ///     Get context for group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public void* GroupCtx(ulong groupId)
        {
            ecs_query_group_info_t* groupInfo = GroupInfo(groupId);
            return groupInfo == null ? null : groupInfo->ctx;
        }

        /// <summary>
        ///     Iterates terms with the provided callback.
        /// </summary>
        /// <param name="callback"></param>
        public void EachTerm(Ecs.TermCallback callback)
        {
            for (int i = 0; i < Handle->term_count; i++)
            {
                Term term = new Term(World, Handle->terms[i]);
                callback(ref term);
            }
        }

        /// <summary>
        ///     Gets term at provided index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Term Term(int index)
        {
            Ecs.Assert(index < Handle->term_count, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
            return new Term(World, Handle->terms[index]);
        }

        /// <summary>
        ///     Gets term count.
        /// </summary>
        /// <returns></returns>
        public int TermCount()
        {
            return Handle->term_count;
        }

        /// <summary>
        ///     Gets field count.
        /// </summary>
        /// <returns></returns>
        public int FieldCount()
        {
            return Handle->field_count;
        }

        /// <summary>
        ///     Searches for a variable by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindVar(string name)
        {
            using NativeString nativeName = (NativeString)name;
            return ecs_query_find_var(Handle, nativeName);
        }

        /// <summary>
        ///     Returns the string of the query.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_query_str(Handle));
        }

        /// <summary>
        ///     Returns a string representing the query plan.
        /// </summary>
        /// <returns></returns>
        public string Plan()
        {
            return NativeString.GetStringAndFree(ecs_query_plan(Handle));
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Iter(Ecs.IterCallback callback)
        {
            Iter(World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="callback">The callback.</param>
        public void Iter(Entity entity, Ecs.IterCallback callback)
        {
            Iter(entity.World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="it">The iter.</param>
        /// <param name="callback">The callback.</param>
        public void Iter(Iter it, Ecs.IterCallback callback)
        {
            Iter(it.World(), callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="callback">The callback.</param>
        public void Iter(World world, Ecs.IterCallback callback)
        {
            ecs_iter_t iter = ecs_query_iter(world, Handle);
            while (ecs_query_next(&iter) == 1)
                Invoker.Iter(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(Ecs.EachEntityCallback callback)
        {
            Each(World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Entity entity, Ecs.EachEntityCallback callback)
        {
            Each(entity.World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="it">The iter.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Iter it, Ecs.EachEntityCallback callback)
        {
            Each(it.World(), callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="callback">The callback.</param>
        public void Each(World world, Ecs.EachEntityCallback callback)
        {
            ecs_iter_t iter = ecs_query_iter(world, Handle);
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(Ecs.EachIndexCallback callback)
        {
            Each(World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Entity entity, Ecs.EachIndexCallback callback)
        {
            Each(entity.World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="it">The iter.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Iter it, Ecs.EachIndexCallback callback)
        {
            Each(it.World(), callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="callback">The callback.</param>
        public void Each(World world, Ecs.EachIndexCallback callback)
        {
            ecs_iter_t iter = ecs_query_iter(world, Handle);
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Iter(delegate*<Iter, void> callback)
        {
            Iter(World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="callback">The callback.</param>
        public void Iter(Entity entity, delegate*<Iter, void> callback)
        {
            Iter(entity.World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="it">The iter.</param>
        /// <param name="callback">The callback.</param>
        public void Iter(Iter it, delegate*<Iter, void> callback)
        {
            Iter(it.World(), callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="callback">The callback.</param>
        public void Iter(World world, delegate*<Iter, void> callback)
        {
            ecs_iter_t iter = ecs_query_iter(world, Handle);
            while (ecs_query_next(&iter) == 1)
                Invoker.Iter(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(delegate*<Entity, void> callback)
        {
            Each(World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Entity entity, delegate*<Entity, void> callback)
        {
            Each(entity.World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="it">The iter.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Iter it, delegate*<Entity, void> callback)
        {
            Each(it.World(), callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="callback">The callback.</param>
        public void Each(World world, delegate*<Entity, void> callback)
        {
            ecs_iter_t iter = ecs_query_iter(world, Handle);
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(delegate*<Iter, int, void> callback)
        {
            Each(World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Entity entity, delegate*<Iter, int, void> callback)
        {
            Each(entity.World, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="it">The iter.</param>
        /// <param name="callback">The callback.</param>
        public void Each(Iter it, delegate*<Iter, int, void> callback)
        {
            Each(it.World(), callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="callback">The callback.</param>
        public void Each(World world, delegate*<Iter, int, void>callback)
        {
            ecs_iter_t iter = ecs_query_iter(world, Handle);
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }
#endif

        /// <summary>
        ///     Create an iterator object that can be modified before iterating.
        /// </summary>
        /// <returns></returns>
        public IterIterable Iter()
        {
            return new IterIterable(ecs_query_iter(World, Handle));
        }

        /// <summary>
        ///     Return number of entities matched by iterable.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Iter().Count();
        }

        /// <summary>
        ///     Return whether iterable has any matches.
        /// </summary>
        /// <returns></returns>
        public bool IsTrue()
        {
            return Iter().IsTrue();
        }

        /// <summary>
        ///     Return first entity matched by iterable.
        /// </summary>
        /// <returns></returns>
        public Entity First()
        {
            return Iter().First();
        }

        /// <summary>
        ///     Converts a <see cref="Query"/> instance to an <see cref="ecs_query_t"/>*.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static ecs_query_t* To(Query query)
        {
            return query.Handle;
        }

        /// <summary>
        ///     Returns true if query handle is not a null pointer, otherwise return false.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool ToBoolean(Query query)
        {
            return query.Handle != null;
        }

        /// <summary>
        ///     Converts a <see cref="Query"/> instance to an <see cref="ecs_query_t"/>*.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static implicit operator ecs_query_t*(Query query)
        {
            return To(query);
        }

        /// <summary>
        ///     Returns true if query handle is not a null pointer, otherwise return false.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static implicit operator bool(Query query)
        {
            return ToBoolean(query);
        }

        /// <summary>
        ///     Checks if two <see cref="Query"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Query other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="Query"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Query other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Query"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Query"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Query left, Query right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Query"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Query left, Query right)
        {
            return !(left == right);
        }
    }
}
