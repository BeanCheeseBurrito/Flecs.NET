using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Flecs.NET.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct FilterBuilder : IDisposable
    {
        public ecs_world_t* World { get; }

        internal ecs_filter_desc_t FilterDesc;
        internal UnsafeList<ecs_term_t> Terms;
        internal UnsafeList<NativeString> Strings;

        private TermIdType _termIdType;
        private int _termIndex;
        private int _exprCount;

        private readonly ref ecs_term_t CurrentTerm => ref Terms[_termIndex - 1];

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

        public FilterBuilder(ecs_world_t* world)
        {
            World = world;
            Terms = default;
            Strings = default;

            _termIdType = TermIdType.Src;
            FilterDesc = default;
            _exprCount = default;
            _termIndex = default;
        }

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
            Assert.True(!Unsafe.IsNullRef(ref CurrentTermId), "No active term (call .Term() first)");
        }

        [Conditional("DEBUG")]
        private readonly void AssertTerm()
        {
            Assert.True(!Unsafe.IsNullRef(ref CurrentTermId), "No active term (call .term() first)");
        }

        public ref FilterBuilder Self()
        {
            AssertTermId();
            CurrentTermId.flags |= EcsSelf;
            return ref this;
        }

        public ref FilterBuilder Up(ulong traverse = 0)
        {
            AssertTermId();
            CurrentTermId.flags |= EcsUp;

            if (traverse != 0)
                CurrentTermId.trav = traverse;

            return ref this;
        }

        public ref FilterBuilder Up<T>()
        {
            return ref Up(Type<T>.Id(World));
        }

        public ref FilterBuilder Cascade(ulong traverse = 0)
        {
            AssertTermId();
            CurrentTermId.flags |= EcsCascade;

            if (traverse != 0)
                CurrentTermId.trav = traverse;

            return ref this;
        }

        public ref FilterBuilder Cascade<T>()
        {
            return ref Cascade(Type<T>.Id(World));
        }

        public ref FilterBuilder Parent()
        {
            AssertTermId();
            CurrentTermId.flags |= EcsParent;
            return ref this;
        }

        public ref FilterBuilder Trav(ulong traverse, uint flags = 0)
        {
            AssertTermId();
            CurrentTermId.trav = traverse;
            CurrentTermId.flags |= flags;
            return ref this;
        }

        public ref FilterBuilder Id(ulong id)
        {
            AssertTermId();
            CurrentTermId.id = id;
            return ref this;
        }

        public ref FilterBuilder Entity(ulong entity)
        {
            AssertTermId();
            CurrentTermId.flags = EcsIsEntity;
            CurrentTermId.id = entity;
            return ref this;
        }

        public ref FilterBuilder Name(string name)
        {
            AssertTermId();

            NativeString nativeName = (NativeString)name;
            Strings.Add(nativeName);

            CurrentTermId.name = nativeName;
            CurrentTermId.flags |= EcsIsEntity;

            return ref this;
        }

        public ref FilterBuilder Var(string name)
        {
            AssertTermId();

            NativeString nativeName = (NativeString)name;
            Strings.Add(nativeName);

            CurrentTermId.name = nativeName;
            CurrentTermId.flags |= EcsIsVariable;
            return ref this;
        }

        public ref FilterBuilder Flags(uint flags)
        {
            AssertTermId();
            CurrentTermId.flags = flags;
            return ref this;
        }

        public ref FilterBuilder Src()
        {
            AssertTerm();
            _termIdType = TermIdType.Src;
            return ref this;
        }

        public ref FilterBuilder First()
        {
            AssertTerm();
            _termIdType = TermIdType.First;
            return ref this;
        }

        public ref FilterBuilder Second()
        {
            AssertTerm();
            _termIdType = TermIdType.Second;
            return ref this;
        }

        public ref FilterBuilder Src(ulong srcId)
        {
            return ref Src().Id(srcId);
        }

        public ref FilterBuilder Src<T>()
        {
            return ref Src(Type<T>.Id(World));
        }

        public ref FilterBuilder Src(string name)
        {
            Src();
            return ref name[0] == '$' ? ref Var(name[1..]) : ref Name(name);
        }

        public ref FilterBuilder First(ulong firstId)
        {
            return ref First().Id(firstId);
        }

        public ref FilterBuilder First<T>()
        {
            return ref First(Type<T>.Id(World));
        }

        public ref FilterBuilder First(string name)
        {
            First();
            return ref (name[0] == '$' ? ref Var(name[1..]) : ref Name(name));
        }

        public ref FilterBuilder Second(ulong secondId)
        {
            return ref Second().Id(secondId);
        }

        public ref FilterBuilder Second<T>()
        {
            return ref Second(Type<T>.Id(World));
        }

        public ref FilterBuilder Second(string secondName)
        {
            Second();
            return ref (secondName[0] == '$' ? ref Var(secondName[1..]) : ref Name(secondName));
        }

        public ref FilterBuilder Role(ulong role)
        {
            AssertTerm();
            CurrentTerm.id_flags = role;
            return ref this;
        }

        public ref FilterBuilder InOut(ecs_inout_kind_t inOut)
        {
            AssertTerm();
            CurrentTerm.inout = inOut;
            return ref this;
        }

        public ref FilterBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            AssertTerm();
            CurrentTerm.inout = inOut;
            if (CurrentTerm.oper != EcsNot)
                Src().Entity(0);

            return ref this;
        }

        public ref FilterBuilder Write()
        {
            return ref InOutStage(EcsOut);
        }

        public ref FilterBuilder Read()
        {
            return ref InOutStage(EcsIn);
        }

        public ref FilterBuilder ReadWrite()
        {
            return ref InOutStage(EcsInOut);
        }

        public ref FilterBuilder In()
        {
            return ref InOut(EcsIn);
        }

        public ref FilterBuilder Out()
        {
            return ref InOut(EcsOut);
        }

        public ref FilterBuilder InOut()
        {
            return ref InOut(EcsInOut);
        }

        public ref FilterBuilder InOutNone()
        {
            return ref InOut(EcsInOutNone);
        }

        public ref FilterBuilder Oper(ecs_oper_kind_t oper)
        {
            AssertTerm();
            CurrentTerm.oper = oper;
            return ref this;
        }

        public ref FilterBuilder And()
        {
            return ref Oper(EcsAnd);
        }

        public ref FilterBuilder Or()
        {
            return ref Oper(EcsOr);
        }

        public ref FilterBuilder Not()
        {
            return ref Oper(EcsNot);
        }

        public ref FilterBuilder Optional()
        {
            return ref Oper(EcsOptional);
        }

        public ref FilterBuilder AndFrom()
        {
            return ref Oper(EcsAndFrom);
        }

        public ref FilterBuilder OrFrom()
        {
            return ref Oper(EcsOrFrom);
        }

        public ref FilterBuilder NotFrom()
        {
            return ref Oper(EcsNotFrom);
        }

        public ref FilterBuilder Singleton()
        {
            AssertTerm();
            Assert.True(CurrentTerm.id != 0 || CurrentTerm.first.id != 0, "no component specified for singleton");

            ulong singletonId = CurrentTerm.id;
            if (singletonId == 0) singletonId = CurrentTerm.first.id;

            Assert.True(singletonId != 0, nameof(ECS_INVALID_PARAMETER));
            CurrentTerm.src.id = singletonId;
            return ref this;
        }

        public ref FilterBuilder Filter()
        {
            CurrentTerm.src.flags |= EcsFilter;
            return ref this;
        }

        public ref FilterBuilder Instanced()
        {
            FilterDesc.instanced = Macros.True;
            return ref this;
        }

        public ref FilterBuilder FilterFlags(uint flags)
        {
            FilterDesc.flags |= flags;
            return ref this;
        }

        public ref FilterBuilder Expr(string expr)
        {
            Assert.True(_exprCount == 0, "FilterBuilder.Expr() called more than once");

            NativeString nativeExpr = (NativeString)expr;
            Strings.Add(nativeExpr);

            FilterDesc.expr = nativeExpr;
            _exprCount++;

            return ref this;
        }

        public ref FilterBuilder With(ulong id)
        {
            return ref Term(id);
        }

        public ref FilterBuilder With(ulong first, ulong second)
        {
            return ref Term(first, second);
        }

        public ref FilterBuilder With(ulong first, string second)
        {
            return ref Term(first, second);
        }

        public ref FilterBuilder With(string first, ulong second)
        {
            return ref Term(first, second);
        }

        public ref FilterBuilder With(string first, string second)
        {
            return ref Term(first, second);
        }

        public ref FilterBuilder With<T>()
        {
            return ref Term<T>();
        }

        public ref FilterBuilder With<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember);
        }

        public ref FilterBuilder With<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second);
        }

        public ref FilterBuilder With<TFirst>(string second)
        {
            return ref Term<TFirst>(second);
        }

        public ref FilterBuilder With<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>();
        }

        public ref FilterBuilder With<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum);
        }

        public ref FilterBuilder WithSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first);
        }

        public ref FilterBuilder WithSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first);
        }

        public ref FilterBuilder Without(ulong id)
        {
            return ref Term(id).Not();
        }

        public ref FilterBuilder Without(ulong first, ulong second)
        {
            return ref Term(first, second).Not();
        }

        public ref FilterBuilder Without(ulong first, string second)
        {
            return ref Term(first, second).Not();
        }

        public ref FilterBuilder Without(string first, ulong second)
        {
            return ref Term(first, second).Not();
        }

        public ref FilterBuilder Without(string first, string second)
        {
            return ref Term(first, second).Not();
        }

        public ref FilterBuilder Without<T>()
        {
            return ref Term<T>().Not();
        }

        public ref FilterBuilder Without<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember).Not();
        }

        public ref FilterBuilder Without<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second).Not();
        }

        public ref FilterBuilder Without<TFirst>(string second)
        {
            return ref Term<TFirst>(second).Not();
        }

        public ref FilterBuilder Without<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>().Not();
        }

        public ref FilterBuilder Without<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum).Not();
        }

        public ref FilterBuilder WithoutSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first).Not();
        }

        public ref FilterBuilder WithoutSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first).Not();
        }

        public ref FilterBuilder Write(ulong id)
        {
            return ref Term(id).Write();
        }

        public ref FilterBuilder Write(ulong first, ulong second)
        {
            return ref Term(first, second).Write();
        }

        public ref FilterBuilder Write(ulong first, string second)
        {
            return ref Term(first, second).Write();
        }

        public ref FilterBuilder Write(string first, ulong second)
        {
            return ref Term(first, second).Write();
        }

        public ref FilterBuilder Write(string first, string second)
        {
            return ref Term(first, second).Write();
        }

        public ref FilterBuilder Write<T>()
        {
            return ref Term<T>().Write();
        }

        public ref FilterBuilder Write<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember).Write();
        }

        public ref FilterBuilder Write<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second).Write();
        }

        public ref FilterBuilder Write<TFirst>(string second)
        {
            return ref Term<TFirst>(second).Write();
        }

        public ref FilterBuilder Write<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>().Write();
        }

        public ref FilterBuilder Write<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum).Write();
        }

        public ref FilterBuilder WriteSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first).Write();
        }

        public ref FilterBuilder WriteSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first).Write();
        }

        public ref FilterBuilder Read(ulong id)
        {
            return ref Term(id).Read();
        }

        public ref FilterBuilder Read(ulong first, ulong second)
        {
            return ref Term(first, second).Read();
        }

        public ref FilterBuilder Read(ulong first, string second)
        {
            return ref Term(first, second).Read();
        }

        public ref FilterBuilder Read(string first, ulong second)
        {
            return ref Term(first, second).Read();
        }

        public ref FilterBuilder Read(string first, string second)
        {
            return ref Term(first, second).Read();
        }

        public ref FilterBuilder Read<T>()
        {
            return ref Term<T>().Read();
        }

        public ref FilterBuilder Read<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return ref Term(enumMember).Read();
        }

        public ref FilterBuilder Read<TFirst>(ulong second)
        {
            return ref Term<TFirst>(second).Read();
        }

        public ref FilterBuilder Read<TFirst>(string second)
        {
            return ref Term<TFirst>(second).Read();
        }

        public ref FilterBuilder Read<TFirst, TSecond>()
        {
            return ref Term<TFirst, TSecond>().Read();
        }

        public ref FilterBuilder Read<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            return ref Term<TFirst, TSecondEnum>(secondEnum).Read();
        }

        public ref FilterBuilder ReadSecond<TSecond>(ulong first)
        {
            return ref TermSecond<TSecond>(first).Read();
        }

        public ref FilterBuilder ReadSecond<TSecond>(string first)
        {
            return ref TermSecond<TSecond>(first).Read();
        }

        public ref FilterBuilder ScopeOpen()
        {
            return ref With(EcsScopeOpen).Entity(0);
        }

        public ref FilterBuilder ScopeClose()
        {
            return ref With(EcsScopeClose).Entity(0);
        }

        public ref FilterBuilder IncrementTerm()
        {
            if (Terms.Count >= _termIndex)
                Terms.Add(default);

            _termIndex++;
            return ref this;
        }

        public ref FilterBuilder TermAt(int termIndex)
        {
            Assert.True(termIndex > 0, nameof(ECS_INVALID_PARAMETER));

            int prevIndex = _termIndex;
            _termIndex = termIndex - 1;
            IncrementTerm();
            _termIndex = prevIndex;

            Assert.True(ecs_term_is_initialized((ecs_term_t*)Unsafe.AsPointer(ref CurrentTerm)) == 1,
                nameof(ECS_INVALID_PARAMETER));

            return ref this;
        }

        public ref FilterBuilder Arg(int termIndex)
        {
            return ref TermAt(termIndex);
        }


        public ref FilterBuilder Term(ulong id)
        {
            IncrementTerm();
            SetTermId(id);
            return ref this;
        }

        public ref FilterBuilder Term(string name)
        {
            IncrementTerm();
            SetTermId();
            First(name);
            return ref this;
        }

        public ref FilterBuilder Term(ulong first, ulong second)
        {
            IncrementTerm();
            SetTermId(first, second);
            return ref this;
        }

        public ref FilterBuilder Term(ulong first, string second)
        {
            IncrementTerm();
            SetTermId(first);
            Second(second);
            return ref this;
        }

        public ref FilterBuilder Term(string first, ulong second)
        {
            IncrementTerm();
            SetTermId();
            First(first);
            Second(second);
            return ref this;
        }

        public ref FilterBuilder Term(string first, string second)
        {
            IncrementTerm();
            SetTermId();
            First(first);
            Second(second);
            return ref this;
        }

        public ref FilterBuilder Term<T>()
        {
            IncrementTerm();
            SetTermId(Type<T>.Id(World));
            CurrentTerm.inout = EcsInOutDefault;
            return ref this;
        }

        public ref FilterBuilder Term<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(enumMember, World);
            ulong pair = Macros.Pair<TEnum>(enumId, World);
            return ref Term(pair);
        }

        public ref FilterBuilder Term<TFirst>(ulong second)
        {
            return ref Term(Type<TFirst>.Id(World), second);
        }

        public ref FilterBuilder Term<TFirst>(string second)
        {
            return ref Term(Type<TFirst>.Id(World)).Second(second);
        }

        public ref FilterBuilder Term<TFirst, TSecond>()
        {
            return ref Term<TFirst>(Type<TSecond>.Id(World));
        }

        public ref FilterBuilder Term<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, World);
            return ref Term<TFirst>(enumId);
        }

        public ref FilterBuilder TermSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref Term(pair);
        }

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
                CurrentTerm = new ecs_term_t() { id = id };
                return;
            }

            CurrentTerm = new ecs_term_t() { first = new ecs_term_id_t() { id = id } };
        }

        private void SetTermId(ulong first, ulong second)
        {
            CurrentTerm = new ecs_term_t() { id = Macros.Pair(first, second) };
        }
    }
}
