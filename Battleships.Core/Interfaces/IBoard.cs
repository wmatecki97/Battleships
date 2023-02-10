﻿using System.Collections.Generic;
using Battleships.Core.Models;

namespace Battleships.Core.Interfaces;

public interface IBoard
{
    int Size { get; }

    Field[] Fields { get; }

    IEnumerable<IShip> Ships { get; }

    Field GetField(int x, int y);
}