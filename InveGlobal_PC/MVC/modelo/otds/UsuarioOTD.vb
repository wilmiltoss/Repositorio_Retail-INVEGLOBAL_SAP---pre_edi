Imports CdgPersistencia.ClasesBases

Public Class UsuarioOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "UsuarioOTD"

    
    Private __aContrasena As Byte()
    Private __NivelAcceso_OTD As NivelAccesoOTD
    Private __bHabilitado As Boolean
    Private __Locales As List(Of LocalOTD)


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        __aContrasena = Nothing
        __NivelAcceso_OTD = New NivelAccesoOTD()
        __bHabilitado = False
        __Locales = New List(Of LocalOTD)

    End Sub

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Nombre del usuario</param>
    ''' <param name="aContrasenaParam">Contrasena de acceso</param>
    ''' <param name="oNivelAccesoParam">Instancia de Nivel de Acceso asociado</param>
    ''' <param name="bHabilitadoParam">Si esta habilitado o no</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long, ByVal cDescripcionParam As String _
                    , ByVal aContrasenaParam As Byte(), ByVal oNivelAccesoParam As NivelAccesoOTD _
                    , ByVal bHabilitadoParam As Boolean)
        MyBase.New(nIdParam, cDescripcionParam)

        __aContrasena = aContrasenaParam
        __NivelAcceso_OTD = oNivelAccesoParam
        __bHabilitado = bHabilitadoParam
        __Locales = New List(Of LocalOTD)

    End Sub


#End Region


#Region "GETTERS Y SETTERS"

    ''' <summary>
    ''' Devuelve o establece el identificador del objeto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Id() As Long
        Get
            Return nId
        End Get
        Set(ByVal value As Long)
            nId = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece el nombre del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Descripcion() As String
        Get
            Return cDescripcion
        End Get
        Set(ByVal value As String)
            cDescripcion = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece la contrasena del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Contrasena() As Byte()
        Get
            Return __aContrasena
        End Get
        Set(ByVal value As Byte())
            __aContrasena = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece el nivel de acceso del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NivelAcceso() As NivelAccesoOTD
        Get
            Return __NivelAcceso_OTD
        End Get
        Set(ByVal value As NivelAccesoOTD)
            __NivelAcceso_OTD = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece el estado de habilitacion del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Habilitado() As Boolean
        Get
            Return __bHabilitado
        End Get
        Set(ByVal value As Boolean)
            __bHabilitado = value
        End Set
    End Property

    Public Property Locales() As List(Of LocalOTD)
        Get
            Return __Locales
        End Get
        Set(ByVal value As List(Of LocalOTD))
            __Locales = value
        End Set
    End Property

#End Region



End Class
