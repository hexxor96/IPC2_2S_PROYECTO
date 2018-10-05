using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocios.Acceso.Escuela.Servicio.Reservacion
{
	public class TipoActividad
	{
		/// <summary>
		/// Crea un nuevo tipo de actividad
		/// </summary>
		/// <param name="nombre">Nombre del tipo de actividad</param>
		/// <returns></returns>
		public int Crear(String nombre)
		{
			int resultado = -1;
			//Conexion a BD
			SqlConnection con = null;
			SqlCommand command = null;

			String insert = "INSERT INTO TIPO_ACTIVIDAD VALUES (@NOMBRE)";
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
			String query = "SELECT * FROM TIPO_ACTIVIDAD";

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
		/// Actualiza el nombre del tipo de actividad
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
			String insert = "UPDATE TIPO_ACTIVIDAD SET NOMBRE = @NOMBRE WHERE IDTIPO_ACTIVIDAD = @ID";
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
		/// Elimina el tipo de actividad 
		/// </summary>
		/// <param name="identificador"></param>
		/// <returns>Resultado de operación</returns>
		public int Eliminar(int identificador)
		{
			int resultado = -1;
			//Conexion a BD
			SqlConnection con = null;
			SqlCommand command = null;
			String delete = "DELETE TIPO_ACTIVIDAD WHERE IDTIPO_ACTIVIDAD = @ID";
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
