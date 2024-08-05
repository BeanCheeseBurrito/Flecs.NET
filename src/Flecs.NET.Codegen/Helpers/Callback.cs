namespace Flecs.NET.Codegen.Helpers;

public enum Callback
{
    #region Iter

    IterFieldCallbackDelegate,
    IterSpanCallbackDelegate,
    IterPointerCallbackDelegate,

    IterFieldCallbackPointer,
    IterSpanCallbackPointer,
    IterPointerCallbackPointer,

    #endregion

    #region Each

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

    #region Components

    ReadRefCallbackDelegate,
    WriteRefCallbackDelegate,
    InsertRefCallbackDelegate

    #endregion
}
