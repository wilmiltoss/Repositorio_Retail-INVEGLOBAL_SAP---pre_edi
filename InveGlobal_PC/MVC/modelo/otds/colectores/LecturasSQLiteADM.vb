Imports CdgPersistencia.ClasesBases

Public Class LecturasSQLiteADM
    Inherits ADMbase



#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "LecturasSQLiteADM"

    Private __oApModelo As ApModeloSQLite


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModeloSQLite)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModeloParam
        Set_tabla(New LecturasSQLiteTBL())

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

    Public Overrides Function lGet_elemento(ByVal OTDbase As OTDbase) As List(Of Object)
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
            lResultado = _oConector.lEjecutar_sentencia(LecturasSQLiteTBL.SQL_DROP_TABLA)

            'si se ejecuto correctamente, creamos de vuelta la tabla
            If lResultado(0).Equals(1) Then lResultado = _oConector.lEjecutar_sentencia(LecturasSQLiteTBL.SQL_CREAR_TABLA)

        Catch ex As Exception

            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

#End Region


End Class
