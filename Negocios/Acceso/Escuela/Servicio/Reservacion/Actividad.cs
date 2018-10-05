using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocios.Acceso.Escuela.Servicio.Reservacion
{
	public class Actividad
	{
		/// <summary>
		/// Sube la presentación del catedrático
		/// </summary>
		/// <param name="idActividad"></param>
		/// <param name="presentacion"></param>
		/// <returns></returns>
		public int SubirPresentacion(int idActividad, byte[] presentacion, String nombre)
		{
			int resultado = 0;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE ACTIVIDAD SET PRESENTACION = @PRESENTACION, NOMBRE = @NAME WHERE ACTIVIDAD = @IDACTIVIDAD ";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command =  new SqlCommand(update,con))
					{
						command.Parameters.Add(new SqlParameter("@PRESENTACION", SqlDbType.VarBinary)).Value = presentacion ;
						command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarBinary)).Value = nombre;
						command.Parameters.Add(new SqlParameter("@IDACTIVIDAD", SqlDbType.Int)).Value = idActividad ;
						resultado = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
			}
			finally
			{
				try
				{
					if (con != null) con.Close();
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}
			return resultado;
		}
		/// <summary>
		/// Obtiene las actividades que no posean presentación
		/// </summary>
		/// <param name="idReservacion"></param>
		/// <returns></returns>
		/*public DataTable ObtenerUltima(int idReservacion)
		{
			DataTable table = null;
			SqlConnection con = null;
			SqlCommand command = null;
			string select = "SELECT * FROM ACTIVIDAD " +
				"WHERE ACTIVIDAD.IDRESERVACION = @RESERVACION AND ACTIVIDAD.PRESENTACION = null ";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(select, con))
					{
						command.Parameters.Add(new SqlParameter("@RESERVACION",SqlDbType.Int)).Value = idReservacion;
						using (SqlDataAdapter data = new SqlDataAdapter(command))
						{
							table = new DataTable();
							data.Fill(table);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
			}
			finally
			{
				try
				{
					if (con != null) con.Close();
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}

			return table;
		}*/
		/// <summary>
		/// Obtiene todas las actividades de una reservación
		/// </summary>
		/// <param name="idReservacion"></param>
		/// <returns></returns>
		public DataTable ObtenerGeneral(int idReservacion)
		{
			DataTable table = null;
			SqlConnection con = null;
			SqlCommand command = null;
			string select = "SELECT ACTIVIDAD.IDACTIVIDAD, RESERVACION.NOMBRE, ACTIVIDAD.NOMBRE, ACTIVIDAD.PRESENTACION, " +
				"ACTIVIDAD.FECHA_HORA_INICIO, ACTIVIDAD.FECHA_HORA_FINAL " +
				" FROM ACTIVIDAD INNER JOIN RESERVACION ON ACTIVIDAD.IDRESERVACION = RESERVACION.IDRESERVACION " +
				" WHERE IDRESERVACION = @IDRESERVACION";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(select,con))
					{
						command.Parameters.Add(new SqlParameter("@IDRESERVACION",SqlDbType.Int)).Value = idReservacion;
						using (SqlDataAdapter adapter = new SqlDataAdapter(command))
						{
							table = new DataTable();
							adapter.Fill(table);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
			}
			finally
			{
				try
				{
					if (con != null) con.Close();
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}
			return table;

		}
		/// <summary>
		/// Obtiene la info de una sola actividad
		/// </summary>
		/// <param name="idActividad"></param>
		/// <returns></returns>
		public DataTable ObtenerEspecifico(int idActividad)
		{
			DataTable table = null;
			SqlConnection con = null;
			SqlCommand command = null;
			string select = "SELECT RESERVACION.NOMBRE, ACTIVIDAD.PRESENTACION, ACTIVIDAD.NOMBRE, ACTIVIDAD.FECHA_HORA_INICIO, ACTIVIDAD.FECHA_HORA_FINAL" +
				" FROM ACTIVIDAD INNER JOIN RESERVACION ON ACTIVIDAD.IDRESERVACION = RESERVACION.IDRESERVACION " +
				" WHERE ACTIVIDAD.IDACTIVIDAD = @IDACTIVIDAD";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Close();
					using (command = new SqlCommand(select,con))
					{
						command.Parameters.Add(new SqlParameter("@IDACTIVIDAD",SqlDbType.Int)).Value = idActividad ;
						using (SqlDataAdapter adapter = new SqlDataAdapter(command))
						{
							table = new DataTable();
							adapter.Fill(table);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
			}
			finally
			{
				try
				{
					if (con != null) con.Close();
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}

			return table;
		}
		/// <summary>
		/// Elimiana la presentación de la actividad
		/// </summary>
		/// <param name="idActividad"></param>
		/// <returns></returns>
		public int EliminarPresentacion(int idActividad)
		{
			int resultado = 0;
			SqlConnection con = null;
			SqlCommand command = null;
			string update = "UPDATE ACTIVIDAD SET PRESENTACION = NULL WHERE IDACTIVIDAD = @ACTIVIDAD ";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update,con))
					{
						command.Parameters.Add(new SqlParameter("@ACTIVIDAD",SqlDbType.Int)).Value = idActividad;
						resultado = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
			}
			finally
			{
				try
				{
					if (con != null) con.Close();
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}

			return resultado;
		}
	}
}
