// /_/src/Flecs.NET/Generated/Alert/Alert.Id.g.cs
// File was auto-generated by /_/src/Flecs.NET.Codegen/Generators/Alert.cs
namespace Flecs.NET.Core;

public unsafe partial struct Alert
{
    /// <inheritdoc cref="Id.IsPair()"/>
    public bool IsPair()
    {
        return Id.IsPair();
    }
    
    /// <inheritdoc cref="Id.IsWildCard()"/>
    public bool IsWildCard()
    {
        return Id.IsWildCard();
    }
    
    /// <inheritdoc cref="Id.IsEntity()"/>
    public bool IsEntity()
    {
        return Id.IsEntity();
    }
    
    /// <inheritdoc cref="Id.ToEntity()"/>
    public Entity ToEntity()
    {
        return Id.ToEntity();
    }
    
    /// <inheritdoc cref="Id.AddFlags(ulong)"/>
    public Entity AddFlags(ulong flags)
    {
        return Id.AddFlags(flags);
    }
    
    /// <inheritdoc cref="Id.RemoveFlags(ulong)"/>
    public Entity RemoveFlags(ulong flags)
    {
        return Id.RemoveFlags(flags);
    }
    
    /// <inheritdoc cref="Id.RemoveFlags()"/>
    public Entity RemoveFlags()
    {
        return Id.RemoveFlags();
    }
    
    /// <inheritdoc cref="Id.RemoveGeneration()"/>
    public Entity RemoveGeneration()
    {
        return Id.RemoveGeneration();
    }
    
    /// <inheritdoc cref="Id.TypeId()"/>
    public Entity TypeId()
    {
        return Id.TypeId();
    }
    
    /// <inheritdoc cref="Id.HasFlags(ulong)"/>
    public bool HasFlags(ulong flags)
    {
        return Id.HasFlags(flags);
    }
    
    /// <inheritdoc cref="Id.HasFlags()"/>
    public bool HasFlags()
    {
        return Id.HasFlags();
    }
    
    /// <inheritdoc cref="Id.Flags()"/>
    public Entity Flags()
    {
        return Id.Flags();
    }
    
    /// <inheritdoc cref="Id.HasRelation(ulong)"/>
    public bool HasRelation(ulong first)
    {
        return Id.HasRelation(first);
    }
    
    /// <inheritdoc cref="Id.First()"/>
    public Entity First()
    {
        return Id.First();
    }
    
    /// <inheritdoc cref="Id.Second()"/>
    public Entity Second()
    {
        return Id.Second();
    }
    
    /// <inheritdoc cref="Id.Str()"/>
    public string Str()
    {
        return Id.Str();
    }
    
    /// <inheritdoc cref="Id.FlagsStr()"/>
    public string FlagsStr()
    {
        return Id.FlagsStr();
    }
    
    /// <inheritdoc cref="Id.CsWorld()"/>
    public World CsWorld()
    {
        return Id.CsWorld();
    }
}