namespace WS.Examples.Algorithms;

/// <summary>
/// Provides extension helpers for graph-like structures represented as an adjacency list.
/// </summary>
public static class IReadOnlyDictionaryExtensions
{
    /// <summary>
    /// Performs a breadth-first search on a directed graph represented as an <see cref="IReadOnlyDictionary{T, U}"/> where U is an <see cref="IEnumerable{T}"/>.
    /// This method assumes that the start node exists in the graph and that the graph is well-formed (i.e., all nodes referenced in 
    /// the adjacency list are valid keys in the dictionary), and that the start node cannot be the target of the search.
    /// </summary>
    /// <typeparam name="T">The node type. Must implement <see cref="IEquatable{T}"/>.</typeparam>
    /// <param name="graph">The adjacency list representation of the graph.</param>
    /// <param name="start">The starting node for the search.</param>
    /// <param name="predicate">A predicate invoked for each visited node; when it returns <c>true</c> the node is returned.</param>
    /// <returns>
    /// An <c>Option&lt;T&gt;</c> containing the first node that satisfies <paramref name="predicate"/>,
    /// or <c>None</c> if no such node is found.
    /// </returns>
    public static Option<T> BreadthFirstSearch<T>(this IReadOnlyDictionary<T, IEnumerable<T>> graph, T start, Func<T, bool> predicate) where T : IEquatable<T>
    {
        var searchQueue = new Queue<T>();
        var searched = new HashSet<T>();
        searchQueue += graph[start];
        while (searchQueue.Count > 0)
        {
            var current = searchQueue.Dequeue();
            if (!searched.Contains(current))
            {
                if (predicate(current))
                {
                    return Some(current);
                }
                else
                {
                    searchQueue += graph[current];
                    searched.Add(current);
                }
            }
        }
        return None;
    }
}