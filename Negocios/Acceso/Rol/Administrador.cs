using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Negocios.Acceso.Administrador
{
	/// <summary>
	/// Contiene las funciones del administrador
	/// </summary>
	public class Administrador
	{
		/// <summary>
		/// Carga datos de forma masiva desde una hoja de cálculo .xlsx
		/// </summary>
		/// <param name="excelCS">Path del archivo .xlsx</param>
		/// <param name="destino">Entidad destino de la información</param>
		/// <returns>Resultado de la operación</returns>
		public int CargarMasivamente(String excelCS, String destino)
		{
			int resultado = -1;
			//Conexión
			SqlConnection SQLcon = null;
			OleDbConnection OLECon = null;
			OleDbCommand OLEcommand = null;
			String oleSelect = "SELECT * FROM [Sheet1$]";
			try
			{
				using (OLECon = new OleDbConnection(excelCS))
				{
					OLEcommand = new OleDbCommand(oleSelect,OLECon);
					OLECon.Open();
					//Lee la info del .xlsx
					DbDataReader reader = OLEcommand.ExecuteReader();
					//Conecta con la BD
					using (SQLcon = new Conexion().Conectar())
					{
						//Vuelca la info en la BD
						SqlBulkCopy bulk = new SqlBulkCopy(SQLcon)
						{
							DestinationTableName = destino
						};
						bulk.WriteToServer(reader);
					}
				}
				resultado = 1;
			}
			catch (Exception ex)
			{
				resultado = 0;
				Console.WriteLine("Message: " + ex.Message + Environment.NewLine + "Source: " + ex.Source + Environment.NewLine);
			}
			finally
			{
				try
				{
					if (SQLcon != null) SQLcon.Close();
					if (OLECon != null) OLECon.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Message: " + ex.Message + Environment.NewLine + "Source: " + ex.Source + Environment.NewLine);
				}
			}
			return resultado;
		}


	}
}
