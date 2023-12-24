using Savanna.Data.Animals;

namespace Savanna.Data.Board
{
    public interface IBoard
    {
        BoardDimensions BoardDimensions { get; }
        IPrintable[,] Field { get; }

        void Initialize();

        void Display();
    }
}