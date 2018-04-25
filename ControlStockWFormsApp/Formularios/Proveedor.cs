using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStockWFormsApp.Formularios
{
    public partial class Proveedor : Form
    {
        public Proveedor()
        {
            InitializeComponent();

            this.Width = 1000;
            this.Height = 600;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearProveedor crearProveedor = new CrearProveedor();
            crearProveedor.ShowDialog();
        }

        private void Proveedor_Load(object sender, EventArgs e)
        {

        }
    }
}
