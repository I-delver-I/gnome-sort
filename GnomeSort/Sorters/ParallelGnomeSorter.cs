namespace GnomeSort.Sorters;

/// <summary>
/// Provides a method to perform parallel Gnome Sort on an array of integers.
/// </summary>
public class ParallelGnomeSorter
{
    /// <summary>
    /// Sorts the specified array using the parallel Gnome Sort algorithm.
    /// </summary>
    /// <param name="inputArray">The array of integers to sort.</param>
    /// <param name="numberOfThreads">The number of threads to use for sorting.</param>
    /// <returns>A new array containing the sorted integers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of threads is less than or equal to zero.</exception>
    public int[] Sort(int[] inputArray, int numberOfThreads)
    {
        if (numberOfThreads <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfThreads),
                "Number of threads must be greater than zero.");
        }

        if (inputArray.Length < numberOfThreads)
        {
            numberOfThreads = inputArray.Length;
        }
        
        var segmentLength = inputArray.Length / numberOfThreads;
        var sortedArray = (int[])inputArray.Clone();
        var sequentialSorter = new SequentialGnomeSorter();

        Parallel.For(0, numberOfThreads, i =>
        {
            var segmentStartIndex = i * segmentLength;
            var segmentEndIndex = i == numberOfThreads - 1
                ? inputArray.Length
                : segmentStartIndex + segmentLength;

            var sortedSegment = sequentialSorter.Sort(sortedArray, segmentStartIndex, segmentEndIndex);
            Array.Copy(sortedSegment, segmentStartIndex, sortedArray, 
                segmentStartIndex, segmentEndIndex - segmentStartIndex);
        });

        sortedArray = MergeSegments(sortedArray, numberOfThreads, segmentLength);
        return sortedArray;
    }

    /// <summary>
    /// Merges the sorted segments of the array.
    /// </summary>
    /// <param name="array">The array containing sorted segments.</param>
    /// <param name="segmentCount">The number of segments.</param>
    /// <param name="segmentSize">The size of each segment.</param>
    /// <returns>A new array containing the merged sorted segments.</returns>
    private int[] MergeSegments(int[] array, int segmentCount, int segmentSize)
    {
        var mergedArray = (int[])array.Clone();
    
        for (var segmentIndex = 1; segmentIndex < segmentCount; segmentIndex++)
        {
            var currentSegmentStart = segmentIndex * segmentSize;
            var currentSegmentEnd = segmentIndex == segmentCount - 1 
                ? array.Length : currentSegmentStart + segmentSize;
            
            var tempMergedSegment = new int[currentSegmentEnd];
            var mergedSegmentIndex = 0;
            var previousSegmentIndex = 0;
            var currentSegmentPointer = currentSegmentStart;
    
            while (previousSegmentIndex < currentSegmentStart 
                   && currentSegmentPointer < currentSegmentEnd)
            {
                tempMergedSegment[mergedSegmentIndex++] 
                    = mergedArray[previousSegmentIndex] <= mergedArray[currentSegmentPointer] 
                    ? mergedArray[previousSegmentIndex++] : mergedArray[currentSegmentPointer++];
            }
    
            while (previousSegmentIndex < currentSegmentStart)
            {
                tempMergedSegment[mergedSegmentIndex++] = mergedArray[previousSegmentIndex++];
            }
    
            while (currentSegmentPointer < currentSegmentEnd)
            {
                tempMergedSegment[mergedSegmentIndex++] = mergedArray[currentSegmentPointer++];
            }
    
            Array.Copy(tempMergedSegment, 0, mergedArray, 
                0, currentSegmentEnd);
        }
    
        return mergedArray;
    }
}