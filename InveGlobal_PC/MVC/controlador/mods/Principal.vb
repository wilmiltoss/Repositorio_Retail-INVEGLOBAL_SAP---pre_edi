
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' MODULO PRINCIPAL DEL SISTEMA
''' </summary>
''' <remarks></remarks>
Module principal

#Region "DECLARACIONES_VARIABLES_GLOBALES_Y_MODULO"
    'VARIABLES PUBLICAS
    Public cLink_Reporte_Problemas As String = String.Empty
    Public cID_Usuario, cNombre_Usuario As String
    Public nNivel_Acceso_Usuario As Integer = 0
    Public cNombre_PC As String = String.Empty

    'VARIABLE PARA CONTROL DE ERRORES
    Public bHay_Error As Boolean = False

    'VARIABLES DE SISTEMA
    Private cDir_Logs As String
    Public cUnidad_De_Red As String
    Private cBat_Montaje As String
    Public cNombre_Maestro_CSV As String
    Public cBat_Python_SQLite As String
    Public nFilas_Para_Excel As Integer = 1000

    'VARIABLES PARA CONTROL DE DATOS DEL INVENTARIO
    Public cID_Inventario As String = String.Empty
    Public cID_Local As String = String.Empty
    Public cNombre_Local As String = String.Empty
    Public cID_Sistema_Gestion As String = String.Empty
    Public cNombre_Sistema_Gestion As String = String.Empty
    Public cFecha_Inventario As String = String.Empty
    Public bInventario_Cerrado As Boolean = False
    Public nNro_Conteo_Aplicado As Integer = 0
    Public cComentario_Inventario As String = String.Empty
    Public nCantidad_Maxima_Conteo As Double = 0
    Public bCantidad_Conteo_Con_Decimales As Boolean = True
    Public bPesables_Incluidos As Boolean = True

    'VARIABLES PARA MANIPULACION DE DATOS
    Public dsDataSetAuxiliar As DataSet = Nothing
    Public dtDataTableAuxiliar As DataTable = Nothing
    Public drwFila As DataRow
    Public dclColumna As DataColumn

    'VARIABLES PARA COMUNICACION SQLSERVER
    Public cSentenciaSQL, cCatalogoSQL As String
    Private cnConexionSQL As SqlConnection = Nothing
    Private cmComandoSQL As SqlCommand = Nothing
    Public cServidorSQL, cUsuario, cContrasena, cTimeOut As String

    'VARIABLES PARA COMUNICACION SQLite
    Public cCarpetaBDSQLite As String = String.Empty

    'VARIABLES PARA COMUNICACION CON DISPOSITIVOS COLECTORES
    Public cCarpetaDestinoBDSQLite As String = String.Empty
    Public cPathArchivoOrigenCSV As String = String.Empty
    Public cCarpetaDestinoCSV As String = String.Empty

    'variable de control de estado de conexion a BD
    Dim bConectado As Boolean = False

#End Region

    ''' <summary>
    '''PROCEDIMIENTO DE CARGA DE DATOS Y CONFIGURACIONES DEL SISTEMA
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub main()
        'creamos una nueva instancia de la clase de lectura de archivos ini
        Dim oArchivoConfig As New ArchivosIni(CurDir() & "\Config.ini")

        'obtenemos el Nombre de Equipo
        cNombre_PC = My.Computer.Name

        'reseteamos todas las variables de datos de usuario
        cID_Usuario = String.Empty
        cNombre_Usuario = String.Empty

        'cargamos todos los parametros desde el archivo de configuracion
        cDir_Logs = CurDir() & oArchivoConfig.leer_ini("Parametros", "Logs", "\LOGS\")
        cLink_Reporte_Problemas = oArchivoConfig.leer_ini("Parametros", "LinkReporteProblemas", String.Empty)
        cPathArchivoOrigenCSV = oArchivoConfig.leer_ini("Parametros", "PathArchivoOrigenCSV", "\IPSM\data_bases\lectura.csv")
        cCarpetaDestinoCSV = CurDir() & oArchivoConfig.leer_ini("Parametros", "CarpetaDestinoCSV", "\CSV\")
        cUnidad_De_Red = oArchivoConfig.leer_ini("Parametros", "UnidadDeRed", "Z:")
        cBat_Montaje = CurDir() & oArchivoConfig.leer_ini("Parametros", "BatMontaje", "\montarUnidad.bat")
        cNombre_Maestro_CSV = oArchivoConfig.leer_ini("Parametros", "NombreCSV", "InveGlobal.CSV")
        nFilas_Para_Excel = oArchivoConfig.ObtenerInteger("Parametros", "FilasParaExcel", 0)

        'variables de configuracion de conexion a la BD Local
        cServidorSQL = oArchivoConfig.leer_ini("SQLServer", "ServidorBD", "AJVRETAIL")
        cCatalogoSQL = oArchivoConfig.leer_ini("SQLServer", "NombreBD", "INVESTOCK_GENERAL")
        cUsuario = oArchivoConfig.leer_ini("SQLServer", "IDUsuario", "stock")
        cContrasena = oArchivoConfig.leer_ini("SQLServer", "Contrasena", "12345")
        cTimeOut = oArchivoConfig.leer_ini("SQLServer", "TimeOut", "90")

        'variables para SQLite
        cCarpetaBDSQLite = CurDir() & oArchivoConfig.leer_ini("SQLite", "CarpetaArchivoBD", "\BDATOS\")
        cCarpetaDestinoBDSQLite = oArchivoConfig.leer_ini("SQLite", "CarpetaDestinoBD", "\IPSM\")
        cBat_Python_SQLite = CurDir() & oArchivoConfig.leer_ini("SQLite", "BatPythonSQLite", "\PYTHON\py.bat")

        'si alguna de las variables estan sin determinar
        If cDir_Logs.Equals(String.Empty) _
            Or cServidorSQL.Equals(String.Empty) Or cCatalogoSQL.Equals(String.Empty) _
            Or cUsuario.Equals(String.Empty) Or cContrasena.Equals(String.Empty) _
            Or cCarpetaBDSQLite.Equals(String.Empty) Or cCarpetaDestinoBDSQLite.Equals(String.Empty) _
            Or cPathArchivoOrigenCSV.Equals(String.Empty) Or cCarpetaDestinoCSV.Equals(String.Empty) Then

            'mensaje
            MessageBox.Show("Parametros de Configuracion sin Valores." + Chr(13) + "Se Asignaran los Valores por Defecto del Sistema", "Parametros de Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            'evento a LOG
            'pInfo_a_Log("Parametros de Configuracion sin Valores. Se Asignaran los Valores por Defecto del Sistema")

            'evaluamos cada parametro, si estan vacios les asignamos los valores por defecto
            If cDir_Logs.Equals(String.Empty) Then
                cDir_Logs = CurDir() & "\LOGS\"
            End If
            If cServidorSQL.Equals(String.Empty) Then
                cServidorSQL = "AJVRETAIL"
            End If
            If cCatalogoSQL.Equals(String.Empty) Then
                cCatalogoSQL = "INVESTOCK_GENERAL"
            End If
            If cUsuario.Length = 0 Then
                cUsuario = "stock"
            End If
            If cContrasena.Length = 0 Then
                cContrasena = "12345"
            End If
            If cCarpetaBDSQLite.Equals(String.Empty) Then
                cCarpetaBDSQLite = CurDir() + "\BDATOS\"
            End If
            If cCarpetaDestinoBDSQLite.Equals(String.Empty) Then
                cCarpetaDestinoBDSQLite = "\IPSM\"
            End If
            If cPathArchivoOrigenCSV.Equals(String.Empty) Then
                cPathArchivoOrigenCSV = "\IPSM\data_bases\lectura.csv"
            End If
            If cCarpetaDestinoCSV.Equals(String.Empty) Then
                cCarpetaDestinoCSV = CurDir() & "\CSV\"
            End If

        End If

        'ejecutamos el procedimiento de Montaje de Unidad de Red
        pMontar_Unidad_De_Red()

    End Sub

    ''' <summary>
    ''' EJECUTA EL BAT DE CONEXION DE UNIDAD DE RED
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pMontar_Unidad_De_Red()
        Try
            'ejecutamos el batch de montaje de la unidad de red
            Shell(cBat_Montaje)

        Catch Ex As Exception
            'en caso de error, mensajes de notificacion
            MessageBox.Show("Unidad de Red no Montada!", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(Ex.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub


    ''' <summary>
    ''' FUNCION DE CONEXION A LA BD 
    ''' </summary>
    ''' <returns name="T/F">Devuelve 'True' si la Conexion se pudo Realizar.</returns>
    ''' <remarks></remarks>
    Public Function bConectar_A_BD() As Boolean

        'variables a utilzar
        Dim CADENA_CONEXION As String = "Data Source={0}; Initial Catalog={1};User Id={2};Password={3};Connection Timeout={4}"

        'resultado por defecto para la funcion
        bConectar_A_BD = False

        'intentamos abrir la conexion a la BD
        Try
            'MessageBox.Show(String.Format(CADENA_CONEXION, cServidorSQL, cCatalogoSQL, cUsuario, cContrasena, cTimeOut), _
            '                "Cadena de Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cnConexionSQL = New SqlConnection(String.Format(CADENA_CONEXION, cServidorSQL, cCatalogoSQL, cUsuario, cContrasena, cTimeOut))
            cnConexionSQL.Open()

            'valor del resultado de la funcion
            bConectar_A_BD = True

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show("No Se Pudo Establecer la Conexion a " + cServidorSQL, "Conexion a Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show("Error :" & ex.Message)

            'evento a LOG
            'pInfo_a_Log("No Se Pudo Establecer la Conexion a " + cServidorSQL)
            'pInfo_a_Log("Detalle del Error : " & ex.Message)

        End Try

        'devolvemos el resultado de la funcion
        Return bConectar_A_BD

    End Function

    ''' <summary>
    ''' FUNCION QUE EJECUTA UNA SENTENCIA SQL Y DEVUELVE UN DATATABLE
    ''' </summary>
    ''' <param name="cConsultaSQL">Consulta SQL a Ejecutar.</param>
    ''' <returns>Devuelve un DataTable con los Resultados de la Sentencia.</returns>
    ''' <remarks></remarks>
    Public Function dtEjecutar_ConsultaSQL(ByVal cConsultaSQL As String) As DataTable

        'valor por defecto para la funcion
        dtEjecutar_ConsultaSQL = Nothing

        'creamos una nueva instancia del DataTable
        Dim daAdapter As SqlDataAdapter
        Dim dtTablaDatos As DataTable

        'intentamos ejecutar la sentencia
        Try
            daAdapter = New SqlDataAdapter(cConsultaSQL, cnConexionSQL)
            dtTablaDatos = New DataTable

            'pasamos los resultados al DataTable
            daAdapter.Fill(dtTablaDatos)

            'devolvemos el resultado de la funcion
            dtEjecutar_ConsultaSQL = dtTablaDatos

        Catch ex As Exception
            'en caso de error
            MessageBox.Show("Error Intentando Ejecutar Consulta SQL", "Ejecucion de Sentencia SQL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(ex.Message.ToString())

            'evento a LOG
            'pInfo_a_Log("Error Ejecutando Consulta SQL : " + cConsultaSQL)
            'pInfo_a_Log("Detalle del Error : " & ex.Message)

        End Try

        'devolvemos el resultado de la funcion
        Return dtEjecutar_ConsultaSQL

    End Function

    ''' <summary>
    ''' FUNCION QUE EJECUTA UNA SENTENCIA SQL Y DEVUELVE UN BOOLEANO
    ''' </summary>
    ''' <param name="cSentenciaSQL"></param>
    ''' <returns>Devuelve 'True' si la Sentencia se Ejecuto Correctamente.</returns>
    ''' <remarks></remarks>
    Public Function bEjecutar_SentenciaSQL(ByVal cSentenciaSQL As String) As Boolean

        'creamos una nueva instancia del DataTable
        Dim daAdapter As SqlDataAdapter
        Dim dtTablaDatos As DataTable

        'intentamos ejecutar la sentencia
        Try
            daAdapter = New SqlDataAdapter(cSentenciaSQL, cnConexionSQL)
            dtTablaDatos = New DataTable

            'pasamos los resultados al DataTable
            daAdapter.Fill(dtTablaDatos)

            'devolvemos el resultado de la funcion
            bEjecutar_SentenciaSQL = True

        Catch ex As SqlException
            'en caso de error
            MessageBox.Show("Error Intentando Ejecutar Sentencia SQL", "Ejecucion de Sentencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(ex.Message.ToString())

            'evento a LOG
            'pInfo_a_Log("Error Ejecutando Sentencia SQL : " + cSentenciaSQL)
            'pInfo_a_Log("Detalle del Error : " & ex.Message)

            'devolvemos el resultado de la funcion
            bEjecutar_SentenciaSQL = False

        End Try

        'devolvemos el resultado de la funcion
        Return bEjecutar_SentenciaSQL

    End Function

    ''' <summary>
    ''' EJECUTA UNA CONSULTA SQL Y DEVUELVE EL VALOR DEL PRIMER CAMPO DE LA PRIMERA FILA DE RESULTADOS
    ''' </summary>
    ''' <param name="cConsultaSQL">Consulta SQL a Ejecutar</param>
    ''' <returns>Devuelve el Resultado como String</returns>
    ''' <remarks></remarks>
    Public Function cEjecutar_Consulta_Escalar(ByVal cConsultaSQL As String) As String
        'variables a utilizar
        Dim cResultado As String = String.Empty

        Try
            'pasamos la Consulta SQL al comando
            cmComandoSQL = New SqlCommand(cConsultaSQL, cnConexionSQL)
            cmComandoSQL.CommandType = CommandType.Text

            'ejecutamos el comando
            cResultado = cmComandoSQL.ExecuteScalar.ToString()

            'devolvemos el resultado de la funcion
            Return cResultado

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando ejecutar Comando SQL : " & cConsultaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error Intentando Ejecutar Comando SQL.", "Ejecucion de Consulta Escalar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show("Error Encontrado: " & Ex.Message, "Ejecucion de Consulta Escalar", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'devolvemos el resultado de la funcion
            Return String.Empty

        End Try

    End Function

    ''' <summary>
    ''' FUNCION QUE EJECUTA UN PROCEDIMIENTO ALMACENADO EN LA BD Y DEVUELVE UN BOOLEANO
    ''' </summary>
    ''' <param name="cNombreProcedimientoAlmacenado"> Nombre del Procedimiento Almacenado a Ejecutar.</param>
    ''' <returns>Devuelve 'True' si el Procedimiento de ejecuto Correctamente.</returns>
    ''' <remarks></remarks>
    Public Function bEjecutar_Procedimiento(ByVal cNombreProcedimientoAlmacenado As String) As Boolean

        'creamos una nueva instancia del Comando
        Dim cmComando As New SqlCommand
        Dim nCaptaResultado As Integer

        'intentamos ejecutar la sentencia
        Try
            cmComando.Connection = cnConexionSQL
            cmComando.CommandType = CommandType.StoredProcedure
            cmComando.CommandText = cNombreProcedimientoAlmacenado

            nCaptaResultado = cmComando.ExecuteNonQuery

            'asignamos el valor a devolver
            Return bEjecutar_Procedimiento = True

        Catch ex As Exception
            'en caso de error
            MessageBox.Show("Error Intentando Ejecutar Procedimiento Almacenado.", "Ejecucion de SP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show("Error Encontrado: " & ex.Message, "Ejecucion de SP", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'pInfo_a_Log("Error Ejecutando Procedimiento Almacenado : " + cNombreProcedimientoAlmacenado)
            'pInfo_a_Log("Detalle del Error : " & ex.Message)

            'asignamos el valor a devolver
            Return bEjecutar_Procedimiento = False

        End Try

    End Function

    ''' <summary>
    ''' FUNCION QUE EJECUTA UNA SENTENCIA SQL Y DEVUELVE UN DATASET
    ''' </summary>
    ''' <param name="cSentenciaSQL">Sentencia que recupera los Datos para el DataSet.</param>
    ''' <returns>Devuelve un DataSet con los Datos Devueltos por la Sentencia.</returns>
    ''' <remarks></remarks>
    Public Function dsCrear_DataSet(ByVal cSentenciaSQL As String) As DataSet

        'creamos una nueva instancia del DataTable
        Dim daAdapter As SqlDataAdapter
        Dim dsDataSet As DataSet = Nothing

        'intentamos ejecutar la sentencia
        Try
            daAdapter = New SqlDataAdapter(cSentenciaSQL, cnConexionSQL)
            dsDataSet = New DataSet

            'pasamos los resultados al DataTable
            daAdapter.Fill(dsDataSet)

            'devolvemos el resultado de la funcion
            Return dsDataSet

        Catch ex As Exception
            'en caso de error
            MessageBox.Show("Error Intentando Ejecutar Sentencia SQL", "Ejecucion de Sentencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MessageBox.Show(ex.Message.ToString())

            'evento a LOG
            'pInfo_a_Log("Error Ejecutando Sentencia SQL para DataSet : " + cSentenciaSQL)
            'pInfo_a_Log("Detalle del Error : " & ex.Message)

            'devolvemos el resultado de la funcion
            Return dsDataSet

        End Try

    End Function

    ''' <summary>
    ''' FUNCION DE CIERRE DE CONEXION A LA BD, DEVUELVE UN BOOLEANO
    ''' </summary>
    ''' <returns>Devuelve 'True' si todo salio bien.</returns>
    ''' <remarks></remarks>
    Private Function bDesconectar_BD() As Boolean

        'valor por defecto de la funcion
        bDesconectar_BD = False

        'si la conexion esta abierta
        If cnConexionSQL.State = ConnectionState.Open Then
            'intentamos cerrarla
            Try
                cnConexionSQL.Close()

                'establecemos como positivo el valor a devolver 
                bDesconectar_BD = True

            Catch ex As Exception
                'en caso de error, evento a LOG
                'pInfo_a_Log("Error Intentando Cerrar Conexion a BD : " + ex.Message)

            End Try

        Else
            'si ya estaba cerrada desde un principio
            'establecemos como positivo el valor a devolver 
            bDesconectar_BD = True

        End If

        'devolvemos el resultado de la funcion
        Return bDesconectar_BD

    End Function

    ''' <summary>
    ''' SEPARA EN CAMPOS UNA CADENA Y DEVUELVE UN ARRAY DE LOS MISMOS
    ''' </summary>
    ''' <param name="strCadenaCampos">Secuencia de Campos a Separar</param>
    ''' <param name="chrSeparador">Caracter Separador</param>
    ''' <returns>Devuelve un Array de los Campos</returns>
    ''' <remarks></remarks>
    Public Function arlstSepararCampos(ByVal strCadenaCampos As String, ByVal chrSeparador As Char) As ArrayList
        'variables a utilizar
        Dim arlstArreglo As New ArrayList()
        Dim strCampo As String = String.Empty
        Dim chrCaracter As Char

        'recorremos la linea caracter a caracter
        For Each chrCaracter In strCadenaCampos
            'si el caracter no es el separador de campos
            If Not chrCaracter.Equals(chrSeparador) Then
                'lo concatenamos al valor de campo
                strCampo = strCampo & chrCaracter

            Else
                'si es el separador, anadimos el valor de campo al arreglo
                arlstArreglo.Add(strCampo)

                'limpiamos la variable de valor de campo
                strCampo = String.Empty

            End If
        Next

        'anadimos el ultimo valor de campo
        arlstArreglo.Add(strCampo)

        'devolvemos el resultado de la funcion
        Return arlstArreglo

    End Function

    ''' <summary>
    ''' EJECUTA UNA CONSULTA SQL Y DEVUELVE EL VALOR DEL PRIMER CAMPO DE LA PRIMERA FILA ENCONTRADA
    ''' </summary>
    ''' <param name="cConsultaSQL">Consulta SQL a Ejecutar</param>
    ''' <returns>Devuelve un String con el Valor del Campo</returns>
    ''' <remarks></remarks>
    Public Function cConsulta_Unico_Resultado(ByVal cConsultaSQL As String) As String
        'variables a utilizar
        Dim cCampo As String = String.Empty

        Try
            'llamamos a Funcion de Ejecucion de Consulta SQL
            dtDataTableAuxiliar = dtEjecutar_ConsultaSQL(cConsultaSQL)

            'si hay resultado devueltos
            If dtDataTableAuxiliar.Rows.Count > 0 Then
                'tomamos el valor del primer campo de la primera fila
                cCampo = dtDataTableAuxiliar.Rows(0).Item(0).ToString()

                'devolvemos el resultado de la funcion
                Return cCampo

            Else
                'sino, devolvemos la cadena vacia
                Return cCampo

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'pInfo_a_Log("Error intentando obtener Tipo de Datos de Campo : " & cConsultaSQL)
            'pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return cCampo

        End Try

    End Function

    ''' <summary>
    ''' DEVUELVE EL TIPO DE DATOS DEL CAMPO DE LA TABLA SOLICITADA
    ''' </summary>
    ''' <param name="cNombreTabla">Nombre de la Tabla o Vista</param>
    ''' <param name="cNombreCampo">Nombre del Campo</param>
    ''' <returns>Devuelve un String con el Nombre del Tipo de Dato</returns>
    ''' <remarks></remarks>
    Public Function cObtener_Tipo_Dato_DeCampo(ByVal cNombreTabla As String, ByVal cNombreCampo As String) As String
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "EXECUTE [SP_OBTENER_TIPO_DATO_DE_CAMPO] " _
                                           & "@nombre_tabla= '" & cNombreTabla & "'" _
                                           & ",@nombre_campo= '" & cNombreCampo & "' ;"

        'obtenemos el tipo de dato del campo y lo devolvemos como rersultado de la funcion
        Return cConsulta_Unico_Resultado(cSentenciaSQL)

    End Function

    ''' <summary>
    ''' CARGA LOS PARAMETROS DEL SISTEMA, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="strNombre_Sistema">Nombre del Sistema</param>
    ''' <param name="strID_Sistema">ID del Sistema</param>
    ''' <returns>Devuelve 'True' si se Cargaron Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bCargar_Parametros_Sistema(ByVal strNombre_Sistema As String, ByVal strID_Sistema As String) As Boolean
        'intentamos obtener los valores devueltos
        Try
            Select Case strNombre_Sistema.Trim.ToUpper()
                Case "EMERETAIL"
                    'sistema es EMERETAIL, llamamos a la funcion de carga de parametros del modulo correspondiente
                    Return EMERETAIL.bCargar_Parametros_Sistema(strID_Sistema)

                Case Else
                    'si no es ninguno de los sistemas registrados
                    MessageBox.Show("El Sistema " & strNombre_Sistema & " no Tiene un Modulo Definido!", "Sistema de Gestion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'evento a LOG
                    'principal.pInfo_a_Log("Error intentando Cargar Datos de Sistema sin MODULO : " & strNombre_Sistema)

                    'devolvemos el resultado de la funcion
                    Return False

            End Select

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Datos de Sistema de Gestión", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error intentando Obtener Datos de Sistema : " & strNombre_Sistema)
            'principal.pInfo_a_Log("Detalles de Error: " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        End Try

        'devolvemos el resultado de la funcion
        Return True


    End Function

    ''' <summary>
    ''' ELIMINA UN REGISTRO DE CABECERA DE DATOS DE INVENTARIO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Elimino Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bEliminar_Cabecera_Inventario() As Boolean
        'ensamblamos la Sentencia SQL
        cSentenciaSQL = "EXECUTE [SP_ELIMINAR_INVENTARIO_NUEVO_FALLIDO] @id_inventario= " & cID_Inventario

        'llamamos a function de ejecucion de Sentencia SQL y devolvemos el resultado de la misma
        Return bEjecutar_SentenciaSQL(cSentenciaSQL)

    End Function


End Module
