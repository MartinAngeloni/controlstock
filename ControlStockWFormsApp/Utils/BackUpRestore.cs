using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
	static class BackUpRestore
	{
		public static string globalDirectory;
		public static string colorFileName = "Color.bup";
		public static string marcaFileName = "Marca.bup";
		public static string productoFileName = "Producto.bup";
		public static string pedidoFileName = "Pedido.bup";
		public static string ventaFileName = "Venta.bup";
		public static string usuarioFileName = "Usuario.bup";
		public static string prodXMFileName = "ProdXM.bup";
		public static string prodXVentaFileName = "ProdXVenta.bup";
		public static string prodXPedidoFileName = "prodXPedido.bup";
        public static string proveedor = "proveedor.bup";
        public static string prodXProveedor = "prodXProveedor.bup";


        public static Boolean typeError = false;
		public static Boolean error = false;

		/*
		 example to driving files
		 String line;try 
		{
		//Pass the file path and file name to the StreamReader constructor
		StreamReader sr = new StreamReader("C:\\Sample.txt");

		//Read the first line of text
		line = sr.ReadLine();

		//Continue to read until you reach end of file
		while (line != null) 
		{
		//write the lie to console window
		Console.WriteLine(line);
		//Read the next line
		line = sr.ReadLine();
		}

		//close the file
		sr.Close();
		Console.ReadLine();
		}
		catch(Exception e)
		{
		Console.WriteLine("Exception: " + e.Message);
		}
		   finally 
		{
		Console.WriteLine("Executing finally block.");
		}
		 */

		public static Boolean checkAndLoadBackUpDirectory() {
			if (File.Exists("../bud.conf"))
			{
				String dir = File.ReadAllText("../bud.conf");
				if ("".Equals(dir))
				{
					return false;
				}
				else
				{
					
					globalDirectory = dir;
					if (Directory.Exists(@globalDirectory) && checkFiles())
					{
						return true;
					}
					else {
						return false;
					}
					
				}
			}
			else {
				return false;
			}
		}

		public static void setGlobalDirectory(string path) {
			File.WriteAllText("../bud.conf",path);
		}

		public static Boolean checkFiles() {
			return (File.Exists(@globalDirectory + @colorFileName) && File.Exists(@globalDirectory + @marcaFileName) && File.Exists(@globalDirectory + @productoFileName) && File.Exists(@globalDirectory + @usuarioFileName) && File.Exists(@globalDirectory + @pedidoFileName) && File.Exists(@globalDirectory + @ventaFileName) && File.Exists(@globalDirectory + @prodXMFileName) && File.Exists(@globalDirectory + @prodXVentaFileName) && File.Exists(@globalDirectory + @prodXPedidoFileName) && File.Exists(@globalDirectory + @prodXProveedor) && File.Exists(@globalDirectory + @proveedor));
		}


		public static void checkFilesAndDoFullBackUp() {
			//Vamos a comprobar
			if (!error)
			{
				if (!File.Exists(@globalDirectory + @colorFileName))
				{
					fullBackUpColor();
				}
				if (!File.Exists(@globalDirectory + @marcaFileName))
				{
					fullBackUpMarca();
				}
				if (!File.Exists(@globalDirectory + @productoFileName))
				{
					fullBackUpProducto();
				}
				if (!File.Exists(@globalDirectory + @usuarioFileName))
				{
					fullBackUpUsuario();
				}
				if (!File.Exists(@globalDirectory + @pedidoFileName))
				{
					fullBackUpPedido();
				}
				if (!File.Exists(@globalDirectory + @ventaFileName))
				{
					fullBackUpVenta();
				}
				if (!File.Exists(@globalDirectory + @prodXMFileName))
				{
					fullBackUpProdXM();
				}
				if (!File.Exists(@globalDirectory + @prodXVentaFileName))
				{
					fullBackUpProdXVenta();
				}
				if (!File.Exists(@globalDirectory + @prodXPedidoFileName))
				{
					fullBackUpProdXPedido();
				}
                if (!File.Exists(@globalDirectory + @proveedor))
                {
                    fullBackUpProveedor();
                }
                if (!File.Exists(@globalDirectory + @prodXProveedor))
                {
                    fullBackUpProdXProveedor();
                }
            }
			else {
				throw new Exception();
			}
		}

		public static void fullBackUpColor()
		{
			DAOColor.obtenerColores();
			foreach (DataRow dt in DAOColor.colores.Rows) {
				backupInsertColor(int.Parse(dt[0].ToString()),dt[1].ToString());
			}
		}
		public static void fullBackUpMarca()
		{
			DAOMarca.obtenerMarcas();
			foreach (DataRow dt in DAOMarca.marcas.Rows)
			{
				backupInsertMarca(int.Parse(dt[0].ToString()), dt[1].ToString());
			}
		}
		public static void fullBackUpProducto()
		{
			DAOProducto.obtenerListaProductos();
			foreach (DataRow dt in DAOProducto.listaProducto.Rows)
			{
				backupInsertProducto(int.Parse(dt[0].ToString()), dt[1].ToString(), dt[2].ToString(), dt[3].ToString());
			}
		}
		public static void fullBackUpVenta()
		{
            string format = "yyyy-MM-dd HH:mm:ss";
            DAOVenta.obtenerHistorialVentas();
			foreach (DataRow dt in DAOVenta.historialVenta.Rows)
			{
				backupInsertVenta(int.Parse(dt[0].ToString()), DateTime.Parse(dt[1].ToString()).ToString(format), dt[2].ToString(), float.Parse(dt[3].ToString()), float.Parse(dt[4].ToString()));
			}
		}
		public static void fullBackUpProdXM()
		{
			DAOProducto.obtenerProXm();
			foreach (DataRow dt in DAOProducto.proxm.Rows)
			{
				backupInsertProdXM(int.Parse(dt[0].ToString()), int.Parse(dt[1].ToString()), int.Parse(dt[2].ToString()), float.Parse(dt[3].ToString()), int.Parse(dt[4].ToString()), int.Parse(dt[5].ToString()), dt[6].ToString(), int.Parse(dt[7].ToString()),Boolean.Parse(dt[8].ToString()));
			}
		}
		public static void fullBackUpPedido()
		{
            string format = "yyyy-MM-dd HH:mm:ss";
            DAOPedido.obtenerHistorialPedido();
			foreach (DataRow dt in DAOPedido.historialPedido.Rows)
			{
				backupInsertPedido(int.Parse(dt[0].ToString()), DateTime.Parse(dt[2].ToString()).ToString(format), dt[1].ToString(), dt[3].ToString());
			}
		}


		public static void fullBackUpUsuario()
		{
			SqlConnection conexion = new SqlConnection(Variables.conectionString);
			SqlDataAdapter sqa = new SqlDataAdapter("Select * from Usuario ", conexion);
			DataTable dt = new DataTable();
			sqa.Fill(dt);
			foreach (DataRow dr in dt.Rows)
			{
				backupUsuario(int.Parse(dr[0].ToString()),dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
			}
		}
		
		public static void fullBackUpProdXVenta()
		{
			SqlConnection conexion = new SqlConnection(Variables.conectionString);
			SqlDataAdapter sqa = new SqlDataAdapter("Select * from ProdXVenta", conexion);
			DataTable dt = new DataTable();
			sqa.Fill(dt);
			foreach (DataRow dr in dt.Rows)
			{
				backupInsertProdXVenta(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString()), int.Parse(dr[3].ToString()), float.Parse(dr[4].ToString()));
			}
		}
		public static void fullBackUpProdXPedido()
		{
			SqlConnection conexion = new SqlConnection(Variables.conectionString);
			SqlDataAdapter sqa = new SqlDataAdapter("Select * from ProdXPedido", conexion);
			DataTable dt = new DataTable();
			sqa.Fill(dt);
			foreach (DataRow dr in dt.Rows)
			{
				backupInsertProdXPedido(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString()), int.Parse(dr[3].ToString()), float.Parse(dr[4].ToString()));
			}
		}

        public static void fullBackUpProveedor()
        {
            SqlConnection conexion = new SqlConnection(Variables.conectionString);
            SqlDataAdapter sqa = new SqlDataAdapter("Select * from Proveedor", conexion);
            DataTable dt = new DataTable();
            sqa.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                backupProveedor(int.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
        }
        public static void fullBackUpProdXProveedor()
        {
            SqlConnection conexion = new SqlConnection(Variables.conectionString);
            SqlDataAdapter sqa = new SqlDataAdapter("Select * from ProdXProveedor", conexion);
            DataTable dt = new DataTable();
            sqa.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                backupProdXProveedor(int.Parse(dr[0].ToString()), int.Parse(dr[1].ToString()), int.Parse(dr[2].ToString()));
            }
        }


        //BackupColor
        public static void backupInsertColor(int id, string color)
		{
			String insert = "Insert into Color (id,color) values (" + id + ",'" + color + "');" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @colorFileName, insert);
		}
		public static void backupDeleteColor(int id)
		{
			String delete = "Delete from Color where id="+ id +";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @colorFileName, delete);
		}
		//BackupMarca
		public static void backupInsertMarca(int id, string nombre)
		{
			String insert = "Insert into Marca (id,nombre) values (" + id + ",'" + nombre + "');" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @marcaFileName, insert);
		}
		public static void backupDeleteMarca(int id)
		{
			String delete = "Delete from Marca where id=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @marcaFileName, delete);
		}
		//BackUpUsuario
		public static void backupUsuario(int id, String usuario,String pass, String tipo)
		{
			String insert = "Insert into Usuario (Id,Usuario,Contrasenia,Tipo) values (" + id + ",'" + usuario + "','" + pass + "','" + tipo + "');" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @usuarioFileName, insert);
		}
		//BackUpProducto
		public static void backupInsertProducto(int cod, string nombre, string modelo, string desc)
		{
			String insert = "Insert into Producto (Cod_Producto,Nombre,Modelo,Descripcion) values (" + cod + ",'" + nombre + "','" + modelo + "','" + desc.Replace(Environment.NewLine, "") + "');" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @productoFileName, insert);
		}
		public static void backupDeleteProducto(int id)
		{
			String delete = "Delete from Producto where Cod_Producto=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @productoFileName, delete);
		}
		//BackUpProXM
		public static void backupInsertProdXM(int id, int idMarca,int idProd,float precio,int idColor,int stock,string codigo, int alerta,Boolean baja)
		{
			String insert = "Insert into ProXm (id,id_marca,id_producto,precio,id_color,stock,codigo,alerta_stock,baja) values (" + id + "," + idMarca + "," + idProd + "," + precio.ToString().Replace(",",".") + "," + idColor + "," + stock + ",'" + codigo + "'," + alerta + ",'" + baja + "');" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @prodXMFileName, insert);
		}
		public static void backupDeleteProdXM(int id)
		{
			String delete = "Delete from ProXm where id=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @prodXMFileName, delete);
		}
		//BackUpVenta
		public static void backupInsertVenta(int id, String fecha, string descripcion, float total,float recambio)
		{
			String insert = "Insert into Venta (id,fecha,descripcion,total,diferencia_cambio) values (" + id + ",'" + fecha + "','" + descripcion.Replace(Environment.NewLine, "") + "'," + total.ToString().Replace(",", ".") + "," + recambio.ToString().Replace(",", ".") + ");" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @ventaFileName, insert);
		}
		public static void backupDeleteVenta(int id)
		{
			String delete = "Delete from Venta where id=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @ventaFileName, delete);
		}
		//BackUpProdXVenta
		public static void backupInsertProdXVenta(int id, string idProd,int idVent, int cant, float precio)
		{
			String insert = "Insert into ProdXVenta (id,id_producto,id_venta,cantidad,precio) values (" + id + ",'" + idProd + "'," + idVent + "," + cant + "," + precio.ToString().Replace(",", ".") + ");" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @prodXVentaFileName, insert);
		}
		public static void backupDeleteProdXVenta(int id)
		{
			String delete = "Delete from ProdXVenta where id=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @prodXVentaFileName, delete);
		}
		//BackUpPedido
		public static void backupInsertPedido(int id, String fecha, string descripcion, string proveedor)
		{
			String insert = "Insert into Pedido (id_pedido,descripcion,fecha,proveedor) values (" + id + ",'" + descripcion.Replace(Environment.NewLine,"") + "','" + fecha + "','" + proveedor + "');" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @pedidoFileName, insert);
		}
		public static void backupDeletePedido(int id)
		{
			String delete = "Delete from Pedido where id=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @pedidoFileName, delete);
		}
		//BackUpProdXPedido
		public static void backupInsertProdXPedido(int id, string idProd,int idPed, int cant, float precio)
		{
			String insert = "Insert into ProdXPedido (id,id_producto,id_pedido,cantidad,precio) values (" + id + ",'" + idProd + "'," + idPed + "," + cant + "," + precio.ToString().Replace(",", ".") + ");" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @prodXPedidoFileName, insert);
		}
        //BackUpProveedor
        public static void backupProveedor(int id, string nombre, string cuit, string cbu, string direccion, string telefono, string correo, string pagina)
        {
            String insert = "Insert into Proveedor (id,nombre,cuit,cbu,direccion,telefono,correo,pagina) values (" + id + ",'" + nombre + "','" + cuit + "','" + cbu + "','" + direccion + "','" + telefono + "','" + correo + "','" + pagina + "')" + System.Environment.NewLine;
            File.AppendAllText(@globalDirectory + @prodXPedidoFileName, insert);
        }
        public static void backupProdXProveedor(int id, int idProd, int idProv)
        {
            String insert = "Insert into ProdXProveedor (id,id_producto,id_proveedor) values (" + id + "," + idProd + "," + idProv + ");" + System.Environment.NewLine;
            File.AppendAllText(@globalDirectory + @prodXPedidoFileName, insert);
        }


        public static void backupDeleteProdXPedido(int id)
		{
			String delete = "Delete from ProdXPedido where id=" + id + ";" + System.Environment.NewLine;
			File.AppendAllText(@globalDirectory + @prodXPedidoFileName, delete);
		}

		public static void restoreAllDB() {

			// set IDENTITY_INSERT Tablename on;

			// set IDENTITY_INSERT Tablename off;
			

			SqlConnection con = new SqlConnection(Variables.conectionString);
			StreamReader colorTxt = new StreamReader(@globalDirectory + @colorFileName);
			StreamReader marcaTxt = new StreamReader(@globalDirectory + @marcaFileName);
			StreamReader productoTxt = new StreamReader(@globalDirectory + @productoFileName);
			StreamReader usuarioTxt = new StreamReader(@globalDirectory + @usuarioFileName);
			StreamReader pedidoTxt = new StreamReader(@globalDirectory + @pedidoFileName);
			StreamReader ventaTxt = new StreamReader(@globalDirectory + @ventaFileName);
			StreamReader prodXMTxt = new StreamReader(@globalDirectory + @prodXMFileName);
			StreamReader prodXVentaTxt = new StreamReader(@globalDirectory + @prodXVentaFileName);
			StreamReader prodXPedido = new StreamReader(@globalDirectory + @prodXPedidoFileName);
	
			con.Open();

			setEnableInsertIdentity("Usuario", con);
			restoreEntity(usuarioTxt, con);
			setDisableInsertIdentity("Usuario", con);

			setEnableInsertIdentity("Color",con);
			restoreEntity(colorTxt, con);
			setDisableInsertIdentity("Color", con);

			setEnableInsertIdentity("Marca", con);
			restoreEntity(marcaTxt, con);
			setDisableInsertIdentity("Marca", con);

			setEnableInsertIdentity("Producto", con);
			restoreEntity(productoTxt, con);
			setDisableInsertIdentity("Producto", con);

			setEnableInsertIdentity("ProXM", con);
			restoreEntity(prodXMTxt, con);
			setDisableInsertIdentity("ProXM", con);


			setEnableInsertIdentity("Venta", con);
			restoreEntity(ventaTxt, con);
			setDisableInsertIdentity("Venta", con);

			
			setEnableInsertIdentity("Pedido", con);
			restoreEntity(pedidoTxt, con);
			setDisableInsertIdentity("Pedido", con);



			
			setEnableInsertIdentity("ProdXVenta", con);
			restoreEntity(prodXVentaTxt, con);
			setDisableInsertIdentity("ProdXVenta", con);

			setEnableInsertIdentity("ProdXPedido", con);
			restoreEntity(prodXPedido, con);
			setDisableInsertIdentity("ProdXPedido", con);

			con.Close();

		}

		public static void setEnableInsertIdentity(String tablename, SqlConnection con) {
			String query = "set IDENTITY_INSERT "+ tablename +" on";
			SqlCommand sc = new SqlCommand(@query, con);
			sc.ExecuteNonQuery();
		}

		public static void setDisableInsertIdentity(String tablename, SqlConnection con)
		{
			String query = "set IDENTITY_INSERT " + tablename + " off";
			SqlCommand sc = new SqlCommand(@query, con);
			sc.ExecuteNonQuery();
		}

		public static void restoreEntity(StreamReader entity,SqlConnection con) {
			String query;
			SqlCommand sc;
			do {
				query = entity.ReadLine();
				if(query !=null)
				{
				sc = new SqlCommand(@query,con);
				sc.ExecuteNonQuery();
				}
			} while (query != null);
		}
	}
}
