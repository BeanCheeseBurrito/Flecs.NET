namespace Flecs.NET.Codegen.Helpers;

public enum Callback
{
    #region Run

    RunCallbackDelegate,
    RunCallbackPointer,

    RunDelegateCallbackDelegate,
    RunDelegateCallbackPointer,

    RunPointerCallbackDelegate,
    RunPointerCallbackPointer,

    #endregion

    #region Iter

    IterCallbackDelegate,
    IterFieldCallbackDelegate,
    IterSpanCallbackDelegate,
    IterPointerCallbackDelegate,

    IterCallbackPointer,
    IterFieldCallbackPointer,
    IterSpanCallbackPointer,
    IterPointerCallbackPointer,

    #endregion

    #region Each

    EachEntityCallbackDelegate,
    EachEntityCallbackPointer,

    EachIterCallbackDelegate,
    EachIterCallbackPointer,

    EachRefCallbackDelegate,
    EachEntityRefCallbackDelegate,
    EachIterRefCallbackDelegate,

    EachRefCallbackPointer,
    EachEntityRefCallbackPointer,
    EachIterRefCallbackPointer,

    EachPointerCallbackDelegate,
    EachEntityPointerCallbackDelegate,
    EachIterPointerCallbackDelegate,

    EachPointerCallbackPointer,
    EachEntityPointerCallbackPointer,
    EachIterPointerCallbackPointer,

    #endregion

    #region Find

    FindEntityCallbackDelegate,
    FindEntityCallbackPointer,

    FindIterCallbackDelegate,
    FindIterCallbackPointer,

    FindRefCallbackDelegate,
    FindEntityRefCallbackDelegate,
    FindIterRefCallbackDelegate,

    FindRefCallbackPointer,
    FindEntityRefCallbackPointer,
    FindIterRefCallbackPointer,

    FindPointerCallbackDelegate,
    FindEntityPointerCallbackDelegate,
    FindIterPointerCallbackDelegate,

    FindPointerCallbackPointer,
    FindEntityPointerCallbackPointer,
    FindIterPointerCallbackPointer,

    #endregion

    #region Observe

    ObserveCallbackDelegate,
    ObserveCallbackPointer,

    ObserveRefCallbackDelegate,
    ObserveRefCallbackPointer,

    ObservePointerCallbackDelegate,
    ObservePointerCallbackPointer,

    ObserveEntityCallbackDelegate,
    ObserveEntityCallbackPointer,

    ObserveEntityRefCallbackDelegate,
    ObserveEntityRefCallbackPointer,

    ObserveEntityPointerCallbackDelegate,
    ObserveEntityPointerCallbackPointer,

    #endregion

    #region Components

    ReadRefCallbackDelegate,
    WriteRefCallbackDelegate,
    InsertRefCallbackDelegate

    #endregion
}
