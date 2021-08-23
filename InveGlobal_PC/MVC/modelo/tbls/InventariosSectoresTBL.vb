Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class InventariosSectoresTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "InventariosSectoresTBL"
    Public Shadows Const NOMBRE_TABLA As String = "INVENTARIO_SECTORES"

    'nombres de campos
    Public Shared ID_INVENTARIO As New Campo("ID_INVENTARIO", 0, False, GetType(Long))
    Public Shared ID_SECTOR As New Campo("ID_SECTOR", 1, False, GetType(String))

    ''' <summary>
    ''' Devuelve el comando se ejecucion de Asignacion de todos los sectores
    ''' al inventario parametro "@id_inventario = {0}"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared SP_ASIGNAR_TODOS As String = "EXEC [dbo].[SP_ASIGNAR_TODOS_LOS_SECTORES] @id_inventario = {0}"

#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID_INVENTARIO, ID_SECTOR}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
