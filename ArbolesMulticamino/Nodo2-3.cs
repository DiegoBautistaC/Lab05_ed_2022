using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesMulticamino
{
    public class Nodo2_3 <T>
    {
        public T Menor { get; set; }
        public T Mayor { get; set; }

        public bool MayorLleno { get; set; }

        public Nodo2_3<T> Izquierdo;

        public Nodo2_3<T> Medio;

        public Nodo2_3<T> Derecho;

        public Nodo2_3()
        {
            Menor = default(T);
            Mayor = default(T);
            MayorLleno = false;
            Izquierdo = null;
            Derecho = null;
            Medio = null;
        }

        public Nodo2_3(T menor)
        {
            Nodo2_3<T> n = new Nodo2_3<T>();
            Menor = menor;
            Mayor = default(T);
            MayorLleno = false;
            Izquierdo = null;
            Derecho = null;
            Medio = null;
        }
    }
}
