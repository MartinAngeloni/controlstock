using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStockWFormsApp.Formularios
{
	public partial class Config : Form
	{
        Inicio i = new Inicio();

        public string stringCon;
		public Config()
		{
			InitializeComponent();
            i.config = this;
            i.Show();
           
            

        }

        public void comprobar()
        {
            if (File.Exists(@"../dbc.sv"))
            {
                string readText = File.ReadAllText(@"../dbc.sv");
                Utils.Variables.conectionString = readText;
                stringCon = readText;
                if (!"".Equals(readText) && Utils.Variables.checkStringConnection())
                {
                    i.Close();
                    ConfigBackUpRestore cbr = new ConfigBackUpRestore();
                    cbr.Show();
                    Close();
                }
                else
                {
                    i.Close();
                    Show();
                }
            }
            else
            {
                i.Close();
                Show();
            }
            textBox1.Text = stringCon;
        }

		private void Config_Load(object sender, EventArgs e)
		{
            
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Utils.Variables.conectionString = @textBox1.Text;
			button1.Text = "Comprobando...";
			button1.Enabled = false;
			button2.Enabled = false;
			if (Utils.Variables.checkStringConnection())
			{
				MessageBox.Show("Conectado correctamente");
				File.WriteAllText(@"../dbc.sv", textBox1.Text);
                Login login = new Login();
                login.Show();
                this.Close();
			}
			else
			{
				MessageBox.Show("Conexion erronea compruebe la conexion");
			}
			button1.Text = "Probar Conexion";
			button1.Enabled = true;
			button2.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
            this.Close();
		}

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
