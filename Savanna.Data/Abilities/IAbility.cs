using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace Savanna.Data.Abilities
{
    public interface IAbility
    {
        bool IsActive { get; }

        double ActivationChance { get; }

        void Activate();

        void ApplyEffect(Animal animal, IBoard board, IAnimalManager animalManager);
    }
}