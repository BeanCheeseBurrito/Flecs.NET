using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     The world is the container of all ECS data and systems. If the world is deleted, all data in the world will be deleted as well.
    /// </summary>
    public unsafe struct World : IDisposable, IEquatable<World>
    {
        /// <summary>
        ///     The handle to the C world.
        /// </summary>
        public ecs_world_t* Handle { get; private set; }

        /// <summary>
        ///     Represents whether or not the world is owned.
        /// </summary>
        public bool Owned { get; private set; }

        internal BindingContext.WorldContext WorldContext;

        /// <summary>
        ///     Constructs a world from an <see cref="ecs_world_t"/> pointer.
        /// </summary>
        /// <param name="handle">The world handle.</param>
        /// <param name="owned">The owned boolean.</param>
        public World(ecs_world_t* handle, bool owned = true)
        {
            Handle = handle;
            Owned = owned;
            WorldContext = default;
        }

        /// <summary>
        ///     Creates a flecs world that is owned.
        /// </summary>
        /// <returns></returns>
        public static World Create()
        {
            return new World(ecs_init());
        }

        /// <summary>
        ///     Creates a flecs world from an <see cref="ecs_world_t"/> pointer that is not owned.
        /// </summary>
        /// <param name="world">A C world.</param>
        /// <returns>A newly created world.</returns>
        public static World Create(ecs_world_t* world)
        {
            return new World(world, false);
        }

        /// <summary>
        ///     Creates world from command line arguments.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns></returns>
        public static World Create(string[] args)
        {
            NativeString* nativeStrings = Memory.AllocZeroed<NativeString>(args.Length);

            for (int i = 0; i < args.Length; i++)
                nativeStrings[i] = (NativeString)args[i];

            ecs_world_t* world = ecs_init_w_args(args.Length, (byte**)nativeStrings);

            for (int i = 0; i < args.Length; i++)
                nativeStrings[i].Dispose();

            Memory.Free(nativeStrings);

            return new World(world);
        }

        /// <summary>
        ///     Calls <see cref="ecs_fini"/> and cleans up resources.
        /// </summary>
        public void Dispose()
        {
            if (Handle == null)
                return;

            _ = ecs_fini(Handle);
            WorldContext.Dispose();
            Handle = null;
        }

        /// <summary>
        ///     Deletes and creates a new world.
        /// </summary>
        public void Reset()
        {
            Assert.True(Owned, nameof(ECS_INVALID_OPERATION));
            _ = ecs_fini(Handle);
            Handle = ecs_init();
        }

        /// <summary>
        ///     Signals that the application should quit. The next call to <see cref="Progress"/> returns false.
        /// </summary>
        public readonly void Quit()
        {
            ecs_quit(Handle);
        }

        public void AtFini(Ecs.FiniAction action, void* ctx)
        {
            BindingContext.SetCallback(ref WorldContext.AtFini, action);
            ecs_atfini(Handle, WorldContext.AtFini.Function, ctx);
        }

        public readonly bool ShouldQuit()
        {
            return ecs_should_quit(Handle) == 1;
        }

        public float FrameBegin(float deltaTime = 0)
        {
            return ecs_frame_begin(Handle, deltaTime);
        }

        public void FrameEnd()
        {
            ecs_frame_end(Handle);
        }

        public bool ReadonlyBegin()
        {
            return ecs_readonly_begin(Handle) == 1;
        }

        public void ReadonlyEnd()
        {
            ecs_readonly_end(Handle);
        }

        public bool DeferBegin()
        {
            return ecs_defer_begin(Handle) == 1;
        }

        public bool DeferEnd()
        {
            return ecs_defer_end(Handle) == 1;
        }

        public bool IsDeferred()
        {
            return ecs_is_deferred(Handle) == 1;
        }

        public void SetStageCount(int stages)
        {
            ecs_set_stage_count(Handle, stages);
        }

        public int GetStageCount()
        {
            return ecs_get_stage_count(Handle);
        }

        public int GetStageId()
        {
            return ecs_get_stage_id(Handle);
        }

        public bool IsStage()
        {
            Assert.True(
                ecs_poly_is_(Handle, ecs_world_t_magic) == 1 &&
                ecs_poly_is_(Handle, ecs_stage_t_magic) == 1,
                nameof(ECS_INVALID_PARAMETER)
            );
            return ecs_poly_is_(Handle, ecs_stage_t_magic) == 1;
        }

        public void SetAutoMerge(bool autoMerge)
        {
            ecs_set_automerge(Handle, Macros.Bool(autoMerge));
        }

        public void Merge()
        {
            ecs_merge(Handle);
        }

        public World GetStage(int stageId)
        {
            return new World(ecs_get_stage(Handle, stageId), false);
        }

        public World AsyncStage()
        {
            return new World(ecs_async_stage_new(Handle));
        }

        public World GetWorld()
        {
            return new World(Handle == null ? null : ecs_get_world(Handle), false);
        }

        public bool IsReadOnly()
        {
            return ecs_stage_is_readonly(Handle) == 1;
        }

        public void SetContext(void* ctx)
        {
            ecs_set_context(Handle, ctx);
        }

        public void* GetContext()
        {
            return ecs_get_context(Handle);
        }

        public void Dim(int entityCount)
        {
            ecs_dim(Handle, entityCount);
        }

        public void SetEntityRange(ulong min, ulong max)
        {
            ecs_set_entity_range(Handle, min, max);
        }

        public void EnableRangeCheck(bool enabled)
        {
            ecs_enable_range_check(Handle, Macros.Bool(enabled));
        }

        public Entity SetScope(ulong scope)
        {
            return new Entity(ecs_set_scope(Handle, scope));
        }

        public Entity GetScope()
        {
            return new Entity(Handle, ecs_get_scope(Handle));
        }

        public ulong* SetLookupPath(ulong* searchPath)
        {
            return ecs_set_lookup_path(Handle, searchPath);
        }

        public Entity Lookup(string name)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSep = (NativeString)"::";

            return new Entity(Handle, ecs_lookup_path_w_sep(Handle, 0, nativeName, nativeSep, nativeSep, Macros.True));
        }

        public ref World Set<T>(ref T component)
        {
            Entity<T>().Set(ref component);
            return ref this;
        }

        public ref World Set<TFirst>(ulong second, ref TFirst component)
        {
            Entity<TFirst>().Set(second, ref component);
            return ref this;
        }

        public ref World SetFirst<TFirst, TSecond>(ref TFirst component)
        {
            Entity<TFirst>().SetFirst<TFirst, TSecond>(ref component);
            return ref this;
        }

        public ref World SetSecond<TFirst, TSecond>(ref TSecond component)
        {
            Entity<TFirst>().SetSecond<TFirst, TSecond>(ref component);
            return ref this;
        }

        public ref World Set<T>(T component)
        {
            return ref Set(ref component);
        }

        public ref World Set<TFirst>(ulong second, TFirst component)
        {
            return ref Set(second, ref component);
        }

        public ref World SetFirst<TFirst, TSecond>(TFirst component)
        {
            return ref SetFirst<TFirst, TSecond>(ref component);
        }

        public ref World SetSecond<TFirst, TSecond>(TSecond component)
        {
            return ref SetSecond<TFirst, TSecond>(ref component);
        }

        public T* GetMutPtr<T>() where T : unmanaged
        {
            return Entity<T>().GetMutPtr<T>();
        }

        public TFirst* GetMutPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            return Entity<TFirst>().GetMutPtr<TFirst>(second);
        }

        public TFirst* GetMutFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            return Entity<TFirst>().GetMutFirstPtr<TFirst, TSecond>();
        }

        public TSecond* GetMutSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            return Entity<TFirst>().GetMutSecondPtr<TFirst, TSecond>();
        }

        public ref T GetMut<T>()
        {
            return ref Entity<T>().GetMut<T>();
        }

        public ref TFirst GetMut<TFirst>(ulong second)
        {
            return ref Entity<TFirst>().GetMut<TFirst>(second);
        }

        public ref TFirst GetMutFirst<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetMutFirst<TFirst, TSecond>();
        }

        public ref TSecond GetMutSecond<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetMutSecond<TFirst, TSecond>();
        }

        public void Modified<T>()
        {
            Entity<T>().Modified<T>();
        }

        public void Modified<TFirst>(ulong second)
        {
            Entity<TFirst>().Modified<TFirst>(second);
        }

        public void Modified<TFirst, TSecond>()
        {
            Entity<TFirst>().Modified<TFirst, TSecond>();
        }

        public Ref<T> GetRef<T>()
        {
            return Entity<T>().GetRef<T>();
        }

        public Ref<TFirst> GetRef<TFirst>(ulong second)
        {
            return Entity<TFirst>().GetRef<TFirst>(second);
        }

        public Ref<TFirst> GetRefFirst<TFirst, TSecond>()
        {
            return Entity<TFirst>().GetRefFirst<TFirst, TSecond>();
        }

        public Ref<TSecond> GetRefSecond<TFirst, TSecond>()
        {
            return Entity<TFirst>().GetRefSecond<TFirst, TSecond>();
        }

        public T* GetPtr<T>() where T : unmanaged
        {
            return Entity<T>().GetPtr<T>();
        }

        public TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            return Entity<TFirst>().GetPtr<TFirst>(second);
        }

        public TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            return Entity<TFirst>().GetFirstPtr<TFirst, TSecond>();
        }

        public TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            return Entity<TFirst>().GetSecondPtr<TFirst, TSecond>();
        }

        public ref readonly T Get<T>()
        {
            return ref Entity<T>().Get<T>();
        }

        public ref readonly TFirst Get<TFirst>(ulong second)
        {
            return ref Entity<TFirst>().Get<TFirst>(second);
        }

        public ref readonly TFirst GetFirst<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetFirst<TFirst, TSecond>();
        }

        public ref readonly TSecond GetSecond<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetSecond<TFirst, TSecond>();
        }

        public bool Has(ulong first, ulong second)
        {
            return Entity(first).Has(first, second);
        }

        public bool Has<T>()
        {
            return Entity<T>().Has<T>();
        }

        public bool Has<TFirst>(ulong second)
        {
            return Entity<TFirst>().Has<TFirst>(second);
        }

        public bool Has<TFirst, TSecond>()
        {
            return Entity<TFirst>().Has<TFirst, TSecond>();
        }

        public void Add(ulong first, ulong second)
        {
            Entity(first).Add(first, second);
        }

        public void Add<T>()
        {
            Entity<T>().Add<T>();
        }

        public void Add<TFirst>(ulong second)
        {
            Entity<TFirst>().Add<TFirst>(second);
        }

        public void Add<TFirst, TSecond>()
        {
            Entity<TFirst>().Add<TFirst, TSecond>();
        }

        public void Remove(ulong first, ulong second)
        {
            Entity(first).Remove(first, second);
        }

        public void Remove<T>()
        {
            Entity<T>().Remove<T>();
        }

        public void Remove<TFirst>(ulong second)
        {
            Entity<TFirst>().Remove<TFirst>(second);
        }

        public void Remove<TFirst, TSecond>()
        {
            Entity<TFirst>().Remove<TFirst, TSecond>();
        }

        public void Children(Ecs.EachEntityCallback func)
        {
            Entity(0).Children(func);
        }

        public Entity Singleton<T>()
        {
            return Entity<T>();
        }

        public Entity Target<TFirst>(int index = 0)
        {
            return Entity(ecs_get_target(Handle, Type<TFirst>.Id(Handle), Type<TFirst>.Id(Handle), index));
        }

        public Entity Target<T>(ulong first, int index = 0)
        {
            return Entity(ecs_get_target(Handle, Type<T>.Id(Handle), first, index));
        }

        public Entity Target(ulong first, int index = 0)
        {
            return Entity(ecs_get_target(Handle, first, first, index));
        }

        public Entity Use<T>(string alias = "")
        {
            ulong entity = Type<T>.Id(Handle);

            if (string.IsNullOrEmpty(alias))
                alias = NativeString.GetString(ecs_get_name(Handle, entity));

            using NativeString nativeAlias = (NativeString)alias;

            ecs_set_alias(Handle, entity, nativeAlias);
            return Entity(entity);
        }

        public Entity Use(string name, string alias = "")
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSep = (NativeString)"::";

            ulong entity = ecs_lookup_path_w_sep(Handle, 0, nativeName, nativeSep, nativeSep, Macros.True);
            Assert.True(entity != 0, nameof(ECS_INVALID_PARAMETER));

            using NativeString nativeAlias = (NativeString)alias;

            ecs_set_alias(Handle, entity, nativeAlias);
            return Entity(entity);
        }

        public void Use(ulong entity, string alias = "")
        {
            if (string.IsNullOrEmpty(alias))
                alias = NativeString.GetString(ecs_get_name(Handle, entity));

            using NativeString nativeAlias = (NativeString)alias;

            ecs_set_alias(Handle, entity, nativeAlias);
        }

        public int Count(ulong componentId)
        {
            return ecs_count_id(Handle, componentId);
        }

        public int Count(ulong first, ulong second)
        {
            return Count(Macros.Pair(first, second));
        }

        public int Count<T>()
        {
            return Count(Type<T>.Id(Handle));
        }

        public int Count<TFirst>(ulong second)
        {
            return Count(Macros.Pair<TFirst>(second, Handle));
        }

        public int Count<TFirst, TSecond>()
        {
            return Count(Macros.Pair<TFirst, TSecond>(Handle));
        }

        /// <summary>
        /// All entities created in <see cref="func"/> will have <see cref="withId"/> added to them.
        /// </summary>
        /// <param name="withId">Id to be added to the created entities.</param>
        /// <param name="func"></param>
        public void With(ulong withId, Action func)
        {
            ulong prev = ecs_set_with(Handle, withId);
            func();
            ecs_set_with(Handle, prev);
        }

        public void With(ulong first, ulong second, Action func)
        {
            With(Macros.Pair(first, second), func);
        }

        public void With<T>(Action func)
        {
            With(Type<T>.Id(Handle), func);
        }

        public void With<TEnum>(TEnum @enum, Action func) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            With<TEnum>(enumId, func);
        }

        public void With<TFirst>(ulong second, Action func)
        {
            With(Macros.Pair<TFirst>(second, Handle), func);
        }

        public void With<TFirst, TSecondEnum>(TSecondEnum secondEnum, Action func) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            With<TFirst>(enumId, func);
        }

        public void With<TFirst, TSecond>(Action func)
        {
            With(Macros.Pair<TFirst, TSecond>(Handle), func);
        }

        public void WithSecond<TSecond>(ulong first, Action func)
        {
            With(Macros.PairSecond<TSecond>(first, Handle), func);
        }

        public void Scope(ulong parent, Action func)
        {
            ulong prev = ecs_set_scope(Handle, parent);
            func();
            ecs_set_scope(Handle, prev);
        }

        public void Scope<T>(Action func)
        {
            Scope(Type<T>.Id(Handle), func);
        }

        public void DeleteWith(ulong id)
        {
            ecs_delete_with(Handle, id);
        }

        public void DeleteWith(ulong first, ulong second)
        {
            DeleteWith(Macros.Pair(first, second));
        }

        public void DeleteWith<T>()
        {
            DeleteWith(Type<T>.Id(Handle));
        }

        public void DeleteWith<TEnum>(TEnum @enum) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            DeleteWith<TEnum>(enumId);
        }

        public void DeleteWith<TFirst>(ulong second)
        {
            DeleteWith(Macros.Pair<TFirst>(second, Handle));
        }

        public void DeleteWith<TFirst, TSecond>()
        {
            DeleteWith(Macros.Pair<TFirst, TSecond>(Handle));
        }

        public void DeleteWith<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            DeleteWith<TFirst>(enumId);
        }

        public void DeleteWithSecond<TSecond>(ulong first)
        {
            DeleteWith(Macros.PairSecond<TSecond>(first, Handle));
        }

        public void RemoveAll(ulong id)
        {
            ecs_remove_all(Handle, id);
        }

        public void RemoveAll(ulong first, ulong second)
        {
            RemoveAll(Macros.Pair(first, second));
        }

        public void RemoveAll<T>()
        {
            RemoveAll(Type<T>.Id(Handle));
        }

        public void RemoveAll<TEnum>(TEnum @enum) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            RemoveAll<TEnum>(enumId);
        }

        public void RemoveAll<TFirst>(ulong second)
        {
            RemoveAll(Macros.Pair<TFirst>(second, Handle));
        }

        public void RemoveAll<TFirst, TSecond>()
        {
            RemoveAll(Macros.Pair<TFirst, TSecond>(Handle));
        }

        public void RemoveAll<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            RemoveAll<TFirst>(enumId);
        }

        public void RemoveAllSecond<TSecond>(ulong first)
        {
            RemoveAll(Macros.PairSecond<TSecond>(first, Handle));
        }

        public void Defer(Action func)
        {
            ecs_defer_begin(Handle);
            func();
            ecs_defer_end(Handle);
        }

        public void DeferSuspend()
        {
            ecs_defer_suspend(Handle);
        }

        public void DeferResume()
        {
            ecs_defer_resume(Handle);
        }

        public bool Exists(ulong entity)
        {
            return ecs_exists(Handle, entity) == 1;
        }

        public bool IsAlive(ulong entity)
        {
            return ecs_is_alive(Handle, entity) == 1;
        }

        public bool IsValid(ulong entity)
        {
            return ecs_is_valid(Handle, entity) == 1;
        }

        public Entity GetAlive(ulong entity)
        {
            return Entity(ecs_get_alive(Handle, entity));
        }

        public Entity Ensure(ulong entity)
        {
            ecs_ensure(Handle, entity);
            return Entity(entity);
        }

        public void RunPostFrame(Ecs.FiniAction action, void* ctx)
        {
            BindingContext.SetCallback(ref WorldContext.RunpostFrame, action);
            ecs_run_post_frame(Handle, WorldContext.RunpostFrame.Function, ctx);
        }

        public ecs_world_info_t* GetInfo()
        {
            return ecs_get_world_info(Handle);
        }

        public Id Id(ulong id)
        {
            return new Id(Handle, id);
        }

        public Id Id(ulong first, ulong second)
        {
            return new Id(Handle, first, second);
        }

        public Id Id<T>()
        {
            return new Id(Handle, Type<T>.Id(Handle));
        }

        public Id Id<TEnum>(TEnum @enum) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            return new Id(Handle, enumId);
        }

        public Id Id<TFirst>(ulong second)
        {
            return new Id(Handle, Macros.Pair<TFirst>(second, Handle));
        }

        public Id Id<TFirst, TSecond>()
        {
            return new Id(Handle, Macros.Pair<TFirst, TSecond>(Handle));
        }

        public Id Id<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            return new Id(Handle, Macros.Pair<TFirst>(enumId, Handle));
        }

        public Id Pair(ulong first, ulong second)
        {
            return Id(first, second);
        }

        public Id Pair<TFirst>(ulong second)
        {
            return Id<TFirst>(second);
        }

        public Id Pair<TFirst, TSecond>()
        {
            return Id<TFirst, TSecond>();
        }

        public Component<T> Component<T>()
        {
            return new Component<T>(Handle);
        }

        public Entity Entity()
        {
            return new Entity(Handle);
        }

        public Entity Entity(ulong id)
        {
            return new Entity(Handle, id);
        }

        public Entity Entity(string name)
        {
            return new Entity(Handle, name);
        }

        public Entity Entity<T>()
        {
            return new Entity(Handle, Type<T>.Id(Handle));
        }

        public Entity Entity<T>(string name)
        {
            return new Entity(Handle, Type<T>.IdExplicit(Handle, name, true, 0, false));
        }

        public Entity Entity<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return new Entity(Handle, EnumType<TEnum>.Id(enumMember, Handle));
        }

        public Entity Prefab()
        {
            return new Entity(Handle).Add(EcsPrefab);
        }

        public Entity Prefab<T>(string name)
        {
            return new Component<T>(Handle, name).Entity.Add(EcsPrefab);
        }

        // TODO: Add event_builder stuff here later

        // TODO: Add World.Each stuff here later

        public Entity Module<TModule>(string name = "") where TModule : IFlecsModule, new()
        {
            ulong result = Type<TModule>.Id(Handle);

            if (!string.IsNullOrEmpty(name))
            {
                using NativeString nativeName = (NativeString)name;
                using NativeString nativeSep = (NativeString)"::";
                ecs_add_path_w_sep(Handle, result, 0, nativeName, nativeSep, nativeSep);
            }

            ecs_set_scope(Handle, result);
            return Entity(result);
        }

        public ulong Import<T>() where T : IFlecsModule, new()
        {
            return Core.Module.Import<T>(this);
        }

        // TODO: Add pipeline_builder

        public void SetPipeline(ulong pipeline)
        {
            ecs_set_pipeline(Handle, pipeline);
        }

        public void SetPipeline<T>()
        {
            ecs_set_pipeline(Handle, Type<T>.Id(Handle));
        }

        public Entity GetPipeline()
        {
            return Entity(ecs_get_pipeline(Handle));
        }

        public bool Progress()
        {
            return ecs_progress(Handle, 0) == 1;
        }

        public void RunPipeline(ulong pipeline, float deltaTime = 0)
        {
            ecs_run_pipeline(Handle, pipeline, deltaTime);
        }

        public void RunPipeline<TPipeline>(float deltaTime = 0)
        {
            ecs_run_pipeline(Handle, Type<TPipeline>.Id(Handle), deltaTime);
        }

        public void SetTimeScale(float scale)
        {
            ecs_set_time_scale(Handle, scale);
        }

        public void SetTargetFps(float targetFps)
        {
            ecs_set_target_fps(Handle, targetFps);
        }

        public void ResetClock()
        {
            ecs_reset_clock(Handle);
        }

        public void SetThreads(int threads)
        {
            ecs_set_threads(Handle, threads);
        }

        public int GetThreads()
        {
            return ecs_get_stage_count(Handle);
        }

        public void SetTaskThreads(int taskThreads)
        {
            ecs_set_task_threads(Handle, taskThreads);
        }

        public bool UsingTaskThreads()
        {
            return ecs_using_task_threads(Handle) == 1;
        }

        public Snapshot Snapshot()
        {
            return new Snapshot(Handle);
        }

        public int PlecsFromStr(string name, string str)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeStr = (NativeString)str;

            return ecs_plecs_from_str(Handle, nativeName, nativeStr);
        }

        public int PlecsFromFile(string fileName)
        {
            using NativeString nativeFileName = (NativeString)fileName;
            return ecs_plecs_from_file(Handle, nativeFileName);
        }

        public string ToExpr(ulong id, void* value)
        {
            return NativeString.GetStringAndFree(ecs_ptr_to_expr(Handle, id, value));
        }

        public string ToExpr<T>(T* value) where T : unmanaged
        {
            return ToExpr(Type<T>.Id(Handle), value);
        }

        public Cursor Cursor(ulong id, void* data)
        {
            return new Cursor(Handle, id, data);
        }

        public Cursor Cursor<T>(void* data)
        {
            return Cursor(Type<T>.Id(Handle), data);
        }

        public Entity Primitive(ecs_primitive_kind_t kind)
        {
            ecs_primitive_desc_t desc = default;
            desc.kind = kind;

            ulong id = ecs_primitive_init(Handle, &desc);
            Assert.True(id != 0, nameof(ECS_INVALID_OPERATION));

            return Entity(id);
        }

        public Entity Array(ulong elemId, int arrayCount)
        {
            ecs_array_desc_t desc = default;
            desc.type = elemId;
            desc.count = arrayCount;

            ulong id = ecs_array_init(Handle, &desc);
            Assert.True(id != 0, nameof(ECS_INVALID_OPERATION));

            return Entity(id);
        }

        public Entity Array<T>(int arrayCount)
        {
            return Array(Type<T>.Id(Handle), arrayCount);
        }

        public Entity Vector(ulong elemId)
        {
            ecs_vector_desc_t desc = default;
            desc.type = elemId;
            ulong id = ecs_vector_init(Handle, &desc);
            Assert.True(id != 0, nameof(ECS_INVALID_OPERATION));
            return Entity(id);
        }

        public Entity Vector<T>()
        {
            return Vector(Type<T>.Id(Handle));
        }

        public string ToJson(ulong id, void* value)
        {
            return NativeString.GetStringAndFree(ecs_ptr_to_json(Handle, id, value));
        }

        public string ToJson<T>(T* value) where T : unmanaged
        {
            return ToJson(Type<T>.Id(Handle), value);
        }

        public string ToJson()
        {
            return NativeString.GetStringAndFree(ecs_world_to_json(Handle, null));
        }

        public void* FromJson(ulong id, void* value, string json, ecs_from_json_desc_t* desc = null)
        {
            using NativeString nativeJson = (NativeString)json;
            return ecs_ptr_from_json(Handle, id, value, nativeJson, desc);
        }

        public void* FromJson<T>(T* value, string json, ecs_from_json_desc_t* desc = null) where T : unmanaged
        {
            return FromJson(Type<T>.Id(Handle), value, json, desc);
        }

        public void* FromJson(string json, ecs_from_json_desc_t *desc = null)
        {
            using NativeString nativeJson = (NativeString)json;
            return ecs_world_from_json(Handle, nativeJson, desc);
        }

        // TODO: Add metric builder here

        public AppBuilder App()
        {
            return new AppBuilder(Handle);
        }

        public FilterBuilder FilterBuilder()
        {
            return new FilterBuilder(Handle);
        }

        public QueryBuilder QueryBuilder()
        {
            return new QueryBuilder(Handle);
        }

        public ObserverBuilder ObserverBuilder()
        {
            return new ObserverBuilder(Handle);
        }

        public RoutineBuilder RoutineBuilder()
        {
            return new RoutineBuilder(Handle);
        }

        public Filter Filter(string name = "", FilterBuilder filterBuilder = default)
        {
            return new Filter(Handle, name, filterBuilder);
        }

        public Rule Rule(string name = "", FilterBuilder filterBuilder = default)
        {
            return new Rule(Handle, name, filterBuilder);
        }

        public Query Query(string name = "", FilterBuilder filterBuilder = default, QueryBuilder queryBuilder = default)
        {
            return new Query(Handle, name, filterBuilder, queryBuilder);
        }

        public Observer Observer(string name = "", FilterBuilder filterBuilder = default,
            ObserverBuilder observerBuilder = default,
            Ecs.IterCallback? callback = null)
        {
            return new Observer(Handle, name, filterBuilder, observerBuilder, callback);
        }

        public Observer Observer(ulong entity)
        {
            return new Observer(Handle, entity);
        }

        public Routine Routine(string name = "", FilterBuilder filterBuilder = default,
            QueryBuilder queryBuilder = default,
            RoutineBuilder routineBuilder = default, Ecs.IterCallback? callback = null)
        {
            return new Routine(Handle, name, filterBuilder, queryBuilder, routineBuilder, callback);
        }

        public Routine Routine(ulong entity)
        {
            return new Routine(Handle, entity);
        }

        public static implicit operator ecs_world_t*(World world)
        {
            return world.Handle;
        }

        public ecs_world_t* To()
        {
            return Handle;
        }

        public bool Equals(World other)
        {
            return Handle == other.Handle;
        }

        public override bool Equals(object obj)
        {
            return obj is World world && Equals(world);
        }

        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        public static bool operator ==(World left, World right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(World left, World right)
        {
            return !(left == right);
        }
    }
}
