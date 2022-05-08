using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba_2_3.G3Tree
{
    public class Tree2_3<T>
    {
        public Node2_3<T> Root;

        public delegate int FirstDelegate(T value1, T value2);
        FirstDelegate Delegate1;

        public Tree2_3(FirstDelegate firstDelegate)
        {
            this.Root = null;
            this.Delegate1 = firstDelegate;
        }

        public void Insert(T value)
        {
            this.Insert(value, ref this.Root);
        }

        void Insert(T value, ref Node2_3<T> root)
        {
            if (root.Minor == null)
            {
                root.Minor = value;
            }
            else
            {
                var position = this.IdealPosition(value, root);
                switch (position)
                {
                    case 1:
                        if (root.First != null)
                        {
                            this.Insert(value, ref root.First);
                        }
                        else
                        {
                            if (root.Mayor == null)
                            {
                                root.Mayor = root.Minor;
                                root.Minor = value;
                            }
                            else
                            {
                                root.Overflow.Minor = root.Minor;
                                root.Minor = value;
                            }
                        }
                        break;
                    case 2:
                        if (root.Second != null)
                        {
                            this.Insert(value, ref root.Second);
                        }
                        else
                        {
                            if (root.Mayor == null)
                            {
                                root.Mayor = value;
                            }
                            else
                            {
                                root.Overflow.Minor = value;
                            }
                        }
                        break;
                    case 3:
                        if (root.Third != null)
                        {
                            this.Insert(value, ref root.Third);
                        }
                        else
                        {
                            root.Overflow.Minor = root.Mayor;
                            root.Mayor = value;
                        }
                        break;
                    default:
                        break;
                }

                //Comprobación de Overflows
                if (root == this.Root)
                {
                    if (root.Overflow.Minor != null)
                    {
                        Node2_3<T> newRoot = new Node2_3<T>();
                        
                    }
                }
            }
        }

        //De momento la posición le es indiferente si son valores repetidos, si los hay los posiciona lo más a la derecha.
        int IdealPosition(T value, Node2_3<T> root)
        {
            if (root.Mayor == null)
            {
                if (this.Delegate1(value,root.Minor) < 0)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                if (this.Delegate1(value, root.Minor) < 0)
                {
                    return 1;
                }
                else if (this.Delegate1(value, root.Minor) > 0 && this.Delegate1(value, root.Mayor) < 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        public void Remove()
        {

        }
    }
}
