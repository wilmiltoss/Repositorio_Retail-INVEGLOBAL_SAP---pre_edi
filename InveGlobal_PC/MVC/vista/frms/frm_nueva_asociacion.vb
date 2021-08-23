Public Class frm_nueva_asociacion
#Region "DECLARACION_VARIABLES"
    Public cTabla_Unaria As String = String.Empty
    Public nID_Unario As Integer = 0

#End Region

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_nueva_asociacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        Try
            'ensamblamos la Sentencia SQL
            cSentenciaSQL = "EXECUTE [SP_CARGAR_TABLA_ASOCIACION_NUEVA] @tabla_unaria= '" & cTabla_Unaria & "',@id_unario = " & nID_Unario.ToString()

            'llamamos a funcion de Ejecucion de Consulta SQL y el resultado lo establecemos como origen de la grilla
            Me.grd_varios.DataSource = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'las columnas "ID" y  "DESCRIPCION" las establecemos como solo de lectura
            Me.grd_varios.Columns("ID").ReadOnly = True
            Me.grd_varios.Columns("DESCRIPCION").ReadOnly = True

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Cargar Grilla de Formulario de Asociaciones Soportes - Tipos de Locales : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error intentando Cargar Grilla de Datos " _
                                & Chr(13) & Ex.Message, "Asociacion Soportes - Locales", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancelar.Click
        'establecemos el Resultado del Dialogo de este formulario a "Cancel"
        Me.DialogResult = Windows.Forms.DialogResult.Cancel

        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Guardar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_guardar.Click
        'ejecutamos la funcion de guardado de datos
        If Me.bGuardar_Datos Then
            'si se ejecuto correctamente, llamamos a procedimimiento de confirmacion de asociaciones
            Me.pConfirmar_Asociaciones()

            'establecemos el Resultado del Dialogo de este formulario a "OK"
            Me.DialogResult = Windows.Forms.DialogResult.OK

            'cerramos este formulario
            Me.Close()

        Else
            'sino, mensaje de notificacion
            MessageBox.Show("No se Pudieron Guardar las Asociaciones Realizadas", "Asociacion de Soportes - Locales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

    End Sub

    ''' <summary>
    ''' GUARDA LOS CAMBIOS REALIZADOS EN LA GRILLA EN LA TABLA [TMP_NUEVA_ASOCIACION] Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Guardaron Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bGuardar_Datos() As Boolean
        'variables a utilizar
        Dim bAsociado As Boolean = False
        Dim cID As String = String.Empty

        Try
            'recorremos las filas de la grilla
            For Each oFilas As DataGridViewRow In Me.grd_varios.Rows
                'tomamos el valor del campo "ID"
                cID = oFilas.Cells("ID").Value.ToString()

                'tomamos el valor del campo "ASOCIAR"
                bAsociado = oFilas.Cells("ASOCIAR").Value

                'si el item esta asociado
                If bAsociado Then
                    'ensamblamos la Sentencia SQL
                    cSentenciaSQL = "EXECUTE [SP_MARCAR_NUEVA_ASOCIACION] @id_unario = " & cID

                    'llamamos a procedimiento de Ejecucion de Sentencia SQL
                    If Not principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
                        'si no se ejecuto correctamente, evento a LOG
                        'principal.pInfo_a_Log("No se pudo Marcar la Nueva Asociacion Soporte a Tipo Local : " & cSentenciaSQL)

                    End If

                End If

            Next oFilas

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Asociar Soporte a Tipo de Local")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return True

        End Try

    End Function

    ''' <summary>
    ''' CONFIRMA LAS NUEVAS ASOCIACIONES REALIZADAS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pConfirmar_Asociaciones()
        'ensamblamos la Sentencia SQL
        cSentenciaSQL = "EXECUTE [SP_CONFIRMAR_ASOCIACIONES]"

        'ejecutamos la Sentencia SQL
        If Not principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("No se Confirmaron las Nuevas Asociaciones entre Soportes y Tipos de Locales")

        End If

    End Sub

End Class