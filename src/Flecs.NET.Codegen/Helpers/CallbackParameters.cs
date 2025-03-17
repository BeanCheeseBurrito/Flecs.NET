namespace Flecs.NET.Codegen.Helpers;

public enum CallbackParameters
{
    None,

    Iter,
    IterField,
    IterSpan,
    IterPointer,
    IterPointerCallback,
    IterDelegateCallback,

    EachRef,
    EachPointer,

    EachEntity,
    EachEntityRef,
    EachEntityPointer,

    EachIter,
    EachIterRef,
    EachIterPointer,

    ObserveRef,
    ObservePointer,
    ObserveEntity,
    ObserveEntityRef,
    ObserveEntityPointer,

    ReadRef
}
