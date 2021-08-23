Imports CdgPersistencia.ClasesBases

Public Class EntradaColectoraOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "EntradaColectoraOTD"


    Public nIdInventario As Long
    Public nIdLocacion As Integer
    Public nNroConteo As Integer
    Public nIdSoporte As Integer

    Public nNroSoporte As Integer
    Public nIdLetraSoporte As Integer
    Public cScanning As String
    Public nNivel As Integer

    Public nMetro As Integer
    Public nCantidad As Double
    Public cColectora As String
    Public nIdUsuario As Long


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nIdInventario = 0L
        nIdLocacion = 0
        nNroConteo = 0
        nIdSoporte = 0

        nNroSoporte = 0
        nIdLetraSoporte = 0
        cScanning = String.Empty
        nNivel = 0

        nMetro = 0
        nCantidad = 0D
        cColectora = String.Empty
        nIdUsuario = 0L

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdInventarioParam">Identificador del inventario</param>
    ''' <param name="nIdLocacionParam">Identificador de la locacion</param>
    ''' <param name="nNroConteoParam">Nro. de conteo realizado</param>
    ''' <param name="nIdSoporteParam">Identificador del soporte</param>
    ''' <param name="nNroSoporteParam">Nro. del soporte</param>
    ''' <param name="nIdLetraSoporteParam">Identificador de la letra del soporte</param>
    ''' <param name="cScanningParam">Codigo de barras del articulo</param>
    ''' <param name="nNivelParam">Nivel</param>
    ''' <param name="nMetroParam">Metro</param>
    ''' <param name="nCantidadParam">Cantidad registrada</param>
    ''' <param name="cColectorParam">Nombre del Dispositivo Colector</param>
    ''' <param name="nIdUsuarioParam">Identificador del usuario que carga los datos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdInventarioParam As Long, ByVal nIdLocacionParam As Integer _
                   , ByVal nNroConteoParam As Integer, ByVal nIdSoporteParam As Integer _
                   , ByVal nNroSoporteParam As Integer, ByVal nIdLetraSoporteParam As Integer _
                   , ByVal cScanningParam As String, ByVal nNivelParam As Integer _
                   , ByVal nMetroParam As Integer, ByVal nCantidadParam As Double _
                   , ByVal cColectorParam As String, ByVal nIdUsuarioParam As Long _
                    )
        MyBase.New(Long.Parse(cScanningParam), nCantidadParam.ToString())


        nIdInventario = nIdInventarioParam
        nIdLocacion = nIdLocacionParam
        nNroConteo = nNroConteoParam
        nIdSoporte = nIdSoporteParam

        nNroSoporte = nNroSoporteParam
        nIdLetraSoporte = nIdLetraSoporteParam
        cScanning = cScanningParam
        nNivel = nNivelParam

        nMetro = nMetroParam
        nCantidad = nCantidadParam
        cColectora = cColectorParam
        nIdUsuario = nIdUsuarioParam

    End Sub

#End Region


End Class
