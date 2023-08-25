using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    /// <summary>
    /// A wrapper around ecs_type_info_t.
    /// </summary>
    public unsafe struct TypeInfo
    {
        private ecs_type_info_t* _handle;

        /// <summary>
        /// A reference to the handle.
        /// </summary>
        public ref ecs_type_info_t* Handle => ref _handle;

        /// <summary>
        /// Creates a type info from the provided handle.
        /// </summary>
        /// <param name="typeInfo"></param>
        public TypeInfo(ecs_type_info_t* typeInfo)
        {
            _handle = typeInfo;
        }
    }
}
