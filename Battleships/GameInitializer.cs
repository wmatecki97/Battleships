using Battleships.Interfaces;
using Battleships.Models;

namespace Battleships
{
    internal class RandomGameInitializer : IGameInitializer
    {
        public void Initialize(IGame game)
        {
            var battleship = new Ship(5);
            PlaceShipOnBoard(battleship, game);
            var destroyer1 = new Ship(4);
            PlaceShipOnBoard(destroyer1, game);
            var destroyer2 = new Ship(4);
            PlaceShipOnBoard(destroyer2, game);
        }

        private void PlaceShipOnBoard(Ship ship, IGame game)
        {
            game.Ships.Add(ship);
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

                var xValues = GetNumbersInRange(startX, endX);
                var yValues = GetNumbersInRange(startY, endY);
                var fieldsToCheck = from x in xValues
                                    from y in yValues
                                    select new { X = x, Y = y };

                var consideredFields = fieldsToCheck.Select(f => game.Board.GetField(f.X, f.Y)).ToList();
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

            IEnumerable<int> GetNumbersInRange(int start, int end)
            {
                if (start == end)
                {
                    yield return start;
                }

                foreach (var number in Enumerable.Range(start, end - start))
                {
                    yield return number;
                }
            }

            void GetPossibleShipCoordinates(out int start1, out int start2, out int end1, out int end2)
            {
                start1 = rand.Next(0, game.Board.Size - ship.Length - 1);
                start2 = rand.Next(0, game.Board.Size - 1);
                end1 = start1 + ship.Length;
                end2 = start2;
            }
        }
    }
}
