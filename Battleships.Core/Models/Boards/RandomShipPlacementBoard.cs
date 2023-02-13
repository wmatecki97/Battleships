using System.Collections.Generic;
using Battleships.Core.Exceptions;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Models.Boards;

/// <summary>
///     Board that places the given ships in the random places by default
/// </summary>
public class RandomShipPlacementBoard : BoardBase
{
    private readonly IRandomNumberGenerator _randomNumberGenerator;

    public RandomShipPlacementBoard(IEnumerable<IShip> ships, int size, IRandomNumberGenerator randomNumberGenerator) :
        base(ships, size)
    {
        _randomNumberGenerator = randomNumberGenerator;
        foreach (var ship in Ships)
        {
            PlaceShipOnBoardRandomly(ship);
        }
    }

    private void PlaceShipOnBoardRandomly(IShip ship)
    {
        var possibleShipCoordinates = GetAllPossibleShipCoordinates(ship);

        if (possibleShipCoordinates.Count == 0)
        {
            throw new NotEnoughPlaceOnTheBoardException();
        }

        int randomShipCoordinateIndex = _randomNumberGenerator.GetRandomNumber(0, possibleShipCoordinates.Count);
        var shipCoordinate = possibleShipCoordinates[randomShipCoordinateIndex];

        for (int x = shipCoordinate.X1; x <= shipCoordinate.X2; x++)
        {
            for (int y = shipCoordinate.Y1; y <= shipCoordinate.Y2; y++)
            {
                var field = GetField(x, y);
                field.Ship = ship;
                ship.Fields.Add(field);
            }
        }
    }

    private List<ShipCoordinate> GetAllPossibleShipCoordinates(IShip ship)
    {
        var possiblePlacements = new List<ShipCoordinate>();
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                var isHorizontalPlacementValid = true;
                var isVerticalPlacementValid = true;
                for (int s = 0; s < ship.Length; s++)
                {
                    if (y > Size - ship.Length || IsNotEmpty(x, y + s))
                    {
                        isHorizontalPlacementValid = false;
                    }

                    if (x > Size - ship.Length || IsNotEmpty(x + s, y))
                    {
                        isVerticalPlacementValid = false;
                    }

                    if (!isHorizontalPlacementValid && !isVerticalPlacementValid)
                    {
                        break;
                    }
                }

                if (isHorizontalPlacementValid)
                {
                    possiblePlacements.Add(new ShipCoordinate { X1 = x, Y1 = y, X2 = x, Y2 = y + ship.Length - 1 });
                }

                if (isVerticalPlacementValid)
                {
                    possiblePlacements.Add(new ShipCoordinate { X1 = x, Y1 = y, X2 = x + ship.Length - 1, Y2 = y });
                }
            }
        }

        return possiblePlacements;
    }

    private bool IsNotEmpty(int i, int j)
    {
        return GetField(i, j).Ship is not null;
    }
}