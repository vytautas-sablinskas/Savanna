using Savanna.Data.Constants;
using Savanna.Data.Interfaces;

namespace Savanna.ConsoleApp.Navigation.Services
{
    public class DimensionSelectionService : IObjectSelectionService
    {
        private readonly IDisplayOperations _displayOperations;

        public DimensionSelectionService(IDisplayOperations displayOperations)
        {
            _displayOperations = displayOperations;
        }

        public object ExecuteService(object input)
        {
            var dimensionName = (string)input;
            return (int)GetDimension(dimensionName);
        }

        private int? GetDimension(string dimensionName)
        {
            _displayOperations.ClearMessages();

            while (true)
            {
                var input = _displayOperations.PromptInput(Messages.SelectDimension(dimensionName));
                if (string.IsNullOrWhiteSpace(input))
                {
                    _displayOperations.WriteLine(Messages.INVALID_DIMENSION);
                    continue;
                }

                var userExits = _displayOperations.UserSelectedExitToMainMenu(input);
                if (userExits)
                {
                    return NavigationActions.EXIT_TO_MAIN_MENU;
                }

                var isValidDimension = int.TryParse(input, out var newSize) &&
                    newSize >= BoardSizeLimits.SMALLEST_BOARD_DIMENSION &&
                    newSize <= BoardSizeLimits.LARGEST_BOARD_DIMENSION;
                if (!isValidDimension)
                {
                    _displayOperations.WriteLine(Messages.INVALID_DIMENSION);
                    continue;
                }

                return newSize;
            }
        }
    }
}