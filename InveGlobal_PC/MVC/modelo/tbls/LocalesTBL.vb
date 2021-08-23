Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class LocalesTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "LocalesTBL"
    Public Shadows Const NOMBRE_TABLA As String = "LOCALES"

    'nombres de campos
    Public Shared ID As New Campo("ID_LOCAL", 0, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("NOMBRE_LOCAL", 1, False, GetType(String))
    Public Shared ID_SISTEMA As New Campo("ID_SISTEMA", 2, False, GetType(Integer))
    Public Shared ID_EN_SISTEMA As New Campo("ID_EN_SISTEMA", 3, False, GetType(Integer))

    Public Shared ID_TIPO_LOCAL As New Campo("ID_TIPO_LOCAL", 4, False, GetType(Integer))
    Public Shared ID_EMPRESA As New Campo("ID_EMPRESA", 5, False, GetType(Integer))
    Public Shared NOMBRE_CLAVE As New Campo("NOMBRE_CLAVE", 6, False, GetType(String))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION, ID_SISTEMA, ID_EN_SISTEMA, ID_TIPO_LOCAL, ID_EMPRESA, NOMBRE_CLAVE}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
