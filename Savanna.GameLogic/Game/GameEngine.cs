using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Interfaces;
using Savanna.GameLogic.Board;
using Savanna.GameLogic.Game;
using System.Text;

namespace Savanna.ConsoleApp.Game
{
    public class GameEngine : IGameEngine
    {
        private readonly IBoard _board;
        private readonly IDisplayOperations _displayOperations;
        private readonly IGameInputListener _gameInputListener;
        private readonly IAnimalManager _animalManager;
        private readonly IBirthManager _birthManager;
        private readonly List<AnimalGameInput> _animalGameInputs;

        private bool _isRunning = true;
        private readonly object _runningLock = new object();

        private bool IsRunning
        {
            get
            {
                lock (_runningLock)
                {
                    return _isRunning;
                }
            }
            set
            {
                lock (_runningLock)
                {
                    _isRunning = value;
                }
            }
        }

        public GameEngine(IBoard board, IDisplayOperations displayOperations, IGameInputListener gameInputListener, IAnimalManager animalManager, IBirthManager birthManager, List<AnimalGameInput> animalGameInputs)
        {
            _board = board;
            _displayOperations = displayOperations;
            _gameInputListener = gameInputListener;
            _animalManager = animalManager;
            _birthManager = birthManager;
            _animalGameInputs = animalGameInputs;

            SubscribeToEvents();
        }

        public void Start()
        {
            _board.Initialize();

            new Thread(_gameInputListener.StartListening).Start();

            while (IsRunning)
            {
                _birthManager.TrackProximity();
                _animalManager.UpdateAnimals();

                if (_birthManager.CheckIfBirthIsPossible())
                {
                    _birthManager.BirthNewBaby();
                }

                DisplayAllInformation();

                Thread.Sleep(AppTimes.ITERATION_TIME_MILLISECONDS);
            }

            UnsubscribeFromEvents();
            InformAboutEndOfGame();
        }

        private void SubscribeToEvents()
        {
            _gameInputListener.StopGameRequested += OnStopGameRequested;

            foreach (var input in _animalGameInputs)
            {
                input.Event += HandleAnimalGameInputEvent(input.AnimalType);
            }
        }

        private void UnsubscribeFromEvents()
        {
            _gameInputListener.StopGameRequested -= OnStopGameRequested;

            foreach (var input in _animalGameInputs)
            {
                input.Event -= HandleAnimalGameInputEvent(input.AnimalType);
            }
        }

        private void DisplayAllInformation()
        {
            _displayOperations.ClearMessages();
            InformAboutButtons();
            _board.Display();
        }

        private void OnStopGameRequested() => IsRunning = false;

        private Action HandleAnimalGameInputEvent(Type animalType)
        {
            return () =>
            {
                var animal = Activator.CreateInstance(animalType) as Animal;
                _animalManager.AddAnimalToRandomPosition(animal);
                DisplayAllInformation();
            };
        }

        private void InformAboutButtons()
        {
            var keys = string.Join(", ", _animalGameInputs.Select(input => input.Key));
            var message = _displayOperations.FormatMessage($"Keys To Add Animals - {keys} | S - Stops game");

            _displayOperations.WriteLine(message);
        }

        private void InformAboutEndOfGame()
        {
            _displayOperations.ClearMessages();
            _displayOperations.WriteLine("You have left the game! Press enter to continue.");
            _displayOperations.ReadLine();
        }
    }
}