using System.Diagnostics;
using GnomeSort.Input;
using GnomeSort.Sorters;

namespace GnomeSort.Tests;

public class ParallelSortTests
{
    public static void RunSort()
    {
        var stopwatch = new Stopwatch();
        var inputCatcher = new InputCatcher();
        var parallelSorter = new ParallelGnomeSorter<int>();
        
        var arrayLength = inputCatcher.CatchArrayLength();
        var numberOfThreads = inputCatcher.CatchNumberOfThreads();

        var random = new Random();
        const int minRandomValue = 0;
        const int maxRandomValue = 1000;
        var randomArray = ArrayUtils.GenerateRandomArray(arrayLength, () => 
            random.Next(minRandomValue, maxRandomValue));
        
        Console.WriteLine("Sorting array...\n");
        
        var parallelSortedArray = Array.Empty<int>();
        stopwatch.Start();
        
        try
        {
            parallelSortedArray = parallelSorter.Sort(randomArray, numberOfThreads);
            var sequentialArraySortingTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Parallel Gnome Sort took {sequentialArraySortingTime} ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Parallel Gnome Sort failed:\n{ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
        }
        
        Console.WriteLine();
        Console.WriteLine("Parallel Gnome Sort result is sorted " 
                          + (ArrayUtils.IsSortedAscending(parallelSortedArray) 
                              ? "correctly." : "incorrectly."));
    }
}