using Savanna.ConsoleApp.Display;
using Savanna.ConsoleApp.Game;
using Savanna.ConsoleApp.LoopConditions;
using Savanna.ConsoleApp.Navigation.Services;
using Savanna.ConsoleDisplay.Navigation.Services;
using Savanna.Data.Animals;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Interfaces;
using Savanna.GameLogic.Abilities;
using Savanna.GameLogic.Board;
using Savanna.GameLogic.Game;
using Savanna.GameLogic.Plugins;
using System.Reflection.Metadata.Ecma335;

namespace Savanna.ConsoleApp.Navigation.Actions
{
    public class MenuActionFactory : IMenuActionFactory
    {
        private readonly IDisplayOperations _displayOperations;
        private readonly IObjectSelectionService _dimensionSelectionService;
        private readonly BoardDimensions _boardDimensions;

        public MenuActionFactory(IDisplayOperations displayOperations, IObjectSelectionService dimensionSelectionService, BoardDimensions boardDimensions)
        {
            _displayOperations = displayOperations;
            _dimensionSelectionService = dimensionSelectionService;
            _boardDimensions = boardDimensions;
        }

        public IMenuAction CreateMenuAction(string actionToExecute)
        {
            switch (actionToExecute)
            {
                case NavigationActions.START_GAME:
                    return new StartNewGameAction(InitializeGame());

                case NavigationActions.CHANGE_BOARD_SIZE:
                    return new ChangeBoardSizeAction(_boardDimensions, _dimensionSelectionService, _displayOperations);

                case NavigationActions.EXIT_APP:
                    return new ExitApplicationAction();

                default:
                    return null;
            }
        }

        private IGameEngine InitializeGame()
        {
            var runCondition = new AlwaysRunCondition();
            var board = new Board(_boardDimensions);

            var gameInputs = new List<AnimalGameInput>
            {
                new AnimalGameInput(ConsoleKey.L, () => { }, typeof(Lion)),
                new AnimalGameInput(ConsoleKey.A, () => { }, typeof(Antelope))
            };

            var fileSelectionService = new FileSelectionService(_displayOperations);
            var selectedDllFile = (string)fileSelectionService.ExecuteService(Paths.SAVED_FILES_FOLDER);
            if (!string.IsNullOrEmpty(selectedDllFile))
            {
                var pluginLoader = new PluginLoader(selectedDllFile);
                pluginLoader.LoadAnimalPlugins(gameInputs);
            }

            var gameInputListener = new GameInputListener(_displayOperations, runCondition, gameInputs);
            var abilityActivationRoller = new AbilityActivationRoller(new Random());
            var animalManager = new AnimalManager(board, abilityActivationRoller);
            var birthManager = new BirthManager(animalManager);

            return new GameEngine(board, _displayOperations, gameInputListener, animalManager, birthManager, gameInputs);
        }
    }
}