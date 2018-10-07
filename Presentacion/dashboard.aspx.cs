using System;
using Negocios.Acceso.Usuario;

namespace Presentacion
{
	public partial class dashboard : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//Verifica si existen las variables de usuario esenciales
				if (Session["idUsuario"] != null && Session["tipoUsuario"] != null)
				{
					DecidirUsuario();
				}
			}
		}
		/// <summary>
		/// Dadas las credenciales del usuario, se busca en la BD su idUsuario y su tipoUsuario
		/// Si se encuentran se redirige al módulo correspondiente
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void IniciarSesion(object sender, EventArgs e)
		{
			if (IsValid)
			{
				if (new Usuario().ObtenerIdUsuario(this.txtUsuario.Text, this.txtContrasena.Text, out int idUsuario, out int tipoUsuario))
				{
					Session["idUsuario"] = idUsuario;
					Session["tipoUsuario"] = tipoUsuario;
					DecidirUsuario();
				}
				else
				{
					new Datos.Alerta().MostrarAlerta(3, "Credenciales inválidas.", this.AlertaResultado);
				}
			}
		}
		/// <summary>
		/// Recupera la contraseña del usuario , dadas la palabra clave y correo electrónico
		/// Si coinciden con las del la BD se muestra la contraseña
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RecuperarContrasena(object sender, EventArgs e)
		{
			if (IsValid)
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("<script type = 'text/javascript'>");
				sb.Append("window.onload=function(){");
				sb.Append("alert('");
				if (new Usuario().RecuperarContrasenna(this.TxtPalabraClave.Text, this.TxtCorreo.Text, out string contrasena))
				{
					sb.Append(contrasena);
				}
				else
				{
					sb.Append("Credenciales inválidas");
				}
				sb.Append("')};");
				sb.Append("</script>");
				ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
				this.TxtCorreo.Text = String.Empty;
			}
		}
		/// <summary>
		/// Registra un nuevo estudiante
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RegistarUsuario(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					if ((new Usuario().Crear(4, Int32.Parse(this.TxtCarnet.Text), this.TxtEmail.Text, this.TxtNombre.Text, this.TxtApellido.Text, DateTime.Parse(this.TxtFecha.Text), Int32.Parse(this.TxtTelefono.Text), this.TxtUserName.Text, this.TxtPassword.Text, this.TxtClave.Text) != 1)
					{
						new Datos.Alerta().MostrarAlerta(1, "Ahora inicia sesión con tus credenciales", this.AlertaResultado);
					}
					else
					{
						new Datos.Alerta().MostrarAlerta(2, "Ocurrió un error al registrar el usuario. Si el problema persiste intente más tarde.", this.PHRegistro);
					}
				} catch (Exception ex)
				{
					new Datos.Alerta().MostrarAlerta(3, ex.Message, this.PHRegistro);
				}
			}
		}

		/// <summary>
		/// Determina a dónde enviar al usuario según su tipo
		/// </summary>
		private void DecidirUsuario()
		{
			//Procede a enviar a cada usuario a su lugar
			switch (Session["tipoUsuario"].ToString())
			{
				//Administrador
				case "1":
					break;
				//Operador
				case "2":
					break;
				//Instructor
				case "3":
					break;
				//Estudiante
				case "4":
					break;
				default:
					break;
			}
		}
	}
}