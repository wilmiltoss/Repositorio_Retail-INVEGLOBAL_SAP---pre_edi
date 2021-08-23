Imports CdgPersistencia.ClasesBases

Public Class SistemaOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "SistemaOTD"

    Public cUsuarioSistema As String
    Public cIpServidor As String
    Public cNombreServicio As String
    Public nPuertoServicio As Integer
    Public cPassword As String



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        cUsuarioSistema = String.Empty
        cIpServidor = String.Empty
        cNombreServicio = String.Empty
        nPuertoServicio = 0
        cPassword = String.Empty

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Nombre del Sistema</param>
    ''' <param name="cUsuarioParam">Usuario de conexion a sistema</param>
    ''' <param name="cIpParam">Ip del Servidor</param>
    ''' <param name="cServicioParam">Nombre del servicio</param>
    ''' <param name="nPuertoParam">Puerto de Escucha</param>
    ''' <param name="cPasswordParam">Contrasena de acceso</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Integer, ByVal cDescripcionParam As String _
                   , ByVal cUsuarioParam As String, ByVal cIpParam As String _
                   , ByVal cServicioParam As String, ByVal nPuertoParam As Integer, ByVal cPasswordParam As String)
        MyBase.New(nIdParam, cDescripcionParam)

        cUsuarioSistema = cUsuarioParam
        cIpServidor = cIpParam
        cNombreServicio = cServicioParam
        nPuertoServicio = nPuertoParam
        cPassword = cPasswordParam

    End Sub

#End Region


End Class
