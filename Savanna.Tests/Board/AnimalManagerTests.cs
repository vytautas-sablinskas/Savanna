using Moq;
using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Enums;
using Savanna.GameLogic.Abilities;
using Savanna.GameLogic.Board;

namespace BoardTests
{
    public class AnimalManagerTests
    {
        private Mock<IBoard> _mockBoard;
        private Mock<IAbilityActivationRoller> _mockAbilityActivationRoller;
        private readonly BoardDimensions _boardDimensions;
        private AnimalManager _animalManager;
        private IPrintable[,] _field;

        public AnimalManagerTests()
        {
            _mockBoard = new Mock<IBoard>();
            _mockAbilityActivationRoller = new Mock<IAbilityActivationRoller>();
            _boardDimensions = new BoardDimensions(10, 10);
            _field = InitializeBoard(10);
            SetupBoardMocks();

            _animalManager = new AnimalManager(_mockBoard.Object, _mockAbilityActivationRoller.Object);
        }

        [Fact]
        public void GetAnimalsInRange_ReturnsCorrectAnimals()
        {
            var testPosition = new Position(5, 5);
            var testAnimal1 = new Lion { Position = new Position(4, 4) };
            var testAnimal2 = new Antelope { Position = new Position(6, 6) };

            _field[4, 4] = testAnimal1;
            _field[6, 6] = testAnimal2;

            var result = _animalManager.GetAnimalsInRange(testPosition, 2);

            Assert.Contains(testAnimal1, result);
            Assert.Contains(testAnimal2, result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void AddAnimalToRandomPosition_AddsAnimalToBoard()
        {
            var resultPosition = _animalManager.AddAnimalToRandomPosition(new Lion());

            Assert.True(_field[resultPosition.Y, resultPosition.X] is Lion);
        }

        [Fact]
        public void GetAllAnimals_ReturnsAllAnimals()
        {
            _animalManager.AddAnimalToRandomPosition(new Lion());
            _animalManager.AddAnimalToRandomPosition(new Lion());

            var animals = _animalManager.GetAllAnimals();

            Assert.True(animals.Count == 2);
        }

        [Fact]
        public void UpdateAnimals_SuccessfullyAttacksIfTheresATarget()
        {
            var antelope = new Antelope { Position = new Position(1, 1) };
            var lion = new Lion { Position = new Position(0, 0) };

            _field[0, 0] = lion;
            _field[1, 1] = antelope;

            _animalManager.UpdateAnimals();

            Assert.True(antelope.Health < 100);
        }

        [Fact]
        public void UpdateAnimalPosition_DeadAnimal_MakesSquareEmpty()
        {
            var antelope = new Antelope { Position = new Position(1, 1) };
            _field[1, 1] = antelope;

            var lion = new Lion();
            for (int i = 0; i < 7; i++)
            {
                lion.Attack(antelope);
            }

            _animalManager.UpdateAnimalPosition(antelope, Direction.South);
            var animalsOnBoard = _animalManager.GetAllAnimals();

            Assert.True(_field[1, 1] is EmptySquare);
            Assert.True(animalsOnBoard.Count == 0);
        }

        [Fact]
        public void UpdateAnimalPosition_AliveAnimal_MovesToDirectionGiven()
        {
            var antelope = new Antelope { Position = new Position(1, 1) };
            _field[1, 1] = antelope;

            _animalManager.UpdateAnimalPosition(antelope, Direction.South);

            Assert.True(_field[2, 1] is Antelope);
        }

        private void SetupBoardMocks()
        {
            _mockBoard.Setup(b => b.Field).Returns(_field);
            _mockBoard.Setup(b => b.BoardDimensions).Returns(_boardDimensions);
        }

        private IPrintable[,] InitializeBoard(int size)
        {
            var boardField = new IPrintable[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    boardField[i, j] = new EmptySquare();
                }
            }

            return boardField;
        }
    }
}