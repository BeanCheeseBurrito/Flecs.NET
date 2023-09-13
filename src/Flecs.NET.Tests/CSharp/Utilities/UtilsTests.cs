using System.Runtime.InteropServices;
using Flecs.NET.Utilities;
using Xunit;
using Assert = Xunit.Assert;

namespace Flecs.NET.Tests.CSharp.Utilities
{
    public unsafe class UtilsTests
    {
        [Fact]
        private void StringEqual()
        {
            byte* s1 = (byte*)Marshal.StringToHGlobalAnsi("Hello");
            byte* s2 = (byte*)Marshal.StringToHGlobalAnsi("Hello");
            byte* s3 = (byte*)Marshal.StringToHGlobalAnsi("Hello World");
            byte* s4 = (byte*)Marshal.StringToHGlobalAnsi("hello world");

            Assert.True(Utils.StringEqual(s1, s2));
            Assert.False(Utils.StringEqual(s2, s3));
            Assert.False(Utils.StringEqual(s3, s4));
        }
    }
}
