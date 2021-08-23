Imports System.IO

''' <summary>
''' Manejador de archivos de salida CSV
''' </summary>
''' <remarks></remarks>
Public Class ArchivoMaestroCSV

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "ArchivoMaestroCSV"

    Private __oArchivo As StreamWriter
    Private __cPathArchivo As String
    Private __cNombreArchivo As String

#End Region


    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal cPathDestinoParam As String)

        'formamos el nombre del archivo
        __cNombreArchivo = SystemInformation.ComputerName & String.Format("{0:ddhhmmss}.CSV", DateTime.Now)

        'ensamblamos el directorio de creacion del archivo
        __cPathArchivo = cPathDestinoParam & __cNombreArchivo

        'creamos una nueva instancia del archivo a escribir
        __oArchivo = File.CreateText(__cPathArchivo)

    End Sub

    ''' <summary>
    ''' Devuelve la instancia del archivo actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Archivo() As StreamWriter
        Get
            Return __oArchivo
        End Get
    End Property

    ''' <summary>
    ''' Devuelve el nombre del archivo actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Nombre_archivo() As String
        Get
            Return __cNombreArchivo
        End Get
    End Property

    ''' <summary>
    ''' Devuelve el path absoluto al archivo actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Path_archivo() As String
        Get
            Return __cPathArchivo
        End Get
    End Property

End Class
