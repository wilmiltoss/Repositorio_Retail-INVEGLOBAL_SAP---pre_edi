Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class SistemasTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "SistemasTBL"
    Public Shadows Const NOMBRE_TABLA As String = "SISTEMAS"

    'nombres de campos
    Public Shared ID As New Campo("ID_SISTEMA", 0, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("NOMBRE_SISTEMA", 1, False, GetType(String))
    Public Shared USUARIO_DE_SISTEMA As New Campo("USUARIO_DE_SISTEMA", 2, False, GetType(String))
    Public Shared IP_SERVIDOR As New Campo("IP_SERVIDOR", 3, False, GetType(String))

    Public Shared NOMBRE_SERVICIO As New Campo("NOMBRE_SERVICIO", 4, False, GetType(String))
    Public Shared PUERTO_SERVICIO As New Campo("PUERTO_SERVICIO", 5, False, GetType(Integer))
    Public Shared PASSWORD As New Campo("PASSWORD", 6, False, GetType(String))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION, USUARIO_DE_SISTEMA, IP_SERVIDOR, NOMBRE_SERVICIO, PUERTO_SERVICIO, PASSWORD}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
