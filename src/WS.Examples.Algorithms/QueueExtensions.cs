namespace WS.Examples.Algorithms;

/// <summary>
/// Provides helper extensions for <see cref="Queue{T}"/>.
/// </summary>
public static class QueueExtensions
{
    /// <summary>
    /// Placeholder generic extension container (keeps original structure).
    /// </summary>
    /// <typeparam name="T">The element type of the queue.</typeparam>
    extension<T>(Queue<T> _)
    {
        /// <summary>
        /// Enqueues all items from <paramref name="items"/> into <paramref name="queue"/>.
        /// </summary>
        /// <param name="queue">The queue to which items will be enqueued.</param>
        /// <param name="items">The sequence of items to enqueue.</param>
        /// <returns>The same <paramref name="queue"/> instance.</returns>
        public static Queue<T> operator +(Queue<T> queue, IEnumerable<T> items)
        {   
            foreach (var item in items)
            {
                queue.Enqueue(item);
            }
            return queue;
        }
        
        /// <summary>
        /// Enqueues a single <paramref name="item"/> into <paramref name="queue"/>.
        /// </summary>
        /// <param name="queue">The queue to which the item will be enqueued.</param>
        /// <param name="item">The item to enqueue.</param>
        /// <returns>The same <paramref name="queue"/> instance.</returns>
        public static Queue<T> operator +(Queue<T> queue, T item)
        {   
            queue.Enqueue(item);
            return queue;
        }
    }
}