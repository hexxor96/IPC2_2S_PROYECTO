using System;
using System.Data.SqlClient;
using System.Text;

namespace Negocios.Acceso
{
	public class Conexion
	{
		private SqlConnection connection = null;

		public SqlConnection Conectar()
		{
			return connection = new SqlConnection(@"Data Source=DESKTOP-M0RM3OG\SQLEXPRESS;database=IPC2SISTEMAS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}
		/// <summary>
		/// Crea y despliega un mensaje de error en la consola
		/// </summary>
		/// <param name="ex"></param>
		public void CrearError(SqlException ex)
		{
			StringBuilder errorMessages = new StringBuilder();
			for (int i = 0; i < ex.Errors.Count; i++)
			{
				errorMessages.Append("Index #" + i + "\n" +
					"Message: " + ex.Errors[i].Message + "\n" +
					"Error Number: " + ex.Errors[i].Number + "\n" +
					"LineNumber: " + ex.Errors[i].LineNumber + "\n" +
					"Source: " + ex.Errors[i].Source + "\n" +
					"Procedure: " + ex.Errors[i].Procedure + "\n");
			}
			Console.WriteLine(errorMessages.ToString());
		}
		
	}


}
