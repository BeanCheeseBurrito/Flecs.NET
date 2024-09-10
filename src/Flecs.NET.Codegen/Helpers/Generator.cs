using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Flecs.NET.Codegen.Helpers;

public static class Generator
{
    public const int GenericCount = 16;

    #region Type Parameters

    // Generates "T0, T1, T2, ..."
    public static readonly string[] TypeParameters = CacheJoinedStrings(Separator.Comma, i => $"T{i}");
    // Generates "T0, T1, T2, ..."
    public static readonly string[] TypeParametersUnderscored = CacheJoinedStrings(Separator.Comma, i => $"T_{i}");

    // Generates "ref T0, ref T1, ref T2, ..."
    public static readonly string[] RefTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"ref T{i}");

    // Generates "Field<T0>, Field<T1>, Field<T2, ..."
    public static readonly string[] FieldTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"Field<T{i}>");

    // Generates "Span<T0>, Span<T1>, Span<T2, ..."
    public static readonly string[] SpanTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"Span<T{i}>");

    // Generates "T0*, T1*, T2*, ..."
    public static readonly string[] PointerTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"T{i}*");

    // Generates "T0 t0, T1 t1, T2 t2, ..."
    public static readonly string[] Parameters = CacheJoinedStrings(Separator.Comma, i => $"T{i} t{i}");

    // Generates "ref T0 t0, ref T1 t1, ref T2 t2, ..."
    public static readonly string[] RefParameters = CacheJoinedStrings(Separator.Comma, i => $"ref T{i} t{i}");

    // Generates "Field<T0> t0, Field<T1> t1, Field<T2> t2, ..."
    public static readonly string[] FieldParameters = CacheJoinedStrings(Separator.Comma, i => $"Field<T{i}> t{i}");

    // Generates "Span<T0> t0, Span<T1> t1, Span<T2> t2, ..."
    public static readonly string[] SpanParameters = CacheJoinedStrings(Separator.Comma, i => $"Span<T{i}> t{i}");

    // Generates "T0* t0, T1* t1, T2* t2, ..."
    public static readonly string[] PointerParameters = CacheJoinedStrings(Separator.Comma, i => $"T{i}* t{i}");

    // Generates "ref readonly t0, ref readonly T1 t1, ref readonly T2 t2, ..."
    public static readonly string[] RefReadOnlyParameters = CacheJoinedStrings(Separator.Comma, i => $"ref readonly T{i} t{i}");

    // Generates "where T0 : unmanaged where T1 : unmanaged where T2 : unmanaged..."
    public static readonly string[] TypeConstraints = CacheJoinedStrings(Separator.Space, i => $"where T{i} : unmanaged");

    // Generates "where T0_ : unmanaged, T0 where T1_ : unmanaged, T1 where T2_ : unmanaged, T2..."
    public static readonly string[] TypeConstraintsUnderScored = CacheJoinedStrings(Separator.Space, i => $"where T_{i} : unmanaged, T{i}");

    #endregion

    #region Callback Delegates

    public const string RunCallbackDelegate = "Ecs.RunCallback";

    public const string IterCallbackDelegate = "Ecs.IterCallback";

    public const string EachEntityCallbackDelegate = "Ecs.EachEntityCallback";

    public const string EachIterCallbackDelegate = "Ecs.EachIterCallback";

    // Generates "Ecs.IterFieldCallback<T0, T1, T2, ...>"
    public static readonly string[] IterFieldCallbackDelegate = CacheStrings(i => $"Ecs.IterFieldCallback<{TypeParameters[i]}>");

    // Generates "Ecs.IterSpanCallback<T0, T1, T2, ...>"
    public static readonly string[] IterSpanCallbackDelegate = CacheStrings(i => $"Ecs.IterSpanCallback<{TypeParameters[i]}>");

    // Generates "Ecs.IterPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] IterPointerCallbackDelegate = CacheStrings(i => $"Ecs.IterPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.EachRefCallback<T0, T1, T2, ...>"
    public static readonly string[] EachRefCallbackDelegate = CacheStrings(i => $"Ecs.EachRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.EachEntityRefCallback<T0, T1, T2, ...>"
    public static readonly string[] EachEntityRefCallbackDelegate = CacheStrings(i => $"Ecs.EachEntityRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.EachIterRefCallback<T0, T1, T2, ...>"
    public static readonly string[] EachIterRefCallbackDelegate = CacheStrings(i => $"Ecs.EachIterRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.EachPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] EachPointerCallbackDelegate = CacheStrings(i => $"Ecs.EachPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.EachEntityPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] EachEntityPointerCallbackDelegate = CacheStrings(i => $"Ecs.EachEntityPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.EachIterPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] EachIterPointerCallbackDelegate = CacheStrings(i => $"Ecs.EachIterPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.FindRefCallback<T0, T1, T2, ...>"
    public static readonly string[] FindRefCallbackDelegate = CacheStrings(i => $"Ecs.FindRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.FindEntityRefCallback<T0, T1, T2, ...>"
    public static readonly string[] FindEntityRefCallbackDelegate = CacheStrings(i => $"Ecs.FindEntityRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.FindIterRefCallback<T0, T1, T2, ...>"
    public static readonly string[] FindIterRefCallbackDelegate = CacheStrings(i => $"Ecs.FindIterRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.FindPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] FindPointerCallbackDelegate = CacheStrings(i => $"Ecs.FindPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.FindEntityPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] FindEntityPointerCallbackDelegate = CacheStrings(i => $"Ecs.FindEntityPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.FindIterPointerCallback<T0, T1, T2, ...>"
    public static readonly string[] FindIterPointerCallbackDelegate = CacheStrings(i => $"Ecs.FindIterPointerCallback<{TypeParameters[i]}>");

    // Generates "Ecs.ReadRefCallback<T0, T1, T2, ...>"
    public static readonly string[] ReadRefCallbackDelegate = CacheStrings(i => $"Ecs.ReadRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.WriteRefCallback<T0, T1, T2, ...>"
    public static readonly string[] WriteRefCallbackDelegate = CacheStrings(i => $"Ecs.WriteRefCallback<{TypeParameters[i]}>");

    // Generates "Ecs.InsertRefCallback<T0, T1, T2, ...>"
    public static readonly string[] InsertRefCallbackDelegate = CacheStrings(i => $"Ecs.InsertRefCallback<{TypeParameters[i]}>");

    #endregion

    #region Callback Function Pointers

    public const string RunCallbackPointer = "delegate*<Iter, void>";

    public const string IterCallbackPointer = "delegate*<Iter, void>";

    public const string EachEntityCallbackPointer = "delegate*<Entity, void>";

    public const string EachIterCallbackPointer = "delegate*<Iter, int, void>";

    // Generates "delegate*<Iter, Field<T0>, Field<T1>, Field<T2>, void>"
    public static readonly string[] IterFieldCallbackPointer = CacheStrings(i => $"delegate*<Iter, {FieldTypeParameters[i]}, void>");

    // Generates "delegate*<Iter, Span<T0>, Span<T1>, Span<T2>, void>"
    public static readonly string[] IterSpanCallbackPointer = CacheStrings(i => $"delegate*<Iter, {SpanTypeParameters[i]}, void>");

    // Generates "delegate*<Iter, T0*, T1*, T2*, void>"
    public static readonly string[] IterPointerCallbackPointer = CacheStrings(i => $"delegate*<Iter, {PointerTypeParameters[i]}, void>");

    // Generates "delegate*<ref T0, ref T1, ref T2, void>"
    public static readonly string[] EachRefCallbackPointer = CacheStrings(i => $"delegate*<{RefTypeParameters[i]}, void>");

    // Generates "delegate*<Entity, ref T0, ref T1, ref T2, void>"
    public static readonly string[] EachEntityRefCallbackPointer = CacheStrings(i => $"delegate*<Entity, {RefTypeParameters[i]}, void>");

    // Generates "delegate*<Iter, int, ref T0, ref T1, ref T2, void>"
    public static readonly string[] EachIterRefCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {RefTypeParameters[i]}, void>");

    // Generates "delegate*<T0*, T1*, T2*, void>"
    public static readonly string[] EachPointerCallbackPointer = CacheStrings(i => $"delegate*<{PointerTypeParameters[i]}, void>");

    // Generates "delegate*<Entity, T0*, T1*, T2*, void>"
    public static readonly string[] EachEntityPointerCallbackPointer = CacheStrings(i => $"delegate*<Entity, {PointerTypeParameters[i]}, void>");

    // Generates "delegate*<Iter, int, T0*, T1*, T2*, void>"
    public static readonly string[] EachIterPointerCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {PointerTypeParameters[i]}, void>");

    // Generates "delegate*<ref T0, ref T1, ref T2, bool>"
    public static readonly string[] FindRefCallbackPointer = CacheStrings(i => $"delegate*<{RefTypeParameters[i]}, bool>");

    // Generates "delegate*<Entity, ref T0, ref T1, ref T2, bool>"
    public static readonly string[] FindEntityRefCallbackPointer = CacheStrings(i => $"delegate*<Entity, {RefTypeParameters[i]}, bool>");

    // Generates "delegate*<Iter, int, ref T0, ref T1, ref T2, bool>"
    public static readonly string[] FindIterRefCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {RefTypeParameters[i]}, bool>");

    // Generates "delegate*<T0*, T1*, T2*, bool>"
    public static readonly string[] FindPointerCallbackPointer = CacheStrings(i => $"delegate*<{PointerTypeParameters[i]}, bool>");

    // Generates "delegate*<Entity, T0*, T1*, T2*, bool>"
    public static readonly string[] FindEntityPointerCallbackPointer = CacheStrings(i => $"delegate*<Entity, {PointerTypeParameters[i]}, bool>");

    // Generates "delegate*<Iter, int, T0*, T1*, T2*, bool>"
    public static readonly string[] FindIterPointerCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {PointerTypeParameters[i]}, bool>");

    #endregion

    #region Invoker Variables

    // Generates "T0* pointer0 = it.GetPointer<T0>(0); T1* pointer1 = it.GetPointer<T1>(1);..."
    public static readonly string[] IterPointerVariables = CacheJoinedStrings(Separator.Space, i => $"T{i}* pointer{i} = it.GetPointer<T{i}>({i});");

    // Generates "int step0 = it.Step<T0>(0); int step1 = it.Step<T1>(1);..."
    public static readonly string[] IterStepVariables = CacheJoinedStrings(Separator.Space, i => $"int step{i} = it.Step<T{i}>({i});");

    #endregion

    #region Invoker Callback Arguments

    // Generates "it.Field<T0>(0), it.Field<T1>(1), it.Field<T2>(2)..."
    public static readonly string[] IterFieldArguments = CacheJoinedStrings(Separator.Comma, i => $"it.Field<T{i}>({i})");

    // Generates "it.GetSpan<T0>(0), it.GetSpan<T1>(1), it.GetSpan<T2>(2)..."
    public static readonly string[] IterSpanArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetSpan<T{i}>({i})");

    // Generates "it.GetPointer<T0>(0), it.GetPointer<T1>(1), it.GetPointer<T2>(2)..."
    public static readonly string[] IterPointerArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetPointer<T{i}>({i})");

    // Generates "ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]), ref Managed.GetTypeRef<T2>(&pointer2[i])..."
    public static readonly string[] EachRefArguments = CacheJoinedStrings(Separator.Comma, i => $"ref Managed.GetTypeRef<T{i}>(&pointer{i}[i])");

    // Generates "new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]), ref Managed.GetTypeRef<T2>(&pointer2[i])"
    public static readonly string[] EachEntityRefArguments = CacheStrings(i => $"new Entity(it.Handle->world, it.Handle->entities[i]), {EachRefArguments[i]}");

    // Generates "it, i, ref Managed.GetTypeRef<T0>(&pointer0[i]), ref Managed.GetTypeRef<T1>(&pointer1[i]), ref Managed.GetTypeRef<T2>(&pointer2[i])"
    public static readonly string[] EachIterRefArguments = CacheStrings(i => $"it, i, {EachRefArguments[i]}");

    // Generates "&pointer0[i], &pointer1[i], &pointer2[i]..."
    public static readonly string[] EachPointerArguments = CacheJoinedStrings(Separator.Comma, i => $"&pointer{i}[i]");

    // Generates "new Entity(it.Handle->world, it.Handle->entities[i]), pointer0[i], &pointer1[i], &pointer2[i]"
    public static readonly string[] EachEntityPointerArguments = CacheStrings(i => $"new Entity(it.Handle->world, it.Handle->entities[i]), {EachPointerArguments[i]}");

    // Generates "it, i, pointer0[i], &pointer1[i], &pointer2[i]"
    public static readonly string[] EachIterPointerArguments = CacheStrings(i => $"it, i, {EachPointerArguments[i]}");

    // Generates "ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]), ref Managed.GetTypeRef<T2>(&pointer2[i * step2])..."
    public static readonly string[] EachRefSteppedArguments = CacheJoinedStrings(Separator.Comma, i => $"ref Managed.GetTypeRef<T{i}>(&pointer{i}[i * step{i}])");

    // Generates "new Entity(it.Handle->world, it.Handle->entities[i]), ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]), ref Managed.GetTypeRef<T2>(&pointer2[i * step2])"
    public static readonly string[] EachEntityRefSteppedArguments = CacheStrings(i => $"new Entity(it.Handle->world, it.Handle->entities[i]), {EachRefSteppedArguments[i]}");

    // Generates "it, i, ref Managed.GetTypeRef<T0>(&pointer0[i * step0]), ref Managed.GetTypeRef<T1>(&pointer1[i * step1]), ref Managed.GetTypeRef<T2>(&pointer2[i * step2])"
    public static readonly string[] EachIterRefSteppedArguments = CacheStrings(i => $"it, i, {EachRefSteppedArguments[i]}");

    // Generates "&pointer0[i * step0], &pointer1[i * step1], &pointer2[i * step2]..."
    public static readonly string[] EachPointerSteppedArguments = CacheJoinedStrings(Separator.Comma, i => $"&pointer{i}[i * step{i}]");

    // Generates "new Entity(it.Handle->world, it.Handle->entities[i]), &pointer0[i * step0], &pointer1[i * step1], &pointer2[i * step2]..."
    public static readonly string[] EachEntityPointerSteppedArguments = CacheStrings(i => $"new Entity(it.Handle->world, it.Handle->entities[i]), {EachPointerSteppedArguments[i]}");

    // Generates "it, i, &pointer0[i * step0], &pointer1[i * step1], &pointer2[i * step2]..."
    public static readonly string[] EachIterPointerSteppedArguments = CacheStrings(i => $"it, i, {EachPointerSteppedArguments[i]}");

    // Generates "ref Managed.GetTypeRef<T0>(&pointers[0]), ref Managed.GetTypeRef<T0>(&pointers[0])"
    public static readonly string[] ReadRefArguments = CacheJoinedStrings(Separator.Comma, i => $"ref Managed.GetTypeRef<T{i}>(pointers[{i}])");
    public static readonly string[] WriteRefArguments = ReadRefArguments;
    public static readonly string[] InsertRefArguments = ReadRefArguments;

    #endregion

    #region Entity Component Callbacks

    // Generates "Type<T0>.Id(world), Type<T1>.Id(world), Type<T2>.Id(world)..."
    public static readonly string[] TypeIdList = CacheJoinedStrings(Separator.Comma, i => $"Type<T{i}>.Id(world)");

    // Generates "ecs_table_get_column_index(realWorld, table, ids[0]), ecs_table_get_column_index(realWorld, table, ids[1])..."
    public static readonly string[] ColumnList = CacheJoinedStrings(Separator.Comma, i => $"ecs_table_get_column_index(realWorld, table, ids[{i}])");

    // Generates "ulong* ids = stackalloc ulong[] { Type<T0>.Id(world), Type<T1>.Id(world) };"
    public static readonly string[] IdsArray = CacheStrings(i => $"ulong* ids = stackalloc ulong[] {{ {TypeIdList[i]} }};");

    // Generates "int* columns = stackalloc int[] { ecs_table_get_column_index(realWorld, table, ids[0]), ecs_table_get_column_index(realWorld, table, ids[1]) };"
    public static readonly string[] ColumnsArray = CacheStrings(i => $"int* columns = stackalloc int[] {{ {ColumnList[i]} }};");

    // Generates "ptrs[0] = ecs_ensure_id(world, e, Type<T0>.Id(world)); ptrs[1] = ecs_ensure_id(world, e, Type<T1>.Id(world));"
    public static readonly string[] EnsurePointers = CacheJoinedStrings(Separator.Space, i => $"ptrs[{i}] = ecs_ensure_id(world, e, Type<T{i}>.Id(world));");

    // Generates "ecs_modified_id(world, id, ids[0]); ecs_modified_id(world, id, ids[1]);..."
    public static readonly string[] ModifiedChain = CacheJoinedStrings(Separator.Space, i => $"ecs_modified_id(world, entity, ids[{i}]);");

    #endregion

    #region Type Helpers

    // Generates "(Type<T0>.IsTag ? 1 << 0 : 0) | (Type<T1>.IsTag ? 1 << 1 : 0)..."
    public static readonly string[] Tags = CacheJoinedStrings(Separator.BitwiseOr, i => $"(Type<T{i}>.IsTag ? 1 << {i} : 0)");

    // Generates "(RuntimeHelpers.IsReferenceOrContainsReferences<T0>() ? 1 << 0 : 0) | (RuntimeHelpers.IsReferenceOrContainsReferences<T1>() ? 1 << 1 : 0)..."
    public static readonly string[] ReferenceTypes = CacheJoinedStrings(Separator.BitwiseOr, i => $"(RuntimeHelpers.IsReferenceOrContainsReferences<T{i}>() ? 1 << {i} : 0)");

    // Generates "Type<T0>.FullName, Type<T1>.Fullname..."
    public static readonly string[] TypeFullNames = CacheJoinedStrings(Separator.Comma, i => $"Type<T{i}>.FullName");

    #endregion

    #region Misc

    // Generates ".With<T0>.With<T1>().With<T2>()..."
    public static readonly string[] WithChain = CacheJoinedStrings(Separator.None, i => $".With<T{i}>()");

    // Generates "<typeparam name="T0">The T0 component type.</typeparam> <typeparam name="T1">The T1 component type.</typeparam> <typeparam name="T2">The T2 component type.</typeparam>..."
    public static readonly string[] XmlTypeParameters = CacheJoinedStrings(Separator.Space, i => $"<typeparam name=\"T{i}\">The T{i} component type.</typeparam>");

    #endregion

    public static readonly Callback[] CallbacksIter =
    [
        Callback.IterFieldCallbackDelegate,
        Callback.IterSpanCallbackDelegate,
        Callback.IterPointerCallbackDelegate,

        Callback.IterFieldCallbackPointer,
        Callback.IterSpanCallbackPointer,
        Callback.IterPointerCallbackPointer
    ];

    public static readonly Callback[] CallbacksRun =
    [
        Callback.RunCallbackDelegate,
        Callback.RunCallbackPointer,
    ];

    public static readonly Callback[] CallbacksEach =
    [
        Callback.EachRefCallbackDelegate,
        Callback.EachEntityRefCallbackDelegate,
        Callback.EachIterRefCallbackDelegate,

        Callback.EachRefCallbackPointer,
        Callback.EachEntityRefCallbackPointer,
        Callback.EachIterRefCallbackPointer,

        Callback.EachPointerCallbackDelegate,
        Callback.EachEntityPointerCallbackDelegate,
        Callback.EachIterPointerCallbackDelegate,

        Callback.EachPointerCallbackPointer,
        Callback.EachEntityPointerCallbackPointer,
        Callback.EachIterPointerCallbackPointer
    ];

    public static readonly Callback[] CallbacksFind =
    [
        Callback.FindRefCallbackDelegate,
        Callback.FindEntityRefCallbackDelegate,
        Callback.FindIterRefCallbackDelegate,

        Callback.FindRefCallbackPointer,
        Callback.FindEntityRefCallbackPointer,
        Callback.FindIterRefCallbackPointer,

        Callback.FindPointerCallbackDelegate,
        Callback.FindEntityPointerCallbackDelegate,
        Callback.FindIterPointerCallbackDelegate,

        Callback.FindPointerCallbackPointer,
        Callback.FindEntityPointerCallbackPointer,
        Callback.FindIterPointerCallbackPointer
    ];

    public static readonly Callback[] CallbacksReadAndWrite =
    [
        Callback.ReadRefCallbackDelegate,
        Callback.WriteRefCallbackDelegate,
    ];

    public static readonly Callback[] CallbacksInsert =
    [
        Callback.InsertRefCallbackDelegate
    ];

    public static readonly Callback[] CallbacksIterAndEach =
    [
        ..CallbacksIter,
        ..CallbacksEach
    ];

    public static readonly Callback[] CallbacksRunAndIterAndEach =
    [
        .. CallbacksRun,
        .. CallbacksIterAndEach
    ];

    public static readonly Callback[] CallbacksRunAndIterAndEachAndFind =
    [
        .. CallbacksRun,
        .. CallbacksIter,
        .. CallbacksEach,
        .. CallbacksFind
    ];

    public static readonly string[][] GenericNames = typeof(Type).GetEnumNames()
        .Select((str) => CacheStrings(i => $"{str}<{TypeParameters[i]}>"))
        .ToArray();

    public static string GetTypeName(Type type, int i = -1)
    {
        return i < 0
            ? type.ToString()
            : GenericNames[(int)type][i];
    }

    public static string GetCallbackType(Callback callback, int i)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate => RunCallbackDelegate,
            Callback.RunCallbackPointer => RunCallbackPointer,

            Callback.IterFieldCallbackDelegate => IterFieldCallbackDelegate[i],
            Callback.IterSpanCallbackDelegate => IterSpanCallbackDelegate[i],
            Callback.IterPointerCallbackDelegate => IterPointerCallbackDelegate[i],

            Callback.IterFieldCallbackPointer => IterFieldCallbackPointer[i],
            Callback.IterSpanCallbackPointer => IterSpanCallbackPointer[i],
            Callback.IterPointerCallbackPointer => IterPointerCallbackPointer[i],

            Callback.EachRefCallbackDelegate => EachRefCallbackDelegate[i],
            Callback.EachEntityRefCallbackDelegate => EachEntityRefCallbackDelegate[i],
            Callback.EachIterRefCallbackDelegate => EachIterRefCallbackDelegate[i],
            Callback.EachPointerCallbackDelegate => EachPointerCallbackDelegate[i],
            Callback.EachEntityPointerCallbackDelegate => EachEntityPointerCallbackDelegate[i],
            Callback.EachIterPointerCallbackDelegate => EachIterPointerCallbackDelegate[i],

            Callback.EachRefCallbackPointer => EachRefCallbackPointer[i],
            Callback.EachEntityRefCallbackPointer => EachEntityRefCallbackPointer[i],
            Callback.EachIterRefCallbackPointer => EachIterRefCallbackPointer[i],
            Callback.EachPointerCallbackPointer => EachPointerCallbackPointer[i],
            Callback.EachEntityPointerCallbackPointer => EachEntityPointerCallbackPointer[i],
            Callback.EachIterPointerCallbackPointer => EachIterPointerCallbackPointer[i],

            Callback.FindRefCallbackDelegate => FindRefCallbackDelegate[i],
            Callback.FindEntityRefCallbackDelegate => FindEntityRefCallbackDelegate[i],
            Callback.FindIterRefCallbackDelegate => FindIterRefCallbackDelegate[i],
            Callback.FindPointerCallbackDelegate => FindPointerCallbackDelegate[i],
            Callback.FindEntityPointerCallbackDelegate => FindEntityPointerCallbackDelegate[i],
            Callback.FindIterPointerCallbackDelegate => FindIterPointerCallbackDelegate[i],

            Callback.FindRefCallbackPointer => FindRefCallbackPointer[i],
            Callback.FindEntityRefCallbackPointer => FindEntityRefCallbackPointer[i],
            Callback.FindIterRefCallbackPointer => FindIterRefCallbackPointer[i],
            Callback.FindPointerCallbackPointer => FindPointerCallbackPointer[i],
            Callback.FindEntityPointerCallbackPointer => FindEntityPointerCallbackPointer[i],
            Callback.FindIterPointerCallbackPointer => FindIterPointerCallbackPointer[i],

            Callback.ReadRefCallbackDelegate => ReadRefCallbackDelegate[i],
            Callback.WriteRefCallbackDelegate => WriteRefCallbackDelegate[i],
            Callback.InsertRefCallbackDelegate => InsertRefCallbackDelegate[i],

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static bool GetCallbackIsRun(Callback callback)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate or Callback.RunCallbackPointer => true,
            _ => false
        };
    }

    public static bool GetCallbackIsDelegate(Callback callback)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate or

            Callback.IterFieldCallbackDelegate or
            Callback.IterSpanCallbackDelegate or
            Callback.IterPointerCallbackDelegate or

            Callback.EachRefCallbackDelegate or
            Callback.EachEntityRefCallbackDelegate or
            Callback.EachIterRefCallbackDelegate or
            Callback.EachPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachIterPointerCallbackDelegate or

            Callback.FindRefCallbackDelegate or
            Callback.FindEntityRefCallbackDelegate or
            Callback.FindIterRefCallbackDelegate or
            Callback.FindPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindIterPointerCallbackDelegate => true,

            Callback.RunCallbackPointer or

            Callback.IterFieldCallbackPointer or
            Callback.IterSpanCallbackPointer or
            Callback.IterPointerCallbackPointer or

            Callback.EachRefCallbackPointer or
            Callback.EachEntityRefCallbackPointer or
            Callback.EachIterRefCallbackPointer or
            Callback.EachPointerCallbackPointer or
            Callback.EachEntityPointerCallbackPointer or
            Callback.EachIterPointerCallbackPointer or

            Callback.FindRefCallbackPointer or
            Callback.FindEntityRefCallbackPointer or
            Callback.FindIterRefCallbackPointer or
            Callback.FindPointerCallbackPointer or
            Callback.FindEntityPointerCallbackPointer or
            Callback.FindIterPointerCallbackPointer => false,

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static bool GetCallbackIsUnmanaged(Callback callback)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate or
            Callback.RunCallbackPointer or

            Callback.IterFieldCallbackDelegate or
            Callback.IterFieldCallbackPointer or

            Callback.EachRefCallbackDelegate or
            Callback.EachEntityRefCallbackDelegate or
            Callback.EachIterRefCallbackDelegate or

            Callback.EachRefCallbackPointer or
            Callback.EachEntityRefCallbackPointer or
            Callback.EachIterRefCallbackPointer or

            Callback.FindRefCallbackDelegate or
            Callback.FindEntityRefCallbackDelegate or
            Callback.FindIterRefCallbackDelegate or

            Callback.FindRefCallbackPointer or
            Callback.FindEntityRefCallbackPointer or
            Callback.FindIterRefCallbackPointer => false,

            Callback.IterSpanCallbackDelegate or
            Callback.IterPointerCallbackDelegate or

            Callback.IterSpanCallbackPointer or
            Callback.IterPointerCallbackPointer or

            Callback.EachPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachIterPointerCallbackDelegate or

            Callback.EachPointerCallbackPointer or
            Callback.EachEntityPointerCallbackPointer or
            Callback.EachIterPointerCallbackPointer or

            Callback.FindPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindIterPointerCallbackDelegate or

            Callback.FindPointerCallbackPointer or
            Callback.FindEntityPointerCallbackPointer or
            Callback.FindIterPointerCallbackPointer => true,

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetCallbackArguments(int i, Callback callback)
    {
        return callback switch
        {
            Callback.IterFieldCallbackDelegate or Callback.IterFieldCallbackPointer => IterFieldArguments[i],
            Callback.IterSpanCallbackDelegate or Callback.IterSpanCallbackPointer => IterSpanArguments[i],
            Callback.IterPointerCallbackDelegate or Callback.IterPointerCallbackPointer => IterPointerArguments[i],
            Callback.EachRefCallbackDelegate or Callback.EachRefCallbackPointer or Callback.FindRefCallbackDelegate or Callback.FindRefCallbackPointer => EachRefArguments[i],
            Callback.EachEntityRefCallbackDelegate or Callback.EachEntityRefCallbackPointer or Callback.FindEntityRefCallbackDelegate or Callback.FindEntityRefCallbackPointer => EachEntityRefArguments[i],
            Callback.EachIterRefCallbackDelegate or Callback.EachIterRefCallbackPointer or Callback.FindIterRefCallbackDelegate or Callback.FindIterRefCallbackPointer => EachIterRefArguments[i],
            Callback.EachPointerCallbackDelegate or Callback.EachPointerCallbackPointer or Callback.FindPointerCallbackDelegate or Callback.FindPointerCallbackPointer => EachPointerArguments[i],
            Callback.EachEntityPointerCallbackDelegate or Callback.EachEntityPointerCallbackPointer or Callback.FindEntityPointerCallbackDelegate or Callback.FindEntityPointerCallbackPointer => EachEntityPointerArguments[i],
            Callback.EachIterPointerCallbackDelegate or Callback.EachIterPointerCallbackPointer or Callback.FindIterPointerCallbackDelegate or Callback.FindIterPointerCallbackPointer => EachIterPointerArguments[i],
            Callback.ReadRefCallbackDelegate or Callback.WriteRefCallbackDelegate or Callback.InsertRefCallbackDelegate => ReadRefArguments[i],
            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetCallbackSteppedArguments(int i, Callback callback)
    {
        return callback switch
        {
            Callback.EachRefCallbackDelegate or Callback.EachRefCallbackPointer or Callback.FindRefCallbackDelegate or Callback.FindRefCallbackPointer => EachRefSteppedArguments[i],
            Callback.EachEntityRefCallbackDelegate or Callback.EachEntityRefCallbackPointer or Callback.FindEntityRefCallbackDelegate or Callback.FindEntityRefCallbackPointer => EachEntityRefSteppedArguments[i],
            Callback.EachIterRefCallbackDelegate or Callback.EachIterRefCallbackPointer or Callback.FindIterRefCallbackDelegate or Callback.FindIterRefCallbackPointer => EachIterRefSteppedArguments[i],
            Callback.EachPointerCallbackDelegate or Callback.EachPointerCallbackPointer or Callback.FindPointerCallbackDelegate or Callback.FindPointerCallbackPointer => EachPointerSteppedArguments[i],
            Callback.EachEntityPointerCallbackDelegate or Callback.EachEntityPointerCallbackPointer or Callback.FindEntityPointerCallbackDelegate or Callback.FindEntityPointerCallbackPointer => EachEntityPointerSteppedArguments[i],
            Callback.EachIterPointerCallbackDelegate or Callback.EachIterPointerCallbackPointer or Callback.FindIterPointerCallbackDelegate or Callback.FindIterPointerCallbackPointer => EachIterPointerSteppedArguments[i],
            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetCallbackCountVariable(Callback callback)
    {
        return callback switch
        {
            Callback.EachRefCallbackDelegate or
            Callback.EachRefCallbackPointer or

            Callback.EachPointerCallbackDelegate or
            Callback.EachPointerCallbackPointer or

            Callback.FindRefCallbackDelegate or
            Callback.FindRefCallbackPointer or

            Callback.FindPointerCallbackDelegate or
            Callback.FindPointerCallbackPointer or

            Callback.EachIterRefCallbackDelegate or
            Callback.EachIterRefCallbackPointer or

            Callback.EachIterPointerCallbackDelegate or
            Callback.EachIterPointerCallbackPointer or

            Callback.FindIterRefCallbackDelegate or
            Callback.FindIterRefCallbackPointer or

            Callback.FindIterPointerCallbackDelegate or
            Callback.FindIterPointerCallbackPointer => "int count = it.Handle->count == 0 && it.Handle->table == null ? 1 : it.Handle->count;",

            Callback.EachEntityRefCallbackDelegate or
            Callback.EachEntityRefCallbackPointer or

            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackPointer or

            Callback.FindEntityRefCallbackDelegate or
            Callback.FindEntityRefCallbackPointer or

            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackPointer => "int count = it.Handle->count; Ecs.Assert(it.Handle->count > 0, \"No entities returned, use Iter() or Each() without the entity argument instead.\");",

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetInvokerName(Callback callback)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate or
            Callback.RunCallbackPointer => "Run",

            Callback.IterCallbackDelegate or
            Callback.IterFieldCallbackDelegate or
            Callback.IterSpanCallbackDelegate or
            Callback.IterPointerCallbackDelegate or

            Callback.IterCallbackPointer or
            Callback.IterFieldCallbackPointer or
            Callback.IterSpanCallbackPointer or
            Callback.IterPointerCallbackPointer => "Iter",

            Callback.EachEntityCallbackDelegate or
            Callback.EachIterCallbackDelegate or
            Callback.EachRefCallbackDelegate or
            Callback.EachEntityRefCallbackDelegate or
            Callback.EachIterRefCallbackDelegate or
            Callback.EachPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachIterPointerCallbackDelegate or

            Callback.EachEntityCallbackPointer or
            Callback.EachIterCallbackPointer or
            Callback.EachRefCallbackPointer or
            Callback.EachEntityRefCallbackPointer or
            Callback.EachIterRefCallbackPointer or
            Callback.EachPointerCallbackPointer or
            Callback.EachEntityPointerCallbackPointer or
            Callback.EachIterPointerCallbackPointer => "Each",

            Callback.FindRefCallbackDelegate or
            Callback.FindEntityRefCallbackDelegate or
            Callback.FindIterRefCallbackDelegate or
            Callback.FindPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindIterPointerCallbackDelegate or

            Callback.FindRefCallbackPointer or
            Callback.FindEntityRefCallbackPointer or
            Callback.FindIterRefCallbackPointer or
            Callback.FindPointerCallbackPointer or
            Callback.FindEntityPointerCallbackPointer or
            Callback.FindIterPointerCallbackPointer => "Find",

            Callback.ReadRefCallbackDelegate => "Read",
            Callback.WriteRefCallbackDelegate => "Write",
            Callback.InsertRefCallbackDelegate => "Insert",

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetInvokerReturnType(Callback callback)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate or
            Callback.RunCallbackPointer or

            Callback.IterCallbackDelegate or
            Callback.IterFieldCallbackDelegate or
            Callback.IterSpanCallbackDelegate or
            Callback.IterPointerCallbackDelegate or

            Callback.IterCallbackPointer or
            Callback.IterFieldCallbackPointer or
            Callback.IterSpanCallbackPointer or
            Callback.IterPointerCallbackPointer or

            Callback.EachEntityCallbackDelegate or
            Callback.EachIterCallbackDelegate or
            Callback.EachRefCallbackDelegate or
            Callback.EachEntityRefCallbackDelegate or
            Callback.EachIterRefCallbackDelegate or
            Callback.EachPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachIterPointerCallbackDelegate or

            Callback.EachEntityCallbackPointer or
            Callback.EachIterCallbackPointer or
            Callback.EachRefCallbackPointer or
            Callback.EachEntityRefCallbackPointer or
            Callback.EachIterRefCallbackPointer or
            Callback.EachPointerCallbackPointer or
            Callback.EachEntityPointerCallbackPointer or
            Callback.EachIterPointerCallbackPointer => "void",

            Callback.FindRefCallbackDelegate or
            Callback.FindEntityRefCallbackDelegate or
            Callback.FindIterRefCallbackDelegate or
            Callback.FindPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindIterPointerCallbackDelegate or

            Callback.FindRefCallbackPointer or
            Callback.FindEntityRefCallbackPointer or
            Callback.FindIterRefCallbackPointer or
            Callback.FindPointerCallbackPointer or
            Callback.FindEntityPointerCallbackPointer or
            Callback.FindIterPointerCallbackPointer => "Entity",

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetInvokerReturn(Callback callback)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate or
            Callback.RunCallbackPointer or

            Callback.IterCallbackDelegate or
            Callback.IterFieldCallbackDelegate or
            Callback.IterSpanCallbackDelegate or
            Callback.IterPointerCallbackDelegate or

            Callback.IterCallbackPointer or
            Callback.IterFieldCallbackPointer or
            Callback.IterSpanCallbackPointer or
            Callback.IterPointerCallbackPointer or

            Callback.EachEntityCallbackDelegate or
            Callback.EachIterCallbackDelegate or
            Callback.EachRefCallbackDelegate or
            Callback.EachEntityRefCallbackDelegate or
            Callback.EachIterRefCallbackDelegate or
            Callback.EachPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachIterPointerCallbackDelegate or

            Callback.EachEntityCallbackPointer or
            Callback.EachIterCallbackPointer or
            Callback.EachRefCallbackPointer or
            Callback.EachEntityRefCallbackPointer or
            Callback.EachIterRefCallbackPointer or
            Callback.EachPointerCallbackPointer or
            Callback.EachEntityPointerCallbackPointer or
            Callback.EachIterPointerCallbackPointer => string.Empty,

            Callback.FindRefCallbackDelegate or
            Callback.FindEntityRefCallbackDelegate or
            Callback.FindIterRefCallbackDelegate or
            Callback.FindPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindIterPointerCallbackDelegate or

            Callback.FindRefCallbackPointer or
            Callback.FindEntityRefCallbackPointer or
            Callback.FindIterRefCallbackPointer or
            Callback.FindPointerCallbackPointer or
            Callback.FindEntityPointerCallbackPointer or
            Callback.FindIterPointerCallbackPointer => "return ",

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static void AddSource(
        IncrementalGeneratorPostInitializationContext context,
        string file,
        string source,
        [CallerFilePath] string callerPath = "")
    {
        context.AddSource(file, $"// {file}\n// File was auto-generated by {callerPath}\n{source}");
    }

    private static string JoinString(int count, string separator, Func<int, string> callback)
    {
        if (callback == null)
            throw new ArgumentNullException(nameof(callback));

        StringBuilder str = new();

        for (int i = 0; i < count; i++)
        {
            str.Append(callback(i));
            if (i < count - 1)
                str.Append(separator);
        }

        return str.ToString();
    }

    private static string[] CacheJoinedStrings(string separator, Func<int, string> callback)
    {
        return Enumerable.Range(0, GenericCount)
            .Select((int i) => JoinString(i + 1, separator, callback))
            .ToArray();
    }

    private static string[] CacheStrings(Func<int, string> callback)
    {
        return Enumerable.Range(0, GenericCount)
            .Select(callback)
            .ToArray();
    }
}
