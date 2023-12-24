using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Constants;

namespace Savanna.GameLogic.Board
{
    public class BirthManager : IBirthManager
    {
        public Dictionary<(Animal, Animal), int> ProximityCounter = new Dictionary<(Animal, Animal), int>();
        private HashSet<(Animal, Animal)> _currentProximityPairs = new HashSet<(Animal, Animal)>();
        private readonly IAnimalManager _animalManager;

        public BirthManager(IAnimalManager animalManager)
        {
            _animalManager = animalManager;
        }

        public void TrackProximity()
        {
            var animalList = _animalManager.GetAllAnimals();
            _currentProximityPairs.Clear();

            for (int i = 0; i < animalList.Count; i++)
            {
                var firstAnimal = animalList[i];
                var possibleNeighbors = firstAnimal.Position.GetValidNeighborPositions();

                for (int j = i + 1; j < animalList.Count; j++)
                {
                    var secondAnimal = animalList[j];

                    if (firstAnimal.GetType() != secondAnimal.GetType())
                        continue;

                    if (!possibleNeighbors.Contains(secondAnimal.Position))
                        continue;

                    var orderedKey = OrderAnimalPair(firstAnimal, secondAnimal);
                    _currentProximityPairs.Add(orderedKey);

                    if (ProximityCounter.ContainsKey(orderedKey))
                    {
                        ProximityCounter[orderedKey]++;
                    }
                    else
                    {
                        ProximityCounter[orderedKey] = 1;
                    }
                }
            }

            ResetCounterForAnimalsNotInProximityAnymore();
        }

        public void BirthNewBaby()
        {
            var pairsReadyForBirth = ProximityCounter.Where(c => c.Value == Settings.ITERATIONS_TILL_BIRTH_IS_AVAILABLE).ToList();

            foreach (var pair in pairsReadyForBirth)
            {
                var animalType = pair.Key.Item1.GetType();
                var newAnimal = (Animal)Activator.CreateInstance(animalType);

                _animalManager.AddAnimalToRandomPosition(newAnimal);

                ProximityCounter.Clear();
            }
        }

        public bool CheckIfBirthIsPossible() => ProximityCounter.Any(c => c.Value == Settings.ITERATIONS_TILL_BIRTH_IS_AVAILABLE);

        private (Animal, Animal) OrderAnimalPair(Animal firstAnimal, Animal secondAnimal)
        {
            return firstAnimal.GetHashCode() < secondAnimal.GetHashCode() ? (firstAnimal, secondAnimal) : (secondAnimal, firstAnimal);
        }

        private void ResetCounterForAnimalsNotInProximityAnymore()
        {
            foreach (var key in ProximityCounter.Keys.ToList())
            {
                if (!_currentProximityPairs.Contains(key))
                {
                    ProximityCounter.Remove(key);
                }
            }
        }
    }
}