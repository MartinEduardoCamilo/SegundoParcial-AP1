using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial.BLL
{
    public class FacturaBLL : RepositorioBase<Factura>
    {
        public override bool Modificar(Factura factura)
        {
            var Anterior = base.db.Factura.Find(factura.FacturaId);

            foreach (var item in Anterior.categoriaDetalle)
            {
                if (!factura.categoriaDetalle.Exists(f => f.CategoriaId == item.CategoriaId))
                    base.db.Entry(item).State = EntityState.Deleted;
            }

            bool paso = base.Modificar(factura);

            return paso;
        }

        public override Factura Buscar(int id)
        {
            Factura factura = new Factura();

            factura = base.db.Factura.Find(id);

            if (factura != null)
                factura.categoriaDetalle.Count();

            return base.Buscar(id);
        }
    }
}
