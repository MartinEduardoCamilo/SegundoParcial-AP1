using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.Entidades
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; }
        public double Total { get; set; }

        public virtual List<CategoriaDetalle> categoriaDetalle { get; set; }

        public Factura()
        {
            this.FacturaId = 0;
            Fecha = DateTime.Now;
            Estudiante = string.Empty;
            Total = 0;
            categoriaDetalle = new List<CategoriaDetalle>();
        }
    }
}
