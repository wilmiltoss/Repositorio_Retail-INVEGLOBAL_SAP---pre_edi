Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class SoportesTBL
    Inherits TBLbase

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "SoportesTBL"
    Public Shadows Const NOMBRE_TABLA As String = "SOPORTES"

    
    'nombres de campos
    Public Shared ID As New Campo("ID_SOPORTE", 0, False, GetType(Long))
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 1, False, GetType(String))
    Public Shared SUB_DIVISIBLE As New Campo("SUBDIVISIBLE", 2, False, GetType(Boolean))


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION, SUB_DIVISIBLE}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
