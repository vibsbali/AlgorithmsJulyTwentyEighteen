using System;

namespace StacksAndQueues
{
    public class Deque<T> 
    {
        T[] backingStore; // The number of items in the queue.

        public int Count { get; private set; }
        internal int Head { get; private set; } 
        public int Tail { get; private set; }

        public Deque(int size = 0)
        {
            if (size < 4)
            {
                size = 4;
            }
            backingStore = new T[size];

            Head = 0;
            Tail = backingStore.Length - 1;
        }

        //Think of it as a linked list add item to front of head
        //so create a new node and add it as previous to head 
        public void EnqueueFront(T item)
        {
            //We have exhausted the backing store
            if (backingStore.Length == Count)
            {
                IncreaseStorage();
            }

            if (Head == 0)
            {
                Head = backingStore.Length - 1;
            }
            else
            {
                --Head;
            }
            
            backingStore[Head] = item;
            ++Count;
        }

        public T PeekFront()
        {
            if (Count == 0)
            {
                throw new InvalidCastException();
            }

            return backingStore[Head];
        }

        public T PeekLast()
        {
            if (Count == 0)
            {
                throw new InvalidCastException();
            }

            return backingStore[Tail];
        }

        private void IncreaseStorage()
        {
            var temp = new T[backingStore.Length * 2];
            var index = 0;

            //check if head is wrapped
            if (Head > Tail)
            {
                //Start from Head all the way to end of the array and copy
                for (int i = Head; i < backingStore.Length; i++)
                {
                    temp[index++] = backingStore[i];
                }
                //Then start from zero and the way to Tail
                for (int i = 0; i <= Tail; i++)
                {
                    temp[index++] = backingStore[i];
                }
            }
            else
            {
                //Start from Head to Tail 
                for (int i = Head; i <= Tail; i++)
                {
                    temp[index++] = backingStore[i];
                }
            }
            //extra bump
            --index;

            backingStore = temp;
            Head = 0;
            Tail = index;
        }

        private void DecreaseStorage()
        {
            var temp = new T[backingStore.Length * 1/2];
            var index = 0;

            //check if head is wrapped
            if (Head > Tail)
            {
                //Start from Head all the way to end of the array and copy
                for (int i = Head; i < backingStore.Length; i++)
                {
                    temp[index++] = backingStore[i];
                }
                //Then start from zero and the way to Tail
                for (int i = 0; i <= Tail; i++)
                {
                    temp[index++] = backingStore[i];
                }
            }
            else
            {
                //Start from Head to Tail 
                for (int i = Head; i <= Tail; i++)
                {
                    temp[index++] = backingStore[i];
                }
            }
            //extra bump
            --index;

            backingStore = temp;
            Head = 0;
            Tail = index;
        }

        //Add item to tail so 
        public void EnqueLast(T item)
        {
            if (backingStore.Length == Count)
            {
                IncreaseStorage();
            }

            if (Tail == backingStore.Length - 1)
            {
                Tail = 0;
            }
            else
            {
                Tail++;
            }

            backingStore[Tail] = item;
            ++Count;
        }


        //remove from head and move head to next
        public T DequeueFront()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }                                  

            var itemToReturn = backingStore[Head];
            backingStore[Head] = default(T);
            if (Head == backingStore.Length - 1)
            {
                Head = 0;
            }
            else
            {
                Head++;
            }

            //if 2/3 array is empty
            if (backingStore.Length >  4 && Count < backingStore.Length * 2/3)
            {
                DecreaseStorage();
            }

            --Count;
            return itemToReturn;
        }

        //remove from tail and move tail to previous
        public T DequeueBack()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var itemToReturn = backingStore[Tail];
            backingStore[Tail] = default(T);
            if (Tail == 0)
            {
                Tail = backingStore.Length - 1; 
            }
            else
            {
                Tail--;
            }

            //if 2/3 array is empty
            if (backingStore.Length > 4 && Count < backingStore.Length * 2 / 3)
            {
                DecreaseStorage();
            }

            --Count;
            return itemToReturn;
        }
    }
}
