Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class ConfiguracionesTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ConfiguracionesTBL"
    Public Shadows Const NOMBRE_TABLA As String = "CONFIGURACIONES"

    'nombres de campos
    Public Shared ID As New Campo("ID_INVENTARIO", 0, False, GetType(Long))
    Public Shared CONTEO_MAXIMO As New Campo("CONTEO_MAXIMO", 1, False, GetType(Decimal))
    Public Shared CONTEO_CON_DECIMALES As New Campo("CONTEO_CON_DECIMALES", 2, False, GetType(Boolean))
    Public Shared PESABLES_INCLUIDOS As New Campo("PESABLES_INCLUIDOS", 3, False, GetType(Boolean))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, CONTEO_MAXIMO, CONTEO_CON_DECIMALES, PESABLES_INCLUIDOS}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
