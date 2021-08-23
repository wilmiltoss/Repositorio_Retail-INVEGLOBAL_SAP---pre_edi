Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class SoportesSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "SoportesSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "SOPORTES"

    ''' <summary>
    ''' Devuelve el comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS SOPORTES"
    ''' <summary>
    ''' Devuelve el comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE SOPORTES(ID_SOPORTE INTEGER PRIMARY KEY ASC, DESCRIPCION VARCHAR, SUBDIVISIBLE INTEGER)"

    'nombres de campos
    Public Shared ID As New Campo("ID_SOPORTE", 0, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 1, False, GetType(String))
    Public Shared SUB_DIVISIBLE As New Campo("SUBDIVISIBLE", 2, False, GetType(Integer))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION, SUB_DIVISIBLE}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
