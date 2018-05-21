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
    public partial class Venta : Form
    {
		DataTable realizandoVenta;
        float totalVenta = 0;

		public Venta()
        {
            InitializeComponent();
            

            label8.AutoSize = false;
            label8.TextAlign = ContentAlignment.MiddleCenter;

            this.Width = 1000;
            this.Height = 680;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla

			Utils.DAOProducto.obtenerProductos();
			Utils.DAOVenta.obtenerVenta();
			Utils.DAOVenta.obtenerProdXVenta();
			Utils.DAOVenta.initRealizandoVenta();

			initDt();
			//dataGridView1.DataSource = Utils.DAOPedido.realizandoPedido;
            dataGridView1.DataSource = Utils.DAOVenta.realizandoVenta;

            comboBox1.Items.Add("Contado");
            comboBox1.Items.Add("Lista");
            comboBox1.Items.Add("Re Venta");
            comboBox1.Items.Add("Tarjeta");
            comboBox1.SelectedIndex = 0;
        }

		private void initDt()
		{
			realizandoVenta = new DataTable();
			realizandoVenta.Columns.Add("Codigo");
			realizandoVenta.Columns.Add("Nombre");
			realizandoVenta.Columns.Add("Marca");
			realizandoVenta.Columns.Add("Modelo");
			realizandoVenta.Columns.Add("Cantidad");
			realizandoVenta.Columns.Add("Precio");
		}

        public void quitarProductoDeLaLista() {
			if (dataGridView1.SelectedRows.Count != 0)
			{
                Utils.DAOVenta.quitarProductoDeLista(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                for (int i = 0; i < realizandoVenta.Rows.Count; i++)
                {
                    DataRow dr = realizandoVenta.Rows[i];
                    if (dr["Codigo"].ToString().Equals(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        realizandoVenta.Rows[i].Delete();

                        Console.WriteLine("asd");
                        break;
                    }
                }
                realizandoVenta.AcceptChanges();
			}
		}

        public void agregarProductoALaLista(int tipo)
        {
            if ("".Equals(textBox1.Text))
            {
                MessageBox.Show("Por favor ingrese el codigo del producto");
            }
            else if ("".Equals(textBox2.Text))
            {
                MessageBox.Show("Por favor ingrese una cantidad");
            }
            else
            {
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
                    if (Utils.DAOProducto.consultarStock(prod["Codigo"].ToString()) >= Int32.Parse(textBox2.Text))
                    {
                        DataRow dr = realizandoVenta.NewRow();
                        dr[0] = prod["Codigo"];
                        dr[1] = prod["Nombre"];
                        dr[2] = prod["Marca"];
                        dr[3] = prod["Modelo"];
                        dr[4] = Int32.Parse(textBox2.Text);
                        if (tipo == 0)
                        {
                            dr["Precio"] = prod["Precio Contado"];
                        }
                        else if (tipo == 1)
                        {
                            dr["Precio"] = prod["Precio Lista"];
                        }
                        else if (tipo == 2)
                        {
                            dr["Precio"] = prod["Precio Re Venta"];
                        }
                        realizandoVenta.Rows.Add(dr);
                        setearPrecios(tipo);
                        Utils.DAOVenta.agregarProductoAlVenta(prod["Codigo"].ToString(), prod["Nombre"].ToString(), prod["Marca"].ToString(), prod["Modelo"].ToString(), Int32.Parse(textBox2.Text), float.Parse(dr["Precio"].ToString()), "-");
                        dataGridView1.DataSource = realizandoVenta;
                    }
                    else
                    {
                        MessageBox.Show("El producto no tiene suficiente stock");
                    }
                }
                else
                {
                    MessageBox.Show("El producto no existe");
                }

            }
        }

        public void setearPrecios(int tipo) {
            foreach (DataRow itemVenta in realizandoVenta.Rows)
            {
                foreach (DataRow dr in Utils.DAOProducto.productos.Rows)
                {
                    
                    if (dr["Codigo"].Equals(itemVenta["Codigo"]))
                    {
                        if (tipo == 0) {
                            itemVenta["Precio"] = dr["Precio Contado"]; 
                        } else if (tipo == 1)
                        {
                            itemVenta["Precio"] = dr["Precio Lista"];
                        } else if (tipo == 2)
                        {
                            itemVenta["Precio"] = dr["Precio Re Venta"];
                        }
                        itemVenta.AcceptChanges();
                        break;
                    }
                }
                foreach (DataRow dr in Utils.DAOVenta.realizandoVenta.Rows)
                {
                    if (dr[0].Equals(itemVenta["Codigo"]))
                    {
                        dr[5] = itemVenta["Precio"];
                        dr.AcceptChanges();
                    }
                }
            }
        }

        public void realizarVenta() {
			Utils.DAOVenta.CrearVenta(totalVenta, float.Parse(textBox3.Text)); 

			if (Utils.DAOVenta.error)
			{
				MessageBox.Show("Ocurrio un error por favor vuelva a intentarlo");
				Utils.DAOVenta.error = false;
			}
			else
			{
				MessageBox.Show("Venta realizada correctamente!");
                Program.main.actualizarAlerta();
				this.Close();
			}
		}

		private void Venta_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SeleccionDeProducto sdp = new SeleccionDeProducto();
            sdp.ShowDialog();
            if (Utils.DAOProducto.productoSeleccionado != null)
            {
                textBox1.Text = Utils.DAOProducto.productoSeleccionado;
                Utils.DAOProducto.productoSeleccionado = null;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void actualizarTotal()
        {
            float total = 0;

                foreach (DataRow dr in Utils.DAOVenta.realizandoVenta.Rows)
                {
                    total += Int32.Parse(dr["Cantidad"].ToString()) * float.Parse(dr["Precio"].ToString());
                }
            

            totalVenta = total;
            textBox3.Text = total.ToString();
            textBox4.Text = total.ToString();
            label5.Text = "$ " + total.ToString();

        }

        private void button3_Click_1(object sender, EventArgs e)
		{

		}

        private void button4_Click(object sender, EventArgs e)
        {
            agregarProductoALaLista(comboBox1.SelectedIndex);
            actualizarTotal();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            quitarProductoDeLaLista();
            actualizarTotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ("".Equals(textBox3.Text))
            {
                MessageBox.Show("Por favor agregue productos a la venta y luego ingrese el monto cobrado");
            }
            if ("".Equals(textBox4.Text))
            {
                MessageBox.Show("Por favor ingrese el dinero recibido por el cliente");
            }
            else
            {
                realizarVenta();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back ))
            { e.Handled = true; }
            
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Historial h = new Historial();
            h.tipo = "venta";
            this.Dispose();
            h.ShowDialog();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox3.Text != "" && Convert.ToDouble(textBox4.Text) - Convert.ToDouble(textBox3.Text) > 0)
                label10.Text = "$" + (Convert.ToDouble(textBox4.Text) - Convert.ToDouble(textBox3.Text)).ToString();
            else
                label10.Text = "$" + 0.ToString();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox3.Text != "" && Convert.ToDouble(textBox4.Text) - Convert.ToDouble(textBox3.Text) > 0)
                label10.Text = "$" + (Convert.ToDouble(textBox4.Text) - Convert.ToDouble(textBox3.Text)).ToString();
            else
                label10.Text = "$" + 0.ToString();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setearPrecios(comboBox1.SelectedIndex);
            actualizarTotal();
        }
    }
}
