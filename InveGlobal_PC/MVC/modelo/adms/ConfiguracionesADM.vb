Imports CdgPersistencia.ClasesBases

Public Class ConfiguracionesADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ConfiguracionesADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New ConfiguracionesTBL())


    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oConfiguracionParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'casteamos el parametro a su tipo
            Dim oConfig As ConfiguracionOTD = CType(oConfiguracionParam, ConfiguracionOTD)

            'establecemos la sentencia de insercion de registro
            Dim cInsert As String = String.Format(_Insert_sql, oConfig.nId, oConfig.nConteoMaximo _
                                                  , oConfig.bConDecimales, oConfig.bConPesables)

            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_sentencia(cInsert)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lActualizar(ByVal oConfiguracionParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lActualizar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'casteamos el parametro a su tipo
            Dim oConfig As ConfiguracionOTD = CType(oConfiguracionParam, ConfiguracionOTD)

            'establecemos la sentencia de insercion de registro
            Dim cUpdate As String = String.Format(_Update_sql, oConfig.nId, oConfig.nConteoMaximo _
                                                  , oConfig.bConDecimales, oConfig.bConPesables _
                                                  )
            cUpdate += String.Format(" WHERE {0} = {1}", ConfiguracionesTBL.ID.cNombre, oConfig.nId)

            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_sentencia(cUpdate)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lEliminar(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elemento(ByVal oConfiguracionesParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lGet_elemento()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'intentamos recuperar los datos
        Try
            'establecemos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(_oTabla.Get_filtro_where_int(), ConfiguracionesTBL.ID.cNombre, oConfiguracionesParam.nId)

            'ejecutamos la busqueda filtrada
            lResultado = lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, CType(lResultado(1), List(Of ConfiguracionOTD))(0)})

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
                Dim lConfigs As New List(Of ConfiguracionOTD)()
                Dim dtResultado As DataTable = CType(lResultado(1), DataTable)

                'recorremos las filas devueltas
                For Each dr As DataRow In dtResultado.Rows
                    'lo agregamos a la lista
                    lConfigs.Add(New ConfiguracionOTD(Long.Parse(dr(ConfiguracionesTBL.ID.nIndice).ToString()) _
                                            , Decimal.Parse(dr(ConfiguracionesTBL.CONTEO_MAXIMO.nIndice).ToString()) _
                                            , Boolean.Parse(dr(ConfiguracionesTBL.CONTEO_CON_DECIMALES.nIndice).ToString()) _
                                            , Boolean.Parse(dr(ConfiguracionesTBL.PESABLES_INCLUIDOS.nIndice).ToString()) _
                                           ))

                Next

                'establecemos el resultado del metodo
                lResultado = New List(Of Object)(New Object() {1, lConfigs})

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
