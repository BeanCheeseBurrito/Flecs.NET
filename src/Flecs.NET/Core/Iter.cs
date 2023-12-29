using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Class for iterating over query results.
    /// </summary>
    public readonly unsafe struct Iter : IEnumerable<int>, IEquatable<Iter>
    {
        /// <summary>
        ///     Reference to handle.
        /// </summary>
        public ecs_iter_t* Handle { get; }

        /// <summary>
        /// </summary>
        public int Begin { get; }

        /// <summary>
        /// </summary>
        public int End { get; }

        /// <summary>
        ///     Creates an iter wrapper using the provided handle.
        /// </summary>
        /// <param name="iter"></param>
        public Iter(ecs_iter_t* iter)
        {
            Handle = iter;
            Begin = 0;
            End = iter->count;
        }

        /// <summary>
        ///     Returns entity id of system.
        /// </summary>
        /// <returns></returns>
        public Entity System()
        {
            return new Entity(Handle->world, Handle->system);
        }

        /// <summary>
        ///     Returns entity id of event.
        /// </summary>
        /// <returns></returns>
        public Entity Event()
        {
            return new Entity(Handle->world, Handle->@event);
        }

        /// <summary>
        ///     Returns the entity id of the event id.
        /// </summary>
        /// <returns></returns>
        public Id EventId()
        {
            return new Id(Handle->world, Handle->event_id);
        }

        /// <summary>
        ///     Returns staged C# world.
        /// </summary>
        /// <returns></returns>
        public World World()
        {
            return new World(Handle->world, false);
        }

        /// <summary>
        ///     Returns count of iter.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Handle->count;
        }

        /// <summary>
        ///     Returns the delta time.
        /// </summary>
        /// <returns></returns>
        public float DeltaTime()
        {
            return Handle->delta_time;
        }

        /// <summary>
        ///     Returns the delta system time.
        /// </summary>
        /// <returns></returns>
        public float DeltaSystemTime()
        {
            return Handle->delta_system_time;
        }

        /// <summary>
        ///     Returns the type of the iterated table.
        /// </summary>
        /// <returns></returns>
        public Types Type()
        {
            return new Types(Handle->world, ecs_table_get_type(Handle->table));
        }

        /// <summary>
        ///     Returns table of the iter.
        /// </summary>
        /// <returns></returns>
        public Table Table()
        {
            return new Table(Handle->world, Handle->table);
        }

        /// <summary>
        ///     Returns table range of the iter.
        /// </summary>
        /// <returns></returns>
        public Table Range()
        {
            return new Table(Handle->world, Handle->table, Handle->offset, Handle->count);
        }

        /// <summary>
        ///     Returns context pointer.
        /// </summary>
        /// <returns></returns>
        public void* CtxPtr()
        {
            return Handle->ctx;
        }

        /// <summary>
        ///     Returns context pointer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* CtxPtr<T>() where T : unmanaged
        {
            return (T*)Handle->ctx;
        }

        /// <summary>
        ///     Returns context ref.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref T Ctx<T>() where T : unmanaged
        {
            return ref *CtxPtr<T>();
        }

        /// <summary>
        ///     Returns param pointer.
        /// </summary>
        /// <returns></returns>
        public void* ParamPtr()
        {
            return Handle->param;
        }

        /// <summary>
        ///     Returns param pointer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* ParamPtr<T>() where T : unmanaged
        {
            return (T*)Handle->param;
        }

        /// <summary>
        ///     Returns param ref.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ref T Param<T>() where T : unmanaged
        {
            return ref *ParamPtr<T>();
        }

        /// <summary>
        ///     Obtain mutable handle to entity being iterated over.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Entity Entity(int row)
        {
            Ecs.Assert(row < Handle->count, nameof(ECS_COLUMN_INDEX_OUT_OF_RANGE));
            return new Entity(Handle->world, Handle->entities[row]);
        }

        /// <summary>
        ///     Returns whether field is matched on self.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsSelf(int index)
        {
            return ecs_field_is_self(Handle, index) == 1;
        }

        /// <summary>
        ///     Returns whether field is set.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsSet(int index)
        {
            return ecs_field_is_set(Handle, index) == 1;
        }

        /// <summary>
        ///     Returns whether field is readonly.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsReadonly(int index)
        {
            return ecs_field_is_readonly(Handle, index) == 1;
        }

        /// <summary>
        ///     Number of fields in iterator.
        /// </summary>
        /// <returns></returns>
        public int FieldCount()
        {
            return Handle->field_count;
        }

        /// <summary>
        ///     Size of field data type.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ulong Size(int index)
        {
            return (ulong)ecs_field_size(Handle, index);
        }

        /// <summary>
        ///     Obtain field source (0 if This).
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Entity Src(int index)
        {
            return new Entity(Handle->world, ecs_field_src(Handle, index));
        }

        /// <summary>
        ///     Obtain id matched for field.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Id Id(int index)
        {
            return new Id(Handle->world, ecs_field_id(Handle, index));
        }

        /// <summary>
        ///     Obtain pair id matched for field.
        ///     This operation will fail if the id is not a pair.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Id Pair(int index)
        {
            ulong id = ecs_field_id(Handle, index);
            Ecs.Assert(Macros.EntityHasIdFlag(id, ECS_PAIR) != 0, nameof(ECS_INVALID_PARAMETER));
            return new Id(Handle->world, id);
        }

        /// <summary>
        ///     Obtain column index for field.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int ColumnIndex(int index)
        {
            return ecs_field_column_index(Handle, index);
        }

        /// <summary>
        ///     Convert current iterator result to string.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_iter_str(Handle));
        }

        /// <summary>
        ///     Get access to field data.
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Column<T> Field<T>(int index)
        {
            return GetField<T>(index);
        }

        /// <summary>
        ///     Get managed ref to the first element in a field.
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [SuppressMessage("Usage", "CA1720")]
        public ref T Single<T>(int index)
        {
            return ref GetField<T>(index)[0];
        }

        /// <summary>
        ///     Get access to entity ids.
        /// </summary>
        /// <returns></returns>
        public Column<ulong> Entities()
        {
            return new Column<ulong>(Handle->entities, Handle->count);
        }

        /// <summary>
        ///     Obtain the total number of tables the iterator will iterate over.
        /// </summary>
        /// <returns></returns>
        public int TableCount()
        {
            return Handle->table_count;
        }

        /// <summary>
        ///     Check if the current table has changed since the last iteration.
        ///     Can only be used when iterating queries and/or systems.
        /// </summary>
        /// <returns></returns>
        public bool Changed()
        {
            return ecs_query_changed(null, Handle) == 1;
        }

        /// <summary>
        ///     Skip current table.
        ///     This indicates to the query that the data in the current table is not
        ///     modified. By default, iterating a table with a query will mark the
        ///     iterated components as dirty if they are annotated with InOut or Out.
        /// </summary>
        public void Skip()
        {
            ecs_query_skip(Handle);
        }

        /// <summary>
        ///     Return group id for current table (grouped queries only)
        /// </summary>
        /// <returns></returns>
        public ulong GroupId()
        {
            return Handle->group_id;
        }

        /// <summary>
        ///     Get value of variable by id.
        /// </summary>
        /// <param name="varId"></param>
        /// <returns></returns>
        public Entity GetVar(int varId)
        {
            Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));
            return new Entity(Handle->world, ecs_iter_get_var(Handle, varId));
        }

        /// <summary>
        ///     Get value of variable by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entity GetVar(string name)
        {
            ecs_rule_iter_t* ruleIter = &Handle->priv.iter.rule;
            ecs_rule_t* rule = ruleIter->rule;

            using NativeString nativeName = (NativeString)name;
            int varId = ecs_rule_find_var(rule, nativeName);
            Ecs.Assert(varId != -1, nameof(ECS_INVALID_PARAMETER));

            return new Entity(Handle->world, ecs_iter_get_var(Handle, varId));
        }

        private Column<T> GetField<T>(int index)
        {
            AssertFieldId<T>(Handle, index);

            bool isShared = ecs_field_is_self(Handle, index) == 0;
            int count = isShared ? 1 : Handle->count;

            void* ptr = ecs_field_w_size(Handle, (IntPtr)Managed.ManagedSize<T>(), index);
            return new Column<T>(ptr, count, isShared);
        }

        [Conditional("DEBUG")]
        internal static void AssertFieldId<T>(ecs_iter_t* iter, int index)
        {
            ulong termId = ecs_field_id(iter, index);
            ulong typeId = Type<T>.Id(iter->world);

            if (termId == typeId)
                return;

            ulong termTypeid = ecs_get_typeid(iter->world, termId);

            if (termTypeid == typeId)
                return;

            if (typeof(T) == typeof(ulong) &&
                (ecs_id_is_tag(iter->world, termId) == Macros.True ||
                 ecs_id_is_union(iter->world, termId) == Macros.True))
            {
                return;
            }

            Entity expected = new Entity(iter->world, termId);
            Entity actual = new Entity(iter->world, typeId);
            Ecs.Error(
                $"Type argument mismatch at term index {index}.\nExpected Term: {expected}\nActual Term: {actual}");
        }

        /// <summary>
        ///     Gets an enumerator for iter.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Gets an enumerator for iter.
        /// </summary>
        /// <returns></returns>
        public IterEnumerator GetEnumerator()
        {
            return new IterEnumerator(Handle->count);
        }

        /// <summary>
        ///     Checks if two <see cref="Iter"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Iter other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="Iter"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Iter other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of te <see cref="Iter"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Iter"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Iter left, Iter right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Iter"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Iter left, Iter right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    ///     Enumerator for iters.
    /// </summary>
    public struct IterEnumerator : IEnumerator<int>
    {
        /// <summary>
        ///     Length of the iter.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Current index of the iter.
        /// </summary>
        public int Current { get; private set; }

        /// <summary>
        ///     Current index of the iter.
        /// </summary>
        readonly object IEnumerator.Current => Current;

        /// <summary>
        ///     Create a new iter with the provided length.
        /// </summary>
        /// <param name="length"></param>
        public IterEnumerator(int length)
        {
            Length = length;
            Current = -1;
        }

        /// <summary>
        ///     Moves to the next index of the iter.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            Current++;
            return Current < Length;
        }

        /// <summary>
        ///     Resets the index of the enumerator.
        /// </summary>
        public void Reset()
        {
            Current = -1;
        }

        /// <summary>
        ///     Disposes of resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
