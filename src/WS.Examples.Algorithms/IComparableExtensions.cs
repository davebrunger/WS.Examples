namespace WS.Examples.Algorithms;

/// <summary>
/// Collection of comparison helpers for types implementing <see cref="IComparable{T}"/>.
/// </summary>
/// <remarks>
/// This file contains conceptual operator-style declarations for convenience; those declarations
/// are placeholders and may not be valid C# in all contexts. They are left unchanged here per
/// the request to avoid modifying logic. Replace the placeholders with proper extension methods
/// on concrete types when you need compile-time operator semantics.
/// </remarks>
public static class IComparableExtensions
{
    extension<T>(T _) where T : IComparable<T>
    {
        /// <summary>
        /// Operator-like convenience: returns <c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> when left &gt; right.</returns>
        public static bool operator >(T left, T right) => Comparer<T>.Default.Compare(left, right) > 0;

        /// <summary>
        /// Operator-like convenience: returns <c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> when left &lt; right.</returns>
        public static bool operator <(T left, T right) => Comparer<T>.Default.Compare(left, right) < 0;

        /// <summary>
        /// Operator-like convenience: returns <c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> when left &gt;= right.</returns>
        public static bool operator >=(T left, T right) => Comparer<T>.Default.Compare(left, right) >= 0;

        /// <summary>
        /// Operator-like convenience: returns <c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> when left &lt;= right.</returns>
        public static bool operator <=(T left, T right) => Comparer<T>.Default.Compare(left, right) <= 0;

        /// <summary>
        /// Operator-like convenience: returns <c>true</c> if <paramref name="left"/> equals <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> when left == right.</returns>
        public static bool operator ==(T left, T right) => Comparer<T>.Default.Compare(left, right) == 0;

        /// <summary>
        /// Operator-like convenience: returns <c>true</c> if <paramref name="left"/> does not equal <paramref name="right"/>.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> when left != right.</returns>
        public static bool operator !=(T left, T right) => Comparer<T>.Default.Compare(left, right) != 0;
    }
}
