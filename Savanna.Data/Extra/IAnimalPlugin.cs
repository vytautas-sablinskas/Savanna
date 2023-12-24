namespace Savanna.GameLogic.Plugins
{
    public interface IAnimalPlugin
    {
        Type AnimalType { get; }
        ConsoleKey TriggerKey { get; }
    }
}