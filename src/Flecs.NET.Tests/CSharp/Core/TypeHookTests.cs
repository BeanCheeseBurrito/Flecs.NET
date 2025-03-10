using System.Collections.Generic;
using Flecs.NET.Core;
using Flecs.NET.Core.Hooks;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

public unsafe class TypeHookTests
{
    private static int CtorInvoked { get; set; }
    private static int DtorInvoked { get; set; }
    private static int CopyInvoked { get; set; }
    private static int MoveInvoked { get; set; }
    private static int OnAddInvoked { get; set; }
    private static int OnSetInvoked { get; set; }
    private static int OnRemoveInvoked { get; set; }

    private static void ResetInvokes()
    {
        CtorInvoked = 0;
        DtorInvoked = 0;
        MoveInvoked = 0;
        CopyInvoked = 0;
        OnAddInvoked = 0;
        OnSetInvoked = 0;
        OnRemoveInvoked = 0;
    }

    private enum RegistrationKind
    {
        Interface, // Register using interfaces.
        Delegate,  // Register using delegates.
        Pointer    // Register using function pointers.
    }

    private enum TypeKind
    {
        Struct,
        Class
    }

    public static IEnumerable<object[]> TestData = [
        [RegistrationKind.Interface, TypeKind.Struct],
        [RegistrationKind.Interface, TypeKind.Class],
        [RegistrationKind.Delegate,  TypeKind.Struct],
        [RegistrationKind.Delegate,  TypeKind.Class],
        [RegistrationKind.Pointer,   TypeKind.Struct],
        [RegistrationKind.Pointer,   TypeKind.Class],
    ];

    private interface ITestInterface
    {
        public int Value { get; set; }
    }

    private struct Struct :
        ITestInterface,
        ICtorHook<Struct>,
        IDtorHook<Struct>,
        ICopyHook<Struct>,
        IMoveHook<Struct>,
        IOnAddHook<Struct>,
        IOnSetHook<Struct>,
        IOnRemoveHook<Struct>
    {
        public int Value { get; set; }
        public static void Ctor(ref Struct data, TypeInfo _) => Ctor<Struct>(ref data, _);
        public static void Dtor(ref Struct data, TypeInfo _) => Dtor<Struct>(ref data, _);
        public static void Copy(ref Struct dst, ref Struct src, TypeInfo _) => Copy<Struct>(ref dst, ref src, _);
        public static void Move(ref Struct dst, ref Struct src, TypeInfo _) => Move<Struct>(ref dst, ref src, _);
        public static void OnAdd(Iter it, int i, ref Struct data) => OnAdd<Struct>(it, i, ref data);
        public static void OnSet(Iter it, int i, ref Struct data) => OnSet<Struct>(it, i, ref data);
        public static void OnRemove(Iter it, int i, ref Struct data) => OnRemove<Struct>(it, i, ref data);
    }

    private class Class :
        ITestInterface,
        ICtorHook<Class>,
        IDtorHook<Class>,
        ICopyHook<Class>,
        IMoveHook<Class>,
        IOnAddHook<Class>,
        IOnSetHook<Class>,
        IOnRemoveHook<Class>
    {
        public int Value { get; set; }
        public static void Ctor(ref Class data, TypeInfo _) => Ctor<Class>(ref data, _);
        public static void Dtor(ref Class data, TypeInfo _) => Dtor<Class>(ref data, _);
        public static void Copy(ref Class dst, ref Class src, TypeInfo _) => Copy<Class>(ref dst, ref src, _);
        public static void Move(ref Class dst, ref Class src, TypeInfo _) => Move<Class>(ref dst, ref src, _);
        public static void OnAdd(Iter it, int i, ref Class data) => OnAdd<Class>(it, i, ref data);
        public static void OnSet(Iter it, int i, ref Class data) => OnSet<Class>(it, i, ref data);
        public static void OnRemove(Iter it, int i, ref Class data) => OnRemove<Class>(it, i, ref data);
    }

    private static void Ctor<T>(ref T data, TypeInfo _) where T : ITestInterface, new()
    {
        CtorInvoked++;
        data = new T { Value = 10 };
    }

    private static void Dtor<T>(ref T data, TypeInfo _) where T : ITestInterface
    {
        DtorInvoked++;
        data = default!;
    }

    private static void Copy<T>(ref T dst, ref T src, TypeInfo _) where T : ITestInterface
    {
        CopyInvoked++;
        dst = src;
    }

    private static void Move<T>(ref T dst, ref T src, TypeInfo _) where T : ITestInterface
    {
        MoveInvoked++;
        dst = src;
        src = default!;
    }

    private static void OnAdd<T>(Iter it, int i, ref T data) where T : ITestInterface
    {
        OnAddInvoked++;
    }

    private static void OnSet<T>(Iter it, int i, ref T data) where T : ITestInterface
    {
        OnSetInvoked++;
    }

    private static void OnRemove<T>(Iter it, int i, ref T data) where T : ITestInterface
    {
        OnRemoveInvoked++;
    }

    private static void SetupHooks<T>(World world, RegistrationKind registrationKind) where T : ITestInterface, new()
    {
        ResetInvokes();

        if (registrationKind == RegistrationKind.Delegate)
        {
            world.Component<T>()
                .Ctor(Ctor)
                .Dtor(Dtor)
                .Copy(Copy)
                .Move(Move)
                .OnAdd(OnAdd)
                .OnSet(OnSet)
                .OnRemove(OnRemove);
        }
        else if (registrationKind == RegistrationKind.Pointer)
        {
            world.Component<T>()
                .Ctor(&Ctor)
                .Dtor(&Dtor)
                .Copy(&Copy)
                .Move(&Move)
                .OnAdd(&OnAdd)
                .OnSet(&OnSet)
                .OnRemove(&OnRemove);
        }
    }

    private static void SetupHooks(World world, RegistrationKind registrationKind, TypeKind typeKind)
    {
        if (typeKind == TypeKind.Struct)
            SetupHooks<Struct>(world, registrationKind);
        else
            SetupHooks<Class>(world, registrationKind);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    private void Add(RegistrationKind registrationKind, TypeKind typeKind)
    {
        using World world = World.Create();

        SetupHooks(world, registrationKind, typeKind);

        if (typeKind == TypeKind.Struct)
            Assert.Equal(10, world.Entity().Add<Struct>().Get<Struct>().Value);
        else
            Assert.Equal(10, world.Entity().Add<Class>().Get<Class>().Value);

        Assert.Equal(1, CtorInvoked);
        Assert.Equal(0, DtorInvoked);
        Assert.Equal(0, CopyInvoked);
        Assert.Equal(0, MoveInvoked);
        Assert.Equal(1, OnAddInvoked);
        Assert.Equal(0, OnSetInvoked);
        Assert.Equal(0, OnRemoveInvoked);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    private void AddRemove(RegistrationKind registrationKind, TypeKind typeKind)
    {
        using World world = World.Create();

        SetupHooks(world, registrationKind, typeKind);

        Entity e = world.Entity();

        if (typeKind == TypeKind.Struct)
            Assert.Equal(10, e.Add<Struct>().Get<Struct>().Value);
        else
            Assert.Equal(10, e.Add<Class>().Get<Class>().Value);

        Assert.Equal(1, CtorInvoked);
        Assert.Equal(0, DtorInvoked);
        Assert.Equal(0, CopyInvoked);
        Assert.Equal(0, MoveInvoked);
        Assert.Equal(1, OnAddInvoked);
        Assert.Equal(0, OnSetInvoked);
        Assert.Equal(0, OnRemoveInvoked);

        if (typeKind == TypeKind.Struct)
            e.Remove<Struct>();
        else
            e.Remove<Class>();

        Assert.Equal(1, CtorInvoked);
        Assert.Equal(1, DtorInvoked);
        Assert.Equal(0, CopyInvoked);
        Assert.Equal(0, MoveInvoked);
        Assert.Equal(1, OnAddInvoked);
        Assert.Equal(0, OnSetInvoked);
        Assert.Equal(1, OnRemoveInvoked);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    private void AddAdd(RegistrationKind registrationKind, TypeKind typeKind)
    {
        using World world = World.Create();

        SetupHooks(world, registrationKind, typeKind);

        Entity e = world.Entity();

        if (typeKind == TypeKind.Struct)
            Assert.Equal(10, e.Add<Struct>().Get<Struct>().Value);
        else
            Assert.Equal(10, e.Add<Class>().Get<Class>().Value);

        Assert.Equal(1, CtorInvoked);
        Assert.Equal(0, DtorInvoked);
        Assert.Equal(0, CopyInvoked);
        Assert.Equal(0, MoveInvoked);
        Assert.Equal(1, OnAddInvoked);
        Assert.Equal(0, OnSetInvoked);
        Assert.Equal(0, OnRemoveInvoked);

        e.Add<Position>();

        Assert.Equal(2, CtorInvoked);
        Assert.Equal(1, DtorInvoked);
        Assert.Equal(0, CopyInvoked);
        Assert.Equal(1, MoveInvoked);
        Assert.Equal(1, OnAddInvoked);
        Assert.Equal(0, OnSetInvoked);
        Assert.Equal(0, OnRemoveInvoked);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    private void Set(RegistrationKind registrationKind, TypeKind typeKind)
    {
        using World world = World.Create();

        SetupHooks(world, registrationKind, typeKind);

        if (typeKind == TypeKind.Struct)
            Assert.Equal(20, world.Entity().Set(new Struct { Value = 20 }).Get<Struct>().Value);
        else
            Assert.Equal(20, world.Entity().Set(new Class { Value = 20 }).Get<Class>().Value);

        Assert.Equal(1, CtorInvoked);
        Assert.Equal(0, DtorInvoked);
        Assert.Equal(1, CopyInvoked);
        Assert.Equal(0, MoveInvoked);
        Assert.Equal(1, OnAddInvoked);
        Assert.Equal(1, OnSetInvoked);
        Assert.Equal(0, OnRemoveInvoked);
    }
}
