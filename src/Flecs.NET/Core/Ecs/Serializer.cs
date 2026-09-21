using System;
using System.Buffers;
using System.Text;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    /// <summary>
    ///     Serializer facade passed to opaque type serialize callbacks. Wraps the
    ///     <see cref="ecs_serializer_t"/> used by flecs' reflection framework. Values are
    ///     serialized as primitives, entities, strings, or as any type registered with
    ///     the world.
    /// </summary>
    public readonly ref struct Serializer
    {
        private const int StackBufferSize = 256;

        private readonly ecs_serializer_t* _serializer;

        internal Serializer(ecs_serializer_t* serializer)
        {
            _serializer = serializer;
        }

        /// <summary>
        ///     Serializes a bool value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(bool value)
        {
            return _serializer->value(_serializer, Ecs.Bool, &value);
        }

        /// <summary>
        ///     Serializes a char value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(char value)
        {
            byte @char = (byte)value;
            return _serializer->value(_serializer, Ecs.Char, &@char);
        }

        /// <summary>
        ///     Serializes a long value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(long value)
        {
            return _serializer->value(_serializer, Ecs.I64, &value);
        }

        /// <summary>
        ///     Serializes an unsigned integer value.
        /// </summary>
        public int Value(ulong value)
        {
            return _serializer->value(_serializer, Ecs.U64, &value);
        }

        /// <summary>
        ///     Serializes an entity value.
        /// </summary>
        public int Entity(ulong value)
        {
            return _serializer->value(_serializer, Ecs.Entity, &value);
        }

        /// <summary>
        ///     Serializes a float value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(float value)
        {
            return _serializer->value(_serializer, Ecs.F32, &value);
        }

        /// <summary>
        ///     Serializes a double value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(double value)
        {
            return _serializer->value(_serializer, Ecs.F64, &value);
        }

        /// <summary>
        ///     Serializes an int value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(int value)
        {
            return _serializer->value(_serializer, Ecs.I32, &value);
        }

        /// <summary>
        ///     Serializes a uint value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(uint value)
        {
            return _serializer->value(_serializer, Ecs.U32, &value);
        }

        /// <summary>
        ///     Serializes a string value. The string is transcoded to a null-terminated
        ///     UTF-8 string.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        public int Value(string? value)
        {
            if (value is null)
            {
                byte* nullString = null;
                return _serializer->value(_serializer, Ecs.String, &nullString);
            }

            int byteCount = Encoding.UTF8.GetByteCount(value);

            if (byteCount <= StackBufferSize)
            {
                byte* buffer = stackalloc byte[byteCount + 1];
                WriteUtf8(buffer, value, byteCount);
                return _serializer->value(_serializer, Ecs.String, &buffer);
            }

            byte[] rented = ArrayPool<byte>.Shared.Rent(byteCount + 1);
            try
            {
                fixed (byte* buffer = rented)
                {
                    WriteUtf8(buffer, value, byteCount);
                    return _serializer->value(_serializer, Ecs.String, &buffer);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }

        /// <summary>
        ///     Serializes a value of a type registered with the world.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <typeparam name="T">The type of the value to serialize.</typeparam>
        public int Value<T>(in T value) where T : unmanaged
        {
            fixed (T* ptr = &value)
            {
                return _serializer->value(_serializer, Type<T>.Id(_serializer->world), ptr);
            }
        }

        /// <summary>
        ///     Serializes a struct member by name. Only valid while serializing a type that
        ///     is mapped to a struct.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        public int Member(string name)
        {
            int byteCount = Encoding.UTF8.GetByteCount(name);

            if (byteCount <= StackBufferSize)
            {
                byte* buffer = stackalloc byte[byteCount + 1];
                WriteUtf8(buffer, name, byteCount);
                return _serializer->member(_serializer, buffer);
            }

            byte[] rented = ArrayPool<byte>.Shared.Rent(byteCount + 1);
            try
            {
                fixed (byte* buffer = rented)
                {
                    WriteUtf8(buffer, name, byteCount);
                    return _serializer->member(_serializer, buffer);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }


        /// <summary>
        ///     Writes the null-terminated UTF-8 encoding of the provided string to the buffer.
        /// </summary>
        private static void WriteUtf8(byte* buffer, string value, int byteCount)
        {
            int written = Encoding.UTF8.GetBytes(value, new Span<byte>(buffer, byteCount + 1));
            buffer[written] = 0;
        }
    }
}
