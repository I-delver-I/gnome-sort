namespace GnomeSort.Sorters;

public abstract class GnomeSorter<T> where T : IComparable<T>
{
    protected void GnomeSort(T[] array)
    {
        GnomeSort(array, 0, array.Length - 1);
    }
    
    protected void GnomeSort(T[] array, int startIndex, int endIndex)
    {
        var currentIndex = startIndex + 1;

        while (currentIndex <= endIndex)
        {
            if (currentIndex == startIndex
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

    protected T[] MergeSegments(ArraySegment<T>[] segments)
    {
        var result = new T[segments.Sum(s => s.Count)];
        var indices = new int[segments.Length]; 

        var resultIndex = 0;
        T minValue = default;

        while (true)
        {
            var minSegmentIndex = -1;

            for (var i = 0; i < segments.Length; i++)
            {
                if (indices[i] >= segments[i].Count)
                {
                    continue;
                }

                if (minSegmentIndex != -1 && 
                    segments[i].Array![segments[i].Offset + indices[i]].CompareTo(minValue) >= 0)
                {
                    continue;
                }

                minValue = segments[i].Array![segments[i].Offset + indices[i]];
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

    protected int CalculateOptimalSegmentSize(T[] array)
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

    protected void Swap(T[] array, int index1, int index2)
    {
        (array[index1], array[index2]) = (array[index2], array[index1]);
    }
}