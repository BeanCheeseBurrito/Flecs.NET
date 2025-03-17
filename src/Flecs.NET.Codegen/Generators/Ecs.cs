using Flecs.NET.Codegen.Helpers;

namespace Flecs.NET.Codegen.Generators;

public class Ecs : GeneratorBase
{
    public override void Generate()
    {
        for (int i = 0; i < Generator.GenericCount; i++)
            AddSource($"Delegates/T{i + 1}.g.cs", GenerateDelegates(i));
    }

    private static string GenerateDelegates(int i)
    {
        return $$"""
            using System;

            namespace Flecs.NET.Core;

            public static unsafe partial class Ecs
            {
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/> and {{i + 1}} <see cref="Field{T}"/> arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void IterFieldCallback<{{Generator.TypeParameters[i]}}>(Iter it, {{Generator.FieldParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/> and {{i + 1}} <see cref="Span{T}"/> arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void IterSpanCallback<{{Generator.TypeParameters[i]}}>(Iter it, {{Generator.SpanParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/> and {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void IterPointerCallback<{{Generator.TypeParameters[i]}}>(Iter it, {{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void EachRefCallback<{{Generator.TypeParameters[i]}}>({{Generator.RefParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Entity"/> and {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void EachEntityRefCallback<{{Generator.TypeParameters[i]}}>(Entity entity, {{Generator.RefParameters[i]}});
            
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/>, an <see cref="int"/>, and {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void EachIterRefCallback<{{Generator.TypeParameters[i]}}>(Iter it, int i, {{Generator.RefParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void EachPointerCallback<{{Generator.TypeParameters[i]}}>({{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Entity"/> and {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void EachEntityPointerCallback<{{Generator.TypeParameters[i]}}>(Entity entity, {{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/>, an <see cref="int"/>, and {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void EachIterPointerCallback<{{Generator.TypeParameters[i]}}>(Iter it, int i, {{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate bool FindRefCallback<{{Generator.TypeParameters[i]}}>({{Generator.RefParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Entity"/> and {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate bool FindEntityRefCallback<{{Generator.TypeParameters[i]}}>(Entity entity, {{Generator.RefParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/>, an <see cref="int"/>, and {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate bool FindIterRefCallback<{{Generator.TypeParameters[i]}}>(Iter it, int i, {{Generator.RefParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate bool FindPointerCallback<{{Generator.TypeParameters[i]}}>({{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Entity"/> and {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate bool FindEntityPointerCallback<{{Generator.TypeParameters[i]}}>(Entity entity, {{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes an <see cref="Iter"/>, an <see cref="int"/>, and {{i + 1}} T* arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate bool FindIterPointerCallback<{{Generator.TypeParameters[i]}}>(Iter it, int i, {{Generator.PointerParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} ref readonly T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void ReadRefCallback<{{Generator.TypeParameters[i]}}>({{Generator.RefReadOnlyParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void WriteRefCallback<{{Generator.TypeParameters[i]}}>({{Generator.RefParameters[i]}});
                
                /// <summary>
                ///     Function signature that takes {{i + 1}} ref T arguments.
                /// </summary>
                /// {{Generator.XmlTypeParameters[i]}}
                public delegate void InsertRefCallback<{{Generator.TypeParameters[i]}}>({{Generator.RefParameters[i]}});
            }
            """;
    }
}
