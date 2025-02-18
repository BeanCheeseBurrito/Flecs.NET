using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flecs.NET.Codegen.Helpers;

public static class Generator
{
    public const int GenericCount = 16;

    #region Type Parameters

    public static readonly string[] TypeParameters = CacheJoinedStrings(Separator.Comma, i => $"T{i}");
    public static readonly string[] RefTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"ref T{i}");
    public static readonly string[] FieldTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"Field<T{i}>");
    public static readonly string[] SpanTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"Span<T{i}>");
    public static readonly string[] PointerTypeParameters = CacheJoinedStrings(Separator.Comma, i => $"T{i}*");
    public static readonly string[] RefParameters = CacheJoinedStrings(Separator.Comma, i => $"ref T{i} t{i}");
    public static readonly string[] FieldParameters = CacheJoinedStrings(Separator.Comma, i => $"Field<T{i}> t{i}");
    public static readonly string[] SpanParameters = CacheJoinedStrings(Separator.Comma, i => $"Span<T{i}> t{i}");
    public static readonly string[] PointerParameters = CacheJoinedStrings(Separator.Comma, i => $"T{i}* t{i}");
    public static readonly string[] RefReadOnlyParameters = CacheJoinedStrings(Separator.Comma, i => $"ref readonly T{i} t{i}");

    #endregion

    #region Callback Delegates

    public const string RunCallbackDelegate = "Ecs.RunCallback";

    public static readonly string[] IterFieldCallbackDelegate = CacheStrings(i => $"Ecs.IterFieldCallback<{TypeParameters[i]}>");
    public static readonly string[] IterSpanCallbackDelegate = CacheStrings(i => $"Ecs.IterSpanCallback<{TypeParameters[i]}>");
    public static readonly string[] IterPointerCallbackDelegate = CacheStrings(i => $"Ecs.IterPointerCallback<{TypeParameters[i]}>");

    public static readonly string[] EachRefCallbackDelegate = CacheStrings(i => $"Ecs.EachRefCallback<{TypeParameters[i]}>");
    public static readonly string[] EachEntityRefCallbackDelegate = CacheStrings(i => $"Ecs.EachEntityRefCallback<{TypeParameters[i]}>");
    public static readonly string[] EachIterRefCallbackDelegate = CacheStrings(i => $"Ecs.EachIterRefCallback<{TypeParameters[i]}>");
    public static readonly string[] EachPointerCallbackDelegate = CacheStrings(i => $"Ecs.EachPointerCallback<{TypeParameters[i]}>");
    public static readonly string[] EachEntityPointerCallbackDelegate = CacheStrings(i => $"Ecs.EachEntityPointerCallback<{TypeParameters[i]}>");
    public static readonly string[] EachIterPointerCallbackDelegate = CacheStrings(i => $"Ecs.EachIterPointerCallback<{TypeParameters[i]}>");

    public static readonly string[] FindRefCallbackDelegate = CacheStrings(i => $"Ecs.FindRefCallback<{TypeParameters[i]}>");
    public static readonly string[] FindEntityRefCallbackDelegate = CacheStrings(i => $"Ecs.FindEntityRefCallback<{TypeParameters[i]}>");
    public static readonly string[] FindIterRefCallbackDelegate = CacheStrings(i => $"Ecs.FindIterRefCallback<{TypeParameters[i]}>");
    public static readonly string[] FindPointerCallbackDelegate = CacheStrings(i => $"Ecs.FindPointerCallback<{TypeParameters[i]}>");
    public static readonly string[] FindEntityPointerCallbackDelegate = CacheStrings(i => $"Ecs.FindEntityPointerCallback<{TypeParameters[i]}>");
    public static readonly string[] FindIterPointerCallbackDelegate = CacheStrings(i => $"Ecs.FindIterPointerCallback<{TypeParameters[i]}>");

    public static readonly string[] ReadRefCallbackDelegate = CacheStrings(i => $"Ecs.ReadRefCallback<{TypeParameters[i]}>");
    public static readonly string[] WriteRefCallbackDelegate = CacheStrings(i => $"Ecs.WriteRefCallback<{TypeParameters[i]}>");
    public static readonly string[] InsertRefCallbackDelegate = CacheStrings(i => $"Ecs.InsertRefCallback<{TypeParameters[i]}>");

    #endregion

    #region Callback Function Pointers

    public const string RunCallbackPointer = "delegate*<Iter, void>";

    public static readonly string[] IterFieldCallbackPointer = CacheStrings(i => $"delegate*<Iter, {FieldTypeParameters[i]}, void>");
    public static readonly string[] IterSpanCallbackPointer = CacheStrings(i => $"delegate*<Iter, {SpanTypeParameters[i]}, void>");
    public static readonly string[] IterPointerCallbackPointer = CacheStrings(i => $"delegate*<Iter, {PointerTypeParameters[i]}, void>");

    public static readonly string[] EachRefCallbackPointer = CacheStrings(i => $"delegate*<{RefTypeParameters[i]}, void>");
    public static readonly string[] EachEntityRefCallbackPointer = CacheStrings(i => $"delegate*<Entity, {RefTypeParameters[i]}, void>");
    public static readonly string[] EachIterRefCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {RefTypeParameters[i]}, void>");
    public static readonly string[] EachPointerCallbackPointer = CacheStrings(i => $"delegate*<{PointerTypeParameters[i]}, void>");
    public static readonly string[] EachEntityPointerCallbackPointer = CacheStrings(i => $"delegate*<Entity, {PointerTypeParameters[i]}, void>");
    public static readonly string[] EachIterPointerCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {PointerTypeParameters[i]}, void>");

    public static readonly string[] FindRefCallbackPointer = CacheStrings(i => $"delegate*<{RefTypeParameters[i]}, bool>");
    public static readonly string[] FindEntityRefCallbackPointer = CacheStrings(i => $"delegate*<Entity, {RefTypeParameters[i]}, bool>");
    public static readonly string[] FindIterRefCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {RefTypeParameters[i]}, bool>");
    public static readonly string[] FindPointerCallbackPointer = CacheStrings(i => $"delegate*<{PointerTypeParameters[i]}, bool>");
    public static readonly string[] FindEntityPointerCallbackPointer = CacheStrings(i => $"delegate*<Entity, {PointerTypeParameters[i]}, bool>");
    public static readonly string[] FindIterPointerCallbackPointer = CacheStrings(i => $"delegate*<Iter, int, {PointerTypeParameters[i]}, bool>");

    #endregion

    #region Invoker

    public static readonly string[] FieldDataVariables = CacheJoinedStrings(Separator.Space, i => $"FieldData<T{i}> field{i} = it.GetFieldData<T{i}>({i});");
    public static readonly string[] FieldDataParameters = CacheJoinedStrings(Separator.Comma, i => $"ref FieldData<T{i}> field{i}");
    public static readonly string[] FieldDataRefs = CacheJoinedStrings(Separator.Comma, i => $"ref field{i}");

    #endregion

    #region Invoker Callback Arguments

    public static readonly string[] IterFieldArguments = CacheJoinedStrings(Separator.Comma, i => $"it.Field<T{i}>({i})");
    public static readonly string[] IterSpanArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetSpan<T{i}>({i})");
    public static readonly string[] IterPointerArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetPointer<T{i}>({i})");

    public static readonly string[] ReadRefArguments = CacheJoinedStrings(Separator.Comma, i => $"ref Managed.GetTypeRef<T{i}>(pointers[{i}])");
    public static readonly string[] WriteRefArguments = ReadRefArguments;
    public static readonly string[] InsertRefArguments = ReadRefArguments;

    #endregion

    #region Entity Component Callbacks

    public static readonly string[] TypeIdList = CacheJoinedStrings(Separator.Comma, i => $"Type<T{i}>.Id(world)");

    public static readonly string[] ColumnList = CacheJoinedStrings(Separator.Comma, i => $"ecs_table_get_column_index(realWorld, table, ids[{i}])");

    public static readonly string[] IdsArray = CacheStrings(i => $"ulong* ids = stackalloc ulong[] {{ {TypeIdList[i]} }};");

    public static readonly string[] ColumnsArray = CacheStrings(i => $"int* columns = stackalloc int[] {{ {ColumnList[i]} }};");

    public static readonly string[] EnsurePointers = CacheJoinedStrings(Separator.Space, i => $"ptrs[{i}] = ecs_ensure_id(world, e, Type<T{i}>.Id(world));");

    public static readonly string[] ModifiedChain = CacheJoinedStrings(Separator.Space, i => $"ecs_modified_id(world, entity, ids[{i}]);");

    #endregion

    #region Type Helpers

    public static readonly string[] Tags = CacheJoinedStrings(Separator.BitwiseOr, i => $"(Type<T{i}>.IsTag ? 1 << {i} : 0)");

    public static readonly string[] ReferenceTypes = CacheJoinedStrings(Separator.BitwiseOr, i => $"(RuntimeHelpers.IsReferenceOrContainsReferences<T{i}>() ? 1 << {i} : 0)");

    public static readonly string[] TypeFullNames = CacheJoinedStrings(Separator.Comma, i => $"Type<T{i}>.FullName");

    public static readonly string[] SparseBitField = CacheJoinedStrings(Separator.BitwiseOr, i => $"(ecs_has_id(world, Type<T{i}>.Id(world), Ecs.Sparse) ? 1 << {i} : 0)");

    public static readonly string[] ContainsReferenceTypes = CacheJoinedStrings(Separator.Or, i => $"RuntimeHelpers.IsReferenceOrContainsReferences<T{i}>()");

    #endregion

    #region Misc

    public static readonly string[] WithChain = CacheJoinedStrings(Separator.None, i => $".With<T{i}>()");

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
        if (type == Type.Component)
            return $"{type}<TComponent>";

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

    public static bool GetCallbackIsIter(Callback callback)
    {
        return callback switch
        {
            Callback.IterFieldCallbackDelegate or
            Callback.IterFieldCallbackPointer or
            Callback.IterSpanCallbackDelegate or
            Callback.IterSpanCallbackPointer or
            Callback.IterPointerCallbackDelegate or
            Callback.IterPointerCallbackPointer => true,
            _ => false
        };
    }

    public static CallbackParameters GetCallbackParameters(Callback callback)
    {
        return callback switch
        {
            Callback.IterFieldCallbackDelegate or
            Callback.IterFieldCallbackPointer => CallbackParameters.IterField,

            Callback.IterSpanCallbackDelegate or
            Callback.IterSpanCallbackPointer =>  CallbackParameters.IterSpan,

            Callback.IterPointerCallbackDelegate or
            Callback.IterPointerCallbackPointer =>  CallbackParameters.IterPointer,

            Callback.EachRefCallbackDelegate or
            Callback.EachRefCallbackPointer or
            Callback.FindRefCallbackDelegate or
            Callback.FindRefCallbackPointer => CallbackParameters.EachRef,

            Callback.EachEntityRefCallbackDelegate or
            Callback.EachEntityRefCallbackPointer or
            Callback.FindEntityRefCallbackDelegate or
            Callback.FindEntityRefCallbackPointer => CallbackParameters.EachEntityRef,

            Callback.EachIterRefCallbackDelegate or
            Callback.EachIterRefCallbackPointer or
            Callback.FindIterRefCallbackDelegate or
            Callback.FindIterRefCallbackPointer => CallbackParameters.EachIterRef,

            Callback.EachPointerCallbackDelegate or
            Callback.EachPointerCallbackPointer or
            Callback.FindPointerCallbackDelegate or
            Callback.FindPointerCallbackPointer => CallbackParameters.EachPointer,

            Callback.EachEntityPointerCallbackDelegate or
            Callback.EachEntityPointerCallbackPointer or
            Callback.FindEntityPointerCallbackDelegate or
            Callback.FindEntityPointerCallbackPointer => CallbackParameters.EachEntityPointer,

            Callback.EachIterPointerCallbackDelegate or
            Callback.EachIterPointerCallbackPointer or
            Callback.FindIterPointerCallbackDelegate or
            Callback.FindIterPointerCallbackPointer => CallbackParameters.EachIterPointer,

            Callback.ReadRefCallbackDelegate or
            Callback.WriteRefCallbackDelegate or
            Callback.InsertRefCallbackDelegate => CallbackParameters.ReadRef,

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetCallbackArguments(Callback callback, int i)
    {
        return GetCallbackParameters(callback) switch
        {
            CallbackParameters.IterField => IterFieldArguments[i],
            CallbackParameters.IterSpan => IterSpanArguments[i],
            CallbackParameters.IterPointer => IterPointerArguments[i],
            CallbackParameters.ReadRef => ReadRefArguments[i],
            _ => throw new ArgumentOutOfRangeException(nameof(callback))
        };
    }

    public static string GetCallbackArguments(Callback parameters, IterationTechnique iterationTechnique, int i)
    {
        return GetCallbackParameters(parameters) switch
        {
            CallbackParameters.EachRef => $"{GetEachRefArguments(iterationTechnique, i)}",
            CallbackParameters.EachPointer => $"{GetEachPointerArguments(iterationTechnique, i)}",
            CallbackParameters.EachEntityRef => $"new Entity(it.Handle->world, it.Handle->entities[i]), {GetEachRefArguments(iterationTechnique, i)}",
            CallbackParameters.EachEntityPointer => $"new Entity(it.Handle->world, it.Handle->entities[i]), {GetEachPointerArguments(iterationTechnique, i)}",
            CallbackParameters.EachIterRef => $"it, i, {GetEachRefArguments(iterationTechnique, i)}",
            CallbackParameters.EachIterPointer => $"it, i, {GetEachPointerArguments(iterationTechnique, i)}",
            _ => throw new ArgumentOutOfRangeException(nameof(parameters), parameters, null)
        };
    }

    public static readonly string[][] EachPointerArguments = typeof(IterationTechnique).GetEnumNames()
        .Select((str) => CacheJoinedStrings(Separator.Comma, i => $"field{i}.Pointer{str}(i)"))
        .ToArray();

    public static readonly string[][] EachRefArguments = typeof(IterationTechnique).GetEnumNames()
        .Select((str) => CacheJoinedStrings(Separator.Comma, i => $"ref field{i}.Ref{str}(i)"))
        .ToArray();

    public static string GetEachPointerArguments(IterationTechnique type, int i)
    {
        return EachPointerArguments[(int)type][i];
    }

    public static string GetEachRefArguments(IterationTechnique type, int i)
    {
        return EachRefArguments[(int)type][i];
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
            Callback.FindEntityPointerCallbackPointer => "int count = it.Handle->count; Ecs.Assert(it.Handle->entities != null, \"No entities returned, use Iter() or Each() without the entity argument instead.\");",

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

    public static void AddSource(string file, string source, [CallerFilePath] string callerPath = "")
    {
        string filePath = Path.GetFullPath(Path.Combine(callerPath, "..", "..", "..", "Flecs.NET", "Temporary", file));
        string directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath!);

        File.WriteAllText(filePath,  $$"""
        // {{file}}
        // File was auto-generated by
        {{source}}
        """);
    }

    private static string JoinString(int count, string separator, Func<int, string> callback)
    {
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
