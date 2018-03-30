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
    public partial class Marca : Form
    {
        public Marca()
        {
            InitializeComponent();

            Utils.DAOMarca.obtenerMarcas();

            dataGridView1.DataSource = Utils.DAOMarca.marcas;
            this.dataGridView1.Columns["id"].Visible = false;

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button3.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan

            this.Width = 600;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla

        }

        private void Marca_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!"".Equals(textBox1.Text))
            {
                DataRow nuevo1 = Utils.DAOMarca.marcas.NewRow();

                nuevo1[1] = textBox1.Text;

                Utils.DAOMarca.marcas.Rows.Add(nuevo1);

                Utils.DAOMarca.CrearMarca();

                Utils.DAOMarca.obtenerMarcas();

                textBox1.Text = "";
                MessageBox.Show("Marca agregada con exito!");
                dataGridView1.DataSource = Utils.DAOMarca.marcas;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            Utils.DAOMarca.CrearMarca();


            if (Utils.DAOMarca.error)
            {
                MessageBox.Show("Hay un producto que esta utilizando esta marca, no se puede eliminar.");
                Utils.DAOMarca.error = false;
            }
            else
            {
                MessageBox.Show("Eliminacion exitosa!");
            }

            Utils.DAOMarca.obtenerMarcas();
            dataGridView1.DataSource = Utils.DAOMarca.marcas;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
