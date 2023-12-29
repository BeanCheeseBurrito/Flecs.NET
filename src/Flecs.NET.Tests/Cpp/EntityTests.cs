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
            Assert.Equal("Foo.Bar", child.Path());
        }

        [Fact]
        private void NewNestedNamedFromNestedScope()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo.Bar");
            Assert.True(entity != 0);
            Assert.Equal("Bar", entity.Name());
            Assert.Equal("Foo.Bar", entity.Path());

            Entity prev = world.SetScope(entity);

            Entity child = world.Entity("Hello.World");
            Assert.True(child != 0);

            world.SetScope(prev);

            Assert.Equal("World", child.Name());
            Assert.Equal("Foo.Bar.Hello.World", child.Path());
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

            void* voidPointer = entity.GetMutPtr(position);
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
            Id id = position.Id;

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
            Id id = position.Id;

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

            void* voidPointer = entity.GetMutPtr(id);
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
            Id id = position.Id;

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
        private void set_generic_no_size_w_id()
        {
            using World world = World.Create();

            Component<Position> position = world.Component<Position>();
            Id id = position.Id;

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
        private void set_generic_no_size_w_id_t()
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
        private void add_role()
        {
            using World world = World.Create();

            Entity entity = world.Entity();

            entity = entity.Id.AddFlags(ECS_PAIR);

            Assert.True((entity & ECS_PAIR) != 0);
        }

        [Fact]
        private void remove_role()
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
        private void has_role()
        {
            using World world = World.Create();

            Entity entity = world.Entity();

            entity = entity.Id.AddFlags(ECS_PAIR);

            Assert.True(entity.Id.HasFlags(ECS_PAIR));

            entity = entity.Id.RemoveFlags();

            Assert.True(!entity.Id.HasFlags(ECS_PAIR));
        }

        [Fact]
        private void pair_role()
        {
            using World world = World.Create();

            Entity a = world.Entity();
            Entity b = world.Entity();

            Id pair = new Id(a, b);
            pair = pair.AddFlags(ECS_PAIR).Id;

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
        private void compare_0()
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
        private void compare_id_t()
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
        private void compare_id()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Id id1 = e1.Id;
            Id id2 = e2.Id;

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
        private void compare_literal()
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
        private void greater_than()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Assert.True(e2 > e1);
            Assert.True(e2 >= e1);
        }

        [Fact]
        private void less_than()
        {
            using World world = World.Create();

            Entity e1 = world.Entity();
            Entity e2 = world.Entity();

            Assert.True(e1 < e2);
            Assert.True(e1 <= e2);
        }

        [Fact]
        private void not_0_or_1()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            ulong id = e;

            Assert.True(id != 0);
            Assert.True(id != 1);
        }

        [Fact]
        private void has_childof()
        {
            using World world = World.Create();

            Entity parent = world.Entity();

            Entity e = world.Entity()
                .Add(EcsChildOf, parent);

            Assert.True(e.Has(EcsChildOf, parent));
        }

        [Fact]
        private void has_instanceof()
        {
            using World world = World.Create();

            Entity @base = world.Entity();

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True(e.Has(EcsIsA, @base));
        }

        [Fact]
        private void has_instanceof_indirect()
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
        private void null_string()
        {
            using World world = World.Create();

            Entity e = world.Entity();

            Assert.Equal("", e.Name());
        }

        [Fact]
        private void set_name()
        {
            using World world = World.Create();

            Entity e = world.Entity();
            Assert.Equal("", e.Name());

            e.SetName("Foo");
            Assert.Equal("Foo", e.Name());
        }

        [Fact]
        private void change_name()
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

            // Entity ids should be equal without the generation
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

            Assert.True((e.Enabled<Position, TgtA>()));
            Assert.True((!e.Enabled<Position, TgtB>()));
        }

        [Fact]
        private void is_enabled_pair_enabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position, Tgt>()
                .Enable<Position, Tgt>();

            Assert.True((e.Enabled<Position, Tgt>()));
        }

        [Fact]
        private void is_disabled_pair_enabled()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Add<Position, Tgt>()
                .Disable<Position, Tgt>();

            Assert.True((!e.Enabled<Position, Tgt>()));
        }

        [Fact]
        private void is_pair_enabled_w_ids()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();

            Entity e = world.Entity()
                .Add(rel, tgtA);

            Assert.True((e.Enabled(rel, tgtA)));
            Assert.True((!e.Enabled(rel, tgtB)));
        }

        [Fact]
        private void is_enabled_pair_enabled_w_ids()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add(rel, tgt)
                .Enable(rel, tgt);

            Assert.True((e.Enabled(rel, tgt)));
        }

        [Fact]
        private void is_disabled_pair_enabled_w_ids()
        {
            using World world = World.Create();

            Entity rel = world.Entity();
            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add(rel, tgt)
                .Disable(rel, tgt);

            Assert.True((!e.Enabled(rel, tgt)));
        }

        [Fact]
        private void is_pair_enabled_w_tgt_id()
        {
            using World world = World.Create();

            Entity tgtA = world.Entity();
            Entity tgtB = world.Entity();

            Entity e = world.Entity()
                .Add<Position>(tgtA);

            Assert.True((e.Enabled<Position>(tgtA)));
            Assert.True((!e.Enabled<Position>(tgtB)));
        }

        [Fact]
        private void is_enabled_pair_enabled_w_tgt_id()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add<Position>(tgt)
                .Enable<Position>(tgt);

            Assert.True((e.Enabled<Position>(tgt)));
        }

        [Fact]
        private void is_disabled_pair_enabled_w_tgt_id()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            Entity e = world.Entity()
                .Add<Position>(tgt)
                .Disable<Position>(tgt);

            Assert.True((!e.Enabled<Position>(tgt)));
        }

        [Fact]
        private void get_types()
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
        private void get_nonempty_type()
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
        //         .Set<Pod>({10});
        //     Assert.Equal(Pod.copy_invoked, 0);
        //
        //     Assert.True(e.Has<Pod>());
        //     const Pod *p = e.GetPtr<Pod>();
        //     Assert.True(p != NULL);
        //     Assert.Equal(p.value, 10);
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
        //     Assert.True(p != NULL);
        //     Assert.Equal(p.value, 10);
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
        private void override_pair_w_ids()
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
        private void override_pair()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .Override<Position, TagA>()
                .Add<Position, TagB>();

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True((e.Has<Position, TagA>()));
            Assert.True((e.Owns<Position, TagA>()));

            Assert.True((e.Has<Position, TagB>()));
            Assert.True((!e.Owns<Position, TagB>()));
        }

        [Fact]
        private void set_override()
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
        private void set_override_lvalue()
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
        private void set_override_pair()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .SetOverrideFirst<Position, Tgt>(new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True((e.Has<Position, Tgt>()));
            Assert.True((e.Owns<Position, Tgt>()));

            Position* p = e.GetFirstPtr<Position, Tgt>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetFirstPtr<Position, Tgt>();
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void set_override_pair_w_tgt_id()
        {
            using World world = World.Create();

            Entity tgt = world.Entity();

            Entity @base = world.Entity()
                .SetOverride<Position>(tgt, new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True((e.Has<Position>(tgt)));
            Assert.True((e.Owns<Position>(tgt)));

            Position* p = e.GetPtr<Position>(tgt);
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetPtr<Position>(tgt);
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void set_override_pair_w_rel_tag()
        {
            using World world = World.Create();

            Entity @base = world.Entity()
                .SetOverrideSecond<Tgt, Position>(new Position(10, 20));

            Entity e = world.Entity()
                .Add(EcsIsA, @base);

            Assert.True((e.Has<Tgt, Position>()));
            Assert.True((e.Owns<Tgt, Position>()));

            Position* p = e.GetSecondPtr<Tgt, Position>();
            Assert.Equal(10, p->X);
            Assert.Equal(20, p->Y);

            Position* pBase = @base.GetSecondPtr<Tgt, Position>();
            Assert.True(p != pBase);
            Assert.Equal(10, pBase->X);
            Assert.Equal(20, pBase->Y);
        }

        [Fact]
        private void implicit_name_to_char()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);
            Assert.Equal("Foo", entity.Name());

            Assert.Equal("Foo", entity.Name());
        }

        [Fact]
        void Path()
        {
            using World world = World.Create();
            Entity parent = world.Entity("parent");

            using ScopedWorld scope = world.Scope(parent);
            Entity child = world.Entity("child");
            Assert.Equal("parent.child", child.Path());
        }

        [Fact]
        void PathFrom()
        {
            using World world = World.Create();

            Entity parent = world.Entity("parent");

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("parent.child.grandchild", grandchild.Path());
            Assert.Equal("child.grandchild", grandchild.PathFrom(parent));
        }

        [Fact]
        void PathFromType()
        {
            using World world = World.Create();
            Entity parent = world.Entity<Parent>();

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("Parent.child.grandchild", grandchild.Path());
            Assert.Equal("child.grandchild", grandchild.PathFrom<Parent>());
        }

        [Fact]
        void PathCustomSep()
        {
            using World world = World.Create();
            Entity parent = world.Entity("parent");

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            Assert.Equal("parent_child", child.Path("_", ""));
        }

        [Fact]
        void PathFromCustomSep()
        {
            using World world = World.Create();
            Entity parent = world.Entity("parent");

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("parent.child.grandchild", grandchild.Path());
            Assert.Equal("child_grandchild", grandchild.PathFrom(parent, "_"));
        }

        [Fact]
        void PathFromTypeCustomSep()
        {
            using World world = World.Create();
            Entity parent = world.Entity<Parent>();

            using ScopedWorld parentScope = world.Scope(parent);
            Entity child = world.Entity("child");

            using ScopedWorld childScope = world.Scope(child);
            Entity grandchild = world.Entity("grandchild");

            Assert.Equal("Parent.child.grandchild", grandchild.Path());
            Assert.Equal("child_grandchild", grandchild.PathFrom<Parent>("_"));
        }

        [Fact]
        private void implicit_path_to_char()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo.Bar");
            Assert.True(entity != 0);
            Assert.Equal("Bar", entity.Name());

            Assert.Equal("Foo.Bar", entity.Path());
        }

        [Fact]
        private void implicit_type_str_to_char()
        {
            using World world = World.Create();

            Entity entity = new Entity(world, "Foo");
            Assert.True(entity != 0);

            Assert.Equal("(Identifier,Name)", entity.Type().Str());
        }

        [Fact] // TODO: Continue porting there
        private void SetTemplate()
        {
            using World world = World.Create();

            Entity e = world.Entity()
                .Set(new Template<int> { X = 10, Y = 20 });

            Template<int>* ptr = e.GetPtr<Template<int>>();
            Assert.Equal(10, ptr->X);
            Assert.Equal(20, ptr->Y);
        }

        // [Fact]
        // private void WithSelf()
        // {
        //     using World world = World.Create();
        //
        //     Entity tag = world.Entity().With(() =>
        //     {
        //         Entity e1 = world.Entity(); e1.Set(new Self { Value = e1 });
        //         Entity e2 = world.Entity(); e2.Set(new Self { Value = e2 });
        //         Entity e3 = world.Entity(); e3.Set(new Self { Value = e3 });
        //     });
        //
        //     Component<Self> self = world.Component<Self>();
        //     Assert.True(!self.Entity.Has(tag));
        //
        //     int count = 0;
        //     Query query = world.Query(
        //         filter: world.FilterBuilder().Term(tag)
        //     );
        //
        //     query.Iter(it =>
        //     {
        //         foreach (int i in it)
        //         {
        //             Entity e = it.Entity(i);
        //
        //             Assert.True(e.Has(tag));
        //         }
        //     });
        //
        //         auto q = world.query_builder<>().term(Tag).build();
        //
        //     q.each([&](flecs.entity e) {
        //         test_assert(e.has(Tag));
        //
        //         test_bool(e.get([&](const Self& s){
        //             test_assert(s.value == e);
        //         }), true);
        //
        //         count ++;
        //     });
        //
        //     test_int(count, 3);
        // }

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
        private void defer_new_w_name()
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
        private void defer_new_w_nested_name()
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
            Assert.Equal("Foo.Bar", e.Path());
        }


        [Fact]
        private void defer_new_w_scope_name()
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
            Assert.Equal("Parent.Foo", e.Path());
        }

        [Fact]
        private void defer_new_w_scope_nested_name()
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
            Assert.Equal("Parent.Foo.Bar", e.Path());
        }

        [Fact]
        private void defer_new_w_deferred_scope_nested_name()
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
            Assert.Equal("Parent", parent.Path());

            Assert.True(e.Has<EcsIdentifier>(EcsName));
            Assert.Equal("Bar", e.Name());
            Assert.Equal("Parent.Foo.Bar", e.Path());
        }

        [Fact]
        private void defer_new_w_scope()
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
        private void defer_new_w_with()
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
        private void defer_new_w_name_scope_with()
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
            Assert.Equal("Parent.Foo", e.Path());
        }

        [Fact]
        private void defer_new_w_nested_name_scope_with()
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
            Assert.Equal("Parent.Foo.Bar", e.Path());
        }
        //
        // [Fact]
        // void defer_w_with_implicit_component() {
        //     using World world = World.Create();
        //
        //     struct Tag { };
        //
        //     Entity e;
        //
        //     world.Defer(() =>
        //  {
        //         world.with<Tag>(() => {
        //             e = world.Entity();
        //             Assert.True(!e.Has<Tag>());
        //         });
        //         Assert.True(!e.Has<Tag>());
        //     });
        //
        //     Assert.True(e.Has<Tag>());
        // }
        //
        // [Fact]
        // void defer_suspend_resume() {
        //     using World world = World.Create();
        //
        //     struct TagA { };
        //     struct TagB { };
        //
        //     Entity e = world.Entity();
        //
        //     world.Defer(() =>
        //  {
        //         e.Add<TagA>();
        //         Assert.True(!e.Has<TagA>());
        //
        //         world.defer_suspend();
        //         e.Add<TagB>();
        //         Assert.True(!e.Has<TagA>());
        //         Assert.True(e.Has<TagB>());
        //         world.defer_resume();
        //
        //         Assert.True(!e.Has<TagA>());
        //         Assert.True(e.Has<TagB>());
        //     });
        //
        //     Assert.True(e.Has<TagA>());
        //     Assert.True(e.Has<TagB>());
        // }
        //
        // [Fact]
        // void with_after_builder_method() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //
        //     var A = world.Entity()
        //         .Set(new Position() { X = 10, Y = 20 })
        //         .With(() => {
        //             world.Entity("X");
        //         });
        //
        //     var B = world.Entity().Set<Position>({30, 40})
        //         .with<Likes>(() => {
        //             world.Entity("Y");
        //         });
        //
        //     var C = world.Entity().Set<Position>({50, 60})
        //         .With(EcsIsA, () => {
        //             world.Entity("Z");
        //         });
        //
        //     Assert.True(A.Get([](const Position& p) {
        //         Assert.Equal(10, p->X);
        //         Assert.Equal(20, p->Y);
        //     }));
        //
        //     Assert.True(B.Get([](const Position& p) {
        //         Assert.Equal(p.x, 30);
        //         Assert.Equal(p.y, 40);
        //     }));
        //
        //     Assert.True(C.Get([](const Position& p) {
        //         Assert.Equal(p.x, 50);
        //         Assert.Equal(p.y, 60);
        //     }));
        //
        //     var X = world.Lookup("X");
        //     Assert.True(X != 0);
        //     Assert.True(X.Has(A));
        //
        //     var Y = world.Lookup("Y");
        //     Assert.True(Y != 0);
        //     Assert.True(Y.Has<Likes>(B));
        //
        //     var Z = world.Lookup("Z");
        //     Assert.True(Z != 0);
        //     Assert.True(Z.Has(EcsIsA, C));
        // }
        //
        // [Fact]
        // void with_before_builder_method() {
        //     using World world = World.Create();
        //
        //     struct Likes { };
        //
        //     var A = world.Entity()
        //         .With(() => {
        //             world.Entity("X");
        //         })
        //         .Set(new Position() { X = 10, Y = 20 });
        //
        //     var B = world.Entity().with<Likes>(() => {
        //             world.Entity("Y");
        //         })
        //         .Set<Position>({30, 40});
        //
        //     var C = world.Entity().With(EcsIsA, () => {
        //             world.Entity("Z");
        //         })
        //         .Set<Position>({50, 60});
        //
        //     Assert.True(A.Get([](const Position& p) {
        //         Assert.Equal(10, p->X);
        //         Assert.Equal(20, p->Y);
        //     }));
        //
        //     Assert.True(B.Get([](const Position& p) {
        //         Assert.Equal(p.x, 30);
        //         Assert.Equal(p.y, 40);
        //     }));
        //
        //     Assert.True(C.Get([](const Position& p) {
        //         Assert.Equal(p.x, 50);
        //         Assert.Equal(p.y, 60);
        //     }));
        //
        //     var X = world.Lookup("X");
        //     Assert.True(X != 0);
        //     Assert.True(X.Has(A));
        //
        //     var Y = world.Lookup("Y");
        //     Assert.True(Y != 0);
        //     Assert.True(Y.Has<Likes>(B));
        //
        //     var Z = world.Lookup("Z");
        //     Assert.True(Z != 0);
        //     Assert.True(Z.Has(EcsIsA, C));
        // }
        //
        // [Fact]
        // void scope_after_builder_method() {
        //     using World world = World.Create();
        //
        //     world.Entity("P")
        //         .Set(new Position() { X = 10, Y = 20 })
        //         .Scope(() => {
        //             world.Entity("C");
        //         });
        //
        //     var C = world.Lookup("P.C");
        //     Assert.True(C != 0);
        // }
        //
        // [Fact]
        // void scope_before_builder_method() {
        //     using World world = World.Create();
        //
        //     world.Entity("P")
        //         .Scope(() => {
        //             world.Entity("C");
        //         })
        //         .Set(new Position() { X = 10, Y = 20 });
        //
        //     var C = world.Lookup("P.C");
        //     Assert.True(C != 0);
        // }
        //
        // [Fact]
        // void emplace() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity()
        //         .emplace<Position>(10.0f, 20.0f);
        //
        //     Assert.True(e.Has<Position>());
        //
        //     const Position *p = e.GetPtr<Position>();
        //     Assert.True(p != NULL);
        //     Assert.Equal(10, p->X);
        //     Assert.Equal(20, p->Y);
        // }
        //
        // [Fact]
        // void entity_id_str() {
        //     using World world = World.Create();
        //
        //     Id id = world.Entity("Foo");
        //
        //     Assert.Equal("Foo", id.str());
        // }
        //
        // [Fact]
        // void pair_id_str() {
        //     using World world = World.Create();
        //
        //     Id id = world.pair( world.Entity("Rel"), world.Entity("Obj") );
        //
        //     Assert.Equal("(Rel,Obj)", id.str());
        // }
        //
        // [Fact]
        // void role_id_str() {
        //     using World world = World.Create();
        //
        //     Id id = flecs.id(ecs, ECS_OVERRIDE | world.Entity("Foo"));
        //
        //     Assert.Equal("OVERRIDE|Foo", id.str());
        // }
        //
        // [Fact]
        // void id_str_from_entity_view() {
        //     using World world = World.Create();
        //
        //     flecs.entity_view id = world.Entity("Foo");
        //
        //     Assert.Equal("Foo", id.str());
        // }
        //
        // [Fact]
        // void id_str_from_entity() {
        //     using World world = World.Create();
        //
        //     Entity id = world.Entity("Foo");
        //
        //     Assert.Equal("Foo", id.str());
        // }
        //
        // [Fact]
        // void null_entity() {
        //     Entity e = flecs.entity.null();
        //     Assert.True(e.id() == 0);
        // }
        //
        // [Fact]
        // void null_entity_w_world() {
        //     using World world = World.Create();
        //
        //     Entity e = flecs.entity.null(ecs);
        //     Assert.True(e.id() == 0);
        //     Assert.True(e.world().c_ptr() == world.c_ptr());
        // }
        //
        // [Fact]
        // void null_entity_w_0() {
        //     Entity e = flecs.entity(static_cast<ulong>(0));
        //     Assert.True(e.id() == 0);
        //     Assert.True(e.world().c_ptr() == null);
        // }
        //
        // [Fact]
        // void null_entity_w_world_w_0() {
        //     using World world = World.Create();
        //
        //     Entity e = flecs.entity.null(ecs);
        //     Assert.True(e.id() == 0);
        //     Assert.True(e.world().c_ptr() == world.c_ptr());
        // }
        //
        // [Fact]
        // void entity_view_null_entity() {
        //     flecs.entity_view e = flecs.entity.null();
        //     Assert.True(e.id() == 0);
        // }
        //
        // [Fact]
        // void entity_view_null_entity_w_world() {
        //     using World world = World.Create();
        //
        //     flecs.entity_view e = flecs.entity.null(ecs);
        //     Assert.True(e.id() == 0);
        //     Assert.True(e.world().c_ptr() == world.c_ptr());
        // }
        //
        // [Fact]
        // void entity_view_null_entity_w_0() {
        //     flecs.entity_view e = flecs.entity(static_cast<ulong>(0));
        //     Assert.True(e.id() == 0);
        //     Assert.True(e.world().c_ptr() == null);
        // }
        //
        // [Fact]
        // void entity_view_null_entity_w_world_w_0() {
        //     using World world = World.Create();
        //
        //     flecs.entity_view e = flecs.entity.null(ecs);
        //     Assert.True(e.id() == 0);
        //     Assert.True(e.world().c_ptr() == world.c_ptr());
        // }
        //
        // [Fact]
        // void is_wildcard() {
        //     using World world = World.Create();
        //
        //     var e1 = world.Entity();
        //     var e2 = world.Entity();
        //
        //     var p0 = e1;
        //     var p1 = world.pair(e1, e2);
        //     var p2 = world.pair(e1, flecs.Wildcard);
        //     var p3 = world.pair(flecs.Wildcard, e2);
        //     var p4 = world.pair(flecs.Wildcard, flecs.Wildcard);
        //
        //     test_bool(e1.is_wildcard(), false);
        //     test_bool(e2.is_wildcard(), false);
        //     test_bool(p0.is_wildcard(), false);
        //     test_bool(p1.is_wildcard(), false);
        //     test_bool(p2.is_wildcard(), true);
        //     test_bool(p3.is_wildcard(), true);
        //     test_bool(p4.is_wildcard(), true);
        // }
        //
        // [Fact]
        // void has_id_t() {
        //     using World world = World.Create();
        //
        //     ulong id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     ulong id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Has(id_1), true);
        //     test_bool(e.Has(id_2), false);
        // }
        //
        // [Fact]
        // void has_pair_id_t() {
        //     using World world = World.Create();
        //
        //     ulong id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     ulong id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     ulong id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1, id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Has(id_1, id_2), true);
        //     test_bool(e.Has(id_1, id_3), false);
        // }
        //
        // [Fact]
        // void has_pair_id_t_w_type() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     ulong id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     ulong id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add<Rel>(id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Has<Rel>(id_2), true);
        //     test_bool(e.Has<Rel>(id_3), false);
        // }
        //
        // [Fact]
        // void has_id() {
        //     using World world = World.Create();
        //
        //     flecs.id id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     flecs.id id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Has(id_1), true);
        //     test_bool(e.Has(id_2), false);
        // }
        //
        // [Fact]
        // void has_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs.id id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     flecs.id id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     flecs.id id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1, id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Has(id_1, id_2), true);
        //     test_bool(e.Has(id_1, id_3), false);
        // }
        //
        // [Fact]
        // void has_pair_id_w_type() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     flecs.id id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     flecs.id id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add<Rel>(id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Has<Rel>(id_2), true);
        //     test_bool(e.Has<Rel>(id_3), false);
        // }
        //
        // [Fact]
        // void has_wildcard_id() {
        //     using World world = World.Create();
        //
        //     Id id = world.Entity();
        //     Assert.True(id != 0);
        //
        //     var e1 = world.Entity().Add(id);
        //     var e2 = world.Entity();
        //
        //     Assert.True(e1 != 0);
        //     Assert.True(e2 != 0);
        //
        //     test_bool(e1.Has(flecs.Wildcard), true);
        //     test_bool(e2.Has(flecs.Wildcard), false);
        // }
        //
        // [Fact]
        // void has_wildcard_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs.id rel = world.Entity();
        //     Assert.True(rel != 0);
        //
        //     flecs.id obj = world.Entity();
        //     Assert.True(obj != 0);
        //
        //     flecs.id obj_2 = world.Entity();
        //     Assert.True(obj_2 != 0);
        //
        //     flecs.id w1 = world.id(rel, flecs.Wildcard);
        //     flecs.id w2 = world.id(flecs.Wildcard, obj);
        //
        //     var e1 = world.Entity().Add(rel, obj);
        //     var e2 = world.Entity().Add(rel, obj_2);
        //
        //     Assert.True(e1 != 0);
        //     Assert.True(e2 != 0);
        //
        //     test_bool(e1.Has(w1), true);
        //     test_bool(e1.Has(w2), true);
        //
        //     test_bool(e2.Has(w1), true);
        //     test_bool(e2.Has(w2), false);
        // }
        //
        // [Fact]
        // void owns_id_t() {
        //     using World world = World.Create();
        //
        //     ulong id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     ulong id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Owns(id_1), true);
        //     test_bool(e.Owns(id_2), false);
        // }
        //
        // [Fact]
        // void owns_pair_id_t() {
        //     using World world = World.Create();
        //
        //     ulong id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     ulong id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     ulong id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1, id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Owns(id_1, id_2), true);
        //     test_bool(e.Owns(id_1, id_3), false);
        // }
        //
        // [Fact]
        // void owns_pair_id_t_w_type() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     ulong id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     ulong id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add<Rel>(id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Owns<Rel>(id_2), true);
        //     test_bool(e.Owns<Rel>(id_3), false);
        // }
        //
        // [Fact]
        // void owns_id() {
        //     using World world = World.Create();
        //
        //     flecs.id id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     flecs.id id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Owns(id_1), true);
        //     test_bool(e.Owns(id_2), false);
        // }
        //
        // [Fact]
        // void owns_pair_id() {
        //     using World world = World.Create();
        //
        //     flecs.id id_1 = world.Entity();
        //     Assert.True(id_1 != 0);
        //
        //     flecs.id id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     flecs.id id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add(id_1, id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Owns(id_1, id_2), true);
        //     test_bool(e.Owns(id_1, id_3), false);
        // }
        //
        // [Fact]
        // void owns_wildcard_id() {
        //     using World world = World.Create();
        //
        //     Id id = world.Entity();
        //     Assert.True(id != 0);
        //
        //     var e1 = world.Entity().Add(id);
        //     var e2 = world.Entity();
        //
        //     Assert.True(e1 != 0);
        //     Assert.True(e2 != 0);
        //
        //     test_bool(e1.Owns(flecs.Wildcard), true);
        //     test_bool(e2.Owns(flecs.Wildcard), false);
        // }
        //
        // [Fact]
        // void owns_wildcard_pair() {
        //     using World world = World.Create();
        //
        //     flecs.id rel = world.Entity();
        //     Assert.True(rel != 0);
        //
        //     flecs.id obj = world.Entity();
        //     Assert.True(obj != 0);
        //
        //     flecs.id obj_2 = world.Entity();
        //     Assert.True(obj_2 != 0);
        //
        //     flecs.id w1 = world.id(rel, flecs.Wildcard);
        //     flecs.id w2 = world.id(flecs.Wildcard, obj);
        //
        //     var e1 = world.Entity().Add(rel, obj);
        //     var e2 = world.Entity().Add(rel, obj_2);
        //
        //     Assert.True(e1 != 0);
        //     Assert.True(e2 != 0);
        //
        //     test_bool(e1.Owns(w1), true);
        //     test_bool(e1.Owns(w2), true);
        //
        //     test_bool(e2.Owns(w1), true);
        //     test_bool(e2.Owns(w2), false);
        // }
        //
        // [Fact]
        // void owns_pair_id_w_type() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     flecs.id id_2 = world.Entity();
        //     Assert.True(id_2 != 0);
        //
        //     flecs.id id_3 = world.Entity();
        //     Assert.True(id_3 != 0);
        //
        //     Entity e = world.Entity()
        //         .Add<Rel>(id_2);
        //
        //     Assert.True(e != 0);
        //     test_bool(e.Owns<Rel>(id_2), true);
        //     test_bool(e.Owns<Rel>(id_3), false);
        // }
        //
        // [Fact]
        // void id_from_world() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     Assert.True(e != 0);
        //
        //     flecs.id id_1 = world.id(e);
        //     Assert.True(id_1 != 0);
        //     Assert.True(id_1 == e);
        //     Assert.True(id_1.world() == ecs);
        //     test_bool(id_1.is_pair(), false);
        //     test_bool(id_1.is_wildcard(), false);
        //
        //     flecs.id id_2 = world.id(flecs.Wildcard);
        //     Assert.True(id_2 != 0);
        //     Assert.True(id_2 == flecs.Wildcard);
        //     Assert.True(id_2.world() == ecs);
        //     test_bool(id_2.is_pair(), false);
        //     test_bool(id_2.is_wildcard(), true);
        // }
        //
        // [Fact]
        // void id_pair_from_world() {
        //     using World world = World.Create();
        //
        //     var rel = world.Entity();
        //     Assert.True(rel != 0);
        //
        //     var obj = world.Entity();
        //     Assert.True(obj != 0);
        //
        //     flecs.id id_1 = world.id(rel, obj);
        //     Assert.True(id_1 != 0);
        //     Assert.True(id_1.first() == rel);
        //     Assert.True(id_1.second() == obj);
        //     Assert.True(id_1.world() == ecs);
        //     test_bool(id_1.is_pair(), true);
        //     test_bool(id_1.is_wildcard(), false);
        //
        //     flecs.id id_2 = world.id(rel, flecs.Wildcard);
        //     Assert.True(id_2 != 0);
        //     Assert.True(id_2.first() == rel);
        //     Assert.True(id_2.second() == flecs.Wildcard);
        //     Assert.True(id_2.world() == ecs);
        //     test_bool(id_2.is_pair(), true);
        //     test_bool(id_2.is_wildcard(), true);
        // }
        //
        // [Fact]
        // void id_default_from_world() {
        //     using World world = World.Create();
        //
        //     flecs.id id_default = world.id();
        //     Assert.True(id_default == 0);
        // }
        //
        // [Fact]
        // void is_a() {
        //     using World world = World.Create();
        //
        //     Entity @base = world.Entity();
        //
        //     Entity e = world.Entity().is_a(@base);
        //
        //     Assert.True(e.Has(EcsIsA, @base));
        // }
        //
        // [Fact]
        // void is_a_w_type() {
        //     using World world = World.Create();
        //
        //     struct Prefab { };
        //
        //     var @base = world.Entity<Prefab>();
        //
        //     Entity e = world.Entity().is_a<Prefab>();
        //
        //     Assert.True(e.Has(EcsIsA, @base));
        //     Assert.True(e.has_second<Prefab>(EcsIsA));
        // }
        //
        // [Fact]
        // void child_of() {
        //     using World world = World.Create();
        //
        //     Entity @base = world.Entity();
        //
        //     Entity e = world.Entity().ChildOf(@base);
        //
        //     Assert.True(e.Has(EcsChildOf, @base));
        // }
        //
        // [Fact]
        // void child_of_w_type() {
        //     using World world = World.Create();
        //
        //     struct Parent { };
        //
        //     var @base = world.Entity<Parent>();
        //
        //     Entity e = world.Entity().child_of<Parent>();
        //
        //     Assert.True(e.Has(EcsChildOf, @base));
        //     Assert.True(e.has_second<Parent>(EcsChildOf));
        // }
        //
        // [Fact]
        // void slot_of() {
        //     using World world = World.Create();
        //
        //     var @base = world.Prefab();
        //     var base_child = world.Prefab()
        //         .ChildOf(@base)
        //         .slot_of(@base);
        //
        //     Assert.True(base_child.Has(flecs.SlotOf, @base));
        //
        //     var inst = world.Entity().is_a(@base);
        //     Assert.True(inst.Has(base_child, flecs.Wildcard));
        // }
        //
        // [Fact]
        // void slot_of_w_type() {
        //     using World world = World.Create();
        //
        //     struct Parent { };
        //
        //     var @base = world.prefab<Parent>();
        //     var base_child = world.Prefab()
        //         .ChildOf(@base)
        //         .slot_of<Parent>();
        //
        //     Assert.True(base_child.Has(flecs.SlotOf, @base));
        //
        //     var inst = world.Entity().is_a(@base);
        //     Assert.True(inst.Has(base_child, flecs.Wildcard));
        // }
        //
        // [Fact]
        // void slot() {
        //     using World world = World.Create();
        //
        //     var @base = world.Prefab();
        //     var base_child = world.Prefab()
        //         .ChildOf(@base).slot();
        //
        //     Assert.True(base_child.Has(flecs.SlotOf, @base));
        //
        //     var inst = world.Entity().is_a(@base);
        //     Assert.True(inst.Has(base_child, flecs.Wildcard));
        // }
        //
        // [Fact]
        // void id_get_entity() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //
        //     var id = world.id(e);
        //
        //     Assert.True(id.Entity() == e);
        // }
        //
        // [Fact]
        // void id_get_invalid_entity() {
        //     install_test_abort();
        //
        //     using World world = World.Create();
        //
        //     var r = world.Entity();
        //     var o = world.Entity();
        //
        //     var id = world.id(r, o);
        //
        //     test_expect_abort();
        //
        //     id.Entity();
        // }
        //
        // [Fact]
        // void each_in_stage() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //     struct Obj { };
        //
        //     Entity e = world.Entity().Add<Rel, Obj>();
        //     Assert.True((e.Has<Rel, Obj>()));
        //
        //     world.readonly_begin();
        //
        //     var s = world.get_stage(0);
        //     var em = e.mut(s);
        //     Assert.True((em.Has<Rel, Obj>()));
        //
        //     int count = 0;
        //
        //     em.each<Rel>([&](Entity obj) {
        //         count ++;
        //         Assert.True(obj == world.Id<Obj>());
        //     });
        //
        //     Assert.Equal(count, 1);
        //
        //     world.readonly_end();
        // }
        //
        // [Fact]
        // void iter_recycled_parent() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     e.Destruct();
        //
        //     var e2 = world.Entity();
        //     Assert.True(e != e2);
        //     Assert.True((uint)e.id() == (uint)e2.id());
        //
        //     var e_child = world.Entity().ChildOf(e2);
        //     int32_t count = 0;
        //
        //     e2.children([&](Entity child){
        //         count ++;
        //         Assert.True(child == e_child);
        //     });
        //
        //     Assert.Equal(count, 1);
        // }
        //
        // [Fact]
        // void get_lambda_from_stage() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity().Set(new Position() { X = 10, Y = 20 });
        //
        //     world.readonly_begin();
        //
        //     flecs.world stage = world.get_stage(0);
        //
        //     bool invoked = false;
        //     e.mut(stage).Get([&](const Position& p) {
        //         invoked = true;
        //         Assert.Equal(10, p->X);
        //         Assert.Equal(20, p->Y);
        //     });
        //     test_bool(invoked, true);
        //
        //     world.readonly_end();
        // }
        //
        // [Fact]
        // void default_ctor() {
        //     using World world = World.Create();
        //
        //     Entity e1;
        //     Entity e2 = {};
        //
        //     flecs.entity_view e3;
        //     flecs.entity_view e4 = {};
        //
        //     flecs.id id1;
        //     flecs.id id2 = {};
        //
        //     e1 = world.Entity();
        //     e2 = world.Entity();
        //     e3 = world.Entity();
        //     e4 = world.Entity();
        //
        //     Assert.True(e1 != 0);
        //     Assert.True(e2 != 0);
        //     Assert.True(e3 != 0);
        //     Assert.True(e4 != 0);
        //
        //     Assert.True(id2 == 0);
        // }
        //
        // [Fact]
        // void get_obj_by_template() {
        //     using World world = World.Create();
        //
        //     struct Rel {};
        //
        //     Entity e1 = world.Entity();
        //     Entity o1 = world.Entity();
        //     Entity o2 = world.Entity();
        //
        //     e1.Add<Rel>(o1);
        //     e1.Add<Rel>(o2);
        //
        //     Assert.True(o1 == e1.target<Rel>());
        //     Assert.True(o1 == e1.target<Rel>(0));
        //     Assert.True(o2 == e1.target<Rel>(1));
        // }
        //
        // [Fact]
        // void create_named_twice_deferred() {
        //     using World world = World.Create();
        //
        //     world.defer_begin();
        //
        //     var e1 = world.Entity("e");
        //     var e2 = world.Entity("e");
        //
        //     var f1 = world.Entity("p.f");
        //     var f2 = world.Entity("p.f");
        //
        //     var g1 = world.Scope(world.Entity("q")).Entity("g");
        //     var g2 = world.Scope(world.Entity("q")).Entity("g");
        //
        //     world.defer_end();
        //
        //     Assert.Equal(e1.Path(), "e");
        //     Assert.Equal(f1.Path(), "p.f");
        //     Assert.Equal(g1.Path(), "q.g");
        //
        //     Assert.True(e1 == e2);
        //     Assert.True(f1 == f2);
        //     Assert.True(g1 == g2);
        // }
        //
        // struct PositionInitialized {
        //     PositionInitialized() { }
        //     PositionInitialized(float x_, float y_) : x(x_), y(y_) { }
        //     float x = -1.0;
        //     float y = -1.0;
        // };
        //
        // [Fact]
        // void clone() {
        //     using World world = World.Create();
        //
        //     PositionInitialized v(10, 20);
        //
        //     var src = world.Entity().Add<Tag>().Set<PositionInitialized>(v);
        //     var dst = src.clone(false);
        //     Assert.True(dst.Has<Tag>());
        //     Assert.True(dst.Has<PositionInitialized>());
        //
        //     const PositionInitialized *ptr = dst.GetPtr<PositionInitialized>();
        //     Assert.True(ptr != NULL);
        //     Assert.Equal(ptr->x, -1);
        //     Assert.Equal(ptr->y, -1);
        // }
        //
        // [Fact]
        // void clone_w_value() {
        //     using World world = World.Create();
        //
        //     PositionInitialized v = {10, 20};
        //
        //     var src = world.Entity().Add<Tag>().Set<PositionInitialized>(v);
        //     var dst = src.clone();
        //     Assert.True(dst.Has<Tag>());
        //     Assert.True(dst.Has<PositionInitialized>());
        //
        //     const PositionInitialized *ptr = dst.GetPtr<PositionInitialized>();
        //     Assert.True(ptr != NULL);
        //     Assert.Equal(10, ptr->X);
        //     Assert.Equal(20, ptr->Y);
        // }
        //
        // [Fact]
        // void clone_to_existing() {
        //     using World world = World.Create();
        //
        //     PositionInitialized v = {10, 20};
        //
        //     var src = world.Entity().Add<Tag>().Set<PositionInitialized>(v);
        //     var dst = world.Entity();
        //     var result = src.clone(true, dst);
        //     Assert.True(result == dst);
        //
        //     Assert.True(dst.Has<Tag>());
        //     Assert.True(dst.Has<PositionInitialized>());
        //
        //     const PositionInitialized *ptr = dst.GetPtr<PositionInitialized>();
        //     Assert.True(ptr != NULL);
        //     Assert.Equal(10, ptr->X);
        //     Assert.Equal(20, ptr->Y);
        // }
        //
        // [Fact]
        // void clone_to_existing_overlap() {
        //     install_test_abort();
        //     using World world = World.Create();
        //
        //     PositionInitialized v = {10, 20};
        //
        //     var src = world.Entity().Add<Tag>().Set<PositionInitialized>(v);
        //     var dst = world.Entity().Add<PositionInitialized>();
        //
        //     test_expect_abort();
        //     src.clone(true, dst);
        // }
        //
        // [Fact]
        // void set_doc_name() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity("foo_bar")
        //         .set_doc_name("Foo Bar");
        //
        //     Assert.Equal(e.Name(), "foo_bar");
        //     Assert.Equal(e.doc_name(), "Foo Bar");
        // }
        //
        // [Fact]
        // void set_doc_brief() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity("foo_bar")
        //         .set_doc_brief("Foo Bar");
        //
        //     Assert.Equal(e.Name(), "foo_bar");
        //     Assert.Equal(e.doc_brief(), "Foo Bar");
        // }
        //
        // [Fact]
        // void set_doc_detail() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity("foo_bar")
        //         .set_doc_detail("Foo Bar");
        //
        //     Assert.Equal(e.Name(), "foo_bar");
        //     Assert.Equal(e.doc_detail(), "Foo Bar");
        // }
        //
        // [Fact]
        // void set_doc_link() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity("foo_bar")
        //         .set_doc_link("Foo Bar");
        //
        //     Assert.Equal(e.Name(), "foo_bar");
        //     Assert.Equal(e.doc_link(), "Foo Bar");
        // }
        //
        // [Fact]
        // void entity_w_root_name() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity("foo");
        //     Assert.Equal("Foo", e.Name());
        //     Assert.Equal(e.Path(), "foo");
        // }
        //
        // [Fact]
        // void entity_w_root_name_from_scope() {
        //     using World world = World.Create();
        //
        //     var p = world.Entity("parent");
        //     world.set_scope(p);
        //     var e = world.Entity("foo");
        //     world.set_scope(0);
        //
        //     Assert.Equal("Foo", e.Name());
        //     Assert.Equal(e.Path(), "foo");
        // }
        //
        // struct EntityType { };
        //
        // [Fact]
        // void entity_w_type() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity<EntityType>();
        //
        //     Assert.Equal(e.Name(), "EntityType");
        //     Assert.Equal(e.Path(), "EntityType");
        //     Assert.True(!e.Has<flecs.Component>());
        //
        //     var e_2 = world.Entity<EntityType>();
        //     Assert.True(e == e_2);
        // }
        //
        // struct Turret {
        //     struct Base { };
        // };
        //
        // struct Railgun {
        //     struct Base { };
        //     struct Head { };
        //     struct Beam { };
        // };
        //
        // [Fact]
        // void prefab_hierarchy_w_types() {
        //     using World world = World.Create();
        //
        //     var turret = world.prefab<Turret>();
        //         var turret_base = world.prefab<Turret.Base>();
        //
        //     Assert.True(turret != 0);
        //     Assert.True(turret_base != 0);
        //     Assert.True(turret_base.Has(EcsChildOf, turret));
        //
        //     Assert.Equal(turret.Path(), "Turret");
        //     Assert.Equal(turret_base.Path(), "Turret.Base");
        //
        //     Assert.Equal(turret.symbol(), "Turret");
        //     Assert.Equal(turret_base.symbol(), "Turret.Base");
        //
        //     var railgun = world.prefab<Railgun>().is_a<Turret>();
        //         var railgun_base = railgun.Lookup("Base");
        //         var railgun_head = world.prefab<Railgun.Head>();
        //         var railgun_beam = world.prefab<Railgun.Beam>();
        //
        //     Assert.True(railgun != 0);
        //     Assert.True(railgun_base != 0);
        //     Assert.True(railgun_head != 0);
        //     Assert.True(railgun_beam != 0);
        //     Assert.True(railgun_base.Has(EcsChildOf, railgun));
        //     Assert.True(railgun_head.Has(EcsChildOf, railgun));
        //     Assert.True(railgun_beam.Has(EcsChildOf, railgun));
        //
        //     Assert.Equal(railgun.Path(), "Railgun");
        //     Assert.Equal(railgun_base.Path(), "Railgun.Base");
        //     Assert.Equal(railgun_head.Path(), "Railgun.Head");
        //     Assert.Equal(railgun_beam.Path(), "Railgun.Beam");
        //
        //     Assert.Equal(railgun.symbol(), "Railgun");
        //     Assert.Equal(railgun_head.symbol(), "Railgun.Head");
        //     Assert.Equal(railgun_beam.symbol(), "Railgun.Beam");
        // }
        //
        // struct Base { };
        //
        // [Fact]
        // void prefab_hierarchy_w_root_types() {
        //     using World world = World.Create();
        //
        //     var turret = world.prefab<Turret>();
        //     var turret_base = world.prefab<Base>().child_of<Turret>();
        //
        //     Assert.True(turret != 0);
        //     Assert.True(turret_base != 0);
        //     Assert.True(turret_base.Has(EcsChildOf, turret));
        //
        //     Assert.Equal(turret.Path(), "Turret");
        //     Assert.Equal(turret_base.Path(), "Turret.Base");
        //
        //     Assert.Equal(turret.symbol(), "Turret");
        //     Assert.Equal(turret_base.symbol(), "Base");
        //
        //     var inst = world.Entity().is_a<Turret>();
        //     Assert.True(inst != 0);
        //
        //     var inst_base = inst.Lookup("Base");
        //     Assert.True(inst_base != 0);
        // }
        //
        // [Fact]
        // void prefab_hierarchy_w_child_override() {
        //     using World world = World.Create();
        //
        //     struct Foo {};
        //     struct Bar {};
        //
        //     var t = world.prefab<Turret>();
        //     var tb = world.prefab<Turret.Base>().Add<Foo>();
        //     Assert.True(t != 0);
        //     Assert.True(tb != 0);
        //
        //     var r = world.prefab<Railgun>().is_a<Turret>();
        //     var rb = world.prefab<Railgun.Base>().Add<Bar>();
        //     Assert.True(r != 0);
        //     Assert.True(rb != 0);
        //
        //     var i = world.Entity().is_a<Railgun>();
        //     Assert.True(i != 0);
        //     var ib = i.Lookup("Base");
        //     Assert.True(ib != 0);
        //     Assert.True(ib.Has<Foo>());
        //     Assert.True(ib.Has<Bar>());
        // }
        //
        // [Fact]
        // void entity_w_nested_type() {
        //     using World world = World.Create();
        //
        //     var e = world.Entity<Parent.EntityType>();
        //     var p = world.Entity<Parent>();
        //
        //     Assert.Equal(e.Name(), "EntityType");
        //     Assert.Equal(e.Path(), "Parent.EntityType");
        //     Assert.True(e.Has(EcsChildOf, p));
        //     Assert.True(!e.Has<flecs.Component>());
        //
        //     var e_2 = world.Entity<Parent.EntityType>();
        //     Assert.True(e == e_2);
        // }
        //
        // [Fact]
        // void entity_array() {
        //     struct TagA {};
        //     struct TagB {};
        //
        //     using World world = World.Create();
        //
        //     flecs.to_array({
        //         world.Entity(),
        //         world.Entity(),
        //         world.Entity()
        //     }).each([](Entity e) { e.Add<TagA>().Add<TagB>(); });
        //
        //     Assert.Equal( world.count<TagA>(), 3 );
        //     Assert.Equal( world.count<TagB>(), 3 );
        // }
        //
        // [Fact]
        // void entity_w_type_defer() {
        //     using World world = World.Create();
        //
        //     world.defer_begin();
        //     var e = world.Entity<Tag>();
        //     world.defer_end();
        //
        //     Assert.Equal(e.Name(), "Tag");
        //     Assert.Equal(e.symbol(), "Tag");
        //     Assert.True(world.Id<Tag>() == e);
        // }
        //
        // [Fact]
        // void add_if_true_T() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //
        //     e.add_if<Tag>(true);
        //     Assert.True( e.Has<Tag>());
        // }
        //
        // [Fact]
        // void add_if_false_T() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //
        //     e.add_if<Tag>(false);
        //     Assert.True( !e.Has<Tag>());
        //
        //     e.Add<Tag>();
        //     Assert.True( e.Has<Tag>());
        //     e.add_if<Tag>(false);
        //     Assert.True( !e.Has<Tag>());
        // }
        //
        // [Fact]
        // void add_if_true_id() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     var t = world.Entity();
        //
        //     e.add_if(true, t);
        //     Assert.True( e.Has(t));
        // }
        //
        // [Fact]
        // void add_if_false_id() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     var t = world.Entity();
        //
        //     e.add_if(false, t);
        //     Assert.True( !e.Has(t));
        //     e.Add(t);
        //     Assert.True( e.Has(t));
        //     e.add_if(false, t);
        //     Assert.True( !e.Has(t));
        // }
        //
        // [Fact]
        // void add_if_true_R_O() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //     struct Obj { };
        //
        //     Entity e = world.Entity();
        //
        //     e.add_if<Rel, Obj>(true);
        //     Assert.True( (e.Has<Rel, Obj>()) );
        // }
        //
        // [Fact]
        // void add_if_false_R_O() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //     struct Obj { };
        //
        //     Entity e = world.Entity();
        //
        //     e.add_if<Rel, Obj>(false);
        //     Assert.True( (!e.Has<Rel, Obj>()));
        //     e.Add<Rel, Obj>();
        //     Assert.True( (e.Has<Rel, Obj>()));
        //     e.add_if<Rel, Obj>(false);
        //     Assert.True( (!e.Has<Rel, Obj>()));
        // }
        //
        // [Fact]
        // void add_if_true_R_o() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     Entity e = world.Entity();
        //     var o = world.Entity();
        //
        //     e.add_if<Rel>(true, o);
        //     Assert.True( e.Has<Rel>(o));
        // }
        //
        // [Fact]
        // void add_if_false_R_o() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     Entity e = world.Entity();
        //     var o = world.Entity();
        //
        //     e.add_if<Rel>(false, o);
        //     Assert.True( !e.Has<Rel>(o));
        //     e.Add<Rel>(o);
        //     Assert.True( e.Has<Rel>(o));
        //     e.add_if<Rel>(false, o);
        //     Assert.True( !e.Has<Rel>(o));
        // }
        //
        // [Fact]
        // void add_if_true_r_o() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     var r = world.Entity();
        //     var o = world.Entity();
        //
        //     e.add_if(true, r, o);
        //     Assert.True( e.Has(r, o));
        // }
        //
        // [Fact]
        // void add_if_false_r_o() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     var r = world.Entity();
        //     var o = world.Entity();
        //
        //     e.add_if(false, r, o);
        //     Assert.True( !e.Has(r, o));
        //     e.Add(r, o);
        //     Assert.True( e.Has(r, o));
        //     e.add_if(false, r, o);
        //     Assert.True( !e.Has(r, o));
        // }
        //
        // [Fact]
        // void add_if_exclusive_r_o() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     var r = world.Entity().Add(flecs.Exclusive);
        //     var o_1 = world.Entity();
        //     var o_2 = world.Entity();
        //
        //     e.Add(r, o_1);
        //     Assert.True(e.Has(r, o_1));
        //
        //     e.add_if(true, r, o_2);
        //     Assert.True(!e.Has(r, o_1));
        //     Assert.True(e.Has(r, o_2));
        //
        //     e.add_if(false, r, o_1);
        //     Assert.True(!e.Has(r, o_1));
        //     Assert.True(!e.Has(r, o_2));
        // }
        //
        // [Fact]
        // void add_if_exclusive_R_o() {
        //     using World world = World.Create();
        //
        //     struct First { };
        //
        //     world.Component<First>().Add(flecs.Exclusive);
        //
        //     Entity e = world.Entity();
        //     var o_1 = world.Entity();
        //     var o_2 = world.Entity();
        //
        //     e.Add<First>(o_1);
        //     Assert.True(e.Has<First>(o_1));
        //
        //     e.add_if<First>(true, o_2);
        //     Assert.True(!e.Has<First>(o_1));
        //     Assert.True(e.Has<First>(o_2));
        //
        //     e.add_if<First>(false, o_1);
        //     Assert.True(!e.Has<First>(o_1));
        //     Assert.True(!e.Has<First>(o_2));
        // }
        //
        // [Fact]
        // void add_if_exclusive_R_O() {
        //     using World world = World.Create();
        //
        //     struct R { };
        //     struct O_1 { };
        //     struct O_2 { };
        //
        //     world.Component<R>().Add(flecs.Exclusive);
        //
        //     Entity e = world.Entity();
        //
        //     e.Add<R, O_1>();
        //     Assert.True((e.Has<R, O_1>()));
        //
        //     e.add_if<R, O_2>(true);
        //     Assert.True((!e.Has<R, O_1>()));
        //     Assert.True((e.Has<R, O_2>()));
        //
        //     e.add_if<R, O_1>(false);
        //     Assert.True((!e.Has<R, O_1>()));
        //     Assert.True((!e.Has<R, O_2>()));
        // }
        //
        // [Fact]
        // void add_if_pair_w_0_object() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     var r = world.Entity();
        //     var o_1 = world.Entity();
        //
        //     e.Add(r, o_1);
        //     Assert.True(e.Has(r, o_1));
        //
        //     e.add_if(0, r, 0);
        //     Assert.True(!e.Has(r, o_1));
        //     Assert.True(!e.Has(r, flecs.Wildcard));
        // }
        //
        // [Fact]
        // void children_w_custom_relation() {
        //     using World world = World.Create();
        //
        //     Entity rel = world.Entity();
        //
        //     Entity parent = world.Entity();
        //     Entity child_1 = world.Entity().Add(rel, parent);
        //     Entity child_2 = world.Entity().Add(rel, parent);
        //     world.Entity().ChildOf(parent);
        //
        //     bool child_1_found = false;
        //     bool child_2_found = false;
        //     int32_t count = 0;
        //
        //     parent.children(rel, [&](Entity child) {
        //         if (child == child_1) {
        //             child_1_found = true;
        //         } else if (child == child_2) {
        //             child_2_found = true;
        //         }
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        //     Assert.True(child_1_found == true);
        //     Assert.True(child_2_found == true);
        // }
        //
        // [Fact]
        // void children_w_custom_relation_type() {
        //     using World world = World.Create();
        //
        //     struct Rel { };
        //
        //     Entity parent = world.Entity();
        //     Entity child_1 = world.Entity().Add<Rel>(parent);
        //     Entity child_2 = world.Entity().Add<Rel>(parent);
        //     world.Entity().ChildOf(parent);
        //
        //     bool child_1_found = false;
        //     bool child_2_found = false;
        //     int32_t count = 0;
        //
        //     parent.children<Rel>([&](Entity child) {
        //         if (child == child_1) {
        //             child_1_found = true;
        //         } else if (child == child_2) {
        //             child_2_found = true;
        //         }
        //         count ++;
        //     });
        //
        //     Assert.Equal(count, 2);
        //     Assert.True(child_1_found == true);
        //     Assert.True(child_2_found == true);
        // }
        //
        // [Fact]
        // void children_w_this() {
        //     using World world = World.Create();
        //
        //     int32_t count = 0;
        //     world.Entity(flecs.This).children([&](flecs.entity) {
        //         count ++;
        //     });
        //     Assert.True(count == 0);
        // }
        //
        // [Fact]
        // void children_w_wildcard() {
        //     using World world = World.Create();
        //
        //     int32_t count = 0;
        //     world.Entity(flecs.Wildcard).children([&](flecs.entity) {
        //         count ++;
        //     });
        //     Assert.True(count == 0);
        // }
        //
        // [Fact]
        // void children_w_any() {
        //     using World world = World.Create();
        //
        //     int32_t count = 0;
        //     world.Entity(flecs.Any).children([&](flecs.entity) {
        //         count ++;
        //     });
        //     Assert.True(count == 0);
        // }
        //
        // [Fact]
        // void children_from_root() {
        //     using World world = World.Create();
        //
        //     int32_t count = 0;
        //     world.Entity(0).children([&](Entity e) {
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
        //         count ++;
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

        //
        // [Fact]
        // void to_view() {
        //     using World world = World.Create();
        //
        //     Entity e = world.Entity();
        //     flecs.entity_view ev = e.view();
        //     Assert.True(e == ev);
        // }
        //
        // [Fact]
        // void to_view_from_stage() {
        //     using World world = World.Create();
        //
        //     var stage = world.get_stage(0);
        //
        //     Entity e = stage.Entity();
        //     flecs.entity_view ev = e.view();
        //     Assert.True(e == ev);
        //     Assert.True(e.world() == stage);
        //     Assert.True(ev.world() == world);
        // }
        //
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
        // void emplace_w_observer() {
        //     using World world = World.Create();
        //
        //     world.observer<Position>()
        //         .event(flecs.OnAdd)
        //         .each([](Entity e, Position&) {
        //             e.emplace<Velocity>(1.0f, 2.0f);
        //         });
        //
        //     Entity e = world.Entity()
        //         .emplace<Position>(10.0f, 20.0f);
        //
        //     Assert.True(e.Has<Position>());
        //     Assert.True(e.Has<Velocity>());
        //     Assert.Equal(e.GetPtr<Velocity>()->x, 1);
        //     Assert.Equal(e.GetPtr<Velocity>()->y, 2);
        //     Assert.Equal(e.GetPtr<Position>()->x, 10);
        //     Assert.Equal(e.GetPtr<Position>()->y, 20);
        // }
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
