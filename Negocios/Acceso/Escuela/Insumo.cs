using System;
using System.Data;
using System.Data.SqlClient;
namespace Negocios.Acceso.Escuela
{
	class Insumo
	{
		public int Crear(String nombre, String descripcion, byte[] foto ,int tipoInsumo, int disponibilidad)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String insert = "INSERTO INTO INSUMO VALUES (@IDTIPO_INSUMO,@IDDISPONIBLIDAD,@NOMBRE,@DESCRIPCION,@FOTO)";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(insert, con))
					{
						command.Parameters.Add(new SqlParameter("@IDTIPO_INSUMO", SqlDbType.Int)).Value = tipoInsumo;
						command.Parameters.Add(new SqlParameter("@IDDISPONIBLIDAD", SqlDbType.Int)).Value = disponibilidad;
						command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = nombre;
						command.Parameters.Add(new SqlParameter("@DESCRIPCION", SqlDbType.Text)).Value = descripcion;
						command.Parameters.Add(new SqlParameter("@FOTO", SqlDbType.VarBinary)).Value = foto;
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
		public int Actualizar(int idInsumo,String nombre, String descripcion, int tipoInsumo, int disponibilidad)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE INSUMO SET IDTIPO_INSUMO = @TIPOINSUMO, NOMBRE = @NAME, DESCRIPCION = @DESC, IDDISPONIBILIDAD = @DISPO " +
				"WHERE IDINSUMO = @IDINSUMO";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update, con))
					{
						command.Parameters.Add(new SqlParameter("@TIPOINSUMO", SqlDbType.Int)).Value = tipoInsumo;
						command.Parameters.Add(new SqlParameter("@NAME", SqlDbType.VarChar)).Value = nombre;
						command.Parameters.Add(new SqlParameter("@DESC", SqlDbType.Text)).Value = descripcion;
						command.Parameters.Add(new SqlParameter("@DISPO", SqlDbType.Int)).Value = disponibilidad;
						command.Parameters.Add(new SqlParameter("@IDINSUMO", SqlDbType.Int)).Value = idInsumo;
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
		public int ActualizarFoto(int idInsumo, byte[] foto)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE INSUMO SET  FOTO = @FOTO " +
				"WHERE IDINSUMO = @IDINSUMO";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update, con))
					{
						command.Parameters.Add(new SqlParameter("@FOTO", SqlDbType.VarBinary)).Value = foto;
						command.Parameters.Add(new SqlParameter("@IDINSUMO", SqlDbType.Int)).Value = idInsumo;
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
			String select = "SELECT I.IDINSUMO, I.NOMBRE, I.DESCRIPCION, D.NOMBRE, T.NOMBRE, I.FOTO " +
				"FROM INSUMO AS I INNER JOIN TIPO_INSUMO  AS T ON INSUMO.IDTIPO_INSUMO = TIPO_INSUMO.IDTIPO_INSUMO " +
				"INNER JOIN DISPONIBILIDAD AS D ON DISPONIBILIDAD.IDDISPONIBILIDAD = INSUMO.IDDISPONIBILIDAD";

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
		public DataTable ObtenerEspecifico(int idInsumo)
		{
			SqlConnection con = null;
			SqlCommand command = null;
			DataTable table = null;
			String select = "SELECT I.NOMBRE, I.DESCRIPCION, D.NOMBRE, T.NOMBRE, I.FOTO " +
				"FROM INSUMO AS I INNER JOIN TIPO_INSUMO  AS T ON INSUMO.IDTIPO_INSUMO = TIPO_INSUMO.IDTIPO_INSUMO " +
				"INNER JOIN DISPONIBILIDAD AS D ON DISPONIBILIDAD.IDDISPONIBILIDAD = INSUMO.IDDISPONIBILIDAD " +
				"WHERE I.IDINSUMO = @IDINSUMO";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(select, con))
					{
						command.Parameters.Add(new SqlParameter("@IDINSUMO", SqlDbType.Int)).Value = idInsumo;
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
		public int Eliminar(int idInsumo)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String delete = "DELETE FROM INSUMO WHERE IDINSUMO = @IDINSUMO";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(delete, con))
					{
						command.Parameters.Add(new SqlParameter("@IDINSUMO", SqlDbType.Int)).Value = idInsumo;
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
		public int CambiarDisponibilidad(int idInsumo, int disponibilidad)
		{
			int resultado = -1;
			SqlConnection con = null;
			SqlCommand command = null;
			String update = "UPDATE INSUMO SET IDDISPONIBLIDAD = @DISPO WHERE IDINSUMO = @IDINSUMO";

			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(update, con))
					{
						command.Parameters.Add(new SqlParameter("@DISPO", SqlDbType.Int)).Value = disponibilidad;
						command.Parameters.Add(new SqlParameter("@IDINSUMO", SqlDbType.Int)).Value = idInsumo;
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
