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

namespace SegundoParcial.UI.Consulta
{
    public partial class cCategorias : Form
    {
        public cCategorias()
        {
            InitializeComponent();
        }

        private void Consultabutton_Click(object sender, EventArgs e)
        {
            FacturaBLL repositorio = new FacturaBLL();

            List<Factura> lista = new List<Factura>();

            if (CriteriotextBox.Text.Trim().Length > 0)
            {
                switch (FiltrocomboBox.SelectedIndex)
                {
                    case 0:
                        lista = repositorio.GetList(P => true);
                        break;

                    case 1:
                        int id = Convert.ToInt32(CriteriotextBox.Text);
                        lista = repositorio.GetList(p => p.FacturaId == id);
                        break;

                    case 2:
                        lista = repositorio.GetList(p => p.Estudiante.Contains(CriteriotextBox.Text));
                        break;

                    case 3:
                        double total = Convert.ToDouble(CriteriotextBox.Text);
                        lista = repositorio.GetList(p => p.Total == total);
                        break;
                }

                lista = lista.Where(p => p.Fecha >= DesdedateTimePicker.Value && p.Fecha <= HastadateTimePicker1.Value).ToList();
            }
            else
            {
                lista = repositorio.GetList(p => true);
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;

        }
    }
 }
