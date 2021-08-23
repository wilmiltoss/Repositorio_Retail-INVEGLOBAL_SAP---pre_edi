Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class UsuariosLocalesTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "UsuariosLocalesTBL"
    Public Shadows Const NOMBRE_TABLA As String = "USUARIO_LOCAL"

    'nombres de campos
    Public Shared ID As New Campo("ID", 0, False, GetType(Long), True)
    Public Shared ID_USUARIO As New Campo("ID_USUARIO", 1, False, GetType(Long))
    Public Shared ID_LOCAL As New Campo("ID_LOCAL", 2, False, GetType(Long))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, ID_USUARIO, ID_LOCAL}

    End Sub



#End Region

    ''' <summary>
    ''' Devuelve el nombre de la tabla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
