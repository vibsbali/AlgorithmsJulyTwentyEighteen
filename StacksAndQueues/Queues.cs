using System;

namespace StacksAndQueues
{
    public class Queues<T>
    {
        private Deque<T> backingStore;

       public Queues(int size = 0)
       {
          if (size <= 4)
          {
             size = 4;
          }

          InitialSize = size;
          backingStore = new Deque<T>(InitialSize);
       }

       public void Enqueue(T item)
       {
            backingStore.EnqueLast(item);
       }
        
       public T Dequeue()
       {
            return backingStore.DequeueFirst();
       }

       public T Peek()
       {
          if (Count == 0)
          {
             throw new InvalidOperationException();
          }

          return backingStore.PeekFront();
       }

        public int Count => backingStore.Count;
       public int InitialSize { get; set; }
    }
}
