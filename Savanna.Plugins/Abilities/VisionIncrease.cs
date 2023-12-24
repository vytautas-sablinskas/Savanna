using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace Savanna.Plugins.Abilities
{
    public class VisionIncrease : IAbility
    {
        public bool IsActive { get; set; }

        public double ActivationChance => 0.20;

        private int durationByRounds;

        public void Activate()
        {
            IsActive = true;
            durationByRounds = 2;
        }

        public void ApplyEffect(Animal animal, IBoard board, IAnimalManager animalManager)
        {
            animal.VisionLength *= 2;

            if (!IsActive)
                return;

            if (durationByRounds > 0)
            {
                durationByRounds--;
                return;
            }

            IsActive = false;
            animal.VisionLength /= 2;
        }
    }
}