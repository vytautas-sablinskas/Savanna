using Savanna.ConsoleApp.LoopConditions;
using Savanna.Data.Interfaces;
using Savanna.GameLogic.Game;

namespace Savanna.ConsoleApp.Game
{
    public class GameInputListener : IGameInputListener
    {
        public event Action StopGameRequested;

        private readonly IDisplayOperations _displayOperations;
        private readonly IRunCondition _runCondition;
        private readonly List<AnimalGameInput> _gameInputs;

        public GameInputListener(IDisplayOperations displayOperations, IRunCondition runCondition, List<AnimalGameInput> gameInputs)
        {
            _displayOperations = displayOperations;
            _runCondition = runCondition;
            _gameInputs = gameInputs;
        }

        public void StartListening()
        {
            while (_runCondition.ShouldContinue())
            {
                var key = _displayOperations.ReadKey(intercept: true).Key;
                
                if (key == ConsoleKey.S)
                {
                    StopGameRequested?.Invoke();
                }

                foreach (var input in _gameInputs)
                {
                    if (key == input.Key)
                    {
                        input.Event?.Invoke();
                    }
                }
            }
        }
    }
}