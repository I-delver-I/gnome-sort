namespace GnomeSort.Calculators;

public class ParallelGnomeSortCalculator
{
    public int[] Sort(int[] arr, int segmentsCount)
    {
        var segmentSize = arr.Length / segmentsCount;
        var tasks = new Task[segmentsCount];
        var result = (int[])arr.Clone();
        var sequentialCalculator = new SequentialGnomeSortCalculator();

        for (var i = 0; i < segmentsCount; i++)
        {
            var start = i * segmentSize;
            var end = i == segmentsCount - 1 ? result.Length : start + segmentSize;

            tasks[i] = Task.Run(() =>
            {
                var sortedSegment = sequentialCalculator.Sort(result, start, end);

                lock (result)
                {
                    Array.Copy(sortedSegment, start, 
                        result, start, end - start);
                }
            });
        }

        Task.WaitAll(tasks);
        result = MergeSegments(result, segmentsCount, segmentSize);
        return result;
    }

    private int[] MergeSegments(int[] arr, int segmentsCount, int segmentSize)
    {
        var result = (int[])arr.Clone();
    
        for (var i = 1; i < segmentsCount; i++)
        {
            var start = i * segmentSize;
            var end = (i == segmentsCount - 1) ? arr.Length : start + segmentSize;
            var merged = new int[end];
            int k = 0, j = 0, l = start;
    
            while (j < start && l < end)
            {
                if (result[j] <= result[l])
                {
                    merged[k++] = result[j++];
                }
                else
                {
                    merged[k++] = result[l++];
                }
            }
    
            while (j < start)
            {
                merged[k++] = result[j++];
            }
    
            while (l < end)
            {
                merged[k++] = result[l++];
            }
    
            Array.Copy(merged, 0, result, 0, end);
        }
    
        return result;
    }
}