using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public readonly unsafe struct Table
    {
        public ecs_world_t* World { get; }
        public ecs_table_t* Handle { get; }

        public Table(ecs_world_t* world, ecs_table_t* table)
        {
            World = world;
            Handle = table;
        }

        public string Str()
        {
            return NativeString.GetStringAndFree(ecs_table_str(World, Handle));
        }

        public Types Types()
        {
            return new Types(World, ecs_table_get_type(Handle));
        }

        public int Count()
        {
            return ecs_table_count(Handle);
        }

        public int TypeIndex(ulong id)
        {
            return ecs_table_get_type_index(World, Handle, id);
        }

        public int TypeIndex(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return TypeIndex(pair);
        }

        public int TypeIndex<T>()
        {
            return TypeIndex(Type<T>.Id(World));
        }

        public int TypeIndex<TFirst>(ulong second)
        {
            return TypeIndex(Type<TFirst>.Id(World), second);
        }

        public int TypeIndex<TFirst, TSecond>()
        {
            return TypeIndex(Type<TFirst>.Id(World), Type<TSecond>.Id(World));
        }

        public int TypeIndexSecond<TSecond>(ulong first)
        {
            return TypeIndex(first, Type<TSecond>.Id(World));
        }

        public int ColumnIndex(ulong id)
        {
            return ecs_table_get_column_index(World, Handle, id);
        }

        public int ColumnIndex(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return ColumnIndex(pair);
        }

        public int ColumnIndex<T>()
        {
            return ColumnIndex(Type<T>.Id(World));
        }

        public int ColumnIndex<TFirst>(ulong second)
        {
            ulong pair = Macros.Pair<TFirst>(second, World);
            return ColumnIndex(pair);
        }

        public int ColumnIndex<TFirst, TSecond>()
        {
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ColumnIndex(pair);
        }

        public int ColumnIndexSecond<TSecond>(ulong first)
        {
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ColumnIndex(pair);
        }

        public bool Has(ulong id)
        {
            return TypeIndex(id) != -1;
        }

        public bool Has(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return TypeIndex(pair) != -1;
        }

        public bool Has<T>()
        {
            return TypeIndex<T>() != -1;
        }

        public bool Has<TFirst>(ulong second)
        {
            return TypeIndex<TFirst>(second) != -1;
        }

        public bool Has<TFirst, TSecond>()
        {
            return TypeIndex<TFirst, TSecond>() != -1;
        }

        public bool HasSecond<TSecond>(ulong first)
        {
            return TypeIndexSecond<TSecond>(first) != -1;
        }

        public void* GetColumn(int index)
        {
            return ecs_table_get_column(Handle, index, 0);
        }

        public void* GetPtr(ulong id)
        {
            int index = ColumnIndex(id);
            return index == -1 ? null : GetColumn(index);
        }

        public void* GetPtr(ulong first, ulong second)
        {
            ulong pair = Macros.Pair(first, second);
            return GetPtr(pair);
        }

        public ref T Get<T>()
        {
            Assert.True(Type<T>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            return ref Managed.GetTypeRef<T>(GetPtr(Type<T>.Id(World)));
        }

        public ref TFirst GetFirst<TFirst, TSecond>()
        {
            Assert.True(Type<TFirst>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref Managed.GetTypeRef<TFirst>(GetPtr(pair));
        }

        public ref TSecond GetSecond<TFirst, TSecond>()
        {
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.Pair<TFirst, TSecond>(World);
            return ref Managed.GetTypeRef<TSecond>(GetPtr(pair));
        }

        public ref TSecond GetSecond<TSecond>(ulong first)
        {
            Assert.True(Type<TSecond>.GetSize() != 0, nameof(ECS_INVALID_PARAMETER));
            ulong pair = Macros.PairSecond<TSecond>(first, World);
            return ref Managed.GetTypeRef<TSecond>(GetPtr(pair));
        }

        public ulong ColumnSize(int index)
        {
            return ecs_table_get_column_size(Handle, index);
        }

        public int Depth(ulong rel)
        {
            return ecs_table_get_depth(World, Handle, rel);
        }

        public int Depth<T>()
        {
            return Depth(Type<T>.Id(World));
        }
    }
}