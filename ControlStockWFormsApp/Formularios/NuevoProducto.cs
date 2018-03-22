using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ControlStockWFormsApp
{
    public partial class NuevoProducto : Form
    {
        public NuevoProducto()
        {
            InitializeComponent();

            Utils.DAOMarca.obtenerMarcas();

            Utils.DAOColor.obtenerColores();

            Utils.DAOProducto.obtenerProXm();

            Utils.DAOProducto.obtenerListaProductos();

            comboBox1.DataSource = Utils.DAOMarca.marcas;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";


            comboBox2.DataSource = Utils.DAOColor.colores;
            comboBox2.DisplayMember = "color";
            comboBox2.ValueMember = "id";

            checkBox1.Checked = true;

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            this.Width = 400;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla

        }

		public string generarCodigoProducto() {
			return "";
		}

        private void button1_Click(object sender, EventArgs e) 
        {
            if ("".Equals(textNombre.Text)) {
                MessageBox.Show("Ingrese un nombre");
            }
            else if ("".Equals(textModelo.Text))
            {
                MessageBox.Show("Ingrese un modelo");
            }
            else if ("".Equals(textCodigo.Text) && checkBox1.Checked)
            {
                MessageBox.Show("Ingrese un codigo");
            }
            else if ("".Equals(textBox1.Text))
            {
                MessageBox.Show("Ingrese una alerta de stock");
            }
            else if ("".Equals(textDescripcion.Text))
            {
                MessageBox.Show("Ingrese una descripcion");
            }
            else if ("".Equals(textPrecio.Text))
            {
                MessageBox.Show("Ingrese un precio");
            }
            else if ("".Equals(textStock.Text))
            {
                MessageBox.Show("Ingrese un valor de stock disponible");
            }
            else if (Utils.DAOColor.colores.Rows.Count < 1)
            {
                MessageBox.Show("Cargue colores desde el menu principal");
            }
            else if (Utils.DAOMarca.marcas.Rows.Count < 1)
            {
                MessageBox.Show("Cargue marcas desde el menu principal");
            }
			else if (Utils.DAOProducto.comprobarProducto(textCodigo.Text))
			{
				MessageBox.Show("El Codigo de producto ya existe, elija otro");
			}
			else { 

                DataRow producto = Utils.DAOProducto.listaProducto.NewRow();
                //Creo el producto
                producto[1] = textNombre.Text;
                producto[2] = textModelo.Text;
                producto[3] = textDescripcion.Text;
                Utils.DAOProducto.listaProducto.Rows.Add(producto);
                Utils.DAOProducto.actualizarListaProducto();

                Utils.Variables.conexion.Open();

                string query = @"select IDENT_CURRENT('Producto')";

                SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);

                SqlDataReader dr = cm.ExecuteReader();
                dr.Read();
                int id = Convert.ToInt32(dr[0]);
                Utils.Variables.conexion.Close();

                DataRow proxm = Utils.DAOProducto.proxm.NewRow();

                //Cargo la edicion del producto
                proxm[1] = Convert.ToInt32(comboBox1.SelectedValue);
                proxm[2] = id;
                proxm[3] = Convert.ToDouble(textPrecio.Text); //revisar esta falla, el precio es float
                proxm[4] = Convert.ToInt32(comboBox2.SelectedValue);
                proxm[5] = Convert.ToInt32(textStock.Text);
				if (checkBox1.Checked)
				{
					proxm[6] = textCodigo.Text;
				}
				else {
					proxm[6] = Utils.DAOProducto.generateCodigo();
				}
                
                proxm[7] = Convert.ToInt32(textBox1.Text);
                Utils.DAOProducto.proxm.Rows.Add(proxm);
                Utils.DAOProducto.crearProducto();
                Utils.DAOProducto.obtenerProductos();
                Program.main.actualizarAlerta(); //actualizamos el sto
                Dispose();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NuevoProducto_Load(object sender, EventArgs e)
        {

        }

        private void textDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textCodigo.Enabled = true;
            }
            else
            {
                textCodigo.Enabled = false;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
