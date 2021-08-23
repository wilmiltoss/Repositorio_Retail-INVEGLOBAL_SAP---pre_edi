Imports CdgPersistencia.ClasesBases

Public Class UsuarioSQLiteOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "UsuarioSQLiteOTD"


    Public nNivelAcceso As Integer
    Public aContrasena As Byte()


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        nNivelAcceso = 0
        aContrasena = Nothing

    End Sub


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Nombre del Usuario</param>
    ''' <param name="nNivelAccesoParam">Nivel de acceso del usuario</param>
    ''' <param name="aContrasenaParam">Contrasena de acceso</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Long, ByVal cDescripcionParam As String _
                   , ByVal nNivelAccesoParam As Integer, ByVal aContrasenaParam As Byte() _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

        nNivelAcceso = nNivelAccesoParam
        aContrasena = aContrasenaParam


    End Sub

#End Region


End Class
