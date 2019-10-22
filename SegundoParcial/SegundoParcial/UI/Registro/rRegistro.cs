using SegundoParcial.BLL;
using SegundoParcial.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegundoParcial.UI.Registro
{
    public partial class rRegistro : Form
    {
        public List<CategoriaDetalle> Detalle { get; set; }
       
        public rRegistro()
        {
            InitializeComponent();
            this.Detalle = new List<CategoriaDetalle>();
            
        }

        private void CargarGrid()
        {
            DetalledataGridView.DataSource = null;
            DetalledataGridView.DataSource = this.Detalle;
        }

        private void Limpiar()
        {
            errorProvider1.Clear();

            CategoriaidnumericUpDown1.Value = 0;
            EstudiantetextBox.Text = string.Empty;
            ServiciocomboBox.ResetText();
            CantidadtextBox.Text = string.Empty;
            PreciotextBox.Text = string.Empty;
            ImportetextBox.Text = string.Empty;
            TotaltextBox.Text = string.Empty;

            this.Detalle = new List<CategoriaDetalle>();
            CargarGrid();
        }

        private void LlenaCampo(Factura factura)
        {
            CategoriaidnumericUpDown1.Value = factura.FacturaId;
            EstudiantetextBox.Text = factura.Estudiante;
            TotaltextBox.Text = Convert.ToString(factura.Total);
            this.Detalle = factura.categoriaDetalle;

            CargarGrid();
        }

        private Factura LlenaClase()
        {
            Factura factura = new Factura();

            factura.FacturaId = Convert.ToInt32(CategoriaidnumericUpDown1.Value);
            factura.Estudiante = EstudiantetextBox.Text;
            factura.Total = Convert.ToDouble(TotaltextBox.Text);
            factura.categoriaDetalle = this.Detalle;

            return factura;
        }

        private bool Validar()
        {
            errorProvider1.Clear();
            bool paso = true;

            if (string.IsNullOrWhiteSpace(EstudiantetextBox.Text))
            {
                errorProvider1.SetError(EstudiantetextBox, "El campo estudiante no puede estar vacio");
                EstudiantetextBox.Focus();
                paso = false;
            }

            if (this.Detalle.Count == 0)
            {
                errorProvider1.SetError(DetalledataGridView, "Debe de agregar una categoria al detalle");
                DetalledataGridView.Focus();
                paso = false;
            }

            return paso;
        }

        private bool Existe()
        {
            FacturaBLL repositorio = new FacturaBLL();
            Factura factura = repositorio.Buscar((int)CategoriaidnumericUpDown1.Value);
            return (factura != null);
        }

        private bool ValidarDetalle()
        {
            errorProvider1.Clear();
            bool paso = true;

            if (string.IsNullOrWhiteSpace(CantidadtextBox.Text))
            {
                errorProvider1.SetError(CantidadtextBox, "El campo cantidad no puede estar vacio");
                CantidadtextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(PreciotextBox.Text))
            {
                errorProvider1.SetError(PreciotextBox, "El campo precio no puede estar vacio");
                PreciotextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(ServiciocomboBox.Text))
            {
                errorProvider1.SetError(ServiciocomboBox, "El campo categoria no puede estar vacio, debe de seleccionar una categoria");
                ServiciocomboBox.Focus();
                paso = false;
            }

            if (Convert.ToInt32(CantidadtextBox.Text) < 0)
            {
                errorProvider1.SetError(CantidadtextBox, "El campo cantidad no puede ser menor a 0");
                CantidadtextBox.Focus();
                paso = false;
            }

            if (Convert.ToDouble(PreciotextBox.Text) < 0)
            {
                errorProvider1.SetError(PreciotextBox, "El campo precio no puede ser negativo");
                PreciotextBox.Focus();
                paso = false;
            }

            return paso;
        }
     
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            FacturaBLL repositorio = new FacturaBLL();

            int id;
            int.TryParse(Convert.ToString(CategoriaidnumericUpDown1.Value), out id);

            Factura factura = repositorio.Buscar(id);

            Limpiar();

            if (factura != null)
            {
                LlenaCampo(factura);
            }
            else
            {
                MessageBox.Show("Factura no encontrada");
            }
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            double total = 0;

            if (!ValidarDetalle())
                return;

            if (DetalledataGridView.DataSource != null)
                this.Detalle = (List<CategoriaDetalle>)DetalledataGridView.DataSource;

            this.Detalle.Add(
                new CategoriaDetalle(
                    categoriaId: 0,
                    cantidad: Convert.ToInt32(CantidadtextBox.Text),
                    precio: Convert.ToDouble(PreciotextBox.Text),
                    importe: Convert.ToDouble(ImportetextBox.Text),
                    facturaId: Convert.ToInt32(CategoriaidnumericUpDown1.Value)
                    )
                );

            CargarGrid();
            ServiciocomboBox.Focus();
            ServiciocomboBox.ResetText();
            CantidadtextBox.Text = string.Empty;
            PreciotextBox.Text = string.Empty;
            ImportetextBox.Text = string.Empty;

            foreach (var item in this.Detalle)
            {
                total += Convert.ToDouble(item.Importe);
            }

            TotaltextBox.Text = Convert.ToString(total);
        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (DetalledataGridView.Rows.Count > 0 && DetalledataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(DetalledataGridView.CurrentRow.Index);

                CargarGrid();
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            FacturaBLL repositorio = new FacturaBLL();
            bool paso = false;

            if (!Validar())
                return;

            Factura factura = LlenaClase();

            if (CategoriaidnumericUpDown1.Value == 0)
                paso = repositorio.Guardar(factura);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("No se encuentra en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                paso = repositorio.Modificar(factura);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se puede guardar", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            FacturaBLL repositorio = new FacturaBLL();

            int id;
            int.TryParse(Convert.ToString(CategoriaidnumericUpDown1.Value), out id);

            Factura factura = repositorio.Buscar(id);
            Limpiar();

            if (factura != null)
            {
                repositorio.Eliminar(id);
            }
            else
            {
                errorProvider1.SetError(CategoriaidnumericUpDown1, "No se puede eliminar una factura que no existe");
            }
        }

        private void rRegistro_Load(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

            foreach (var item in repositorio.GetList(c => true))
            {
                ServiciocomboBox.Items.Add(item.Decripcion);
            }

            CantidadtextBox.Text = Convert.ToString(0);
            PreciotextBox.Text = Convert.ToString(0);
            ImportetextBox.Text = Convert.ToString(0);
        }

        private void PreciotextBox_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(PreciotextBox.Text) || string.IsNullOrWhiteSpace(CantidadtextBox.Text)))
            {
                if (!(PreciotextBox.Text == "-" || CantidadtextBox.Text == "-"))
                {
                    ImportetextBox.Text = Convert.ToString(Convert.ToDouble(PreciotextBox.Text) * Convert.ToDouble(PreciotextBox.Text));
                }   
            }            
        }
    }
}
