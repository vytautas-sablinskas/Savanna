namespace Savanna.Data.Interfaces
{
    public interface IGameInputListener
    {
        event Action StopGameRequested;

        void StartListening();
    }
}