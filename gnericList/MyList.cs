using System;
using System.Collections.Generic;

namespace gnericList
{
    public class MyList<T>
    {
        #region fildes$proprties
        public T[] items;
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
        public void Add(T item)
        {
            if (currentIndex >= items.Length)
                items = Resize(items);

            items[currentIndex] = item;
            currentIndex++;
            Count++;

        }

        #endregion
    }
}
