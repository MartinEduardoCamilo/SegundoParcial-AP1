using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.Entidades
{
    public class CategoriaDetalle
    {
        [Key]
        public int CategoriaId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Importe { get; set; }
        public int FacturaId { get; set; }

        public CategoriaDetalle(int categoriaId, int cantidad, double precio, double importe, int facturaId)
        {
            CategoriaId = categoriaId;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
            FacturaId = facturaId;
        }


        public CategoriaDetalle()
        {
            CategoriaId = 0;
            Cantidad = 0;
            Precio = 0;
            Importe = 0;
            FacturaId = 0;
        }
    }
}
