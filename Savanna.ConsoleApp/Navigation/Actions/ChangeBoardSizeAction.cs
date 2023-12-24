using Savanna.ConsoleApp.Navigation.Services;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Interfaces;

namespace Savanna.ConsoleApp.Navigation.Actions
{
    public class ChangeBoardSizeAction : IMenuAction
    {
        private readonly BoardDimensions _boardDimensions;
        private readonly IObjectSelectionService _dimensionSelectionService;
        private readonly IDisplayOperations _displayOperations;

        public ChangeBoardSizeAction(BoardDimensions boardDimensions, IObjectSelectionService dimensionSelectionService, IDisplayOperations displayOperations)
        {
            _boardDimensions = boardDimensions;
            _dimensionSelectionService = dimensionSelectionService;
            _displayOperations = displayOperations;
        }

        public void Execute()
        {
            bool userChangedValues = SelectDimensions();
            if (userChangedValues)
            {
                _displayOperations.WriteLine(Messages.SuccessfulDimensionChange(_boardDimensions.Height, _boardDimensions.Width));
            }
        }

        private bool SelectDimensions()
        {
            var width = (int?)_dimensionSelectionService.ExecuteService(Words.WIDTH.ToLower());
            if (!width.HasValue || width.Value == -1)
                return false;

            var height = (int?)_dimensionSelectionService.ExecuteService(Words.HEIGHT.ToLower());
            if (!height.HasValue || height.Value == -1)
                return false;

            SetDimensionValues(width.Value, height.Value);
            return true;
        }

        private void SetDimensionValues(int width, int height)
        {
            _boardDimensions.Width = width;
            _boardDimensions.Height = height;
        }
    }
}