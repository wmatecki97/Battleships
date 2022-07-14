using Battleships.Interfaces;
using Battleships.Models;

namespace Battleships
{
    public class Game : IGame
    {
        public IBoard Board { get; }
        public List<Ship> Ships { get; }
        public int BoardSize { get; }

        public Game(int boardSize, IBoard? board = null)
        {
            Board = board ?? new Board(boardSize);
            Ships = new List<Ship>();
        }

        public EShootResult Shoot(int x, int y)
        {
            return EShootResult.Miss;
        }

        public bool IsGameWon()
        {
            throw new NotImplementedException();
        }
    }
}
