using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     Id interface.
/// </summary>
public unsafe interface IId
{
    /// <summary>
    ///     A reference to the world.
    /// </summary>
    public ref ecs_world_t* World { get; }

    /// <summary>
    ///     A reference to the id.
    /// </summary>
    public ref Id Id { get; }

    /// <inheritdoc cref="Id.IsPair()"/>
    public bool IsPair();

    /// <inheritdoc cref="Id.IsWildCard()"/>
    public bool IsWildCard();

    /// <inheritdoc cref="Id.IsEntity()"/>
    public bool IsEntity();

    /// <inheritdoc cref="Id.ToEntity()"/>
    public Entity ToEntity();

    /// <inheritdoc cref="Id.AddFlags(ulong)"/>
    public Entity AddFlags(ulong flags);

    /// <inheritdoc cref="Id.RemoveFlags(ulong)"/>
    public Entity RemoveFlags(ulong flags);

    /// <inheritdoc cref="Id.RemoveFlags()"/>
    public Entity RemoveFlags();

    /// <inheritdoc cref="Id.RemoveGeneration()"/>
    public Entity RemoveGeneration();

    /// <inheritdoc cref="Id.TypeId()"/>
    public Entity TypeId();

    /// <inheritdoc cref="Id.HasFlags(ulong)"/>
    public bool HasFlags(ulong flags);

    /// <inheritdoc cref="Id.HasFlags()"/>
    public bool HasFlags();

    /// <inheritdoc cref="Id.Flags()"/>
    public Entity Flags();

    /// <inheritdoc cref="Id.HasRelation(ulong)"/>
    public bool HasRelation(ulong first);

    /// <inheritdoc cref="Id.First()"/>
    public Entity First();

    /// <inheritdoc cref="Id.Second()"/>
    public Entity Second();

    /// <inheritdoc cref="Id.Str()"/>
    public string Str();

    /// <inheritdoc cref="Id.FlagsStr()"/>
    public string FlagsStr();

    /// <inheritdoc cref="Id.CsWorld()"/>
    public World CsWorld();
}
