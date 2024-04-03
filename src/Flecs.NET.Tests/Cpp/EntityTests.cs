using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Flecs.NET.Core;
using Xunit;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Tests.Cpp
{
    public unsafe class EntityTests
    {
        public EntityTests()
        {
            FlecsInternal.Reset();
        }

        [Fact]
        public void New()
        {
            using World world = World.Create();

            Entity entity = world.Entity();
            Assert.True(entity != 0);
        }

        [Fact]
        public void NewNamed()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);
            Assert.Equal("Foo", entity.Name());
        }

        [Fact]
        public void NewNamedFromScope()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);
            Assert.Equal("Foo", entity.Name());

            Entity prev = world.SetScope(entity);

            Entity child = world.Entity("Bar");
            Assert.True(child != 0);

            world.SetScope(prev);

            Assert.Equal("Bar", child.Name());
            Assert.Equal("::Foo.Bar", child.Path());
        }

        [Fact]
        private void NewNestedNamedFromFromScope()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);
            Assert.Equal("Foo", entity.Name());

            Entity prev = world.SetScope(entity);

            Entity child = world.Entity("Bar.Hello");
            Assert.True(child != 0);

            world.SetScope(prev);

            Assert.Equal("Hello", child.Name());
            Assert.Equal("::Foo.Bar.Hello", child.Path());
        }

        [Fact]
        private void NewNestedNamedFromNestedScope()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo.Bar");
            Assert.True(entity != 0);
            Assert.Equal("Bar", entity.Name());
            Assert.Equal("::Foo.Bar", entity.Path());

            Entity prev = world.SetScope(entity);

            Entity child = world.Entity("Hello.World");
            Assert.True(child != 0);

            world.SetScope(prev);

            Assert.Equal("World", child.Name());
            Assert.Equal("::Foo.Bar.Hello.World", child.Path());
        }

        [Fact]
        private void NewAdd()
        {
            using World world = World.Create();

            world.Component<Position>();

            Entity entity = world.Entity()
                .Add<Position>();

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());
        }

        [Fact]
        private void NewAdd2()
        {
            using World world = World.Create();

            world.Component<Position>();
            world.Component<Velocity>();

            Entity entity = world.Entity()
                .Add<Position>()
                .Add<Velocity>();

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());
            Assert.True(entity.Has<Velocity>());
        }

        [Fact]
        private void NewSet()
        {
            using World world = World.Create();

            world.Component<Position>();

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void NewSet2()
        {
            using World world = World.Create();

            world.Component<Position>();
            world.Component<Velocity>();

            Entity entity = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());
            Assert.True(entity.Has<Velocity>());

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void Add()
        {
            using World world = World.Create();

            world.Component<Position>();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add<Position>();
            Assert.True(entity.Has<Position>());
        }

        [Fact]
        private void Remove()
        {
            using World world = World.Create();

            world.Component<Position>();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add<Position>();
            Assert.True(entity.Has<Position>());

            entity.Remove<Position>();
            Assert.True(!entity.Has<Position>());
        }

        [Fact]
        private void Set()
        {
            using World world = World.Create();

            world.Component<Position>();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Set(new Position(10, 20));
            Assert.True(entity.Has<Position>());

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void Add2()
        {
            using World world = World.Create();

            world.Component<Position>();
            world.Component<Velocity>();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add<Position>()
                .Add<Velocity>();

            Assert.True(entity.Has<Position>());
            Assert.True(entity.Has<Velocity>());
        }

        [Fact]
        private void AddEntity()
        {
            using World world = World.Create();

            Entity tag = world.Entity();
            Assert.True(tag != 0);

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add(tag);
            Assert.True(entity.Has(tag));
        }

        [Fact]
        private void AddChildOf()
        {
            using World world = World.Create();

            Entity parent = world.Entity();
            Assert.True(parent != 0);

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add(EcsChildOf, parent);
            Assert.True(entity.Has(EcsChildOf, parent));
        }

        [Fact]
        private void AddInstanceOf()
        {
            using World world = World.Create();

            Entity @base = world.Entity();
            Assert.True(@base != 0);

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add(EcsIsA, @base);
            Assert.True(entity.Has(EcsIsA, @base));
        }

        [Fact]
        private void Remove2()
        {
            using World world = World.Create();

            world.Component<Position>();
            world.Component<Velocity>();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add<Position>()
                .Add<Velocity>();

            Assert.True(entity.Has<Position>());
            Assert.True(entity.Has<Velocity>());

            entity.Remove<Position>()
                .Remove<Velocity>();

            Assert.True(!entity.Has<Position>());
            Assert.True(!entity.Has<Velocity>());
        }

        [Fact]
        private void Set2()
        {
            using World world = World.Create();

            world.Component<Position>();
            world.Component<Velocity>();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Set(new Position(10, 20))
                .Set(new Velocity(1, 2));
            Assert.True(entity.Has<Position>());
            Assert.True(entity.Has<Velocity>());

            Position* p = entity.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Velocity* v = entity.GetPtr<Velocity>();
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void RemoveEntity()
        {
            using World world = World.Create();

            Entity tag = world.Entity();
            Assert.True(tag != 0);

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add(tag);
            Assert.True(entity.Has(tag));

            entity.Remove(tag);
            Assert.True(!entity.Has(tag));
        }

        [Fact]
        private void RemoveChildOf()
        {
            using World world = World.Create();

            Entity parent = world.Entity();
            Assert.True(parent != 0);

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add(EcsChildOf, parent);
            Assert.True(entity.Has(EcsChildOf, parent));

            entity.Remove(EcsChildOf, parent);
            Assert.True(!entity.Has(EcsChildOf, parent));
        }

        [Fact]
        private void RemoveInstanceOf()
        {
            using World world = World.Create();

            Entity @base = world.Entity();
            Assert.True(@base != 0);

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            entity.Add(EcsIsA, @base);
            Assert.True(entity.Has(EcsIsA, @base));

            entity.Remove(EcsIsA, @base);
            Assert.True(!entity.Has(EcsIsA, @base));
        }

        [Fact]
        private void GetGeneric()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            void* voidPointer = entity.GetPtr(position);
            Assert.True(voidPointer != null);

            Position* p = (Position*)voidPointer;
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }


        [Fact]
        private void GetMutGeneric()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            bool invoked = false;

            world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) => { invoked = true; });

            void* voidPointer = entity.EnsurePtr(position);
            Assert.True(voidPointer != null);

            Position* p = (Position*)voidPointer;
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            entity.Modified(position);
            Assert.True(invoked);
        }

        [Fact]
        private void GetGenericWithId()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            Id id = position;

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            void* voidPointer = entity.GetPtr(id);
            Assert.True(voidPointer != null);

            Position* p = (Position*)voidPointer;
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void GetGenericWithUlong()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            ulong id = position;

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            void* voidPointer = entity.GetPtr(id);
            Assert.True(voidPointer != null);

            Position* p = (Position*)voidPointer;
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }


        [Fact]
        private void GetMutGenericWithId()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            Id id = position;

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            bool invoked = false;

            world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) => { invoked = true; });

            void* voidPointer = entity.GetPtr(id);
            Assert.True(voidPointer != null);

            Position* p = (Position*)voidPointer;
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            entity.Modified(id);
            Assert.True(invoked);
        }

        [Fact]
        private void GetMutGenericWithUlong()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            ulong id = position;

            Entity entity = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(entity != 0);
            Assert.True(entity.Has<Position>());

            bool invoked = false;

            world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) => { invoked = true; });

            void* voidPointer = entity.EnsurePtr(id);
            Assert.True(voidPointer != null);

            Position* p = (Position*)voidPointer;
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            entity.Modified(id);
            Assert.True(invoked);
        }

        [Fact]
        private void SetGeneric()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();

            Position p = new Position(10, 20);

            Entity e = world.Entity()
                .SetPtr(position, sizeof(Position), &p);

            Assert.True(e.Has<Position>());
            Assert.True(e.Has(position));

            Position* ptr = e.GetPtr<Position>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void SetGenericWithId()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            Id id = position;

            Position p = new Position(10, 20);

            Entity e = world.Entity()
                .SetPtr(id, sizeof(Position), &p);

            Assert.True(e.Has<Position>());
            Assert.True(e.Has(id));

            Position* ptr = e.GetPtr<Position>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void SetGenericWithUlong()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            ulong id = position;

            Position p = new Position(10, 20);

            Entity e = world.Entity()
                .SetPtr(id, sizeof(Position), &p);

            Assert.True(e.Has<Position>());
            Assert.True(e.Has(id));

            Position* ptr = e.GetPtr<Position>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void SetGenericNoSize()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();

            Position p = new Position(10, 20);

            Entity e = world.Entity()
                .SetPtr(position, &p);

            Assert.True(e.Has<Position>());
            Assert.True(e.Has(position));

            Position* ptr = e.GetPtr<Position>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void SetGenericNoSizWithId()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            Id id = position;

            Position p = new Position(10, 20);

            Entity e = world.Entity()
                .SetPtr(id, &p);

            Assert.True(e.Has<Position>());
            Assert.True(e.Has(id));

            Position* ptr = e.GetPtr<Position>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void SetGenericNoSizeWithUlong()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            ulong id = position;

            Position p = new Position(10, 20);

            Entity e = world.Entity()
                .SetPtr(id, &p);

            Assert.True(e.Has<Position>());
            Assert.True(e.Has(id));

            Position* ptr = e.GetPtr<Position>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void AddRole()
        {
            using World world = World.Create();

            Entity entity = world.Entity();

            entity = entity.Id.AddFlags(ECS_PAIR);

            Assert.True((entity & ECS_PAIR) != 0);
        }

        [Fact]
        private void RemoveRole()
        {
            using World world = World.Create();

            Entity entity = world.Entity();

            ulong id = entity;

            entity = entity.Id.AddFlags(ECS_PAIR);

            Assert.True((entity & ECS_PAIR) != 0);

            entity = entity.Id.RemoveFlags();

            Assert.True(entity == id);
        }

        [Fact]
        private void HasRole()
        {
            using World world = World.Create();

            Entity entity = world.Entity();

            entity = entity.Id.AddFlags(ECS_PAIR);

            Assert.True(entity.Id.HasFlags(ECS_PAIR));

            entity = entity.Id.RemoveFlags();

            Assert.True(!entity.Id.HasFlags(ECS_PAIR));
        }

        [Fact]
        private void PairRole()
        {
            using World world = World.Create();

            Entity a = world.Entity();
            Entity b = world.Entity();

            Id pair = new Id(a, b);
            pair = pair.AddFlags(ECS_PAIR);

            Assert.True(pair.HasFlags(ECS_PAIR));

            Entity rel = pair.First();
            Entity obj = pair.Second();

            Assert.True(rel == a);
            Assert.True(obj == b);
        }

        [Fact]
        private void EqualsTrue()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Entity e12 = world.Entity(e1);
            Entity e22 = world.Entity(e2);

            Assert.True(e1 == e12);
            Assert.True(e2 == e22);
            Assert.True(e1 >= e12);
            Assert.True(e1 <= e12);
            Assert.True(e2 >= e22);
            Assert.True(e2 <= e22);
            Assert.True(e1 != e2);

            Assert.True(!(e2 == e12));
            Assert.True(!(e1 == e22));
            Assert.True(!(e2 <= e12));
            Assert.True(!(e1 >= e22));
        }

        [Fact]
        private void Compare0()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity e0 = world.Entity(0);
            Entity e02 = world.Entity(0);

            Assert.True(e != e0);
            Assert.True(e > e0);
            Assert.True(e >= e0);
            Assert.True(e0 < e);
            Assert.True(e0 <= e);

            Assert.True(e0 == e02);
            Assert.True(e0 >= e02);
            Assert.True(e0 <= e02);
        }

        [Fact]
        private void CompareUlong()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            ulong id1 = e1;
            ulong id2 = e2;

            Assert.True(e1 == id1);
            Assert.True(e2 == id2);

            Assert.True(e1 != id2);
            Assert.True(e2 != id1);

            Assert.True(e1 >= id1);
            Assert.True(e2 >= id2);

            Assert.True(e1 <= id1);
            Assert.True(e2 <= id2);

            Assert.True(e1 <= id2);
            Assert.True(e2 >= id1);

            Assert.True(e1 < id2);
            Assert.True(e2 > id1);


            Assert.True(e2 != id1);
            Assert.True(e1 != id2);

            Assert.True(e2 == id2);
            Assert.True(e1 == id1);

            Assert.True(!(e1 >= id2));
            Assert.True(!(e2 <= id1));

            Assert.True(!(e2 < id2));
            Assert.True(!(e1 > id1));
        }

        [Fact]
        private void CompareId()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Id id1 = e1;
            Id id2 = e2;

            Assert.True(e1 == id1);
            Assert.True(e2 == id2);

            Assert.True(e1 != id2);
            Assert.True(e2 != id1);

            Assert.True(e1 >= id1);
            Assert.True(e2 >= id2);

            Assert.True(e1 <= id1);
            Assert.True(e2 <= id2);

            Assert.True(e1 <= id2);
            Assert.True(e2 >= id1);

            Assert.True(e1 < id2);
            Assert.True(e2 > id1);


            Assert.True(e2 != id1);
            Assert.True(e1 != id2);

            Assert.True(e2 == id2);
            Assert.True(e1 == id1);

            Assert.True(!(e1 >= id2));
            Assert.True(!(e2 <= id1));

            Assert.True(!(e2 < id2));
            Assert.True(!(e1 > id1));
        }

        [Fact]
        private void CompareLiteral()
        {
            using World world = World.Create();

            Entity e1 = world.Entity(500);
            Entity e2 = world.Entity(600);

            Assert.True(e1 == 500);
            Assert.True(e2 == 600);

            Assert.True(e1 != 600);
            Assert.True(e2 != 500);

            Assert.True(e1 >= 500);
            Assert.True(e2 >= 600);

            Assert.True(e1 <= 500);
            Assert.True(e2 <= 600);

            Assert.True(e1 <= 600);
            Assert.True(e2 >= 500);

            Assert.True(e1 < 600);
            Assert.True(e2 > 500);


            Assert.True(e2 != 500);
            Assert.True(e1 != 600);

            Assert.True(e2 == 600);
            Assert.True(e1 == 500);

            Assert.True(!(e1 >= 600));
            Assert.True(!(e2 <= 500));

            Assert.True(!(e2 < 600));
            Assert.True(!(e1 > 500));
        }

        [Fact]
        private void GreaterThan()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Assert.True(e2 > e1);
            Assert.True(e2 >= e1);
        }

        [Fact]
        private void LessThan()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Assert.True(e1 < e2);
            Assert.True(e1 <= e2);
        }

        [Fact]
        private void Not0Or1()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            ulong id = e;

            Assert.True(id != 0);
            Assert.True(id != 1);
        }

        [Fact]
        private void HasChildOf()
        {
            using World world = World.Create();

            Entity parent = world.Entity();

            Entity e = world.Entity()
                .Add(EcsChildOf, parent);

            Assert.True(e.Has(EcsChildOf, parent));
        }

        [Fact]
        private void HasInstanceOf()
        {
            using World world = World.Create();

            Entity @base = world.Entity();

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has(EcsIsA, @base));
        }

        [Fact]
        private void HasInstanceOfIndirect()
        {
            using World world = World.Create();

            Entity baseOfBase = world.Entity();

            Entity @base = world.Entity()
                .Add(EcsIsA, baseOfBase);

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has(EcsIsA, baseOfBase));
        }

        [Fact]
        private void NullString()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            Assert.Equal("", e.Name());
        }

        [Fact]
        private void SetName()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Assert.Equal("", e.Name());

            e.SetName("Foo");
            Assert.Equal("Foo", e.Name());
        }

        [Fact]
        private void ChangeName()
        {
            using World world = World.Create();

            Entity e = world.Entity("Bar");
            Assert.Equal("Bar", e.Name());

            e.SetName("Foo");
            Assert.Equal("Foo", e.Name());
        }

        [Fact]
        private void Delete()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position>()
                .Add<Velocity>();

            e.Destruct();

            Assert.True(!e.IsAlive());

            Entity e2 = world.Entity();
            Assert.True((uint)e2 == (uint)e);
            Assert.True(e2 != e);
        }

        [Fact]
        private void Clear()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position>()
                .Add<Velocity>();

            e.Clear();

            Assert.True(!e.Has<Position>());
            Assert.True(!e.Has<Velocity>());

            Entity e2 = world.Entity();
            Assert.True(e2 > e);
        }

        [Fact]
        private void ForceOwned()
        {
            using World world = World.Create();

            Entity prefab = world.Prefab()
                .Add<Position>()
                .Add<Velocity>()
                .Override<Position>();

            Entity e = world.Entity()
                .Add(EcsIsA, prefab);

            Assert.True(e.Has<Position>());
            Assert.True(e.Owns<Position>());
            Assert.True(e.Has<Velocity>());
            Assert.True(!e.Owns<Velocity>());
        }

        [Fact]
        private void ForceOwned2()
        {
            using World world = World.Create();

            Entity prefab = world.Prefab()
                .Add<Position>()
                .Add<Velocity>()
                .Override<Position>()
                .Override<Velocity>();

            Entity e = world.Entity()
                .Add(EcsIsA, prefab);

            Assert.True(e.Has<Position>());
            Assert.True(e.Owns<Position>());
            Assert.True(e.Has<Velocity>());
            Assert.True(e.Owns<Velocity>());
        }

        [Fact]
        private void ForceOwnedNested()
        {
            using World world = World.Create();

            Entity prefab = world.Prefab()
                .Add<Position>()
                .Add<Velocity>()
                .Override<Position>();

            Entity prefab2 = world.Prefab()
                .Add(EcsIsA, prefab);

            Entity e = world.Entity()
                .Add(EcsIsA, prefab2);

            Assert.True(e.Has<Position>());
            Assert.True(e.Owns<Position>());
            Assert.True(e.Has<Velocity>());
            Assert.True(!e.Owns<Velocity>());
        }

        [Fact]
        private void TagHasSizeZero()
        {
            using World world = World.Create();

            Component<MyTag> comp = world.Component<MyTag>();

            EcsComponent* ptr = comp.Entity.GetPtr<EcsComponent>();
            Assert.Equal(0, ptr->size);
            Assert.Equal(0, ptr->alignment);
        }


        [Fact]
        private void GetTarget()
        {
            using World world = World.Create();

            Entity rel = world.Entity();

            Entity obj1 = world.Entity()
                .Add<Position>();

            Entity obj2 = world.Entity()
                .Add<Velocity>();

            Entity obj3 = world.Entity()
                .Add<Mass>();

            Entity child = world.Entity()
                .Add(rel, obj1)
                .Add(rel, obj2)
                .Add(rel, obj3);

            Entity p = child.Target(rel);
            Assert.True(p != 0);
            Assert.True(p == obj1);

            p = child.Target(rel);
            Assert.True(p != 0);
            Assert.True(p == obj1);

            p = child.Target(rel, 1);
            Assert.True(p != 0);
            Assert.True(p == obj2);

            p = child.Target(rel, 2);
            Assert.True(p != 0);
            Assert.True(p == obj3);

            p = child.Target(rel, 3);
            Assert.True(p == 0);
        }

        [Fact]
        private void GetParent()
        {
            using World world = World.Create();

            Entity parent = world.Entity();
            Entity child = world.Entity().ChildOf(parent);

            Assert.True(child.Target(EcsChildOf) == parent);
            Assert.True(child.Parent() == parent);
        }

        [Fact]
        private void IsComponentEnabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position>();

            Assert.True(e.Enabled<Position>());
            Assert.True(!e.Enabled<Velocity>());
        }

        [Fact]
        private void IsEnabledComponentEnabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position>()
                .Enable<Position>();

            Assert.True(e.Enabled<Position>());
        }

        [Fact]
        private void IsDisabledComponentEnabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position>()
                .Disable<Position>();

            Assert.True(!e.Enabled<Position>());
        }

        [Fact]
        private void IsPairEnabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position, TgtA>();

            Assert.True(e.Enabled<Position, TgtA>());
            Assert.True(!e.Enabled<Position, TgtB>());
        }

        [Fact]
        private void IsEnabledPairEnabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position, Tgt>()
                .Enable<Position, Tgt>();

            Assert.True(e.Enabled<Position, Tgt>());
        }

        [Fact]
        private void IsDisabledPairEnabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position, Tgt>()
                .Disable<Position, Tgt>();

            Assert.True(!e.Enabled<Position, Tgt>());
        }

        [Fact]
        private void IsPairEnabledWithIds()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();

            Entity e = world.Entity()
                .Add(rel, tgtA);

            Assert.True(e.Enabled(rel, tgtA));
            Assert.True(!e.Enabled(rel, tgtB));
        }

        [Fact]
        private void IsEnabledPairEnabledWithIds()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add(rel, tgt)
                .Enable(rel, tgt);

            Assert.True(e.Enabled(rel, tgt));
        }

        [Fact]
        private void IsDisabledPairEnabledWithIds()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add(rel, tgt)
                .Disable(rel, tgt);

            Assert.True(!e.Enabled(rel, tgt));
        }

        [Fact]
        private void IsPairEnabledWithTargetId()
        {
            using World world = World.Create();

            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();

            Entity e = world.Entity()
                .Add<Position>(tgtA);

            Assert.True(e.Enabled<Position>(tgtA));
            Assert.True(!e.Enabled<Position>(tgtB));
        }

        [Fact]
        private void IsEnabledPairEnabledWithTargetId()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add<Position>(tgt)
                .Enable<Position>(tgt);

            Assert.True(e.Enabled<Position>(tgt));
        }

        [Fact]
        private void IsDisabledPairEnabledWithTargetId()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add<Position>(tgt)
                .Disable<Position>(tgt);

            Assert.True(!e.Enabled<Position>(tgt));
        }

        [Fact]
        private void GetTypes()
        {
            using World world = World.Create();

            Entity entity = world.Entity();
            Assert.True(entity != 0);

            Types type1 = entity.Type();
            Assert.Equal(0, type1.Count());

            Types type2 = entity.Type();
            Assert.Equal(0, type2.Count());
        }

        [Fact]
        private void GetNonEmptyType()
        {
            using World world = World.Create();

            Entity entity = world.Entity()
                .Add<Position>();
            Assert.True(entity != 0);

            Types type1 = entity.Type();
            Assert.Equal(1, type1.Count());
            Assert.Equal(type1.Get(0), world.Id<Position>());

            Types type2 = entity.Type();
            Assert.Equal(1, type2.Count());
            Assert.Equal(type2.Get(0), world.Id<Position>());
        }

        // [Fact]
        // void set_no_copy() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .Set<Pod>({10));
        //     Assert.Equal(Pod.copy_invoked, 0);
        //
        //     Assert.True(e.Has<Pod>());
        //     const Pod *p = e.GetPtr<Pod>();
        //     Assert.True(p != null);
        //     Assert.Equal(p.Value, 10);
        // }
        //
        // [Fact]
        // void set_copy() {
        //     using World world = World.Create();
        //
        //     Pod val(10);
        //
        //     Entity e = world.Entity()
        //         .Set<Pod>(val);
        //     Assert.Equal(Pod.copy_invoked, 1);
        //
        //     Assert.True(e.Has<Pod>());
        //     const Pod *p = e.GetPtr<Pod>();
        //     Assert.True(p != null);
        //     Assert.Equal(p.Value, 10);
        // }
        //
        [Fact]
        private void SetDeduced()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20));

            Assert.True(e.Has<Position>());

            Position* p = e.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void Override()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .Override<Position>();

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position>());
            Assert.True(e.Owns<Position>());
        }

        [Fact]
        private void OverrideId()
        {
            using World world = World.Create();

            Entity tagA = world.Entity();
            Entity tagB = world.Entity();

            Entity @base = world.Entity()
                .Override(tagA)
                .Add(tagB);

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has(tagA));
            Assert.True(e.Owns(tagA));

            Assert.True(e.Has(tagB));
            Assert.True(!e.Owns(tagB));
        }

        [Fact]
        private void OverridePairWithTargetId()
        {
            using World world = World.Create();

            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();

            Entity @base = world.Entity()
                .Override<Position>(tgtA)
                .Add<Position>(tgtB);

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position>(tgtA));
            Assert.True(e.Owns<Position>(tgtA));

            Assert.True(e.Has<Position>(tgtB));
            Assert.True(!e.Owns<Position>(tgtB));
        }

        [Fact]
        private void OverridePairWithIds()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();

            Entity @base = world.Entity()
                .Override(rel, tgtA)
                .Add(rel, tgtB);

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has(rel, tgtA));
            Assert.True(e.Owns(rel, tgtA));

            Assert.True(e.Has(rel, tgtB));
            Assert.True(!e.Owns(rel, tgtB));
        }

        [Fact]
        private void OverridePair()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .Override<Position, TagA>()
                .Add<Position, TagB>();

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position, TagA>());
            Assert.True(e.Owns<Position, TagA>());

            Assert.True(e.Has<Position, TagB>());
            Assert.True(!e.Owns<Position, TagB>());
        }

        [Fact]
        private void SetOverride()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .SetOverride(new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position>());
            Assert.True(e.Owns<Position>());

            Position* p = e.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetPtr<Position>();
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void SetOverrideLValue()
        {
            using World world = World.Create();

            Position plvalue = new Position(10, 20);

            Entity @base = world.Entity()
                .SetOverride(plvalue);

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position>());
            Assert.True(e.Owns<Position>());

            Position* p = e.GetPtr<Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetPtr<Position>();
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void SetOverridePair()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .SetOverride<Position, Tgt>(new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position, Tgt>());
            Assert.True(e.Owns<Position, Tgt>());

            Position* p = e.GetFirstPtr<Position, Tgt>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetFirstPtr<Position, Tgt>();
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void SetOverridePairWithTargetId()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            Entity @base = world.Entity()
                .SetOverride(tgt, new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Position>(tgt));
            Assert.True(e.Owns<Position>(tgt));

            Position* p = e.GetPtr<Position>(tgt);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetPtr<Position>(tgt);
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void SetOverridePairWithRelTag()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .SetOverride<Tgt, Position>(new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has<Tgt, Position>());
            Assert.True(e.Owns<Tgt, Position>());

            Position* p = e.GetSecondPtr<Tgt, Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetSecondPtr<Tgt, Position>();
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void ImplicitNameToChar()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);
            Assert.Equal("Foo", entity.Name());

            Assert.Equal("Foo", entity.Name());
        }

        [Fact]
        private void Path()
        {
            using World world = World.Create();
            Entity parent = world.Entity("parent");

            using ScopedWorld scope = world.Scope(parent);
            Entity child = world.Entity("child");
            Assert.Equal("::parent.child", child.Path());
        }

        [Fact]
        private void PathFrom()
        {
            using World world = World.Create();

            Entity parent = world.Entity("parent");

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("::parent.child.grandchild", grandchild.Path());
            Assert.Equal("child.grandchild", grandchild.PathFrom(parent));
        }

        [Fact]
        private void PathFromType()
        {
            using World world = World.Create();
            Entity parent = world.Entity<Parent>();

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("::Parent.child.grandchild", grandchild.Path());
            Assert.Equal("child.grandchild", grandchild.PathFrom<Parent>());
        }

        [Fact]
        private void PathCustomSep()
        {
            using World world = World.Create();
            Entity parent = world.Entity("parent");

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            Assert.Equal("parent_child", child.Path("_", ""));
        }

        [Fact]
        private void PathFromCustomSep()
        {
            using World world = World.Create();
            Entity parent = world.Entity("parent");

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("::parent.child.grandchild", grandchild.Path());
            Assert.Equal("child_grandchild", grandchild.PathFrom(parent, "_"));
        }

        [Fact]
        private void PathFromTypeCustomSep()
        {
            using World world = World.Create();
            Entity parent = world.Entity<Parent>();

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("::Parent.child.grandchild", grandchild.Path());
            Assert.Equal("child_grandchild", grandchild.PathFrom<Parent>("_"));
        }

        [Fact]
        private void ImplicitPathToChar()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo.Bar");
            Assert.True(entity != 0);
            Assert.Equal("Bar", entity.Name());

            Assert.Equal("::Foo.Bar", entity.Path());
        }

        [Fact]
        private void ImplicitTypeStringToChar()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);

            Assert.Equal("(Identifier,Name)", entity.Type().Str());
        }

        private void SetTemplate()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Template<int> { X = 10, Y = 20 });

            Template<int>* ptr = e.GetPtr<Template<int>>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void Get1ComponentWithCallback()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Entity e2 = world.Entity()
                .Set(new Position(11, 22));

            Entity e3 = world.Entity()
                .Set(new Velocity(1, 2));

            Assert.True(e1.Read((in Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            }));

            Assert.True(e2.Read((in Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
            }));

            Assert.False(e3.Read((in Position p) => { }));
        }

        [Fact]
        private void Get2ComponentWithCallback()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Entity e2 = world.Entity()
                .Set(new Position(11, 22));

            Entity e3 = world.Entity()
                .Set(new Velocity(1, 2));

            Assert.True(e1.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            }));

            Assert.False(e2.Read((in Position p, in Velocity v) => { }));

            Assert.False(e3.Read((in Position p, in Velocity v) => { }));
        }

        [Fact]
        private void Ensure1ComponentWithCallback()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Entity e2 = world.Entity()
                .Set(new Position(11, 22));

            Entity e3 = world.Entity()
                .Set(new Velocity(1, 2));

            Assert.True(e1.Write((ref Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
                p.X++;
                p.Y += 2;
            }));

            Assert.True(e2.Write((ref Position p) =>
            {
                Assert.Equal(11, p.X);
                Assert.Equal(22, p.Y);
                p.X++;
                p.Y += 2;
            }));

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            p = e2.GetPtr<Position>();
            Assert.Equal(12, p->X);
            Assert.Equal(24, p->Y);

            Assert.False(e3.Read((in Position p) => { }));
        }

        [Fact]
        private void Ensure2ComponentWithCallback()
        {
            using World world = World.Create();

            Entity e1 = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Entity e2 = world.Entity()
                .Set(new Position(11, 22));

            Entity e3 = world.Entity()
                .Set(new Velocity(1, 2));

            Assert.True(e1.Write((ref Position p, ref Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);

                p.X++;
                p.Y += 2;
                v.X += 3;
                v.Y += 4;
            }));

            Assert.False(e2.Read((in Position p, in Velocity v) => { }));

            Assert.False(e3.Read((in Position p, in Velocity v) => { }));

            Position* p = e1.GetPtr<Position>();
            Assert.Equal(11, p->X);
            Assert.Equal(22, p->Y);

            Velocity* v = e1.GetPtr<Velocity>();
            Assert.Equal(4, v->X);
            Assert.Equal(6, v->Y);
        }

        [Fact]
        private void GetComponentWithCallbackNested()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Position(10, 20))
                .Set(new Velocity(1, 2));

            Assert.True(e.Read((in Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.True(e.Read((in Velocity v) =>
                {
                    Assert.Equal(1, v.X);
                    Assert.Equal(2, v.Y);
                }));
            }));
        }

        // TODO: Need way to check if table is locked so a C# exception can be thrown
        // [Fact]
        // [Conditional("DEBUG")]
        // private void EnsureComponentWithCallbackNested()
        // {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .Set(new Position(10, 20))
        //         .Set(new Velocity(1, 2));
        //
        //     Assert.True(e.Write((ref Position p) =>
        //     {
        //         Assert.Equal(10, p.X);
        //         Assert.Equal(20, p.Y);
        //
        //         Assert.Throws<Ecs.AssertionException>(() =>
        //         {
        //             e.Write((ref Velocity p) => { });
        //         });
        //     }));
        // }

        [Fact]
        private void Set1ComponentWithCallback()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Ensure((ref Position p) =>
                {
                    p.X = 10;
                    p.Y = 20;
                });

            Assert.True(e.Has<Position>());

            Position* p = e.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);
        }

        [Fact]
        private void Set2ComponentsWithCallback()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                });

            Assert.True(e.Has<Position>());

            Position* p = e.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Velocity* v = e.GetPtr<Velocity>();
            Assert.True(v != null);
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);
        }

        [Fact]
        private void Set3ComponentsWithCallback()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v, ref Mass m) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                    m = new Mass(50);
                });

            Assert.True(e.Has<Position>());

            Position* p = e.GetPtr<Position>();
            Assert.True(p != null);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Velocity* v = e.GetPtr<Velocity>();
            Assert.True(v != null);
            Assert.Equal(1, v->X);
            Assert.Equal(2, v->Y);

            Mass* m = e.GetPtr<Mass>();
            Assert.True(m != null);
            Assert.Equal(50, m->Value);
        }

        [Fact]
        private void DeferSet1Component()
        {
            using World world = World.Create();

            world.DeferBegin();

            Entity e = world.Entity()
                .Ensure((ref Position p) =>
                {
                    p.X = 10;
                    p.Y = 20;
                });

            Assert.True(!e.Has<Position>());

            world.DeferEnd();

            Assert.True(e.Has<Position>());

            e.Read((in Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });
        }

        [Fact]
        private void DeferSet2Components()
        {
            using World world = World.Create();

            world.DeferBegin();

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                });

            Assert.True(!e.Has<Position>());
            Assert.True(!e.Has<Velocity>());

            world.DeferEnd();

            Assert.True(e.Has<Position>());
            Assert.True(e.Has<Velocity>());

            e.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            });
        }

        [Fact]
        private void DeferSet3Components()
        {
            using World world = World.Create();

            world.DeferBegin();

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v, ref Mass m) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                    m = new Mass(50);
                });

            Assert.True(!e.Has<Position>());
            Assert.True(!e.Has<Velocity>());
            Assert.True(!e.Has<Mass>());

            world.DeferEnd();

            Assert.True(e.Has<Position>());
            Assert.True(e.Has<Velocity>());
            Assert.True(e.Has<Mass>());

            e.Read((in Position p, in Velocity v, in Mass m) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);

                Assert.Equal(50, m.Value);
            });
        }

        [Fact]
        private void Set2WithOnSet()
        {
            using World world = World.Create();

            int positionSet = 0;
            int velocitySet = 0;

            world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) =>
                {
                    positionSet++;
                    Assert.Equal(10, p.X);
                    Assert.Equal(20, p.Y);
                });

            world.Observer<Velocity>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Velocity v) =>
                {
                    velocitySet++;
                    Assert.Equal(1, v.X);
                    Assert.Equal(2, v.Y);
                });

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                });

            Assert.Equal(1, positionSet);
            Assert.Equal(1, velocitySet);

            Assert.True(e.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            }));
        }

        [Fact]
        private void DeferSet2WithOnSet()
        {
            using World world = World.Create();

            int positionSet = 0;
            int velocitySet = 0;

            world.Observer<Position>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Position p) =>
                {
                    positionSet++;
                    Assert.Equal(10, p.X);
                    Assert.Equal(20, p.Y);
                });

            world.Observer<Velocity>()
                .Event(Ecs.OnSet)
                .Each((Entity e, ref Velocity v) =>
                {
                    velocitySet++;
                    Assert.Equal(1, v.X);
                    Assert.Equal(2, v.Y);
                });

            world.DeferBegin();

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                });

            Assert.Equal(0, positionSet);
            Assert.Equal(0, velocitySet);

            world.DeferEnd();

            Assert.Equal(1, positionSet);
            Assert.Equal(1, velocitySet);

            Assert.True(e.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);
            }));
        }

        [Fact]
        private void Set2AfterFluent()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Mass(50))
                .Ensure((ref Position p, ref Velocity v) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                });

            Assert.True(e.Has<Position>());
            Assert.True(e.Has<Velocity>());
            Assert.True(e.Has<Mass>());

            Assert.True(e.Read((in Position p, in Velocity v, in Mass m) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);

                Assert.Equal(50, m.Value);
            }));
        }

        [Fact]
        private void Set2BeforeFluent()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Ensure((ref Position p, ref Velocity v) =>
                {
                    p = new Position(10, 20);
                    v = new Velocity(1, 2);
                })
                .Set(new Mass(50));

            Assert.True(e.Has<Position>());
            Assert.True(e.Has<Velocity>());
            Assert.True(e.Has<Mass>());

            Assert.True(e.Read((in Position p, in Velocity v, in Mass m) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);

                Assert.Equal(50, m.Value);
            }));
        }

        [Fact]
        private void Set2AfterSet1()
        {
            using World world = World.Create();

            int called = 0;

            Entity e = world.Entity().Set(new Position(5, 10));
            Assert.True(e.Has<Position>());

            Assert.True(e.Read((in Position p) =>
            {
                Assert.Equal(5, p.X);
                Assert.Equal(10, p.Y);
            }));

            e.Ensure((ref Position p, ref Velocity v) =>
            {
                p = new Position(10, 20);
                v = new Velocity(1, 2);
            });

            Assert.True(e.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);

                called++;
            }));

            Assert.Equal(1, called);
        }

        [Fact]
        private void Set2AfterSet2()
        {
            using World world = World.Create();

            int called = 0;

            Entity e = world.Entity()
                .Set(new Position(5, 10))
                .Set(new Velocity(1, 2));
            Assert.True(e.Has<Position>());
            Assert.True(e.Has<Velocity>());

            Assert.True(e.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(5, p.X);
                Assert.Equal(10, p.Y);

                Assert.Equal(1, v.X);
                Assert.Equal(2, v.Y);

                called++;
            }));

            Assert.Equal(1, called);

            e.Ensure((ref Position p, ref Velocity v) =>
            {
                p = new Position(10, 20);
                v = new Velocity(3, 4);
            });

            Assert.True(e.Read((in Position p, in Velocity v) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);

                Assert.Equal(3, v.X);
                Assert.Equal(4, v.Y);

                called++;
            }));

            Assert.Equal(2, called);
        }

        [Fact]
        private void WithSelf()
        {
            using World world = World.Create();

            Entity tag = world.Entity().With(() =>
            {
                Entity e1 = world.Entity();
                e1.Set(new Self { Value = e1 });
                Entity e2 = world.Entity();
                e2.Set(new Self { Value = e2 });
                Entity e3 = world.Entity();
                e3.Set(new Self { Value = e3 });
            });

            Component<Self> self = world.Component<Self>();
            Assert.True(!self.Entity.Has(tag));

            int count = 0;
            Query query = world.QueryBuilder().Term(tag).Build();

            query.Each((Entity e) =>
            {
                Assert.True(e.Has(tag));

                Assert.True(e.Read((in Self s) => { Assert.True(s.Value == e); }));

                count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void WithRelationTypeSelf()
        {
            using World world = World.Create();

            Entity bob = world.Entity().With<Likes>(() =>
            {
                Entity e1 = world.Entity();
                e1.Set(new Self(e1));
                Entity e2 = world.Entity();
                e2.Set(new Self(e2));
                Entity e3 = world.Entity();
                e3.Set(new Self(e3));
            });

            Component<Self> self = world.Component<Self>();
            Assert.True(!self.Entity.Has<Likes>(bob));

            int count = 0;
            Query q = world.QueryBuilder().Term<Likes>(bob).Build();

            q.Each((Entity e) =>
            {
                Assert.True(e.Has<Likes>(bob));

                Assert.True(e.Read((in Self s) => { Assert.True(s.Value == e); }));

                count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void WithRelationSelf()
        {
            using World world = World.Create();

            Entity likes = world.Entity();

            Entity bob = world.Entity().With(likes, () =>
            {
                Entity e1 = world.Entity();
                e1.Set(new Self(e1));
                Entity e2 = world.Entity();
                e2.Set(new Self(e2));
                Entity e3 = world.Entity();
                e3.Set(new Self(e3));
            });

            Component<Self> self = world.Component<Self>();
            Assert.True(!self.Entity.Has(likes, bob));

            int count = 0;
            Query q = world.QueryBuilder().Term(likes, bob).Build();

            q.Each((Entity e) =>
            {
                Assert.True(e.Has(likes, bob));

                Assert.True(e.Read((in Self s) => { Assert.True(s.Value == e); }));

                count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void WithSelfWithName()
        {
            using World world = World.Create();

            Entity tier1 = world.Entity("Tier1").With(() =>
            {
                Entity tier2 = world.Entity("Tier2");
                tier2.Set(new Self(tier2));
            });

            Entity tier2 = world.Lookup("Tier2");
            Assert.True(tier2 != 0);

            Assert.True(tier2.Has(tier1));
        }

        [Fact]
        private void WithSelfNested()
        {
            using World world = World.Create();

            Entity tier1 = world.Entity("Tier1").With(() =>
            {
                world.Entity("Tier2").With(() => { world.Entity("Tier3"); });
            });

            Entity tier2 = world.Lookup("Tier2");
            Assert.True(tier2 != 0);

            Entity tier3 = world.Lookup("Tier3");
            Assert.True(tier3 != 0);

            Assert.True(tier2.Has(tier1));
            Assert.True(tier3.Has(tier2));
        }

        [Fact]
        private void WithScope()
        {
            using World world = World.Create();

            Entity parent = world.Entity("P").Scope(() =>
            {
                Entity e1 = world.Entity("C1");
                e1.Set(new Self(e1));
                Entity e2 = world.Entity("C2");
                e2.Set(new Self(e2));
                Entity e3 = world.Entity("C3");
                e3.Set(new Self(e3));

                Assert.True(world.Lookup("C1") == e1);
                Assert.True(world.Lookup("C2") == e2);
                Assert.True(world.Lookup("C3") == e3);

                Assert.True(world.Lookup("::P.C1") == e1);
                Assert.True(world.Lookup("::P.C2") == e2);
                Assert.True(world.Lookup("::P.C3") == e3);
            });

            Assert.True(world.Lookup("C1") == 0);
            Assert.True(world.Lookup("C2") == 0);
            Assert.True(world.Lookup("C3") == 0);

            Assert.True(parent.Lookup("C1") != 0);
            Assert.True(parent.Lookup("C2") != 0);
            Assert.True(parent.Lookup("C3") != 0);

            Assert.True(world.Lookup("P.C1") == parent.Lookup("C1"));
            Assert.True(world.Lookup("P.C2") == parent.Lookup("C2"));
            Assert.True(world.Lookup("P.C3") == parent.Lookup("C3"));

            Component<Self> self = world.Component<Self>();
            Assert.True(!self.Entity.Has(Ecs.ChildOf, parent));

            int count = 0;
            Query q = world.QueryBuilder().Term(Ecs.ChildOf, parent).Build();

            q.Each((Entity e) =>
            {
                Assert.True(e.Has(Ecs.ChildOf, parent));

                Assert.True(e.Read((in Self s) => { Assert.True(s.Value == e); }));

                count++;
            });

            Assert.Equal(3, count);
        }

        [Fact]
        private void WithScopeNested()
        {
            using World world = World.Create();

            Entity parent = world.Entity("P").Scope(() =>
            {
                Entity child = world.Entity("C").Scope(() =>
                {
                    Entity gchild = world.Entity("GC");
                    Assert.True(gchild == world.Lookup("GC"));
                    Assert.True(gchild == world.Lookup("::P.C.GC"));
                });

                Assert.True(world.Lookup("C") == child);
                Assert.True(world.Lookup("::P.C") == child);
                Assert.True(world.Lookup("::P.C.GC") != 0);
            });

            Assert.True(0 == world.Lookup("C"));
            Assert.True(0 == world.Lookup("GC"));
            Assert.True(0 == world.Lookup("C.GC"));

            Entity child = world.Lookup("P.C");
            Assert.True(0 != child);
            Assert.True(child.Has(Ecs.ChildOf, parent));

            Entity gchild = world.Lookup("P.C.GC");
            Assert.True(0 != gchild);
            Assert.True(gchild.Has(Ecs.ChildOf, child));
        }

        [Fact]
        private void WithScopeNestedSameNameAsParent()
        {
            using World world = World.Create();

            Entity parent = world.Entity("P").Scope(() =>
            {
                Entity child = world.Entity("C").Scope(() =>
                {
                    Entity gchild = world.Entity("C");
                    Assert.True(gchild == world.Lookup("C"));
                    Assert.True(gchild == world.Lookup("::P.C.C"));
                });

                Assert.True(world.Lookup("C") == child);
                Assert.True(world.Lookup("::P.C") == child);
                Assert.True(world.Lookup("::P.C.C") != 0);
            });

            Assert.True(0 == world.Lookup("C"));
            Assert.True(0 == world.Lookup("C"));
            Assert.True(0 == world.Lookup("C.C"));

            Entity child = world.Lookup("P.C");
            Assert.True(0 != child);
            Assert.True(child.Has(Ecs.ChildOf, parent));

            Entity gchild = world.Lookup("P.C.C");
            Assert.True(0 != gchild);
            Assert.True(gchild.Has(Ecs.ChildOf, child));
        }

        [Fact]
        private void NoRecursiveLookup()
        {
            using World world = World.Create();

            Entity p = world.Entity("P");
            Entity c = world.Entity("C").ChildOf(p);
            Entity gc = world.Entity("GC").ChildOf(c);

            Assert.True(c.Lookup("GC") == gc);
            Assert.True(c.Lookup("C") == 0);
            Assert.True(c.Lookup("P") == 0);
        }

        [Fact]
        private void DeferNewWithName()
        {
            using World world = World.Create();

            Entity e = default;

            world.Defer(() =>
            {
                e = world.Entity("Foo");
                Assert.True(e != 0);
            });

            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Foo", e.Name());
        }

        [Fact]
        private void DeferNewWithNestedName()
        {
            using World world = World.Create();

            Entity e = default;

            world.Defer(() =>
            {
                e = world.Entity("Foo.Bar");
                Assert.True(e != 0);
            });

            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Bar", e.Name());
            Assert.Equal("::Foo.Bar", e.Path());
        }


        [Fact]
        private void DeferNewWithScopeName()
        {
            using World world = World.Create();

            Entity e = default;
            Entity parent = world.Entity("Parent");

            world.Defer(() =>
            {
                parent.Scope(() =>
                {
                    e = world.Entity("Foo");
                    Assert.True(e != 0);
                });
            });

            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Foo", e.Name());
            Assert.Equal("::Parent.Foo", e.Path());
        }

        [Fact]
        private void DeferNewWithScopeNestedName()
        {
            using World world = World.Create();

            Entity e = default;
            Entity parent = world.Entity("Parent");

            world.Defer(() =>
            {
                parent.Scope(() =>
                {
                    e = world.Entity("Foo.Bar");
                    Assert.True(e != 0);
                });
            });

            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Bar", e.Name());
            Assert.Equal("::Parent.Foo.Bar", e.Path());
        }

        [Fact]
        private void DeferNewWithDeferredScopeNestedName()
        {
            using World world = World.Create();

            Entity e = default;
            Entity parent = default;

            world.Defer(() =>
            {
                parent = world.Entity("Parent").Scope(() =>
                {
                    e = world.Entity("Foo.Bar");
                    Assert.True(e != 0);
                });
            });

            Assert.True(parent.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Parent", parent.Name());
            Assert.Equal("::Parent", parent.Path());

            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Bar", e.Name());
            Assert.Equal("::Parent.Foo.Bar", e.Path());
        }

        [Fact]
        private void DeferNewWithScope()
        {
            using World world = World.Create();

            Entity e = default;
            Entity parent = world.Entity();

            world.Defer(() =>
            {
                parent.Scope(() =>
                {
                    e = world.Entity();
                    Assert.True(e != 0);
                });
            });

            Assert.True(e.Has(EcsChildOf, parent));
        }

        [Fact]
        private void DeferNewWithWith()
        {
            using World world = World.Create();

            Entity e = default;
            Entity tag = world.Entity();

            world.Defer(() =>
            {
                tag.With(() =>
                {
                    e = world.Entity();
                    Assert.True(e != 0);
                    Assert.True(!e.Has(tag));
                });
            });

            Assert.True(e.Has(tag));
        }

        [Fact]
        private void DeferNewWithNameScopeWith()
        {
            using World world = World.Create();

            Entity e = default;
            Entity tag = world.Entity();
            Entity parent = world.Entity("Parent");

            world.Defer(() =>
            {
                tag.With(() =>
                {
                    parent.Scope(() =>
                    {
                        e = world.Entity("Foo");
                        Assert.True(e != 0);
                        Assert.True(!e.Has(tag));
                    });
                    Assert.True(!e.Has(tag));
                });
                Assert.True(!e.Has(tag));
            });

            Assert.True(e.Has(tag));
            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Foo", e.Name());
            Assert.Equal("::Parent.Foo", e.Path());
        }

        [Fact]
        private void DeferNewWithNestedNameScopeWith()
        {
            using World world = World.Create();

            Entity e = default;
            Entity tag = world.Entity();
            Entity parent = world.Entity("Parent");

            world.Defer(() =>
            {
                tag.With(() =>
                {
                    parent.Scope(() =>
                    {
                        e = world.Entity("Foo.Bar");
                        Assert.True(e != 0);
                        Assert.True(!e.Has(tag));
                    });
                    Assert.True(!e.Has(tag));
                });
                Assert.True(!e.Has(tag));
            });

            Assert.True(e.Has(tag));
            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Bar", e.Name());
            Assert.Equal("::Parent.Foo.Bar", e.Path());
        }

        [Fact]
        private void DeferWithWithImplicitComponent()
        {
            using World world = World.Create();

            Entity e = default;

            world.Defer(() =>
            {
                world.With<Tag>(() =>
                {
                    e = world.Entity();
                    Assert.True(!e.Has<Tag>());
                });
                Assert.True(!e.Has<Tag>());
            });

            Assert.True(e.Has<Tag>());
        }

        [Fact]
        private void DeferSuspendResume()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            world.Defer(() =>
            {
                e.Add<TagA>();
                Assert.True(!e.Has<TagA>());

                world.DeferSuspend();
                e.Add<TagB>();
                Assert.True(!e.Has<TagA>());
                Assert.True(e.Has<TagB>());
                world.DeferResume();

                Assert.True(!e.Has<TagA>());
                Assert.True(e.Has<TagB>());
            });

            Assert.True(e.Has<TagA>());
            Assert.True(e.Has<TagB>());
        }

        [Fact]
        private void WithAfterBuilderMethod()
        {
            using World world = World.Create();

            Entity a = world.Entity()
                .Set(new Position(10, 20))
                .With(() => { world.Entity("X"); });

            Entity b = world.Entity().Set(new Position(30, 40))
                .With<Likes>(() => { world.Entity("Y"); });

            Entity c = world.Entity().Set(new Position(50, 60))
                .With(EcsIsA, () => { world.Entity("Z"); });

            Assert.True(a.Read((in Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            }));

            Assert.True(b.Read((in Position p) =>
            {
                Assert.Equal(30, p.X);
                Assert.Equal(40, p.Y);
            }));

            Assert.True(c.Read((in Position p) =>
            {
                Assert.Equal(50, p.X);
                Assert.Equal(60, p.Y);
            }));

            Entity x = world.Lookup("X");
            Assert.True(x != 0);
            Assert.True(x.Has(a));

            Entity y = world.Lookup("Y");
            Assert.True(y != 0);
            Assert.True(y.Has<Likes>(b));

            Entity z = world.Lookup("Z");
            Assert.True(z != 0);
            Assert.True(z.Has(EcsIsA, c));
        }

        [Fact]
        private void WithBeforeBuilderMethod()
        {
            using World world = World.Create();

            Entity a = world.Entity()
                .With(() => { world.Entity("X"); })
                .Set(new Position(10, 20));

            Entity b = world.Entity()
                .With<Likes>(() => { world.Entity("Y"); })
                .Set(new Position(30, 40));

            Entity c = world.Entity()
                .With(EcsIsA, () => { world.Entity("Z"); })
                .Set(new Position(50, 60));

            Assert.True(a.Read((in Position p) =>
            {
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            }));

            Assert.True(b.Read((in Position p) =>
            {
                Assert.Equal(30, p.X);
                Assert.Equal(40, p.Y);
            }));

            Assert.True(c.Read((in Position p) =>
            {
                Assert.Equal(50, p.X);
                Assert.Equal(60, p.Y);
            }));

            Entity x = world.Lookup("X");
            Assert.True(x != 0);
            Assert.True(x.Has(a));

            Entity y = world.Lookup("Y");
            Assert.True(y != 0);
            Assert.True(y.Has<Likes>(b));

            Entity z = world.Lookup("Z");
            Assert.True(z != 0);
            Assert.True(z.Has(EcsIsA, c));
        }

        [Fact]
        private void ScopeAfterBuilderMethod()
        {
            using World world = World.Create();

            world.Entity("P")
                .Set(new Position(10, 20))
                .Scope(() => { world.Entity("C"); });

            Entity c = world.Lookup("P.C");
            Assert.True(c != 0);
        }

        [Fact]
        private void ScopeBeforeBuilderMethod()
        {
            using World world = World.Create();

            world.Entity("P")
                .Scope(() => { world.Entity("C"); })
                .Set(new Position(10, 20));

            Entity c = world.Lookup("P.C");
            Assert.True(c != 0);
        }


        [Fact]
        private void EntityIdStr()
        {
            using World world = World.Create();

            Id id = world.Entity("Foo");

            Assert.Equal("Foo", id.Str());
        }

        [Fact]
        private void PairIdStr()
        {
            using World world = World.Create();

            Id id = world.Pair(world.Entity("Rel"), world.Entity("Obj"));

            Assert.Equal("(Rel,Obj)", id.Str());
        }

        [Fact]
        private void RoleIdStr()
        {
            using World world = World.Create();

            Id id = new Id(world, ECS_OVERRIDE | world.Entity("Foo"));

            Assert.Equal("OVERRIDE|Foo", id.Str());
        }

        [Fact]
        private void IdStrFromEntity()
        {
            using World world = World.Create();

            Entity id = world.Entity("Foo");

            Assert.Equal("Foo", id.Str());
        }

        [Fact]
        private void NullEntity()
        {
            Entity e = Entity.Null();
            Assert.True(e.Id == 0);
        }

        [Fact]
        private void NullEntityWithWorld()
        {
            using World world = World.Create();

            Entity e = Entity.Null(world);
            Assert.True(e.Id == 0);
            Assert.True(e.World == world);
        }

        [Fact]
        private void NullEntityWith0()
        {
            Entity e = new Entity(0);
            Assert.True(e.Id == 0);
            Assert.True(e.World == null);
        }

        [Fact]
        private void NullENtityWithWorldWith0()
        {
            using World world = World.Create();

            Entity e = Entity.Null(world);
            Assert.True(e.Id == 0);
            Assert.True(e.World == world);
        }

        [Fact]
        private void IsWildcard()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Entity p0 = e1;
            Id p1 = world.Pair(e1, e2);
            Id p2 = world.Pair(e1, Ecs.Wildcard);
            Id p3 = world.Pair(Ecs.Wildcard, e2);
            Id p4 = world.Pair(Ecs.Wildcard, Ecs.Wildcard);

            Assert.False(e1.IsWildCard());
            Assert.False(e2.IsWildCard());
            Assert.False(p0.IsWildCard());
            Assert.False(p1.IsWildCard());
            Assert.True(p2.IsWildCard());
            Assert.True(p3.IsWildCard());
            Assert.True(p4.IsWildCard());
        }

        [Fact]
        private void HasUlong()
        {
            using World world = World.Create();

            ulong id1 = world.Entity();
            Assert.True(id1 != 0);

            ulong id2 = world.Entity();
            Assert.True(id2 != 0);

            Entity e = world.Entity()
                .Add(id1);

            Assert.True(e != 0);
            Assert.True(e.Has(id1));
            Assert.False(e.Has(id2));
        }

        [Fact]
        private void HasPairUlong()
        {
            using World world = World.Create();

            ulong id1 = world.Entity();
            Assert.True(id1 != 0);

            ulong id2 = world.Entity();
            Assert.True(id2 != 0);

            ulong id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add(id1, id2);

            Assert.True(e != 0);
            Assert.True(e.Has(id1, id2));
            Assert.False(e.Has(id1, id3));
        }

        [Fact]
        private void HasPairUlongWithType()
        {
            using World world = World.Create();

            ulong id2 = world.Entity();
            Assert.True(id2 != 0);

            ulong id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add<Rel>(id2);

            Assert.True(e != 0);
            Assert.True(e.Has<Rel>(id2));
            Assert.False(e.Has<Rel>(id3));
        }

        [Fact]
        private void HasId()
        {
            using World world = World.Create();

            Id id1 = world.Entity();
            Assert.True(id1 != 0);

            Id id2 = world.Entity();
            Assert.True(id2 != 0);

            Entity e = world.Entity()
                .Add(id1);

            Assert.True(e != 0);
            Assert.True(e.Has(id1));
            Assert.False(e.Has(id2));
        }

        [Fact]
        private void HasPairId()
        {
            using World world = World.Create();

            Id id1 = world.Entity();
            Assert.True(id1 != 0);

            Id id2 = world.Entity();
            Assert.True(id2 != 0);

            Id id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add(id1, id2);

            Assert.True(e != 0);
            Assert.True(e.Has(id1, id2));
            Assert.False(e.Has(id1, id3));
        }

        [Fact]
        private void HasPairIdWithType()
        {
            using World world = World.Create();

            Id id2 = world.Entity();
            Assert.True(id2 != 0);

            Id id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add<Rel>(id2);

            Assert.True(e != 0);
            Assert.True(e.Has<Rel>(id2));
            Assert.False(e.Has<Rel>(id3));
        }

        [Fact]
        private void HasWildCardId()
        {
            using World world = World.Create();

            Id id = world.Entity();
            Assert.True(id != 0);

            Entity e1 = world.Entity().Add(id);
            Entity e2 = world.Entity();

            Assert.True(e1 != 0);
            Assert.True(e2 != 0);

            Assert.True(e1.Has(Ecs.Wildcard));
            Assert.False(e2.Has(Ecs.Wildcard));
        }

        [Fact]
        private void HasWildCardPairId()
        {
            using World world = World.Create();

            Id rel = world.Entity();
            Assert.True(rel != 0);

            Id obj = world.Entity();
            Assert.True(obj != 0);

            Id obj2 = world.Entity();
            Assert.True(obj2 != 0);

            Id w1 = world.Id(rel, Ecs.Wildcard);
            Id w2 = world.Id(Ecs.Wildcard, obj);

            Entity e1 = world.Entity().Add(rel, obj);
            Entity e2 = world.Entity().Add(rel, obj2);

            Assert.True(e1 != 0);
            Assert.True(e2 != 0);

            Assert.True(e1.Has(w1));
            Assert.True(e1.Has(w2));

            Assert.True(e2.Has(w1));
            Assert.False(e2.Has(w2));
        }

        [Fact]
        private void OwnsUlong()
        {
            using World world = World.Create();

            ulong id1 = world.Entity();
            Assert.True(id1 != 0);

            ulong id2 = world.Entity();
            Assert.True(id2 != 0);

            Entity e = world.Entity()
                .Add(id1);

            Assert.True(e != 0);
            Assert.True(e.Owns(id1));
            Assert.False(e.Owns(id2));
        }

        [Fact]
        private void OwnsPairUlong()
        {
            using World world = World.Create();

            ulong id1 = world.Entity();
            Assert.True(id1 != 0);

            ulong id2 = world.Entity();
            Assert.True(id2 != 0);

            ulong id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add(id1, id2);

            Assert.True(e != 0);
            Assert.True(e.Owns(id1, id2));
            Assert.False(e.Owns(id1, id3));
        }

        [Fact]
        private void OwnsPairUlongWithType()
        {
            using World world = World.Create();

            ulong id2 = world.Entity();
            Assert.True(id2 != 0);

            ulong id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add<Rel>(id2);

            Assert.True(e != 0);
            Assert.True(e.Owns<Rel>(id2));
            Assert.False(e.Owns<Rel>(id3));
        }

        [Fact]
        private void OwnsId()
        {
            using World world = World.Create();

            Id id1 = world.Entity();
            Assert.True(id1 != 0);

            Id id2 = world.Entity();
            Assert.True(id2 != 0);

            Entity e = world.Entity()
                .Add(id1);

            Assert.True(e != 0);
            Assert.True(e.Owns(id1));
            Assert.False(e.Owns(id2));
        }

        [Fact]
        private void OwnsPairId()
        {
            using World world = World.Create();

            Id id1 = world.Entity();
            Assert.True(id1 != 0);

            Id id2 = world.Entity();
            Assert.True(id2 != 0);

            Id id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add(id1, id2);

            Assert.True(e != 0);
            Assert.True(e.Owns(id1, id2));
            Assert.False(e.Owns(id1, id3));
        }

        [Fact]
        private void OwnsWildCardId()
        {
            using World world = World.Create();

            Id id = world.Entity();
            Assert.True(id != 0);

            Entity e1 = world.Entity().Add(id);
            Entity e2 = world.Entity();

            Assert.True(e1 != 0);
            Assert.True(e2 != 0);

            Assert.True(e1.Owns(Ecs.Wildcard));
            Assert.False(e2.Owns(Ecs.Wildcard));
        }

        [Fact]
        private void OwnsWildcardPair()
        {
            using World world = World.Create();

            Id rel = world.Entity();
            Assert.True(rel != 0);

            Id obj = world.Entity();
            Assert.True(obj != 0);

            Id obj2 = world.Entity();
            Assert.True(obj2 != 0);

            Id w1 = world.Id(rel, Ecs.Wildcard);
            Id w2 = world.Id(Ecs.Wildcard, obj);

            Entity e1 = world.Entity().Add(rel, obj);
            Entity e2 = world.Entity().Add(rel, obj2);

            Assert.True(e1 != 0);
            Assert.True(e2 != 0);

            Assert.True(e1.Owns(w1));
            Assert.True(e1.Owns(w2));

            Assert.True(e2.Owns(w1));
            Assert.False(e2.Owns(w2));
        }

        [Fact]
        private void OwnsPairIdWithType()
        {
            using World world = World.Create();

            Id id2 = world.Entity();
            Assert.True(id2 != 0);

            Id id3 = world.Entity();
            Assert.True(id3 != 0);

            Entity e = world.Entity()
                .Add<Rel>(id2);

            Assert.True(e != 0);
            Assert.True(e.Owns<Rel>(id2));
            Assert.False(e.Owns<Rel>(id3));
        }

        [Fact]
        private void IdFromWorld()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Assert.True(e != 0);

            Id id1 = world.Id(e);
            Assert.True(id1 != 0);
            Assert.True(id1 == e);
            Assert.True(id1.World == world);
            Assert.False(id1.IsPair());
            Assert.False(id1.IsWildCard());

            Id id2 = world.Id(Ecs.Wildcard);
            Assert.True(id2 != 0);
            Assert.True(id2 == Ecs.Wildcard);
            Assert.True(id2.World == world);
            Assert.False(id2.IsPair());
            Assert.True(id2.IsWildCard());
        }

        [Fact]
        private void IdPairFromWorld()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Assert.True(rel != 0);

            Entity obj = world.Entity();
            Assert.True(obj != 0);

            Id id1 = world.Id(rel, obj);
            Assert.True(id1 != 0);
            Assert.True(id1.First() == rel);
            Assert.True(id1.Second() == obj);
            Assert.True(id1.World == world);
            Assert.True(id1.IsPair());
            Assert.False(id1.IsWildCard());

            Id id2 = world.Id(rel, Ecs.Wildcard);
            Assert.True(id2 != 0);
            Assert.True(id2.First() == rel);
            Assert.True(id2.Second() == Ecs.Wildcard);
            Assert.True(id2.World == world);
            Assert.True(id2.IsPair());
            Assert.True(id2.IsWildCard());
        }

        [Fact]
        private void IdDefaultFromWorld()
        {
            using World world = World.Create();

            Id idDefault = world.Id();
            Assert.True(idDefault == 0);
        }

        [Fact]
        private void IsA()
        {
            using World world = World.Create();

            Entity @base = world.Entity();

            Entity e = world.Entity().IsA(@base);

            Assert.True(e.Has(EcsIsA, @base));
        }

        [Fact]
        private void IsAWithType()
        {
            using World world = World.Create();

            Entity @base = world.Entity<Prefab>();

            Entity e = world.Entity().IsA<Prefab>();

            Assert.True(e.Has(Ecs.IsA, @base));
            Assert.True(e.HasSecond<Prefab>(Ecs.IsA));
        }

        [Fact]
        private void ChildOf()
        {
            using World world = World.Create();

            Entity @base = world.Entity();

            Entity e = world.Entity().ChildOf(@base);

            Assert.True(e.Has(Ecs.ChildOf, @base));
        }

        [Fact]
        private void ChildOfWithType()
        {
            using World world = World.Create();

            Entity @base = world.Entity<Parent>();

            Entity e = world.Entity().ChildOf<Parent>();

            Assert.True(e.Has(EcsChildOf, @base));
            Assert.True(e.HasSecond<Parent>(EcsChildOf));
        }

        [Fact]
        private void SlotOf()
        {
            using World world = World.Create();

            Entity @base = world.Prefab();
            Entity baseChild = world.Prefab()
                .ChildOf(@base)
                .SlotOf(@base);

            Assert.True(baseChild.Has(Ecs.SlotOf, @base));

            Entity inst = world.Entity().IsA(@base);
            Assert.True(inst.Has(baseChild, Ecs.Wildcard));
        }

        [Fact]
        private void SlotOfWithType()
        {
            using World world = World.Create();

            Entity @base = world.Prefab<Parent>();
            Entity baseChild = world.Prefab()
                .ChildOf(@base)
                .SlotOf<Parent>();

            Assert.True(baseChild.Has(Ecs.SlotOf, @base));

            Entity inst = world.Entity().IsA(@base);
            Assert.True(inst.Has(baseChild, Ecs.Wildcard));
        }

        [Fact]
        private void Slot()
        {
            using World world = World.Create();

            Entity @base = world.Prefab();
            Entity baseChild = world.Prefab()
                .ChildOf(@base).Slot();

            Assert.True(baseChild.Has(Ecs.SlotOf, @base));

            Entity inst = world.Entity().IsA(@base);
            Assert.True(inst.Has(baseChild, Ecs.Wildcard));
        }

        [Fact]
        private void IdGetEntity()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            Id id = world.Id(e);

            Assert.True(id.Entity() == e);
        }

        [Fact]
        [Conditional("DEBUG")]
        private void IdGetInvalidEntity()
        {
            using World world = World.Create();

            Entity r = world.Entity();
            Entity o = world.Entity();

            Id id = world.Id(r, o);

            Assert.Throws<Ecs.AssertionException>(() => id.Entity());
        }

        [Fact]
        private void EachInStage()
        {
            using World world = World.Create();

            Entity e = world.Entity().Add<Rel, Obj>();
            Assert.True(e.Has<Rel, Obj>());

            world.ReadonlyBegin();

            World s = world.GetStage(0);
            Entity em = e.Mut(s);
            Assert.True(em.Has<Rel, Obj>());

            int count = 0;

            em.Each<Rel>((Entity obj) =>
            {
                count++;
                Assert.True(obj == world.Id<Obj>());
            });

            Assert.Equal(1, count);

            world.ReadonlyEnd();
        }

        [Fact]
        private void IterRecycledParent()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            e.Destruct();

            Entity e2 = world.Entity();
            Assert.True(e != e2);
            Assert.True((uint)e.Id == (uint)e2.Id);

            Entity eChild = world.Entity().ChildOf(e2);
            int count = 0;

            e2.Children((Entity child) =>
            {
                count++;
                Assert.True(child == eChild);
            });

            Assert.Equal(1, count);
        }

        [Fact]
        private void GetLambdaFromStage()
        {
            using World world = World.Create();

            Entity e = world.Entity().Set(new Position(10, 20));

            world.ReadonlyBegin();

            World stage = world.GetStage(0);

            bool invoked = false;
            e.Mut(stage).Read((in Position p) =>
            {
                invoked = true;
                Assert.Equal(10, p.X);
                Assert.Equal(20, p.Y);
            });
            Assert.True(invoked);

            world.ReadonlyEnd();
        }


        [Fact]
        private void GetObjByTemplate()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity o1 = world.Entity();
            Entity o2 = world.Entity();

            e1.Add<Rel>(o1);
            e1.Add<Rel>(o2);

            Assert.True(o1 == e1.Target<Rel>());
            Assert.True(o1 == e1.Target<Rel>(0));
            Assert.True(o2 == e1.Target<Rel>(1));
        }

        [Fact]
        private void CreateNamedTwiceDeferred()
        {
            using World world = World.Create();

            world.DeferBegin();

            Entity e1 = world.Entity("e");
            Entity e2 = world.Entity("e");

            Entity f1 = world.Entity("p.f");
            Entity f2 = world.Entity("p.f");

            Entity g1 = default;
            Entity g2 = default;

            world.Scope(world.Entity("q"), () => { g1 = world.Entity("g"); });

            world.Scope(world.Entity("q"), () => { g2 = world.Entity("g"); });

            world.DeferEnd();

            Assert.Equal("::e", e1.Path());
            Assert.Equal("::p.f", f1.Path());
            Assert.Equal("::q.g", g1.Path());

            Assert.True(e1 == e2);
            Assert.True(f1 == f2);
            Assert.True(g1 == g2);
        }

        [Fact]
        private void CloneWithValue()
        {
            using World world = World.Create();

            PositionInitialized v = new PositionInitialized(10, 20);

            Entity src = world.Entity().Add<Tag>().Set(v);
            Entity dst = src.Clone();
            Assert.True(dst.Has<Tag>());
            Assert.True(dst.Has<PositionInitialized>());

            PositionInitialized* ptr = dst.GetPtr<PositionInitialized>();
            Assert.True(ptr != null);
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void CloneToExisting()
        {
            using World world = World.Create();

            PositionInitialized v = new PositionInitialized(10, 20);

            Entity src = world.Entity().Add<Tag>().Set(v);
            Entity dst = world.Entity();
            Entity result = src.Clone(true, dst);
            Assert.True(result == dst);

            Assert.True(dst.Has<Tag>());
            Assert.True(dst.Has<PositionInitialized>());

            PositionInitialized* ptr = dst.GetPtr<PositionInitialized>();
            Assert.True(ptr != null);
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        [Fact]
        private void SetDocName()
        {
            using World world = World.Create();

            Entity e = world.Entity("foo_bar")
                .SetDocName("Foo Bar");

            Assert.Equal("foo_bar", e.Name());
            Assert.Equal("Foo Bar", e.DocName());
        }

        [Fact]
        private void SetDocBrief()
        {
            using World world = World.Create();

            Entity e = world.Entity("foo_bar")
                .SetDocBrief("Foo Bar");

            Assert.Equal("foo_bar", e.Name());
            Assert.Equal("Foo Bar", e.DocBrief());
        }

        [Fact]
        private void SetDocDetail()
        {
            using World world = World.Create();

            Entity e = world.Entity("foo_bar")
                .SetDocDetail("Foo Bar");

            Assert.Equal("foo_bar", e.Name());
            Assert.Equal("Foo Bar", e.DocDetail());
        }

        [Fact]
        private void SetDocLink()
        {
            using World world = World.Create();

            Entity e = world.Entity("foo_bar")
                .SetDocLink("Foo Bar");

            Assert.Equal("foo_bar", e.Name());
            Assert.Equal("Foo Bar", e.DocLink());
        }

        [Fact]
        private void EntityWithRootName()
        {
            using World world = World.Create();

            Entity e = world.Entity("::foo");
            Assert.Equal("foo", e.Name());
            Assert.Equal("::foo", e.Path());
        }

        [Fact]
        private void EntityWithRootNameFromScope()
        {
            using World world = World.Create();

            Entity p = world.Entity("parent");
            world.SetScope(p);
            Entity e = world.Entity("::foo");
            world.SetScope(0);

            Assert.Equal("foo", e.Name());
            Assert.Equal("::foo", e.Path());
        }

        [Fact]
        private void EntityWithType()
        {
            using World world = World.Create();

            Entity e = world.Entity<EntityType>();

            Assert.Equal("EntityType", e.Name());
            Assert.Equal("::EntityType", e.Path());

            Entity e2 = world.Entity<EntityType>();
            Assert.True(e == e2);
        }

        [Fact]
        private void PrefabHierarchyWithTypes()
        {
            using World world = World.Create();

            Entity turret = world.Prefab<Turret>();
            Entity turretBase = world.Prefab<Turret.Base>();

            Assert.True(turret != 0);
            Assert.True(turretBase != 0);
            Assert.True(turretBase.Has(EcsChildOf, turret));

            Assert.Equal("::Turret", turret.Path());
            Assert.Equal("::Turret.Base", turretBase.Path());

            Assert.Equal("Turret", turret.Symbol());
            Assert.Equal("Turret.Base", turretBase.Symbol());

            Entity railgun = world.Prefab<Railgun>().IsA<Turret>();
            Entity railgunBase = railgun.Lookup("Base");
            Entity railgunHead = world.Prefab<Railgun.Head>();
            Entity railgunBeam = world.Prefab<Railgun.Beam>();

            Assert.True(railgun != 0);
            Assert.True(railgunBase != 0);
            Assert.True(railgunHead != 0);
            Assert.True(railgunBeam != 0);
            Assert.True(railgunBase.Has(EcsChildOf, railgun));
            Assert.True(railgunHead.Has(EcsChildOf, railgun));
            Assert.True(railgunBeam.Has(EcsChildOf, railgun));

            Assert.Equal("::Railgun", railgun.Path());
            Assert.Equal("::Railgun.Base", railgunBase.Path());
            Assert.Equal("::Railgun.Head", railgunHead.Path());
            Assert.Equal("::Railgun.Beam", railgunBeam.Path());

            Assert.Equal("Railgun", railgun.Symbol());
            Assert.Equal("Railgun.Head", railgunHead.Symbol());
            Assert.Equal("Railgun.Beam", railgunBeam.Symbol());
        }

        [Fact]
        private void PrefabHierarchyWithRootTypes()
        {
            using World world = World.Create();

            Entity turret = world.Prefab<Turret>();
            Entity turretBase = world.Prefab<Base>().ChildOf<Turret>();

            Assert.True(turret != 0);
            Assert.True(turretBase != 0);
            Assert.True(turretBase.Has(EcsChildOf, turret));

            Assert.Equal("::Turret", turret.Path());
            Assert.Equal("::Turret.Base", turretBase.Path());

            Assert.Equal("Turret", turret.Symbol());
            Assert.Equal("Base", turretBase.Symbol());

            Entity inst = world.Entity().IsA<Turret>();
            Assert.True(inst != 0);

            Entity instBase = inst.Lookup("Base");
            Assert.True(instBase != 0);
        }

        // TODO: Fix type registration code.
        // [Fact]
        // void PrefabHierarchyWithChildOverride() {
        //     using World world = World.Create();
        //
        //     var t = world.Prefab<Turret>();
        //     var tb = world.Prefab<Turret.Base>().Add<Foo>();
        //     Assert.True(t != 0);
        //     Assert.True(tb != 0);
        //
        //     var r = world.Prefab<Railgun>().IsA<Turret>();
        //     var rb = world.Prefab<Railgun.Base>().Add<Bar>();
        //     Assert.True(r != 0);
        //     Assert.True(rb != 0);
        //
        //     var i = world.Entity().IsA<Railgun>();
        //     Assert.True(i != 0);
        //     var ib = i.Lookup("Base");
        //     Assert.True(ib != 0);
        //     Assert.True(ib.Has<Foo>());
        //     Assert.True(ib.Has<Bar>());
        // }

        [Fact]
        private void EntityWithNestedType()
        {
            using World world = World.Create();

            Entity e = world.Entity<Parent.EntityType>();
            Entity p = world.Entity<Parent>();

            Assert.Equal("EntityType", e.Name());
            Assert.Equal("::Parent.EntityType", e.Path());
            Assert.True(e.Has(EcsChildOf, p));

            Entity e2 = world.Entity<Parent.EntityType>();
            Assert.True(e == e2);
        }

        [Fact]
        private void EntityWithTypeDefer()
        {
            using World world = World.Create();

            world.DeferBegin();
            Entity e = world.Entity<Tag>();
            world.DeferEnd();

            Assert.Equal("Tag", e.Name());
            Assert.Equal("Tag", e.Symbol());
            Assert.True(world.Id<Tag>() == e);
        }

        [Fact]
        private void AddIfTrueT()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            e.AddIf<Tag>(true);
            Assert.True(e.Has<Tag>());
        }

        [Fact]
        private void AddIfFalseT()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            e.AddIf<Tag>(false);
            Assert.True(!e.Has<Tag>());

            e.Add<Tag>();
            Assert.True(e.Has<Tag>());
            e.AddIf<Tag>(false);
            Assert.True(!e.Has<Tag>());
        }

        [Fact]
        private void AddIfTrueId()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity t = world.Entity();

            e.AddIf(true, t);
            Assert.True(e.Has(t));
        }

        [Fact]
        private void AddIfFalseId()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity t = world.Entity();

            e.AddIf(false, t);
            Assert.True(!e.Has(t));
            e.Add(t);
            Assert.True(e.Has(t));
            e.AddIf(false, t);
            Assert.True(!e.Has(t));
        }

        [Fact]
        private void AddIfTrueRO()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            e.AddIf<Rel, Obj>(true);
            Assert.True(e.Has<Rel, Obj>());
        }

        [Fact]
        private void AddIfFalseRO()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            e.AddIf<Rel, Obj>(false);
            Assert.True(!e.Has<Rel, Obj>());
            e.Add<Rel, Obj>();
            Assert.True(e.Has<Rel, Obj>());
            e.AddIf<Rel, Obj>(false);
            Assert.True(!e.Has<Rel, Obj>());
        }

        [Fact]
        private void AddIfTrueRo()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity o = world.Entity();

            e.AddIf<Rel>(true, o);
            Assert.True(e.Has<Rel>(o));
        }

        [Fact]
        private void AddIfFalseRo()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity o = world.Entity();

            e.AddIf<Rel>(false, o);
            Assert.True(!e.Has<Rel>(o));
            e.Add<Rel>(o);
            Assert.True(e.Has<Rel>(o));
            e.AddIf<Rel>(false, o);
            Assert.True(!e.Has<Rel>(o));
        }

        [Fact]
        private void AddIfTruero()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity r = world.Entity();
            Entity o = world.Entity();

            e.AddIf(true, r, o);
            Assert.True(e.Has(r, o));
        }

        [Fact]
        private void AddIfFalsero()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity r = world.Entity();
            Entity o = world.Entity();

            e.AddIf(false, r, o);
            Assert.True(!e.Has(r, o));
            e.Add(r, o);
            Assert.True(e.Has(r, o));
            e.AddIf(false, r, o);
            Assert.True(!e.Has(r, o));
        }

        [Fact]
        private void AddIfExclusivero()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Entity r = world.Entity().Add(Ecs.Exclusive);
            Entity o1 = world.Entity();
            Entity o2 = world.Entity();

            e.Add(r, o1);
            Assert.True(e.Has(r, o1));

            e.AddIf(true, r, o2);
            Assert.True(!e.Has(r, o1));
            Assert.True(e.Has(r, o2));

            e.AddIf(false, r, o1);
            Assert.True(!e.Has(r, o1));
            Assert.True(!e.Has(r, o2));
        }

        [Fact]
        private void AddIfExclusiveRo()
        {
            using World world = World.Create();

            world.Component<First>().Entity.Add(Ecs.Exclusive);

            Entity e = world.Entity();
            Entity o1 = world.Entity();
            Entity o2 = world.Entity();

            e.Add<First>(o1);
            Assert.True(e.Has<First>(o1));

            e.AddIf<First>(true, o2);
            Assert.True(!e.Has<First>(o1));
            Assert.True(e.Has<First>(o2));

            e.AddIf<First>(false, o1);
            Assert.True(!e.Has<First>(o1));
            Assert.True(!e.Has<First>(o2));
        }

        [Fact]
        private void AddIfExclusiveRO()
        {
            using World world = World.Create();

            world.Component<R>().Entity.Add(Ecs.Exclusive);

            Entity e = world.Entity();

            e.Add<R, O1>();
            Assert.True(e.Has<R, O1>());

            e.AddIf<R, O2>(true);
            Assert.True(!e.Has<R, O1>());
            Assert.True(e.Has<R, O2>());

            e.AddIf<R, O1>(false);
            Assert.True(!e.Has<R, O1>());
            Assert.True(!e.Has<R, O2>());
        }

        [Fact]
        private void ChildrenWithCustomRelation()
        {
            using World world = World.Create();

            Entity rel = world.Entity();

            Entity parent = world.Entity();
            Entity child1 = world.Entity().Add(rel, parent);
            Entity child2 = world.Entity().Add(rel, parent);
            world.Entity().ChildOf(parent);

            bool child1Found = false;
            bool child2Found = false;
            int count = 0;

            parent.Children(rel, (Entity child) =>
            {
                if (child == child1)
                    child1Found = true;
                else if (child == child2) child2Found = true;
                count++;
            });

            Assert.Equal(2, count);
            Assert.True(child1Found);
            Assert.True(child2Found);
        }

        [Fact]
        private void ChildrenWithCustomRelationType()
        {
            using World world = World.Create();

            Entity parent = world.Entity();
            Entity child1 = world.Entity().Add<Rel>(parent);
            Entity child2 = world.Entity().Add<Rel>(parent);
            world.Entity().ChildOf(parent);

            bool child1Found = false;
            bool child2Found = false;
            int count = 0;

            parent.Children<Rel>((Entity child) =>
            {
                if (child == child1)
                    child1Found = true;
                else if (child == child2) child2Found = true;
                count++;
            });

            Assert.Equal(2, count);
            Assert.True(child1Found);
            Assert.True(child2Found);
        }

        [Fact]
        private void ChildrenWithThis()
        {
            using World world = World.Create();

            int count = 0;
            world.Entity(Ecs.This).Children((Entity e) => { count++; });
            Assert.True(count == 0);
        }

        [Fact]
        private void ChildrenWithWildcard()
        {
            using World world = World.Create();

            int count = 0;
            world.Entity(Ecs.Wildcard).Children((Entity e) => { count++; });
            Assert.True(count == 0);
        }

        [Fact]
        private void ChildrenWithAny()
        {
            using World world = World.Create();

            int count = 0;
            world.Entity(Ecs.Any).Children((Entity e) => { count++; });
            Assert.True(count == 0);
        }

        // TODO: Fix this test later. Root entity is not being found?
        // [Fact]
        // void ChildrenFromRoot() {
        //     using World world = World.Create();
        //
        //     int count = 0;
        //     world.Entity(0).Children((Entity e) =>
        //     {
        //         Assert.True(e == world.Entity("flecs"));
        //         count ++;
        //     });
        //     Assert.True(count == 1);
        // }
        //
        // TODO: Fix this test later. Root entity is not being found?
        // [Fact]
        // private void ChildrenFromRootWorld()
        // {
        //     using World world = World.Create();
        //
        //     int count = 0;
        //     world.Children((Entity e) =>
        //     {
        //         Assert.True(e == world.Entity("Flecs"));
        //         count++;
        //     });
        //     Assert.True(count == 1);
        // }

        [Fact]
        private void GetDepth()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity().ChildOf(e1);
            Entity e3 = world.Entity().ChildOf(e2);
            Entity e4 = world.Entity().ChildOf(e3);

            Assert.Equal(0, e1.Depth(EcsChildOf));
            Assert.Equal(1, e2.Depth(EcsChildOf));
            Assert.Equal(2, e3.Depth(EcsChildOf));
            Assert.Equal(3, e4.Depth(EcsChildOf));
        }

        [Fact]
        private void GetDepthWithType()
        {
            using World world = World.Create();

            world.Component<Rel>().Entity.Add(EcsTraversable);

            Entity e1 = world.Entity();
            Entity e2 = world.Entity().Add<Rel>(e1);
            Entity e3 = world.Entity().Add<Rel>(e2);
            Entity e4 = world.Entity().Add<Rel>(e3);

            Assert.Equal(0, e1.Depth<Rel>());
            Assert.Equal(1, e2.Depth<Rel>());
            Assert.Equal(2, e3.Depth<Rel>());
            Assert.Equal(3, e4.Depth<Rel>());
        }

        [Fact]
        private void SetAlias()
        {
            using World world = World.Create();

            Entity e = world.Entity("parent.child");
            e.SetAlias("parent_child");

            Assert.True(e == world.Lookup("parent.child"));
            Assert.True(e == world.Lookup("parent_child"));
        }

        //
        // [Fact]
        // void scoped_world() {
        //     using World world = World.Create();
        //
        //     Entity parent = world.Entity();
        //     Entity child = parent.Scope().Entity();
        //     Assert.True(child.Parent() == parent);
        // }
    }
}
