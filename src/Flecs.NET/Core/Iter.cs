using System.Collections;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public readonly unsafe struct Iter : IEnumerable
    {
        public ecs_iter_t* Handle { get; }
        public int Begin { get; }
        public int End { get; }

        public Iter(ecs_iter_t* iter)
        {
            Handle = iter;
            Begin = 0;
            End = iter->count;
        }

        public Entity System()
        {
            return new Entity(Handle->world, Handle->system);
        }

        public Entity Event()
        {
            return new Entity(Handle->world, Handle->@event);
        }

        public Entity EventId()
        {
            return new Entity(Handle->world, Handle->event_id);
        }

        public World World()
        {
            return new World(Handle->world, false);
        }

        public ecs_iter_t* CPtr()
        {
            return Handle;
        }

        public int Count()
        {
            return (Handle->count);
        }

        public float DeltaTime()
        {
            return Handle->delta_time;
        }

        public float DeltaSystemTime()
        {
            return Handle->delta_system_time;
        }

        public Types Types()
        {
            return new Types(Handle->world, ecs_table_get_type(Handle->table));
        }

        public Table Table()
        {
            return new Table(Handle->world, Handle->table);
        }

        // public bool HasModule()
        // {
        //     return ecs_table_has_module(Handle->table) == 1;
        // }

        public void* CtxPtr()
        {
            return Handle->ctx;
        }

        public T* CtxPtr<T>() where T : unmanaged
        {
            return (T*)Handle->ctx;
        }

        public ref T Ctx<T>() where T : unmanaged
        {
            return ref *CtxPtr<T>();
        }

        public void* ParamPtr()
        {
            return Handle->param;
        }

        public T* ParamPtr<T>() where T : unmanaged
        {
            return (T*)Handle->param;
        }

        public ref T Param<T>() where T : unmanaged
        {
            return ref *ParamPtr<T>();
        }

        public Entity Entity(int row)
        {
            Assert.True(row < Handle->count, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
            return new Entity(Handle->world, Handle->entities[row]);
        }

        public bool IsSelf(int index)
        {
            return ecs_field_is_self(Handle, index) == 1;
        }

        public bool IsSet(int index)
        {
            return ecs_field_is_set(Handle, index) == 1;
        }

        public bool IsReadonly(int index)
        {
            return ecs_field_is_readonly(Handle, index) == 1;
        }

        public int FieldCount()
        {
            return Handle->field_count;
        }

        public ulong Size(int index)
        {
            return ecs_field_size(Handle, index);
        }

        public Entity Src(int index)
        {
            return new Entity(Handle->world, ecs_field_src(Handle, index));
        }

        public Entity Id(int index)
        {
            return new Entity(Handle->world, ecs_field_id(Handle, index));
        }

        public Entity Pair(int index)
        {
            ulong id = ecs_field_id(Handle, index);
            Assert.True(Macros.EntityHasIdFlag(id, ECS_PAIR) != 0, nameof(ECS_INVALID_PARAMETER));
            return new Entity(Handle->world, id);
        }

        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_iter_str(Handle));
        }

        public Column<T> Field<T>(int index)
        {
            return GetField<T>(index);
        }

        public int TableCount()
        {
            return Handle->table_count;
        }

        public bool Changed()
        {
            return ecs_query_changed(null, Handle) == 1;
        }

        public void Skip()
        {
            ecs_query_skip(Handle);
        }

        public ulong GroupId()
        {
            return Handle->group_id;
        }

        public Entity GetVar(int varId)
        {
            Assert.True(varId != -1, nameof(ECS_INVALID_PARAMETER));
            return new Entity(Handle->world, ecs_iter_get_var(Handle, varId));
        }

        public Entity GetVar(string name)
        {
            ecs_rule_iter_t* ruleIter = &Handle->priv.iter.rule;
            ecs_rule_t* rule = ruleIter->rule;

            using NativeString nativeName = (NativeString)name;
            int varId = ecs_rule_find_var(rule, nativeName);
            Assert.True(varId != -1, nameof(ECS_INVALID_PARAMETER));

            return new Entity(Handle->world, ecs_iter_get_var(Handle, varId));
        }

        private Column<T> GetField<T>(int index)
        {
#if DEBUG
            ulong termId = ecs_field_id(Handle, index);
            Assert.True(Macros.EntityHasIdFlag(termId, ECS_PAIR) != 0 || termId == Type<T>.Id(Handle->world),
                nameof(ECS_COLUMN_TYPE_MISMATCH));
#endif
            bool isShared = ecs_field_is_self(Handle, index) == 0;
            int count = isShared ? 1 : Handle->count;

            void* ptr = ecs_field_w_size(Handle, (ulong)Managed.ManagedSize<T>(), index);
            return new Column<T>(ptr, count, isShared);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IterEnumerator GetEnumerator()
        {
            return new IterEnumerator(Handle->count);
        }
    }

    public struct IterEnumerator : IEnumerator
    {
        public int Length { get; }
        public int Current { get; private set; }

        readonly object IEnumerator.Current => Current;

        public IterEnumerator(int length)
        {
            Length = length;
            Current = -1;
        }

        public bool MoveNext()
        {
            Current++;
            return Current < Length;
        }

        public void Reset()
        {
            Current = -1;
        }
    }
}