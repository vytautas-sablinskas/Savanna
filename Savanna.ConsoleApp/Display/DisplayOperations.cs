using Savanna.Data.Interfaces;
using System.Text;

namespace Savanna.ConsoleApp.Display
{
    public class DisplayOperations : IDisplayOperations
    {
        public virtual string PromptInput(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }

        public virtual void ClearMessages() => Console.Clear();

        public void Write(string message = "") => Console.Write(message);

        public void WriteLine(string message = "") => Console.WriteLine(message);

        public string ReadLine() => Console.ReadLine();

        public bool UserSelectedExitToMainMenu(string userInput) =>
            userInput.Equals("Q", StringComparison.OrdinalIgnoreCase);

        public string FormatMessage(string message)
        {
            var sb = new StringBuilder();

            int width = message.Length + 2;
            sb.Append(RepeatCharacter('-', width) + "\n");
            sb.Append($"|{message}|\n");
            sb.Append(RepeatCharacter('-', width));

            return sb.ToString();
        }

        public string RepeatCharacter(char character, int times)
        {
            return new string(character, times);
        }

        public ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);
    }
}