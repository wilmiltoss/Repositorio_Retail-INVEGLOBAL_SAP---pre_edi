Imports System.IO

Module CSV
#Region "DECLARACIONES"
    Private cNombreCVS As String = String.Empty
    Private cNombreArchivo As String = String.Empty
    Private oArchivoCSV As StreamWriter

#End Region

    ''' <summary>
    ''' CREA UN NUEVO ARCHIVO CSV, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="cDirectorioCVS">Directorio Absoluto donde Residira el Archivo</param>
    ''' <param name="cNombreArchivoCVS">Nombre del Archivo CSV a crear</param>
    ''' <returns>Devuelve 'True' si se Creo el Archivo</returns>
    ''' <remarks></remarks>
    Public Function bCrearCVS(ByVal cDirectorioCVS As String, ByVal cNombreArchivoCVS As String) As Boolean
        'variables a utilizar
        Dim cNombreFinal As String = String.Empty

        'si el directorio de destino no existe
        If Not Directory.Exists(cDirectorioCVS) Then
            'lo intentamos crear
            Try
                Directory.CreateDirectory(cDirectorioCVS)

            Catch Ex As Exception
                'en caso de error, mensaje
                MessageBox.Show("Error Intentando Crear Directorio para CSV", "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'devolvemos el resultado de la funcion
                Return False

            End Try

        End If

        'si el Directorio del Archivo Incluye la "\" al final, se la dejamos, sino se la incluimos
        cDirectorioCVS = IIf(cDirectorioCVS.EndsWith(Chr(92)), cDirectorioCVS, cDirectorioCVS & Chr(92))

        'si el nombre del Archivo incluye la "\" al inicio, se lo extremos
        cNombreArchivo = IIf(cNombreArchivoCVS.StartsWith(Chr(92)), cNombreArchivoCVS.Substring(1), cNombreArchivoCVS)

        'ensamblamos el Directorio Absoluto del Archivo
        cNombreFinal = cDirectorioCVS & cNombreArchivo

        Try
            'si el archivo ya existe, lo eliminamos
            If File.Exists(cNombreFinal) Then File.Delete(cNombreFinal)

            'intentamos crear el archivo
            oArchivoCSV = File.CreateText(cNombreFinal)

            'lo cerramos de vuelta
            oArchivoCSV.Close()

            'establecemos el nombre del archivo a manipular 
            cNombreCVS = cNombreFinal

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As Exception
            'en caso de error, mensajes de notificaciones
            MessageBox.Show("Error intentando Crear Archivo : " & cNombreFinal, "Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("Detalles del Error : " & Ex.Message, "Archivo CSV", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' ABRE EL ARCHIVO CSV Y LO PREPARA PARA ESCRIBIR EN EL, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Abrio correctamente</returns>
    ''' <remarks></remarks>
    Public Function bAbrirCSV() As Boolean
        'si el archivo a escribir existe
        If File.Exists(cNombreCVS) Then
            'intentamos abrir el archivo en modo de escritura
            Try
                oArchivoCSV = File.AppendText(cNombreCVS)

                'devolvemos el resultado de la funcion
                Return True

            Catch Ex As IOException
                'en caso de error, mensajes de notificaciones
                MessageBox.Show("Error Intentando Abrir CSV : " & cNombreCVS, "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'devolvemos el resultado de la funcion
                Return False

            Catch Ex As Exception
                'en caso de error, mensaje
                MessageBox.Show("Error Intentando Abrir CSV : " & cNombreCVS, "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("Error Encontrado : " & Ex.Message, "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'devolvemos el resultado de la funcion
                Return False

            End Try

        Else
            'si no existe, mensaje de notificacion
            MessageBox.Show("Error Intentando Abrir CSV.. " & Chr(13) & "El archivo : " & cNombreCVS & " No Existe!", "Abrir CSV", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'devolvemos el resultado de la funcion
            Return False

        End If

    End Function

    ''' <summary>
    ''' ESCRIBE UNA LINEA EN EL ARCHIVO CSV Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="cLineaAEscribir">Cadena a Escribir en el Archivo</param>
    ''' <returns>Devuelve 'True' si se Escribio Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bEscribirEnCSV(ByVal cLineaAEscribir As String) As Boolean
        'si el archivo a escribir existe
        If File.Exists(cNombreCVS) Then
            'intentamos escribir en el
            Try
                oArchivoCSV.WriteLine(cLineaAEscribir)

                'devolvemos el resultado de la funcion
                Return True

            Catch Ex As Exception
                'en caso de error, mensaje
                'MessageBox.Show("Error Intentando Escribir en CSV", "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de Procedimiento", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'devolvemos el resultado de la funcion
                Return False

            End Try

        Else
            'devolvemos el resultado de la funcion
            Return False

        End If

    End Function

    ''' <summary>
    ''' CIERRA EL ARCHIVO CSV Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Cerro Normalmente</returns>
    ''' <remarks></remarks>
    Public Function bCerrarCSV()
        'intentamos cerrar el archivo CSV
        Try
            oArchivoCSV.Close()

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As Exception
            'en caso de error, mensaje
            MessageBox.Show("Error Intentando Cerrar CSV : " & cNombreCVS, "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' COPIA EL ARCHIVO CSV AL DIRECTORIO ESPECIFICADO Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="cDestinoCSV">Directorio de Destino del CSV</param>
    ''' <returns>Devuelve 'True' si se Copio Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bCopiarCSV(ByVal cDestinoCSV As String) As Boolean
        'variables a utilizar
        Dim cNombreFinal As String = String.Empty

        'si el directorio de destino no existe
        If Not Directory.Exists(cDestinoCSV) Then
            'lo intentamos crear
            Try
                Directory.CreateDirectory(cDestinoCSV)

            Catch Ex As Exception
                'en caso de error, mensaje
                MessageBox.Show("Error Intentando Crear Directorio Destino para CSV", "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)
                MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)

                'devolvemos el resultado de la funcion
                Return False

            End Try

        End If

        'si el Directorio de Destino Incluye la "\" al final, se la dejamos, sino se la incluimos
        cDestinoCSV = IIf(cDestinoCSV.EndsWith(Chr(92)), cDestinoCSV, cDestinoCSV & Chr(92))

        'ensamblamos el Directorio Absoluto del Archivo
        cNombreFinal = cDestinoCSV & cNombreArchivo

        'intententamos copiar el archivo
        Try
            'si el archivo ya existe, lo eliminamos
            If File.Exists(cNombreFinal) Then File.Delete(cNombreFinal)

            File.Copy(cNombreCVS, cNombreFinal, True)

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As IOException
            'en caso de error, mensajes de notificaciones
            MessageBox.Show("Error Intentando Copiar CSV : " & cNombreCVS & " a " & cNombreFinal, "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'devolvemos el resultado de la funcion
            Return False

        Catch Ex As Exception
            'en caso de error, mensaje
            MessageBox.Show("Error Intentando Copiar CSV : " & cNombreCVS & " a " & cNombreFinal, "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("Detalles del Error : " & Ex.Message, "Error de Procedimiento CSV", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

End Module
