using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace Savanna.Data.Abilities
{
    public class Invisibility : IAbility
    {
        public bool IsActive { get; set; }

        public double ActivationChance => 0.10;

        private int durationByRounds;

        public void Activate()
        {
            IsActive = true;
            durationByRounds = 2;
        }

        public void ApplyEffect(Animal animal, IBoard board, IAnimalManager animalManager)
        {
            if (!IsActive)
                return;

            if (durationByRounds > 0)
                durationByRounds--;
            else
                IsActive = false;
        }
    }
}