using Savanna.ConsoleApp.Display;
using Savanna.ConsoleApp.LoopConditions;
using Savanna.ConsoleApp.Navigation;
using Savanna.ConsoleApp.Navigation.Actions;
using Savanna.ConsoleApp.Navigation.Services;
using Savanna.Data.Board;
using Savanna.GameLogic.Board;

namespace Savanna.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var navigationMenu = InitializeNavigationMenu();

            navigationMenu.ShowMenu();
        }

        private static NavigationMenu InitializeNavigationMenu()
        {
            var runCondition = new AlwaysRunCondition();
            var boardDimensions = new BoardDimensions();
            var displayOperations = new DisplayOperations();
            var dimensionSelectionService = new DimensionSelectionService(displayOperations);
            var menuActionFactory = new MenuActionFactory(displayOperations, dimensionSelectionService, boardDimensions);

            return new NavigationMenu(runCondition, menuActionFactory, displayOperations);
        }
    }
}