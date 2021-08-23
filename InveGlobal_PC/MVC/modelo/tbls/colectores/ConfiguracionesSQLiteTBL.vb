Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class ConfiguracionesSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "ConfiguracionesSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "CONFIGURACIONES"


    ''' <summary>
    ''' Comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS CONFIGURACIONES"
    ''' <summary>
    ''' Comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE CONFIGURACIONES (CANTIDAD_MAXIMA_CONTEO FLOAT NOT NULL DEFAULT 0, CANTIDAD_CONTEO_CON_DECIMALES INTEGER NOT NULL DEFAULT 1)"

    'nombres de campos
    Public Shared CANTIDAD_MAXIMA As New Campo("CANTIDAD_MAXIMA_CONTEO", 0, False, GetType(Double))
    Public Shared CON_DECIMALES As New Campo("CANTIDAD_CONTEO_CON_DECIMALES", 1, False, GetType(Integer))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {CANTIDAD_MAXIMA, CON_DECIMALES}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
