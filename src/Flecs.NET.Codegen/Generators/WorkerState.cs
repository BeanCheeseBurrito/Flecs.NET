using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class WorkerState : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"T{i}.g.cs", GenerateWorkerStateStructs(i));
        }
    }

    public static string GenerateWorkerStateStructs(int i)
    {
        IEnumerable<string> structs = Generator.CallbacksRunAndIterAndEach.Select((Callback callback) => $$"""
                public struct {{callback}}
                {
                    public CountdownEvent Countdown;
                    public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker;
                    public {{Generator.GetCallbackType(callback, i)}} Callback;
                }
            """);

        return $$"""
            using System;
            using System.Threading;

            namespace Flecs.NET.Core;

            public static unsafe partial class {{Generator.GetTypeName(Type.WorkerState, i)}}
            {
            {{string.Join(Separator.DoubleNewLine, structs)}}
            }
            """;
    }
}
