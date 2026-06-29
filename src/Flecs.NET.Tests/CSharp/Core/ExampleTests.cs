using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

// Parallelization needs to be disabled for this test collection because Ecs.Log functions aren't thread safe.
[Collection(nameof(ExampleTests))]
[CollectionDefinition(nameof(ExampleTests), DisableParallelization = true)]
public class ExampleTests
{
    [Fact]
    private void HelloWorld()
    {
        global::HelloWorld.Main();
    }

    [Fact]
    private void SimpleModule()
    {
        global::SimpleModule.Main();
    }

    [Fact]
    private void Entities_Basics()
    {
        global::Entities_Basics.Main();
    }

    [Fact]
    private void Entities_Hierarchy()
    {
        global::Entities_Hierarchy.Main();
    }

    [Fact]
    private void Entities_Hooks()
    {
        global::Entities_Hooks.Main();
    }

    [Fact]
    private void Entities_IterateComponents()
    {
        global::Entities_IterateComponents.Main();
    }

    [Fact]
    private void Entities_MultiSetGet()
    {
        global::Entities_MultiSetGet.Main();
    }

    [Fact]
    private void Entities_Prefab()
    {
        global::Entities_Prefab.Main();
    }

    [Fact]
    private void Entities_SettingComponents()
    {
        global::Entities_SettingComponents.Main();
    }

    [Fact]
    private void GameMechanics_InventorySystem()
    {
        global::GameMechanics_InventorySystem.Main();
    }

    [Fact]
    private void GameMechanics_SceneManagement()
    {
        global::GameMechanics_SceneManagement.Main();
    }

    [Fact]
    private void Observers_Basics()
    {
        global::Observers_Basics.Main();
    }

    [Fact]
    private void Observers_CustomEvent()
    {
        global::Observers_CustomEvent.Main();
    }

    [Fact]
    private void Observers_EnqueueEntityEvent()
    {
        global::Observers_EnqueueEntityEvent.Main();
    }

    [Fact]
    private void Observers_EnqueueEvent()
    {
        global::Observers_EnqueueEvent.Main();
    }

    [Fact]
    private void Observers_EntityEvent()
    {
        global::Observers_EntityEvent.Main();
    }

    [Fact]
    private void Observers_Monitor()
    {
        global::Observers_Monitor.Main();
    }

    [Fact]
    private void Observers_Propagate()
    {
        global::Observers_Propagate.Main();
    }

    [Fact]
    private void Observers_TwoComponents()
    {
        global::Observers_TwoComponents.Main();
    }

    [Fact]
    private void Observers_YieldExisting()
    {
        global::Observers_YieldExisting.Main();
    }

    [Fact]
    private void Prefabs_Basics()
    {
        global::Prefabs_Basics.Main();
    }

    [Fact]
    private void Prefabs_Hierarchy()
    {
        global::Prefabs_Hierarchy.Main();
    }

    [Fact]
    private void Prefabs_NestedPrefabs()
    {
        global::Prefabs_NestedPrefabs.Main();
    }

    [Fact]
    private void Prefabs_Override()
    {
        global::Prefabs_Override.Main();
    }

    [Fact]
    private void Prefabs_Slots()
    {
        global::Prefabs_Slots.Main();
    }

    [Fact]
    private void Prefabs_TypedPrefabs()
    {
        global::Prefabs_TypedPrefabs.Main();
    }

    [Fact]
    private void Prefabs_Variant()
    {
        global::Prefabs_Variant.Main();
    }

    [Fact]
    private void Queries_BasicIteration()
    {
        global::Queries_BasicIteration.Main();
    }

    [Fact]
    private void Queries_Basics()
    {
        global::Queries_Basics.Main();
    }

    [Fact]
    private void Queries_ChangeTracking()
    {
        global::Queries_ChangeTracking.Main();
    }

    [Fact]
    private void Queries_ComponentInheritance()
    {
        global::Queries_ComponentInheritance.Main();
    }

    [Fact]
    private void Queries_CyclicVariables()
    {
        global::Queries_CyclicVariables.Main();
    }

    [Fact]
    private void Queries_Facts()
    {
        global::Queries_Facts.Main();
    }

    [Fact]
    private void Queries_FindEntity()
    {
        global::Queries_FindEntity.Main();
    }

    [Fact]
    private void Queries_GroupBy()
    {
        global::Queries_GroupBy.Main();
    }

    [Fact]
    private void Queries_GroupByCallbacks()
    {
        global::Queries_GroupByCallbacks.Main();
    }

    [Fact]
    private void Queries_GroupByCustom()
    {
        global::Queries_GroupByCustom.Main();
    }

    [Fact]
    private void Queries_GroupIter()
    {
        global::Queries_GroupIter.Main();
    }

    [Fact]
    private void Queries_Hierarchy()
    {
        global::Queries_Hierarchy.Main();
    }

    [Fact]
    private void Queries_Iter()
    {
        global::Queries_Iter.Main();
    }

    [Fact]
    private void Queries_IterTargets()
    {
        global::Queries_IterTargets.Main();
    }

    [Fact]
    private void Queries_SettingVariables()
    {
        global::Queries_SettingVariables.Main();
    }

    [Fact]
    private void Queries_Singleton()
    {
        global::Queries_Singleton.Main();
    }

    [Fact]
    private void Queries_Sorting()
    {
        global::Queries_Sorting.Main();
    }

    [Fact]
    private void Queries_TransitiveQueries()
    {
        global::Queries_TransitiveQueries.Main();
    }

    [Fact]
    private void Queries_Variables()
    {
        global::Queries_Variables.Main();
    }

    [Fact]
    private void Queries_Wildcards()
    {
        global::Queries_Wildcards.Main();
    }

    [Fact]
    private void Queries_With()
    {
        global::Queries_With.Main();
    }

    [Fact]
    private void Queries_Without()
    {
        global::Queries_Without.Main();
    }

    [Fact]
    private void Queries_WorldQuery()
    {
        global::Queries_WorldQuery.Main();
    }

    [Fact]
    private void Reflection_Basics()
    {
        global::Reflection_Basics.Main();
    }

    [Fact]
    private void Reflection_BasicsBitmask()
    {
        global::Reflection_BasicsBitmask.Main();
    }

    [Fact]
    private void Reflection_BasicsDeserialize()
    {
        global::Reflection_BasicsDeserialize.Main();
    }

    [Fact]
    private void Reflection_BasicsEnum()
    {
        global::Reflection_BasicsEnum.Main();
    }

    [Fact]
    private void Reflection_BasicsJson()
    {
        global::Reflection_BasicsJson.Main();
    }

    [Fact]
    private void Reflection_EntityType()
    {
        global::Reflection_EntityType.Main();
    }

    [Fact]
    private void Reflection_MemberRanges()
    {
        global::Reflection_MemberRanges.Main();
    }

    [Fact]
    private void Reflection_NestedSetMember()
    {
        global::Reflection_NestedSetMember.Main();
    }

    [Fact]
    private void Reflection_NestedStruct()
    {
        global::Reflection_NestedStruct.Main();
    }

    [Fact]
    private void Reflection_QueryToCustomJson()
    {
        global::Reflection_QueryToCustomJson.Main();
    }

    [Fact]
    private void Reflection_QueryToJson()
    {
        global::Reflection_QueryToJson.Main();
    }

    [Fact]
    private void Reflection_RuntimeComponent()
    {
        global::Reflection_RuntimeComponent.Main();
    }

    [Fact]
    private void Reflection_RuntimeNestedComponent()
    {
        global::Reflection_RuntimeNestedComponent.Main();
    }

    [Fact]
    private void Reflection_Units()
    {
        global::Reflection_Units.Main();
    }

    [Fact]
    private void Reflection_WorldSerializeDeserialize()
    {
        global::Reflection_WorldSerializeDeserialize.Main();
    }

    [Fact]
    private void Relationships_Basics()
    {
        global::Relationships_Basics.Main();
    }

    [Fact]
    private void Relationships_EnumRelations()
    {
        global::Relationships_EnumRelations.Main();
    }

    [Fact]
    private void Relationships_ExclusiveRelations()
    {
        global::Relationships_ExclusiveRelations.Main();
    }

    [Fact]
    private void Relationships_RelationComponent()
    {
        global::Relationships_RelationComponent.Main();
    }

    [Fact]
    private void Relationships_SymmetricRelations()
    {
        global::Relationships_SymmetricRelations.Main();
    }

    [Fact]
    private void Systems_Basics()
    {
        global::Systems_Basics.Main();
    }

    [Fact]
    private void Systems_CustomPhases()
    {
        global::Systems_CustomPhases.Main();
    }

    [Fact]
    private void Systems_CustomPhasesNoBuiltIn()
    {
        global::Systems_CustomPhasesNoBuiltIn.Main();
    }

    [Fact]
    private void Systems_CustomPipeline()
    {
        global::Systems_CustomPipeline.Main();
    }

    [Fact]
    private void Systems_CustomRunner()
    {
        global::Systems_CustomRunner.Main();
    }

    [Fact]
    private void Systems_DeltaTime()
    {
        global::Systems_DeltaTime.Main();
    }

    [Fact]
    private void Systems_Immediate()
    {
        global::Systems_Immediate.Main();
    }

    [Fact]
    private void Systems_MutateEntity()
    {
        global::Systems_MutateEntity.Main();
    }

    [Fact]
    private void Systems_MutateEntityHandle()
    {
        global::Systems_MutateEntityHandle.Main();
    }

    [Fact]
    private void Systems_Pipeline()
    {
        global::Systems_Pipeline.Main();
    }

    [Fact]
    private void Systems_RunWithCallback()
    {
        global::Systems_RunWithCallback.Main();
    }

    [Fact]
    private void Systems_StartupSystem()
    {
        global::Systems_StartupSystem.Main();
    }

    [Fact]
    private void Systems_SyncPoint()
    {
        global::Systems_SyncPoint.Main();
    }

    [Fact]
    private void Systems_SyncPointDelete()
    {
        global::Systems_SyncPointDelete.Main();
    }

    [Fact]
    private void Systems_SystemCtx()
    {
        global::Systems_SystemCtx.Main();
    }

    [Fact]
    private void Systems_TargetFps()
    {
        global::Systems_TargetFps.Main();
    }

    [Fact]
    private void Systems_TimeInterval()
    {
        global::Systems_TimeInterval.Main();
    }
}
