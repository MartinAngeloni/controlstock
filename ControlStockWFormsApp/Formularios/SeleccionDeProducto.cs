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
    public partial class SeleccionDeProducto : Form
    {
        public SeleccionDeProducto()
        {
            InitializeComponent();

			Utils.DAOProducto.obtenerProductos(); //rellenamos la DataTable de Productos

			dataGridView1.DataSource = Utils.DAOProducto.productos;

			button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            this.Width = 900;
            this.Height = 500;

            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla

        }

        private void button1_Click(object sender, EventArgs e)
        {
			if (dataGridView1.SelectedRows.Count != 0)
			{
				Utils.DAOProducto.productoSeleccionado = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
				this.Close();
			}
			
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SeleccionDeProducto_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
