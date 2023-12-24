namespace Savanna.ConsoleApp.Navigation.Actions
{
    public class ExitApplicationAction : IMenuAction
    {
        public void Execute() => Environment.Exit(0);
    }
}