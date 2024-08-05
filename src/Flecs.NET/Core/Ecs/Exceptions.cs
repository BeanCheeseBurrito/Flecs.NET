using System;

namespace Flecs.NET.Core;

public static partial class Ecs
{
    /// <summary>
    ///     Flecs.NET assertion exception.
    /// </summary>
    [Serializable]
    public class AssertionException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        public AssertionException()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public AssertionException(string message) : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AssertionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    ///     Flecs.NET error exception.
    /// </summary>
    [Serializable]
    public class ErrorException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        public ErrorException()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public ErrorException(string message) : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    ///     Flecs native exception.
    /// </summary>
    [Serializable]
    public class NativeException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        public NativeException()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public NativeException(string message) : base(message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NativeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}