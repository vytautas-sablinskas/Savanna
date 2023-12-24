namespace Savanna.GameLogic.Game
{
    public class AnimalGameInput
    {
        public ConsoleKey Key { get; }
        public Action Event { get; set; }
        public Type AnimalType { get; }

        public AnimalGameInput(ConsoleKey key, Action eventToInvoke, Type animalType)
        {
            Key = key;
            Event = eventToInvoke;
            AnimalType = animalType;
        }
    }
}