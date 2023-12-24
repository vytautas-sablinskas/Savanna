using Savanna.Data.Animals;
using Savanna.Data.Board;

namespace Savanna.GameLogic.Board
{
    public class Board : IBoard
    {
        public IPrintable[,] Field { get; private set; }
        public BoardDimensions BoardDimensions { get; }

        public Board(BoardDimensions dimensions)
        {
            BoardDimensions = dimensions;
            Initialize();
        }

        public void Initialize()
        {
            Field = new IPrintable[BoardDimensions.Height, BoardDimensions.Width];
            InitializeBoardWithNoAnimals();
        }

        public void Display()
        {
            for (int row = 0; row < BoardDimensions.Height; row++)
            {
                for (int column = 0; column < BoardDimensions.Width; column++)
                {
                    Console.Write(Field[row, column].ConsoleRepresentation);
                }

                Console.WriteLine();
            }
        }

        private void InitializeBoardWithNoAnimals()
        {
            for (int row = 0; row < BoardDimensions.Height; row++)
            {
                for (int column = 0; column < BoardDimensions.Width; column++)
                {
                    Field[row, column] = new EmptySquare();
                }
            }
        }
    }
}