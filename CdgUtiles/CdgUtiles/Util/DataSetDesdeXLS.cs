using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CdgUtiles.Util
{
    /// <summary>
    /// Clase de carga de datos desde archivos XLS
    /// </summary>
    public class DataSetDesdeXLS
    {

        /// <summary>
        /// Ejecuta la carga de datos desde el arcxhivo Excel y lo devuelve en un DataSet
        /// </summary>
        /// <param name="cPathArchivoXLS">Path absoluto al archivo XLS</param>
        /// <param name="bContieneCabeceras">SI el archivo contiene cabeceras o no</param>
        /// <returns>DataSet con los datos importados</returns>
        public static DataSet ImportarExcelXLS(string cPathArchivoXLS, bool bContieneCabeceras)
        {
            string HDR = bContieneCabeceras ? "Yes" : "No";
            string strConn;
            if (cPathArchivoXLS.Substring(cPathArchivoXLS.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + cPathArchivoXLS + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + cPathArchivoXLS + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn)) {
                conn.Open();

                DataTable schemaTable = conn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow schemaRow in schemaTable.Rows) {
                    string sheet = schemaRow["TABLE_NAME"].ToString();

                    if (!sheet.EndsWith("_")) {
                        try {
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            cmd.CommandType = CommandType.Text;

                            DataTable outputTable = new DataTable(sheet);
                            output.Tables.Add(outputTable);
                            new OleDbDataAdapter(cmd).Fill(outputTable);
                        } catch (Exception ex) {
                            throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, cPathArchivoXLS), ex);
                        }
                    }
                }
            }
            return output;
        } 

    }
}
