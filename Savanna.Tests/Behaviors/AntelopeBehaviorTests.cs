using Moq;
using Savanna.Data.Animals;
using Savanna.Data.Behaviors;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Enums;
using Savanna.GameLogic.Board;

namespace BehaviorTests
{
    public class AntelopeBehaviorTests
    {
        private readonly AntelopeBehavior _behavior;
        private readonly Mock<IBoard> _mockBoard;
        private readonly Mock<IAnimalManager> _mockAnimalManager;
        private readonly EmptySquare _emptySquare;
        private readonly Antelope _antelope;
        private readonly Lion _lion;

        public AntelopeBehaviorTests()
        {
            _behavior = new AntelopeBehavior();
            _mockBoard = new Mock<IBoard>();
            _mockAnimalManager = new Mock<IAnimalManager>();
            _emptySquare = new EmptySquare();
            _antelope = new Antelope { Position = new Position(1, 1) };
            _lion = new Lion { Position = new Position(0, 1) };
        }

        private void SetupBoard(IPrintable[,] fields)
        {
            _mockBoard.Setup(board => board.Field).Returns(fields);
            _mockBoard.Setup(board => board.BoardDimensions).Returns(new BoardDimensions(fields.GetLength(0), fields.GetLength(1)));
        }

        [Fact]
        public void DecideNextMove_NoPreysNearby_ReturnsRandomMove()
        {
            SetupBoard(new IPrintable[,] { { _antelope } });
            _mockAnimalManager.Setup(manager => manager.GetAnimalsInRange(_antelope.Position, It.IsAny<int>()))
                      .Returns(new List<Animal>());

            var movementAntTarget = _behavior.DecideNextMove(_mockBoard.Object, _mockAnimalManager.Object, _antelope, 5);

            Assert.Contains(movementAntTarget.DirectionToMoveTo, Settings.DIRECTIONS);
        }

        [Fact]
        public void DecideNextMove_SinglePredatorNearby_MovesAwayFromPredator()
        {
            SetupBoard(new IPrintable[,]
            {
                { _emptySquare, _lion },
                { _emptySquare, _antelope },
                { _emptySquare, _emptySquare }
            });
            _mockAnimalManager.Setup(manager => manager.GetAnimalsInRange(_antelope.Position, It.IsAny<int>()))
                      .Returns(new List<Animal> { _lion });

            var movementAndTarget = _behavior.DecideNextMove(_mockBoard.Object, _mockAnimalManager.Object, _antelope, _antelope.VisionLength);

            Assert.Equal(Direction.South, movementAndTarget.DirectionToMoveTo);
        }

        [Fact]
        public void DecideNextMove_MultiplePredators_ThenMovesAwayFromClosestPredators()
        {
            var lion2 = new Lion { Position = new Position(1, 2) };

            SetupBoard(new IPrintable[,]
            {
                { _emptySquare, _lion, _emptySquare },
                { _emptySquare, _antelope, lion2 },
                { _emptySquare, _emptySquare, _emptySquare }
            });
            _mockAnimalManager.Setup(manager => manager.GetAnimalsInRange(_antelope.Position, It.IsAny<int>()))
                      .Returns(new List<Animal> { _lion, lion2 });

            var movementAndTarget = _behavior.DecideNextMove(_mockBoard.Object, _mockAnimalManager.Object, _antelope, 5);

            var expectedDirections = new List<Direction> { Direction.South, Direction.West };
            Assert.Contains(movementAndTarget.DirectionToMoveTo, expectedDirections);
        }
    }
}