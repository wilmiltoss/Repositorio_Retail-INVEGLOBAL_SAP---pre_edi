Imports System.Windows.Forms

Public Class FrmSectoresCubiertos


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmSectoresCubiertos"

    Private __oApControlador As ApControlador

    Private __dtSectores As DataTable



#End Region



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
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

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'recuperamos la lista de sectores cubiertos por el inventario
        Dim lResultado As List(Of Object) = __lRecuperar_sectores()

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

        'si NO se ejecuto correctamente
        If Not lResultado(0).Equals(1) Then
            'mensaje de notificacion
            __oApControlador.notificar_error(lResultado(1), "Datos de Sectores Cubiertos")

        End If


    End Sub


#End Region


#Region "METODOS"

    ''' <summary>
    ''' Recupera la lista de sectores cubiertos por el inventario
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lRecuperar_sectores() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lRecuperar_sectores()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado! "})

        Try
            'ensamblamos la sentencia de filtrado de registros de sectores
            Dim cFiltroWhere As String = "INNER JOIN {0} ON {1}.{2} = {0}.{3} WHERE {0}.{4} = {5}"
            cFiltroWhere = String.Format(cFiltroWhere _
                                         , InventariosSectoresTBL.NOMBRE_TABLA _
                                         , SectoresTBL.NOMBRE_TABLA _
                                         , SectoresTBL.ID.cNombre _
                                         , InventariosSectoresTBL.ID_SECTOR.cNombre _
                                         , InventariosSectoresTBL.ID_INVENTARIO.cNombre _
                                         , __oApControlador.Inventario_OTD.nId _
                                         )

            'llamamos al metodo de recuperacion de datos
            lResultado = __oApControlador.oApModelo.Sectores_ADM().lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos solo la tabla devuelta
                __oApControlador.dtTablaAuxiliar = CType(lResultado(2), DataTable)

                'lo asignamos como origen de datos de la grilla
                Me.grd_sectores.DataSource = __oApControlador.dtTablaAuxiliar

            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    ''' <summary>
    ''' Ejecuta la busqueda del sector en la tabla original
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __buscar_sector()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__buscar_sector()"

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            'reseteamos el origen de datos de la grilla
            Me.grd_sectores.DataSource = Nothing

            'si hay un valor ingresado en el cuadro de texto
            If Me.txt_buscado.Text.Trim().Length > 0 Then
                'cadena de filtrado
                Dim cFiltro As String = "{0} LIKE '{1}%'"

                'si el valor es numerico
                If IsNumeric(Me.txt_buscado.Text) Then
                    cFiltro = String.Format(cFiltro, SectoresTBL.ID.cNombre, Me.txt_buscado.Text)
                Else
                    cFiltro = String.Format(cFiltro, SectoresTBL.DESCRIPCION.cNombre, Me.txt_buscado.Text)
                End If

                'clonamos la estructura de la tabla original 
                __dtSectores = __oApControlador.dtTablaAuxiliar.Clone()

                'filtramos los registros
                For Each dr As DataRow In __oApControlador.dtTablaAuxiliar.Select(cFiltro)
                    'pasamos a la tabla auxiliar
                    __dtSectores.ImportRow(dr)
                Next

                'establecemos el origen de datos de la grilla
                Me.grd_sectores.DataSource = __dtSectores

            Else
                'sino, establecemos el origen de datos de la grilla a la tabla original
                Me.grd_sectores.DataSource = __oApControlador.dtTablaAuxiliar

            End If

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Datos de Sectores Cubiertos")

        End Try

        'refrescamos el form
        Refresh()

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

#End Region


#Region "EVENTOS"

    ''' <summary>
    ''' Cuando se presiona una tecla en el campo de valor de busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_buscado_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_buscado.KeyDown

        'si la tecla presionada es [ENTER]
        If e.KeyCode.Equals(Keys.Enter) Then
            'llamamos al metodo de filtrado de tabla de sectores
            __buscar_sector()
        End If

    End Sub

#End Region

    
End Class
