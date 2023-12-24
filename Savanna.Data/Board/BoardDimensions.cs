using Savanna.Data.Constants;

namespace Savanna.Data.Board
{
    public class BoardDimensions
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public BoardDimensions()
        {
            Height = DefaultBoardDimensions.HEIGHT;
            Width = DefaultBoardDimensions.WIDTH;
        }

        public BoardDimensions(int height, int width)
        {
            if (height < BoardSizeLimits.SMALLEST_BOARD_DIMENSION || height > BoardSizeLimits.LARGEST_BOARD_DIMENSION)
                throw new ArgumentException(nameof(height));

            if (width < BoardSizeLimits.SMALLEST_BOARD_DIMENSION || width > BoardSizeLimits.LARGEST_BOARD_DIMENSION)
                throw new ArgumentException(nameof(width));

            Height = height;
            Width = width;
        }
    }
}