Public Class FrmEntradasColectores


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmEntradasColectores"

    Private __oApControlador As ApControlador

    Private bolDatosAceptados As Boolean = False
    Private __dtTablaAuxiliar As DataTable

#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
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
        __oApControlador.log().Escribir("Se carga el Formulario : " & Me.Name)

        'asignamos el origen de datos de la grilla 
        Me.grd_lecturas.DataSource = __oApControlador.dtTablaAuxiliar

        'obtenemos la cantidad de registros que se despliegan
        Me.lbl_estado.Text = Me.grd_lecturas.Rows.Count.ToString() & " Lecturas Registradas..."

        'llamamos al procedimiento de carga de combo de locaciones
        __cargar_combo_campos()
        __dtTablaAuxiliar = New DataTable()


    End Sub


#End Region


#Region "METODOS"


    ''' <summary>
    ''' CARGA LOS ITEMS EN EL COMBO DE "Campos de Filtrado"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_combo_campos()

        'recorremos las columnas de la tabla
        For Each dcCol As DataColumn In __oApControlador.dtTablaAuxiliar.Columns
            'tomamos el nombre de la columna y lo agregamos al combo  de campos
            Me.cmb_campos.Items.Add(dcCol.ColumnName)

        Next

        'insertamos uno por defecto
        Me.cmb_campos.Items.Insert(0, "TODOS")

        'seleccionamos el primer elemento
        Me.cmb_campos.SelectedIndex = 0

    End Sub

    ''' <summary>
    ''' ELIMINA LOS REGISTROS DE LECTURAS REALIZADAS
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lDescartar_lecturas() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lDescartar_lecturas()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'si los datos ya fueron aceptados anteriormente
        If bolDatosAceptados Then
            'establecemos el resultado de la funcion
            lResultado(1) = "Los Datos ya Fueron Ingresados como Parte del Inventario Antes de esta Operación"

        Else
            'sino, pedimos la confirmacion del usuario
            If MessageBox.Show("Esta seguro de descartar estos datos?", "Datos de Lecturas" _
                               , MessageBoxButtons.YesNo, MessageBoxIcon.Question _
                               , MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                'cambiamos el cursor del mouse a "Esperar..."
                Cursor.Current = Cursors.WaitCursor

                'creamos una instancia auxiliar de Entrada de Colector
                Dim oEntrada As New EntradaColectoraOTD()
                oEntrada.nIdInventario = __oApControlador.Inventario_OTD.nId
                oEntrada.cColectora = __oApControlador.cNombreColector

                'llamamos al metodo de eliminacion de registros
                lResultado = __oApControlador.oApModelo.EntradasColectorasADM().lEliminar(oEntrada)

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'cambiamos el valor de la variable de control de toma de datos
                    bolDatosAceptados = True

                    'reseteamos la tabla en el controlador
                    __oApControlador.dtTablaAuxiliar = New DataTable()

                    'mensaje de notificacion
                    __oApControlador.notificar_exito("Datos de Lecturas Descartados Correctamente" _
                                                     , "Datos de Lecturas")

                Else
                    'sino, mensaje de notificacion
                    __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Lecturas")

                End If

                'cambiamos el cursor del mouse a "Normal"
                Cursor.Current = Cursors.Default

            End If

        End If

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' TRANSFIERE LOS REGISTROS DE LA TABLA [ENTRADA_COLECTORAS] A [DETALLES_CONTEOS]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __lTomar_datos_lecturas()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lTomar_datos_lecturas()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'pedimos la confirmacion del usuario
        If MessageBox.Show("Los Datos Visualizados Pasaran a Formar Parte del Inventario!" _
                            & Chr(13) & "¿Esta Seguro de Continuar?", "Toma de Datos de Conteos", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question _
                            , MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

            Dim nModoPrueba As Integer = 0

            'evaluamos si son datos de prueba o no
            If Me.chkModoPrueba.Checked Then
                nModoPrueba = 1
            End If

            'ejecutamos la llamada al metodo de toma de datos de lecturas
            lResultado = __oApControlador.oApModelo.EntradasColectorasADM().lTomar_entradas(__oApControlador.Inventario_OTD _
                                                                                            , __oApControlador.cNombreColector _
                                                                                            , nModoPrueba)

            'si se ejecuto correctamente 
            If lResultado(0).Equals(1) Then
                'cambiamos el valor de la variable de control de acciones sobre registros
                bolDatosAceptados = True

                'mensaje de notificacion
                __oApControlador.notificar_exito("Los Datos fueron Aceptados Correctamente!" _
                                                 , "Toma de Datos de Conteos")

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Toma de Datos de Conteos")

            End If

        End If

    End Sub


#End Region


#Region "EVENTOS"


    ''' <summary>
    ''' cuando se esta cerrando el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_entradas_colectores_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        'si no se han tomado acciones en cuanto a los datos de lecturas
        If Not bolDatosAceptados Then
            'pedimos confirmacion del usuario
            If MessageBox.Show("Si sale ahora los datos seran descartados.." _
                                    & Chr(13) & "¿Esta seguro de continuar?" _
                                    , "Datos de Lecturas", _
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question _
                                    , MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                'llamamos a funcion de eliminacion de datos de lecturas
                __lDescartar_lecturas()

            Else
                'si la respuesta fue otra, cancelamos el proceso de de salida
                e.Cancel = True

            End If
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click

        'cerramos el form
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Descartar Datos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_descartar_datos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_descartar_datos.Click
        'llamamos a funcion de eliminacion de registros de lecturas
        Dim lResultado As List(Of Object) = __lDescartar_lecturas()

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then
            'cerramos este form
            Me.Close()

        Else
            'sino, mensaje de notificacion
            __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Lecturas")

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Tomar Datos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_tomar_datos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_tomar_datos.Click
        'si la grilla tiene filas
        If Not Me.grd_lecturas.RowCount.Equals(0) Then
            'evaluamos que la grilla contenga el campo de Identificador de usuario
            If Me.grd_lecturas.Columns.Contains(EntradasColectorasTBL.ID_USUARIO.cNombre) Then

                'si esta seleccionado el checkbox de Datos de Prueba
                If Me.chkModoPrueba.Checked Then
                    'consultamos si esta seguro de continuar
                    If MessageBox.Show("Los Datos de tomaran en Modo de Prueba y " _
                                       & "no se considerarán para los Totales Finales " _
                                       & "\n ¿Esta seguro de continuar?", "MODO PRUEBA" _
                                       , MessageBoxButtons.YesNo, MessageBoxIcon.Warning _
                                       , MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then

                        'si la respuesta fue diferente de SI, salimos del proceso
                        Return

                    End If
                End If

                'tomamos el indice de la columna
                Dim nIdx As Integer = Me.grd_lecturas.Columns(EntradasColectorasTBL.ID_USUARIO.cNombre).Index

                'evaluamos que el nombre del usuario que colecto los datos sea valido
                If Val(Me.grd_lecturas.Rows(0).Cells(nIdx).Value) > 0 Then
                    'llamamos a procedimiento de toma de datos
                    __lTomar_datos_lecturas()

                    'cerramos este formulario
                    Me.Close()
                    Me.Dispose()
                Else
                    'sino, mensaje de estado
                    __oApControlador.notificar_stop("El Usuario que Tomo los Datos no se registró con su ID !...", "Datos de Usuario")

                End If

            Else
                'si no se encontro la columna, mensaje de notificacion
                __oApControlador.notificar_stop("No se encontró la columna de ID de Usuario a validar!!", "Datos de Usuario")

            End If




        End If

    End Sub

    ''' <summary>
    ''' Cuando cambia el valor del cuadro de "Valor Buscado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_valor.TextChanged

        'si NO hay filas por filtrar, salimos del metodo
        If __oApControlador.dtTablaAuxiliar.Rows.Count.Equals(0) Then Exit Sub

        'reseteamos el origen de datos de la grilla
        Me.grd_lecturas.DataSource = Nothing

        'si hay un campo de filtrado seleccionado
        If Not Me.cmb_campos.SelectedItem = "TODOS" And Not Me.txt_valor.Text.Trim().Equals(String.Empty) Then
            'ensamblamos la sentencia de filtrado de la tabla
            Dim cFiltroWhere As String = "{0} = {1}"

            'evaluamos el tipo de datos del campo seleccionado
            If __oApControlador.dtTablaAuxiliar.Columns(Me.cmb_campos.SelectedItem).DataType.Name = "String" Then
                'cambiamos la condicion de filtrado
                cFiltroWhere = "{0} LIKE '{1}%'"
            End If

            'aplicamos la condicion
            cFiltroWhere = String.Format(cFiltroWhere, Me.cmb_campos.SelectedItem, Me.txt_valor.Text.Trim())
            __dtTablaAuxiliar = __oApControlador.dtTablaAuxiliar.Clone()

            'tomamos las filas devueltas
            For Each dr As DataRow In __oApControlador.dtTablaAuxiliar.Select(cFiltroWhere)
                'y las pasamos a la tabla auxiliar
                __dtTablaAuxiliar.ImportRow(dr)

            Next

            'establecemos el origen de datos de la grilla
            Me.grd_lecturas.DataSource = __dtTablaAuxiliar

        ElseIf Me.cmb_campos.SelectedItem = "TODOS" Then
            'sino, mostramos toda la tabla original
            Me.grd_lecturas.DataSource = __oApControlador.dtTablaAuxiliar

        End If

        'refrecamos el form
        Refresh()

    End Sub

    ''' <summary>
    ''' CUANDO CAMBIA EL ESTADO DEL CHECKBOX DE [Datos de Prueba]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkModoPrueba_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkModoPrueba.CheckedChanged
        'si esta seleccionado el checkbox de Datos de Prueba
        If chkModoPrueba.Checked Then
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Else
            Me.BackColor = System.Drawing.SystemColors.Control
        End If
    End Sub


#End Region



End Class