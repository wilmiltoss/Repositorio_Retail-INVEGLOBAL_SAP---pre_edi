Imports System.Windows.Forms

Public Class FrmDatosInventario

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmDatosInventario"
    Private __oApControlador As ApControlador

    Private __bNuevosDatos As Boolean = False
    Private __nPosicion As Integer = -1
    Private __cStringAuxiliar As String = String.Empty

    Public lSectores As New List(Of SectorOTD)

    Private __lLocales As New List(Of LocalOTD)


    Enum TITULOS
        Guardar
        Aceptar
    End Enum



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControlador As ApControlador)

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'tomamos la instancia del constrolador
        __oApControlador = oApControlador

        'inicializamos el resto de los componentes
        __Inicializar_componentes()


    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion de los demas componentes internos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__Inicializar_componentes()"

        Try
            'evento a LOG
            __oApControlador.log().Escribir("Se carga el Formulario : " & Me.Name)

            'cargamos el combo de empresas
            Dim lResultado As List(Of Object) = __lCargar_empresas()

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then

                'establecemos la empresa actual
                Me.cmb_empresa.SelectedIndex = Me.cmb_empresa.Items.IndexOf(__oApControlador.Inventario_OTD.Local_OTD.Empresa_OTD)

                'cargamos los datos de locales
                lResultado = __lCargar_locales()

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'obtenemos los datos del inventario actual
                    lResultado = __lObtener_datos_inventario()

                    'si no se ejecuto correctamente, mensaje de notificacion
                    If Not lResultado(0).Equals(1) Then __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Locales")

                Else
                    'sino, mensaje de notificacion
                    __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Locales")

                End If
            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Empresas")

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(ex.Message, NOMBRE_METODO)

        End Try

    End Sub



#End Region


#Region "METODOS"

    ''' <summary>
    ''' Carga los items del combo de empresas
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lCargar_empresas() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lCargar_empresas()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No ejecutado!"})

        Cursor.Current = Cursors.WaitCursor

        Try
            'recuperamos los datos de empresas
            lResultado = __oApControlador.oApModelo.Empresas_ADM.lGet_elementos(String.Empty)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'asignamos el origen de datos del combo de empresas
                cmb_empresa.DataSource = CType(lResultado(1), List(Of EmpresaOTD))
                cmb_empresa.DisplayMember = "cDescripcion"
                cmb_empresa.SelectedItem = cmb_empresa.Items(0)
            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        Cursor.Current = Cursors.Default

        'devolvemos el resultado
        Return lResultado


    End Function

    ''' <summary>
    ''' Carga los items del combo de locales
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lCargar_locales() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lCargar_locales()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No ejecutado!"})

        Cursor.Current = Cursors.WaitCursor

        Try
            Dim cFiltroWhere As String = " WHERE {0} IN ({1})"
            Dim cIdLocales As String = " "

            'recorremos el listado de locales al que el usuari esta asignado
            For Each oLocal As LocalOTD In __oApControlador.Usuario_OTD.Locales
                cIdLocales += oLocal.nId.ToString() + ","
            Next

            'establecemos los parametros del filtro
            cFiltroWhere = String.Format(cFiltroWhere _
                                            , LocalesTBL.ID.cNombre _
                                            , Mid(cIdLocales, 1, cIdLocales.Length - 1) _
                                        )


            'recuperamos los datos de locales de la misma empresa
            lResultado = __oApControlador.oApModelo.Locales_ADM.lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                __lLocales = CType(lResultado(1), List(Of LocalOTD))

                'asignamos el origen de datos del combo de locales
                cmb_local.DataSource = __lLocales
                cmb_local.SelectedItem = cmb_empresa.Items(0)

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        Cursor.Current = Cursors.Default

        'devolvemos el resultado
        Return lResultado


    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE OBTENCION DE DATOS ULTIMO INVENTARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Function __lObtener_datos_inventario() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lObtener_datos_inventario()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Cursor.Current = Cursors.WaitCursor

        'intentamos desplegar los datos del inventario actual
        Try
            Me.dtp_fecha_inventario.Value = __oApControlador.Inventario_OTD.dFechaInventario
            Me.txt_sistema_gestion.Text = __oApControlador.Inventario_OTD.Sistema_OTD.ToString()
            Me.txt_comentarios.Text = __oApControlador.Inventario_OTD.cDescripcion
            Me.cmb_local.SelectedIndex = Me.cmb_local.Items.IndexOf(__oApControlador.Inventario_OTD.Local_OTD)

            Me.Refresh()

            'establecemos el resultado del metodo
            lResultado = New List(Of Object)(New Object() {1, "Ok"})

        Catch ex As Exception
            'en caso de error, valor a Cuadro de "Sistema de Gestión"
            Me.txt_sistema_gestion.Text = "No Hay Datos de Inventario"

            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        Cursor.Current = Cursors.Default

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    ''' <summary>
    ''' habilita los controles para ingresar nuevos datos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __habilitar_controles()

        'habilitamos los controles necesarios para ingresar los datos
        dtp_fecha_inventario.Enabled = True
        cmb_empresa.Enabled = True
        cmb_local.Enabled = True
        txt_comentarios.Enabled = True

        'borramos el contenido del cuadro de "Codigo de Sector"
        'txt_sector.Text = String.Empty

        'habilitamos el menu "Guardar Datos"
        'mnu_guardar_datos.Enabled = True

        'habilitamos el boton [Cancelar]
        Cancel_Button.Enabled = True

        'deshabilitamos el menu de "Nuevo Inventario"
        mnu_nuevo_inventario.Enabled = False

    End Sub

    ''' <summary>
    ''' Deshabilita los controles de ingresao de datos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __deshabilitar_controles()
        'habilitamos los controles necesarios para ingresar los datos
        Me.dtp_fecha_inventario.Enabled = False
        Me.cmb_empresa.Enabled = False
        Me.cmb_local.Enabled = False
        Me.txt_comentarios.Enabled = False

        'y tambien el boton de seleccion de sectores
        Me.cmd_sector.Enabled = False

    End Sub

    ''' <summary>
    ''' Ejecuta la persistencia de los nuevos datos de inventario
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function lGuardar_inventario() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lGuardar_inventario()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'si no se encontro el ID del Local
            If __oApControlador.NuevoInventario_OTD.Local_OTD.nId <= 0 Then
                'mensaje de notificacion
                lResultado = New List(Of Object)(New Object() {0, "Aparentemente el Local Seleccionado no es Válido.." _
                                                                    & Chr(13) & "No Se puede Continuar bajo ésta Condición."})

                'devolvemos el resultado de la funcion
                Return lResultado

            End If

            'si no se encontro el ID del Sistema
            If __oApControlador.NuevoInventario_OTD.Local_OTD.Sistema_OTD.nId <= 0 Then
                'mensaje de notificacion
                lResultado = New List(Of Object)(New Object() {0, "Aparentemente el Sistema de Gestión no es Válido.." _
                                                                & Chr(13) & "No Se puede Continuar bajo ésta Condición."})

                'devolvemos el resultado de la funcion
                Return lResultado

            End If

            'establecemos el cursor del mouse a "Esperar..."
            Cursor.Current = Cursors.WaitCursor

            'llamamos al metodo de insersion denuevo inventario
            lResultado = __oApControlador.oApModelo.Inventarios_ADM.lAgregar(__oApControlador.NuevoInventario_OTD)

            'si se ejecuto exitosamente
            If lResultado(0).Equals(1) Then
                'si se ejecuto correctamente, evento a LOG
                __oApControlador.log().Escribir(String.Format("Se Abre un Nuevo Inventario. FECHA_INVENTARIO: {0:dd/MM/yyyy}, Local: {1}, Usuario: {2}" _
                                                              , __oApControlador.NuevoInventario_OTD.dFechaInventario _
                                                              , __oApControlador.NuevoInventario_OTD.Local_OTD.ToString() _
                                                              , __oApControlador.NuevoInventario_OTD.Usuario_OTD.ToString() _
                                                              ))

                'llamamos a funcion de Intercambio de Variables del Modulo de Administracion de Sistemas
                SISTEMAS.oficializar_sistema()

                __oApControlador.Inventario_OTD = __oApControlador.NuevoInventario_OTD
                __oApControlador.Local_OTD = Me.cmb_local.SelectedItem
                'cambiamos el valor de la variable de control de datos nuevos
                __bNuevosDatos = False

            End If

        Catch ex As Exception
            'en caso de error, establecemos el resultado del metodo
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    ''' <summary>
    ''' OBTIENE Y GUARDA LOS SECTORES AFECTADOS POR EL INVENTARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pGuardar_Sectores_Inventario()
        'variables a utilizar
        Dim cNivel, cCodigo As String

        'recorremos los elementos de la Lista de Sectores a Inventariar
        For Each oSector As Object In Me.lSectores
            'separamos los componentes
            cNivel = oSector.ToString.Substring(0, oSector.ToString.IndexOf("|"))
            cCodigo = oSector.ToString.Substring(oSector.ToString.IndexOf("|") + 1)

            'evaluamos el nivel del sector actual
            Select Case Int16.Parse(cNivel)
                Case 1
                    cCodigo = cCodigo.Substring(0, 2)
                Case 2
                    cCodigo = cCodigo.Substring(0, 4)
                Case 3
                    cCodigo = cCodigo.Substring(0, 6)
                Case 4
                    cCodigo = cCodigo.Substring(0, 8)
                Case 5
                    cCodigo = cCodigo.Trim()
                Case Else
                    cCodigo = cCodigo.Trim()

            End Select

            'ensamblamos la Sentencia SQL
            cSentenciaSQL = "EXECUTE [SP_INSERTAR_INVENTARIO_SECTORES] " _
                                                      & " @id_local = " & cID_Local _
                                                      & ",@id_sistema = " & cID_Sistema_Gestion _
                                                      & ",@id_sector = '" & cCodigo & "'"

            'llamamos a funcion de Ejecucion de Sentencia SQL
            If principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
            End If

        Next oSector

    End Sub

    ''' <summary>
    ''' OBTIENE LOS DATOS DE SECTORES DESDE LA BD DEL SISTEMA DE GESTION, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Obtuvieron Correctamente</returns>
    ''' <remarks></remarks>
    Private Function lObtener_sectores() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".lObtener_sectores()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Return lResultado

        'reseteamos el DataTable Auxiliar y el DataRow
        dtDataTableAuxiliar = New DataTable

        Try
            'llamamos a funcion de creacion de DataTable
            dtDataTableAuxiliar = SISTEMAS.dtbObtener_Sectores()

            'recorremos las filas del DataTable
            For Each drwFila In dtDataTableAuxiliar.Rows
                'ensamblamos la Sentencia SQL con los valores de cada campo
                cSentenciaSQL = "EXECUTE [SP_UPSERT_SECTORES] " _
                                        & " @id_sistema= " & cID_Sistema_Gestion _
                                        & " ,@id_sector= '" & drwFila.Item(0).ToString() & "'" _
                                        & " ,@nombre_sector= '" & drwFila.Item(1).ToString.Replace("'", "-") & "'" _
                                        & " ,@nivel= " & drwFila.Item(2).ToString() _
                                        & " ,@id_sector_padre= '" & drwFila.Item(3).ToString() & "'"

                'llamamos a procedimiento de Ejecucion de Sentencia SQL
                If Not principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
                    'sino se inserto el registro, evento a LOG
                    'principal.pInfo_a_Log("No se Transfirieron los Datos de Sectores a Inventariar")

                    'el resultado lo devolvemos como false
                    Return lResultado

                End If
            Next

            'el resultado lo devolvemos como true
            Return lResultado

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error Intentando Transferir Registros de Sectores a BD Local")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'el resultado lo devolvemos como false
            Return lResultado

        End Try

    End Function

    ''' <summary>
    ''' Metodo de filtrado de lista de locales
    ''' </summary>
    ''' <param name="oLocal">Item a Evaluar</param>
    ''' <returns>True|False segun sea</returns>
    ''' <remarks></remarks>
    Private Function EsDeEmpresa(ByVal oLocal As LocalOTD) As Boolean
        Return oLocal.Empresa_OTD.nId = CType(cmb_empresa.SelectedItem, EmpresaOTD).nId
    End Function



#End Region


#Region "EVENTOS"


    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'si el texto de este boton es [Guardar]
        If Me.OK_Button.Text = "Guardar" Then
            'llamamos al evento click del Menu "Guardar Datos Nuevos"
            Me.mnu_guardar_datos_Click(sender, e)

            'salimos del sub
            Exit Sub

        End If

        'si son Nuevos Datos no confirmados
        If __bNuevosDatos Then
            'mensaje de notificacion de Grabado de Datos Pendientes
            MessageBox.Show("Esta Pendiente el Grabado de los Datos Nuevos..." _
                            & Chr(13) & "Ingrese al Menu 'Archivo' y luego 'Guardar Datos'", _
                            "Datos Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

            'salimos del sub
            Exit Sub

        End If

        'establecemos el resultado del Cuadro de Dialogo a "OK"
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        'cerramos el formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        'cambiamos el valor de la variable de controld e datos nuevos
        __bNuevosDatos = False

        'deshabilitamos este boton
        Me.Cancel_Button.Enabled = False

        'deshabilitamos el menu "Guardar Datos"
        mnu_guardar_datos.Enabled = False

        'habilitamos el menu "Nuevo Inventario"
        mnu_nuevo_inventario.Enabled = True

        'establecemos a "CANCEL el resultado de dialogo de este formulario
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        'cerramo el formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' Cuando se hace click en el menu "Nuevo Inventario"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_nuevo_inventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_nuevo_inventario.Click
        'pedimos la confirmación del usuario
        If MessageBox.Show("¿Está seguro de Iniciar un Nuevo Inventario? " _
                            & Chr(13) & "Esto reemplazará los Datos Cargados Actuales..", _
                            "Nuevo Proceso de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                            MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

            'cambiamos el estado de la variable de Nuevos Datos
            __bNuevosDatos = True

            'reseteamos la instancia de inventario nuevo en el controlador
            __oApControlador.NuevoInventario_OTD = New InventarioOTD()

            'llamamos a procedimiento de habilitacion de controles
            __habilitar_controles()

            'reseteamos los valores de los controles
            dtp_fecha_inventario.Value = Date.Today.Date
            cmb_local.SelectedItem = cmb_local.Items(0)
            txt_sistema_gestion.Text = "0 - S/D"
            txt_comentarios.Text = "INGRESE UN COMENTARIO..."

            'cambiamos el titulo del Boton [Aceptar] a [Guardar] y lo deshabilitamos
            OK_Button.Text = TITULOS.Guardar.ToString()
            OK_Button.Enabled = False

            'enfoque a Combo de Locales
            cmb_local.Focus()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU [Guardar Datos]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_guardar_datos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_guardar_datos.Click

        'asignamos los datos requeridos 
        __oApControlador.NuevoInventario_OTD.Local_OTD = CType(cmb_local.SelectedItem, LocalOTD)
        __oApControlador.NuevoInventario_OTD.dFechaInventario = dtp_fecha_inventario.Value
        __oApControlador.NuevoInventario_OTD.Usuario_OTD = __oApControlador.Usuario_OTD
        __oApControlador.NuevoInventario_OTD.cDescripcion = txt_comentarios.Text.Trim().ToUpper()

        'llamamos al metodo de persistencia de inventario nuevo
        Dim lResultado As List(Of Object) = lGuardar_inventario()

        'llamamos a procedimiento de Almacenamiento de Datos Nuevos
        If lResultado(0).Equals(1) Then
            'si se almacenaron correctamente, mensaje de notificacion
            __oApControlador.notificar_exito("Los Datos del Nuevo Inventario se Guardaron Correctamente!", "Datos de Inventario")

            'establecemos el el Resultado del Dialogo a "OK"
            Me.DialogResult = DialogResult.OK

        Else
            'sino, mensaje de notificacion
            __oApControlador.notificar_error("Los Datos del Nuevo Inventario No se Guardaron Correctamente debido a algún Error!" _
                            & Chr(13) & "Inténtelo una vez más y si no Funciona informe a TIC", "Datos de Inventario")

            __oApControlador.notificar_error("Detalles del error:" + lResultado(1).ToString(), "Datos de Inventario")

            'establecemos el el Resultado del Dialogo a "No"
            Me.DialogResult = DialogResult.No

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [...]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_sector_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_sector.Click

        'mostramos el formulario de Seleccion de Sectores en modo dialogo
        __oApControlador.Get_sectores_frm().ShowDialog()

        'si el resultado del cuadro de dialogo es "OK"
        If __oApControlador.Get_sectores_frm().DialogResult.Equals(DialogResult.OK) Then
            'habilitamos el menu de guardado
            mnu_guardar_datos.Enabled = True

            'habilitamos el botono [Guardar]
            OK_Button.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click
        'establecemos el Resultado del Dialogo a "Cancel"
        Me.DialogResult = DialogResult.Cancel

        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' Cuando cambia el item seleccionado del combo de "Empresas"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_empresa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_empresa.SelectedIndexChanged
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        Cursor.Current = Cursors.WaitCursor

        'si es una instancia de empresa valida
        If CType(cmb_empresa.SelectedItem, EmpresaOTD).nId > 0 Then
            'filtramos la lista de locales
            cmb_local.DataSource = Nothing
            cmb_local.DataSource = __lLocales.FindAll(AddressOf EsDeEmpresa)

        End If

        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Cuando se cambia el item seleccionado del combo de "Locales"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_local_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_local.SelectedIndexChanged

        'si el form no puede recibir el enfoque, salimos 
        If Not CanFocus Or cmb_local.SelectedItem Is Nothing Then Return

        'eliminamos el contenido de la lista de sectores seleccionados
        lSectores = New List(Of SectorOTD)

        'bloqueamos el comando y el menu "Guardar"
        mnu_guardar_datos.Enabled = False
        OK_Button.Enabled = False

        'tomamos la instancia de local seleccionada
        Dim oLocal As LocalOTD = CType(cmb_local.SelectedItem, LocalOTD)

        'si son Datos de Inventario Nuevo, llamamos a funcion de Carga de Parametros de Sistema
        If __bNuevosDatos Then SISTEMAS.bPreparar_sistema_temporal(oLocal.Sistema_OTD)

        'si es un local valido, habilitamos el boton [...]
        If oLocal.nId > 0 Then cmd_sector.Enabled = True


    End Sub



#End Region


End Class
