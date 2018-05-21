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
	public partial class ConfigBackUpRestore : Form
	{
		public ConfigBackUpRestore()
		{
			InitializeComponent();
           
		}


		public void comprobar()
		{

			if (Utils.BackUpRestore.checkAndLoadBackUpDirectory())
			{

                Utils.BackUpRestore.saveFiles();
                Utils.BackUpRestore.checkFilesAndDoFullBackUp();



                Login login = new Login();
				login.Show();
				this.Close();
			}
			else {
				textBox1.Text = Utils.BackUpRestore.globalDirectory;
			}
		}


		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void ConfigBackUpRestore_Load(object sender, EventArgs e)
		{
            comprobar();
        }

		private void button3_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderSelect= new FolderBrowserDialog();
			folderSelect.ShowDialog();
			textBox1.Text = @folderSelect.SelectedPath + @"\";
			Utils.BackUpRestore.globalDirectory = @folderSelect.SelectedPath + @"\";
           
        }

		private void button1_Click(object sender, EventArgs e){

			if ("".Equals(textBox1.Text))
			{
				MessageBox.Show("Ingrese un directorio");
			}
			else
			{
				if (!Utils.BackUpRestore.checkAndLoadBackUpDirectory())
				{
					Utils.BackUpRestore.setGlobalDirectory(Utils.BackUpRestore.globalDirectory);
                    Utils.BackUpRestore.saveFiles();
					Utils.BackUpRestore.checkFilesAndDoFullBackUp();
                    comprobar();
                }
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			Utils.BackUpRestore.restoreAllDB();
		}

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
