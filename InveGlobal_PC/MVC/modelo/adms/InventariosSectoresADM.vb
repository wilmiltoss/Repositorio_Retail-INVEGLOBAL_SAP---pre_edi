Imports CdgPersistencia.ClasesBases

Public Class InventariosSectoresADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "InventariosSectoresADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New InventariosSectoresTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oInveSectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'casteamos el parametro a su tipo
            Dim oInveSector As InventarioSectorOTD = CType(oInveSectorParam, InventarioSectorOTD)

            'si es identificador de todos los sectores
            If oInveSector.cIdSector.Equals(SectorOTD.TODOS.ToString()) Then
                'ejecutamos el procedimiento de asignacion masiva
                lResultado = _oConector.lEjecutar_sentencia(String.Format(InventariosSectoresTBL.SP_ASIGNAR_TODOS, oInveSector.nIdInventario))

            Else
                'establecemos la sentencia de insercion de registro
                lResultado = _oConector.lEjecutar_sentencia(String.Format(_Insert_sql, oInveSector.nIdInventario, oInveSector.cIdSector))

            End If

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lActualizar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elemento(ByVal oInveSectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'casteamos el parametro a su tipo
            Dim oInveSector As InventarioSectorOTD = CType(oInveSectorParam, InventarioSectorOTD)

            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(" WHERE {0}.{1} = {2} AND {0}.{3} = {4}" _
                                                       , InventariosSectoresTBL.NOMBRE_TABLA _
                                                       , InventariosSectoresTBL.ID_INVENTARIO.cNombre _
                                                       , oInveSector.nIdInventario _
                                                       , InventariosSectoresTBL.ID_SECTOR.cNombre _
                                                       , oInveSector.cIdSector _
                                                       )

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of InventarioSectorOTD))(0)})

            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elementos()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ensamblamos la consulta sql
            Dim cConsulta As String = String.Format(_Select_sql & cFiltroWhere)

            'busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(cConsulta)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'instancias auxiliares
                Dim lInveSector As New List(Of InventarioSectorOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    'lo agregamos a la lista
                    lInveSector.Add(New InventarioSectorOTD(Long.Parse(dr(InventariosSectoresTBL.ID_INVENTARIO.nIndice).ToString()) _
                                            , dr(InventariosSectoresTBL.ID_SECTOR.nIndice).ToString() _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lInveSector})

            End If

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

#End Region


#Region "ESPECIFICOS"


#End Region

End Class
