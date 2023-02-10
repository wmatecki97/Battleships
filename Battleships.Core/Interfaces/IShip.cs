using System.Collections.Generic;
using Battleships.Core.Models;

namespace Battleships.Core.Interfaces;

public interface IShip
{
    List<Field> Fields { get; }

    int Length { get; }
}