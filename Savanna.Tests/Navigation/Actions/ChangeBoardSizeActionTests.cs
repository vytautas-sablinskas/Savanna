using Moq;
using Savanna.ConsoleApp.Navigation.Actions;
using Savanna.ConsoleApp.Navigation.Services;
using Savanna.Data.Board;
using Savanna.Data.Constants;
using Savanna.Data.Interfaces;

namespace NavigationTests
{
    public class ChangeBoardSizeActionTests
    {
        private readonly BoardDimensions _mockGameFieldDimensions;
        private readonly Mock<IObjectSelectionService> _mockDimensionSelectionService;
        private readonly Mock<IDisplayOperations> _displayOperations;
        private readonly ChangeBoardSizeAction _action;

        public ChangeBoardSizeActionTests()
        {
            _mockGameFieldDimensions = new BoardDimensions();
            _mockDimensionSelectionService = new Mock<IObjectSelectionService>();
            _displayOperations = new Mock<IDisplayOperations>();

            _mockGameFieldDimensions.Height = 0;
            _mockGameFieldDimensions.Width = 0;

            _action = new ChangeBoardSizeAction(_mockGameFieldDimensions, _mockDimensionSelectionService.Object, _displayOperations.Object);
        }

        [Fact]
        public void Execute_ValidDimensions_ChangesDimensionsAndDisplaysMessage()
        {
            var width = 10;
            var height = 20;
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.WIDTH.ToLower())).Returns(width);
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.HEIGHT.ToLower())).Returns(height);

            _action.Execute();

            Assert.True(_mockGameFieldDimensions.Height == height);
            Assert.True(_mockGameFieldDimensions.Width == width);
            _displayOperations.Verify(d => d.WriteLine(Messages.SuccessfulDimensionChange(height, width)), Times.Once);
        }

        [Fact]
        public void Execute_ExitBeforeWidth_DoesNotChangeDimensionsOrDisplayMessage()
        {
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(It.IsAny<string>())).Returns(-1);

            _action.Execute();

            _displayOperations.Verify(d => d.WriteLine(Messages.SuccessfulDimensionChange(It.IsAny<int>(), It.IsAny<int>())), Times.Never);
        }

        [Fact]
        public void Execute_ExitBeforeHeight_DoesNotChangeDimensionsOrDisplayMessage()
        {
            var width = 10;
            var height = NavigationActions.EXIT_TO_MAIN_MENU;
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.WIDTH.ToLower())).Returns(width);
            _mockDimensionSelectionService.Setup(s => s.ExecuteService(Words.HEIGHT.ToLower())).Returns(height);

            _action.Execute();

            Assert.True(_mockGameFieldDimensions.Height != height);
            Assert.True(_mockGameFieldDimensions.Width != width);
            _displayOperations.Verify(d => d.WriteLine(Messages.SuccessfulDimensionChange(It.IsAny<int>(), It.IsAny<int>())), Times.Never);
        }
    }
}