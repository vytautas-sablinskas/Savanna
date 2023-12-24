namespace Savanna.Data.Constants
{
    public class Messages
    {
        public const string MENU_INFORMATION = "1. Start Game\n" +
                                               "2. Change board size\n" +
                                               "3. Exit app.";

        public const string INVALID_ACTION = "Invalid action was chosen. Try again.";

        public const string INVALID_DIMENSION = "Invalid dimension was chosen. Try again.";

        public static string SuccessfulDimensionChange(int height, int width)
        {
            return $"Changed to new dimensions: {height}x{width}";
        }

        public static string SelectDimension(string dimensionName)
        {
            return $"Select {dimensionName} with a number between {BoardSizeLimits.SMALLEST_BOARD_DIMENSION}-{BoardSizeLimits.LARGEST_BOARD_DIMENSION} (Write Q/q to exit):";
        }
    }
}