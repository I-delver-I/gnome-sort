using System.Buffers;

namespace GnomeSort.Sorters;

public class SequentialHybridGnomeMergeSorter<T> where T : IComparable<T>
{
    private int _segmentSize;
    private readonly ArrayPool<T> _arrayPool = ArrayPool<T>.Shared;
    
    public T[] Sort(T[] array)
    {
        ValidateParameters(array);
        _segmentSize = CalculateOptimalSegmentSize(array);
        var numberOfSegments = (int)Math.Ceiling((double)array.Length / _segmentSize);
        var segments = new T[numberOfSegments][];

        for (var i = 0; i < numberOfSegments; i++)
        {
            var start = i * _segmentSize;
            var end = Math.Min(start + _segmentSize, array.Length);
            segments[i] = _arrayPool.Rent(end - start); 
            Array.Copy(array, start, segments[i], 0, end - start);
        }
        
        foreach (var segment in segments)
        {
            GnomeSort(segment);
        }
        
        var result = MergeSegments(segments);
        
        foreach (var segment in segments)
        {
            _arrayPool.Return(segment);
        }

        return result;
    }
    
    private void GnomeSort(T[] array)
    {
        var currentIndex = 1;

        while (currentIndex < array.Length)
        {
            if (currentIndex == 0
                || array[currentIndex].CompareTo(array[currentIndex - 1]) >= 0)
            {
                currentIndex++;
            }
            else
            {
                Swap(array, currentIndex, currentIndex - 1);
                currentIndex--;
            }
        }
    }

    private T[] MergeSegments(T[][] segments)
    {
        var result = new T[segments.Sum(s => s.Length)];
        var indices = new int[segments.Length];

        var resultIndex = 0;
        
        while (true)
        {
            T minValue = default;
            var minSegmentIndex = -1;

            for (var i = 0; i < segments.Length; i++)
            {
                if (indices[i] >= segments[i].Length)
                {
                    continue;
                }
                
                if (minSegmentIndex != -1 && segments[i][indices[i]].CompareTo(minValue) >= 0)
                {
                    continue;
                }
                
                minValue = segments[i][indices[i]];
                minSegmentIndex = i;
            }

            if (minSegmentIndex == -1)
            {
                break;
            }

            result[resultIndex++] = minValue;
            indices[minSegmentIndex]++;
        }

        return result;
    }

    private int CalculateOptimalSegmentSize(T[] array)
    {
        const int minimumSegmentSize = 64;

        if (array.Length <= minimumSegmentSize)
        {
            return array.Length;
        }

        var baseSegmentSize = array.Length switch
        { 
            <= 1000 => Math.Min(minimumSegmentSize, array.Length / 8),     
            <= 100000 => Math.Max(minimumSegmentSize, array.Length / 500), 
            <= 1000000 => Math.Max(minimumSegmentSize, array.Length / 1300),
            _ => Math.Max(minimumSegmentSize, array.Length / 2000)         
        };

        return baseSegmentSize;
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