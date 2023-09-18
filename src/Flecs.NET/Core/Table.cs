using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     A table is where entities and components are stored.
    /// </summary>
    public readonly unsafe struct Table
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
        /// <param name="table"></param>
        public Table(ecs_world_t* world, ecs_table_t* table)
        {
            World = world;
            Handle = table;
            Offset = 0;
            Count = ecs_table_count(table);
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
        public Types Types()
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
        ///     Find column index for type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int ColumnIndex<T>()
        {
            return ColumnIndex(Type<T>.Id(World));
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
        ///     Get managed column to component array by component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Column<T> Get<T>()
        {
            Ecs.Assert(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return new Column<T>(GetPtr(Type<T>.Id(World)), ColumnSize(ColumnIndex<T>()));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Column<TFirst> GetFirst<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return new Column<TFirst>(GetPtr(pair), ColumnSize(ColumnIndex<TFirst, TSecond>()));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Column<TSecond> GetSecond<TFirst, TSecond>()
        {
            Ecs.Assert(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return new Column<TSecond>(GetPtr(pair), ColumnSize(ColumnIndex<TFirst, TSecond>()));
        }

        /// <summary>
        ///     Get managed column to component array by pair.
        /// </summary>
        /// <param name="first"></param>
        /// <typeparam name="TSecond"></typeparam>
        /// <returns></returns>
        public Column<TSecond> GetSecond<TSecond>(ulong first)
        {
            Ecs.Assert(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return new Column<TSecond>(GetPtr(pair), ColumnSize(ColumnIndexSecond<TSecond>(first)));
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
        ///     Returns a string representation of the table.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Str();
        }
    }
}
