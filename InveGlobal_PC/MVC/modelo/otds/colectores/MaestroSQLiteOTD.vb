Imports CdgPersistencia.ClasesBases


Public Class MaestroSQLiteOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "MaestroSQLiteOTD"


    Public cScanning As String
    Public cDetalle As String
    Public cPesable As Char
    Public nCosto As Double
    Public cIdSector As String


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        cScanning = String.Empty
        cDetalle = String.Empty
        cPesable = ""
        nCosto = 0D
        cIdSector = String.Empty

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="cScanningParam">Codigo EAN del Articulo</param>
    ''' <param name="cDescripcionParam">Descripcion del Articulo</param>
    ''' <param name="cDetalleParam">Detalles del articulo</param>
    ''' <param name="cPesableParam">Si es Pesable 'P' o 'N' si no lo es</param>
    ''' <param name="nCostoParam">Costo del Articulo</param>
    ''' <param name="cIdSectorParam">Identificador del Sector al que pertenece (dos digitos)</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cScanningParam As String, ByVal cDescripcionParam As String _
                    , ByVal cDetalleParam As String, ByVal cPesableParam As Char, ByVal nCostoParam As Double _
                    , ByVal cIdSectorParam As String _
                    )
        MyBase.New(Long.Parse(cScanningParam), cDescripcionParam)

        cScanning = cScanningParam
        cDetalle = cDetalleParam
        cPesable = cPesableParam
        nCosto = nCostoParam
        cIdSector = cIdSectorParam

    End Sub

#End Region


End Class
