using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
	class DAOVenta
	{
		public static SqlDataAdapter sqaVenta = new SqlDataAdapter("Select * from Venta where id < 0 ", Variables.conexion);

		public static SqlDataAdapter sqaVentaXProducto = new SqlDataAdapter("Select * from ProdXVenta where id < 0", Variables.conexion);

        public static SqlDataAdapter sqaHistorialVenta = new SqlDataAdapter("select * from Venta", Variables.conexion);

        public static DataTable historialVenta;

        public static DataTable venta;

		public static DataTable prodXVenta;

		public static Boolean error = false;

		public static DataTable realizandoVenta = new DataTable();




		public static void initRealizandoVenta()
		{
			Variables.conexion.Open();
			realizandoVenta = new DataTable();
			realizandoVenta.Columns.Add("Codigo");
			realizandoVenta.Columns.Add("Nombre");
			realizandoVenta.Columns.Add("Marca");
			realizandoVenta.Columns.Add("Modelo");
			realizandoVenta.Columns.Add("Cantidad");
			realizandoVenta.Columns.Add("Precio");
			Variables.conexion.Close();
		}

		public static void agregarProductoAlVenta(String codigo, String nombre, String marca, String modelo, int cant, float precio, String desc)
		{
			DataRow dr = realizandoVenta.NewRow();
			dr[0] = codigo;
			dr[1] = nombre;
			dr[2] = marca;
			dr[3] = modelo;
			dr[4] = cant;
			dr[5] = precio;
			realizandoVenta.Rows.Add(dr);

			DataRow vent = venta.NewRow();

			vent[2] = desc;
			vent[1] = DateTime.Now;


			Variables.conexion.Open();
			string query = @"select ISNULL(Max(id),0) from Venta";
			SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
			SqlDataReader sdr = cm.ExecuteReader();
			sdr.Read();
			int id = Convert.ToInt32(sdr[0]);
			Variables.conexion.Close();

			vent[0] = id + 1;
			venta.Rows.Add(vent);

			DataRow prodXVent = prodXVenta.NewRow();
			prodXVent[1] = codigo;
			prodXVent[2] = id + 1;
			prodXVent[3] = cant;
			prodXVent[4] = precio;

			prodXVenta.Rows.Add(prodXVent);
		}

		public static void quitarProductoDeLista(String codigo)
		{
			for (int i = 0; i < realizandoVenta.Rows.Count; i++)
			{
				DataRow dr = realizandoVenta.Rows[i];
				Console.WriteLine("{0} {1}", codigo, dr["Codigo"]);
				if (dr["Codigo"].ToString().Equals(codigo))
				{
					realizandoVenta.Rows[i].Delete();
					realizandoVenta.AcceptChanges();
					Console.WriteLine("asd");
					break;
				}
			}

			for (int i = 0; i < prodXVenta.Rows.Count; i++)
			{
				DataRow dr = prodXVenta.Rows[i];
				if (dr["id_producto"].Equals(codigo))
				{
					dr.Delete();
					break;
				}
			}

		}

		public static void obtenerVenta()
		{
			venta = new DataTable();
			sqaVenta.Fill(venta);
		}

        public static void cancelarVenta(int codigo)
        {
            string query = @"select pv.id_producto,pv.cantidad from venta v inner join ProdXVenta pv on v.id = pv.id_venta where pv.id = "+codigo;
            SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
            Variables.conexion.Open();
            SqlDataReader drr = cm.ExecuteReader();
            List<String> data = new List<string>();
            while (drr.Read()) {
                data.Add(drr["id_producto"].ToString());
                data.Add(drr["cantidad"].ToString());
            }
            drr.Close();
            for (int x =0;x<data.Count;x=x+2)
            {
                DAOProducto.actualizarStockProducto(data[x],Convert.ToInt32(data[x+1]));
            }
            String sqlrxv = "delete from ProdXVenta where id_venta = " + codigo;
            SqlCommand cmd1 = new SqlCommand(sqlrxv, Variables.conexion);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            String sqlrv = "delete from Venta where id = " + codigo;
            SqlCommand cmd2 = new SqlCommand(sqlrv, Variables.conexion);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            Variables.conexion.Close();
        }

        public static void obtenerHistorialVentas()
        {
            historialVenta = new DataTable();
            sqaHistorialVenta.Fill(historialVenta);
        }
        

        public static void CrearVenta(float total, float diferencia)
		{
			try
			{
				Variables.conexion.Open();
				String sqlvent = "insert into Venta (descripcion,fecha,total,diferencia_cambio) values (@desc,@fecha,@total,@diff)";
				SqlCommand cmd = new SqlCommand(sqlvent, Variables.conexion);
				cmd.Parameters.AddWithValue("@desc", venta.Rows[0][2]);
				cmd.Parameters.AddWithValue("@fecha", venta.Rows[0][1]);
                cmd.Parameters.AddWithValue("@total", Math.Round(total,2));
                cmd.Parameters.AddWithValue("@diff", diferencia - total);


                cmd.CommandType = CommandType.Text;
				cmd.ExecuteNonQuery();


				string query = @"select IDENT_CURRENT('Venta')";

				SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);

				SqlDataReader drr = cm.ExecuteReader();
				drr.Read();
				int id = Convert.ToInt32(drr[0]);
				drr.Close();

				String sqlpp = "insert into ProdXVenta (id_producto,id_venta,cantidad,precio) values (@id,@desc,@fecha,@prov)";
				foreach (DataRow dr in prodXVenta.Rows)
				{

					SqlCommand cmd1 = new SqlCommand(sqlpp, Variables.conexion);
					cmd1.Parameters.AddWithValue("@id", dr[1]);
					cmd1.Parameters.AddWithValue("@desc", id);
					cmd1.Parameters.AddWithValue("@fecha", dr[3]);
					cmd1.Parameters.AddWithValue("@prov", dr[4]);

					cmd1.CommandType = CommandType.Text;
					cmd1.ExecuteNonQuery();
					Utils.DAOProducto.actualizarStockProducto(dr[1].ToString(), -Int32.Parse(dr[3].ToString()));
				}



				Variables.conexion.Close();
				obtenerVenta();
				obtenerProdXVenta();

			}
			catch (SqlException e)
			{
				error = true;
                Variables.conexion.Close();
            }
            catch (Exception e)
            {
                error = true;
                Variables.conexion.Close();
            }

        }


		public static void obtenerProdXVenta()
		{
			prodXVenta = new DataTable();
			sqaVentaXProducto.Fill(prodXVenta);
		}

		public static void CrearProdXVenta()
		{
			new SqlCommandBuilder(sqaVentaXProducto);
			sqaVentaXProducto.Update(prodXVenta);
		}


	}
}
