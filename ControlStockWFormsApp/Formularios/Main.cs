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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Program.main = this;
            Program.started();
            //button1.TextImageRelation = TextImageRelation.ImageBeforeText;

            //color para los botones
            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198,216,175); //verde manzana
            button3.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button4.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button5.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button7.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button8.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            //pantalla maximizada
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.Width = Screen.PrimaryScreen.Bounds.Width; //Obtiene el ancho de la pantalla principal en pixeles. 
            this.Height = Screen.PrimaryScreen.Bounds.Height; //Obtiene el alto de la pantalla principal en pixeles. 

            //titulo en el medio
            label3.AutoSize = false;
            label3.TextAlign = ContentAlignment.MiddleCenter;

            

            Utils.DAOProducto.obtenerProductosBajoStock(); //rellenar dataGridView
            dataGridView1.DataSource = Utils.DAOProducto.productosInAlert; //rellenar dataGridView
            


        }

        //actualiza el dataGridView
        public void actualizarAlerta()
        {
            Utils.DAOProducto.obtenerProductosBajoStock();
            dataGridView1.DataSource = Utils.DAOProducto.productosInAlert;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca();
            marca.ShowDialog();   
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Color color = new Color();
            color.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido();
            pedido.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Productos productos = new Productos();
            productos.ShowDialog();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Color c = new Color();
            c.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Marca m = new Marca();
            m.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Estadistica es = new Estadistica();
            es.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Venta v = new Venta();
            v.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Pedido p = new Pedido();
            p.ShowDialog();
        }
    }
}
