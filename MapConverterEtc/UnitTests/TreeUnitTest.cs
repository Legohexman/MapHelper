using Godot;
using RbTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class TreeUnitTest : Node
{
    [Export] public float randRangeMin1d;
    [Export] public float randRangeMax1d;

    [Export] public Vector3 randRangeMin3d;
    [Export] public Vector3 randRangeMax3d;
    
    [Export] public float searchLeft1d;
    [Export] public float searchRight1d;

    [Export] public Vector3 searchLeft3d;
    [Export] public Vector3 searchRight3d;

    [Export] public int numItems;

    public RbTree<float, float> Tree1d;
    public List<float> array1d;

    public Vector3Tree<int> Tree3d;
    public List<Vector3> array3d;


    public override void _EnterTree()
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        Stopwatch timer = new Stopwatch();
        timer.Reset();
        
        TimeSpan addAllElementsToArray1d = new TimeSpan(0);
        TimeSpan longestArrayAddition1d = new TimeSpan(0);

        TimeSpan addAllElementsToTree1d = new TimeSpan(0);
        TimeSpan longestTreeAddition1d = new TimeSpan(0);

        TimeSpan addAllElementsToArray3d = new TimeSpan(0);
        TimeSpan longestArrayAddition3d = new TimeSpan(0);

        TimeSpan addAllElementsToTree3d = new TimeSpan(0);
        TimeSpan longestTreeAddition3d = new TimeSpan(0);

        Tree1d = new RbTree<float, float>();
        Tree3d = new Vector3Tree<int>();
        array1d = new List<float>();
        array3d = new List<Vector3>();

        TimeSpan elapsedTimeCache;

        for (int i = 0; i < numItems; i++)
        {


            float randF = rng.RandfRange(randRangeMin1d, randRangeMax1d);

            timer.Restart();

            //array1d.Add(randF);

            elapsedTimeCache = timer.Elapsed;
            addAllElementsToArray1d += elapsedTimeCache;


            timer.Restart();

            //Tree1d.AddElement(randF, randF);

            elapsedTimeCache = timer.Elapsed;
            addAllElementsToTree1d += elapsedTimeCache;





            Vector3 randV = new Vector3(
                rng.RandfRange(randRangeMin3d.X, randRangeMax3d.X),
                rng.RandfRange(randRangeMin3d.Y, randRangeMax3d.Y),
                rng.RandfRange(randRangeMin3d.Z, randRangeMax3d.Z)
                );



            timer.Restart();

            array3d.Add(randV);

            timer.Stop();

            elapsedTimeCache = timer.Elapsed;
            addAllElementsToArray3d += elapsedTimeCache;

            timer.Reset();



            timer.Restart();

            Tree3d.AddElement(randV, i);

            elapsedTimeCache = timer.Elapsed;
            addAllElementsToTree3d += elapsedTimeCache;
        }

        GD.Print($"AddAllElements to float array: {addAllElementsToArray1d.TotalMilliseconds}");
        GD.Print($"LongestAddition to float array: {longestArrayAddition1d.TotalMilliseconds}");
        GD.Print($"AddAllElements to float tree: {addAllElementsToTree1d.TotalMilliseconds}");
        GD.Print($"LongestAddition to float tree: {longestTreeAddition1d.TotalMilliseconds}");

        GD.Print($"AddAllElements to vectore array: {addAllElementsToArray3d.TotalMilliseconds}");
        GD.Print($"LongestAddition to vector array: {longestArrayAddition3d.TotalMilliseconds}");
        GD.Print($"AddAllElements to vector tree: {addAllElementsToTree3d.TotalMilliseconds}");
        GD.Print($"LongestAddition to vector tree: {longestTreeAddition3d.TotalMilliseconds}");

        Console.WriteLine("pogger");

        timer.Restart();
        
        //var search1doutput = Tree1d.SearchRange(searchLeft1d, searchRight1d);
        var search1doutput = Tree3d.SearchRange(searchLeft3d, searchRight3d);

        elapsedTimeCache = timer.Elapsed;
        
        
        //GDPrintWithName(elapsedTimeCache.TotalMilliseconds, "SearchRangeTree1d");
        GDPrintWithName(elapsedTimeCache.TotalMilliseconds, "SearchRangeTree3d");




        timer.Restart();
        
        /*List<float> valuesInRange1d = new List<float>();
        for(int i = 0; i < numItems; i++)
        {
            if (array1d[i] > searchLeft1d && array1d[i] < searchRight1d)
            {
                valuesInRange1d.Add(array1d[i]);
            }
        }*/
        
        List<Vector3> valuesInRange3d = new List<Vector3>();
        for(int i = 0; i < numItems; i++)
        {
            if (array3d[i] > searchLeft3d && array3d[i] < searchRight3d)
            {
                valuesInRange3d.Add(array3d[i]);
            }
        }

        elapsedTimeCache = timer.Elapsed;
        //GDPrintWithName(elapsedTimeCache.TotalMilliseconds, "SearchRangeArray1d");
        GDPrintWithName(elapsedTimeCache.TotalMilliseconds, "SearchRangeArray3d");
        

        //GD.Print($"ArrayREsultCount: {valuesInRange1d.Count}, TreeRsultCount: {search1doutput.Count}");
        GD.Print($"ArrayREsultCount: {valuesInRange3d.Count}, TreeRsultCount: {search1doutput.Count}");
        /*foreach( var item in search1doutput)
        {
            GD.Print($"Tree search output: {item}");
        }

        foreach(var item in valuesInRange1d)
        {
            GD.Print($"List search output: {item}");
        }*/
    }

    private void StartStopwatch(Stopwatch timer)
    {
        timer.Start();
    }

    private TimeSpan timeFunction(Stopwatch timer, System.Action action)
    {
        timer.Reset();
        timer.Start();

        action();

        timer.Stop();

        return timer.Elapsed;
    }

    private TimeSpan returnLarger(TimeSpan ts1, TimeSpan ts2)
    {
        return ts1 > ts2 ? ts1 : ts2;
    }

    private void GDPrintWithName(object obj, string name)
    {
        GD.Print($"{name}: {obj}");
    }
}
