using System;
using System.Collections.Generic;

namespace gnericList
{
    public class MyList<T>
    {
        #region fildes$proprties
        private T[] items;
        public int Count { get; private set; }
        public int Capacity
        {
            get { return items.Length; }
        }
        private int currentIndex;

        #endregion

        #region Costructors
        public MyList() : this(4)
        {
        }
        public MyList(int capasity)
        {
            items = new T[capasity];
            Count = 0;
            currentIndex = 0;
        }
        #endregion

        #region indexers
        public T this[int index]
        {
            set
            {
                if (index < 0 || index >= currentIndex)
                    throw new IndexOutOfRangeException();
                items[index] = value;

            }
            get
            {
                if (index < 0 || index >= currentIndex)
                    throw new IndexOutOfRangeException();
                return items[index];
            }
        }
        public List<T> this[string indexes]
        {
            get
            {
                if (string.IsNullOrEmpty(indexes))
                    throw new IndexOutOfRangeException();

                string[] indexesArray = indexes.Split(',');

                List<T> list = new List<T>();

                foreach (string index in indexesArray)
                {
                    if (Convert.ToInt32(index) < 0 || Convert.ToInt32(index) >= Count)
                        continue;

                    if (items[Convert.ToInt32(index)] == null)
                        continue;

                    list.Add(items[Convert.ToInt32(index)]);

                }
                return list;

            }
        }

        #endregion

        #region Methodes
        private T[] Resize(T[] Arr)
        {
            int l = Arr.Length * 2;
            T[] Arr2 = new T[l];
            for (int i = 0; i < Arr.Length; i++)
            {
                Arr2[i] = Arr[i];
            }

            return Arr2;
        }
        private void ResizeTo(int requiredCapacity)
        {
            if (items.Length >= requiredCapacity)
                return;

            T[] newArr = new T[requiredCapacity];

            for (int i = 0; i < items.Length; i++)
            {
                newArr[i] = items[i];
            }

            items = newArr;
        }
        public void Add(T item)
        {
            if (currentIndex >= items.Length)
                items = Resize(items);

            items[currentIndex] = item;
            currentIndex++;
            Count++;

        }

        void ShiftingToLeft(int index)
        {
            for (int i = index; i < currentIndex - 1; i++)
            {
                items[i] = items[i + 1];
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= currentIndex)
                throw new ArgumentOutOfRangeException(nameof(index));
            ShiftingToLeft(index);
            Count--;
            currentIndex--;
            items[Count] = default(T);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < currentIndex; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item))
                    return i;
            }
            return -1;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);

            if (index == -1)
                return false;

            ShiftingToLeft(index);

            Count--;
            currentIndex--;

            items[Count] = default(T);
            return true;
        }

        public void AddRange(T[] elements)
        {
            ResizeTo(Count + elements.Length);

            for (int i = 0; i < elements.Length; i++)
            {
                items[Count + i] = elements[i];
            }
            Count += elements.Length;
            currentIndex += elements.Length;
        }

        public void RemoveRange(int index, int count)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            if (count < 0 || index + count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = index; i < currentIndex - count; i++)
            {
                items[i] = items[i + count];
            }

            for (int i = currentIndex - count; i < currentIndex; i++)
            {
                items[i] = default(T);
            }

            currentIndex -= count;
            Count -= count;
            if (Count > 0 && Count <= items.Length / 4)
            {
                int newSize = items.Length / 2;

                if (newSize < 4)
                    newSize = 4;

                ResizeTo(newSize);
            }

        }


        #endregion



    }
}
