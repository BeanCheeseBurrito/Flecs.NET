using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Flecs.NET.Codegen
{
    [Generator]
    public class Generator : IIncrementalGenerator
    {
        private const int GenericCount = 16;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput((IncrementalGeneratorPostInitializationContext postContext) =>
            {
                postContext.AddSource("Flecs.NET.g.cs", CodeFormatter.Format(Generate()));
            });
        }

        private static string Generate()
        {
            return $@"
                #pragma warning disable 1591
                #nullable enable              

                using System;
                using System.Runtime.CompilerServices;
                using System.Runtime.InteropServices;
                using Flecs.NET.Utilities; 
                using static Flecs.NET.Bindings.Native;

                namespace Flecs.NET.Core 
                {{
                    {GenerateWorldExtensions()}
                    {GenerateEntityExtensions()}
                    {GenerateEcsExtensions()}
                    {GenerateInvokerExtensions()}
                    {GenerateBindingContextExtensions()}
                    {GenerateIterableExtensions()}
                    {GenerateObserverExtensions()}
                    {GenerateRoutineExtensions()}
                }}
                
                #pragma warning restore 1591
            ";
        }

        private static string GenerateWorldExtensions()
        {
            return $@"
                public unsafe partial struct World
                {{
                    {GenerateBuilderFactoryExtensions()}
                    {GenerateWorldEachCallbackFunctions()}
                }}
            ";
        }

        private static string GenerateEntityExtensions()
        {
            return $@"
                public unsafe partial struct Entity
                {{
                    {GenerateEntityReadCallbacks()}
                    {GenerateEntityWriteCallbacks()} 
                    {GenerateEntityEnsureCallbacks()}
                }}
            ";
        }

        private static string GenerateEcsExtensions()
        {
            return $@"
                public static unsafe partial class Ecs 
                {{
                    {GenerateDelegates()}
                }}
            ";
        }

        private static string GenerateInvokerExtensions()
        {
            return $@"
                public static unsafe partial class Invoker 
                {{
                    {GenerateIterInvokers()}
                    {GenerateEachInvokers()}
                    {GenerateGetPointers()}
                    {GenerateEnsurePointers()}
                    {GenerateReadInvokers()}
                    {GenerateWriteInvokers()}
                    {GenerateEnsureInvokers()}
                }}
            ";
        }

        private static string GenerateIterableExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeConstraints = ConcatString(i + 1, " ", index => $"where T{index} : unmanaged");
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.Append($@"
                    {GenerateIterableCallbackFunctions(false, $"Iter<{typeParams}>", $"Ecs.IterFieldCallback<{typeParams}>", $"delegate*<Iter, {fieldParams}, void>", "GetNext")}
                    {GenerateIterableCallbackFunctions(false, $"Iter<{typeParams}>", $"Ecs.IterSpanCallback<{typeParams}>", $"delegate*<Iter, {spanParams}, void>", "GetNext", typeConstraints)}
                    {GenerateIterableCallbackFunctions(false, $"Iter<{typeParams}>", $"Ecs.IterPointerCallback<{typeParams}>", $"delegate*<Iter, {pointerParams}, void>", "GetNext", typeConstraints)}
                    {GenerateIterableCallbackFunctions(true, $"Each<{typeParams}>", $"Ecs.EachCallback<{typeParams}>", $"delegate*<{refParams}, void>", "GetNextInstanced")} 
                    {GenerateIterableCallbackFunctions(true, $"Each<{typeParams}>", $"Ecs.EachEntityCallback<{typeParams}>", $"delegate*<Entity, {refParams}, void>", "GetNextInstanced")} 
                    {GenerateIterableCallbackFunctions(true, $"Each<{typeParams}>", $"Ecs.EachIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refParams}, void>", "GetNextInstanced")}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindCallback<{typeParams}>", $"delegate*<{refParams}, bool>")}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindEntityCallback<{typeParams}>", $"delegate*<Entity, {refParams}, bool>")}
                    {GenerateIterableFindCallbackFunctions(typeParams, $"Ecs.FindIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refParams}, bool>")}
                ");
            }

            return $@"
                public unsafe partial struct Query
                {{
                    {str}
                }}

                public unsafe partial struct IterIterable
                {{
                    {str}
                }}

                public unsafe partial struct PageIterable
                {{
                    {str}
                }}

                public unsafe partial struct WorkerIterable
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateObserverExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeConstraints = ConcatString(i + 1, " ", index => $"where T{index} : unmanaged");
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public Observer Iter<{typeParams}>(Ecs.IterFieldCallback<{typeParams}> callback) 
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Observer Iter<{typeParams}>(Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Observer Iter<{typeParams}>(Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(Ecs.EachIndexCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                #if NET5_0_OR_GREATER
                    public Observer Iter<{typeParams}>(delegate*<Iter, {fieldParams}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Observer Iter<{typeParams}>(delegate*<Iter, {spanParams}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Observer Iter<{typeParams}>(delegate*<Iter, {pointerParams}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(delegate*<{refParams}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(delegate*<Entity, {refParams}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Each<{typeParams}>(delegate*<Iter, int, {refParams}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(delegate*<Iter, void> run, Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(delegate*<Iter, void> run, Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(delegate*<Iter, void> run, Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(delegate*<Iter, void> run, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(delegate*<Iter, void> run, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(delegate*<Iter, void> run, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, {fieldParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, {spanParams}, void> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, {pointerParams}, void> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, delegate*<{refParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, delegate*<Entity, {refParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Observer Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, int, {refParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}
                #endif
                ");
            }

            return $@"
                public unsafe partial struct ObserverBuilder
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateRoutineExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string typeConstraints = ConcatString(i + 1, " ", index => $"where T{index} : unmanaged");
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public Routine Iter<{typeParams}>(Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Routine Iter<{typeParams}>(Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Routine Iter<{typeParams}>(Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return Build(ref callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(Ecs.EachIndexCallback<{typeParams}> callback) 
                    {{
                        return Instanced().Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        return PopulateRun(run).Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                #if NET5_0_OR_GREATER
                    public Routine Iter<{typeParams}>(delegate*<Iter, {fieldParams}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Routine Iter<{typeParams}>(delegate*<Iter, {spanParams}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Routine Iter<{typeParams}>(delegate*<Iter, {pointerParams}, void> callback) 
                    {{
                        return Build((IntPtr)callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(delegate*<{refParams}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(delegate*<Entity, {refParams}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Each<{typeParams}>(delegate*<Iter, int, {refParams}, void> callback) 
                    {{
                        return Instanced().Build((IntPtr)callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(delegate*<Iter, void> run, Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(delegate*<Iter, void> run, Ecs.IterSpanCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(delegate*<Iter, void> run, Ecs.IterPointerCallback<{typeParams}> callback) {typeConstraints}
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(delegate*<Iter, void> run, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(delegate*<Iter, void> run, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(delegate*<Iter, void> run, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        return PopulateRun((IntPtr)run).Build(ref callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, {fieldParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.IterFieldCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, {spanParams}, void> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.IterSpanCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, {pointerParams}, void> callback) {typeConstraints}
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.IterPointerCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, delegate*<{refParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.EachCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, delegate*<Entity, {refParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.EachEntityCallbackPointer);
                    }}

                    public Routine Run<{typeParams}>(Ecs.IterCallback run, delegate*<Iter, int, {refParams}, void> callback)
                    {{
                        return PopulateRun(run).Build((IntPtr)callback, BindingContext<{typeParams}>.EachIndexCallbackPointer);
                    }}
                #endif
                ");
            }

            return $@"
                public unsafe partial struct RoutineBuilder
                {{
                    {str}
                }}
            ";
        }

        private static string GenerateBindingContextExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    internal static unsafe partial class BindingContext<{typeParams}>
                    {{
                        #if NET5_0_OR_GREATER
                        {GenerateBindingContextPointers("IterFieldCallback")}
                        {GenerateBindingContextPointers("IterSpanCallback")}
                        {GenerateBindingContextPointers("IterPointerCallback")}
                        {GenerateBindingContextPointers("EachCallback")}
                        {GenerateBindingContextPointers("EachEntityCallback")}
                        {GenerateBindingContextPointers("EachIndexCallback")}
                        #else
                        {GenerateBindingContextDelegates("IterFieldCallback")}
                        {GenerateBindingContextDelegates("IterSpanCallback")}
                        {GenerateBindingContextDelegates("IterPointerCallback")}
                        {GenerateBindingContextDelegates("EachCallback")}
                        {GenerateBindingContextDelegates("EachEntityCallback")}
                        {GenerateBindingContextDelegates("EachIndexCallback")}
                        #endif
                        {GenerateBindingContextCallbacks("IterFieldCallback", $"Ecs.IterFieldCallback<{typeParams}>", $"delegate*<Iter, {fieldParams}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("IterSpanCallback", $"Ecs.IterSpanCallback<{typeParams}>", $"delegate*<Iter, {spanParams}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("IterPointerCallback", $"Ecs.IterPointerCallback<{typeParams}>", $"delegate*<Iter, {pointerParams}, void>", "Iter")}
                        {GenerateBindingContextCallbacks("EachCallback", $"Ecs.EachCallback<{typeParams}>", $"delegate*<{refParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachEntityCallback", $"Ecs.EachEntityCallback<{typeParams}>", $"delegate*<Entity, {refParams}, void>", "Each")}
                        {GenerateBindingContextCallbacks("EachIndexCallback", $"Ecs.EachIndexCallback<{typeParams}>", $"delegate*<Iter, int, {refParams}, void>", "Each")} 
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateBuilderFactoryExtensions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string termBuilders = ConcatString(i + 1, "\n", index => $".With<T{index}>()");
                str.AppendLine($@"
                    public AlertBuilder AlertBuilder<{typeParams}>()
                    {{
                        return new AlertBuilder(Handle){termBuilders};
                    }}

                    public AlertBuilder AlertBuilder<{typeParams}>(string name)
                    {{
                        return new AlertBuilder(Handle, name){termBuilders};
                    }}

                    public AlertBuilder AlertBuilder<{typeParams}>(ulong entity)
                    {{
                        return new AlertBuilder(Handle, entity){termBuilders};
                    }}

                    public Alert Alert<{typeParams}>()
                    {{
                        return AlertBuilder<{typeParams}>().Build();
                    }}

                    public Alert Alert<{typeParams}>(string name)
                    {{
                        return AlertBuilder<{typeParams}>(name).Build();
                    }}

                    public Alert Alert<{typeParams}>(ulong entity)
                    {{
                        return AlertBuilder<{typeParams}>(entity).Build();
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>()
                    {{
                        return new QueryBuilder(Handle){termBuilders};
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>(string name)
                    {{
                        return new QueryBuilder(Handle, name){termBuilders};
                    }}

                    public QueryBuilder QueryBuilder<{typeParams}>(ulong entity)
                    {{
                        return new QueryBuilder(Handle, entity){termBuilders};
                    }}

                    public Query Query<{typeParams}>()
                    {{
                        return QueryBuilder<{typeParams}>().Build();
                    }}

                    public Query Query<{typeParams}>(string name)
                    {{
                        return QueryBuilder<{typeParams}>(name).Build();
                    }}

                    public Query Query<{typeParams}>(ulong entity)
                    {{
                        return QueryBuilder<{typeParams}>(entity).Build();
                    }}

                    public RoutineBuilder Routine<{typeParams}>()
                    {{
                        return new RoutineBuilder(Handle){termBuilders};
                    }}

                    public RoutineBuilder Routine<{typeParams}>(string name)
                    {{
                        return new RoutineBuilder(Handle, name){termBuilders};
                    }}

                    public ObserverBuilder Observer<{typeParams}>()
                    {{
                        return new ObserverBuilder(Handle){termBuilders};
                    }}

                    public ObserverBuilder Observer<{typeParams}>(string name)
                    {{
                        return new ObserverBuilder(Handle, name){termBuilders};
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWorldEachCallbackFunctions()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string refArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                str.AppendLine($@"
                    public void Each<{typeParams}>(Ecs.EachCallback<{typeParams}> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);   
                    }}

                    public void Each<{typeParams}>(Ecs.EachEntityCallback<{typeParams}> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);
                    }}

                #if NET5_0_OR_GREATER
                    public void Each<{typeParams}>(delegate*<{refArgs}, void> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);   
                    }}

                    public void Each<{typeParams}>(delegate*<Entity, {refArgs}, void> callback) 
                    {{
                        using Query query = Query<{typeParams}>();
                        query.Each(callback);
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityReadCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public bool Read<{typeParams}>(Ecs.InvokeReadCallback<{typeParams}> callback)
                    {{
                        return Invoker.InvokeRead(World, Id, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityWriteCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public bool Write<{typeParams}>(Ecs.InvokeWriteCallback<{typeParams}> callback)
                    {{
                        return Invoker.InvokeWrite(World, Id, callback);
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEntityEnsureCallbacks()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                str.AppendLine($@"
                    public ref Entity Insert<{typeParams}>(Ecs.InvokeInsertCallback<{typeParams}> callback)
                    {{
                        Invoker.InvokeEnsure(World, Id, callback);
                        return ref this;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateDelegates()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);
                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}> field{index}");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}> span{index}");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}* pointer{index}");
                string refParams = ConcatString(i + 1, ", ", index => $"ref T{index} comp{index}");
                string inParams = ConcatString(i + 1, ", ", index => $"in T{index} comp{index}");

                str.AppendLine($"public delegate void IterFieldCallback<{typeParams}>(Iter it, {fieldParams});");
                str.AppendLine($"public delegate void IterSpanCallback<{typeParams}>(Iter it, {spanParams});");
                str.AppendLine($"public delegate void IterPointerCallback<{typeParams}>(Iter it, {pointerParams});");

                str.AppendLine($"public delegate void EachCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate void EachEntityCallback<{typeParams}>(Entity entity, {refParams});");
                str.AppendLine($"public delegate void EachIndexCallback<{typeParams}>(Iter it, int i, {refParams});");

                str.AppendLine($"public delegate bool FindCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate bool FindEntityCallback<{typeParams}>(Entity entity, {refParams});");
                str.AppendLine($"public delegate bool FindIndexCallback<{typeParams}>(Iter it, int i, {refParams});");

                str.AppendLine($"public delegate void InvokeReadCallback<{typeParams}>({inParams});");
                str.AppendLine($"public delegate void InvokeWriteCallback<{typeParams}>({refParams});");
                str.AppendLine($"public delegate void InvokeInsertCallback<{typeParams}>({refParams});");
            }

            return str.ToString();
        }

        private static string GenerateIterInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string fieldParams = ConcatString(i + 1, ", ", index => $"Field<T{index}>");
                string spanParams = ConcatString(i + 1, ", ", index => $"Span<T{index}>");
                string pointerParams = ConcatString(i + 1, ", ", index => $"T{index}*");

                string fieldArgs = ConcatString(i + 1, ", ", index => $"it.Field<T{index}>({index})");
                string spanArgs = ConcatString(i + 1, ", ", index => $"it.GetSpan<T{index}>({index})");
                string pointerArgs = ConcatString(i + 1, ", ", index => $"it.GetPointer<T{index}>({index})");

                string fieldBody = $@"
                    Macros.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {fieldArgs});
                    Macros.TableUnlock(iter->world, iter->table);
                ";

                string spanBody = $@"
                    Macros.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {spanArgs});
                    Macros.TableUnlock(iter->world, iter->table);
                ";

                string pointerBody = $@"
                    Macros.TableLock(iter->world, iter->table);
                    Iter it = new Iter(iter);
                    callback(it, {pointerArgs});
                    Macros.TableUnlock(iter->world, iter->table);
                ";

                str.AppendLine($@"
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterFieldCallback<{typeParams}> callback)
                    {{
                        {fieldBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterSpanCallback<{typeParams}> callback)
                    {{
                        {spanBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, Ecs.IterPointerCallback<{typeParams}> callback)
                    {{
                        {pointerBody}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {fieldParams}, void> callback)
                    {{
                        {fieldBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {spanParams}, void> callback)
                    {{
                        {spanBody}
                    }}

                    public static void Iter<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, {pointerParams}, void> callback)
                    {{
                        {pointerBody}
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateEachInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string refArgs = ConcatString(i + 1, ", ", index => $"ref T{index}");

                string fieldAssertions = ConcatString(i + 1, "\n",
                    index => $"Core.Iter.AssertFieldId<T{index}>(iter, {index});");

                string sizes = ConcatString(i + 1, "\n",
                    index => $"int size{index} = (iter->sources == null || iter->sources[{index}] == 0) && (iter->set_fields & (1 << {index})) != 0 ? Type<T{index}>.Size : 0;");

                string pointers = ConcatString(i + 1, "\n",
                    index => $"IntPtr pointer{index} = (IntPtr)iter->ptrs[{index}];");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(pointer{index})");

                string increments = ConcatString(i + 1, ", ",
                    index => $"pointer{index} += size{index}");

                string each = $@"
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Macros.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 || iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                        callback({callbackArgs});

                    Macros.TableUnlock(iter->world, iter->table);
                ";

                string eachEntity = $@"
                    Ecs.Assert(iter->count > 0, ""No entities returned, use Iter() or Each() without the entity argument instead."");

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Macros.TableLock(iter->world, iter->table);

                    for (int i = 0; i < iter->count; i++, {increments})
                        callback(new Entity(iter->world, iter->entities[i]), {callbackArgs});

                    Macros.TableUnlock(iter->world, iter->table);
                ";

                string eachIndex = $@"
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    iter->flags |= EcsIterCppEach;

                    Macros.TableLock(iter->world, iter->table);

                    int count = iter->count == 0 || iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                        callback(new Iter(iter), i, {callbackArgs});

                    Macros.TableUnlock(iter->world, iter->table);
                ";

                string find = $@"
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    Macros.TableLock(iter->world, iter->table);

                    Entity result = default;

                    int count = iter->count == 0 || iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                    {{
                        if (!callback({callbackArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Macros.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findEntity = $@"
                    Ecs.Assert(iter->count > 0, ""No entities returned, use Find() without the Entity argument instead."");

                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    Macros.TableLock(iter->world, iter->table);

                    Entity result = default;

                    for (int i = 0; i < iter->count; i++, {increments})
                    {{
                        if (!callback(new Entity(iter->world, iter->entities[i]), {callbackArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Macros.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                string findIndex = $@"
                    {fieldAssertions}
                    {sizes}
                    {pointers}

                    Macros.TableLock(iter->world, iter->table);

                    Entity result = default;

                    int count = iter->count == 0 || iter->table == null ? 1 : iter->count;

                    for (int i = 0; i < count; i++, {increments})
                    {{
                        if (!callback(new Iter(iter), i, {callbackArgs}))
                            continue;

                        result = new Entity(iter->world, iter->entities[i]);
                        break;
                    }}

                    Macros.TableUnlock(iter->world, iter->table);

                    return result;
                ";

                str.AppendLine($@"
                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachCallback<{typeParams}> callback)
                    {{
                        {each}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachEntityCallback<{typeParams}> callback)
                    {{
                        {eachEntity}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, Ecs.EachIndexCallback<{typeParams}> callback)
                    {{
                        {eachIndex}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindCallback<{typeParams}> callback)
                    {{
                        {find}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindEntityCallback<{typeParams}> callback)
                    {{
                        {findEntity}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, Ecs.FindIndexCallback<{typeParams}> callback)
                    {{
                        {findIndex}
                    }}

                #if NET5_0_OR_GREATER
                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<{refArgs}, void> callback)
                    {{
                        {each}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {refArgs}, void> callback)
                    {{
                        {eachEntity}
                    }}

                    public static void Each<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {refArgs}, void> callback)
                    {{
                        {eachIndex}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<{refArgs}, bool> callback)
                    {{
                        {find}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Entity, {refArgs}, bool> callback)
                    {{
                        {findEntity}
                    }}

                    public static Entity Find<{typeParams}>(ecs_iter_t* iter, delegate*<Iter, int, {refArgs}, bool> callback)
                    {{
                        {findIndex}
                    }}
                #endif
                ");
            }

            return str.ToString();
        }

        private static string GenerateGetPointers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string columnIndexes = ConcatString(i + 1, "\n",
                    index => $"int t{index} = ecs_table_get_column_index(realWorld, table, Type<T{index}>.Id(world));");

                string condition = ConcatString(i + 1, " || ",
                    index => $"t{index} == -1");

                string typeIds = ConcatString(i + 1, "\n",
                    index => $"ptrs[{index}] = ecs_record_get_by_column(r, t{index}, default);");

                str.AppendLine($@"
                    internal static bool GetPointers<{typeParams}>(ecs_world_t* world, ecs_record_t* r, ecs_table_t* table, void** ptrs)
                    {{
                        Ecs.Assert(table != null, nameof(ECS_INTERNAL_ERROR));

                        if (ecs_table_column_count(table) == 0)
                            return false;

                        ecs_world_t* realWorld = ecs_get_world(world);

                        {columnIndexes}

                        if ({condition})
                            return false;

                        {typeIds}

                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEnsurePointers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string typeIds = ConcatString(i + 1, "\n",
                    index => $"ptrs[{index}] = ecs_ensure_id(world, e, Type<T{index}>.Id(world));");

                str.AppendLine($@"
                    internal static bool EnsurePointers<{typeParams}>(ecs_world_t* world, ulong e, void** ptrs)
                    {{
                        {typeIds}
                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateReadInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"in Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                str.AppendLine($@"
                    internal static bool InvokeRead<{typeParams}>(ecs_world_t* world, ulong e, Ecs.InvokeReadCallback<{typeParams}> callback)
                    {{
                        ecs_record_t* r = ecs_read_begin(world, e);

                        if (r == null)
                            return false;

                        ecs_table_t *table = r->table;

                        if (table == null)
                            return false;

                        void** ptrs = stackalloc void*[{i + 1}];
                        bool hasComponents = GetPointers<{typeParams}>(world, r, table, ptrs);

                        if (hasComponents)
                            callback({callbackArgs});

                        ecs_read_end(r);

                        return hasComponents;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateWriteInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                str.AppendLine($@"
                    internal static bool InvokeWrite<{typeParams}>(ecs_world_t* world, ulong e, Ecs.InvokeWriteCallback<{typeParams}> callback)
                    {{
                        ecs_record_t* r = ecs_write_begin(world, e);

                        if (r == null)
                            return false;

                        ecs_table_t *table = r->table;

                        if (table == null)
                            return false;

                        void** ptrs = stackalloc void*[{i + 1}];
                        bool hasComponents = GetPointers<{typeParams}>(world, r, table, ptrs);

                        if (hasComponents)
                            callback({callbackArgs});

                        ecs_write_end(r);

                        return hasComponents;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateEnsureInvokers()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < GenericCount; i++)
            {
                string typeParams = GenerateTypeParams(i + 1);

                string addedIds = ConcatString(i + 1, "\n", index => $@"
                    next = ecs_table_add_id(world, prev, Type<T{index}>.Id(world));
                    if (prev != next) added[elem++] = Type<T{index}>.Id(world);
                    prev = next;
                ");

                string callbackArgs = ConcatString(i + 1, ", ",
                    index => $"ref Managed.GetTypeRef<T{index}>(ptrs[{index}])");

                string modified = ConcatString(i + 1, "\n",
                    index => $"ecs_modified_id(world, id, Type<T{index}>.Id(world));");

                str.AppendLine($@"
                    internal static bool InvokeEnsure<{typeParams}>(ecs_world_t* world, ulong id, Ecs.InvokeInsertCallback<{typeParams}> callback)
                    {{
                        World w = new World(world);

                        void** ptrs = stackalloc void*[{i + 1}];
                        ecs_table_t* table = null;

                        if (!w.IsDeferred())
                        {{
                            Ecs.Assert(!w.IsStage(), nameof(ECS_INVALID_PARAMETER));

                            ecs_record_t* r = ecs_record_find(world, id);
                            if (r != null)
                                table = r->table;

                            ecs_table_t* prev = table;
                            ecs_table_t* next;
                            int elem = 0;
                            ulong* added = stackalloc ulong[{i + 1}];

                            {addedIds}

                            if (table != next)
                            {{
                                ecs_type_t ids = default;
                                ids.array = added;
                                ids.count = elem;
                                ecs_commit(world, id, r, next, &ids, null);
                                table = next;
                            }}

                            if (!GetPointers<{typeParams}>(w, r, table, ptrs))
                                Ecs.Error(nameof(ECS_INTERNAL_ERROR));

                            Macros.TableLock(world, table);
                        }}
                        else
                        {{
                            EnsurePointers<{typeParams}>(world, id, ptrs);
                        }}

                        callback({callbackArgs});

                        if (!w.IsDeferred())
                            Macros.TableUnlock(world, table);

                        {modified}

                        return true;
                    }}
                ");
            }

            return str.ToString();
        }

        private static string GenerateBindingContextPointers(string callbackName)
        {
            return $@"
                internal static readonly IntPtr {callbackName}Pointer =
                    (IntPtr)(delegate* <ecs_iter_t*, void>)&{callbackName};
            ";
        }

        private static string GenerateBindingContextDelegates(string functionName)
        {
            return $@"
                internal static readonly IntPtr {functionName}Pointer =
                    Marshal.GetFunctionPointerForDelegate({functionName}Reference = {functionName});
                private static readonly Ecs.IterAction {functionName}Reference;
            ";
        }

        private static string GenerateBindingContextCallbacks(
            string callbackName,
            string delegateName,
            string functionPointerName,
            string invokerName)
        {
            return $@"
                internal static void {callbackName}(ecs_iter_t* iter)
                {{
                    BindingContext.IteratorContext* context = (BindingContext.IteratorContext*)iter->callback_ctx;
            #if NET5_0_OR_GREATER
                    if (context->Callback.GcHandle == default)
                    {{
                        Invoker.{invokerName}(iter, ({functionPointerName})context->Callback.Function);
                        return;
                    }}
            #endif
                    {delegateName} callback = ({delegateName})context->Callback.GcHandle.Target!;
                    Invoker.{invokerName}(iter, callback);
                }}
            ";
        }

        private static string GenerateIterableCallbackFunctions(
            bool instanced,
            string functionName,
            string delegateName,
            string functionPointerName,
            string nextName,
            string typeConstraints = "")
        {
            return $@"
                public void {functionName}({delegateName} callback) {typeConstraints}
                {{
                    ecs_iter_t it = GetIter();
                    {(instanced ? "it.flags |= EcsIterIsInstanced;" : "")}
                    while ({nextName}(&it))
                        Invoker.{functionName}(&it, callback);
                }}

                #if NET5_0_OR_GREATER
                public void {functionName}({functionPointerName} callback) {typeConstraints}
                {{
                    ecs_iter_t it = GetIter();
                    {(instanced ? "it.flags |= EcsIterIsInstanced;" : "")}
                    while ({nextName}(&it))
                        Invoker.{functionName}(&it, callback);
                }}
                #endif
            ";
        }

        private static string GenerateIterableFindCallbackFunctions(string typeParams, string delegateName, string functionPointerName)
        {
            string methodBody = $@"
                ecs_iter_t it = GetIter();
                it.flags |= EcsIterIsInstanced;

                Entity result = default;

                while (result == 0 && GetNextInstanced(&it))
                    result = Invoker.Find(&it, callback);
                
                if (result != 0)
                    ecs_iter_fini(&it);

                return result;
            ";

            return $@"
                public Entity Find<{typeParams}>({delegateName} callback)
                {{
                    {methodBody}
                }}
            #if NET5_0_OR_GREATER
                public Entity Find<{typeParams}>({functionPointerName} callback)
                {{
                    {methodBody}
                }}
            #endif
            ";
        }

        private static string ConcatString(int count, string separator, Func<int, string> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                str.Append(callback(i));
                if (i < count - 1)
                    str.Append(separator);
            }

            return str.ToString();
        }

        private static string GenerateTypeParams(int num)
        {
            return ConcatString(num, ", ", index => $"T{index}");
        }
    }
}
