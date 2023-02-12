using System.Collections.Generic;
using Battleships.Core.Exceptions;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Models.Boards;

/// <summary>
/// Board that places the given ships in the random places by default
/// </summary>
public class RandomShipPlacementBoard : BoardBase
{
    private readonly IRandomNumberGenerator _randomNumberGenerator;

    public RandomShipPlacementBoard(IEnumerable<IShip> ships, int size, IRandomNumberGenerator randomNumberGenerator) : base(ships, size)
    {
        _randomNumberGenerator = randomNumberGenerator;
        foreach (var ship in Ships)
        {
            PlaceShipOnBoardRandomly(ship);
        }
    }

    private void PlaceShipOnBoardRandomly(IShip ship)
    {
        var possiblePlacements = GetAllPossiblePlacementsOfShip(ship);

        if (possiblePlacements.Count == 0)
        {
            throw new NotEnoughPlaceOnTheBoardException();
        }

        var randomPlacementId = _randomNumberGenerator.GetRandomNumber(0, possiblePlacements.Count);
        var selectedPlacement = possiblePlacements[randomPlacementId];

        for (int x = selectedPlacement.X1; x <= selectedPlacement.X2; x++)
        {
            for (int y = selectedPlacement.Y1; y <= selectedPlacement.Y2; y++)
            {
                var field = GetField(x, y);
                field.Ship =ship;
                ship.Fields.Add(field);
            }
        }
    }

    private List<Placement> GetAllPossiblePlacementsOfShip(IShip ship)
    {
        var possiblePlacements = new List<Placement>();
        for (int x = 0; x < Size - ship.Length + 1; x++)
        {
            for (int y = 0; y < Size - ship.Length + 1; y++)
            {
                bool isHorizontalPlacementValid = true;
                bool isVerticalPlacementValid = true;
                for (int s = 0; s < ship.Length; s++)
                {
                    if (!IsEmpty(x, y + s))
                    {
                        isHorizontalPlacementValid = false;
                    }

                    if (!IsEmpty(x + s, y))
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
                    possiblePlacements.Add(new Placement { X1 = x, Y1 = y, X2 = x, Y2 = y + ship.Length - 1 });
                }

                if (isVerticalPlacementValid)
                {
                    possiblePlacements.Add(new Placement { X1 = x, Y1 = y, X2 = x + ship.Length - 1, Y2 = y });
                }
            }
        }

        return possiblePlacements;
    }

    private bool IsEmpty(int i, int j)
    {
        return GetField(i,j).Ship is null;
    }
}