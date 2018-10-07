using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Datos
{
	public class Alerta
	{
		public int ALERTA_EXITO = 1;
		public int ALERTA_PRECAUCION = 2;
		public int ALERTA_ERROR = 3;


		/// <summary>
		/// Muestra una alerta
		/// </summary>
		/// <param name="tipoInsercion">Específica qué tipo de alerta se mostrará, 1:éxito 2:precaucion 3:error </param> 
		/// <param name="mensaje"></param>
		public void MostrarAlerta(int tipoInsercion, String mensaje, PlaceHolder phContent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<div class= \"alert {0} alert-dismissible\">",
				tipoInsercion == 1 ? "alert-success" : tipoInsercion == 2 ? "alert-warning" : "alert-danger");
			stringBuilder.AppendFormat("<span class=\"badge badge-pill {0} \">{1}</span>",
				tipoInsercion == 1 ? "badge-success" : tipoInsercion == 2 ? "badge-warning" : "badge-danger",
				tipoInsercion == 1 ? "Éxito" : tipoInsercion == 2 ? "Precaución" : "Error");
			stringBuilder.AppendLine("  " + mensaje);
			stringBuilder.AppendLine("<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>");
			stringBuilder.AppendLine("</div>");
			phContent.Controls.Add(new LiteralControl(stringBuilder.ToString()));
		}
	}
}