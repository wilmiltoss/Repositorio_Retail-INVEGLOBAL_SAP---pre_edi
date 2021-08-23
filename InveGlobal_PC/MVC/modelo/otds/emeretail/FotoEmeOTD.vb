Imports CdgPersistencia.ClasesBases


Public Class FotoEmeOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "FotoEmeOTD"

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

        nCosto = 0D
        cDetalle = String.Empty
        nTeorico = 0D
        cIdSector = String.Empty

        cPesable = "N"

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Descripcion del articulo</param>
    ''' <param name="nCostoParam">Costo del articulo</param>
    ''' <param name="cDetalleParam">Detalles del articulo</param>
    ''' <param name="nTeoricoParam">Cantidad teorica en el sistema</param>
    ''' <param name="cIdSectorParam">Identificador del sector al que pertenece</param>
    ''' <param name="cPesableParam">Marca de producto pesable ['P'|'N']</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long, ByVal cDescripcionParam As String _
                    , ByVal nCostoParam As Double, ByVal cDetalleParam As String _
                    , ByVal nTeoricoParam As Double, ByVal cIdSectorParam As String _
                    , ByVal cPesableParam As Char _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

        nCosto = nCostoParam
        cDetalle = cDetalleParam
        nTeorico = nTeoricoParam
        cIdSector = cIdSectorParam

        cPesable = cPesableParam

    End Sub

#End Region


End Class
