Imports CdgPersistencia.ClasesBases

Public Class VwDetalleConteoOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "VwDetalleConteoOTD"

    Public cNombreLocal As String
    Public cLocacion As String
    Public cSoporte As String
    Public nMetro As Integer

    Public nNivel As Integer
    Public cScanning As String
    Public cArticulo As String
    Public nConteo1 As Double

    Public nConteo2 As Double
    Public nDiferencia12 As Double
    Public nConteo3 As Double
    Public cNombreUsuario As String

    Public nIdInventario As Long
    Public nIdLocacion As Integer
    Public nIdSoporte As Integer
    Public nNroSoporte As Integer

    Public nIdLetraSoporte As Integer
    Public cIdSector As String
    Public nCosto As Double
    Public nPrueba1 As Double
    Public nPrueba2 As Double
    Public nPrueba3 As Double






#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        cNombreLocal = String.Empty
        cLocacion = String.Empty
        cSoporte = String.Empty
        nMetro = 0

        nNivel = 0
        cScanning = String.Empty
        cArticulo = String.Empty
        nConteo1 = 0D

        nConteo2 = 0D
        nDiferencia12 = 0D
        nConteo3 = 0D
        cNombreUsuario = String.Empty

        nIdInventario = 0L
        nIdLocacion = 0
        nIdSoporte = 0
        nNroSoporte = 0

        nIdLetraSoporte = 0
        cIdSector = String.Empty

        nCosto = 0D
        nPrueba1 = 0
        nPrueba2 = 0
        nPrueba3 = 0

    End Sub

    ''' <summary>
    ''' Constructor de la clase 
    ''' </summary>
    ''' <param name="cNombreLocalParam">Nombre del Local</param>
    ''' <param name="cLocacionParam">Descripcion de la locacion</param>
    ''' <param name="cSoporteParam">Descripcion del soporte</param>
    ''' <param name="nMetroParam">Metro</param>
    ''' <param name="nNivelParam">Nivel</param>
    ''' <param name="cScanningParam">Codigo de barras del articulo</param>
    ''' <param name="cArticuloParam">Descripcion del articulo</param>
    ''' <param name="nConteo1Param">Cantidad en primer conteo</param>
    ''' <param name="nConteo2Param">Cantidad en segundo conteo</param>
    ''' <param name="nDiferencia12Param">Diferencia entre los primeros dos conteos</param>
    ''' <param name="nConteo3Param">Cantidad en tercer conteo</param>
    ''' <param name="cNombreUsuarioParam">Nombre del Usuario</param>
    ''' <param name="nIdInventarioParam">Identificador del inventario</param>
    ''' <param name="nIdLocacionParam">Identificador de la locacion</param>
    ''' <param name="nIdSoporteParam">Identificador del soporte</param>
    ''' <param name="nNroSoporteParam">Nro. de Soporte</param>
    ''' <param name="nIdLetraSoporteParam">Identificador de la letra del soporte</param>
    ''' <param name="cIdSectorParam">Identificador del sector del articulo</param>
    ''' <param name="nCostoParam">Costo del Articulo</param>
    ''' <param name="nPrueba1Param">Conteo de Prueba numero 1</param>
    ''' <param name="nPrueba2Param">Conteo de Prueba numero 2</param>
    ''' <param name="nPrueba3Param">Conteo de Prueba numero 3</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cNombreLocalParam As String, ByVal cLocacionParam As String, ByVal cSoporteParam As String, _
        ByVal nMetroParam As Integer, ByVal nNivelParam As Integer, ByVal cScanningParam As String, _
        ByVal cArticuloParam As String, ByVal nConteo1Param As Double, ByVal nConteo2Param As Double, _
        ByVal nDiferencia12Param As Double, ByVal nConteo3Param As Double, ByVal cNombreUsuarioParam As String, _
        ByVal nIdInventarioParam As Long, ByVal nIdLocacionParam As Integer, ByVal nIdSoporteParam As Integer, _
        ByVal nNroSoporteParam As Integer, ByVal nIdLetraSoporteParam As Integer, ByVal cIdSectorParam As String, _
        ByVal nCostoParam As Double, ByVal nPrueba1Param As Double, ByVal nPrueba2Param As Double, _
        ByVal nPrueba3Param As Double _
        )
        MyBase.New(0, (cLocacionParam & " - " & cArticuloParam))

        cNombreLocal = String.Empty
        cLocacion = String.Empty
        cSoporte = String.Empty
        nMetro = 0

        nNivel = 0
        cScanning = String.Empty
        cArticulo = String.Empty
        nConteo1 = 0D

        nConteo2 = 0D
        nDiferencia12 = 0D
        nConteo3 = 0D
        cNombreUsuario = String.Empty

        nIdInventario = 0L
        nIdLocacion = 0
        nIdSoporte = 0
        nNroSoporte = 0

        nIdLetraSoporte = 0
        cIdSector = String.Empty
        nCosto = nCostoParam
        nPrueba1 = nPrueba1Param
        nPrueba2 = nPrueba2Param
        nPrueba3 = nPrueba3Param

    End Sub

#End Region


End Class
