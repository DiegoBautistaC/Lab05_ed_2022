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
        public Arbol2_3<CarroModel> Arbol23_CarroPlaca = new Arbol2_3<CarroModel>((CarroModel carro1, CarroModel carro2) => { return carro1.Placa - carro2.Placa; });
        //public Queue<CarroModel> Cola = new Queue<CarroModel>();
    }
}
