﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab05_ed_2022.Helpers;

namespace Lab05_ed_2022.Models
{
    public class CarroModel
    {
        [Display(Name = "Placa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La {0} es requerida.")]
        [MinLength(6, ErrorMessage = "El número mínimo de dígitos es de 6.")]
        [MaxLength(6, ErrorMessage = "El número máximo de dígitos es de 6.")]
        public int Placa { get; set; }

        [Display(Name = "Color")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es requerido.")]
        [MaxLength(20, ErrorMessage = "Se ha excedido el límite de caracteres.")]
        public string Color { get; set; }

        [Display(Name = "Propietario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es requerido.")]
        [MinLength(6,ErrorMessage = "Debe ser escrito el nombre completo del propietario.")]
        [MaxLength(25,ErrorMessage = "Se ha excedido el límite de caracteres.")]
        public string Propietario { get; set; }

        [Display(Name = "Latitud")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "La {0} es requerida.")]
        [Range(-90,90,ErrorMessage = "La latitud es un número entre -90 y 90.")]
        public int Latitud { get; set; }

        [Display(Name = "Longitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La {0} es requerida.")]
        [Range(-180, 180, ErrorMessage = "La latitud es un número entre -180 y 180.")]
        public int Longitud { get; set; }

        public static bool Guardar23(CarroModel carro)
        {
            Data.Instance.Arbol23_CarroPlaca.Insertar(carro);
            return true;
        }
    }
}