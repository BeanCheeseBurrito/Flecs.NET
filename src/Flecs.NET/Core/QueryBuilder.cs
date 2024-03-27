using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Wrapper around ecs_query_desc_t.
    /// </summary>
    public unsafe partial struct QueryBuilder : IDisposable, IEquatable<QueryBuilder>
    {
        private ecs_world_t* _world;

        internal ecs_query_desc_t QueryDesc;
        internal FilterBuilder FilterBuilder;
        internal BindingContext.QueryContext QueryContext;

        /// <summary>
        ///     Reference to the world.
        /// </summary>
        public ref ecs_world_t* World => ref _world;

        /// <summary>
        ///     Reference to the query description.
        /// </summary>
        public ref ecs_query_desc_t Desc => ref QueryDesc;

        /// <summary>
        ///     Creates a named query builder for the provided world.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="name"></param>
        public QueryBuilder(ecs_world_t* world, string? name = null)
        {
            _world = world;
            FilterBuilder = new FilterBuilder(world);
            QueryDesc = default;
            QueryContext = default;

            if (string.IsNullOrEmpty(name))
                return;

            using NativeString nativeName = (NativeString)name;

            ecs_entity_desc_t entityDesc = default;
            entityDesc.name = nativeName;
            entityDesc.sep = BindingContext.DefaultSeparator;
            entityDesc.root_sep = BindingContext.DefaultRootSeparator;
            FilterBuilder.Desc.entity = ecs_entity_init(world, &entityDesc);
        }

        /// <summary>
        ///     Disposes the query builder.
        /// </summary>
        public void Dispose()
        {
            QueryContext.Dispose();
        }

        /// <summary>
        ///     Builds a new query.
        /// </summary>
        /// <returns></returns>
        public Query Build()
        {
            fixed (QueryBuilder* self = &this)
            {
                BindingContext.QueryContext* queryContext = Memory.Alloc<BindingContext.QueryContext>(1);
                queryContext[0] = QueryContext;

                ecs_query_desc_t* queryDesc = &self->QueryDesc;
                queryDesc->filter = FilterBuilder.Desc;
                queryDesc->filter.terms_buffer = FilterBuilder.Terms.Data;
                queryDesc->filter.terms_buffer_count = FilterBuilder.Terms.Count;
                queryDesc->binding_ctx = queryContext;
                queryDesc->binding_ctx_free = BindingContext.QueryContextFreePointer;

                ecs_query_t* handle = ecs_query_init(World, queryDesc);

                if (handle == null)
                    Ecs.Error("Query failed to init");

                FilterBuilder.Dispose();

                return new Query(World, handle);
            }
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
        ///     Sort the output of a query.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public ref QueryBuilder OrderBy(ulong component, Ecs.OrderByAction compare)
        {
            BindingContext.SetCallback(ref QueryContext.OrderByAction, compare);
            QueryDesc.order_by = QueryContext.OrderByAction.Function;
            QueryDesc.order_by_component = component;
            return ref this;
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="groupByAction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref QueryBuilder GroupBy<T>(Ecs.GroupByAction groupByAction)
        {
            return ref GroupBy(Type<T>.Id(World), groupByAction);
        }

        /// <summary>
        ///     Group and sort matched tables.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="groupByAction"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component, Ecs.GroupByAction groupByAction)
        {
            BindingContext.SetCallback(ref QueryContext.GroupByAction, groupByAction);
            QueryDesc.group_by = QueryContext.GroupByAction.Function;
            QueryDesc.group_by_id = component;
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
        /// <returns></returns>
        public ref QueryBuilder GroupBy(ulong component)
        {
            QueryDesc.group_by = IntPtr.Zero;
            QueryDesc.group_by_id = component;
            return ref this;
        }

        /// <summary>
        ///     Specify context to be passed to group_by function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="contextFree"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupByCtx(void* ctx, Ecs.ContextFree contextFree)
        {
            BindingContext.SetCallback(ref QueryContext.ContextFree, contextFree);
            QueryDesc.group_by_ctx_free = QueryContext.ContextFree.Function;
            QueryDesc.group_by_ctx = ctx;
            return ref this;
        }

        /// <summary>
        ///     Specify context to be passed to group_by function.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ref QueryBuilder GroupByCtx(void* ctx)
        {
            QueryDesc.group_by_ctx = ctx;
            QueryDesc.group_by_ctx_free = IntPtr.Zero;
            return ref this;
        }

        /// <summary>
        ///     Specify on_group_create action.
        /// </summary>
        /// <param name="onGroupCreate"></param>
        /// <returns></returns>
        public ref QueryBuilder OnGroupCreate(Ecs.GroupCreateAction onGroupCreate)
        {
            BindingContext.SetCallback(ref QueryContext.GroupCreateAction, onGroupCreate);
            QueryDesc.on_group_create = QueryContext.GroupCreateAction.Function;
            return ref this;
        }

        /// <summary>
        ///     Specify on_group_delete action.
        /// </summary>
        /// <param name="onGroupDelete"></param>
        /// <returns></returns>
        public ref QueryBuilder OnGroupDelete(Ecs.GroupDeleteAction onGroupDelete)
        {
            BindingContext.SetCallback(ref QueryContext.GroupDeleteAction, onGroupDelete);
            QueryDesc.on_group_delete = QueryContext.GroupDeleteAction.Function;
            return ref this;
        }

        /// <summary>
        ///     Specify parent query (creates subquery)
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public ref QueryBuilder Observable(Query parent)
        {
            return ref Observable(ref parent);
        }

        /// <summary>
        ///     Specify parent query (creates subquery)
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public ref QueryBuilder Observable(ref Query parent)
        {
            QueryDesc.parent = parent.Handle;
            return ref this;
        }

        /// <summary>
        ///     Checks if two <see cref="QueryBuilder"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(QueryBuilder other)
        {
            return Desc == other.Desc && FilterBuilder == other.FilterBuilder;
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
        ///     Returns the hash code of the <see cref="QueryBuilder"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Desc.GetHashCode(), FilterBuilder.GetHashCode());
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

    // FilterBuilder extensions.
    public unsafe partial struct QueryBuilder
    {
        /// <inheritdoc cref="Core.FilterBuilder.Self"/>
        public ref QueryBuilder Self()
        {
            FilterBuilder.Self();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up"/>
        public ref QueryBuilder Up(ulong traverse = 0)
        {
            FilterBuilder.Up(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Up{T}"/>
        public ref QueryBuilder Up<T>()
        {
            FilterBuilder.Up<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade"/>
        public ref QueryBuilder Cascade(ulong traverse = 0)
        {
            FilterBuilder.Cascade(traverse);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Cascade{T}"/>
        public ref QueryBuilder Cascade<T>()
        {
            FilterBuilder.Cascade<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Descend"/>
        public ref QueryBuilder Descend()
        {
            FilterBuilder.Descend();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Parent"/>
        public ref QueryBuilder Parent()
        {
            FilterBuilder.Parent();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Trav"/>
        public ref QueryBuilder Trav(ulong traverse, uint flags = 0)
        {
            FilterBuilder.Trav(traverse, flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Id"/>
        public ref QueryBuilder Id(ulong id)
        {
            FilterBuilder.Id(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Entity"/>
        public ref QueryBuilder Entity(ulong entity)
        {
            FilterBuilder.Entity(entity);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Name"/>
        public ref QueryBuilder Name(string name)
        {
            FilterBuilder.Name(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Var"/>
        public ref QueryBuilder Var(string name)
        {
            FilterBuilder.Var(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Flags"/>
        public ref QueryBuilder Flags(uint flags)
        {
            FilterBuilder.Flags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src()"/>
        public ref QueryBuilder Src()
        {
            FilterBuilder.Src();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First()"/>
        public ref QueryBuilder First()
        {
            FilterBuilder.First();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second()"/>
        public ref QueryBuilder Second()
        {
            FilterBuilder.Second();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(ulong)"/>
        public ref QueryBuilder Src(ulong srcId)
        {
            FilterBuilder.Src(srcId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src{T}"/>
        public ref QueryBuilder Src<T>()
        {
            FilterBuilder.Src<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Src(string)"/>
        public ref QueryBuilder Src(string name)
        {
            FilterBuilder.Src(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(ulong)"/>
        public ref QueryBuilder First(ulong firstId)
        {
            FilterBuilder.First(firstId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First{T}"/>
        public ref QueryBuilder First<T>()
        {
            FilterBuilder.First<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.First(string)"/>
        public ref QueryBuilder First(string name)
        {
            FilterBuilder.First(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(ulong)"/>
        public ref QueryBuilder Second(ulong secondId)
        {
            FilterBuilder.Second(secondId);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second{T}"/>
        public ref QueryBuilder Second<T>()
        {
            FilterBuilder.Second<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Second(string)"/>
        public ref QueryBuilder Second(string secondName)
        {
            FilterBuilder.Second(secondName);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Role"/>
        public ref QueryBuilder Role(ulong role)
        {
            FilterBuilder.Role(role);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut(Flecs.NET.Bindings.Native.ecs_inout_kind_t)"/>
        public ref QueryBuilder InOut(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOut(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutStage"/>
        public ref QueryBuilder InOutStage(ecs_inout_kind_t inOut)
        {
            FilterBuilder.InOutStage(inOut);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write()"/>
        public ref QueryBuilder Write()
        {
            FilterBuilder.Write();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read()"/>
        public ref QueryBuilder Read()
        {
            FilterBuilder.Read();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadWrite"/>
        public ref QueryBuilder ReadWrite()
        {
            FilterBuilder.ReadWrite();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.In"/>
        public ref QueryBuilder In()
        {
            FilterBuilder.In();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Out"/>
        public ref QueryBuilder Out()
        {
            FilterBuilder.Out();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOut()"/>
        public ref QueryBuilder InOut()
        {
            FilterBuilder.InOut();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.InOutNone"/>
        public ref QueryBuilder InOutNone()
        {
            FilterBuilder.InOutNone();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Oper"/>
        public ref QueryBuilder Oper(ecs_oper_kind_t oper)
        {
            FilterBuilder.Oper(oper);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.And"/>
        public ref QueryBuilder And()
        {
            FilterBuilder.And();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Or"/>
        public ref QueryBuilder Or()
        {
            FilterBuilder.Or();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Not"/>
        public ref QueryBuilder Not()
        {
            FilterBuilder.Not();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Optional"/>
        public ref QueryBuilder Optional()
        {
            FilterBuilder.Optional();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.AndFrom"/>
        public ref QueryBuilder AndFrom()
        {
            FilterBuilder.AndFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.OrFrom"/>
        public ref QueryBuilder OrFrom()
        {
            FilterBuilder.OrFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.NotFrom"/>
        public ref QueryBuilder NotFrom()
        {
            FilterBuilder.NotFrom();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Singleton"/>
        public ref QueryBuilder Singleton()
        {
            FilterBuilder.Singleton();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Filter"/>
        public ref QueryBuilder Filter()
        {
            FilterBuilder.Filter();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Instanced"/>
        public ref QueryBuilder Instanced()
        {
            FilterBuilder.Instanced();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.FilterFlags"/>
        public ref QueryBuilder FilterFlags(uint flags)
        {
            FilterBuilder.FilterFlags(flags);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Expr"/>
        public ref QueryBuilder Expr(string expr)
        {
            FilterBuilder.Expr(expr);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong)"/>
        public ref QueryBuilder With(ulong id)
        {
            FilterBuilder.With(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, ulong)"/>
        public ref QueryBuilder With(ulong first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(ulong, string)"/>
        public ref QueryBuilder With(ulong first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, ulong)"/>
        public ref QueryBuilder With(string first, ulong second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With(string, string)"/>
        public ref QueryBuilder With(string first, string second)
        {
            FilterBuilder.With(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}()"/>
        public ref QueryBuilder With<T>()
        {
            FilterBuilder.With<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(T)"/>
        public ref QueryBuilder With<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.With(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(ulong)"/>
        public ref QueryBuilder With<TFirst>(ulong second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T}(string)"/>
        public ref QueryBuilder With<TFirst>(string second)
        {
            FilterBuilder.With<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}()"/>
        public ref QueryBuilder With<TFirst, TSecond>()
        {
            FilterBuilder.With<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.With{T1, T2}(T2)"/>
        public ref QueryBuilder With<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.With<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(ulong)"/>
        public ref QueryBuilder WithSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithSecond{T}(string)"/>
        public ref QueryBuilder WithSecond<TSecond>(string first)
        {
            FilterBuilder.WithSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong)"/>
        public ref QueryBuilder Without(ulong id)
        {
            FilterBuilder.Without(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, ulong)"/>
        public ref QueryBuilder Without(ulong first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(ulong, string)"/>
        public ref QueryBuilder Without(ulong first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, ulong)"/>
        public ref QueryBuilder Without(string first, ulong second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without(string, string)"/>
        public ref QueryBuilder Without(string first, string second)
        {
            FilterBuilder.Without(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}()"/>
        public ref QueryBuilder Without<T>()
        {
            FilterBuilder.Without<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(T)"/>
        public ref QueryBuilder Without<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Without(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(ulong)"/>
        public ref QueryBuilder Without<TFirst>(ulong second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T}(string)"/>
        public ref QueryBuilder Without<TFirst>(string second)
        {
            FilterBuilder.Without<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}()"/>
        public ref QueryBuilder Without<TFirst, TSecond>()
        {
            FilterBuilder.Without<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Without{T1, T2}(T2)"/>
        public ref QueryBuilder Without<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Without<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(ulong)"/>
        public ref QueryBuilder WithoutSecond<TSecond>(ulong first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WithoutSecond{T}(string)"/>
        public ref QueryBuilder WithoutSecond<TSecond>(string first)
        {
            FilterBuilder.WithoutSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong)"/>
        public ref QueryBuilder Write(ulong id)
        {
            FilterBuilder.Write(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, ulong)"/>
        public ref QueryBuilder Write(ulong first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(ulong, string)"/>
        public ref QueryBuilder Write(ulong first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, ulong)"/>
        public ref QueryBuilder Write(string first, ulong second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write(string, string)"/>
        public ref QueryBuilder Write(string first, string second)
        {
            FilterBuilder.Write(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}()"/>
        public ref QueryBuilder Write<T>()
        {
            FilterBuilder.Write<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(T)"/>
        public ref QueryBuilder Write<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Write(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(ulong)"/>
        public ref QueryBuilder Write<TFirst>(ulong second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T}(string)"/>
        public ref QueryBuilder Write<TFirst>(string second)
        {
            FilterBuilder.Write<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}()"/>
        public ref QueryBuilder Write<TFirst, TSecond>()
        {
            FilterBuilder.Write<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Write{T1, T2}(T2)"/>
        public ref QueryBuilder Write<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Write<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(ulong)"/>
        public ref QueryBuilder WriteSecond<TSecond>(ulong first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.WriteSecond{T}(string)"/>
        public ref QueryBuilder WriteSecond<TSecond>(string first)
        {
            FilterBuilder.WriteSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong)"/>
        public ref QueryBuilder Read(ulong id)
        {
            FilterBuilder.Read(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, ulong)"/>
        public ref QueryBuilder Read(ulong first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(ulong, string)"/>
        public ref QueryBuilder Read(ulong first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, ulong)"/>
        public ref QueryBuilder Read(string first, ulong second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read(string, string)"/>
        public ref QueryBuilder Read(string first, string second)
        {
            FilterBuilder.Read(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}()"/>
        public ref QueryBuilder Read<T>()
        {
            FilterBuilder.Read<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(T)"/>
        public ref QueryBuilder Read<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Read(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(ulong)"/>
        public ref QueryBuilder Read<TFirst>(ulong second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T}(string)"/>
        public ref QueryBuilder Read<TFirst>(string second)
        {
            FilterBuilder.Read<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}()"/>
        public ref QueryBuilder Read<TFirst, TSecond>()
        {
            FilterBuilder.Read<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Read{T1, T2}(T2)"/>
        public ref QueryBuilder Read<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Read<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(ulong)"/>
        public ref QueryBuilder ReadSecond<TSecond>(ulong first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ReadSecond{T}(string)"/>
        public ref QueryBuilder ReadSecond<TSecond>(string first)
        {
            FilterBuilder.ReadSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeOpen"/>
        public ref QueryBuilder ScopeOpen()
        {
            FilterBuilder.ScopeOpen();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.ScopeClose"/>
        public ref QueryBuilder ScopeClose()
        {
            FilterBuilder.ScopeClose();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.IncrementTerm"/>
        public ref QueryBuilder IncrementTerm()
        {
            FilterBuilder.IncrementTerm();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermAt"/>
        public ref QueryBuilder TermAt(int termIndex)
        {
            FilterBuilder.TermAt(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Arg"/>
        public ref QueryBuilder Arg(int termIndex)
        {
            FilterBuilder.Arg(termIndex);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term()"/>
        public ref QueryBuilder Term()
        {
            FilterBuilder.Term();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(Core.Term)"/>
        public ref QueryBuilder Term(Term term)
        {
            FilterBuilder.Term(term);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong)"/>
        public ref QueryBuilder Term(ulong id)
        {
            FilterBuilder.Term(id);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string)"/>
        public ref QueryBuilder Term(string name)
        {
            FilterBuilder.Term(name);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, ulong)"/>
        public ref QueryBuilder Term(ulong first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(ulong, string)"/>
        public ref QueryBuilder Term(ulong first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, ulong)"/>
        public ref QueryBuilder Term(string first, ulong second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term(string, string)"/>
        public ref QueryBuilder Term(string first, string second)
        {
            FilterBuilder.Term(first, second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}()"/>
        public ref QueryBuilder Term<T>()
        {
            FilterBuilder.Term<T>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(T)"/>
        public ref QueryBuilder Term<TEnum>(TEnum enumMember) where TEnum : Enum
        {
            FilterBuilder.Term(enumMember);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(ulong)"/>
        public ref QueryBuilder Term<TFirst>(ulong second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T}(string)"/>
        public ref QueryBuilder Term<TFirst>(string second)
        {
            FilterBuilder.Term<TFirst>(second);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}()"/>
        public ref QueryBuilder Term<TFirst, TSecond>()
        {
            FilterBuilder.Term<TFirst, TSecond>();
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.Term{T1, T2}(T2)"/>
        public ref QueryBuilder Term<TFirst, TSecondEnum>(TSecondEnum secondEnum) where TSecondEnum : Enum
        {
            FilterBuilder.Term<TFirst, TSecondEnum>(secondEnum);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(ulong)"/>
        public ref QueryBuilder TermSecond<TSecond>(ulong first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }

        /// <inheritdoc cref="Core.FilterBuilder.TermSecond{T}(string)"/>
        public ref QueryBuilder TermSecond<TSecond>(string first)
        {
            FilterBuilder.TermSecond<TSecond>(first);
            return ref this;
        }
    }
}
