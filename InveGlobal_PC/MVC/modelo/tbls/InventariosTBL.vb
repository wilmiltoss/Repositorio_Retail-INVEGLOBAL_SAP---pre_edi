Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class InventariosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "InventariosTBL"
    Public Shadows Const NOMBRE_TABLA As String = "DATOS_INVENTARIO"

    'nombres de campos
    Public Shared ID As New Campo("ID_INVENTARIO", 0, False, GetType(Long), True)
    Public Shared FECHA_INVENTARIO As New Campo("FECHA_INVENTARIO", 1, False, GetType(DateTime))
    Public Shared ID_LOCAL As New Campo("ID_LOCAL", 2, False, GetType(Long))
    Public Shared ID_SISTEMA As New Campo("ID_SISTEMA", 3, False, GetType(Long))

    Public Shared COMENTARIOS As New Campo("COMENTARIOS", 4, False, GetType(String))
    Public Shared ULTIMO_CONTEO As New Campo("ULTIMO_CONTEO", 5, False, GetType(Integer))
    Public Shared NRO_CONTEO_APLICADO As New Campo("NRO_CONTEO_APLICADO", 6, False, GetType(Integer))
    Public Shared CERRADO As New Campo("CERRADO", 7, False, GetType(Boolean))

    Public Shared ID_USUARIO As New Campo("ID_USUARIO", 8, False, GetType(Long))
    Public Shared FECHA As New Campo("FECHA", 9, False, GetType(DateTime), True)


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, FECHA_INVENTARIO, ID_LOCAL, ID_SISTEMA, COMENTARIOS _
                            , ULTIMO_CONTEO, NRO_CONTEO_APLICADO, CERRADO, ID_USUARIO}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
