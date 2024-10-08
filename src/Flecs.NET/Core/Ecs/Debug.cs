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
}