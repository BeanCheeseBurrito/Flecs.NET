using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

/// <summary>
///     A wrapper around <see cref="ecs_delete_empty_tables_desc_t"/>.
/// </summary>
public unsafe record struct DeleteEmptyTablesDesc
{
    /// <summary>
    ///     The <see cref="ecs_delete_empty_tables_desc_t"/> object.
    /// </summary>
    public ecs_delete_empty_tables_desc_t Desc;

    /// <summary>
    ///     Free table data when generation > clear_generation.
    /// </summary>
    public ref ushort ClearGeneration => ref Desc.clear_generation;

    /// <summary>
    ///     Delete table when generation > delete_generation.
    /// </summary>
    public ref ushort DeleteGeneration => ref Desc.delete_generation;

    /// <summary>
    ///     Amount of time operation is allowed to spend.
    /// </summary>
    public ref double TimeBudgetSeconds => ref Desc.time_budget_seconds;
}
