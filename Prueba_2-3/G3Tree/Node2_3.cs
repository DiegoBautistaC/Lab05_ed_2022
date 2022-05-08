using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba_2_3.G3Tree
{
    public class Node2_3<T>
    {
        public T Minor { get; set; }
        
        public T Mayor { get; set; }

        public Node2_3<T> Overflow;

        public Node2_3<T> First;

        public Node2_3<T> Second;
        
        public Node2_3<T> Third;

        public Node2_3()
        {
            this.First = null;
            this.Second = null;
            this.Third = null;
        }
    }
}
