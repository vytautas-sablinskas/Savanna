using Savanna.Data.Animals;
using Savanna.Data.Constants;
using Savanna.Data.Enums;

namespace Savanna.Data.Board
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int row, int column)
        {
            X = column;
            Y = row;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public bool IsWithinBoard()
        {
            return X >= 0
                   && X < DefaultBoardDimensions.WIDTH
                   && Y >= 0
                   && Y < DefaultBoardDimensions.HEIGHT;
        }

        public double Distance(Position otherPosition)
        {
            return Math.Sqrt(Math.Pow(otherPosition.X - X, 2) + Math.Pow(otherPosition.Y - Y, 2));
        }

        public bool IsPositionValid(IPrintable[,] field, BoardDimensions dimensions, Position currentPosition)
        {
            var thisPositionIsSameAsCurrent = currentPosition.Equals(this);
            var isWithinBorders = X >= 0 && X < dimensions.Width &&
                                  Y >= 0 && Y < dimensions.Height;

            return thisPositionIsSameAsCurrent ||
                   isWithinBorders && field[Y, X] is EmptySquare;
        }

        public bool IsValidMoveDirection(Direction direction)
        {
            Position newPosition = GetNewPositionBasedOnDirection(direction);

            return newPosition.IsWithinBoard();
        }

        public Position GetNewPositionBasedOnDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Position(Y - 1, X);

                case Direction.South:
                    return new Position(Y + 1, X);

                case Direction.East:
                    return new Position(Y, X + 1);

                case Direction.West:
                    return new Position(Y, X - 1);

                default:
                    return this;
            }
        }

        public IEnumerable<Position> GetValidNeighborPositions()
        {
            var possiblePositions = new List<Position>
            {
                new Position(Y - 1, X),
                new Position(Y + 1, X),
                new Position(Y, X + 1),
                new Position(Y, X - 1),
                new Position(Y - 1, X - 1),
                new Position(Y - 1, X + 1),
                new Position(Y + 1, X - 1),
                new Position(Y + 1, X + 1)
            };

            return possiblePositions.Where(position => position.IsWithinBoard());
        }
    }
}