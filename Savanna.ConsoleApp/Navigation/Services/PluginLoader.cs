using Savanna.Data.Animals;
using Savanna.GameLogic.Game;
using System.Reflection;

namespace Savanna.GameLogic.Plugins
{
    public class PluginLoader
    {
        private readonly string _dllPath;

        public PluginLoader(string dllPath)
        {
            _dllPath = dllPath;
        }

        public void LoadAnimalPlugins(List<AnimalGameInput> gameInputs)
        {
            try
            {
                var assembly = Assembly.LoadFrom(_dllPath);
                foreach (var type in assembly.GetTypes())
                {
                    var isUnusableAnimalPlugin = !typeof(IAnimalPlugin).IsAssignableFrom(type) || type.IsInterface;
                    if (isUnusableAnimalPlugin)
                        continue;

                    var pluginInstance = Activator.CreateInstance(type) as IAnimalPlugin;
                    if (gameInputs.Any(gi => gi.Key == pluginInstance.TriggerKey || gi.AnimalType == pluginInstance.AnimalType))
                        continue;

                    var animalInstance = Activator.CreateInstance(pluginInstance.AnimalType);
                    if ((animalInstance is IPredator || animalInstance is IPrey) && animalInstance is Animal)
                    {
                        var animalGameInput = new AnimalGameInput(pluginInstance.TriggerKey, () => { }, pluginInstance.AnimalType);
                        gameInputs.Add(animalGameInput);
                    }
                }
            } 
            catch
            {
                return;
            }
        }
    }
}