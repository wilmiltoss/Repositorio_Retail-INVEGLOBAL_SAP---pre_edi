Imports System.Windows.Forms

Public Class FrmModificarConteo

#Region "ATRIBUTOS"

    Private bHayCambios As Boolean = False
    Private __oApControlador As ApControlador

#End Region


#Region "COSNTRUCTORES"

    Public Sub New(ByVal oApControladorParam As ApControlador)

        'tomamos la instancia del controlador
        __oApControlador = oApControladorParam

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'llamamos al metodo de inicializacion de los demas componentes


    End Sub


    Private Sub __Inicializar_componentes()

        'evento a LOG
        __oApControlador.log().Escribir("Se carga el Formulario : " & Me.Name)

    End Sub

#End Region

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'si hubo cambios en los valores de conteos
        If Me.bHayCambios Then
            'pedimos confirmacion del usuario, si la respuesta es diferente a "SI", salimos del sub
            If Not MessageBox.Show("Se Guardarán los Cambios Realizados.." _
                                    & Chr(13) & "¿Esta Seguro de Continuar?" _
                                    , "Modificación de Detalle de Conteo" _
                                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question _
                                    , MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then Exit Sub

            Try
                'ensamblamos la sentencia SQL
                cSentenciaSQL = "EXECUTE [SP_ACTUALIZAR_CANTIDAD_DETALLE_CONTEO] " _
                                                      & " @id_locacion = " & Me.lbl_id_locacion.Text _
                                                      & " ,@id_inventario = " & __oApControlador.Inventario_OTD.nId.ToString() _
                                                      & " ,@id_soporte = " & Me.lbl_id_soporte.Text _
                                                      & " ,@nro_soporte = " & Me.lbl_nro_soporte.Text _
                                                      & " ,@id_letra_soporte = " & Me.lbl_id_letra_soporte.Text _
                                                      & " ,@nivel = " & Me.lbl_nivel.Text _
                                                      & " ,@metro = " & Me.lbl_metro.Text _
                                                      & " ,@scanning = '" & Me.lbl_scanning.Text & "'" _
                                                      & " ,@conteo = " & Me.lbl_conteo.Text _
                                                      & " ,@cantidad = '" & Me.txt_conteo_1.Text.Replace(",", ".") & "'" _
                                                      & ", @modo_prueba = 0;"

                'llamamos a funcion de ejecucion de Sentencia SQL
                Dim lResultado As List(Of Object) = __oApControlador.oApModelo.Get_conector().lEjecutar_sentencia(cSentenciaSQL)

                If lResultado(0).Equals(1) Then
                    'si se ejecuto correctamente, establecemos el resultado del dialogo a "OK"
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK

                Else
                    'sino establecemos el resultado del dialogo a "Cancel"
                    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

                    'evento a LOG
                    __oApControlador.notificar_error(lResultado(1).ToString(), "Modificación de Detalle de Conteo")

                End If

            Catch Ex As Exception
                'en caso de error, evento a LOG
                __oApControlador.notificar_error(ex.Message, "Modificación de Detalle de Conteo")

                'establecemos el resultado del dialogo a "Cancel"
                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

            End Try

        End If

        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        'establecemos el resultado del dialogo
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO EL CUADRO DE "Conteo 1" PIERDE EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_conteo_1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_conteo_1.LostFocus
        'si el cuadro de texto esta vacio
        If Me.txt_conteo_1.Text.Trim.Length.Equals(0) Then
            'le asignamos el valor por defecto "0"
            Me.txt_conteo_1.Text = "0"

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL VALOR DEL CUADRO DE "Conteo 1"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_conteo_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_conteo_1.TextChanged
        'cambiamos el valor de la variable de control de cambios
        Me.bHayCambios = True

        'si el valor no es numerico
        If Not IsNumeric(Me.txt_conteo_1.Text) Then
            'mensaje de notificacion
            __oApControlador.notificar_stop("El Valor ingresado no es un Número", "Validación de Datos")

            'establecemos a 0 (cero) el valor del combo
            Me.txt_conteo_1.Text = "0"

        End If

    End Sub

End Class
