using Godot;
using RbTree;
using System;
using System.Collections;
using System.Collections.Generic;

public struct SwitchableValue<T>
{
    public T Value;
    public bool switchState;

    public SwitchableValue(T val, bool switchStateI)
    {
        Value = val;
        switchState = switchStateI;
    }

    public SwitchableValue(SwitchableValue<T> switchableValue, bool switchStateI)
    {
        Value = switchableValue.Value;
        switchState = switchStateI;
    }
}

/// <summary>
/// the bool in the tuple is to make range searching faster by not considering nodes that have been set false by other passes
/// </summary>
/// <typeparam name="ValueType"></typeparam>
public class Vector3Tree<ValueType> : ITree<Vector3, ValueType>
{


    ITree<float, SwitchableValue<ValueType>> xTree = new RbTree.RbTree<float,SwitchableValue<ValueType>>();
    ITree<float, SwitchableValue<ValueType>> yTree = new RbTree.RbTree<float, SwitchableValue<ValueType>>();
    ITree<float, SwitchableValue<ValueType>> zTree = new RbTree.RbTree<float, SwitchableValue<ValueType>>();

    public void AddElement(Vector3 key, ValueType value)
    {
        xTree.AddElement(key.X, new SwitchableValue<ValueType>(value,false));
        yTree.AddElement(key.Y, new SwitchableValue<ValueType>(value, false));
        zTree.AddElement(key.Z, new SwitchableValue<ValueType>(value, false));
    }

    public void RemoveValue(Vector3 key, ValueType value)
    {
        xTree.RemoveValue(key.X, new SwitchableValue<ValueType>(value, false));
        yTree.RemoveValue(key.Y, new SwitchableValue<ValueType>(value, false));
        zTree.RemoveValue(key.Z, new SwitchableValue<ValueType>(value, false));
    }

    public void RemoveKey(Vector3 key)
    {
        xTree.RemoveKey(key.X);
        yTree.RemoveKey(key.Y);
        zTree.RemoveKey(key.Z);
    }

    public void NewTreeFrom(SwitchableValue<ValueType>[] values)
    {
        throw new NotImplementedException();
    }

    public void AddElement(Vector3 key, SwitchableValue<ValueType> value)
    {
        throw new NotImplementedException();
    }

    public void RemoveValue(Vector3 key, SwitchableValue<ValueType> node)
    {
        throw new NotImplementedException();
    }

    public void OptimizeTree()
    {
        throw new NotImplementedException();
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public void NewTreeFrom(ValueType[] values)
    {
        throw new NotImplementedException();
    }

    public List<ValueType> SearchRange(Vector3 leftBound, Vector3 rightBound)
    {
        List<SwitchableValue<ValueType>> IntermediateList = new List<SwitchableValue<ValueType>>();
        xTree.SearchRange(
            leftBound.X,
            rightBound.X,
            null,
            SwitchToPredicate
            );

        yTree.SearchRange(
            leftBound.Y,
            rightBound.Y,
            (SwitchableValue<ValueType> val) => val.switchState,
            SwitchToPredicate
            );
        IntermediateList = (zTree.SearchRange(
            leftBound.Z,
            rightBound.Z,
            (SwitchableValue<ValueType> val) => val.switchState,
            SwitchToPredicate
            ));

        List<ValueType> output = new List<ValueType>(IntermediateList.Count);
        foreach (var item in IntermediateList)
        {
            output.Add(item.Value);
        }

        return output;
    }

    private SwitchableValue<ValueType> SwitchToPredicate(SwitchableValue<ValueType> val, bool pred)
    {
        return new SwitchableValue<ValueType>(val, pred);
    }

    public List<ValueType> SearchRange(Vector3 leftBound, Vector3 rightBound, Predicate<ValueType> predicate, Func<ValueType, bool, ValueType> function)
    {
        List<SwitchableValue<ValueType>> IntermediateList = new List<SwitchableValue<ValueType>>();
        xTree.SearchRange(
            leftBound.X,
            rightBound.X,
            null,
            SwitchToPredicate
            );

        yTree.SearchRange(
            leftBound.Y,
            rightBound.Y,
            (SwitchableValue<ValueType> val) => val.switchState,
            SwitchToPredicate
            );
        IntermediateList = (zTree.SearchRange(
            leftBound.Z,
            rightBound.Z,
            (SwitchableValue<ValueType> val) => val.switchState,
            SwitchToPredicate
            ));

        List<ValueType> output = new List<ValueType>(IntermediateList.Count);
        foreach (var item in IntermediateList)
        {
            if (predicate(item.Value))
            {
                output.Add(function(item.Value,true));
            }
        }

        return output;
    }

    IEnumerator<ITree<Vector3, ValueType>> IEnumerable<ITree<Vector3, ValueType>>.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}