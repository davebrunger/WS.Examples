namespace WS.Examples.Algorithms;
/// <summary>
/// Represents a node in a tree; exposes the node value and its children and
/// supports depth-first traversal via <see cref="DepthFirstSearch"/>.
/// </summary>
/// <typeparam name="T">The type of the node value.</typeparam>
public interface ITreeNode<T> : IEnumerable<T>
{
    /// <summary>
    /// Gets the value stored in the node.
    /// </summary>
    T Value { get; }

    /// <summary>
    /// Gets the immediate children of this node.
    /// </summary>
    IEnumerable<ITreeNode<T>> Children { get; }

    /// <summary>
    /// Returns an enumerator that iterates this node and its descendants in depth-first order,
    /// yielding the node values (<typeparamref name="T"/>).
    /// </summary>
    /// <returns>An <see cref="IEnumerator{T}"/> that iterates the node values.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        yield return Value;

        foreach (var child in Children)
        {
            foreach (var descendant in child)
            {
                yield return descendant;
            }
        }
    }

    /// <summary>
    /// Explicit non-generic enumerator implementation forwarding to the generic enumerator.
    /// </summary>
    /// <returns>An <see cref="IEnumerator"/> for the node values.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Performs a depth-first search starting at this node and returns the first node
    /// for which <paramref name="predicate"/> returns <c>true</c>.
    /// </summary>
    /// <param name="predicate">A function to test node values.</param>
    /// <returns>An <see cref="Option{T}"/> containing the matching node value, or <c>None</c> if none found.</returns>
    public Option<T> DepthFirstSearch(Func<T, bool> predicate)
    {
        if (predicate(Value))
        {
            return Some(Value);
        }

        foreach (var child in Children)
        {
            var result = child.DepthFirstSearch(predicate);
            if (result.IsSome)
            {
                return result;
            }
        }

        return None;
    }
}

/// <summary>
/// A binary tree node implementation of <see cref="ITreeNode{T}"/> with optional left/right children.
/// </summary>
/// <typeparam name="T">The type of the node value.</typeparam>
public class BinaryTreeNode<T> : ITreeNode<T>
{
    /// <summary>
    /// The value stored in the node.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Optional left child.
    /// </summary>
    public Option<BinaryTreeNode<T>> Left { get; }

    /// <summary>
    /// Optional right child.
    /// </summary>
    public Option<BinaryTreeNode<T>> Right { get; }

    /// <summary>
    /// Yields the non-empty children as <see cref="ITreeNode{T}"/> instances.
    /// </summary>
    public IEnumerable<ITreeNode<T>> Children
    {
        get
        {
            var(leftIsSome, left) = Left.Match(some => (true, some), () => (false, default!));
            if (leftIsSome)
            {
                yield return left;
            }
            var (rightIsSome, right) = Right.Match(some => (true, some), () => (false, default!));
            if (rightIsSome)
            {
                yield return right;
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="BinaryTreeNode{T}"/>.
    /// </summary>
    /// <param name="value">The node value.</param>
    /// <param name="left">Optional left child.</param>
    /// <param name="right">Optional right child.</param>
    public BinaryTreeNode(T value, Option<BinaryTreeNode<T>> left, Option<BinaryTreeNode<T>> right)
    {
        Value = value;
        Left = left;
        Right = right;
    }
}