using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

public unsafe class TypeHookTests
{
    public struct Struct
    {
        public int Value { get; set; }

        public static int CtorInvoked { get; set; }
        public static int DtorInvoked { get; set; }
        public static int MoveInvoked { get; set; }
        public static int CopyInvoked { get; set; }
        public static int OnAddInvoked { get; set; }
        public static int OnSetInvoked { get; set; }
        public static int OnRemoveInvoked { get; set; }

        public Struct(int value)
        {
                Value = value;
            }

        public static void Ctor(ref Struct data, TypeInfo _)
        {
                CtorInvoked++;
                data = default;
                data.Value = 10;
            }

        public static void Dtor(ref Struct data, TypeInfo _)
        {
                DtorInvoked++;
                data = default;
            }

        public static void Move(ref Struct dst, ref Struct src, TypeInfo _)
        {
                MoveInvoked++;
                dst = src;
                src = default;
            }

        public static void Copy(ref Struct dst, ref Struct src, TypeInfo _)
        {
                CopyInvoked++;
                dst = src;
            }

        public static void OnAdd(ref Struct data)
        {
                OnAddInvoked++;
            }

        public static void OnSet(ref Struct data)
        {
                OnSetInvoked++;
            }

        public static void OnRemove(ref Struct data)
        {
                OnRemoveInvoked++;
            }

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

        public static void RegisterHookDelegates(World world)
        {
                ResetInvokes();
                world.Component<Struct>()
                    .Ctor(Ctor)
                    .Dtor(Dtor)
                    .Move(Move)
                    .Copy(Copy)
                    .OnAdd(OnAdd)
                    .OnSet(OnSet)
                    .OnRemove(OnRemove);
            }

        public static void RegisterHookPointers(World world)
        {
                ResetInvokes();
                world.Component<Struct>()
                    .Ctor(&Ctor)
                    .Dtor(&Dtor)
                    .Move(&Move)
                    .Copy(&Copy)
                    .OnAdd(&OnAdd)
                    .OnSet(&OnSet)
                    .OnRemove(&OnRemove);
            }
    }

    public class Class
    {
        public int Value { get; set; }

        public static int CtorInvoked { get; set; }
        public static int DtorInvoked { get; set; }
        public static int MoveInvoked { get; set; }
        public static int CopyInvoked { get; set; }
        public static int OnAddInvoked { get; set; }
        public static int OnSetInvoked { get; set; }
        public static int OnRemoveInvoked { get; set; }

        public Class(int value)
        {
                Value = value;
            }

        public static void Ctor(ref Class data, TypeInfo _)
        {
                CtorInvoked++;
                data = new Class(10);
            }

        public static void Dtor(ref Class data, TypeInfo _)
        {
                DtorInvoked++;
                data = default!;
            }

        public static void Move(ref Class dst, ref Class src, TypeInfo _)
        {
                MoveInvoked++;
                dst = src;
                src = default!;
            }

        public static void Copy(ref Class dst, ref Class src, TypeInfo _)
        {
                CopyInvoked++;
                dst = src;
            }
        public static void OnAdd(ref Class data)
        {
                OnAddInvoked++;
            }

        public static void OnSet(ref Class data)
        {
                OnSetInvoked++;
            }

        public static void OnRemove(ref Class data)
        {
                OnRemoveInvoked++;
            }

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

        public static void RegisterHookDelegates(World world)
        {
                ResetInvokes();
                world.Component<Class>()
                    .Ctor(Ctor)
                    .Dtor(Dtor)
                    .Move(Move)
                    .Copy(Copy)
                    .OnAdd(OnAdd)
                    .OnSet(OnSet)
                    .OnRemove(OnRemove);
            }

        public static void RegisterHookPointers(World world)
        {
                ResetInvokes();
                world.Component<Class>()
                    .Ctor(&Ctor)
                    .Dtor(&Dtor)
                    .Move(&Move)
                    .Copy(&Copy)
                    .OnAdd(&OnAdd)
                    .OnSet(&OnSet)
                    .OnRemove(&OnRemove);
            }
    }

    [Fact]
    private void AddUnmanagedDelegates()
    {
            using World world = World.Create();

            Struct.RegisterHookDelegates(world);

            Entity e = world.Entity().Add<Struct>();

            Assert.Equal(10, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void AddManagedDelegates()
    {
            using World world = World.Create();

            Class.RegisterHookDelegates(world);

            Entity e = world.Entity().Add<Class>();

            Assert.Equal(10, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);
        }

    [Fact]
    private void AddRemoveUnmanagedDelegates()
    {
            using World world = World.Create();

            Struct.RegisterHookDelegates(world);

            Entity e = world.Entity().Add<Struct>();

            Assert.Equal(10, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);

            e.Remove<Struct>();

            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(1, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(1, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void AddRemoveManagedDelegates()
    {
            using World world = World.Create();

            Class.RegisterHookDelegates(world);

            Entity e = world.Entity().Add<Class>();

            Assert.Equal(10, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);

            e.Remove<Class>();

            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(1, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(1, Class.OnRemoveInvoked);
        }

    [Fact]
    private void AddAddUnmanagedDelegates()
    {
            using World world = World.Create();

            Struct.RegisterHookDelegates(world);

            Entity e = world.Entity().Add<Struct>();

            Assert.Equal(10, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);

            e.Add<Position>();

            Assert.Equal(2, Struct.CtorInvoked);
            Assert.Equal(1, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(1, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void AddAddManagedDelegates()
    {
            using World world = World.Create();

            Class.RegisterHookDelegates(world);

            Entity e = world.Entity().Add<Class>();

            Assert.Equal(10, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);

            e.Add<Position>();

            Assert.Equal(2, Class.CtorInvoked);
            Assert.Equal(1, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(1, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);
        }

    [Fact]
    private void SetUnmanagedDelegates()
    {
            using World world = World.Create();

            Struct.RegisterHookDelegates(world);

            Entity e = world.Entity().Set(new Struct(20));

            Assert.Equal(20, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(1, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(1, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void SetManagedDelegates()
    {
            using World world = World.Create();

            Class.RegisterHookDelegates(world);

            Entity e = world.Entity().Set(new Class(20));

            Assert.Equal(20, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(1, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(1, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);
        }

    [Fact]
    private void AddUnmanagedPointers()
    {
            using World world = World.Create();

            Struct.RegisterHookPointers(world);

            Entity e = world.Entity().Add<Struct>();

            Assert.Equal(10, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void AddManagedPointers()
    {
            using World world = World.Create();

            Class.RegisterHookPointers(world);

            Entity e = world.Entity().Add<Class>();

            Assert.Equal(10, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);
        }

    [Fact]
    private void AddRemoveUnmanagedPointers()
    {
            using World world = World.Create();

            Struct.RegisterHookPointers(world);

            Entity e = world.Entity().Add<Struct>();

            Assert.Equal(10, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);

            e.Remove<Struct>();

            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(1, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(1, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void AddRemoveManagedPointers()
    {
            using World world = World.Create();

            Class.RegisterHookPointers(world);

            Entity e = world.Entity().Add<Class>();

            Assert.Equal(10, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);

            e.Remove<Class>();

            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(1, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(1, Class.OnRemoveInvoked);
        }

    [Fact]
    private void AddAddUnmanagedPointers()
    {
            using World world = World.Create();

            Struct.RegisterHookPointers(world);

            Entity e = world.Entity().Add<Struct>();

            Assert.Equal(10, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);

            e.Add<Position>();

            Assert.Equal(2, Struct.CtorInvoked);
            Assert.Equal(1, Struct.DtorInvoked);
            Assert.Equal(0, Struct.CopyInvoked);
            Assert.Equal(1, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(0, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void AddAddManagedPointers()
    {
            using World world = World.Create();

            Class.RegisterHookPointers(world);

            Entity e = world.Entity().Add<Class>();

            Assert.Equal(10, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);

            e.Add<Position>();

            Assert.Equal(2, Class.CtorInvoked);
            Assert.Equal(1, Class.DtorInvoked);
            Assert.Equal(0, Class.CopyInvoked);
            Assert.Equal(1, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(0, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);
        }

    [Fact]
    private void SetUnmanagedPointers()
    {
            using World world = World.Create();

            Struct.RegisterHookPointers(world);

            Entity e = world.Entity().Set(new Struct(20));

            Assert.Equal(20, e.Get<Struct>().Value);
            Assert.Equal(1, Struct.CtorInvoked);
            Assert.Equal(0, Struct.DtorInvoked);
            Assert.Equal(1, Struct.CopyInvoked);
            Assert.Equal(0, Struct.MoveInvoked);
            Assert.Equal(1, Struct.OnAddInvoked);
            Assert.Equal(1, Struct.OnSetInvoked);
            Assert.Equal(0, Struct.OnRemoveInvoked);
        }

    [Fact]
    private void SetManagedPointers()
    {
            using World world = World.Create();

            Class.RegisterHookPointers(world);

            Entity e = world.Entity().Set(new Class(20));

            Assert.Equal(20, e.Get<Class>().Value);
            Assert.Equal(1, Class.CtorInvoked);
            Assert.Equal(0, Class.DtorInvoked);
            Assert.Equal(1, Class.CopyInvoked);
            Assert.Equal(0, Class.MoveInvoked);
            Assert.Equal(1, Class.OnAddInvoked);
            Assert.Equal(1, Class.OnSetInvoked);
            Assert.Equal(0, Class.OnRemoveInvoked);
        }
}