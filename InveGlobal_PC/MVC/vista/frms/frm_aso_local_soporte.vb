Public Class frm_aso_local_soporte

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'cerramos el formulario
        Me.Close()
        Me.Dispose()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Nuevo Soporte]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_nuevo_soporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_nuevo_soporte.Click
        'desplegamos el formulario de ABM de Soportes en modo dialogo
        frm_abm_soportes.ShowDialog()

        'si el resultado del dialogo del formulario es "OK"
        If frm_abm_soportes.DialogResult = Windows.Forms.DialogResult.OK Then
            'si el Resultado del Dialogo es "OK"
            If frm_abm_soportes.DialogResult = Windows.Forms.DialogResult.OK Then
                'llamamos al procedimiento de carga de items a Combo de Soportes
                Me.pCargar_Combo_Soportes()

            End If

        End If

        'liberamos de la memoria el formulario
        frm_abm_soportes.Dispose()

    End Sub

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_aso_local_soporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        'llamamos a procedimientos de carga de datos a controles
        Me.pCargar_Combo_Soportes()
        Me.pCargar_Combo_Locales()
        Me.pCargar_Grilla_Soportes()
        Me.pCargar_Grilla_Tipo_Locales()

    End Sub

    ''' <summary>
    ''' CARGA LOS ITEMS EN EL CONTROL COMBOBOX DE "Soportes"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Combo_Soportes()
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT CAST([ID_SOPORTE] AS VARCHAR) + ' - ' + [DESCRIPCION] FROM [VW_SOPORTES]"

        'llamamos a funcion de carga de valores a combo
        'principal.pCargar_Combo_Valores(Me.cmb_soportes, cSentenciaSQL)

    End Sub

    ''' <summary>
    ''' CARGA LOS ITEMS EN EL CONTROL COMBOBOX DE "Tipos de Locales"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Combo_Locales()
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT CAST([ID_TIPO_LOCAL] AS VARCHAR) + ' - ' + [DESCRIPCION] FROM [VW_TIPO_LOCALES]"

        'llamamos a funcion de carga de valores a combo
        'principal.pCargar_Combo_Valores(Me.cmb_tipo_locales, cSentenciaSQL)

    End Sub

    ''' <summary>
    ''' CARGA LA GRILLA DE "Tipos de Locales Asociados"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Grilla_Tipo_Locales()
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT [ID_TIPO_LOCAL],[TIPO_LOCAL] FROM [VW_ASO_TIPO_LOCAL_SOPORTE] WHERE [ID_SOPORTE] = " & Me.cmb_soportes.Text.Substring(0, Me.cmb_soportes.Text.IndexOf("-"))

        'ejecutamos la funcion de Consulta SQL y establecemos el resultado como Origen de Datos de la grilla
        Me.grd_locales.DataSource = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

    End Sub

    ''' <summary>
    ''' CARGA LA GRILLA DE "Soportes Asociados"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Grilla_Soportes()
        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT [ID_SOPORTE],[SOPORTE] FROM [VW_ASO_TIPO_LOCAL_SOPORTE] WHERE [ID_TIPO_LOCAL] = " & Me.cmb_tipo_locales.Text.Substring(0, Me.cmb_tipo_locales.Text.IndexOf("-"))

        'ejecutamos la funcion de Consulta SQL y establecemos el resultado como Origen de Datos de la grilla
        Me.grd_soportes.DataSource = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Soportes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_soportes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_soportes.SelectedIndexChanged
        'llamamos a procedimeinto de carga de datos en grilla de tipos de locales
        Me.pCargar_Grilla_Tipo_Locales()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Tipos de Locales"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_tipo_locales_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_tipo_locales.SelectedIndexChanged
        'llamamos a procedimiento de carga de datos en grilla de soportes
        Me.pCargar_Grilla_Soportes()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Asociar con Tipo Local]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_asociar_local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_asociar_local.Click
        'pasamos los parametros de carga al formulario de "Nueva Asociacion"
        frm_nueva_asociacion.nID_Unario = Me.cmb_soportes.Text.Substring(0, Me.cmb_soportes.Text.IndexOf("-"))
        frm_nueva_asociacion.cTabla_Unaria = "SOPORTES"
        frm_nueva_asociacion.lbl_uno.Text = Me.cmb_soportes.Text

        'mostramos el formulario en forma de dialogo
        frm_nueva_asociacion.ShowDialog()

        'si el Resultado del Dialogo es "OK"
        If frm_nueva_asociacion.DialogResult = Windows.Forms.DialogResult.OK Then
            'recargamos la grilla de "Tipos de Locales"
            Me.pCargar_Grilla_Tipo_Locales()

        End If

        'lo liberamos de la memoria
        frm_nueva_asociacion.Dispose()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Asociar con Soporte]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_asociar_soporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_asociar_soporte.Click
        'pasamos los parametros de carga al formulario de "Nueva Asociacion"
        frm_nueva_asociacion.nID_Unario = Me.cmb_tipo_locales.Text.Substring(0, Me.cmb_tipo_locales.Text.IndexOf("-"))
        frm_nueva_asociacion.cTabla_Unaria = "TIPOS_DE_LOCALES"
        frm_nueva_asociacion.lbl_uno.Text = Me.cmb_tipo_locales.Text

        'mostramos el formulario en forma de dialogo
        frm_nueva_asociacion.ShowDialog()

        'si el Resultado del Dialogo es "OK"
        If frm_nueva_asociacion.DialogResult = Windows.Forms.DialogResult.OK Then
            'recargamos la grilla de "Soportes"
            Me.pCargar_Grilla_Soportes()

        End If

        'lo liberamos de la memoria
        frm_nueva_asociacion.Dispose()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click
        'llamamos al evento Click del Boton [Salir]
        Me.cmd_salir_Click(sender, e)

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Nuevo Soporte"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_nuevo_soporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_nuevo_soporte.Click
        'llamamos al evento Click del boton [Nuevo Soporte]
        Me.cmd_nuevo_soporte_Click(sender, e)

    End Sub

End Class