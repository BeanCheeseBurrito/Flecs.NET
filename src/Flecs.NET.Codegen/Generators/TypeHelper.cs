using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class TypeHelper : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"T{i}.g.cs", GenerateTypeHelper(i));
        }
    }

    public static string GenerateTypeHelper(int i)
    {
        return $$"""
            #nullable enable

            using System.Diagnostics;
            using System.Diagnostics.CodeAnalysis;
            using System.Linq;
            using System.Runtime.CompilerServices;
            using Flecs.NET.Utilities;

            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
            internal static unsafe partial class {{Generator.GetTypeName(Type.TypeHelper, i)}}
            {
                private static string[]? _typeNames;
                public static string[] TypeNames => _typeNames ??= [ {{Generator.TypeFullNames[i]}} ];
                
                public static readonly int Tags = {{Generator.Tags[i]}};
                public static readonly int ReferenceTypes = {{Generator.ReferenceTypes[i]}};
                
                private static string GetTypeListString(int fields)
                {
                    return string.Join(", ", Enumerable.Range(0, {{i + 1}})
                        .Where(i => (fields & (1 << i)) != 0)
                        .Select(i => TypeNames[i]));
                }
                
                [Conditional("DEBUG")]
                public static void AssertNoTags()
                {
                    if (Tags == 0)
                        return;
                
                    Ecs.Error($"Cannot use zero-sized structs as generic type arguments for this struct. Remove the following type arguments: {GetTypeListString(Tags)}");
                }
                
                [Conditional("DEBUG")]
                public static void AssertReferenceTypes(bool allowReferenceTypes)
                {
                    if (allowReferenceTypes || ReferenceTypes == 0)
                        return;
                
                    Ecs.Error($"Cannot use managed types as generic type arguments for callback signatures that retrieve pointers or spans. Remove the following type arguments: {GetTypeListString(ReferenceTypes)}");
                }
                
                [Conditional("DEBUG")]
                public static void AssertSparseTypes(ecs_world_t* world, bool allowSparseTypes)
                {
                    if (allowSparseTypes)
                        return;
             
                    int sparseTypes = {{Generator.SparseBitField[i]}};
             
                    if (sparseTypes == 0)
                        return;
             
                    Ecs.Error($"Cannot use sparse components as generic type arguments for this struct when using .Iter() to iterate because sparse fields must be obtained with Iter.FieldAt(). Use .Each()/.Run() to iterate or remove the following types from the list: {GetTypeListString(sparseTypes)}");
                }
            }
            """;
    }
}
