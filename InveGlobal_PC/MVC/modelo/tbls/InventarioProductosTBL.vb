Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class InventarioProductosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "InventarioProductosTBL"
    Public Shadows Const NOMBRE_TABLA As String = "INVENTARIO_PRODUCTOS"

    'nombres de campos
    Public Shared ID As New Campo("ID_INVENTARIO_PRODUCTO", 0, False, GetType(Long))
    Public Shared ID_INVENTARIO As New Campo("ID_INVENTARIO", 1, False, GetType(String))
    Public Shared ID_PRODUCTO As New Campo("ID_PRODUCTO", 2, False, GetType(String))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, ID_INVENTARIO, ID_PRODUCTO}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
