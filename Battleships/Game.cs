using Battleships.Interfaces;
using Battleships.Models;

namespace Battleships
{
    public class Game : IGame
    {
        public Dictionary<string, IBoard> Boards { get; set; }
        public int BoardSize { get; set; }

        public Game(string player1Name, string player2Name, int boardSize, IBoard? player1Board = null, IBoard? player2Board = null)
        {
            Boards = new Dictionary<string, IBoard>()
            {
                [player1Name] = player1Board ?? new Board(boardSize),
                [player2Name] = player2Board ?? new Board(boardSize),
            };
            BoardSize = boardSize;
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
