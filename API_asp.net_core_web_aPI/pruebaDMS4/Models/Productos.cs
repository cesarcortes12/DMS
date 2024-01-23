using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace pruebaDMS4.Models
{
    public class Productos
    {
        public int IdProducto { get; set; } = 0;
        public string? NombreProducto { get; set; } = "";
        public int PrecioProducto { get; set; } 
        public int Cantidad { get; set; }
        public int fkIdCliente {  get; set; }
    }
}
