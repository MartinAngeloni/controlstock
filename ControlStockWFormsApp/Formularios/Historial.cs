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
    public partial class Historial : Form
    {

        public String tipo = null;

        public Historial()
        {
            InitializeComponent();

            this.Width = 900;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla

        }

        private void Historial_Load(object sender, EventArgs e)
        {


            if (tipo == null) {
                Dispose();
            }
            else
            {
                if ("producto".Equals(tipo))
                {

                    Utils.DAOProducto.obtenerProductosDadosDeBaja();
                    dataGridView1.DataSource = Utils.DAOProducto.productosBaja;
                    this.Text = "Historial de Producto";
                    this.generico.Text = "Restaurar";
                }

                if ("venta".Equals(tipo))
                {

                    Utils.DAOVenta.obtenerHistorialVentas();
                    dataGridView1.DataSource = Utils.DAOVenta.historialVenta;
                    this.Text = "Historial de venta";
                    this.generico.Text = "Restaurar";
                }

                if ("pedido".Equals(tipo))
                {

                    Utils.DAOPedido.obtenerHistorialPedido();
                    dataGridView1.DataSource = Utils.DAOPedido.historialPedido;
                    this.Text = "Historial de pedido";
                    this.generico.Text = "Restaurar";
                }

            }


        }

        private void generico_Click(object sender, EventArgs e)
        {
            if ("producto".Equals(tipo))
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    DialogResult result = MessageBox.Show("Esta seguro de restaurar el producto?", "Confirmacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                    {
                        Utils.DAOProducto.restaurarProducto(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        if (Utils.DAOProducto.error)
                        {
                            MessageBox.Show("Ha ocurrido un error al intentar restaurar este producto");
                            Utils.DAOProducto.error = false;
                        }
                        else
                        {
                            dataGridView1.DataSource = Utils.DAOProducto.productosBaja;
                            Program.main.actualizarAlerta();
                        }

                    }


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Historial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tipo == null)
            {
                Dispose();
            }
            else
            {
                if ("producto".Equals(tipo))
                {
                    Productos p = new Productos();
                    this.Dispose();
                    p.ShowDialog();
                }

            }
        }
    }
}
