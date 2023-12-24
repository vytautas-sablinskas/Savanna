using Savanna.Data.Animals;
using Savanna.Data.Enums;

namespace Savanna.Data.Board
{
    public interface IAnimalManager
    {
        Position AddAnimalToRandomPosition(Animal animal);

        void UpdateAnimals();

        void UpdateAnimalPosition(Animal animal, Direction direction);

        List<Animal> GetAllAnimals();

        List<Animal> GetAnimalsInRange(Position position, int range);
    }
}