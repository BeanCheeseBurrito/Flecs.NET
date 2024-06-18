using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

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
            Ecs.Assert(iter->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");

            iter->flags |= EcsIterCppEach;

            Macros.TableLock(iter->world, iter->table);

            for (int i = 0; i < iter->count; i++)
                callback(new Entity(iter->world, iter->entities[i]));

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes an each callback using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, Ecs.EachIterCallback callback)
        {
            iter->flags |= EcsIterCppEach;

            Macros.TableLock(iter->world, iter->table);

            int count = iter->count == 0 ? 1 : iter->count;

            for (int i = 0; i < count; i++)
                callback(new Iter(iter), i);

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes a run callback using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Run(ecs_iter_t* iter, Ecs.IterCallback callback)
        {
            iter->flags &= ~EcsIterIsValid;
            callback(new Iter(iter));
        }

        /// <summary>
        ///     Invokes an entity observer using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Observe(ecs_iter_t* iter, Ecs.EachEntityCallback callback)
        {
            callback(new Entity(iter->world, ecs_field_src(iter, 0)));
        }

        /// <summary>
        ///     Invokes an entity observer using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public static void Observe<T>(ecs_iter_t* iter, Ecs.EachRefCallback<T> callback)
        {
            Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
            callback(ref Managed.GetTypeRef<T>(iter->param));
        }

        /// <summary>
        ///     Invokes an entity observer using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        public static void Observe<T>(ecs_iter_t* iter, Ecs.EachEntityRefCallback<T> callback)
        {
            Ecs.Assert(iter->param != null, "Entity observer invoked without event payload");
            callback(new Entity(iter->world, ecs_field_src(iter, 0)), ref Managed.GetTypeRef<T>(iter->param));
        }

#if NET5_0_OR_GREATER
        /// <summary>
        ///     Invokes an iter callback using a managed function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Iter(ecs_iter_t* iter, delegate*<Iter, void> callback)
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
        public static void Each(ecs_iter_t* iter, delegate*<Entity, void> callback)
        {
            Ecs.Assert(iter->count > 0, "No entities returned, use Iter() or Each() without the entity argument instead.");

            iter->flags |= EcsIterCppEach;

            Macros.TableLock(iter->world, iter->table);

            for (int i = 0; i < iter->count; i++)
                callback(new Entity(iter->world, iter->entities[i]));

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///      Invokes an each callback using a managed function pointer.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Each(ecs_iter_t* iter, delegate*<Iter, int, void> callback)
        {
            iter->flags |= EcsIterCppEach;

            Macros.TableLock(iter->world, iter->table);

            int count = iter->count == 0 ? 1 : iter->count;

            for (int i = 0; i < count; i++)
                callback(new Iter(iter), i);

            Macros.TableUnlock(iter->world, iter->table);
        }

        /// <summary>
        ///     Invokes a run callback using a delegate.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="callback"></param>
        public static void Run(ecs_iter_t* iter, delegate*<Iter, void> callback)
        {
            iter->flags &= ~EcsIterIsValid;
            callback(new Iter(iter));
        }
#endif
    }
}
