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

            Utils.DAOProducto.obtenerListaProductos();

            comboBox1.DataSource = Utils.DAOMarca.marcas;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";
            

            comboBox2.DataSource = Utils.DAOColor.colores;
            comboBox2.DisplayMember = "color";
            comboBox2.ValueMember = "id";

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            this.Width = 400;
            this.Height = 500;
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
            textStock.Text = prodXM["stock"].ToString();
            textStock.Enabled = false; //desactivamos el stock
            textBox1.Text = prodXM["alerta_stock"].ToString();
        }



        private void EditarProducto_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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


                //Cargo la edicion del producto
                //Utils.DAOProducto.proxm.Rows.Remove(prodXM);
                foreach (DataRow dr in Utils.DAOProducto.proxm.Rows)
                {
                    if (int.Parse(dr["id"].ToString()) == idProdXM)
                    {
                        dr[1] = Convert.ToInt32(comboBox1.SelectedValue);
                        dr[2] = Int32.Parse(producto["Cod_Producto"].ToString());
                        dr[3] = Convert.ToDouble(textPrecio.Text); //revisar esta falla, el precio es float
                        dr[4] = Convert.ToInt32(comboBox2.SelectedValue);
                        dr[5] = Convert.ToInt32(textStock.Text);
                        dr[6] = textCodigo.Text;
                        dr[7] = Convert.ToInt32(textBox1.Text);
                    }
                }

                //Utils.DAOProducto.proxm.Rows.Add(prodXM);
                Utils.DAOProducto.crearProducto();
                Utils.DAOProducto.obtenerProductos();
                Program.main.actualizarAlerta(); //actualizamos el stock
                Dispose();
            }



        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
