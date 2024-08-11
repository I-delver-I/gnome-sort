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
        /// <param name="size">The size of the array to generate.</param>
        /// <returns>An array of random integers.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the size is less than or equal to zero.</exception>
        public static int[] GenerateRandomIntArray(int size)
        {
            if (size <= 0) {
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than zero.");
            }
            
            var array = new int[size];

            for (var i = 0; i < size; i++) {
                array[i] = Random.Next(MinRandomValue, MaxRandomValue);
            }

            return array;
        }
    }
}