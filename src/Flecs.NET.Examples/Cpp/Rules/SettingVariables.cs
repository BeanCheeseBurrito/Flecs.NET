// This example extends the component_inheritance example, and shows how
// we can use a single rule to match units from different players and platoons
// by setting query variables before we iterate.
//
// The units in this example belong to a platoon, with the platoons belonging
// to a player.

#if Cpp_Rules_SettingVariables

using Flecs.NET.Core;

using World world = World.Create();


public struct Likes { }

#endif
