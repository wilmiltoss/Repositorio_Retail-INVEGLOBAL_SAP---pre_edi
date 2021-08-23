Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Win32

Public Class ArchivosPlanos

#Region "ATRIBUTOS_DE_LA_CLASE"

    Private cNombreArchivo As String = String.Empty
    Private cExtensionArchivo As String = ".txt"
    Private oDialogoGuardarArchivo As SaveFileDialog
    Private cDirectorioHome As String = CurDir()
    Private oArchivo As Object

#End Region

    ''' <summary>
    ''' Constructor por defecto de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="strExtensionArchivo">Extension del tipo de Archivo. Ej. [csv|txt|...]</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal strExtensionArchivo As String)
        'seteamos el atributo correspondiente
        Me.cExtensionArchivo = strExtensionArchivo

    End Sub

    ''' <summary>
    ''' crea el archivo a escribir
    ''' </summary>
    ''' <returns>Devuelve 'True' si se creao correctamente</returns>
    ''' <remarks></remarks>
    Private Function bCrear_Archivo() As Boolean
        'configuramos instancia del del dialogo de guardado de archivo
        oDialogoGuardarArchivo = New SaveFileDialog()
        oDialogoGuardarArchivo.Filter = String.Format("Archivos {0}(*.{1})|*.{1}", Me.cExtensionArchivo.ToUpper, Me.cExtensionArchivo)
        oDialogoGuardarArchivo.InitialDirectory = cDirectorioHome

        'mostramos el cuadro de dialogo para guardar el archivo a generar
        oDialogoGuardarArchivo.ShowDialog()

        'si se selecciono un nombre para el archivo
        If Not oDialogoGuardarArchivo.FileName.Equals(String.Empty) Then
            'llamamos al procedimiento de transcripcion de filas al archivo CSV
            Me.cNombreArchivo = oDialogoGuardarArchivo.FileName

            'intentamos crear y abrir el archivo
            Try
                Me.oArchivo = File.CreateText(cNombreArchivo)

                'Me.oArchivo = File.OpenWrite(cNombreArchivo)

                'devolvemos el resultado del metodo
                bCrear_Archivo = True

            Catch ex As IOException

                'devolvemos el resultado del metodo
                bCrear_Archivo = False

            End Try

        Else
            'si no se selecciono un nombre para el archivo, devolvemos el resultado del metodo
            bCrear_Archivo = False

        End If

    End Function

    ''' <summary>
    ''' Escribe una linea en el archivo
    ''' </summary>
    ''' <param name="strLineaAEscribir">Linea a escribir</param>
    ''' <returns>Devuelve 'True' si se escribio correctamente</returns>
    ''' <remarks></remarks>
    Public Function bEscribir_Linea(ByVal strLineaAEscribir As String) As Boolean
        'si existe el archivo
        If File.Exists(cNombreArchivo) Then
            'intentamos escribir la linea en el archivo
            Try
                oArchivo.WriteLine(strLineaAEscribir)

                'devolvemos el resultado del metodo
                bEscribir_Linea = True

            Catch ex As Exception
                'en caso de error, devolvemos el resultado del metodo
                bEscribir_Linea = False

            End Try

        Else
            'llamamos al metodo de creacion del archivo
            If bCrear_Archivo() Then
                'si se creo llamamos a este mismo metodo y devolvemos el resultado de su ejecucion
                bEscribir_Linea = bEscribir_Linea(strLineaAEscribir)

            Else
                'devolvemos el resultado del metodo
                bEscribir_Linea = False

            End If

        End If

    End Function

    ''' <summary>
    ''' Abre el Archivo que se esta Trabajando con el Programa Predeterminado. Devuelve Boolean
    ''' </summary>
    ''' <returns>Devuvel 'True' si el Archivo se abrio correctamente</returns>
    ''' <remarks></remarks>
    Public Function bAbrir_Archivo() As Boolean

        'si el archivo existe
        If File.Exists(cNombreArchivo) Then
            'intentamos abrir el archivo 
            Try
                Process.Start(Me.cNombreArchivo)

            Catch Ex As Exception
                'en caso de error, devolvemos el resultado del metodo
                bAbrir_Archivo = False

            End Try

        Else
            'devolvemos el resultado del metodo
            bAbrir_Archivo = False

        End If

    End Function

    ''' <summary>
    ''' Cierra el Archivo que se esta trabajando
    ''' </summary>
    ''' <returns>Devuelve 'True' si cerro correctamente</returns>
    ''' <remarks></remarks>
    Public Function bCerrar_Archivo() As Boolean

        'intentamos cerrar el archivo
        Try
            oArchivo.Close()

            'devolvemos el resultado del metodo
            bCerrar_Archivo = True

        Catch ex As Exception
            'devolvemos el resultado del metodo
            bCerrar_Archivo = False

        End Try

    End Function

End Class
