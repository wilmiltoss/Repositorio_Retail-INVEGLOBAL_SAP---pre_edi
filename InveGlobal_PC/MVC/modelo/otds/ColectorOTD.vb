Public Class ColectorOTD

    Private __nId As Integer
    Private __cNombreColector, __cDireccionIP As String


    ''' <summary>
    ''' Constructor por defecto
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()


    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nId">Identificador de la instancia</param>
    ''' <param name="cNombreColector">Nombre del Dispositivo</param>
    ''' <param name="cDireccionIP">Direccion IP del Dispositivo</param>
    ''' <remarks></remarks>
    Public Sub New(nId As Integer, cNombreColector As String, cDireccionIP As String)

        __nId = nId
        __cNombreColector = cNombreColector
        __cDireccionIP = cDireccionIP


    End Sub

    ''' <summary>
    ''' Devuelve o establece el Nombre asignado al Equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NombreColector() As String
        Get
            Return __cNombreColector
        End Get
        Set(ByVal value As String)
            __cNombreColector = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece la Direccion IP del Equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DireccionIP() As String
        Get
            Return __cDireccionIP
        End Get
        Set(ByVal value As String)
            __cDireccionIP = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece el Identificador del Equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Id() As Integer
        Get
            Return __nId
        End Get
        Set(ByVal value As Integer)
            __nId = value
        End Set
    End Property

    Shadows Property ToString() As String
        Get
            If __cNombreColector <> Nothing And __cDireccionIP <> Nothing Then
                Return String.Format("{0} - {1}", __cNombreColector, __cDireccionIP)
            End If
            Return MyBase.ToString()

        End Get
        Set(ByVal value As String)
        End Set
    End Property

End Class
