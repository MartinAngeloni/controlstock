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

namespace ControlStockWFormsApp
{
    public partial class Color : Form
    {
        public Color()
        {
            InitializeComponent();

            Utils.DAOColor.obtenerColores();
            dataGridView1.DataSource = Utils.DAOColor.colores;
            this.dataGridView1.Columns["id"].Visible = false;

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            button3.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan

            this.Width = 600;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!"".Equals(textBox1.Text))
            {
                DataRow nuevo1 = Utils.DAOColor.colores.NewRow();

                nuevo1[1] = textBox1.Text;
                
                Utils.DAOColor.colores.Rows.Add(nuevo1);

                Utils.DAOColor.CrearColor();

                Utils.DAOColor.obtenerColores();

                textBox1.Text = "";
                MessageBox.Show("Color agregado con exito!");
                dataGridView1.DataSource = Utils.DAOColor.colores;

            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count != 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            Utils.DAOColor.CrearColor();
            

            if (Utils.DAOColor.error)
            {
                MessageBox.Show("Hay un producto que esta utilizando este color, no se lo puede eliminar");
                Utils.DAOColor.error = false;
            }
            else
            {
                MessageBox.Show("Eliminacion exitosa");
            }

            Utils.DAOColor.obtenerColores();
            dataGridView1.DataSource = Utils.DAOColor.colores;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Color_Load(object sender, EventArgs e)
        {

        }

        private void Color_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
