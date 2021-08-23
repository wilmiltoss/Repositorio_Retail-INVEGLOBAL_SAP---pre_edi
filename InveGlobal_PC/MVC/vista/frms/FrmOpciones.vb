Public Class FrmOpciones

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmOpciones"

    Private __oApControlador As ApControlador


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControlador As ApControlador)

        'tomamos la instancia del controlador
        __oApControlador = oApControlador

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'inicializamos los demasn componentes
        __Inicializar_componentes()


    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion de los demas componentes de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        'enlazamos los datos de configuracion actual a sus controles correspondientes
        With __oApControlador.Inventario_OTD
            Me.num_cantidad_maxima.DataBindings.Add("Value", .Configuracion_OTD, "conteoMaximo")
            Me.chk_pesables.DataBindings.Add("Checked", .Configuracion_OTD, "conPesables")
            Me.chk_decimales.DataBindings.Add("Checked", .Configuracion_OTD, "conDecimales")

        End With
        
    End Sub

#End Region

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_aceptar.Click

        'si el inventario NO esta cerrado aun
        If Not __oApControlador.Inventario_OTD.bCerrado Then

            'sino, cursor del mouse a "Esperar..."
            Cursor.Current = Cursors.WaitCursor

            'actualizamos los atributos de la configuracion del inventario
            Me.num_cantidad_maxima.DataBindings(0).WriteValue()
            Me.chk_pesables.DataBindings(0).WriteValue()
            Me.chk_decimales.DataBindings(0).WriteValue()

            __oApControlador.Inventario_OTD.Configuracion_OTD.nId = __oApControlador.Inventario_OTD.nId

            'llamamos al metodo de actualizacion del registro de configuracion
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.Configuraciones_ADM().lActualizar( _
                                                                                    __oApControlador.Inventario_OTD.Configuracion_OTD)

            'si se ejcuto correctamente
            If lResultado(0).Equals(1) Then
                'establecemos el Resultado del Dialogo a "OK"
                Me.DialogResult = Windows.Forms.DialogResult.OK

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString() _
                                                 , "Opciones del Inventario")

                'establecemos el Resultado del Dialogo a "Cancel"
                Me.DialogResult = Windows.Forms.DialogResult.Cancel

            End If

            'si se guardaron, cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End If

        'cerramos este form
        Close()

    End Sub


End Class