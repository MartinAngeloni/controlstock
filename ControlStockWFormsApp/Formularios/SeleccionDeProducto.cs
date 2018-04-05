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

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["id_producto"].Visible = false;

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

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //sintaxis para busqueda en dataGridView
            DataView dataView = Utils.DAOProducto.productos.DefaultView;
            dataView.RowFilter = string.Format("nombre like '%{0}%' or marca like '%{0}%' or codigo like '%{0}%' or modelo like '%{0}%' or color like '%{0}%'", textBox1.Text); //busqueda por nombre o marca
            dataGridView1.DataSource = dataView;
        }
    }
}
