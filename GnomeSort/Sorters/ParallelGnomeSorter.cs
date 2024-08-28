namespace GnomeSort.Sorters
{
    class ParallelGnomeSorter<T> where T : IComparable<T>
    {
        private object[] _locks;

        public T[] Sort(T[] array, int numberOfThreads)
        {
            ValidateParameters(array, ref numberOfThreads);
            GenerateLocks(array.Length);
            var sortedArray = (T[])array.Clone();
            var step = 2 * numberOfThreads;

            Parallel.For(0, numberOfThreads, threadIndex =>
            {
                var currentIndex = threadIndex * 2 + 1;

                while (currentIndex < sortedArray.Length)
                {
                    while (currentIndex > 0 
                           && sortedArray[currentIndex - 1].CompareTo(sortedArray[currentIndex]) > 0)
                    {
                        SwapWithLock(sortedArray, currentIndex, currentIndex - 1);
                        currentIndex--;
                    }
                    
                    currentIndex += step;
                }
            });
            
            // FinalPass(sortedArray);
            return sortedArray;
        }

        private void GenerateLocks(int arrayLength)
        {
            _locks = new object[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                _locks[i] = new object();
            }
        }
        
        /// <summary>
        /// Performs a final pass to ensure the array is fully sorted.
        /// </summary>
        /// <param name="array">The array to be sorted.</param>
        private void FinalPass(T[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var currentIndex = i;

                while (currentIndex > 0 
                       && array[currentIndex - 1].CompareTo(array[currentIndex]) > 0)
                {
                    Swap(array, currentIndex, currentIndex - 1);
                    currentIndex--;
                }
            }
        }
        
        
        /// <summary>
        /// Swaps the elements at the specified indices in the array with thread safety.
        /// </summary>
        /// <param name="array">The array containing the elements to swap.</param>
        /// <param name="i">The index of the first element to swap.</param>
        /// <param name="j">The index of the second element to swap.</param>
        private void SwapWithLock(T[] array, int i, int j)
        {
            lock (_locks[i])
            {
                lock (_locks[j])
                {
                    Swap(array, i, j);
                }
            }
        }
        
        private void ValidateParameters(T[] array, ref int numberOfThreads)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Input array cannot be null.");
            }
            
            if (numberOfThreads <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfThreads), 
                    "Number of threads must be greater than zero.");
            }
            
            if (array.Length < numberOfThreads)
            {
                numberOfThreads = array.Length;
            }
        }

        private void Swap(T[] array, int i, int j)
        {
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
