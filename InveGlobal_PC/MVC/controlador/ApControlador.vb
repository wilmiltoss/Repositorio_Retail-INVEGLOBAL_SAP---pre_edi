Imports CdgUtiles.Log
Imports CdgPersistencia
Imports CdgPersistencia.ClasesBases
Imports CdgUtiles.Util
Imports System.Threading



Public Class ApControlador


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ApControlador"

    Public Const NOMBRE_APLICACION As String = "INVEGLOBAL_PC"

    Public Const VERSION As String = "8.5.8"

    Private __infoAlog As InfoALog

    'administradores de datos
    Public oApModelo As ApModelo
    Public oApModeloSQLite As ApModeloSQLite

    'marcador de error fatal
    Public bErrorFatal As Boolean

    'marcador de archivo SQLite actualizado
    Public bMaestroSQLiteActualizado As Boolean = False
    Public bParametrosSQLiteActualizado As Boolean = False

    'datos de sesion actual
    Public cNombrePc As String
    Public cDirHomeusuario As String
    Public cSeparadorListas As String

    Public Usuario_OTD As New UsuarioOTD()
    Public Local_OTD As New LocalOTD()
    Public Inventario_OTD As New InventarioOTD()
    Public NuevoInventario_OTD As New InventarioOTD()
    Public Colector_OTD As New ColectorOTD()

    Public Enum OperacionesArchivos
        CARGA = 0
        DESCARGA = 1
    End Enum

#Region "FORMULARIOS"

    Private __PrincipalFRM As FrmPrincipal
    Private __LoginFRM As FrmSeleccionLocal
    Private __DatosInventarioFRM As FrmDatosInventario
    Private __SectoresFRM As FrmSectores

    Private __DetallesUbicacionFRM As FrmDetallesUbicaciones
    Private __CargaManualFRM As FrmCargaManual
    Private __EntradasFRM As FrmEntradasColectores
    Private __OpcionesFRM As FrmOpciones

    Private __VerExistenciasFRM As FrmVerExistencias
    Private __SectoresCubiertosFRM As FrmSectoresCubiertos
    Private __InfoActualFRM As FrmInfoActual
    Private __AbmUsuariosFRM As FrmAbmUsuarios

    Private __ModConteoFRM As FrmModificarConteo
    Private __ColectoresFRM As FrmColectores


#End Region

    Private __oArchivoCSV As ArchivoMaestroCSV
    Private __lTeoricos As List(Of FotoEmeOTD)



    Public dtTablaAuxiliar As DataTable
    Public cNombreColector As String

    Public Enum OPERADORES
        IGUAL = 0
        NO_IGUAL
        MAYOR_QUE
        MENOR_QUE
        ENTRE
        NO_ENTRE
    End Enum

    Public Shared aComparadores As String() = {"es igual a", "no es igual a", "es mayor que", "es menor que", "esta entre", "no esta entre"}

#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion de los componentes internos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__Inicializar_componentes()"

        Try
            'seteamos el controlador de errores
            bErrorFatal = False

            'obtenemos alugnos datos utiles del equipo actual
            cNombrePc = SystemInformation.ComputerName
            cDirHomeusuario = Environment.GetEnvironmentVariable("HOMEPATH")
            cSeparadorListas = ";"

            'obtenemos una instancia del modelo de datos
            oApModelo = ApModelo.Get_instancia()
            oApModeloSQLite = ApModeloSQLite.Get_instancia()

            'cerramos la conexion al archivo SQLite para no tenerlo bloqueado
            oApModeloSQLite.Get_conector().lDesconectar(NOMBRE_METODO)

            'montamos la unidad de red
            Shell(Application.StartupPath & Config.Get_instancia().get_valor(Config.BAT_MONTAR_UNIDAD))



        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            notificar_error(NOMBRE_METODO & " Error: " & ex.Message, NOMBRE_APLICACION)

            'seteamos el controlador de errores
            bErrorFatal = True

        End Try

    End Sub


#End Region


#Region "METODOS"

    ''' <summary>
    ''' Valida el inicio de Sesion del Usuario
    ''' </summary>
    ''' <param name="oUsuarioParam">Instancia de Usuario a validar</param>
    ''' <returns>Lista de Resultado [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lIniciar_sesion(ByVal oUsuarioParam As UsuarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lIniciar_sesion()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'llamamos al metodo de recuperacion de datos del usuario
            lResultado = oApModelo.Usuarios_ADM.lValidar_usuario(oUsuarioParam)

            'si la sesion es valida
            If lResultado(0).Equals(1) Then
                'tomamos la instancia de usuario devuelta como dato de la sesion actual
                Usuario_OTD = CType(lResultado(1), UsuarioOTD)
            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Recupera los datos del ultimo inventario del Local actual
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lObtener_datos_inventario() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lObtener_datos_inventario()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos del ultimo inventario del local seleccionado
        Try
            lResultado = oApModelo.Inventarios_ADM.lGet_ultimo_inventario(Local_OTD)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la instancia devuelta
                Inventario_OTD = CType(lResultado(1), InventarioOTD)

                'formamos la sentencia de filtrado de registros
                Dim cFiltro As String = "INNER JOIN {0} ON {1}.{2} = {0}.{3} INNER JOIN {4} ON {0}.{5} = {4}.{6} WHERE {4}.{6} = {7}"
                cFiltro = String.Format(cFiltro, InventariosSectoresTBL.NOMBRE_TABLA _
                                        , SectoresTBL.NOMBRE_TABLA, SectoresTBL.ID.cNombre _
                                        , InventariosSectoresTBL.ID_SECTOR.cNombre _
                                        , InventariosTBL.NOMBRE_TABLA _
                                        , InventariosSectoresTBL.ID_INVENTARIO.cNombre _
                                        , InventariosTBL.ID.cNombre _
                                        , Inventario_OTD.nId _
                                        )


                'recuperamos la lista de sectores cubiertos
                lResultado = oApModelo.Sectores_ADM.lGet_elementos(cFiltro)

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'asignamos la lista de Sectores cubiertos al inventario
                    Inventario_OTD.lSectores = CType(lResultado(1), List(Of SectorOTD))

                Else
                    'sino, mensaje de notificacion
                    notificar_stop(lResultado(1).ToString, NOMBRE_METODO)

                End If

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, "Ok"})

            End If

        Catch Ex As Exception

            'devolvemos el resultado de la funcion
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & Ex.Message})

        End Try

        'devolvemos el resultado de la funcion
        Return lResultado


    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE PAUSA DE PROCESOS ACTUALES
    ''' </summary>
    ''' <param name="nSegundos">Cantidad de Segundos a Esperar.</param>
    ''' <remarks></remarks>
    Public Sub pausar_procesos(ByVal nSegundos As Integer)

        'varieables a utilizar
        Dim Inicio, Final

        'establecemos la hora de inicio
        Inicio = TimeOfDay

        'establecemos la hora de finalizacion
        Final = DateAdd(DateInterval.Second, nSegundos, TimeOfDay)

        'mientras no sea la hora de finalizacion
        Do While TimeOfDay < Final
            'Cambia a otros procesos.
            Application.DoEvents()
        Loop

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE RESULTADO DE CONSULTA A COMBOBOX
    ''' </summary>
    ''' <param name="oNombreCombo">Nombre del Combo de Destino de los Items.</param>
    ''' <param name="cSentenciaSQL">Sentencia SQL que devolvera los Items para el Combo.</param>
    ''' <param name="cPrimerValor">Primer Valor que aparecera en el Combo. Opcional.</param>
    ''' <remarks></remarks>
    Public Sub cargar_combo_valores(ByVal oNombreCombo As ComboBox, ByVal cSentenciaSQL As String, Optional ByVal cPrimerValor As String = "")

        'variables a utilizar
        Dim dsMiDataSet As DataSet = Nothing
        Dim rwFila As DataRow = Nothing

        'limpiamos previamente el combo
        oNombreCombo.Items.Clear()

        'ejecutamos la sentencia
        dsMiDataSet = dsCrear_DataSet(cSentenciaSQL)

        'si el dataset fue cargado
        If Not dsMiDataSet.Tables(0).Rows.Count.Equals(0) Then
            'trabajamos con la grilla
            With oNombreCombo
                'si se paso la parametro de cPrimerValor
                If Not cPrimerValor.Equals(String.Empty) Then
                    'lo insertamos como primer valor del combo
                    .Items.Add(cPrimerValor)

                End If

                'recorremos los valores devueltos
                For Each rwFila In dsMiDataSet.Tables.Item(0).Rows
                    'cargamos el valor del campo en el combo
                    .Items.Add(rwFila.Item(0))

                Next

            End With

        Else
            'sino, cargamos la palabra "VACIO"
            With oNombreCombo
                .Items.Add("VACIO")

            End With

        End If

        'mostramos el primer valor del combo
        oNombreCombo.Text = oNombreCombo.Items(0)

        'liberamos el dataset
        dsMiDataSet.Dispose()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE RESULTADO DE CONSULTA A LISTBOX
    ''' </summary>
    ''' <param name="oNombreCombo">Nombre del List de Destino de los Items.</param>
    ''' <param name="cSentenciaSQL">Sentencia SQL que devolvera los Items para el Combo.</param>
    ''' <remarks></remarks>
    Public Sub cargar_lista_valores(ByVal oNombreCombo As ListBox, ByVal cSentenciaSQL As String)

        'variables a utilizar
        Dim dsMiDataSet As DataSet = Nothing
        Dim rwFila As DataRow = Nothing

        'limpiamos previamente el combo
        oNombreCombo.Items.Clear()

        'ejecutamos la sentencia
        dsMiDataSet = dsCrear_DataSet(cSentenciaSQL)

        'si el dataset fue cargado
        If Not dsMiDataSet.Tables(0).Rows.Count.Equals(0) Then
            'trabajamos con la grilla
            With oNombreCombo
                'recorremos los valores devueltos
                For Each rwFila In dsMiDataSet.Tables.Item(0).Rows
                    'cargamos el valor del campo en el combo
                    .Items.Add(rwFila.Item(0))

                Next

            End With

        Else
            'sino, cargamos la palabra "VACIO"
            With oNombreCombo
                .Items.Add("VACIO")

            End With

        End If

        'mostramos el primer valor del combo
        oNombreCombo.Text = oNombreCombo.Items(0)

        'liberamos el dataset
        dsMiDataSet.Dispose()

    End Sub

    ''' <summary>
    ''' Ejecuta el procedimiento de validacion de datos de Usuario
    ''' </summary>
    ''' <param name="oUsuarioParam">Instancia de la clase de Usuario</param>
    ''' <returns>Lista de Resultados (int, object)</returns>
    ''' <remarks></remarks>
    Public Function lValidar_sesion(ByVal oUsuarioParam As UsuarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lValidar_sesion()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'llamamos al metodo de recuperacion del datos
            lResultado = oApModelo.Inventarios_ADM.lGet_elemento(oUsuarioParam)

            'si se ejecuto exitosamente
            If lResultado(0).Equals(1) Then
                'tomamos la instancia devuelta
                Dim oUsr As UsuarioOTD = CType(lResultado(1), UsuarioOTD)

                'comparamos las contrasenas
                If oUsr.Contrasena.Equals(oUsuarioParam.Contrasena) Then
                    'establecemos el resultado del metodo
                    lResultado = New List(Of Object)(New Object() {1, "Ok"})

                    'tomamos la instancia como sesion actual
                    Usuario_OTD = oUsr

                Else
                    'sino, mensaje de notificacion
                    lResultado = New List(Of Object)(New Object() {-1, "Id de Usuario o Contraseña no válidos!"})

                End If
            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Abre la conexion al sistem ad e gestion del Local de Inventario
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lConectar_sistema_gestion() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lConectar_sistema_gestion()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'creamos una instancia de la clase de Utileria Oracle
            oApModelo.Get_conexion_oracle = New OracleUtiles(Local_OTD.Sistema_OTD.cIpServidor, Local_OTD.Sistema_OTD.cUsuarioSistema _
                                                  , Local_OTD.Sistema_OTD.cPassword, Local_OTD.Sistema_OTD.cNombreServicio _
                                                  , Local_OTD.Sistema_OTD.nPuertoServicio, 600)

            'intentamos abrir la conexion
            lResultado = oApModelo.Get_conexion_oracle.lConectar()

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Recupera los items de existencia desde el Sist. EmeRetail para el local actual
    ''' </summary>
    ''' <returns>Liusta de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lGet_foto_eme() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lGet_foto_eme()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'creamos una instancia de la clase de Utileria Oracle
            lResultado = oApModelo.FotosEme_ADM().lGet_foto_inventario(Local_OTD)

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la lista de items devuelta
                __lTeoricos = CType(lResultado(1), List(Of FotoEmeOTD))

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, "Ok"})

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta los procesos de carga del maestro temporal
    ''' </summary>
    ''' <param name="nTipoActualizacion">Tipo de Actualizacion a Ejecutar [REEMPLAZAR|ACTUALIZAR]</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>

    '#######################################################################################################################'
    Public Function lGet_foto_sap() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lGet_foto_sap()"

        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})
        Try

            'ABRIMOS EL DIALOGO para importar csv
            lResultado = lImportarCSV(";")


        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function
    '#######################################################################################################################'

    Public Function lImportarCSV(ByVal delimitador As String) As List(Of Object)
        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lImportarCSV()"
        Dim lResultado As List(Of Object) = New List(Of Object)(New Object() {0, "Ok"})
        Dim myFileDialog As New OpenFileDialog()
        Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim groupSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator
        Try
            Dim ruta As String = ""
            With myFileDialog
                .InitialDirectory = "c:\temp\"
                .Filter = "CSV files (*.csv)|*.CSV"
                .FilterIndex = 2
                .RestoreDirectory = True

                If (.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                    ruta = .FileName 'se guarda la ruta

                End If
            End With
            Dim textLine As String = "" 'leemos linea por linea
            Dim splitLine() As String

            __lTeoricos = New List(Of FotoEmeOTD)

            If System.IO.File.Exists(ruta) = True Then 'si existe la ruta

                Dim objReader As New System.IO.StreamReader(ruta) ' lee el archivo

                Do While objReader.Peek() <> -1 'recorre todo el archivo

                    textLine = objReader.ReadLine() 'cada linea q lee guarda en TexLine

                    splitLine = Split(textLine, delimitador) 'lo almacena en SplitLine

                    If splitLine.GetValue(3).ToString().Contains(groupSeparator) Then 'si la posicion 3 posee puntos

                        splitLine.SetValue(splitLine.GetValue(3).ToString().Replace(groupSeparator, decimalSeparator), 3) 'lo reemplazamos con coma

                    End If

                    If splitLine.GetValue(5).ToString().Contains(groupSeparator) Then 'si la posicion 5 posee puntos

                        splitLine.SetValue(splitLine.GetValue(5).ToString().Replace(groupSeparator, decimalSeparator), 5) 'lo reemplazamos con coma

                    End If

                    Console.WriteLine(splitLine.Length)
                    __lTeoricos.Add(New FotoEmeOTD(Convert.ToInt64(splitLine.GetValue(1)),
                                                                    splitLine.GetValue(2),
                                                   Convert.ToDouble(splitLine.GetValue(3)),
                                                                     splitLine.GetValue(4),
                                                                     Convert.ToDouble(splitLine.GetValue(5)),
                                                                     splitLine.GetValue(6),
                                                                     Convert.ToChar(splitLine.GetValue(7))))
                Loop
                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, "Ok"})

            Else
                lResultado = New List(Of Object)(New Object() {-1, "Archivo Inexistente"})
            End If
        Catch ex As Exception
            lResultado = New List(Of Object)(New Object() {-1, "Error de Importacion: " + ex.ToString})

        End Try
        Return lResultado
    End Function


    '-------------------------------------------------------------------------------------------------------------------------------------------------------'

  Public Function lCargar_maestro_temporal(ByRef nTipoActualizacion As Integer) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lCargar_maestro_temporal()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'creamos una instancia temporal de un item de de la tabla
            Dim oItemTmp As New TmpMaestroArticuloOTD()
            oItemTmp.nIdInventario = Inventario_OTD.nId

            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Limpiando Maestro Temporal", "...")

            'llamamos al metodo de eliminacion de registros del maestro temporal
            lResultado = oApModelo.TmpMaestro_ADM().lEliminar(oItemTmp)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then

                'creamos un archivo donde derivar los registros
                __oArchivoCSV = New ArchivoMaestroCSV(Config.Get_instancia().get_valor(Config.UNIDAD_MONTADA))

                'mensaje de estado
                Principal_frm().mostrar_mensaje_estado("Recorriendo items de foto de stock", "...")

                'variables auxiliares
                Dim c As Integer = 0

                'seteamos la barra de progreso
                Principal_frm.pbr_progreso.Maximum = __lTeoricos.Count

                'recorremos la lista de items de la foto actual
                For Each oItem As FotoEmeOTD In __lTeoricos

                    'si el contador tiene un valor multiplo de 1000 o es cero o igual al numero de elementos de la lista
                    If c.Equals(0) Or (c Mod 1000 = 0) Or c.Equals(Principal_frm.pbr_progreso.Maximum) Then
                        'mensaje de estado
                        Principal_frm().mostrar_mensaje_estado("Recorriendo items de foto de stock" _
                                                               , String.Format("{0} de {1}", c, __lTeoricos.Count) _
                                                               )
                        Application.DoEvents()

                    End If

                    'incrementamos el contador
                    c += 1
                    Principal_frm.pbr_progreso.Value = c

                    If oItem.nId = 124188 Then
                        oItem.nId = oItem.nId
                    End If

                    'si el inventario permite pesables
                    If Inventario_OTD.Configuracion_OTD.bConPesables Then
                        'creamos una nueva instancia de item de maestro temporal
                        Dim oTmpMaestro As New TmpMaestroArticuloOTD(Inventario_OTD.nId _
                                                                     , oItem.cDescripcion.Trim().ToUpper() _
                                                                     , oItem.nId _
                                                                     , oItem.nCosto _
                                                                     , oItem.cDetalle _
                                                                     , oItem.nTeorico _
                                                                     , oItem.cIdSector _
                                                                     , oItem.cPesable _
                                                                     )

                        'llamamos al metodo de insercion en la tabla del maestro temporal 
                        __oArchivoCSV.Archivo.WriteLine(oTmpMaestro.Get_csv())

                    ElseIf oItem.cPesable.Equals(TmpMaestroArticuloOTD.NO_PESABLE) Then
                        'si no permite pesables
                        'creamos una nueva instancia de item de maestro temporal
                        Dim oTmpMaestro As New TmpMaestroArticuloOTD(Inventario_OTD.nId _
                                                                     , oItem.cDescripcion.Trim().ToUpper() _
                                                                     , oItem.nId _
                                                                     , oItem.nCosto _
                                                                     , oItem.cDetalle _
                                                                     , oItem.nTeorico _
                                                                     , oItem.cIdSector _
                                                                     , oItem.cPesable _
                                                                     )

                        'llamamos al metodo de insercion en la tabla del maestro temporal 
                        __oArchivoCSV.Archivo.WriteLine(oTmpMaestro.Get_csv())

                    End If

                Next

                'cerramos el archivo
                __oArchivoCSV.Archivo.Close()

                '-------------------------------------------------------------------------------------------------------------------------------------------------------'

                'evaluamos el tipo de actualizacion a ejecutar
                Select Case nTipoActualizacion
                    Case FrmPrincipal.REEMPLAZAR
                        'mensaje de estado
                        Principal_frm().mostrar_mensaje_estado("Ejecutando Carga Masiva de Articulos", "...")

                        'llamamos al procedimiento de ejecucion de carga masiva
                        lResultado = oApModelo.TmpMaestro_ADM().lCarga_masiva(__oArchivoCSV.Nombre_archivo, Inventario_OTD)

                        'llamamos al metodo de actualizacion de parametros de la bd
                        lResultado = lTransferir_parametros_a_colector()

                    Case Else
                        'mensaje de estado
                        Principal_frm().mostrar_mensaje_estado("Ejecutando Actualización de Existencias de Articulos", "...")

                        'llamamos al procedimiento de ejecucion de carga masiva
                        lResultado = oApModelo.TmpMaestro_ADM().lActualizacion_masiva(__oArchivoCSV.Nombre_archivo, Inventario_OTD)


                End Select



            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Error escribiendo en archivo CSV" _
                                                   , lResultado(1).ToString())

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia de los datos de parametros desde la db local a la db SQLite
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lTransferir_parametros_a_colector() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lTransferir_parametros_a_colector()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Conectando a BD SQLite", "...")

            'abrimos la conexion al archivo SQLITE
            lResultado = oApModeloSQLite.Get_conector().lConectar()

            'si se pudo establecer
            If lResultado(0).Equals(1) Then

                'mensaje de estado
                Principal_frm().mostrar_mensaje_estado("Pasando parametros de ", " configuraciones")

                'creamos una instancia auxiliar
                Dim oConfig As New ConfiguracionSQLiteOTD()
                oConfig.nCantidadMaxima = Inventario_OTD.Configuracion_OTD.nConteoMaximo
                If Inventario_OTD.Configuracion_OTD.bConDecimales Then oConfig.nConDecimales = 1

                'actualizamos la configuracion actual
                lResultado = oApModeloSQLite.Configuraciones_ADM().lActualizar(oConfig)

                'transferimos las locaciones
                __lTransferir_locaciones_sqlite()

                'transferimos los soportes
                __lTransferir_soportes_sqlite()

                'transferimos los usuarios
                __lTrasferir_usuarios_sqlite()

                'transferimos los sectores
                __lTrasferir_sectores_sqlite()

            End If

            'cerramos la conexion al archivo SQLITE
            oApModeloSQLite.Get_conector().lDesconectar(NOMBRE_METODO)

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia de los datos del maestro de articulos desde la db local a la db SQLite
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lTransferir_maestro_a_colector() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lTransferir_maestro_a_colector()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Pasando maestro de", "articulos")

            'recuperamos los sectores de productos
            lResultado = oApModelo.Productos_ADM().lGet_maestro_inventario(Inventario_OTD)

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                Dim dtTabla As DataTable = CType(lResultado(1), DataTable)

                'abrimos la conexion al archivo SQLITE
                lResultado = oApModeloSQLite.Get_conector().lConectar()

                'si tiene elementos, llamamos al metodo de CREACION de la tabla
                lResultado = oApModeloSQLite.Maestro_ADM().lCrear_tabla()

                'si se ejecuto correctamente, ejecutamos la insercion masiva de datos de sectores
                If lResultado(0).Equals(1) Then lResultado = oApModeloSQLite.Maestro_ADM().lBulk_insert(dtTabla)

                'cerramos la conexion al archivo SQLITE
                oApModeloSQLite.Get_conector().lDesconectar(NOMBRE_METODO)

            Else
                'si no se recuperaron las locaciones, evento alog
                log().Escribir(NOMBRE_METODO & " Error recuperando Maestro local: " & lResultado(1).ToString())

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})
            log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia de datos de Locaciones a la BD SQLite
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lTransferir_locaciones_sqlite() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lTransferir_locaciones_sqlite()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Pasando parametros de ", " locaciones")

            'recuperamos las locaciones
            lResultado = oApModelo.Locaciones_ADM().lGet_elementos(String.Empty)

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la lista devuelta
                Dim lLocaciones As List(Of LocacionOTD) = CType(lResultado(1), List(Of LocacionOTD))

                'si tiene elementos, llamamos al metodo de limpieza de la tabla
                lResultado = oApModeloSQLite.Locaciones_ADM().lEliminar(New LocacionSQLiteOTD())

                'estado
                Dim c As Integer = 0
                Principal_frm.pbr_progreso.Value = 0
                Principal_frm.pbr_progreso.Maximum = lLocaciones.Count

                'recorremos la lista y las insertamos en la tabla nueva
                For Each oLocacion As LocacionOTD In lLocaciones
                    'mensaje de estado
                    Principal_frm().mostrar_mensaje_estado("Cargando locacion " _
                                                           , String.Format("{0} de {1}", c, lLocaciones.Count) _
                                                           )
                    Principal_frm.pbr_progreso.Value = c

                    'llamamos al metodo de insercion en la tabla sqlite
                    lResultado = oApModeloSQLite.Locaciones_ADM().lAgregar(New LocacionSQLiteOTD(oLocacion.nId _
                                                                                                , oLocacion.cDescripcion _
                                                                                                ) _
                                                                           )

                    'si no se ejecuto correctamente
                    If Not lResultado(0).Equals(1) Then
                        'evento a log
                        log().Escribir(NOMBRE_METODO & " Error insertando Locaciones en SQLite: " & lResultado(1).ToString())
                        'salimos del bucle
                        Exit For
                    End If

                    'incrementamos el contador
                    c += 1

                Next

            Else
                'si no se recuperaron las locaciones, evento alog
                log().Escribir(NOMBRE_METODO & " Error recuperando Locaciones locales: " & lResultado(1).ToString())

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})
            log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia de datos de Soportes a la BD SQLite
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lTransferir_soportes_sqlite() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lTransferir_soportes_sqlite()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Pasando parametros de ", " soportes")

            'recuperamos los soportes
            lResultado = oApModelo.Soportes_ADM().lGet_elementos(String.Empty)

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la lista devuelta
                Dim lSoportes As List(Of SoporteOTD) = CType(lResultado(1), List(Of SoporteOTD))

                'si tiene elementos, llamamos al metodo de limpieza de la tabla
                lResultado = oApModeloSQLite.Soportes_ADM().lEliminar(New SoporteSQLiteOTD())

                'estado
                Dim c As Integer = 0
                Principal_frm.pbr_progreso.Value = 0
                Principal_frm.pbr_progreso.Maximum = lSoportes.Count

                'recorremos la lista y las insertamos en la tabla nueva
                For Each oSoporte As SoporteOTD In lSoportes
                    'mensaje de estado
                    Principal_frm().mostrar_mensaje_estado("Cargando soporte " _
                                                           , String.Format("{0} de {1}", c, lSoportes.Count) _
                                                           )
                    Principal_frm.pbr_progreso.Value = c

                    'creamos una nueva instancia de soporte
                    Dim oSoporteSQLite As New SoporteSQLiteOTD()
                    oSoporteSQLite.nId = oSoporte.nId
                    oSoporteSQLite.cDescripcion = oSoporte.cDescripcion
                    If oSoporte.bSubDivisible Then oSoporteSQLite.nSubDivisible = 1

                    'llamamos al metodo de insercion en la tabla sqlite
                    lResultado = oApModeloSQLite.Soportes_ADM().lAgregar(oSoporteSQLite)

                    'si no se ejecuto correctamente
                    If Not lResultado(0).Equals(1) Then
                        'evento a log
                        log().Escribir(NOMBRE_METODO & " Error insertando Soportes en SQLite: " & lResultado(1).ToString())
                        'salimos del bucle
                        Exit For
                    End If

                    'incrementamos el contador
                    c += 1

                Next

            Else
                'si no se recuperaron las locaciones, evento alog
                log().Escribir(NOMBRE_METODO & " Error recuperando Soportes locales: " & lResultado(1).ToString())

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})
            log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia de datos de Usuarios a la BD SQLite
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lTrasferir_usuarios_sqlite() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lTrasferir_usuarios_sqlite()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Pasando parametros de ", " usuarios")

            'recuperamos las usuarios habilitados
            lResultado = oApModelo.Usuarios_ADM().lGet_elementos(String.Format(" WHERE {0}.{1} = '{2}'" _
                                                                                , UsuariosTBL.NOMBRE_TABLA _
                                                                                , UsuariosTBL.HABILITADO.cNombre _
                                                                                , "True" _
                                                                               ) _
                                                                )

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la lista devuelta
                Dim lUsuarios As List(Of UsuarioOTD) = CType(lResultado(1), List(Of UsuarioOTD))

                'si tiene elementos, llamamos al metodo de limpieza de la tabla
                lResultado = oApModeloSQLite.Usuarios_ADM().lEliminar(New UsuarioSQLiteOTD())

                'estado
                Dim c As Integer = 0
                Principal_frm.pbr_progreso.Value = 0
                Principal_frm.pbr_progreso.Maximum = lUsuarios.Count

                'recorremos la lista y las insertamos en la tabla nueva
                For Each oUsuario As UsuarioOTD In lUsuarios
                    'mensaje de estado
                    Principal_frm().mostrar_mensaje_estado("Cargando usuario " _
                                                           , String.Format("{0} de {1}", c, lUsuarios.Count) _
                                                           )
                    Principal_frm.pbr_progreso.Value = c

                    'llamamos al metodo de insercion en la tabla sqlite
                    lResultado = oApModeloSQLite.Usuarios_ADM().lAgregar(New UsuarioSQLiteOTD(oUsuario.nId, oUsuario.cDescripcion _
                                                                        , oUsuario.NivelAcceso.nId, oUsuario.Contrasena) _
                                                                        )

                    'si no se ejecuto correctamente
                    If Not lResultado(0).Equals(1) Then
                        'evento a log
                        log().Escribir(NOMBRE_METODO & " Error insertando Usuarios en SQLite: " & lResultado(1).ToString())
                        'salimos del bucle
                        Exit For
                    End If

                    'incrementamos el contador
                    c += 1

                Next

            Else
                'si no se recuperaron las locaciones, evento alog
                log().Escribir(NOMBRE_METODO & " Error recuperando Usuarios locales: " & lResultado(1).ToString())

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})
            log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia de datos de Sectores a la BD SQLite
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lTrasferir_sectores_sqlite() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lTrasferir_sectores_sqlite()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Pasando parametros de ", " sectores")

            'recuperamos los sectores de productos
            lResultado = oApModelo.Sectores_ADM().lGet_sectores_sqlite()

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la lista devuelta
                Dim lSectores As List(Of SectorOTD) = CType(lResultado(1), List(Of SectorOTD))
                Dim dtTabla As DataTable = CType(lResultado(2), DataTable)

                'si tiene elementos, llamamos al metodo de limpieza de la tabla
                lResultado = oApModeloSQLite.Sectores_ADM().lEliminar(New SectorSQLiteOTD())

                'ejecutamos la insercion masiva de datos de sectores
                lResultado = oApModeloSQLite.Sectores_ADM().lBulk_insert(dtTabla)

            Else
                'si no se recuperaron las locaciones, evento alog
                log().Escribir(NOMBRE_METODO & " Error recuperando Sectores locales: " & lResultado(1).ToString())

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})
            log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transferencia del archivo SQLite de la PC al colector
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lTransferir_bd_a_colector() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lTransferir_bd_a_colector()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'intentamos conectarnos al archivo SQLite
            lResultado = oApModeloSQLite.Get_conector().lConectar()

            'si se pudo conectar
            If lResultado(0).Equals(1) Then
                'llamamos al metodo de creacion de la tabla LECTURAS
                lResultado = oApModeloSQLite.Lecturas_ADM().lCrear_tabla()

                'compactamos la BD
                lResultado = oApModeloSQLite.Get_conector().lEjecutar_sentencia("VACUUM")

            End If

            'intentamos conectarnos al archivo SQLite
            lResultado = oApModeloSQLite.Get_conector().lDesconectar(NOMBRE_METODO)


            'AGREGADO -- CDGS - 06/10/2017 - Transferencia de archivos vis WI-FI FTP
            'si el modo de transferencia seleccionado es USB
            If __PrincipalFRM.mnu_modo_transmision.SelectedIndex = 0 Then
                'path de destino del archivo
                Dim cDestinoSQLite As String = WinMobile.Get_rom() + "\"
                cDestinoSQLite += Config.Get_instancia().get_valor(Config.NOMBRE_SQLITE)'coloca el nombre

                'intentamos transferir el archivo de la PC al colector
                lResultado = Me.lTransferir_archivo_x_USB(oApModeloSQLite.Get_path_sqlite(), cDestinoSQLite, OperacionesArchivos.CARGA)

            Else
                Dim cDestinoSQLite As String = Config.Get_instancia().get_valor(Config.DESTINO_SQLITE)
                lResultado = Me.lTransferir_archivo_x_FTP(oApModeloSQLite.Get_path_sqlite(), cDestinoSQLite, OperacionesArchivos.CARGA)

            End If

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'mensaje de notificacion
                notificar_exito("Archivo Transferido Existosamente!", "Maestro a Colector")

            Else
                'sino, mensaje de notificacion
                notificar_stop(lResultado(1).ToString(), "Maestro a Colector")

            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Error escribiendo en archivo CSV", lResultado(1).ToString())

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la transmisión del archivo de origen al destino, en el colector, por medio de la conexion USB
    ''' </summary>
    ''' <param name="cPathOrigen">Directorio, absoluto, del archivo de origen en la PC local</param>
    ''' <param name="cPathDestino">Directorio, absoluto, de destino del archivo en colector</param>
    ''' <param name="eOperacion">Tipo de Operación a ejecutar</param>
    ''' <returns>Lista de resultado [Integer, Object]</returns>
    ''' <remarks></remarks>
    Public Function lTransferir_archivo_x_USB(cPathOrigen As String, cPathDestino As String, eOperacion As OperacionesArchivos) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lTransferir_archivo_x_USB()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        __PrincipalFRM.mostrar_mensaje_estado("Detectando dispositivo", "")

        'si el equipo esta conectado
        If WinMobile.Equipo_conectado() Then
            'invocamos al colector de basura de memoria (por si acaso)
            GC.Collect()

            'evaluamos el tipo de operacion a ejecutar
            Select Case eOperacion

                Case OperacionesArchivos.CARGA

                    'mostramos el estado
                    __PrincipalFRM.mostrar_mensaje_estado(String.Format("Transfiriendo archivo PC > PDA via USB [{0} a {1}]", cPathOrigen, cPathDestino), "Progreso")

                    'intentamos transferir el archivo de la PC al colector
                    lResultado = WinMobile.lMover_Archivo_Dispositivo(cPathOrigen, cPathDestino, True, True)

                Case OperacionesArchivos.DESCARGA

                    'mostramos el estado
                    __PrincipalFRM.mostrar_mensaje_estado(String.Format("Transfiriendo archivo PDA > PC via USB [{0} a {1}]", cPathOrigen, cPathDestino), "Progreso")

                    'ejecutamos la tranferencia del archivo al colector
                    lResultado = WinMobile.lMover_Archivo_Dispositivo(cPathOrigen, cPathDestino, True, False)

            End Select
        Else
            'si no esta conectado, notificar
            notificar_stop("No se detecta el dispositivo conectado a la PC!", "Maestro a Colector")

        End If

        'devolvemos el resultado del metodo
        Return lResultado


    End Function


    ''' <summary>
    ''' Ejecuta la transmisión del archivo de origen al destino, en el colector, por medio de la conexion WIFI
    ''' </summary>
    ''' <param name="cPathOrigen">Directorio, absoluto, del archivo de origen en la PC local</param>
    ''' <param name="cPathDestino">Directorio, absoluto, de destino del archivo en colector</param>
    ''' <param name="eOperacion">Tipo de Operación a ejecutar</param>
    ''' <returns>Lista de resultado [Integer, Object]</returns>
    ''' <remarks></remarks>
    Public Function lTransferir_archivo_x_FTP(cPathOrigen As String, cPathDestino As String, eOperacion As OperacionesArchivos) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lTransferir_archivo_x_FTP()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'desplegamos el selector de colector de destino 
        Dim oResultado As DialogResult = Me.FrmColectores().ShowDialog()

        'sise selecciono alguno
        If oResultado = DialogResult.OK And Not Me.Colector_OTD Is Nothing And Not Me.Colector_OTD.DireccionIP Is Nothing And Me.Colector_OTD.DireccionIP.Length > 7 Then
            '

            'invocamos al colector de basura de memoria (por si acaso)
            GC.Collect()

            'evaluamos el tipo de operacion a ejecutar
            Select Case eOperacion

                Case OperacionesArchivos.CARGA
                    'mostramos el estado
                    __PrincipalFRM.mostrar_mensaje_estado(String.Format("Transfiriendo archivo PC > PDA via FTP [{0} a {1}]", cPathOrigen, cPathDestino), "Progreso")

                    'ejecutamos la tranferencia del archivo al colector
                    lResultado = ClienteFTP.lEnviar(Me.Colector_OTD.DireccionIP, cPathDestino, cPathOrigen)

                Case OperacionesArchivos.DESCARGA
                    'mostramos el estado
                    __PrincipalFRM.mostrar_mensaje_estado(String.Format("Transfiriendo archivo PDA > PC via FTP [{0} a {1}]", cPathOrigen, cPathDestino), "Progreso")

                    'ejecutamos la tranferencia del archivo al colector
                    lResultado = ClienteFTP.lTraer(Me.Colector_OTD.DireccionIP, cPathOrigen, cPathDestino)


            End Select



        ElseIf Not Me.Colector_OTD.DireccionIP Is Nothing And Me.Colector_OTD.DireccionIP.Length < 8 Then

            'si no esta conectado, notificar
            notificar_stop(String.Format("El colector parece no tener una IP válida!\nEs: {0}", Me.Colector_OTD.DireccionIP), "Maestro a Colector")

        End If

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    ''' <summary>
    ''' Ejecuta la transferencia del archivo de Lecturas desde el Colector a la PC
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lDescargar_lecturas() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lDescargar_lecturas()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'variables a utilizar
            Dim cDestinoCSV As String = "{0}\{1:yyMMddHHmmss}.csv"

            'cambiamos el cursor del mouse a "Esperar..."
            Cursor.Current = Cursors.WaitCursor

            __PrincipalFRM.mostrar_mensaje_estado("Validando destino de archivo", "")

            'si no hay un directorio definido en el archivo de configuraciones
            If Config.Get_instancia().get_valor(Config.DESTINO_LECTURAS).Equals(String.Empty) Then
                'ensamblamos el nuevo nombre para el archivo a descargar en el directorio home del usuario
                cDestinoCSV = String.Format(cDestinoCSV, Me.cDirHomeusuario, Date.Now)
            Else
                'si esta definido, lo descargamos donde se indique
                cDestinoCSV = Config.Get_instancia().get_valor(Config.DESTINO_LECTURAS) _
                                                & String.Format("{0:yyMMddHHmmss}.csv", Date.Now)
            End If

            __PrincipalFRM.mostrar_mensaje_estado("Moviendo archivo desde dispositivo", "a PC...")

            'formamos el directorio de origen del archivo de lecturas
            'Dim cPathLecturasCsv As String = String.Format("{0}\{1}", Config.Get_instancia().get_valor(Config.DESTINO_SQLITE), Config.Get_instancia().get_valor(Config.ORIGEN_LECTURAS))
            Dim cPathLecturasCsv As String = String.Format("{0}\{1}", WinMobile.Get_rom(), Config.Get_instancia().get_valor(Config.ORIGEN_LECTURAS))

            'AGREGADO -- CDGS - 06/10/2017 - Transferencia de archivos vis WI-FI FTP
            'si el modo de transferencia seleccionado es USB
            If __PrincipalFRM.mnu_modo_transmision.SelectedIndex <= 0 Then
                'intentamos transferir el archivo de la PC al colector
                lResultado = Me.lTransferir_archivo_x_USB(cPathLecturasCsv, cDestinoCSV, OperacionesArchivos.DESCARGA)

            Else
                lResultado = Me.lTransferir_archivo_x_FTP(cPathLecturasCsv, cDestinoCSV, OperacionesArchivos.DESCARGA)

            End If

            'si se ejecuto correctamente, devolvemos el directorio de destino del archivo como parte del resultado
            If lResultado(0).Equals(1) Then
                lResultado(1) = cDestinoCSV
            End If

        Catch ex As Exception
            'en caso de error, resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

            'mensaje de estado
            Principal_frm().mostrar_mensaje_estado("Error escribiendo en archivo CSV", lResultado(1).ToString())

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' //////////////////////////////////////////////////////////////////////////////// '''
    '''                                 METODOS ESTANDARES
    ''' //////////////////////////////////////////////////////////////////////////////// '''


    ''' <summary>
    ''' Despliega el mensaje de evento con observaciones con el formato correspondiente
    ''' </summary>
    ''' <param name="cMensaje">Mensaje a desplegar</param>
    ''' <param name="cTitulo">Titulo del cuadro de mensajes</param>
    ''' <remarks></remarks>
    Public Sub notificar_stop(ByVal cMensaje As String, ByVal cTitulo As String)

        'mostramos el mensaje de notificacion
        MessageBox.Show(cMensaje, cTitulo, MessageBoxButtons.OK, MessageBoxIcon.Stop)

        'evento a log
        log.Escribir("Titulo: " & cMensaje & ". Mensaje: " & cMensaje)

    End Sub

    ''' <summary>
    ''' Despliega el mensaje de error con el formato correspondiente
    ''' </summary>
    ''' <param name="cMensaje">Mensaje a desplegar</param>
    ''' <param name="cTitulo">Titulo del cuadro de mensajes</param>
    ''' <remarks></remarks>
    Public Sub notificar_error(ByVal cMensaje As String, ByVal cTitulo As String)

        'mostramos el mensaje de notificacion
        MessageBox.Show(cMensaje, cTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error)

        'evento a log
        log.Escribir("Titulo: " & cMensaje & ". Mensaje: " & cMensaje)

    End Sub

    ''' <summary>
    ''' Despliega el mensaje de evento existoso con el formato correspondiente
    ''' </summary>
    ''' <param name="cMensaje">Mensaje a desplegar</param>
    ''' <param name="cTitulo">Titulo del cuadro de mensajes</param>
    ''' <remarks></remarks>
    Public Sub notificar_exito(ByVal cMensaje As String, ByVal cTitulo As String)

        'mostramos el mensaje de notificacion
        MessageBox.Show(cMensaje, cTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub



#End Region


#Region "GETTERS Y SETTERS"


#Region "FORMULARIOS"

    ''' <summary>
    ''' Devuelve o establece la instancia del formulario Principal de la aplicacion
    ''' </summary>
    ''' <value>Nueva instancia del Formulario Principal</value>
    ''' <returns>Instancia del Formulario Principal</returns>
    ''' <remarks></remarks>
    Public Property Principal_frm() As FrmPrincipal
        Get
            Return __PrincipalFRM
        End Get
        Set(ByVal value As FrmPrincipal)
            __PrincipalFRM = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Login de la aplicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del Formulario de Login</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_login_frm() As FrmSeleccionLocal
        Get
            If __LoginFRM Is Nothing Then
                __LoginFRM = New FrmSeleccionLocal(Me)
            ElseIf __LoginFRM.IsDisposed Then
                __LoginFRM = New FrmSeleccionLocal(Me)
            End If
            Return __LoginFRM

        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Datos de Inventario Actual
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Datos de Inventario Actual</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_datosInventario_frm() As FrmDatosInventario
        Get
            If __DatosInventarioFRM Is Nothing Then
                __DatosInventarioFRM = New FrmDatosInventario(Me)
            ElseIf __DatosInventarioFRM.IsDisposed Then
                __DatosInventarioFRM = New FrmDatosInventario(Me)
            End If
            Return __DatosInventarioFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Seleccion de Sectores
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Seleccion de Sectores</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_sectores_frm() As FrmSectores
        Get
            If __SectoresFRM Is Nothing Then
                __SectoresFRM = New FrmSectores(Me)
            ElseIf __SectoresFRM.IsDisposed Then
                __SectoresFRM = New FrmSectores(Me)
            End If
            Return __SectoresFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Visualizacion de Detalles x Ubicaciones fisicas
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Visualizacion de Detalles x Ubicaciones</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_detalles_x_ubicacion_frm() As FrmDetallesUbicaciones
        Get
            If __DetallesUbicacionFRM Is Nothing Then
                __DetallesUbicacionFRM = New FrmDetallesUbicaciones(Me)
            ElseIf __DetallesUbicacionFRM.IsDisposed Then
                __DetallesUbicacionFRM = New FrmDetallesUbicaciones(Me)
            End If
            Return __DetallesUbicacionFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Carga Manual de Conteos
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Carga Manual de Conteos</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_carga_manual_frm() As FrmCargaManual
        Get
            If __CargaManualFRM Is Nothing Then
                __CargaManualFRM = New FrmCargaManual(Me)
            ElseIf __CargaManualFRM.IsDisposed Then
                __CargaManualFRM = New FrmCargaManual(Me)
            End If
            Return __CargaManualFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Visualizacion de Entradas de Lecturas
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Visualizacion de Entradas de Lecturas</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_entrada_colectores_frm() As FrmEntradasColectores
        Get
            If __EntradasFRM Is Nothing Then
                __EntradasFRM = New FrmEntradasColectores(Me)
            ElseIf __EntradasFRM.IsDisposed Then
                __EntradasFRM = New FrmEntradasColectores(Me)
            End If
            Return __EntradasFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Visualizacion de Opciones del Inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Visualizacion de Opciones del Inventario</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_opciones_frm() As FrmOpciones
        Get
            If __OpcionesFRM Is Nothing Then
                __OpcionesFRM = New FrmOpciones(Me)
            ElseIf __OpcionesFRM.IsDisposed Then
                __OpcionesFRM = New FrmOpciones(Me)
            End If
            Return __OpcionesFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Visualizacion de Existencias
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Visualizacion de Existencias</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_ver_existencias_frm() As FrmVerExistencias
        Get
            If __VerExistenciasFRM Is Nothing Then
                __VerExistenciasFRM = New FrmVerExistencias(Me)
            ElseIf __VerExistenciasFRM.IsDisposed Then
                __VerExistenciasFRM = New FrmVerExistencias(Me)
            End If
            Return __VerExistenciasFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Visualizacion de Sectores Cubiertos
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Visualizacion de Sectores Cubiertos</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_sectores_cubiertos_frm() As FrmSectoresCubiertos
        Get
            If __SectoresCubiertosFRM Is Nothing Then
                __SectoresCubiertosFRM = New FrmSectoresCubiertos(Me)
            ElseIf __SectoresCubiertosFRM.IsDisposed Then
                __SectoresCubiertosFRM = New FrmSectoresCubiertos(Me)
            End If
            Return __SectoresCubiertosFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Visualizacion de Info de Inventario actual
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Visualizacion de Info de Inventario actual</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_info_actual_frm() As FrmInfoActual
        Get
            If __InfoActualFRM Is Nothing Then
                __InfoActualFRM = New FrmInfoActual(Me)
            ElseIf __InfoActualFRM.IsDisposed Then
                __InfoActualFRM = New FrmInfoActual(Me)
            End If
            Return __InfoActualFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de ABM de Usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de ABM de Usuarios</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_abm_usuarios_frm() As FrmAbmUsuarios
        Get
            If __AbmUsuariosFRM Is Nothing Then
                __AbmUsuariosFRM = New FrmAbmUsuarios(Me)
            ElseIf __AbmUsuariosFRM.IsDisposed Then
                __AbmUsuariosFRM = New FrmAbmUsuarios(Me)
            End If
            Return __AbmUsuariosFRM
        End Get
    End Property

    ''' <summary>
    ''' Devuelve una instancia del formulario de Modificacion de Conteo
    ''' </summary>
    ''' <value></value>
    ''' <returns>Instancia del formulario de Modificacion de Conteo</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Get_mod_conteo_frm() As FrmModificarConteo
        Get
            If __ModConteoFRM Is Nothing Then
                __ModConteoFRM = New FrmModificarConteo(Me)
            ElseIf __ModConteoFRM.IsDisposed Then
                __ModConteoFRM = New FrmModificarConteo(Me)
            End If
            Return __ModConteoFRM
        End Get
    End Property

#End Region


    ''' <summary>
    ''' Devuelve una instancia del manejador de logs de la aplicacion
    ''' </summary>
    ''' <returns>Instancia del manejador de logs</returns>
    ''' <remarks></remarks>
    Public Function log() As InfoALog
        If __infoAlog Is Nothing Then
            __infoAlog = New InfoALog(NOMBRE_APLICACION)
        End If

        Return __infoAlog

    End Function

    ''' <summary>
    ''' Devuelve la referencia a la instancia del formulario de seleccion de colectores
    ''' </summary>
    ''' <value></value>
    ''' <returns>Referencia a la instancia de FrmColectores</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FrmColectores() As FrmColectores
        Get
            If __ColectoresFRM Is Nothing Then
                __ColectoresFRM = New FrmColectores(Me, oApModelo)
            End If
            Return __ColectoresFRM
        End Get
    End Property


#End Region




End Class
