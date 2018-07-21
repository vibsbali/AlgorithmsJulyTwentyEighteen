using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ArrayListsTests")]
namespace ArrayLists
{
   public class ArrayList<T> : IList<T>
      where T : IComparable<T>
   {
      private T[] backingArray;

      public ArrayList(int length = 0)
      {
         if (length < 4)
         {
            length = 4;
         }

         backingArray = new T[length];
      }


      public IEnumerator<T> GetEnumerator()
      {
         for (int i = 0; i < Count; i++)
         {
            yield return backingArray[i];
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public void Add(T item)
      {
         if (Count == backingArray.Length)
         {
            IncreaseStorage();
         }

         backingArray[Count++] = item;
      }

      private void IncreaseStorage()
      {
         var temp = new T[Count * 2];
         Array.Copy(backingArray, 0, temp, 0, Count);

         backingArray = temp;
      }

      public void Clear()
      {
         backingArray = new T[4];
         Count = 0;
      }

      public bool Contains(T item)
      {
         return IndexOf(item) != -1;
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
         Array.Copy(backingArray, 0, array, arrayIndex, Count);
      }

      public bool Remove(T item)
      {
         var index = this.IndexOf(item);

         if (index > -1)
         {
            RemoveAt(index);
            return true;
         }

         return false;
      }

      public int Count { get; private set; }
      public bool IsReadOnly => false;
      public int IndexOf(T item)
      {
         for (int i = 0; i < Count; i++)
         {
            if (backingArray[i].CompareTo(item) == 0)
            {
               return i;
            }
         }

         return -1;
      }

      public void Insert(int index, T item)
      {
         if (index <0 || index > Count - 1)
         {
            throw new IndexOutOfRangeException();
         }

         if (Count == backingArray.Length)
         {
            IncreaseStorage();
         }

         Array.Copy(backingArray, index, backingArray, index + 1, Count - index);
         backingArray[index] = item;
         ++Count;
      }

      public void RemoveAt(int index)
      {
         if (index > Count -1 || index < 0)
         {
            throw new IndexOutOfRangeException();
         }

         backingArray[index] = default(T);
         --Count;

         Array.Copy(backingArray, index + 1, backingArray, index, Count - index);
         if (backingArray.Length > 4 && Count < backingArray.Length / 2)
         {
            DecreaseStorage();
         }
      }

      private void DecreaseStorage()
      {
         var temp = new T[backingArray.Length * 2 / 3];
         Array.Copy(backingArray, 0, temp, 0, Count);
         backingArray = temp;
      }

      public T this[int index]
      {
         get
         {
            if (index < 0 || index > Count - 1)
            {
               throw new IndexOutOfRangeException();
            }

            return backingArray[index];
         }

         set => Insert(index, value);
      }
   }
}