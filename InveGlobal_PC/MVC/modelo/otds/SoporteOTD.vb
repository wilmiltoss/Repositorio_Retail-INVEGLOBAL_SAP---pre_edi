Imports CdgPersistencia.ClasesBases

Public Class SoporteOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "SoporteOTD"

    Public bSubDivisible As Boolean



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        bSubDivisible = False

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Descripcion del objeto</param>
    ''' <param name="bSubDivisibleParam">Si es subdivisible o no (metros y niveles)</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long _
                   , ByVal cDescripcionParam As String, ByVal bSubDivisibleParam As Boolean _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

        bSubDivisible = bSubDivisibleParam

    End Sub

#End Region


End Class
