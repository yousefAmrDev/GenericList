using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericList
{
    public class MyList<T> : IEnumerable<T>
    {
        #region Fields & Properties

        // Internal array used to store list elements dynamically
        private T[] items;

        // Represents the actual number of stored elements
        public int Count { get; private set; }

        // Represents the current allocated array size
        public int Capacity => items.Length;

        #endregion

        #region Constructors

        // Initializes the list with default capacity
        public MyList() : this(4)
        {
        }

        // Initializes the list with custom capacity
        public MyList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            items = new T[capacity];
            Count = 0;
        }

        #endregion

        #region Indexers

        // Provides direct access to elements using index
        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return items[index];
            }

            set
            {
                ValidateIndex(index);
                items[index] = value;
            }
        }

        // Returns multiple elements using comma-separated indexes
        public List<T> this[string indexes]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(indexes))
                    throw new ArgumentException("Indexes string cannot be empty.");

                string[] indexesArray = indexes.Split(',');

                List<T> result = new List<T>();

                foreach (string indexText in indexesArray)
                {
                    if (!int.TryParse(indexText, out int index))
                        continue;

                    if (index < 0 || index >= Count)
                        continue;

                    result.Add(items[index]);
                }

                return result;
            }
        }

        #endregion

        #region Core Add Operations

        // Adds a new element to the end of the list
        public void Add(T item)
        {
            EnsureCapacity(Count + 1);

            items[Count] = item;
            Count++;
        }

        // Adds multiple elements to the end of the list
        public void AddRange(T[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            EnsureCapacity(Count + elements.Length);

            for (int i = 0; i < elements.Length; i++)
            {
                items[Count + i] = elements[i];
            }

            Count += elements.Length;
        }

        #endregion

        #region Insert Operations

        // Inserts a single element at a specific index
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            EnsureCapacity(Count + 1);

            ShiftRight(index);

            items[index] = item;

            Count++;
        }

        // Inserts multiple elements starting from a specific index
        public void InsertRange(int index, T[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            EnsureCapacity(Count + values.Length);

            for (int i = Count - 1; i >= index; i--)
            {
                items[i + values.Length] = items[i];
            }

            for (int i = 0; i < values.Length; i++)
            {
                items[index + i] = values[i];
            }

            Count += values.Length;
        }

        #endregion

        #region Remove Operations

        // Removes element at a specific index
        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            ShiftLeft(index);

            Count--;

            items[Count] = default(T);

            ShrinkIfNeeded();
        }

        // Removes the first matching element from the list
        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index == -1)
                return false;

            RemoveAt(index);

            return true;
        }

        // Removes a range of elements starting from a specific index
        public void RemoveRange(int index, int count)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count < 0 || index + count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = index; i < Count - count; i++)
            {
                items[i] = items[i + count];
            }

            for (int i = Count - count; i < Count; i++)
            {
                items[i] = default(T);
            }

            Count -= count;

            ShrinkIfNeeded();
        }

        // Removes all elements and resets the list
        public void Clear()
        {
            items = new T[4];
            Count = 0;
        }

        #endregion

        #region Search Operations

        // Returns the index of the first matching element
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item))
                    return i;
            }

            return -1;
        }

        // Checks whether the list contains a specific element
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        #endregion

        #region Utility Operations

        // Reverses the order of active list elements
        public void Reverse()
        {
            int left = 0;
            int right = Count - 1;

            while (left < right)
            {
                T temp = items[left];
                items[left] = items[right];
                items[right] = temp;

                left++;
                right--;
            }
        }

        // Returns a new array containing active elements only
        public T[] ToArray()
        {
            T[] result = new T[Count];

            Array.Copy(items, result, Count);

            return result;
        }

        #endregion

        #region Internal Helper Methods

        // Validates index boundaries before accessing elements
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        // Ensures the internal array has enough storage capacity
        private void EnsureCapacity(int requiredCapacity)
        {
            if (items.Length >= requiredCapacity)
                return;

            int newCapacity = items.Length == 0 ? 4 : items.Length * 2;

            while (newCapacity < requiredCapacity)
            {
                newCapacity *= 2;
            }

            ResizeTo(newCapacity);
        }

        // Resizes the internal array to a specific capacity
        private void ResizeTo(int newCapacity)
        {
            T[] newArray = new T[newCapacity];

            for (int i = 0; i < Count; i++)
            {
                newArray[i] = items[i];
            }

            items = newArray;
        }

        // Shrinks array size when usage becomes very low
        private void ShrinkIfNeeded()
        {
            if (Count <= items.Length / 4 && items.Length > 4)
            {
                int newCapacity = items.Length / 2;

                if (newCapacity < 4)
                    newCapacity = 4;

                ResizeTo(newCapacity);
            }
        }

        // Shifts elements to the left after deletion
        private void ShiftLeft(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                items[i] = items[i + 1];
            }
        }

        // Shifts elements to the right before insertion
        private void ShiftRight(int index)
        {
            for (int i = Count; i > index; i--)
            {
                items[i] = items[i - 1];
            }
        }

        #endregion

        #region Enumeration Support

        // Enables foreach iteration over active elements only
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        // Non-generic enumerator implementation
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}