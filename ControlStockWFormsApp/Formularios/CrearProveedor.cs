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

        public int id = 0;
        private DataRow prov = null;
        public CrearProveedor()
        {
            InitializeComponent();

            textBox1.SelectionStart=0;
            
            this.Width = 750;
            this.Height = 400;
        }

        private void CrearProveedor_Load(object sender, EventArgs e)
        {
            if (id > 0)
            {
                this.Name = "Editar Proveedor";
                foreach (DataRow proveedor in Utils.DAOProveedor.proveedores.Rows)
                {
                    if (Convert.ToInt32(proveedor[0]) == id)
                    {
                        textBox1.Text = proveedor[1].ToString();
                        textBox2.Text = proveedor[2].ToString();
                        textBox3.Text = proveedor[3].ToString();
                        textBox4.Text = proveedor[4].ToString();
                        textBox5.Text = proveedor[5].ToString();
                        textBox7.Text = proveedor[6].ToString(); //martin
                        textBox6.Text = proveedor[7].ToString();
                        prov = proveedor;
                    }
                }
            }
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
                if (id > 0)
                {
                    prov[1] = textBox1.Text;
                    prov[2] = textBox2.Text;
                    prov[3] = textBox3.Text;
                    prov[4] = textBox4.Text;
                    prov[5] = textBox5.Text;
                    prov[6] = textBox7.Text; //martin
                    prov[7] = textBox6.Text;
                } else
                {
                    DataRow proveedor = Utils.DAOProveedor.proveedores.NewRow();
                    //Creo el producto
                    proveedor[1] = textBox1.Text;
                    proveedor[2] = textBox2.Text;
                    proveedor[3] = textBox3.Text;
                    proveedor[4] = textBox4.Text;
                    proveedor[5] = textBox5.Text;
                    proveedor[6] = textBox7.Text; //martin
                    proveedor[7] = textBox6.Text;
                    Utils.DAOProveedor.proveedores.Rows.Add(proveedor);
                }
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
