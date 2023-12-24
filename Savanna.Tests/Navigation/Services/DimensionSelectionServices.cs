using Moq;
using Savanna.ConsoleApp.Navigation.Services;
using Savanna.Data.Constants;
using Savanna.Data.Interfaces;

namespace NavigationTests
{
    public class DimensionSelectionServiceTests
    {
        private readonly Mock<IDisplayOperations> _mockDisplayOperations;
        private readonly DimensionSelectionService _service;

        public DimensionSelectionServiceTests()
        {
            _mockDisplayOperations = new Mock<IDisplayOperations>();
            _service = new DimensionSelectionService(_mockDisplayOperations.Object);
        }

        [Fact]
        public void ExecuteService_ValidInput_ReturnsExpectedDimension()
        {
            int expectedDimensionValue = 25;

            _mockDisplayOperations.Setup(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns(expectedDimensionValue.ToString());

            var result = _service.ExecuteService(Words.WIDTH);

            Assert.Equal(expectedDimensionValue, (int)result);
        }

        [Fact]
        public void ExecuteService_UserExits_ReturnsExitValue()
        {
            _mockDisplayOperations.Setup(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns("Q");
            _mockDisplayOperations.Setup(m => m.UserSelectedExitToMainMenu("Q")).Returns(true);

            var result = _service.ExecuteService(Words.WIDTH);

            Assert.Equal(NavigationActions.EXIT_TO_MAIN_MENU, (int)result);
        }

        [Theory]
        [InlineData("invalidNumber", "25")]
        [InlineData("", "10")]
        public void ExecuteService_InvalidInput_PromptsAgain(string invalidNumber, string validNumber)
        {
            _mockDisplayOperations.SetupSequence(m => m.PromptInput(It.IsAny<string>()))
                                          .Returns(invalidNumber)
                                          .Returns(validNumber);

            var result = _service.ExecuteService(Words.WIDTH);

            Assert.Equal(int.Parse(validNumber), (int)result);
            _mockDisplayOperations.Verify(m => m.WriteLine(Messages.INVALID_DIMENSION), Times.Once);
        }
    }
}