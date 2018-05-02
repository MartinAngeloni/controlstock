using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
    static class DAOMarca
    {

        public static bool error = false;

        public static SqlDataAdapter sqaMarca = new SqlDataAdapter("Select id as ID, nombre as Marca from Marca ", Variables.conexion);

        public static DataTable marcas = new DataTable();

        public static SqlDataAdapter sqaMarcas;

        public static void obtenerMarcas()
        {
            marcas = new DataTable();
            sqaMarca.Fill(marcas);
        }

        public static void CrearMarca()
        {
            try
            {
                new SqlCommandBuilder(sqaMarca);
                sqaMarca.Update(marcas);

            }
            catch (SqlException e)
            {
                error = true;
            }
            
        }
    }
}
