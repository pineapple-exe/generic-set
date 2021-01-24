using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GenericSet
{
    public class Set<T> : IEnumerable<T>
    {
        private T[] collection;

        private int next;

        public int Count { get { return next; } }

        public Set(int capacity = 1)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity has to be a positive integer.");
            }
            next = 0;
            collection = new T[capacity];
        }

        public void Insert(T element)
        {
            if (!collection.Contains(element))
            {
                if (next >= collection.Length)
                {
                    T[] expandedCollection = new T[collection.Length * 2];
                    for (int i = 0; i < collection.Length; i++)
                    {
                        expandedCollection[i] = collection[i];
                    }
                    collection = expandedCollection;
                }
                collection[next] = element;
                next++;
            }
        }

        public Set<T> Intersection(Set<T> theOtherSet)
        {
            Set<T> intersection = new Set<T>();

            for (int i = 0; i < next; i++)
            {
                for (int j = 0; j < theOtherSet.next; j++)
                {
                    if (collection[i].Equals(theOtherSet.collection[j]))
                    {
                        intersection.Insert(theOtherSet.collection[j]);
                    }
                }
            }
            return intersection;
        }

        public Set<T> Union(Set<T> theOtherSet)
        {
            Set<T> union = new Set<T>();

            for (int i = 0; i < next; i++)
            {
                union.Insert(collection[i]);
            }

            for (int j = 0; j < theOtherSet.next; j++)
            {
                if (!union.collection.Contains(theOtherSet.collection[j]))
                {
                    union.Insert(theOtherSet.collection[j]);
                }
            }
            return union;
        }

        public Set<T> Difference(Set<T> theOtherSet)
        {
            Set<T> union = Union(theOtherSet);
            Set<T> intersection = Intersection(theOtherSet);

            T[] differenceArray = union.collection.Where(e => !intersection.collection.Contains(e)).ToArray();

            Set<T> difference = new Set<T>(differenceArray.Length);
            
            foreach (T element in differenceArray)
            {
                difference.Insert(element);
            }
            return difference;
        }

        public void Delete(T eToRemove)
        {
            bool found = false;
            int indexOfRemoval = 0;

            for (int i = 0; i < next && !found; i++)
            {
                if (collection[i].Equals(eToRemove))
                {
                    found = true;
                    indexOfRemoval = i;
                }
            }
            if (found)
            {
                for (int j = indexOfRemoval + 1; j < next; j++)
                {
                    collection[j - 1] = collection[j];
                }
                next--;
            }
        }

        public bool SubSet(Set<T> theOtherSet)
        {
            int equivalenceCount = 0;

            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < theOtherSet.Count; j++)
                {
                    if (collection[i].Equals(theOtherSet.collection[j]))
                    {
                        equivalenceCount++;
                    }
                }
            }
            if (equivalenceCount == Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < next; i++)
            { 
                yield return collection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
