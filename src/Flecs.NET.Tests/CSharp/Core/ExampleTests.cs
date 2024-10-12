#if NET8_0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core;

// Parallelization needs to be disabled for this test collection because Ecs.Log functions aren't thread safe.
[Collection(nameof(ExampleTests))]
[CollectionDefinition(nameof(ExampleTests), DisableParallelization = true)]
public class ExampleTests
{
    public static IEnumerable<object[]> Examples => typeof(Example).Assembly.GetTypes()
        .Where(type => type.Name != "Explorer" && type.Name != "Playground")
        .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        .Where(method => method.Name == "Main")
        .Select(method => new object[] { method.DeclaringType!.Name, method });

    [Theory]
    [MemberData(nameof(Examples))]
    private void RunTest(string typeName, MethodInfo method)
    {
        _ = typeName;
        method.Invoke(null, method.GetParameters().Length == 0 ? null : [Array.Empty<string>()]);
    }
}

#endif
