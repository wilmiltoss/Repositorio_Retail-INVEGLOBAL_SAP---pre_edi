Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class LocacionesSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "LocacionesSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "LOCACIONES"

    ''' <summary>
    ''' Comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS LOCACIONES"
    ''' <summary>
    ''' Comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE LOCACIONES(ID_LOCACION INTEGER PRIMARY KEY ASC, DESCRIPCION VARCHAR)"

    'nombres de campos
    Public Shared ID As New Campo("ID_LOCACION", 0, False, GetType(Long))
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
