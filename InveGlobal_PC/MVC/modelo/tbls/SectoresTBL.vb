Imports CdgPersistencia.ClasesBases
Imports CdgPersistencia.Componentes

Public Class SectoresTBL
    Inherits TBLbase

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "SectoresTBL"
    Public Shadows Const NOMBRE_TABLA As String = "SECTORES"

    'nombres de campos
    Public Shared ID As New Campo("ID_SECTOR", 0, False, GetType(String))
    Public Shared DESCRIPCION As New Campo("NOMBRE_SECTOR", 1, False, GetType(String))
    Public Shared NIVEL As New Campo("NIVEL", 2, False, GetType(Integer))
    Public Shared ID_SECTOR_PADRE As New Campo("ID_SECTOR_PADRE", 3, False, GetType(String))



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'asignamos la coleccion de campos
        CAMPOS = New Campo() {ID, DESCRIPCION, NIVEL, ID_SECTOR_PADRE}

    End Sub



#End Region


    Public Overrides Function Get_nombre_tabla() As String
        Return NOMBRE_TABLA
    End Function


End Class
