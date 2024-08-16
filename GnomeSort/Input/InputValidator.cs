namespace GnomeSort.Input;

/// <summary>
/// Provides validation methods for user input.
/// </summary>
public class InputValidator
{
    /// <summary>
    /// Validates the array length.
    /// </summary>
    /// <param name="length">The length of the array.</param>
    /// <exception cref="ArgumentException">Thrown when the array length is not a positive integer.</exception>
    public void ValidateArrayLength(int length)
    {
        if (length <= 0)
        {
            throw new ArgumentException("Array length must be a positive integer.");
        }
    }
    
    /// <summary>
    /// Validates the number of threads.
    /// </summary>
    /// <param name="numberOfThreads">The number of threads.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of threads is less than or equal to zero.</exception>
    public void ValidateThreadsCount(int numberOfThreads)
    {
        if (numberOfThreads <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfThreads), 
                "Number of threads must be greater than zero.");
        }
    }
}