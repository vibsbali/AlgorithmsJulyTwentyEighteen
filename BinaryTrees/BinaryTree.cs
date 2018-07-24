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
    }
}

