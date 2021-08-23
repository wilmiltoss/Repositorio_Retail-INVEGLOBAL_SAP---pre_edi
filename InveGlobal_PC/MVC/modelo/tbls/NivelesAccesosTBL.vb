Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class NivelesAccesosTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "NivelesAccesosTBL"
    Public Shadows Const NOMBRE_TABLA As String = "NIVELES_ACCESO"

    'nombres de campos
    Public Shared ID As New Campo("ID_NIVEL", 0, False, GetType(Long), True)
    Public Shared DESCRIPCION As New Campo("DESCRIPCION", 1, False, GetType(String))
    Public Shared MOSTRAR As New Campo("MOSTRAR", 2, False, GetType(Boolean))

#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION, MOSTRAR}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
