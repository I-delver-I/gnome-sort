namespace GnomeSort.Sorters;

/// <summary>
/// Provides a method to perform Gnome Sort on an array of integers.
/// </summary>
public class SequentialGnomeSorter
{
    /// <summary>
    /// Sorts a segment of the specified array using the Gnome Sort algorithm.
    /// </summary>
    /// <param name="array">The array of integers to sort.</param>
    /// <param name="segmentStart">The starting index of the segment to sort.</param>
    /// <param name="segmentEnd">The ending index of the segment to sort.</param>
    /// <returns>A new array containing the sorted segment.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the start or end index is out of range.</exception>
    /// <exception cref="ArgumentException">Thrown when the start index is not less than the end index.</exception>
    public int[] Sort(int[] array, int segmentStart, int segmentEnd)
    {
        ValidateParameters(array, segmentStart, segmentEnd);
        var currentIndex = segmentStart;
        var sortedSegment = (int[])array.Clone();

        while (currentIndex < segmentEnd)
        {
            if (currentIndex == segmentStart || sortedSegment[currentIndex] >= sortedSegment[currentIndex - 1])
            {
                currentIndex++;
            }
            else
            {
                (sortedSegment[currentIndex], sortedSegment[currentIndex - 1]) 
                    = (sortedSegment[currentIndex - 1], sortedSegment[currentIndex]);
                currentIndex--;
            }
        }

        return sortedSegment;
    }

    /// <summary>
    /// Sorts the specified array using the Gnome Sort algorithm.
    /// </summary>
    /// <param name="array">The array of integers to sort.</param>
    /// <returns>A new array containing the sorted integers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the start or end index is out of range.</exception>
    /// <exception cref="ArgumentException">Thrown when the start index is not less than the end index.</exception>
    public int[] Sort(int[] array)
    {
        return Sort(array, 0, array.Length);
    }
    
    private void ValidateParameters(int[] array, int segmentStart, int segmentEnd)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Input array cannot be null.");
        }

        if (segmentStart < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(segmentStart), "Start index cannot be negative.");
        }

        if (segmentEnd > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(segmentEnd), 
                "End index cannot be greater than the array length.");
        }

        if (segmentStart >= segmentEnd)
        {
            throw new ArgumentException("Start index must be less than end index.", nameof(segmentStart));
        }
    }
}