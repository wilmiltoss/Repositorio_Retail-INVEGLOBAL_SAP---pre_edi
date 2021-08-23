Public Class frm_soportes
#Region "DECLARACIONES_LOCALES"
    Dim bHubo_Cambios As Boolean = False
#End Region

    ''' <summary>
    ''' CUANDO EL FORMULARIO SE ESTA CERRANDO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_soportes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'si hubo cambios
        If bHubo_Cambios Then
            'establecemos el resultado del Dialogo a "OK"
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Else
            'establecemos el resultado del Dialogo a "Cancel"
            Me.DialogResult = Windows.Forms.DialogResult.Cancel

        End If
    End Sub

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_soportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        'reseteamos la variable de control de cambios
        bHubo_Cambios = False

        'llamamos a procedimiento de Carga de Combo de Soportes
        'Me.pCargar_Lista_Soportes()

        'llamamos a procedimiento de Carga de Datos a Grilla de Soportes
        Me.pCargar_Grilla_Soportes()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE COMBO DE SOPORTES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Lista_Soportes()
        'ensamblamos la Consulta SQL
        'cSentenciaSQL = "EXECUTE [SP_OBTENER_SOPORTES_EXCLUIDOS] @id_inventario= " & cID_Inventario

        'llamamos a Procedimiento de Carga de Items en Lista
        'principal.pCargar_Lista_Valores(Me.lst_soportes, cSentenciaSQL)

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE LA GRILLA DE SOPORTES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Grilla_Soportes()
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "EXECUTE [SP_OBTENER_SOPORTES_DEL_INVENTARIO] @id_inventario = " & cID_Inventario

        'creamos una neva instancia del DataTable
        dtDataTableAuxiliar = New DataTable

        Try
            'llamamos a funcion de Carga del DataTable
            dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'mostramos la cantidad de filas devueltas
            Me.lbl_cantidad_soportes.Text = dtDataTableAuxiliar.Rows.Count & " Soportes Seleccionados"

            'asignamos el DataTable como Origen de Datos de la Grilla
            Me.grd_soportes.DataSource = dtDataTableAuxiliar

        Catch Ex As Exception
            'en caso de error, 'evento a LOG
            'principal.pInfo_a_Log("Error Intentando Cargar la Grilla de Soportes FRM Soportes")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error Intentando Cargar la Grilla de Soportes Seleccionados", "Selección de Soportes", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE INCLUSION DE SOPORTE A INVENTARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pIncluir_Soporte(ByVal strID_Soporte As String)
        'ensamblamos la Sentencia de Inclusion de Soporte Nuevo a Inventario
        cSentenciaSQL = "EXECUTE [SP_AB_SOPORTE_INVENTARIO] " _
                                                          & " @id_inventario = " & cID_Inventario _
                                                          & ", @id_soporte = " & strID_Soporte _
                                                          & ", @operacion = 1 ;"

        'llamamos a funcion de Ejecucion de Sentencia SQL
        If principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
            'si se ejecuto correctamente, llamamos a procedimiento de Carga de Datos a la Grilla de Soportes
            Me.pCargar_Grilla_Soportes()

            'llamamos a procedimiento de Carga de la Lista de "Soportes Disponibles"
            Me.pCargar_Lista_Soportes()

            'seteamos el valor de la variable de Control de Cambios
            bHubo_Cambios = True

            'refrescamos el Formulario
            Me.Refresh()

        Else
            'sino, mensaje de notificacion
            MessageBox.Show("No se pudo Incluir el Soporte Seleccionado!", "Seleccion de Soportes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

            'evento a LOG
            'principal.pInfo_a_Log("Error Intentando Incluir el Soporte " & strID_Soporte)

        End If

        'pasamos el enfoque a la Lista de "Soportes Disponibles"
        'Me.lst_soportes.Focus()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE EXCLUSION DE SOPORTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pExcluir_Soporte(ByVal strID_Soporte As String)
        'ensamblamos la Sentencia de Inclusion de Soporte Nuevo a Inventario
        cSentenciaSQL = "EXECUTE [SP_AB_SOPORTE_INVENTARIO] " _
                                                          & " @id_inventario = " & cID_Inventario _
                                                          & ", @id_soporte = " & strID_Soporte _
                                                          & ", @operacion = 2 ;"

        'llamamos a funcion de Ejecucion de Sentencia SQL
        If principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
            'si se ejecuto correctamente, llamamos a procedimiento de Carga de Datos a la Grilla de Soportes
            Me.pCargar_Grilla_Soportes()

            'luego volvemos a recargar la Lista de Soportes Disponibles
            Me.pCargar_Lista_Soportes()

            'seteamos el valor de la variable de Control de Cambios
            bHubo_Cambios = True

            'refrescamos el Formulario
            Me.Refresh()

        Else
            'sino, mensaje de notificacion
            MessageBox.Show("No se pudo Excluir el Soporte Seleccionado!", "Seleccion de Soportes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

            'evento a LOG
            'principal.pInfo_a_Log("Error Intentando Incluir el Soporte " & strID_Soporte)

        End If

        'pasamos el enfoque a la Grilla de "Soportes Seleccionados"
        Me.grd_soportes.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE DOBLE CLICK SOBRE EL CONTENIDO DE UNA CELDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_soportes_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_soportes.CellContentDoubleClick
        'llamamos a procedimiento de Exclusion de Soporte
        'Me.pExcluir_Soporte(Me.grd_soportes.Rows(Me.grd_soportes.SelectedCells.Item(0).RowIndex).Cells("ID_SOPORTE").Value.ToString())

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE INCLUSION EN INVENTARIO DE TODOS LOS SOPORTES DISPONIBLES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pIncluir_Todos_Los_Soportes()
        'si no hay items en la Lista, salimos del procedimiento
        'If Me.lst_soportes.Items.Count.Equals(0) Then Exit Sub

        'variables a utilizar
        'Dim strItemDeLista As String = String.Empty

        'recorremos todos los Items de la Lista de "Soportes Disponibles"
        'For i As Integer = 1 To Me.lst_soportes.Items.Count
        'tomamos el Valor del Item
        '    strItemDeLista = Me.lst_soportes.Items(0)

        'si el item es Valido
        'If strItemDeLista.IndexOf("-") > -1 Then
        'llamamos a procedimiento de Inclusion de Soporte
        '    Me.pIncluir_Soporte(strItemDeLista.Substring(0, strItemDeLista.IndexOf("-")))

        'End If
        'Next

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE EXCLUSION DE INVENTARIO DE TODOS LOS SOPORTES DISPONIBLES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pExcluir_Todos_Los_Soportes()
        'si no hay filas en la Grila, salimos del procedimiento
        If Me.grd_soportes.Rows.Count.Equals(0) Then Exit Sub

        'recorremos todas las Filas de la Grilla de "Soportes Seleccionados"
        For i As Integer = 1 To Me.grd_soportes.Rows.Count
            'llamamos a procedimiento de Exclusion de Soporte
            Me.pExcluir_Soporte(Me.grd_soportes.Rows(Me.grd_soportes.Rows.Count - 1).Cells("ID_SOPORTE").Value.ToString())

        Next

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click
        'cerramos el formulario
        Me.Close()

    End Sub

End Class