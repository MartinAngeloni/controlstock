using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ControlStockWFormsApp.Utils
{
    static class DAOProducto
    {
        
        public static SqlDataAdapter sqaProducto = new SqlDataAdapter("Select I.codigo as Codigo, P.Nombre, M.nombre as Marca, P.Modelo, C.color as Color, I.Precio, I.stock as Stock, I.alerta_stock as AlertaStock, I.id, I.id_producto from Producto P inner join ProXm I on P.Cod_Producto = I.id_producto inner join Marca M on I.id_marca = M.id inner join Color C on I.id_color = C.id where I.baja = 0", Variables.conexion);

        public static SqlDataAdapter sqaProductoBaja = new SqlDataAdapter("Select I.codigo as Codigo, P.Nombre, M.nombre as Marca, P.Modelo, C.color, I.Precio, I.stock, I.alerta_stock, I.id, I.id_producto from Producto P inner join ProXm I on P.Cod_Producto = I.id_producto inner join Marca M on I.id_marca = M.id inner join Color C on I.id_color = C.id where I.baja = 1", Variables.conexion);

        public static DataTable productosBaja = new DataTable();

        public static SqlDataAdapter sqaProductoInAlert = new SqlDataAdapter("Select I.codigo as Codigo, P.Nombre, M.nombre as Marca, P.Modelo, C.color as Color, I.Precio, I.stock as Stock, I.alerta_stock as AlertaStock from Producto P inner join ProXm I on P.Cod_Producto = I.id_producto inner join Marca M on I.id_marca = M.id inner join Color C on I.id_color = C.id where I.alerta_stock > I.stock and I.baja = 0", Variables.conexion);

        public static DataTable productos = new DataTable();

        public static DataTable productosInAlert = new DataTable();

        public static SqlDataAdapter sqaActualizarListaProducto = new SqlDataAdapter("Select * from Producto", Variables.conexion);

        public static DataTable listaProducto = new DataTable();

        public static SqlDataAdapter sqaProXm = new SqlDataAdapter("Select * from ProXm", Variables.conexion);

        public static DataTable proxm = new DataTable();

		public static string productoSeleccionado = null;


		
		public static Boolean error = false; 

		public static string generateCodigo() {
			Variables.conexion.Open();
			string query = @"select ISNULL(Max(codigo),'ER00000000') from ProXm where codigo like 'ER%'";
			SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
			SqlDataReader drr = cm.ExecuteReader();
			drr.Read();
			String codigo = drr[0].ToString();
			int numero = Int32.Parse(codigo.Substring(2,8));
			numero++;
			drr.Close();
			Variables.conexion.Close();
			return "ER" + numero.ToString("D8");
		} 

		public static void eliminarProducto(String codigo)
		{
			obtenerListaProductos();
			obtenerProXm();
			try
			{
				int id = -1;
				foreach (DataRow dr in proxm.Rows)
				{
					if (dr["codigo"].ToString().Equals(codigo))
					{
						id = Int32.Parse(dr["id_producto"].ToString());
						Variables.conexion.Open();
						String sqlped = "Delete from ProXm where codigo = '" + codigo + "'";
						SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
						cmd.CommandType = CommandType.Text;
						cmd.ExecuteNonQuery();
						break;
					}
				}
				if (id == -1)
				{
					error = true;
				}
				else
				{
					foreach (DataRow dr in listaProducto.Rows)
					{
						if (Int32.Parse(dr["Cod_Producto"].ToString()) == id)
						{
							String sqlped = "Delete from Producto where Cod_Producto = " + id.ToString();
							SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
							cmd.CommandType = CommandType.Text;
							cmd.ExecuteNonQuery();
							Variables.conexion.Close();
							obtenerProductos();
							obtenerListaProductos();
							obtenerProXm();
							break;
						}
					}
				}


			}
			catch (SqlException e) {
				error = true;
				Variables.conexion.Close();
			}
		}


        public static void obtenerProductosBajoStock()
        {
            productosInAlert = new DataTable();
            sqaProductoInAlert.Fill(productosInAlert); //rellenamos un DataTable productos con sqlAdapter de productos
        }

        public static void actualizarStockProducto(string codigo, int difStock)
        {
            string query = @"select stock from ProXm where codigo = '" + codigo + "'";
            SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
            SqlDataReader drr = cm.ExecuteReader();
            drr.Read();
            int stock = Convert.ToInt32(drr[0]);
            drr.Close();
            if (-difStock > stock)
            {
				error = true;  //Esta linea cambiala
            }
            else
            {
                String sqlped = "UPDATE ProXm Set stock = @sto where codigo = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
                cmd.Parameters.AddWithValue("@sto", difStock + stock);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

        }

        public static void actualizarPrecioProducto(string codigo, float porcentaje)
        {
            Variables.conexion.Open();
            string query = @"select precio from ProXm where codigo = '" + codigo + "'";
            SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
            SqlDataReader drr = cm.ExecuteReader();
            drr.Read();
            float precio = float.Parse(drr[0].ToString());
            drr.Close();
			precio = (float) Math.Round(precio + (precio * (porcentaje / 100)), 2);
            String sqlped = "UPDATE ProXm Set precio = @pre where codigo = '" + codigo + "'";
            SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
            cmd.Parameters.AddWithValue("@pre", precio);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            Variables.conexion.Close();

        }

        //metodo reciente Fernandez
        public static int consultarStock(String codigo)
        {
            int stock = -1;
            Utils.Variables.conexion.Open();
            string query = @"select ISNULL(stock,-1) from ProXm where codigo = '" + codigo + "'";
            SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
            SqlDataReader drr = cm.ExecuteReader();
            drr.Read();
            stock = Convert.ToInt32(drr[0]);
            drr.Close();
            Utils.Variables.conexion.Close();
            return stock;
        }

        public static void obtenerProductos()
        {
            productos = new DataTable();
            sqaProducto.Fill(productos); //rellenamos un DataTable productos con sqlAdapter de productos
        }

        public static DataRow obtenerProductoById(int id)
        {
            obtenerListaProductos();
            foreach (DataRow dr in listaProducto.Rows) {
                if (int.Parse(dr["Cod_Producto"].ToString()) == id) {
                    return dr;
                }
            }
            return null;
        }

        public static DataRow obtenerProdXMById(int id)
        {
            obtenerProXm();
            foreach (DataRow dr in proxm.Rows)
            {
                if (int.Parse(dr["id"].ToString()) == id)
                {
                    return dr;
                }
            }
            return null;
        }

        public static void obtenerListaProductos()
        {
            listaProducto = new DataTable();
            sqaActualizarListaProducto.Fill(listaProducto);
        }


        public static DataTable actualizarProductos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Marca");
            dt.Columns.Add("Modelo");
    
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Costo");
            dt.Columns.Add("Stock");
            dt.Columns.Add("StockAlerta");

            foreach (DataRow prod in Utils.DAOProducto.productos.Rows)
            {
                DataRow nuevo = dt.NewRow();
                nuevo[0] = prod[0];
                nuevo[1] = prod[1];
                //nuevo[2] = prod[0];
                nuevo[3] = prod[2];
                nuevo[4] = prod[3];
                nuevo[5] = prod[4];
                //nuevo[6] = prod[0];
                nuevo[7] = prod[5];
                nuevo[8] = prod[6];
                dt.Rows.Add(nuevo);
            }

            return dt;
        }

        public static void actualizarListaProducto()
        {
            new SqlCommandBuilder(sqaActualizarListaProducto);
            sqaActualizarListaProducto.Update(listaProducto);
        }

        public static void crearProducto()
        {
				new SqlCommandBuilder(sqaProXm);
				sqaProXm.Update(proxm);
        }

		public static Boolean comprobarProducto(string codigo) {
			foreach (DataRow dr in proxm.Rows)
			{
				if (dr["codigo"].Equals(codigo))
				{
					return true;
				}
			}
			return false;
		}

        public static void obtenerProXm()
        {
            proxm = new DataTable();
            sqaProXm.Fill(proxm);
        }


        public static void darDeBajaProducto(string codigo)
        {
            obtenerProXm();
            try {
                Variables.conexion.Open();
                String sqlped = "UPDATE ProXm Set baja = @baja where codigo = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
                cmd.Parameters.AddWithValue("@baja", true);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                obtenerProductos();
                Variables.conexion.Close();
            }
            catch (SqlException e)
            {
                error = true;
            }
        }

        public static void restaurarProducto(string codigo)
        {
            obtenerProXm();
            try
            {
                Variables.conexion.Open();
                String sqlped = "UPDATE ProXm Set baja = @baja where codigo = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
                cmd.Parameters.AddWithValue("@baja", false);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                obtenerProductos();
                obtenerProductosDadosDeBaja();
                Variables.conexion.Close();
            }
            catch (SqlException e)
            {
                error = true;
            }
        }


        public static void obtenerProductosDadosDeBaja()
        {
            productosBaja = new DataTable();
            sqaProductoBaja.Fill(productosBaja); //rellenamos un DataTable
        }

    }
}
