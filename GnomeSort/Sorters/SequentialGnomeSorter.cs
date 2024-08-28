namespace GnomeSort.Sorters;

/// <summary>
/// Provides a method to perform Gnome Sort on an array.
/// </summary>
public class SequentialGnomeSorter<T> where T : IComparable<T>
{
    /// <summary>
    /// Sorts the specified array using the Gnome Sort algorithm.
    /// </summary>
    /// <param name="array">The array to sort.</param>
    /// <returns>Sorted array.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the array length is zero.</exception>
    public T[] Sort(T[] array)
    {
        ValidateParameters(array);
        var currentIndex = 1;
        var sortedSegment = (T[])array.Clone();

        while (currentIndex < sortedSegment.Length)
        {
            if (currentIndex == 0 
                || sortedSegment[currentIndex].CompareTo(sortedSegment[currentIndex - 1]) >= 0)
            {
                currentIndex++;
            }
            else
            {
                Swap(sortedSegment, currentIndex, currentIndex - 1);
                currentIndex--;
            }
        }

        return sortedSegment;
    }
    
    private void Swap(T[] array, int index1, int index2)
    {
        (array[index1], array[index2]) = (array[index2], array[index1]);
    }
    
    private void ValidateParameters(T[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Input array cannot be null.");
        }
        
        if (array.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(array), "Input array cannot be empty.");
        }
    }
}