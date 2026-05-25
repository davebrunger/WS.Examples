# WS.Examples.Algorithms

Example implementations of selected algorithms from [*Grokking Algorithms, Second Edition*](https://www.manning.com/books/grokking-algorithms-second-edition) 
by Aditya Y Bhargava, translated to modern C#.

This project focuses on small, self-contained examples intended for learning and experimentation rather than production-ready libraries.

## Contents

| Chapter | Page | Algorithm                             | Implementation                                                       |
|--------:|-----:|---------------------------------------|----------------------------------------------------------------------|
|       1 |    9 | Binary Search                         | [IReadOnlyListExtensions.cs](IReadOnlyListExtensions.cs)             |
|       2 |   38 | Selection Sort                        | [IReadOnlyListExtensions.cs](IReadOnlyListExtensions.cs)             |
|       3 |   44 | Recursion                             | [IEnumerableExtensions.cs](IEnumerableExtensions.cs)                 |
|       4 |   69 | Quick Sort                            | [IReadOnlyListExtensions.cs](IReadOnlyListExtensions.cs)             |
|       5 |    - | Hash Table                            | [HashTable.cs](HashTable.cs)                                         |
|       6 |  116 | Breadth-First Search                  | [IReadOnlyDictionaryExtensions.cs](IReadOnlyDictionaryExtensions.cs) |
|       7 |    - | Tree, Depth-First Search, Binary Tree | [Trees.cs](Trees.cs)                                                 |

Wherever practical the example code preserves the identifiers and structure used in the book to make comparison easier.

## Notes

- `IReadOnlyList<T>` is used instead of arrays or `List<T>` so the algorithms work on both.
- `IReadOnlyDictionary<T, IEnumerable<T>>` is used to represent a graph. The keys are the nodes and the value for each key represents a traversal from
  the key to that node.
- `HashTable<TKey, TValue>` is mutable and represents the implementation from the book. It simply there to demonstrate that implementation. As the book
  suggests just use `Dictionary<TKey, TValue>` instead. A similar implementation for an immutable version is planned be added to the project
  at a later date.

## Getting started

Prerequisites: .NET 10 SDK (or newer).

Build the project:

```powershell
dotnet build
```

There are no runnable demos in this project; the source files are small algorithm examples you can reference from your own code or test harness.

## Examples

- `IReadOnlyListExtensions.cs`: Contains `BinarySearch<T>` (returns `Option<int>`) and a selection sort implementation that returns a new sorted `List<T>`.
- `IEnumerableExtensions.cs`: Depth-first and explicit-stack iterative traversal helpers for nested content sequences (`IContents<T>`).

## Helpers

Small helper files included to keep examples concise and readable.

| Helper                                               | Purpose                                                                                          |
|------------------------------------------------------|--------------------------------------------------------------------------------------------------|
| [IComparableExtensions.cs](IComparableExtensions.cs) | Enables comparison operator syntax for types implementing `IComparable<T>` used in the examples. |
| [IEquatableExtensions.cs](IEquatableExtensions.cs)   | Enables equality operator syntax for types implementing `IEquatable<T>`.                         |
| [IListExtensions.cs](IListExtensions.cs)             | Adds a `Pop` extension used by the selection sort example.                                       |
| [LinkedList.cs](LinkedList.cs)                       | Provides a simple example implementation of a linked list.                                       |
| [Range.cs](Range.cs)                                 | Provides a small enumerable `Range` helper with an exclusive upper bound.                        |
| [QueueExtensions](QueueExtensions.cs)                | Add ability to easily enqueue multiple items at the same time using operator syntax              |

## Contributing

Contributions and improvements are welcome. Open an issue or submit a pull request with a clear description of the change and a short rationale.

## License

This repository is provided for educational purposes. Check the repository root for license information (if any).
