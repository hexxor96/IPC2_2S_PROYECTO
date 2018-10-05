using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocios.Acceso.Estudiante
{

	public class Estudiante
	{
		/// <summary>
		/// Crea una registro en la entidad asistencia, validando así la asistencia del estudiante
		/// </summary>
		/// <param name="idActividad"></param>
		/// <param name="idEstudiante"></param>
		/// <param name="fecha_hora"></param>
		/// <returns></returns>
		public int ValidarAsistencia(int idActividad, int idEstudiante, DateTime fecha_hora)
		{
			int resultado = 0;
			SqlConnection con = null;
			SqlCommand command = null;
			string update = "INSERT INTO ASISTENCIA VALUES (@IDACTIVIDAD, " +
				"(SELECT MATRICULACION.IDMATRICULACION FROM MATRICULACION WHERE MATRICULACION.IDUSUARIO = @IDESTUDIANTE), @FECHA_HORA )";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update,con))
					{
						command.Parameters.Add(new SqlParameter("@IDACTIVIDAD", SqlDbType.Int)).Value = idActividad;
						command.Parameters.Add(new SqlParameter("@IDESTUDIANTE",SqlDbType.Int)).Value = idActividad;
						command.Parameters.Add(new SqlParameter("@FECHA_HORA",SqlDbType.DateTime)).Value = idActividad;
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

				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}
			return resultado;
		}
		/// <summary>
		/// Crea un registro en la entidad matricualción, validando así la matriculación del estudiante
		/// </summary>
		/// <param name="idReservacion"></param>
		/// <param name="idEstudiante"></param>
		/// <param name="fecha_hora"></param>
		/// <returns></returns>
		public int MatricularReservacion(int idReservacion, int idEstudiante, DateTime fecha_hora)
		{
			int resultado = 0;
			SqlConnection con = null;
			SqlCommand command = null;
			string insert = "INSERT INTO MATRICULACION VALUES (@IDRESERVACION,@IDUSUARIO,@FECHA_MATRICULACION,@ACTIVA)";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(insert,con))
					{
						command.Parameters.Add(new SqlParameter("@IDRESERVACION", SqlDbType.Int)).Value = idReservacion;
						command.Parameters.Add(new SqlParameter("@IDUSUARIO", SqlDbType.Int)).Value = idEstudiante;
						command.Parameters.Add(new SqlParameter("@FECHA_MATRICULACION", SqlDbType.DateTime)).Value = fecha_hora;
						command.Parameters.Add(new SqlParameter("@ACTIVA",SqlDbType.Bit)).Value = 1;
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
