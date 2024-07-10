using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around <see cref="ecs_query_desc_t"/>.
    /// </summary>
    public unsafe struct QueryBuilder : IDisposable, IEquatable<QueryBuilder>
    {
        private ecs_world_t* _world;
        private ecs_query_desc_t _desc;
        private int _termIndex;
        private int _termCount;
        private int _exprCount;
        private TermIdType _termIdType;
        private ref ecs_term_t CurrentTerm => ref Desc.terms[_termIndex];
        private ref ecs_term_ref_t CurrentTermId
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                switch (_termIdType)
                {
                    case TermIdType.First:
                        return ref CurrentTerm.first;
                    case TermIdType.Second:
                        return ref CurrentTerm.second;
                    case TermIdType.Src:
                        return ref CurrentTerm.src;
                    default:
                        return ref Unsafe.NullRef<ecs_term_ref_t>();
                }
            }
        }

        internal BindingContext.QueryContext Context;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the query description.
        /// </summary>
        public ref ecs_query_desc_t Desc => ref _desc;

        /// <summary>
        ///     Creates a query builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        public QueryBuilder(ecs_world_t* world)
        {
            _world = world;
            _desc = default;
            _termIndex = default;
            _termCount = default;
            _exprCount = default;
            _termIdType = TermIdType.Src;
            Context = default;
        }

        /// <summary>
        ///     Creates a query builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="entity">The query entity.</param>
        public QueryBuilder(ecs_world_t* world, ulong entity) : this(world)
        {
            Desc.entity = entity;
        }

        /// <summary>
        ///     Creates a query builder for the provided world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="name">The query name.</param>
        public QueryBuilder(ecs_world_t* world, string name) : this(world)
        {
            if (string.IsNullOrEmpty(name))
                return;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t desc = default;
            desc.name = nativeName;
            desc.sep = BindingContext.DefaultSeparator;
            desc.root_sep = BindingContext.DefaultSeparator;
            Desc.entity = ecs_entity_init(World, &desc);
        }

        /// <summary>
        ///     Cleans up native resources. This should be called if the query builder
        ///     will be discarded and .Build() isn't called.
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        ///     Builds a new Query.
        /// </summary>
        /// <returns></returns>
        public Query Build()
        {
            fixed (ecs_query_desc_t* ptr = &Desc)
                return new Query(World, ecs_query_init(World, ptr));
        }

        /// <summary>
        ///     The self flags indicates the term identifier itself is used.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Self()
        {
            AssertTermId();
            CurrentTermId.id |= EcsSelf;
            return ref this;
        }

        /// <summary>
        ///     Specify value of identifier by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref QueryBuilder Id(ulong id)
        {
            AssertTermId();
            CurrentTermId.id = id;
            return ref this;
        }

        /// <summary>
        ///     Specify value of identifier by id. Almost the same as id(entity), but this
        ///     operation explicitly sets the EcsIsEntity flag. This forces the id to
        ///     be interpreted as entity, whereas not setting the flag would implicitly
        ///     convert ids for builtin variables such as EcsThis to a variable.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ref QueryBuilder Entity(ulong entity)
        {
            AssertTermId();
            CurrentTermId.id = entity | EcsIsEntity;
            return ref this;
        }

        /// <summary>
        ///     Specify value of identifier by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder Name(string name)
        {
            AssertTermId();

            NativeString nativeName = (NativeString)name;
            Context.Strings.Add(nativeName);

            CurrentTermId.id |= EcsIsEntity;
            CurrentTermId.name = nativeName;

            return ref this;
        }

        /// <summary>
        ///     Specify identifier is a variable (resolved at query evaluation time).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder Var(string name)
        {
            AssertTermId();

            NativeString nativeName = (NativeString)name;
            Context.Strings.Add(nativeName);

            CurrentTermId.id |= EcsIsVariable;
            CurrentTermId.name = nativeName;
            return ref this;
        }

        /// <summary>
        ///     Sets term id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref QueryBuilder Term(ulong id)
        {
            return ref Id(id);
        }

        /// <summary>
        ///     Call prior to setting values for src identifier.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Src()
        {
            AssertTerm();
            _termIdType = TermIdType.Src;
            return ref this;
        }

        /// <summary>
        ///     Call prior to setting values for first identifier. This is either the
        ///     component identifier, or first element of a pair (in case second is
        ///     populated as well).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder First()
        {
            AssertTerm();
            _termIdType = TermIdType.First;
            return ref this;
        }

        /// <summary>
        ///     Call prior to setting values for second identifier. This is the second
        ///     element of a pair. Requires that First() is populated as well.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Second()
        {
            AssertTerm();
            _termIdType = TermIdType.Second;
            return ref this;
        }

        /// <summary>
        ///     Select src identifier, initialize it with entity id.
        /// </summary>
        /// <param name="srcId"></param>
        /// <returns></returns>
        public ref QueryBuilder Src(ulong srcId)
        {
            return ref Src().Id(srcId);
        }

        /// <summary>
        ///     Select src identifier, initialize it with id associated with type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Src<T>()
        {
            return ref Src(Type<T>.Id(World));
        }

        /// <summary>
        ///     Select src identifier, initialize it with id associated with enum member.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Src<T>(T value) where T : Enum
        {
            return ref Src(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Select src identifier, initialize it with name. If name starts with a $
        ///     the name is interpreted as a variable.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder Src(string name)
        {
            Src();
            return ref name[0] == '$' ? ref Var(name[1..]) : ref Name(name);
        }

        /// <summary>
        ///     Select first identifier, initialize it with entity id.
        /// </summary>
        /// <param name="firstId"></param>
        /// <returns></returns>
        public ref QueryBuilder First(ulong firstId)
        {
            return ref First().Id(firstId);
        }

        /// <summary>
        ///     Select first identifier, initialize it with id associated with type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder First<T>()
        {
            return ref First(Type<T>.Id(World));
        }

        /// <summary>
        ///     Select first identifier, initialize it with id associated with enum member.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder First<T>(T value) where T : Enum
        {
            return ref First(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Select first identifier, initialize it with name. If name starts with a $
        ///     the name is interpreted as a variable.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder First(string name)
        {
            First();
            return ref name[0] == '$' ? ref Var(name[1..]) : ref Name(name);
        }

        /// <summary>
        ///     Select second identifier, initialize it with entity id.
        /// </summary>
        /// <param name="secondId"></param>
        /// <returns></returns>
        public ref QueryBuilder Second(ulong secondId)
        {
            return ref Second().Id(secondId);
        }

        /// <summary>
        ///     Select second identifier, initialize it with id associated with type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Second<T>()
        {
            return ref Second(Type<T>.Id(World));
        }

        /// <summary>
        ///     Select second identifier, initialize it with id associated with enum member.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Second<T>(T value) where T : Enum
        {
            return ref Second(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Select second identifier, initialize it with name. If name starts with a $
        ///     the name is interpreted as a variable.
        /// </summary>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public ref QueryBuilder Second(string secondName)
        {
            Second();
            return ref secondName[0] == '$' ? ref Var(secondName[1..]) : ref Name(secondName);
        }

        /// <summary>
        ///     The up flag indicates that the term identifier may be substituted by
        ///     traversing a relationship upwards. For example: substitute the identifier
        ///     with its parent by traversing the ChildOf relationship.
        /// </summary>
        /// <param name="traverse"></param>
        /// <returns></returns>
        public ref QueryBuilder Up(ulong traverse = 0)
        {
            AssertTermId();
            Ecs.Assert(_termIdType == TermIdType.Src, "Up traversal can only be applied to term source.");

            CurrentTermId.id |= EcsUp;

            if (traverse != 0)
                CurrentTerm.trav = traverse;

            return ref this;
        }

        /// <summary>
        ///     The up flag indicates that the term identifier may be substituted by
        ///     traversing a relationship upwards. For example: substitute the identifier
        ///     with its parent by traversing the ChildOf relationship.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Up<T>()
        {
            return ref Up(Type<T>.Id(World));
        }

        /// <summary>
        ///     The up flag indicates that the term identifier may be substituted by
        ///     traversing a relationship upwards. For example: substitute the identifier
        ///     with its parent by traversing the ChildOf relationship.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Up<T>(T value) where T : Enum
        {
            return ref Up(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     The cascade flag is like up, but returns results in breadth-first order.
        ///     Only supported for Query.
        /// </summary>
        /// <param name="traverse"></param>
        /// <returns></returns>
        public ref QueryBuilder Cascade(ulong traverse = 0)
        {
            AssertTermId();

            Up();
            CurrentTermId.id |= EcsCascade;

            if (traverse != 0)
                CurrentTerm.trav = traverse;

            return ref this;
        }

        /// <summary>
        ///     The cascade flag is like up, but returns results in breadth-first order.
        ///     Only supported for Query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Cascade<T>()
        {
            return ref Cascade(Type<T>.Id(World));
        }

        /// <summary>
        ///     The cascade flag is like up, but returns results in breadth-first order.
        ///     Only supported for Query.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Cascade<T>(T value) where T : Enum
        {
            return ref Cascade(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Use with cascade to iterate results in descending (bottom -> top) order
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Descend()
        {
            AssertTermId();
            CurrentTermId.id |= EcsDesc;
            return ref this;
        }

        /// <summary>
        ///     Same as up(), exists for backwards compatibility.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Parent()
        {
            return ref Up();
        }

        /// <summary>
        ///     Specify relationship to traverse, and flags to indicate direction.
        /// </summary>
        /// <param name="traverse"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ref QueryBuilder Trav(ulong traverse, uint flags = 0)
        {
            AssertTermId();
            CurrentTerm.trav = traverse;
            CurrentTermId.id |= flags;
            return ref this;
        }

        /// <summary>
        ///     Specify relationship to traverse, and flags to indicate direction.
        /// </summary>
        /// <param name="flags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Trav<T>(uint flags = 0)
        {
            return ref Trav(Type<T>.Id(World), flags);
        }

        /// <summary>
        ///     Specify relationship to traverse, and flags to indicate direction.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Trav<T>(T value, uint flags = 0) where T : Enum
        {
            return ref Trav(Type<T>.Id(World, value), flags);
        }

        /// <summary>
        ///     Set id flags for term.
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ref QueryBuilder Flags(ulong flags)
        {
            AssertTerm();
            CurrentTerm.id |= flags;
            return ref this;
        }

        /// <summary>
        ///     Set read/write access of term.
        /// </summary>
        /// <param name="inOut"></param>
        /// <returns></returns>
        public ref QueryBuilder InOut(ecs_inout_kind_t inOut)
        {
            AssertTerm();
            CurrentTerm.inout = (short)inOut;
            return ref this;
        }

        /// <summary>
        ///     Set read/write access for stage. Use this when a system reads or writes
        ///     components other than the ones provided by the query. This information
        ///     can be used by schedulers to insert sync/merge points between systems
        ///     where deferred operations are flushed.
        ///     Setting this is optional. If not set, the value of the accessed component
        ///     may be out of sync for at most one frame.
        /// </summary>
        /// <param name="inOut"></param>
        /// <returns></returns>
        public ref QueryBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            AssertTerm();
            CurrentTerm.inout = (short)inOut;
            if (CurrentTerm.oper != (short)EcsNot)
                Src().Entity(0);

            return ref this;
        }

        /// <summary>
        ///     Short for InOutStage(EcsOut).
        ///     Use when system uses add, remove or set.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Write()
        {
            return ref InOutStage(EcsOut);
        }

        /// <summary>
        ///     Short for InOutStage(EcsIn).
        ///     Use when system uses get.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Read()
        {
            return ref InOutStage(EcsIn);
        }

        /// <summary>
        ///     Short for InOutStage(EcsInOut).
        ///     Use when system uses get_mut.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder ReadWrite()
        {
            return ref InOutStage(EcsInOut);
        }

        /// <summary>
        ///     Short for InOut(EcsIn)
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder In()
        {
            return ref InOut(EcsIn);
        }

        /// <summary>
        ///     Short for InOut(EcsOut)
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Out()
        {
            return ref InOut(EcsOut);
        }

        /// <summary>
        ///     Short for InOut(EcsInOut)
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder InOut()
        {
            return ref InOut(EcsInOut);
        }

        /// <summary>
        ///     Short for InOut(EcsInOutNone)
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder InOutNone()
        {
            return ref InOut(EcsInOutNone);
        }

        /// <summary>
        ///     Set operator of term.
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public ref QueryBuilder Oper(ecs_oper_kind_t oper)
        {
            AssertTerm();
            CurrentTerm.oper = (short)oper;
            return ref this;
        }

        /// <summary>
        ///     Short for Oper(EcsAnd).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder And()
        {
            return ref Oper(EcsAnd);
        }

        /// <summary>
        ///     Short for Oper(EcsOr).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Or()
        {
            return ref Oper(EcsOr);
        }

        /// <summary>
        ///     Short for Oper(EcsNot).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Not()
        {
            return ref Oper(EcsNot);
        }

        /// <summary>
        ///     Short for Oper(EcsOptional).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Optional()
        {
            return ref Oper(EcsOptional);
        }


        /// <summary>
        ///     Short for Oper(EcsAndFrom).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder AndFrom()
        {
            return ref Oper(EcsAndFrom);
        }

        /// <summary>
        ///     Short for Oper(EcsOFrom).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder OrFrom()
        {
            return ref Oper(EcsOrFrom);
        }

        /// <summary>
        ///     Short for Oper(EcsNotFrom).
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder NotFrom()
        {
            return ref Oper(EcsNotFrom);
        }

        /// <summary>
        ///     Match singleton.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Singleton()
        {
            AssertTerm();
            Ecs.Assert(CurrentTerm.id != 0 || CurrentTerm.first.id != 0, "no component specified for singleton");

            ulong singletonId = CurrentTerm.id;

            if (singletonId == 0)
                singletonId = CurrentTerm.first.id;

            Ecs.Assert(singletonId != 0, nameof(ECS_INVALID_PARAMETER));
            CurrentTerm.src.id = !Ecs.IsPair(singletonId) ? singletonId : Ecs.PairFirst(World, singletonId);

            return ref this;
        }

        /// <summary>
        ///     Filter terms are not triggered on by observers.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Filter()
        {
            CurrentTerm.inout = (short)EcsInOutFilter;
            return ref this;
        }

        /// <summary>
        ///     When true, terms returned by an iterator may either contain 1 or N
        ///     elements, where terms with N elements are owned, and terms with 1 element
        ///     are shared, for example from a parent or base entity. When false, the
        ///     iterator will at most return 1 element when the result contains both
        ///     owned and shared terms.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Instanced()
        {
            Desc.flags |= EcsQueryIsInstanced;
            return ref this;
        }

        /// <summary>
        ///     Set query flags for advanced usage
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ref QueryBuilder QueryFlags(uint flags)
        {
            Desc.flags |= flags;
            return ref this;
        }

        /// <summary>
        ///     Sets the cache kind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public ref QueryBuilder CacheKind(ecs_query_cache_kind_t kind)
        {
            Desc.cache_kind = kind;
            return ref this;
        }

        /// <summary>
        ///     Cache query terms that are cacheable.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Cached()
        {
            return ref CacheKind(EcsQueryCacheAuto);
        }

        /// <summary>
        ///     Query expression. Should not be set at the same time as terms array.
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public ref QueryBuilder Expr(string expr)
        {
            Ecs.Assert(_exprCount == 0, "QueryBuilder.Expr() cannot be called more than once");

            NativeString nativeExpr = (NativeString)expr;
            Context.Strings.Add(nativeExpr);

            Desc.expr = nativeExpr;
            _exprCount++;

            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided value.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ref QueryBuilder With(Term term)
        {
            Term();
            CurrentTerm = term.Value;
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref QueryBuilder With(ulong id)
        {
            Term();
            SetTermId(id);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder With(string name)
        {
            Term();
            SetTermId();
            First(name);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder With(ulong first, ulong second)
        {
            Term();
            SetTermId(first, second);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder With(ulong first, string second)
        {
            Term();
            SetTermId(first);
            Second(second);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder With(string first, ulong second)
        {
            Term();
            SetTermId();
            First(first);
            Second(second);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder With(string first, string second)
        {
            Term();
            SetTermId();
            First(first);
            Second(second);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<T>()
        {
            Term();
            SetTermId(Type<T>.Id(World));
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided enum.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<T>(T value) where T : Enum
        {
            return ref With<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TFirst>(ulong second)
        {
            return ref With(Type<TFirst>.Id(World), second);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TFirst>(string second)
        {
            return ref With(Type<TFirst>.Id(World)).Second(second);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TFirst, TSecond>()
        {
            return ref With<TFirst>(Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref With<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref WithSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            return ref With(Type<TFirst>.Id(World, first), second);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder With<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            return ref With(first, Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder WithSecond<TSecond>(ulong first)
        {
            ulong pair = Ecs.PairSecond<TSecond>(first, World);
            return ref With(pair);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder WithSecond<TSecond>(string first)
        {
            return ref With(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(Term term)
        {
            return ref With(term).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(ulong id)
        {
            return ref With(id).Not();
        }

        /// <summary>
        ///    Alternative form of With().Not().
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(string name)
        {
            return ref With(name).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(ulong first, ulong second)
        {
            return ref With(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(ulong first, string second)
        {
            return ref With(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(string first, ulong second)
        {
            return ref With(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Without(string first, string second)
        {
            return ref With(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<T>()
        {
            return ref With<T>().Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<T>(T value) where T : Enum
        {
            return ref With(value).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TFirst>(ulong second)
        {
            return ref With<TFirst>(second).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TFirst>(string second)
        {
            return ref With<TFirst>(second).Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TFirst, TSecond>()
        {
            return ref With<TFirst, TSecond>().Not();
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Without<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref WithoutSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            return ref Without(Type<TFirst>.Id(World, first), second);
        }

        /// <summary>
        ///     Alternative form of With().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Without<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            return ref Without(first, Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Alternative form of WithSecond().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder WithoutSecond<TSecond>(ulong first)
        {
            return ref WithSecond<TSecond>(first).Not();
        }

        /// <summary>
        ///     Alternative form of WithSecond().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder WithoutSecond<TSecond>(string first)
        {
            return ref WithSecond<TSecond>(first).Not();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(Term term)
        {
            return ref With(term).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(ulong id)
        {
            return ref With(id).Write();
        }

        /// <summary>
        ///    Alternative form of With().Write().
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(string name)
        {
            return ref With(name).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(ulong first, ulong second)
        {
            return ref With(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(ulong first, string second)
        {
            return ref With(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(string first, ulong second)
        {
            return ref With(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Write(string first, string second)
        {
            return ref With(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<T>()
        {
            return ref With<T>().Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<T>(T value) where T : Enum
        {
            return ref With(value).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TFirst>(ulong second)
        {
            return ref With<TFirst>(second).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TFirst>(string second)
        {
            return ref With<TFirst>(second).Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TFirst, TSecond>()
        {
            return ref With<TFirst, TSecond>().Write();
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Write<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref WriteSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            return ref Write(Type<TFirst>.Id(World, first), second);
        }

        /// <summary>
        ///     Alternative form of With().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Write<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            return ref Write(first, Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Alternative form of WithSecond().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder WriteSecond<TSecond>(ulong first)
        {
            return ref WithSecond<TSecond>(first).Write();
        }

        /// <summary>
        ///     Alternative form of WithSecond().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder WriteSecond<TSecond>(string first)
        {
            return ref WithSecond<TSecond>(first).Write();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(Term term)
        {
            return ref With(term).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(ulong id)
        {
            return ref With(id).Read();
        }

        /// <summary>
        ///    Alternative form of With().Read().
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(string name)
        {
            return ref With(name).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(ulong first, ulong second)
        {
            return ref With(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(ulong first, string second)
        {
            return ref With(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(string first, ulong second)
        {
            return ref With(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref QueryBuilder Read(string first, string second)
        {
            return ref With(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<T>()
        {
            return ref With<T>().Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<T>(T value) where T : Enum
        {
            return ref With(value).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TFirst>(ulong second)
        {
            return ref With<TFirst>(second).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TFirst>(string second)
        {
            return ref With<TFirst>(second).Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TFirst, TSecond>()
        {
            return ref With<TFirst, TSecond>().Read();
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ref Read<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ref ReadSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TFirst>(TFirst first, string second) where TFirst : Enum
        {
            return ref Read(Type<TFirst>.Id(World, first), second);
        }

        /// <summary>
        ///     Alternative form of With().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder Read<TSecond>(string first, TSecond second) where TSecond : Enum
        {
            return ref Read(first, Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Alternative form of WithSecond().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder ReadSecond<TSecond>(ulong first)
        {
            return ref WithSecond<TSecond>(first).Read();
        }

        /// <summary>
        ///     Alternative form of WithSecond().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder ReadSecond<TSecond>(string first)
        {
            return ref WithSecond<TSecond>(first).Read();
        }

        /// <summary>
        ///     Alternative form of With(EcsScopeOpen).Entity(0)
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder ScopeOpen()
        {
            return ref With(EcsScopeOpen).Entity(0);
        }

        /// <summary>
        ///     Alternative form of With(EcsScopeClose).Entity(0)
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder ScopeClose()
        {
            return ref With(EcsScopeClose).Entity(0);
        }

        /// <summary>
        ///     Term notation for more complex query features.
        /// </summary>
        /// <returns></returns>
        public ref QueryBuilder Term()
        {
            Ecs.Assert(_termIndex < FLECS_TERM_COUNT_MAX, "Cannot have more than 32 terms.");
            _termIndex = _termCount++;
            return ref this;
        }

        /// <summary>
        ///     Sets the current term to the one at the provided index.
        /// </summary>
        /// <param name="termIndex"></param>
        /// <returns></returns>
        public ref QueryBuilder TermAt(int termIndex)
        {
            Ecs.Assert(termIndex >= 0 && termIndex < FLECS_TERM_COUNT_MAX, "TermIndex argument must be between 0-31.");

            _termIndex = termIndex;
            _termIdType = TermIdType.Src;

            fixed (ecs_term_t* ptr = &CurrentTerm)
                Ecs.Assert(ecs_term_is_initialized(ptr) == Utils.True, "Term is not initialized.");

            return ref this;
        }

        /// <summary>
        ///     Sort the output of a query.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public ref QueryBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            BindingContext.SetCallback(ref Context.OrderByAction, compare);
            Desc.order_by_callback = Context.OrderByAction.Pointer;
            Desc.order_by = component;
            return ref this;
        }

        /// <summary>
        ///     Sort the output of a query.
        /// </summary>
        /// <param name="compare"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder OrderBy<T>(Ecs.OrderByAction compare)
        {
            return ref OrderBy(Type<T>.Id(World), compare);
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component)
        {
            Desc.group_by_callback = IntPtr.Zero;
            Desc.group_by = component;
            return ref this;
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder GroupBy<T>()
        {
            return ref GroupBy(Type<T>.Id(World));
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component, Ecs.GroupByAction callback)
        {
            BindingContext.SetCallback(ref Context.GroupByAction, callback);
            Desc.group_by_callback = Context.GroupByAction.Pointer;
            Desc.group_by = component;
            return ref this;
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder GroupBy<T>(Ecs.GroupByAction callback)
        {
            return ref GroupBy(Type<T>.Id(World), callback);
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component, Ecs.GroupByCallback callback)
        {
            Ecs.Assert(Desc.group_by_ctx == null,
                "Cannot set .GroupBy callback if group_by_ctx is already occupied.");

            Context.GroupByAction.Dispose();
            Context.ContextFree.Dispose();

            BindingContext.GroupByContext* context = Memory.AllocZeroed<BindingContext.GroupByContext>(1);
            BindingContext.SetCallback(ref context->GroupBy, callback);
            Desc.group_by_callback = BindingContext.GroupByCallbackPointer;
            Desc.group_by_ctx_free = BindingContext.GroupByContextFreePointer;
            Desc.group_by_ctx = context;
            Desc.group_by = component;

            return ref this;
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder GroupBy<T>(Ecs.GroupByCallback callback)
        {
            return ref GroupBy(Type<T>.Id(World), callback);
        }

        /// <summary>
        ///     Specify context to be passed to group_by function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="contextFree"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            BindingContext.SetCallback(ref Context.ContextFree, contextFree);
            Desc.group_by_ctx_free = Context.ContextFree.Pointer;
            Desc.group_by_ctx = ctx;
            return ref this;
        }

        /// <summary>
        ///     Specify context to be passed to group_by function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupByCtx(void* ctx)
        {
            Desc.group_by_ctx = ctx;
            Desc.group_by_ctx_free = IntPtr.Zero;
            return ref this;
        }

        /// <summary>
        ///     Specify on_group_create action.
        /// </summary>
        /// <param name="onGroupCreate"></param>
        /// <returns></returns>
        public ref QueryBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            BindingContext.SetCallback(ref Context.GroupCreateAction, onGroupCreate);
            Desc.on_group_create = Context.GroupCreateAction.Pointer;
            return ref this;
        }

        /// <summary>
        ///     Specify on_group_delete action.
        /// </summary>
        /// <param name="onGroupDelete"></param>
        /// <returns></returns>
        public ref QueryBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            BindingContext.SetCallback(ref Context.GroupDeleteAction, onGroupDelete);
            Desc.on_group_delete = Context.GroupDeleteAction.Pointer;
            return ref this;
        }

        [Conditional("DEBUG")]
        private void AssertTermId()
        {
            Ecs.Assert(!Unsafe.IsNullRef(ref CurrentTermId), "No active term (call .Term() first)");
        }

        [Conditional("DEBUG")]
        private void AssertTerm()
        {
            Ecs.Assert(!Unsafe.IsNullRef(ref CurrentTermId), "No active term (call .term() first)");
        }

        private void SetTermId()
        {
            CurrentTerm = default;
        }

        private void SetTermId(ulong id)
        {
            if ((id & ECS_ID_FLAGS_MASK) != 0)
            {
                CurrentTerm = new ecs_term_t { id = id };
                return;
            }

            CurrentTerm = new ecs_term_t { first = new ecs_term_ref_t { id = id } };
        }

        private void SetTermId(ulong first, ulong second)
        {
            CurrentTerm = new ecs_term_t { id = Ecs.Pair(first, second) };
        }

        private enum TermIdType
        {
            Src,
            First,
            Second
        }

        /// <summary>
        ///     Checks if two <see cref="QueryBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(QueryBuilder other)
        {
            return Desc == other.Desc;
        }

        /// <summary>
        ///     Checks if two <see cref="QueryBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is QueryBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="EventBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="QueryBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(QueryBuilder left, QueryBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="QueryBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(QueryBuilder left, QueryBuilder right)
        {
            return !(left == right);
        }
    }
}
