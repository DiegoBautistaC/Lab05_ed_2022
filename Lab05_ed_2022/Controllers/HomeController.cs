using Lab05_ed_2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArbolesMulticamino;
using CsvHelper;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Lab05_ed_2022.Controllers;
using Lab05_ed_2022.Models;
using Lab05_ed_2022.Helpers;
using System.IO;
//using Lab05_ed_2022.Helpers

namespace Lab05_ed_2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            Arbol2_3<int> prueba = new Arbol2_3<int>((int v1, int v2) => v1 - v2);
            prueba.Insertar(4);
            prueba.Insertar(5);
            prueba.Insertar(19);
            prueba.Insertar(45);
            prueba.Insertar(46);
            prueba.Insertar(22);
            prueba.Insertar(7);
            prueba.Insertar(14);
            prueba.Insertar(11);
            prueba.Insertar(9);
            prueba.Insertar(8);
            prueba.Insertar(72);
            prueba.Insertar(75);
            prueba.Insertar(77);
            prueba.Insertar(13);
            prueba.Insertar(20);
            prueba.Insertar(25);
            prueba.Insertar(50);
            prueba.Insertar(32);
            prueba.Insertar(1);
            prueba.Insertar(42);
            prueba.Insertar(61);
            prueba.Insertar(52);
            prueba.Insertar(60);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                        Data.Instance.CarroPlaca_Arbol23.Insertar(carro);
                        
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
