using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocios.Acceso.Escuela
{
	public class Espacio
	{
		public int Crear(int idEdificio, int noSalon, int capacidad, int idDisponibilidad)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String insert = "INSERTO INTO LABORATORIO VALUES (@IDEDIFICIO, @NOSALON, @CAPACIDAD, @IDDISPONIBLIDAD);";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(insert,con))
					{
						command.Parameters.Add(new SqlParameter("@IDEDIFICIO", SqlDbType.Int)).Value = idEdificio;
						command.Parameters.Add(new SqlParameter("@NOSALON", SqlDbType.Int)).Value = noSalon;
						command.Parameters.Add(new SqlParameter("@CAPACIDAD", SqlDbType.Int)).Value = capacidad;
						command.Parameters.Add(new SqlParameter("@IDDISPONIBLIDAD", SqlDbType.Int)).Value = idDisponibilidad;
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
		public int Actualizar(int idLaboratorio, int idEdificio,int noSalon, int capacidad, int disponibilidad)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE LABORATORIO SET IDEDIFICIO = @IDEDI, NO_SALON = @NOSA, CAPACIDAD = @CAPA, IDDISPONIBLIDAD = @DISPO" +
				"WHERE IDLABORATORIO = @IDLABORATORIO";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update, con))
					{
						command.Parameters.Add(new SqlParameter("@IDEDI", SqlDbType.Int)).Value = idEdificio;
						command.Parameters.Add(new SqlParameter("@NOSA", SqlDbType.Int)).Value = noSalon;
						command.Parameters.Add(new SqlParameter("@CAPA", SqlDbType.Int)).Value = capacidad;
						command.Parameters.Add(new SqlParameter("@DISPO", SqlDbType.Int)).Value = disponibilidad;
						command.Parameters.Add(new SqlParameter("@IDLABORATORIO", SqlDbType.Int)).Value = idLaboratorio;
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
		public DataTable ObtenerGeneral()
		{
			SqlConnection con = null;
			SqlCommand command = null;
			DataTable table = null;
			String select = "SELECT L.IDLABORATORIO ,E.NOMBRE, L.NO_SALON, L.CAPACIDAD, D.NOMBRE" +
				"FROM LABORATORIO AS L INNER JOIN EDIFICIO  AS E ON LABORATORIO.IDEDIFICIO = EDIFICIO.IDEDIFICIO " +
				"INNER JOIN DISPONIBILIDAD AS D ON DISPONIBILIDAD.IDDISPONIBILIDAD = LABORATORIO.IDDISPONIBILIDAD";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(select, con))
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
		public DataTable ObtenerEspecifico(int idLaboratorio)
		{
			SqlConnection con = null;
			SqlCommand command = null;
			DataTable table = null;
			String select = "SELECT E.NOMBRE, L.NO_SALON, L.CAPACIDAD, D.NOMBRE" +
				"FROM LABORATORIO AS L INNER JOIN EDIFICIO  AS E ON LABORATORIO.IDEDIFICIO = EDIFICIO.IDEDIFICIO " +
				"INNER JOIN DISPONIBILIDAD AS D ON DISPONIBILIDAD.IDDISPONIBILIDAD = LABORATORIO.IDDISPONIBILIDAD" +
				"WHERE L.IDLABORATORIO = @IDLABORATORIO";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(select,con))
					{
						command.Parameters.Add(new SqlParameter("@IDLABORATORIO",SqlDbType.Int)).Value = idLaboratorio;
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
		public int Eliminar(int idLaboratorio)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String delete = "DELETE FROM LABORATORIO WHERE IDLABORATORIO = @IDLABORATORIO";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(delete,con))
					{
						command.Parameters.Add(new SqlParameter("@IDLABORATORIO",SqlDbType.Int)).Value = idLaboratorio;
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
		public int CambiarDisponibilidad(int idLaboratorio, int disponibilidad)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE LABORATORIO SET IDDISPONIBLIDAD = @DISPO WHERE IDLABORATORIO = @IDLABORATORIO";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update, con))
					{
						command.Parameters.Add(new SqlParameter("@DISPO", SqlDbType.Int)).Value = disponibilidad;
						command.Parameters.Add(new SqlParameter("@IDLABORATORIO", SqlDbType.Int)).Value = idLaboratorio;
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
