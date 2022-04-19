using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArbolesMulticamino;
using Lab05_ed_2022.Models;

namespace Lab05_ed_2022.Helpers
{
    public class Data
    {
        private static Data _instance = null;

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }
        public Arbol2_3<CarroModel> CarroPlaca_Arbol23 = new < CarroModel > ((CarroModel valor1, CarroModel valor2) => { return valor1.Placa.CompareTo(valor2.Placa); }, (valor1, valor2) => valor1.CompareTo(valor2.Placa));
    }
}
