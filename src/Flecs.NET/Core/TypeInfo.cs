using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct TypeInfo
    {
        public ecs_type_info_t* Handle { get; }

        public TypeInfo(ecs_type_info_t* typeInfo)
        {
            Handle = typeInfo;
        }
    }
}