using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        internal class Node
        {
            public Node LeftNode { get; internal set; }
            public Node RightNode { get; internal set; }

            public Node(T value)
            {
                Value = value;
            }

            public T Value { get; }
            public bool HasLeftChild => LeftNode != null;
            public bool HasRightChild => RightNode != null;
        }

        internal Node Head { get; private set; }

        public void Add(T item)
        {
            var nodeToAdd = new Node(item);
            if (Head == null)
            {
                Head = nodeToAdd;
            }
            else
            {
                var node = Head;
                while (node != null)
                {
                    if (item.CompareTo(node.Value) < 0)
                    {
                        if (node.HasLeftChild)
                        {
                            node = node.LeftNode;
                        }
                        else
                        {
                            node.LeftNode = nodeToAdd;
                        }
                    }
                    else
                    {
                        if (node.HasRightChild)
                        {
                            node = node.RightNode;
                        }
                        else
                        {
                            node.RightNode = nodeToAdd;
                        }
                    }
                }
            }

            ++Count;
        }

        public bool Remove(T item)
        {
            var currentNode = Head;
            Node previous = null;
            while (currentNode != null)
            {
                //We have found the node
                if (item.CompareTo(currentNode.Value) == 0)
                {
                    //if node to remove has no children
                    if (!currentNode.HasLeftChild && !currentNode.HasRightChild)
                    {
                        //If previous is null we are at head
                        if (previous == null)
                        {
                            Clear();
                            return true;
                        }
                        else
                        {
                            //if current is left of parent then set parent's left child to null
                            if (IsLeftOfParent(currentNode, previous))
                            {
                                previous.LeftNode = null;
                            }
                            //else set right child to null
                            else
                            {
                                previous.RightNode = null;
                            }
                        }
                    }
                    //Node to remove has no right child promote left
                    else if (!currentNode.HasRightChild)
                    {
                        if (previous == null)
                        {
                            Head = currentNode.LeftNode;
                            currentNode = default(Node);
                        }
                        else
                        {
                            if (IsLeftOfParent(currentNode, previous))
                            {
                                previous.LeftNode = currentNode.LeftNode;
                            }
                            else
                            {
                                previous.RightNode = currentNode.LeftNode;
                            }
                        }
                    }
                    //Node to remove has right child
                    else
                    {
                        //check if node to remove has right child which doesn't have a left child
                        if (!currentNode.RightNode.HasLeftChild)
                        {
                            //get the existing left node
                            var currentLeft = currentNode.LeftNode;

                            //create node to replace with along with pointers
                            var nodeToReplaceWith = currentNode.RightNode;
                            nodeToReplaceWith.LeftNode = currentLeft;

                            //If head
                            if (previous == null)
                            {
                                Head = nodeToReplaceWith;
                            }
                            else
                            {
                                if (IsLeftOfParent(currentNode, previous))
                                {
                                    previous.LeftNode = nodeToReplaceWith;
                                }
                                else
                                {
                                    previous.RightNode = nodeToReplaceWith;
                                }
                            }
                        }
                        //current node has a right which has a left child
                        else
                        {
                            var currentLeft = currentNode.LeftNode;
                            var currentRight = currentNode.RightNode;

                            var leftMostNodesParentSoFar = currentRight;
                            var nodeToReplaceWith = currentRight.LeftNode;
                            while (nodeToReplaceWith.HasLeftChild)
                            {
                                leftMostNodesParentSoFar = nodeToReplaceWith;
                                nodeToReplaceWith = nodeToReplaceWith.LeftNode;
                            }

                            //remove the left most node's parent's left to null
                            leftMostNodesParentSoFar.LeftNode = null;
                            nodeToReplaceWith.LeftNode = currentLeft;
                            nodeToReplaceWith.RightNode = currentRight;

                            if (previous == null)
                            {
                                Head = nodeToReplaceWith;
                            }
                            else
                            {
                                if (IsLeftOfParent(currentNode, previous))
                                {
                                    previous.LeftNode = nodeToReplaceWith;
                                }
                                else
                                {
                                    previous.RightNode = nodeToReplaceWith;
                                }
                            }
                        }
                    }

                    --Count;
                    return true;
                }

                if (item.CompareTo(currentNode.Value) < 0)
                {
                    previous = currentNode;
                    currentNode = currentNode.LeftNode;
                }
                else
                {
                    previous = currentNode;
                    currentNode = currentNode.RightNode;
                }
            }

            return false;
        }

        private bool IsLeftOfParent(Node currentNode, Node parent)
        {
            //if parent > current node then current node will be on left
            return parent.Value.CompareTo(currentNode.Value) > 0;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var currentNode = Head;
            while (currentNode != null)
            {
                if (item.CompareTo(currentNode.Value) == 0)
                {
                    return true;
                }

                if (item.CompareTo(currentNode.Value) > 0)
                {
                    currentNode = currentNode.RightNode;
                }
                else
                {
                    currentNode = currentNode.LeftNode;
                }
            }

            return false;
        }

        public uint Count { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> BreadthFirstSearch()
        {
            var auxQueue = new Queue<Node>();
            var finalQueue = new Queue<Node>();

            auxQueue.Enqueue(Head);
            while (auxQueue.Count > 0)
            {
                var currentNode = auxQueue.Dequeue();

                finalQueue.Enqueue(currentNode);
                if (currentNode.HasLeftChild)
                {
                    auxQueue.Enqueue(currentNode.LeftNode);
                }

                if (currentNode.HasRightChild)
                {
                    auxQueue.Enqueue(currentNode.RightNode);
                }
            }

            while (finalQueue.Count > 0)
            {
                yield return finalQueue.Dequeue().Value;
            }
        }

        public IEnumerator<T> DepthFirstSearch()
        {
            var auxQueue = new Queue<Node>();
            var finalStack = new Stack<Node>();

            auxQueue.Enqueue(Head);
            while (auxQueue.Count > 0)
            {
                var currentNode = auxQueue.Dequeue();

                finalStack.Push(currentNode);
                if (currentNode.HasLeftChild)
                {
                    auxQueue.Enqueue(currentNode.LeftNode);
                }

                if (currentNode.HasRightChild)
                {
                    auxQueue.Enqueue(currentNode.RightNode);
                }
            }

            while (finalStack.Count > 0)
            {
                yield return finalStack.Pop().Value;
            }
        }

        public void InOrderTraversal(Action<T> actionToPerform)
        {
            PerformInOrderTravesal(actionToPerform, Head);
        }

        private void PerformInOrderTravesal(Action<T> actionToPerform, Node current)
        {
            if (current == null)
            {
                return;
            }

            PerformInOrderTravesal(actionToPerform, current.LeftNode);
            actionToPerform.Invoke(current.Value);
            PerformInOrderTravesal(actionToPerform, current.RightNode);
        }

        public void PreOrderTraversal(Action<T> actionToPerform)
        {
            PerformPreOrderTraversal(actionToPerform, Head);
        }

        private void PerformPreOrderTraversal(Action<T> actionToPerform, Node current)
        {
            if (current == null)
            {
                return;
            }

            actionToPerform.Invoke(current.Value);
            PerformInOrderTravesal(actionToPerform, current.LeftNode);
            PerformInOrderTravesal(actionToPerform, current.RightNode);
        }

        public void PostOrderTraversal(Action<T> actionToPerform)
        {
            PerformPostOrderTraversal(actionToPerform, Head);
        }

        private void PerformPostOrderTraversal(Action<T> actionToPerform, Node current)
        {
            if (current == null)
            {
                return;
            }

            PerformInOrderTravesal(actionToPerform, current.LeftNode);
            PerformInOrderTravesal(actionToPerform, current.RightNode);
            actionToPerform.Invoke(current.Value);
        }
    }
}

