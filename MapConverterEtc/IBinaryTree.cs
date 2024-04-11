using System.Collections.Generic;

/// <summary>
/// A node contains the element, the parent ref, and the children refs.
/// An element is the data held in a node
/// 
/// </summary>
/// <typeparam name="ElementType"></typeparam>
public interface IBinaryTree<ElementType, NodeType>
{
    public void NewTreeFrom(ElementType[] elements);
    public void OptimizeTree();
    public NodeType AddElement(ElementType element);
    public void RemoveNode(NodeType node);
    /// <summary>
    /// Search for the node with the element closest to target
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public NodeType SearchNearestNode(ElementType target);
    /// <summary>
    /// Search for nodes within a range of values specified by the center and radius
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <returns></returns> 
    public List<NodeType> SearchRange(ElementType center, ElementType radius);

}
