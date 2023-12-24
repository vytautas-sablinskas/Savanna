using Savanna.Data.Enums;

namespace Savanna.Data.Animals
{
    public class MovementAndTarget
    {
        public Direction DirectionToMoveTo { get; set; }

        public IPrey AnimalToAttack { get; set; }

        public MovementAndTarget(Direction direction, IPrey animal)
        {
            DirectionToMoveTo = direction;
            AnimalToAttack = animal;
        }
    }
}