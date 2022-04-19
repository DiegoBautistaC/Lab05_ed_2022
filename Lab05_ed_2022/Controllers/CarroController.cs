using Lab05_ed_2022.Helpers;
using Lab05_ed_2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab05_ed_2022.Controllers
{
    public class CarroController : Controller
    {
        // GET: CarroController
        public ActionResult Index()
        {
            Data.Instance.Arbol23_CarroPlaca.Insertar(new CarroModel
            {
                Placa = 366452,
                Color = "Rojo",
                Propietario = "Propietario1",
                Latitud = 54,
                Longitud = 145
            });
            Data.Instance.Arbol23_CarroPlaca.Insertar(new CarroModel
            {
                Placa = 123456,
                Color = "Azul",
                Propietario = "Propietario2",
                Latitud = 90,
                Longitud = 14
            });
            Data.Instance.Arbol23_CarroPlaca.Insertar(new CarroModel
            {
                Placa = 999999,
                Color = "Verde",
                Propietario = "Propietario3",
                Latitud = 64,
                Longitud = 150
            });
            Data.Instance.Arbol23_CarroPlaca.Insertar(new CarroModel
            {
                Placa = 541287,
                Color = "Rosado",
                Propietario = "Propietario4",
                Latitud = 46,
                Longitud = 132
            });
            Data.Instance.Arbol23_CarroPlaca.Insertar(new CarroModel
            {
                Placa = 753159,
                Color = "Magenta",
                Propietario = "Propietario5",
                Latitud = 68,
                Longitud = -180
            }); Data.Instance.Arbol23_CarroPlaca.Insertar(new CarroModel
            {
                Placa = 100000,
                Color = "Rojo",
                Propietario = "Propietario1",
                Latitud = -82,
                Longitud = -55
            });
            return View(Data.Instance.Arbol23_CarroPlaca);
        }

        // GET: CarroController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarroController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
