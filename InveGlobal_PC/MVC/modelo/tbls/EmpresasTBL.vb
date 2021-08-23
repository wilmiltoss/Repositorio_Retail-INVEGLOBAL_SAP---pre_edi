Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes


Public Class EmpresasTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "EmpresasTBL"
    Public Shadows Const NOMBRE_TABLA As String = "EMPRESAS"

    'nombres de campos
    Public Shared ID As New Campo("ID_EMPRESA", 0, False, GetType(Integer))
    Public Shared DESCRIPCION As New Campo("NOMBRE_EMPRESA", 1, False, GetType(String))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION}

    End Sub



#End Region

    ''' <summary>
    ''' Devuelve el nombre de la tabla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
