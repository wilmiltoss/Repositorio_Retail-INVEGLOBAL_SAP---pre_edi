Imports CdgPersistencia.ClasesBases

Public Class ConfiguracionOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "ConfiguracionOTD"

    Public nConteoMaximo As Integer
    Public bConDecimales As Boolean
    Public bConPesables As Boolean



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nConteoMaximo = 0
        bConDecimales = False
        bConPesables = False

    End Sub


    ' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="cIdParam">Identificador del inventario al que referencia</param>
    ''' <param name="nConteoMaximoParam">Conteo maximo antes de solicitar confirmacion</param>
    ''' <param name="bConDecimalesParam">Si se permiten decimales o no</param>
    ''' <param name="bConPesablesParam">Si se incluyen pesables o no</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long, ByVal nConteoMaximoParam As Decimal, ByVal bConDecimalesParam As Boolean _
                   , ByVal bConPesablesParam As Boolean _
                    )
        MyBase.New(nIdParam, String.Format("CONFIGURACION DEL INVENTARIO #{0}", nIdParam))

        nConteoMaximo = nConteoMaximoParam
        bConDecimales = bConDecimalesParam
        bConPesables = bConPesablesParam

    End Sub



#End Region

#Region "GETTERS Y SETTERS"

    Public Property conteoMaximo() As Integer
        Get
            Return nConteoMaximo
        End Get
        Set(ByVal value As Integer)
            nConteoMaximo = value
        End Set
    End Property

    Public Property conDecimales() As Boolean
        Get
            Return bConDecimales
        End Get
        Set(ByVal value As Boolean)
            bConDecimales = value
        End Set
    End Property

    Public Property conPesables() As Boolean
        Get
            Return bConPesables
        End Get
        Set(ByVal value As Boolean)
            bConPesables = value
        End Set
    End Property

#End Region

End Class
