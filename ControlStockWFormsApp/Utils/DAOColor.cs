using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
    static class DAOColor
    {
        public static bool error = false;

        public static SqlDataAdapter sqaColor = new SqlDataAdapter("Select id as ID, color as Color from Color ", Variables.conexion);


        public static DataTable colores = new DataTable();


        public static void obtenerColores()
        {
            colores = new DataTable();
            sqaColor.Fill(colores);
        }

        public static void CrearColor()
        {
            
            try
            {
                new SqlCommandBuilder(sqaColor);
                sqaColor.Update(colores);
                
            }
            catch (SqlException e)
            {
                error = true;
            }

        }
        

    }
    
}
