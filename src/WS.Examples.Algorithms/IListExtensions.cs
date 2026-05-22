namespace WS.Examples.Algorithms;

/// <summary>
/// Provides extension methods for the <seealso cref="IList{T}"/> interface, 
/// including a <seealso cref="Pop{T}"/> method that removes and returns an 
/// element at a specified index.
/// </summary>
public static class IListExtensions
{
    /// <summary>
    /// Removes and returns the element at the specified index from the list. 
    /// This method modifies the original list by removing the element at the 
    /// given index and returns that element to the caller.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="arr">The list from which to remove the element.</param>
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <returns>The element that was removed from the list.</returns>
    public static T Pop<T>(this IList<T> arr, int index)
    {
        var item = arr[index];
        arr.RemoveAt(index);
        return item;
    }
}