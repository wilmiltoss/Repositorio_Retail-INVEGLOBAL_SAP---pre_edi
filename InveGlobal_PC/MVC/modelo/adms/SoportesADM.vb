Imports CdgPersistencia.ClasesBases

Public Class SoportesADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "SoportesADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New SoportesTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oSoporteParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oSoporte As SoporteOTD = CType(oSoporteParam, SoporteOTD)

            'ensamblamos la sentencia de actualizacion
            Dim cInsert As String = String.Format(_Insert_sql, oSoporte.nId, oSoporte.cDescripcion, oSoporte.bSubDivisible)

            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_sentencia(cInsert)

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

    Public Overrides Function lGet_elemento(ByVal oSectorParam As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elementos()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_consulta(_Select_sql & cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                Dim dtTabla As DataTable = CType(lResultado(1), DataTable)
                Dim lSoportes As New List(Of SoporteOTD)()

                'recorremos las filas de la tabla
                For Each dr As DataRow In dtTabla.Rows
                    'creamos una nueva instancia de la locacion y la agregamos a la lista
                    lSoportes.Add(New SoporteOTD(Long.Parse(dr(SoportesTBL.ID.nIndice).ToString()) _
                                                    , dr(SoportesTBL.DESCRIPCION.nIndice).ToString() _
                                                    , Boolean.Parse(dr(SoportesTBL.SUB_DIVISIBLE.nIndice).ToString()) _
                                                    ) _
                                    )
                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lSoportes})

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
