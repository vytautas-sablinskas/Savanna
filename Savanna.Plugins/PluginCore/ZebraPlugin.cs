using Savanna.GameLogic.Plugins;
using Savanna.Plugins.GameCharacters;

namespace Savanna.Plugins.PluginCore
{
    public class ZebraPlugin : IAnimalPlugin
    {
        public Type AnimalType => typeof(Zebra);

        public ConsoleKey TriggerKey => ConsoleKey.Z;
    }
}