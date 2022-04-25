using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesMulticamino
{
    public class Arbol2_3<T> : IEnumerable<T>
    {
        Nodo2_3<T> Raiz;

        Nodo2_3<T> OverFlow;

        public delegate int Delegado(T valor1, T valor2);

        public Delegado Comparador;

        public delegate int Delegado2(int valor1, T valor2);

        public Delegado2 Comparador2;

        public Arbol2_3(Delegado unComparador, Delegado2 unComparador2)
        {
            this.Comparador = unComparador;
            this.Comparador2 = unComparador2;
            Raiz = null;
        }

        public bool Insertar(T valor)
        {
            bool insertado = new bool();
            this.Insertar(valor, ref this.Raiz, ref insertado);
            return insertado;
        }

        void Insertar(T valor, ref Nodo2_3<T> raizActual, ref bool insertado)
        {
            if (raizActual == null)
            {
                Nodo2_3<T> nuevaRaiz = new Nodo2_3<T>();
                nuevaRaiz.Menor = valor;
                raizActual = nuevaRaiz;
            }
            else
            {
                int posicion = this.PosicionInsercion(valor, raizActual);
                if (posicion == 1)
                {
                    if (raizActual.Izquierdo != null)
                    {
                         this.Insertar(valor, ref raizActual.Izquierdo, ref insertado);
                    }
                    else
                    {
                        if (!raizActual.MayorLleno)
                        {
                            raizActual.Mayor = raizActual.Menor;
                            raizActual.Menor = valor;
                            raizActual.MayorLleno = true;
                        }
                        else
                        {
                            this.OverFlow = this.NodoOverflow(valor, raizActual, posicion);
                        }
                    }
                }
                else if (posicion == 2)
                {
                    if (raizActual.Medio != null)
                    {
                        this.Insertar(valor, ref raizActual.Medio, ref insertado);
                    }
                    else
                    {
                        if (!raizActual.MayorLleno)
                        {
                            raizActual.Mayor = valor;
                            raizActual.MayorLleno = true;
                        }
                        else
                        {
                            this.OverFlow = this.NodoOverflow(valor, raizActual, posicion);
                        }
                    }
                }
                else if (posicion == 3)
                {
                    if (raizActual.Derecho != null)
                    {
                        this.Insertar(valor, ref raizActual.Derecho, ref insertado);
                    }
                    else
                    {
                        this.OverFlow = this.NodoOverflow(valor, raizActual, posicion);
                    }
                }
                else
                {
                    insertado = false;
                }

                //Verificación del Overflow
                if (OverFlow != null)
                {
                    posicion = this.PosicionInsercion(valor, raizActual);
                    if (raizActual == this.Raiz)
                    {
                        if (!raizActual.MayorLleno)
                        {
                            if (posicion == 1)
                            {
                                raizActual.Mayor = raizActual.Menor;
                                raizActual.Menor = this.OverFlow.Menor;
                                raizActual.Derecho = raizActual.Medio;
                                raizActual.Izquierdo = this.OverFlow.Izquierdo;
                                raizActual.Medio = this.OverFlow.Medio;
                            }
                            else
                            {
                                raizActual.Mayor = this.OverFlow.Menor;
                                raizActual.Medio = this.OverFlow.Izquierdo;
                                raizActual.Derecho = this.OverFlow.Medio;
                            }
                            this.OverFlow = null;
                            raizActual.MayorLleno = true;
                            insertado = true;
                        }
                        else
                        {
                            //Creación de una nueva raiz
                            this.OverFlow = this.NodoOverflow(valor, raizActual, posicion);
                            this.Raiz = this.OverFlow;
                            this.OverFlow = null;
                            insertado = true;
                        }
                    }
                    else
                    {
                        if (!raizActual.MayorLleno)
                        {
                            if (posicion == 1)
                            {
                                raizActual.Mayor = raizActual.Menor;
                                raizActual.Menor = this.OverFlow.Menor;
                                raizActual.Derecho = raizActual.Medio;
                                raizActual.Izquierdo = this.OverFlow.Izquierdo;
                                raizActual.Medio = this.OverFlow.Medio;
                            }
                            else
                            {
                                raizActual.Mayor = this.OverFlow.Menor;
                                raizActual.Medio = this.OverFlow.Izquierdo;
                                raizActual.Derecho = this.OverFlow.Medio;
                            }
                            this.OverFlow = null;
                            raizActual.MayorLleno = true;
                            insertado = true;
                        }
                        else
                        {
                            //Validación que no se encuentre en una hoja
                            if (raizActual.Izquierdo != null && raizActual.Medio != null)
                            {
                                this.OverFlow = this.NodoOverflow(valor, raizActual, posicion);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Función utilizada para determianar la posición ideal en la que debería moverse un valor entre los nodos del árbol que también brinda su valor respecto
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="raiz"></param>
        /// <returns></returns>
        public int PosicionInsercion(T valor, Nodo2_3<T> raiz)
        {
            if (!raiz.MayorLleno)
            {
                if (this.Comparador(valor,raiz.Menor) < 0)
                {
                    return 1;
                }
                else if (this.Comparador(valor, raiz.Menor) > 0)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (this.Comparador(valor,raiz.Menor) == 0 || this.Comparador(valor,raiz.Mayor) == 0)
                {
                    return 0;
                }
                else if (this.Comparador(valor, raiz.Menor) < 0)
                {
                    return 1;
                }
                else if (this.Comparador(valor, raiz.Menor) > 0 && this.Comparador(valor, raiz.Mayor) < 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        int PosicionInsercion(int llave, Nodo2_3<T> raiz)
        {
            if (!raiz.MayorLleno)
            {
                if (this.Comparador2(llave, raiz.Menor) < 0)
                {
                    return 1;
                }
                else if (this.Comparador2(llave, raiz.Menor) > 0)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (this.Comparador2(llave, raiz.Menor) == 0 || this.Comparador2(llave, raiz.Mayor) == 0)
                {
                    return 0;
                }
                else if (this.Comparador2(llave, raiz.Menor) < 0)
                {
                    return 1;
                }
                else if (this.Comparador2(llave, raiz.Menor) > 0 && this.Comparador2(llave, raiz.Mayor) < 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        Nodo2_3<T> NodoOverflow(T valor, Nodo2_3<T> raiz, int posicion)
        {
            Nodo2_3<T> raizOverflow = new Nodo2_3<T>();
            if (raiz.Izquierdo == null && raiz.Medio == null && raiz.Derecho == null)
            {
                if (posicion == 1)
                {
                    raizOverflow.Menor = raiz.Menor;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(valor);
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                }
                else if (posicion == 2)
                {
                    raizOverflow.Menor = valor;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                }
                else
                {
                    raizOverflow.Menor = raiz.Mayor;
                    raizOverflow.Izquierdo =  new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = new Nodo2_3<T>(valor);
                }
            }
            else
            {
                if (posicion == 1)
                {
                    raizOverflow.Menor = raiz.Menor;
                    raizOverflow.Izquierdo = this.OverFlow;
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                    raizOverflow.Medio.Izquierdo = raiz.Medio;
                    raizOverflow.Medio.Medio = raiz.Derecho;
                }
                else if (posicion == 2)
                {
                    raizOverflow.Menor = this.OverFlow.Menor;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                    raizOverflow.Izquierdo.Izquierdo = raiz.Izquierdo;
                    raizOverflow.Izquierdo.Medio = this.OverFlow.Izquierdo;
                    raizOverflow.Medio.Izquierdo = this.OverFlow.Medio;
                    raizOverflow.Medio.Medio = raiz.Derecho;
                }
                else
                {
                    raizOverflow.Menor = raiz.Mayor;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = this.OverFlow;
                    raizOverflow.Izquierdo.Izquierdo = raiz.Izquierdo;
                    raizOverflow.Izquierdo.Medio = raiz.Medio;
                }
            }

            return raizOverflow;
        }
            
        public T Encontrar(int llave)
        {
            return this.Encontrar(llave, ref this.Raiz);
        }

        T Encontrar(int llave, ref Nodo2_3<T> raizActual)
        {
            int posicion = this.PosicionInsercion(llave);
            return();
        }

        void Leer(Nodo2_3 <T> raizActual, ref Queue<T> cola)
        {
            if (raizActual.Izquierdo == null)
            {
                cola.Enqueue(raizActual.Menor);
                if (raizActual.MayorLleno)
                {
                    cola.Enqueue(raizActual.Mayor);
                }
            }
            else
            {
                this.Leer(raizActual.Izquierdo, ref cola);
                cola.Enqueue(raizActual.Menor);
                this.Leer(raizActual.Medio, ref cola);
                if (raizActual.MayorLleno)
                {
                    cola.Enqueue(raizActual.Mayor);
                    this.Leer(raizActual.Derecho, ref cola);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Raiz != null)
            {
                var cola = new Queue<T>();
                this.Leer(this.Raiz, ref cola);
                while (!(cola.Count == 0))
                {
                    yield return cola.Dequeue();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
