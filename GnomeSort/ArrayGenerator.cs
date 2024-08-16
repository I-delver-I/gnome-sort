namespace GnomeSort
{
    /// <summary>
    /// Provides methods to generate random integer arrays.
    /// </summary>
    public static class ArrayGenerator
    {
        private static readonly Random Random = new();
        private const int MinRandomValue = 0;
        private const int MaxRandomValue = 1000;

        /// <summary>
        /// Generates an array of random integers with the specified size.
        /// </summary>
        /// <param name="length">The size of the array to generate.</param>
        /// <returns>An array of random integers.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the size is less than or equal to zero.</exception>
        public static int[] GenerateRandomArray(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Size must be greater than zero.");
            }
            
            var randomArray = new int[length];

            for (var i = 0; i < length; i++) 
            {
                randomArray[i] = Random.Next(MinRandomValue, MaxRandomValue);
            }

            return randomArray;
        }
    }
}