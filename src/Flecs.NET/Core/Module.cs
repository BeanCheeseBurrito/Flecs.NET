using Flecs.NET.Utilities;
using static Flecs.NET.Bindings.Native;

namespace Flecs.NET.Core
{
    public static unsafe class Module
    {
        public static Entity Import<T>(World world) where T : IFlecsModule, new()
        {
            string symbol = Type<T>.GetSymbolName();
            using NativeString nativeSymbol = (NativeString)symbol;

            ulong module = ecs_lookup_symbol(world, nativeSymbol, Macros.True, Macros.False);

            if (!Type<T>.IsRegistered(world))
            {
                if (module != 0)
                    Type<T>.Init(module, false);
                else
                    module = DoImport<T>(world, symbol);
            }
            else if (module == 0)
            {
                module = DoImport<T>(world, symbol);
            }

            return new Entity(world, module);
        }

        public static ulong DoImport<T>(World world, string symbol) where T : IFlecsModule, new()
        {
            ulong scope = ecs_set_scope(world, 0);

            Component<T> moduleComponent = new Component<T>(world, null, false);
            ecs_add_id(world, moduleComponent, EcsModule);

            ecs_set_scope(world, moduleComponent);

            T module = new T();
            module.InitModule(ref world);
            world.Set(ref module);

            ecs_set_scope(world, scope);

            using NativeString nativeSymbol = (NativeString)symbol;
            ulong moduleEntity = ecs_lookup_symbol(world, nativeSymbol, Macros.True, Macros.False);
            Assert.True(moduleEntity != 0, $"{nameof(ECS_MODULE_UNDEFINED)} {symbol}");
            Assert.True(moduleEntity == moduleComponent, nameof(ECS_INTERNAL_ERROR));

            return moduleEntity;
        }
    }
}