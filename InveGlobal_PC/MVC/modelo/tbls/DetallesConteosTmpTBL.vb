Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class DetallesConteosTmpTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "DetallesConteosTBL"
    Public Shadows NOMBRE_TABLA As String = ""

    ''' <summary>
    ''' Devuelve el comando que se utiliza para generar las tablas temporales
    ''' para consulta de datos de conteos detallados x ubicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SP_CREAR_TABLA_TMP As String = "EXEC [dbo].[SP_CREAR_TBL_CONTEOS] @id_inventario = {0}, @nro_conteo = {1}"

    'nombres de campos
    Public Shared NOMBRE_LOCAL As New Campo("NOMBRE_LOCAL", 0, False, GetType(String))
    Public Shared ULTIMO_CONTEO As New Campo("ULTIMO_CONTEO", 1, False, GetType(Integer))
    Public Shared NOMBRE_SISTEMA As New Campo("NOMBRE_SISTEMA", 2, False, GetType(String))
    Public Shared LOCACION As New Campo("LOCACION", 3, False, GetType(String))

    Public Shared NRO_CONTEO As New Campo("NRO_CONTEO", 4, False, GetType(Integer))
    Public Shared SOPORTE As New Campo("SOPORTE", 5, False, GetType(String))
    Public Shared NRO_SOPORTE As New Campo("NRO_SOPORTE", 6, False, GetType(Integer))
    Public Shared LETRA As New Campo("LETRA", 7, False, GetType(String))

    Public Shared NIVEL As New Campo("NIVEL", 8, False, GetType(Integer))
    Public Shared METRO As New Campo("METRO", 9, False, GetType(Integer))
    Public Shared ID_SECTOR As New Campo("ID_SECTOR", 10, False, GetType(String))
    Public Shared NOMBRE_SECTOR As New Campo("NOMBRE_SECTOR", 11, False, GetType(String))

    Public Shared SCANNING As New Campo("SCANNING", 12, False, GetType(String))
    Public Shared ARTICULO As New Campo("ARTICULO", 13, False, GetType(String))
    Public Shared CANTIDAD As New Campo("CANTIDAD", 14, False, GetType(Double))
    Public Shared NOMBRE_USUARIO As New Campo("NOMBRE_USUARIO", 15, False, GetType(String))

    Public Shared ID_LOCACION As New Campo("ID_LOCACION", 16, False, GetType(Integer))
    Public Shared ID_INVENTARIO As New Campo("ID_INVENTARIO", 17, False, GetType(Long))
    Public Shared ID_SOPORTE As New Campo("ID_SOPORTE", 18, False, GetType(Integer))
    Public Shared ID_LETRA_SOPORTE As New Campo("ID_LETRA_SOPORTE", 19, False, GetType(Integer))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal cNombreTabla As String)
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {NOMBRE_LOCAL, ULTIMO_CONTEO, NOMBRE_SISTEMA, LOCACION, _
                                NRO_CONTEO, SOPORTE, NRO_SOPORTE, LETRA, _
                                NIVEL, METRO, ID_SECTOR, NOMBRE_SECTOR, _
                                SCANNING, ARTICULO, CANTIDAD, NOMBRE_USUARIO, _
                                ID_LOCACION, ID_INVENTARIO, ID_SOPORTE, ID_LETRA_SOPORTE _
                                }

        NOMBRE_TABLA = cNombreTabla

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class

