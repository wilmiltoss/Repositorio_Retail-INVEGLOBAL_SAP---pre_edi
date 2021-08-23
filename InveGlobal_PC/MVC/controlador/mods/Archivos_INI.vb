Public Class ArchivosIni
    ' Funciones API
    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As System.Text.StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Ansi Function GetPrivateProfileInt Lib "kernel32.dll" Alias "GetPrivateProfileIntA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
    Private Declare Ansi Function FlushPrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As Integer, ByVal lpKeyName As Integer, ByVal lpString As Integer, ByVal lpFileName As String) As Integer
    Dim strFilename As String

    ' Constructor, acepta un nombre de fichero (si no existe se creará)
    Public Sub New(ByVal Filename As String)
        strFilename = Filename

    End Sub

    ' Propiedad para Read-only
    ReadOnly Property FileName() As String
        Get
            Return strFilename

        End Get

    End Property
    Public Function leer_ini(ByVal Seccion As String, ByVal Clave As String, ByVal [Default] As String) As String
        ' Devuelve una cadena desde tu archivo INI
        Dim intCharCount As Integer
        Dim objResult As New System.Text.StringBuilder(256)

        intCharCount = GetPrivateProfileString(Seccion, Clave, [Default], objResult, objResult.Capacity, strFilename)

        If intCharCount > 0 Then
            leer_ini = Left(objResult.ToString, intCharCount)
        Else
            leer_ini = [Default]
        End If


    End Function
    Public Function ObtenerInteger(ByVal Seccion As String, ByVal Clave As String, ByVal [Default] As Integer) As Integer
        ' Devuelve un número desde tu archivo INI
        Return GetPrivateProfileInt(Seccion, Clave, [Default], strFilename)

    End Function
    Public Function ObtenerBoolean(ByVal Seccion As String, ByVal Clave As String, ByVal [Default] As Boolean) As Boolean
        ' Devuelve un valo lógico desde un archivo INI
        Return leer_ini(Seccion, Clave, [Default])

    End Function
End Class
