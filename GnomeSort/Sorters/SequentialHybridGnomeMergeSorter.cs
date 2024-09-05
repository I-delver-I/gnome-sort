namespace GnomeSort.Sorters;

public class SequentialHybridGnomeMergeSorter<T> : GnomeSorter<T> where T : IComparable<T>
{
    private int _segmentSize;
    
    public T[] Sort(T[] array)
    {
        ValidateParameters(array);
        _segmentSize = CalculateOptimalSegmentSize(array);
        var numberOfSegments = (int)Math.Ceiling((double)array.Length / _segmentSize);
        
        var segmentData = new T[array.Length];
        var segments = new ArraySegment<T>[numberOfSegments];

        for (var i = 0; i < numberOfSegments; i++)
        {
            var start = i * _segmentSize;
            var end = Math.Min(start + _segmentSize, array.Length);
        
            segments[i] = new ArraySegment<T>(segmentData, start, end - start); 
            Array.Copy(array, start, segmentData, start, end - start); 
        }

        foreach (var segment in segments)
        {
            GnomeSort(segment.Array, segment.Offset, segment.Offset + segment.Count - 1); 
        }

        var result = MergeSegments(segments);

        return result;
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