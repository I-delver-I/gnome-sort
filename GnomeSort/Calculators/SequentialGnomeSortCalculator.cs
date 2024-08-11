namespace GnomeSort.Calculators;

/// <summary>
/// Provides a method to perform Gnome Sort on an array of integers.
/// </summary>
public class SequentialGnomeSortCalculator
{
    /// <summary>
    /// Sorts a segment of the specified array using the Gnome Sort algorithm.
    /// </summary>
    /// <param name="arr">The array of integers to sort.</param>
    /// <param name="start">The starting index of the segment to sort.</param>
    /// <param name="end">The ending index of the segment to sort.</param>
    /// <returns>A new array containing the sorted segment.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the start or end index is out of range.</exception>
    /// <exception cref="ArgumentException">Thrown when the start index is not less than the end index.</exception>
    public int[] Sort(int[] arr, int start, int end)
    {
        if (arr == null) {
            throw new ArgumentNullException(nameof(arr), "Input array cannot be null.");
        }

        if (start < 0) {
            throw new ArgumentOutOfRangeException(nameof(start), "Start index cannot be negative.");
        }

        if (end > arr.Length) {
            throw new ArgumentOutOfRangeException(nameof(end), "End index cannot be greater than the array length.");
        }

        if (start >= end) {
            throw new ArgumentException("Start index must be less than end index.", nameof(start));
        }

        var index = start;
        var result = (int[])arr.Clone();

        while (index < end)
        {
            if (index == start || result[index] >= result[index - 1])
            {
                index++;
            }
            else
            {
                (result[index], result[index - 1]) = (result[index - 1], result[index]);
                index--;
            }
        }

        return result;
    }

    /// <summary>
    /// Sorts the specified array using the Gnome Sort algorithm.
    /// </summary>
    /// <param name="arrayToSort">The array of integers to sort.</param>
    /// <returns>A new array containing the sorted integers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the input array is empty.</exception>
    public int[] Sort(int[] arrayToSort)
    {
        if (arrayToSort == null) {
            throw new ArgumentNullException(nameof(arrayToSort), "Input array cannot be null.");
        }

        if (arrayToSort.Length == 0) {
            throw new ArgumentException("Input array cannot be empty.", nameof(arrayToSort));
        }

        return Sort(arrayToSort, 0, arrayToSort.Length);
    }
}