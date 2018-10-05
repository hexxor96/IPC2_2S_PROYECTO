using System;
using Negocios.Acceso;

namespace Presentacion
{
	public partial class iniciarsesion : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnSesion_Click(object sender, EventArgs e)
		{
			if (new Conexion().Conectar())
			{
			}

		}
	}
}