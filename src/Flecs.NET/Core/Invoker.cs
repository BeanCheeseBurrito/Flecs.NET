using System;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A static class for holding callback invokers.
    /// </summary>
    public static unsafe partial class Invoker
    {
        /// <summary>
        ///     Invokes an iter callback using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Iter(ecs_iter_t* iter, Ecs.IterCallback callback)
        {
            Macros.TableLock(iter->world, iter->table);
            callback(new Iter(iter));
            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes an each callback using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, Ecs.EachEntityCallback callback)
        {
            ecs_world_t* world = iter->world;
            int count = iter->count;

            Ecs.Assert(count > 0, "No entities returned, use Iter() instead.");

            Macros.TableLock(iter->world, iter->table);

            for (int i = 0; i < count; i++)
                callback(new Entity(world, iter->entities[i]));

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes an each callback using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, Ecs.EachIndexCallback callback)
        {
            int count = iter->count == 0 ? 1 : iter->count;

            Iter it = new Iter(iter);

            Macros.TableLock(iter->world, iter->table);

            for (int i = 0; i < count; i++)
                callback(it, i);

            Macros.TableUnlock(iter->world, iter->table);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        ///     Invokes an iter callback using a managed function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Iter(ecs_iter_t* iter, delegate* managed<Iter, void> callback)
        {
            Macros.TableLock(iter->world, iter->table);
            callback(new Iter(iter));
            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes an iter callback using an unmanaged function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Iter(ecs_iter_t* iter, delegate* unmanaged<Iter, void> callback)
        {
            Macros.TableLock(iter->world, iter->table);
            callback(new Iter(iter));
            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes an each callback using a managed function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, delegate* managed<Entity, void> callback)
        {
            Macros.TableLock(iter->world, iter->table);

            ecs_world_t* world = iter->world;
            int count = iter->count;

            Ecs.Assert(count > 0, "No entities returned, use Iter() instead.");

            for (int i = 0; i < count; i++)
                callback(new Entity(world, iter->entities[i]));

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes an each callback using an unmanaged function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, delegate* unmanaged<Entity, void> callback)
        {
            Macros.TableLock(iter->world, iter->table);

            ecs_world_t* world = iter->world;
            int count = iter->count;

            Ecs.Assert(count > 0, "No entities returned, use Iter() instead.");

            for (int i = 0; i < count; i++)
                callback(new Entity(world, iter->entities[i]));

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///      Invokes an each callback using a managed function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, delegate* managed<Iter, int, void> callback)
        {
            int count = iter->count;

            if (count == 0)
                count = 1;

            Iter it = new Iter(iter);

            Macros.TableLock(iter->world, iter->table);

            for (int i = 0; i < count; i++)
                callback(it, i);

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///      Invokes an each callback using an unmanaged function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, delegate* unmanaged<Iter, int, void> callback)
        {
            int count = iter->count;

            if (count == 0)
                count = 1;

            Iter it = new Iter(iter);

            Macros.TableLock(iter->world, iter->table);

            for (int i = 0; i < count; i++)
                callback(it, i);

            Macros.TableUnlock(iter->world, iter->table);
        }
#endif
    }
}
