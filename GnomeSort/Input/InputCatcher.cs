namespace GnomeSort.Input;

/// <summary>
/// Provides methods to catch user input for array length and number of threads.
/// </summary>
public class InputCatcher
{
    private readonly InputValidator _validator = new();
    
    /// <summary>
    /// Catches the number of threads to use for sorting from user input.
    /// </summary>
    /// <returns>The number of threads to use for sorting.</returns>
    public int CatchNumberOfThreads()
    {
        int numberOfThreads;
        
        while (true)
        {
            Console.Write($"Enter the number of threads to use for sorting or press Enter " +
                          $"to use available in your system ({Environment.ProcessorCount}): ");
            var input = Console.ReadLine() ?? string.Empty;

            try
            {
                numberOfThreads = string.IsNullOrEmpty(input)
                    ? Environment.ProcessorCount
                    : int.Parse(input);
                _validator.ValidateThreadsCount(numberOfThreads);
                
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Number of threads must be a valid integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number of threads is too large.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return numberOfThreads;
    }
    
    /// <summary>
    /// Catches the array length from user input.
    /// </summary>
    /// <returns>The length of the array.</returns>
    public int CatchArrayLength()
    {
        int arrayLength;
        
        while (true)
        {
            Console.Write("Enter array length: ");
            var input = Console.ReadLine() ?? string.Empty;

            try
            {
                arrayLength = int.Parse(input);
                _validator.ValidateArrayLength(arrayLength);
                
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Array length must be a valid integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Array length is too large.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        return arrayLength;
    }
}