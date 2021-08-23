Imports CdgPersistencia.ClasesBases

Public Class LetrasSQLiteADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "LetrasSQLiteADM"

    Private __oApModelo As ApModeloSQLite


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModeloSQLite)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New LetrasSQLiteTBL())

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

    Public Overrides Function lGet_elemento(ByVal oSectorParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(_oTabla.Get_filtro_where_int(), SectoresTBL.ID.cNombre, oSectorParam.nId)

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of SectorOTD))(0)})

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
                Dim lSectores As New List(Of SectorOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    'lo agregamos a la lista
                    lSectores.Add(New SectorOTD(dr(SectoresTBL.ID.nIndice).ToString() _
                                            , dr(SectoresTBL.DESCRIPCION.nIndice).ToString() _
                                            , Integer.Parse(dr(SectoresTBL.NIVEL.nIndice).ToString()) _
                                            , dr(SectoresTBL.ID_SECTOR_PADRE.nIndice).ToString() _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lSectores})

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

    ''' <summary>
    ''' Ejecuta la creacion de la tabla administrada
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Public Function lCrear_tabla() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lCrear_tabla()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'ejecutamos el comando de eliminacion de la tabla
            lResultado = _oConector.lEjecutar_sentencia(LetrasSQLiteTBL.SQL_DROP_TABLA)

            'si se ejecuto correctamente, creamos de vuelta la tabla
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(LetrasSQLiteTBL.SQL_CREAR_TABLA)

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

#End Region

End Class
