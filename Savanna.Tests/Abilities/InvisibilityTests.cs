using Moq;
using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace AbilitiesTests
{
    public class InvisibilityTests
    {
        private readonly Invisibility _invisibilitySpell;

        public InvisibilityTests()
        {
            _invisibilitySpell = new Invisibility();
        }

        [Fact]
        public void TestInvisibilityActivation()
        {
            _invisibilitySpell.Activate();

            Assert.True(_invisibilitySpell.IsActive);
        }

        [Fact]
        public void TestInvisibilityEffectApplication()
        {
            var mockAnimal = new Mock<Animal>();
            var mockBoard = new Mock<IBoard>();
            var mockAnimalManager = new Mock<IAnimalManager>();

            _invisibilitySpell.Activate();
            _invisibilitySpell.ApplyEffect(mockAnimal.Object, mockBoard.Object, mockAnimalManager.Object);
            Assert.True(_invisibilitySpell.IsActive);

            _invisibilitySpell.ApplyEffect(mockAnimal.Object, mockBoard.Object, mockAnimalManager.Object);
            Assert.True(_invisibilitySpell.IsActive);

            _invisibilitySpell.ApplyEffect(mockAnimal.Object, mockBoard.Object, mockAnimalManager.Object);
            Assert.False(_invisibilitySpell.IsActive);
        }
    }
}