Imports CdgPersistencia.ClasesBases

Public Class InventarioOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "InventarioOTD"


    Public dFechaInventario As DateTime
    Public Local_OTD As LocalOTD
    Public Sistema_OTD As SistemaOTD

    Public nUltimoConteo As Integer
    Public nConteoAplicado As Integer
    Public bCerrado As Boolean
    Public Usuario_OTD As UsuarioOTD

    Public lSectores As List(Of SectorOTD)
    Public Configuracion_OTD As ConfiguracionOTD




#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        dFechaInventario = DateTime.Today
        Local_OTD = New LocalOTD()
        Sistema_OTD = New SistemaOTD()

        nUltimoConteo = 0
        nConteoAplicado = 0
        bCerrado = False
        Usuario_OTD = New UsuarioOTD()

        lSectores = New List(Of SectorOTD)
        Configuracion_OTD = New ConfiguracionOTD()

    End Sub


    Public Sub New(ByVal nIdParam As Long, ByVal cComentarioParam As String _
                   , ByVal dFechaInventarioParam As DateTime, ByVal oLocalParam As LocalOTD _
                   , ByVal oSistemaParam As SistemaOTD _
                   , ByVal oUsuarioParam As UsuarioOTD)
        MyBase.New(nIdParam, cComentarioParam)

        dFechaInventario = dFechaInventarioParam
        Local_OTD = oLocalParam
        Sistema_OTD = oSistemaParam

        nUltimoConteo = 0
        nConteoAplicado = 0
        bCerrado = False
        Usuario_OTD = oUsuarioParam

        lSectores = New List(Of SectorOTD)
        Configuracion_OTD = New ConfiguracionOTD()

    End Sub

#End Region


End Class
