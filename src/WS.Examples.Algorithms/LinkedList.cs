namespace WS.Examples.Algorithms;
/// <summary>
/// Non-generic sentinel type used for conversions to generic <see cref="LinkedList{T}"/>.
/// This type represents an empty list marker and is not intended to hold values.
/// </summary>
public sealed class LinkedList
{
    private LinkedList()
    {
    }

    /// <summary>
    /// Gets an instance representing an empty list marker.
    /// </summary>
    public static LinkedList Empty => new();
}


/// <summary>
/// Immutable, singly-linked persistent list type that implements <see cref="IReadOnlyList{T}"/>.
/// Instances are either empty or a head element paired with a tail list. The type is functional
/// in nature: operations produce new lists rather than mutating existing ones.
/// </summary>
/// <typeparam name="T">The element type stored in the list.</typeparam>
public abstract class LinkedList<T> : IReadOnlyList<T>
{
    private LinkedList()
    {
    }

    /// <summary>
    /// Gets the element at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get.</param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when <paramref name="index"/> is outside the bounds of the list.</exception>
    public abstract T this[int index] { get; }

    /// <summary>
    /// Gets the number of elements in the list.
    /// </summary>
    public abstract int Count { get; }

    /// <summary>
    /// Gets a value indicating whether the list is empty.
    /// </summary>
    public abstract bool IsEmpty { get; }

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <returns>An <see cref="IEnumerator{T}"/> for the list.</returns>
    public abstract IEnumerator<T> GetEnumerator();

    /// <summary>
    /// Pattern-match over the list shape and return a <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The return type of the match operation.</typeparam>
    /// <param name="onEmpty">Function invoked when the list is empty.</param>
    /// <param name="onNonEmpty">Function invoked when the list is non-empty; receives the head and tail.</param>
    /// <returns>The result produced by the invoked branch.</returns>
    public abstract TResult Match<TResult>(Func<TResult> onEmpty, Func<T, LinkedList<T>, TResult> onNonEmpty);

    /// <summary>
    /// Pattern-match over the list shape and invoke the corresponding action.
    /// </summary>
    /// <param name="onEmpty">Action invoked when the list is empty.</param>
    /// <param name="onNonEmpty">Action invoked when the list is non-empty; receives the head and tail.</param>
    public abstract void Switch(Action onEmpty, Action<T, LinkedList<T>> onNonEmpty);

    /// <summary>
    /// Removes elements that match the provided <paramref name="predicate"/> from the list,
    /// returning a new list with those elements removed together with the count of removed elements.
    /// This operation is functional and does not mutate the original list.
    /// </summary>
    /// <param name="predicate">A predicate used to determine which elements to remove.</param>
    /// <returns>
    /// A tuple containing the new list with matching elements removed and the number of elements removed.
    /// </returns>
    public abstract (LinkedList<T> NewList, int RemovedCount) Remove(Func<T, bool> predicate);

    /// <summary>
    /// Determines whether any element in the list satisfies the specified <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns><c>true</c> if any element matches the predicate; otherwise, <c>false</c>.</returns>
    public abstract bool Contains(Func<T, bool> predicate);

    /// <summary>
    /// Explicit non-generic enumerator implementation forwarding to the generic enumerator.
    /// </summary>
    /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the list.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private sealed class EmptyList : LinkedList<T>
    {
        public override T this[int index] => throw new IndexOutOfRangeException();

        public override int Count => 0;

        public override bool IsEmpty => true;

        public override IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        public override TResult Match<TResult>(Func<TResult> onEmpty, Func<T, LinkedList<T>, TResult> onNonEmpty)
        {
            return onEmpty();
        }

        public override void Switch(Action onEmpty, Action<T, LinkedList<T>> onNonEmpty)
        {
            onEmpty();
        }

        public override (LinkedList<T> NewList, int RemovedCount) Remove(Func<T, bool> predicate)
        {
            return (this, 0);
        }

        public override bool Contains(Func<T, bool> predicate)
        {
            return false;
        }
    }

    private sealed class NonEmptyList : LinkedList<T>
    {
        private readonly T head;
        private readonly LinkedList<T> tail;

        public NonEmptyList(T head, LinkedList<T> tail)
        {
            this.head = head;
            this.tail = tail;
        }

        public override T this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return head;
                }
                else
                {
                    return tail[index - 1];
                }
            }
        }

        public override int Count => 1 + tail.Count;

        public override bool IsEmpty => false;

        public override IEnumerator<T> GetEnumerator()
        {
            yield return head;
            foreach (var item in tail)
            {
                yield return item;
            }
        }

        public override TResult Match<TResult>(Func<TResult> onEmpty, Func<T, LinkedList<T>, TResult> onNonEmpty)
        {
            return onNonEmpty(head, tail);
        }

        public override void Switch(Action onEmpty, Action<T, LinkedList<T>> onNonEmpty)
        {
            onNonEmpty(head, tail);
        }

        public override (LinkedList<T> NewList, int RemovedCount) Remove(Func<T, bool> predicate)
        {
            var (newTail, removedCount) = tail.Remove(predicate);
            if (predicate(head))
            {
                return (newTail, removedCount + 1);
            }
            else
            {
                return (head + newTail, removedCount);
            }
        }

        public override bool Contains(Func<T, bool> predicate)
        {
            if (predicate(head))
            {
                return true;
            }
            else
            {
                return tail.Contains(predicate);
            }
        }
    }

    /// <summary>
    /// Gets an empty list instance of <see cref="LinkedList{T}"/>.
    /// </summary>
    public static LinkedList<T> Empty { get; } = new EmptyList();

    /// <summary>
    /// Creates a new list with the specified head and tail.
    /// </summary>
    /// <param name="head">The head element.</param>
    /// <param name="tail">The tail list.</param>
    /// <returns>A new <see cref="LinkedList{T}"/> whose first element is <paramref name="head"/>.</returns>
    public static LinkedList<T> Cons(T head, LinkedList<T> tail)
    {
        return new NonEmptyList(head, tail);
    }

    /// <summary>
    /// Converts the non-generic empty marker into an empty generic list.
    /// </summary>
    /// <param name="_">The non-generic empty marker.</param>
    /// <returns>An empty <see cref="LinkedList{T}"/>.</returns>
    public static implicit operator LinkedList<T>(LinkedList _) => Empty;

    /// <summary>
    /// Creates a single-element list from the provided head value.
    /// </summary>
    /// <param name="head">The element to wrap in a list.</param>
    /// <returns>A <see cref="LinkedList{T}"/> containing only <paramref name="head"/>.</returns>
    public static implicit operator LinkedList<T>(T head) => Cons(head, Empty);

    /// <summary>
    /// Prepends <paramref name="head"/> to <paramref name="list"/> returning a new list.
    /// </summary>
    /// <param name="head">The element to prepend.</param>
    /// <param name="list">The list to which the element will be prepended.</param>
    /// <returns>A new <see cref="LinkedList{T}"/> with <paramref name="head"/> as its first element.</returns>
    public static LinkedList<T> operator +(T head, LinkedList<T> list) => Cons(head, list);
}