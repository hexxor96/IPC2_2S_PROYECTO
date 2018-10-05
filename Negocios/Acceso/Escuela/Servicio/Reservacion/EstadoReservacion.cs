using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocios.Acceso.Escuela.Servicio.Reservacion
{
	public class EstadoReservacion
	{
		/// <summary>
		/// Crea un nuevo estado de reservación
		/// </summary>
		/// <param name="nombre">Nombre del estado de reservación</param>
		/// <returns></returns>
		public int Crear(String nombre)
		{
			int resultado = -1;
			//Conexion a BD
			SqlConnection con = null;
			SqlCommand command = null;

			String insert = "INSERT INTO ESTADO_RESERVACION VALUES (@NOMBRE)";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(insert, con))
					{
						command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = nombre;
						resultado = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
				resultado = ex.Number;
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
		/// Obtiene toda la información almacenada en la etidad Tipo_USUARIO
		/// </summary>
		/// <returns></returns>
		public DataTable ObtenerGeneral()
		{
			//Conexión
			SqlConnection con = null;
			SqlCommand command = null;
			DataTable table = null;
			String query = "SELECT * FROM ESTADO_RESERVACION";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();

					using (command = new SqlCommand(query, con))
					{
						using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
						{
							table = new DataTable();
							dataAdapter.Fill(table);
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
		/// Actualiza el nombre del estado de reservación
		/// </summary>
		/// <param name="identificador"></param>
		/// <param name="nuevoNombre"></param>
		/// <returns>Resultado de la operación</returns>
		public int Actualizar(int identificador, String nuevoNombre)
		{
			int resultado = -1;
			//Conexion a BD
			SqlConnection con = null;
			SqlCommand command = null;
			String insert = "UPDATE ESTADO_RESERVACION SET NOMBRE = @NOMBRE WHERE IDESTADO_RESERVACION = @ID";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(insert, con))
					{
						command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = nuevoNombre;
						command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = identificador;
						resultado = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
				resultado = ex.Number;
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
		/// Elimina el estado de reservación 
		/// </summary>
		/// <param name="identificador"></param>
		/// <returns>Resultado de operación</returns>
		public int Eliminar(int identificador)
		{
			int resultado = -1;
			//Conexion a BD
			SqlConnection con = null;
			SqlCommand command = null;
			String delete = "DELETE ESTADO_RESERVACION WHERE IDESTADO_RESERVACION = @ID";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(delete, con))
					{
						command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = identificador;
						resultado = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
				resultado = ex.Number;
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
