using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     The world is the container of all ECS data and systems. If the world is deleted, all data in the world will be
    ///     deleted as well.
    /// </summary>
    public unsafe struct World : IDisposable, IEquatable<World>
    {
        private ecs_world_t* _handle;
        private bool _owned;

        private ref BindingContext.WorldContext WorldContext => ref *EnsureBindingContext();

        /// <summary>
        ///     The handle to the C world.
        /// </summary>
        public ref ecs_world_t* Handle => ref _handle;

        /// <summary>
        ///     Represents whether or not the world is owned.
        /// </summary>
        public ref bool Owned => ref _owned;

        /// <summary>
        ///     Constructs a world from an <see cref="ecs_world_t" /> pointer.
        /// </summary>
        /// <param name="handle">The world handle.</param>
        /// <param name="owned">The owned boolean.</param>
        /// <param name="overrideOsAbort"></param>
        public World(ecs_world_t* handle, bool owned = true, bool overrideOsAbort = false)
        {
            _handle = handle;
            _owned = owned;

            InitBuiltinComponents();

            if (overrideOsAbort)
                FlecsInternal.OverrideOsAbort();
        }

        /// <summary>
        ///     Creates a flecs world that is owned.
        /// </summary>
        /// <returns></returns>
        public static World Create(bool overrideOsAbort = true)
        {
            return new World(ecs_init(), true, overrideOsAbort);
        }

        /// <summary>
        ///     Creates a flecs world from an <see cref="ecs_world_t" /> pointer that is not owned.
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
        ///     Calls <see cref="ecs_fini" /> and cleans up resources.
        /// </summary>
        public void Dispose()
        {
            if (Handle == null)
                return;

            _ = ecs_fini(Handle);
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
        ///     Signals that the application should quit. The next call to <see cref="Progress" /> returns false.
        /// </summary>
        public void Quit()
        {
            ecs_quit(Handle);
        }

        /// <summary>
        ///     Register action to be executed when world is destroyed.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ctx"></param>
        public void AtFini(Ecs.FiniAction action, void* ctx)
        {
            BindingContext.SetCallback(ref WorldContext.AtFini, action);
            ecs_atfini(Handle, WorldContext.AtFini.Function, ctx);
        }

        /// <summary>
        ///     Test if Quit() has been called.
        /// </summary>
        /// <returns></returns>
        public bool ShouldQuit()
        {
            return ecs_should_quit(Handle) == 1;
        }

        /// <summary>
        ///     Begin frame.
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public float FrameBegin(float deltaTime = 0)
        {
            return ecs_frame_begin(Handle, deltaTime);
        }

        /// <summary>
        ///     End frame.
        /// </summary>
        public void FrameEnd()
        {
            ecs_frame_end(Handle);
        }

        /// <summary>
        ///     Begin staging.
        /// </summary>
        /// <returns></returns>
        public bool ReadonlyBegin()
        {
            return ecs_readonly_begin(Handle) == 1;
        }

        /// <summary>
        ///     End staging.
        /// </summary>
        public void ReadonlyEnd()
        {
            ecs_readonly_end(Handle);
        }

        /// <summary>
        ///     Defer operations until end end of frame, or until DeferEnd() is called.
        /// </summary>
        /// <returns></returns>
        public bool DeferBegin()
        {
            return ecs_defer_begin(Handle) == 1;
        }

        /// <summary>
        ///     End block of operations to defer.
        /// </summary>
        /// <returns></returns>
        public bool DeferEnd()
        {
            return ecs_defer_end(Handle) == 1;
        }

        /// <summary>
        ///     Test whether deferring is enabled.
        /// </summary>
        /// <returns></returns>
        public bool IsDeferred()
        {
            return ecs_is_deferred(Handle) == 1;
        }

        /// <summary>
        ///     Configure world to have N stages.
        /// </summary>
        /// <param name="stages"></param>
        public void SetStageCount(int stages)
        {
            ecs_set_stage_count(Handle, stages);
        }

        /// <summary>
        ///     Get number of configured stages.
        /// </summary>
        /// <returns></returns>
        public int GetStageCount()
        {
            return ecs_get_stage_count(Handle);
        }

        /// <summary>
        ///     Get current stage id.
        /// </summary>
        /// <returns></returns>
        public int GetStageId()
        {
            return ecs_get_stage_id(Handle);
        }

        /// <summary>
        ///     Test if is a stage.
        /// </summary>
        /// <returns></returns>
        public bool IsStage()
        {
            Assert.True(
                ecs_poly_is_(Handle, ecs_world_t_magic) == 1 &&
                ecs_poly_is_(Handle, ecs_stage_t_magic) == 1,
                nameof(ECS_INVALID_PARAMETER)
            );
            return ecs_poly_is_(Handle, ecs_stage_t_magic) == 1;
        }

        /// <summary>
        ///     Enable/disable auto merging for world or stage.
        /// </summary>
        /// <param name="autoMerge"></param>
        public void SetAutoMerge(bool autoMerge)
        {
            ecs_set_automerge(Handle, Macros.Bool(autoMerge));
        }

        /// <summary>
        ///     Merge world or stage.
        /// </summary>
        public void Merge()
        {
            ecs_merge(Handle);
        }

        /// <summary>
        ///     Get stage-specific world pointer.
        /// </summary>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public World GetStage(int stageId)
        {
            return new World(ecs_get_stage(Handle, stageId), false);
        }

        /// <summary>
        ///     Create asynchronous stage.
        /// </summary>
        /// <returns></returns>
        public World AsyncStage()
        {
            return new World(ecs_async_stage_new(Handle));
        }

        /// <summary>
        ///     Get actual world.
        /// </summary>
        /// <returns></returns>
        public World GetWorld()
        {
            return new World(Handle == null ? null : ecs_get_world(Handle), false);
        }

        /// <summary>
        ///     Test whether the current world object is readonly.
        /// </summary>
        /// <returns></returns>
        public bool IsReadOnly()
        {
            return ecs_stage_is_readonly(Handle) == 1;
        }

        /// <summary>
        ///     Set world context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ctxFree"></param>
        public void SetCtx(void* ctx, Ecs.ContextFree? ctxFree = null)
        {
            BindingContext.SetCallback(ref WorldContext.ContextFree, ctxFree);
            ecs_set_ctx(Handle, ctx, WorldContext.ContextFree.Function);
        }

        /// <summary>
        ///     Get world context.
        /// </summary>
        /// <returns></returns>
        public void* GetCtx()
        {
            return ecs_get_ctx(Handle);
        }

        /// <summary>
        ///     Preallocate memory for number of entities.
        /// </summary>
        /// <param name="entityCount"></param>
        public void Dim(int entityCount)
        {
            ecs_dim(Handle, entityCount);
        }

        /// <summary>
        ///     Set entity range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void SetEntityRange(ulong min, ulong max)
        {
            ecs_set_entity_range(Handle, min, max);
        }

        /// <summary>
        ///     Enforce that operations cannot modify entities outside of range.
        /// </summary>
        /// <param name="enabled"></param>
        public void EnableRangeCheck(bool enabled)
        {
            ecs_enable_range_check(Handle, Macros.Bool(enabled));
        }

        /// <summary>
        ///     Set current scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public Entity SetScope(ulong scope)
        {
            return new Entity(ecs_set_scope(Handle, scope));
        }

        /// <summary>
        ///     Get current scope.
        /// </summary>
        /// <returns></returns>
        public Entity GetScope()
        {
            return new Entity(Handle, ecs_get_scope(Handle));
        }

        /// <summary>
        ///     Set search path.
        /// </summary>
        /// <param name="searchPath"></param>
        /// <returns></returns>
        public ulong* SetLookupPath(ulong* searchPath)
        {
            return ecs_set_lookup_path(Handle, searchPath);
        }

        /// <summary>
        ///     Lookup entity by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entity Lookup(string name)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeSep = (NativeString)"::";

            return new Entity(Handle, ecs_lookup_path_w_sep(Handle, 0, nativeName, nativeSep, nativeSep, Macros.True));
        }

        /// <summary>
        ///     Set singleton component.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref World Set<T>(ref T component)
        {
            Entity<T>().Set(ref component);
            return ref this;
        }

        /// <summary>
        ///     Set singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref World Set<TFirst>(ulong second, ref TFirst component)
        {
            Entity<TFirst>().Set(second, ref component);
            return ref this;
        }

        /// <summary>
        ///     Set singleton pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref World SetFirst<TFirst, TSecond>(ref TFirst component)
        {
            Entity<TFirst>().SetFirst<TFirst, TSecond>(ref component);
            return ref this;
        }

        /// <summary>
        ///     Set singleton pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref World SetSecond<TFirst, TSecond>(ref TSecond component)
        {
            Entity<TFirst>().SetSecond<TFirst, TSecond>(ref component);
            return ref this;
        }

        /// <summary>
        ///     Set singleton component.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref World Set<T>(T component)
        {
            return ref Set(ref component);
        }

        /// <summary>
        ///     Set singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref World Set<TFirst>(ulong second, TFirst component)
        {
            return ref Set(second, ref component);
        }

        /// <summary>
        ///     Set singleton pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref World SetFirst<TFirst, TSecond>(TFirst component)
        {
            return ref SetFirst<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Set singleton pair.
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref World SetSecond<TFirst, TSecond>(TSecond component)
        {
            return ref SetSecond<TFirst, TSecond>(ref component);
        }

        /// <summary>
        ///     Get mut pointer to singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* GetMutPtr<T>() where T : unmanaged
        {
            return Entity<T>().GetMutPtr<T>();
        }

        /// <summary>
        ///     Get mut pointer to singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            return Entity<TFirst>().GetMutPtr<TFirst>(second);
        }

        /// <summary>
        ///     Get mut pointer to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetMutFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            return Entity<TFirst>().GetMutFirstPtr<TFirst, TSecond>();
        }

        /// <summary>
        ///     Get mut pointer to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetMutSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            return Entity<TFirst>().GetMutSecondPtr<TFirst, TSecond>();
        }

        /// <summary>
        ///     Get managed mut reference to singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref T GetMut<T>()
        {
            return ref Entity<T>().GetMut<T>();
        }

        /// <summary>
        ///     Get managed mut reference to singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMut<TFirst>(ulong second)
        {
            return ref Entity<TFirst>().GetMut<TFirst>(second);
        }

        /// <summary>
        ///     Get managed mut reference to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TFirst GetMutFirst<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetMutFirst<TFirst, TSecond>();
        }

        /// <summary>
        ///     Get managed mut reference to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref TSecond GetMutSecond<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetMutSecond<TFirst, TSecond>();
        }

        /// <summary>
        ///     Mark singleton component as modified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Modified<T>()
        {
            Entity<T>().Modified<T>();
        }

        /// <summary>
        ///     Mark singleton pair as modified.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Modified<TFirst>(ulong second)
        {
            Entity<TFirst>().Modified<TFirst>(second);
        }

        /// <summary>
        ///     Mark singleton pair as modified.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Modified<TFirst, TSecond>()
        {
            Entity<TFirst>().Modified<TFirst, TSecond>();
        }

        /// <summary>
        ///     Gets ref to singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Ref<T> GetRef<T>()
        {
            return Entity<T>().GetRef<T>();
        }

        /// <summary>
        ///     Gets ref to singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Ref<TFirst> GetRef<TFirst>(ulong second)
        {
            return Entity<TFirst>().GetRef<TFirst>(second);
        }

        /// <summary>
        ///     Gets ref to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TFirst> GetRefFirst<TFirst, TSecond>()
        {
            return Entity<TFirst>().GetRefFirst<TFirst, TSecond>();
        }

        /// <summary>
        ///     Gets ref to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Ref<TSecond> GetRefSecond<TFirst, TSecond>()
        {
            return Entity<TFirst>().GetRefSecond<TFirst, TSecond>();
        }

        /// <summary>
        ///     Gets pointer to singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* GetPtr<T>() where T : unmanaged
        {
            return Entity<T>().GetPtr<T>();
        }

        /// <summary>
        ///     Gets pointer to singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            return Entity<TFirst>().GetPtr<TFirst>(second);
        }

        /// <summary>
        ///     Gets pointer to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            return Entity<TFirst>().GetFirstPtr<TFirst, TSecond>();
        }


        /// <summary>
        ///     Gets pointer to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            return Entity<TFirst>().GetSecondPtr<TFirst, TSecond>();
        }

        /// <summary>
        ///     Gets managed reference to singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref readonly T Get<T>()
        {
            return ref Entity<T>().Get<T>();
        }

        /// <summary>
        ///     Gets managed reference to singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst Get<TFirst>(ulong second)
        {
            return ref Entity<TFirst>().Get<TFirst>(second);
        }

        /// <summary>
        ///     Gets managed reference to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TFirst GetFirst<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetFirst<TFirst, TSecond>();
        }

        /// <summary>
        ///     Gets managed reference to singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public ref readonly TSecond GetSecond<TFirst, TSecond>()
        {
            return ref Entity<TFirst>().GetSecond<TFirst, TSecond>();
        }

        /// <summary>
        ///     Test if world has pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool Has(ulong first, ulong second)
        {
            return Entity(first).Has(first, second);
        }

        /// <summary>
        ///     Test if world has singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>()
        {
            return Entity<T>().Has<T>();
        }

        /// <summary>
        ///     Test if world has singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst>(ulong second)
        {
            return Entity<TFirst>().Has<TFirst>(second);
        }

        /// <summary>
        ///     Test if world has singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>()
        {
            return Entity<TFirst>().Has<TFirst, TSecond>();
        }

        /// <summary>
        ///     Add singleton pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public void Add(ulong first, ulong second)
        {
            Entity(first).Add(first, second);
        }

        /// <summary>
        ///     Add singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>()
        {
            Entity<T>().Add<T>();
        }

        /// <summary>
        ///     Add singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Add<TFirst>(ulong second)
        {
            Entity<TFirst>().Add<TFirst>(second);
        }

        /// <summary>
        ///     Add singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Add<TFirst, TSecond>()
        {
            Entity<TFirst>().Add<TFirst, TSecond>();
        }

        /// <summary>
        ///     Remove singleton pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public void Remove(ulong first, ulong second)
        {
            Entity(first).Remove(first, second);
        }

        /// <summary>
        ///     Remove singleton component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>()
        {
            Entity<T>().Remove<T>();
        }

        /// <summary>
        ///     Remove singleton pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void Remove<TFirst>(ulong second)
        {
            Entity<TFirst>().Remove<TFirst>(second);
        }

        /// <summary>
        ///     Remove singleton pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void Remove<TFirst, TSecond>()
        {
            Entity<TFirst>().Remove<TFirst, TSecond>();
        }

        /// <summary>
        ///     Iterate entities in root of world
        /// </summary>
        /// <param name="func"></param>
        public void Children(Ecs.EachEntityCallback func)
        {
            Entity(0).Children(func);
        }

        /// <summary>
        ///     Get singleton entity for type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Singleton<T>()
        {
            return Entity<T>();
        }

        /// <summary>
        ///     Get target for a given pair from a singleton entity.
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Entity Target<TFirst>(int index = 0)
        {
            return Entity(ecs_get_target(Handle, Type<TFirst>.Id(Handle), Type<TFirst>.Id(Handle), index));
        }

        /// <summary>
        ///     Get target for a given pair from a singleton entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Target<T>(ulong first, int index = 0)
        {
            return Entity(ecs_get_target(Handle, Type<T>.Id(Handle), first, index));
        }

        /// <summary>
        ///     Get target for a given pair from a singleton entity.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Entity Target(ulong first, int index = 0)
        {
            return Entity(ecs_get_target(Handle, first, first, index));
        }

        /// <summary>
        ///     Create alias for component.
        /// </summary>
        /// <param name="alias"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Use<T>(string alias = "")
        {
            ulong entity = Type<T>.Id(Handle);

            if (string.IsNullOrEmpty(alias))
                alias = NativeString.GetString(ecs_get_name(Handle, entity));

            using NativeString nativeAlias = (NativeString)alias;

            ecs_set_alias(Handle, entity, nativeAlias);
            return Entity(entity);
        }

        /// <summary>
        ///     Create alias for component.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Create alias for component.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="alias"></param>
        public void Use(ulong entity, string alias = "")
        {
            if (string.IsNullOrEmpty(alias))
                alias = NativeString.GetString(ecs_get_name(Handle, entity));

            using NativeString nativeAlias = (NativeString)alias;

            ecs_set_alias(Handle, entity, nativeAlias);
        }

        /// <summary>
        ///     Count entities matching a component.
        /// </summary>
        /// <param name="componentId"></param>
        /// <returns></returns>
        public int Count(ulong componentId)
        {
            return ecs_count_id(Handle, componentId);
        }

        /// <summary>
        ///     Count entities matching a pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public int Count(ulong first, ulong second)
        {
            return Count(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Count entities matching a component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>()
        {
            return Count(Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Count entities matching a pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public int Count<TFirst>(ulong second)
        {
            return Count(Macros.Pair<TFirst>(second, Handle));
        }

        /// <summary>
        ///     Count entities matching a pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Count<TFirst, TSecond>()
        {
            return Count(Macros.Pair<TFirst, TSecond>(Handle));
        }

        /// <summary>
        ///     All entities created in function are created with id.
        /// </summary>
        /// <param name="withId">Id to be added to the created entities.</param>
        /// <param name="func"></param>
        public void With(ulong withId, Action func)
        {
            ulong prev = ecs_set_with(Handle, withId);
            func();
            ecs_set_with(Handle, prev);
        }

        /// <summary>
        ///     All entities created in function are created with pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="func"></param>
        public void With(ulong first, ulong second, Action func)
        {
            With(Macros.Pair(first, second), func);
        }

        /// <summary>
        ///     All entities created in function are created with type.
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        public void With<T>(Action func)
        {
            With(Type<T>.Id(Handle), func);
        }

        /// <summary>
        ///     All entities created in function are created with enum.
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="func"></param>
        /// <typeparam name="TEnum"></typeparam>
        public void With<TEnum>(TEnum @enum, Action func) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            With<TEnum>(enumId, func);
        }

        /// <summary>
        ///     All entities created in function are created with pair.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="func"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void With<TFirst>(ulong second, Action func)
        {
            With(Macros.Pair<TFirst>(second, Handle), func);
        }

        /// <summary>
        ///     All entities created in function are created with pair.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <param name="func"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        public void With<TFirst, TSecondEnum>(TSecondEnum secondEnum, Action func) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            With<TFirst>(enumId, func);
        }

        /// <summary>
        ///     All entities created in function are created with pair.
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void With<TFirst, TSecond>(Action func)
        {
            With(Macros.Pair<TFirst, TSecond>(Handle), func);
        }

        /// <summary>
        ///     All entities created in function are created with pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="func"></param>
        /// <typeparam name="TSecond"></typeparam>
        public void WithSecond<TSecond>(ulong first, Action func)
        {
            With(Macros.PairSecond<TSecond>(first, Handle), func);
        }

        /// <summary>
        ///     All entities created in function are created in scope. All operations
        ///     called in function (such as lookup) are relative to scope.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="func"></param>
        public void Scope(ulong parent, Action func)
        {
            ulong prev = ecs_set_scope(Handle, parent);
            func();
            ecs_set_scope(Handle, prev);
        }

        /// <summary>
        ///     Same as scope(parent, func), but with T as parent.
        /// </summary>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        public void Scope<T>(Action func)
        {
            Scope(Type<T>.Id(Handle), func);
        }

        /// <summary>
        ///     Use provided scope for operations ran on returned world.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public ScopedWorld Scope(ulong parent)
        {
            return new ScopedWorld(Handle, parent);
        }

        /// <summary>
        ///     Use provided scope for operations ran on returned world.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ScopedWorld Scope<T>()
        {
            return Scope(Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Use provided scope for operations ran on returned world.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ScopedWorld Scope(string name)
        {
            return Scope(Entity(name));
        }

        /// <summary>
        ///     Delete all entities with specified id.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWith(ulong id)
        {
            ecs_delete_with(Handle, id);
        }

        /// <summary>
        ///     Delete all entities with specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public void DeleteWith(ulong first, ulong second)
        {
            DeleteWith(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Delete all entities with specified component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteWith<T>()
        {
            DeleteWith(Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Delete all entities with specified enum.
        /// </summary>
        /// <param name="enum"></param>
        /// <typeparam name="TEnum"></typeparam>
        public void DeleteWith<TEnum>(TEnum @enum) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            DeleteWith<TEnum>(enumId);
        }

        /// <summary>
        ///     Delete all entities with specified pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void DeleteWith<TFirst>(ulong second)
        {
            DeleteWith(Macros.Pair<TFirst>(second, Handle));
        }

        /// <summary>
        ///     Delete all entities with specified pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void DeleteWith<TFirst, TSecond>()
        {
            DeleteWith(Macros.Pair<TFirst, TSecond>(Handle));
        }

        /// <summary>
        ///     Delete all entities with specified pair.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        public void DeleteWith<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            DeleteWith<TFirst>(enumId);
        }

        /// <summary>
        ///     Delete all entities with specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        public void DeleteWithSecond<TSecond>(ulong first)
        {
            DeleteWith(Macros.PairSecond<TSecond>(first, Handle));
        }

        /// <summary>
        ///     Remove all instances of specified id.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAll(ulong id)
        {
            ecs_remove_all(Handle, id);
        }

        /// <summary>
        ///     Remove all instances of specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public void RemoveAll(ulong first, ulong second)
        {
            RemoveAll(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Remove all instances of specified component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveAll<T>()
        {
            RemoveAll(Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Remove all instances of specified enum.
        /// </summary>
        /// <param name="enum"></param>
        /// <typeparam name="TEnum"></typeparam>
        public void RemoveAll<TEnum>(TEnum @enum) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            RemoveAll<TEnum>(enumId);
        }

        /// <summary>
        ///     Remove all instances of specified pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        public void RemoveAll<TFirst>(ulong second)
        {
            RemoveAll(Macros.Pair<TFirst>(second, Handle));
        }

        /// <summary>
        ///     Remove all instances of specified pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        public void RemoveAll<TFirst, TSecond>()
        {
            RemoveAll(Macros.Pair<TFirst, TSecond>(Handle));
        }

        /// <summary>
        ///     Remove all instances of specified pair.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        public void RemoveAll<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            RemoveAll<TFirst>(enumId);
        }

        /// <summary>
        ///     Remove all instances of specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        public void RemoveAllSecond<TSecond>(ulong first)
        {
            RemoveAll(Macros.PairSecond<TSecond>(first, Handle));
        }

        /// <summary>
        ///     Defer all operations called in function. If the world is already in
        ///     deferred mode, do nothing.
        /// </summary>
        /// <param name="func"></param>
        public void Defer(Action func)
        {
            ecs_defer_begin(Handle);
            func();
            ecs_defer_end(Handle);
        }

        /// <summary>
        ///     Suspend deferring operations.
        /// </summary>
        public void DeferSuspend()
        {
            ecs_defer_suspend(Handle);
        }

        /// <summary>
        ///     Resume deferring operations.
        /// </summary>
        public void DeferResume()
        {
            ecs_defer_resume(Handle);
        }

        /// <summary>
        ///     Check if entity id exists in the world.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Exists(ulong entity)
        {
            return ecs_exists(Handle, entity) == 1;
        }

        /// <summary>
        ///     Check if entity id exists in the world.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsAlive(ulong entity)
        {
            return ecs_is_alive(Handle, entity) == 1;
        }

        /// <summary>
        ///     Check if entity id is valid.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsValid(ulong entity)
        {
            return ecs_is_valid(Handle, entity) == 1;
        }

        /// <summary>
        ///     Get alive entity for id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity GetAlive(ulong entity)
        {
            return Entity(ecs_get_alive(Handle, entity));
        }

        /// <summary>
        ///     Ensures that entity with provided generation is alive.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Ensure(ulong entity)
        {
            ecs_ensure(Handle, entity);
            return Entity(entity);
        }

        /// <summary>
        ///     Run callback after completing frame.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ctx"></param>
        public void RunPostFrame(Ecs.FiniAction action, void* ctx)
        {
            BindingContext.SetCallback(ref WorldContext.RunPostFrame, action);
            ecs_run_post_frame(Handle, WorldContext.RunPostFrame.Function, ctx);
        }

        /// <summary>
        ///     Get the world info.
        /// </summary>
        /// <returns></returns>
        public ecs_world_info_t* GetInfo()
        {
            return ecs_get_world_info(Handle);
        }

        /// <summary>
        ///     Get id from id value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Id Id(ulong id)
        {
            return new Id(Handle, id);
        }

        /// <summary>
        ///     Get pair from id values.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Id Id(ulong first, ulong second)
        {
            return new Id(Handle, first, second);
        }

        /// <summary>
        ///     Get id from a type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Id Id<T>()
        {
            return new Id(Handle, Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Get id from an enum.
        /// </summary>
        /// <param name="enum"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public Id Id<TEnum>(TEnum @enum) where TEnum : Enum
        {
            ulong enumId = EnumType<TEnum>.Id(@enum, Handle);
            return new Id(Handle, enumId);
        }

        /// <summary>
        ///     Get pair id.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Id Id<TFirst>(ulong second)
        {
            return new Id(Handle, Macros.Pair<TFirst>(second, Handle));
        }

        /// <summary>
        ///     Get pair id.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Id Id<TFirst, TSecond>()
        {
            return new Id(Handle, Macros.Pair<TFirst, TSecond>(Handle));
        }

        /// <summary>
        ///     Get pair id.
        /// </summary>
        /// <param name="secondEnum"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecondEnum"></typeparam>
        /// <returns></returns>
        public Id Id<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            ulong enumId = EnumType<TSecondEnum>.Id(secondEnum, Handle);
            return new Id(Handle, Macros.Pair<TFirst>(enumId, Handle));
        }

        /// <summary>
        ///     Get pair id.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Id Pair(ulong first, ulong second)
        {
            Assert.True(!Macros.IsPair(first) && !Macros.IsPair(second), "Cannot create nested pairs.");
            return Id(first, second);
        }

        /// <summary>
        ///     Get pair id.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Id Pair<TFirst>(ulong second)
        {
            Assert.True(!Macros.IsPair(second), "Cannot create nested pairs.");
            return Id<TFirst>(second);
        }

        /// <summary>
        ///     Get pair id.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Id Pair<TFirst, TSecond>()
        {
            return Id<TFirst, TSecond>();
        }

        /// <summary>
        ///     Find or register component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Component<T> Component<T>()
        {
            return new Component<T>(Handle);
        }

        /// <summary>
        ///     Get component with name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Component<T> Component<T>(string name)
        {
            return new Component<T>(Handle, name);
        }

        /// <summary>
        ///     Create an entity.
        /// </summary>
        /// <returns></returns>
        public Entity Entity()
        {
            return new Entity(Handle);
        }

        /// <summary>
        ///     Create an entity from id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity Entity(ulong id)
        {
            return new Entity(Handle, id);
        }

        /// <summary>
        ///     Create an entity from name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entity Entity(string name)
        {
            return new Entity(Handle, name);
        }

        /// <summary>
        ///     Create an entity from type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Entity<T>()
        {
            return new Entity(Handle, Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Create an entity that's associated with a type.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Entity<T>(string name)
        {
            return new Entity(Handle, Type<T>.IdExplicit(Handle, name, true, 0, false));
        }

        /// <summary>
        ///     Convert enum constant to entity.
        /// </summary>
        /// <param name="enumMember"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public Entity Entity<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            return new Entity(Handle, EnumType<TEnum>.Id(enumMember, Handle));
        }

        /// <summary>
        ///     Create a prefab.
        /// </summary>
        /// <returns></returns>
        public Entity Prefab()
        {
            return new Entity(Handle).Add(EcsPrefab);
        }

        /// <summary>
        ///     Create a prefab with the provided name.
        /// </summary>
        /// <returns></returns>
        public Entity Prefab(string name)
        {
            return new Entity(Handle, name).Add(EcsPrefab);
        }

        /// <summary>
        ///     Create a prefab that's associated with a type.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Prefab<T>(string name)
        {
            return new Component<T>(Handle, name).Entity.Add(EcsPrefab);
        }

        // TODO: Add event_builder stuff here later

        // TODO: Add World.Each stuff here later

        /// <summary>
        ///     Define a module.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        ///     Import a module.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ulong Import<T>() where T : IFlecsModule, new()
        {
            return Core.Module.Import<T>(this);
        }

        // TODO: Add pipeline_builder

        /// <summary>
        ///     Set pipeline.
        /// </summary>
        /// <param name="pipeline"></param>
        public void SetPipeline(ulong pipeline)
        {
            ecs_set_pipeline(Handle, pipeline);
        }

        /// <summary>
        ///     Set pipeline.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetPipeline<T>()
        {
            ecs_set_pipeline(Handle, Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Get pipeline.
        /// </summary>
        /// <returns></returns>
        public Entity GetPipeline()
        {
            return Entity(ecs_get_pipeline(Handle));
        }

        /// <summary>
        ///     Progress world one tick.
        /// </summary>
        /// <returns></returns>
        public bool Progress()
        {
            return ecs_progress(Handle, 0) == 1;
        }

        /// <summary>
        ///     Run pipeline.
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="deltaTime"></param>
        public void RunPipeline(ulong pipeline, float deltaTime = 0)
        {
            ecs_run_pipeline(Handle, pipeline, deltaTime);
        }

        /// <summary>
        ///     Run pipeline.
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <typeparam name="TPipeline"></typeparam>
        public void RunPipeline<TPipeline>(float deltaTime = 0)
        {
            ecs_run_pipeline(Handle, Type<TPipeline>.Id(Handle), deltaTime);
        }

        /// <summary>
        ///     Set timescale.
        /// </summary>
        /// <param name="scale"></param>
        public void SetTimeScale(float scale)
        {
            ecs_set_time_scale(Handle, scale);
        }

        /// <summary>
        ///     Set target FPS.
        /// </summary>
        /// <param name="targetFps"></param>
        public void SetTargetFps(float targetFps)
        {
            ecs_set_target_fps(Handle, targetFps);
        }

        /// <summary>
        ///     Reset simulation clock.
        /// </summary>
        public void ResetClock()
        {
            ecs_reset_clock(Handle);
        }

        /// <summary>
        ///     Set number of threads.
        /// </summary>
        /// <param name="threads"></param>
        public void SetThreads(int threads)
        {
            ecs_set_threads(Handle, threads);
        }

        /// <summary>
        ///     Set number of threads.
        /// </summary>
        /// <returns></returns>
        public int GetThreads()
        {
            return ecs_get_stage_count(Handle);
        }

        /// <summary>
        ///     Set number of task threads.
        /// </summary>
        /// <param name="taskThreads"></param>
        public void SetTaskThreads(int taskThreads)
        {
            ecs_set_task_threads(Handle, taskThreads);
        }

        /// <summary>
        ///     Returns true if task thread use has been requested.
        /// </summary>
        /// <returns></returns>
        public bool UsingTaskThreads()
        {
            return ecs_using_task_threads(Handle) == 1;
        }

        /// <summary>
        ///     Create a snapshot.
        /// </summary>
        /// <returns></returns>
        public Snapshot Snapshot()
        {
            return new Snapshot(Handle);
        }

        /// <summary>
        ///     Create a timer.
        /// </summary>
        /// <returns></returns>
        public Timer Timer()
        {
            return new Timer(Handle);
        }

        /// <summary>
        ///     Create a timer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Timer Timer(ulong id)
        {
            return new Timer(Handle, id);
        }

        /// <summary>
        ///     Create a timer.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Timer Timer(string name)
        {
            return new Timer(Handle, name);
        }

        /// <summary>
        ///     Enable randomization of initial time values for timers.
        /// </summary>
        public void RandomizeTimers()
        {
            ecs_randomize_timers(Handle);
        }

        /// <summary>
        ///     Load plecs string.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public int PlecsFromStr(string name, string str)
        {
            using NativeString nativeName = (NativeString)name;
            using NativeString nativeStr = (NativeString)str;

            return ecs_plecs_from_str(Handle, nativeName, nativeStr);
        }

        /// <summary>
        ///     Load plecs from file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int PlecsFromFile(string fileName)
        {
            using NativeString nativeFileName = (NativeString)fileName;
            return ecs_plecs_from_file(Handle, nativeFileName);
        }

        /// <summary>
        ///     Convert value to string.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ToExpr(ulong id, void* value)
        {
            return NativeString.GetStringAndFree(ecs_ptr_to_expr(Handle, id, value));
        }

        /// <summary>
        ///     Convert value to string
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string ToExpr<T>(T* value) where T : unmanaged
        {
            return ToExpr(Type<T>.Id(Handle), value);
        }

        /// <summary>
        ///     Return meta cursor to value.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Cursor Cursor(ulong id, void* data)
        {
            return new Cursor(Handle, id, data);
        }

        /// <summary>
        ///     Return meta cursor to value.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Cursor Cursor<T>(void* data)
        {
            return Cursor(Type<T>.Id(Handle), data);
        }

        /// <summary>
        ///     Create primitive type.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public Entity Primitive(ecs_primitive_kind_t kind)
        {
            ecs_primitive_desc_t desc = default;
            desc.kind = kind;

            ulong id = ecs_primitive_init(Handle, &desc);
            Assert.True(id != 0, nameof(ECS_INVALID_OPERATION));

            return Entity(id);
        }

        /// <summary>
        ///     Create array type.
        /// </summary>
        /// <param name="elemId"></param>
        /// <param name="arrayCount"></param>
        /// <returns></returns>
        public Entity Array(ulong elemId, int arrayCount)
        {
            ecs_array_desc_t desc = default;
            desc.type = elemId;
            desc.count = arrayCount;

            ulong id = ecs_array_init(Handle, &desc);
            Assert.True(id != 0, nameof(ECS_INVALID_OPERATION));

            return Entity(id);
        }

        /// <summary>
        ///     Create array type.
        /// </summary>
        /// <param name="arrayCount"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Array<T>(int arrayCount)
        {
            return Array(Type<T>.Id(Handle), arrayCount);
        }

        /// <summary>
        ///     Create vector type.
        /// </summary>
        /// <param name="elemId"></param>
        /// <returns></returns>
        public Entity Vector(ulong elemId)
        {
            ecs_vector_desc_t desc = default;
            desc.type = elemId;
            ulong id = ecs_vector_init(Handle, &desc);
            Assert.True(id != 0, nameof(ECS_INVALID_OPERATION));
            return Entity(id);
        }

        /// <summary>
        ///     Create vector type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Entity Vector<T>()
        {
            return Vector(Type<T>.Id(Handle));
        }

        /// <summary>
        ///     Serialize untyped value to JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ToJson(ulong id, void* value)
        {
            return NativeString.GetStringAndFree(ecs_ptr_to_json(Handle, id, value));
        }

        /// <summary>
        ///     Serialize value to JSON.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string ToJson<T>(T* value) where T : unmanaged
        {
            return ToJson(Type<T>.Id(Handle), value);
        }

        /// <summary>
        ///     Serialize world to JSON.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return NativeString.GetStringAndFree(ecs_world_to_json(Handle, null));
        }

        /// <summary>
        ///     Deserialize value from JSON.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public void* FromJson(ulong id, void* value, string json, ecs_from_json_desc_t* desc = null)
        {
            using NativeString nativeJson = (NativeString)json;
            return ecs_ptr_from_json(Handle, id, value, nativeJson, desc);
        }

        /// <summary>
        ///     Deserialize value from JSON.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void* FromJson<T>(T* value, string json, ecs_from_json_desc_t* desc = null) where T : unmanaged
        {
            return FromJson(Type<T>.Id(Handle), value, json, desc);
        }

        /// <summary>
        ///     Deserialize value from JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public void* FromJson(string json, ecs_from_json_desc_t* desc = null)
        {
            using NativeString nativeJson = (NativeString)json;
            return ecs_world_from_json(Handle, nativeJson, desc);
        }

        /// <summary>
        ///     Create app builder.
        /// </summary>
        /// <returns></returns>
        public AppBuilder App()
        {
            return new AppBuilder(Handle);
        }

        /// <summary>
        ///     Create metric builder.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MetricBuilder Metric(ulong entity)
        {
            return new MetricBuilder(Handle, entity);
        }

        /// <summary>
        ///     Create filter builder.
        /// </summary>
        /// <returns></returns>
        public FilterBuilder FilterBuilder()
        {
            return new FilterBuilder(Handle);
        }

        /// <summary>
        ///     Create query builder.
        /// </summary>
        /// <returns></returns>
        public QueryBuilder QueryBuilder()
        {
            return new QueryBuilder(Handle);
        }

        /// <summary>
        ///     Create observer builder.
        /// </summary>
        /// <returns></returns>
        public ObserverBuilder ObserverBuilder()
        {
            return new ObserverBuilder(Handle);
        }

        /// <summary>
        ///     Create routine builder.
        /// </summary>
        /// <returns></returns>
        public RoutineBuilder RoutineBuilder()
        {
            return new RoutineBuilder(Handle);
        }

        /// <summary>
        ///     Create filter.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Filter Filter(FilterBuilder filter = default, string name = "")
        {
            return new Filter(Handle, filter, name);
        }

        /// <summary>
        ///     Create rule.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Rule Rule(FilterBuilder filter = default, string name = "")
        {
            return new Rule(Handle, filter, name);
        }

        /// <summary>
        ///     Create query.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public Query Query(FilterBuilder filter = default, QueryBuilder query = default, string name = "")
        {
            return new Query(Handle, filter, query, name);
        }

        /// <summary>
        ///     Create observer.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <param name="observer"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Observer Observer(
            FilterBuilder filter = default,
            ObserverBuilder observer = default,
            Ecs.IterCallback? callback = null,
            string name = "")
        {
            return new Observer(Handle, filter, observer, callback, name);
        }

        /// <summary>
        ///     Create observer.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Observer Observer(ulong entity)
        {
            return new Observer(Handle, entity);
        }

        /// <summary>
        ///     Create routine.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <param name="query"></param>
        /// <param name="routine"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public Routine Routine(
            FilterBuilder filter = default,
            QueryBuilder query = default,
            RoutineBuilder routine = default,
            Ecs.IterCallback? callback = null,
            string name = "")
        {
            return new Routine(Handle, filter, query, routine, callback, name);
        }

        /// <summary>
        ///     Create routine.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Routine Routine(ulong entity)
        {
            return new Routine(Handle, entity);
        }

        /// <summary>
        ///     Initializes built-in components.
        /// </summary>
        public void InitBuiltinComponents()
        {
            Import<Ecs.Meta>();
        }

        private BindingContext.WorldContext* EnsureBindingContext()
        {
            BindingContext.WorldContext* ptr = (BindingContext.WorldContext*)ecs_get_binding_ctx(Handle);

            if (ptr != null)
                return ptr;

            ptr = Memory.AllocZeroed<BindingContext.WorldContext>(1);
            ecs_set_binding_ctx(Handle, ptr, BindingContext.WorldContextFreePointer);
            return ptr;
        }

        /// <summary>
        /// </summary>
        /// <param name="world"></param>
        /// <returns></returns>
        public static implicit operator ecs_world_t*(World world)
        {
            return world.Handle;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ecs_world_t* To()
        {
            return Handle;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(World other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is World world && Equals(world);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(World left, World right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(World left, World right)
        {
            return !(left == right);
        }
    }
}
