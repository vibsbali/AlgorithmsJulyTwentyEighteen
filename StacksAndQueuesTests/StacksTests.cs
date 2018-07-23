using Microsoft.VisualStudio.TestTools.UnitTesting;
using StacksAndQueues;

namespace StacksAndQueuesTests
{
    [TestClass]
    public class StacksTests
    {
        [TestMethod]
        public void AssertCountZeroAtInitialization()
        {
            var queue = new Stacks<int>();
            Assert.IsTrue(queue.Count == 0);
        }

        [TestMethod]
        public void AddFiveRemoveFiveAddTenRemoveTenAddFour()
        {
            var stack = new Stacks<int>();
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i);
            }

            for (int i = 4; i >= 0; i--)
            {
                Assert.AreEqual(i, stack.Pop());
            }

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            for (int i = 9; i >= 9; i--)
            {
                Assert.AreEqual(i, stack.Pop());
            }

            for (int i = 0; i < 4; i++)
            {
                stack.Push(i);
            }

            for (int i = 3; i >= 3; i--)
            {
                Assert.AreEqual(i, stack.Pop());
            }
        }

        [TestMethod]
        public void AssertSizeFourAtEmptyInitialization()
        {
            var queue = new Stacks<int>();
            Assert.IsTrue(queue.InitialSize == 4);
        }

        [TestMethod]
        public void AssertSizeSixteenWhenInstantiatedWithSixteenSize()
        {
            var queue = new Stacks<int>(16);
            Assert.IsTrue(queue.InitialSize == 16);
        }

        [TestMethod]
        public void AddFiveElementsAssertCountFive()
        {
            var queue = new Stacks<int>();

            for (int i = 0; i < 5; i++)
            {
                queue.Push(i);
            }

            Assert.AreEqual(5, queue.Count);
        }

        [TestMethod]
        public void AddFiveElementsAssertElementsAddedInOrder()
        {
            var queue = new Stacks<int>();

            for (int i = 0; i < 5; i++)
            {
                queue.Push(i);
            }

            for (int i = 4; i >= 0; i--)
            {
                var item = queue.Pop();
                Assert.AreEqual(i, item);
            }
        }

        [TestMethod]
        public void AddSixteenItems_RemoveNineItems_AssertCorrectBehaviourOfResize()
        {
            var stack = new Stacks<int>();

            for (int i = 0; i < 16; i++)
            {
                stack.Push(i);
            }

            for (int k = 15; k > 6; k--)
            {
                var item = stack.Pop();
            }

            Assert.AreEqual(7, stack.Count);
        }

        [TestMethod]
        public void AddFour_RemoveTwo_AddFour_Assert_CorrectBehviour()
        {
            var stack = new Stacks<int>();

           stack.Push(0);
           stack.Push(1);
           stack.Push(2);
           stack.Push(3);

            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(2, stack.Pop());

            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            Assert.AreEqual(6, stack.Count);



        }

        [TestMethod]
        public void AddThree_RemoveTwo_AddOne_Assert_CorrectBehviourForWrappedHead()
        {
            var stack = new Stacks<int>();

            for (int i = 0; i < 3; i++)
            {
                stack.Push(i);
            }

            for (int k = 2; k > 0; k--)
            {
                var item = stack.Pop();
                Assert.AreEqual(item, k);
            }

            //Add one more
            stack.Push(1);

            for (int i = 1; i >= 0; i--)
            {
                var item = stack.Pop();
                Assert.AreEqual(item, i);
            }

        }
    }
}
