// using Flecs.NET.Core;
//
// using static Flecs.NET.Bindings.Native;
//
// namespace Flecs.NET.Tests.Cpp
// {
//     public class WorldTests
//     {
//         public WorldTests()
//         {
//             FlecsInternal.Reset();
//         }
//
//         [Fact]
//         private void World_multi_world_empty()
//         {
//             flecs::world *w1 = new flecs::world();
//             delete w1;
//             flecs::world *w2 = new flecs::world();
//             delete w2;
//
//             Ecs.Assert(true);
//         }
//
//         class FooModule {
//         public:
//             FooModule(ref World world) {
//                 world.Module<FooModule>();
//             }
//         };
//
//         typedef struct TestInteropModule {
//             int dummy;
//         } TestInteropModule;
//
//         static
//         void TestInteropModuleImport(ecs_world_t *world) {
//             ECS_MODULE(world, TestInteropModule);
//
//             ECS_COMPONENT(world, Position);
//             ECS_COMPONENT(world, Velocity);
//         }
//
//         namespace test {
//         namespace interop {
//
//         class module : TestInteropModule {
//         public:
//             struct Velocity : ::Velocity { };
//
//             module(ref World world) {
//                 TestInteropModuleImport(world);
//
//                 world.Module<test::interop::module>();
//                 world.Component<Position>("::test::interop::module::Position");
//                 world.Component<Velocity>("::test::interop::module::Velocity");
//             }
//         };
//
//         }
//         }
//
//         namespace ns {
//             struct FooComp {
//                 int value;
//             };
//
//             struct namespace_module {
//                 namespace_module(ref World ecs) {
//                     world.Module<namespace_module>();
//
//                     world.Component<FooComp>();
//
//                     import_count ++;
//
//                     world.Routine<FooComp>()
//                         .Kind(flecs::OnUpdate)
//                         .Each((Entity entity, FooComp &sc) {
//                             namespace_module::system_invoke_count ++;
//                         });
//                 }
//
//                 static int import_count;
//                 static int system_invoke_count;
//             };
//
//             int namespace_module::import_count = 0;
//             int namespace_module::system_invoke_count = 0;
//         }
//
//         struct nested_component_module {
//             struct Foo {
//                 struct Bar { };
//             };
//
//             nested_component_module(ref World ecs) {
//                 world.Component<Foo>();
//                 world.Component<Foo::Bar>();
//             }
//         };
//
//         [Fact]
//         private void World_builtin_components()
//         {
//             using World world = World.Create();
//
//             Ecs.Assert(world.Component<EcsComponent>() == ecs_id(EcsComponent));
//             Ecs.Assert(world.Component<EcsIdentifier>() == ecs_id(EcsIdentifier));
//             Ecs.Assert(world.Component<EcsPoly>() == ecs_id(EcsPoly));
//             Ecs.Assert(world.Component<EcsRateFilter>() == ecs_id(EcsRateFilter));
//             Ecs.Assert(world.Component<EcsTickSource>() == ecs_id(EcsTickSource));
//             Ecs.Assert(Ecs.Name == EcsName);
//             Ecs.Assert(Ecs.Symbol == EcsSymbol);
//             Ecs.Assert(Ecs.System == EcsSystem);
//             Ecs.Assert(Ecs.Observer == EcsObserver);
//             Ecs.Assert(Ecs.Query == EcsQuery);
//         }
//
//         [Fact]
//         private void World_multi_world_component()
//         {
//             flecs::world w1;
//             flecs::world w2;
//
//             var p_1 = w1.Component<Position>();
//             var v_1 = w1.Component<Velocity>();
//             var v_2 = w2.Component<Velocity>();
//             var m_2 = w2.Component<Mass>();
//
//             Ecs.Assert(v_1.id() == v_2.id());
//             Ecs.Assert(p_1.id() != m_2.id());
//             Ecs.Assert(m_2.id() > v_2.id());
//
//             var m_1 = w2.Component<Mass>();
//             Ecs.Assert(m_1.id() == m_2.id());
//         }
//
//         namespace A {
//             struct Comp {
//                 float x;
//                 float y;
//             };
//         }
//
//         [Fact]
//         private void World_multi_world_component_namespace()
//         {
//             flecs::world *w = new flecs::world();
//             var c = w->component<A::Comp>();
//             var id_1 = c.id();
//             delete w;
//
//             w = new flecs::world();
//             c = w->component<A::Comp>();
//             var id_2 = c.id();
//
//             Ecs.Assert(id_1 == id_2);
//
//             delete w;
//         }
//
//         [Fact]
//         private void World_multi_world_module()
//         {
//             flecs::world world1;
// 	        world1.import<ns::namespace_module>();
//
// 	        flecs::world world2;
// 	        world2.import<ns::namespace_module>();
//
//             world1.Entity().add<ns::FooComp>();
//             world2.Entity().add<ns::FooComp>();
//
//             world1.progress();
//             test_int(ns::namespace_module::system_invoke_count, 1);
//
//             world2.progress();
//             test_int(ns::namespace_module::system_invoke_count, 2);
//         }
//
//         [Fact]
//         private void World_multi_world_recycled_component()
//         {
//             Entity c;
//             {
//                 using World world = World.Create();
//                 for (int i = 0; i < FLECS_HI_COMPONENT_ID; i ++) {
//                     ecs_new_low_id(ecs);
//                 }
//
//                 world.Entity().Destruct();
//                 c = world.Component<Position>();
//             }
//             {
//                 using World world = World.Create();
//                 Ecs.Assert((c == world.Component<Position>()));
//             }
//         }
//
//         [Fact]
//         private void World_multi_world_recycled_component_different_generation()
//         {
//             Entity c;
//             {
//                 using World world = World.Create();
//                 for (int i = 0; i < FLECS_HI_COMPONENT_ID; i ++) {
//                     ecs_new_low_id(ecs);
//                 }
//
//                 world.Entity().Destruct();
//                 c = world.Component<Position>();
//             }
//             {
//                 using World world = World.Create();
//                 for (int i = 0; i < FLECS_HI_COMPONENT_ID; i ++) {
//                     ecs_new_low_id(ecs);
//                 }
//
//                 world.Entity().Destruct();
//                 Ecs.Assert((c == world.Component<Position>()));
//             }
//         }
//
//         [Fact]
//         private void World_type_id()
//         {
//             using World world = World.Create();
//
//             var p = world.Component<Position>();
//
//             Ecs.Assert(p.id() == Ecs.TypeId<Position>());
//         }
//
//         [Fact]
//         private void World_different_comp_same_name()
//         {
//             install_test_abort();
//
//             using World world = World.Create();
//
//             test_expect_abort();
//
//             world.Component<Position>("Position");
//             world.Component<Velocity>("Position");
//         }
//
//         [Fact]
//         private void World_reregister_after_reset()
//         {
//             using World world = World.Create();
//
//             var p1 = world.Component<Position>("Position");
//
//             // Simulate different binary
//             flecs::_::type<Position>::reset();
//
//             var p2 = world.Component<Position>("Position");
//
//             Ecs.Assert(p1.id() == p2.id());
//         }
//
//         [Fact]
//         private void World_implicit_reregister_after_reset()
//         {
//             using World world = World.Create();
//
//             world.Entity().add<Position>();
//
//             ulong p_id_1 = Ecs.TypeId<Position>();
//
//             // Simulate different binary
//             flecs::_::type<Position>::reset();
//
//             world.Entity().add<Position>();
//
//             ulong p_id_2 = Ecs.TypeId<Position>();
//
//             Ecs.Assert(p_id_1 == p_id_2);
//         }
//
//         [Fact]
//         private void World_reregister_after_reset_w_namespace()
//         {
//             using World world = World.Create();
//
//             world.Component<ns::FooComp>();
//
//             ulong p_id_1 = Ecs.TypeId<ns::FooComp>();
//
//             // Simulate different binary
//             flecs::_::type<ns::FooComp>::reset();
//
//             world.Component<ns::FooComp>();
//
//             ulong p_id_2 = Ecs.TypeId<ns::FooComp>();
//
//             Ecs.Assert(p_id_1 == p_id_2);
//         }
//
//         [Fact]
//         private void World_reregister_namespace()
//         {
//             using World world = World.Create();
//
//             world.Component<ns::FooComp>();
//
//             ulong p_id_1 = Ecs.TypeId<ns::FooComp>();
//
//             world.Component<ns::FooComp>();
//
//             ulong p_id_2 = Ecs.TypeId<ns::FooComp>();
//
//             Ecs.Assert(p_id_1 == p_id_2);
//         }
//
//         [Fact]
//         private void World_reregister_after_reset_different_name()
//         {
//             install_test_abort();
//
//             using World world = World.Create();
//
//             test_expect_abort();
//
//             world.Component<Position>("Position");
//
//             // Simulate different binary
//             flecs::_::type<Position>::reset();
//
//             world.Component<Position>("Velocity");
//         }
//
//         [Fact]
//         private void World_register_component_w_reset_in_multithreaded()
//         {
//             using World world = World.Create();
//
//             world.set_threads(2);
//
//             Entity pos = world.Component<Position>();
//             Entity e = world.Entity();
//
//             flecs::_::type<Position>::reset();
//
//             world.readonly_begin();
//             e.set<Position>({10, 20});
//             world.readonly_end();
//
//             Ecs.Assert(e.has<Position>());
//             Ecs.Assert(e.has(pos));
//             const Position *p = e.get<Position>();
//             Ecs.Assert(p != nullptr);
//             test_int(p->x, 10);
//             test_int(p->y, 20);
//         }
//
//         struct Module { };
//
//         [Fact]
//         private void World_register_component_w_core_name()
//         {
//             using World world = World.Create();
//
//             Entity c = world.Component<Module>();
//             Ecs.Assert(c != 0);
//             test_str(c.path().c_str(), "::Module");
//         }
//
//         template <typename T>
//         struct Tmp { int32_t v; };
//         struct Test { };
//
//         [Fact]
//         private void World_register_short_template()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<Tmp<Test>>();
//             Ecs.Assert(c != 0);
//             test_str(c.name(), "Tmp<Test>");
//
//             const EcsComponent *ptr = c.get<flecs::Component>();
//             Ecs.Assert(ptr != NULL);
//             test_int(ptr->size, 4);
//             test_int(ptr->alignment, 4);
//         }
//
//         [Fact]
//         private void World_reimport()
//         {
//             using World world = World.Create();
//
//             var m1 = world.import<FooModule>();
//
//             var m2 = world.import<FooModule>();
//
//             Ecs.Assert(m1.id() == m2.id());
//         }
//
//         [Fact]
//         private void World_reimport_module_after_reset()
//         {
//             using World world = World.Create();
//
//             var m1 = world.import<FooModule>();
//
//             // Simulate different binary
//             flecs::_::type<FooModule>::reset();
//
//             var m2 = world.import<FooModule>();
//
//             Ecs.Assert(m1.id() == m2.id());
//         }
//
//         [Fact]
//         private void World_reimport_module_new_world()
//         {
//             Entity e1;
//             {
//                 using World world = World.Create();
//
//                 e1 = world.import<FooModule>();
//             }
//
//             {
//                 using World world = World.Create();
//
//                 var e2 = world.import<FooModule>();
//
//                 Ecs.Assert(e1.id() == e2.id());
//             }
//         }
//
//         [Fact]
//         private void World_reimport_namespaced_module()
//         {
//             using World world = World.Create();
//
//             test_int(ns::namespace_module::import_count, 0);
//
//             // Import first time, should call module constructor.
//             world.import<ns::namespace_module>();
//
//             test_int(ns::namespace_module::import_count, 1);
//
//             // Import second time, should not call constructor.
//             world.import<ns::namespace_module>();
//
//             test_int(ns::namespace_module::import_count, 1);
//         }
//
//
//         [Fact]
//         private void World_c_interop_module()
//         {
//             using World world = World.Create();
//
//             world.import<test::interop::module>();
//
//             var e_pos = world.lookup("test::interop::module::Position");
//             Ecs.Assert(e_pos.id() != 0);
//         }
//
//         [Fact]
//         private void World_c_interop_after_reset()
//         {
//             using World world = World.Create();
//
//             world.import<test::interop::module>();
//
//             var e_pos = world.lookup("test::interop::module::Position");
//             Ecs.Assert(e_pos.id() != 0);
//
//             flecs::_::type<test::interop::module>::reset();
//
//             world.import<test::interop::module>();
//         }
//
//         [Fact]
//         private void World_implicit_register_w_new_world()
//         {
//             {
//                 using World world = World.Create();
//
//                 var e = world.Entity().set<Position>({10, 20});
//                 Ecs.Assert(e.has<Position>());
//                 var *p = e.get<Position>();
//                 Ecs.Assert(p != NULL);
//                 test_int(p->x, 10);
//                 test_int(p->y, 20);
//             }
//
//             {
//                 /* Recreate world, does not reset static state */
//                 using World world = World.Create();
//
//                 var e = world.Entity().set<Position>({10, 20});
//                 Ecs.Assert(e.has<Position>());
//                 var *p = e.get<Position>();
//                 Ecs.Assert(p != NULL);
//                 test_int(p->x, 10);
//                 test_int(p->y, 20);
//             }
//         }
//
//         [Fact]
//         private void World_implicit_register_after_reset_register_w_custom_name()
//         {
//             using World world = World.Create();
//
//             Entity c = world.Component<Position>("MyPosition");
//             test_str(c.name(), "MyPosition");
//
//             flecs::reset(); // Simulate working across boundary
//
//             var e = world.Entity().add<Position>();
//             Ecs.Assert(e.has<Position>());
//             Ecs.Assert(e.has(c));
//         }
//
//         [Fact]
//         private void World_register_after_reset_register_w_custom_name()
//         {
//             using World world = World.Create();
//
//             Entity c1 = world.Component<Position>("MyPosition");
//             test_str(c1.name(), "MyPosition");
//
//             flecs::reset(); // Simulate working across boundary
//
//             Entity c2 = world.Component<Position>();
//             test_str(c2.name(), "MyPosition");
//         }
//
//         [Fact]
//         private void World_register_builtin_after_reset()
//         {
//             using World world = World.Create();
//
//             var c1 = world.Component<flecs::Component>();
//             Ecs.Assert(c1 == ecs_id(EcsComponent));
//
//             flecs::reset(); // Simulate working across boundary
//
//             var c2 = world.Component<flecs::Component>();
//             Ecs.Assert(c2 == ecs_id(EcsComponent));
//             Ecs.Assert(c1 == c2);
//         }
//
//         [Fact]
//         private void World_register_meta_after_reset()
//         {
//             using World world = World.Create();
//
//             var c1 = world.Component<Position>();
//
//             flecs::reset(); // Simulate working across boundary
//
//             var c2 = world.Component<Position>()
//                 .member<float>("x")
//                 .member<float>("y");
//
//             Ecs.Assert(c1 == c2);
//         }
//
//         [Fact]
//         private void World_count()
//         {
//             using World world = World.Create();
//
//             test_int(world.count<Position>(), 0);
//
//             world.Entity().add<Position>();
//             world.Entity().add<Position>();
//             world.Entity().add<Position>();
//             world.Entity().add<Position>().add<Mass>();
//             world.Entity().add<Position>().add<Mass>();
//             world.Entity().add<Position>().add<Velocity>();
//
//             test_int(world.count<Position>(), 6);
//         }
//
//         [Fact]
//         private void World_count_id()
//         {
//             using World world = World.Create();
//
//             var ent = world.Entity();
//
//             test_int(world.count(ent), 0);
//
//             world.Entity().add(ent);
//             world.Entity().add(ent);
//             world.Entity().add(ent);
//             world.Entity().add(ent).add<Mass>();
//             world.Entity().add(ent).add<Mass>();
//             world.Entity().add(ent).add<Velocity>();
//
//             test_int(world.count(ent), 6);
//         }
//
//         [Fact]
//         private void World_count_pair()
//         {
//             using World world = World.Create();
//
//             var parent = world.Entity();
//
//             test_int(world.count(flecs::ChildOf, parent), 0);
//
//             world.Entity().add(flecs::ChildOf, parent);
//             world.Entity().add(flecs::ChildOf, parent);
//             world.Entity().add(flecs::ChildOf, parent);
//             world.Entity().add(flecs::ChildOf, parent).add<Mass>();
//             world.Entity().add(flecs::ChildOf, parent).add<Mass>();
//             world.Entity().add(flecs::ChildOf, parent).add<Velocity>();
//
//             test_int(world.count(flecs::ChildOf, parent), 6);
//         }
//
//         [Fact]
//         private void World_count_pair_type_id()
//         {
//             using World world = World.Create();
//
//             struct Rel { };
//
//             var parent = world.Entity();
//
//             test_int(world.count<Rel>(parent), 0);
//
//             world.Entity().add<Rel>(parent);
//             world.Entity().add<Rel>(parent);
//             world.Entity().add<Rel>(parent);
//             world.Entity().add<Rel>(parent).add<Mass>();
//             world.Entity().add<Rel>(parent).add<Mass>();
//             world.Entity().add<Rel>(parent).add<Velocity>();
//
//             test_int(world.count<Rel>(parent), 6);
//         }
//
//         [Fact]
//         private void World_count_pair_id()
//         {
//             using World world = World.Create();
//
//             struct Rel { };
//
//             var rel = world.Entity();
//             var parent = world.Entity();
//
//             test_int(world.count(rel, parent), 0);
//
//             world.Entity().add(rel, parent);
//             world.Entity().add(rel, parent);
//             world.Entity().add(rel, parent);
//             world.Entity().add(rel, parent).add<Mass>();
//             world.Entity().add(rel, parent).add<Mass>();
//             world.Entity().add(rel, parent).add<Velocity>();
//
//             test_int(world.count(rel, parent), 6);
//         }
//
//         [Fact]
//         private void World_staged_count()
//         {
//             using World world = World.Create();
//
//             flecs::world stage = world.get_stage(0);
//
//             world.readonly_begin();
//
//             test_int(stage.count<Position>(), 0);
//
//             world.readonly_end();
//
//             world.readonly_begin();
//
//             stage.Entity().add<Position>();
//             stage.Entity().add<Position>();
//             stage.Entity().add<Position>();
//             stage.Entity().add<Position>().add<Mass>();
//             stage.Entity().add<Position>().add<Mass>();
//             stage.Entity().add<Position>().add<Velocity>();
//
//             test_int(stage.count<Position>(), 0);
//
//             world.readonly_end();
//
//             test_int(stage.count<Position>(), 6);
//         }
//
//         [Fact]
//         private void World_async_stage_add()
//         {
//             using World world = World.Create();
//
//             world.Component<Position>();
//
//             var e = world.Entity();
//
//             flecs::world async = world.async_stage();
//             e.mut(async).add<Position>();
//             Ecs.Assert(!e.has<Position>());
//             async.merge();
//             Ecs.Assert(e.has<Position>());
//         }
//
//         [Fact]
//         private void World_with_tag()
//         {
//             using World world = World.Create();
//
//             var Tag = world.Entity();
//
//             world.with(Tag, [&]{
//                 var e1 = world.Entity(); e1.set<Self>({e1});
//                 var e2 = world.Entity(); e2.set<Self>({e2});
//                 var e3 = world.Entity(); e3.set<Self>({e3});
//             });
//
//             // Ensures that while Self is (implicitly) registered within the with, it
//             // does not get any contents from the with.
//             var self = world.Component<Self>();
//             Ecs.Assert(!self.has(Tag));
//
//             var q = world.query_builder<>().with(Tag).build();
//
//             int32_t count = 0;
//
//             q.Each((Entity e) {
//                 Ecs.Assert(e.has(Tag));
//
//                 test_bool(e.get((const Self& s) {
//                     Ecs.Assert(s.value == e);
//                 }), true);
//
//                 count ++;
//             });
//
//             count ++;
//         }
//
//         [Fact]
//         private void World_with_tag_type()
//         {
//             using World world = World.Create();
//
//             struct Tag { };
//
//             world.with<Tag>([&]{
//                 var e1 = world.Entity(); e1.set<Self>({e1});
//                 var e2 = world.Entity(); e2.set<Self>({e2});
//                 var e3 = world.Entity(); e3.set<Self>({e3});
//             });
//
//             // Ensures that while Self is (implicitly) registered within the with, it
//             // does not get any contents from the with.
//             var self = world.Component<Self>();
//             Ecs.Assert(!self.has<Tag>());
//
//             var q = world.query_builder<>().with<Tag>().build();
//
//             int32_t count = 0;
//
//             q.Each((Entity e) {
//                 Ecs.Assert(e.has<Tag>());
//
//                 test_bool(e.get((const Self& s) {
//                     Ecs.Assert(s.value == e);
//                 }), true);
//
//                 count ++;
//             });
//
//             count ++;
//         }
//
//         [Fact]
//         private void World_with_relation()
//         {
//             using World world = World.Create();
//
//             var Likes = world.Entity();
//             var Bob = world.Entity();
//
//             world.with(Likes, Bob, [&]{
//                 var e1 = world.Entity(); e1.set<Self>({e1});
//                 var e2 = world.Entity(); e2.set<Self>({e2});
//                 var e3 = world.Entity(); e3.set<Self>({e3});
//             });
//
//             // Ensures that while Self is (implicitly) registered within the with, it
//             // does not get any contents from the with.
//             var self = world.Component<Self>();
//             Ecs.Assert(!self.has(Likes, Bob));
//
//             var q = world.query_builder<>().with(Likes, Bob).build();
//
//             int32_t count = 0;
//
//             q.Each((Entity e) {
//                 Ecs.Assert(e.has(Likes, Bob));
//
//                 test_bool(e.get((const Self& s) {
//                     Ecs.Assert(s.value == e);
//                 }), true);
//
//                 count ++;
//             });
//
//             count ++;
//         }
//
//         [Fact]
//         private void World_with_relation_type()
//         {
//             using World world = World.Create();
//
//             struct Likes { };
//             var Bob = world.Entity();
//
//             world.with<Likes>(Bob, [&]{
//                 var e1 = world.Entity(); e1.set<Self>({e1});
//                 var e2 = world.Entity(); e2.set<Self>({e2});
//                 var e3 = world.Entity(); e3.set<Self>({e3});
//             });
//
//             // Ensures that while Self is (implicitly) registered within the with, it
//             // does not get any contents from the with.
//             var self = world.Component<Self>();
//             Ecs.Assert(!self.has<Likes>(Bob));
//
//             var q = world.query_builder<>().with<Likes>(Bob).build();
//
//             int32_t count = 0;
//
//             q.Each((Entity e) {
//                 Ecs.Assert(e.has<Likes>(Bob));
//
//                 test_bool(e.get((const Self& s) {
//                     Ecs.Assert(s.value == e);
//                 }), true);
//
//                 count ++;
//             });
//
//             count ++;
//         }
//
//         [Fact]
//         private void World_with_relation_object_type()
//         {
//             using World world = World.Create();
//
//             struct Likes { };
//             struct Bob { };
//
//             world.with<Likes, Bob>([&]{
//                 var e1 = world.Entity(); e1.set<Self>({e1});
//                 var e2 = world.Entity(); e2.set<Self>({e2});
//                 var e3 = world.Entity(); e3.set<Self>({e3});
//             });
//
//             // Ensures that while Self is (implicitly) registered within the with, it
//             // does not get any contents from the with.
//             var self = world.Component<Self>();
//             Ecs.Assert(!(self.has<Likes, Bob>()));
//
//             var q = world.query_builder<>().with<Likes, Bob>().build();
//
//             int32_t count = 0;
//
//             q.Each((Entity e) {
//                 Ecs.Assert((e.has<Likes, Bob>()));
//
//                 test_bool(e.get((const Self& s) {
//                     Ecs.Assert(s.value == e);
//                 }), true);
//
//                 count ++;
//             });
//
//             count ++;
//         }
//
//         [Fact]
//         private void World_with_scope()
//         {
//             using World world = World.Create();
//
//             var parent = world.Entity("P");
//
//             world.scope(parent, [&]{
//                 var e1 = world.Entity("C1"); e1.set<Self>({e1});
//                 var e2 = world.Entity("C2"); e2.set<Self>({e2});
//                 var e3 = world.Entity("C3"); e3.set<Self>({e3});
//
//                 // Ensure relative lookups work
//                 Ecs.Assert(world.lookup("C1") == e1);
//                 Ecs.Assert(world.lookup("C2") == e2);
//                 Ecs.Assert(world.lookup("C3") == e3);
//
//                 Ecs.Assert(parent.lookup("C1") == e1);
//                 Ecs.Assert(parent.lookup("C2") == e2);
//                 Ecs.Assert(parent.lookup("C3") == e3);
//
//                 Ecs.Assert(world.lookup("::P::C1") == e1);
//                 Ecs.Assert(world.lookup("::P::C2") == e2);
//                 Ecs.Assert(world.lookup("::P::C3") == e3);
//             });
//
//             Ecs.Assert(parent.lookup("C1") != 0);
//             Ecs.Assert(parent.lookup("C2") != 0);
//             Ecs.Assert(parent.lookup("C3") != 0);
//
//             Ecs.Assert(world.lookup("P::C1") == parent.lookup("C1"));
//             Ecs.Assert(world.lookup("P::C2") == parent.lookup("C2"));
//             Ecs.Assert(world.lookup("P::C3") == parent.lookup("C3"));
//
//             // Ensures that while Self is (implicitly) registered within the with, it
//             // does not become a child of the parent.
//             var self = world.Component<Self>();
//             Ecs.Assert(!self.has(flecs::ChildOf, parent));
//
//             int count = 0;
//             var q = world.query_builder<>().with(flecs::ChildOf, parent).build();
//
//             q.Each((Entity e) {
//                 Ecs.Assert(e.has(flecs::ChildOf, parent));
//
//                 test_bool(e.get((const Self& s){
//                     Ecs.Assert(s.value == e);
//                 }), true);
//
//                 count ++;
//             });
//
//             test_int(count, 3);
//         }
//
//         struct ParentScope { };
//
//         [Fact]
//         private void World_with_scope_type()
//         {
//             using World world = World.Create();
//
//             world.scope<ParentScope>([&]{
//                 world.Entity("Child");
//             });
//
//             var parent = world.lookup("ParentScope");
//             Ecs.Assert(parent != 0);
//
//             var child = world.lookup("ParentScope::Child");
//             Ecs.Assert(child != 0);
//             Ecs.Assert(child == parent.lookup("Child"));
//         }
//
//         [Fact]
//         private void World_with_scope_type_staged()
//         {
//             using World world = World.Create();
//
//             Entity e;
//             flecs::world stage = world.get_stage(0);
//
//             world.readonly_begin();
//             stage.scope<ParentScope>([&]{
//                 e = stage.Entity("Child");
//             });
//             world.readonly_end();
//
//             Ecs.Assert( e.has(flecs::ChildOf, world.id<ParentScope>()) );
//
//             var parent = world.lookup("ParentScope");
//             Ecs.Assert(parent != 0);
//
//             var child = world.lookup("ParentScope::Child");
//             Ecs.Assert(child != 0);
//             Ecs.Assert(child == parent.lookup("Child"));
//         }
//
//         [Fact]
//         private void World_with_scope_no_lambda()
//         {
//             using World world = World.Create();
//
//             var parent = world.Entity("Parent");
//             var child = world.scope(parent).Entity("Child");
//
//             Ecs.Assert(child.has(flecs::ChildOf, parent));
//             Ecs.Assert(world.get_scope() == 0);
//         }
//
//         [Fact]
//         private void World_with_scope_type_no_lambda()
//         {
//             using World world = World.Create();
//
//             var child = world.scope<ParentScope>().Entity("Child");
//
//             Ecs.Assert(child.has(flecs::ChildOf, world.id<ParentScope>()));
//             Ecs.Assert(world.get_scope() == 0);
//         }
//
//         [Fact]
//         private void World_with_tag_nested()
//         {
//             using World world = World.Create();
//
//             var Tier1 = world.Entity();
//
//             world.with(Tier1, [&]{
//                 world.Entity("Tier2").with([&]{
//                     world.Entity("Tier3");
//                 });
//             });
//
//             var Tier2 = world.lookup("Tier2");
//             Ecs.Assert(Tier2 != 0);
//
//             var Tier3 = world.lookup("Tier3");
//             Ecs.Assert(Tier3 != 0);
//
//             Ecs.Assert(Tier2.has(Tier1));
//             Ecs.Assert(Tier3.has(Tier2));
//         }
//
//         [Fact]
//         private void World_with_scope_nested()
//         {
//             using World world = World.Create();
//
//             var parent = world.Entity("P");
//
//             world.scope(parent, [&]{
//                 var child = world.Entity("C").scope([&]{
//                     var gchild = world.Entity("GC");
//                     Ecs.Assert(gchild == world.lookup("GC"));
//                     Ecs.Assert(gchild == world.lookup("::P::C::GC"));
//                 });
//
//                 // Ensure relative lookups work
//                 Ecs.Assert(world.lookup("C") == child);
//                 Ecs.Assert(world.lookup("::P::C") == child);
//                 Ecs.Assert(world.lookup("::P::C::GC") != 0);
//             });
//
//             Ecs.Assert(0 == world.lookup("C"));
//             Ecs.Assert(0 == world.lookup("GC"));
//             Ecs.Assert(0 == world.lookup("C::GC"));
//
//             var child = world.lookup("P::C");
//             Ecs.Assert(0 != child);
//             Ecs.Assert(child.has(flecs::ChildOf, parent));
//
//             var gchild = world.lookup("P::C::GC");
//             Ecs.Assert(0 != gchild);
//             Ecs.Assert(gchild.has(flecs::ChildOf, child));
//         }
//
//         [Fact]
//         private void World_recursive_lookup()
//         {
//             using World world = World.Create();
//
//             var A = world.Entity("A");
//             var B = world.Entity("B");
//
//             var P = world.Entity("P");
//             P.scope([&]{
//                 var CA = world.Entity("A");
//                 Ecs.Assert(CA != A);
//
//                 Ecs.Assert(CA == world.lookup("A"));
//                 Ecs.Assert(CA == world.lookup("P::A"));
//                 Ecs.Assert(CA == world.lookup("::P::A"));
//                 Ecs.Assert(A == world.lookup("::A"));
//
//                 Ecs.Assert(B == world.lookup("B"));
//                 Ecs.Assert(B == world.lookup("::B"));
//             });
//         }
//
//         [Fact]
//         private void World_type_w_tag_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<Tag>();
//             Ecs.Assert(c != Entity());
//             test_str(c.path().c_str(), "::Tag");
//             Ecs.Assert(c != flecs::PairIsTag);
//         }
//
//         [Fact]
//         private void World_entity_w_tag_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Entity("Tag");
//             Ecs.Assert(c != Entity());
//             test_str(c.path().c_str(), "::Tag");
//             Ecs.Assert(c != flecs::PairIsTag);
//         }
//
//         template <typename T>
//         struct TemplateType { };
//
//         [Fact]
//         private void World_template_component_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<TemplateType<Position>>();
//             test_str(c.name().c_str(), "TemplateType<Position>");
//             test_str(c.path().c_str(), "::TemplateType<Position>");
//         }
//
//         namespace ns {
//         template <typename T>
//         struct TemplateType { };
//         struct foo { };
//         }
//
//         [Fact]
//         private void World_template_component_w_namespace_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<ns::TemplateType<Position>>();
//             test_str(c.name().c_str(), "TemplateType<Position>");
//             test_str(c.path().c_str(), "::ns::TemplateType<Position>");
//         }
//
//         [Fact]
//         private void World_template_component_w_namespace_name_and_namespaced_arg()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<ns::TemplateType<ns::foo>>();
//             test_str(c.name().c_str(), "TemplateType<ns::foo>");
//             test_str(c.path().c_str(), "::ns::TemplateType<ns::foo>");
//         }
//
//         namespace foo {
//         template <typename T>
//         struct foo { };
//         struct bar { };
//         }
//
//         [Fact]
//         private void World_template_component_w_same_namespace_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<foo::foo<Position>>();
//             test_str(c.name().c_str(), "foo<Position>");
//             test_str(c.path().c_str(), "::foo::foo<Position>");
//         }
//
//         [Fact]
//         private void World_template_component_w_same_namespace_name_and_namespaced_arg()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<foo::foo<foo::bar>>();
//             test_str(c.name().c_str(), "foo<foo::bar>");
//             test_str(c.path().c_str(), "::foo::foo<foo::bar>");
//         }
//
//         struct module_w_template_component {
//             struct Foo { };
//             struct Bar { };
//
//             template <typename T, typename U>
//             struct TypeWithArgs { };
//
//             module_w_template_component(flecs::world &world) {
//                 world.Module<module_w_template_component>();
//                 world.Component<TypeWithArgs<Foo, Bar>>();
//             };
//         };
//
//
//         [Fact]
//         private void World_template_component_from_module_2_args()
//         {
//             using World world = World.Create();
//
//             var m = world.import<module_w_template_component>();
//             Ecs.Assert(m == world.lookup("module_w_template_component"));
//
//             var tid = world.id<module_w_template_component::TypeWithArgs<
//                 module_w_template_component::Foo,
//                 module_w_template_component::Bar>>();
//             Ecs.Assert(tid != 0);
//
//             var mid = m.lookup("TypeWithArgs<module_w_template_component::Foo, module_w_template_component::Bar>");
//             if (mid == 0) {
//                 mid = m.lookup("TypeWithArgs<module_w_template_component::Foo,module_w_template_component::Bar>");
//             }
//             Ecs.Assert(mid != 0);
//             Ecs.Assert(tid == mid);
//         }
//
//         [Fact]
//         private void World_entity_as_tag()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity<Tag>();
//             Ecs.Assert(e.id() != 0);
//
//             var t = world.Component<Tag>();
//             Ecs.Assert(t.id() != 0);
//             Ecs.Assert(e == t);
//
//             var e2 = world.Entity()
//                 .add<Tag>();
//
//             test_bool(e2.has<Tag>(), true);
//             test_bool(e2.has(e), true);
//
//             test_str(e.name(), "Tag");
//         }
//
//         [Fact]
//         private void World_entity_w_name_as_tag()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity<Tag>("Foo");
//             Ecs.Assert(e.id() != 0);
//
//             var t = world.Component<Tag>();
//             Ecs.Assert(t.id() != 0);
//             Ecs.Assert(e == t);
//
//             var e2 = world.Entity()
//                 .add<Tag>();
//
//             test_bool(e2.has<Tag>(), true);
//             test_bool(e2.has(e), true);
//
//             test_str(e.name(), "Foo");
//         }
//
//         [Fact]
//         private void World_entity_as_component()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity<Position>();
//             Ecs.Assert(e.id() != 0);
//
//             var t = world.Component<Position>();
//             Ecs.Assert(t.id() != 0);
//             Ecs.Assert(e == t);
//
//             var e2 = world.Entity()
//                 .set<Position>({10, 20});
//
//             test_bool(e2.has<Position>(), true);
//             test_bool(e2.has(e), true);
//
//             test_str(e.name(), "Position");
//         }
//
//         [Fact]
//         private void World_entity_w_name_as_component()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity<Position>("Foo");
//             Ecs.Assert(e.id() != 0);
//
//             var t = world.Component<Position>();
//             Ecs.Assert(t.id() != 0);
//             Ecs.Assert(e == t);
//
//             var e2 = world.Entity()
//                 .set<Position>({10, 20});
//
//             test_bool(e2.has<Position>(), true);
//             test_bool(e2.has(e), true);
//
//             test_str(e.name(), "Foo");
//         }
//
//         [Fact]
//         private void World_entity_as_component_2_worlds()
//         {
//             flecs::world ecs_1;
//             var e_1 = ecs_1.Entity<Position>();
//             Ecs.Assert(e_1.id() != 0);
//
//             flecs::world ecs_2;
//             var e_2 = ecs_2.Entity<Position>();
//             Ecs.Assert(e_2.id() != 0);
//
//             Ecs.Assert(e_1 == e_2);
//             Ecs.Assert(e_1 == ecs_1.Component<Position>());
//             Ecs.Assert(e_2 == ecs_2.Component<Position>());
//         }
//
//         struct Parent {
//             struct Child { };
//         };
//
//         [Fact]
//         private void World_entity_as_namespaced_component_2_worlds()
//         {
//             flecs::world ecs_1;
//             var e_1 = ecs_1.Entity<Parent>();
//             Ecs.Assert(e_1.id() != 0);
//
//             var e_1_1 = ecs_1.Entity<Parent::Child>();
//             Ecs.Assert(e_1_1.id() != 0);
//
//             flecs::world ecs_2;
//             var e_2 = ecs_2.Entity<Parent>();
//             Ecs.Assert(e_2.id() != 0);
//
//             var e_2_1 = ecs_2.Entity<Parent::Child>();
//             Ecs.Assert(e_2_1.id() != 0);
//
//             Ecs.Assert(e_1 == e_2);
//             Ecs.Assert(e_1 == ecs_1.Component<Parent>());
//             Ecs.Assert(e_2 == ecs_2.Component<Parent>());
//
//             Ecs.Assert(e_1_1 == e_2_1);
//             Ecs.Assert(e_1_1 == ecs_1.Component<Parent::Child>());
//             Ecs.Assert(e_2_1 == ecs_2.Component<Parent::Child>());
//         }
//
//         [Fact]
//         private void World_entity_as_component_2_worlds_implicit_namespaced()
//         {
//             flecs::world ecs_1;
//             var e_1 = ecs_1.Entity<Parent>();
//             Ecs.Assert(e_1.id() != 0);
//
//             ecs_1.Entity().add<Parent::Child>();
//
//             flecs::world ecs_2;
//             var e_2 = ecs_2.Entity<Parent>();
//             Ecs.Assert(e_2.id() != 0);
//
//             ecs_2.Entity().add<Parent::Child>();
//
//             Ecs.Assert(e_1 == e_2);
//             Ecs.Assert(e_1 == ecs_1.Component<Parent>());
//             Ecs.Assert(e_2 == ecs_2.Component<Parent>());
//
//             Ecs.Assert(ecs_1.Component<Parent::Child>() ==
//                 ecs_2.Component<Parent::Child>());
//         }
//
//         struct PositionDerived : Position {
//             PositionDerived() { }
//             PositionDerived(float x, float y) : Position{x, y} { }
//         };
//
//         [Fact]
//         private void World_delete_with_id()
//         {
//             using World world = World.Create();
//
//             flecs::id tag = world.Entity();
//             var e_1 = world.Entity().add(tag);
//             var e_2 = world.Entity().add(tag);
//             var e_3 = world.Entity().add(tag);
//
//             world.delete_with(tag);
//
//             Ecs.Assert(!e_1.is_alive());
//             Ecs.Assert(!e_2.is_alive());
//             Ecs.Assert(!e_3.is_alive());
//         }
//
//         [Fact]
//         private void World_delete_with_type()
//         {
//             using World world = World.Create();
//
//             var e_1 = world.Entity().add<Tag>();
//             var e_2 = world.Entity().add<Tag>();
//             var e_3 = world.Entity().add<Tag>();
//
//             world.delete_with<Tag>();
//
//             Ecs.Assert(!e_1.is_alive());
//             Ecs.Assert(!e_2.is_alive());
//             Ecs.Assert(!e_3.is_alive());
//         }
//
//         [Fact]
//         private void World_delete_with_pair()
//         {
//             using World world = World.Create();
//
//             flecs::id rel = world.Entity();
//             flecs::id obj = world.Entity();
//             var e_1 = world.Entity().add(rel, obj);
//             var e_2 = world.Entity().add(rel, obj);
//             var e_3 = world.Entity().add(rel, obj);
//
//             world.delete_with(rel, obj);
//
//             Ecs.Assert(!e_1.is_alive());
//             Ecs.Assert(!e_2.is_alive());
//             Ecs.Assert(!e_3.is_alive());
//         }
//
//         [Fact]
//         private void World_delete_with_pair_type()
//         {
//             using World world = World.Create();
//
//             struct Rel { };
//             struct Obj { };
//
//             var e_1 = world.Entity().add<Rel, Obj>();
//             var e_2 = world.Entity().add<Rel, Obj>();
//             var e_3 = world.Entity().add<Rel, Obj>();
//
//             world.delete_with<Rel, Obj>();
//
//             Ecs.Assert(!e_1.is_alive());
//             Ecs.Assert(!e_2.is_alive());
//             Ecs.Assert(!e_3.is_alive());
//         }
//
//         [Fact]
//         private void World_delete_with_implicit()
//         {
//             using World world = World.Create();
//
//             world.delete_with<Tag>();
//
//             Ecs.Assert(true);
//         }
//
//         [Fact]
//         private void World_delete_with_pair_implicit()
//         {
//             using World world = World.Create();
//
//             struct Rel { };
//             struct Obj { };
//
//             world.delete_with<Rel, Obj>();
//
//             Ecs.Assert(true);
//         }
//
//         [Fact]
//         private void World_remove_all_id()
//         {
//             using World world = World.Create();
//
//             flecs::id tag_a = world.Entity();
//             flecs::id tag_b = world.Entity();
//             var e_1 = world.Entity().add(tag_a);
//             var e_2 = world.Entity().add(tag_a);
//             var e_3 = world.Entity().add(tag_a).add(tag_b);
//
//             world.remove_all(tag_a);
//
//             Ecs.Assert(e_1.is_alive());
//             Ecs.Assert(e_2.is_alive());
//             Ecs.Assert(e_3.is_alive());
//
//             Ecs.Assert(!e_1.has(tag_a));
//             Ecs.Assert(!e_2.has(tag_a));
//             Ecs.Assert(!e_3.has(tag_a));
//
//             Ecs.Assert(e_3.has(tag_b));
//         }
//
//         [Fact]
//         private void World_remove_all_type()
//         {
//             using World world = World.Create();
//
//             var e_1 = world.Entity().add<Position>();
//             var e_2 = world.Entity().add<Position>();
//             var e_3 = world.Entity().add<Position>().add<Velocity>();
//
//             world.remove_all<Position>();
//
//             Ecs.Assert(e_1.is_alive());
//             Ecs.Assert(e_2.is_alive());
//             Ecs.Assert(e_3.is_alive());
//
//             Ecs.Assert(!e_1.has<Position>());
//             Ecs.Assert(!e_2.has<Position>());
//             Ecs.Assert(!e_3.has<Position>());
//
//             Ecs.Assert(e_3.has<Velocity>());
//         }
//
//         [Fact]
//         private void World_remove_all_pair()
//         {
//             using World world = World.Create();
//
//             flecs::id rel = world.Entity();
//             flecs::id obj_a = world.Entity();
//             flecs::id obj_b = world.Entity();
//             var e_1 = world.Entity().add(rel, obj_a);
//             var e_2 = world.Entity().add(rel, obj_a);
//             var e_3 = world.Entity().add(rel, obj_a).add(rel, obj_b);
//
//             world.remove_all(rel, obj_a);
//
//             Ecs.Assert(e_1.is_alive());
//             Ecs.Assert(e_2.is_alive());
//             Ecs.Assert(e_3.is_alive());
//
//             Ecs.Assert(!e_1.has(rel, obj_a));
//             Ecs.Assert(!e_2.has(rel, obj_a));
//             Ecs.Assert(!e_3.has(rel, obj_a));
//
//             Ecs.Assert(e_3.has(rel, obj_b));
//         }
//
//         [Fact]
//         private void World_remove_all_pair_type()
//         {
//             using World world = World.Create();
//
//             struct Rel { };
//             struct ObjA { };
//             struct ObjB { };
//
//             var e_1 = world.Entity().add<Rel, ObjA>();
//             var e_2 = world.Entity().add<Rel, ObjA>();
//             var e_3 = world.Entity().add<Rel, ObjA>().add<Rel, ObjB>();
//
//             world.remove_all<Rel, ObjA>();
//
//             Ecs.Assert(e_1.is_alive());
//             Ecs.Assert(e_2.is_alive());
//             Ecs.Assert(e_3.is_alive());
//
//             Ecs.Assert((!e_1.has<Rel, ObjA>()));
//             Ecs.Assert((!e_2.has<Rel, ObjA>()));
//             Ecs.Assert((!e_3.has<Rel, ObjA>()));
//
//             Ecs.Assert((!e_1.has<Rel, ObjB>()));
//             Ecs.Assert((!e_2.has<Rel, ObjB>()));
//             Ecs.Assert((e_3.has<Rel, ObjB>()));
//         }
//
//         [Fact]
//         private void World_remove_all_implicit()
//         {
//             using World world = World.Create();
//
//             world.remove_all<Tag>();
//
//             Ecs.Assert(true);
//         }
//
//         [Fact]
//         private void World_remove_all_pair_implicit()
//         {
//             using World world = World.Create();
//
//             struct Rel { };
//             struct Obj { };
//
//             world.remove_all<Rel, Obj>();
//
//             Ecs.Assert(true);
//         }
//
//         [Fact]
//         private void World_get_scope()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity("scope");
//
//             world.set_scope(e);
//
//             var s = world.get_scope();
//             Ecs.Assert(s == e);
//             test_str(s.name(), "scope");
//         }
//
//         [Fact]
//         private void World_get_scope_type()
//         {
//             using World world = World.Create();
//
//             world.set_scope<ParentScope>();
//
//             var s = world.get_scope();
//             Ecs.Assert(s == world.id<ParentScope>());
//             test_str(s.name(), "ParentScope");
//         }
//
//         struct Outer
//         {
//             struct Inner { };
//         };
//
//         [Fact]
//         private void World_register_namespace_after_component()
//         {
//             using World world = World.Create();
//             var inn = world.Component<Outer::Inner>();
//             var out = world.Component<Outer>();
//
//             test_str(inn.path().c_str(), "::Outer::Inner");
//             test_str(out.path().c_str(), "::Outer");
//
//             const char *inn_sym = ecs_get_symbol(ecs, inn);
//             const char *out_sym = ecs_get_symbol(ecs, out);
//
//             test_str(inn_sym, "Outer.Inner");
//             test_str(out_sym, "Outer");
//         }
//
//         [Fact]
//         private void World_is_alive()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity();
//
//             test_bool(world.is_alive(e), true);
//             test_bool(world.is_alive(1000), false);
//
//             e.Destruct();
//
//             test_bool(world.is_alive(e), false);
//         }
//
//         [Fact]
//         private void World_is_valid()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity();
//
//             test_bool(world.is_valid(e), true);
//             test_bool(world.is_valid(1000), true);
//             test_bool(world.is_valid(0), false);
//
//             e.Destruct();
//
//             test_bool(world.is_valid(e), false);
//         }
//
//         [Fact]
//         private void World_exists()
//         {
//             using World world = World.Create();
//
//             var e = world.Entity();
//
//             test_bool(world.exists(e), true);
//             test_bool(world.exists(1000), false);
//         }
//
//         [Fact]
//         private void World_get_alive()
//         {
//             using World world = World.Create();
//
//             var e_1 = world.Entity();
//             var e_no_gen = flecs::strip_generation(e_1);
//             Ecs.Assert(e_1 == e_no_gen);
//             e_1.Destruct();
//
//             var e_2 = world.Entity();
//             Ecs.Assert(e_1 != e_2);
//             Ecs.Assert(e_no_gen == flecs::strip_generation(e_2));
//
//             Ecs.Assert(world.get_alive(e_no_gen) == e_2);
//         }
//
//         [Fact]
//         private void World_make_alive()
//         {
//             using World world = World.Create();
//
//             var e_1 = world.Entity();
//             e_1.Destruct();
//             Ecs.Assert(!e_1.is_alive());
//
//             var e_2 = world.Entity();
//             Ecs.Assert(e_1 != e_2);
//             Ecs.Assert(e_1 == flecs::strip_generation(e_2));
//             e_2.Destruct();
//             Ecs.Assert(!e_2.is_alive());
//
//             var e_3 = world.make_alive(e_2);
//             Ecs.Assert(e_2 == e_3);
//             Ecs.Assert(e_3.is_alive());
//         }
//
//         [Fact]
//         private void World_reset_all()
//         {
//             Entity pos, vel;
//
//             {
//                 using World world = World.Create();
//                 pos = world.Component<Position>();
//                 vel = world.Component<Velocity>();
//             }
//
//             Ecs.Assert(Ecs.TypeId<Position>() == pos);
//             Ecs.Assert(Ecs.TypeId<Velocity>() == vel);
//
//             flecs::reset();
//
//             Ecs.Assert(Ecs.TypeId<Position>() == 0);
//
//             /* Register components in opposite order, should result in different ids */
//             {
//                 using World world = World.Create();
//                 Ecs.Assert(world.Component<Position>() != 0);
//                 Ecs.Assert(world.Component<Velocity>() != 0);
//             }
//         }
//
//         [Fact]
//         private void World_get_tick()
//         {
//             using World world = World.Create();
//
//             test_int(world.get_info()->frame_count_total, 0);
//
//             world.progress();
//
//             test_int(world.get_info()->frame_count_total, 1);
//
//             world.progress();
//
//             test_int(world.get_info()->frame_count_total, 2);
//         }
//
//         struct Scope { };
//
//         struct FromScope { };
//
//         namespace Nested {
//             struct FromScope { };
//         }
//
//         [Fact]
//         private void World_register_from_scope()
//         {
//             using World world = World.Create();
//
//             world.set_scope<Scope>();
//             var c = world.Component<FromScope>();
//             world.set_scope(0);
//
//             Ecs.Assert(c.has(flecs::ChildOf, world.id<Scope>()));
//         }
//
//         [Fact]
//         private void World_register_nested_from_scope()
//         {
//             using World world = World.Create();
//
//             world.set_scope<Scope>();
//             var c = world.Component<Nested::FromScope>();
//             world.set_scope(0);
//
//             Ecs.Assert(c.has(flecs::ChildOf, world.id<Scope>()));
//         }
//
//         [Fact]
//         private void World_register_w_root_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<Scope>("::Root");
//
//             Ecs.Assert(!c.has(flecs::ChildOf, flecs::Wildcard));
//             test_str(c.path().c_str(), "::Root");
//         }
//
//         [Fact]
//         private void World_register_nested_w_root_name()
//         {
//             using World world = World.Create();
//
//             var c = world.Component<Nested::FromScope>("::Root");
//
//             Ecs.Assert(!c.has(flecs::ChildOf, flecs::Wildcard));
//             test_str(c.path().c_str(), "::Root");
//         }
//
//         [Fact]
//         private void World_set_lookup_path()
//         {
//             using World world = World.Create();
//
//             var parent = world.Entity("Parent");
//             var child = world.scope(parent).Entity("Child");
//
//             Ecs.Assert(world.lookup("Parent") == parent);
//             Ecs.Assert(world.lookup("Child") == 0);
//             Ecs.Assert(world.lookup("Parent::Child") == child);
//
//             ulong lookup_path[] = { parent, 0 };
//             ulong *old_path = world.set_lookup_path(lookup_path);
//
//             Ecs.Assert(world.lookup("Parent") == parent);
//             Ecs.Assert(world.lookup("Child") == child);
//             Ecs.Assert(world.lookup("Parent::Child") == child);
//
//             world.set_lookup_path(old_path);
//         }
//
//         [Fact]
//         private void World_run_post_frame()
//         {
//             using World world = World.Create();
//
//             int ctx = 10;
//
//             world.Routine()
//                 .iter((flecs::iter& it) {
//                     it.world().run_post_frame((flecs::world_t *w, void *ctx) {
//                         int *i = static_cast<int*>(ctx);
//                         test_int(*i, 10);
//                         i[0] ++;
//                     }, &ctx);
//                 });
//             test_int(ctx, 10);
//
//             world.progress();
//
//             test_int(ctx, 11);
//         }
//
//         [Fact]
//         private void World_component_w_low_id()
//         {
//             using World world = World.Create();
//
//             Entity p = world.Component<Position>();
//
//             Ecs.Assert(p.id() < FLECS_HI_COMPONENT_ID);
//         }
//
//         [Fact]
//         private void World_reregister_after_reset_w_hooks_and_in_use()
//         {
//             using World world = World.Create();
//
//             world.Component<Pod>();
//
//             world.Entity().add<Pod>();
//             test_int(1, Pod::ctor_invoked);
//
//             flecs::reset();
//
//             world.Component<Pod>();
//
//             world.Entity().add<Pod>();
//             test_int(2, Pod::ctor_invoked);
//         }
//
//         [Fact]
//         private void World_reregister_after_reset_w_hooks_and_in_use_implicit()
//         {
//             using World world = World.Create();
//
//             world.Component<Pod>();
//
//             world.Entity().add<Pod>();
//             test_int(1, Pod::ctor_invoked);
//
//             flecs::reset();
//
//             world.Entity().add<Pod>();
//             test_int(2, Pod::ctor_invoked);
//         }
//
//         [Fact]
//         private void World_get_ref() {
//             using World world = World.Create();
//
//             struct Space { int v; };
//             world.set<Space>({12});
//
//             var space = world.get_ref<Space>();
//             test_int(12, space->v);
//         }
//
//         [Fact]
//         private void World_get_set_log_level()
//         {
//             test_int(flecs::log::get_level(), -1);
//             flecs::log::set_level(4);
//             test_int(flecs::log::get_level(), 4);
//         }
//
//         [Fact]
//         private void World_reset_world()
//         {
//             using World world = World.Create();
//             Entity e = world.Entity();
//
//             Ecs.Assert(world.exists(e));
//             world.reset();
//             Ecs.Assert(!world.exists(e));
//         }
//
//         [Fact]
//         private void World_id_from_pair_type()
//         {
//             using World world = World.Create();
//
//             flecs::id id = world.id<flecs::pair<Position, Velocity>>();
//             Ecs.Assert(id.is_pair());
//             Ecs.Assert(id.first() == world.id<Position>());
//             Ecs.Assert(id.second() == world.id<Velocity>());
//         }
//
//
//         [Fact]
//         private void World_scope_w_name()
//         {
//             using World world = World.Create();
//
//             Entity parent = world.Entity("parent");
//             Entity child = world.scope("parent").Entity();
//
//             Ecs.Assert(child.has(flecs::ChildOf, parent));
//         }
//
//         [Fact]
//         private void World_set_get_context()
//         {
//             using World world = World.Create();
//
//             int ctx;
//             world.set_ctx(&ctx);
//             Ecs.Assert(world.get_ctx() == &ctx);
//             Ecs.Assert(world.get_binding_ctx() == nullptr);
//         }
//
//         [Fact]
//         private void World_set_get_binding_context()
//         {
//             using World world = World.Create();
//
//             int ctx;
//             world.set_binding_ctx(&ctx);
//             Ecs.Assert(world.get_binding_ctx() == &ctx);
//             Ecs.Assert(world.get_ctx() == nullptr);
//         }
//
//         static void ctx_free(void *ctx) {
//             static_cast<int*>(ctx)[0] = 10;
//         }
//
//         [Fact]
//         private void World_set_get_context_w_free()
//         {
//             int ctx = 0;
//
//             {
//                 using World world = World.Create();
//
//                 world.set_ctx(&ctx, ctx_free);
//                 Ecs.Assert(world.get_ctx() == &ctx);
//                 Ecs.Assert(world.get_binding_ctx() == nullptr);
//                 test_int(ctx, 0);
//             }
//
//             test_int(ctx, 10);
//         }
//
//         [Fact]
//         private void World_set_get_binding_context_w_free()
//         {
//             int ctx = 0;
//
//             {
//                 using World world = World.Create();
//
//                 world.set_binding_ctx(&ctx, ctx_free);
//                 Ecs.Assert(world.get_binding_ctx() == &ctx);
//                 Ecs.Assert(world.get_ctx() == nullptr);
//                 test_int(ctx, 0);
//             }
//
//             test_int(ctx, 10);
//         }
//
//         [Fact]
//         private void World_make_pair()
//         {
//             using World world = World.Create();
//
//             Entity r = world.Entity();
//             Entity t = world.Entity();
//             flecs::id id = world.pair(r, t);
//
//             Ecs.Assert(id.is_pair());
//             Ecs.Assert(id.first() == r);
//             Ecs.Assert(id.second() == t);
//         }
//
//         [Fact]
//         private void World_make_pair_of_pair_id()
//         {
//             install_test_abort();
//             using World world = World.Create();
//
//             Entity r = world.Entity();
//             Entity t = world.Entity();
//             flecs::id id = world.pair(r, t);
//
//             test_expect_abort();
//             world.pair(id, t);
//         }
//
//         [Fact]
//         private void World_make_pair_of_pair_id_tgt()
//         {
//             install_test_abort();
//             using World world = World.Create();
//
//             Entity r = world.Entity();
//             Entity t = world.Entity();
//             flecs::id id = world.pair(r, t);
//
//             test_expect_abort();
//             world.pair(r, id);
//         }
//
//         [Fact]
//         private void World_make_pair_of_pair_type()
//         {
//             install_test_abort();
//             using World world = World.Create();
//
//             Entity t = world.Entity();
//             flecs::id id = world.pair<Position>(t);
//
//             Ecs.Assert(id.is_pair());
//             Ecs.Assert(id.first() == world.id<Position>());
//             Ecs.Assert(id.second() == t);
//         }
//
//         [Fact]
//         private void World_delta_time()
//         {
//             using World world = World.Create();
//
//             float dt = 0;
//
//             world.Entity().add<Tag>();
//
//             world.Routine<Tag>()
//                 .Each((Entity e, Tag) {
//                     dt = e.world().delta_time();
//                 });
//
//             world.progress(2);
//
//             test_int(dt, 2);
//         }
//
//         [Fact]
//         private void World_register_nested_component_in_module()
//         {
//             using World world = World.Create();
//
//             world.import<nested_component_module>();
//
//             Ecs.Assert(Ecs.TypeId<nested_component_module::Foo>() != 0);
//             Ecs.Assert(Ecs.TypeId<nested_component_module::Foo::Bar>() != 0);
//
//             Entity foo = world.Component<nested_component_module::Foo>();
//             Entity bar = world.Component<nested_component_module::Foo::Bar>();
//
//             test_str(foo.path().c_str(), "::nested_component_module::Foo");
//             test_str(bar.path().c_str(), "::nested_component_module::Foo::Bar");
//         }
//
//         static void *atfini_ctx = nullptr;
//         static int atfini_invoked = 0;
//         static void atfini_callback(flecs::world_t *world, void *ctx) {
//             Ecs.Assert(world != nullptr);
//             atfini_ctx = ctx;
//             atfini_invoked ++;
//         }
//
//         [Fact]
//         private void World_atfini()
//         {
//             {
//                 using World world = World.Create();
//                 world.atfini(atfini_callback);
//             }
//             test_int(atfini_invoked, 1);
//             Ecs.Assert(atfini_ctx == nullptr);
//         }
//
//         [Fact]
//         private void World_atfini_w_ctx()
//         {
//             int ctx;
//             {
//                 using World world = World.Create();
//                 world.atfini(atfini_callback, &ctx);
//             }
//             test_int(atfini_invoked, 1);
//             Ecs.Assert(atfini_ctx == &ctx);
//         }
//
//         [Fact]
//         private void World_copy_world()
//         {
//             flecs::world world_1;
//             flecs::world world_2 = world_1;
//
//             Ecs.Assert(world_1.c_ptr() == world_2.c_ptr());
//         }
//     }
// }
