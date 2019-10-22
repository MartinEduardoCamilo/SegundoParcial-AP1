using SegundoParcial.BLL;
using SegundoParcial.Entidades;
using System;
using System.Windows.Forms;

namespace SegundoParcial.UI.Registro
{
    public partial class rCategorias : Form
    {
        public rCategorias()
        {
            InitializeComponent();
        }
        private void Limpiar()
        {
            errorProvider.Clear();

            CategoriaidnumericUpDown.Value = 0;
            DescripciontextBox.Text = string.Empty;
        }

        private void LlenaCampo(Categorias categoria)
        {
            CategoriaidnumericUpDown.Value = categoria.CategoriaId;
            DescripciontextBox.Text = categoria.Decripcion;
        }

        private Categorias LlenaClase()
        {
            Categorias categoria = new Categorias();

            categoria.CategoriaId = Convert.ToInt32(CategoriaidnumericUpDown.Value);
            categoria.Decripcion = DescripciontextBox.Text;

            return categoria;
        }

        private bool Validar()
        {
            errorProvider.Clear();
            bool paso = true;

            if (string.IsNullOrWhiteSpace(DescripciontextBox.Text))
            {
                errorProvider.SetError(DescripciontextBox, "El campo descripcion no puede estar vacio");
                DescripciontextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private bool Existe()
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

            Categorias categoria = repositorio.Buscar((int)CategoriaidnumericUpDown.Value);

            return (categoria != null);
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

            int id;
            int.TryParse(Convert.ToString(CategoriaidnumericUpDown.Value), out id);

            Categorias categoria = repositorio.Buscar(id);

            Limpiar();

            if (categoria != null)
                repositorio.Eliminar(id);
            else
            {
                errorProvider.SetError(CategoriaidnumericUpDown, "No se puede eliminar una categoria que no existe");
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

            bool paso = false;
            errorProvider.Clear();

            if (!Validar())
                return;

            Categorias categoria = LlenaClase();

            if (CategoriaidnumericUpDown.Value == 0)
                paso = repositorio.Guardar(categoria);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("No se encuentra en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                paso = repositorio.Modificar(categoria);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Categoria guardada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No fue posible guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

            int id;
            int.TryParse(Convert.ToString(CategoriaidnumericUpDown.Value), out id);

            Categorias categoria = repositorio.Buscar(id);

            Limpiar();

            if (categoria != null)
                repositorio.Eliminar(id);
            else
            {
                errorProvider.SetError(CategoriaidnumericUpDown, "No se puede eliminar una categoria que no existe");
            }

        }
    }
}
