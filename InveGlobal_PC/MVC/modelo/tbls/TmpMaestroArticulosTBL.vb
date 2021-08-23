Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class TmpMaestroArticulosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "TmpMaestroArticulos"
    Public Shadows Const NOMBRE_TABLA As String = "TMP_MAESTRO_ARTICULOS"

    'nombres de campos
    Public Shared ID_INVENTARIO As New Campo("ID_INVENTARIO", 0, False, GetType(Long))
    Public Shared SCANNING As New Campo("SCANNING", 1, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 2, False, GetType(String))
    Public Shared COSTO As New Campo("COSTO", 3, False, GetType(Decimal))

    Public Shared DETALLE As New Campo("DETALLE", 4, False, GetType(String))
    Public Shared CANTIDAD_TEORICA As New Campo("CANTIDAD_TEORICA", 5, False, GetType(Decimal))
    Public Shared ID_SECTOR As New Campo("ID_SECTOR", 6, False, GetType(String))
    Public Shared PESABLE As New Campo("PESABLE", 7, False, GetType(String))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID_INVENTARIO, SCANNING, DESCRIPCION, COSTO, DETALLE, CANTIDAD_TEORICA, ID_SECTOR, PESABLE}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
