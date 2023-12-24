using Savanna.Data.Abilities;
using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Constants;

namespace Savanna.Data.Behaviors
{
    public class LionBehavior : Behavior
    {
        public override MovementAndTarget DecideNextMove(IBoard board, IAnimalManager animalManager, Animal lion, int visionRange)
        {
            var visiblePreys = animalManager.GetAnimalsInRange(lion.Position, visionRange)
                        .Where(animal => animal is IPrey && !(animal.Ability is Invisibility && animal.Ability.IsActive))
                        .ToList();

            if (!visiblePreys.Any())
            {
                return new MovementAndTarget(RandomMove(), null);
            }

            var targetPrey = (IPrey)visiblePreys.FirstOrDefault(prey => lion.Position.Distance(prey.Position) < Settings.MAX_ATTACK_RANGE);

            return new MovementAndTarget(MoveByDistanceStrategy(visiblePreys, board, lion.Position, towards: true), targetPrey);
        }
    }
}