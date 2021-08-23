Imports CdgPersistencia.ClasesBases

Public Class InventarioSectorOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "InventarioSectorOTD"

    Public cIdSector As String
    Public nIdInventario As Long



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nIdInventario = 0
        cIdSector = "0"

    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdInventarioParam">Identificador del Inventario</param>
    ''' <param name="cIdSectorParam">Identificador del Sector</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdInventarioParam As Long, ByVal cIdSectorParam As String)
        MyBase.New(0, NOMBRE_CLASE)

        nIdInventario = nIdInventarioParam
        cIdSector = cIdSectorParam

    End Sub

#End Region


End Class
