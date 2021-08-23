Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class SectoresSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "SectoresSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "SECTORES"

    ''' <summary>
    ''' Devuelve el comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS SECTORES"
    ''' <summary>
    ''' Devuelve el comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE SECTORES(ID_SECTOR VARCHAR NOT NULL PRIMARY KEY ASC, DESCRIPCION VARCHAR NOT NULL)"

    'nombres de campos
    Public Shared ID As New Campo("ID_SECTOR", 0, False, GetType(String))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 1, False, GetType(String))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
