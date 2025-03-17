using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class IEntity : GeneratorBase
{
    public override void Generate()
    {
        AddSource("IEntity.Observe.g.cs", GenerateObserveFunctions());
    }

    public static string GenerateObserveFunctions()
    {
        IEnumerable<string> untyped = Generator.GetObserveCallbacks()
            .Select((Callback callback) => $$"""
                    /// <summary>
                    ///     Registers a callback to be called when the provided event is signaled.
                    /// </summary>
                    /// <param name="id">The event id.</param>
                    /// <param name="callback">The callback.</param>
                    /// <returns>Reference to self.</returns>
                    public void Observe(ulong id, {{Generator.GetCallbackType(callback)}} callback);
                    
                    /// <summary>
                    ///     Registers a callback to be called when the provided event is signaled.
                    /// </summary>
                    /// <param name="callback">The callback.</param>
                    /// <typeparam name="T0">The event type.</typeparam>
                    /// <returns>Reference to self.</returns>
                    public void Observe<T0>({{Generator.GetCallbackType(callback)}} callback);
                """);

        IEnumerable<string> typed = Generator.GetObserveCallbacks(0)
            .Select((Callback callback) => $$"""
                    /// <summary>
                    ///     Registers a callback to be called when the provided event is signaled.
                    /// </summary>
                    /// <param name="callback">The callback.</param>
                    /// <typeparam name="T0">The event type.</typeparam>
                    /// <returns>Reference to self.</returns>
                    public void Observe<T0>({{Generator.GetCallbackType(callback)}} callback);
                """);

        return $$"""
            namespace Flecs.NET.Core;
            
            public unsafe partial interface IEntity<TEntity>
            {
            {{string.Join(Separator.DoubleNewLine, untyped.Concat(typed))}}
            }
            """;
    }

}
