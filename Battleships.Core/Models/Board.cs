using System.Linq;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Models;

public sealed class Board : IBoard
{
    public Board(int size)
    {
        int fieldsCount = size * size;
        Fields = Enumerable.Range(0, fieldsCount).Select(_ => new Field()).ToArray();
        Size = size;
    }

    public Field[] Fields { get; }

    public int Size { get; }

    public Field GetField(int x, int y)
    {
        return Fields[x * Size + y];
    }
}