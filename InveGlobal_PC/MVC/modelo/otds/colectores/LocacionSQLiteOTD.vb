Imports CdgPersistencia.ClasesBases

Public Class LocacionSQLiteOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "LocacionSQLiteOTD"



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
    ''' <param name="cDescripcionParam">Descripcion del objeto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long _
                   , ByVal cDescripcionParam As String _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

    End Sub

#End Region


End Class
