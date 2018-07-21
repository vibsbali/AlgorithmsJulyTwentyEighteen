
using ArrayLists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayListsTests
{
   [TestClass]
   public class ArrayListTests
   {
      [TestMethod]
      public void AssertCountZeroAtInitialization()
      {
         var arrayList = new ArrayList<int>();
         Assert.IsTrue(arrayList.Count == 0);
      }

      [TestMethod]
      public void AddFiveElementsAssertCountFive()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 5; i++)
         {
            arrayList.Add(i);
         }

         Assert.AreEqual(5, arrayList.Count);
      }

      [TestMethod]
      public void AddFiveElementsAssertElementsAddedInOrder()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 5; i++)
         {
            arrayList.Add(i);
         }

         for (int i = 0; i < 5; i++)
         {
            Assert.AreEqual(i, arrayList[i]);
         }
      }

      [TestMethod]
      public void AddFiveElementsTestForEnumeration()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 5; i++)
         {
            arrayList.Add(i);
         }

         var j = 0;
         foreach (var item in arrayList)
         {
            Assert.AreEqual(j, item);
            j++;
         }
      }

      [TestMethod]
      public void AddFiveElementsThenClearAssertInitialSizeAndSizeZero()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 5; i++)
         {
            arrayList.Add(i);
         }

         arrayList.Clear();

         Assert.AreEqual(0, arrayList.Count);
      }

      [TestMethod]
      public void AddFourItemsAssertItemIsPresentAndNonAddedItemIsNotPresent()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 4; i++)
         {
            arrayList.Add(i);
         }

         Assert.IsTrue(arrayList.Contains(1));

         Assert.IsFalse(arrayList.Contains(5));
      }

      [TestMethod]
      public void AddFourItemsInsertItemAtIndexZero()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 4; i++)
         {
            arrayList.Add(i);
         }

         arrayList.Insert(0, 100);
         Assert.IsTrue(arrayList.Contains(100));
         Assert.AreEqual(100, arrayList[0]);

         var j = 0;
         for (int i = 1; i < 5; i++)
         {
            Assert.AreEqual(j, arrayList[i]);
            j++;
         }
      }

      [TestMethod]
      public void AddSixteenItems_RemoveNineItems_AssertCorrectBehaviourOfResize()
      {
         var arrayList = new ArrayList<int>();

         for (int i = 0; i < 16; i++)
         {
            arrayList.Add(i);
         }

         for (int k = 15; k > 6; k--)
         {
            arrayList.RemoveAt(k);
         }

         Assert.AreEqual(7, arrayList.Count);
      }
   }
}
