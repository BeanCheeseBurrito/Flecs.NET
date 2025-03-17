using System.Collections.Generic;
using System.Linq;
using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class IIterable : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
        {
            AddSource($"IIterable/T{i + 1}.g.cs", GenerateIIterableInterface(i));
        }
    }

    public static string GenerateIIterableInterface(int i)
    {
        IEnumerable<string> iterators = Generator.GetRunCallbacks()
            .Concat(Generator.GetIterCallbacks(i))
            .Concat(Generator.GetEachCallbacks(i))
            .Concat(Generator.GetFindCallbacks(i))
            .Select((Callback callback) => $$"""
                /// <summary>
                ///     Iterates the iterable object using the provided .{{Generator.GetInvokerName(callback)}} callback.
                /// </summary>
                /// <param name="callback">The callback.</param>
                public {{Generator.GetInvokerReturnType(callback)}} {{Generator.GetInvokerName(callback)}}({{Generator.GetCallbackType(callback, i)}} callback);
            """);

        return $$"""
            using System;

            using static Flecs.NET.Bindings.flecs;

            namespace Flecs.NET.Core;

            /// <summary>
            ///     Interface for iterable objects.
            /// </summary>
            /// {{Generator.XmlTypeParameters[i]}}
            public unsafe interface {{Generator.GetTypeName(Type.IIterable, i)}} : IIterableBase
            {
                /// <inheritdoc cref="IIterable.Page(int, int)"/>
                public {{Generator.GetTypeName(Type.PageIterable, i)}} Page(int offset, int limit);
                
                /// <inheritdoc cref="IIterable.Worker(int, int)"/>
                public {{Generator.GetTypeName(Type.WorkerIterable, i)}} Worker(int index, int count);
                
                /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.World)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(World world = default);
                
                /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.Iter)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Iter it);
                
                /// <inheritdoc cref="IIterable.Iter(Flecs.NET.Core.Entity)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} Iter(Entity entity);
            
                /// <inheritdoc cref="IIterable.Count()"/>
                public int Count();
                
                /// <inheritdoc cref="IIterable.IsTrue()"/>
                public bool IsTrue();
                
                /// <inheritdoc cref="IIterable.First()"/>
                public Entity First();
                
                /// <inheritdoc cref="IIterable.SetVar(int, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(int varId, ulong value);
                
                /// <inheritdoc cref="IIterable.SetVar(string, ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ulong value);
                
                /// <inheritdoc cref="IIterable.SetVar(string, ecs_table_t*)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_t* value);
                
                /// <inheritdoc cref="IIterable.SetVar(string, ecs_table_range_t)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, ecs_table_range_t value);
                
                /// <inheritdoc cref="IIterable.SetVar(string, Table)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetVar(string name, Table value);
                
                /// <inheritdoc cref="IIterable.SetGroup(ulong)"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup(ulong groupId);
                
                /// <inheritdoc cref="IIterable.SetGroup{T}()"/>
                public {{Generator.GetTypeName(Type.IterIterable, i)}} SetGroup<T>();
                
            {{string.Join(Separator.DoubleNewLine, iterators)}}
            }
            """;
    }

    public static string GenerateIterators(Type type, int i)
    {
        string GenerateComment(Callback callback)
        {
            return $$"""
                    /// <summary>
                    ///     Iterates the <see cref="{{type}}"/> using the provided .{{Generator.GetInvokerName(callback)}} callback.
                    /// </summary>
                    /// <param name="callback">The callback.</param>
                """;
        }

        IEnumerable<string> run = Generator.GetRunCallbacks().Select((Callback callback) => $$"""
            {{GenerateComment(callback)}}
                public void {{Generator.GetInvokerName(callback)}}({{Generator.GetCallbackType(callback)}} callback)
                {
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Run<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback)}}>(ref this, callback);
                }
                
            {{GenerateComment(callback)}}
                public void RunJob({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Job<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback)}}>(ref this, callback);
                }
            """);

        IEnumerable<string> iter = Generator.GetIterCallbacks(i).Select((Callback callback) => $$"""
            {{GenerateComment(callback)}}
                public void Iter({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertSparseTypes(Ecs.GetIterableWorld(ref this), true);
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Iter<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback, i)}}>(ref this, callback);
                }
                
            {{GenerateComment(callback)}}
                public void IterJob({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Job<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback, i)}}>(ref this, callback);
                }
            """);

        IEnumerable<string> each = Generator.GetEachCallbacks(i).Select((Callback callback) => $$"""
            {{GenerateComment(callback)}}
                public void Each({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Each<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback, i)}}>(ref this, callback);
                }
                
            {{GenerateComment(callback)}}
                public void EachJob({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    {{Generator.GetTypeName(Type.Invoker, i)}}.Job<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback, i)}}>(ref this, callback);
                }
            """);

        IEnumerable<string> find = Generator.GetFindCallbacks(i).Select((Callback callback) => $$"""
            {{GenerateComment(callback)}}
                public Entity Find({{Generator.GetCallbackType(callback, i)}} callback)
                {
                    {{Generator.GetTypeName(Type.Types, i)}}.AssertReferenceTypes({{(Generator.GetCallbackIsUnmanaged(callback) ? "false" : "true")}});
                    return {{Generator.GetTypeName(Type.Invoker, i)}}.Find<{{Generator.GetTypeName(type, i)}}, {{Generator.GetCallbackName(callback, i)}}>(ref this, callback);
                }
            """);

        return $$"""
            using System;
            using Flecs.NET.Core.Invokers;
            using Flecs.NET.Utilities;

            namespace Flecs.NET.Core;

            public unsafe partial struct {{Generator.GetTypeName(type, i)}}
            {
            {{string.Join(Separator.DoubleNewLine, run.Concat(iter).Concat(each).Concat(find))}}
            }
            """;
    }
}
