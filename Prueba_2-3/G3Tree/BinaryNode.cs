using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba_2_3.G3Tree
{
    public class BinaryNode<T>
    {
        public T Value { get; set; }

        public BinaryNode<T> Left;

        public BinaryNode<T> Rigth;

        public BinaryNode(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Rigth = null;
        }
    }
}
