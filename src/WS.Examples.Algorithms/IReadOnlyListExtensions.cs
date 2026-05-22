namespace WS.Examples.Algorithms;

/// <summary>
/// Provides extension methods for <see cref="IReadOnlyList{T}"/> such as searching and sorting algorithms.
/// </summary>
public static class IReadOnlyListExtensions
{
    /// <summary>
    /// Performs a binary search on a sorted <seealso cref="IReadOnlyList{T}"/> to find the index of a specified item.
    /// The method returns an  <seealso cref="Option{T}"/> containing the index of the item if found, or <seealso cref="Option{T}.None"/> if 
    /// the item is not present in the list. The binary search algorithm works by repeatedly dividing 
    /// the search interval in half, comparing the target item to the middle element of the current 
    /// interval, and adjusting the search range accordingly until the item is found or the search 
    /// range is empty. This method assumes that the input list is sorted in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="arr">The sorted list to search.</param>
    /// <param name="item">The item to search for.</param>
    /// <returns>An <seealso cref="Option{T}"/> containing the index of the item if found, or <seealso cref="Option{T}.None"/> if not found.</returns>
    public static Option<int> BinarySearch<T>(this IReadOnlyList<T> arr, T item) where T : IComparable<T>
    {
        var low = 0;
        var high = arr.Count - 1;

        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            var guess = arr[mid];

            if (guess == item)    
            {
                return Some(mid);
            }
            else if (guess > item)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return None;
    }

    /// <summary>
    /// Sorts the elements of an <seealso cref="IReadOnlyList{T}"/> using the selection sort algorithm and returns a new sorted list.
    /// The selection sort algorithm works by repeatedly selecting the smallest unsorted element and swapping it with the first unsorted element 
    /// until the entire list is sorted. This method creates a new list to hold the sorted elements,
    /// leaving the original list unchanged.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="arr">The list to sort.</param>
    /// <returns>A new list containing the sorted elements.</returns>
    public static List<T> SelectionSort<T>(this IReadOnlyList<T> arr) where T : IComparable<T>
    {
        static int FindSmallest(IReadOnlyList<T> list)
        {
            var smallestIndex = 0;

            foreach (var i in new Range(1, list.Count))
            {
                if (list[i] < list[smallestIndex])
                {
                    smallestIndex = i;
                }
            }

            return smallestIndex;
        }

        var newArr = new List<T>(arr.Count);
        var copiedArr = new List<T>(arr);

        foreach (var i in new Range(arr.Count))
        {
            var smallest = FindSmallest(copiedArr);
            newArr.Add(copiedArr.Pop(smallest));
        }

        return newArr;
    }
}