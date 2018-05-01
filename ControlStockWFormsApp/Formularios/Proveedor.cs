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
    public partial class Proveedor : Form
    {
        public Proveedor()
        {
            InitializeComponent();
			Utils.DAOProveedor.obtenerProveedores();


            dataGridView1.DataSource = Utils.DAOProveedor.proveedores;


            this.Width = 1000;
            this.Height = 600;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearProveedor crearProveedor = new CrearProveedor();
            crearProveedor.ShowDialog();
        }

        private void Proveedor_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DialogResult result = MessageBox.Show("Esta seguro de eliminar el Proveedor?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    Utils.DAOProveedor.eliminarProveedor(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    if (Utils.DAOProveedor.error)
                    {
                        MessageBox.Show("El Proveedor tiene productos asociados en su lista, no se puede eliminar");
                        Utils.DAOProveedor.error = false;
                    }
                    else
                    {
                        Utils.DAOProveedor.obtenerProveedores();
                        dataGridView1.DataSource = Utils.DAOProveedor.proveedores;
                    }
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                CrearProveedor crearProveedor = new CrearProveedor();
                crearProveedor.id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                crearProveedor.ShowDialog();
            }
        }
    }
}
