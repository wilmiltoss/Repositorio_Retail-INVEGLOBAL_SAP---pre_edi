Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes


Public Class UsuariosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "UsuariosTBL"
    Public Shadows Const NOMBRE_TABLA As String = "USUARIOS"

    'nombres de campos
    Public Shared ID As New Campo("ID_USUARIO", 0, False, GetType(Long))
    Public Shared NOMBRE_USUARIO As New Campo("NOMBRE_USUARIO", 1, False, GetType(String))
    Public Shared CONTRASENA As New Campo("CONTRASENA", 2, False, GetType(Byte()))
    Public Shared NIVEL_ACCESO As New Campo("NIVEL_ACCESO", 3, False, GetType(Integer))

    Public Shared HABILITADO As New Campo("HABILITADO", 4, False, GetType(Boolean))
    Public Shared MOSTRAR As New Campo("MOSTRAR", 5, False, GetType(Boolean))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, NOMBRE_USUARIO, CONTRASENA, NIVEL_ACCESO, HABILITADO, MOSTRAR}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
