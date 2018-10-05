using System;
using System.Data;
using System.Data.SqlClient;


namespace Negocios.Acceso.Usuario
{
	public class Usuario
    {
		/// <summary>
		/// Crea un nuevo usuario
		/// </summary>
		/// <param name="tipo_usuario">Número indicando el tipo de usuario</param>
		/// <param name="carnet">Número de carnet</param>
		/// <param name="email">Correo electrónico</param>
		/// <param name="nombre">Nombres</param>
		/// <param name="apellido">Apellidos</param>
		/// <param name="nacimiento">Fecha de Nacimiento</param>
		/// <param name="telefono">Número de teléfono</param>
		/// <param name="usuario">Nickname</param>
		/// <param name="contra">Contraseña</param>
		/// <param name="palabra">Palabra clave</param>
		/// <returns>El resultado de la operació</returns>
        public int Crear(int tipo_usuario, int carnet, String email, String nombre, String apellido, DateTime nacimiento, int telefono, String usuario, String contra, String palabra)
        {
            //
            int resultado = -1;

            //Conexion a BD
            SqlConnection con = null;
            SqlCommand command = null;

            String insert = "INSERT INTO USUARIO " +
                "VALUES (@idTIPO_USUARIO, @CARNET, @NOMBRE, @APELLIDO, @NACIMIENTO, @TELEFONO, @USER_, @PASS, @CLAVE, @EMAIL)";

            //
            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();
                    using (command = new SqlCommand(insert, con))
                    {
                        //
                        command.Parameters.Add(new SqlParameter("@idTIPO_USUARIO", SqlDbType.Int)).Value = tipo_usuario;
                        command.Parameters.Add(new SqlParameter("@CARNET", SqlDbType.Int)).Value = carnet;
                        command.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar)).Value = nombre;
                        command.Parameters.Add(new SqlParameter("@APELLIDO", SqlDbType.VarChar)).Value = apellido;
                        command.Parameters.Add(new SqlParameter("@NACIMIENTO", SqlDbType.Date)).Value = nacimiento;
                        command.Parameters.Add(new SqlParameter("@TELEFONO", SqlDbType.Int)).Value = telefono;
                        command.Parameters.Add(new SqlParameter("@USER_", SqlDbType.VarChar)).Value = usuario;
                        command.Parameters.Add(new SqlParameter("@PASS", SqlDbType.VarChar)).Value = contra;
                        command.Parameters.Add(new SqlParameter("@CLAVE", SqlDbType.VarChar)).Value = palabra;
                        command.Parameters.Add(new SqlParameter("@EMAIL", SqlDbType.VarChar)).Value = email;
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
		/// Se utiliza para iniciar sesión
		/// </summary>
		/// <param name="usuario">Credencial nickename</param>
		/// <param name="contrasenna">Credencial contraseñá</param>
		/// <param name="idUsuario">Devuelve el idUsuario</param>
		/// <param name="tipoUsuario">Devuelve el tipoUsuario</param>
		/// <returns>La existencia de un usuario con las credenciales específicadas</returns>
        public bool ObtenerIdUsuario(String usuario, String contrasenna, out int idUsuario, out int tipoUsuario)
        {
            //Asigna valores iniciales
            idUsuario = -1;
            tipoUsuario = -1;

            //Conexión
            SqlConnection con = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            String query = "SELECT USUARIO.IDUSUARIO, USUARIO.IDTIPO_USUARIO " +
                           "FROM USUARIO " +
                           "WHERE USUARIO.USER_ = @USER_ AND USUARIO.PASS = @PASS_";
            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();

                    using (command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add("@USER_", SqlDbType.VarChar).Value = usuario;
                        command.Parameters.Add("@PASS_", SqlDbType.VarChar).Value = contrasenna;

                        //Ejecuta la consulta
                        using (reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idUsuario = reader.GetInt32(0);
                                tipoUsuario = reader.GetInt32(1);
                            }
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
                    if (reader != null) reader.Close();
                    if (con != null) con.Close();
                }
                catch (SqlException ex)
                {
                    new Conexion().CrearError(ex);
                }
            }
            return idUsuario > 0 && tipoUsuario > 0;
        }
		/// <summary>
		/// Devuelve la contraseña de un usuario
		/// </summary>
		/// <param name="palabra">Palabra clave del usuario</param>
		/// <param name="email">Correo electrónico del usuario</param>
		/// <param name="contrasenna">Devuelve la contraseña del usuario</param>
		/// <returns>Determina si la palabra y el correo electrónico coincide con lo alojado en la BD</returns>
        public bool RecuperarContrasenna(String palabra, String email, out String contrasenna)
        {
            contrasenna = null;

            //Conexión
            SqlConnection con = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            String query = "SELECT USUARIO.PASS " +
                           "FROM USUARIO " +
                           "WHERE USUARIO.EMAIL = @EMAIL_ AND USUARIO.CLAVE = @CLAVE_";

            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();

                    using (command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add("@EMAIL_", SqlDbType.VarChar).Value = email;
                        command.Parameters.Add("@CLAVE_", SqlDbType.VarChar).Value = palabra;

                        using (reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contrasenna = reader.GetString(0);
                            }
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
                    if (reader != null) reader.Close();
                    if (con != null) con.Close();
                }
                catch (SqlException ex)
                {
                    new Conexion().CrearError(ex);
                }
            }

            return contrasenna != null;
        }
		/// <summary>
		/// Actualiza información personal del usuario
		/// </summary>
		/// <param name="idUsuario">Identifica al usuario</param>
		/// <param name="nombre">Nuevo nombre</param>
		/// <param name="apellido">Nuevo apellido</param>
		/// <param name="nacimiento">Nueva fecha de nacimiento</param>
		/// <param name="telefono">Nuevo teléfono</param>
		/// <returns>Resultado de la operación</returns>
		public int Actualizar (int idUsuario, String nombre, String apellido, DateTime nacimiento, int telefono, String clave)
		{
            int resultado = -1;
            //Conexion a BD
            SqlConnection con = null;
            SqlCommand command = null;
            String insert = "UPDATE USUARIO SET NOMBRE = @NOMBRE_ , " +
											   "APELLIDO = @APELLIDO_ , " +
											   "NACIMIENTO = @NACIMIENTO_, " +
											   "TELEFONO = @TELEFONO_, " +
											   "CLAVE = @CLAVE_ " +
							"WHERE IDUSUARIO = @IDUSUARIO_";
            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();
                    using (command = new SqlCommand(insert, con))
                    {
                        command.Parameters.Add(new SqlParameter("@NOMBRE_", SqlDbType.VarChar)).Value = nombre;
                        command.Parameters.Add(new SqlParameter("@APELLIDO_", SqlDbType.VarChar)).Value = apellido;
                        command.Parameters.Add(new SqlParameter("@NACIMIENTO_", SqlDbType.Date)).Value = nacimiento;
                        command.Parameters.Add(new SqlParameter("@TELEFONO_", SqlDbType.Int)).Value = telefono;
						command.Parameters.Add(new SqlParameter("@CLAVE_", SqlDbType.VarChar)).Value = clave;
						command.Parameters.Add(new SqlParameter("@IDUSUARIO_", SqlDbType.Int)).Value = idUsuario;
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
		/// Actualiza la contraseña del usuario
		/// </summary>
		/// <param name="idUsuario">Identifica al usuario</param>
		/// <param name="antigua">Antigua contraseña</param>
		/// <param name="nueva">Nueva contraseña</param>
		/// <returns>Resultado de la operación</returns>
		public int ActualizarContrasenna (int idUsuario, String antigua, String nueva)
		{
            int resultado = -1;

            //Conexion a BD
            SqlConnection con = null;
            SqlCommand command = null;

            String insert = "UPDATE USUARIO SET PASS = @PASS_ " +
                "WHERE IDUSUARIO = @IDUSUARIO_ AND PASS = @PASS__";

            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();
                    using (command = new SqlCommand(insert, con))
                    {
                        //
                        command.Parameters.Add(new SqlParameter("@PASS_", SqlDbType.VarChar)).Value = nueva;
                        command.Parameters.Add(new SqlParameter("@PASS__", SqlDbType.VarChar)).Value = antigua;
                        command.Parameters.Add(new SqlParameter("@IDUSUARIO_", SqlDbType.Int)).Value = idUsuario;
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
		/// Actualiza las credenciales del usuario
		/// </summary>
		/// <param name="idUsuario">Identifica al usuario</param>
		/// <param name="email">Nuevo correo electrónico</param>
		/// <param name="user">Nuevo nombre de usuario</param>
		/// <returns>Resultado de la operación</returns>
		public int ActualizarCredenciales (int idUsuario, String email, String user)
		{
			//
            int resultado = -1;

            //Conexion a BD
            SqlConnection con = null;
            SqlCommand command = null;

            String insert = "UPDATE USUARIO SET EMAIL = @EMAIL_, USER_ = @USERR " +
                "WHERE IDUSUARIO = @IDUSUARIO_ ";

            //
            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();
                    using (command = new SqlCommand(insert, con))
                    {
                        //
                        command.Parameters.Add(new SqlParameter("@EMAIL_", SqlDbType.VarChar)).Value = email;
                        command.Parameters.Add(new SqlParameter("@USERR", SqlDbType.VarChar)).Value = user;
                        command.Parameters.Add(new SqlParameter("@IDUSUARIO_", SqlDbType.Int)).Value = idUsuario;
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
		/// Obtiene todos los datos de un usuario
		/// </summary>
		/// <param name="idUsuario">Identifica al usuario</param>
		/// <returns>Tabla con información del usuario</returns>
		public DataTable ObtenerEspecifico (int idUsuario)
		{
            //Conexión
            SqlConnection con = null;
            SqlCommand command = null;
            DataTable table = null;
            String query = "SELECT T.NOMBRE, U.CARNET, U.NOMBRE, U.APELLIDO, U.NACIMIENTO, U.TELEFONO, U.USER_, U.CLAVE, U.EMAIL " +
				"FROM USUARIO AS U INNER JOIN TIPO_USUARIO AS T ON USUARIO.IDTIPO_USUARIO = TIPO_USUARIO.IDTIPO_USUARIO " +
				"WHERE U.IDUSUARIO = @idUser ";

            try
            {
                using (con = new Conexion().Conectar())
                {
                    con.Open();

                    using (command = new SqlCommand(query, con))
                    {
						command.Parameters.Add(new SqlParameter("@idUser", SqlDbType.Int)).Value = idUsuario;

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
		/// Elimina toda la información de un Usuario
		/// </summary>
		/// <param name="idUsuario">Identifica al usuario</param>
		/// <returns>El resultado de la operación</returns>
		public int Eliminar(int idUsuario)
		{
			int resultado = -1;
			//Conexion a BD
			SqlConnection con = null;
			SqlCommand command = null;
			String delete = "DELETE FROM USUARIO WHERE IDUSUARIO = @IDUSUARIO_";
			try
			{
				using (con = new Conexion().Conectar())
				{
					con.Open();
					using (command = new SqlCommand(delete, con))
					{
						command.Parameters.Add(new SqlParameter("@IDUSUARIO_", SqlDbType.Int)).Value = idUsuario;
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
