Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class VwDetallesConteosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "VwDetallesConteosTBL"
    Public Shadows NOMBRE_TABLA As String = ""

    Public Const SP_CREAR_VISTA As String = "EXEC [dbo].[SP_CREAR_VISTA_DETALLES_CONTEOS] @id_inventario = {0},@nombre_tabla_1 = '{1}',@nombre_tabla_2 = '{2}',@nombre_tabla_3 = '{3}'"

    'nombres de campos
    Public Shared NOMBRE_LOCAL As New Campo("NOMBRE_LOCAL", 0, False, GetType(String))
    Public Shared LOCACION As New Campo("LOCACION", 1, False, GetType(String))
    Public Shared SOPORTE As New Campo("SOPORTE", 2, False, GetType(String))
    Public Shared METRO As New Campo("METRO", 3, False, GetType(Integer))

    Public Shared NIVEL As New Campo("NIVEL", 4, False, GetType(Integer))
    Public Shared SCANNING As New Campo("SCANNING", 5, False, GetType(String))
    Public Shared ARTICULO As New Campo("ARTICULO", 6, False, GetType(String))
    Public Shared CONTEO_1 As New Campo("CONTEO_1", 7, False, GetType(Double))

    Public Shared CONTEO_2 As New Campo("CONTEO_2", 8, False, GetType(Double))
    Public Shared DIFERENCIA_1_2 As New Campo("DIFERENCIA_1_2", 9, False, GetType(Double))
    Public Shared CONTEO_3 As New Campo("CONTEO_3", 10, False, GetType(Double))
    Public Shared NOMBRE_USUARIO As New Campo("NOMBRE_USUARIO", 11, False, GetType(String))

    Public Shared ID_INVENTARIO As New Campo("ID_INVENTARIO", 12, False, GetType(Long))
    Public Shared ID_LOCACION As New Campo("ID_LOCACION", 13, False, GetType(Integer))
    Public Shared ID_SOPORTE As New Campo("ID_SOPORTE", 14, False, GetType(Integer))
    Public Shared NRO_SOPORTE As New Campo("NRO_SOPORTE", 15, False, GetType(Integer))

    Public Shared ID_LETRA_SOPORTE As New Campo("ID_LETRA_SOPORTE", 16, False, GetType(Integer))
    Public Shared ID_SECTOR As New Campo("ID_SECTOR", 17, False, GetType(String))
    Public Shared COSTO As New Campo("COSTO", 18, False, GetType(String))
    Public Shared PRUEBA_1 As New Campo("PRUEBA_1", 19, False, GetType(Double))
    Public Shared PRUEBA_2 As New Campo("PRUEBA_2", 20, False, GetType(Double))
    Public Shared PRUEBA_3 As New Campo("PRUEBA_3", 21, False, GetType(Double))

#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="cNombreVistaParam">Nombre de la vista a representar</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cNombreVistaParam As String)
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {NOMBRE_LOCAL, LOCACION, SOPORTE, METRO _
                                      , NIVEL, SCANNING, ARTICULO, CONTEO_1 _
                                      , CONTEO_2, DIFERENCIA_1_2, CONTEO_3, ID_INVENTARIO, ID_LOCACION _
                                      , ID_SOPORTE, NRO_SOPORTE, ID_LETRA_SOPORTE, NOMBRE_USUARIO, ID_SECTOR _
                                      , COSTO, PRUEBA_1, PRUEBA_2, PRUEBA_3 _
                                }

        NOMBRE_TABLA = cNombreVistaParam

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class

