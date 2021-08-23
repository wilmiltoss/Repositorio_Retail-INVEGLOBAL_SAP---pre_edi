Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class EntradasColectorasTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "EntradasColectorasTBL"
    Public Shadows Const NOMBRE_TABLA As String = "ENTRADAS_COLECTORAS"

    'nombres de campos
    Public Shared ID_INVENTARIO As New Campo("ID_INVENTARIO", 0, False, GetType(Long))
    Public Shared ID_LOCACION As New Campo("ID_LOCACION", 1, False, GetType(Integer))
    Public Shared NRO_CONTEO As New Campo("NRO_CONTEO", 2, False, GetType(Integer))
    Public Shared ID_SOPORTE As New Campo("ID_SOPORTE", 3, False, GetType(Integer))

    Public Shared NRO_SOPORTE As New Campo("NRO_SOPORTE", 4, False, GetType(Integer))
    Public Shared ID_LETRA_SOPORTE As New Campo("ID_LETRA_SOPORTE", 5, False, GetType(Integer))
    Public Shared SCANNING As New Campo("SCANNING", 6, False, GetType(String))
    Public Shared NIVEL As New Campo("NIVEL", 7, False, GetType(Integer))

    Public Shared METRO As New Campo("METRO", 8, False, GetType(Integer))
    Public Shared CANTIDAD As New Campo("CANTIDAD", 9, False, GetType(Double))
    Public Shared COLECTORA As New Campo("COLECTORA", 10, False, GetType(String))
    Public Shared ID_USUARIO As New Campo("ID_USUARIO", 11, False, GetType(Long))




#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID_INVENTARIO, ID_LOCACION, NRO_CONTEO, ID_SOPORTE _
                                , NRO_SOPORTE, ID_LETRA_SOPORTE, SCANNING, NIVEL _
                                , METRO, CANTIDAD, COLECTORA, ID_USUARIO _
                                }

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
