using System;
using System.Collections.Generic;

/// <summary>
/// A node contains the element, the parent ref, and the children refs.
/// An element is the data held in a node
/// 
/// </summary>
/// <typeparam name="ValueType"></typeparam>
public interface ITree<KeyType, ValueType>: IEnumerable<ITree<KeyType, ValueType>>
{
    public void NewTreeFrom(ValueType[] values);
    public void OptimizeTree();
    public void AddElement(KeyType key, ValueType value);
    public void RemoveValue(KeyType key, ValueType value);
    public void RemoveKey(KeyType key);
    /// <summary>
    /// Search for the node with the element closest to target
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    //public NodeType SearchNearestNode(ElementType target);
    /// <summary>
    /// Search for nodes within a range of values specified by the center and radius
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <returns></returns> 
    public List<ValueType> SearchRange(KeyType leftBound, KeyType rightBound);

    /// <summary>
    /// return the value if the predicate is true, then modify the value in the tree with function. Predicate implicitly always includes "isInRange"
    /// the bool in the function is the predicate return
    /// </summary>
    /// <param name="leftBound"></param>
    /// <param name="rightBound"></param>
    /// <param name="predicate"></param>
    /// <param name="function"></param>
    /// <returns></returns>
    public List<ValueType> SearchRange(KeyType leftBound, KeyType rightBound, Predicate<ValueType> predicate, Func<ValueType, bool, ValueType> function);

}
