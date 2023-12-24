using Moq;
using Savanna.Data.Animals;
using Savanna.Data.Behaviors;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Enums;
using Savanna.GameLogic.Board;

namespace BehaviorTests
{
    public class LionBehaviorTests
    {
        private LionBehavior _behavior;
        private Mock<IBoard> _mockBoard;
        private Mock<IAnimalManager> _mockAnimalManager;
        private Lion _lion;
        private EmptySquare _emptySquare;

        public LionBehaviorTests()
        {
            _behavior = new LionBehavior();
            _mockBoard = new Mock<IBoard>();
            _mockAnimalManager = new Mock<IAnimalManager>();
            _lion = new Lion { Position = new Position(0, 0) };
            _emptySquare = new EmptySquare();
        }

        private void SetupBoard(IPrintable[,] field)
        {
            _mockBoard.Setup(board => board.Field).Returns(field);
            _mockBoard.Setup(board => board.BoardDimensions).Returns(new BoardDimensions(field.GetLength(0), field.GetLength(1)));
        }

        [Fact]
        public void DecideNextMove_NoPreysNearby_ReturnsRandomMove()
        {
            SetupBoard(new IPrintable[,] { { _lion } });
            _mockAnimalManager.Setup(manager => manager.GetAnimalsInRange(_lion.Position, It.IsAny<int>()))
                      .Returns(new List<Animal>());

            var movementAndTarget = _behavior.DecideNextMove(_mockBoard.Object, _mockAnimalManager.Object, _lion, 5);

            Assert.Contains(movementAndTarget.DirectionToMoveTo, Settings.DIRECTIONS);
        }

        [Fact]
        public void DecideNextMove_PreysNearbyButOutOfAttackRange_MovesTowardsClosestPrey()
        {
            var antelope = new Antelope { Position = new Position(2, 2) };
            var expectedDirection = new List<Direction> { Direction.South, Direction.East };

            SetupBoard(new IPrintable[,]
            {
                { _lion, _emptySquare, _emptySquare },
                { _emptySquare, _emptySquare, _emptySquare },
                { _emptySquare, _emptySquare, antelope }
            });

            _mockAnimalManager.Setup(manager => manager.GetAnimalsInRange(_lion.Position, It.IsAny<int>()))
                  .Returns(new List<Animal> { antelope });

            var movementAndTarget = _behavior.DecideNextMove(_mockBoard.Object, _mockAnimalManager.Object, _lion, _lion.VisionLength);

            Assert.Contains(movementAndTarget.DirectionToMoveTo, expectedDirection);
        }

        [Fact]
        public void DecideNextMove_PreyWithinAttackRange_ReturnsCorrectPreyToAttack()
        {
            var antelope = new Antelope { Position = new Position(0, 1) };

            SetupBoard(new IPrintable[,] { { _lion, antelope } });

            _mockAnimalManager.Setup(manager => manager.GetAnimalsInRange(_lion.Position, It.IsAny<int>()))
                      .Returns(new List<Animal> { antelope });

            var movementAndTarget = _behavior.DecideNextMove(_mockBoard.Object, _mockAnimalManager.Object, _lion, 5);

            Assert.True(movementAndTarget.AnimalToAttack == antelope);
        }
    }
}