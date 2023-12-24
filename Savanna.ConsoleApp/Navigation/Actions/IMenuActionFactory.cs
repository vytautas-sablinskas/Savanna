namespace Savanna.ConsoleApp.Navigation.Actions
{
    public interface IMenuActionFactory
    {
        IMenuAction CreateMenuAction(string actionToExecute);
    }
}