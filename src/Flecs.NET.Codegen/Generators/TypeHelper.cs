using System.Diagnostics.CodeAnalysis;
using Flecs.NET.Codegen.Helpers;
using Microsoft.CodeAnalysis;

[Generator]
[SuppressMessage("ReSharper", "CheckNamespace")]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
public class TypeHelper : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
        {
            for (int i = 0; i < Generator.GenericCount; i++)
            {
                Generator.AddSource(postContext, $"T{i}.g.cs", GenerateTypeHelper(i));
            }
        });
    }

    public static string GenerateTypeHelper(int i)
    {
        return $$"""
        #nullable enable
        
        using System.Diagnostics;
        using System.Diagnostics.CodeAnalysis;
        using System.Linq;
        using System.Runtime.CompilerServices;
        
        namespace Flecs.NET.Core;
        
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        internal static partial class {{Generator.GetTypeName(Type.TypeHelper, i)}}
        {
            private static string[]? _typeNames;
            public static string[] TypeNames => _typeNames ??= [ {{Generator.TypeFullNames[i]}} ];
            
            public static readonly int Tags = {{Generator.Tags[i]}};
            public static readonly int ReferenceTypes = {{Generator.ReferenceTypes[i]}};
            
            [Conditional("DEBUG")]
            public static void AssertNoTags()
            {
                if (Tags == 0)
                    return;
            
                string tags = string.Join(", ", Enumerable.Range(0, {{i + 1}})
                    .Where(i => (Tags & (1 << i)) != 0)
                    .Select(i => TypeNames[i]));
            
                Ecs.Error($"Cannot use zero-sized structs as generic type arguments for this struct. Remove the following type arguments: {tags}");
            }
            
            [Conditional("DEBUG")]
            public static void AssertReferenceTypes(bool allowReferenceTypes)
            {
                if (allowReferenceTypes || ReferenceTypes == 0)
                    return;
            
                string referenceTypes = string.Join(", ", Enumerable.Range(0, {{i + 1}})
                    .Where(i => (ReferenceTypes & (1 << i)) != 0)
                    .Select(i => TypeNames[i]));
            
                Ecs.Error($"Cannot use managed types as generic type arguments for callback signatures that retrieve pointers or spans. Remove the following type arguments: {referenceTypes}");
            }
        }
        """;
    }
}
