Imports CdgPersistencia.ClasesBases

Public Class UsuariosLocalesADM
    Inherits ADMbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "UsuariosLocalesADM"

    Private __oApModelo As ApModelo


#End Region


#Region "CONSTRUCTORES"

    Public Sub New(ByVal ApModeloParam As ApModelo)
        MyBase.New(ApModeloParam.Get_conector())

        __oApModelo = ApModelo.Get_instancia()
        Set_tabla(New UsuariosLocalesTBL())

    End Sub

#End Region


#Region "Miembros de ADMbase"

    Public Overrides Function lAgregar(ByVal oUsuarioLocalOTDparam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lAgregar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oUsuarioLocal As UsuarioLocalOTD = CType(oUsuarioLocalOTDparam, UsuarioLocalOTD)

            'establecemos la sentencia de actualizacion
            Dim cInsert As String = String.Format(_Insert_sql _
                                                  , oUsuarioLocal.Usuario_OTD.nId _
                                                  , oUsuarioLocal.Local_OTD.nId)

            'ejecutamos la sentencia
            lResultado = _oConector.lEjecutar_sentencia(cInsert)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lActualizar(ByVal oUsuarioLocalOTDparam As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lEliminar(ByVal oUsuarioLocalOTDparam As OTDbase) As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE + ".lEliminar()"

        'resultado por defecto del metodo
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'casteamos el parametro a su tipo
            Dim oUsuario As UsuarioOTD = CType(oUsuarioLocalOTDparam, UsuarioOTD)

            'establecemos la sentencia de actualizacion
            Dim cDelete As String = String.Format(_Delete_sql _
                                                  , oUsuarioLocalOTDparam.nId)

            'ejecutamos la sentencia
            lResultado = _oConector.lEjecutar_sentencia(cDelete)

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado

    End Function

    Public Overrides Function lGet_elemento(ByVal oOTDbase As OTDbase) As List(Of Object)
        Throw New NotImplementedException()
    End Function

    Public Overrides Function lGet_elementos(ByVal cFiltroWhere As String) As List(Of Object)
        Throw New NotImplementedException()
    End Function

#End Region


#Region "ESPECIFICOS"


#End Region

End Class
