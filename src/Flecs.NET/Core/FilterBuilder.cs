using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A wrapper around ecs_filter_desc_t.
    /// </summary>
    public unsafe struct FilterBuilder : IDisposable, IEquatable<FilterBuilder>
    {
        private ecs_world_t* _world;
        private TermIdType _termIdType;
        private int _termIndex;
        private int _exprCount;
        private readonly ref ecs_term_t CurrentTerm => ref Terms[_termIndex];

        private readonly ref ecs_term_id_t CurrentTermId
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
                        throw new InvalidOperationException("Failed to get ref to CurrentTermId.");
                }
            }
        }

        internal ecs_filter_desc_t FilterDesc;
        internal NativeList<ecs_term_t> Terms;
        internal NativeList<NativeString> Strings;

        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     A reference to the filter description.
        /// </summary>
        public ref ecs_filter_desc_t Desc => ref FilterDesc;

        /// <summary>
        ///     Creates a filter builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        public FilterBuilder(ecs_world_t* world)
        {
            FilterDesc = default;
            Terms = default;
            Strings = default;

            _world = world;
            _termIdType = TermIdType.Src;
            _exprCount = default;
            _termIndex = default;
        }

        /// <summary>
        ///     Cleans up resources.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < Strings.Count; i++)
                Strings[i].Dispose();

            Terms.Dispose();
            Strings.Dispose();
        }

        [Conditional("DEBUG")]
        private readonly void AssertTermId()
        {
            Ecs.Assert(!Unsafe.IsNullRef(ref CurrentTermId), "No active term (call .Term() first)");
        }

        [Conditional("DEBUG")]
        private readonly void AssertTerm()
        {
            Ecs.Assert(!Unsafe.IsNullRef(ref CurrentTermId), "No active term (call .term() first)");
        }

        /// <summary>
        ///     The self flags indicates the term identifier itself is used.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Self()
        {
            AssertTermId();
            CurrentTermId.flags |= EcsSelf;
            return ref this;
        }

        /// <summary>
        ///     The up flag indicates that the term identifier may be substituted by
        ///     traversing a relationship upwards. For example: substitute the identifier
        ///     with its parent by traversing the ChildOf relationship.
        /// </summary>
        /// <param name="traverse"></param>
        /// <returns></returns>
        public ref FilterBuilder Up(ulong traverse = 0)
        {
            AssertTermId();
            CurrentTermId.flags |= EcsUp;

            if (traverse != 0)
                CurrentTermId.trav = traverse;

            return ref this;
        }

        /// <summary>
        ///     The up flag indicates that the term identifier may be substituted by
        ///     traversing a relationship upwards. For example: substitute the identifier
        ///     with its parent by traversing the ChildOf relationship.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Up<T>()
        {
            return ref Up(Type<T>.Id(World));
        }

        /// <summary>
        ///     The cascade flag is like up, but returns results in breadth-first order.
        ///     Only supported for Query.
        /// </summary>
        /// <param name="traverse"></param>
        /// <returns></returns>
        public ref FilterBuilder Cascade(ulong traverse = 0)
        {
            AssertTermId();
            CurrentTermId.flags |= EcsCascade;

            if (traverse != 0)
                CurrentTermId.trav = traverse;

            return ref this;
        }

        /// <summary>
        ///     The cascade flag is like up, but returns results in breadth-first order.
        ///     Only supported for Query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Cascade<T>()
        {
            return ref Cascade(Type<T>.Id(World));
        }

        /// <summary>
        ///     The parent flag is short for Up(EcsChildOf).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Parent()
        {
            AssertTermId();
            CurrentTermId.flags |= EcsParent;
            return ref this;
        }

        /// <summary>
        ///     Specify relationship to traverse, and flags to indicate direction.
        /// </summary>
        /// <param name="traverse"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ref FilterBuilder Trav(ulong traverse, uint flags = 0)
        {
            AssertTermId();
            CurrentTermId.trav = traverse;
            CurrentTermId.flags |= flags;
            return ref this;
        }

        /// <summary>
        ///     Specify value of identifier by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref FilterBuilder Id(ulong id)
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
        public ref FilterBuilder Entity(ulong entity)
        {
            AssertTermId();
            CurrentTermId.flags = EcsIsEntity;
            CurrentTermId.id = entity;
            return ref this;
        }

        /// <summary>
        ///     Specify value of identifier by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref FilterBuilder Name(string name)
        {
            AssertTermId();

            NativeString nativeName = (NativeString)name;
            Strings.Add(nativeName);

            CurrentTermId.name = nativeName;
            CurrentTermId.flags |= EcsIsEntity;

            return ref this;
        }

        /// <summary>
        ///     Specify identifier is a variable (resolved at query evaluation time).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref FilterBuilder Var(string name)
        {
            AssertTermId();

            NativeString nativeName = (NativeString)name;
            Strings.Add(nativeName);

            CurrentTermId.name = nativeName;
            CurrentTermId.flags |= EcsIsVariable;
            return ref this;
        }

        /// <summary>
        ///     Override term id flags.
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ref FilterBuilder Flags(uint flags)
        {
            AssertTermId();
            CurrentTermId.flags = flags;
            return ref this;
        }

        /// <summary>
        ///     Call prior to setting values for src identifier.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Src()
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
        public ref FilterBuilder First()
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
        public ref FilterBuilder Second()
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
        public ref FilterBuilder Src(ulong srcId)
        {
            return ref Src().Id(srcId);
        }

        /// <summary>
        ///     Select src identifier, initialize it with id associated with type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Src<T>()
        {
            return ref Src(Type<T>.Id(World));
        }

        /// <summary>
        ///     Select src identifier, initialize it with name. If name starts with a $
        ///     the name is interpreted as a variable.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref FilterBuilder Src(string name)
        {
            Src();
            return ref name[0] == '$' ? ref Var(name[1..]) : ref Name(name);
        }

        /// <summary>
        ///     Select first identifier, initialize it with entity id.
        /// </summary>
        /// <param name="firstId"></param>
        /// <returns></returns>
        public ref FilterBuilder First(ulong firstId)
        {
            return ref First().Id(firstId);
        }

        /// <summary>
        ///     Select first identifier, initialize it with id associated with type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder First<T>()
        {
            return ref First(Type<T>.Id(World));
        }

        /// <summary>
        ///     Select first identifier, initialize it with name. If name starts with a $
        ///     the name is interpreted as a variable.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref FilterBuilder First(string name)
        {
            First();
            return ref name[0] == '$' ? ref Var(name[1..]) : ref Name(name);
        }

        /// <summary>
        ///     Select second identifier, initialize it with entity id.
        /// </summary>
        /// <param name="secondId"></param>
        /// <returns></returns>
        public ref FilterBuilder Second(ulong secondId)
        {
            return ref Second().Id(secondId);
        }

        /// <summary>
        ///     Select second identifier, initialize it with id associated with type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Second<T>()
        {
            return ref Second(Type<T>.Id(World));
        }

        /// <summary>
        ///     Select second identifier, initialize it with name. If name starts with a $
        ///     the name is interpreted as a variable.
        /// </summary>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public ref FilterBuilder Second(string secondName)
        {
            Second();
            return ref secondName[0] == '$' ? ref Var(secondName[1..]) : ref Name(secondName);
        }

        /// <summary>
        ///     Set role of term.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ref FilterBuilder Role(ulong role)
        {
            AssertTerm();
            CurrentTerm.id_flags = role;
            return ref this;
        }

        /// <summary>
        ///     Set read/write access of term.
        /// </summary>
        /// <param name="inOut"></param>
        /// <returns></returns>
        public ref FilterBuilder InOut(ecs_inout_kind_t inOut)
        {
            AssertTerm();
            CurrentTerm.inout = inOut;
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
        public ref FilterBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            AssertTerm();
            CurrentTerm.inout = inOut;
            if (CurrentTerm.oper != EcsNot)
                Src().Entity(0);

            return ref this;
        }

        /// <summary>
        ///     Short for InOutStage(EcsOut).
        ///     Use when system uses add, remove or set.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Write()
        {
            return ref InOutStage(EcsOut);
        }

        /// <summary>
        ///     Short for InOutStage(EcsIn).
        ///     Use when system uses get.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Read()
        {
            return ref InOutStage(EcsIn);
        }

        /// <summary>
        ///     Short for InOutStage(EcsInOut).
        ///     Use when system uses get_mut.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder ReadWrite()
        {
            return ref InOutStage(EcsInOut);
        }

        /// <summary>
        ///     Short for InOut(EcsIn)
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder In()
        {
            return ref InOut(EcsIn);
        }

        /// <summary>
        ///     Short for InOut(EcsOut)
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Out()
        {
            return ref InOut(EcsOut);
        }

        /// <summary>
        ///     Short for InOut(EcsInOut)
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder InOut()
        {
            return ref InOut(EcsInOut);
        }

        /// <summary>
        ///     Short for InOut(EcsInOutNone)
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder InOutNone()
        {
            return ref InOut(EcsInOutNone);
        }

        /// <summary>
        ///     Set operator of term.
        /// </summary>
        /// <param name="oper"></param>
        /// <returns></returns>
        public ref FilterBuilder Oper(ecs_oper_kind_t oper)
        {
            AssertTerm();
            CurrentTerm.oper = oper;
            return ref this;
        }

        /// <summary>
        ///     Short for Oper(EcsAnd).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder And()
        {
            return ref Oper(EcsAnd);
        }

        /// <summary>
        ///     Short for Oper(EcsOr).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Or()
        {
            return ref Oper(EcsOr);
        }

        /// <summary>
        ///     Short for Oper(EcsNot).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Not()
        {
            return ref Oper(EcsNot);
        }

        /// <summary>
        ///     Short for Oper(EcsOptional).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Optional()
        {
            return ref Oper(EcsOptional);
        }


        /// <summary>
        ///     Short for Oper(EcsAndFrom).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder AndFrom()
        {
            return ref Oper(EcsAndFrom);
        }

        /// <summary>
        ///     Short for Oper(EcsOFrom).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder OrFrom()
        {
            return ref Oper(EcsOrFrom);
        }

        /// <summary>
        ///     Short for Oper(EcsNotFrom).
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder NotFrom()
        {
            return ref Oper(EcsNotFrom);
        }

        /// <summary>
        ///     Match singleton.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Singleton()
        {
            AssertTerm();
            Ecs.Assert(CurrentTerm.id != 0 || CurrentTerm.first.id != 0, "no component specified for singleton");

            ulong singletonId = CurrentTerm.id;

            if (singletonId == 0)
                singletonId = CurrentTerm.first.id;

            Ecs.Assert(singletonId != 0, nameof(ECS_INVALID_PARAMETER));
            CurrentTerm.src.id = !Macros.IsPair(singletonId) ? singletonId : Macros.PairFirst(World, singletonId);

            return ref this;
        }

        /// <summary>
        ///     Filter terms are not triggered on by observers.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder Filter()
        {
            CurrentTerm.src.flags |= EcsFilter;
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
        public ref FilterBuilder Instanced()
        {
            Desc.instanced = Macros.True;
            return ref this;
        }

        /// <summary>
        ///     Set flags for advanced usage
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public ref FilterBuilder FilterFlags(uint flags)
        {
            Desc.flags |= flags;
            return ref this;
        }

        /// <summary>
        ///     Filter expression. Should not be set at the same time as terms array.
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public ref FilterBuilder Expr(string expr)
        {
            Ecs.Assert(_exprCount == 0, "FilterBuilder.Expr() called more than once");

            NativeString nativeExpr = (NativeString)expr;
            Strings.Add(nativeExpr);

            Desc.expr = nativeExpr;
            _exprCount++;

            return ref this;
        }

        // TODO: Evaluate whether this commit should be added? https://github.com/SanderMertens/flecs/commit/bea64d33a5941b7156a379fa3434bfa28ee82c16

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref FilterBuilder With(ulong id)
        {
            return ref Term(id);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder With(ulong first, ulong second)
        {
            return ref Term(first, second);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder With(ulong first, string second)
        {
            return ref Term(first, second);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder With(string first, ulong second)
        {
            return ref Term(first, second);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder With(string first, string second)
        {
            return ref Term(first, second);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder With<T>()
        {
            return ref Term<T>();
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder With<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder With<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder With<TFirst>(string second)
        {
            return ref Term<TFirst>(second);
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder With<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>();
        }

        /// <summary>
        ///     Alternative form of Term().
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder With<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum);
        }

        /// <summary>
        ///     Alternative form of TermSecond().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder WithSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first);
        }

        /// <summary>
        ///     Alternative form of TermSecond().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder WithSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first);
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref FilterBuilder Without(ulong id)
        {
            return ref Term(id).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Without(ulong first, ulong second)
        {
            return ref Term(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Without(ulong first, string second)
        {
            return ref Term(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Without(string first, ulong second)
        {
            return ref Term(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Without(string first, string second)
        {
            return ref Term(first, second).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Without<T>()
        {
            return ref Term<T>().Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Without<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Without<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Without<TFirst>(string second)
        {
            return ref Term<TFirst>(second).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Without<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>().Not();
        }

        /// <summary>
        ///     Alternative form of Term().Not().
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Without<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum).Not();
        }

        /// <summary>
        ///     Alternative form of TermSecond().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder WithoutSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first).Not();
        }

        /// <summary>
        ///     Alternative form of TermSecond().Not().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder WithoutSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first).Not();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref FilterBuilder Write(ulong id)
        {
            return ref Term(id).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Write(ulong first, ulong second)
        {
            return ref Term(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Write(ulong first, string second)
        {
            return ref Term(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Write(string first, ulong second)
        {
            return ref Term(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Write(string first, string second)
        {
            return ref Term(first, second).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Write<T>()
        {
            return ref Term<T>().Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Write<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Write<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Write<TFirst>(string second)
        {
            return ref Term<TFirst>(second).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Write<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>().Write();
        }

        /// <summary>
        ///     Alternative form of Term().Write().
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Write<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum).Write();
        }

        /// <summary>
        ///     Alternative form of TermSecond().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder WriteSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first).Write();
        }

        /// <summary>
        ///     Alternative form of TermSecond().Write().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder WriteSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first).Write();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref FilterBuilder Read(ulong id)
        {
            return ref Term(id).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Read(ulong first, ulong second)
        {
            return ref Term(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Read(ulong first, string second)
        {
            return ref Term(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Read(string first, ulong second)
        {
            return ref Term(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Read(string first, string second)
        {
            return ref Term(first, second).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Read<T>()
        {
            return ref Term<T>().Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Read<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Read<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Read<TFirst>(string second)
        {
            return ref Term<TFirst>(second).Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Read<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>().Read();
        }

        /// <summary>
        ///     Alternative form of Term().Read().
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Read<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum).Read();
        }

        /// <summary>
        ///     Alternative form of TermSecond().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder ReadSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first).Read();
        }

        /// <summary>
        ///     Alternative form of TermSecond().Read().
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder ReadSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first).Read();
        }

        /// <summary>
        ///     Alternative form of With(EcsScopeOpen).Entity(0)
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder ScopeOpen()
        {
            return ref With(EcsScopeOpen).Entity(0);
        }

        /// <summary>
        ///     Alternative form of With(EcsScopeClose).Entity(0)
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder ScopeClose()
        {
            return ref With(EcsScopeClose).Entity(0);
        }

        /// <summary>
        ///     Increments to the next term.
        /// </summary>
        /// <returns></returns>
        public ref FilterBuilder IncrementTerm()
        {
            Terms.Add(default);
            _termIndex = Terms.Count - 1;
            _termIdType = TermIdType.Src;
            return ref this;
        }

        /// <summary>
        ///     Sets the current term to the one at the provided index.
        /// </summary>
        /// <param name="termIndex"></param>
        /// <returns></returns>
        public ref FilterBuilder TermAt(int termIndex)
        {
            Ecs.Assert(termIndex > 0 && termIndex <= Terms.Count, nameof(ECS_INVALID_PARAMETER));

            _termIndex = termIndex - 1;
            _termIdType = TermIdType.Src;

            Ecs.Assert(ecs_term_is_initialized((ecs_term_t*)Unsafe.AsPointer(ref CurrentTerm)) == 1,
                nameof(ECS_INVALID_PARAMETER));

            return ref this;
        }

        /// <summary>
        ///     Alternative form for TermAt().
        /// </summary>
        /// <param name="termIndex"></param>
        /// <returns></returns>
        public ref FilterBuilder Arg(int termIndex)
        {
            return ref TermAt(termIndex);
        }

        /// <summary>
        ///     Increments to the next term with the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ref FilterBuilder Term(ulong id)
        {
            IncrementTerm();
            SetTermId(id);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ref FilterBuilder Term(string name)
        {
            IncrementTerm();
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
        public ref FilterBuilder Term(ulong first, ulong second)
        {
            IncrementTerm();
            SetTermId(first, second);
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public ref FilterBuilder Term(ulong first, string second)
        {
            IncrementTerm();
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
        public ref FilterBuilder Term(string first, ulong second)
        {
            IncrementTerm();
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
        public ref FilterBuilder Term(string first, string second)
        {
            IncrementTerm();
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
        public ref FilterBuilder Term<T>()
        {
            IncrementTerm();
            SetTermId(Type<T>.Id(World));
            CurrentTerm.inout = EcsInOutDefault;
            return ref this;
        }

        /// <summary>
        ///     Increments to the next term with the provided enum.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Term<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            ulong pair = Macros.Pair<TEnum>(enumId, World);
            return ref Term(pair);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Term<TFirst>(ulong second)
        {
            return ref Term(Type<TFirst>.Id(World), second);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Term<TFirst>(string second)
        {
            return ref Term(Type<TFirst>.Id(World)).Second(second);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Term<TFirst, TSecond>()
        {
            return ref Term<TFirst>(Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder Term<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, World);
            return ref Term<TFirst>(enumId);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder TermSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref Term(pair);
        }

        /// <summary>
        ///     Increments to the next term with the provided pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref FilterBuilder TermSecond<TSecond>(string first)
        {
            return ref Term(first, Type<TSecond>.Id(World));
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

            CurrentTerm = new ecs_term_t { first = new ecs_term_id_t { id = id } };
        }

        private void SetTermId(ulong first, ulong second)
        {
            CurrentTerm = new ecs_term_t { id = Macros.Pair(first, second) };
        }

        private enum TermIdType
        {
            Src,
            First,
            Second
        }

        /// <summary>
        ///     Checks if two <see cref="FilterBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(FilterBuilder other)
        {
            return Equals(Desc, other);
        }

        /// <summary>
        ///     Checks if two <see cref="FilterBuilder"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool Equals(object? obj)
        {
            return obj is EventBuilder other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code for the <see cref="EventBuilder"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetHashCode()
        {
            return Desc.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="FilterBuilder"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(FilterBuilder left, FilterBuilder right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="FilterBuilder"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(FilterBuilder left, FilterBuilder right)
        {
            return !(left == right);
        }
    }
}
