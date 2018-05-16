using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStockWFormsApp.Formularios
{
    public partial class EditarProducto : Form
    {
        public DataRow producto;
        public DataRow prodXM;
        int idProd;
        int idProdXM;

        public EditarProducto()
        {
            InitializeComponent();

            Utils.DAOMarca.obtenerMarcas();

            Utils.DAOColor.obtenerColores();

            Utils.DAOProducto.obtenerProXm();

            Utils.DAOProveedor.obtenerProveedores();

            Utils.DAOProducto.obtenerListaProductos();

            comboBox1.DataSource = Utils.DAOMarca.marcas;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";
            

            comboBox2.DataSource = Utils.DAOColor.colores;
            comboBox2.DisplayMember = "color";
            comboBox2.ValueMember = "id";


            comboBox3.DataSource = Utils.DAOProveedor.proveedores;
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "id";


            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            this.Width = 500;
            this.Height = 550;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla
        }

        public void mostrarProducto(int idProd,int idProdxm)
        {
            this.idProd = idProd;
            this.idProdXM = idProdxm;
            textNombre.Text = producto["Nombre"].ToString();
            textModelo.Text = producto["Modelo"].ToString();
            textDescripcion.Text = producto["Descripcion"].ToString();


            comboBox2.SelectedValue = Int32.Parse(prodXM["id_color"].ToString());
            comboBox1.SelectedValue = Int32.Parse(prodXM["id_marca"].ToString());


            textCodigo.Text = prodXM["codigo"].ToString();
            textCodigo.Enabled = false; //desactivamos el id
            textPrecio.Text = prodXM["precio"].ToString();
            textStock.Text = prodXM["precio_contado"].ToString();
            textBox2.Text = prodXM["precio_lista"].ToString();
            textBox3.Text = prodXM["precio_re_venta"].ToString();
            textBox1.Text = prodXM["alerta_stock"].ToString();

            Utils.DAOProveedor.obtenerProdXProveedor();

            List<int> listaIdProv = new List<int>();

            foreach (DataRow dr in Utils.DAOProveedor.prodXProveedor.Rows) {
                if (Convert.ToInt32(dr["id_producto"]) == idProdxm) {
                    listaIdProv.Add(Convert.ToInt32(dr["id_proveedor"]));
                }
            }

            foreach (int proveed in listaIdProv)
            {
                foreach (DataRow dr in Utils.DAOProveedor.proveedores.Rows)
                {
                    if (Convert.ToInt32(dr["id"]) == proveed)
                    {
                        listBox1.Items.Add(dr["nombre"].ToString());
                        break;
                    }
                }
            }

        }



        private void EditarProducto_Load(object sender, EventArgs e)
        {
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permite solo numeros enteros
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            { e.Handled = true; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if ("".Equals(textNombre.Text))
            {
                MessageBox.Show("Ingrese un nombre");
            }
            else if ("".Equals(textModelo.Text))
            {
                MessageBox.Show("Ingrese un modelo");
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
            else if ("".Equals(textBox2.Text))
            {
                MessageBox.Show("Ingrese un precio de lista");
            }
            else if ("".Equals(textBox3.Text))
            {
                MessageBox.Show("Ingrese un precio de pre venta");
            }
            else if ("".Equals(textStock.Text))
            {
                MessageBox.Show("Ingrese un precio de contado");
            }
            else if (Utils.DAOColor.colores.Rows.Count < 1)
            {
                MessageBox.Show("Cargue colores desde el menu principal");
            }
            else if (Utils.DAOMarca.marcas.Rows.Count < 1)
            {
                MessageBox.Show("Cargue marcas desde el menu principal");
            }
            else if (listBox1.Items.Count < 1)
            {
                MessageBox.Show("Por favor agregue al menos un proveedor");
            }
            else
            {
                //Utils.DAOProducto.listaProducto.Rows.Remove(producto);

                //Creo el producto
                foreach (DataRow dr in Utils.DAOProducto.listaProducto.Rows)
                {
                    if (int.Parse(dr["Cod_Producto"].ToString()) == idProd)
                    {
                        dr[1] = textNombre.Text;
                        dr[2] = textModelo.Text;
                        dr[3] = textDescripcion.Text;
                    }
                }
                Utils.DAOProducto.actualizarListaProducto();
                int id = 0;

                //Cargo la edicion del producto
                //Utils.DAOProducto.proxm.Rows.Remove(prodXM);
                foreach (DataRow dr in Utils.DAOProducto.proxm.Rows)
                {
                    if (int.Parse(dr["id"].ToString()) == idProdXM)
                    {
                        id = Convert.ToInt32(dr[0]);
                        dr[1] = Convert.ToInt32(comboBox1.SelectedValue);
                        dr[2] = Int32.Parse(producto["Cod_Producto"].ToString());
                        dr[3] = Convert.ToDouble(textPrecio.Text); //revisar esta falla, el precio es float
                        dr[4] = Convert.ToInt32(comboBox2.SelectedValue);
                        dr[5] = Convert.ToInt32(textStock.Text);
                        dr[6] = textCodigo.Text;
                        dr[7] = Convert.ToInt32(textBox1.Text);
                        dr[9] = Convert.ToInt32(textBox2.Text);
                        dr[10] = Convert.ToInt32(textStock.Text);
                        dr[11] = Convert.ToInt32(textBox3.Text);
                    }
                }

                //Utils.DAOProducto.proxm.Rows.Add(prodXM);
                Utils.DAOProducto.crearProducto();
                Utils.DAOProducto.obtenerProductos();
                Program.main.actualizarAlerta(); //actualizamos el stock

                //quitan proveedores viejos
                Utils.Variables.conexion.Open();
                String sqlrxv = "delete from ProdXProveedor where id_producto = " + id;
                SqlCommand cmd1 = new SqlCommand(sqlrxv, Utils.Variables.conexion);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                Utils.Variables.conexion.Close();

                // Agregando el proveedor
                Utils.DAOProveedor.obtenerProdXProveedor();
                for (int x = 0; x < listBox1.Items.Count; x++)
                {
                    DataRow dp = Utils.DAOProveedor.prodXProveedor.NewRow();
                    dp[1] = id;
                    foreach (DataRow proveedor in Utils.DAOProveedor.proveedores.Rows)
                    {
                        if (proveedor[1].ToString().Equals(listBox1.Items[x].ToString()))
                        {
                            dp[2] = proveedor[0];
                        }
                    }
                    Utils.DAOProveedor.prodXProveedor.Rows.Add(dp);
                }
                Utils.DAOProveedor.crearProdXProveedor();
                Dispose();
            }



        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!listBox1.Items.Contains(comboBox3.Text.ToString()))
            {
                listBox1.Items.Add(comboBox3.Text.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textPrecio_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo enteros
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            { e.Handled = true; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo enteros
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            { e.Handled = true; }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo enteros
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            { e.Handled = true; }
        }

        //textbox alerta de stock
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //solo enteros
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            { e.Handled = true; }
        }
    }
}
