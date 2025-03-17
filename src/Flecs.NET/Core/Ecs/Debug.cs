using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Flecs.NET.Core;

public static partial class Ecs
{
    /// <summary>
    ///     Debug assert.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="message"></param>
    /// <param name="line"></param>
    /// <param name="member"></param>
    /// <param name="file"></param>
    /// <param name="conditionStr"></param>
    [Conditional("DEBUG")]
    public static void Assert(
        bool condition,
        string message = "",
        [CallerLineNumber] int line = default,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerArgumentExpression("condition")] string conditionStr = "")
    {
        if (condition)
            return;

        throw new AssertionException($"\n[Flecs.NET Assertion Failed]: Line {line}, In Method '{member}', {file}\n[Failed Condition]: {conditionStr}\n[Assertion Message]: {message}");
    }

    /// <summary>
    ///     Debug assert.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="line"></param>
    /// <param name="member"></param>
    /// <param name="file"></param>
    [Conditional("DEBUG")]
    public static void Error(
        string message = "",
        [CallerLineNumber] int line = default,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "")
    {
        throw new ErrorException($"\n[Flecs.NET Error]: Line {line}, In Method '{member}', {file}\n[Error Message]: {message}");
    }

    /// <summary>
    ///     Wraps the provided exceptions inside an aggregate exception that can be thrown.
    /// </summary>
    /// <param name="exceptions">Enumerable collection of exceptions.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="line">The caller line.</param>
    /// <param name="member">The caller member name.</param>
    /// <param name="file">The caller file path.</param>
    /// <returns></returns>
    public static AggregateException Exception(
        IEnumerable<Exception> exceptions,
        string message = "",
        [CallerLineNumber] int line = default,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "")
    {
        return new AggregateException($"\n[Flecs.NET Exception]: Line {line}, In Method '{member}', {file}\n[Message]: {message}\n", exceptions).Flatten();
    }

    /// <summary>
    ///     Wraps the provided exception inside an aggregate exception that can be thrown.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="line">The caller line.</param>
    /// <param name="member">The caller member name.</param>
    /// <param name="file">The caller file path.</param>
    /// <returns></returns>
    public static AggregateException Exception(
        Exception exception,
        string message = "",
        [CallerLineNumber] int line = default,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "")
    {
        return Exception([exception], message, line, member, file);
    }
}
