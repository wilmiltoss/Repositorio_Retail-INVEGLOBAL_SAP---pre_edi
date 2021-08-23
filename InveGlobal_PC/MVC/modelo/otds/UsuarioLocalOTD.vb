Imports CdgPersistencia.ClasesBases

Public Class UsuarioLocalOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "UsuarioLocalOTD"

    Public Usuario_OTD As UsuarioOTD
    Public Local_OTD As LocalOTD


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)


        Usuario_OTD = New UsuarioOTD()
        Local_OTD = New LocalOTD()

    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="oUsuarioParam">Instancia de UsuarioOTD</param>
    ''' <param name="oLocalParam">Instancia de LocalOTD</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Integer _
                   , ByVal oUsuarioParam As UsuarioOTD _
                   , ByVal oLocalParam As LocalOTD)
        MyBase.New(nIdParam, String.Format("{0} - {1}", oUsuarioParam.Descripcion, oLocalParam.cDescripcion))

        Usuario_OTD = oUsuarioParam
        Local_OTD = oLocalParam

    End Sub



#End Region


End Class
