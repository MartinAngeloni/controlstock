using ControlStockWFormsApp.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStockWFormsApp
{
    static class Program
    {

        public static Main main;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Config config = new Config();
            

            Application.Run();
        }

        static void checkedClose(object sender, EventArgs e)
        {
            if (main.IsDisposed)
            {
                Application.Exit();
            }
        }

        static Timer t;
        public static void started()
        {
            t = new Timer();
            t.Interval = 2000;
            t.Tick += checkedClose;
            t.Start();
        }

    }



}
