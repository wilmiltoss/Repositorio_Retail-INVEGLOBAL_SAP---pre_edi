Imports CdgPersistencia.ClasesBases

Public Class SectorOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "SectorOTD"

    Public Const TODOS As Long = -1

    Public cIdSector As String
    Public nNivel As Integer
    Public cIdSectorPadre As String



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        cIdSector = "0"
        nNivel = 0
        cIdSectorPadre = "0"

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="cIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Nombre del Sector</param>
    ''' <param name="nNivelParam">Nivel de Sectorizacion</param>
    ''' <param name="cIdSectorPadreParam">Identificador del Sector Padre</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cIdParam As String, ByVal cDescripcionParam As String _
                    , ByVal nNivelParam As Integer, ByVal cIdSectorPadreParam As String _
                    )
        MyBase.New(Long.Parse(cIdParam), cDescripcionParam)

        cIdSector = cIdParam
        nNivel = nNivelParam
        cIdSectorPadre = cIdSectorPadreParam

    End Sub

#End Region


End Class
