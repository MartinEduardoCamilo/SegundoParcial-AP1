using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.Entidades
{
    public class Categorias
    {
        [Key]
        public int CategoriaID { get; set; }
        public string Detalle { get; set; }

        public Categorias(int categoriaID, string detalle)
        {
            this.CategoriaID = categoriaID;
            this.Detalle = detalle;
        }
    }
}
