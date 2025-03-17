using System;
using System.Linq;
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
    public const string RunDelegateCallbackDelegate = "Ecs.RunDelegateCallback";
    public const string RunPointerCallbackDelegate = "Ecs.RunPointerCallback";

    public const string IterCallbackDelegate = "Ecs.IterCallback";

    public const string EachEntityCallbackDelegate = "Ecs.EachEntityCallback";
    public const string EachIterCallbackDelegate = "Ecs.EachIterCallback";

    public const string FindEntityCallbackDelegate = "Ecs.FindEntityCallback";
    public const string FindIterCallbackDelegate = "Ecs.FindIterCallback";

    public const string ObserveCallbackDelegate = "Ecs.ObserveCallback";
    public const string ObserveRefCallbackDelegate = "Ecs.ObserveRefCallback<T0>";
    public const string ObservePointerCallbackDelegate = "Ecs.ObservePointerCallback<T0>";
    public const string ObserveEntityCallbackDelegate = "Ecs.ObserveEntityCallback";
    public const string ObserveEntityRefCallbackDelegate = "Ecs.ObserveEntityRefCallback<T0>";
    public const string ObserveEntityPointerCallbackDelegate = "Ecs.ObserveEntityPointerCallback<T0>";

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
    public const string RunDelegateCallbackPointer = "delegate*<Iter, Action<Iter>, void>";
    public const string RunPointerCallbackPointer = "delegate*<Iter, delegate*<Iter, void>, void>";

    public const string IterCallbackPointer = "delegate*<Iter, void>";

    public const string EachEntityCallbackPointer = "delegate*<Entity, void>";
    public const string EachIterCallbackPointer = "delegate*<Iter, int, void>";

    public const string FindEntityCallbackPointer = "delegate*<Entity, bool>";
    public const string FindIterCallbackPointer = "delegate*<Iter, int, bool>";

    public const string ObserveCallbackPointer = "delegate*<void>";
    public const string ObserveRefCallbackPointer = "delegate*<ref T0, void>";
    public const string ObservePointerCallbackPointer = "delegate*<T0*, void>";
    public const string ObserveEntityCallbackPointer = "delegate*<Entity, void>";
    public const string ObserveEntityRefCallbackPointer = "delegate*<Entity, ref T0, void>";
    public const string ObserveEntityPointerCallbackPointer = "delegate*<Entity, T0*, void>";

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

    #region Invoker Callback Arguments

    public static readonly string[] IterFieldArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetField<T{i}>({i})");
    public static readonly string[] IterSpanArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetSpan<T{i}>({i})");
    public static readonly string[] IterPointerArguments = CacheJoinedStrings(Separator.Comma, i => $"it.GetPointer<T{i}>({i})");

    public static readonly string[] EachRefArguments = CacheJoinedStrings(Separator.Comma, i => $"ref Managed.GetTypeRef<T{i}>(TFieldGetter.Get<T{i}>(in fields, {i}, i))");
    public static readonly string[] EachPointerArguments = CacheJoinedStrings(Separator.Comma, i => $"TFieldGetter.Get<T{i}>(in fields, {i}, i)");

    public static readonly string[] ReadRefArguments = CacheJoinedStrings(Separator.Comma, i => $"ref Managed.GetTypeRef<T{i}>(pointers[{i}])");
    public static readonly string[] WriteRefArguments = ReadRefArguments;
    public static readonly string[] InsertRefArguments = ReadRefArguments;

    #endregion

    #region Misc

    public static readonly string[] WithChain = CacheJoinedStrings(Separator.None, i => $".With<T{i}>()");

    public static readonly string[] XmlTypeParameters = CacheJoinedStrings(Separator.Space, i => $"<typeparam name=\"T{i}\">The T{i} component type.</typeparam>");

    #endregion

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

    public static Callback[] GetRunCallbacks()
    {
        return
        [
            Callback.RunCallbackDelegate,
            Callback.RunCallbackPointer,
            Callback.RunDelegateCallbackDelegate,
            Callback.RunDelegateCallbackPointer,
            Callback.RunPointerCallbackDelegate,
            Callback.RunPointerCallbackPointer
        ];
    }

    public static Callback[] GetIterCallbacks(int i = -1)
    {
        return i == -1
            ?
            [
                Callback.IterCallbackDelegate,
                Callback.IterCallbackPointer
            ]
            :
            [
                Callback.IterFieldCallbackDelegate,
                Callback.IterFieldCallbackPointer,

                Callback.IterSpanCallbackDelegate,
                Callback.IterSpanCallbackPointer,

                Callback.IterPointerCallbackDelegate,
                Callback.IterPointerCallbackPointer,
            ];
    }

    public static Callback[] GetEachCallbacks(int i = -1)
    {
        return i == -1
            ?
            [
                Callback.EachEntityCallbackDelegate,
                Callback.EachEntityCallbackPointer,
                Callback.EachIterCallbackDelegate,
                Callback.EachIterCallbackPointer
            ]
            :
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
    }

    public static Callback[] GetFindCallbacks(int i = -1)
    {
        return i == -1
            ?
            [
                Callback.FindEntityCallbackDelegate,
                Callback.FindEntityCallbackPointer,
                Callback.FindIterCallbackDelegate,
                Callback.FindIterCallbackPointer
            ]
            :
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
    }

    public static Callback[] GetObserveCallbacks(int i = -1)
    {
        return i == -1
            ?
            [
                Callback.ObserveCallbackDelegate,
                Callback.ObserveCallbackPointer,
                Callback.ObserveEntityCallbackDelegate,
                Callback.ObserveEntityCallbackPointer,
            ]
            :
            [
                Callback.ObserveRefCallbackDelegate,
                Callback.ObserveRefCallbackPointer,
                Callback.ObservePointerCallbackDelegate,
                Callback.ObservePointerCallbackPointer,
                Callback.ObserveEntityRefCallbackDelegate,
                Callback.ObserveEntityRefCallbackPointer,
                Callback.ObserveEntityPointerCallbackDelegate,
                Callback.ObserveEntityPointerCallbackPointer,
            ];
    }

    public static Callback[] GetReadCallbacks()
    {
        return [ Callback.ReadRefCallbackDelegate ];
    }

    public static Callback[] GetWriteCallbacks()
    {
        return [ Callback.WriteRefCallbackDelegate ];
    }

    public static Callback[] GetInsertCallbacks()
    {
        return [ Callback.InsertRefCallbackDelegate ];
    }

    public static string PartialTypeParameters(int count)
    {
        string[] strings = new string[GenericCount];
        for (int i = 0; i < GenericCount; i++)
            strings[i] = i <= count ? $"T{i}" : "_";
        return string.Join(", ", strings);
    }

    public static string GetTypeName(Type type, int i = -1)
    {
        if (type == Type.Component)
            return $"{type}<TComponent>";

        if (type == Type.Types || type == Type.Invoker)
            return $"{type}<{PartialTypeParameters(i)}>";

        if (type == Type.System && i == -1)
            return "System_";

        return i < 0
            ? type.ToString()
            : $"{type}<{TypeParameters[i]}>";
    }

    public static string GetCallbackName(Callback callback, int i = -1)
    {
        return i == -1 ? callback.ToString() : $"{callback}<{TypeParameters[i]}>";
    }

    public static string GetCallbackType(Callback callback, int i = -1)
    {
        return callback switch
        {
            Callback.RunCallbackDelegate => RunCallbackDelegate,
            Callback.RunCallbackPointer => RunCallbackPointer,

            Callback.RunDelegateCallbackDelegate => RunDelegateCallbackDelegate,
            Callback.RunDelegateCallbackPointer => RunDelegateCallbackPointer,

            Callback.RunPointerCallbackDelegate => RunPointerCallbackDelegate,
            Callback.RunPointerCallbackPointer => RunPointerCallbackPointer,

            Callback.IterCallbackDelegate => IterCallbackDelegate,
            Callback.IterCallbackPointer => IterCallbackPointer,

            Callback.EachEntityCallbackDelegate => EachEntityCallbackDelegate,
            Callback.EachEntityCallbackPointer => EachEntityCallbackPointer,
            Callback.EachIterCallbackDelegate => EachIterCallbackDelegate,
            Callback.EachIterCallbackPointer => EachIterCallbackPointer,

            Callback.FindEntityCallbackDelegate => FindEntityCallbackDelegate,
            Callback.FindEntityCallbackPointer => FindEntityCallbackPointer,
            Callback.FindIterCallbackDelegate => FindIterCallbackDelegate,
            Callback.FindIterCallbackPointer => FindIterCallbackPointer,

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

            Callback.ObserveCallbackDelegate => ObserveCallbackDelegate,
            Callback.ObserveCallbackPointer => ObserveCallbackPointer,
            Callback.ObserveRefCallbackDelegate => ObserveRefCallbackDelegate,
            Callback.ObserveRefCallbackPointer => ObserveRefCallbackPointer,
            Callback.ObservePointerCallbackDelegate => ObservePointerCallbackDelegate,
            Callback.ObservePointerCallbackPointer => ObservePointerCallbackPointer,
            Callback.ObserveEntityCallbackDelegate => ObserveEntityCallbackDelegate,
            Callback.ObserveEntityCallbackPointer => ObserveEntityCallbackPointer,
            Callback.ObserveEntityRefCallbackDelegate => ObserveEntityRefCallbackDelegate,
            Callback.ObserveEntityRefCallbackPointer => ObserveEntityRefCallbackPointer,
            Callback.ObserveEntityPointerCallbackDelegate => ObserveEntityPointerCallbackDelegate,
            Callback.ObserveEntityPointerCallbackPointer => ObserveEntityPointerCallbackPointer,

            Callback.ReadRefCallbackDelegate => ReadRefCallbackDelegate[i],
            Callback.WriteRefCallbackDelegate => WriteRefCallbackDelegate[i],
            Callback.InsertRefCallbackDelegate => InsertRefCallbackDelegate[i],

            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static CallbackParameters GetCallbackParameters(Callback callback)
    {
        switch (callback)
        {
            case Callback.ObserveCallbackDelegate:
            case Callback.ObserveCallbackPointer:
                return CallbackParameters.None;
            case Callback.RunDelegateCallbackDelegate:
            case Callback.RunDelegateCallbackPointer:
                return CallbackParameters.IterDelegateCallback;
            case Callback.RunPointerCallbackDelegate:
            case Callback.RunPointerCallbackPointer:
                return CallbackParameters.IterPointerCallback;
            case Callback.RunCallbackDelegate:
            case Callback.RunCallbackPointer:
            case Callback.IterCallbackDelegate:
            case Callback.IterCallbackPointer:
                return CallbackParameters.Iter;
            case Callback.IterFieldCallbackDelegate:
            case Callback.IterFieldCallbackPointer:
                return CallbackParameters.IterField;
            case Callback.IterSpanCallbackDelegate:
            case Callback.IterSpanCallbackPointer:
                return CallbackParameters.IterSpan;
            case Callback.IterPointerCallbackDelegate:
            case Callback.IterPointerCallbackPointer:
                return CallbackParameters.IterPointer;
            case Callback.EachRefCallbackDelegate:
            case Callback.EachRefCallbackPointer:
            case Callback.FindRefCallbackDelegate:
            case Callback.FindRefCallbackPointer:
                return CallbackParameters.EachRef;
            case Callback.EachEntityCallbackDelegate:
            case Callback.EachEntityCallbackPointer:
            case Callback.FindEntityCallbackDelegate:
            case Callback.FindEntityCallbackPointer:
                return CallbackParameters.EachEntity;
            case Callback.EachIterCallbackDelegate:
            case Callback.EachIterCallbackPointer:
            case Callback.FindIterCallbackDelegate:
            case Callback.FindIterCallbackPointer:
                return CallbackParameters.EachIter;
            case Callback.EachEntityRefCallbackDelegate:
            case Callback.EachEntityRefCallbackPointer:
            case Callback.FindEntityRefCallbackDelegate:
            case Callback.FindEntityRefCallbackPointer:
                return CallbackParameters.EachEntityRef;
            case Callback.EachIterRefCallbackDelegate:
            case Callback.EachIterRefCallbackPointer:
            case Callback.FindIterRefCallbackDelegate:
            case Callback.FindIterRefCallbackPointer:
                return CallbackParameters.EachIterRef;
            case Callback.EachPointerCallbackDelegate:
            case Callback.EachPointerCallbackPointer:
            case Callback.FindPointerCallbackDelegate:
            case Callback.FindPointerCallbackPointer:
                return CallbackParameters.EachPointer;
            case Callback.EachEntityPointerCallbackDelegate:
            case Callback.EachEntityPointerCallbackPointer:
            case Callback.FindEntityPointerCallbackDelegate:
            case Callback.FindEntityPointerCallbackPointer:
                return CallbackParameters.EachEntityPointer;
            case Callback.EachIterPointerCallbackDelegate:
            case Callback.EachIterPointerCallbackPointer:
            case Callback.FindIterPointerCallbackDelegate:
            case Callback.FindIterPointerCallbackPointer:
                return CallbackParameters.EachIterPointer;
            case Callback.ObserveRefCallbackDelegate:
            case Callback.ObserveRefCallbackPointer:
                return CallbackParameters.ObserveRef;
            case Callback.ObservePointerCallbackDelegate:
            case Callback.ObservePointerCallbackPointer:
                return CallbackParameters.ObservePointer;
            case Callback.ObserveEntityCallbackDelegate:
            case Callback.ObserveEntityCallbackPointer:
                return CallbackParameters.ObserveEntity;
            case Callback.ObserveEntityRefCallbackDelegate:
            case Callback.ObserveEntityRefCallbackPointer:
                return CallbackParameters.ObserveEntityRef;
            case Callback.ObserveEntityPointerCallbackDelegate:
            case Callback.ObserveEntityPointerCallbackPointer:
                return CallbackParameters.ObserveEntityPointer;
            case Callback.ReadRefCallbackDelegate:
            case Callback.WriteRefCallbackDelegate:
            case Callback.InsertRefCallbackDelegate:
                return CallbackParameters.ReadRef;
            default:
                throw new ArgumentOutOfRangeException(nameof(callback), callback, null);
        }
    }

    public static string GetCallbackArguments(Callback callback, int i = -1)
    {
        return GetCallbackParameters(callback) switch
        {
            CallbackParameters.None => "",
            CallbackParameters.Iter => "it",
            CallbackParameters.IterField => $"it, {IterFieldArguments[i]}",
            CallbackParameters.IterSpan => $"it, {IterSpanArguments[i]}",
            CallbackParameters.IterPointer => $"it, {IterPointerArguments[i]}",
            CallbackParameters.IterPointerCallback => "it, &Callback",
            CallbackParameters.IterDelegateCallback => "it, Callback",
            CallbackParameters.EachRef => EachRefArguments[i],
            CallbackParameters.EachPointer => EachPointerArguments[i],
            CallbackParameters.EachEntity => "new Entity(fields.Iter->world, fields.Iter->entities[i])",
            CallbackParameters.EachEntityRef => $"new Entity(fields.Iter->world, fields.Iter->entities[i]), {EachRefArguments[i]}",
            CallbackParameters.EachEntityPointer => $"new Entity(fields.Iter->world, fields.Iter->entities[i]), {EachPointerArguments[i]}",
            CallbackParameters.EachIter => "fields.Iter, i",
            CallbackParameters.EachIterRef => $"fields.Iter, i, {EachRefArguments[i]}",
            CallbackParameters.EachIterPointer => $"fields.Iter, i, {EachPointerArguments[i]}",
            CallbackParameters.ObserveRef => $"ref Managed.GetTypeRef<T0>(it.Handle->param)",
            CallbackParameters.ObservePointer => $"(T0*)it.Handle->param",
            CallbackParameters.ObserveEntity => "new Entity(it.Handle->world, it.Handle->sources[0])",
            CallbackParameters.ObserveEntityRef => $"new Entity(it.Handle->world, it.Handle->sources[0]), ref Managed.GetTypeRef<T0>(it.Handle->param)",
            CallbackParameters.ObserveEntityPointer => $"new Entity(it.Handle->world, it.Handle->sources[0]), (T0*)it.Handle->param",
            CallbackParameters.ReadRef => ReadRefArguments[i],
            _ => throw new ArgumentOutOfRangeException(nameof(callback), callback, null)
        };
    }

    public static string GetInvokerName(Callback callback, int i = -1)
    {
        switch (callback)
        {
            case Callback.RunCallbackDelegate:
            case Callback.RunCallbackPointer:
            case Callback.RunDelegateCallbackDelegate:
            case Callback.RunDelegateCallbackPointer:
            case Callback.RunPointerCallbackDelegate:
            case Callback.RunPointerCallbackPointer:
                return i == -1 ? "Run" : $"Run<{TypeParameters[i]}>";
            case Callback.IterCallbackDelegate:
            case Callback.IterFieldCallbackDelegate:
            case Callback.IterSpanCallbackDelegate:
            case Callback.IterPointerCallbackDelegate:
            case Callback.IterCallbackPointer:
            case Callback.IterFieldCallbackPointer:
            case Callback.IterSpanCallbackPointer:
            case Callback.IterPointerCallbackPointer:
                return i == -1 ? "Iter" : $"Iter<{TypeParameters[i]}>";
            case Callback.EachEntityCallbackDelegate:
            case Callback.EachEntityCallbackPointer:
            case Callback.EachIterCallbackDelegate:
            case Callback.EachIterCallbackPointer:
            case Callback.EachRefCallbackDelegate:
            case Callback.EachEntityRefCallbackDelegate:
            case Callback.EachIterRefCallbackDelegate:
            case Callback.EachRefCallbackPointer:
            case Callback.EachEntityRefCallbackPointer:
            case Callback.EachIterRefCallbackPointer:
            case Callback.EachPointerCallbackDelegate:
            case Callback.EachEntityPointerCallbackDelegate:
            case Callback.EachIterPointerCallbackDelegate:
            case Callback.EachPointerCallbackPointer:
            case Callback.EachEntityPointerCallbackPointer:
            case Callback.EachIterPointerCallbackPointer:
                return i == -1 ? "Each" : $"Each<{TypeParameters[i]}>";
            case Callback.FindEntityCallbackDelegate:
            case Callback.FindEntityCallbackPointer:
            case Callback.FindIterCallbackDelegate:
            case Callback.FindIterCallbackPointer:
            case Callback.FindRefCallbackDelegate:
            case Callback.FindEntityRefCallbackDelegate:
            case Callback.FindIterRefCallbackDelegate:
            case Callback.FindRefCallbackPointer:
            case Callback.FindEntityRefCallbackPointer:
            case Callback.FindIterRefCallbackPointer:
            case Callback.FindPointerCallbackDelegate:
            case Callback.FindEntityPointerCallbackDelegate:
            case Callback.FindIterPointerCallbackDelegate:
            case Callback.FindPointerCallbackPointer:
            case Callback.FindEntityPointerCallbackPointer:
            case Callback.FindIterPointerCallbackPointer:
                return i == -1 ? "Find" : $"Find<{TypeParameters[i]}>";
            case Callback.ObserveCallbackDelegate:
            case Callback.ObserveCallbackPointer:
            case Callback.ObserveRefCallbackDelegate:
            case Callback.ObserveRefCallbackPointer:
            case Callback.ObservePointerCallbackDelegate:
            case Callback.ObservePointerCallbackPointer:
            case Callback.ObserveEntityCallbackDelegate:
            case Callback.ObserveEntityCallbackPointer:
            case Callback.ObserveEntityRefCallbackDelegate:
            case Callback.ObserveEntityRefCallbackPointer:
            case Callback.ObserveEntityPointerCallbackDelegate:
            case Callback.ObserveEntityPointerCallbackPointer:
                return i == -1 ? "Observe" : $"Observe<{TypeParameters[i]}>";
            case Callback.ReadRefCallbackDelegate:
                return i == -1 ? "Read" : $"Read<{TypeParameters[i]}>";
            case Callback.WriteRefCallbackDelegate:
                return i == -1 ? "Write" : $"Write<{TypeParameters[i]}>";
            case Callback.InsertRefCallbackDelegate:
                return i == -1 ? "Insert" : $"Insert<{TypeParameters[i]}>";
            default:
                throw new ArgumentOutOfRangeException(nameof(callback), callback, null);
        }
    }

    public static string GetInvokerReturnType(Callback callback)
    {
        switch (callback)
        {
            case Callback.FindEntityCallbackDelegate:
            case Callback.FindEntityCallbackPointer:
            case Callback.FindIterCallbackDelegate:
            case Callback.FindIterCallbackPointer:
            case Callback.FindRefCallbackDelegate:
            case Callback.FindEntityRefCallbackDelegate:
            case Callback.FindIterRefCallbackDelegate:
            case Callback.FindRefCallbackPointer:
            case Callback.FindEntityRefCallbackPointer:
            case Callback.FindIterRefCallbackPointer:
            case Callback.FindPointerCallbackDelegate:
            case Callback.FindEntityPointerCallbackDelegate:
            case Callback.FindIterPointerCallbackDelegate:
            case Callback.FindPointerCallbackPointer:
            case Callback.FindEntityPointerCallbackPointer:
            case Callback.FindIterPointerCallbackPointer:
                return "Entity";
            case Callback.RunCallbackDelegate:
            case Callback.RunCallbackPointer:
            case Callback.RunDelegateCallbackDelegate:
            case Callback.RunDelegateCallbackPointer:
            case Callback.RunPointerCallbackDelegate:
            case Callback.RunPointerCallbackPointer:
            case Callback.IterCallbackDelegate:
            case Callback.IterFieldCallbackDelegate:
            case Callback.IterSpanCallbackDelegate:
            case Callback.IterPointerCallbackDelegate:
            case Callback.IterCallbackPointer:
            case Callback.IterFieldCallbackPointer:
            case Callback.IterSpanCallbackPointer:
            case Callback.IterPointerCallbackPointer:
            case Callback.EachEntityCallbackDelegate:
            case Callback.EachEntityCallbackPointer:
            case Callback.EachIterCallbackDelegate:
            case Callback.EachIterCallbackPointer:
            case Callback.EachRefCallbackDelegate:
            case Callback.EachEntityRefCallbackDelegate:
            case Callback.EachIterRefCallbackDelegate:
            case Callback.EachRefCallbackPointer:
            case Callback.EachEntityRefCallbackPointer:
            case Callback.EachIterRefCallbackPointer:
            case Callback.EachPointerCallbackDelegate:
            case Callback.EachEntityPointerCallbackDelegate:
            case Callback.EachIterPointerCallbackDelegate:
            case Callback.EachPointerCallbackPointer:
            case Callback.EachEntityPointerCallbackPointer:
            case Callback.EachIterPointerCallbackPointer:
            case Callback.ObserveCallbackDelegate:
            case Callback.ObserveCallbackPointer:
            case Callback.ObserveRefCallbackDelegate:
            case Callback.ObserveRefCallbackPointer:
            case Callback.ObservePointerCallbackDelegate:
            case Callback.ObservePointerCallbackPointer:
            case Callback.ObserveEntityCallbackDelegate:
            case Callback.ObserveEntityCallbackPointer:
            case Callback.ObserveEntityRefCallbackDelegate:
            case Callback.ObserveEntityRefCallbackPointer:
            case Callback.ObserveEntityPointerCallbackDelegate:
            case Callback.ObserveEntityPointerCallbackPointer:
            case Callback.ReadRefCallbackDelegate:
            case Callback.WriteRefCallbackDelegate:
            case Callback.InsertRefCallbackDelegate:
                return "void";
            default:
                throw new ArgumentOutOfRangeException(nameof(callback), callback, null);
        }
    }

    public static bool GetCallbackIsUnmanaged(Callback callback)
    {
        switch (callback)
        {
            case Callback.IterSpanCallbackDelegate:
            case Callback.IterSpanCallbackPointer:
            case Callback.IterPointerCallbackDelegate:
            case Callback.IterPointerCallbackPointer:
            case Callback.EachPointerCallbackDelegate:
            case Callback.EachPointerCallbackPointer:
            case Callback.EachEntityPointerCallbackDelegate:
            case Callback.EachEntityPointerCallbackPointer:
            case Callback.EachIterPointerCallbackDelegate:
            case Callback.EachIterPointerCallbackPointer:
            case Callback.FindPointerCallbackDelegate:
            case Callback.FindPointerCallbackPointer:
            case Callback.FindEntityPointerCallbackDelegate:
            case Callback.FindEntityPointerCallbackPointer:
            case Callback.FindIterPointerCallbackDelegate:
            case Callback.FindIterPointerCallbackPointer:
            case Callback.ObservePointerCallbackDelegate:
            case Callback.ObservePointerCallbackPointer:
            case Callback.ObserveEntityPointerCallbackDelegate:
            case Callback.ObserveEntityPointerCallbackPointer:
                return true;
            case Callback.RunCallbackDelegate:
            case Callback.RunCallbackPointer:
            case Callback.RunDelegateCallbackDelegate:
            case Callback.RunDelegateCallbackPointer:
            case Callback.RunPointerCallbackDelegate:
            case Callback.RunPointerCallbackPointer:
            case Callback.IterCallbackDelegate:
            case Callback.IterFieldCallbackDelegate:
            case Callback.IterCallbackPointer:
            case Callback.IterFieldCallbackPointer:
            case Callback.EachEntityCallbackDelegate:
            case Callback.EachEntityCallbackPointer:
            case Callback.EachIterCallbackDelegate:
            case Callback.EachIterCallbackPointer:
            case Callback.EachRefCallbackDelegate:
            case Callback.EachEntityRefCallbackDelegate:
            case Callback.EachIterRefCallbackDelegate:
            case Callback.EachRefCallbackPointer:
            case Callback.EachEntityRefCallbackPointer:
            case Callback.EachIterRefCallbackPointer:
            case Callback.FindEntityCallbackDelegate:
            case Callback.FindEntityCallbackPointer:
            case Callback.FindIterCallbackDelegate:
            case Callback.FindIterCallbackPointer:
            case Callback.FindRefCallbackDelegate:
            case Callback.FindEntityRefCallbackDelegate:
            case Callback.FindIterRefCallbackDelegate:
            case Callback.FindRefCallbackPointer:
            case Callback.FindEntityRefCallbackPointer:
            case Callback.FindIterRefCallbackPointer:
            case Callback.ObserveCallbackDelegate:
            case Callback.ObserveCallbackPointer:
            case Callback.ObserveRefCallbackDelegate:
            case Callback.ObserveRefCallbackPointer:
            case Callback.ObserveEntityCallbackDelegate:
            case Callback.ObserveEntityCallbackPointer:
            case Callback.ObserveEntityRefCallbackDelegate:
            case Callback.ObserveEntityRefCallbackPointer:
            case Callback.ReadRefCallbackDelegate:
            case Callback.WriteRefCallbackDelegate:
            case Callback.InsertRefCallbackDelegate:
                return false;
            default:
                throw new ArgumentOutOfRangeException(nameof(callback), callback, null);
        }
    }

    // A .Run() callback signature that take a secondary callback require .Iter() or .Each() to be called to finish building
    // systems and observers.
    public static bool RunCallbackBuilds(Callback callback)
    {
        switch (callback)
        {
            case Callback.RunCallbackDelegate:
            case Callback.RunCallbackPointer:
                return true;
            case Callback.RunDelegateCallbackDelegate:
            case Callback.RunDelegateCallbackPointer:
            case Callback.RunPointerCallbackDelegate:
            case Callback.RunPointerCallbackPointer:
                return false;
            case Callback.IterCallbackDelegate:
            case Callback.IterFieldCallbackDelegate:
            case Callback.IterSpanCallbackDelegate:
            case Callback.IterPointerCallbackDelegate:
            case Callback.IterCallbackPointer:
            case Callback.IterFieldCallbackPointer:
            case Callback.IterSpanCallbackPointer:
            case Callback.IterPointerCallbackPointer:
            case Callback.EachEntityCallbackDelegate:
            case Callback.EachEntityCallbackPointer:
            case Callback.EachIterCallbackDelegate:
            case Callback.EachIterCallbackPointer:
            case Callback.EachRefCallbackDelegate:
            case Callback.EachEntityRefCallbackDelegate:
            case Callback.EachIterRefCallbackDelegate:
            case Callback.EachRefCallbackPointer:
            case Callback.EachEntityRefCallbackPointer:
            case Callback.EachIterRefCallbackPointer:
            case Callback.EachPointerCallbackDelegate:
            case Callback.EachEntityPointerCallbackDelegate:
            case Callback.EachIterPointerCallbackDelegate:
            case Callback.EachPointerCallbackPointer:
            case Callback.EachEntityPointerCallbackPointer:
            case Callback.EachIterPointerCallbackPointer:
            case Callback.FindEntityCallbackDelegate:
            case Callback.FindEntityCallbackPointer:
            case Callback.FindIterCallbackDelegate:
            case Callback.FindIterCallbackPointer:
            case Callback.FindRefCallbackDelegate:
            case Callback.FindEntityRefCallbackDelegate:
            case Callback.FindIterRefCallbackDelegate:
            case Callback.FindRefCallbackPointer:
            case Callback.FindEntityRefCallbackPointer:
            case Callback.FindIterRefCallbackPointer:
            case Callback.FindPointerCallbackDelegate:
            case Callback.FindEntityPointerCallbackDelegate:
            case Callback.FindIterPointerCallbackDelegate:
            case Callback.FindPointerCallbackPointer:
            case Callback.FindEntityPointerCallbackPointer:
            case Callback.FindIterPointerCallbackPointer:
            case Callback.ObserveCallbackDelegate:
            case Callback.ObserveCallbackPointer:
            case Callback.ObserveRefCallbackDelegate:
            case Callback.ObserveRefCallbackPointer:
            case Callback.ObservePointerCallbackDelegate:
            case Callback.ObservePointerCallbackPointer:
            case Callback.ObserveEntityCallbackDelegate:
            case Callback.ObserveEntityCallbackPointer:
            case Callback.ObserveEntityRefCallbackDelegate:
            case Callback.ObserveEntityRefCallbackPointer:
            case Callback.ObserveEntityPointerCallbackDelegate:
            case Callback.ObserveEntityPointerCallbackPointer:
            case Callback.ReadRefCallbackDelegate:
            case Callback.WriteRefCallbackDelegate:
            case Callback.InsertRefCallbackDelegate:
            default:
                throw new ArgumentOutOfRangeException(nameof(callback), callback, null);
        }
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
