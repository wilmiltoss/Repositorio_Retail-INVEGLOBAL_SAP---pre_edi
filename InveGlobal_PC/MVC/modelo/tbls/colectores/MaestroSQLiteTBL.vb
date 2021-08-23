Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class MaestroSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "MaestroSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "MAESTRO"

    ''' <summary>
    ''' Comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS MAESTRO"
    ''' <summary>
    ''' Comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE MAESTRO(SCANNING VARCHAR PRIMARY KEY ASC, DESCRIPCION VARCHAR, DETALLE VARCHAR, PESABLE VARCHAR, COSTO FLOAT, ID_SECTOR VARCHAR)"

    'nombres de campos
    Public Shared SCANNING As New Campo("SCANNING", 0, False, GetType(String))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 1, False, GetType(String))
    Public Shared DETALLE As New Campo("DETALLE", 2, False, GetType(String))
    Public Shared PESABLE As New Campo("PESABLE", 3, False, GetType(String))

    Public Shared COSTO As New Campo("COSTO", 4, False, GetType(Double))
    Public Shared ID_SECTOR As New Campo("ID_SECTOR", 5, False, GetType(String))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {SCANNING, DESCRIPCION, DETALLE, PESABLE, COSTO, ID_SECTOR}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
