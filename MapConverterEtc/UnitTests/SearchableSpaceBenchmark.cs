using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class SearchableSpaceBenchmark : Node
{
    [Export] public Vector2 minGen; 
    [Export] public Vector2 maxGen;

    [Export] public Vector2 minSearch;
    [Export] public Vector2 maxSearch;

    [Export]
    public int count;

    [Export]
    public int squarePartitionCellWidth;

    
    private List<Vector2> list;
    private SquareSpacePartition<Vector2> squarePartition;

    public override void _EnterTree()
    {
        squarePartition = new SquareSpacePartition<Vector2>(minGen, maxGen - minGen, squarePartitionCellWidth);
        list = new List<Vector2>();

        base._EnterTree();

        RandomNumberGenerator rng = new RandomNumberGenerator();

        Stopwatch stopwatch = new Stopwatch();

        TimeSpan listAddTime = new TimeSpan(0);
        TimeSpan partitionAddTime = new TimeSpan(0);

        TimeSpan listSearchTime = new TimeSpan(0);
        TimeSpan partitionSearchTime = new TimeSpan(0);


        for (int i = 0; i < count; i++)
        {
            Vector2 randPoint = RandomVec2(rng,minGen,maxGen);

            stopwatch.Restart();
            list.Add(randPoint);
            listAddTime += stopwatch.Elapsed;

            stopwatch.Restart();
            squarePartition.Add(randPoint,randPoint);
            partitionAddTime += stopwatch.Elapsed;
        }

        GDPrintWithName("listCount", list.Count);
        GDPrintWithName(nameof(listAddTime), listAddTime.TotalMilliseconds);
        //GDPrintWithName("partitionCount");
        GDPrintWithName(nameof(partitionAddTime), partitionAddTime.TotalMilliseconds);

        List<Vector2> partitionSearchResult;

        stopwatch.Restart();
        partitionSearchResult = squarePartition.SearchRange(minSearch, maxSearch);
        partitionSearchTime = stopwatch.Elapsed;

        stopwatch.Restart();
        List<Vector2> listSearchResult = new List<Vector2>();
        /*foreach(Vector2 v in list)
        {
            if (v < minSearch || v > maxSearch) continue;
            if(v <= maxSearch && v >= minSearch) listSearchResult.Add(v);
        }*/

        for(int i = 0;i < list.Count;i++)
        {
            //if (list[i] < maxSearch && list[i] > minSearch) listSearchResult.Add(list[i]);
            if (UtilShit.AGreatEqualB(maxSearch, list[i]) && UtilShit.AGreatEqualB(list[i], minSearch)) listSearchResult.Add(list[i]);
        }
        listSearchTime = stopwatch.Elapsed;

        GDPrintWithName(nameof(partitionSearchTime), partitionSearchTime.TotalMilliseconds);
        GDPrintWithName(nameof(listSearchTime), listSearchTime.TotalMilliseconds);

        /*foreach(Vector2 v in list)
        {
            GDPrintWithName(nameof(list), v);
        }

        for(int x = 0; x < squarePartition.cellWidth; x++)
        {
            for(int y = 0; y < squarePartition.cellWidth; y++)
            {
                for(int i = 0; i < squarePartition.storage[x,y].Count; i++)
                {
                    GDPrintWithName(nameof(squarePartition), squarePartition.storage[x, y][i]);
                }
            }
        }*/
        GDPrintWithName(nameof(partitionSearchResult), partitionSearchResult.Count);
        GDPrintWithName(nameof(listSearchResult), listSearchResult.Count);
    }

    public Vector2 RandomVec2(RandomNumberGenerator rng, Vector2 min, Vector2 max)
    {
        return new Vector2(rng.RandfRange(min.X, max.X), rng.RandfRange(min.Y, max.Y));
    }

    private void GDPrintWithName(string name, object obj)
    {
        GD.Print($"{name}: {obj}");
    }
}
