using System;
using System.Runtime.InteropServices;
using Flecs.NET.Bindings;
using Flecs.NET.Core;

namespace Flecs.NET.Utilities
{
    internal readonly unsafe struct NativeString : IEquatable<NativeString>, IDisposable
    {
        public IntPtr Data { get; }
        public bool IsFlecs { get; }

        public NativeString(byte* str, bool isFlecs)
        {
            Data = (IntPtr)str;
            IsFlecs = isFlecs;
        }

        public NativeString(sbyte* str, bool isFlecs)
        {
            Data = (IntPtr)str;
            IsFlecs = isFlecs;
        }

        public NativeString(IntPtr str, bool isFlecs)
        {
            Data = str;
            IsFlecs = isFlecs;
        }

        public static string GetString(byte* data)
        {
            return Marshal.PtrToStringAnsi((IntPtr)data) ?? string.Empty;
        }

        public static string GetString(sbyte* data)
        {
            Ecs.Assert(data != null, "Pointer to string is null");
            return Marshal.PtrToStringAnsi((IntPtr)data) ?? string.Empty;
        }

        public static string GetStringAndFree(byte* data)
        {
            using NativeString temp = (NativeString)data;
            return (string)temp;
        }

        public static string GetStringAndFree(sbyte* data)
        {
            using NativeString temp = (NativeString)data;
            return (string)temp;
        }

        public static explicit operator NativeString(byte* str)
        {
            return From(str);
        }

        public static explicit operator NativeString(sbyte* str)
        {
            return From(str);
        }

        public static implicit operator byte*(NativeString nativeString)
        {
            return (byte*)To(nativeString);
        }

        public static implicit operator sbyte*(NativeString nativeString)
        {
            return (sbyte*)To(nativeString);
        }

        public static explicit operator NativeString(string? str)
        {
            return FromString(str);
        }

        public static explicit operator string(NativeString nativeString)
        {
            return ToString(nativeString);
        }

        public static NativeString From(byte* str)
        {
            return new NativeString(str, true);
        }

        public static NativeString From(sbyte* str)
        {
            return new NativeString(str, true);
        }

        public static void* To(NativeString nativeString)
        {
            return (void*)nativeString.Data;
        }

        public static NativeString FromString(string? str)
        {
            return new NativeString(Marshal.StringToHGlobalAnsi(str), false);
        }

        public static string ToString(NativeString nativeString)
        {
            return Marshal.PtrToStringAnsi(nativeString.Data) ?? string.Empty;
        }

        public bool Equals(NativeString other)
        {
            return Data == other.Data;
        }

        public override bool Equals(object? obj)
        {
            return obj is NativeString nativeString && Equals(nativeString);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public static bool operator ==(NativeString left, NativeString right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NativeString left, NativeString right)
        {
            return !(left == right);
        }

        public void Dispose()
        {
            if (IsFlecs)
            {
#if NET5_0_OR_GREATER
                ((delegate* unmanaged[Cdecl]<IntPtr, void>)Native.ecs_os_api.free_)(Data);
#else
                Marshal.GetDelegateForFunctionPointer<Ecs.Free>(Native.ecs_os_api.free_)(Data);
#endif
                return;
            }

            Marshal.FreeHGlobal(Data);
        }
    }
}
