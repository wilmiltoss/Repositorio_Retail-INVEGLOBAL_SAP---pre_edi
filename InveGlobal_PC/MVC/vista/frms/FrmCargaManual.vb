Imports CdgPersistencia.ClasesBases

Public Class FrmCargaManual

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmCargaManual"

    Private __oApControlador As ApControlador


    Private __lLocaciones As List(Of LocacionOTD)
    Private __lSoportes As List(Of SoporteOTD)

    Private __oLocacion As LocacionOTD
    Private __oSoporte As SoporteOTD
    Private __oCargaManual As CargaManualOTD
    Private __oArticulo As ProductoOTD

    Private __dtTablaDetalles As DataTable

    Private __bReemplazando As Boolean = False
    Private Const NO_ENCONTRADO As String = "NO ENCONTRADO"


#End Region


#Region "CONSTRUTORES"

    ''' <summary>
    ''' Contructor de la clase
    ''' </summary>
    ''' <param name="oApControladorParam">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControladorParam As ApControlador)

        'tomamos la instancia del controlador
        __oApControlador = oApControladorParam

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'inicializamos el resto de los componentes
        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion de los demas componentes de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()
        'evento a LOG
        __oApControlador.log().Escribir("Se abre el Formulario : " & Me.Text)

        'llamamos a procedimientos de carga de items de combos
        __cargar_combos()

        'llamamos al procedimiento de carga de datos a grilla
        __cargar_grilla()

        'cambiamos el tamao del texto
        grd_cargas.Font = New Font(Me.Label1.Font, FontStyle.Regular)

        'si la grilla contiene al menos una fila
        If grd_cargas.RowCount > 0 Then
            'habilitamos el boton [Validad Conteo] y tambien el de [Limpiar Conteo]
            Me.cmd_validar.Enabled = True
            Me.cmd_limpiar.Enabled = True
            Me.cmb_locaciones.Enabled = False

        Else
            'sino, los deshabilitamos
            Me.cmd_validar.Enabled = False
            Me.cmd_limpiar.Enabled = False
            Me.cmb_locaciones.Enabled = True

        End If

    End Sub


#End Region


#Region "METODOS"

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE VALORES A COMBOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_combos()

        Dim lResultado As List(Of Object)

        'recuperamos la lista de locaciones disponibles
        lResultado = __oApControlador.oApModelo.Locaciones_ADM().lGet_elementos(String.Empty)

        'si se ejecuto crrectamente, tomamos la lista devuelta
        If lResultado(0).Equals(1) Then __lLocaciones = CType(lResultado(1), List(Of LocacionOTD))

        'recuperamos la lista de soportes disponibles
        lResultado = __oApControlador.oApModelo.Soportes_ADM().lGet_elementos(String.Empty)

        'si se ejecuto correctamente, tomamos la lista devuelta
        If lResultado(0).Equals(1) Then __lSoportes = CType(lResultado(1), List(Of SoporteOTD))

        'asignamos los origenes de datos de los combos
        cmb_locaciones.DataSource = __lLocaciones
        cmb_soportes.DataSource = __lSoportes
        cmb_locaciones.DisplayMember = "cDescripcion"
        cmb_soportes.DisplayMember = "cDescripcion"

        'mostramos los elementos de la primera posicion de cada lista
        cmb_locaciones.SelectedIndex = 0
        cmb_soportes.SelectedIndex = 0


    End Sub

    ''' <summary>
    ''' MUESTRA LOS REGISTROS DE CONTEOS REALIZADOS EN EL EQUIPO CORRESPONDIENTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_grilla()

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'sentencia de siltrado de registros
        Dim cFiltroWhere As String = String.Format(" WHERE {0}.{1} = {2}" _
                                                   , CargasManualesTBL.NOMBRE_TABLA _
                                                   , CargasManualesTBL.ID_INVENTARIO.cNombre _
                                                   , __oApControlador.Inventario_OTD.nId _
                                                   )

        'llamamos al metodo de recuperacion de registros
        Dim lResultado As List(Of Object) = __oApControlador.oApModelo.CargasManuales_ADM().lGet_elementos(cFiltroWhere)

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then
            'tomamos la tabla devuelta
            __dtTablaDetalles = CType(lResultado(2), DataTable)

            'la asignamos como origen de datos de la grilla
            grd_cargas.DataSource = Nothing
            grd_cargas.DataSource = __dtTablaDetalles

            'llamamos al metodo de ocultar las columnas innecesarias
            __ocultar_columnas()

        ElseIf lResultado(1).ToString().Equals(ConectorBase.ERROR_NO_HAY_FILAS) Then
            'si es que no hay filas, limpiamos la grilla
            __dtTablaDetalles = New DataTable()
            grd_cargas.DataSource = Nothing
        Else
            'sino, mensaje de notificacion
            __oApControlador.notificar_stop(lResultado(1).ToString(), ApControlador.NOMBRE_APLICACION)

        End If


        'habilitamos o deshabilitamos los botones de validacion o limpieza de cargas
        Me.cmd_validar.Enabled = (grd_cargas.Rows.Count > 0)
        Me.cmd_limpiar.Enabled = (grd_cargas.Rows.Count > 0)
        Me.cmb_locaciones.Enabled = False


        'mostramos la cantidad de registros en la grilla
        Me.lbl_estado.Text = Me.grd_cargas.RowCount.ToString() & " Registros..."

        Refresh()

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Oculta las columnas de la grilla que no son necesarias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __ocultar_columnas()

        'recorremos las columnas de la grilla
        For Each cl As DataGridViewColumn In grd_cargas.Columns
            'si la columna actual es alguna de las siguientes las ocultamos
            Select Case cl.HeaderText
                Case CargasManualesTBL.ID_LETRA_SOPORTE.cNombre
                    cl.Visible = False
                Case CargasManualesTBL.COLECTOR.cNombre
                    cl.Visible = False
                Case CargasManualesTBL.ID_LOCACION.cNombre
                    cl.Visible = False
                Case CargasManualesTBL.ID_SOPORTE.cNombre
                    cl.Visible = False

            End Select
        Next

        'refrescamos el form
        Refresh()
        Application.DoEvents()

    End Sub

    ''' <summary>
    ''' EJECUTA LA CONSULTA DE SI EL SOPORTE ES SUBDIVISIBLE, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="cID_Soporte">ID del Soporte a Consultar</param>
    ''' <returns>Devuelve 'True' si es SubDivisible</returns>
    ''' <remarks></remarks>
    Private Function bSoporte_SubDivisible(ByVal cID_Soporte As String) As Boolean
        'variables a utilizar
        Dim bResultado As Boolean = False

        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT ISNULL([SUBDIVISIBLE], 'True') FROM [SOPORTES] WHERE [ID_SOPORTE] = " & cID_Soporte.Trim()

        'llamamos a funcion de Consulta Escalar SQL y evaluamos el resutlado devuelto
        bResultado = IIf(principal.cConsulta_Unico_Resultado(cSentenciaSQL).ToUpper.Equals("FALSE"), False, True)

        'devolvemos el resultado de la funcion
        Return bResultado

    End Function

    ''' <summary>
    ''' DESPLIEGA LOS DATOS DEL ARTICULO CUYO SCANNING FIGURA EN EL CUADRO DE HOMONIMO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __buscar_articulo()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__buscar_articulo()"

        'si el cuadro de "Scanning" esta vacio, salimos del sub
        If Me.txt_scanning.Text.Trim.Equals(String.Empty) Then
            'llamamos a procedimiento de limpieza de controles
            __limpiar_controles()

        Else
            'sino, si los IDs de Ubicacion son validos
            If __bDatos_validos() Then
                'asignamos los datos de la carga de instancia manual
                __oCargaManual = New CargaManualOTD()

                __oCargaManual.nIdInventario = __oApControlador.Inventario_OTD.nId
                __oCargaManual.cColector = __oApControlador.cNombrePc
                __oCargaManual.nIdLocacion = __oLocacion.nId
                __oCargaManual.nNroConteo = Integer.Parse(num_conteo.Value)

                __oCargaManual.nIdSoporte = __oSoporte.nId
                __oCargaManual.nNroSoporte = Integer.Parse(num_nro_soporte.Value)
                __oCargaManual.nIdLetraSoporte = 1
                __oCargaManual.nNivel = Integer.Parse(num_nivel.Value)

                __oCargaManual.nMetro = Integer.Parse(num_metro.Value)
                __oCargaManual.cScanning = txt_scanning.Text.Trim()


                'llamamos al metodo de recuperacion de registro de carga manual
                Dim lResultado As List(Of Object) = __oApControlador.oApModelo.CargasManuales_ADM().lGet_elemento(__oCargaManual)

                'reseteamos el marcador de datos reemplazando
                __bReemplazando = False
                Me.lbl_valor_actual.Text = "0"

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'tomamos la instancia devuelta
                    __oCargaManual = CType(lResultado(1), CargaManualOTD)

                    'marcador de datos reemplazando
                    __bReemplazando = True
                    Me.lbl_valor_actual.Text = __oCargaManual.nCantidad.ToString()

                ElseIf Not lResultado(1).ToString().Equals(ConectorBase.ERROR_NO_HAY_FILAS) Then
                    'sino, mensaje de notificacion
                    __oApControlador.notificar_error(lResultado(1).ToString(), "Recuperando Carga Manual")

                End If


                'establecemos los atributos de la instancia de articulo a buscar
                __oArticulo = New ProductoOTD()
                __oArticulo.cScanning = Me.txt_scanning.Text.Trim()

                'buscamos la instancia del articulo
                lResultado = __oApControlador.oApModelo.Productos_ADM().lGet_elemento(__oArticulo)

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'tomamos la instancia devuelta
                    __oArticulo = CType(lResultado(1), ProductoOTD)

                    'desplegamos los datos del articulo y su existencia registrada actualmente
                    __desplegar_datos()

                ElseIf lResultado(1).ToString().Equals(ConectorBase.ERROR_NO_HAY_FILAS) Then
                    'si no se encontraron registros, mostramos uno con datos especificos
                    __oArticulo = New ProductoOTD()
                    __oArticulo.cDescripcion = NO_ENCONTRADO

                Else
                    'sino, mensaje de notificacion
                    __oApControlador.notificar_error(lResultado(1).ToString(), "Recuperando datos de Artículo")

                    __oArticulo = New ProductoOTD()
                    __oArticulo.cDescripcion = NO_ENCONTRADO

                    'limpiamos los controles
                    __limpiar_controles()

                End If

                'desplegamos los datos del articulo actual
                Me.txt_descripcion.Text = __oArticulo.cDescripcion
                Me.txt_detalle.Text = __oArticulo.cDetalle
                Me.txt_cantidad.Text = __oCargaManual.nCantidad.ToString()

            End If
        End If

    End Sub

    ''' <summary>
    ''' VALIDA LOS IDs DE LOS COMBOS DE UBICACION Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si los Datos son Validos</returns>
    ''' <remarks></remarks>
    Private Function __bDatos_validos() As Boolean

        'resultado por defecto
        Dim bResultado As Boolean = False

        If __oLocacion Is Nothing Or __oSoporte Is Nothing Then
            __oLocacion = CType(cmb_locaciones.SelectedItem, LocacionOTD)
            __oSoporte = CType(cmb_soportes.SelectedItem, SoporteOTD)

        End If

        'si hay una locacion seleccionada
        If __oLocacion.nId > 0 Then
            'si hay un soporte seleccionado
            If __oSoporte.nId > 0 Then
                bResultado = True
            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_stop("Seleccione un Soporte válido!", "Carga manual")
            End If
        Else
            'sino, mensaje de notificacion
            __oApControlador.notificar_stop("Seleccione una Locación válida!", "Carga manual")
        End If

        'devolvemos el resultado del metodo
        Return bResultado

    End Function

    ''' <summary>
    ''' Despliega los datos del articulo actual
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __desplegar_datos()

        'mostramos la descripcion del articulo
        Me.txt_descripcion.Text = __oArticulo.cDescripcion
        Me.txt_detalle.Text = __oArticulo.cDetalle
        Me.txt_cantidad.Text = __oCargaManual.nCantidad.ToString()

        'enfoque a cuadro de cantidad
        Me.txt_cantidad.Focus()

        'habilitamos el boton [Cancelar] y [Guardar]
        Me.cmd_guardar.Enabled = True
        Me.cmd_cancelar.Enabled = True

        Refresh()

    End Sub

    ''' <summary>
    ''' EJECUTA EL PROCEDIMIENTO DE ELIMINACION DE REGISTROS DE LA TABLA DE [CARGA_MANUAL]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __descartar_cargas_manuales()

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'instancia para eliminacion de registros de la tabla
        Dim oCargaManual As New CargaManualOTD()
        oCargaManual.nIdInventario = __oApControlador.Inventario_OTD.nId
        oCargaManual.nIdLocacion = -1
        oCargaManual.nIdSoporte = -1

        'llamamos al metodo de recuperacion de registros
        Dim lResultado As List(Of Object) = __oApControlador.oApModelo.CargasManuales_ADM().lEliminar(oCargaManual)

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then
            'recargamos la grilla
            __cargar_grilla()

        Else
            'sino, mensaje de notificacion
            __oApControlador.notificar_stop(lResultado(1).ToString(), ApControlador.NOMBRE_APLICACION)

        End If

        'limpiamos los controles
        __limpiar_controles()

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' EJECUTA LA TRANSFERENCIA DE LOS REGISTROS DE CONTEOS MANUALES A DETALLES DE CONTEOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __validar_conteos()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__validar_conteos()"

        Try
            'ejecutamos llamamos al metodo de toma de datos de conteos manuales
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.CargasManuales_ADM().lTomar_conteos(__oApControlador.Inventario_OTD _
                                                                                                               , __oApControlador.cNombrePc)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'mensaje de notificacion de exito
                __oApControlador.notificar_exito("Conteos Manuales validados correctamente!", ApControlador.NOMBRE_APLICACION)

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), NOMBRE_METODO)

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Cargas Manuales")

        End Try

        'limpiamos los controles
        __limpiar_controles()

        'llamamos a procedimiento de carga de Datos en Grilla
        Me.__cargar_grilla()


    End Sub

    ''' <summary>
    ''' ELIMINA EL REGISTRO SELECCIONADO DE LA GRILLA DE CARGAS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __eliminar_registro()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__eliminar_registro()"

        'cursor del mouse a "Esperar"
        Cursor.Current = Cursors.WaitCursor

        Try
            'tomamos los atribtutos de la instancia a eliminar
            Dim oCargaEliminar As New CargaManualOTD()

            With grd_cargas

                'obtenemos los IDs de ubicacion del registro actual
                oCargaEliminar.nIdInventario = __oApControlador.Inventario_OTD.nId
                oCargaEliminar.cColector = __oApControlador.cNombrePc
                oCargaEliminar.nIdLocacion = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.ID_LOCACION.cNombre).Value.ToString())
                oCargaEliminar.nNroConteo = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.NRO_CONTEO.cNombre).Value.ToString())

                oCargaEliminar.nIdSoporte = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.ID_SOPORTE.cNombre).Value.ToString())
                oCargaEliminar.nNroSoporte = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.NRO_SOPORTE.cNombre).Value.ToString())

                oCargaEliminar.nIdLetraSoporte = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.ID_LETRA_SOPORTE.cNombre).Value.ToString())
                oCargaEliminar.nNivel = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.NIVEL.cNombre).Value.ToString())
                oCargaEliminar.nMetro = Integer.Parse(.SelectedRows(0).Cells(CargasManualesTBL.METRO.cNombre).Value.ToString())
                oCargaEliminar.cScanning = .SelectedRows(0).Cells(CargasManualesTBL.SCANNING.cNombre).Value.ToString()

            End With



            'llamamos al metodo de elinacion del registro
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.CargasManuales_ADM().lEliminar(oCargaEliminar)

            'si NO se ejecuto correctamente
            If Not lResultado(0).Equals(1) Then
                'mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Eliminar registro de Carga")

            Else
                'si se ejecuto, correctamente, llamamos a procedimiento de carga datos en grilla
                __cargar_grilla()

            End If

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Eliminar registro de Carga")

        End Try

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default


    End Sub

    ''' <summary>
    ''' GUARDA EL REGISTRO ACTUAL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __guardar_conteo()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__guardar_conteo()"

        Try
            'si la cantidad ingresada supera el limite
            If Val(Me.txt_cantidad.Text) > __oApControlador.Inventario_OTD.Configuracion_OTD.nConteoMaximo Then

                If Not __oApControlador.Inventario_OTD.Configuracion_OTD.nConteoMaximo.Equals(0) Then
                    'mensaje de notificacion
                    Dim oRespuesta As DialogResult = MessageBox.Show("La Cantidad Ingresada Supera el Limite Establecido" _
                                        & Chr(13) & "¿Esta seguro de continuar?" _
                                            & Chr(13) & "Limite Establecido : " & __oApControlador.Inventario_OTD.Configuracion_OTD.nConteoMaximo.ToString() _
                                            , "Datos de Conteo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning _
                                            , MessageBoxDefaultButton.Button2)

                    'si la respuesta fue diferente de "SI"
                    If Not oRespuesta = DialogResult.Yes Then
                        'establecemos la cantidad al limite actual
                        Me.txt_cantidad.Text = nCantidad_Maxima_Conteo.ToString()

                        'enfoque al cuadro de "Cantidad"
                        Me.txt_cantidad.Focus()
                        Me.txt_cantidad.SelectAll()

                        'salimos del metodo
                        Exit Sub

                    End If
                End If
            End If

            'asignamos los atributos del conteo
            __oCargaManual.nIdInventario = __oApControlador.Inventario_OTD.nId
            __oCargaManual.cColector = __oApControlador.cNombrePc
            __oCargaManual.cScanning = __oArticulo.cScanning
            __oCargaManual.nCantidad = Val(Me.txt_cantidad.Text)
            __oCargaManual.nIdUsuario = __oApControlador.Usuario_OTD.nId

            'variable para resultados
            Dim lResultado As New List(Of Object)

            'si se esta reemplazando
            If __bReemplazando Then
                'llamamos al metodo de ejecucion de actualizacion de registro existente
                lResultado = __oApControlador.oApModelo.CargasManuales_ADM().lActualizar(__oCargaManual)

            Else
                'llamamos al metodo de ejecucion de insercion de registro nuevo
                lResultado = __oApControlador.oApModelo.CargasManuales_ADM().lAgregar(__oCargaManual)

            End If

            'si NO se ejecuto correctamente
            If Not lResultado(0).Equals(1) Then
                'mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Conteo")

            Else
                'llamamos a procedimiento de limpieza de cuadros de texto
                __limpiar_controles()

                'llamamos a procedimiento de Carga de Datos en Grilla
                __cargar_grilla()

                Refresh()

            End If

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Grabar conteo")

        End Try

        'reseteamos el marcador de datos reemplazando
        __bReemplazando = False

    End Sub

    ''' <summary>
    ''' LIMPIA LOS CONTROLES UTILIZADOS PARA CARGA DE DATOS DE CONTEO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __limpiar_controles()
        'limpiamos los cuadros de texto
        Me.txt_scanning.Text = String.Empty
        Me.txt_descripcion.Text = String.Empty
        Me.txt_cantidad.Text = String.Empty
        Me.txt_detalle.Text = String.Empty

        'deshabilitamos el boton [Guardar]
        Me.cmd_guardar.Enabled = False

        'deshabilitamos el boton [Cancelar]
        Me.cmd_cancelar.Enabled = False

        'enfoque a cuadro de "Scanning"
        Me.txt_scanning.Focus()

        'la etiqueta de cantidad actual
        Me.lbl_valor_actual.Text = "0"

        'las instancia de datos
        __oArticulo = New ProductoOTD()
        __oCargaManual = New CargaManualOTD()

        'los marcadores de reemplazo
        __bReemplazando = False
        Me.lbl_valor_actual.Text = "0"

    End Sub


#End Region


#Region "EVENTOS"

    ''' <summary>
    ''' CUANDO SE PRESIONA UNA TECLA ESTANDO EN EL CUADRO DE "Scanning"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_scanning_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_scanning.KeyDown
        'si la tecla es [Enter]
        If e.KeyCode = Keys.Enter Then

            'cursor del mouse a "Esperar..."
            Cursor.Current = Cursors.WaitCursor

            'llamamos a procedimiento de busqueda del articulo
            __buscar_articulo()

            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Guardar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_guardar.Click

        'si no hay campos vacios
        If Me.txt_scanning.Text.Trim().Length > 0 And Me.txt_descripcion.Text.Trim().Length > 0 Then

            'si la cantidad anterior es diferente de cero
            If Val(Me.lbl_valor_actual.Text) > 0 Then
                'mostramos cuadro de dialogo y guardamos la respuesta del usuario
                Dim oRespuesta As Object = MessageBox.Show("¿Desea agregar esta Cantidad a la Cargada Antes?", "Modificar Cantidad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                'evaluamos la respuesta del usuario
                Select Case oRespuesta

                    Case Windows.Forms.DialogResult.Yes
                        'si los valores a sumar son numericos
                        If IsNumeric(Me.txt_cantidad.Text) And IsNumeric(Me.lbl_valor_actual.Text) Then
                            'calculamos la nueva cantidad
                            Me.txt_cantidad.Text = (Val(Me.txt_cantidad.Text) + Val(Me.lbl_valor_actual.Text)).ToString()

                        Else
                            'sino, mensaje de notificacion
                            __oApControlador.notificar_stop("El Valor Actual o el Anterior parecen no ser Numericos", "Datos de Conteo")

                            'salimos del sub
                            Exit Sub

                        End If

                    Case Windows.Forms.DialogResult.No
                        'volvemos apedir una segunda confirmacion
                        oRespuesta = MessageBox.Show("¿Esta Seguro de Reemplazar la Cantidad Antes Cargada?", "Modificar Cantidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                        'si la respuesta fue "SI"
                        If oRespuesta = DialogResult.Yes Then
                            'si los valores a sumar son numericos
                            If IsNumeric(Me.txt_cantidad.Text) And IsNumeric(Me.lbl_valor_actual.Text) Then
                                'dejamos la cantidad tal cual se cargo
                                Me.txt_cantidad.Text = Me.txt_cantidad.Text
                            Else
                                'sino, mensaje de notificacion
                                __oApControlador.notificar_stop("El Valor Actual o el Anterior parecen no ser Numericos", "Datos de Conteo")

                                'salimos del sub
                                Exit Sub

                            End If
                        Else
                            'si fue "NO", salimos del sub
                            Exit Sub

                        End If

                    Case Else
                        'en otro caso, salimos del sub
                        Exit Sub

                End Select

            End If

            'si la cantidad ingresada mayor o igual de cero
            If Val(Me.txt_cantidad.Text) >= 0 Then

                Cursor.Current = Cursors.WaitCursor

                'si los IDs de ubicacion son validos, ejecutamos el procedimiento de guardado
                If __bDatos_validos() Then Me.__guardar_conteo()

                Cursor.Current = Cursors.Default

            Else
                'si la cantidad es cero, mensaje de notificacion
                __oApControlador.notificar_stop("El Valor ingresado debe ser Positivo o Cero!", "Datos de Conteo")

                'enfoque a cuadro de "Cantidad"
                Me.txt_cantidad.Focus()
                Me.txt_cantidad.SelectAll()

            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO EL CUADRO DE "Cantidad" RECIBE EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_cantidad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cantidad.GotFocus

        'si la descripcion del articulo contiene la frase "NO ENCONTRADO" o no hay descripcion
        If Me.txt_descripcion.Text.Equals(NO_ENCONTRADO) _
            Or Me.txt_descripcion.Text.Length.Equals(0) Then
            'devolvemos el enfoque al cuadro de "Scanning"
            Me.txt_scanning.Focus()
            Me.txt_scanning.SelectAll()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE PRESIONA UNA TECLA EN EL CUADRO DE "Cantidad"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_cantidad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_cantidad.KeyDown
        'si la tecla es [Enter]
        If e.KeyCode = Keys.Enter Then
            'pasamos el enfoque al Boton [Guardar]
            Me.cmd_guardar.Focus()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO EL CUADRO DE "Cantidad" PIERDE EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_cantidad_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cantidad.LostFocus
        'si el valor es un numero
        If IsNumeric(Me.txt_cantidad.Text) Then
            'si no se aceptan valores decimales o el articulo no es pesable
            If Not __oApControlador.Inventario_OTD.Configuracion_OTD.bConDecimales _
            Or Not __oArticulo.bPesable Then
                'convertimos el valor a entero
                Me.txt_cantidad.Text = Int(Val(Me.txt_cantidad.Text)).ToString()

            End If
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL CONTENIDO DEL CUADRO DE "Cantidad"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_cantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_cantidad.TextChanged

        'si la longitud de la cantidad ingresada es mayor a 0
        If Not Me.txt_cantidad.Text.Length.Equals(0) Then
            'deshabilitamos el Boton [Guardar]
            Me.cmd_guardar.Enabled = True

            'si el valor no es numerico
            If Not IsNumeric(Me.txt_cantidad.Text) Then
                'mensaje de notificacion
                __oApControlador.notificar_stop("El Valor Ingresado debe ser Numérico!", "Datos de Conteo")

                'borramos el contenido del cuadro de "Cantidad"
                Me.txt_cantidad.Text = String.Empty

                'deshabilitamos el Boton [Guardar]
                Me.cmd_guardar.Enabled = False

                'enfoque al cuadro de cantidad
                Me.txt_cantidad.Focus()

            End If

        Else
            'deshabilitamos el Boton [Guardar]
            Me.cmd_guardar.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'cerramos el formulario y lo liberamos de la memoria
        Me.Close()
        Me.Dispose()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Limpiar Conteo]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_limpiar.Click
        'pedimos la confirmacion del usuario
        If MessageBox.Show("Esta a punto de Eliminar los Conteo Cargados.." _
                                    & Chr(13) & "¿Esta seguro de Continuar?", "Datos de Conteo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            'si la respuesta fue si, llamamos a procedimiento de limpieza de Cargas Manuales
            __descartar_cargas_manuales()

            'llamamos a procedimiento de limpieza de controles
            __limpiar_controles()

            'llamamos a procedimiento de carga de datos en grilla
            __cargar_grilla()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN LA CABECERA DE UNA FILA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_cargas_RowHeaderMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd_cargas.RowHeaderMouseDoubleClick

        'pedimos confirmacion al usuario
        If MessageBox.Show("Se Eliminará el Registro Seleccionado" _
                                & Chr(13) & "¿Esta Seguro de Continuar?", "Datos de Conteo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue "SI", cursor del mouse a "Esperar..."
            Cursor.Current = Cursors.WaitCursor

            'llamamos a procedimiento de elimnacion de registro
            __eliminar_registro()

            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default
        End If
    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Validar Conteo]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_validar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_validar.Click
        'pedimos confirmacion del usuario
        If MessageBox.Show("Está a punto de Ingresar los Conteos al Inventario Actual.." _
                                & Chr(13) & "¿Esta Seguro de Continuar?", "Datos de Conteo" _
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2 _
                                ) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue "SI", cursor del mouse a "Esperar"
            Cursor.Current = Cursors.WaitCursor

            'llamamos al procedimiento de toma de Cargas Manuales
            __validar_conteos()

            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Locacion"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_locaciones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_locaciones.SelectedIndexChanged
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'tomamos el item seleccionado
        __oLocacion = CType(cmb_locaciones.SelectedItem, LocacionOTD)

        'establecemos los demas combos a su primer valor
        Me.cmb_soportes.SelectedIndex = 0
        'Me.cmb_letras.Text = Me.cmb_letras.Items(0)

        'y los controles numericos a su valor minimo
        Me.num_nivel.Value = Me.num_nivel.Minimum
        Me.num_metro.Value = Me.num_metro.Minimum
        Me.num_nro_soporte.Value = Me.num_nro_soporte.Minimum

        'limpiamos los controles
        __limpiar_controles()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Soporte"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_soportes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_soportes.SelectedIndexChanged
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'tomamos el item seleccionado
        __oSoporte = CType(cmb_soportes.SelectedItem, SoporteOTD)

        'establecemos los demas combos a su primer valor
        'Me.cmb_letras.Text = Me.cmb_letras.Items(0)

        'y los controles numericos a su valor minimo
        Me.num_nivel.Value = Me.num_nivel.Minimum
        Me.num_metro.Value = Me.num_metro.Minimum
        Me.num_nro_soporte.Value = Me.num_nro_soporte.Minimum



        'si el soporte es subdivisible
        If __oSoporte.bSubDivisible Then
            'desbloqueamos los controles numericos de "Nivel" y "Metro"
            Me.num_nivel.Enabled = True
            Me.num_metro.Enabled = True
            'Me.num_nro_soporte.Enabled = True

        Else
            'sino, los bloqueamos
            Me.num_nivel.Enabled = False
            Me.num_metro.Enabled = False
            'Me.num_nro_soporte.Enabled = False

        End If

        'limpiamos los controles
        __limpiar_controles()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Letra"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_letras_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_letras.SelectedIndexChanged
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'establecemos los controles numericos a su valor minimo
        Me.num_nivel.Value = Me.num_nivel.Minimum
        Me.num_metro.Value = Me.num_metro.Minimum

        'limpiamos los controles
        __limpiar_controles()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancelar.Click
        'llamamos al procedimiento de limpieza de controles
        __limpiar_controles()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL CONTROL DE "Metro"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub num_metro_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles num_metro.ValueChanged
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'establecemos el valor del control numerico de Nivel a su valor minimo
        Me.num_nivel.Value = Me.num_nivel.Minimum

        'limpiamos los controles
        __limpiar_controles()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL CONTROL DE "Nivel"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub num_nivel_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles num_nivel.ValueChanged
        'limpiamos los controles
        __limpiar_controles()
    End Sub




#End Region



    

End Class