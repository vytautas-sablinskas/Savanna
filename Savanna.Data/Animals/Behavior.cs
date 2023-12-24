using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Enums;

namespace Savanna.Data.Animals
{
    public abstract class Behavior
    {
        public abstract MovementAndTarget DecideNextMove(IBoard board, IAnimalManager animalManager, Animal lion, int visionRange);

        protected Direction MoveByDistanceStrategy(List<Animal> nearbyAnimals, IBoard board, Position currentPosition, bool towards)
        {
            var closestAnimalPosition = nearbyAnimals
                                        .OrderBy(animal => currentPosition.Distance(animal.Position))
                                        .First().Position;

            var validDirections = Settings.DIRECTIONS
                                  .Where(direction =>
                                  {
                                      var newPosition = currentPosition.GetNewPositionBasedOnDirection(direction);
                                      return newPosition.IsPositionValid(board.Field, board.BoardDimensions, currentPosition);
                                  })
                                  .ToList();

            var bestDirection = towards ?
                                validDirections
                                    .OrderBy(direction =>
                                    {
                                        var newPosition = currentPosition.GetNewPositionBasedOnDirection(direction);
                                        return newPosition.Distance(closestAnimalPosition);
                                    })
                                    .First() :
                                validDirections
                                    .OrderByDescending(direction =>
                                    {
                                        var newPosition = currentPosition.GetNewPositionBasedOnDirection(direction);
                                        return newPosition.Distance(closestAnimalPosition);
                                    })
                                    .First();

            return bestDirection;
        }

        protected Direction RandomMove()
        {
            Random rnd = new Random();
            Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
            return directions[rnd.Next(directions.Length)];
        }
    }
}