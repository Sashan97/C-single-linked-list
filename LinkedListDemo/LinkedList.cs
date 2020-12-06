using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace LinkedListDemo
{
    public class LinkedList<T>
    {
        private Node<T> head;

        public LinkedList()
        {
            head = null;
        }

        /// <summary>
        /// Adds an element to the head of linked list.
        /// </summary>
        /// <param name="item">Element to add.</param>
        public void AddToStart(T item)
        {
            Node<T> node = new Node<T>(item);

            node.Next = head;
            head = node;
        }

        /// <summary>
        /// Adds an element to the end of linked list.
        /// </summary>
        /// <param name="item">Element to add.</param>
        public void AddToEnd(T item)
        {
            Node<T> node = new Node<T>(item);
            Node<T> current = head;

            if (current == null)
            {
                head = node;
            }
            else
            {
                while (current.Next != null) current = current.Next;
                current.Next = node;
            }
        }

        /// <summary>
        /// Adds an element to the specified position of the linked list.
        /// </summary>
        /// <param name="position">Position in the list.</param>
        /// <param name="item">Element to add.</param>
        public void AddOnPosition(int position, T item)
        {
            if (position < 0) throw new IndexOutOfRangeException();
            else if (position == 0) AddToStart(item);
            else
            {
                Node<T> node = new Node<T>(item);
                Node<T> current = head;
                if(position > 0)
                {
                    for (int i = 0; i < position - 1; i++)
                    {
                        if (current.Next != null) current = current.Next;
                        else throw new IndexOutOfRangeException();
                    }
                }

                node.Next = current.Next;
                current.Next = node;
            }
        }

        /// <summary>
        /// Returns the total amount of elements in the linked list.
        /// </summary>
        /// <returns>Returns the length of the linked list.</returns>
        public int Length()
        {
            if (head == null) return 0;

            int count = 1;
            Node<T> current = head;
            while (current.Next != null)
            {
                count++;
                current = current.Next;
            }
            
            return count;
        }

        /// <summary>
        /// Returns true if list is empty, false, otherwise.
        /// </summary>
        /// <returns>Returns true if list is empty, false, otherwise.</returns>
        public bool IsEmpty()
        {
            return head == null; 
        }

        /// <summary>
        /// Reverses the order of elements in the linked list.
        /// </summary>
        public void Reverse()
        {
            if (head == null) return;

            Node<T> previous = null;
            Node<T> current = head;
            Node<T> temp;
            while (current != null)
            {
                temp = current.Next;
                current.Next = previous;
                previous = current;
                current = temp;
            }
            head = previous;
        }

        /// <summary>
        /// Removes the first element of the linked list.
        /// </summary>
        public void RemoveFromStart()
        {
            if (head != null) head = head.Next;
            else return;
        }

        /// <summary>
        /// Removes the last element of the linked list.
        /// </summary>
        public void RemoveFromEnd()
        {
            if (head != null)
            {
                if (head.Next == null) Clear();
                else
                {
                    Node<T> current = head;
                    Node<T> previous = null;
                    while (current.Next != null)
                    {
                        previous = current;
                        current = current.Next;
                    }
                    previous.Next = null;
                }
            }
            else return;
        }

        /// <summary>
        /// Removes the specified element of the linked list.
        /// </summary>
        /// <param name="position">Element to delete.</param>
        public void RemoveAt(int position)
        {
            if (head == null || position < 0) throw new IndexOutOfRangeException();

            Node<T> current = head;
            Node<T> previous = null;
            if(position > 0)
            {
                for (int i = 0; i < position; i++)
                {
                    if (current.Next != null)
                    {
                        previous = current;
                        current = current.Next;
                    }
                    else throw new IndexOutOfRangeException();
                }
                previous.Next = current.Next;
            }
            else RemoveFromStart();
        }

        /// <summary>
        /// Returns the specified element of the linked list.
        /// </summary>
        /// <param name="position">Position of the element to return.</param>
        /// <returns>Returns the list element at the specified position.</returns>
        public T ElementAt(int position)
        {
            int i = 0;
            Node<T> current = head;
            while (current != null)
            {
                if (i == position) return current.Data;
                current = current.Next;
                i++;
            }

            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Removes all elements from the linked list.
        /// </summary>
        public void Clear()
        {
            head = null;
        }
    }
}
