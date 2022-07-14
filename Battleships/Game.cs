using Battleships.Interfaces;
using Battleships.Models;

namespace Battleships
{
    public class Game : IGame
    {
        public IBoard Board { get; }
        public List<Ship> Ships { get; }
        public int BoardSize => Board.Size;

        public Game(int boardSize = 10, IBoard? board = null)
        {
            Board = board ?? new Board(boardSize);
            Ships = new List<Ship>();

            var battleship = new Ship(5);
            PlaceShipOnBoard(battleship);
            var destroyer1 = new Ship(4);
            PlaceShipOnBoard(destroyer1);
            var destroyer2 = new Ship(4);
            PlaceShipOnBoard(destroyer2);
        }

        private void PlaceShipOnBoard(Ship ship)
        {
            Ships.Add(ship);
            bool isFieldFree = false;
            var rand = new Random();
            while (!isFieldFree)
            {
                var isShipPlacedHorizontally = rand.Next(0, 1) == 0;
                int startX, startY, endX, endY;

                if (isShipPlacedHorizontally)
                {
                    GetPossibleShipCoordinates(out startX, out startY, out endX, out endY);
                }
                else
                {
                    GetPossibleShipCoordinates(out startY, out startX, out endY, out endX);
                }

                var xValues = Enumerable.Range(startX, endX);
                var yValues = Enumerable.Range(startY, endY);
                var fieldsToCheck = from x in xValues
                                    from y in yValues
                                    select new { X = x, Y = y };

                var consideredFields = fieldsToCheck.Select(f => Board.GetField(f.X, f.Y)).ToList();
                isFieldFree = consideredFields.All(f => f.Ship is null);
                if (isFieldFree)
                {
                    consideredFields.ForEach(f =>
                        {
                            f.Ship = ship;
                            ship.Fields.Add(f);
                        });
                }

            }

            void GetPossibleShipCoordinates(out int start1, out int start2, out int end1, out int end2)
            {
                start1 = rand.Next(0, BoardSize - ship.Length - 1);
                start2 = rand.Next(0, BoardSize - 1);
                end1 = start1 + ship.Length;
                end2 = start2;
            }
        }

        public virtual EShootResult Shoot(int x, int y)
        {
            return EShootResult.Miss;
        }

        public bool IsGameWon()
        {
            throw new NotImplementedException();
        }
    }
}
