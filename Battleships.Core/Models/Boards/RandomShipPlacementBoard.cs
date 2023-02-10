using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Models.Boards;

public class RandomShipPlacementBoard : BoardBase
{
    //todo wrap to test
    private readonly Random _rand = new();

    protected RandomShipPlacementBoard(IEnumerable<IShip> ships, int size) : base(ships, size)
    {
        foreach (var ship in Ships)
        {
            PlaceShipOnBoardRandomly(ship);
        }
    }

    private void PlaceShipOnBoardRandomly(IShip ship)
    {
        bool isFieldFree = false;

        while (!isFieldFree)
        {
            var isShipPlacedVertically = _rand.Next(0, 1) == 0;

            GetRandomPossibleHorizontalPlacement(out var startX, out var startY, out var endX, out var endY, ship.Length);

            if (isShipPlacedVertically)
            {
                (startX, startY) = (startY, startX);
                (endX, endY) = (endY, endX);
            }

            var coordinates = from x in Enumerable.Range(startX, endX - startX + 1)
                from y in Enumerable.Range(startY, endY - startY + 1)
                select new { X = x, Y = y };

            var fieldsToCheck = coordinates.Select(f => GetField(f.X, f.Y)).ToList();
            isFieldFree = fieldsToCheck.All(f => f.Ship is null);

            if (isFieldFree)
            {
                fieldsToCheck.ForEach(f =>
                {
                    f.Ship = ship;
                    ship.Fields.Add(f);
                });
            }
        }
    }

    private void GetRandomPossibleHorizontalPlacement(out int x1, out int y1, out int x2, out int y2, int shipLength)
    {
        x1 = _rand.Next(0, Size - shipLength + 1); //for length 3 ship and size 4 board we can start at index 0 or 1
        y1 = _rand.Next(0, Size);
        x2 = x1 + shipLength - 1;
        y2 = y1;
    }
}