using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace Savanna.Data.Abilities
{
    public class SpeedBoost : IAbility
    {
        public bool IsActive { get; set; }

        public double ActivationChance => 0.15;

        public void Activate()
        {
            IsActive = true;
        }

        public void ApplyEffect(Animal animal, IBoard board, IAnimalManager animalManager)
        {
            if (!IsActive)
                return;

            var movementAndTarget = animal.Behavior.DecideNextMove(board, animalManager, animal, animal.VisionLength);
            animalManager.UpdateAnimalPosition(animal, movementAndTarget.DirectionToMoveTo);

            IsActive = false;
        }
    }
}