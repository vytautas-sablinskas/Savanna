using Moq;
using Savanna.ConsoleDisplay.Navigation.Services;
using Savanna.Data.Interfaces;

namespace NavigationTests
{
    public class FileSelectionServiceTests
    {
        private readonly Mock<IDisplayOperations> _mockDisplayOperations;
        private readonly FileSelectionService _service;

        public FileSelectionServiceTests()
        {
            _mockDisplayOperations = new Mock<IDisplayOperations>();
            _service = new FileSelectionService(_mockDisplayOperations.Object);
        }

        [Fact]
        public void ExecuteService_NoDLLFiles_ReturnsEmptyString()
        {
            var folderPath = "testPath";
            _mockDisplayOperations.Setup(d => d.ClearMessages()).Verifiable();
            Directory.CreateDirectory(folderPath);
            Directory.GetFiles(folderPath, "*.dll").ToList().ForEach(File.Delete);

            var result = _service.ExecuteService(folderPath);

            Assert.Equal(string.Empty, result);
            Directory.Delete(folderPath);
        }

        [Fact]
        public void GetUserChoiceFromDisplayedFiles_NoChoice_ReturnsEmptyString()
        {
            var savedFiles = new[] { "file1.dll", "file2.dll" };
            var tempFolderPath = CreateTempFolderWithFiles(savedFiles);

            _mockDisplayOperations.Setup(d => d.PromptInput(It.IsAny<string>())).Returns(string.Empty);

            var result = _service.ExecuteService(tempFolderPath);

            Assert.Equal(string.Empty, result);

            CleanupTempFolder(tempFolderPath, savedFiles);
        }

        [Fact]
        public void GetUserChoiceFromDisplayedFiles_ValidChoice_ReturnsCorrectFile()
        {
            var chosenFileNumber = "2";
            var savedFiles = new[] { "file1.dll", "file2.dll" };
            var tempFolderPath = CreateTempFolderWithFiles(savedFiles);
            var expectedResult = tempFolderPath + "\\" + savedFiles[1];

            _mockDisplayOperations.Setup(d => d.PromptInput(It.IsAny<string>())).Returns(chosenFileNumber);

            var result = _service.ExecuteService(tempFolderPath);

            Assert.Equal(expectedResult, result);

            CleanupTempFolder(tempFolderPath, savedFiles);
        }

        [Fact]
        public void GetUserChoiceFromDisplayedFiles_DisplaysAllFilesCorrectly()
        {
            var savedFiles = new[] { "file1.dll", "file2.dll" };
            var tempFolderPath = CreateTempFolderWithFiles(savedFiles);

            _mockDisplayOperations.Setup(d => d.PromptInput(It.IsAny<string>())).Returns("1"); // Making a choice to avoid infinite loop

            _service.ExecuteService(tempFolderPath);

            foreach (var file in savedFiles)
            {
                _mockDisplayOperations.Verify(d => d.WriteLine(It.Is<string>(s => s.Contains(Path.GetFileName(file)))), Times.Once());
            }

            CleanupTempFolder(tempFolderPath, savedFiles);
        }

        private string CreateTempFolderWithFiles(string[] files)
        {
            var tempFolderPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempFolderPath);

            foreach (var file in files)
            {
                File.Create(Path.Combine(tempFolderPath, file)).Dispose();
            }

            return tempFolderPath;
        }

        private void CleanupTempFolder(string folderPath, string[] files)
        {
            files.ToList().ForEach(file => File.Delete(Path.Combine(folderPath, file)));
            Directory.Delete(folderPath);
        }
    }
}