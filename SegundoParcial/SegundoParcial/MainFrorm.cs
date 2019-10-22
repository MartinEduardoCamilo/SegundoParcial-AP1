using SegundoParcial.UI.Consulta;
using SegundoParcial.UI.Registro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegundoParcial
{
    public partial class MainFrorm : Form
    {
        public MainFrorm()
        {
            InitializeComponent();
        }

        private void registroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rRegistroFactura registro = new rRegistroFactura();
            registro.MdiParent = this;
            registro.Show();
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCategorias registro = new cCategorias();
            registro.MdiParent = this;
            registro.Show();
        }

        private void servicionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCategorias categorias = new rCategorias();
            categorias.MdiParent = this;
            categorias.Show();
        }
    }
}
