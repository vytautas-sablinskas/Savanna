using Moq;
using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Enums;
namespace AbilitiesTests
{
    public class SpeedBoostTests
    {
        private readonly SpeedBoost _speedBoost;

        public SpeedBoostTests()
        {
            _speedBoost = new SpeedBoost();
        }

        [Fact]
        public void TestSpeedBoostActivation()
        {
            _speedBoost.Activate();

            Assert.True(_speedBoost.IsActive);
        }

        [Fact]
        public void TestSpeedBoostEffectApplication()
        {
            var mockAnimal = new Mock<Animal>();
            var mockBoard = new Mock<IBoard>();
            var mockAnimalManager = new Mock<IAnimalManager>();
            var mockBehavior = new Mock<Behavior>();

            mockBehavior.Setup(b => b.DecideNextMove(It.IsAny<IBoard>(), It.IsAny<IAnimalManager>(), It.IsAny<Animal>(), It.IsAny<int>()))
                .Returns(new MovementAndTarget(Direction.North, null));
            mockAnimal.Setup(a => a.Behavior).Returns(mockBehavior.Object);

            _speedBoost.Activate();
            Assert.True(_speedBoost.IsActive);

            _speedBoost.ApplyEffect(mockAnimal.Object, mockBoard.Object, mockAnimalManager.Object);
            Assert.False(_speedBoost.IsActive);
        }
    }
}