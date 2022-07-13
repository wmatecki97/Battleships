using Battleships.Interfaces;
using Battleships.Models;

namespace Battleships
{
    public class Game : IGame
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public int BoardSize { get; set; }

        public Game(string player1Name, string player2Name, int boardSize, IBoard? player1Board = null, IBoard? player2Board = null)
        {
            Player1 = new Player
            {
                Board = player1Board ?? new Board(boardSize),
                Name = player1Name,
            };

            Player2 = new Player
            {
                Board = player2Board ?? new Board(boardSize),
                Name = player2Name,
            };
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
