namespace Savanna.Data.Interfaces
{
    public interface IDisplayOperations
    {
        string PromptInput(string message);

        void ClearMessages();

        void Write(string message);

        void WriteLine(string message);

        string ReadLine();

        bool UserSelectedExitToMainMenu(string input);

        ConsoleKeyInfo ReadKey(bool intercept);

        string FormatMessage(string message);

        string RepeatCharacter(char character, int times);
    }
}