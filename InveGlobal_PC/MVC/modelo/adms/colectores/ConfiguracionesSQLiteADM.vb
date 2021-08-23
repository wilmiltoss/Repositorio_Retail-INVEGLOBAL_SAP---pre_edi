Imports CdgPersistencia.ClasesBases

Public Class ConfiguracionesSQLiteADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ConfiguracionesSQLiteADM"

    Private __oApModelo As ApModeloSQLite


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModeloSQLite)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New ConfiguracionesSQLiteTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oConfigParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oConfig As ConfiguracionSQLiteOTD = CType(oConfigParam, ConfiguracionSQLiteOTD)

            'ensamblamos la sentencia de actualizacion
            Dim cUpdate As String = String.Format(_Insert_sql, oConfig.nCantidadMaxima, oConfig.nConDecimales)

            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_sentencia(cUpdate)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lActualizar(ByVal oConfigParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lActualizar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oConfig As ConfiguracionSQLiteOTD = CType(oConfigParam, ConfiguracionSQLiteOTD)

            'ensamblamos la sentencia de actualizacion
            Dim cUpdate As String = String.Format(_Update_sql, oConfig.nCantidadMaxima, oConfig.nConDecimales)

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

    Public Overrides Function lGet_elemento(ByVal oSectorParam As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)
        Throw New NotImplementedException()
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
            lResultado = _oConector.lEjecutar_sentencia(ConfiguracionesSQLiteTBL.SQL_DROP_TABLA)

            'si se ejecuto correctamente, creamos de vuelta la tabla
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(ConfiguracionesSQLiteTBL.SQL_CREAR_TABLA)

            'si se ejecuto correctamente, insertamos un registro
            If lResultado(0).Equals(1) Then lResultado = lAgregar(New ConfiguracionSQLiteOTD())

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

#End Region

End Class
