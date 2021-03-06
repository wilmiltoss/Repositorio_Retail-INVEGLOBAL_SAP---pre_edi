Imports System.Data.OracleClient
Imports System.IO

''' <summary>
''' MODULO INTERFACE CON EL SISTEMA SICS
''' </summary>
''' <remarks></remarks>
Module SICS

#Region "DECLARACIONES_GENERALES"
    'declaramos las variables del modulo
    'VARIABLES PARA PARAMETROS DEL SISTEMA
    Private strIPServidor As String = String.Empty
    Private strNombreServicio As String = String.Empty
    Private strPuertoServicio As String = String.Empty
    Private strIDUsuario As String = String.Empty
    Private strPassword As String = String.Empty

    Private strSentenciaSQL As String = String.Empty

    'VARIABLES PARA MANIPULACION DE DATOS
    Private dstOraDataSet As DataSet
    Private dtbOraDataTable As DataTable
    Private drwOraFila As DataRow
    Private drwOraColumna As DataColumn
    Private dadOraDataAdapter As OracleDataAdapter
    Private conOraConexionBD As OracleConnection
    Private cmdOraComandoSQL As OracleCommand
    Private dtrOraDataReader As OracleDataReader
    Private bindingSrcSICS As New BindingSource


#End Region

    ''' <summary>
    ''' ASIGNA LOS VALORES DE ATRIBUTOS DEL MODULO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se tomaron todos los Valores</returns>
    ''' <remarks></remarks>
    Public Function bCargar_Parametros_Sistema(ByVal strID_Sistema As String) As Boolean
        'ensamblamos la Consulta SQL para obtener los datos del Sistema
        strSentenciaSQL = "EXECUTE [SP_OBTENER_DATOS_SISTEMA] @id_sistema = " & strID_Sistema

        'intentamos obtener los valores devueltos
        Try
            'llamamos a procedimiento de Ejecucion de Consulta SQL
            principal.dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

            'recorremos cada fila devuelta
            For Each drwFila In dtDataTableAuxiliar.Rows
                'obtenemos los campos de cada campo
                strIDUsuario = drwFila.Item(0)
                strIPServidor = drwFila.Item(1)
                strNombreServicio = drwFila.Item(2)
                strPuertoServicio = drwFila.Item(3)

                'modificado 13/003/2010 para incluir password del usuario
                strPassword = drwFila.Item(4)

                'salimos del bucle
                Exit For

            Next

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Cargar Atributos del Modulo SICS")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' CONECTA A BD DE SICS, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si la Conexion se Establecio Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bConectar_SICS() As Boolean
        'variables a utilizar
        Dim strCadenaConexion As String

        'ensamblamos la cadena de conexion
        strCadenaConexion = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & _
                            strIPServidor & ")(PORT=" & strPuertoServicio & "))(CONNECT_DATA=(SERVICE_NAME=" & _
                            strNombreServicio & ")));uid=" & strIDUsuario & ";pwd=" & strPassword & ";"

        'intentamos conectar a la BD
        Try
            conOraConexionBD = New OracleConnection(strCadenaConexion)

            'abrimos la conexion
            conOraConexionBD.Open()

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As OracleException
            'si el error es manejable por el cliente oracle, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error de Conexion a SICS", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error de Conexion a SICS : " + strCadenaConexion)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        Catch Ex As Exception
            'si el error es de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error de Conexion a SICS", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error de Conexion a SICS : " + strCadenaConexion)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        End Try


    End Function

    ''' <summary>
    ''' CREA Y DEVUELVE UN DATATABLE CON EL MAESTRO DE ARTICULOS DEL LOCAL
    ''' </summary>
    ''' <returns>Devuelve un DataTable con el Maestro de Articulos</returns>
    ''' <remarks></remarks>
    Public Function dtObtener_Maestro_Articulos(ByVal cID_Local_Maestro As String, ByVal cID_Sector_Maestro As String) As DataTable
        'creamos las nuevas instancias de los objetos a utilizar
        cmdOraComandoSQL = New OracleCommand
        dtbOraDataTable = New DataTable

        'establecemos la conexion del comando
        cmdOraComandoSQL.Connection = conOraConexionBD

        'pasamos la Sentencia a Ejecutar
        cmdOraComandoSQL.CommandText = "PR_GENERA_DATOS_PARA_COLECTOR"
        'cmdOraComandoSQL.CommandText = "select * from(articulos) where arti_codigo= '07791293047119'"

        'establecemos el tipo del comando
        cmdOraComandoSQL.CommandType = CommandType.StoredProcedure
        'cmdOraComandoSQL.CommandType = CommandType.Text

        'pasamos los parametros del SP
        cmdOraComandoSQL.Parameters.Add("P_SUCU", OracleType.Number).Value = Int(cID_Local_Maestro)
        cmdOraComandoSQL.Parameters.Add("P_CLASIF", OracleType.VarChar).Value = cID_Sector_Maestro
        cmdOraComandoSQL.Parameters.Add("P_CANT", OracleType.Number).Value = 0
        cmdOraComandoSQL.Parameters.Add("TMP_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output

        'ejecutamos el Comando y derivamos los Resultados al Data Adapter
        dadOraDataAdapter = New OracleDataAdapter(cmdOraComandoSQL)

        'intentamos obtener los datos desde la BD
        Try
            dadOraDataAdapter.Fill(dtbOraDataTable)

            'devolvemos el DataTable como resultado de la funcion
            Return dtbOraDataTable

        Catch Ex As OracleException
            'en caso de error manejable por el cliente Oracle, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Obtener Maestro de Articulos", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error Obteniendo Maestro de Articulos SICS :" & cmdOraComandoSQL.CommandText())
            'principal.pInfo_a_Log("Detalle de Error : " & Ex.Message)

            'devolvemos el DataTable como resultado de la funcion
            Return dtbOraDataTable

        Catch Ex As Exception
            'en caso de error de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Obtener Maestro de Articulos", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error Obteniendo Maestro de Articulos SICS :" & cmdOraComandoSQL.CommandText())
            'principal.pInfo_a_Log("Detalle de Error : " & Ex.Message)

            'devolvemos el DataTable como resultado de la funcion
            Return dtbOraDataTable

        End Try

    End Function

    ''' <summary>
    ''' EJECUTA UNA SENTENCIA SQL EN LA BD Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="strSentenciaSQL">Sentencia SQL a Ejecutar</param>
    ''' <returns>Devuelve 'True' si se Ejecuto Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bEjecutar_Sentencia_SQL(ByVal strSentenciaSQL As String) As Boolean
        'instanciamos un nuevo comando
        cmdOraComandoSQL = New OracleCommand(strSentenciaSQL, conOraConexionBD)

        'transferimos la Sentencia al Comando
        'cmdOraComandoSQL.CommandText = strSentenciaSQL

        'intentamos ejecutar la Sentencia SQL
        Try
            cmdOraComandoSQL.ExecuteNonQuery()

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As OracleException
            'si el error es manejable por el cliente oracle, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error de Ejecutando Sentencia SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error de Ejecutando Sentencia SQL : " + strSentenciaSQL)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        Catch Ex As Exception
            'si el error es de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error de Ejecutando Sentencia SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error de Ejecutando Sentencia SQL : " + strSentenciaSQL)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' EJECUTA UNA CONSULTA SQL Y DEVUELVE UN DATATABLE
    ''' </summary>
    ''' <param name="strConsultaSQL">Sentencia SQL de Consulta</param>
    ''' <returns>Devuelve un DataTable con los Resultados</returns>
    ''' <remarks></remarks>
    Public Function dtbEjecutar_ConsultaSQL(ByVal strConsultaSQL As String) As DataTable
        'reseteamos las variables a utilizar
        dadOraDataAdapter = Nothing
        dtbOraDataTable = Nothing

        'si la conexion no esta definida
        If IsNothing(SICS.conOraConexionBD) Then
            'si not se abre la conexion
            If Not SICS.bConectar_SICS() Then
                'devolvemos el resultado de la funcion
                Return dtbOraDataTable
            End If
        End If


        'intentamos ejecutar la sentencia
        Try
            dadOraDataAdapter = New OracleDataAdapter(strConsultaSQL, conOraConexionBD)
            dtbOraDataTable = New DataTable

            'pasamos los resultados al DataTable
            dadOraDataAdapter.Fill(dtbOraDataTable)

            'devolvemos el resultado de la funcion
            Return dtbOraDataTable

        Catch Ex As OracleException
            'si el error es manejable por el cliente oracle, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error de Ejecución de Consulta SICS", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error Ejecutando Consulta SQL : " + strConsultaSQL)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return Nothing

        Catch Ex As Exception
            'si el error es de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error de Ejecución de Consulta SICS", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error Ejecutando Consulta SQL : " + strConsultaSQL)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return Nothing

        End Try

    End Function

    ''' <summary>
    ''' EJECUTA UNA CONSULTA SQL Y DEVUELVE UN DATASET
    ''' </summary>
    ''' <param name="strConsultaSQL">Sentencia de Consulta de Datos para el DataSet</param>
    ''' <returns>Devuelve un DataSet con los Datos Devueltos</returns>
    ''' <remarks></remarks>
    Public Function dstCrear_DataSet(ByVal strConsultaSQL As String) As DataSet
        'reseteamos las variabels a utilizar
        dadOraDataAdapter = Nothing
        dstOraDataSet = Nothing

        'intentamos ejecutar la sentencia
        Try
            dadOraDataAdapter = New OracleDataAdapter(strConsultaSQL, conOraConexionBD)
            dstOraDataSet = New DataSet

            'pasamos los resultados al DataSet
            dadOraDataAdapter.Fill(dstOraDataSet)

            'devolvemos el resultado de la funcion
            Return dstOraDataSet

        Catch Ex As OracleException
            'si el error es manejable por el cliente oracle, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error Creando DataSet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'evento a LOG
            'principal.pInfo_a_Log("Error Creando DataSet : " & strConsultaSQL)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return Nothing

        Catch Ex As Exception
            'si el error es de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Error Creando DataSet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'evento a LOG
            'principal.pInfo_a_Log("Error Creando DataSet : " + strConsultaSQL)
            'principal.pInfo_a_Log("Detalle del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return Nothing

        End Try

    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE RESULTADO DE CONSULTA A COMBOBOX
    ''' </summary>
    ''' <param name="cmbNombreCombo">Nombre del Combo de Destino de los Items</param>
    ''' <param name="strConsultaSQL">Consulta SQL que devolvera los Items para el Combo</param>
    ''' <remarks></remarks>
    Public Sub pCargar_Combo_Valores(ByVal cmbNombreCombo As ComboBox, ByVal strConsultaSQL As String)
        'ejecutamos la Consulta SQL
        dstOraDataSet = dsCrear_DataSet(cSentenciaSQL)

        'si el dataset fue cargado
        If Not dstOraDataSet.Equals(Nothing) Then
            'trabajamos con la grilla
            With cmbNombreCombo
                'recorremos los valores devueltos
                For Each drwOraFila In dstOraDataSet.Tables.Item(0).Rows
                    'cargamos el valor del campo en el combo
                    .Items.Add(drwOraFila.Item(0))

                Next
            End With

        Else
            'sino, cargamos la palabra "VACIO"
            With cmbNombreCombo
                .Items.Add("VACIO")

            End With

        End If

        'mostramos el primer valor del combo
        cmbNombreCombo.Text = cmbNombreCombo.Items(0)

        'liberamos el dataset
        dstOraDataSet.Dispose()

    End Sub

    ''' <summary>
    ''' DEVUELVE UN DATATABLE CON LOS DATOS DE SECTORES CUYOS ID COINCIDEN CON EL PARAMETRO
    ''' </summary>
    ''' <param name="strCodigoSector">Codigo Filtro de Sectores</param>
    ''' <returns>Devuelve un Datatable con los Sectores afectados</returns>
    ''' <remarks></remarks>
    Public Function dtbSeleccionar_Sectores(ByVal strCodigoSector As String) As DataTable
        'ensamblamos la Consulta SQL
        strSentenciaSQL = "SELECT ID_SECTOR, NOMBRE_SECTOR, NIVEL FROM VW_SECTORES_SICS WHERE ID_SECTOR LIKE '" & strCodigoSector & "%'"

        'si la conexion no esta definida
        'If IsNothing(SICS.conOraConexionBD) Then
        'si se abre la conexion
        '    If SICS.bConectar_SICS() Then
        'llamamos a Funcion de Ejecucion de Consulta SQL y devolvemos el resultado de la funcion
        'Return SICS.dtbEjecutar_ConsultaSQL(strSentenciaSQL)
        '        Return principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

        '    End If
        'End If

        'llamamos a Funcion de Ejecucion de Consulta SQL y devolvemos el resultado de la funcion
        'Return SICS.dtbEjecutar_ConsultaSQL(strSentenciaSQL)
        Return principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

    End Function

    ''' <summary>
    ''' DEVUELVE UN DATATABLE CON LOS DATOS DE SECTORES DEL SISTEMA
    ''' </summary>
    ''' <returns>Devuelve un Datatable con los Sectores afectados</returns>
    ''' <remarks></remarks>
    Public Function dtbObtener_Sectores() As DataTable

        '/////////////////////////////////////////////////////////////
        '
        '             MODIFICADO PARA QUE ASUMA LOS SECTORES EME
        '
        '/////////////////////////////////////////////////////////////

        'ensamblamos la Consulta SQL
        'strSentenciaSQL = "SELECT ID_SECTOR, NOMBRE_SECTOR, NIVEL, ID_SECTOR_PADRE FROM VW_SECTORES_SICS"

        'si la conexion no esta definida
        'If IsNothing(SICS.conOraConexionBD) Then
        'si se abre la conexion
        '    If SICS.bConectar_SICS() Then
        '        'llamamos a Funcion de Ejecucion de Consulta SQL y devolvemos el resultado de la funcion
        '        'Return SICS.dtbEjecutar_ConsultaSQL(strSentenciaSQL)
        '        Return principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

        '    End If
        'End If

        'llamamos a Funcion de Ejecucion de Consulta SQL y devolvemos el resultado de la funcion
        'Return SICS.dtbEjecutar_ConsultaSQL(strSentenciaSQL)


        'ensamblamos la Consulta SQL
        strSentenciaSQL = "SELECT [ID_SECTOR], [NOMBRE_SECTOR], [NIVEL], [ID_SECTOR_PADRE] FROM [SECTORES]"

        'llamamos a Funcion de Ejecucion de Consulta SQL y devolvemos el resultado de la funcion
        Return principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

    End Function

    ''' <summary>
    ''' EJECUTA EL SP DE AJUSTE DE INVENTARIO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="sLocal_ID">ID del Local en el Sistema de Gestion</param>
    ''' <param name="sFecha">Fecha del Inventario</param>
    ''' <param name="sScanning">Scanning del Articulo</param>
    ''' <param name="sCantidadAjuste">Cantidad a Ajustar</param>
    ''' <param name="sCosto">Costo del Articulo</param>
    ''' <returns>Devuelve 'True' si se Ejecuto Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bAplicar_Ajustes(ByVal sLocal_ID As String _
                                        , ByVal sFecha As String _
                                        , ByVal sScanning As String _
                                        , ByVal sCantidadAjuste As String _
                                        , ByVal sCantidadTeorica As String _
                                        , ByVal sCosto As String) As Boolean

        'variables a utilizar
        Dim nResultado As Integer = 0
        Dim nPosicionSeparadorDecimal As Integer = -1

        'formateamos los valores de ajustes
        'si el separador decimal es la coma ","
        If sCantidadAjuste.IndexOf(",") > 0 Or sCantidadAjuste.IndexOf(".") > 0 Then
            sCantidadAjuste = IIf(sCantidadAjuste.IndexOf("-") < 0, sCantidadAjuste.PadLeft(9, "0"), "-" & sCantidadAjuste.Substring(1).PadLeft(9, "0"))
            sCosto = IIf(sCosto.IndexOf("-") < 0, sCosto.PadLeft(9, "0"), "-" & sCosto.Substring(1).PadLeft(9, "0"))
            sCantidadTeorica = IIf(sCantidadTeorica.IndexOf("-") < 0, sCantidadTeorica.PadLeft(9, "0"), "-" & sCantidadTeorica.Substring(1).PadLeft(9, "0"))

        End If

        'rellenamos con ceros el scanning
        sScanning = sScanning.PadLeft(13, "0")

        'pasamos la Sentencia a Ejecutar
        cmdOraComandoSQL = New OracleCommand("PR_AJUSTE_DE_INVENTARIO", conOraConexionBD)

        'si la conexion esta cerrada, la abrimos
        If conOraConexionBD.State = ConnectionState.Closed Then conOraConexionBD.Open()

        'establecemos el tipo del comando
        cmdOraComandoSQL.CommandType = CommandType.StoredProcedure

        'pasamos los parametros del SP
        cmdOraComandoSQL.Parameters.Add("P_SUCU", OracleType.Number).Value = Convert.ToInt16(sLocal_ID)
        cmdOraComandoSQL.Parameters.Add("P_FECHA", OracleType.VarChar).Value = sFecha
        cmdOraComandoSQL.Parameters.Add("P_ARTICULO", OracleType.VarChar).Value = sScanning
        cmdOraComandoSQL.Parameters.Add("P_AJUSTE", OracleType.VarChar).Value = sCantidadAjuste
        cmdOraComandoSQL.Parameters.Add("P_COSTO", OracleType.VarChar).Value = sCosto
        cmdOraComandoSQL.Parameters.Add("P_TEORICO", OracleType.VarChar).Value = sCantidadTeorica
        cmdOraComandoSQL.Parameters.Add("P_RESULTADO", OracleType.Number).Direction = ParameterDirection.Output

        'intentamos ejecutar el Comando
        Try
            nResultado = cmdOraComandoSQL.ExecuteNonQuery()

            'si el resultado de la ejecucion del SP es Cero
            If Int(cmdOraComandoSQL.Parameters("P_RESULTADO").Value) = 0 Then
                'devolvemos el resultado de la funcion
                Return True

            Else
                'sino, evento a LOG
                'principal.pInfo_a_Log("Codigo de Error de Ajuste SICS no Realizado : " & nResultado)

                'devolvemos el resultado de la funcion
                Return False

            End If

        Catch Ex As OracleException
            'en caso de error manejable por el cliente Oracle, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Aplicar Ajuste de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error Ejecutando SP de Ajuste SICS : " & cmdOraComandoSQL.CommandText())
            'principal.pInfo_a_Log("Detalle de Error : " & Ex.Message)

            'devolvemos el DataTable como resultado de la funcion
            Return False

        Catch Ex As Exception
            'en caso de error de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Aplicar Ajuste de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error Ejecutando SP de Ajuste SICS : " & cmdOraComandoSQL.CommandText())
            'principal.pInfo_a_Log("Detalle de Error : " & Ex.Message)

            'devolvemos el DataTable como resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE DESCONEXION DE BD SICS
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub pDesconectar_SICS()
        'intentamos cerrar la conexion
        Try
            conOraConexionBD.Close()

            'evento a LOG
            'principal.pInfo_a_Log("Desconectado de BD SICS")

        Catch Ex As Exception
            'en caso de error, nada

        End Try

    End Sub

End Module
