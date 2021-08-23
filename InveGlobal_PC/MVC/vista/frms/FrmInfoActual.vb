Public Class FrmInfoActual


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmInfoActual"

    Private __oApControlador As ApControlador


#End Region



#Region "CONSTRUTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControlador As ApControlador)

        'tomamos la instancia del controlador de la aplicacion
        __oApControlador = oApControlador

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

        'llamamos al metodo de carga de datos
        __cargar_info()

    End Sub


#End Region


#Region "METODOS"

    ''' <summary>
    ''' Ejecuta la recuperacion de datos del inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_info()

        Dim NOMBRE_METODO = NOMBRE_CLASE & ".__cargar_info()"

        Try
            'establecemos la consulta Sql
            Dim cConsultaSql = String.Format("EXEC [SP_OBTENER_INFO_INVENTARIO] @id_inventario = {0}", __oApControlador.Inventario_OTD.nId)

            'evento a log
            __oApControlador.log().Escribir("Se carga el Formulario " & Me.Name)

            'llamamos al metodo de ejecucion de consultas 
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.Get_conector().lEjecutar_consulta(cConsultaSql)

            'si se ejecuto sin problemas
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                __oApControlador.dtTablaAuxiliar = CType(lResultado(1), DataTable)

                'agregamos los demas valores que queremos desplegar
                With __oApControlador.dtTablaAuxiliar.Rows
                    Dim arrNuevaFila() As String = {"DESTINO PARA CSV MAESTRO" _
                                                    , Config.Get_instancia().get_valor(Config.UNIDAD_MONTADA)}
                    .Add(arrNuevaFila)

                    arrNuevaFila.SetValue("     SERVIDOR BD", 0)
                    arrNuevaFila.SetValue(Config.Get_instancia().get_valor(Config.SERVIDOR_DB), 1)
                    .Add(arrNuevaFila)

                    arrNuevaFila.SetValue("CONEXION A DATOS", 0)
                    arrNuevaFila.SetValue(__oApControlador.oApModelo.Get_conector().ToString(), 1)
                    .Add(arrNuevaFila)

                    arrNuevaFila.SetValue("     UNIDAD MONTADA", 0)
                    arrNuevaFila.SetValue(Config.Get_instancia().get_valor(Config.UNIDAD_MONTADA), 1)
                    .Add(arrNuevaFila)

                End With

                'asignamos el resultado como origen de datos de la grilla
                Me.grd_inventario.DataSource = __oApControlador.dtTablaAuxiliar

                'ajustamos el ancho de las columnas
                'Me.grd_inventario.col
                Me.grd_inventario.AutoResizeColumns()

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Info de Inventario Actual")

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Info de Inventario Actual")

        End Try

        'refrescamos el formulario
        Refresh()

    End Sub

#End Region


#Region "EVENTOS"

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_aceptar.Click

        'cerramos el formulario
        Me.Close()
        Me.Dispose()

    End Sub

#End Region

    

End Class