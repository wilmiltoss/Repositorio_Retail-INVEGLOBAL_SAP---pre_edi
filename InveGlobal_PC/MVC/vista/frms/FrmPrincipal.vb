Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports CdgUtiles.Util
Imports System.Data.OleDb

Public Class FrmPrincipal

#Region "CAMPOS"


    Public Const NOMBRE_CLASE As String = "FrmPrincipal"

    Public oApControlador As ApControlador
    Public Const REEMPLAZAR As Integer = 0
    Public Const ACTUALIZAR As Integer = 1



#End Region


#Region "CONTRUCTORES "


    Public Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        oApControlador = New ApControlador()

    End Sub

#End Region


#Region "METODOS"

    ''' <summary>
    ''' Ejecuta el despliegue de datos de inventarios en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub mostrar_form()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".mostrar_form()"

        Try
            'cursor del mouse a "Esperar"
            Cursor.Current = Cursors.WaitCursor

            'si el ID de Local es 0 (cero)
            If oApControlador.Local_OTD.nId.Equals(0) Then

                'bloqueamos todos los menues a diferencia de los necesarios para abrir un nuevo inventario
                'ayuda o salir del sistema
                Me.mnu_actualizar_teoricos.Enabled = False
                Me.mnu_colectores.Enabled = False
                Me.mnu_opciones.Enabled = False
                Me.mnu_configuracion_inventario.Enabled = False
                Me.mnu_obtener_maestro.Enabled = False
                Me.mnu_carga_manual.Enabled = False
                Me.mnu_detalles_x_ubicaciones.Enabled = False
                Me.mnu_resumen_inventario.Enabled = False
                Me.cmd_sectores.Enabled = False



            Else
                'llamamos al metodo de recuperacion de datos de inventario
                Dim lResultado As List(Of Object) = oApControlador.lObtener_datos_inventario()

                'si se ejcuto correctamente
                If lResultado(0).Equals(1) Then
                    'los valores para los controles del formulario
                    Me.lbl_empresa.Text = "Empresa : " & oApControlador.Inventario_OTD.Local_OTD.Empresa_OTD.ToString()
                    Me.lbl_nro_conteo.Text = oApControlador.Inventario_OTD.nUltimoConteo.ToString()
                    Me.lbl_fecha.Text = String.Format("Fecha Inventario : {0:dd/MM/yyyy}", oApControlador.Inventario_OTD.dFechaInventario)
                    Me.lbl_local.Text = "Local : " & oApControlador.Inventario_OTD.Local_OTD.ToString()
                    Me.lbl_sistema.Text = "Sistema : " & oApControlador.Inventario_OTD.Sistema_OTD.ToString()
                    Me.lbl_estado_inventario.Text = IIf(oApControlador.Inventario_OTD.bCerrado, "Cerrado", "Abierto")

                Else
                    'mensaje de notificacion
                    oApControlador.notificar_stop(lResultado(1).ToString(), "Datos de Inventario")

                    Me.lbl_sistema.Text = "No Hay Datos de Inventario"

                    'salimos del sub
                    Exit Sub

                End If

                'llamamos a la funcion de Preparacion del Modulo Correspondiente al Sistema
                'If Not SISTEMAS.bPreparar_sistema(oApControlador.Inventario_OTD.Sistema_OTD) Then
                'si no se preparo el modulo correctamente, mensaje de notificacion
                'MessageBox.Show("El modulo " & cNombre_Sistema_Gestion & " no esta preparado!", "Carga de Modulos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'End If


            End If

            'si el inventario ya esta cerrado, lo deshabilitamos
            Me.mnu_cerrar_inventario.Enabled = (oApControlador.Inventario_OTD.bCerrado = False)
            Me.mnu_modo_transmision.SelectedIndex = 0

            Me.mnu_version.Text = Me.mnu_version.Text + " " + ApControlador.VERSION

            'asignamos la instancia de este formulario al controlador
            oApControlador.Principal_frm() = Me

        Catch ex As Exception

        Finally
            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End Try

    End Sub

    ''' <summary>
    ''' MUESTRA UN MENSAJE EN LA BARRA DE ESTADO
    ''' </summary>
    ''' <param name="cMensajeParam">Mensaje a Desplegar</param>
    ''' <param name="cEstadoParam">Mensaje de estado a Desplegar en las segunda posicion</param>
    ''' <remarks></remarks>
    Public Sub mostrar_mensaje_estado(ByVal cMensajeParam As String, ByVal cEstadoParam As String)

        'si los mensajes son diferentes
        If Not Me.lbl_mensaje.Text.Equals(cMensajeParam) Then
            'pasamos el mensaje principal
            Me.lbl_mensaje.Text = cMensajeParam
        End If

        'si los mensajes son diferentes
        If Not Me.lbl_estado.Text.Equals(cEstadoParam) Then
            'pasamos la barra de estado
            Me.lbl_estado.Text = cEstadoParam
        End If

        'refrescamos el formulario
        Me.StatusStrip1.Refresh()
        Application.DoEvents()

    End Sub

    ''' <summary>
    '''  CARGA EN LA BASE DE DATOS LOS REGISTROS DESDE EL ARCHIVO CSV Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="strNombreArchivoCSV">Nombre del Archivo CSV a Cargar</param>
    ''' <returns>Devuelve 'True' si se Cargo Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bCargar_Datos_Lecturas(ByVal strNombreArchivoCSV As String) As Boolean

        MessageBox.Show("Nombre de Archivo " & strNombreArchivoCSV)

        'variables a utilizar
        Dim fstreamRArchivoCSV As StreamReader
        Dim strLineaArchivo As String = String.Empty
        Dim strNombreColector As String = String.Empty
        Dim intPosicionSeparador As Integer = -1
        Dim arlstCampos As New ArrayList()

        Dim SP_INSERTAR_ENTRADA_COLECTORA As String = "EXECUTE [SP_INSERTAR_ENTRADA_COLECTORA] " _
                                                          & " @id_inventario = {0}" _
                                                          & ", @id_locacion = {1} " _
                                                          & ", @nro_conteo = {2}" _
                                                          & ", @id_soporte = {3}" _
                                                          & ", @nro_soporte = {4}" _
                                                          & ", @id_letra_soporte = {5}" _
                                                          & ", @nivel = {6}" _
                                                          & ", @metro = {7}" _
                                                          & ", @scanning = '{8}'" _
                                                          & ", @cantidad = '{9}'" _
                                                          & ", @nombre_colector = '{10}'" _
                                                          & ", @id_usuario= {11}"

        'intentamos abrir el archivo para leerlo
        Try
            fstreamRArchivoCSV = New StreamReader(strNombreArchivoCSV)

            'leeamos la primera linea del archivo
            strLineaArchivo = fstreamRArchivoCSV.ReadLine()

            'tomamos la posicion del separador de nombre del dispositivo
            intPosicionSeparador = IIf(strLineaArchivo = Nothing, -1, strLineaArchivo.IndexOf(":"))

            'si la linea contiene el nombre del dispositivo colector
            If intPosicionSeparador > -1 Then
                'tomamos el nombre del colector
                strNombreColector = strLineaArchivo.Substring(intPosicionSeparador + 1)

            Else
                'sino, lo tomamos como "Desconocido"
                strNombreColector = "Desconocido"

            End If

            'leemos todas las lineas del archivo mientras no sea fin del mismo
            While Not fstreamRArchivoCSV.EndOfStream
                'tomamos la linea
                strLineaArchivo = fstreamRArchivoCSV.ReadLine()

                'separamos los campos
                arlstCampos = principal.arlstSepararCampos(strLineaArchivo, ";")

                'DEBUG
                'MessageBox.Show("Campos " & arlstCampos.Count())

                'intentamos en concatenar la sentencia SQL
                Try
                    If arlstCampos.Count = 10 Then
                        'MessageBox.Show(arlstCampos.ToString())
                        cSentenciaSQL = String.Format(SP_INSERTAR_ENTRADA_COLECTORA, cID_Inventario _
                                                                                      , arlstCampos(0) _
                                                                                      , arlstCampos(1) _
                                                                                      , arlstCampos(2) _
                                                                                      , arlstCampos(3) _
                                                                                      , arlstCampos(4) _
                                                                                      , arlstCampos(5) _
                                                                                      , arlstCampos(6) _
                                                                                      , arlstCampos(7) _
                                                                                      , arlstCampos(8) _
                                                                                      , strNombreColector _
                                                                                      , arlstCampos(9))

                    Else
                        'si el archivo no contiene todos los campos esperados
                        MessageBox.Show("El Archivo Generado solo contiene " + arlstCampos.Count().ToString + " Campos", _
                                            "Archivo CSV de Datos Colectados", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

                        'devolvemos el resultado de la funcion
                        Return False

                    End If

                Catch Ex As Exception
                    'en caso de error, mensaje de notificacion
                    MessageBox.Show("Error Separar los Campos CSV", "Descarga de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'evento a LOG
                    'principal.pInfo_a_Log("Error Separar los Campos CSV : " & strLineaArchivo)
                    'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

                    'cerramos el archivo
                    fstreamRArchivoCSV.Close()

                    'liberamos la variable
                    fstreamRArchivoCSV.Dispose()

                    'devolvemos el resultado de la funcion
                    Return False

                End Try

                'llamamos a funcion de Ejecucion de Sentencia SQL
                If Not principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
                    'si no se ejecuto, mensaje de notificacion
                    MessageBox.Show("No se pudo Guardar uno de los Registros", "Descarga de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'cerramos el archivo
                    fstreamRArchivoCSV.Close()

                    'liberamos la variable
                    fstreamRArchivoCSV.Dispose()

                    'devolvemos el resultado de la funcion
                    Return False

                End If

            End While

        Catch Ex As IOException
            'en caso de error de Entrada-Salida, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Descarga de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'evento a LOG
            'principal.pInfo_a_Log("Error intentando abrir y leer Archivo CSV : " & strNombreArchivoCSV)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        Catch Ex As Exception
            'en caso de error de otro tipo, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Descarga de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'evento a LOG
            'principal.pInfo_a_Log("Error intentando abrir y leer Archivo CSV : " & strNombreArchivoCSV)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        End Try

        'cerramos el archivo
        fstreamRArchivoCSV.Close()

        'liberamos la variable
        fstreamRArchivoCSV.Dispose()

        'intentamos eliminar el archivo
        Try
            File.Move(strNombreArchivoCSV, strNombreArchivoCSV.ToUpper.Replace(".csv", "bkp"))

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error Intentando Mover Archivo CSV a BKP: " & strNombreArchivoCSV)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

        'devolvemos el resultado de la funcion
        Return True

    End Function

    ''' <summary>
    ''' DESPLIEGUA LOS DATOS DE ENTRADAS LECTURAS Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <remarks></remarks>
    Private Function dtbDatos_Cargados() As DataTable
        'variables a utilizar
        Dim intCantidadRegistros As Integer = -1

        Dim SENTENCIA_SQL_1 As String = "SELECT * FROM [VW_ENTRADAS_COLECTORES] WHERE [ID_INVENTARIO] = {0}"

        'intentamos recuperar los datos
        Try
            'llamamos a funcion de Ejecucion de Consulta SQL
            dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(String.Format(SENTENCIA_SQL_1, cID_Inventario))

            'devolvemos el DataTable como resultado de la funcion
            Return dtDataTableAuxiliar

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Despliegue de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'evento a LOG
            'principal.pInfo_a_Log("Error intentando desplegar Entradas de Colectores : " & SENTENCIA_SQL_1)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return dtDataTableAuxiliar

        End Try

    End Function

    ''' <summary>
    ''' EJECUTA LA ACTUALIZACION DEL MAESTRO DE ARTICULOS Y LAS EXISTENCIAS TEORICAS
    ''' </summary>
    ''' <param name="nTipoParam">Tipo de Actualizacion a Realizar [REEMPLAZAR|ACTUALIZAR]</param>
    ''' <remarks></remarks>
    Private Sub tomar_maestro(ByRef nTipoParam)

        'si no hay Datos de Inventario, salimos del proceso
        If oApControlador.Inventario_OTD Is Nothing Or oApControlador.Inventario_OTD.nId.Equals(0) Then
            'mensaje de notificacion
            oApControlador.notificar_error("No hay datos del Inventario!", "Estado del Inventario")

            'salimos del sub
            Exit Sub

        End If

        'si el inventario ya esta cerrado
        If oApControlador.Inventario_OTD.bCerrado Then
            'mensaje de notificacion
            oApControlador.notificar_stop("El Inventario ya esta Cerrado!", "Estado del Inventario")

            'salimos del sub
            Exit Sub

        End If

        'si el conteo es mayor a 1
        If oApControlador.Inventario_OTD.nUltimoConteo > 1 Then
            'mensaje de notificacion
            oApControlador.notificar_stop("El Maestro ya no Puede Ser Reemplazado!" & Chr(13) & "Nro. Conteo : " _
                                          & oApControlador.Inventario_OTD.nUltimoConteo.ToString(), "Estado del Inventario")

            'salimos del sub
            Exit Sub

        End If

        'pedimos la confirmacion del usuario
        If Not MessageBox.Show("¿Esta seguro de Reemplazar el Maestro Actual?" _
                            & Chr(13) & "Obs.: Esto puede tomar varios minutos y puede que el Sistema" _
                            & " deje de responder mientras dura la operación...", "Toma de Maestro Nuevo", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue diferente a "SI", salimos del sub
            Exit Sub
        Else
            If Not MessageBox.Show("¿Esta realmente seguro de lo que va a hacer? " & Chr(13) & "¿Reemplazar el Maestro Actual junto con las existencias?" _
                                               , "Reconfirmar Maestro Nuevo", _
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                'si la respuesta fue diferente a "SI", salimos del sub
                Exit Sub

            ElseIf Not MessageBox.Show("A partir de este punto no habrá vuelta atrás... ¿Esta seguro de lo que hace?" _
                                        , "Reconfirmar Maestro Nuevo", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                'si la respuesta fue diferente a "SI", salimos del sub
                Exit Sub
            End If
        End If

        'establecemos el cursor del mouse a "Esperar"
        Cursor.Current = Cursors.WaitCursor


        'mostramos el mensaje
        Me.mostrar_mensaje_estado("Obteniendo Foto de Existencias", "...")

        Dim lResultado As List(Of Object)
        'llamamos al metodo de recuperacion de datos de existencias=================================

        'Si es local SAP 18 ejecuta directamente lGet_foto_sap, sino intetará conectarse al EME
        If oApControlador.Local_OTD.Sistema_OTD.nId = 18 Then
            lResultado = oApControlador.lGet_foto_sap()

        Else
            'mostramos el mensaje
            mostrar_mensaje_estado("Intentado conectar al Sistema: ", oApControlador.Local_OTD.Sistema_OTD.ToString())

            'llamamos al metodo de conexion al sistema de gestion correspondiente
            lResultado = oApControlador.lConectar_sistema_gestion()

            'si se conecto sin problemas
            If lResultado(0).Equals(1) Then
                lResultado = oApControlador.lGet_foto_eme()
            End If

        End If


        Application.DoEvents()

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then

            ''' //////////////////////////////////////////////////////// '''
            ''' ejecutamos los procedimientos de preparacion del maestro '''
            ''' //////////////////////////////////////////////////////// '''
            'cargamos el maestro temporal  A
            lResultado = oApControlador.lCargar_maestro_temporal(nTipoParam)

            Application.DoEvents()

            'si se ejecuto correctamente y lo que se hizo fue reemplazar el maestro
            If lResultado(0).Equals(1) And nTipoParam = REEMPLAZAR Then

                'llamamos al metodo de transferencia del maestro a la BD sqlite
                lResultado = oApControlador.lTransferir_maestro_a_colector()

            End If

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then

                'mensaje de notificacion
                oApControlador.notificar_exito("Maestro de Articulos Actualizado Correctamente", "Inventario Actual")

                'habilitamos los menues siguientes
                Me.mnu_actualizar_teoricos.Enabled = True
                Me.mnu_colectores.Enabled = True
                Me.mnu_carga_manual.Enabled = True
                Me.mnu_detalles_x_ubicaciones.Enabled = True
                Me.mnu_resumen_inventario.Enabled = True

                Me.mnu_cerrar_inventario.Enabled = True

                'mensaje de estado
                mostrar_mensaje_estado("Maestro obtenido exitosamente", "")


            End If




        End If

        'si alguno de los anteriores finalizo con error
        If Not lResultado(0).Equals(1) Then
            'mensaje de notificacion
            oApControlador.notificar_error(lResultado(1).ToString(), "Inventario Actual")

            'mensaje de estado
            mostrar_mensaje_estado("Error obteniendo Maestro", "")

        End If

        'reseteamos la barra de progreso
        pbr_progreso.Value = 0

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

        'refrescamos el formulario
        Me.Refresh()


    End Sub

#End Region


#Region "EVENTOS"


#Region "FORMULARIO"

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_principal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'mostramos el formulario de login
        oApControlador.Get_login_frm().ShowDialog()

        'si el resultado es distinto de "Ok"
        If Not oApControlador.Get_login_frm().DialogResult.Equals(DialogResult.OK) Then
            'cerramos este formulario
            Close()

        Else
            'llamamos al metodo de despliegue de este formulario
            mostrar_form()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO EL FORMULARIO GANA EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_principal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        'si el ID del inventario es diferente de Cero
        If Not oApControlador.Inventario_OTD.nId.Equals("0") Then
            'mostramos el estado del inventario
            Me.lbl_estado_inventario.Text = IIf(oApControlador.Inventario_OTD.bCerrado, "Cerrado", "Abierto")
        End If
    End Sub

    ''' <summary>
    ''' CUANDO EL FORMULARIO SE ESTA CERRANDO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_principal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'pedimos confirmacion al usuario
        If Not MessageBox.Show("¿Esta Seguro de Salir de la Aplicación?", "InveGlobal 1.0", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue diferente de "SI", cancelamos el cierre del formlario
            e.Cancel = True

        End If
    End Sub


#End Region


#Region "MENU ARCHIVO"

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Datos de Inventario"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_datos_inventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_datos_inventario.Click

        'mostramos el formulario de Datos de Inventario
        oApControlador.Get_datosInventario_frm().ShowDialog()

        'si el resultado del dialogo es "OK"
        If oApControlador.Get_datosInventario_frm().DialogResult.Equals(DialogResult.OK) Then
            'bloqueamos todos los menues a diferencia de los necesarios para abrir un nuevo inventario, ayuda o salir del sistema
            Me.mnu_colectores.Enabled = False
            Me.mnu_opciones.Enabled = False
            Me.mnu_configuracion_inventario.Enabled = False
            Me.mnu_obtener_maestro.Enabled = False

            Me.mnu_actualizar_teoricos.Enabled = False

            Me.mnu_carga_manual.Enabled = False
            Me.mnu_resumen_inventario.Enabled = False
            Me.cmd_sectores.Enabled = False
            Me.mnu_detalles_x_ubicaciones.Enabled = False

            Me.mnu_cerrar_inventario.Enabled = False

            'llamamos al metodo de recuperacion de datos de inventario
            Dim lResultado As List(Of Object) = oApControlador.lObtener_datos_inventario()

            'si se ejcuto correctamente
            If lResultado(0).Equals(1) Then
                'los valores para los controles del formulario
                Me.lbl_empresa.Text = "Empresa : " & oApControlador.Inventario_OTD.Local_OTD.Empresa_OTD.ToString()
                Me.lbl_nro_conteo.Text = oApControlador.Inventario_OTD.nUltimoConteo.ToString()
                Me.lbl_fecha.Text = String.Format("Fecha Inventario : {0:dd/MM/yyyy}", oApControlador.Inventario_OTD.dFechaInventario)
                Me.lbl_local.Text = "Local : " & oApControlador.Inventario_OTD.Local_OTD.ToString()
                Me.lbl_sistema.Text = "Sistema : " & oApControlador.Inventario_OTD.Sistema_OTD.ToString()
                Me.lbl_estado_inventario.Text = IIf(oApControlador.Inventario_OTD.bCerrado, "Cerrado", "Abierto")

                'si se cargaron correctamente, habilitamos los menues
                Me.mnu_opciones.Enabled = True
                Me.mnu_configuracion_inventario.Enabled = True

                Me.cmd_sectores.Enabled = True

            Else
                'mensaje de notificacion
                oApControlador.notificar_stop(lResultado(1).ToString(), "Datos de Inventario")

                Me.lbl_sistema.Text = "No Hay Datos de Inventario"

                'salimos del sub
                Exit Sub

            End If
        End If

        'refrescamos este formulario
        Me.Refresh()


    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Configuracion del Inventario"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_configuracion_inventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_configuracion_inventario.Click
        'desplegamos el formulario de opciones de configuracion en modo de dialogo
        oApControlador.Get_opciones_frm().ShowDialog()

        'si el Resultado de Dialogo del formulario es "OK"
        If oApControlador.Get_opciones_frm().DialogResult = DialogResult.OK Then
            'habilitamos el menu de "Obtener Maestro"
            Me.mnu_obtener_maestro.Enabled = True

        End If

        'refrescamos el formulario
        Me.Refresh()


    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Obtener Maestro"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_obtener_maestro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_obtener_maestro.Click

        'llamamos a la funcion de Preparacion del Modulo Correspondiente al Sistema
        If Not SISTEMAS.bPreparar_sistema(oApControlador.Inventario_OTD.Sistema_OTD) Then
            'si no se preparo el modulo correctamente, mensaje de notificacion
            'MessageBox.Show("El modulo " & cNombre_Sistema_Gestion & " no esta preparado!", "Carga de Modulos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tomar_maestro(REEMPLAZAR)
        Else
            'invocamos al metodo de obtencion del maestro
            tomar_maestro(REEMPLAZAR)

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU 'Actualizar Teoricos'
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_actualizar_teoricos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_actualizar_teoricos.Click

        'llamamos a la funcion de Preparacion del Modulo Correspondiente al Sistema
        If Not SISTEMAS.bPreparar_sistema(oApControlador.Inventario_OTD.Sistema_OTD) Then
            'si no se preparo el modulo correctamente, mensaje de notificacion
            MessageBox.Show("El modulo " & cNombre_Sistema_Gestion & " no esta preparado!", "Carga de Modulos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else
            'invocamos al metodo de obtencion del maestro
            tomar_maestro(ACTUALIZAR)
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Carga Manual"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param> 
    ''' <remarks></remarks>
    Private Sub mnu_carga_manual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_carga_manual.Click
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            oApControlador.notificar_stop("El Inventario ya esta Cerrado!", ApControlador.NOMBRE_APLICACION)
        Else
            'mostramos el formulario de Carga Manual en modo de Dialogo
            oApControlador.Get_carga_manual_frm().ShowDialog()
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Detalles x Ubicaciones"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_detalles_x_ubicaciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_detalles_x_ubicaciones.Click
        'mostramos el formulario de despliegue de detalles por ubicaciones
        oApControlador.Get_detalles_x_ubicacion_frm().ShowDialog()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Resumen de Inventario"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param> 
    ''' <remarks></remarks>
    Private Sub mnu_resumen_inventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_resumen_inventario.Click
        'mostramos el formulario de resumen en forma de dialogo
        oApControlador.Get_ver_existencias_frm().ShowDialog()

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click
        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Sectores Cubiertos]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_sectores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_sectores.Click

        'mostramos el formulario de sectores cubiertos por el inventario
        oApControlador.Get_sectores_cubiertos_frm().ShowDialog()

    End Sub




    ''' <summary>
    ''' CUANDO SE CAMBIA EL TEXTO DE LA ETIQUETA DE "Estado Actual"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lbl_estado_inventario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_estado_inventario.TextChanged
        'si su nuevo Valor es "Cerrado"
        If Me.lbl_estado_inventario.Text.ToUpper.Equals("CERRADO") Then
            'color de la Etiqueta a Rojo
            Me.lbl_estado_inventario.ForeColor = Color.Red

        Else
            'sino, si es "Abierto"
            If Me.lbl_estado_inventario.Text.ToUpper.Equals("CERRADO") Then
                'color de la Etiqueta a Verde
                Me.lbl_estado_inventario.ForeColor = Color.Green

            Else
                'si es desconocido, a Azul
                Me.lbl_estado_inventario.ForeColor = Color.Blue

            End If
        End If
    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU [CERRAR INVENTARIO]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_cerrar_inventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_cerrar_inventario.Click

        'pedimos la confirmacion del usuario
        If (MessageBox.Show("Se procedera a Cerrar el Inventario Actual... " + Chr(13) + "Esta seguro de Proceder?", "Cierre de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes) Then

            'pedimos la RE confirmacion del usuario
            If (MessageBox.Show("Esta muy Seguro de hacerlo??", "Cierre de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes) Then

                'cambiamos el estado del inventario actual
                Me.oApControlador.Inventario_OTD.bCerrado = True
                principal.bInventario_Cerrado = True

                'llamamos al metodo de actualziacion del estado del inventario
                Dim lResultado As New List(Of Object)(New Object() {0, "Cierre de Inventario No Ejecutado!"})

                lResultado = Me.oApControlador.oApModelo.Inventarios_ADM.lActualizar(Me.oApControlador.Inventario_OTD)

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'notificacion
                    Me.oApControlador.notificar_exito("Inventario Cerrado Correctamente!", "Cierre de Inventario")

                Else
                    'notificacion de error
                    Me.oApControlador.notificar_error("Error en Proceso de Cierre de Inventario:" + Chr(13) + "Detalles: " + lResultado(1), "Cierre de Inventario")

                    'devolvemos el inventario actual a su estado
                    Me.oApControlador.Inventario_OTD.bCerrado = False
                    principal.bInventario_Cerrado = False

                End If


                'mostramos el nuevo estado final
                Me.lbl_estado_inventario.Text = IIf(oApControlador.Inventario_OTD.bCerrado, "Cerrado", "Abierto")

            End If

        End If

        'si el inventario ya esta cerrado, lo deshabilitamos
        Me.mnu_cerrar_inventario.Enabled = (oApControlador.Inventario_OTD.bCerrado = False)

    End Sub

#End Region


#Region "MENU COLECTORES"

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Copiar Maestro a Colectores"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_maestro_a_colectores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_maestro_a_colectores.Click
        'pedimos confirmacion del usuario
        If MessageBox.Show("Esto hará que se borre el Contenido Actual del Colector" _
                            & Chr(13) & "¿Esta Seguro de Continuar?", "Transferencia de Maestro Nuevo", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

            'establecemos el cursor del mouse a "Esperar"
            Cursor.Current = Cursors.WaitCursor

            'si alguno de las tablas SQLite no estan actualizadas, las actualizamos
            If Not oApControlador.bMaestroSQLiteActualizado Then mnu_actualizar_maestro_colectora_Click(sender, e)
            If Not oApControlador.bParametrosSQLiteActualizado Then mnu_actualizar_tablas_parametros_Click(sender, e)

            'llamamos al metodo de transferencia de archivo de datos al colector
            oApControlador.lTransferir_bd_a_colector()

            'establecemos el cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU DE "Actualizar Tablas de Parametros"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_actualizar_tablas_parametros_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_actualizar_tablas_parametros.Click

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'llamamos al metodo de actualizacion de tablas de parametros
        Dim lResultado As List(Of Object) = oApControlador.lTransferir_parametros_a_colector()

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then

            'si el que ejecuta la accion NO es el menu de transferencia de archivo SQLite a colector
            If Not sender.GetHashCode().Equals(Me.mnu_maestro_a_colectores.GetHashCode()) Then
                'mensaje de notificacion
                oApControlador.notificar_exito("Parametros actualizados Correctamente!", ApControlador.NOMBRE_APLICACION)
                mostrar_mensaje_estado("Parametros actualizados", "...")

            End If

            'establecemos el marcador de actualizacion de mestro
            oApControlador.bParametrosSQLiteActualizado = True

        Else
            'sino, mensaje de error
            oApControlador.notificar_error(lResultado(1).ToString(), ApControlador.NOMBRE_APLICACION)
            mostrar_mensaje_estado("Error ", "Parametros NO actualizados")

        End If

        'cursor del mouse a Normal
        Cursor.Current = Cursors.Default


    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Actualizar Maestro para Colectoras"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_actualizar_maestro_colectora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_actualizar_maestro_colectora.Click

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'llamamos al metodo de transferencia de maestro de articulos a bd SQLite
        Dim lResultado As List(Of Object) = oApControlador.lTransferir_maestro_a_colector()

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then

            'si el que ejecuta la accion NO es el menu de transferencia de archivo SQLite a colector
            If Not sender.GetHashCode().Equals(Me.mnu_maestro_a_colectores.GetHashCode()) Then
                'mensaje de notificacion
                oApControlador.notificar_exito("Maestro de Articulos transferido exitosamente!", ApControlador.NOMBRE_APLICACION)
                mostrar_mensaje_estado("Maestro a BD Colectores...", "Ok!")

            End If

            'establecemos el marcador de actualizacion de mestro
            oApControlador.bMaestroSQLiteActualizado = True

        Else
            'sino, mensaje de notificacion
            oApControlador.notificar_error(lResultado(1).ToString(), ApControlador.NOMBRE_APLICACION)
            mostrar_mensaje_estado("Maestro NO transferido a BD Colectores!", "")

        End If

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Descargar Lecturas"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_descargar_lecturas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_descargar_lecturas.Click

        'si el inventario ya esta cerrado
        If oApControlador.Inventario_OTD.bCerrado Then
            'mensaje de notificacion
            oApControlador.notificar_stop("El Inventario ya esta Cerrado!", "Estado del Inventario")

            'salimos del sub
            Exit Sub

        End If

        'cambiamos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        ' invocamos al metodo de descarga del archivo de lecturas
        Dim lResultado As List(Of Object) = oApControlador.lDescargar_lecturas()

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then

            mostrar_mensaje_estado("Cargando registros a BD", "")

            'tomamos el archivo y lo cargamos a la bd
            lResultado = oApControlador.oApModelo.EntradasColectorasADM().lCargar_entradas(oApControlador.Inventario_OTD _
                                                                                          , lResultado(1).ToString())

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos el nombre del colector devuelto
                oApControlador.cNombreColector = lResultado(1).ToString()

                mostrar_mensaje_estado("Desplegando datos de lecturas", "")

                'llamamos al metodo de recuperacion de registros de detalles de lecturas
                lResultado = oApControlador.oApModelo.EntradasColectorasADM().lGet_vista_datos(oApControlador.Inventario_OTD _
                                                                                               , oApControlador.cNombreColector)

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'tomamos la tabla devuelta
                    Dim dtTabla As DataTable = CType(lResultado(1), DataTable)

                    'la pasamos la tabla al comtrolador
                    oApControlador.dtTablaAuxiliar = dtTabla

                    'desplegamos el form
                    oApControlador.Get_entrada_colectores_frm().ShowDialog()

                Else
                    'sino se pudo, mensaje de notificacion
                    oApControlador.notificar_error(lResultado(1).ToString(), "Recuperación de de Lecturas")

                    mostrar_mensaje_estado("Lecturas no recuperadas desde la BD", "")

                End If
            Else
                'sino, mensaje de notificacion
                oApControlador.notificar_error(lResultado(1).ToString(), "Carga de Lecturas")

                mostrar_mensaje_estado("Lecturas no cargadas a la BD", "")

            End If

        Else
            'sino se pudo descargar el archivo, mensaje de notificacion
            oApControlador.notificar_stop(lResultado(1).ToString(), "Datos de Lecturas")

            mostrar_mensaje_estado("Archivo no copiado desde dispositivo", "")

        End If

    End Sub



#End Region


#Region "MENU OPCIONES"


    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU DE "Usuarios"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_usuarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_usuarios.Click
        'llamamos al formulario de mantenimiento de usuarios
        oApControlador.Get_abm_usuarios_frm().ShowDialog()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Tipos de Soportes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_soportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_soportes.Click
        'mostramos el Formulario de Seleccion de Soportes en Modo Dialogo
        'frm_soportes.ShowDialog()

        'refrescamos el formulario
        Me.Refresh()

        'mostramos el formulario de Mantenimiento de Datos de Soportes
        'frm_abm_soportes.ShowDialog()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Asociación Tipo Local - Soportes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_aso_local_soporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_aso_local_soportes.Click
        'refrescamos el formulario
        Me.Refresh()

        'desplegamos el formulario de Asociacion de Locales y Soportes en modo de dialogo
        'frm_aso_local_soporte.ShowDialog()

    End Sub


#End Region


#Region "MENU AYUDA"


    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Reportar un Problema"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_reportar_problema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_reportar_problema.Click
        'si el link no esta vacio
        If Not Config.Get_instancia().get_valor(Config.LINK_AYUDA).Equals(Config.NOMBRE_CLASE) Then
            'iniciamos el proceso que se ha definido
            System.Diagnostics.Process.Start(Config.Get_instancia().get_valor(Config.LINK_AYUDA))

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Info Actual"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_info_actual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_info_actual.Click

        'desplegamos el formulario de visualizacion de informacion del inventario actual
        oApControlador.Get_info_actual_frm().ShowDialog()

    End Sub

    Private Sub XToolStripMenuItem_Click(sender As Object, e As EventArgs)
        OpenFileDialog1.ShowDialog()
    End Sub


#End Region


#End Region




End Class
