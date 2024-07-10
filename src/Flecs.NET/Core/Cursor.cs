using System;
using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core
{
    /// <summary>
    ///     Class for reading/writing dynamic values.
    /// </summary>
    public unsafe struct Cursor : IEquatable<Cursor>
    {
        private ecs_meta_cursor_t _cursor;

        /// <summary>
        /// </summary>
        /// <param name="world"></param>
        /// <param name="typeId"></param>
        /// <param name="data"></param>
        public Cursor(ecs_world_t* world, ulong typeId, void* data)
        {
            _cursor = ecs_meta_cursor(world, typeId, data);
        }

        /// <summary>
        ///     Push value scope.
        /// </summary>
        /// <returns></returns>
        public int Push()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_push(cursor);
            }
        }

        /// <summary>
        ///     Pop value Scope.
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_pop(cursor);
            }
        }

        /// <summary>
        ///     Move to the next member/element.
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_next(cursor);
            }
        }

        /// <summary>
        ///     Move to member by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Member(string name)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                using NativeString nativeName = (NativeString)name;
                return ecs_meta_member(cursor, nativeName);
            }
        }

        /// <summary>
        ///     Move to element by index.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public int Elem(int elem)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_elem(cursor, elem);
            }
        }

        /// <summary>
        ///     Test if current scope is a collection type.
        /// </summary>
        /// <returns></returns>
        public bool IsCollection()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_is_collection(cursor) == 1;
            }
        }

        /// <summary>
        ///     Get member name.
        /// </summary>
        /// <returns></returns>
        public string GetMember()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return NativeString.GetString(ecs_meta_get_member(cursor));
            }
        }

        /// <summary>
        ///     Get type of value.
        /// </summary>
        /// <returns></returns>
        public new Entity GetType()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return new Entity(_cursor.world, ecs_meta_get_type(cursor));
            }
        }

        /// <summary>
        ///     Get unit of value.
        /// </summary>
        /// <returns></returns>
        public Entity GetUnit()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return new Entity(_cursor.world, ecs_meta_get_unit(cursor));
            }
        }

        /// <summary>
        ///     Get untyped pointer to value.
        /// </summary>
        /// <returns></returns>
        public void* GetPtr()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_get_ptr(cursor);
            }
        }

        /// <summary>
        ///     Set boolean value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetBool(bool value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_bool(cursor, Utils.Bool(value));
            }
        }

        /// <summary>
        ///     Set char value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetChar(char value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_char(cursor, (byte)value);
            }
        }

        /// <summary>
        ///     Set signed int value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetInt(long value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_int(cursor, value);
            }
        }

        /// <summary>
        ///     Set unsigned int value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetUInt(ulong value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_uint(cursor, value);
            }
        }

        /// <summary>
        ///     Set float value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetFloat(double value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_float(cursor, value);
            }
        }

        /// <summary>
        ///     Set string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetString(string value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                using NativeString nativeString = (NativeString)value;
                return ecs_meta_set_string(cursor, nativeString);
            }
        }

        /// <summary>
        ///     Set string literal value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetLiteralString(string value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                using NativeString nativeString = (NativeString)value;
                return ecs_meta_set_string_literal(cursor, nativeString);
            }
        }

        /// <summary>
        ///     Set entity value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetEntity(ulong value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_entity(cursor, value);
            }
        }

        /// <summary>
        ///     Set (component) id value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetId(ulong value)
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_id(cursor, value);
            }
        }

        /// <summary>
        ///     Set null value.
        /// </summary>
        /// <returns></returns>
        public int SetNull()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_null(cursor);
            }
        }

        /// <summary>
        ///     Get boolean value.
        /// </summary>
        /// <returns></returns>
        public bool GetBool()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_set_null(cursor) == 1;
            }
        }

        /// <summary>
        ///     Get char value.
        /// </summary>
        /// <returns></returns>
        public char GetChar()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return (char)ecs_meta_get_char(cursor);
            }
        }

        /// <summary>
        ///     Get signed int value.
        /// </summary>
        /// <returns></returns>
        public long GetInt()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_get_int(cursor);
            }
        }

        /// <summary>
        ///     Get unsigned int value.
        /// </summary>
        /// <returns></returns>
        public ulong GetUInt()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_get_uint(cursor);
            }
        }

        /// <summary>
        ///     Get float value.
        /// </summary>
        public double GetFloat()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return ecs_meta_get_float(cursor);
            }
        }

        /// <summary>
        ///     Get string value.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return NativeString.GetString(ecs_meta_get_string(cursor));
            }
        }

        /// <summary>
        ///     Get entity value.
        /// </summary>
        /// <returns></returns>
        public Entity GetEntity()
        {
            fixed (ecs_meta_cursor_t* cursor = &_cursor)
            {
                return new Entity(_cursor.world, ecs_meta_get_entity(cursor));
            }
        }

        /// <summary>
        ///     Checks if two <see cref="Cursor"/> instances are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Cursor other)
        {
            return Equals(_cursor, other._cursor);
        }

        /// <summary>
        ///     Checks if two <see cref="Cursor"/> instances are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Cursor other && Equals(other);
        }

        /// <summary>
        ///     Gets the hash code of the <see cref="Cursor"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _cursor.GetHashCode();
        }

        /// <summary>
        ///     Checks if two <see cref="Cursor"/> instances are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Cursor left, Cursor right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks if two <see cref="Cursor"/> instances are not equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Cursor left, Cursor right)
        {
            return !(left == right);
        }
    }
}
