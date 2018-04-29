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

        public static SqlDataAdapter sqaProveedor = new SqlDataAdapter("Select * from Proveedor", Variables.conexion);

        public static SqlDataAdapter sqaProdXProveedor = new SqlDataAdapter("Select * from ProdXProveedor", Variables.conexion);

        public static SqlDataAdapter sqaListaProductosConProveedor = new SqlDataAdapter("select * from Proveedor p inner join ProdXProveedor pp on p.id = pp.id_proveedor inner join ProdXM pm on pp.id_producto = pm.id inner join Producto pr on pr.id = pm.id_producto", Variables.conexion);

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

        public static void obtenerProdXPedido()
        {
            prodXProveedor = new DataTable();
            sqaProdXProveedor.Fill(prodXProveedor);
        }

		public static void crearProveedor() {

		}

        public static void agregarProveedorAlProducto()
        {

        }

        public static void quitarProveedorAlProducto()
        {

        }

        public static void actualizarPrecioAlProducto()
        {

        }

        public static void CrearProveedor()
        {
            new SqlCommandBuilder(sqaProveedor);
            sqaProveedor.Update(proveedores);
        }

        public static void CrearProdXProveedor()
        {
            new SqlCommandBuilder(sqaProdXProveedor);
            sqaProdXProveedor.Update(prodXProveedor);
        }    

    }
}
