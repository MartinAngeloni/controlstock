using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
    static class DAOPedido
    {

        public static SqlDataAdapter sqaPedido = new SqlDataAdapter("Select * from Pedido where id_pedido < 0 ", Variables.conexion);

        public static SqlDataAdapter sqaPedidoXProducto = new SqlDataAdapter("Select * from ProdXPedido where id < 0", Variables.conexion);

        public static SqlDataAdapter sqaHistorialPedido = new SqlDataAdapter("select * from Pedido", Variables.conexion);

        public static DataTable historialPedido;

        public static DataTable pedido;

		public static DataTable prodXPedido;

		public static Boolean error = false;

		public static DataTable realizandoPedido = new DataTable();


	

		public static void initRealizandoPedido() {
			Variables.conexion.Open();
			realizandoPedido = new DataTable();
			realizandoPedido.Columns.Add("Codigo");
			realizandoPedido.Columns.Add("Nombre");
			realizandoPedido.Columns.Add("Marca");
			realizandoPedido.Columns.Add("Modelo");
			realizandoPedido.Columns.Add("Proveedor");
			realizandoPedido.Columns.Add("Cantidad");
			realizandoPedido.Columns.Add("Precio");
			Variables.conexion.Close();
		}

		public static void agregarProductoAlPedido(String codigo, String nombre, String marca, String modelo,String proveedor,int cant, float precio, String desc) {
			DataRow dr = realizandoPedido.NewRow();
			dr[0] = codigo;
			dr[1] = nombre;
			dr[2] = marca;
			dr[3] = modelo;
			dr[4] = proveedor;
			dr[5] = cant;
			dr[6] = precio;
			realizandoPedido.Rows.Add(dr);

			DataRow ped = pedido.NewRow();
			
			ped[1] = desc;
			ped[2] = DateTime.Now;
			ped[3] = proveedor;

			Variables.conexion.Open();
			string query = @"select ISNULL(Max(id_pedido),0) from Pedido";
			SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
			SqlDataReader sdr = cm.ExecuteReader();
			sdr.Read();
			int id = Convert.ToInt32(sdr[0]);
			Variables.conexion.Close();

			ped[0] = id + 1;
			pedido.Rows.Add(ped);

			DataRow prodXPed = prodXPedido.NewRow();
			prodXPed[1] = codigo;
			prodXPed[2] = id + 1;
			prodXPed[3] = cant;
			prodXPed[4] = precio;

			prodXPedido.Rows.Add(prodXPed);
		}

		public static void quitarProductoDeLista(String codigo) {
			for(int i = 0; i< realizandoPedido.Rows.Count; i++) {
				DataRow dr = realizandoPedido.Rows[i];
				Console.WriteLine("{0} {1}",codigo,dr["Codigo"]);
				if (dr["Codigo"].ToString().Equals(codigo)) {
					realizandoPedido.Rows[i].Delete();
					realizandoPedido.AcceptChanges();
					Console.WriteLine("asd");
					break;
				}
			}

			for (int i = 0; i < prodXPedido.Rows.Count; i++)
			{
				DataRow dr = prodXPedido.Rows[i];
				if (dr["id_producto"].Equals(codigo))
				{
					dr.Delete();
					break;
				}
			}

		}

        public static void obtenerPedido()
        {
            pedido = new DataTable();
            sqaPedido.Fill(pedido);
        }

        public static void obtenerHistorialPedido()
        {
            historialPedido = new DataTable();
            sqaHistorialPedido.Fill(historialPedido);
        }

        

        public static void CrearPedido()
        {
			try
			{
				Variables.conexion.Open();
				String sqlped = "insert into Pedido (descripcion,fecha,proveedor) values (@desc,@fecha,@prov)";
				SqlCommand cmd = new SqlCommand(sqlped, Variables.conexion);
				cmd.Parameters.AddWithValue("@desc", pedido.Rows[0][1]);
				cmd.Parameters.AddWithValue("@fecha", pedido.Rows[0][2]);
				cmd.Parameters.AddWithValue("@prov", pedido.Rows[0][3]);

				cmd.CommandType = CommandType.Text;
				cmd.ExecuteNonQuery();


				string query = @"select IDENT_CURRENT('Pedido')";

				SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);

				SqlDataReader drr = cm.ExecuteReader();
				drr.Read();
				int id = Convert.ToInt32(drr[0]);
				drr.Close();

				String sqlpp = "insert into ProdXPedido (id_producto,id_pedido,cantidad,precio) values (@id,@desc,@fecha,@prov)";
				foreach (DataRow dr in prodXPedido.Rows)
				{

					SqlCommand cmd1 = new SqlCommand(sqlpp, Variables.conexion);
					cmd1.Parameters.AddWithValue("@id", dr[1]);
					cmd1.Parameters.AddWithValue("@desc", id);
					cmd1.Parameters.AddWithValue("@fecha", dr[3]);
					cmd1.Parameters.AddWithValue("@prov", dr[4]);

					cmd1.CommandType = CommandType.Text;
					cmd1.ExecuteNonQuery();
					Utils.DAOProducto.actualizarStockProducto(dr[1].ToString(), Int32.Parse(dr[3].ToString()));
				}



				Variables.conexion.Close();
				obtenerPedido();
				obtenerProdXPedido();

			}
			catch (SqlException e) {
				error = true;
                Variables.conexion.Close();
            }
            catch (Exception e)
            {
                error = true;
                Variables.conexion.Close();
            }

        }


        public static void obtenerProdXPedido()
        {
			prodXPedido = new DataTable();
            sqaPedidoXProducto.Fill(prodXPedido);
        }

        public static void CrearProdXPedido()
        {
            new SqlCommandBuilder(sqaPedidoXProducto);
            sqaPedidoXProducto.Update(prodXPedido);
        }


        public static void cancelarPedido(int codigo)
        {
            string query = @"select pp.id_producto,pp.cantidad from Pedido p inner join ProdXPedido pp on p.id_pedido = pp.id_pedido where pp.id_pedido = " + codigo;
            SqlCommand cm = new SqlCommand(query, Utils.Variables.conexion);
            Variables.conexion.Open();
            SqlDataReader drr = cm.ExecuteReader();
            List<String> data = new List<string>();
            while (drr.Read())
            {
                data.Add(drr["id_producto"].ToString());
                data.Add(drr["cantidad"].ToString());
            }
            drr.Close();
            for (int x = 0; x < data.Count; x = x + 2)
            {
                DAOProducto.actualizarStockProducto(data[x], -Convert.ToInt32(data[x + 1]));
            }
            String sqlrxv = "delete from ProdXPedido where id_pedido = " + codigo;
            SqlCommand cmd1 = new SqlCommand(sqlrxv, Variables.conexion);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            String sqlrv = "delete from Pedido where id_pedido = " + codigo;
            SqlCommand cmd2 = new SqlCommand(sqlrv, Variables.conexion);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            Variables.conexion.Close();
        }

    }
}
