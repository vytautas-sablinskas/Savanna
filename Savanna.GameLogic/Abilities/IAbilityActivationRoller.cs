using Savanna.Data.Animals;

namespace Savanna.GameLogic.Abilities
{
    public interface IAbilityActivationRoller
    {
        void AttemptActivateAbility(Animal animal);
    }
}