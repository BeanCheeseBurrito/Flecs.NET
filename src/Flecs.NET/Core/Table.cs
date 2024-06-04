using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A table is where entities and components are stored.
    /// </summary>
    public readonly unsafe partial struct Table : IEquatable<Table>
    {
        /// <summary>
        ///     A reference to the world.
        /// </summary>
        public ecs_world_t* World { get; }

        /// <summary>
        ///     A reference to the handle.
        /// </summary>
        public ecs_table_t* Handle { get; }

        /// <summary>
        ///     The offset from the start of the table.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        ///     The number of column indexes in the table range.
        /// </summary>
        public int Count { get; }

        /// <summary>
        ///     Creates a table from the provided world and handle.
        /// </summary>
        /// <param name="world"></param>
        public Table(ecs_world_t* world)
        {
            World = world;
            Handle = null;
            Offset = 0;
            Count = 0;
        }

        /// <summary>
        ///     Creates a table from the provided handle.
        /// </summary>
        /// <param name="table"></param>
        public Table(ecs_table_t* table)
        {
            World = null;
            Handle = table;
            Offset = 0;
            Count = 0;
        }

        /// <summary>
        ///     Creates a table from the provided world and handle.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="table"></param>
        public Table(ecs_world_t* world, ecs_table_t* table)
        {
            World = world;
            Handle = table;
            Offset = 0;
            Count = table == null ? 0 : ecs_table_count(table);
        }

        /// <summary>
        ///     Creates a table range form the provided world and handle.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="table"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public Table(ecs_world_t* world, ecs_table_t* table, int offset, int count)
        {
            World = world;
            Handle = table;
            Offset = offset;
            Count = count;
        }

        /// <summary>
        ///     Convert table type to string.
        /// </summary>
        /// <returns></returns>
        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_table_str(World, Handle));
        }

        /// <summary>
        ///     Get table type.
        /// </summary>
        /// <returns></returns>
        public Types Type()
        {
            return new Types(World, ecs_table_get_type(Handle));
        }

        /// <summary>
        ///     Find type index for (component) id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int TypeIndex(ulong id)
        {
            return ecs_table_get_type_index(World, Handle, id);
        }

        /// <summary>
        ///     Find type index for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public int TypeIndex(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return TypeIndex(pair);
        }

        /// <summary>
        ///     Find type index for type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int TypeIndex<T>()
        {
            return TypeIndex(Type<T>.Id(World));
        }

        /// <summary>
        ///     Find type index for type.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int TypeIndex<T>(T value) where T : Enum
        {
            return TypeIndex<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Find type index for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public int TypeIndex<TFirst>(ulong second)
        {
            return TypeIndex(Type<TFirst>.Id(World), second);
        }

        /// <summary>
        ///     Find type index for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int TypeIndex<TFirst, TSecond>()
        {
            return TypeIndex(Type<TFirst>.Id(World), Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Find type index for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int TypeIndex<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return TypeIndex<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Find type index for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int TypeIndex<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return TypeIndexSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Find type index for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int TypeIndexSecond<TSecond>(ulong first)
        {
            return TypeIndex(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Find column index for (component) id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ColumnIndex(ulong id)
        {
            return ecs_table_get_column_index(World, Handle, id);
        }

        /// <summary>
        ///     Find column index for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public int ColumnIndex(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ColumnIndex(pair);
        }

        /// <summary>
        ///     Find column index for component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<T>()
        {
            return ColumnIndex(Type<T>.Id(World));
        }

        /// <summary>
        ///     Find column index for component.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<T>(T value) where T : Enum
        {
            return ColumnIndex<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Find column index for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ColumnIndex(pair);
        }

        /// <summary>
        ///     Find column index for pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ColumnIndex(pair);
        }

        /// <summary>
        ///     Find column index for pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return ColumnIndex<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Find column index for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return ColumnIndexSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Find column index for pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int ColumnIndexSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ColumnIndex(pair);
        }

        /// <summary>
        ///     Test if table has (component) id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Has(ulong id)
        {
            return TypeIndex(id) != -1;
        }

        /// <summary>
        ///     Test if table has the pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool Has(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return TypeIndex(pair) != -1;
        }

        /// <summary>
        ///     Test if table has the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>()
        {
            return TypeIndex<T>() != -1;
        }

        /// <summary>
        ///     Test if table has the type.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>(T value) where T : Enum
        {
            return Has<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Test if table has the pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst>(ulong second)
        {
            return TypeIndex<TFirst>(second) != -1;
        }

        /// <summary>
        ///     Test if table has the pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>()
        {
            return TypeIndex<TFirst, TSecond>() != -1;
        }

        /// <summary>
        ///     Test if table has the pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Has<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Test if table has the pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool Has<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return HasSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Test if table has the pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public bool HasSecond<TSecond>(ulong first)
        {
            return TypeIndexSecond<TSecond>(first) != -1;
        }

        /// <summary>
        ///     Get pointer to component array by column index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void* GetColumn(int index)
        {
            return ecs_table_get_column(Handle, index, Offset);
        }

        /// <summary>
        ///     Get pointer to component array by component.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void* GetPtr(ulong id)
        {
            int index = ColumnIndex(id);
            return index == -1 ? null : GetColumn(index);
        }

        /// <summary>
        ///     Get pointer to component array by component.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public void* GetPtr(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return GetPtr(pair);
        }

        /// <summary>
        ///     Get pointer to component array by component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T* GetPtr<T>() where T : unmanaged
        {
            Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (T*)GetPtr(Type<T>.Id(World));
        }

        /// <summary>
        ///     Get pointer to component array by pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst>(ulong second) where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetPtr(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get pointer to component array by pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetPtr<TFirst, TSecond>(TSecond second)
            where TFirst : unmanaged
            where TSecond : Enum
        {
            return GetPtr<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get pointer to component array by pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetPtr<TFirst, TSecond>(TFirst first)
            where TFirst : Enum
            where TSecond : unmanaged
        {
            return GetSecondPtr<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get pointer to component array by pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TFirst* GetFirstPtr<TFirst, TSecond>() where TFirst : unmanaged
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TFirst*)GetPtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get pointer to component array by pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TFirst, TSecond>() where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetPtr(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get pointer to component array by pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public TSecond* GetSecondPtr<TSecond>(ulong first) where TSecond : unmanaged
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return (TSecond*)GetPtr(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Get managed column to component array by component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Field<T> Get<T>()
        {
            Ecs.Assert(Type<T>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            return new Field<T>(GetPtr(Type<T>.Id(World)), ColumnSize(ColumnIndex<T>()));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Field<TFirst> Get<TFirst>(ulong second)
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst>(second, World);
            return new Field<TFirst>(GetPtr(pair), ColumnSize(ColumnIndex<TFirst>()));
        }

        /// <summary>
        ///    Get managed column to component array by pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Field<TFirst> Get<TFirst, TSecond>(TSecond second)
            where TFirst : unmanaged
            where TSecond : Enum
        {
            return Get<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Field<TSecond> Get<TFirst, TSecond>(TFirst first)
            where TFirst : Enum
            where TSecond : unmanaged
        {
            return GetSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Field<TFirst> GetFirst<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TFirst>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return new Field<TFirst>(GetPtr(pair), ColumnSize(ColumnIndex<TFirst, TSecond>()));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Field<TSecond> GetSecond<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return new Field<TSecond>(GetPtr(pair), ColumnSize(ColumnIndex<TFirst, TSecond>()));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Field<TSecond> GetSecond<TSecond>(ulong first)
        {
            Ecs.Assert(Type<TSecond>.Size != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return new Field<TSecond>(GetPtr(pair), ColumnSize(ColumnIndexSecond<TSecond>(first)));
        }

        /// <summary>
        ///     Get column size.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int ColumnSize(int index)
        {
            return (int)ecs_table_get_column_size(Handle, index);
        }

        /// <summary>
        ///     Get depth for given relationship.
        /// </summary>
        /// <param name="rel"></param>
        /// <returns></returns>
        public int Depth(ulong rel)
        {
            return ecs_table_get_depth(World, Handle, rel);
        }

        /// <summary>
        ///     Get depth for given relationship.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Depth<T>()
        {
            return Depth(Type<T>.Id(World));
        }

        /// <summary>
        ///     Get depth for given relationship.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Depth<T>(T value) where T : Enum
        {
            return Depth(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Get table.
        /// </summary>
        /// <returns>The table.</returns>
        public ecs_table_t* GetTable()
        {
            return Handle;
        }

        /// <summary>
        ///     Converts a <see cref="Table"/> instance to a <see cref="ecs_table_t"/>*.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static ecs_table_t* To(Table table)
        {
            return table.Handle;
        }

        /// <summary>
        ///     Converts a <see cref="ecs_table_t"/>* instance to a <see cref="Table"/>.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static Table From(ecs_table_t* table)
        {
            return new Table(table);
        }

        /// <summary>
        ///     Converts a <see cref="Table"/> instance to a <see cref="ecs_table_t"/>*.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static implicit operator ecs_table_t*(Table table)
        {
            return To(table);
        }

        /// <summary>
        ///     Converts a <see cref="ecs_table_t"/>* instance to a <see cref="Table"/>.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static implicit operator Table(ecs_table_t* table)
        {
            return From(table);
        }

        /// <summary>
        ///     Returns a string representation of the table.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Macros.IsStageOrWorld(World) ? Str() : string.Empty;
        }

        /// <summary>
        ///     Checks if two <see cref="Table"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Table other)
        {
            return Handle == other.Handle;
        }

        /// <summary>
        ///     Checks if two <see cref="Table"/> instance equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Table other && Equals(other);
        }

        /// <summary>
        ///     Returns the hash code of the <see cref="Table"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Handle->GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Table"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Table left, Table right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Table"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Table left, Table right)
        {
            return !(left == right);
        }
    }

    // Flecs.NET Extensions
    public readonly unsafe partial struct Table
    {
        /// <summary>
        ///     Get table that has all components of current table plus the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Table Add(ulong id)
        {
            return new Table(World, ecs_table_add_id(World, Handle, id));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Table Add(ulong first, ulong second)
        {
            return Add(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Table Add<T>()
        {
            return Add(Type<T>.Id(World));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Table Add<TFirst>(ulong second)
        {
            return Add(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Table Add<T>(T value) where T : Enum
        {
            return Add<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table Add<TFirst, TSecond>()
        {
            return Add(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table Add<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Add<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table Add<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return AddSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get table that has all components of current table plus the specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table AddSecond<TSecond>(ulong first)
        {
            return Add(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Table Remove(ulong id)
        {
            return new Table(World, ecs_table_remove_id(World, Handle, id));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Table Remove(ulong first, ulong second)
        {
            return Remove(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Table Remove<T>()
        {
            return Remove(Type<T>.Id(World));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public Table Remove<TFirst>(ulong second)
        {
            return Remove(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Table Remove<T>(T value) where T : Enum
        {
            return Remove<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table Remove<TFirst, TSecond>()
        {
            return Remove(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table Remove<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Remove<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table Remove<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return RemoveSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Get table that has all components of current table minus the specified pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Table RemoveSecond<TSecond>(ulong first)
        {
            return Remove(first, Type<TSecond>.Id(World));
        }

        /// <summary>
        ///     Search for id index in table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Search(ulong id)
        {
            return ecs_search(World, Handle, id, null);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public int Search(ulong first, ulong second)
        {
            return Search(Macros.Pair(first, second));
        }

        /// <summary>
        ///     Search for component index in table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Search<T>()
        {
            return Search(Type<T>.Id(World));
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public int Search<TFirst>(ulong second)
        {
            return Search(Macros.Pair<TFirst>(second, World));
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Search<T>(T value) where T : Enum
        {
            return Search<T>(Type<T>.Id(World, value));
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>()
        {
            return Search(Macros.Pair<TFirst, TSecond>(World));
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="second"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(TSecond second) where TSecond : Enum
        {
            return Search<TFirst>(Type<TSecond>.Id(World, second));
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(TFirst first) where TFirst : Enum
        {
            return SearchSecond<TSecond>(Type<TFirst>.Id(World, first));
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int SearchSecond<TSecond>(ulong first)
        {
            return Search(Macros.PairSecond<TSecond>(first, World));
        }

        /// <summary>
        ///     Search for id in table.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idOut"></param>
        /// <returns></returns>
        public int Search(ulong id, out ulong idOut)
        {
            fixed (ulong* ptr = &idOut)
            {
                return ecs_search(World, Handle, id, ptr);
            }
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="idOut"></param>
        /// <returns></returns>
        public int Search(ulong first, ulong second, out ulong idOut)
        {
            return Search(Macros.Pair(first, second), out idOut);
        }

        /// <summary>
        ///     Search for component index in table.
        /// </summary>
        /// <param name="idOut"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Search<T>(out ulong idOut)
        {
            return Search(Type<T>.Id(World), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="idOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public int Search<TFirst>(ulong second, out ulong idOut)
        {
            return Search(Macros.Pair<TFirst>(second, World), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="idOut"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Search<T>(T value, out ulong idOut) where T : Enum
        {
            return Search<T>(Type<T>.Id(World, value), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="idOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(out ulong idOut)
        {
            return Search(Macros.Pair<TFirst, TSecond>(World), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="inOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(TSecond second, out ulong inOut) where TSecond : Enum
        {
            return Search<TFirst>(Type<TSecond>.Id(World, second), out inOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="inOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(TFirst first, out ulong inOut) where TFirst : Enum
        {
            return SearchSecond<TSecond>(Type<TFirst>.Id(World, first), out inOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="idOut"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int SearchSecond<TSecond>(ulong first, out ulong idOut)
        {
            return Search(first, Type<TSecond>.Id(World), out idOut);
        }

        /// <summary>
        ///     Search for id in table.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idOut"></param>
        /// <returns></returns>
        public int Search(ulong id, out Id idOut)
        {
            idOut = new Id(World);
            return Search(id, out idOut.Value);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="idOut"></param>
        /// <returns></returns>
        public int Search(ulong first, ulong second, out Id idOut)
        {
            return Search(Macros.Pair(first, second), out idOut);
        }

        /// <summary>
        ///     Search for component index in table.
        /// </summary>
        /// <param name="idOut"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Search<T>(out Id idOut)
        {
            return Search(Type<T>.Id(World), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="idOut"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Search<T>(T value, out Id idOut) where T : Enum
        {
            return Search<T>(Type<T>.Id(World, value), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="idOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <returns></returns>
        public int Search<TFirst>(ulong second, out Id idOut)
        {
            return Search(Macros.Pair<TFirst>(second, World), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="idOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(out Id idOut)
        {
            return Search(Macros.Pair<TFirst, TSecond>(World), out idOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="second"></param>
        /// <param name="inOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(TSecond second, out Id inOut) where TSecond : Enum
        {
            return Search<TFirst>(Type<TSecond>.Id(World, second), out inOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="inOut"></param>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int Search<TFirst, TSecond>(TFirst first, out Id inOut) where TFirst : Enum
        {
            return SearchSecond<TSecond>(Type<TFirst>.Id(World, first), out inOut);
        }

        /// <summary>
        ///     Search for pair index in table.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="idOut"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public int SearchSecond<TSecond>(ulong first, out Id idOut)
        {
            return Search(first, Type<TSecond>.Id(World), out idOut);
        }
    }
}
