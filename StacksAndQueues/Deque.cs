using System;

namespace StacksAndQueues
{
    public class Deque<T> 
    {
        T[] _backingStore; // The number of items in the queue.
        public int Count { get; private set; }
        internal int Head { get; private set; } 
        public int Tail { get; private set; }

        public Deque(int size = 0)
        {
            if (size < 4)
            {
                size = 4;
            }
            _backingStore = new T[size];

            Head = 0;
            Tail = _backingStore.Length - 1;
        }

        //Think of it as a linked list add item to front of head
        //so create a new node and add it as previous to head 
        public void EnqueueFirst(T item)
        {
            //We have exhausted the backing store
            if (_backingStore.Length == Count)
            {
                IncreaseStorage();
            }

            if (Head == 0)
            {
                Head = _backingStore.Length - 1;
            }
            else
            {
                --Head;
            }
            
            _backingStore[Head] = item;
            ++Count;
        }

        public T PeekFront()
        {
            if (Count == 0)
            {
                throw new InvalidCastException();
            }

            return _backingStore[Head];
        }

        public T PeekLast()
        {
            if (Count == 0)
            {
                throw new InvalidCastException();
            }

            return _backingStore[Tail];
        }

        private void IncreaseStorage()
        {
            var temp = new T[_backingStore.Length * 2];
            var index = 0;

            //check if head is wrapped
            if (Head > Tail)
            {
                //Start from Head all the way to end of the array and copy
                for (int i = Head; i < _backingStore.Length; i++)
                {
                    temp[index++] = _backingStore[i];
                }
                //Then start from zero and the way to Tail
                for (int i = 0; i <= Tail; i++)
                {
                    temp[index++] = _backingStore[i];
                }
            }
            else
            {
                //Start from Head to Tail 
                for (int i = Head; i <= Tail; i++)
                {
                    temp[index++] = _backingStore[i];
                }
            }
            //extra bump
            --index;

            _backingStore = temp;
            Head = 0;
            Tail = index;
        }

        private void DecreaseStorage()
        {
            var temp = new T[_backingStore.Length * 1/2];
            var index = 0;

            //check if head is wrapped
            if (Head > Tail)
            {
                //Start from Head all the way to end of the array and copy
                for (int i = Head; i < _backingStore.Length; i++)
                {
                    temp[index++] = _backingStore[i];
                }
                //Then start from zero and the way to Tail
                for (int i = 0; i <= Tail; i++)
                {
                    temp[index++] = _backingStore[i];
                }
            }
            else
            {
                //Start from Head to Tail 
                for (int i = Head; i <= Tail; i++)
                {
                    temp[index++] = _backingStore[i];
                }
            }
            //extra bump
            --index;

            _backingStore = temp;
            Head = 0;
            Tail = index;
        }

        //Add item to tail so 
        public void EnqueLast(T item)
        {
            if (_backingStore.Length == Count)
            {
                IncreaseStorage();
            }

            if (Tail == _backingStore.Length - 1)
            {
                Tail = 0;
            }
            else
            {
                Tail++;
            }

            _backingStore[Tail] = item;
            ++Count;
        }


        //remove from head and move head to next
        public T DequeueFront()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }                                  

            var itemToReturn = _backingStore[Head];
            _backingStore[Head] = default(T);
            if (Head == _backingStore.Length - 1)
            {
                Head = 0;
            }
            else
            {
                Head++;
            }

            //if 2/3 array is empty
            if (_backingStore.Length >  4 && Count < _backingStore.Length * 2/3)
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

            var itemToReturn = _backingStore[Tail];
            _backingStore[Tail] = default(T);
            if (Tail == 0)
            {
                Tail = _backingStore.Length - 1; 
            }
            else
            {
                Tail--;
            }

            //if 2/3 array is empty
            if (_backingStore.Length > 4 && Count < _backingStore.Length * 2 / 3)
            {
                DecreaseStorage();
            }

            --Count;
            return itemToReturn;
        }
    }
}
