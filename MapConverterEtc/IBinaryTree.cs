/// <summary>
/// A node contains the element, the parent ref, and the children refs
/// An element is the data held in a node
/// 
/// </summary>
/// <typeparam name="ElementType"></typeparam>
public interface IBinaryTree<ElementType>
{
    public void SearchRange(ElementType center, ElementType radius);
    public void AddElement(ElementType element);
    public void 
}
