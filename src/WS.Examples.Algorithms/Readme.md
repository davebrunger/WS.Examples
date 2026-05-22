# WS.Examples.Algorithms

Example implementations of selected algorithms from *Grokking Algorithms* by Aditya Y Bhargava, translated to modern C#.

This project focuses on small, self-contained examples intended for learning and experimentation rather than production-ready libraries.

## Contents

| Chapter | Page | Algorithm | Implementation |
|--------:|-----:|:---------:|----------------|
| 1 | 9  | Binary Search   | [IReadOnlyListExtensions.cs](IReadOnlyListExtensions.cs)
| 2 | 38 | Selection Sort  | [IReadOnlyListExtensions.cs](IReadOnlyListExtensions.cs)
| 3 | 44 | Recursion       | [IEnumerableExtensions.cs](IEnumerableExtensions.cs)

Wherever practical the example code preserves the identifiers and structure used in the book to make comparison easier.

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

| Helper | Purpose |
|-------|--------|
| [IComparableExtensions.cs](IComparableExtensions.cs) | Enables comparison operator syntax for types implementing `IComparable<T>` used in the examples. |
| [IEquatableExtensions.cs](IEquatableExtensions.cs) | Enables equality operator syntax for types implementing `IEquatable<T>`. |
| [IListExtensions.cs](IListExtensions.cs) | Adds a `Pop` extension used by the selection sort example. |
| [Range.cs](Range.cs) | Provides a small enumerable `Range` helper with an exclusive upper bound. |

## Contributing

Contributions and improvements are welcome. Open an issue or submit a pull request with a clear description of the change and a short rationale.

## License

This repository is provided for educational purposes. Check the repository root for license information (if any).
