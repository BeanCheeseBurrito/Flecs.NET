using System;
using System.Runtime.InteropServices;
using Flecs.NET.Core;
using Xunit;

namespace Flecs.NET.Tests.CSharp.Core
{

public unsafe class OpaqueTests
{
    [Fact]
    private void OpaqueStringInStructMember()
    {
        using World world = World.Create();

        world.Component<FixedString64>().Opaque(o => o
            .String()
            .Serialize(static (ref readonly Ecs.Serializer ser, in FixedString64 value) => ser.Value(value.ToString()))
            .AssignString(static (ref FixedString64 value, string str) => value = new FixedString64(str)));

        Entity entity = world.Entity("e1").Set(new Player { Name = new FixedString64("hello opaque"), Score = 42 });

        string json = entity.ToJson();
        Assert.Contains("hello opaque", json);
        Assert.Contains("42", json);

        world.Script("opaque_string").Code("e1 { Player: {Name: \"script wrote\", Score: 7} }").Run();

        Player assigned = entity.Get<Player>();
        Assert.Equal("script wrote", assigned.Name.ToString());
        Assert.Equal(7, assigned.Score);
    }

    [Fact]
    private void FixedBufferStructRegistersWithoutMembers()
    {
        using World world = World.Create();

        Entity entity = world.Entity("e2").Set(new OnlyFixed());

        Assert.Contains("OnlyFixed", entity.ToJson());
    }

    [Fact]
    private void StringComponentSerializes()
    {
        using World world = World.Create();

        Entity entity = world.Entity("e4");
        entity.Set("hello world");

        Assert.Contains("hello world", entity.ToJson());

        world.Script("opaque_component").Code("using Flecs.NET.Core.Ecs.Meta\ne4 { String: \"script wrote\" }").Run();

        Assert.Equal("script wrote", entity.Get<string>());
    }

    [Fact]
    private void OpaqueVectorSerializesAndAssigns()
    {
        using World world = World.Create();

        world.Component<NativeInts>().Opaque<int>(o => o
            .Vector()
            .Serialize(static (ref readonly Ecs.Serializer ser, in NativeInts value) =>
            {
                for (int i = 0; i < value.Length; i++)
                    ser.Value(value[i]);
                return 0;
            })
            .Count(static (in NativeInts value) => value.Length)
            .Resize(static (ref NativeInts value, nuint size) => value.Resize((int)size))
            .EnsureElement(GetElement));

        NativeInts ints = CreateInts(1, 2, 3);
        Entity entity = world.Entity("e5").Set(ints);

        Assert.Contains("[1,2,3]", entity.ToJson().Replace(" ", ""));

        world.Script("opaque_vector").Code("e5 { NativeInts: [4, 5, 6] }").Run();

        NativeInts assigned = entity.Get<NativeInts>();
        Assert.Equal(3, assigned.Length);
        Assert.Equal(4, assigned[0]);
        Assert.Equal(5, assigned[1]);
        Assert.Equal(6, assigned[2]);
        assigned.Dispose();
    }


    [Fact]
    private void OpaqueCallbacksAreIsolatedPerWorld()
    {
        using World first = World.Create();
        first.Component<OpaqueValue>().Opaque(o => o
            .String()
            .Serialize(static (ref readonly Ecs.Serializer ser, in OpaqueValue value) => ser.Value("first")));
        Entity entity = first.Entity().Set(new OpaqueValue());

        using World second = World.Create();
        second.Component<OpaqueValue>().Opaque(o => o
            .String()
            .Serialize(static (ref readonly Ecs.Serializer ser, in OpaqueValue value) => ser.Value("second")));

        Assert.Contains("first", entity.ToJson());
    }

    [Fact]
    private void OpaquePrimitiveAssigns()
    {
        using World world = World.Create();
        world.Component<OpaqueValue>().Opaque(o => o
            .AsType(Ecs.I64)
            .Serialize(static (ref readonly Ecs.Serializer ser, in OpaqueValue value) => ser.Value(value.Value))
            .AssignInt(static (ref OpaqueValue value, long assigned) => value.Value = assigned));

        Entity entity = world.Entity("opaque_primitive").Set(new OpaqueValue { Value = 10 });
        world.Script("opaque_primitive_script").Code("opaque_primitive { OpaqueValue: 42 }").Run();

        Assert.Equal(42, entity.Get<OpaqueValue>().Value);
    }



    [Fact]
    private void OpaqueSerializeExceptionIsCaught()
    {
        using World world = World.Create();

        world.Component<FixedString64>().Opaque(o => o
            .String()
            .Serialize(static (ref readonly Ecs.Serializer ser, in FixedString64 value) => throw new InvalidOperationException("boom")));

        Entity entity = world.Entity("e6").Set(new FixedString64("boom value"));

        Assert.DoesNotContain("boom value", entity.ToJson());
    }

    private static NativeInts CreateInts(params int[] values)
    {
        NativeInts ints = new NativeInts();
        ints.Resize(values.Length);
        for (int i = 0; i < values.Length; i++)
            ints[i] = values[i];
        return ints;
    }

    private static ref int GetElement(ref NativeInts value, nuint index)
    {
        if ((int)index >= value.Length)
            value.Resize((int)index + 1);
        return ref value[(int)index];
    }
}
}

internal struct OpaqueValue
{
    public long Value;
}


internal unsafe struct FixedString64
{
    private fixed char Buffer[64];

    public FixedString64(string value)
    {
        int len = value.Length > 63 ? 63 : value.Length;
        fixed (char* b = Buffer)
        {
            for (int i = 0; i < len; i++)
                b[i] = value[i];
            b[len] = '\0';
        }
    }

    public override readonly string ToString()
    {
        fixed (char* b = Buffer)
            return new string(b);
    }
}

internal struct Player : IFlecsStruct
{
    public FixedString64 Name;
    public int Score;
}

internal unsafe struct OnlyFixed : IFlecsStruct
{
    public fixed char Buffer[8];
}

internal unsafe struct NativeInts : IDisposable
{
    private int _length;
    private int* _buffer;

    public readonly int Length => _length;

    public void Resize(int size)
    {
        if (size == _length)
            return;
        int oldLength = _length;
        int* newBuffer = size == 0 ? null : (int*)NativeMemory.AllocZeroed((nuint)(size * sizeof(int)));
        if (_buffer != null)
        {
            int copyLength = Math.Min(oldLength, size);
            if (copyLength > 0)
                Buffer.MemoryCopy(_buffer, newBuffer, size * sizeof(int), copyLength * sizeof(int));
            NativeMemory.Free(_buffer);
        }
        _buffer = newBuffer;
        _length = size;
    }

    public ref int this[int index] => ref _buffer[index];

    public void Dispose()
    {
        if (_buffer != null)
        {
            NativeMemory.Free(_buffer);
            _buffer = null;
            _length = 0;
        }
    }
}
