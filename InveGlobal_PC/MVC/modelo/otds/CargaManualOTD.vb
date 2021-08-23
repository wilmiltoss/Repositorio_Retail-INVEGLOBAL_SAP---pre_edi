Imports CdgPersistencia.ClasesBases

Public Class CargaManualOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "CargaManualOTD"

    Public cColector As String
    Public nIdInventario As Long
    Public nIdLocacion As Integer
    Public nNroConteo As Integer

    Public nIdSoporte As Integer
    Public nNroSoporte As Integer
    Public nIdLetraSoporte As Integer
    Public nNivel As Integer

    Public nMetro As Integer
    Public cScanning As String
    Public nCantidad As Double
    Public nIdUsuario As Long


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        cColector = String.Empty
        nIdInventario = 0L
        nIdLocacion = 0
        nNroConteo = 0

        nIdSoporte = 0
        nNroSoporte = 0
        nIdLetraSoporte = 0
        nNivel = 0

        nMetro = 0
        cScanning = String.Empty
        nCantidad = 0D
        nIdUsuario = 0L

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="cColectorParam">Nombre del Dispositivo Colector</param>
    ''' <param name="nIdInventarioParam">Identificador del inventario</param>
    ''' <param name="nIdLocacionParam">Identificador de la locacion</param>
    ''' <param name="nNroConteoParam">Nro. de conteo realizado</param>
    ''' <param name="nIdSoporteParam">Identificador del soporte</param>
    ''' <param name="nNroSoporteParam">Nro. del soporte</param>
    ''' <param name="nIdLetraSoporteParam">Identificador de la letra del soporte</param>
    ''' <param name="nNivelParam">Nivel</param>
    ''' <param name="nMetroParam">Metro</param>
    ''' <param name="cScanningParam">Codigo de barras del articulo</param>
    ''' <param name="nCantidadParam">Cantidad registrada</param>
    ''' <param name="nIdUsuarioParam">Identificador del usuario que carga los datos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cColectorParam As String, ByVal nIdInventarioParam As Long _
                    , ByVal nIdLocacionParam As Integer, ByVal nNroConteoParam As Integer _
                    , ByVal nIdSoporteParam As Integer, ByVal nNroSoporteParam As Integer _
                    , ByVal nIdLetraSoporteParam As Integer, ByVal nNivelParam As Integer _
                    , ByVal nMetroParam As Integer, ByVal cScanningParam As String _
                    , ByVal nCantidadParam As Double, ByVal nIdUsuarioParam As Long _
                    )
        MyBase.New(Long.Parse(cScanningParam), nCantidadParam.ToString())

        cColector = cColectorParam
        nIdInventario = nIdInventarioParam
        nIdLocacion = nIdLocacionParam
        nNroConteo = nNroConteoParam

        nIdSoporte = nIdSoporteParam
        nNroSoporte = nNroSoporteParam
        nIdLetraSoporte = nIdLetraSoporteParam
        nNivel = nNivelParam

        nMetro = nMetroParam
        cScanning = cScanningParam
        nCantidad = nCantidadParam
        nIdUsuario = nIdUsuarioParam

    End Sub

#End Region


End Class
