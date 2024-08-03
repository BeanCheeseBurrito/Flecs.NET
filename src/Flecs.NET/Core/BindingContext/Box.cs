using System.Diagnostics.CodeAnalysis;

namespace Flecs.NET.Core.BindingContext;

internal class Box<T>
{
    [MaybeNull]
    public T Value = default!;
    public bool ShouldFree;

    public Box()
    {
    }

    public Box(T value, bool shouldFree = false)
    {
        Value = value;
        ShouldFree = shouldFree;
    }
}
