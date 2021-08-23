Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class InventarioProductoSectoresTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "InventarioProductoSectoresTBL"
    Public Shadows Const NOMBRE_TABLA As String = "INVENTARIO_PRODUCTO_SECTOR"

    'nombres de campos
    Public Shared ID As New Campo("ID_INVENTARIO_PRODUCTO", 0, False, GetType(Long))
    Public Shared ID_SECTOR As New Campo("ID_SECTOR", 1, False, GetType(String))
    Public Shared MONTO As New Campo("MONTO", 2, False, GetType(String))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, ID_SECTOR, MONTO}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
