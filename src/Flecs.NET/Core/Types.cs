using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Flecs.NET.Core;

[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
internal static unsafe class Types<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
    public static readonly int Count = InitCount();
    public static readonly string[] TypeNames = InitTypeNames();
    public static readonly int Tags = InitTags();
    public static readonly int ReferenceTypes = InitReferenceTypes();

    private static string GetTypeListString(int fields)
    {
        return string.Join(", ", Enumerable.Range(0, Count)
            .Where(i => (fields & (1 << i)) != 0)
            .Select(i => TypeNames[i]));
    }

    private static string[] InitTypeNames()
    {
        string[] value = new string[Count];

        if (typeof(T0) != typeof(_))
            value[0] = Type<T0>.FullName;

        if (typeof(T1) != typeof(_))
            value[1] = Type<T1>.FullName;

        if (typeof(T2) != typeof(_))
            value[2] = Type<T2>.FullName;

        if (typeof(T3) != typeof(_))
            value[3] = Type<T3>.FullName;

        if (typeof(T4) != typeof(_))
            value[4] = Type<T4>.FullName;

        if (typeof(T5) != typeof(_))
            value[5] = Type<T5>.FullName;

        if (typeof(T6) != typeof(_))
            value[6] = Type<T6>.FullName;

        if (typeof(T7) != typeof(_))
            value[7] = Type<T7>.FullName;

        if (typeof(T8) != typeof(_))
            value[8] = Type<T8>.FullName;

        if (typeof(T9) != typeof(_))
            value[9] = Type<T9>.FullName;

        if (typeof(T10) != typeof(_))
            value[10] = Type<T10>.FullName;

        if (typeof(T11) != typeof(_))
            value[11] = Type<T11>.FullName;

        if (typeof(T12) != typeof(_))
            value[12] = Type<T12>.FullName;

        if (typeof(T13) != typeof(_))
            value[13] = Type<T13>.FullName;

        if (typeof(T14) != typeof(_))
            value[14] = Type<T14>.FullName;

        if (typeof(T15) != typeof(_))
            value[15] = Type<T15>.FullName;

        return value;
    }

    private static int InitCount()
    {
        int value = 0;

        if (typeof(T0) != typeof(_))
            value++;

        if (typeof(T1) != typeof(_))
            value++;

        if (typeof(T2) != typeof(_))
            value++;

        if (typeof(T3) != typeof(_))
            value++;

        if (typeof(T4) != typeof(_))
            value++;

        if (typeof(T5) != typeof(_))
            value++;

        if (typeof(T6) != typeof(_))
            value++;

        if (typeof(T7) != typeof(_))
            value++;

        if (typeof(T8) != typeof(_))
            value++;

        if (typeof(T9) != typeof(_))
            value++;

        if (typeof(T10) != typeof(_))
            value++;

        if (typeof(T11) != typeof(_))
            value++;

        if (typeof(T12) != typeof(_))
            value++;

        if (typeof(T13) != typeof(_))
            value++;

        if (typeof(T14) != typeof(_))
            value++;

        if (typeof(T15) != typeof(_))
            value++;

        return value;
    }

    private static int InitTags()
    {
        int value = 0;

        if (typeof(T0) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 0 : 0;

        if (typeof(T1) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 1 : 0;

        if (typeof(T2) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 2 : 0;

        if (typeof(T3) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 3 : 0;

        if (typeof(T4) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 4 : 0;

        if (typeof(T5) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 5 : 0;

        if (typeof(T6) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 6 : 0;

        if (typeof(T7) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 7 : 0;

        if (typeof(T8) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 8 : 0;

        if (typeof(T9) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 9 : 0;

        if (typeof(T10) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 10 : 0;

        if (typeof(T11) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 11 : 0;

        if (typeof(T12) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 12 : 0;

        if (typeof(T13) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 13 : 0;

        if (typeof(T14) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 14 : 0;

        if (typeof(T15) != typeof(_))
            value |= Type<T0>.IsTag ? 1 << 15 : 0;

        return value;
    }

    private static int InitReferenceTypes()
    {
        int value = 0;

        if (typeof(T0) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 0 : 0;

        if (typeof(T1) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 1 : 0;

        if (typeof(T2) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 2 : 0;

        if (typeof(T3) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 3 : 0;

        if (typeof(T4) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 4 : 0;

        if (typeof(T5) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 5 : 0;

        if (typeof(T6) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 6 : 0;

        if (typeof(T7) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 7 : 0;

        if (typeof(T8) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 8 : 0;

        if (typeof(T9) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 9 : 0;

        if (typeof(T10) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 10 : 0;

        if (typeof(T11) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 11 : 0;

        if (typeof(T12) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 12 : 0;

        if (typeof(T13) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 13 : 0;

        if (typeof(T14) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 14 : 0;

        if (typeof(T15) != typeof(_))
            value |= RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 15 : 0;

        return value;
    }

    internal static void Ids(World world, ulong* ids)
    {
        if (typeof(T0) != typeof(_))
            ids[0] = Type<T0>.Id(world);

        if (typeof(T1) != typeof(_))
            ids[1] = Type<T1>.Id(world);

        if (typeof(T2) != typeof(_))
            ids[2] = Type<T2>.Id(world);

        if (typeof(T3) != typeof(_))
            ids[3] = Type<T3>.Id(world);

        if (typeof(T4) != typeof(_))
            ids[4] = Type<T4>.Id(world);

        if (typeof(T5) != typeof(_))
            ids[5] = Type<T5>.Id(world);

        if (typeof(T6) != typeof(_))
            ids[6] = Type<T6>.Id(world);

        if (typeof(T7) != typeof(_))
            ids[7] = Type<T7>.Id(world);

        if (typeof(T8) != typeof(_))
            ids[8] = Type<T8>.Id(world);

        if (typeof(T9) != typeof(_))
            ids[9] = Type<T9>.Id(world);

        if (typeof(T10) != typeof(_))
            ids[10] = Type<T10>.Id(world);

        if (typeof(T11) != typeof(_))
            ids[11] = Type<T11>.Id(world);

        if (typeof(T12) != typeof(_))
            ids[12] = Type<T12>.Id(world);

        if (typeof(T13) != typeof(_))
            ids[13] = Type<T13>.Id(world);

        if (typeof(T14) != typeof(_))
            ids[14] = Type<T14>.Id(world);

        if (typeof(T15) != typeof(_))
            ids[15] = Type<T15>.Id(world);
    }

    internal static bool Ensure(Entity entity, void** ptrs)
    {
        if (typeof(T0) != typeof(_))
            ptrs[0] = entity.EnsurePtr(Type<T0>.Id(entity.World));

        if (typeof(T1) != typeof(_))
            ptrs[1] = entity.EnsurePtr(Type<T1>.Id(entity.World));

        if (typeof(T2) != typeof(_))
            ptrs[2] = entity.EnsurePtr(Type<T2>.Id(entity.World));

        if (typeof(T3) != typeof(_))
            ptrs[3] = entity.EnsurePtr(Type<T3>.Id(entity.World));

        if (typeof(T4) != typeof(_))
            ptrs[4] = entity.EnsurePtr(Type<T4>.Id(entity.World));

        if (typeof(T5) != typeof(_))
            ptrs[5] = entity.EnsurePtr(Type<T5>.Id(entity.World));

        if (typeof(T6) != typeof(_))
            ptrs[6] = entity.EnsurePtr(Type<T6>.Id(entity.World));

        if (typeof(T7) != typeof(_))
            ptrs[7] = entity.EnsurePtr(Type<T7>.Id(entity.World));

        if (typeof(T8) != typeof(_))
            ptrs[8] = entity.EnsurePtr(Type<T8>.Id(entity.World));

        if (typeof(T9) != typeof(_))
            ptrs[9] = entity.EnsurePtr(Type<T9>.Id(entity.World));

        if (typeof(T10) != typeof(_))
            ptrs[10] = entity.EnsurePtr(Type<T10>.Id(entity.World));

        if (typeof(T11) != typeof(_))
            ptrs[11] = entity.EnsurePtr(Type<T11>.Id(entity.World));

        if (typeof(T12) != typeof(_))
            ptrs[12] = entity.EnsurePtr(Type<T12>.Id(entity.World));

        if (typeof(T13) != typeof(_))
            ptrs[13] = entity.EnsurePtr(Type<T13>.Id(entity.World));

        if (typeof(T14) != typeof(_))
            ptrs[14] = entity.EnsurePtr(Type<T14>.Id(entity.World));

        if (typeof(T15) != typeof(_))
            ptrs[15] = entity.EnsurePtr(Type<T15>.Id(entity.World));

        return true;
    }

    internal static bool Get(Entity entity, ecs_record_t* r, ecs_table_t* table, void** ptrs)
    {
        Ecs.Assert(table != null, nameof(ECS_INTERNAL_ERROR));

        if (ecs_table_column_count(table) == 0 && !ecs_table_has_flags(table, EcsTableHasSparse))
            return false;

        ecs_world_t* realWorld = ecs_get_world(entity.World);

        ulong* ids = stackalloc ulong[Count];
        int* columns = stackalloc int[Count];

        Ids(entity.World, ids);

        if (typeof(T0) != typeof(_))
            columns[0] = ecs_table_get_column_index(realWorld, table, ids[0]);

        if (typeof(T1) != typeof(_))
            columns[1] = ecs_table_get_column_index(realWorld, table, ids[1]);

        if (typeof(T2) != typeof(_))
            columns[2] = ecs_table_get_column_index(realWorld, table, ids[2]);

        if (typeof(T3) != typeof(_))
            columns[3] = ecs_table_get_column_index(realWorld, table, ids[3]);

        if (typeof(T4) != typeof(_))
            columns[4] = ecs_table_get_column_index(realWorld, table, ids[4]);

        if (typeof(T5) != typeof(_))
            columns[5] = ecs_table_get_column_index(realWorld, table, ids[5]);

        if (typeof(T6) != typeof(_))
            columns[6] = ecs_table_get_column_index(realWorld, table, ids[6]);

        if (typeof(T7) != typeof(_))
            columns[7] = ecs_table_get_column_index(realWorld, table, ids[7]);

        if (typeof(T8) != typeof(_))
            columns[8] = ecs_table_get_column_index(realWorld, table, ids[8]);

        if (typeof(T9) != typeof(_))
            columns[9] = ecs_table_get_column_index(realWorld, table, ids[9]);

        if (typeof(T10) != typeof(_))
            columns[10] = ecs_table_get_column_index(realWorld, table, ids[10]);

        if (typeof(T11) != typeof(_))
            columns[11] = ecs_table_get_column_index(realWorld, table, ids[11]);

        if (typeof(T12) != typeof(_))
            columns[12] = ecs_table_get_column_index(realWorld, table, ids[12]);

        if (typeof(T13) != typeof(_))
            columns[13] = ecs_table_get_column_index(realWorld, table, ids[13]);

        if (typeof(T14) != typeof(_))
            columns[14] = ecs_table_get_column_index(realWorld, table, ids[14]);

        if (typeof(T15) != typeof(_))
            columns[15] = ecs_table_get_column_index(realWorld, table, ids[15]);

        for (int i = 0; i < Count; i++)
        {
            ptrs[i] = columns[i] == -1
                ? entity.GetMutPtr(ids[i])
                : ecs_record_get_by_column(r, columns[i], 0);

            if (columns[i] == -1 && ptrs[i] == null)
                return false;
        }

        return true;
    }

    internal static void Modified(Entity entity, ulong* ids)
    {
        if (typeof(T0) != typeof(_))
            entity.Modified(ids[0]);

        if (typeof(T1) != typeof(_))
            entity.Modified(ids[1]);

        if (typeof(T2) != typeof(_))
            entity.Modified(ids[2]);

        if (typeof(T3) != typeof(_))
            entity.Modified(ids[3]);

        if (typeof(T4) != typeof(_))
            entity.Modified(ids[4]);

        if (typeof(T5) != typeof(_))
            entity.Modified(ids[5]);

        if (typeof(T6) != typeof(_))
            entity.Modified(ids[6]);

        if (typeof(T7) != typeof(_))
            entity.Modified(ids[7]);

        if (typeof(T8) != typeof(_))
            entity.Modified(ids[8]);

        if (typeof(T9) != typeof(_))
            entity.Modified(ids[9]);

        if (typeof(T10) != typeof(_))
            entity.Modified(ids[10]);

        if (typeof(T11) != typeof(_))
            entity.Modified(ids[11]);

        if (typeof(T12) != typeof(_))
            entity.Modified(ids[12]);

        if (typeof(T13) != typeof(_))
            entity.Modified(ids[13]);

        if (typeof(T14) != typeof(_))
            entity.Modified(ids[14]);

        if (typeof(T15) != typeof(_))
            entity.Modified(ids[15]);
    }

    [Conditional("DEBUG")]
    public static void AssertSparseTypes(ecs_world_t* world, bool allowSparseTypes)
    {
        if (allowSparseTypes)
            return;

        int sparseTypes = 0;

        if (typeof(T0) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T0>.Id(world), Ecs.Sparse) ? 1 << 0 : 0;

        if (typeof(T1) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T1>.Id(world), Ecs.Sparse) ? 1 << 1 : 0;

        if (typeof(T2) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T2>.Id(world), Ecs.Sparse) ? 1 << 2 : 0;

        if (typeof(T3) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T3>.Id(world), Ecs.Sparse) ? 1 << 3 : 0;

        if (typeof(T4) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T4>.Id(world), Ecs.Sparse) ? 1 << 4 : 0;

        if (typeof(T5) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T5>.Id(world), Ecs.Sparse) ? 1 << 5 : 0;

        if (typeof(T6) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T6>.Id(world), Ecs.Sparse) ? 1 << 6 : 0;

        if (typeof(T7) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T7>.Id(world), Ecs.Sparse) ? 1 << 7 : 0;

        if (typeof(T8) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T8>.Id(world), Ecs.Sparse) ? 1 << 8 : 0;

        if (typeof(T9) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T9>.Id(world), Ecs.Sparse) ? 1 << 9 : 0;

        if (typeof(T10) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T10>.Id(world), Ecs.Sparse) ? 1 << 10 : 0;

        if (typeof(T11) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T11>.Id(world), Ecs.Sparse) ? 1 << 11 : 0;

        if (typeof(T12) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T12>.Id(world), Ecs.Sparse) ? 1 << 12 : 0;

        if (typeof(T13) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T13>.Id(world), Ecs.Sparse) ? 1 << 13 : 0;

        if (typeof(T14) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T14>.Id(world), Ecs.Sparse) ? 1 << 14 : 0;

        if (typeof(T15) != typeof(_))
            sparseTypes |= ecs_has_id(world, Type<T15>.Id(world), Ecs.Sparse) ? 1 << 15 : 0;

        if (sparseTypes == 0)
            return;

        Ecs.Error($"Cannot use sparse components as generic type arguments for this struct when using .Iter() to iterate because sparse fields must be obtained with Iter.FieldAt(). Use .Each()/.Run() to iterate or remove the following types from the list: {GetTypeListString(sparseTypes)}");
    }

    [Conditional("DEBUG")]
    public static void AssertNoTags()
    {
        if (Tags == 0)
            return;

        Ecs.Error($"Cannot use zero-sized structs as generic type arguments for this struct. Remove the following type arguments: {GetTypeListString(Tags)}");
    }

    [Conditional("DEBUG")]
    public static void AssertReferenceTypes(bool allowReferenceTypes)
    {
        if (allowReferenceTypes || ReferenceTypes == 0)
            return;

        Ecs.Error($"Cannot use managed types as generic type arguments for callback signatures that retrieve pointers or spans. Remove the following type arguments: {GetTypeListString(ReferenceTypes)}");
    }
}
