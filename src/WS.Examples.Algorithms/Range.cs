namespace WS.Examples.Algorithms;

/// <summary>
/// Represents a range of integers, defined by a <paramref name="Start"/>, <paramref name="Stop"/>, 
/// and <paramref name="Step"/> value. The range can be iterated over to produce a sequence of 
/// integers from <paramref name="Start"/> to <paramref name="Stop"/>, incrementing 
/// by <paramref name="Step"/>. The range is exclusive of the <paramref name="Stop"/> value.
/// </summary>
/// <param name="Start">The starting value of the range.</param>
/// <param name="Stop">The exclusive upper bound of the range.</param>
/// <param name="Step">The increment value for each step in the range.</param>
public record Range(int Start, int Stop, int Step = 1) : IEnumerable<int>
{
    /// <summary>
    /// Initializes a new instance of the Range record with a specified <paramref name="stop"/> 
    /// value and an optional <paramref name="step"/> value.
    /// </summary>
    /// <param name="stop">The exclusive upper bound of the range.</param>
    /// <param name="step">The increment value for each step in the range.</param>
    public Range(int stop, int step = 1) : this(0, stop, step)
    {
    }

    /// <inheritdoc/>
    public IEnumerator<int> GetEnumerator()
    {
        for (var i = Start; Stop > Start ? i < Stop : i > Stop; i += Step)
        {
            yield return i;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}