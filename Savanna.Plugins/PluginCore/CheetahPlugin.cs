using Savanna.GameLogic.Plugins;
using Savanna.Plugins.GameCharacters;

namespace Savanna.Plugins.PluginCore
{
    internal class CheetahPlugin : IAnimalPlugin
    {
        public Type AnimalType => typeof(Cheetah);

        public ConsoleKey TriggerKey => ConsoleKey.C;
    }
}