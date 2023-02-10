using System.Collections.Generic;
using System.Linq;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Models.Boards;

public abstract class BoardBase : IBoard
{
    protected BoardBase(IEnumerable<IShip> ships, int size)
    {
        Ships = ships;
        Size = size;
        var fieldsCount = size * size;
        Fields = Enumerable.Range(0, fieldsCount).Select(_ => new Field()).ToArray();
    }

    public Field[] Fields { get; }

    public IEnumerable<IShip> Ships { get; }

    public int Size { get; }

    public Field GetField(int x, int y)
    {
        return Fields[x * Size + y];
    }
}