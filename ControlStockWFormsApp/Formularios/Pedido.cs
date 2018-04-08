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
    public partial class Pedido : Form
    {
        DataTable realizandoPedido;
        public Pedido()
        {
            InitializeComponent();
			Utils.DAOProducto.obtenerProductos();
			Utils.DAOPedido.obtenerPedido();
			Utils.DAOPedido.obtenerProdXPedido();
			Utils.DAOPedido.initRealizandoPedido();

			initDt();
			dataGridView1.DataSource = Utils.DAOPedido.realizandoPedido;

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button3.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button5.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button6.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            this.Width = 1000;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el medio de la pantalla

        }

		private void initDt() {
			realizandoPedido = new DataTable();
			realizandoPedido.Columns.Add("Codigo");
			realizandoPedido.Columns.Add("Nombre");
			realizandoPedido.Columns.Add("Marca");
			realizandoPedido.Columns.Add("Modelo");
			realizandoPedido.Columns.Add("Proveedor");
			realizandoPedido.Columns.Add("Cantidad");
			realizandoPedido.Columns.Add("Precio");
		}

        private void button1_Click(object sender, EventArgs e)
        {
            SeleccionDeProducto sdp = new SeleccionDeProducto();
            sdp.ShowDialog();
            if (Utils.DAOProducto.productoSeleccionado != null)
            {
                textBox1.Text = Utils.DAOProducto.productoSeleccionado;
                Utils.DAOProducto.productoSeleccionado = null;
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
			Console.WriteLine("el");
			if (dataGridView1.SelectedRows.Count != 0) {
				Utils.DAOPedido.quitarProductoDeLista(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
				dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
			}
        }

        private void button3_Click(object sender, EventArgs e)
        {
					
			Utils.DAOPedido.CrearPedido();

			if (Utils.DAOPedido.error)
			{
				MessageBox.Show("Ocurrio un error por favor vuelva a intentarlo");
				Utils.DAOPedido.error = false;
			}
			else {
				MessageBox.Show("Pedido Creado Correctamente");
                Program.main.actualizarAlerta();
                this.Close();
			}
			

        }

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
		{

		}

		private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void button6_Click(object sender, EventArgs e)
		{

			if ("".Equals(textBox1.Text))
			{
				MessageBox.Show("Por favor ingrese el codigo del producto");
			}
			else if ("".Equals(textBox2.Text))
			{
				MessageBox.Show("Por favor ingrese una cantidad");
			}
			else if ("".Equals(textBox3.Text))
			{
				MessageBox.Show("Por favor ingrese el precio");
			}
			else {
				DataRow prod = null;
				foreach (DataRow bs in Utils.DAOProducto.productos.Rows)
				{
					if (bs["Codigo"].Equals(textBox1.Text))
					{
						prod = bs;
					}
				}

				if (prod != null)
				{
					DataRow dr = realizandoPedido.NewRow();
					dr[0] = prod["Codigo"];
					dr[1] = prod["Nombre"];
					dr[2] = prod["Marca"];
					dr[3] = prod["Modelo"];
                    dr[4] = textBox4.Text;
					dr[5] = Int32.Parse(textBox2.Text);
					dr[6] = float.Parse(textBox3.Text);
					realizandoPedido.Rows.Add(dr);

					Utils.DAOPedido.agregarProductoAlPedido(prod["Codigo"].ToString(), prod["Nombre"].ToString(), prod["Marca"].ToString(), prod["Modelo"].ToString(), textBox4.Text + " ", Int32.Parse(textBox2.Text), float.Parse(textBox3.Text), "-");
					dataGridView1.DataSource = realizandoPedido;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
				else {
					MessageBox.Show("Producto no existe");
				}
				
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{

		}

        private void Pedido_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //textBox de precio
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permite solo numeros con comas
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == ','))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == ',' && txtDecimal.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //solo enteros
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            { e.Handled = true; }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Historial h = new Historial();
            h.tipo = "pedido";
            this.Dispose();
            h.ShowDialog();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
