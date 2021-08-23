Imports CdgPersistencia.ClasesBases

Public Class LocalOTD
    Inherits OTDbase

    Public Shadows Const NOMBRE_CLASE As String = "LocalOTD"


    Public Sistema_OTD As SistemaOTD
    Public nIdEnSistema As Integer
    Public nIdTipoLocal As Integer
    Public Empresa_OTD As EmpresaOTD
    Public cNombreClave As String


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Construtor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New(0, NOMBRE_CLASE)

        Sistema_OTD = New SistemaOTD()
        nIdEnSistema = 0
        nIdTipoLocal = 0
        Empresa_OTD = New EmpresaOTD()

        cNombreClave = String.Empty

    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="nIdParam">Identificador del objeto</param>
    ''' <param name="cDescripcionParam">Nombre del local</param>
    ''' <param name="oSistemaParam">Instancia del Sistema de Gestion asociado</param>
    ''' <param name="nIdEnSistemaParam">Identificador del local en el sistema de gestion</param>
    ''' <param name="nIdTipoLocalPAram">Identificador de tipo de local</param>
    ''' <param name="oEmpresaParam">Instancia de Empresa a la que pertenece el local</param>
    ''' <param name="cNombreClaveParam">Nombre clave del local para otros usos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nIdParam As Integer, ByVal cDescripcionParam As String _
                    , ByVal oSistemaParam As SistemaOTD, ByVal nIdEnSistemaParam As Integer _
                    , ByVal nIdTipoLocalPAram As Integer, ByVal oEmpresaParam As EmpresaOTD _
                    , ByVal cNombreClaveParam As String _
                    )
        MyBase.New(nIdParam, cDescripcionParam)

        Sistema_OTD = oSistemaParam
        nIdEnSistema = nIdEnSistemaParam
        nIdTipoLocal = nIdTipoLocalPAram
        Empresa_OTD = oEmpresaParam

        cNombreClave = cNombreClaveParam

    End Sub

#End Region


End Class
