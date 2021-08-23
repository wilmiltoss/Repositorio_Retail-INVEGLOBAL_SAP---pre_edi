
Imports CdgPersistencia.ClasesBases

Public Class ProductoOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "ProductoOTD"

    Public cScanning As String
    Public cDetalle As String
    Public bPesable As Boolean



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        cScanning = String.Empty
        cDetalle = String.Empty
        bPesable = False


    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Descripcion del producto</param>
    ''' <param name="cScanningParam">Codigo de barras</param>
    ''' <param name="cDetalleParam">Detalles del producto</param>
    ''' <param name="bPesableParam">Si es pesable o no</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As String, ByVal cDescripcionParam As String _
                   , ByVal cScanningParam As String, ByVal cDetalleParam As String _
                   , ByVal bPesableParam As Boolean _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

        cScanning = cScanningParam
        cDetalle = cDetalleParam
        bPesable = bPesableParam

    End Sub

#End Region


End Class
