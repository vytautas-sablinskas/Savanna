using Savanna.Data.Interfaces;

namespace Savanna.ConsoleApp.Navigation.Actions
{
    public class StartNewGameAction : IMenuAction
    {
        private readonly IGameEngine _gameEngine;

        public StartNewGameAction(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void Execute()
        {
            _gameEngine.Start();
        }
    }
}