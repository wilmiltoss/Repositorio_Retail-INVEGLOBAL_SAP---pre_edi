Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class UsuariosSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "UsuariosSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "USUARIOS"


    ''' <summary>
    ''' Comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS USUARIOS"
    ''' <summary>
    ''' Comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE USUARIOS(ID_USUARIO INT NOT NULL PRIMARY KEY ASC, NOMBRE_USUARIO VARCHAR NOT NULL, NIVEL_ACCESO INT NOT NULL, CONTRASENA BLOB)"

    'nombres de campos
    Public Shared ID As New Campo("ID_USUARIO", 0, False, GetType(Long))
    Public Shared NOMBRE_USUARIO As New Campo("NOMBRE_USUARIO", 1, False, GetType(String))
    Public Shared CONTRASENA As New Campo("CONTRASENA", 2, False, GetType(String))
    Public Shared NIVEL_ACCESO As New Campo("NIVEL_ACCESO", 3, False, GetType(Integer))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, NOMBRE_USUARIO, CONTRASENA, NIVEL_ACCESO}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
