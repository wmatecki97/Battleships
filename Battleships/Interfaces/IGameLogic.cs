namespace Battleships.Interfaces
{
    public interface IGameLogic
    {
        bool CheckField(int x, int y);
        int BoardSize { get; }

        bool IsGameWon();
    }
}