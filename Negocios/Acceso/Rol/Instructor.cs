using System;
using System.Data.SqlClient;
using System.Data;

namespace Negocios.Acceso.Instructor
{
	public class Instructor
	{
		/// <summary>
		/// Obtiene todas las reservaciones que el instructor ha realizado
		/// </summary>
		/// <param name="idInstructor"></param>
		/// <returns></returns>
		public DataTable ObtenerReservaciones(int idInstructor)
		{
			DataTable table = null;
			SqlConnection con = null;
			SqlCommand command = null;
			String select = "SELECT SUPER.IDRESERVACION, (SELECT USUARIO.NOMBRE FROM RESERVACION INNER JOIN USUARIO ON USUARIO.IDUSUARIO = SUPER.IDOPERADOR) AS OPERADOR," +
				" (SELECT USUARIO.NOMBRE FROM RESERVACION INNER JOIN USUARIO ON USUARIO.IDUSUARIO = SUPER.IDINSTRUCTOR) AS INSTRUCTOR, " +
				" ESTADO.NOMBRE, " +
				" (SELECT CONCAT( EDIFICIO.NOMBRE , ' ' , LABORATORIO.NO_SALON ) FROM LABORATORIO INNER JOIN EDIFICIO " +
				" ON LABORATORIO.IDEDIFICIO = EDIFICIO.IDEDIFICIO INNER JOIN RESERVACION ON LABORATORIO.IDLABORATORIO = SUPER.IDLABORATORIO) AS LABORATORIO, " +
				" ACTIVIDAD.NOMBRE, " +
				" PERIODICIDAD.NOMBRE, " +
				" SUPER.FECHA_HORA_PREVIA,  " +
				" SUPER.FECHA_HORA_FINALIZADA, SUPER.FECHA_FINAL " +
				" SUPER.NOMBRE," +
				" SUPER.DESCRIPCION " +
				" FROM RESERVACION AS SUPER INNER JOIN ESTADO_RESERVACION AS ESTADO ON RESERVACION.IDESTADO_RESERVACION = ESTADO.IDESTADO_RESERVACION INNER JOIN " +
				" ACTIVIDAD ON RESERVACION.IDTIPO_ACTIVIDAD = ACTIVIDAD.IDTIPO_ACTIVIDAD INNER JOIN " +
				" PERIODICIDAD ON PERIODICIDAD.IDPERIODICIDAD = RESERVACION.IDPERIODICIDAD" +
				" WHERE SUPER.IDINSTRUCTOR = @Instructor" +
				" ORDER BY SUPER.FECHA_HORA_PREVIA ASC ";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(select,con))
					{
						command.Parameters.Add(new SqlParameter("@Instructor",SqlDbType.Int)).Value = idInstructor;
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

		
	}
}
