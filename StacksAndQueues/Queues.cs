using System;

namespace StacksAndQueues
{
    public class Queues<T>
    {
       private int head = -1;
       private int tail = 0;
       private T[] backingStore;

       public Queues(int size = 0)
       {
          if (size < 4)
          {
             size = 4;
          }

          InitialSize = size;
          backingStore = new T[InitialSize];
       }

       public void Enqueue(T item)
       {
          //We have depleted the backing store
          if (Count == backingStore.Length || head == backingStore.Length)
          {
             //we still have some storage
             if (Count < backingStore.Length)
             {
                head = -1;
                return;
             }

             IncreaseStorage();
          }

         backingStore[++head] = item;
          ++Count;

          
       }

       private void IncreaseStorage()
       {
          var temp = new T[backingStore.Length * 2];
          //check if wrapped
          if (head <= tail)
          {
             var totalElements = backingStore.Length - tail - 1;
             Array.Copy(backingStore, tail, temp, 0, totalElements);
             Array.Copy(backingStore, 0, temp, totalElements + 1, head);
             tail = 0;
             head = Count - 1;
          }
          else
          {
             Array.Copy(backingStore, 0, temp, 0, Count);
          }

          backingStore = temp;
       }

       public T Dequeue()
       {
          var itemToReturn = Peek();
          backingStore[tail++] = default(T);
          --Count;

          if (tail >= backingStore.Length)
          {
             tail = 0;
          }

          if (backingStore.Length > 4 && Count < backingStore.Length / 3)
          {
             DecreaseSize();
          }

         return itemToReturn;
       }

       private void DecreaseSize()
       {
         var temp = new T[backingStore.Length * 1/2];
          if (tail > head)
          {
             throw new ApplicationException("Edge case");
          }

          if (head == tail)
          {
             temp[0] = backingStore[head];
             tail = 0;
             head = 0;
          }else
          {
             var totalElements = head - tail;
             Array.Copy(backingStore, tail, temp, 0, totalElements);
             Array.Copy(backingStore, 0, temp, totalElements + 1, head);
             tail = 0;
             
          }
          

          backingStore = temp;
      }

       public T Peek()
       {
          if (Count == 0)
          {
             throw new InvalidOperationException();
          }

          return backingStore[tail];
       }

       public int Count { get; private set; }
       public int InitialSize { get; set; }
    }
}
