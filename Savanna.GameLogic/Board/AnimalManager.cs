using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Enums;
using Savanna.GameLogic.Abilities;

namespace Savanna.GameLogic.Board
{
    public class AnimalManager : IAnimalManager
    {
        private readonly IBoard _board;
        private readonly IAbilityActivationRoller _abilityActivationRoller;

        public AnimalManager(IBoard board, IAbilityActivationRoller abilityActivationRoller)
        {
            _board = board;
            _abilityActivationRoller = abilityActivationRoller;
        }

        public List<Animal> GetAnimalsInRange(Position position, int range)
        {
            List<Animal> animalsInRange = new List<Animal>();

            int startRow = Math.Max(0, position.Y - range);
            int endRow = Math.Min(_board.BoardDimensions.Height - 1, position.Y + range);
            int startCol = Math.Max(0, position.X - range);
            int endCol = Math.Min(_board.BoardDimensions.Width - 1, position.X + range);

            for (int row = startRow; row <= endRow; row++)
            {
                for (int col = startCol; col <= endCol; col++)
                {
                    if (_board.Field[row, col] is Animal animal)
                    {
                        animalsInRange.Add(animal);
                    }
                }
            }

            return animalsInRange;
        }

        public Position AddAnimalToRandomPosition(Animal animal)
        {
            var emptyPositions = GetEmptySquarePositions();
            if (emptyPositions.Count == 0)
                return null;

            return GenerateAndAssignRandomPositionToAnimal(emptyPositions, animal);
        }

        public List<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();

            for (int row = 0; row < _board.BoardDimensions.Height; row++)
            {
                for (int col = 0; col < _board.BoardDimensions.Width; col++)
                {
                    if (_board.Field[row, col] is Animal animal)
                    {
                        animals.Add(animal);
                    }
                }
            }

            return animals;
        }

        private Position GenerateAndAssignRandomPositionToAnimal(List<Position> emptyPositions, Animal animal)
        {
            Random rand = new Random();
            Position randomEmptyPosition = emptyPositions[rand.Next(emptyPositions.Count)];

            _board.Field[randomEmptyPosition.Y, randomEmptyPosition.X] = animal;
            animal.Position = new Position(randomEmptyPosition.Y, randomEmptyPosition.X);

            return animal.Position;
        }

        private List<Position> GetEmptySquarePositions()
        {
            List<Position> emptyPositions = new List<Position>();

            for (int row = 0; row < _board.BoardDimensions.Height; row++)
            {
                for (int column = 0; column < _board.BoardDimensions.Width; column++)
                {
                    if (_board.Field[row, column] is EmptySquare)
                    {
                        emptyPositions.Add(new Position(row, column));
                    }
                }
            }

            return emptyPositions;
        }

        public void UpdateAnimals()
        {
            UpdateAnimalsByType<IPrey>();
            UpdateAnimalsByType<IPredator>();
        }

        private void UpdateAnimalsByType<T>() where T : class
        {
            foreach (var animal in GetAllAnimals().Where(a => a is T))
            {
                _abilityActivationRoller.AttemptActivateAbility(animal);
                animal.Ability.ApplyEffect(animal, _board, this);

                MovementAndTarget movementAndTarget = animal.Behavior.DecideNextMove(_board, this, animal, animal.VisionLength);
                UpdateAnimalPosition(animal, movementAndTarget.DirectionToMoveTo);
                animal.DecreaseHealthAfterMove();

                if (animal is IPredator && movementAndTarget.AnimalToAttack != null)
                    AttackPrey((IPredator)animal, movementAndTarget.AnimalToAttack);
            }
        }

        public void UpdateAnimalPosition(Animal animal, Direction direction)
        {
            if (animal.IsDead())
            {
                _board.Field[animal.Position.Y, animal.Position.X] = new EmptySquare();
                return;
            }

            UpdateAliveAnimalPosition(animal, direction);
        }

        private void AttackPrey(IPredator predator, IPrey prey)
        {
            predator.Attack(prey);
        }

        private void UpdateAliveAnimalPosition(Animal animal, Direction direction)
        {
            Position currentPosition = animal.Position;
            Position newPosition = currentPosition.GetNewPositionBasedOnDirection(direction);

            if (newPosition.IsPositionValid(_board.Field, _board.BoardDimensions, currentPosition))
            {
                _board.Field[currentPosition.Y, currentPosition.X] = new EmptySquare();
                _board.Field[newPosition.Y, newPosition.X] = animal;

                animal.Position = newPosition;
            }
        }
    }
}