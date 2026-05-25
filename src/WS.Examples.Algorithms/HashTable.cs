using System.Diagnostics.CodeAnalysis;

namespace WS.Examples.Algorithms;

/// <summary>
/// A simple hash table implementation using separate chaining with immutable linked lists.
/// The table grows when the load factor (proportion of non-empty buckets) exceeds a threshold.
/// </summary>
/// <typeparam name="TKey">The type of keys in the hash table. Must implement <see cref="IEquatable{TKey}"/>.</typeparam>
/// <typeparam name="TValue">The type of values in the hash table.</typeparam>
public class HashTable<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    private static readonly int DefaultSize = 4;

    private static readonly double LoadFactorThreshold = 0.7;

    private LinkedList<(TKey Key, TValue Value)>[] buckets;

    /// <summary>
    /// Gets the number of elements stored in the hash table.
    /// </summary>
    public int Count => buckets.Sum(bucket => bucket.Count);

    /// <summary>
    /// Gets a value indicating whether the collection is read-only. This implementation is mutable,
    /// so the property returns <c>false</c>.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets a collection containing the keys in the hash table.
    /// </summary>
    public ICollection<TKey> Keys => [.. buckets.SelectMany(bucket => bucket.Select(element => element.Key))];

    /// <summary>
    /// Gets a collection containing the values in the hash table.
    /// </summary>
    public ICollection<TValue> Values => [.. buckets.SelectMany(bucket => bucket.Select(element => element.Value))];

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// Getting a missing key throws; setting inserts or updates the value for the key.
    /// </summary>
    /// <param name="key">The key whose value to get or set.</param>
    /// <returns>The value associated with the specified key.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the specified key is not found in the hash table.</exception>
    public TValue this[TKey key]
    {
        get => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException($"The given key '{key}' was not present in the hash table.");
        set => Add(key, value);
    }

    private HashTable(int size)
    {
        buckets = [.. new Range(0, size).Select(_ => LinkedList.Empty)];
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HashTable{TKey, TValue}"/> with a default initial bucket count.
    /// </summary>
    public HashTable() : this(DefaultSize)
    {
    }

    /// <summary>
    /// Adds or updates the specified key and value in the hash table. If necessary, the
    /// table will be resized to maintain an acceptable load factor.
    /// </summary>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value associated with <paramref name="key"/>.</param>
    public void Add(TKey key, TValue value)
    {
        var index = Math.Abs(key.GetHashCode()) % buckets.Length;
        var (newBucket, _) = buckets[index].Remove(element => element.Key.Equals(key));
        buckets[index] = (key, value) + newBucket;

        // Grokking Algorithms recommends resizing the hash table when the load factor (the number of non-empty 
        // buckets divided by the total number of buckets) rather than the total number of elements exceeds a 
        // certain threshold (e.g., 0.7). So this approach is adopted here.
        if (buckets.Count(b => !b.IsEmpty) / (double)buckets.Length <= LoadFactorThreshold)
        {
            return;
        }

        var newBuckets = new Range(0, buckets.Length * 2).Select(_ => LinkedList<(TKey, TValue)>.Empty).ToArray();
        foreach (var bucket in buckets)
        {
            foreach (var element in bucket)
            {
                var newIndex = Math.Abs(element.GetHashCode()) % newBuckets.Length;
                newBuckets[newIndex] = element + newBuckets[newIndex];
            }
        }
        buckets = newBuckets;
    }

    /// <summary>
    /// Adds the specified <see cref="KeyValuePair{TKey, TValue}"/> to the hash table.
    /// </summary>
    /// <param name="item">The key/value pair to add.</param>
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    /// <summary>
    /// Removes all items from the hash table and resets it to its default size.
    /// </summary>
    public void Clear()
    {
        buckets = [.. new Range(0, DefaultSize).Select(_ => LinkedList.Empty)];
    }
    /// <summary>
    /// Determines whether the hash table contains the specified key/value pair.
    /// </summary>
    /// <param name="item">The key/value pair to locate in the hash table.</param>
    /// <returns><c>true</c> if the pair is found; otherwise, <c>false</c>.</returns>
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        var index = Math.Abs(item.Key.GetHashCode()) % buckets.Length;
        return buckets[index].Contains(element => element.Key == item.Key && Equals(element.Value, item.Value));
    }

    /// <summary>
    /// Determines whether the hash table contains a specific key.
    /// </summary>
    /// <param name="key">The key to locate in the hash table.</param>
    /// <returns><c>true</c> if the key is found; otherwise, <c>false</c>.</returns>
    public bool ContainsKey(TKey key)
    {
        var index = Math.Abs(key.GetHashCode()) % buckets.Length;
        return buckets[index].Contains(element => element.Key == key);
    }

    /// <summary>
    /// Copies the elements of the hash table to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The destination array.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        var i = arrayIndex;
        foreach (var bucket in buckets)
        {
            foreach (var (Key, Value) in bucket)
            {
                array[i] = new KeyValuePair<TKey, TValue>(Key, Value);
                i++;
            }
        }
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the hash table.
    /// </summary>
    /// <param name="key">The item to remove.</param>
    /// <returns><c>true</c> if the item was removed; otherwise, <c>false</c>.</returns>
    public bool Remove(TKey key)
    {
        var index = Math.Abs(key.GetHashCode()) % buckets.Length;
        var (newBucket, removed) = buckets[index].Remove(element => element.Key == key);
        buckets[index] = newBucket;
        return removed > 0;
    }

    /// <summary>
    /// Removes the first occurrence of a specific key/value pair from the hash table.
    /// </summary>
    /// <param name="item">The key/value pair to remove.</param>
    /// <returns><c>true</c> if the pair was removed; otherwise, <c>false</c>.</returns>
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var index = Math.Abs(item.Key.GetHashCode()) % buckets.Length;
        var (newBucket, removed) = buckets[index].Remove(element => element.Key == item.Key && Equals(element.Value, item.Value));
        buckets[index] = newBucket;
        return removed > 0;
    }

    /// <summary>
    /// Tries to get the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value to retrieve.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type.</param>
    /// <returns><c>true</c> if the key was found; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        var index = Math.Abs(key.GetHashCode()) % buckets.Length;
        var bucket = buckets[index];
        foreach (var (elementKey, elementValue) in bucket)
        {
            if (elementKey.Equals(key))
            {
                value = elementValue;
                return true;
            }
        }
        value = default;
        return false;
    }

    /// <summary>
    /// Returns an enumerator that iterates through all elements in the hash table.
    /// </summary>
    /// <returns>An <see cref="IEnumerator{T}"/> for the collection.</returns>
    public IEnumerator<(TKey, TValue)> GetEnumerator()
    {
        foreach (var bucket in buckets)
        {
            foreach (var item in bucket)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An <see cref="IEnumerator"/> for the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Returns a generic enumerator that iterates through the key/value pairs in the hash table.
    /// This is the explicit implementation for IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;.
    /// </summary>
    /// <returns>An IEnumerator of <c>KeyValuePair{TKey, TValue}</c> for the collection.</returns>
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
        return this.Select(item => new KeyValuePair<TKey, TValue>(item.Key, item.Value)).GetEnumerator();
    }
}