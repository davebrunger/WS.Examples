namespace WS.Examples.Algorithms;

/// <summary>
/// Represents a single entry inside a container sequence which is either an item of type
/// <typeparamref name="T"/> or a nested sequence of <see cref="IContents{T}"/>.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
public interface IContents<T>
{
    /// <summary>
    /// Gets a value indicating whether this instance contains an item of type <typeparamref name="T"/>.
    /// </summary>
    bool IsItem { get; }

    /// <summary>
    /// Gets a value indicating whether this instance contains a nested container.
    /// </summary>
    bool IsContainer { get; }

    /// <summary>
    /// Gets the contained item as <typeparamref name="T"/>. Only valid when <see cref="IsItem"/> is <c>true</c>.
    /// </summary>
    T AsItem { get; }

    /// <summary>
    /// Gets the contained nested container. Only valid when <see cref="IsContainer"/> is <c>true</c>.
    /// </summary>
    IEnumerable<IContents<T>> AsContainer { get; }
}

/// <summary>
/// Extension helpers for traversing sequences of <see cref="IContents{T}"/> entries.
/// </summary>
public static class IEnumerableExtensions
{
    
    /// <summary>
    /// Searches the container breadth-first using an explicit stack and returns the first element
    /// that satisfies <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="container">The root container to search.</param>
    /// <param name="predicate">Predicate used to test values.</param>
    /// <returns>
    /// An <see cref="Option{T}"/> containing the first matching value if found; otherwise <see cref="Option.None"/>.
    /// </returns>
    public static Option<T> FindIteratively<T>(this IEnumerable<IContents<T>> container, Func<T, bool> predicate)
    {
        var stack = new Stack<IEnumerable<IContents<T>>>();
        stack.Push(container);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            foreach (var contents in current)   
            {
                if (contents.IsContainer)
                {
                    stack.Push(contents.AsContainer);
                }
                else
                {
                    var item = contents.AsItem;
                    if (predicate(item))
                    {
                        return Option.Some(item);
                    }
                }
            }
        }

        return Option.None;
    }

    /// <summary>
    /// Searches the container for a value equal to <paramref name="value"/> using <see cref="IEquatable{T}"/> semantics.
    /// </summary>
    /// <typeparam name="T">The element type which must implement <see cref="IEquatable{T}"/>.</typeparam>
    /// <param name="container">The root container to search.</param>
    /// <param name="value">The value to locate.</param>
    /// <returns>
    /// An <see cref="Option{T}"/> containing the matching value if found; otherwise <see cref="Option.None"/>.
    /// </returns>
    public static Option<T> FindIteratively<T>(this IEnumerable<IContents<T>> container, T value) where T : IEquatable<T>
    {
        return container.FindIteratively(x => x == value);
    }

    /// <summary>
    /// Recursively searches the container depth-first and returns the first element that satisfies <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="container">The container to search.</param>
    /// <param name="predicate">Predicate used to test values.</param>
    /// <returns>
    /// An <see cref="Option{T}"/> containing the first matching value if found; otherwise <see cref="Option.None"/>.
    /// </returns>
    public static Option<T> FindRecursively<T>(this IEnumerable<IContents<T>> container, Func<T, bool> predicate)
    {
        foreach (var contents in container)
        {
            if (contents.IsContainer)
            {
                var result = FindRecursively(contents.AsContainer, predicate);
                if (result.IsSome)
                {
                    return result;
                }
            }
            else
            {
                var item = contents.AsItem;
                if (predicate(item))
                {
                    return Option.Some(item);
                }
            }
        }
        return Option.None;
    }

    /// <summary>
    /// Recursively searches the container for a value equal to <paramref name="value"/> using <see cref="IEquatable{T}"/> semantics.
    /// </summary>
    /// <typeparam name="T">The element type which must implement <see cref="IEquatable{T}"/>.</typeparam>
    /// <param name="container">The container to search.</param>
    /// <param name="value">The value to locate.</param>
    /// <returns>
    /// An <see cref="Option{T}"/> containing the matching value if found; otherwise <see cref="Option.None"/>.
    /// </returns>
    public static Option<T> FindRecursively<T>(this IEnumerable<IContents<T>> container, T value) where T : IEquatable<T>
    {
        return container.FindRecursively(x => x == value);
    }
}