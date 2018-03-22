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
    public partial class Inicio : Form
    {

        public Config config;

        public Inicio()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Inicio_Leave(object sender, EventArgs e)
        {

        }

        private void Inicio_Load(object sender, EventArgs e)
        {

            
        }

        private void Inicio_Shown(object sender, EventArgs e)
        {
            config.comprobar();

        }
    }
}
