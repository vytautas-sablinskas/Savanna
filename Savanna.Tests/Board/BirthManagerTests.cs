using Moq;
using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.GameLogic.Board;

namespace BoardTests
{
    public class BirthManagerTests
    {
        private readonly BirthManager _birthManager;
        private readonly Mock<IAnimalManager> _mockAnimalManager = new Mock<IAnimalManager>();
        private Lion _lion1;
        private Lion _lion2;

        public BirthManagerTests()
        {
            _birthManager = new BirthManager(_mockAnimalManager.Object);
            _lion1 = new Lion { Position = new Position(0, 0) };
            _lion2 = new Lion { Position = new Position(0, 1) };
        }

        [Fact]
        public void TrackProximity_SameTypeAnimalsInProximity_IncrementsCounter()
        {
            var animals = new List<Animal> { _lion1, _lion2 };
            _mockAnimalManager.Setup(m => m.GetAllAnimals()).Returns(animals);

            _birthManager.TrackProximity();

            var proximityCount = GetProximityCount(_lion1, _lion2);
            Assert.Equal(1, proximityCount);
        }

        [Fact]
        public void TrackProximity_DifferentTypeAnimalsInProximity_DoesNotIncrementCounter()
        {
            var antelope = new Antelope { Position = new Position(0, 1) };
            var animals = new List<Animal> { _lion1, antelope };
            _mockAnimalManager.Setup(m => m.GetAllAnimals()).Returns(animals);

            _birthManager.TrackProximity();

            var proximityCount = GetProximityCount(_lion1, antelope);
            Assert.Equal(0, proximityCount);
        }

        [Fact]
        public void TrackProximity_SameTypeAnimalsMovedOutOfProximity_RemovesFromCounter()
        {
            var animals = new List<Animal> { _lion1, _lion2 };
            _mockAnimalManager.Setup(m => m.GetAllAnimals()).Returns(animals);

            _birthManager.TrackProximity();
            Assert.Equal(1, GetProximityCount(_lion1, _lion2));

            _lion2.Position = new Position(5, 5);
            _birthManager.TrackProximity();

            Assert.Equal(0, GetProximityCount(_lion1, _lion2));
        }

        [Fact]
        public void CheckIfBirthIsPossible_ThreeRoundsInProximity_ReturnsTrue()
        {
            var animals = new List<Animal> { _lion1, _lion2 };
            _mockAnimalManager.Setup(m => m.GetAllAnimals()).Returns(animals);

            _birthManager.TrackProximity();
            _birthManager.TrackProximity();
            _birthManager.TrackProximity();

            Assert.Equal(3, GetProximityCount(_lion1, _lion2));
            Assert.True(_birthManager.CheckIfBirthIsPossible());
        }

        [Fact]
        public void CheckIfBirthIsPossible_BrokeProximityAfterTwoRounds_ReturnsFalse()
        {
            var animals = new List<Animal> { _lion1, _lion2 };
            _mockAnimalManager.Setup(m => m.GetAllAnimals()).Returns(animals);

            _birthManager.TrackProximity();
            _birthManager.TrackProximity();
            _lion1.Position = new Position(3, 3);
            _birthManager.TrackProximity();

            Assert.Equal(0, GetProximityCount(_lion1, _lion2));
            Assert.False(_birthManager.CheckIfBirthIsPossible());
        }

        [Fact]
        public void BirthNewBaby_ThreeRoundsInProximity_AddsNewBabyToBoard()
        {
            var animals = new List<Animal> { _lion1, _lion2 };
            _mockAnimalManager.Setup(m => m.GetAllAnimals()).Returns(animals);

            _birthManager.TrackProximity();
            _birthManager.TrackProximity();
            _birthManager.TrackProximity();
            _birthManager.BirthNewBaby();

            _mockAnimalManager.Verify(m => m.AddAnimalToRandomPosition(It.IsAny<Animal>()), Times.Once());
        }

        private int GetProximityCount(Animal firstAnimal, Animal secondAnimal)
        {
            var key = OrderAnimalPair(firstAnimal, secondAnimal);
            return _birthManager.ProximityCounter.ContainsKey(key) ? _birthManager.ProximityCounter[key] : 0;
        }

        private (Animal, Animal) OrderAnimalPair(Animal firstAnimal, Animal secondAnimal)
        {
            return firstAnimal.GetHashCode() < secondAnimal.GetHashCode() ? (firstAnimal, secondAnimal) : (secondAnimal, firstAnimal);
        }
    }
}