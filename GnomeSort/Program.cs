using System.Diagnostics;
using GnomeSort.Calculators;

namespace GnomeSort;

public static class Program
{
    private const int ArraySize = 225000;
    private static readonly int ProcessorCount = Environment.ProcessorCount;
    
    public static void Main()
    {
        var randomArray = ArrayGenerator.GenerateRandomIntArray(ArraySize);
        var stopwatch = new Stopwatch();
        var sequentialCalculator = new SequentialGnomeSortCalculator();
        var parallelCalculator = new ParallelGnomeSortCalculator();

        Console.WriteLine("Array size: " + ArraySize);
        Console.WriteLine("Processor count: " + ProcessorCount);
        
        stopwatch.Start();
        var sequentiallySortedArray = sequentialCalculator.Sort(randomArray);
        stopwatch.Stop();
        Console.WriteLine($"Sequential Gnome Sort took {stopwatch.ElapsedMilliseconds} ms \n");
        
        stopwatch.Reset();
        
        stopwatch.Start();
        var gnomeSortedArray = parallelCalculator.Sort(randomArray, segmentsCount: ProcessorCount);
        stopwatch.Stop();
        Console.WriteLine($"Parallel Gnome Sort took {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine(CompareArrays(sequentiallySortedArray, gnomeSortedArray) 
            ? "\nArrays are equal" : "\nArrays are not equal");
    }
    
    private static void PrintArray(int[] array)
    {
        foreach (var element in array)
        {
            Console.Write($"{element} ");
        }
        
        Console.WriteLine();
    }
    
    private static bool CompareArrays(int[] array1, int[] array2)
    {
        if (array1.Length != array2.Length)
        {
            return false;
        }

        return !array1.Where((t, i) => t != array2[i]).Any();
    }
}