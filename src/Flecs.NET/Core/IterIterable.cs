using System;
using Flecs.NET.Utilities;
#if !NET5_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     An iterator object that can be modified before iterating.
    /// </summary>
    public unsafe struct IterIterable
    {
        private ecs_iter_t _iter;
        private IntPtr _next;
        private IntPtr _nextEach;

        /// <summary>
        ///     Creates an iter iterable.
        /// </summary>
        /// <param name="iter"></param>
        /// <param name="next"></param>
        /// <param name="nextEach"></param>
        public IterIterable(ecs_iter_t iter, IntPtr next, IntPtr nextEach)
        {
            _iter = iter;
            _next = next;
            _nextEach = nextEach;
        }

        /// <summary>
        ///     Set var value.
        /// </summary>
        /// <param name="varId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
        ///     Set var value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ref IterIterable SetVar(string name, ulong value)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                using NativeString nativeName = (NativeString)name;

                ecs_rule_iter_t* rit = &it->priv.iter.rule;
                int varId = ecs_rule_find_var(rit->rule, nativeName);

                Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
                ecs_iter_set_var(it, varId, value);

                return ref this;
            }
        }

        /// <summary>
        ///     Serialize iterator to JSON.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string ToJson(ecs_iter_to_json_desc_t *desc = null) {
            fixed (ecs_iter_t* it = &_iter)
                return NativeString.GetStringAndFree(ecs_iter_to_json(it->real_world, it, desc));
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
                while (NextInstanced(it))
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
                bool result = NextInstanced(it);
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
                if (NextInstanced(it) && it->count != 0)
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
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ref IterIterable SetGroup(ulong groupId)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                ecs_query_set_group(it, groupId);
                return ref this;
            }
        }

        /// <summary>
        ///     Limit results to tables with specified group id (grouped queries only)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref IterIterable SetGroup<T>()
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                ecs_query_set_group(it, Type<T>.Id(it->real_world));
                return ref this;
            }
        }

        /// <summary>
        ///     Iterates using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Iter(Ecs.IterCallback func)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                while (Next(it))
                    Invoker.Iter(it, func);
            }
        }

        /// <summary>
        ///     Iterates using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachEntityCallback func)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                while (NextInstanced(it))
                    Invoker.Each(it, func);
            }
        }

        /// <summary>
        ///     Iterates using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachIndexCallback func)
        {
            fixed (ecs_iter_t* it = &_iter)
            {
                while (NextInstanced(it))
                    Invoker.Each(it, func);
            }
        }

        private bool Next(ecs_iter_t* it)
        {
#if NET5_0_OR_GREATER
            return ((delegate* managed<ecs_iter_t*, byte>)_next)(it) == 1;
#else
            return Marshal.GetDelegateForFunctionPointer<Ecs.IterNextAction>(_next)(it) == 1;
#endif
        }

        private bool NextInstanced(ecs_iter_t* it)
        {
#if NET5_0_OR_GREATER
            return ((delegate* managed<ecs_iter_t*, byte>)_nextEach)(it) == 1;
#else
            return Marshal.GetDelegateForFunctionPointer<Ecs.IterNextAction>(_nextEach)(it) == 1;
#endif
        }
    }
}
