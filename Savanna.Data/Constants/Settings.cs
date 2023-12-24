using Savanna.Data.Enums;

namespace Savanna.Data.Constants
{
    public class Settings
    {
        public static List<Direction> DIRECTIONS = new List<Direction>
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.Stay
        };

        public const int MAX_ATTACK_RANGE = 2;

        public const int ITERATIONS_TILL_BIRTH_IS_AVAILABLE = 3;
    }
}