using Moq;
using Savanna.ConsoleApp.Navigation.Actions;
using Savanna.ConsoleApp.Navigation.Services;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Interfaces;

namespace NavigationTests
{
    public class MenuActionFactoryTests
    {
        private readonly Mock<IDisplayOperations> _mockDisplayOperations;
        private readonly Mock<IObjectSelectionService> _objectSelectionService;
        private readonly Mock<BoardDimensions> _mockGameFieldDimensions;
        private readonly MenuActionFactory _menuActionFactory;

        public MenuActionFactoryTests()
        {
            _mockDisplayOperations = new Mock<IDisplayOperations>();
            _objectSelectionService = new Mock<IObjectSelectionService>();
            _mockGameFieldDimensions = new Mock<BoardDimensions>();

            _menuActionFactory = new MenuActionFactory(_mockDisplayOperations.Object, _objectSelectionService.Object, _mockGameFieldDimensions.Object);
        }

        [Theory]
        [InlineData(NavigationActions.START_GAME, typeof(StartNewGameAction))]
        [InlineData(NavigationActions.CHANGE_BOARD_SIZE, typeof(ChangeBoardSizeAction))]
        [InlineData(NavigationActions.EXIT_APP, typeof(ExitApplicationAction))]
        public void CreateAction_ValidInput_ReturnsExpectedActionType(string input, Type expectedType)
        {
            var action = _menuActionFactory.CreateMenuAction(input);
            Assert.IsType(expectedType, action);
        }

        [Fact]
        public void CreateAction_InvalidInput_ReturnsNull()
        {
            string actionType = "invalid action";

            var action = _menuActionFactory.CreateMenuAction(actionType);
            Assert.Null(action);
        }
    }
}