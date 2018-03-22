using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ControlStockWFormsApp.Formularios;

namespace ControlStockWFormsApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            button1.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzan
            button2.BackColor = System.Drawing.Color.FromArgb(198, 216, 175); //verde manzana
            textBox2.PasswordChar = '*'; //para cubrir las contrasenias

            if (File.Exists(@"../dbc.sv"))
			{
				string readText = File.ReadAllText(@"../dbc.sv");
				Utils.Variables.conectionString = readText;
				if (!"".Equals(readText) && Utils.Variables.checkStringConnection())
				{
                    this.Width = 500;
                    this.Height = 250;
                    this.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla
                    this.Show();
				}
				else
				{
					Config config = new Config();
					config.stringCon = readText;
                    config.StartPosition = FormStartPosition.CenterScreen; //form en el centro de la pantalla
                    config.Show();
					this.Dispose();
				}
			}
			else {
				Config config = new Config();
				config.Show();
				this.Dispose();
			}
		
			

		
			



		}

		private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataRow usuario = Utils.DAOUsuario.login(textBox1.Text, textBox2.Text);

            if (usuario != null)
            {
            

                if (usuario[3].ToString().Equals("Admin"))
                {
                    Utils.Variables.admin = true;
                }
                else
                {
                    Utils.Variables.admin = false;
                }

                Main main = new Main();
                main.Show();
                this.Dispose();
            }
            else
            {
                //en caso de no coincidir contrasenia y/o usuario mostramos el mensaje
                MessageBox.Show("Contrasenia y/o Usuario incorrectos");
            }

           



        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
