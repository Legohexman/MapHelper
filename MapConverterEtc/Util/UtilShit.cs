using Godot;
using System;
using System.Collections.Generic;

public static class UtilShit
{
    public static void ListAddQuantity<T>(List<T> list, T obj, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            list.Add(obj);
        }
    }

    public static void GDPrintWithName(string name, object obj)
    {
        GD.Print($"{name}: {obj}");
    }

    public static bool AGreatB(Vector2 a, Vector2 b)
    {
        return a.X > b.X && a.Y > b.Y;
    }

    public static bool AGreatEqualB(Vector2 a, Vector2 b)
    {
        return a.X >= b.X && a.Y >= b.Y;
    }
}

/// <summary>
/// A list of lists of the same length
/// </summary>
/// <typeparam name=""></typeparam>
public class MultiList<T>
{
    public T defaultObj;
    private List<List<T>> lists;
    public int Length { get { return lists[0].Count; } }

    public MultiList(int listAmount, int listLength, T defaultObjI)
    {
        lists = new List<List<T>>();
        defaultObj = defaultObjI;
        IncreaseListAmount(listAmount, defaultObjI);
        IncreaseListLength(listLength, defaultObjI);
    }



    public void IncreaseListLength(int count, T obj)
    {
        foreach(var list in lists)
        {
            UtilShit.ListAddQuantity(list, obj, count);
        }
    }
    
    public void IncreaseListLength(int count)
    {
        foreach (var list in lists)
        {
            UtilShit.ListAddQuantity(list, defaultObj, count);
        }
    }

    public void IncreaseListAmount(int amount, T obj)
    {
        for(int i = 0; i < amount; i++)
        {
            lists.Add(new List<T>());
            UtilShit.ListAddQuantity(lists[lists.Count - 1], obj, amount);
        }
    }
    
    public void IncreaseListAmount(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            lists.Add(new List<T>());
            UtilShit.ListAddQuantity(lists[lists.Count - 1], defaultObj, amount);
        }
    }

    public void RemoveElementAt(int index)
    {
        foreach(var list in lists)
        {
            list.RemoveAt(index);
        }


    }
}