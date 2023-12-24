using Savanna.Data.Abilities;
using Savanna.Data.Animals;

namespace Savanna.GameLogic.Abilities
{
    public class AbilityActivationRoller : IAbilityActivationRoller
    {
        private readonly Random _random;

        public AbilityActivationRoller(Random random)
        {
            _random = random;
        }

        public void AttemptActivateAbility(Animal animal)
        {
            IAbility ability = animal.Ability;

            if (ability is null || ability.IsActive)
                return;

            if (_random.NextDouble() < ability.ActivationChance)
            {
                ability.Activate();
            }
        }
    }
}