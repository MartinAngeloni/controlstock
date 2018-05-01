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
    public partial class ActualizarPrecio : Form
    {
        DataTable productosDelProveedor;

        public ActualizarPrecio()
        {
            InitializeComponent();
            Utils.DAOProveedor.obtenerProveedores();
            comboBox1.DataSource = Utils.DAOProveedor.proveedores;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ActualizarPrecio_Load(object sender, EventArgs e)
        {
            inittable(comboBox1.Text);
            dataGridView1.DataSource = productosDelProveedor;
        }


        public void inittable(string nombreProv) {
            productosDelProveedor = new DataTable();
            productosDelProveedor.Columns.Add("Codigo");
            productosDelProveedor.Columns.Add("Producto");
            productosDelProveedor.Columns.Add("Modelo");
            productosDelProveedor.Columns.Add("Marca");
            productosDelProveedor.Columns.Add("Color");
            productosDelProveedor.Columns.Add("Precio");
            productosDelProveedor.Columns.Add("Incremento");

            Utils.DAOProveedor.obtenerListaProductoConProveedor();

            foreach (DataRow dr in Utils.DAOProveedor.listaProductoConProveedor.Rows)
            {
                if (dr[6].ToString().Equals(nombreProv))
                {
                    DataRow linea = productosDelProveedor.NewRow();
                    linea[0] = dr[0];
                    linea[1] = dr[1];
                    linea[2] = dr[2];
                    linea[3] = dr[3];
                    linea[4] = dr[4];
                    linea[5] = dr[5];
                    productosDelProveedor.Rows.Add(linea);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in productosDelProveedor.Rows)
            {
                dr[6] = textBox1.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inittable(comboBox1.Text);
            dataGridView1.DataSource = productosDelProveedor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in productosDelProveedor.Rows)
            {
                Utils.DAOProducto.actualizarPrecioProducto(dr[0].ToString(),Convert.ToInt32(dr[6]));
            }
            Dispose();
        }
    }
}
