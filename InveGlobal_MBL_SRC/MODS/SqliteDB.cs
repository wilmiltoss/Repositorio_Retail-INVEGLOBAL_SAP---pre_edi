using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace InveStockMBL.MODS
{
    /// <summary>
    /// INTERFACE CON LA BASE DE DATOS SQLITE
    /// </summary>
    class SqliteDB
    {
        #region DECLARACION_ATRIBUTOS_Y_VARIABLES

        string strBaseDatos= string.Empty;

        private SQLiteConnection conSQLiteConexion;             //Conexion a la BD SQLite (Clase de System.Data.SQLite)
        private SQLiteDataAdapter dadSQLiteDataAdapter;         //Adaptador (Clase de System.Data.SQLite)
        private SQLiteCommand cmdSQLiteComando;                 //Comando SQL para Ejecucion
        private DataSet dstSQLiteDataSet = new DataSet();       //DataSet
        private DataTable dtbSQLiteDataTable = new DataTable(); //DataTable
        //private DataRow drwFila;                              //DataRow

        //CONSTANTES DEL MODULO
        private const String DELETE_LECTURAS = "DELETE FROM LECTURAS";
        private const String INSERT_LECTURA = "INSERT INTO LECTURAS (ID_LOCACION, NRO_CONTEO, ID_SOPORTE, NRO_SOPORTE, ID_LETRA_SOPORTE, NIVEL, METRO, SCANNING, CANTIDAD, ID_USUARIO) "
                                        + " VALUES({0}, {1}, '{2}', {3}, {4}, {5}, {6}, '{7}', {8}, {9})";
        private const String UPDATE_LECTURAS = "UPDATE LECTURAS SET CANTIDAD= {0}, ID_USUARIO= {9} "
                                        + " WHERE ID_LOCACION= {1} AND NRO_CONTEO= {2} AND ID_SOPORTE= {3} AND NRO_SOPORTE= {4} "
                                        + " AND ID_LETRA_SOPORTE= {5} AND NIVEL= {6} AND METRO= {7} AND SCANNING= '{8}'";

        private const String VALIDAR_USUARIO = "SELECT ID_USUARIO, NIVEL_ACCESO FROM USUARIOS WHERE ID_USUARIO= '{0}'";
        private const String COUNT_LECTURAS = "SELECT COUNT(ID_LOCACION) FROM LECTURAS";

        #endregion

        /// <summary>
        /// CONSTRUCTOR DE LA CLASE
        /// </summary>
        /// <param name="strArchivoBD">Path Absoluto del Archivo de BD</param>
        public SqliteDB(String strArchivoBD)
        {
            //pasamos el string al Atributo de la Clase
            this.strBaseDatos = strArchivoBD;
        }//FIN del Contructor de la Clase

        /// <summary>
        /// CONECTA A LA BASE DE DATOS, DEVUELVE UN BOOLEAN
        /// </summary>
        /// <returns>Devuelve 'True' si se Conecto Exitosamente</returns>
        public Boolean bolConectar_BD()
        {
            //si no existe el archivo
            if (!File.Exists(this.strBaseDatos))
            {
                //mensaje de notificacion
                MessageBox.Show("No Existe la Base de Datos", "Archivo BD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;

            }//FIN evalua existe Archivo BD

            //Creamos la conexion a la BD. El Data Source contiene el path del archivo de la BD
            conSQLiteConexion = new SQLiteConnection("Data Source=" + this.strBaseDatos + ";Version=3;New=False;Compress=True;");

            //MessageBox.Show(this.strBaseDatos);

            //intentamos abrir la conexion
            try
            {
                conSQLiteConexion.Open();

                //devolvemos el resultado de la funcion
                return true;
            }
            catch (SQLiteException Ex)
            {
                //en caso de error SQLite, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Conexion a BD SQLite", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;
            }
            catch (Exception Ex)
            {
                //en caso de error de otro tipo, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Conexion a BD SQLite", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;
            }//FIN intento de Conexion a BD
        }//FIN de Conectar a BD

        /// <summary>
        /// EJECUTA UNA CONSULTA SQL Y DEVUELVE UN DATATABLE
        /// </summary>
        /// <param name="strSentenciaSQL">Sentencia de Consulta SQL</param>
        /// <returns>Devuelve un DataTable con los Resutlados de la Consulta</returns>
        public DataTable dtbEjecutar_ConsultaSQL(String strSentenciaSQL)
        {
            //intentamos ejecutar la sentencia
            try
            {
                //reinstanciamos los objetos a utilizar
                dadSQLiteDataAdapter = new SQLiteDataAdapter(strSentenciaSQL, conSQLiteConexion);
                dtbSQLiteDataTable = new DataTable();

                //pasamos los resultados al DataTable
                dadSQLiteDataAdapter.Fill(dtbSQLiteDataTable);

                //devolvemos el resultado de la funcion
                return dtbSQLiteDataTable;
            }
            catch (SQLiteException Ex)
            {
                //si el error es manejable por el SQLite, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Ejecución de Consulta SQL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return dtbSQLiteDataTable;
            }
            catch (Exception Ex)
            {
                //si el error es de otro tipo, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Ejecución de Consulta SQL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return dtbSQLiteDataTable;
            }
        }//FIN de Ejecucion de Consulta SQL

        /// <summary>
        /// EJECUTA UNA SENTENCIA SQL Y DEVUELVE UN BOOLEANO
        /// </summary>
        /// <param name="strSentenciaSQL">Sentencia SQL a Ejecutar</param>
        /// <returns>Devuelve 'True' si se Ejecuto Correctamente</returns>
        public Boolean bolEjecutar_SentenciaSQL(String strSentenciaSQL)
        {
            //intentamos ejecutar la Sentencia SQL
            try
            {
                cmdSQLiteComando = new SQLiteCommand(strSentenciaSQL, conSQLiteConexion);

                //ejecutamos la Sentencia SQL
                cmdSQLiteComando.ExecuteNonQuery();

                //devolvemos el resultado de la funcion
                return true;
            }
            catch (SQLiteException Ex)
            {
                //si el error es manejable por el SQLite, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Ejecucion Sentencia SQL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;
            }
            catch (Exception Ex)
            {
                //si el error es de otro tipo, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Error de Ejecutando Sentencia SQL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;
            }
        }//FIN de Ejecutar Sentencia SQL

        /// <summary>
        /// CREA UN DATASET EN BASE A UNA CONSULTA SQL
        /// </summary>
        /// <param name="strConsultaSQL">Sentencia de Consulta SQL</param>
        /// <returns>Devuelve el DataSet con los Resultados de la Consulta SQL</returns>
        public DataSet dstCrear_DataSet(String strConsultaSQL)
        {
            //intentamos ejecutar la sentencia
            try
            {
                dadSQLiteDataAdapter = new SQLiteDataAdapter(strConsultaSQL, conSQLiteConexion);

                //pasamos los resultados al DataSet
                dadSQLiteDataAdapter.Fill(dstSQLiteDataSet);

                //devolvemos el resultado de la funcion
                return dstSQLiteDataSet;
            }
            catch (SQLiteException Ex)
            {
                //si el error es manejable por el SQLite, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Error Creando DataSet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return dstSQLiteDataSet;
            }
            catch (Exception Ex)
            {
                //si el error es de otro tipo, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Error Creando DataSet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return dstSQLiteDataSet;
            }
        }//FIN de Crear DataSet

        /// <summary>
        /// CARGA LOS RESULTADO SE UNA CONSULTA SQL A UN COMBO
        /// </summary>
        /// <param name="strSentneciaSQL">Sentencia de Consulta SQL</param>
        /// <param name="cmbComboDestino">Combo al que se Cargaran los Items</param>
        /// <returns>Devuelve 'True' si se Cargaron Correctamente</returns>
        public Boolean bolCargar_Valores_Combo(String strSentenciaSQL, ComboBox cmbComboDestino)
        {
            // creamos una instancia del DataSet
            dstSQLiteDataSet = new DataSet();

            //intentamos ejecutar la Consulta SQL
            try
            {
                dstSQLiteDataSet =  this.dstCrear_DataSet(strSentenciaSQL);

                //si el dataset fue cargado (contiene al menos una tabla)
                if (!dstSQLiteDataSet.Tables.Count.Equals(0))
                {
                    //recorremos las tablas devueltas
                    foreach (DataTable dtbTabla in dstSQLiteDataSet.Tables)
                    {
                        //recorremos las filas de la tabla actual
                        foreach (DataRow drwFila in dtbTabla.Rows)
                        {
                            //cargamos el valor del campo en el combo
                            cmbComboDestino.Items.Add(drwFila[0]);
                        }
                        //salimos del bucle
                        break;
                    }
                }
                else
                {
                    //sino, cargamos la palabra "VACIO"
                    cmbComboDestino.Items.Add("VACIO");
                }

                //mostramos el primer valor del combo
                cmbComboDestino.Text = cmbComboDestino.Items[0].ToString();

                //liberamos el dataset
                dstSQLiteDataSet.Dispose();

                //devolvemos el resultado de la funcion
                return true;

            }
            catch(Exception Ex)
            {
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Carga Valores a Combo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //cargamos la palabra "VACIO"
                cmbComboDestino.Items.Add("VACIO");

                //mostramos el primer valor del combo
                cmbComboDestino.Text = cmbComboDestino.Items[0].ToString();

                //devolvemos el resultado de la funcion
                return false;
            }
        }//FIN Cargar Valores Combo

        /// <summary>
        /// CIERRA LA CONEXION ACTUAL A LA BASE DE DATOS
        /// </summary>
        private void proCerrar_ConexionBD()
        {
            //intentamos cerrar la Conexion Actual
            try
            {
                this.conSQLiteConexion.Close();
            }
            catch (Exception Ex)
            {
                //en caso de error, nada
            }
        }//FIN de Cerrar Conexion a BD

        /// <summary>
        /// INSERTA UN REGISTRO EN LA TABLA DE [LECTURAS] Y DEVUELVE UN BOOLEANO
        /// </summary>
        /// <param name="strScanning">Scanning del Articulo</param>
        /// <param name="strID_Soporte">ID del Soporte</param>
        /// <param name="strID_letra_Soporte">ID de la Letra del Soporte</param>
        /// <param name="strCantidad">Cantidad en Stock</param>
        /// <param name="strIdColector">Nombre de la Colectora</param>
        /// <returns>Devuelve 'True' si se inserto Correctamente</returns>
        public Boolean bolInsertar_Lectura(String strIDLocacion, String strNroConteo, String strIDSoporte, String strNroSoporte, String strIDLetraSoporte, String strNivel, String strMetro, String strScanning, String strCantidad, String strIdUsuario)
        {
            //si No se especifico un Id de Usuario
            if (strIdUsuario.Trim().Equals(String.Empty))
            {
                //le especificamos un valor por defecto
                strIdUsuario = "0";
            }

            //intentamos ejecutar la sentencia de insercion
            try
            {
                //ensamblamos la Sentencia SQL de Insercion
                String strSQLiteSentencia = String.Format(INSERT_LECTURA, strIDLocacion, strNroConteo, strIDSoporte, strNroSoporte, strIDLetraSoporte, strNivel, strMetro, strScanning, strCantidad, strIdUsuario);


                //llamamos a Funcion de Ejecucion de Sentencia SQL
                // y devolvemos el resultado que devuelve la funcion
                return this.bolEjecutar_SentenciaSQL(strSQLiteSentencia);
            
            }
            catch (Exception Ex)
            {
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Insercion de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;
            }
        }//FIN de Insertar Lectura

        /// <summary>
        /// ACTUALIZA UN REGISTRO DE LA TABLA DE [LECTURAS] Y DEVUELVE UN BOOLEANO
        /// </summary>
        /// <param name="strScanning">Scanning del Articulo</param>
        /// <param name="strID_Soporte">ID del Soporte</param>
        /// <param name="strID_letra_Soporte">ID de la Letra del Soporte</param>
        /// <param name="strCantidad">Cantidad en Stock</param>
        /// <returns>Devuelve 'True' si se inserto Correctamente</returns>
        public Boolean bolActualizar_Lectura(String strID_Locacion, String strNro_Conteo, String strID_Soporte, String strNro_Soporte, String strID_Letra_Soporte, String strNivel, String strMetro, String strScanning, String strCantidad, String strIdUsuario)
        {
            //si No se especifico un Id de Usuario
            if (strIdUsuario.Trim().Equals(String.Empty))
            {
                //le especificamos un valor por defecto
                strIdUsuario = "0";
            }


            //intentamos ejecutar la sentencia de insercion
            try
            {
                //ensamblamos la Sentencia SQL de Insercion
                string strSQLiteSentencia = string.Format(UPDATE_LECTURAS, strCantidad, strID_Locacion, strNro_Conteo, strID_Soporte,
                                                        strNro_Soporte, strID_Letra_Soporte, strNivel, strMetro, strScanning, strIdUsuario);

                //llamamos a Funcion de Ejecucion de Sentencia SQL
                // y devolvemos el resultado que devuelve la funcion
                return this.bolEjecutar_SentenciaSQL(strSQLiteSentencia);

            }
            catch (Exception Ex)
            {
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Actualizacion de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;
            }
        }//FIN de Actualizar Lectura

        /// <summary>
        /// ELIMINA LOS REGISTROS DE LA TABLA [LECTURAS] Y DEVUELVE UN BOOLEANO
        /// </summary>
        /// <returns>Devuelve 'True' si se Eliminaron Correctamente</returns>
        public Boolean bolLimpiar_Tabla_Lecturas()
        {
            return this.bolEjecutar_SentenciaSQL(DELETE_LECTURAS);

            //compactamos el archivo de base de datos
            //if (bolCompactar_BD()){}

        }//FIN de Limpiar Tabla Lecturas

        /// <summary>
        /// COMPACTA LA BASE DE DATOS Y DEVUELVE UN BOOLEANO
        /// </summary>
        /// <returns>Deuelve 'True' si se Ejecuto Correctamente</returns>
        public Boolean bolCompactar_BD()
        {
            //llamamos a funcion de Ejecucion de Sentencia SQL y devolvemos su mismo resultado
            return this.bolEjecutar_SentenciaSQL("VACUUM");
        }//FIN de Compactar BD

        /// <summary>
        /// Valida los Datos de un Usuario
        /// </summary>
        /// <param name="strIDUsuario">ID del Usuario</param>
        /// <returns></returns>
        public Boolean bolUsuario_Valido(String strIDUsuario)
        {
            //la ejecutamos y guardamos el resultado
            dtbSQLiteDataTable = dtbEjecutar_ConsultaSQL(string.Format(VALIDAR_USUARIO, strIDUsuario));

            //si hay filas devueltas
            if (dtbSQLiteDataTable.Rows.Count > 0)
            {
                //tomamos cada fila
                foreach (DataRow drwFila in dtbSQLiteDataTable.Rows)
                {
                    //si el valor del primer campo es igual al id de usuario
                    if (drwFila[0].ToString().Equals(strIDUsuario))
                    {
                        //devolvemos el resultado del metodo
                        return true;
                    }
                    else
                    {
                        //sino, devolvemos el resultado del metodo
                        return false;
                    }
                }
                //devolvemos el resultado del metodo
                return false;
            }
            else
            {
                //sino, devolvemos el resultado del metodo
                return false;
            }

        }//FIN de Usuario_Valido

        /// <summary>
        /// DEVUELVE LA CANTIDAD DE LECTURAS REGISTRADAS COMO UN STRING
        /// </summary>
        /// <returns>Devuelve la Cantidad de Lecturas Realizadas</returns>
        public string strCantidad_Lecturas()
        {
            //la ejecutamos y guardamos el resultado
            dtbSQLiteDataTable = dtbEjecutar_ConsultaSQL(COUNT_LECTURAS);

            //si hay filas devueltas
            if (dtbSQLiteDataTable.Rows.Count > 0)
            {
                //tomamos cada fila
                foreach (DataRow drwFila in dtbSQLiteDataTable.Rows)
                {
                    //si el valor del primer campo es igual al id de usuario
                    return drwFila[0].ToString();
                }
                //devolvemos el resultado del metodo
                return "0";
            }
            else
            {
                //sino, devolvemos el resultado del metodo
                return "0";
            }
        }

        /// <summary>
        /// Validar el nivel de acceso solicitado para un usuario
        /// </summary>
        /// <param name="cIdUsuario">identificador del usuario</param>
        /// <param name="nNivel">nivel a validar</param>
        /// <returns>'True' si el usuario tiene el nivel de acceso requerido</returns>
        public bool bolValidar_nivel_acceso(string strIdUsuario, int nNivel)
        {
            //resultado por defecto
            bool bResultado = false;

            //la ejecutamos y guardamos el resultado
            dtbSQLiteDataTable = dtbEjecutar_ConsultaSQL(string.Format(VALIDAR_USUARIO, strIdUsuario));

            //si hay filas devueltas
            if (dtbSQLiteDataTable.Rows.Count > 0)
            {
                //tomamos cada fila
                foreach (DataRow drwFila in dtbSQLiteDataTable.Rows)
                {
                    //si el valor del primer campo es igual al id de usuario
                    if (drwFila[0].ToString().Equals(strIdUsuario))
                        //devolvemos el resultado de comparar los niveles de acceso
                        bResultado = (int.Parse(drwFila[1].ToString()) >= nNivel);

                    break;
                }
            }

            //devolvemos el resultado del metodo
            return bResultado;

        }


    }//FIN de Clase SQLite
}
