using LinkedLists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedListTests
{
   [TestClass]
   public class DoublyLinkedListTests
   {
      [TestMethod]
      public void AssertCountZero()
      {
         var linkedList = new DoublyLinkedList<int>();
         Assert.AreEqual(0, linkedList.Count);
      }

      [TestMethod]
      public void AssertHeadTailNull()
      {
         var linkedList = new DoublyLinkedList<int>();
         Assert.IsNull(linkedList.Head);
         Assert.IsNull(linkedList.Tail);
      }

      [TestMethod]
      public void RemoveHeadAssertHeadIsTwo()
      {
         var linkedList = new DoublyLinkedList<int>();
         linkedList.Add(1);
         linkedList.Add(2);
         linkedList.Add(3);
         linkedList.Add(4);

         linkedList.Remove(1);
         Assert.AreEqual(2, linkedList.Head.Value);
         Assert.AreEqual(3, linkedList.Count);

      }


      [TestMethod]
      public void RemoveTailAssertTailIs3()
      {
         var linkedList = new DoublyLinkedList<int>();
         linkedList.Add(1);
         linkedList.Add(2);
         linkedList.Add(3);
         linkedList.Add(4);

         linkedList.Remove(4);
         Assert.AreEqual(3, linkedList.Tail.Value);
         Assert.AreEqual(3, linkedList.Count);
      }

      [TestMethod]
      public void RemoveMiddleAssertHeadAndTailUnchanged()
      {
         var linkedList = new DoublyLinkedList<int>();
         linkedList.Add(1);
         linkedList.Add(2);
         linkedList.Add(3);
         linkedList.Add(4);

         var head = linkedList.Head;
         var tail = linkedList.Tail;

         linkedList.Remove(2);
         Assert.AreEqual(head, linkedList.Head);
         Assert.AreEqual(tail, linkedList.Tail);
         Assert.AreEqual(3, linkedList.Count);
      }

      [TestMethod]
      public void Iteration()
      {
         var linkedList = new DoublyLinkedList<int>();
         linkedList.Add(1);
         linkedList.Add(2);
         linkedList.Add(3);
         linkedList.Add(4);

         var item = 1;
         foreach (var i in linkedList)
         {
            Assert.AreEqual(item, i);
            item++;
         }

      }

   }
}
