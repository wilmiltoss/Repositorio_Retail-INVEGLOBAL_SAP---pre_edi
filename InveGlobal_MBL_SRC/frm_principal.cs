using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using InveStockMBL.MODS;
using System.Data;
using Microsoft.Win32;
using InveStockMBL.CLASES;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CdgUtilesSmart;

namespace InveStockMBL
{
    public partial class frm_principal : Form
    {
        #region ATRIBUTOS

            public const string NOMBRE_APLICACION = "InveGlobal MBL";
            public const string VERSION = " v1.8";

            //para el path del Archivo de BD
            private string strArchivoBD;
            private string strArchivoCSV;
            private string strCadenaTexto;
            private string strSentenciaSQL;
            private string strIdUsuario;
            private StreamWriter fstreamArchivoCSV;
            private int nConteoActual = 0;    

            private string strIDLocacion;
            private string strNroConteo;
            private string strIDSoporte;
            private string strNroSoporte;
            private string strIDLetraSoporte;
            private string strNivel;
            private string strMetro;
            private Boolean bolExisteRegistro = false;

            private double dblCantidad_Maxima_Conteo;
            private Boolean bolCantidad_Conteo_Con_Decimales = false;

            //creamos una nueva instancia de la clase LeeIni para lectura de archivos de configuraciones
            private LeeIni Configuraciones;
            private DataTable dtbDataTableAuxiliar = new DataTable();

            //instanciamos la clase SQLite para acceso a datos de la BD
            private SqliteDB BaseDatos;

        #endregion


        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        /// <summary>
        /// CONSTRUCTOR DEL FORMULARIO PRINCIPAL
        /// </summary>
        public frm_principal()
        {
            //inicializamos el objeto
            InitializeComponent();

            

        }//FIN del Contructor de la Clase

        /// <summary>
        /// CUANDO SE CARGA EL FORMULARIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_principal_Load(object sender, EventArgs e)
        {
            this.Text = NOMBRE_APLICACION + " " + VERSION;
            this.lbl_version.Text += VERSION;

            //mostramos un input box para tomar el nombre del COLECTOR(persona)
            this.strIdUsuario = Inputbox.Show("Responsable", "Ingrese su ID de Usuario");

            //si no hay id de usuario, salimos de la aplicacion
            if (this.strIdUsuario.Trim().Length == 0)
            {
                Application.Exit();

            }
            else
            {
                //creamos una instancia de la Clase LeeIni
                this.Configuraciones = new LeeIni(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\\config.ini", Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));

                //recuperamos el path del Archivo de BD
                this.strArchivoBD = WinMobile.Get_rom + "\\";
                this.strArchivoBD+= Configuraciones.fLeer_Ini("Parametros", "ArchivoBD");
                

                //creamos una nueva instancia de la clase SQLiteDB
                this.BaseDatos = new SqliteDB(strArchivoBD);

                //recuperamos el path del Archivo de Salida
                this.strArchivoCSV = WinMobile.Get_rom + "\\";
                this.strArchivoCSV+= Configuraciones.fLeer_Ini("Parametros", "ArchivoCSV");
                
                //llamamos a la funcion de Conexion a la BD, mientras el usuario no sea valido
                if (this.BaseDatos.bolConectar_BD())
                {
                    //si no es un usuario valido
                    if (!BaseDatos.bolUsuario_Valido(strIdUsuario.Trim()))
                    {
                        //mensaje de notificacion
                        MessageBox.Show("ID de Usuario no Valido", "ID de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                        //salimos de la aplicacion
                        Application.Exit();
                    }
                    else
                    {
                        //si se conecto, llamamos a funcion de Carga de Items en Combos
                        if (!this.bolCargarCombos())
                        {
                            //si no se cargaron, mensaje de notificacion
                            MessageBox.Show("No se Cargaron Los Combos");

                            //salimos de la aplicacion
                            Application.Exit();
                        }

                        //llamamos a funcion de carga de valores de configuracion a variables
                        if (!this.bolCargarConfiguracion())
                        {
                            //si no se cargaron, mensaje de notificacion
                            MessageBox.Show("No se Cargaron Las Configuraciones");

                            //salimos de la aplicacion
                            Application.Exit();
                        }

                        try
                        {
                            //tomamos los parametros del programa de activacion de scanner
                            var cPathScan = Configuraciones.fLeer_Ini("Parametros", "DirScanWedge");
                            var cExeScan = Configuraciones.fLeer_Ini("Parametros", "ExeScanWedge");

                            //si hay un path
                            if (cPathScan.Length > 0)
                            {
                                //si el programa no esta ya en ejecucion
                                if (FindWindow(null, cExeScan.Substring(0, cExeScan.IndexOf("."))).Equals(IntPtr.Zero))
                                {
                                    //y si el archivo ejecutable existe, lo lanzamos
                                    if (File.Exists(cPathScan + cExeScan)) Process.Start(cPathScan + cExeScan, string.Empty);
                                }
                            }

                        }
                        catch (Exception)
                        {
                            throw;

                        }

                        //ocultamos las fichas "Lecturas" y "Stock"
                        this.tab_lecturas.Hide();
                        this.tab_stock.Hide();

                        //actualizamos la cantidad de lecturas actuales
                        this.pDesplegar_Conteo_Lectura();

                    }
                        
                }
            }
            

        }//FIN de Cuando se Carga el Formulario

        /// <summary>
        /// CARGAR LOS ITEMS EN LOS COMBOS RESPECTIVOS Y DEVUELVE UN BOOLEANO
        /// </summary>
        /// <returns>Devuelve 'True' si se Cargaron Correctamente</returns>
        private Boolean bolCargarCombos()
        {
            try
            {
                //ensamblamos la Consulta SQL
                this.strSentenciaSQL = "SELECT ID_LOCACION || ' - ' || DESCRIPCION FROM LOCACIONES";

                //llamamos a funcion de Carga de Items a Combo
                this.BaseDatos.bolCargar_Valores_Combo(this.strSentenciaSQL, this.cmb_locaciones);
                
                //ensamblamos la Consulta SQL
                this.strSentenciaSQL = "SELECT ID_SOPORTE || ' - ' || DESCRIPCION FROM SOPORTES";

                //llamamos a funcion de Carga de Items a Combo
                this.BaseDatos.bolCargar_Valores_Combo(this.strSentenciaSQL, this.cmb_soportes);

                //la siguiente Consulta SQL
                this.strSentenciaSQL = "SELECT ID_LETRA_SOPORTE || ' - ' || LETRA FROM LETRAS_SOPORTES";

                //llamamos a funcion de Carga de Items a Combo
                this.BaseDatos.bolCargar_Valores_Combo(this.strSentenciaSQL, this.cmb_letras);

                //devolvemos el resultado de la funcion
                return true;

            }
            catch (Exception Ex)
            {
                //devolvemos el resultado de la funcion
                return false;
            }
        }//FIN de Cargar Combos

        /// <summary>
        /// CARGA LOS PARAMETROS DE CONFIGURACION Y DEVUELVE UN BOOLEAN
        /// </summary>
        /// <returns>Devuelve 'True' si se Cargaron Correctamente</returns>
        private Boolean bolCargarConfiguracion()
        {
            try
            {
                //ensamblamos la Consulta SQL
                this.strSentenciaSQL = "SELECT CANTIDAD_MAXIMA_CONTEO, CANTIDAD_CONTEO_CON_DECIMALES FROM CONFIGURACIONES";

                //llamamos a funcion de Ejecucion de Consulta SQL y recorremos las filas devueltas
                foreach (DataRow oFila in this.BaseDatos.dtbEjecutar_ConsultaSQL(this.strSentenciaSQL).Rows){
                    //obtenemos los valores devueltos
                    this.dblCantidad_Maxima_Conteo = System.Double.Parse(oFila.ItemArray[0].ToString());
                    switch (System.Int16.Parse(oFila.ItemArray[1].ToString())) {
                        case 0:
                            this.bolCantidad_Conteo_Con_Decimales = false;
                            break;

                        default:
                            this.bolCantidad_Conteo_Con_Decimales = true;
                            break;
                    }

                    //salimos del bucle
                    break;
                }

                //devolvemos el resultado de la funcion
                return true;

            }
            catch (Exception Ex)
            {
                //devolvemos el resultado de la funcion
                return false;
            }
        }//FIN de Cargar Configuracion

        /// <summary>
        /// CUANDO SE HACE CLICK EN [Limpiar Datos]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pLimpiar_Lecturas()
        {
            //pedimos confirmacion del usuario
            if (MessageBox.Show("¿Esta Seguro de Borrar los Datos?","Datos de Lecturas", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)== DialogResult.Yes)
            {
                //si el archivo existe
                if (File.Exists(this.strArchivoCSV))
                {
                    //lo intentamos eliminar
                    try
                    {
                        File.Delete(this.strArchivoCSV);
                    }
                    catch (IOException Ex)
                    {
                        //en caso de error, mensaje de notificacion
                        MessageBox.Show(Ex.Message, "Eliminar Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }

                //si la respuesta fue afirmativa, llamamos a funcion de Limpieza de la Tabla Lecturas
                this.BaseDatos.bolLimpiar_Tabla_Lecturas();

                //actualizamos la cantidad de lecturas actuales
                this.pDesplegar_Conteo_Lectura();

            }
        }//FIN de Click en [Limpiar Datos]

        /// <summary>
        /// CUANDO SE HACE CLICK EN [Hecho]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_hecho_Click(object sender, EventArgs e)
        {
            //validamos el Soporte
            if (!this.cmb_soportes.Text.Length.Equals(0) && this.cmb_soportes.Text.ToUpper().IndexOf("VACIO") < 0 && this.cmb_soportes.Text.ToUpper().IndexOf("-") > -1)
            {
                //validamos la Letra del Soporte
                if (!this.cmb_letras.Text.Length.Equals(0) && this.cmb_letras.Text.ToUpper().IndexOf("VACIO") < 0 && this.cmb_letras.Text.ToUpper().IndexOf("-") > -1)
                {
                    //si paso de las validaciones, tomamos los Datos de Ubicacion
                    this.strIDLocacion = this.cmb_locaciones.Text.Substring(0, this.cmb_locaciones.Text.IndexOf("-") - 1);
                    this.strIDSoporte = this.cmb_soportes.Text.Substring(0, this.cmb_soportes.Text.IndexOf("-") - 1);

                    this.strNroConteo = this.num_conteo.Value.ToString();

                    this.strNroSoporte = this.num_nro_soporte.Value.ToString();
                    this.strIDLetraSoporte= this.cmb_letras.Text.Substring(0, this.cmb_letras.Text.IndexOf("-") - 1);
                    this.strNivel = this.num_nivel.Value.ToString();
                    this.strMetro = this.num_metro.Value.ToString();

                    //mostramos la ubicacion en la pestana de "Lecturas"
                    //-------------------------MODIFICACION SOLICITADA POR CONTROL INTERNO
                    //this.lbl_ubicacion.Text = this.cmb_soportes.Text.Substring(this.cmb_soportes.Text.IndexOf("-") + 1) + "-" + this.num_nro_soporte.Value.ToString() + " C:" + this.cmb_letras.Text.Substring(this.cmb_letras.Text.IndexOf("-") + 1) + " N: " + this.num_nivel.Value.ToString() + " M: " + this.num_metro.Value.ToString();
                    this.lbl_conteo.Text = "Conteo: " + this.num_conteo.Value.ToString();
                    this.lbl_locacion.Text = "Locacion: " + this.cmb_locaciones.Text.Substring(this.cmb_locaciones.Text.IndexOf("-") + 1);
                    this.lbl_soporte.Text = "Soporte: " + this.cmb_soportes.Text.Substring(this.cmb_soportes.Text.IndexOf("-") + 1) + "-" + this.num_nro_soporte.Value.ToString();
                    this.lbl_metro_nivel.Text= "Metro: " + this.num_metro.Value.ToString() + " Nivel: " + this.num_nivel.Value.ToString();

                    //mostramos la pestana de "Lecturas"
                    this.tab_lecturas.Show();

                    //seleccionamos la pestana
                    this.tab_padre.SelectedIndex = 1;

                    //enfoque a cuadro de "Scanning"
                    this.txt_scanning.Focus();
                    this.txt_scanning.SelectAll();

                }
                else
                {
                    //si es no valido, mensjae de notificacion
                    MessageBox.Show("La Letra de Soporte no es Valida!", "Datos de Ubicacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                //si es no valido, mensjae de notificacion
                MessageBox.Show("El Soporte no es Valido!", "Datos de Ubicacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }//FIN de Click en [Hecho]

        /// <summary>
        /// CUANDO SE HACE CLICK EN [Buscar]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_buscar_Click(object sender, EventArgs e)
        {
            //validamos que haya un valor en el cuadro de "Scanning"
            if (!this.txt_scanning.Text.Length.Equals(0))
            {
                //llamamos a procedimiento de busqueda del articulo
                this.proBuscarArticulo(this.txt_scanning.Text.Trim());

                //borramos el contenido del cuadro de "Scanning"
                this.txt_scanning.Text = string.Empty;

            }
        }//FIN de Click en [Buscar]

        /// <summary>
        /// PROCEDIMIENTO DE BUSQUEDA DE ARTICULO
        /// </summary>
        /// <param name="strScanning">Scanning del Articulo a Buscar</param>
        private void proBuscarArticulo(string strScanning)
        {
            //mientras el primer digito sea cero
            while(strScanning.Substring(0,1).Equals("0"))
            {
                //lo eliminamos de la cadena
                strScanning= strScanning.Substring(1);
            }


            //ensamblamos la Consulta SQL
            this.strSentenciaSQL = "SELECT M.SCANNING, M.DESCRIPCION, M.DETALLE, M.PESABLE, M.COSTO, S.DESCRIPCION AS SECTOR";
            this.strSentenciaSQL += " FROM MAESTRO AS M, SECTORES AS S";
            this.strSentenciaSQL += " WHERE M.ID_SECTOR = S.ID_SECTOR";
            this.strSentenciaSQL += " AND M.SCANNING= '" + strScanning + "'";

            //intentamos Ejecutar la Consulta SQL y tomar los resultados devueltos
            try
            {
                //llamamos a funcion de Ejecucion de Consulta SQL
                dtbDataTableAuxiliar = this.BaseDatos.dtbEjecutar_ConsultaSQL(this.strSentenciaSQL);

                //si hay datos devueltos
                if (!this.dtbDataTableAuxiliar.Rows.Count.Equals(0))
                {
                    //recorremos cada fila
                    foreach (DataRow drwFila in this.dtbDataTableAuxiliar.Rows)
                    {
                        /*tomamos los datos de cada campo y los asignamos como valores de 
                         * las Etiquetas de la pestana "Stock"
                        */
                        this.lbl_scanning.Text = drwFila.ItemArray[0].ToString();
                        this.lbl_descripcion.Text = drwFila.ItemArray[1].ToString();
                        this.lbl_detalle.Text = drwFila.ItemArray[2].ToString();
                        this.lbl_pesable.Text = drwFila.ItemArray[3].ToString();
                        this.lbl_costo.Text = drwFila.ItemArray[4].ToString();
                        this.lbl_sector.Text = drwFila.ItemArray[5].ToString();

                        //salimos del bucle
                        break;
                    }


                    //luego buscamos el articulo en la tabla [LECTURAS]
                    this.strSentenciaSQL = "SELECT CANTIDAD FROM LECTURAS "
                                            + " WHERE ID_LOCACION= " + this.strIDLocacion
                                            + " AND NRO_CONTEO= " + this.strNroConteo
                                            + " AND ID_SOPORTE= " + this.strIDSoporte
                                            + " AND NRO_SOPORTE= " + this.strNroSoporte
                                            + " AND ID_LETRA_SOPORTE= " + this.strIDLetraSoporte
                                            + " AND NIVEL= " + this.strNivel
                                            + " AND METRO= " + this.strMetro
                                            + " AND SCANNING= '" + strScanning + "'";

                    //llamamos a funcion de Ejecucion de Consulta SQL
                    dtbDataTableAuxiliar = this.BaseDatos.dtbEjecutar_ConsultaSQL(this.strSentenciaSQL);

                    //reseteamos la etiqueta de cantidad anterior
                    this.lbl_cantidad_anterior.Text = "0";

                    //si hay datos devueltos
                    if (!this.dtbDataTableAuxiliar.Rows.Count.Equals(0))
                    {
                        //recorremos cada fila
                        foreach (DataRow drwFila in this.dtbDataTableAuxiliar.Rows)
                        {
                            /*tomamos el valor del campo lo asignamos como valor de 
                             * el cuadro de "Cantidad"
                            */
                            this.txt_cantidad.Text = drwFila.ItemArray[0].ToString();
                            this.lbl_cantidad_anterior.Text = drwFila.ItemArray[0].ToString();

                            //establecemos el valor de la variable de control a Verdadero
                            bolExisteRegistro = true;

                            //salimos del bucle
                            break;
                        }
                    }
                    else
                    {
                        //sino, es que no esta registrado aun
                        this.txt_cantidad.Text = "0";

                        //establecemos el valor de la variable de control a Falso
                        bolExisteRegistro = false;

                    }

                    //habilitamos el boton [Grabar]
                    this.cmd_grabar.Enabled = true;

                }
                else
                {
                    //si no hay datos devueltos
                    this.lbl_scanning.Text = "0";
                    this.lbl_descripcion.Text = "ART. NO EXISTE";
                    this.lbl_detalle.Text = "S/D";
                    this.lbl_costo.Text = "0";
                    this.lbl_pesable.Text = "P";

                    //deshabilitamos el boton [Grabar]
                    this.cmd_grabar.Enabled = false;

                }

                //liberamos el DataTable
                this.dtbDataTableAuxiliar.Dispose();

                //mostramos la pestana "Stock"
                this.tab_stock.Show();

                //pasamos a la pestana "Stock"
                this.tab_padre.SelectedIndex = 2;

                //pasamos el enfoque al cuadro de "Cantidad"
                this.txt_cantidad.Focus();
                this.txt_cantidad.SelectAll();

            }
            catch (Exception Ex)
            {
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Busqueda de Articulo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            }
        }//FIN de Buscar Articulo

        /// <summary>
        /// CUANDO SE HACE CLICK EN [Grabar]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_grabar_Click(object sender, EventArgs e)
        {
            //variables auxiliares
            DialogResult oRespuesta;

            //validamos los datos del articulo
            if (!this.strIDSoporte.Equals("0") && !this.strIDLetraSoporte.Equals("0")
                //&& (!this.txt_cantidad.Text.Equals("0")) && !this.lbl_scanning.Text.Equals("|")
                && !this.lbl_scanning.Text.Equals("|")
                && !this.lbl_scanning.Text.Length.Equals(0) && !this.lbl_scanning.Text.Equals("0"))
            {
                //si el articulo NO es pesable
                if (!this.lbl_pesable.Text.ToUpper().Equals("P")) {
                    //convertimos a entero la cantidad
                    this.txt_cantidad.Text = Convert.ToInt32(System.Double.Parse(this.txt_cantidad.Text)).ToString();
                }

                //si la cantidad anterior es diferente de cero
                if (!this.lbl_cantidad_anterior.Text.Equals("0")){
                    //preguntamos al usuario que desea hacer
                    oRespuesta= MessageBox.Show("¿Se Agregará esta Cantidad" + Convert.ToChar(13) + "a la ya antes Cargada?", "Datos de Lectura", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    //si la respuesta fue "NO", 
                    if (oRespuesta.Equals(DialogResult.No)){
                        //pedimos confirmacion al usuario
                        if (MessageBox.Show("¿Esta seguro de Reemplazar" + Convert.ToChar(13) + "la Cantidad antes Cargada?", "Datos de Lectura", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1).Equals(DialogResult.Yes))
                        {
                            //si la respuesta fue "SI", insertamos directamente el monto ingresado
                            //llamamos a procedimiento de Grabado de Datos de Lectura
                            this.proGrabarLectura();
                        }
                    }else{
                        //si la respuesta fue "SI"
                        if (oRespuesta.Equals(DialogResult.Yes))
                        {
                            //calculamos la nueva cantidad
                            this.txt_cantidad.Text = Convert.ToDouble(System.Double.Parse(this.txt_cantidad.Text) + System.Double.Parse(this.lbl_cantidad_anterior.Text)).ToString();

                            //llamamos a procedimiento de Grabado de Datos de Lectura
                            this.proGrabarLectura();
                        }
                    }
                }else{
                    //si la cantidad anterior es CERO, insertamos directamente
                    //llamamos a procedimiento de Grabado de Datos de Lectura
                    this.proGrabarLectura();
                }

                //pasamos a la ficha de "Lecturas"
                this.tab_padre.SelectedIndex = 1;

                //ocultamos esta ficha
                this.tab_stock.Hide();

                //establecemos el contenido del cuadro de "Cantidad" a "1"
                //this.txt_cantidad.Text = "1";

                //enfoque a cuadro de "Scanning"
                this.txt_scanning.Focus();
                this.txt_scanning.SelectAll();

            }else{
                //si no son validos los datos, mensaje de notificacion
                MessageBox.Show("Verifique los Datos!", "Datos Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //enfoque a cuadro de cantidad
                this.txt_cantidad.Focus();
                this.txt_cantidad.SelectAll();

            }

        }//FIN de Buscar Articulo

        /// <summary>
        /// PROCEDIMIENTO DE GRABADO DE DATOS DE LECTURA
        /// </summary>
        private void proGrabarLectura()
        {
            //si la cantidad ingresada es positiva
            if (System.Double.Parse(this.txt_cantidad.Text) >= 0)
            {
                    //si la cantidad limite de Conteo es diferente a Cero
                    if (this.dblCantidad_Maxima_Conteo != 0)
                    {
                        //si la cantidad actual es mayor al limite
                        if (System.Double.Parse(this.txt_cantidad.Text) > this.dblCantidad_Maxima_Conteo)
                        {
                            //mensaje de notificacion
                            if (!MessageBox.Show("Cantidad Excede Limite Establecido \n ¿Desea Continuar?", "InveGlobal", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                            {
                                //cantidad a cantidad maxima
                                this.txt_cantidad.Text = this.dblCantidad_Maxima_Conteo.ToString();

                                //salimos del procedimiento
                                return;

                            }
                        }
                    }

                    //si es un registro nuevo
                    if (bolExisteRegistro)
                    {
                        //llamamos a funcion de Insercion de Datos de Lectura
                        if (this.BaseDatos.bolActualizar_Lectura(this.strIDLocacion
                                                                , this.strNroConteo
                                                                , this.strIDSoporte
                                                                , this.strNroSoporte
                                                                , this.strIDLetraSoporte
                                                                , this.strNivel
                                                                , this.strMetro
                                                                , this.lbl_scanning.Text
                                                                , this.txt_cantidad.Text
                                                                , this.strIdUsuario))
                        {
                            //si se grabo correctamente, mensaje de notificacion
                            MessageBox.Show("Datos Guardados!", "Datos de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                        }
                        else
                        {
                            //si no se grabaron, mensaje de notificacion
                            MessageBox.Show("Los Datos No se Guardaron", "Datos de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        //llamamos a funcion de Actualizacion de Datos de Lectura
                        if (this.BaseDatos.bolInsertar_Lectura(this.strIDLocacion
                                                                , this.strNroConteo
                                                                , this.strIDSoporte
                                                                , this.strNroSoporte
                                                                , this.strIDLetraSoporte
                                                                , this.strNivel
                                                                , this.strMetro
                                                                , this.lbl_scanning.Text
                                                                , this.txt_cantidad.Text
                                                                , this.strIdUsuario
                                                                )
                            )
                        {
                            //actualizamos la cantidad de lecturas actuales
                            this.pDesplegar_Conteo_Lectura();

                            //si se grabo correctamente, mensaje de notificacion
                            MessageBox.Show("Datos Guardados!", "Datos de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                        }
                        else
                        {
                            //si no se grabaron, mensaje de notificacion
                            MessageBox.Show("Los Datos No se Guardaron", "Datos de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
            }else{
                //si es negativa, mensaje de notificacion
                MessageBox.Show("La Cantidad debe ser Positiva!", "Datos de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                //enfoque al cuatro de cantidad
                this.txt_cantidad.Focus();

            }

        }//FIN de Grabar Lectura

        /// <summary>
        /// CUANDO SE CAMBIA EL CONTENIDO DEL CUADRO "Cantidad"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_cantidad_TextChanged(object sender, EventArgs e)
        {
            //si el cuadro de texto no esta vacio
            if (!this.txt_cantidad.Text.Length.Equals(0))
            {
                //habilitamos el boton [Grabar]
                this.cmd_grabar.Enabled = true;

                //si el contenido nuevo es numerico
                try
                {
                    System.Double.Parse(this.txt_cantidad.Text) ;
                    
                    //si no se permiten decimales
                    if (!this.bolCantidad_Conteo_Con_Decimales) {
                        //convertimos la cantidad a entero
                        this.txt_cantidad.Text = Convert.ToInt32(System.Double.Parse(this.txt_cantidad.Text)).ToString();
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    //si no lo es, borramos el contenido del cuadro
                    this.txt_cantidad.Text = "1";
                }
            }
            else
            {
                //si esta vacio, lo deshabilitamos
                this.cmd_grabar.Enabled = false;
            }

        }//FIN de Cuando se Cambia el Contenido del cuadro "Cantidad"

        /// <summary>
        /// CUANDO SE PRESIONA UNA TECLA SOBRE EL CUADRO DE "Scanning"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_scanning_KeyDown(object sender, KeyEventArgs e)
        {
            //si la tecla es [Enter]
            if (e.KeyCode == Keys.Enter)
            {
                //llamamos al Evento Click del boton [Hecho]
                this.cmd_buscar_Click(sender, e);
            }

            //si la tecla es [Escape]
            if (e.KeyCode == Keys.Escape)
            {
                //pasamos a la ficha de "Ubicacion"
                this.tab_padre.SelectedIndex = 0;

                //ocultamos esta ficha
                this.tab_lecturas.Hide();

                //enfoque a cuadro de "Scanning"
                this.cmb_soportes.Focus();

            }
        }//Fin de Cuando se Presiona una Tecla sobre el cuadro "Scanning"

        /// <summary>
        /// CUANDO SE PRESIONA UNA TECLA SOBRE EL CUADRO DE "Cantidad"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            //si la tecla es [Enter]
            if (e.KeyCode == Keys.Enter)
            {
                //llamamos al Evento Click del boton [Grabar]
                this.cmd_grabar_Click(sender, e);
            }

            //si la tecla es [Escape]
            if (e.KeyCode == Keys.Escape)
            {
                //pasamos a la ficha de "Lecturas"
                this.tab_padre.SelectedIndex = 1;

                //establecemos a cero la cantidad
                this.txt_cantidad.Text = "0";

                //ocultamos esta ficha
                this.tab_stock.Hide();

                //enfoque a cuadro de "Scanning"
                this.txt_scanning.Focus();
                this.txt_scanning.SelectAll();
            }

        }//Fin de Cuando se Presiona una Tecla sobre el cuadro "Cantidad"

        /// <summary>
        /// CUANDO SE CAMBIA EL CONTENIDO DEL CUADRO DE "Scanning"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_scanning_TextChanged(object sender, EventArgs e)
        {
            //si el cuadro de texto no esta vacio
            if (!this.txt_scanning.Text.Length.Equals(0))
            {
                //habilitamos el boton [Buscar]
                this.cmd_buscar.Enabled = true;
            }
            else
            {
                //si esta vacio, lo deshabilitamos
                this.cmd_buscar.Enabled = false;
            }

        }//Fin de Cuando se Cambia el Contenido del cuadro "Scanning"

        /// <summary>
        /// CUANDO SE HACE CLICK EN EL MENU "Salir"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnu_salir_Click(object sender, EventArgs e)
        {
            //cerramos este formulario
            this.Close();
            this.Dispose();

        }//Fin de Cuando se hace Click en el Menu "Salir"

        /// <summary>
        /// CUANDO SE HACE CLICK EN EL MENU "Limpiar Datos"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnu_limpiar_Click(object sender, EventArgs e)
        {
            //solicitamos el identificador del usuario a habilitar el procedimiento
            string cClave = Inputbox.Show("Autorización p/ Limpiar Datos", "Ingrese su Clave:");

            //si la clave ingresada esta vacia
            if (cClave.Trim().Equals(string.Empty))
            {
                //mensaje de notificacion
                MessageBox.Show("Clave de Autorización no Valida!", "Autorización p/ Limpiar Datos");
            }
            else
            {
                //si no, consultamos en la base de datos el nivel de acceso del usuario
                if (this.BaseDatos.bolValidar_nivel_acceso(cClave.Trim(), 3))
                {
                    //si es valido, llamamos a al Evento Click del Boton [Limpiar Datos]
                    this.pLimpiar_Lecturas();
                }
                //sino, mensaje de notificacion
                else MessageBox.Show("Clave de Autorización no Valida!", "Autorización p/ Limpiar Datos");                
            }


        }//Fin de Cuando se hace Click en el Menu "Limpiar Datos"

        /// <summary>
        /// CUANDO SE PRESIONA UNA TECLA SOBRE EL COMBO DE "Soportes"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_soportes_KeyDown(object sender, KeyEventArgs e)
        {
            //si la tecla es [Enter]
            if(e.KeyCode== Keys.Enter)
            {
                //pasamos el enfoque al siguiente control
                this.cmb_letras.Focus();
                
            }

        }//Fin de Cuando se Presiona una Tecla sobre el Combo "Soportes"

        /// <summary>
        /// CUANDO SE PRESIONA UNA TECLA SOBRE EL COMBO DE "Letra Soporte"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_letras_KeyDown(object sender, KeyEventArgs e)
        {
            //si la tecla es [Enter]
            if (e.KeyCode == Keys.Enter)
            {
                //pasamos el enfoque al siguiente control
                this.num_nivel.Focus();
            }
        }//Fin de Cuando se Presiona una Tecla sobre el Combo "Letra Soporte"

        /// <summary>
        /// CUANDO SE PRESIONA UNA TECLA SOBRE EL CONTROL NUMERICO DE "Nivel"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_nivel_KeyDown(object sender, KeyEventArgs e)
        {
            //si la tecla es [Enter]
            if (e.KeyCode == Keys.Enter)
            {
                //pasamos el enfoque al siguiente control
                this.num_metro.Focus();
            }
        }//FIN de Cuando se Presiona una Tecla sobre el Control Numerico de "Nivel"

        /// <summary>
        /// CUANDO SE PRESIONA UNA TECLA SOBRE EL CONTROL NUMERICO DE "Metro"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_metro_KeyDown(object sender, KeyEventArgs e)
        {
            //si la tecla es [Enter]
            if (e.KeyCode == Keys.Enter)
            {
                //pasamos el enfoque al siguiente control
                this.cmd_hecho.Focus();
            }
        }//FIN de Cuando se Presiona una Tecla sobre el Control Numerico de "Metro"

        /// <summary>
        /// CUANDO SE HACE CLICK EN EL MENU "Generar Archivo"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnu_generar_archivo_Click(object sender, EventArgs e)
        {
            //si NO se ingreso un usuario
            if (this.strIdUsuario.Trim() == String.Empty || this.strIdUsuario.Trim() == "0")
            {
                //mostramos un mensaje de Notificacion
                MessageBox.Show("Usuario No Valido para Generar CSV!", "Generar Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //salimos del metodo
                return;

            }


            //ensamblamos la Consulta SQL
            this.strSentenciaSQL = "SELECT ID_LOCACION, NRO_CONTEO, ID_SOPORTE, NRO_SOPORTE, ID_LETRA_SOPORTE, NIVEL, METRO, SCANNING, CANTIDAD, ID_USUARIO"
                                    + " FROM LECTURAS";

            
            //intentamos obtener los datos de lecturas registradas
            try
            {
                //mostramos el panel de mensajes
                this.pnl_mensajes.Visible = true;

                //mostramos el mensaje de estado actual
                this.lbl_mensaje.Text= "consultando tabla..." ;

                //llamamos a funcion de Ejecucion de Consulta SQL
                dtbDataTableAuxiliar = this.BaseDatos.dtbEjecutar_ConsultaSQL(this.strSentenciaSQL);

                //mostramos el mensaje de estado actual
                this.lbl_mensaje.Text = "verificando resultados...";

                //mostramos el mensaje de estado actual
                this.lbl_mensaje.Text = "creando archivo CSV...";

                //abrimos un archivo nuevo para los datos de salida
                if (this.bolCrearArchivoCSV(this.strArchivoCSV))
                {
                    //si hay registros devueltos
                    if (!dtbDataTableAuxiliar.Rows.Count.Equals(0))
                    {
                        //si se creo
                        try
                        {
                            StringBuilder sbNombreColector = new StringBuilder();

                            try
                            {
                                //
                                foreach(Char cChar in Registry.LocalMachine.OpenSubKey("Ident").GetValue("Name").ToString().ToCharArray())
                                {
                                    //si es un ascii valido, a mi criterio CDGS
                                    if((int)cChar > 30 && (int)cChar < 127) sbNombreColector.Append(cChar);
                                }
                            }
                            catch (Exception ex)
                            {
                                //en caso de error, es el nombre del ID de usuario
                                sbNombreColector = new StringBuilder("COL" + this.strIdUsuario);
                            }
                            
                            //escribimos el nombre de ldispositivo
                            this.fstreamArchivoCSV.WriteLine("Device:" + sbNombreColector.ToString());

                            //contador para filas
                            int intNroFila= 0;

                            //recorremos cada fila del DataTable
                            foreach (DataRow drwFila in dtbDataTableAuxiliar.Rows)
                            {
                                //incrementamos el contador de filas
                                intNroFila++;

                                //reseteamos la variable de cadena de texto
                                this.strCadenaTexto = string.Empty;

                                //recorremos los campos de la fila
                                foreach (object dclColumna in drwFila.ItemArray)
                                {
                                    //concatenamos el valor del campo a la cadena a escribir en el archivo
                                    //si es el primer campo
                                    if (this.strCadenaTexto.Equals(string.Empty))
                                    {
                                        //no se le anade el separador de campos
                                        this.strCadenaTexto = dclColumna.ToString().Trim();
                                    }
                                    else
                                    {
                                        //sino, e anadimos el separador de campos al inicio
                                        this.strCadenaTexto = this.strCadenaTexto + ";" + dclColumna.ToString().Trim();
                                    }
                                }

                                //mostramos el mensaje de estado actual
                                this.lbl_mensaje.Text = "escribiendo " + intNroFila.ToString() + " de " + dtbDataTableAuxiliar.Rows.Count.ToString();

                                //escribimos en el archivo CSV
                                this.fstreamArchivoCSV.WriteLine(this.strCadenaTexto);

                            }

                            //mensaje de notificacion
                            MessageBox.Show("Archivo Generado!", "Generar Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                            //liberamos el DataTable
                            this.dtbDataTableAuxiliar.Dispose();
                        }
                        catch (Exception Ex)
                        {
                            //en caso de error, mensaje de notificacion
                            MessageBox.Show(Ex.Message, "Generar Archivo CSV 1", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        //si no hay registros devueltos, mensaje de notificacion
                        MessageBox.Show("No Hay Datos de Lecturas", "Generar Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    }
                    //mostramos el mensaje de estado actual
                    this.lbl_mensaje.Text = "cerrando archivo CSV...";

                    //cerramos el archivo CSV
                    this.fstreamArchivoCSV.Close();

                }
                else
                {
                    //si no se pudo crear, mensaje de notificacion
                    MessageBox.Show("Archivo CSV No Creado!", "Generar Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                }
            }
            catch (Exception Ex)
            {
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Generar Archivo CSV 4", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            //ocultamos el panel de mensajes
            this.pnl_mensajes.Visible = false;

        }//Fin de Cuando se Presiona una Tecla sobre el Combo "Letra Soporte"

        /// <summary>
        /// CREA O REEMPLAZA EL ARCHIVO DE SALIDA Y DEVUELVE UN BOOLEAN
        /// </summary>
        /// <returns>Devuelve 'True' si el Archivo se Creo Correctamente</returns>
        private Boolean bolCrearArchivoCSV(String strNombreArchivo)
        {
            //si el archivo existe
            if (File.Exists(strNombreArchivo))
            {
                //lo intentamos eliminar
                try
                {
                    File.Delete(strNombreArchivo);
                }
                catch (IOException Ex)
                { 
                    //en caso de error, mensaje de notificacion
                    MessageBox.Show(Ex.Message, "Generar Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    //devolvemos el resultado de la funcion
                    return false;

                }
            }

            //intentamos crear el archivo
            try
            {
                fstreamArchivoCSV = new StreamWriter(strNombreArchivo, false, Encoding.ASCII);

                //devolvemos el resultado de la funcion
                return true;

            }
            catch (IOException Ex)
            { 
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Generar Archivo CSV 6", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                //devolvemos el resultado de la funcion
                return false;

            }
        }//FIN de Funcion Crear Archivo CSV

        /// <summary>
        /// CUANDO SE CAMBIA EL TIPO DE SOPORTE SELECCIONADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_soportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //variables a utilizar
            string strID_Soporte;
            
            try
            {
                //obtenemos el ID del Soporte Seleccionado
                strID_Soporte = this.cmb_soportes.Text.Substring(0, this.cmb_soportes.Text.IndexOf("-"));

                //ensamblamos la Consulta SQL
                this.strSentenciaSQL = "SELECT SUBDIVISIBLE FROM SOPORTES WHERE ID_SOPORTE = " + strID_Soporte;

                //llamamos a funcion de Ejecucionde Consultas SQL y recorremos las filas devueltas
                foreach (DataRow drwFila in BaseDatos.dtbEjecutar_ConsultaSQL(this.strSentenciaSQL).Rows)
                {
                    //los demas elementos a su primer valor
                    this.num_nro_soporte.Value = this.num_nro_soporte.Minimum;
                    this.cmb_letras.Text = this.cmb_letras.Items[0].ToString();
                    this.num_nivel.Value = this.num_nivel.Minimum;
                    this.num_metro.Value = this.num_metro.Minimum;

                    //si el Soporte es SubDivisible
                    if (drwFila.ItemArray[0].ToString() == "1")
                    {
                        //habilitamos los controles numericos
                        this.num_nivel.Enabled = true;
                        this.num_metro.Enabled = true;
                    }
                    else
                    {
                        //sino, los deshabilitamos
                        this.num_nivel.Enabled = false;
                        this.num_metro.Enabled = false;

                        //valores al minimo
                        this.num_nivel.Value = this.num_nivel.Minimum;
                        this.num_metro.Value = this.num_metro.Minimum;

                    }

                    //salimos del bucle
                    break;
                }

            }
            catch (Exception Ex)
            {
                /*
                //en caso de error, mensaje de notificacion
                MessageBox.Show(Ex.Message, "Division Sop.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                //habilitamos los controles
                this.num_nivel.Enabled = true;
                this.num_metro.Enabled = true;

                //los demas elementos a su primer valor
                this.cmb_letras.Text = this.cmb_letras.Items[0].ToString();
                
                //valores al minimo
                this.num_nivel.Value = this.num_nivel.Minimum;
                this.num_metro.Value = this.num_metro.Minimum;
                */
            }
        }//FIN de Cuando se Cambia el Tipo de Soporte

        /// <summary>
        /// CUANDO SE CAMBIA EL NRO DE SOPORTE SELECCIONADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_nro_soporte_ValueChanged(object sender, EventArgs e)
        {
            //establecemos el valor de los demas controles a su minimo
            this.cmb_letras.Text = this.cmb_letras.Items[0].ToString();
            this.num_nivel.Value = this.num_nivel.Minimum;
            this.num_metro.Value = this.num_metro.Minimum;

        }//FIN de Cuando se Cambia el Tipo de Soporte

        /// <summary>
        /// CUANDO SE HACE CLICK EN EL BOTON [-1]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmd_negativo_Click(object sender, EventArgs e)
        {
            //si la cantidad es diferente de cero
            if (System.Double.Parse(this.txt_cantidad.Text)!= 0)
            {
                Double a= (System.Double.Parse(this.txt_cantidad.Text) * -1);

                //la multiplicamos por -1 pàra cambiarle el signo
                this.txt_cantidad.Text =  a.ToString();
                this.txt_cantidad.Focus();

            }
        }//FIN de Cuando se hace Click en el boton [-1]

        /// <summary>
        /// DESPLIEGA LA CANTIDAD DE LECTURAS ACTUALES
        /// </summary>
        private void pDesplegar_Conteo_Lectura()
        {
            this.mnu_cuenta_lecturas.Text = BaseDatos.strCantidad_Lecturas();
        }

        /// <summary>
        /// Cuando el control de conteo gana el enfoque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_conteo_GotFocus(object sender, EventArgs e)
        {
            //tomamos el valor que actualmente muestra
            nConteoActual = (int)this.num_conteo.Value;
        }

        /// <summary>
        /// Cuando el control de conteo pierde el enfoque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_conteo_LostFocus(object sender, EventArgs e)
        {
            //si el valor del control de nro. de conteo ha cambiado
            if (nConteoActual != (int)this.num_conteo.Value)
            {
                //pedimos autorizacion
                //solicitamos el identificador del usuario a habilitar el procedimiento
                string cClave = Inputbox.Show("Autorización p/ Limpiar Datos", "Ingrese su Clave:");

                //si la clave ingresada esta vacia
                if (cClave.Trim().Equals(string.Empty))
                {
                    //establecemos el valor original
                    this.num_conteo.Value = nConteoActual;

                    //mensaje de notificacion
                    MessageBox.Show("Clave de Autorización no Valida!", "Autorización p/ Limpiar Datos");

                }
                else
                {
                    //si no, consultamos en la base de datos el nivel de acceso del usuario
                    if (this.BaseDatos.bolValidar_nivel_acceso(cClave.Trim(), 3))
                    {//si es valido, nada
                        this.Activate();// .cmb_locaciones.Focus();
                    }
                    else
                    {
                        //sino, establecemos el valor original
                        this.num_conteo.Value = nConteoActual;

                        //mensaje de notificacion
                        MessageBox.Show("Clave de Autorización no Valida!", "Autorización p/ Limpiar Datos");
                    }
                }
            }

        }
    }

}