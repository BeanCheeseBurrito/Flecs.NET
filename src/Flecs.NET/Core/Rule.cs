using System;
using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper for ecs_rule_t.
    /// </summary>
    public unsafe partial struct Rule : IEquatable<Rule>, IDisposable
    {
        private ecs_world_t* _world;
        private ecs_rule_t* _handle;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ref ecs_rule_t* Handle => ref _handle;

        /// <summary>
        ///     Creates a rule from the provided world and handle.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="rule"></param>
        public Rule(ecs_world_t* world, ecs_rule_t* rule)
        {
            _world = world;
            _handle = rule;
        }

        /// <summary>
        ///     Disposes the rule.
        /// </summary>
        public void Dispose()
        {
            Destruct();
        }

        /// <summary>
        ///     Destructs the rule.
        /// </summary>
        public void Destruct()
        {
            if (Handle == null)
                return;

            ecs_rule_fini(Handle);
            World = null;
            Handle = null;
        }

        /// <summary>
        ///     Tests if the rule is not null.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return Handle != null;
        }

        /// <summary>
        ///     Returns the entity associated with the rule.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(World, ecs_get_entity(Handle));
        }

        /// <summary>
        ///     Iterates terms with the provided callback.
        /// </summary>
        /// <param name="callback"></param>
        public void EachTerm(Ecs.TermCallback callback)
        {
            Filter().EachTerm(callback);
        }

        /// <summary>
        ///     Gets term at provided index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Term Term(int index)
        {
            return Filter().Term(index);
        }

        /// <summary>
        ///     Returns the filter for the rule.
        /// </summary>
        /// <returns></returns>
        public Filter Filter()
        {
            return new Filter(World, ecs_rule_get_filter(Handle));
        }

        /// <summary>
        ///     Converts rule to a string expression.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            ecs_filter_t* filter = ecs_rule_get_filter(Handle);
            return NativeString.GetStringAndFree(ecs_filter_str(World, filter));
        }

        /// <summary>
        ///     Converts rule to a string that can be used to aid in debugging.
        /// </summary>
        /// <returns></returns>
        public string RuleStr()
        {
            return NativeString.GetStringAndFree(ecs_rule_str(Handle));
        }

        /// <summary>
        ///     Searches for a variable by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindVar(string name)
        {
            using NativeString nativeName = (NativeString)name;
            return ecs_rule_find_var(Handle, nativeName);
        }

        /// <summary>
        ///     Create an iterator object that can be modified before iterating.
        /// </summary>
        /// <returns></returns>
        public IterIterable Iter()
        {
            return new IterIterable(ecs_rule_iter(World, Handle), _next, _nextInstanced);
        }

        /// <summary>
        ///     Iterates the rule using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Iter(Ecs.IterCallback func)
        {
            ecs_iter_t iter = ecs_rule_iter(World, Handle);
            while (ecs_rule_next(&iter) == 1)
                Invoker.Iter(&iter, func);
        }

        /// <summary>
        ///     Iterates the rule using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachEntityCallback func)
        {
            ecs_iter_t iter = ecs_rule_iter(World, Handle);
            while (ecs_rule_next_instanced(&iter) == 1)
                Invoker.Each(&iter, func);
        }

        /// <summary>
        ///     Iterates the rule using the provided callback.
        /// </summary>
        /// <param name="func"></param>
        public void Each(Ecs.EachIndexCallback func)
        {
            ecs_iter_t iter = ecs_rule_iter(World, Handle);
            while (ecs_rule_next_instanced(&iter) == 1)
                Invoker.Each(&iter, func);
        }

        /// <summary>
        ///     Checks if two <see cref="Rule"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Rule other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="Rule"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Rule other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Rule"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Rule"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Rule left, Rule right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="RoutineBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Rule left, Rule right)
        {
            return !(left == right);
        }
    }

#if NET5_0_OR_GREATER
    public unsafe partial struct Rule
    {
        private static IntPtr _next = (IntPtr)(delegate* <ecs_iter_t*, byte>)&ecs_rule_next;
        private static IntPtr _nextInstanced = (IntPtr)(delegate* <ecs_iter_t*, byte>)&ecs_rule_next_instanced;
    }
#else
    public unsafe partial struct Rule
    {
        private static readonly IntPtr _next;
        private static readonly IntPtr _nextInstanced;

        private static readonly Ecs.IterNextAction _nextReference = ecs_rule_next;
        private static readonly Ecs.IterNextAction _nextInstancedReference = ecs_rule_next_instanced;

        static Rule()
        {
            _next = Marshal.GetFunctionPointerForDelegate(_nextReference);
            _nextInstanced = Marshal.GetFunctionPointerForDelegate(_nextInstancedReference);
        }
    }
#endif
}
