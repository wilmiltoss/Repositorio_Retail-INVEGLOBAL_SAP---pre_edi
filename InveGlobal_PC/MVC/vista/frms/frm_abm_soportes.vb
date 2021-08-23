Public Class frm_abm_soportes

#Region "DECLARACION_VARIABLES"
    Dim cSentenciaCargaListaSoportes As String = String.Empty
    Dim bEstadoActualizando As Boolean = False
    Dim bHayCambios As Boolean = False

#End Region

    ''' <summary>
    ''' CUANDO SE CIERRA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_abm_soportes_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'si hay cambios realizados
        If bHayCambios Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If

        'si se estan actualizando datos
        If bEstadoActualizando Then
            'mensaje de notificacion
            If Not MessageBox.Show("Si Cierra la Ventana se Cancelara la Actualización de Datos" _
                                    & "¿Esta Seguro de Continuar?", "Datos de Soportes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                'cancelamos el cierre
                e.Cancel = True

            End If
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_abm_soportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        'llamamos a procedimiento de carga de datos a grilla
        pCargar_Grilla()

        'llamamos a procedimiento de carga de datos en controles de ABM
        pMostrar_Primer_Elemento()

    End Sub

    ''' <summary>
    ''' CARGA LOS DATOS DE SOPORTES EN LA GRILLA DE VISUALIZACION
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Grilla()
        'ensamblamos la Consulta SQL
        cSentenciaCargaListaSoportes = "SELECT * FROM [VW_SOPORTES_ABM]"

        Try
            'llamamos a funcion de ejecucion de Consulta SQL y la establecemos como origen de datos de la grilla
            Me.grd_soportes.DataSource = principal.dtEjecutar_ConsultaSQL(cSentenciaCargaListaSoportes)

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Cargar Grilla ABM de Soportes : " & cSentenciaCargaListaSoportes)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error obteniendo Lista de Soportes.." _
                                    & Chr(13) & Ex.Message, "Datos de Soportes", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'salimos del sub
            Exit Sub

        End Try

    End Sub

    ''' <summary>
    ''' CARGA LOS DATOS DEL PRIMER REGISTRO DE LA GRILLA EN LOS CONTROLES DE ABM
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pMostrar_Primer_Elemento()
        Try
            'si la grilla de soportes tiene al menos un elemento
            If Me.grd_soportes.RowCount > 0 Then
                'copiamos los datos del primer elemento de la grilla a los controles de ABM
                Me.txt_id.Text = Me.grd_soportes.Rows(0).Cells.Item("ID_SOPORTE").Value.ToString()
                Me.txt_descripcion.Text = Me.grd_soportes.Rows(0).Cells.Item("DESCRIPCION").Value.ToString()
                Me.chk_subdivisible.Checked = Me.grd_soportes.Rows(0).Cells.Item("SUBDIVISIBLE").Value

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Transferir primer elemento de la Grilla de Soportes a Controles de ABM")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error obteniendo Lista de Soportes.." _
                                    & Chr(13) & Ex.Message, "Datos de Soportes", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK SOBRE EL CONTENIDO DE UNA CELDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_soportes_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_soportes.CellContentClick
        Try
            'si la grilla de soportes tiene al menos un elemento
            If Me.grd_soportes.RowCount > 0 Then
                'copiamos los datos del primer elemento de la grilla a los controles de ABM
                Me.txt_id.Text = Me.grd_soportes.Rows(Me.grd_soportes.SelectedCells(0).RowIndex).Cells.Item("ID_SOPORTE").Value.ToString()
                Me.txt_descripcion.Text = Me.grd_soportes.Rows(Me.grd_soportes.SelectedCells(0).RowIndex).Cells.Item("DESCRIPCION").Value.ToString()
                Me.chk_subdivisible.Checked = Me.grd_soportes.Rows(Me.grd_soportes.SelectedCells(0).RowIndex).Cells.Item("SUBDIVISIBLE").Value

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Transferir elemento Seleccionado de la Grilla de Soportes a Controles de ABM")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO EL CUADRO DE "ID Soporte" GANA EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_id_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_id.GotFocus
        'lo rechazamos pasandolo al siguiente control
        SendKeys.Send("{TAB}")

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Agregar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_agregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_agregar.Click
        'cambiamos a estado actual de actualizacion
        bEstadoActualizando = True

        'reseteamos los valores de los controles de ABM
        Me.txt_descripcion.Text = String.Empty
        Me.chk_subdivisible.Checked = False

        'llamamos a procedimiento de habilitacion de controles para actualizacion
        pHabilitar_Controles_UP()

        'llamamos a procedimiento de generacion de nuevo ID
        Me.txt_id.Text = nNuevo_ID().ToString()

        'enfoque a cuadrio de "Descripcion"
        Me.txt_descripcion.Focus()
        Me.txt_descripcion.SelectAll()

    End Sub

    ''' <summary>
    ''' HABILITA LOS CONTROLES NECESARIOS PARA UNA ACTUALIZACION DE DATOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pHabilitar_Controles_UP()
        'habilitamos los controles necesarios para una actualizacion
        Me.cmd_cancelar.Enabled = True
        Me.cmd_guardar.Enabled = True

        'los cuadros de texto
        Me.txt_id.Enabled = True
        Me.txt_descripcion.Enabled = True
        Me.chk_subdivisible.Enabled = True

        'deshabilitamos los controles innecesarios
        Me.cmd_agregar.Enabled = False
        Me.cmd_modificar.Enabled = False
        Me.cmd_salir.Enabled = False

    End Sub

    ''' <summary>
    ''' GENERA UN NUEVO ID PARA EL SOPORTE Y LO DEVUELVE COMO RESULTADO
    ''' </summary>
    ''' <returns>Devuelve un Entero con el Resultado de la Consulta</returns>
    ''' <remarks></remarks>
    Private Function nNuevo_ID() As Integer
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "Select ISNULL(MAX([ID_SOPORTE]), 0) + 1 FROM [SOPORTES]"

        Try
            'llamamos a funcion de ejecucion de Consulta SQL
            dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'si se devolvio al menos una Fila
            If dtDataTableAuxiliar.Rows.Count > 0 Then
                'obtenemos el valor del campo de la primera fila y lo devolvemos como resultado de la funcion
                Return dtDataTableAuxiliar.Rows(0).Item(0)

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando obtener Nuevo ID para Soporte : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error :" & Ex.Message)

            'devolvemos el cero, como resultado de la funcion
            Return 0

        End Try

    End Function

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancelar.Click
        'limpiamos el contenido de los controles
        Me.txt_id.Text = String.Empty
        Me.txt_descripcion.Text = String.Empty

        'cambiamos el estado de actualizacion de datos
        bEstadoActualizando = False

        'llamamos a procedimiento de Deshabilitacion de Controles
        pDeshabilitar_Controles_UP()

        'llamamos a procedimiento de carga de datos en controles de ABM
        pMostrar_Primer_Elemento()

    End Sub

    ''' <summary>
    ''' DESHABILITA LOS CONTROLES NECESARIOS PARA UNA ACTUALIZACION DE DATOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pDeshabilitar_Controles_UP()
        'deshabilitamos los controles necesarios para una actualizacion
        Me.cmd_cancelar.Enabled = False
        Me.cmd_guardar.Enabled = False

        'los cuadros de texto
        Me.txt_id.Enabled = False
        Me.txt_descripcion.Enabled = False
        Me.chk_subdivisible.Enabled = False

        'habilitamos los controles necesarios
        Me.cmd_agregar.Enabled = True
        Me.cmd_modificar.Enabled = True
        Me.cmd_salir.Enabled = True

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Modificar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_modificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_modificar.Click
        'llamamos a procedimiento de habilitacion de controles para actualizacion de datos
        pHabilitar_Controles_UP()

        'establecemos el estado actual
        bEstadoActualizando = True

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'cerramos el formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Guardar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_guardar.Click
        'evaluamos la longitud de la descripcion del soporte
        Select Case Me.txt_descripcion.Text.Trim.Length
            Case 0
                'si es 0, mensaje de notificacion
                MessageBox.Show("Debe ingresar Todos los Datos!", "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                'enfoque a cuadro de "Descripcion"
                Me.txt_descripcion.Focus()

                'salimos del sub
                Exit Sub

            Case Is < 3
                'si tiene menos de tres caracteres, mensaje de notificacion
                MessageBox.Show("La Descripcion debe tener al menos 3 Caracteres", "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                'enfoque a cuadro de "Descripcion"
                Me.txt_descripcion.Focus()
                Me.txt_descripcion.SelectAll()

                'salimos del sub
                Exit Sub

            Case Else
                'sino, es que todo, llamamos a procedimeinto de Guardado de Datos del Soporte
                pGuardar_Datos_Soporte()

                'llamamos a procedimiento de carga de datos a grilla
                pCargar_Grilla()

                'cambiamos el estado de actualizacion de datos
                bEstadoActualizando = False

                'establecemos la variable de control de cambios
                bHayCambios = True

                'llamamos a procedimiento de deshabilitado de controles de ABM
                pDeshabilitar_Controles_UP()

        End Select



    End Sub

    ''' <summary>
    ''' GRABA LOS DATOS DEL SOPORTE EN LA TABLA CORRESPONDIENTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pGuardar_Datos_Soporte()
        'ensamblamos la Sentencia SQL
        cSentenciaSQL = "EXECUTE [SP_UPSERT_SOPORTES] @id_soporte= " & Me.txt_id.Text & ", @descripcion= '" & Me.txt_descripcion.Text.Trim() & "', @subdivisible= '" & Me.chk_subdivisible.Checked.ToString() & "'"

        Try
            'variables a utilizar
            Dim nResultado As Integer = -1

            'llamamos a funcion de Ejecucion de CONSULTA SQL 
            dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'obtenemos el valor devuelto
            nResultado = dtDataTableAuxiliar.Rows(0).Item(0)

            'evaluamos el resultado devuelto
            Select Case nResultado
                Case 1
                    'si devuelve uno, es que ya habia uno con la misma descripcion
                    MessageBox.Show("Ya Existe un Soporte con la misma Descripcion", "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                Case -1
                    'si devuelve uno negativo, es que se produjo un error
                    MessageBox.Show("Ocurrio un Error al Guardar Datos del Soporte", "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Case 0
                    'si devuelve cero, es que se guardo sin problemas
                    MessageBox.Show("Datos del Soporte Guardados Correctamente", "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Case Else
                    'si devuelve otro valor, no esta previsto
                    MessageBox.Show("Valor devuelto no Previsto por SP de Insercion de Datos!", "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Select

            'salimos del sub
            Exit Sub

        Catch ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Insertar Nuevo Soporte : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error intentando Guardar Datos de Soporte" _
                                & Chr(13) & ex.Message, "Datos de Soporte", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub

End Class