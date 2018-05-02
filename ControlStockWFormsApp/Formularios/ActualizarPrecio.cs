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

            //modificar solo 1 columna
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = false;


            this.Height = 600;
            this.Width = 1000;
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
            try
            {
                foreach (DataRow dr in productosDelProveedor.Rows)
                {
                    Utils.DAOProducto.actualizarPrecioProducto(dr[0].ToString(), Convert.ToInt32(dr[6]));
                }
                MessageBox.Show("Precios actualizados con exito!");
                Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error. Por favor verifique los datos ingresados");
            }
            
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        //editar datagridview solo en numeros
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column6_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column6_KeyPress);
                }
            }
        }

        //input solo numeros y numeros negativos datagridview
        private void Column6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar))
        && (e.KeyChar != '.') && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
