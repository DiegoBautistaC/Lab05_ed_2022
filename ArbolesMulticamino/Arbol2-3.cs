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

        Nodo2_3<T> Flow;

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
                nuevaRaiz.MenorLleno = true;
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
                            insertado = true;
                        }
                        else
                        {
                            this.Flow = this.NodoOverflow(valor, raizActual, posicion);
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
                            insertado = true;
                        }
                        else
                        {
                            this.Flow = this.NodoOverflow(valor, raizActual, posicion);
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
                        this.Flow = this.NodoOverflow(valor, raizActual, posicion);
                    }
                }
                else
                {
                    insertado = false;
                }

                //Verificación del Overflow
                if (Flow != null)
                {
                    posicion = this.PosicionInsercion(valor, raizActual);
                    if (raizActual == this.Raiz)
                    {
                        if (!raizActual.MayorLleno)
                        {
                            if (posicion == 1)
                            {
                                raizActual.Mayor = raizActual.Menor;
                                raizActual.Menor = this.Flow.Menor;
                                raizActual.Derecho = raizActual.Medio;
                                raizActual.Izquierdo = this.Flow.Izquierdo;
                                raizActual.Medio = this.Flow.Medio;
                            }
                            else
                            {
                                raizActual.Mayor = this.Flow.Menor;
                                raizActual.Medio = this.Flow.Izquierdo;
                                raizActual.Derecho = this.Flow.Medio;
                            }
                            this.Flow = null;
                            raizActual.MayorLleno = true;
                            insertado = true;
                        }
                        else
                        {
                            //Creación de una nueva raiz
                            this.Flow = this.NodoOverflow(valor, raizActual, posicion);
                            this.Raiz = this.Flow;
                            this.Flow = null;
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
                                raizActual.Menor = this.Flow.Menor;
                                raizActual.Derecho = raizActual.Medio;
                                raizActual.Izquierdo = this.Flow.Izquierdo;
                                raizActual.Medio = this.Flow.Medio;
                            }
                            else
                            {
                                raizActual.Mayor = this.Flow.Menor;
                                raizActual.Medio = this.Flow.Izquierdo;
                                raizActual.Derecho = this.Flow.Medio;
                            }
                            this.Flow = null;
                            raizActual.MayorLleno = true;
                            insertado = true;
                        }
                        else
                        {
                            //Validación que no se encuentre en una hoja
                            if (raizActual.Izquierdo != null && raizActual.Medio != null)
                            {
                                this.Flow = this.NodoOverflow(valor, raizActual, posicion);
                            }
                        }
                    }
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
                    raizOverflow.MenorLleno = true;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(valor);
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                }
                else if (posicion == 2)
                {
                    raizOverflow.Menor = valor;
                    raizOverflow.MenorLleno = true;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                }
                else
                {
                    raizOverflow.Menor = raiz.Mayor;
                    raizOverflow.MenorLleno = true;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = new Nodo2_3<T>(valor);
                }
            }
            else
            {
                if (posicion == 1)
                {
                    raizOverflow.Menor = raiz.Menor;
                    raizOverflow.MenorLleno = true;
                    raizOverflow.Izquierdo = this.Flow;
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                    raizOverflow.Medio.Izquierdo = raiz.Medio;
                    raizOverflow.Medio.Medio = raiz.Derecho;
                }
                else if (posicion == 2)
                {
                    raizOverflow.Menor = this.Flow.Menor;
                    raizOverflow.MenorLleno = true;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = new Nodo2_3<T>(raiz.Mayor);
                    raizOverflow.Izquierdo.Izquierdo = raiz.Izquierdo;
                    raizOverflow.Izquierdo.Medio = this.Flow.Izquierdo;
                    raizOverflow.Medio.Izquierdo = this.Flow.Medio;
                    raizOverflow.Medio.Medio = raiz.Derecho;
                }
                else
                {
                    raizOverflow.Menor = raiz.Mayor;
                    raizOverflow.MenorLleno = true;
                    raizOverflow.Izquierdo = new Nodo2_3<T>(raiz.Menor);
                    raizOverflow.Medio = this.Flow;
                    raizOverflow.Izquierdo.Izquierdo = raiz.Izquierdo;
                    raizOverflow.Izquierdo.Medio = raiz.Medio;
                }
            }

            return raizOverflow;
        }

        public bool Remover(int llave)
        {
            bool eliminado = new bool();
            this.Remover(llave, ref this.Raiz, ref eliminado);
            if (!this.Raiz.MenorLleno)
            {
                this.Raiz = this.Flow;
                this.Flow = null;
                eliminado = true;
            }
            return eliminado;
        }

        void Remover(int llave, ref Nodo2_3<T> raizActual, ref bool eliminado)
        {
            int posicion = this.PosicionInsercion(llave, raizActual);
            T aux = default(T);
            bool auxLleno = false;

            if (posicion == 1) // La llave podría encontrarse en el subarbol IZQUIERDO
            {
                if (raizActual.Izquierdo != null)
                {
                    this.Remover(llave, ref raizActual.Izquierdo, ref eliminado);
                }
                else // No encontró la llave
                {
                    eliminado = false;
                }
            }
            else if (posicion == 2) // La llave podría encontrarse en el subarbol MEDIO
            {
                if (raizActual.Medio != null)
                {
                    this.Remover(llave, ref raizActual.Medio, ref eliminado);
                }
                else // No encontró la llave
                {
                    eliminado = false;
                }
            }
            else if (posicion == 3) // La lave podría encontrarse en el subarbol DERECHO
            {
                if (raizActual.Derecho != null)
                {
                    this.Remover(llave, ref raizActual.Derecho, ref eliminado);
                }
                else // No encontró la llave
                {
                    eliminado = false;
                }
            }
            else //La llave se encuentra en la raiz actual
            {
                if (raizActual.Izquierdo == null) // La llave se encuentra en un nodo hoja
                {
                    if (this.Comparador2(llave, raizActual.Menor) == 0)
                    {
                        if (raizActual.MayorLleno) // Como es nodo hoja y tiene ambos valores se elimina el menor y cambia al mayor de lugar y el proceso finaliza
                        {
                            raizActual.Menor = raizActual.Mayor;
                            raizActual.Mayor = default(T);
                            raizActual.MayorLleno = false;
                            eliminado = true;
                        }
                        else // El nodo queda VACIO
                        {
                            raizActual.Menor = default(T);
                            raizActual.MenorLleno = false;
                        }
                    }
                    else // Como es nodo hoja de los dos valores que tiene se elimina el mayor el proceso se acaba
                    {
                        raizActual.Mayor = default(T);
                        raizActual.MayorLleno = false;
                        eliminado = true;
                    }
                }
                else // La llave se encuentra en un nodo intermedio
                {
                    if (this.Comparador2(llave, raizActual.Menor) == 0) // La llave es el valor MENOR del nodo
                    {
                        raizActual.Menor = this.CambiarMayorDeLosMenores(llave, ref raizActual.Izquierdo);
                        this.Remover(llave, ref raizActual.Izquierdo, ref eliminado);
                    }
                    else // La llave es el valor MAYOR del nodo
                    {
                        aux = this.CambiarMayorDeLosMenores(llave, ref raizActual.Medio);
                        raizActual.Mayor = aux;
                        auxLleno = true;
                        this.Remover(llave, ref raizActual.Medio, ref eliminado);
                    }
                }
            }

            if (raizActual.Izquierdo != null) // La verificación de nodos vacios se realiza en los niveles superiores al nodo hoja donde se eliminó
            {
                posicion = this.PosicionInsercion(llave, raizActual, aux, auxLleno);

                if (posicion == 1)
                {
                    if (!raizActual.Izquierdo.MenorLleno) // El hijo IZQUIERDO quedó underflow
                    {
                        if (raizActual.Medio.MayorLleno) // Se puede realizar una rotación con el hermano MEDIO
                        {
                            raizActual.Izquierdo.Menor = raizActual.Menor;
                            raizActual.Menor = raizActual.Medio.Menor;
                            raizActual.Izquierdo.MenorLleno = true;
                            raizActual.Izquierdo.Izquierdo = this.Flow;
                            raizActual.Izquierdo.Medio = raizActual.Medio.Izquierdo;
                            raizActual.Medio.Menor = raizActual.Medio.Mayor;
                            raizActual.Medio.Mayor = default(T);
                            raizActual.Medio.MayorLleno = false;
                            raizActual.Medio.Izquierdo = raizActual.Medio.Medio;
                            raizActual.Medio.Medio = raizActual.Medio.Derecho;
                            raizActual.Medio.Derecho = null;
                            this.Flow = null;
                            eliminado = true;
                        }
                        else
                        {
                            if (raizActual.MayorLleno) // Rotación que sucede con un nodo que posee tres hijos
                            {
                                Nodo2_3<T> auxiliar = new Nodo2_3<T>(raizActual.Mayor);
                                auxiliar.Izquierdo = new Nodo2_3<T>(raizActual.Menor);
                                auxiliar.Izquierdo.Mayor = raizActual.Medio.Menor;
                                auxiliar.Izquierdo.MayorLleno = true;
                                auxiliar.Izquierdo.Izquierdo = this.Flow;
                                auxiliar.Izquierdo.Medio = raizActual.Medio.Izquierdo;
                                auxiliar.Izquierdo.Derecho = raizActual.Medio.Medio;
                                auxiliar.Medio = raizActual.Derecho;
                                raizActual = auxiliar;
                                this.Flow = null;
                                eliminado = true;
                            }
                            else // La creación de un nodo auxiliar debido al underflow que quedará
                            {
                                Nodo2_3<T> auxiliar = new Nodo2_3<T>(raizActual.Menor);
                                auxiliar.Mayor = raizActual.Medio.Menor;
                                auxiliar.MayorLleno = true;
                                auxiliar.Izquierdo = this.Flow;
                                auxiliar.Medio = raizActual.Medio.Izquierdo;
                                auxiliar.Derecho = raizActual.Medio.Medio;
                                this.Flow = auxiliar;
                                raizActual.Menor = default(T);
                                raizActual.MenorLleno = false;
                            }
                        }
                    }
                }
                else if (posicion == 2)
                {
                    if (!raizActual.Medio.MenorLleno) // El hijo MEDIO quedó underflow
                    {
                        if (raizActual.Izquierdo.MayorLleno) // Se puede realizar rotación con el hijo IZQUIERDO
                        {
                            raizActual.Medio.Menor = raizActual.Menor;
                            raizActual.Menor = raizActual.Izquierdo.Mayor;
                            raizActual.Medio.MenorLleno = true;
                            raizActual.Izquierdo.Mayor = default(T);
                            raizActual.Izquierdo.MayorLleno = false;
                            raizActual.Medio.Medio = this.Flow;
                            raizActual.Medio.Izquierdo = raizActual.Izquierdo.Derecho;
                            raizActual.Izquierdo.Derecho = null;
                            this.Flow = null;
                            eliminado = true;
                        }
                        else if (raizActual.MayorLleno)
                        {
                            if (raizActual.Derecho.MayorLleno) // Se puede realizar rotación con el hijo DERECHO
                            {
                                raizActual.Medio.Menor = raizActual.Mayor;
                                raizActual.Medio.MenorLleno = true;
                                raizActual.Mayor = raizActual.Derecho.Menor;
                                raizActual.Medio.Izquierdo = this.Flow;
                                raizActual.Medio.Medio = raizActual.Derecho.Izquierdo;
                                raizActual.Derecho.Menor = raizActual.Derecho.Mayor;
                                raizActual.Derecho.Mayor = default(T);
                                raizActual.Derecho.MayorLleno = false;
                                raizActual.Derecho.Izquierdo = raizActual.Derecho.Medio;
                                raizActual.Derecho.Medio = raizActual.Derecho.Derecho;
                                raizActual.Derecho.Derecho = null;
                                this.Flow = null;
                                eliminado = true;
                            }
                            else // Debido a que el antecesor tiene los dos valores se puede realizar una rotacion especial
                            {
                                raizActual.Medio.Menor = raizActual.Mayor;
                                raizActual.Medio.MenorLleno = true;
                                raizActual.Mayor = default(T);
                                raizActual.MayorLleno = false;
                                raizActual.Medio.Mayor = raizActual.Derecho.Menor;
                                raizActual.Medio.Izquierdo = this.Flow;
                                raizActual.Medio.Medio = raizActual.Derecho.Izquierdo;
                                raizActual.Medio.Derecho = raizActual.Derecho.Medio;
                                raizActual.Derecho = null;
                                this.Flow = null;
                                eliminado = true;
                                
                            }
                        }
                        else // La creación de un nodo auxiliar debido al underflow que quedará
                        {
                            Nodo2_3<T> auxiliar = new Nodo2_3<T>(raizActual.Izquierdo.Menor);
                            auxiliar.Mayor = raizActual.Menor;
                            auxiliar.MayorLleno = true;
                            auxiliar.Izquierdo = raizActual.Izquierdo.Izquierdo;
                            auxiliar.Medio = raizActual.Izquierdo.Medio;
                            auxiliar.Derecho = this.Flow;
                            this.Flow = auxiliar;
                            raizActual.Menor = default(T);
                            raizActual.MenorLleno = false;
                            
                        }
                    }
                }
                else
                {
                    if (!raizActual.Derecho.MenorLleno) // El hijo DERECHO quedó underflow
                    {
                        if (raizActual.Medio.MayorLleno)
                        {
                            raizActual.Derecho.Menor = raizActual.Mayor;
                            raizActual.Derecho.MenorLleno = true;
                            raizActual.Mayor = raizActual.Medio.Mayor;
                            raizActual.Medio.Mayor = default(T);
                            raizActual.MayorLleno = false;
                            raizActual.Derecho.Izquierdo = raizActual.Medio.Derecho;
                            raizActual.Medio.Derecho = null;
                            raizActual.Derecho.Medio = this.Flow;
                            this.Flow = null;
                            eliminado = true;
                        }
                        else
                        {
                            raizActual.Medio.Mayor = raizActual.Mayor;
                            raizActual.Medio.MayorLleno = true;
                            raizActual.Mayor = default(T);
                            raizActual.MayorLleno = false;
                            raizActual.Medio.Derecho = this.Flow;
                            this.Flow = null;
                            eliminado = true;
                        }
                    }
                }
            }
        }

        //Recibe como parámetro al nodo donde necesita realizar la búsqueda del mayor de los menores según el caso
        T CambiarMayorDeLosMenores(int llave, ref Nodo2_3<T> raiz)
        {
            if (raiz.MayorLleno) // Tiene valor MAYOR
            {
                if (raiz.Derecho != null) // El mayor de los menores se encuentra en el hijo DERECHO
                {
                    return this.CambiarMayorDeLosMenores(llave, ref raiz.Derecho);
                }
                else // El mayor de los menores es el valor MAYOR
                {
                    T aux = raiz.Mayor;
                    raiz.Mayor = this.Encontrar(llave);
                    return aux;
                }
            }
            else // No tiene valor MAYOR
            {
                if (raiz.Medio != null) // El mayor de los menores se encuentra en el hijo MEDIO
                {
                    return this.CambiarMayorDeLosMenores(llave, ref raiz.Medio);
                }
                else // El amyor de los menores es el valor menor
                {
                    T aux = raiz.Menor;
                    raiz.Menor = this.Encontrar(llave);
                    return aux;
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

        int PosicionInsercion(int llave, Nodo2_3<T> raiz, T aux, bool auxLleno)
        {
            if (auxLleno)
            {
                if (this.Comparador(aux, raiz.Mayor) == 0)
                {
                    return 2;
                }
                else
                {
                    return this.PosicionInsercion(llave, raiz);
                }
            }
            else
            {
                return this.PosicionInsercion(llave, raiz);
            }
        }

        public T Encontrar(int llave)
        {
            if (this.Raiz == null)
            {
                return default(T);
            }
            return this.Encontrar(llave, ref this.Raiz);
        }

        T Encontrar(int llave, ref Nodo2_3<T> raizActual)
        {
            int posicion = this.PosicionInsercion(llave, raizActual);
            if (posicion == 0)
            {
                if (this.Comparador2(llave, raizActual.Menor ) == 0)
                {
                    return raizActual.Menor;
                }
                else
                {
                    return raizActual.Mayor;
                }
            }
            else if (posicion == 1)
            {
                if (raizActual.Izquierdo != null)
                {
                    return this.Encontrar(llave, ref raizActual.Izquierdo);
                }
                else
                {
                    return default(T);
                }
            }
            else if (posicion == 2)
            {
                if (raizActual.Medio != null)
                {
                    return this.Encontrar(llave, ref raizActual.Medio);
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                if (raizActual.Derecho != null)
                {
                    return this.Encontrar(llave, ref raizActual.Derecho);
                }
                else
                {
                    return default(T);
                }
            }
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
