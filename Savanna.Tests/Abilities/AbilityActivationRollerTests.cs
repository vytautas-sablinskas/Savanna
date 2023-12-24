using Moq;
using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.GameLogic.Abilities;

namespace AbilitiesTests
{
    public class AbilityActivationRollerTests
    {
        private readonly Mock<Animal> _mockAnimal;
        private readonly Mock<Random> _mockRandom;
        private readonly AbilityActivationRoller _abilityActivationRoller;

        public AbilityActivationRollerTests()
        {
            _mockAnimal = new Mock<Animal>();
            _mockRandom = new Mock<Random>();
            _abilityActivationRoller = new AbilityActivationRoller(_mockRandom.Object);
        }

        [Fact]
        public void AttemptActivateAbility_ActivationChanceHigherThanRoll_ActivatesAbility()
        {
            var abilityActivated = 0.05;
            var ability = new Invisibility();

            _mockRandom.Setup(r => r.NextDouble()).Returns(abilityActivated);
            _mockAnimal.Setup(a => a.Ability).Returns(ability);

            _abilityActivationRoller.AttemptActivateAbility(_mockAnimal.Object);

            Assert.True(ability.IsActive);
        }
    }
}