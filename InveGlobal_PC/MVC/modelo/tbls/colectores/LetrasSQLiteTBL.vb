Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class LetrasSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "LetrasSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = " LETRAS_SOPORTES"

    ''' <summary>
    ''' Comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS LETRAS_SOPORTES"
    ''' <summary>
    ''' Comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE LETRAS_SOPORTES(ID_LETRA_SOPORTE INTEGER PRIMARY KEY ASC, LETRA VARCHAR)"

    'nombres de campos
    Public Shared ID As New Campo("ID_LETRA_SOPORTE", 0, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("LETRA", 1, False, GetType(String))


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
