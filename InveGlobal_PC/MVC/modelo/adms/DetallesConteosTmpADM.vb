Imports CdgPersistencia.ClasesBases

Public Class DetallesConteosTmpADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "DetallesConteosTmpADM"

    Private __oApModelo As ApModelo

    Public cNombreTabla_C1 As String
    Public cNombreTabla_C2 As String
    Public cNombreTabla_C3 As String

    Public cNombreVistaDetalles As String


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()

        cNombreTabla_C1 = String.Empty
        cNombreTabla_C2 = String.Empty
        cNombreTabla_C3 = String.Empty
        cNombreVistaDetalles = String.Empty


    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lActualizar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elemento(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)
        Throw New NotImplementedException()
    End Function

#End Region


#Region "ESPECIFICOS"

    ''' <summary>
    ''' Ejecuta la creacion de las tablas de detalles de conteos temporales
    ''' </summary>
    ''' <param name="oInventarioParam">Instancia de inventario actual</param>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lCrear_tablas(ByVal oInventarioParam As InventarioOTD) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lCrear_tablas()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'creamos las tres llamadas para cada conteo
            For i As Integer = 1 To 3
                'ejecutamos los procedimientos de creacion de las tablas temporales
                lResultado = _oConector.lEjecutar_consulta(String.Format(DetallesConteosTmpTBL.SP_CREAR_TABLA_TMP _
                                                                        , oInventarioParam.nId _
                                                                        , i))

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'tomamos la primera fila de la tabla devuelta
                    Dim drResultado As DataRow = CType(lResultado(1), DataTable).Rows(0)

                    If i = 1 Then
                        'si el valor devuelto es 1 en el primer campo, tomamos el nombre de la tabla devuelta
                        If drResultado(0).Equals(1) Then cNombreTabla_C1 = drResultado(1).ToString()
                    ElseIf i = 2 Then
                        'si el valor devuelto es 1 en el primer campo, tomamos el nombre de la tabla devuelta
                        If drResultado(0).Equals(1) Then cNombreTabla_C2 = drResultado(1).ToString()
                    ElseIf i = 3 Then
                        'si el valor devuelto es 1 en el primer campo, tomamos el nombre de la tabla devuelta
                        If drResultado(0).Equals(1) Then cNombreTabla_C3 = drResultado(1).ToString()
                    End If

                Else
                    'sino, salimos del bucle
                    Exit For
                End If
            Next

            'si no hubo probelmas
            If lResultado(0).Equals(1) Then
                'creamos la vista correspondiente
                lResultado = _oConector.lEjecutar_consulta(String.Format(VwDetallesConteosTBL.SP_CREAR_VISTA _
                                                                         , oInventarioParam.nId _
                                                                         , cNombreTabla_C1 _
                                                                         , cNombreTabla_C2 _
                                                                         , cNombreTabla_C3 _
                                                                         ))

                'si se ejecuto correctamente
                If lResultado(0).Equals(1) Then
                    'tomamos la primera fila de la tabla devuelta
                    Dim drResultado As DataRow = CType(lResultado(1), DataTable).Rows(0)

                    'si no hubo problemas, tomamos el nombre de la vista creada 
                    If drResultado(0).Equals(1) Then
                        cNombreVistaDetalles = drResultado(1).ToString()

                        'creamos la instancia del administrador de la vista
                        __oApModelo.VwDetallesConteos_ADM = New VwDetallesConteosADM(__oApModelo, cNombreVistaDetalles)

                    End If
                End If
            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

#End Region

End Class


