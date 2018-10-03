using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocios.Acceso.Escuela.Servicio.Reservacion
{
	class Reservacion
	{
		/// <summary>
		/// Crea una nueva reservación
		/// </summary>
		/// <param name="identificador"></param>
		/// <param name="nombre"></param>
		/// <param name="descripcion"></param>
		/// <param name="reservacion"></param>
		/// <param name="actividad"></param>
		/// <returns></returns>
		public int Crear(int[] identificador, String nombre, String descripcion, DateTime[] reservacion, DateTime[] actividad)
		{
			int resultado = -1; ;
			SqlConnection con = null;
			SqlTransaction tran = null;
			SqlCommand command = null;
			String insertReservacion = "INSERT INTO RESERVACION (IDINSTRUCTOR, IDOPERADOR, IDESTADO_RESERVACION," +
				"IDLABORATORIO, IDTIPO_ACTIVIDAD, IDPERIODICIDAD, " +
				"FECHA_HORA_PREVIA, NOMBRE, DESCRIPCION, FECHA_FINAL) " +
				"VALUES (@INSTRUCTOR, @OPERADOR, @ESTADO, @ESPACIO, @ACTIVIDAD, @PERIODO, " +
				"@PREVIA, @NOMBRE, @DESCRIPCION, @FINAL); " +
				"SELECT SCOPE_IDENTITY();";
			String insertActividad = "INSERT INTO ACTIVIDAD (IDRESERVACION,FECHA_HORA_INICIO, FECHA_HORA_FINAL) " +
				"VALUES (@IDRESERVA,@FECHA_INICIO,@FECHA_FINAL)";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (tran = con.BeginTransaction())
					{
						int idReservacion = 0;
						int aux = 0;
						using (command = new SqlCommand(insertReservacion,con,tran))
						{	
							//Registra la reservación
							//identificador -> int idInstructor, int idOperador, int idEstado, int idEspacio, int idActividad, int idPeriodo,
							//reservacion -> Fecha Previa, Fecha Final
							//actividad -> fecha_inicio, fecha_final
							command.Parameters.Add(new SqlParameter("@INSTRUCTOR", SqlDbType.Int)).Value = identificador[aux++];
							command.Parameters.Add(new SqlParameter("@OPERADOR", SqlDbType.Int)).Value = identificador[aux++];
							command.Parameters.Add(new SqlParameter("@ESTADO", SqlDbType.Int)).Value = identificador[aux++];
							command.Parameters.Add(new SqlParameter("@ESPACIO", SqlDbType.Int)).Value = identificador[aux++];
							command.Parameters.Add(new SqlParameter("@ACTIVIDAD", SqlDbType.Int)).Value = identificador[aux++];
							command.Parameters.Add(new SqlParameter("@PERIODO", SqlDbType.Int)).Value = identificador[aux++];
							aux = 0;
							command.Parameters.Add(new SqlParameter("@PREVIA", SqlDbType.DateTime)).Value = reservacion[aux++];
							command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.)).Value = nombre ;
							command.Parameters.Add(new SqlParameter("@DESCRIPCION", SqlDbType.Text)).Value = descripcion;
							command.Parameters.Add(new SqlParameter("@FINAL", SqlDbType.DateTime)).Value = reservacion[aux++];
							idReservacion = Convert.ToInt32(command.ExecuteScalar());
						}
						if (idReservacion > 0) {
							using (command = new SqlCommand(insertActividad, con, tran))
							{
								//Registra la actividad
								aux = 0;
								command.Parameters.Add(new SqlParameter("@IDRESERVA", SqlDbType.Int)).Value = idReservacion;
								command.Parameters.Add(new SqlParameter("@FECHA_INICIO", SqlDbType.Date)).Value = actividad[aux++];
								command.Parameters.Add(new SqlParameter("@FECHA_FINAL", SqlDbType.Date)).Value = actividad[aux++];
								resultado = command.ExecuteNonQuery();
							}
						}
						else
						{
							tran.Rollback();
						}
						tran.Commit();
					}

				}
			}
			catch (SqlException ex)
			{
				new Conexion().CrearError(ex);
				if (tran != null) tran.Rollback();
			}
			finally
			{
				try
				{	
					if (con != null)  con.Close();
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}
			return resultado;
		}
		/// <summary>
		/// Cambia a 'Expirado' o 'Finalizado'
		/// </summary>
		/// <param name="idReservacion"></param>
		/// <param name="idEstado"></param>
		/// <returns></returns>
		public int CambiarEstado(int idReservacion, int idEstado)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE RESERVACION SET IDESTADO_RESERVACION = @ESTADO WHERE IDRESERVACION = @IDENTIFICADOR";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand (update,con))
					{
						command.Parameters.Add(new SqlParameter("@ESTADO", SqlDbType.Int)).Value = idEstado;
						command.Parameters.Add(new SqlParameter("@IDENTIFICADOR", SqlDbType.Int )).Value = idReservacion;
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
		/// Actualiza el instructor, laboratorio, tipo de actividad, periodicidad, nombre, descripción y fecha final
		/// </summary>
		/// <returns></returns>
		public int Actualizar(int[] identificadores, String nombre, String descripcion, DateTime fechaFinal)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			//SqlTransaction transaction = null;
			String updateReservacion = "UPDATE RESERVACION SET IDINSTRUCTOR = @INSTRUCTOR, IDLABORATORIO = @ESPACIO, " +
				"IDTIPO_ACTIVIDAD = @ACTIVIDAD, IDPERIODICIDAD = @PERIODO, NOMBRE = @NOMBRE, DESCRIPCION = @DESCRIPCION, FECHA_FINAL = @FINAL " +
				" WHERE IDRESERVACION = @IDRESERVACION";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(updateReservacion,con))
					{
						int contador = 0;
						//idInstructor, idEspacio, idActividad, idPeriodo, idReservacion
						command.Parameters.Add(new SqlParameter("@INSTRUCTOR", SqlDbType.Int)).Value = identificadores[contador++];
						command.Parameters.Add(new SqlParameter("@ESPACIO", SqlDbType.Int)).Value = identificadores[contador++];
						command.Parameters.Add(new SqlParameter("@ACTIVIDAD", SqlDbType.Int)).Value = identificadores[contador++];
						command.Parameters.Add(new SqlParameter("@PERIODO", SqlDbType.Int)).Value = identificadores[contador++];
						command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = nombre;
						command.Parameters.Add(new SqlParameter("@DESCRIPCION", SqlDbType.Text)).Value = descripcion;
						command.Parameters.Add(new SqlParameter("@FINAL", SqlDbType.DateTime)).Value = fechaFinal;
						command.Parameters.Add(new SqlParameter("@IDRESERVACION", SqlDbType.Int)).Value = identificadores[contador++];
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
					if (con != null) con.Close;
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}
			return resultado;
		}
		/// <summary>
		/// Agrega la carta digitalizada
		/// </summary>
		/// <param name="idReservacion"></param>
		/// <param name="carta"></param>
		/// <param name="fecha"></param>
		/// <returns></returns>
		public int Finalizar(int idReservacion, byte[] carta, DateTime fecha)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			//SqlTransaction transaction = null;
			String updateReservacion = "UPDATE RESERVACION SET CARTA_RESERVACION = @CARTA, IDESTADO_RESERVACION = @ESTADO, FECHA_HORA_FINALIZADA = @FECHA " +
				" WHERE IDRESERVACION = @IDRESERVACION";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(updateReservacion, con))
					{
						command.Parameters.Add(new SqlParameter("@CARTA", SqlDbType.VarBinary)).Value = carta;
						command.Parameters.Add(new SqlParameter("@ESTADO", SqlDbType.Int)).Value = 2;
						command.Parameters.Add(new SqlParameter("@FECHA", SqlDbType.DateTime)).Value = fecha;
						command.Parameters.Add(new SqlParameter("@IDRESERVACION", SqlDbType.Int)).Value = idReservacion;
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
					if (con != null) con.Close;
				}
				catch (SqlException ex)
				{
					new Conexion().CrearError(ex);
				}
			}
			return resultado;
		}
		/// <summary>
		/// Todas las reservaciones
		/// idReservación, Operador, Instrutor, Estado, Espacio, Acividad, Periodicidasd, Fecha previa, Fecha finalizada, Fecha final, 
		/// Nombre reservacion
		/// </summary>
		/// <returns></returns>
		public DataTable ObtenerGeneral()
		{
			DataTable table = null;
			SqlConnection con = null;
			SqlCommand command;
			String select = "SELECT SUPER.IDRESERVACION , " +
				" (SELECT USUARIO.NOMBRE FROM RESERVACION INNER JOIN USUARIO ON USUARIO.IDUSUARIO = SUPER.IDOPERADOR) AS OPERADOR," +
				" (SELECT USUARIO.NOMBRE FROM RESERVACION INNER JOIN USUARIO ON USUARIO.IDUSUARIO = SUPER.IDINSTRUCTOR) AS INSTRUCTOR, " +
				" ESTADO.NOMBRE, " +
				" (SELECT CONCAT( EDIFICIO.NOMBRE , ' ' , LABORATORIO.NO_SALON ) FROM LABORATORIO INNER JOIN EDIFICIO " +
				" ON LABORATORIO.IDEDIFICIO = EDIFICIO.IDEDIFICIO INNER JOIN RESERVACION ON LABORATORIO.IDLABORATORIO = SUPER.IDLABORATORIO) AS LABORATORIO, " +
				" ACTIVIDAD.NOMBRE, " +
				" PERIODICIDAD.NOMBRE, " +
				" SUPER.FECHA_HORA_PREVIA,  " +
				" SUPER.FECHA_HORA_FINALIZADA, SUPER.FECHA_FINAL " +
				" SUPER.NOMBRE " +
				" FROM RESERVACION AS SUPER INNER JOIN ESTADO_RESERVACION AS ESTADO ON RESERVACION.IDESTADO_RESERVACION = ESTADO.IDESTADO_RESERVACION INNER JOIN " +
				" ACTIVIDAD ON RESERVACION.IDTIPO_ACTIVIDAD = ACTIVIDAD.IDTIPO_ACTIVIDAD INNER JOIN " +
				" PERIODICIDAD ON PERIODICIDAD.IDPERIODICIDAD = RESERVACION.IDPERIODICIDAD";
			try
			{
				using (con = new Conexion().Conectar();)
				{
					con.Open();
					using (command = new SqlCommand(select,con))
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
		/// Una reservación
		/// Operador, Instrutor, Estado, Espacio, Acividad, Periodicidad, carta, Fecha previa, Fecha finalizada, Fecha final, 
		/// Nombre reservacion, descripción reservación
		/// </summary>
		/// <returns></returns>
		public DataTable ObtenerEspecifico(int idReservacion)
		{
			DataTable table = null;
			SqlConnection con = null;
			SqlCommand command;
			String select = "SELECT (SELECT USUARIO.NOMBRE FROM RESERVACION INNER JOIN USUARIO ON USUARIO.IDUSUARIO = SUPER.IDOPERADOR) AS OPERADOR," +
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
				" WHERE SUPER.IDRESERVACION = @IDENTIFICADOR";
			try
			{
				using (con = new Conexion().Conectar();)
				{
					con.Open();
					using (command = new SqlCommand(select, con))
					{
						command.Parameters.Add(new SqlParameter("@IDENTIFICADOR", SqlDbType.Int)).Value = idReservacion;
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
		/// Obtiene la carta de reservación
		/// </summary>
		/// <param name="idReservacion"></param>
		/// <returns></returns>
		public byte[] ObtenerCarta(int idReservacion)
		{
			byte[] resultado = null;
			SqlConnection con = null;
			SqlCommand command;
			String select = "SELECT RESERVACION.CARTA_RESERVACION FROM RESERVACION " +
				" WHERE RESERVACION.IDRESERVACION = @IDENTIFICADOR";
			try
			{
				using (con = new Conexion().Conectar();)
				{
					con.Open();
					using (command = new SqlCommand(select, con))
					{
						command.Parameters.Add(new SqlParameter("@IDENTIFICADOR", SqlDbType.Int)).Value = idReservacion;
						resultado = command.ExecuteScalar() as byte[];
						/*using (reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								resultado = (byte[]) reader[""];
							}
						}*/
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
