using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Categorias> Categoria { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public Contexto() : base("ConStr")
        {

        }
    }
}
