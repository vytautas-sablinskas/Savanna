using Moq;
using Savanna.ConsoleApp.Navigation.Actions;
using Savanna.Data.Interfaces;

namespace NavigationTests
{
    public class StartNewGameActionTests
    {
        private readonly Mock<IGameEngine> _game;
        private readonly StartNewGameAction _startNewGameAction;

        public StartNewGameActionTests()
        {
            _game = new Mock<IGameEngine>();
            _startNewGameAction = new StartNewGameAction(_game.Object);
        }

        [Fact]
        public void Execute_ShouldStartGame()
        {
            _startNewGameAction.Execute();

            _game.Verify(g => g.Start(), Times.Once());
        }
    }
}