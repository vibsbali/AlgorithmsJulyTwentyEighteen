using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LinkedListTests")]
namespace LinkedLists
{
   public class SinglyLinkedList<T> : ICollection<T>
        where T : IComparable<T>
   {
      internal Node Head { get; set; }
      internal Node Tail { get; set; }
      internal class Node
      {
         internal T Value { get; private set; }
         internal Node Next { get; set; }

         public Node(T value)
         {
            Value = value;
         }
      }

      public int Count { get; private set; }

      public bool IsReadOnly => false;

      public void Add(T item)
      {
         var nodeToAdd = new Node(item);
         if (Head == null)
         {
            Head = Tail = nodeToAdd;
         }
         else
         {
            Tail.Next = nodeToAdd;
            Tail = Tail.Next;
         }

         ++Count;
      }

      public bool Remove(T value)
      {
         if (Head == null)
         {
            return false;
         }

         Node previous = null;
         var current = Head;
         while (current != null)
         {
            //match found
            if (current.Value.Equals(value))
            {
               //previous is null i.e. we have found head
               if (previous == null)
               {
                  if (Head == Tail)
                  {
                     Head = Tail = null;
                  }
                  else
                  {
                     Head = current.Next;
                  }
               }
               else 
               {
                  //are we on tail
                  if (current.Next == null)
                  {
                     Tail = previous;
                     Tail.Next = null;
                  }
                  else
                  {
                     previous.Next = current.Next;
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

      public void Clear()
      {
         Head = Tail = null;
         Count = 0;
      }

      public bool Contains(T value)
      {
         foreach (var item in this)
         {
            if (item.CompareTo(value) == 0)
            {
               return true;
            }
         }

         return false;
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
         throw new NotImplementedException();
      }

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
   }
}
