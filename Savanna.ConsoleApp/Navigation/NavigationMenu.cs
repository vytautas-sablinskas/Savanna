using Savanna.ConsoleApp.Display;
using Savanna.ConsoleApp.LoopConditions;
using Savanna.ConsoleApp.Navigation.Actions;
using Savanna.Data.Constants;

namespace Savanna.ConsoleApp.Navigation
{
    public class NavigationMenu
    {
        private readonly IRunCondition _runCondition;
        private readonly IMenuActionFactory _menuActionFactory;
        private readonly DisplayOperations _displayOperations;

        public NavigationMenu(IRunCondition runCondition, IMenuActionFactory menuActionFactory, DisplayOperations displayOperations)
        {
            _runCondition = runCondition;
            _menuActionFactory = menuActionFactory;
            _displayOperations = displayOperations;
        }

        public void ShowMenu()
        {
            while (_runCondition.ShouldContinue())
            {
                _displayOperations.ClearMessages();

                var userChoice = _displayOperations.PromptInput(Messages.MENU_INFORMATION);
                ExecuteUserSelectedAction(userChoice);
            }
        }

        private void ExecuteUserSelectedAction(string actionToExecute)
        {
            var action = _menuActionFactory.CreateMenuAction(actionToExecute);
            if (action == null)
            {
                _displayOperations.ClearMessages();
                _displayOperations.PromptInput(Messages.INVALID_ACTION);
                return;
            }

            action.Execute();
        }
    }
}