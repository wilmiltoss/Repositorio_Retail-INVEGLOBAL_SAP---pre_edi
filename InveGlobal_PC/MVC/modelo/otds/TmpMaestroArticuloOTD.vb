Imports CdgPersistencia.ClasesBases

Public Class TmpMaestroArticuloOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "TmpMaestroArticuloOTD"

    Public Const PESABLE As Char = "P"
    Public Const NO_PESABLE As Char = "N"

    Public nIdInventario As Long
    Public nScanning As Long
    Public nCosto As Double
    Public cDetalle As String

    Public nTeorico As Double
    Public cIdSector As String
    Public cPesable As Char




#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nIdInventario = 0L
        nScanning = 0L
        nCosto = 0D
        cDetalle = NOMBRE_CLASE

        nTeorico = 0D
        cIdSector = String.Empty
        cPesable = ""


    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdInventarioParam">Identificador del inventario asociado</param>
    ''' <param name="cDescripcionParam">Descripcion del articulo</param>
    ''' <param name="nScanningParam">Codigo de barras del articulo</param>
    ''' <param name="nCostoParam">Costo del articulo</param>
    ''' <param name="cDetalleParamm">Detalle del articulo</param>
    ''' <param name="nTeoricoParam">Cantidad teorica en existencia</param>
    ''' <param name="cIdSectorParam">Identificador del sector asociado</param>
    ''' <param name="cPesableParam">Si es pesable o no ['P', 'N']</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdInventarioParam As Long, ByVal cDescripcionParam As String _
                    , ByVal nScanningParam As Long, ByVal nCostoParam As Double _
                    , ByVal cDetalleParamm As String, ByVal nTeoricoParam As Double _
                    , ByVal cIdSectorParam As String, ByVal cPesableParam As Char _
                    )
        MyBase.New(nIdInventarioParam, cDescripcionParam)

        nIdInventario = nIdInventarioParam
        nScanning = nScanningParam
        nCosto = nCostoParam
        cDetalle = cDetalleParamm

        nTeorico = nTeoricoParam
        cIdSector = cIdSectorParam
        cPesable = cPesableParam

    End Sub

#End Region



#Region "ESPECIFICOS"

    Public ReadOnly Property Get_csv() As String
        Get
            Return String.Format("{0};{1};{2};{3};{4};{5};{6};{7}" _
                                 , nIdInventario, nScanning, cDescripcion.Trim().ToUpper().Replace("'", "").Replace(";", "") _
                                 , nCosto, cDetalle.Trim(), nTeorico, cIdSector, cPesable _
                                    ).Replace(",", ".")
        End Get
    End Property


#End Region

End Class
