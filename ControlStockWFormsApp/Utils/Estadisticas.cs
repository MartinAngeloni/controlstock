using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStockWFormsApp.Utils
{
	static class Estadisticas
	{
		public static float costoPedidosMesActual;
		public static float gananciaNetaDelMesActual;
		public static string productoMasVendidoDelMes;
		public static string productoMenosVendidoDelMes;
		public static string productoConMasRecaudo;
		public static string productoMasVendidoEntreFechas;
		public static string productoMenosVendidoEntreFechas;



		public static void obtenerEstadisticasMensuales() {

			Variables.conexion.Open();

			costoPedidosMesActual = obtenerPrecioDeConsulta("select IsNull(SUM(PP.precio*PP.cantidad),0) from Pedido P inner join ProdXPedido PP on P.id_pedido = PP.id_pedido where Month(P.fecha) = MONTH((select SYSDATETIME())) and Year(P.fecha) = Year((select SYSDATETIME()))");

			gananciaNetaDelMesActual = obtenerPrecioDeConsulta("Select IsNull(SUM(PV.precio*PV.cantidad),0) from Venta V inner join ProdXVenta PV on V.id = PV.id_venta where Month(V.fecha) = MONTH((select SYSDATETIME())) and Year(V.fecha) = Year((select SYSDATETIME()))");

			productoMasVendidoDelMes = obtenerProductoDeConsulta("select top 1 Max(P.Nombre),MAX(P.Modelo),SUM(PV.cantidad) as cant from ProXm PM inner join Producto P on PM.id_producto = P.Cod_Producto inner join ProdXVenta PV on PV.id_producto = PM.codigo inner join Venta V on V.id = PV.id_venta where Month(V.fecha) = MONTH((select SYSDATETIME())) and Year(V.fecha) = Year((select SYSDATETIME())) group by PM.codigo order by cant desc");

			productoMenosVendidoDelMes = obtenerProductoDeConsulta("select top 1 Max(P.Nombre),MAX(P.Modelo),SUM(PV.cantidad) as cant from ProXm PM inner join Producto P on PM.id_producto = P.Cod_Producto inner join ProdXVenta PV on PV.id_producto = PM.codigo inner join Venta V on V.id = PV.id_venta where Month(V.fecha) = MONTH((select SYSDATETIME())) and Year(V.fecha) = Year((select SYSDATETIME())) group by PM.codigo order by cant asc");

			productoConMasRecaudo = obtenerGananciaProductoDeConsulta("select top 1 Max(P.Nombre),MAX(P.Modelo),SUM(PV.cantidad*PV.precio) as cant from ProXm PM inner join Producto P on PM.id_producto = P.Cod_Producto inner join ProdXVenta PV on PV.id_producto = PM.codigo inner join Venta V on V.id = PV.id_venta where Month(V.fecha) = MONTH((select SYSDATETIME())) and Year(V.fecha) = Year((select SYSDATETIME())) group by PM.codigo order by cant desc");

			

			Variables.conexion.Close();

		}


		public static float obtenerPrecioDeConsulta(String consulta) {
			SqlCommand cm;
			cm = new SqlCommand(@consulta, Utils.Variables.conexion);
			SqlDataReader dr = cm.ExecuteReader();
			if (dr.Read())
			{
                float cadena = float.Parse(dr[0].ToString());
                dr.Close();
                return (cadena);
			}
			else {
                dr.Close();
                return 0f; }
		}

		public static string obtenerProductoDeConsulta(String consulta)
		{
			SqlCommand cm;
			cm = new SqlCommand(@consulta, Utils.Variables.conexion);
			SqlDataReader dr = cm.ExecuteReader();
			if (dr.Read())
			{
				StringBuilder prod = new StringBuilder();
				prod.Append(dr[0].ToString() + " ");
				prod.Append(dr[1].ToString() + " ");
				prod.Append(dr[2].ToString() + " ");
                dr.Close();
				return (prod.ToString());
			}
			else {
                dr.Close();
                return "";
			}

		}

		public static void obtenerProductoConMasVentasEntreFechas(DateTime start, DateTime end) {

            Console.WriteLine(start.ToString("yyyy-MM-dd"));
            Variables.conexion.Open();
            productoMasVendidoEntreFechas = obtenerProductoDeConsulta("select top 1 Max(P.Nombre),MAX(P.Modelo),SUM(PV.cantidad) as cant from ProXm PM inner join Producto P on PM.id_producto = P.Cod_Producto inner join ProdXVenta PV on PV.id_producto = PM.codigo inner join Venta V on V.id = PV.id_venta where V.fecha >= '"+ start.ToString("yyyy-MM-dd") + "' and V.fecha <= '"+ end.ToString("yyyy-MM-dd") + "' group by PM.codigo order by cant desc");
            Variables.conexion.Close();
        }

		public static void obtenerProductoConMenosVentasEntreFechas(DateTime start, DateTime end)
		{
            Variables.conexion.Open();
            productoMenosVendidoEntreFechas = obtenerProductoDeConsulta("select top 1 Max(P.Nombre),MAX(P.Modelo),SUM(PV.cantidad) as cant from ProXm PM inner join Producto P on PM.id_producto = P.Cod_Producto inner join ProdXVenta PV on PV.id_producto = PM.codigo inner join Venta V on V.id = PV.id_venta where V.fecha >= '" + start.ToString("yyyy-MM-dd") + "' and V.fecha <= '" + end.ToString("yyyy-MM-dd") + "' group by PM.codigo order by cant asc");
            Variables.conexion.Close();
        }


		public static string obtenerGananciaProductoDeConsulta(String consulta)
		{
			SqlCommand cm;
			cm = new SqlCommand(@consulta, Utils.Variables.conexion);
			SqlDataReader dr = cm.ExecuteReader();
			if (dr.Read())
			{
				StringBuilder prod = new StringBuilder();
				prod.Append(dr[0].ToString() + " ");
				prod.Append(dr[1].ToString() + " $");
				prod.Append(dr[2].ToString());
                dr.Close();

                return (prod.ToString());
			}
			else
			{
                dr.Close();
                return "";
			}

		}


	}
}
