//#define TESTLOGS


using Godot;
using System;
using System.Collections.Generic;

public class SquareSpacePartition<Value> : ISearchableSpaceContainer<Vector2, Value>
{
    [Export]
    public List<(Vector2, Value)>[,] storage;
    public Vector2 lowerLeft;
    public Vector2 dimensions;
    public int cellWidth;
    private Vector2 cellDimensions { get { return dimensions / cellWidth; } }
    public SquareSpacePartition(Vector2 lowerLeft, Vector2 dimensions, int cellWidth)
    {
        this.lowerLeft = lowerLeft;
        this.dimensions = dimensions;
        this.cellWidth = cellWidth;
        storage = new List<(Vector2, Value)>[cellWidth,cellWidth];
        for (int i = 0; i < cellWidth; i++)
        {
            for(int j = 0; j < cellWidth; j++)
            {
                storage[i, j] = new List<(Vector2, Value)>();
            }
        }
    }

    private Vector2I CellByPosition(Vector2 key)
    {
        Vector2 cellPos = (key - lowerLeft) / cellDimensions;
        Vector2I cell = (Vector2I)cellPos.Floor();
        return cell;
    }

    public void Add(Vector2 key, Value value)
    {
        Vector2I cell = CellByPosition(key);
        storage[cell.X, cell.Y].Add((key, value));
    }

    public void Remove(Vector2 key)
    {
        Vector2I cell = CellByPosition((key));
        storage[cell.X, cell.Y].RemoveAll(((Vector2, Value) a) => a.Item1 == key);

    }

    /// <summary>
    /// Not implemented yet
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Value SearchNearest(Vector2 target)
    {
        //According to the quality of comments addage, I am a terrible programmer
        /*Vector2I centerCell = CellByPosition(target);
        for(int searchRadius = 1;  searchRadius < cellWidth * 2; searchRadius++)
        {
            for(int x = -searchRadius; x <= searchRadius; x++)
            {
                for(int i = 0;)
            }

            for(int y = -searchRadius + 1; y < searchRadius; y++)
            {

            }
        }*/

        throw new NotImplementedException();
    }

    public List<Value> SearchRange(Vector2 min, Vector2 max)
    {
        List<Value> list = new List<Value>();

        Vector2I minCell = CellByPosition(min);
        Vector2I maxCell = CellByPosition(max);
        for(int x = minCell.X; x <= maxCell.X; x++)
        {
#if TESTLOGS
            UtilShit.GDPrintWithName(nameof(x), x);
#endif
            for(int y = minCell.Y; y <= maxCell.Y; y++)
            {
#if TESTLOGS
                UtilShit.GDPrintWithName(nameof(y), y);
#endif
                foreach ((Vector2,Value) element in storage[x, y])
                {
                    if(element.Item1 >= min && element.Item1 <= max)
                    {
                        list.Add(element.Item2);
                    }
                }
            }
        }

        return list;
    }
}