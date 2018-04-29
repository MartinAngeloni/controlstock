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
    public partial class CrearProveedor : Form
    {
        public CrearProveedor()
        {
            InitializeComponent();

            this.Width = 600;
            this.Height = 500;
        }

        private void CrearProveedor_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

		private void button1_Click(object sender, EventArgs e)
		{
			if ("".Equals(textBox1.Text))
			{
				MessageBox.Show("Ingrese Un Nombre");
			}
			else if ("".Equals(textBox2.Text))
			{
				MessageBox.Show("Ingrese Cuit");
			}
			else if ("".Equals(textBox3.Text))
			{
				MessageBox.Show("Ingrese Cbu");
			}
			else if ("".Equals(textBox4.Text))
			{
				MessageBox.Show("Ingrese Direccion");
			}
			else if ("".Equals(textBox5.Text))
			{
				MessageBox.Show("Ingrese Telefono");
			}
			else if ("".Equals(textBox6.Text))
			{
				MessageBox.Show("Ingrese Web");
			}
			else
			{
				DataRow proveedor = Utils.DAOProveedor.proveedores.NewRow();
				//Creo el producto
				proveedor[1] = textBox1.Text;
				proveedor[2] = textBox2.Text;
				proveedor[3] = textBox3.Text;
				proveedor[4] = textBox4.Text;
				proveedor[5] = textBox5.Text;
				proveedor[6] = textBox6.Text;
				Utils.DAOProveedor.proveedores.Rows.Add(proveedor);
				Utils.DAOProveedor.crearProveedor();
				Dispose();
			}
		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			Dispose();
		}
	}
}
