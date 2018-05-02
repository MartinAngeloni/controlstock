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

            this.dataGridView1.Columns["id"].Visible = false;

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

        private void button5_Click(object sender, EventArgs e)
        {
            ActualizarPrecio ac = new ActualizarPrecio();
            ac.ShowDialog();
        }

        //busqueda de dataGridView
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //sintaxis para busqueda en dataGridView
            DataView dataView = Utils.DAOProveedor.proveedores.DefaultView;
            dataView.RowFilter = string.Format("nombre like '%{0}%' or cuit like '%{0}%' or cbu like '%{0}%' or direccion like '%{0}%' or telefono like '%{0}%' or correo like '%{0}%' or pagina like '%{0}%'", textBox1.Text); //busqueda por nombre o marca
            dataGridView1.DataSource = dataView;
        }
    }
}
