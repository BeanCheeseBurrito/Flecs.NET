#if !NET5_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     An iterator object that can be modified before iterating.
    /// </summary>
    public unsafe partial struct IterIterable : IEquatable<IterIterable>
    {
        private ecs_iter_t _iter;

        /// <summary>
        ///     Creates an iter iterable.
        /// </summary>
        /// <param name="iter"></param>
        public IterIterable(ecs_iter_t iter)
        {
            _iter = iter;
        }

        /// <summary>
        ///     Set value for iterator variable.
        /// </summary>
        /// <param name="varId">The variable id.</param>
        /// <param name="value">The entity variable value.</param>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetVar(int varId, ulong value)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
                ecs_iter_set_var(it, varId, value);
                return ref this;
            }
        }

        /// <summary>
        ///     Set value for iterator variable.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <param name="value">The entity variable value.</param>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetVar(string name, ulong value)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                using NativeString nativeName = (NativeString)name;

                ecs_query_iter_t* iter = &it->priv_.iter.query;
                int varId = ecs_query_find_var(iter->query, nativeName);

                Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
                ecs_iter_set_var(it, varId, value);

                return ref this;
            }
        }

        /// <summary>
        ///     Set value for iterator variable.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <param name="value">The table variable value.</param>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetVar(string name, ecs_table_t* value)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                using NativeString nativeName = (NativeString)name;

                ecs_query_iter_t* iter = &it->priv_.iter.query;
                int varId = ecs_query_find_var(iter->query, nativeName);

                Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
                ecs_iter_set_var_as_table(it, varId, value);

                return ref this;
            }
        }

        /// <summary>
        ///     Set value for iterator variable.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <param name="value">The table variable value.</param>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetVar(string name, ecs_table_range_t value)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                using NativeString nativeName = (NativeString)name;

                ecs_query_iter_t* iter = &it->priv_.iter.query;
                int varId = ecs_query_find_var(iter->query, nativeName);

                Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
                ecs_iter_set_var_as_range(it, varId, &value);

                return ref this;
            }
        }

        /// <summary>
        ///     Set value for iterator variable.
        /// </summary>
        /// <param name="name">The variable name.</param>
        /// <param name="value">The table variable value.</param>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetVar(string name, Table value)
        {
            ecs_table_range_t range;
            range.table = value.GetTable();
            range.offset = value.Offset;
            range.count = value.Count;
            return ref SetVar(name, range);
        }

        /// <summary>
        ///     Serialize iterator to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(ecs_iter_to_json_desc_t* desc)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                return NativeString.GetStringAndFree(ecs_iter_to_json(it, desc));
            }
        }

        /// <summary>
        ///     Serialize iterator to JSON.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return ToJson(null);
        }

        /// <summary>
        ///     Serialize iterator to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(ref IterToJsonDesc desc)
        {
            fixed (ecs_iter_to_json_desc_t* ptr = &desc.Desc)
            {
                return ToJson(ptr);
            }
        }

        /// <summary>
        ///     Serialize iterator to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(IterToJsonDesc desc)
        {
            return ToJson(ref desc);
        }

        /// <summary>
        ///     Returns total number of entities in result.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                int result = 0;
                while (Macros.Bool(ecs_query_next_instanced(it)))
                    result += _iter.count;
                return result;
            }
        }

        /// <summary>
        ///     Returns true if iterator yields at least once result.
        /// </summary>
        /// <returns></returns>
        public bool IsTrue()
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                bool result = Macros.Bool(ecs_query_next_instanced(it));
                if (result)
                    ecs_iter_fini(it);
                return result;
            }
        }

        /// <summary>
        ///     Return first matching entity.
        /// </summary>
        /// <returns></returns>
        public Entity First()
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                Entity result = default;
                if (Macros.Bool(ecs_query_next_instanced(it)) && it->count != 0)
                {
                    result = new Entity(it->world, it->entities[0]);
                    ecs_iter_fini(it);
                }

                return result;
            }
        }

        /// <summary>
        ///     Limit results to tables with specified group id (grouped queries only)
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetGroup(ulong groupId)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                ecs_iter_set_group(it, groupId);
                return ref this;
            }
        }

        /// <summary>
        ///     Limit results to tables with specified group id (grouped queries only)
        /// </summary>
        /// <typeparam name="T">The group type.</typeparam>
        /// <returns>Reference to self.</returns>
        public ref IterIterable SetGroup<T>()
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                ecs_iter_set_group(it, Type<T>.Id(it->real_world));
                return ref this;
            }
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Iter(Ecs.IterCallback callback)
        {
            Iter(_iter.world, callback);
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
            ecs_iter_t iter = _iter;
            iter.world = world;
            while (ecs_query_next(&iter) == 1)
                Invoker.Iter(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(Ecs.EachEntityCallback callback)
        {
            Each(_iter.world, callback);
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
            ecs_iter_t iter = _iter;
            iter.world = world;
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(Ecs.EachIndexCallback callback)
        {
            Each(_iter.world, callback);
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
            ecs_iter_t iter = _iter;
            iter.world = world;
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
            Iter(_iter.world, callback);
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
            ecs_iter_t iter = _iter;
            iter.world = world;
            while (ecs_query_next(&iter) == 1)
                Invoker.Iter(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(delegate*<Entity, void> callback)
        {
            Each(_iter.world, callback);
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
            ecs_iter_t iter = _iter;
            iter.world = world;
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }

        /// <summary>
        ///     Iterates the query using the provided callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Each(delegate*<Iter, int, void> callback)
        {
            Each(_iter.world, callback);
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
            ecs_iter_t iter = _iter;
            iter.world = world;
            while (ecs_query_next_instanced(&iter) == 1)
                Invoker.Each(&iter, callback);
        }
#endif

        /// <summary>
        ///     Checks if two <see cref="IterIterable"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IterIterable other)
        {
            return Equals(_iter, other._iter);
        }

        /// <summary>
        ///     Checks if two <see cref="IterIterable"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is IterIterable other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="IterIterable"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _iter.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="IterIterable"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(IterIterable left, IterIterable right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="IterIterable"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(IterIterable left, IterIterable right)
        {
            return !(left == right);
        }
    }
}
