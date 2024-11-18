
using System.Diagnostics.CodeAnalysis;

namespace Flecs.NET.Codegen.Helpers;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Type
{
    Alert,
    AlertBuilder,
    Component,
    Entity,
    IIterable,
    IterIterable,
    Observer,
    ObserverBuilder,
    PageIterable,
    Pipeline,
    PipelineBuilder,
    Query,
    QueryBuilder,
    System,
    System_,
    SystemBuilder,
    TimerEntity,
    TypeHelper,
    UntypedComponent,
    WorkerIterable
}
