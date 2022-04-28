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
            return View(Data.Instance.Arbol23_CarroPlaca);
        }

        // GET: CarroController/Details/5
        public ActionResult Details(int id)
        {
            return View(Data.Instance.Arbol23_CarroPlaca.Encontrar(id));
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
                    Propietario = collection["Propietario"],
                    Latitud = Convert.ToInt32(collection["Latitud"]),
                    Longitud = Convert.ToInt32(collection["Longitud"])
                });
                if(validacion)
                {
                    ViewBag.Message = "El vehículo ha sido agregado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No ha sido posible agregar al vehículo, verficar placa.";
                return View();
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inseperado.";
                return View();
            }
        }

        // GET: CarroController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Data.Instance.Arbol23_CarroPlaca.Encontrar(id));
        }

        // POST: CarroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var validacion = CarroModel.Editar(id, Convert.ToInt32(collection["Latitud"]), Convert.ToInt32(collection["Longitud"]));
                if (validacion)
                {
                    ViewBag.Message = "Las coordenadas del vehículo han sido actualizadas correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No se ha podido actualizar las coordenadas correctamente.";
                return View();
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inseperado.";
                return View();
            }
        }

        // GET: CarroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Data.Instance.Arbol23_CarroPlaca.Encontrar(id));
        }

        // POST: CarroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (CarroModel.Remover(id))
                {
                    ViewBag.Message = "El vehículo ha sido borrado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "No ha sido posible borrar al vehículo seleccionado";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inseperado.";
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
                        if (carro.Placa.ToString().Length == 6 && carro.Propietario.Length >= 6 && carro.Propietario.Length <= 25 && carro.Latitud >= -90 && carro.Latitud <= 90 && carro.Longitud >= -180 && carro.Longitud <= 180)
                        {
                            Data.Instance.Arbol23_CarroPlaca.Insertar(carro);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Buscar(int placa)
        {
            try
            {
                if (placa != 0)
                {
                    var vehiculo = Data.Instance.Arbol23_CarroPlaca.Encontrar(placa);
                    if (vehiculo != null)
                    {
                        ViewBag.Message = "Vehículo encontrado exitosamente.";
                        return View(vehiculo);
                    }
                    else
                    {
                        ViewBag.Message = "No se ha encontrado al vehiculo con placa " + placa.ToString();
                        return View();
                    }
                }
                ViewBag.Message = "Debe ingresar una placa para buscar";
                return View();
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inseperado.";
                return View();
            }
        }
    }
}
