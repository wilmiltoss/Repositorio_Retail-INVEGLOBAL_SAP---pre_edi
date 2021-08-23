Imports CdgPersistencia.ClasesBases

Public Class EmpresaOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "EmpresaOTD"


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Nombre del local</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Integer, ByVal cDescripcionParam As String)
        MyBase.New(nIdParam, cDescripcionParam)

    End Sub

#End Region


End Class
