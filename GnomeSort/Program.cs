using System.Diagnostics;
using GnomeSort.Input;
using GnomeSort.Sorters;

namespace GnomeSort;

/// <summary>
/// Main program class for Gnome Sort application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Main entry point of the application.
    /// </summary>
    public static void Main()
    {
        var stopwatch = new Stopwatch();
        var inputCatcher = new InputCatcher();
        var sequentialSorter = new SequentialGnomeSorter();
        var parallelSorter = new ParallelGnomeSorter();
        
        var arrayLength = inputCatcher.CatchArrayLength();
        var numberOfThreads = inputCatcher.CatchNumberOfThreads();
        
        var randomArray = ArrayGenerator.GenerateRandomArray(arrayLength);
        Console.WriteLine("Sorting array...\n");
        
        var sequentiallySortedArray = Array.Empty<int>();
        stopwatch.Start();

        try
        {
            sequentiallySortedArray = sequentialSorter.Sort(randomArray);
            Console.WriteLine($"Sequential Gnome Sort took {stopwatch.ElapsedMilliseconds} ms");
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
            Console.WriteLine($"Parallel Gnome Sort took {stopwatch.ElapsedMilliseconds} ms");
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
        Console.WriteLine(AreArraysEqual(sequentiallySortedArray, parallelSortedArray) 
            ? "The sorted arrays are equal." 
            : "The sorted arrays are not equal.");
    }
    
    private static void PrintArrays(int[] randomArray, int[] sequentiallySortedArray, int[] parallelSortedArray)
    {
        Console.WriteLine("Random array:");
        PrintArray(randomArray);
        Console.WriteLine("Sequentially sorted array:");
        PrintArray(sequentiallySortedArray);
        Console.WriteLine("Parallel sorted array:");
        PrintArray(parallelSortedArray);
    }
    
    private static void PrintArray(int[] array)
    {
        foreach (var element in array)
        {
            Console.Write($"{element} ");
        }
        
        Console.WriteLine();
    }
    
    private static bool AreArraysEqual(int[] array1, int[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false;
        }

        return !array1.Where((t, i) => t != array2[i]).Any();
    }
}