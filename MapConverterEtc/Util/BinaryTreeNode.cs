
using Godot;

//A "Tree" is just this node and all of its children
public class BinaryTreeNode<ValueType, ElementType>
{
    public ValueType Value { get; private set; }
    public ValueType TreeAverage { get; private set; } //The average value of this node's tree
    public uint TreeCount {  get; private set; } //the number of nodes in this tree
    public ElementType Element {  get; private set; }
    

    public BinaryTreeNode<ValueType,ElementType> Parent { get; private set; }
    public BinaryTreeNode<ValueType,ElementType> LesserChild { get; private set; }
    public BinaryTreeNode<ValueType,ElementType> GreaterChild { get; private set; }

    public BinaryTreeNode(ValueType value, ElementType element)
    {
        this.Value = value;
        this.Element = element;
    }

    //public void 

    public void AddChildSimple(BinaryTreeNode<ValueType,ElementType> node)
    {
        //recursivly call add child simple until a child pointer is empty
    }

    public void AddChildBalanced()
    {
        //call AddChildBalanced recursively in children while recalculating averages for every node this touches
    }

    
}

/// <summary>
/// INCOMPLETE!!!!!!!!!!
/// Gives the path from one node to another.
/// First 4 bits store the layer of the tree that the node is at from the root node, rest of the bits store the path
/// [0 to 31] of the int is the layer information
/// [32 to uint_max] is the path
/// </summary>
public struct BinaryTreePath
{
    private uint data;
}