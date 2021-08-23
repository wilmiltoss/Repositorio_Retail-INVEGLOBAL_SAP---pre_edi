Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class LocacionesTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "LocacionesTBL"
    Public Shadows Const NOMBRE_TABLA As String = "LOCACIONES"

    
    'nombres de campos
    Public Shared ID As New Campo("ID_LOCACION", 0, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 1, False, GetType(String))


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


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
