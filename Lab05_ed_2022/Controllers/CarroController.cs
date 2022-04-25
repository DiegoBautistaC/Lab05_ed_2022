using CsvHelper;
using Lab05_ed_2022.Helpers;
using Lab05_ed_2022.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public ActionResult Details(int placa)
        {
            return View(Data.Instance.Arbol23_CarroPlaca.Encontrar(placa));
        }

        // GET: CarroController/Create
        public ActionResult Create()
        {
            return View(new CarroModel());
        }

        // POST: CarroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var validacion = CarroModel.Guardar23(new CarroModel
                {
                    Placa = Convert.ToInt32(collection["Placa"]),
                    Color = collection["Color"],
                    Propietario = collection["Proietario"],
                    Latitud = Convert.ToInt32(collection["Latitud"]),
                    Longitud = Convert.ToInt32(collection["Longitud"])
                });
                if(validacion)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
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

        [HttpPost]
        public IActionResult CargarArchivo2_3(IFormFile file, [FromServices] IHostingEnvironment hosting)
        {
            if (file != null)
            {

                string fileName = $"{hosting.WebRootPath}\\Files\\{file.FileName}";
                using (FileStream streamFile = System.IO.File.Create(fileName))
                {
                    file.CopyTo(streamFile);
                    streamFile.Flush();
                }
                var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\Files"}" + "\\" + file.FileName;
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var carro = csv.GetRecord<CarroModel>();
                        Data.Instance.Arbol23_CarroPlaca.Insertar(carro);

                    }
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
