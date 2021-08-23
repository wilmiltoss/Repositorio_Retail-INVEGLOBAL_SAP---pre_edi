Imports CdgPersistencia.ClasesBases

Public Class SoporteSQLiteOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "SoporteSQLiteOTD"

    Public nSubDivisible As Integer



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nSubDivisible = False

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Descripcion del objeto</param>
    ''' <param name="bSubDivisibleParam">Si es subdivisible o no [0|1] (metros y niveles)</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long _
                   , ByVal cDescripcionParam As String, ByVal nSubDivisibleParam As Integer _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

        nSubDivisible = nSubDivisibleParam

    End Sub

#End Region


End Class
