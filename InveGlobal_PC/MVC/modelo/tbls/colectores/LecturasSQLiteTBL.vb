Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class LecturasSQLiteTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "LecturasSQLiteTBL"
    Public Shadows Const NOMBRE_TABLA As String = "LECTURAS"


    ''' <summary>
    ''' Comando de eliminacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_DROP_TABLA As String = "DROP TABLE IF EXISTS LECTURAS"
    ''' <summary>
    ''' Comando de creacion de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_CREAR_TABLA As String = "CREATE TABLE LECTURAS(ID_LOCACION INTEGER, NRO_CONTEO INTEGER, ID_SOPORTE INTEGER " _
                                                + ", NRO_SOPORTE INTEGER, ID_LETRA_SOPORTE INTEGER, NIVEL INTEGER, METRO INTEGER " _
                                                + ", SCANNING TEXT, CANTIDAD FLOAT, ID_USUARIO INT, PRIMARY KEY (ID_LOCACION" _
                                                + ", NRO_CONTEO, ID_SOPORTE, NRO_SOPORTE, ID_LETRA_SOPORTE, NIVEL, METRO, SCANNING))"

    'nombres de campos
    Public Shared ID_LOCACION As New Campo("ID_LOCACION", 0, False, GetType(Long))
    Public Shared NRO_CONTEO As New Campo("NRO_CONTEO", 1, False, GetType(Long))
    Public Shared ID_SOPORTE As New Campo("ID_SOPORTE", 2, False, GetType(Long))
    Public Shared NRO_SOPORTE As New Campo("NRO_SOPORTE", 3, False, GetType(Long))

    Public Shared ID_LETRA_SOPORTE As New Campo("ID_LETRA_SOPORTE", 4, False, GetType(Long))
    Public Shared NIVEL As New Campo("NIVEL", 5, False, GetType(Long))
    Public Shared METRO As New Campo("METRO", 6, False, GetType(Long))

    Public Shared SCANNING As New Campo("SCANNING", 7, False, GetType(String))
    Public Shared CANTIDAD As New Campo("CANTIDAD", 8, False, GetType(Double))
    Public Shared ID_USUARIO As New Campo("ID_USUARIO", 9, False, GetType(Long))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID_LOCACION, NRO_CONTEO, ID_SOPORTE, NRO_SOPORTE, ID_LETRA_SOPORTE _
                              , NIVEL, METRO, SCANNING, CANTIDAD, ID_USUARIO}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
