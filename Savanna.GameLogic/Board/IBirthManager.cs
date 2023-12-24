namespace Savanna.GameLogic.Board
{
    public interface IBirthManager
    {
        void TrackProximity();

        bool CheckIfBirthIsPossible();

        void BirthNewBaby();
    }
}