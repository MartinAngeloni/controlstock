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
    public partial class Estadistica : Form
    {
        public Estadistica()
        {
            InitializeComponent();

            Utils.Estadisticas.obtenerEstadisticasMensuales();
            label12.Text = Utils.Estadisticas.gananciaNetaDelMesActual.ToString();
            label13.Text = Utils.Estadisticas.productoMasVendidoDelMes.ToString();
            label14.Text = Utils.Estadisticas.productoConMasRecaudo.ToString();
            label15.Text = Utils.Estadisticas.costoPedidosMesActual.ToString();
            label11.Text = Utils.Estadisticas.productoMenosVendidoDelMes.ToString();

            label16.Text = "";
            label17.Text = "";

            //label10.AutoSize = false;
            //label10.TextAlign = ContentAlignment.MiddleCenter;
            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana

            //boton desactivado por problemas
            //button1.Enabled = false;

            this.Width = 1000;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el medio de la pantalla

            

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Estadistica_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils.Estadisticas.obtenerProductoConMasVentasEntreFechas(dateTimePicker1.Value, dateTimePicker2.Value);
            Utils.Estadisticas.obtenerProductoConMenosVentasEntreFechas(dateTimePicker1.Value, dateTimePicker2.Value);

            label16.Text = Utils.Estadisticas.productoMasVendidoEntreFechas;
            label17.Text = Utils.Estadisticas.productoMenosVendidoEntreFechas;
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
