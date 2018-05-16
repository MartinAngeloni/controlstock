using ControlStockWFormsApp.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStockWFormsApp
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
            Utils.DAOProducto.obtenerProductos(); //rellenamos la DataTable de Productos
                     
            dataGridView1.DataSource = Utils.DAOProducto.productos;
  
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["id_producto"].Visible = false;

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button3.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button4.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            //dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(70, 99, 101);
            //dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(109, 84, 79);

            this.Width = 1000;
            this.Height = 600;
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            NuevoProducto nuevoProducto = new NuevoProducto();
            nuevoProducto.ShowDialog();
            dataGridView1.DataSource = Utils.DAOProducto.productos;
        }

        private void Productos_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //sintaxis para busqueda en dataGridView
            DataView dataView = Utils.DAOProducto.productos.DefaultView;
            dataView.RowFilter = string.Format("nombre like '%{0}%' or marca like '%{0}%' or codigo like '%{0}%' or modelo like '%{0}%' or color like '%{0}%'", textBox1.Text); //busqueda por nombre o marca
            dataGridView1.DataSource = dataView;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DataGridViewSelectedRowCollection productoSeleccionado = dataGridView1.SelectedRows;
            
            DataRow prod = Utils.DAOProducto.obtenerProductoById((int)productoSeleccionado[0].Cells["id_producto"].Value);
            DataRow prodxm = Utils.DAOProducto.obtenerProdXMById((int)productoSeleccionado[0].Cells["id"].Value);
            EditarProducto ep = new EditarProducto();


            if (productoSeleccionado.Count > 0 && prod!=null && prodxm!=null)
            {
                ep.producto = prod;
                ep.prodXM = prodxm;
                ep.mostrarProducto((int)productoSeleccionado[0].Cells["id_producto"].Value, (int)productoSeleccionado[0].Cells["id"].Value);
                ep.ShowDialog();
                dataGridView1.DataSource = Utils.DAOProducto.productos;
            }
            else
            {
                MessageBox.Show("Seleccione un producto");
            }
            
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

		private void button4_Click(object sender, EventArgs e)
		{
			
		}

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DialogResult result = MessageBox.Show("Esta seguro de eliminar el producto?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    Utils.DAOProducto.eliminarProducto(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    if (Utils.DAOProducto.error)
                    {
                        MessageBox.Show("El producto tiene una venta o un pedido, no se puede eliminar");
                        Utils.DAOProducto.error = false;
                    }
                    else
                    {
                        dataGridView1.DataSource = Utils.DAOProducto.productos;
                        Program.main.actualizarAlerta();
                    }
                }                
                

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DialogResult result = MessageBox.Show("Esta seguro de eliminar el producto?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    Utils.DAOProducto.darDeBajaProducto(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    if (Utils.DAOProducto.error)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar dar de baja este producto");
                        Utils.DAOProducto.error = false;
                    }
                    else
                    {
                        dataGridView1.DataSource = Utils.DAOProducto.productos;
                        Program.main.actualizarAlerta();
                    }

                }

                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Historial h = new Historial();
            h.tipo = "producto";
            this.Dispose();
            h.ShowDialog();
        }
    }
}
