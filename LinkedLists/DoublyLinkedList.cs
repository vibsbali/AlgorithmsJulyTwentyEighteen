using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LinkedListTests")]
namespace LinkedLists
{
   public class DoublyLinkedList<T> : ICollection<T>
      where T : IComparable<T>
   {
      internal class Node
      {
         public T Value { get; internal set; }
         public Node Next { get; set; }
         public Node Previous { get; set; }

         public Node(T value)
         {
            Value = value;
         }
      }

      internal Node Head { get; private set; }
      internal Node Tail { get; private set; }

      public IEnumerator<T> GetEnumerator()
      {
         var current = Head;
         while (current != null)
         {
            yield return current.Value;
            current = current.Next;
         }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public void Add(T item)
      {
         var nodeToAdd = new Node(item);

         //If list is empty
         if (Head == null)
         {
            Head = Tail = nodeToAdd;
         }
         else
         {
            Tail.Next = nodeToAdd;
            nodeToAdd.Previous = Tail;
            Tail = nodeToAdd;
         }

         ++Count;
      }

      public void Clear()
      {
         Head = Tail = null;
         Count = 0;
      }

      public bool Contains(T item)
      {
         foreach (var nodeValue in this)
         {
            if (nodeValue.CompareTo(item) == 0)
            {
               return true;
            }
         }

         return false;
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
         foreach (var nodeValue in this)
         {
            array[arrayIndex++] = nodeValue;
         }
      }

      public bool Remove(T item)
      {
         Node current = Head;
         Node previous = null;

         while (current != null)
         {
            if (current.Value.Equals(item))
            {
               //check if it head
               if (previous == null)
               {
                  if (Head == Tail)
                  {
                     Clear();
                     return true;
                  }
                  else
                  {
                     Head = current.Next;
                     Head.Previous = null;
                     current = null;
                  }
               }
               else
               {
                  //check if tail
                  if (current.Next == null)
                  {
                     Tail = previous;
                     Tail.Next = null;
                     current = null;
                  }
                  else
                  {
                     previous.Next = current.Next;
                     current.Next.Previous = previous;
                  }
               }

               --Count;
               return true;
            }

            previous = current;
            current = current.Next;
         }

         return false;
      }

      public int Count { get; private set; }
      public bool IsReadOnly => false;
   }
}
