using Savanna.ConsoleApp.Navigation.Services;
using Savanna.Data.Interfaces;

namespace Savanna.ConsoleDisplay.Navigation.Services
{
    public class FileSelectionService : IObjectSelectionService
    {
        private readonly IDisplayOperations _displayOperations;

        public FileSelectionService(IDisplayOperations displayOperations)
        {
            _displayOperations = displayOperations;
        }

        public object ExecuteService(object pathToFolder)
        {
            var folderPath = (string)pathToFolder;

            return SelectAndReadSavedGame(folderPath);
        }

        private string SelectAndReadSavedGame(string folderPath)
        {
            _displayOperations.ClearMessages();

            var savedFiles = ListSavedDLLFiles(folderPath);
            if (!savedFiles.Any())
            {
                return string.Empty;
            }

            return GetUserChoiceFromDisplayedFiles(savedFiles.Length, savedFiles);
        }

        private string GetUserChoiceFromDisplayedFiles(int maxChoice, string[] savedFiles)
        {
            while (true)
            {
                _displayOperations.ClearMessages();
                DisplayDLLFiles(savedFiles);

                var input = _displayOperations.PromptInput("Enter the number of your choice: ");
                if (string.IsNullOrEmpty(input))
                    return string.Empty;

                var isValidNumber = int.TryParse(input, out var chosenNumber) && chosenNumber > 0 && chosenNumber <= maxChoice;
                if (!isValidNumber)
                {
                    _displayOperations.WriteLine($"Choice '{chosenNumber}' is invalid, try again or press enter to continue without loading file");
                    continue;
                }

                return savedFiles[chosenNumber - 1];
            }
        }

        private string[] ListSavedDLLFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath, "*.dll");
        }

        private void DisplayDLLFiles(string[] savedFiles)
        {
            _displayOperations.WriteLine("Please choose DLL file to load (Press enter if you don't want to load any file)");
            for (int i = 0; i < savedFiles.Length; i++)
            {
                _displayOperations.WriteLine($"{i + 1}. {Path.GetFileName(savedFiles[i])}");
            }
        }
    }
}