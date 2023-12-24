using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace Savanna.Data.Behaviors
{
    public class AntelopeBehavior : Behavior
    {
        public override MovementAndTarget DecideNextMove(IBoard board, IAnimalManager _animalManager, Animal antelope, int visionRange)
        {
            var nearbyPredators = _animalManager.GetAnimalsInRange(antelope.Position, visionRange)
                                    .Where(animal => animal is IPredator)
                                    .ToList();

            if (!nearbyPredators.Any())
            {
                return new MovementAndTarget(RandomMove(), null);
            }

            return new MovementAndTarget(MoveByDistanceStrategy(nearbyPredators, board, antelope.Position, towards: false), null);
        }
    }
}