using System.Diagnostics;
using GnomeSort.Input;
using GnomeSort.Sorters;

namespace GnomeSort.Tests;

public class SequentialSortTests
{
    public static void RunSort()
    {
        var stopwatch = new Stopwatch();
        var inputCatcher = new InputCatcher();
        var sequentialSorter = new SequentialHybridGnomeMergeSorter<int>();
        
        var arrayLength = inputCatcher.CatchArrayLength();

        var random = new Random();
        const int minRandomValue = 0;
        const int maxRandomValue = 1000;
        var randomArray = ArrayUtils.GenerateRandomArray(arrayLength, () => 
            random.Next(minRandomValue, maxRandomValue));
        
        Console.WriteLine("Sorting array...\n");
        
        var sequentiallySortedArray = Array.Empty<int>();
        stopwatch.Start();
        
        try
        {
            sequentiallySortedArray = sequentialSorter.Sort(randomArray);
            var sequentialArraySortingTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Sequential Gnome Sort took {sequentialArraySortingTime} ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Sequential Gnome Sort failed:\n{ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
        }
        
        Console.WriteLine();
        Console.WriteLine("Sequential Gnome Sort result is sorted " 
                          + (ArrayUtils.IsSortedAscending(sequentiallySortedArray) 
                              ? "correctly." : "incorrectly."));
    }
}