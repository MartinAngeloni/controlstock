using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
    class DAOProveedor
    {

        public static SqlDataAdapter sqaProveedor = new SqlDataAdapter("Select id as ID, nombre as Nombre, cuit as CUIT, cbu as CBU, direccion as Direccion, telefono as Telefono, correo as CorreoElectronico, pagina as Pagina from Proveedor", Variables.conexion);

        public static SqlDataAdapter sqaProdXProveedor = new SqlDataAdapter("Select * from ProdXProveedor", Variables.conexion);

        public static SqlDataAdapter sqaListaProductosConProveedor = new SqlDataAdapter("select pm.codigo,pr.Nombre,pr.Modelo,m.nombre,c.color,pm.precio,p.nombre from Proveedor p inner join ProdXProveedor pp on p.id = pp.id_proveedor inner join ProXm pm on pp.id_producto = pm.id inner join Producto pr on pr.Cod_Producto = pm.id_producto inner join color c on pm.id_color = c.id inner join marca m on m.id = pm.id_marca", Variables.conexion);

        public static DataTable proveedores;

        public static DataTable prodXProveedor;

        public static DataTable listaProductoConProveedor;

        public static Boolean error = false;

        
        public static void obtenerProveedores()
        {
            proveedores = new DataTable();
            sqaProveedor.Fill(proveedores);
        }

        public static void obtenerListaProductoConProveedor()
        {
            listaProductoConProveedor = new DataTable();
            sqaListaProductosConProveedor.Fill(listaProductoConProveedor);
        }

        public static void obtenerProdXProveedor()
        {
            prodXProveedor = new DataTable();
            sqaProdXProveedor.Fill(prodXProveedor);
        }

        public static void quitarProveedorAlProducto()
        {

        }

        public static void actualizarPrecioAlProducto(int producto, float porcentaje)
        {

        }

        public static void eliminarProveedor(int codigo)
        {
           
            try
            {
                Variables.conexion.Open();
                String sqlped = "Delete from Proveedor where id = " + codigo.ToString();
                SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                error = true;
            }
            finally
            {
                Variables.conexion.Close();
            }
        }

        public static void crearProveedor()
        {
            new SqlCommandBuilder(sqaProveedor);
            sqaProveedor.Update(proveedores);
        }

        public static void crearProdXProveedor()
        {
            new SqlCommandBuilder(sqaProdXProveedor);
            sqaProdXProveedor.Update(prodXProveedor);
        }    

    }
}
