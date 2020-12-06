using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListDemo
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T data)
        {
            Next = null;
            Data = data;
        }
    }

}
