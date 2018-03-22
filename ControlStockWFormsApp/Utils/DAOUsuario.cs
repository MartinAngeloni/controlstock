using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
    class DAOUsuario
    {

        //metodo para logeo
        public static DataRow login(String usuario, String contrasenia)
        {
            SqlConnection conexion = new SqlConnection(Utils.Variables.conectionString);

            SqlDataAdapter sqa = new SqlDataAdapter("Select * from Usuario ", conexion);

            DataTable dt = new DataTable();
            sqa.Fill(dt);

            foreach (DataRow a in dt.Rows)
            {
                //si las fila[1] usuario y la fila[2]contrasenia son iguales
                if (a[1].ToString().Equals(usuario) && a[2].ToString().Equals(contrasenia))
                {
                    return a;

                }

            }

            return null;
        }
    }
}
