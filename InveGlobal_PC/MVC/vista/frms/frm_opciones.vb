Public Class frm_opciones

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_opciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'llamamos aprocedimiento de carga de valores de controles
        Me.pCargar_Configuracion_Actual()

        'si el conteo es mayor a 1
        If Val(FrmPrincipal.lbl_nro_conteo.Text) > 1 Then
            'deshabilitamos los controles para evitar cambios
            Me.txt_cantidad_maxima_conteo.Enabled = False
            Me.chk_decimales.Enabled = False
            Me.chk_pesables.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CARGA LOS VALORES DE CONFIGURACION ACTUAL DE ESTE INVENTARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Configuracion_Actual()
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "EXECUTE [SP_OBTENER_CONFIGURACION_ACTUAL] @id_inventario = " & cID_Inventario

        Try
            'llamamos a funcion de ejecucion de Consulta SQL y recorremos las filas devueltas
            For Each drwFila In principal.dtEjecutar_ConsultaSQL(cSentenciaSQL).Rows
                'obtenemos los valores de los campos devueltos
                Me.txt_cantidad_maxima_conteo.Text = drwFila.Item("CONTEO_MAXIMO").ToString()
                Me.chk_decimales.Checked = drwFila.Item("CONTEO_CON_DECIMALES")
                Me.chk_pesables.Checked = drwFila.Item("PESABLES_INCLUIDOS")

                'salimos del bucle
                Exit For

            Next drwFila

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando recuperar Valores de Configuraciones : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Ocurrió un Error Recuperando los Valores de Configuración" _
                                & Chr(13) & Ex.Message, "Opciones del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO EL CUADRO DE CANTIDAD PIERDE EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_cantidad_maxima_conteo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cantidad_maxima_conteo.LostFocus
        'si el contenido es numerico
        If IsNumeric(Me.txt_cantidad_maxima_conteo.Text) Then
            'si el numero es negativo
            If Val(Me.txt_cantidad_maxima_conteo.Text) < 0 Then
                'mensaje de notificacion
                MessageBox.Show("El Número ingresado debe Positivo", "Opciones del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                'enfoque al cuadro de cantidad
                Me.txt_cantidad_maxima_conteo.Focus()
                Me.txt_cantidad_maxima_conteo.SelectAll()

            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL CONTENIDO DEL CUADRO DE TEXTO DE "Cantidad Maxima de Conteo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_cantidad_maxima_conteo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_cantidad_maxima_conteo.TextChanged
        'si el cuadro de texto esta vacio
        If Me.txt_cantidad_maxima_conteo.Text.Trim.Length.Equals(0) Then
            'establecemos el contenido a cero
            Me.txt_cantidad_maxima_conteo.Text = "0"

        End If

        'si el contenido no es numerico
        If Not IsNumeric(Me.txt_cantidad_maxima_conteo.Text) Then
            'mensaje de notificacion
            MessageBox.Show("El Valor ingresado debe ser un Número!", "Opciones del inventario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'borramos el contenido del cuadro de cantidad
            Me.txt_cantidad_maxima_conteo.Text = 0

            'y le pasamos el enfoque
            Me.txt_cantidad_maxima_conteo.Focus()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_aceptar.Click
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'establecemos el Resultado del Dialogo a "Cancel"
            Me.DialogResult = Windows.Forms.DialogResult.Cancel

            'salimos del sub
            Exit Sub

        End If

        'llamamos a funcion de guardado de opciones de configuracion

        '//////////////////////////////////////////////////////////////////////////////
        'If Me.bGuardar_Configuracion() Then
        '//////////////////////////////////////////////////////////////////////////////

        If True Then
            'si se guardaron, cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'establecemos el Resultado del Dialogo a "OK"
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Else
            'sino se guardaron, cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'mensaje de notificacion
            MessageBox.Show("No se guardaron Correctamente los Cambios!...", "Opciones del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'establecemos el Resultado del Dialogo a "Cancel"
            Me.DialogResult = Windows.Forms.DialogResult.Cancel

        End If

    End Sub

    
End Class