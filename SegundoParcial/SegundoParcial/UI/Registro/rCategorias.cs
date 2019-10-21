using SegundoParcial.BLL;
using SegundoParcial.DAL;
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
    public partial class rCategorias : Form
    {
        RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();
        public rCategorias()
        {
            InitializeComponent();
        }


        private void Limpiar()
        {
            CategoriaidnumericUpDown.Value = 0;
            DetalletextBox.Text = string.Empty;
        }

        private void LlenaCampos(Categorias categoria)
        {
            CategoriaidnumericUpDown.Value = categoria.CategoriaID;
            DetalletextBox.Text = categoria.Detalle;
        }

        private Categorias LlenaClase()
        {
            Categorias categoria = new Categorias();
            categoria.CategoriaID = Convert.ToInt32(CategoriaidnumericUpDown.Value);
            categoria.Detalle = DetalletextBox.Text;
            return categoria;
        }

        private bool Validar()
        {
            bool realizado = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(DetalletextBox.Text))
            {
                errorProvider.SetError(DetalletextBox, "el camppo detalle no debe estar vacio");
                DetalletextBox.Focus();
                realizado = false;
            }

            return realizado;
        }

        private bool Existe()
        {
            Categorias categoria = repositorio.Buscar((int)CategoriaidnumericUpDown.Value);
            return (categoria != null);
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Categorias categoria = new Categorias();

            repositorio = new RepositorioBase<Categorias>();
            int.TryParse(CategoriaidnumericUpDown.Text, out id);

            Limpiar();

            categoria = repositorio.Buscar(id);

            if (categoria != null)
            {
                LlenaCampos(categoria);
            }
            else
            {
                MessageBox.Show("Categoria no encontrada");
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Categorias categoria = new Categorias();
            bool realizado = false;

            if (!Validar())
                return;

            categoria = LlenaClase();


            if (CategoriaidnumericUpDown.Value == 0)
                realizado = repositorio.Guardar(categoria);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("no se puede modificar una categoria que no existe", "fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                realizado = repositorio.Modificar(categoria);
            }

            if (realizado)
            {
                Limpiar();
                MessageBox.Show("CGuardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pude guadar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            int id;
            int.TryParse(CategoriaidnumericUpDown.Text, out id);
            Contexto db = new Contexto();

            Limpiar();

            if (repositorio.Eliminar(id))
            {
                MessageBox.Show("Eliminada correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                errorProvider.SetError(CategoriaidnumericUpDown, "No se puede eliminar una categoria inexistente");
            }
        }
    }
}
