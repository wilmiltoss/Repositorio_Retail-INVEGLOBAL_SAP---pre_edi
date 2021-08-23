
Imports System.IO

Public Class FrmDetallesUbicaciones

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmDetallesUbicaciones"

    Private __oApControlador As ApControlador

    Private __oLocacion As LocacionOTD
    Private __oSoporte As SoporteOTD

    Private __lLocaciones As List(Of LocacionOTD)
    Private __lSoportes As List(Of SoporteOTD)


    Private __dtTablaDetalles As DataTable
    Private __dtTablaFiltrada As DataTable


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControladorParam">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControladorParam As ApControlador)

        'tomamos la instancia del controlador
        __oApControlador = oApControladorParam

        InitializeComponent()

        'inicializamos el resto de los componentes
        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion de los demas componentes de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        'instanciamos las clases necesarias
        __lLocaciones = New List(Of LocacionOTD)()
        __lSoportes = New List(Of SoporteOTD)()

        'llamamos al metodo de creacion de las tablas de resumenes de conteos
        __crear_tablas_temporales()

        'llamamos al metodo de asignacion de datos a combos
        __cargar_combos()


    End Sub

#End Region



#Region "METODOS"

    ''' <summary>
    ''' Ejecuta la creacion de las tablas temporales a utilizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __crear_tablas_temporales()

        'cursor del mouse a "Esperar.."
        Cursor.Current = Cursors.WaitCursor

        'llamamos al metodo de creacion de las tablas temporales
        Dim lResultado As List(Of Object) = __oApControlador.oApModelo.DetallesConteosTmp_ADM().lCrear_tablas(__oApControlador.Inventario_OTD)

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then
            'desplegamos los datos en la grilla
            __cargar_grilla()

        Else
            'sino, mensaje de notificacion
            __oApControlador.notificar_error(lResultado(1).ToString(), ApControlador.NOMBRE_APLICACION)

        End If

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default


    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE DATOS A LA GRILLA
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_grilla()

        'llamamos al metodo de recuperacion de datos de la vista
        Dim lResultado As List(Of Object) = __oApControlador.oApModelo.VwDetallesConteos_ADM().lGet_elementos(String.Empty)

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then
            'tomamos la tabla devuelta
            __dtTablaDetalles = CType(lResultado(2), DataTable)

            'asignamos la tabla como origen de datos de la grilla
            Me.grd_detalles_ubicaciones.DataSource = __dtTablaDetalles

            'ocultamos las columnas innecesarias
            __ocultar_columnas()

            'llamamos al procedimiento de marcado de diferencias
            '__marcar_diferencias()

        Else
            'sino se ejecuto correctamente, mensaje de notificacion
            __oApControlador.notificar_error(lResultado(1).ToString(), "Despliegue de Detalles de Conteos")

        End If

        'mostramos la cantidad de registro seleccionados
        Me.lbl_estado.Text = Me.grd_detalles_ubicaciones.RowCount.ToString & " Registros..."

    End Sub

    ''' <summary>
    ''' Oculta las columnas innecesarias de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __ocultar_columnas()

        'recorremos los campos de la grilla
        For Each drgColumna As DataGridViewColumn In Me.grd_detalles_ubicaciones.Columns
            'si la columna actual es alguna de las siguientes, la ocultamos
            Select Case drgColumna.HeaderText
                Case VwDetallesConteosTBL.ID_INVENTARIO.cNombre
                    drgColumna.Visible = False
                Case VwDetallesConteosTBL.ID_LOCACION.cNombre
                    drgColumna.Visible = False
                Case VwDetallesConteosTBL.ID_SOPORTE.cNombre
                    drgColumna.Visible = False
                Case VwDetallesConteosTBL.ID_LETRA_SOPORTE.cNombre
                    drgColumna.Visible = False
                Case VwDetallesConteosTBL.ID_SECTOR.cNombre
                    drgColumna.Visible = False
            End Select
        Next

        'refrescamos el form
        Refresh()
        Application.DoEvents()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE VALORES A COMBOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_combos()

        Dim lResultado As List(Of Object)

        'formamos la primera sentencia de filtrado
        Dim cFiltroWhere As String = "INNER JOIN {0} ON {1}.{2} = {0}.{3} WHERE {0}.{4} = {5} GROUP BY {6}"

        'recuperamos la lista de locaciones disponibles
        lResultado = __oApControlador.oApModelo.Locaciones_ADM().lGet_elementos(String.Format(cFiltroWhere _
                                                                                              , DetallesConteosTBL.NOMBRE_TABLA _
                                                                                              , LocacionesTBL.NOMBRE_TABLA _
                                                                                              , LocacionesTBL.ID.cNombre _
                                                                                              , DetallesConteosTBL.ID_LOCACION.cNombre _
                                                                                              , DetallesConteosTBL.ID_INVENTARIO.cNombre _
                                                                                              , __oApControlador.Inventario_OTD.nId _
                                                                                              , (New LocacionesTBL()).Lista_de_campos_tabla() _
                                                                                              ))

        'si se ejecuto crrectamente, tomamos la lista devuelta
        If lResultado(0).Equals(1) Then __lLocaciones = CType(lResultado(1), List(Of LocacionOTD))

        'recuperamos la lista de soportes disponibles
        lResultado = __oApControlador.oApModelo.Soportes_ADM().lGet_elementos(String.Format(cFiltroWhere _
                                                                                              , DetallesConteosTBL.NOMBRE_TABLA _
                                                                                              , SoportesTBL.NOMBRE_TABLA _
                                                                                              , SoportesTBL.ID.cNombre _
                                                                                              , DetallesConteosTBL.ID_SOPORTE.cNombre _
                                                                                              , DetallesConteosTBL.ID_INVENTARIO.cNombre _
                                                                                              , __oApControlador.Inventario_OTD.nId _
                                                                                              , (New SoportesTBL()).Lista_de_campos_tabla() _
                                                                                              ))

        'si se ejecuto correctamente, tomamos la lista devuelta
        If lResultado(0).Equals(1) Then __lSoportes = CType(lResultado(1), List(Of SoporteOTD))

        'insertamos los elementos por defecto
        __lLocaciones.Insert(0, New LocacionOTD(0, "TODOS"))
        __lSoportes.Insert(0, New SoporteOTD(0, "TODOS", False))

        'asignamos los origenes de datos de los combos
        cmb_locaciones.DataSource = __lLocaciones
        cmb_soportes.DataSource = __lSoportes
        cmb_locaciones.DisplayMember = "cDescripcion"
        cmb_soportes.DisplayMember = "cDescripcion"

        'mostramos los elementos de la primera posicion de cada lista
        cmb_locaciones.SelectedIndex = 0
        cmb_soportes.SelectedIndex = 0


    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE GENERACION DE ARCHIVO CSV
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __generar_csv()
        'variables auxiliares
        Dim cNombreArchivo As String = String.Empty
        Dim cDirectorio As String = String.Empty

        'seteamos el cuadro de dialogo
        Me.svf_guardar_archivo.Filter = "Archivos CSV(*.csv)|*.csv"
        Me.svf_guardar_archivo.InitialDirectory = "C:\"

        'mostramos el cuadro de dialogo para guardar el archivo a generar
        Me.svf_guardar_archivo.ShowDialog()

        'llamamos al procedimiento de transcripcion de filas al archivo CSV
        __escribir_csv(Me.svf_guardar_archivo.FileName)

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE TRANSCRIPCION DEL CONTENDO DE LA GRILLA AL ARCHIVO CSV
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __escribir_csv(ByVal cNombreArchivo As String)
        'variables auxiliares
        Dim oArchivoCSV As StreamWriter = Nothing
        Dim bPrimeraFila As Boolean = True
        Dim bPrimeraCelda As Boolean = True
        Dim cLineaTitulos As String = String.Empty
        Dim cLineaCSV As String = String.Empty

        'si el archivo ya existe
        If File.Exists(cNombreArchivo) Then
            Try
                'lo eliminamos
                File.Delete(cNombreArchivo)

            Catch ex As Exception
                'si no se pudo eliminar, mensaje de notificacion
                __oApControlador.notificar_error("Error intentando reemplazar el Archivo CSV: " & cNombreArchivo _
                                                 & Chr(13) & ex.Message, "Archivo CSV")

                'salimos del sub
                Exit Sub

            End Try

        End If

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            'creamos y abrimos el archivo para escritura
            oArchivoCSV = File.CreateText(cNombreArchivo)

            'recorremos todas las filas de la grilla
            For Each dgrFila As DataGridViewRow In Me.grd_detalles_ubicaciones.Rows
                'si la fila actual esta visible
                If dgrFila.Visible Then
                    'si es la primera fila
                    If bPrimeraFila Then
                        'cambiamos el valor de la variable de control de la primera fila
                        bPrimeraFila = False

                        'recorremos las columnas de la grilla
                        For Each dgrColumna As DataGridViewColumn In Me.grd_detalles_ubicaciones.Columns
                            'si es la primera celda
                            If bPrimeraCelda Then
                                'cambiamos el valor de la variable de control de la primera celda
                                bPrimeraCelda = False
                                cLineaTitulos = dgrColumna.HeaderText.Replace(__oApControlador.cSeparadorListas, ".")

                            Else
                                'anadimos el contenido de la celda en la variable de linea a escribir
                                cLineaTitulos += __oApControlador.cSeparadorListas _
                                                & dgrColumna.HeaderText.Replace(__oApControlador.cSeparadorListas, ".")

                            End If

                        Next dgrColumna

                        'escribimos la linea en el archivo CSV
                        oArchivoCSV.WriteLine(cLineaTitulos)

                        'reseteamos el valor de la variable de control de primera celda
                        bPrimeraCelda = True

                        'recorremos todas las celdas de la fila actual
                        For Each dgrCelda As DataGridViewCell In dgrFila.Cells
                            'si es la primera celda
                            If bPrimeraCelda Then
                                'cambiamos el valor de la variable de control de la primera celda
                                bPrimeraCelda = False

                                'guardamos el contenido de la celda en la variable de linea a escribir
                                cLineaCSV = dgrCelda.Value.ToString.Replace(__oApControlador.cSeparadorListas, ".")

                            Else
                                'anadimos el contenido de la celda en la variable de linea a escribir
                                cLineaCSV += __oApControlador.cSeparadorListas _
                                            & dgrCelda.Value.ToString.Replace(__oApControlador.cSeparadorListas, ".")

                            End If

                        Next dgrCelda

                        'escribimos la linea en el archivo CSV
                        oArchivoCSV.WriteLine(cLineaCSV)

                    Else
                        'si no es la primera fila, reseteamos el valor de la variable de control de primera celda
                        bPrimeraCelda = True

                        'recorremos todas las celdas de la fila actual
                        For Each dgrCelda As DataGridViewCell In dgrFila.Cells
                            'si es la primera celda
                            If bPrimeraCelda Then
                                'cambiamos el valor de la variable de control de la primera celda
                                bPrimeraCelda = False

                                'guardamos el contenido de la celda en la variable de linea a escribir
                                cLineaCSV = dgrCelda.Value.ToString.Replace(__oApControlador.cSeparadorListas, ".")

                            Else
                                'anadimos el contenido de la celda en la variable de linea a escribir
                                cLineaCSV += __oApControlador.cSeparadorListas _
                                            & dgrCelda.Value.ToString.Replace(__oApControlador.cSeparadorListas, ".")

                            End If

                        Next dgrCelda

                        'escribimos la linea en el archivo CSV
                        oArchivoCSV.WriteLine(cLineaCSV)

                    End If
                End If
            Next dgrFila

            'cerramos el archivo CSV
            oArchivoCSV.Close()

            'notificacion de exito
            __oApControlador.notificar_exito("Archivo Generado Exitosame!", "Archivo CSV")

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error("Error intentando escribir en Archivo CSV " & Chr(13) & ex.Message, "Archivo CSV")

            Try
                'intentamos cerrar el archivo
                oArchivoCSV.Close()
            Catch Ex2 As Exception
                'en caso de error, nada
            End Try

        End Try

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default


    End Sub

    ''' <summary>
    ''' MARCA CON COLOR LAS CELDAS DE LA COLUMNA DEL CAMPO [DIFERENCIA_1_2] QUE SEAN DIFERENTES DE CERO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __marcar_diferencias()

        'recorremos las filas de la grilla
        For Each drwFilaActual As DataGridViewRow In Me.grd_detalles_ubicaciones.Rows
            'si el calor del campo de "[DIFERENCIA_1_2]" es diferente a cero
            If Not Val(drwFilaActual.Cells(VwDetallesConteosTBL.DIFERENCIA_1_2.cNombre).Value) = 0 Then
                'cambiamos el color de fondo de la columna de la celda
                drwFilaActual.Cells(VwDetallesConteosTBL.DIFERENCIA_1_2.cNombre).Style.BackColor = Color.Red
            End If
        Next drwFilaActual
    End Sub


#End Region


#Region "EVENTOS"

    ''' <summary>
    ''' Cuando se hace click en el boton de busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_buscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_buscar.Click

        'si NO hay filas en la tabla, salimos del metodo
        If __dtTablaDetalles.Rows.Count = 0 Then Return

        'tomamos la locacion y el soporte seleccionado
        __oLocacion = CType(cmb_locaciones.SelectedItem, LocacionOTD)
        __oSoporte = CType(cmb_soportes.SelectedItem, SoporteOTD)

        'limpiamos la grilla y la tabla auxiliar
        Me.grd_detalles_ubicaciones.DataSource = Nothing
        __dtTablaFiltrada = __dtTablaDetalles.Clone()

        'variable para filtro de registros
        Dim cFiltroFilas As String = String.Empty

        'si se filtra por Locacion
        If __oLocacion.nId > 0 Then
            'agregamos la condicion de filtrado
            cFiltroFilas = String.Format("{0} = {1}", VwDetallesConteosTBL.ID_LOCACION.cNombre, __oLocacion.nId)
        End If

        'si se filtra por Soporte 
        If __oSoporte.nId > 0 Then
            'si no hay condiciones previas
            If Not cFiltroFilas = String.Empty Then
                'agregamos la condicion de filtrado
                cFiltroFilas += String.Format(" and {0} = {1}", VwDetallesConteosTBL.ID_SOPORTE.cNombre, __oSoporte.nId)
            Else
                'agregamos la condicion de filtrado
                cFiltroFilas = String.Format("{0} = {1}", VwDetallesConteosTBL.ID_SOPORTE.cNombre, __oSoporte.nId)
            End If

            'el nro de soporte
            cFiltroFilas += String.Format(" and {0} = {1}", VwDetallesConteosTBL.NRO_SOPORTE.cNombre, Me.num_nro_soporte.Value)

        End If

        'si se filtra por Prueba
        If Me.chk_modo_prueba.Checked Then
            'si no hay condiciones previas
            If Not cFiltroFilas = String.Empty Then
                'agregamos la condicion de filtrado
                cFiltroFilas += String.Format(" and ({0} > {1} or {2} > {1} or {3} > {1})" _
                                              , VwDetallesConteosTBL.PRUEBA_1.cNombre _
                                              , 0 _
                                              , VwDetallesConteosTBL.PRUEBA_2.cNombre _
                                              , VwDetallesConteosTBL.PRUEBA_3.cNombre _
                                              )
            Else
                'agregamos la condicion de filtrado
                cFiltroFilas = String.Format("{0} > {1} or {2} > {1} or {3} > {1}" _
                                              , VwDetallesConteosTBL.PRUEBA_1.cNombre _
                                              , 0 _
                                              , VwDetallesConteosTBL.PRUEBA_2.cNombre _
                                              , VwDetallesConteosTBL.PRUEBA_3.cNombre _
                                              )
            End If
        End If

        'si se filtra por Metro
        If Me.num_metro.Value > 0 Then
            'si no hay condiciones previas
            If Not cFiltroFilas = String.Empty Then
                'agregamos la condicion de filtrado
                cFiltroFilas += String.Format(" and {0} = {1}", VwDetallesConteosTBL.METRO.cNombre, Me.num_metro.Value)
            Else
                'agregamos la condicion de filtrado
                cFiltroFilas = String.Format("{0} = {1}", VwDetallesConteosTBL.METRO.cNombre, Me.num_metro.Value)
            End If
        End If

        'si se filtra por Nivel
        If Me.num_nivel.Value > 0 Then
            'si no hay condiciones previas
            If Not cFiltroFilas = String.Empty Then
                'agregamos la condicion de filtrado
                cFiltroFilas += String.Format(" and {0} = {1}", VwDetallesConteosTBL.NIVEL.cNombre, Me.num_nivel.Value)
            Else
                'agregamos la condicion de filtrado
                cFiltroFilas = String.Format("{0} = {1}", VwDetallesConteosTBL.NIVEL.cNombre, Me.num_nivel.Value)
            End If
        End If

        'si hay una condicion de filtrado
        If cFiltroFilas.Trim.Length > 0 Then
            'filtramos las filas por este campo
            For Each dr As DataRow In __dtTablaDetalles.Select(cFiltroFilas)
                __dtTablaFiltrada.ImportRow(dr)
            Next

            'establememos el origen de datos de la grilla
            Me.grd_detalles_ubicaciones.DataSource = __dtTablaFiltrada

        Else
            'sino, mostramos toda la tabla original
            Me.grd_detalles_ubicaciones.DataSource = __dtTablaDetalles

        End If

        'mostramos la cantidad de registro seleccionados
        Me.lbl_estado.Text = Me.grd_detalles_ubicaciones.RowCount.ToString & " Registros..."

    End Sub

    ''' <summary>
    ''' CUANDO SE CIERRA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_detalles_ubicaciones_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'evento a LOG
        __oApControlador.log().Escribir("Se cierra el formulario " & Me.Text)
    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL TAMANO DEL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_detalles_ubicaciones_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'variables auxiliares
        Dim nX, nY, nX2, nY2, nAncho, nAlto As Integer

        'seteamos las variables
        nX = Me.grd_detalles_ubicaciones.Location.X
        nY = Me.grd_detalles_ubicaciones.Location.Y

        nX2 = nX
        nY2 = 58

        'calculamos los nuevos ancho y alto posibles
        nAncho = Me.Size.Width - (nX + nX2 * 2)
        nAlto = Me.Size.Height - (nY + nY2)

        'ajustamos el tamano de la grilla al del formulario
        Me.grd_detalles_ubicaciones.Size = New Size(nAncho, nAlto)

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Planilla de Diferencias"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_planilla_diferencias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_planilla_diferencias.Click
        'mostramos el formulario de criterios de impresion de planillas de diferencias
        'frm_rpt_diferencias.ShowDialog()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTONO CON EL LOGO DE "Ms Excel"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_excel.Click
        'llamamos a procedimiento de generacion de archivo CSV
        Me.__generar_csv()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK SOBRE LAS CABECERAS DE LAS COLUMNAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_detalles_ubicaciones_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd_detalles_ubicaciones.ColumnHeaderMouseClick
        'llamamos al procedimiento de marcado de diferencias
        __marcar_diferencias()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Exportar a CSV"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_exportar_csv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_exportar_csv.Click
        'llamamos al metodo CLICK del boton con el logo de "Ms Excel"
        Me.cmd_excel_Click(sender, e)

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir.Click
        'cerramos el formulario
        Me.Close()
        Me.Dispose()

    End Sub


#End Region


    
End Class