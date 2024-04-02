using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Wrapper for building rules.
    /// </summary>
    public unsafe partial struct RuleBuilder : IDisposable, IEquatable<RuleBuilder>
    {
        private ecs_world_t* _world;

        internal FilterBuilder FilterBuilder;

        /// <summary>
        ///     Reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Creates a rule builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public RuleBuilder(ecs_world_t* world, string? name = null)
        {
            _world = world;
            FilterBuilder = new FilterBuilder(world);

            if (string.IsNullOrEmpty(name))
                return;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;
            FilterBuilder.Desc.entity = ecs_entity_init(world, &entityDesc);
        }

        /// <summary>
        ///     Disposes the rule builder.
        /// </summary>
        public void Dispose()
        {
            FilterBuilder.Dispose();
        }

        /// <summary>
        ///     Builds a new rule.
        /// </summary>
        /// <returns></returns>
        public Rule Build()
        {
            fixed (ecs_filter_desc_t* filterDesc = &FilterBuilder.Desc)
            {
                filterDesc->terms_buffer = FilterBuilder.Terms.Data;
                filterDesc->terms_buffer_count = FilterBuilder.Terms.Count;

                ecs_rule_t* handle = ecs_rule_init(World, filterDesc);

                FilterBuilder.Dispose();

                return new Rule(World, handle);
            }
        }

        /// <summary>
        ///     Checks if two <see cref="RuleBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RuleBuilder other)
        {
            return FilterBuilder == other.FilterBuilder;
        }

        /// <summary>
        ///     Checks if two <see cref="RuleBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is RuleBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="RuleBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return FilterBuilder.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="RuleBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(RuleBuilder left, RuleBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="RuleBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(RuleBuilder left, RuleBuilder right)
        {
            return !(left == right);
        }
    }

    // FilterBuilder Extensions
    public unsafe partial struct RuleBuilder
    {
        /// <inheritdoc cref="Core.FilterBuilder.Self"/>
        public ref RuleBuilder Self()
        {
            FilterBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up"/>
        public ref RuleBuilder Up(ulong traverse = 0)
        {
            FilterBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up{T}()"/>
        public ref RuleBuilder Up<T>()
        {
            FilterBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up{T}(T)"/>
        public ref RuleBuilder Up<T>(T value) where T : Enum
        {
            FilterBuilder.Up(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade"/>
        public ref RuleBuilder Cascade(ulong traverse = 0)
        {
            FilterBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade{T}()"/>
        public ref RuleBuilder Cascade<T>()
        {
            FilterBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade{T}(T)"/>
        public ref RuleBuilder Cascade<T>(T value) where T : Enum
        {
            FilterBuilder.Cascade(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Descend"/>
        public ref RuleBuilder Descend()
        {
            FilterBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Parent"/>
        public ref RuleBuilder Parent()
        {
            FilterBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Trav(ulong, uint)"/>
        public ref RuleBuilder Trav(ulong traverse, uint flags = 0)
        {
            FilterBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Trav{T}(uint)"/>
        public ref RuleBuilder Trav<T>(uint flags = 0)
        {
            FilterBuilder.Trav<T>(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Trav{T}(T, uint)"/>
        public ref RuleBuilder Trav<T>(T value, uint flags = 0) where T : Enum
        {
            FilterBuilder.Trav(value, flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Id"/>
        public ref RuleBuilder Id(ulong id)
        {
            FilterBuilder.Id(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Entity"/>
        public ref RuleBuilder Entity(ulong entity)
        {
            FilterBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Name"/>
        public ref RuleBuilder Name(string name)
        {
            FilterBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Var"/>
        public ref RuleBuilder Var(string name)
        {
            FilterBuilder.Var(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Flags"/>
        public ref RuleBuilder Flags(uint flags)
        {
            FilterBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src()"/>
        public ref RuleBuilder Src()
        {
            FilterBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First()"/>
        public ref RuleBuilder First()
        {
            FilterBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second()"/>
        public ref RuleBuilder Second()
        {
            FilterBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(ulong)"/>
        public ref RuleBuilder Src(ulong srcId)
        {
            FilterBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src{T}()"/>
        public ref RuleBuilder Src<T>()
        {
            FilterBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src{T}(T)"/>
        public ref RuleBuilder Src<T>(T value) where T : Enum
        {
            FilterBuilder.Src(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(string)"/>
        public ref RuleBuilder Src(string name)
        {
            FilterBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(ulong)"/>
        public ref RuleBuilder First(ulong firstId)
        {
            FilterBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First{T}()"/>
        public ref RuleBuilder First<T>()
        {
            FilterBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First{T}(T)"/>
        public ref RuleBuilder First<T>(T value) where T : Enum
        {
            FilterBuilder.First(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(string)"/>
        public ref RuleBuilder First(string name)
        {
            FilterBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(ulong)"/>
        public ref RuleBuilder Second(ulong secondId)
        {
            FilterBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second{T}()"/>
        public ref RuleBuilder Second<T>()
        {
            FilterBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second{T}(T)"/>
        public ref RuleBuilder Second<T>(T value) where T : Enum
        {
            FilterBuilder.Second(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(string)"/>
        public ref RuleBuilder Second(string secondName)
        {
            FilterBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Role"/>
        public ref RuleBuilder Role(ulong role)
        {
            FilterBuilder.Role(role);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut(Flecs.NET.Bindings.Native.ecs_inout_kind_t)"/>
        public ref RuleBuilder InOut(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutStage"/>
        public ref RuleBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write()"/>
        public ref RuleBuilder Write()
        {
            FilterBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read()"/>
        public ref RuleBuilder Read()
        {
            FilterBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadWrite"/>
        public ref RuleBuilder ReadWrite()
        {
            FilterBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.In"/>
        public ref RuleBuilder In()
        {
            FilterBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Out"/>
        public ref RuleBuilder Out()
        {
            FilterBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut()"/>
        public ref RuleBuilder InOut()
        {
            FilterBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutNone"/>
        public ref RuleBuilder InOutNone()
        {
            FilterBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Oper"/>
        public ref RuleBuilder Oper(ecs_oper_kind_t oper)
        {
            FilterBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.And"/>
        public ref RuleBuilder And()
        {
            FilterBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Or"/>
        public ref RuleBuilder Or()
        {
            FilterBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Not"/>
        public ref RuleBuilder Not()
        {
            FilterBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Optional"/>
        public ref RuleBuilder Optional()
        {
            FilterBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.AndFrom"/>
        public ref RuleBuilder AndFrom()
        {
            FilterBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.OrFrom"/>
        public ref RuleBuilder OrFrom()
        {
            FilterBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.NotFrom"/>
        public ref RuleBuilder NotFrom()
        {
            FilterBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Singleton"/>
        public ref RuleBuilder Singleton()
        {
            FilterBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Filter"/>
        public ref RuleBuilder Filter()
        {
            FilterBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Instanced"/>
        public ref RuleBuilder Instanced()
        {
            FilterBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.FilterFlags"/>
        public ref RuleBuilder FilterFlags(uint flags)
        {
            FilterBuilder.FilterFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Expr"/>
        public ref RuleBuilder Expr(string expr)
        {
            FilterBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong)"/>
        public ref RuleBuilder With(ulong id)
        {
            FilterBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, ulong)"/>
        public ref RuleBuilder With(ulong first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, string)"/>
        public ref RuleBuilder With(ulong first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, ulong)"/>
        public ref RuleBuilder With(string first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, string)"/>
        public ref RuleBuilder With(string first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}()"/>
        public ref RuleBuilder With<T>()
        {
            FilterBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(T)"/>
        public ref RuleBuilder With<T>(T value) where T : Enum
        {
            FilterBuilder.With(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(ulong)"/>
        public ref RuleBuilder With<TFirst>(ulong second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(string)"/>
        public ref RuleBuilder With<TFirst>(string second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}()"/>
        public ref RuleBuilder With<TFirst, TSecond>()
        {
            FilterBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}(T2)"/>
        public ref RuleBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            FilterBuilder.With<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}(T1)"/>
        public ref RuleBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            FilterBuilder.With<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1}(T1, string)"/>
        public ref RuleBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T2}(string, T2)"/>
        public ref RuleBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(ulong)"/>
        public ref RuleBuilder WithSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(string)"/>
        public ref RuleBuilder WithSecond<TSecond>(string first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong)"/>
        public ref RuleBuilder Without(ulong id)
        {
            FilterBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, ulong)"/>
        public ref RuleBuilder Without(ulong first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, string)"/>
        public ref RuleBuilder Without(ulong first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, ulong)"/>
        public ref RuleBuilder Without(string first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, string)"/>
        public ref RuleBuilder Without(string first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}()"/>
        public ref RuleBuilder Without<T>()
        {
            FilterBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(T)"/>
        public ref RuleBuilder Without<T>(T value) where T : Enum
        {
            FilterBuilder.Without(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(ulong)"/>
        public ref RuleBuilder Without<TFirst>(ulong second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(string)"/>
        public ref RuleBuilder Without<TFirst>(string second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}()"/>
        public ref RuleBuilder Without<TFirst, TSecond>()
        {
            FilterBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}(T2)"/>
        public ref RuleBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            FilterBuilder.Without<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}(T1)"/>
        public ref RuleBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            FilterBuilder.Without<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1}(T1, string)"/>
        public ref RuleBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T2}(string, T2)"/>
        public ref RuleBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(ulong)"/>
        public ref RuleBuilder WithoutSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(string)"/>
        public ref RuleBuilder WithoutSecond<TSecond>(string first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong)"/>
        public ref RuleBuilder Write(ulong id)
        {
            FilterBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, ulong)"/>
        public ref RuleBuilder Write(ulong first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, string)"/>
        public ref RuleBuilder Write(ulong first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, ulong)"/>
        public ref RuleBuilder Write(string first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, string)"/>
        public ref RuleBuilder Write(string first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}()"/>
        public ref RuleBuilder Write<T>()
        {
            FilterBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(T)"/>
        public ref RuleBuilder Write<T>(T value) where T : Enum
        {
            FilterBuilder.Write(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(ulong)"/>
        public ref RuleBuilder Write<TFirst>(ulong second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(string)"/>
        public ref RuleBuilder Write<TFirst>(string second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}()"/>
        public ref RuleBuilder Write<TFirst, TSecond>()
        {
            FilterBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}(T2)"/>
        public ref RuleBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            FilterBuilder.Write<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}(T1)"/>
        public ref RuleBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            FilterBuilder.Write<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1}(T1, string)"/>
        public ref RuleBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T2}(string, T2)"/>
        public ref RuleBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(ulong)"/>
        public ref RuleBuilder WriteSecond<TSecond>(ulong first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(string)"/>
        public ref RuleBuilder WriteSecond<TSecond>(string first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong)"/>
        public ref RuleBuilder Read(ulong id)
        {
            FilterBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, ulong)"/>
        public ref RuleBuilder Read(ulong first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, string)"/>
        public ref RuleBuilder Read(ulong first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, ulong)"/>
        public ref RuleBuilder Read(string first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, string)"/>
        public ref RuleBuilder Read(string first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}()"/>
        public ref RuleBuilder Read<T>()
        {
            FilterBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(T)"/>
        public ref RuleBuilder Read<T>(T value) where T : Enum
        {
            FilterBuilder.Read(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(ulong)"/>
        public ref RuleBuilder Read<TFirst>(ulong second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(string)"/>
        public ref RuleBuilder Read<TFirst>(string second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}()"/>
        public ref RuleBuilder Read<TFirst, TSecond>()
        {
            FilterBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}(T2)"/>
        public ref RuleBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            FilterBuilder.Read<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}(T1)"/>
        public ref RuleBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            FilterBuilder.Read<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1}(T1, string)"/>
        public ref RuleBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T2}(string, T2)"/>
        public ref RuleBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(ulong)"/>
        public ref RuleBuilder ReadSecond<TSecond>(ulong first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(string)"/>
        public ref RuleBuilder ReadSecond<TSecond>(string first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeOpen"/>
        public ref RuleBuilder ScopeOpen()
        {
            FilterBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeClose"/>
        public ref RuleBuilder ScopeClose()
        {
            FilterBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.IncrementTerm"/>
        public ref RuleBuilder IncrementTerm()
        {
            FilterBuilder.IncrementTerm();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermAt"/>
        public ref RuleBuilder TermAt(int termIndex)
        {
            FilterBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Arg"/>
        public ref RuleBuilder Arg(int termIndex)
        {
            FilterBuilder.Arg(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term()"/>
        public ref RuleBuilder Term()
        {
            FilterBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(Core.Term)"/>
        public ref RuleBuilder Term(Term term)
        {
            FilterBuilder.Term(term);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong)"/>
        public ref RuleBuilder Term(ulong id)
        {
            FilterBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string)"/>
        public ref RuleBuilder Term(string name)
        {
            FilterBuilder.Term(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, ulong)"/>
        public ref RuleBuilder Term(ulong first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, string)"/>
        public ref RuleBuilder Term(ulong first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, ulong)"/>
        public ref RuleBuilder Term(string first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, string)"/>
        public ref RuleBuilder Term(string first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}()"/>
        public ref RuleBuilder Term<T>()
        {
            FilterBuilder.Term<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(T)"/>
        public ref RuleBuilder Term<T>(T value) where T : Enum
        {
            FilterBuilder.Term(value);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(ulong)"/>
        public ref RuleBuilder Term<TFirst>(ulong second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(string)"/>
        public ref RuleBuilder Term<TFirst>(string second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}()"/>
        public ref RuleBuilder Term<TFirst, TSecond>()
        {
            FilterBuilder.Term<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}(T2)"/>
        public ref RuleBuilder Term<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            FilterBuilder.Term<TFirst, TSecond>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}(T1)"/>
        public ref RuleBuilder Term<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            FilterBuilder.Term<TFirst, TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1}(T1, string)"/>
        public ref RuleBuilder Term<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T2}(string, T2)"/>
        public ref RuleBuilder Term<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(ulong)"/>
        public ref RuleBuilder TermSecond<TSecond>(ulong first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(string)"/>
        public ref RuleBuilder TermSecond<TSecond>(string first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }
    }
}
