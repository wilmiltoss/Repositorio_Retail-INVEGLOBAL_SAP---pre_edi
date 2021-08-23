Public Class frm_entradas_colectores

#Region "DECLARACION_VARIABLES"
    'variables a utilizar
    Private bolDatosAceptados As Boolean = False

    Public dtbTablaEntradas As DataTable = New DataTable()

#End Region

    ''' <summary>
    ''' cuando se esta cerrando el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_entradas_colectores_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'si no se han tomado acciones en cuanto a los datos de lecturas
        If Not bolDatosAceptados Then
            'llamamos a funcion de eliminacion de datos de lecturas
            If Me.bDescartarLecturas() Then
                'si se procedio a descartalas, cerramos este formulario
                Me.Dispose()

            Else
                'sinio, cancelamos el evento de cierre del formulario
                e.Cancel = True

            End If

        Else
            'si se tomaron las acciones, directamente cerramos este formulario
            Me.Dispose()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_entradas_colectores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        'asignamos el origen de datos de la grilla 
        Me.grd_lecturas.DataSource = dtbTablaEntradas

        'obtenemos la cantidad de registros que se despliegan
        Me.lbl_estado.Text = dtbTablaEntradas.Rows.Count.ToString() & " Lecturas Registradas..."

        'llamamos al procedimiento de carga de combo de locaciones
        Me.pCargar_combo_campos()


    End Sub

    ''' <summary>
    ''' CARGA LOS ITEMS EN EL COMBO DE "Campos de Filtrado"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_combo_campos()

        'recorremos las columnas de la tabla
        For Each dcCol As DataColumn In dtbTablaEntradas.Columns
            'tomamos el nombre de la columna y lo agregamos al combo  de campos
            Me.cmb_campos.Items.Add(dcCol.ColumnName)
        Next

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL TAMANO DEL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_entradas_colectores_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'cambiamos tambien el tamano de la grilla
        Me.grd_lecturas.Height = Me.Height - (Me.grd_lecturas.Location.Y + 68)
        Me.grd_lecturas.Width = Me.Width - (Me.grd_lecturas.Location.X + 20)

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click
        'si los datos fueron aceptados
        If bolDatosAceptados Then
            'cerramos el formulario
            Me.Close()

        Else
            'sino, llamamos a funcion de eliminacion de registros de lecturas
            If Me.bDescartarLecturas() Then
                'si se descataron los datos, cerramos el formulario
                Me.Close()

            End If
        End If

    End Sub

    ''' <summary>
    ''' ELIMINA LOS REGISTROS DE LECTURAS REALIZADAS
    ''' </summary>
    ''' <remarks></remarks>
    Private Function bDescartarLecturas() As Boolean
        'si los datos ya fueron aceptados anteriormente
        If bolDatosAceptados Then
            'mensaje de notificacion
            MessageBox.Show("Los Datos ya Fueron Ingresados como Parte del Inventario Antes de esta Operacion", "Datos de Conteos", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'devolvemos el resultado de la funcion
            Return False

        End If

        'pedimos confirmacion del usuario, si la respuesta es diferente a "SI", devolvemos el resultado de la funcion
        If Not MessageBox.Show("Si sale ahora los datos seran descartados.." _
                                & Chr(13) & "¿Esta seguro de continuar?", "Datos de Lecturas", _
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then Return False

        'cambiamos el cursor del mouse a "Esperar"
        Cursor.Current = Cursors.WaitCursor

        'ensamblamos la Sentencia SQL
        cSentenciaSQL = "EXECUTE [SP_ELIMINAR_ENTRADAS_COLECTORAS] @id_inventario= " & cID_Inventario

        'llamamos a funcion de Ejecucion de Sentencia SQL
        If principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
            'cambiamos el valor de la variable de control de toma de datos
            bolDatosAceptados = True

            'mensaje de notificacion
            MessageBox.Show("Datos de Lecturas Descartados Correctamente", "Datos de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            'si no se ejecuto correctamente, mensaje de notificacion
            MessageBox.Show("Los Datos de Lecturas no se Descartaron Totalmente", "Datos de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

        'cambiamos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

        'devolvemos el resultado de la funcion
        Return True

    End Function

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Descartar Datos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_descartar_datos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_descartar_datos.Click
        'llamamos a funcion de eliminacion de registros de lecturas
        If Me.bDescartarLecturas() Then
            'si se descartaron los datos, cerramos este formulario
            Me.Close()
            Me.Dispose()

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
            'llamamos a procedimiento de toma de datos
            Me.proTomar_Datos_Lecturas()

            'cerramos este formulario
            Me.Close()
            Me.Dispose()

        End If

    End Sub

    ''' <summary>
    ''' TRANSFIERE LOS REGISTROS DE LA TABLA [ENTRADA_COLECTORAS] A [DETALLES_CONTEOS]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub proTomar_Datos_Lecturas()
        'si el valor del combo de locaciones NO es valido (No contiene el Caracter separador de valores "-")
        'If Me.cmb_locaciones.Text.IndexOf("-") < 0 Then
        '   'mensaje de notificacion
        '    MessageBox.Show("La Locación Asignada no es Válida!", "Toma de Datos de Conteos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        '   'enfoque a combo de "Locaciones"
        '    Me.cmb_locaciones.Focus()

        '   'salimos del sub
        '    Exit Sub

        'End If

        'pedimos la confirmacion del usuario
        If MessageBox.Show("Los Datos Visualizados Pasaran a Formar Parte del Inventario!" _
                            & Chr(13) & "¿Esta Seguro de Continuar?", "Toma de Datos de Conteos", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta es "SI", ensamblamos la Sentencia SQL
            'cSentenciaSQL = "EXECUTE [SP_TOMAR_ENTRADAS] @id_inventario= " & cID_Inventario
            cSentenciaSQL = "EXECUTE [SP_TOMAR_ENTRADAS] @id_inventario=" & cID_Inventario & ";"

            'establecemos el cursor del mouse a "Esperar"
            Cursor.Current = Cursors.WaitCursor

            'variables a utilizar
            Dim intResultado As Integer = -1

            'intentamos ejecutar el Procedimiento
            Try
                'llamamos a funcion de Ejecucion de Consulta SQL
                dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

                'recorremos las filas del DataTable
                For Each drwFila In dtDataTableAuxiliar.Rows
                    'tomamos el valor del unico campo devuelto
                    intResultado = drwFila.Item(0)

                    'salimos del bucle
                    Exit For

                Next

                'establecemos el cursor del mouse a "Normal"
                Cursor.Current = Cursors.Default

                'si se ejecuto correctamente 
                If intResultado.Equals(0) Then
                    'cambiamos el valor de la variable de control de acciones sobre registros
                    bolDatosAceptados = True

                    'mensaje de notificacion
                    MessageBox.Show("Los Datos fueron Aceptados Correctamente!", "Toma de Datos de Conteos", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'evento a LOG
                    'principal.pInfo_a_Log("Datos de Conteos ingresados a Inventario")

                Else
                    'si no se ejcuto correctamente, mensaje de notificacion
                    MessageBox.Show("Los Datos No se Pudieron Cargar!", "Toma de Datos de Conteos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'evento a LOG
                    'principal.pInfo_a_Log("Datos de Conteos NO ingresados a Inventario")

                End If

            Catch Ex As Exception
                'en caso de error, establecemos el cursor del mouse a "Normal"
                Cursor.Current = Cursors.Default

                'mensaje de notificacion
                MessageBox.Show(Ex.Message, "Toma de Datos de Conteos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                'evento a LOG
                'principal.pInfo_a_Log("Error Intentando Ejecutar Toma de Entradas de Colectores : " & cSentenciaSQL)
                'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            End Try

        End If

    End Sub

    ''' <summary>
    ''' Cuando cambia el valor del cuadro de "Valor Buscado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_valor.TextChanged

        'si no hay un campo de filtrado seleccionado
        If Me.cmb_campos.SelectedItem Is Nothing Then
            'salimos del metodo
            Return
        Else
            'si lo hay, si el valor buscado esta vacio
            If Me.txt_valor.Text.Trim().Equals(String.Empty) Then
                'no hay filtro a aplicar
                Me.grd_lecturas.DataSource = dtbTablaEntradas.Select(Me.cmb_campos.SelectedItem.ToString() + " = " + Me.txt_valor.Text)
            End If

            'si lo hay, establecemos la condicion de filtrado

        End If

    End Sub



End Class