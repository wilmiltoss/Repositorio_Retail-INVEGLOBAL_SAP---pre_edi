Imports CdgPersistencia.ClasesBases

Public Class UsuariosSQLiteADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "UsuariosSQLiteADM"

    Private __oApModelo As ApModeloSQLite


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModeloSQLite)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New UsuariosSQLiteTBL())

        _Insert_sql = _Insert_sql.ToUpper().Replace("INSERT", "INSERT OR REPLACE")


    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oUsuarioParam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oUsuario As UsuarioSQLiteOTD = CType(oUsuarioParam, UsuarioSQLiteOTD)

            'ensamblamos la sentencia de actualizacion
            Dim cInsert As String = String.Format(_Insert_sql, oUsuario.nId, oUsuario.cDescripcion _
                                                  , "0x" + BitConverter.ToString(oUsuario.aContrasena).Replace("-", "") _
                                                  , oUsuario.nNivelAcceso)

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

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lEliminar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'ejecutamos la busqueda filtrada
            lResultado = _oConector.lEjecutar_sentencia(_Delete_sql)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

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
            lResultado = _oConector.lEjecutar_sentencia(UsuariosSQLiteTBL.SQL_DROP_TABLA)

            'si se ejecuto correctamente, creamos de vuelta la tabla
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(UsuariosSQLiteTBL.SQL_CREAR_TABLA)

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

#End Region

End Class
