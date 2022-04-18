using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesMulticamino
{
    public class Arbol2_3<T>
    {
        public Nodo2_3<T> Raiz;

        public Nodo2_3<T> OverFlow;

        public delegate int Delegado(T valor1, T valor2);

        public Delegado Comparador;

        public Arbol2_3(Delegado unComparador)
        {
            this.Comparador = unComparador;
            Raiz = null;
        }
    }

}
