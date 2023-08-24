using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public unsafe struct TypeInfo
    {
        public ecs_type_info_t* _handle;

        public ref ecs_type_info_t* Handle => ref _handle;

        public TypeInfo(ecs_type_info_t* typeInfo)
        {
            _handle = typeInfo;
        }
    }
}
