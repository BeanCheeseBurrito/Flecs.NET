using System;
using Flecs.NET.Core.BindingContext;
using static Flecs.NET.Bindings.flecs;

namespace Flecs.NET.Core;

public static unsafe partial class Ecs
{
    /// <summary>
    ///     Fluent builder that registers opaque type reflection for a component. Opaque
    ///     types map a component to a type known to the flecs reflection framework, like
    ///     a string or a vector, so that it can be serialized and deserialized without
    ///     exposing its layout.
    /// </summary>
    /// <typeparam name="TComponent">The opaque type.</typeparam>
    /// <typeparam name="TElement">The element type of the opaque collection.</typeparam>
    public sealed unsafe class OpaqueBuilder<TComponent, TElement> where TElement : unmanaged
    {
        private readonly OpaqueContext<TComponent, TElement> _context;
        private readonly ecs_world_t* _world;
        private ecs_opaque_desc_t _desc;

        /// <summary>
        ///     The world.
        /// </summary>
        public ecs_world_t* World => _world;

        /// <summary>
        ///     A reference to the opaque type descriptor.
        /// </summary>
        public ref ecs_opaque_desc_t Desc => ref _desc;

        /// <summary>
        ///     Creates an opaque type builder for the provided type entity.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="entity">The type entity of the opaque type.</param>
        public OpaqueBuilder(ecs_world_t* world, ulong entity)
        {
            _world = world;
            _context = new OpaqueContext<TComponent, TElement>(new World(world));
            _desc = default;
            Desc.entity = entity;
        }

        /// <summary>
        ///     Sets the type that describes the serialized structure of the opaque type.
        /// </summary>
        /// <param name="type">The type id. (e.g. <see cref="Ecs.String"/>)</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> AsType(ulong type)
        {
            Desc.type.as_type = type;
            return this;
        }

        /// <summary>
        ///     Maps the opaque type to a string.
        /// </summary>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> String()
        {
            return AsType(Ecs.String);
        }

        /// <summary>
        ///     Maps the opaque type to a vector of <typeparamref name="TElement"/>.
        /// </summary>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> Vector()
        {
            return AsType(new World(World).Vector<TElement>());
        }

        /// <summary>
        ///     Maps the opaque type to an array of <typeparamref name="TElement"/>.
        /// </summary>
        /// <param name="count">The number of elements in the array.</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> Array(int count)
        {
            return AsType(new World(World).Array<TElement>(count));
        }

        /// <summary>
        ///     Sets the serialize callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> Serialize(Ecs.OpaqueSerializeCallback<TComponent> callback)
        {
            _context.Serialize = callback;
            Desc.type.serialize = _context.SerializePointer;
            return this;
        }

        /// <summary>
        ///     Sets the random-access struct member serialize callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> SerializeMember(Ecs.OpaqueSerializeMemberCallback<TComponent> callback)
        {
            _context.SerializeMember = callback;
            Desc.type.serialize_member = _context.SerializeMemberPointer;
            return this;
        }

        /// <summary>
        ///     Sets the random-access collection element serialize callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> SerializeElement(Ecs.OpaqueSerializeElementCallback<TComponent> callback)
        {
            _context.SerializeElement = callback;
            Desc.type.serialize_element = (delegate* unmanaged<ecs_serializer_t*, void*, ulong, int>)_context.SerializeElementPointer;
            return this;
        }

        /// <summary>
        ///     Sets the bool assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignBool(Ecs.OpaqueAssignBoolCallback<TComponent> callback)
        {
            _context.AssignBool = callback;
            Desc.type.assign_bool = _context.AssignBoolPointer;
            return this;
        }

        /// <summary>
        ///     Sets the char assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignChar(Ecs.OpaqueAssignCharCallback<TComponent> callback)
        {
            _context.AssignChar = callback;
            Desc.type.assign_char = _context.AssignCharPointer;
            return this;
        }

        /// <summary>
        ///     Sets the signed integer assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignInt(Ecs.OpaqueAssignIntCallback<TComponent> callback)
        {
            _context.AssignInt = callback;
            Desc.type.assign_int = _context.AssignIntPointer;
            return this;
        }

        /// <summary>
        ///     Sets the unsigned integer assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignUInt(Ecs.OpaqueAssignUIntCallback<TComponent> callback)
        {
            _context.AssignUInt = callback;
            Desc.type.assign_uint = _context.AssignUIntPointer;
            return this;
        }

        /// <summary>
        ///     Sets the floating-point assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignFloat(Ecs.OpaqueAssignFloatCallback<TComponent> callback)
        {
            _context.AssignFloat = callback;
            Desc.type.assign_float = _context.AssignFloatPointer;
            return this;
        }
        /// <summary>
        ///     Sets the assign string callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> AssignString(Ecs.OpaqueAssignStringCallback<TComponent> callback)
        {
            _context.AssignString = callback;
            Desc.type.assign_string = _context.AssignStringPointer;
            return this;
        }

        /// <summary>
        ///     Sets the entity assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignEntity(Ecs.OpaqueAssignEntityCallback<TComponent> callback)
        {
            _context.AssignEntity = callback;
            Desc.type.assign_entity = _context.AssignEntityPointer;
            return this;
        }

        /// <summary>
        ///     Sets the id assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignId(Ecs.OpaqueAssignIdCallback<TComponent> callback)
        {
            _context.AssignId = callback;
            Desc.type.assign_id = _context.AssignIdPointer;
            return this;
        }

        /// <summary>
        ///     Sets the null assignment callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> AssignNull(Ecs.OpaqueAssignNullCallback<TComponent> callback)
        {
            _context.AssignNull = callback;
            Desc.type.assign_null = _context.AssignNullPointer;
            return this;
        }

        /// <summary>
        ///     Sets the collection clear callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> Clear(Ecs.OpaqueClearCallback<TComponent> callback)
        {
            _context.Clear = callback;
            Desc.type.clear = _context.ClearPointer;
            return this;
        }

        /// <summary>
        ///     Sets the struct member lookup callback.
        /// </summary>
        public OpaqueBuilder<TComponent, TElement> EnsureMember(Ecs.OpaqueEnsureMemberCallback<TComponent> callback)
        {
            _context.EnsureMember = callback;
            Desc.type.ensure_member = _context.EnsureMemberPointer;
            return this;
        }
        /// <summary>
        ///     Sets the element count callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> Count(Ecs.OpaqueCountCallback<TComponent> callback)
        {
            _context.Count = callback;
            Desc.type.count = _context.CountPointer;
            return this;
        }

        /// <summary>
        ///     Sets the resize callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> Resize(Ecs.OpaqueResizeCallback<TComponent> callback)
        {
            _context.Resize = callback;
            Desc.type.resize = _context.ResizePointer;
            return this;
        }

        /// <summary>
        ///     Sets the ensure element callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>Reference to self.</returns>
        public OpaqueBuilder<TComponent, TElement> EnsureElement(Ecs.OpaqueEnsureElementCallback<TComponent, TElement> callback)
        {
            _context.EnsureElement = callback;
            Desc.type.ensure_element = _context.EnsureElementPointer;
            return this;
        }

        /// <summary>
        ///     Commits the opaque type descriptor. Called when the builder callback
        ///     passed to <see cref="Component{TComponent}.Opaque"/> returns.
        /// </summary>
        internal void Commit()
        {
            Ecs.Assert(Desc.type.as_type != 0, "Opaque type must map to a type. Use String, Vector, Array or AsType.");

            fixed (ecs_opaque_desc_t* desc = &Desc)
            {
                ulong entity = ecs_opaque_init(World, desc);
                Ecs.Assert(entity != 0, nameof(ECS_INVALID_PARAMETER));
            }
        }
    }
}
