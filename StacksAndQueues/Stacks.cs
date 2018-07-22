using System;
using System.Collections;
using System.Collections.Generic;

namespace StacksAndQueues
{
   public class Stacks<T> : IEnumerable<T>
   {
      private T[] backingStore;
      private int head = -1;
      public int Count { get; private set; }
      public Stacks(int size)
      {
         if (size < 4)
         {
            size = 4;
         }

         backingStore = new T[size];
      }

      public void Push(T item)
      {
         backingStore[++head] = item;
         if (head > backingStore.Length)
         {
            IncreaseSize();
         }
      }

      private void IncreaseSize()
      {
         if (head == 0)
         {
            throw new InvalidOperationException();
         }
         var temp = new T[backingStore.Length * 2];
         Array.Copy(backingStore, 0, temp, 0, head - 1);
         backingStore = temp;
      }

      public T Pop()
      {
         if (Count == 0)
         {
            throw new InvalidOperationException();
         }

         //if only 1/3 is filled then decrease the size
         if (backingStore.Length > 4 && head < backingStore.Length / 3)
         {
            DecreaseSize();
         }

         return backingStore[--head];
      }

      public T Peek()
      {
         if (Count > 0)
         {
            return backingStore[head - 1];
         }

         throw new InvalidOperationException();
      }

      private void DecreaseSize()
      {
         if (head == 0)
         {
            throw new InvalidOperationException();
         }
         var temp = new T[backingStore.Length / 2];
         Array.Copy(backingStore, 0, temp, 0, head - 1);
         backingStore = temp;
      }


      public IEnumerator<T> GetEnumerator()
      {
         for (int i = head - 1; i >= 0 ; i--)
         {
            yield return backingStore[i];
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}
