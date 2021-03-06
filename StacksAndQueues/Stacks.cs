﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace StacksAndQueues
{
    public class Stacks<T>
    {

        private Deque<T> backingStore;

        public Stacks(int size = 0)
        {
            if (size < 4)
            {
                size = 4;
            }
            InitialSize = size;
            backingStore = new Deque<T>(size);
        }

        public int InitialSize { get; set; }

        public void Push(T item)
        {
            backingStore.EnqueueFirst(item);
        }

        public int Count => backingStore.Count;

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
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

    }
}
