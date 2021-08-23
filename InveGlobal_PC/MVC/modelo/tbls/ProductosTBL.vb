Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class ProductosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "Productos_TBL"
    Public Shadows Const NOMBRE_TABLA As String = "PRODUCTOS"

    'nombres de campos
    Public Shared ID As New Campo("ID_PRODUCTO", 0, False, GetType(Long))
    Public Shared SCANNING As New Campo("SCANNING", 1, False, GetType(String))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 2, False, GetType(String))

    Public Shared DETALLE As New Campo("DETALLE", 3, False, GetType(String))
    Public Shared PESABLE As New Campo("PESABLE", 4, False, GetType(Boolean))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, SCANNING, DESCRIPCION, DETALLE, PESABLE}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
