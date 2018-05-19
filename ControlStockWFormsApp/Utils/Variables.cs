using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStockWFormsApp.Utils
{
     static class Variables
    {

        public static String conectionString = @"Data Source=FER-PC\SQLEXPRESS;Initial Catalog=ControlStock;Integrated Security=True";

        public static SqlConnection conexion;

        public static bool admin = false;

        public static DataGridViewRow selectedProductoXPedido;

        public static int cantXPedido;
        

		public static Boolean checkStringConnection(){

			try
			{
				conexion = new SqlConnection(Variables.conectionString);
			} catch (Exception e) {
				return false;
			}
			try {
				conexion.Open();
				if (conexion.State == System.Data.ConnectionState.Open)
				{
					conexion.Close();
					return true;
				}
				else
				{
					return false;
				}
			} catch (SqlException e) {
				return false;
			}
			

		}

    }
}
