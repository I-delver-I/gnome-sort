using System.Diagnostics;
using GnomeSort.Input;
using GnomeSort.Sorters;
using GnomeSort.Tests;

namespace GnomeSort;

public static class Program
{
    public static void Main()
    {
        Run();
        // SequentialSortTests.RunSort();
        // ParallelSortTests.RunSort();
    }

    private static void Run()
    {
        var stopwatch = new Stopwatch();
        var inputCatcher = new InputCatcher();
        var sequentialSorter = new SequentialHybridGnomeMergeSorter<int>();
        var parallelSorter = new ParallelGnomeSorter<int>();
        
        var arrayLength = inputCatcher.CatchArrayLength();
        var numberOfThreads = inputCatcher.CatchNumberOfThreads();
        
        long sequentialArraySortingTime = 0;
        long parallelArraySortingTime = 0;
        
        var random = new Random();
        const int minRandomValue = 0;
        const int maxRandomValue = 30;
        var randomArray = ArrayUtils.GenerateRandomArray(arrayLength, () => 
            random.Next(minRandomValue, maxRandomValue));
        
        Console.WriteLine("Sorting array...\n");
        
        var sequentiallySortedArray = Array.Empty<int>();
        stopwatch.Start();

        try
        {
            sequentiallySortedArray = sequentialSorter.Sort(randomArray);
            sequentialArraySortingTime = stopwatch.ElapsedMilliseconds;
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

        stopwatch.Reset();
        var parallelSortedArray = Array.Empty<int>();
        stopwatch.Start();

        try
        {
            parallelSortedArray = parallelSorter.Sort(randomArray, numberOfThreads);
            parallelArraySortingTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Parallel Gnome Sort took {parallelArraySortingTime} ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Parallel Gnome Sort failed:\n{ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
        }

        var speedUpFactor = (double)sequentialArraySortingTime / parallelArraySortingTime;
        Console.WriteLine($"Speed-up factor: {speedUpFactor}");
        Console.WriteLine();
        
        Console.WriteLine("Sequential Gnome Sort result is sorted " 
                          + (ArrayUtils.IsSortedAscending(sequentiallySortedArray) 
                              ? "correctly." : "incorrectly."));
        Console.WriteLine("Parallel Gnome Sort result is sorted "
                          + (ArrayUtils.IsSortedAscending(parallelSortedArray) 
                              ? "correctly." : "incorrectly."));

        Console.WriteLine();
        Console.WriteLine(ArrayUtils.AreArraysEqual(sequentiallySortedArray, parallelSortedArray) 
            ? "Sorted arrays are equal" : "Sorted arrays are not equal");

        // Console.WriteLine("Initial array:");
        // ArrayUtils.PrintArray(randomArray);

        // Console.WriteLine("Sequentially sorted array:");
        // ArrayUtils.PrintArray(sequentiallySortedArray);
        
        // Console.WriteLine("Parallel sorted array:");
        // ArrayUtils.PrintArray(parallelSortedArray);
    }
    
    private static void PrintArrays(int[] initialArray, int[] sequentiallySortedArray, int[] parallelSortedArray)
    {
        Console.WriteLine("Initial array:");
        ArrayUtils.PrintArray(initialArray);
        
        Console.WriteLine("Sequentially sorted array:");
        ArrayUtils.PrintArray(sequentiallySortedArray);
        Console.WriteLine("Parallel sorted array:");
        ArrayUtils.PrintArray(parallelSortedArray);
    }
}