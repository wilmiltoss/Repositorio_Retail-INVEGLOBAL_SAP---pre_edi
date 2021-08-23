
Public Class frm_despliega_existencias

#Region "VARIABLES_LOCALES"
    Private cListaBasicaCampos As String = String.Empty
    Private cConsultaPrincipal As String = String.Empty
    Private cConsultaAuxiliar As String = String.Empty
    Private cCondicion As String = String.Empty
    Private cTipoDatoCampo As String = String.Empty

    Private bReplegarArbol As Boolean = False
    Private bConteoaplicado As Boolean = False

    Private cControlOrigen As String = String.Empty
#End Region

    ''' <summary>
    ''' CUANDO SE ESTA CERRANDO EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_despliega_existencias_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'cerramos la aplicacion excel que este abierta
        MS_EXCEL.proCerrarExcel()

        Me.Dispose()

    End Sub

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_despliega_existencias_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evaluamos cual es el nro de conteo aplicado
        Select Case nNro_Conteo_Aplicado
            Case 1
                'marcamos la opcion 1
                Me.opt_conteo_1.Checked = True
                bConteoaplicado = True

            Case 2
                'marcamos la opcion 2
                Me.opt_conteo_2.Checked = True
                bConteoaplicado = True

            Case 3
                'marcamos la opcion 3
                Me.opt_conteo_3.Checked = True
                bConteoaplicado = True

            Case Else
                'sino, ninguno de los anteriores
                Me.opt_conteo_1.Checked = False
                Me.opt_conteo_2.Checked = False
                Me.opt_conteo_3.Checked = False
                bConteoaplicado = False

        End Select
        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'establecemos la lista basica de campos a desplegar
        Me.cListaBasicaCampos = Me.cLista_Campos_Basicos()

        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT " & Me.cListaBasicaCampos _
                          & " FROM [VW_EXISTENCIAS] " _
                          & " WHERE [ID_INVENTARIO]= " & cID_Inventario

        'llamamos a funcion de carga de datos a grilla
        If Me.bCargarGrilla(cSentenciaSQL) Then
            'si se ejecuto correctamente, tomamos la Consulta SQL como consulta principal
            Me.cConsultaPrincipal = cSentenciaSQL

            'llamamos a funcion de carga de combo de campos
            Me.pCargarCampos()

            'llamamos a procedimiento de carga de Nodos del Arbol de Sectores
            Me.pCrear_Arbol_Sectores()

            'tamano del arbol de sectores a 0:anchoGrilla
            Me.tvw_arbol.Size = New Size(0, Me.grd_existencias.Size.Height)

        End If

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' OBTIENE LA LISTA DE CAMPOS A DESPLEGAR EN LA GRILLA DE EXISTENCIAS Y LA DEVUELVE COMO CADENA
    ''' </summary>
    ''' <returns>Devuelve una Cadena con la Lista de Campos</returns>
    ''' <remarks></remarks>
    Private Function cLista_Campos_Basicos() As String
        'variables a utilizar
        Dim strCampos As String = String.Empty

        'ensamblamos la Consulta SQL
        cSentenciaSQL = "SELECT * FROM [VW_CAMPOS_A_DESPLEGAR_EXISTENCIAS]"

        Try
            'llamamos a funcion de Ejecucion de Consulta SQL
            Dim dtDataTableCampos As DataTable = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)
            'recorremos cada fila devuelta
            For Each drwFila In dtDataTableCampos.Rows
                'recorremos cada columna de la fila
                For Each dclColumna In dtDataTableCampos.Columns
                    'si es el primer campo
                    If strCampos.Equals(String.Empty) Then
                        'anadimos solo el nombre del campo
                        strCampos = drwFila.Item(dclColumna.ColumnName.ToString()).ToString()

                    Else
                        'anadimos el nombre del campo precedida por la coma separadora
                        strCampos = strCampos & "," & drwFila.Item(dclColumna.ColumnName.ToString()).ToString()

                    End If

                Next dclColumna

                'salimos del bucle de las filas
                Exit For

            Next drwFila

            'devolvemos la lista de campos
            Return strCampos

        Catch Ex As Exception
            'en caso de error, evento a LOG
            principal.pInfo_a_Log("Error intentando obtener la Lista de Campos a Desplegar por Existencias : " & cSentenciaSQL)
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos * como resultado de la funcion
            Return "*"

        End Try

    End Function

    ''' <summary>
    ''' ACTUALIZA LOS DATOS Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="strSentenciaSQL">Consulta SQL a Ejecutar</param>
    ''' <returns>Devuelve 'True' si se Actualizaron Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bCargarGrilla(ByVal strSentenciaSQL As String) As Boolean
        'variables a utilizar
        Dim dclColumnaActual As DataGridViewColumn

        'intentamos actualizar los datos de la grilla
        Try
            'llamamos a funcion de Ejecucion de Consulta SQL
            dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

            'mostramos la cantidad de registros devueltos
            Me.lbl_estado.Text = dtDataTableAuxiliar.Rows.Count.ToString() & " Registros"

            'establecemos el DataTable como origen de datos de la grilla
            Me.grd_existencias.DataSource = dtDataTableAuxiliar

            'recorremos las columnas de la grilla
            'For Each dclColumnaActual In Me.grd_existencias.Columns
            'autoajustamos el ancho de la columna
            '    Me.grd_existencias.Columns(dclColumnaActual.Name).AutoSizeMode = DataGridViewAutoSizeColumnMode.

            'Next

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As Exception
            'en caso de error, mostramos el mensaje de error
            MessageBox.Show(Ex.Message, "Resumen de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            principal.pInfo_a_Log("Error intentando Actualizar Vista de Resumen de Inventario : " & strSentenciaSQL)
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' CARGA LOS ITEMS DEL COMBO DE CAMPOS DE FILTRADO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargarCampos()
        'variables a utilizar
        Dim strCampo As String = String.Empty

        'recorremos cada campo de la lista
        For Each strCampo In principal.arlstSepararCampos(Me.cListaBasicaCampos, ",")
            'lo anadimos como item del combo de "Filtrar por:"
            Me.cmb_campos.Items.Add(strCampo)

        Next

        'si al final el combo tiene al menos un item
        If Me.cmb_campos.Items.Count > 0 Then
            'mostramos el primer elemento del mismo
            Me.cmb_campos.SelectedIndex = 0

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Filtrar por"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_campos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_campos.SelectedIndexChanged
        'si el formulario no puede recibir el enfoque
        If Not Me.CanFocus Then Exit Sub

        'si el campo seleccionado es "[NOMBRE_SECTOR]"
        If Me.cmb_campos.Text = "[NOMBRE_SECTOR]" Then
            'volvemos invisible el combo de operadores y los demas controles de busqueda
            Me.cmb_comparaciones.Visible = False
            Me.txt_desde.Visible = False
            Me.txt_hasta.Visible = False
            Me.lbl_Y.Visible = False
            Me.txt_valor_buscado.Visible = False
            Me.cmd_filtrar.Visible = False

            'llamamos a procedimiento de despliegue del arbol de sectores
            Me.pDesplegar_Arbol_Sectores()

        Else
            'volvemos visible el combo de operadores
            Me.cmb_comparaciones.Visible = True

            'si el arbol esta desplegado
            If Me.tvw_arbol.Width > 1 Then
                'llamamos a procedimiento de repliegue del arbol de sectores
                Me.pReplegar_Arbol_Sectores()

                'enfoque a arbol de sectores
                Me.tvw_arbol.Focus()

                'salimos del sub
                Exit Sub

            End If

        End If

        'le pasamos el enfoque al combo de comparadores
        Me.cmb_comparaciones.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO EN EL COMBO DE COMPARACIONES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_comparaciones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_comparaciones.SelectedIndexChanged
        'limpiamos los cuadros de texto
        Me.txt_valor_buscado.Text = String.Empty
        Me.txt_desde.Text = String.Empty
        Me.txt_hasta.Text = String.Empty

        'ocultamos todos los controles referentes al filtrado
        Me.txt_valor_buscado.Visible = False
        Me.txt_desde.Visible = False
        Me.lbl_Y.Visible = False
        Me.txt_hasta.Visible = False
        Me.cmd_filtrar.Visible = False

        'evaluamos cual es el item seleccionado
        Select Case Me.cmb_comparaciones.Text.Trim
            Case "es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que"
                'mostramos el cuadro de texto de valor buscado
                Me.txt_valor_buscado.Visible = True

                'le pasamos el enfoque
                Me.txt_valor_buscado.Focus()

            Case "esta entre", "no esta entre"
                'mostramos los cuadros de texto DESDE y HASTA y la etiqueta "Y:", tambien el boton de busqueda
                Me.txt_desde.Visible = True
                Me.lbl_Y.Visible = True
                Me.txt_hasta.Visible = True
                Me.cmd_filtrar.Visible = True

                'pasamos el enfoque al cuadro DESDE
                Me.txt_desde.Focus()

            Case Else
                'si no es ninguno de los anteriores, nada

        End Select

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO DE "Valor Buscado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_buscado_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txt_valor_buscado.MouseClick
        'si el campo seleccionado es "[ID_SECTOR]"
        If Me.cmb_campos.Text = "[ID_SECTOR]" Then
            'llamamos a procedimiento de despliegue del arbol de sectores
            Me.pDesplegar_Arbol_Sectores()

            'establecemos este cuadro como control de origen de llamada al arbol
            Me.cControlOrigen = Me.txt_valor_buscado.Name.ToString()

            'pasamos el enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el arbol esta desplegado
            If Me.tvw_arbol.Size.Width > 20 Then
                'lo replegamos
                Me.pReplegar_Arbol_Sectores()

            End If
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL CONTENIDO DEL CUADRO DE VALOR BUSCADO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_buscado_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_valor_buscado.TextChanged
        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'si el cuadro esta vacio
        If Me.txt_valor_buscado.Text.Length.Equals(0) Then
            'llamamos a funcion de actualizacion de datos de la grilla
            If Me.bCargarGrilla(Me.cConsultaPrincipal) Then
                'refrescamos el formulario
                Me.Refresh()

            End If
        Else
            'si no esta vacio, obtenemos el tipo de Dato del Campo seleccionado
            Me.cTipoDatoCampo = principal.cObtener_Tipo_Dato_DeCampo("VW_EXISTENCIAS", Me.cmb_campos.Text.Substring(1, Me.cmb_campos.Text.Length - 2))

            'evaluamos el tipo de dato del campo
            Select Case Me.cTipoDatoCampo.ToUpper()
                'si es numerico
                Case "FLOAT", "NUMERIC", "INTEGER", "INT", "REAL"
                    'evaluamos el campo seleccionado
                    Select Case Me.cmb_comparaciones.Text.Trim
                        'establecemos la condicion correspondiente al tipo de comparacion
                        Case "es igual a"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " = " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case "no es igual a"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " <> " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case "es mayor que"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " > " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case "es menor que"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " < '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case "es mayor o igual que"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " >= " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case "es menor o igual que"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " <= " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case Else
                            'si no es ninguno de los anteriores, nada
                            Me.cCondicion = ""

                    End Select

                Case "VARCHAR", "CHAR", "NVARCHAR", "NCHAR"
                    'evaluamos el campo seleccionado
                    Select Case Me.cmb_comparaciones.Text.Trim
                        'establecemos la condicion correspondiente al tipo de comparacion
                        Case "es igual a"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                        Case "no es igual a"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) NOT LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                        Case "es mayor que"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) > '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case "es menor que"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) < '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case "es mayor o igual que"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) >= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case "es menor o igual que"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) <= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case Else
                            'si no es ninguno de los anteriores, nada
                            Me.cCondicion = ""

                    End Select

                Case "DATETIME", "SMALLDATETIME"
                    'evaluamos el campo seleccionado
                    Select Case Me.cmb_comparaciones.Text.Trim
                        'establecemos la condicion correspondiente al tipo de comparacion
                        Case "es igual a"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                        Case "no es igual a"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) NOT LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                        Case "es mayor que"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) > '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case "es menor que"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) < '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case "es mayor o igual que"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) >= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case "es menor o igual que"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) <= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                        Case Else
                            'si no es ninguno de los anteriores, nada
                            Me.cCondicion = ""
                    End Select
                Case Else
                    'si no es ninguno de los anteriores, nada
                    Me.cCondicion = ""
            End Select

            'ensamblamos la Consulta Auxiliar
            cConsultaAuxiliar = Me.cConsultaPrincipal & Me.cCondicion

            'llamamos a funcion de actualizacion de datos de la grilla
            If Me.bCargarGrilla(Me.cConsultaAuxiliar) Then
                'refrescamos el formulario
                Me.Refresh()
            End If
        End If

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON DE BUSQUEDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_filtrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_filtrar.Click
        'si ninguno de los cuadros esta vacio
        If Not Me.txt_desde.Text.Trim.Equals(String.Empty) And Not Me.txt_hasta.Text.Trim.Equals(String.Empty) Then
            'si el arbol de sectores esta desplegado
            If Me.tvw_arbol.Size.Width > 20 Then
                'lo replegamos
                Me.pReplegar_Arbol_Sectores()

            End If

            'si no estan vacios, obtenemos el tipo de Dato del Campo seleccionado
            Me.cTipoDatoCampo = principal.cObtener_Tipo_Dato_DeCampo("VW_EXISTENCIAS", Me.cmb_campos.Text.Substring(1, Me.cmb_campos.Text.Length - 2))

            'evaluamos el tipo de dato del campo
            Select Case Me.cTipoDatoCampo.ToUpper()
                'si es numerico
                Case "FLOAT", "NUMERIC", "INTEGER", "INT", "REAL"
                    'evaluamos el campo seleccionado
                    Select Case Me.cmb_comparaciones.Text.Trim
                        'establecemos la condicion correspondiente al tipo de comparacion
                        Case "esta entre"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " BETWEEN " & Me.txt_desde.Text.Trim.Replace("'", "''").Replace(",", ".") & " AND " & Me.txt_hasta.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case "no esta entre"
                            Me.cCondicion = " AND " & Me.cmb_campos.Text & " NOT BETWEEN " & Me.txt_desde.Text.Trim.Replace("'", "''").Replace(",", ".") & " AND " & Me.txt_hasta.Text.Trim.Replace("'", "''").Replace(",", ".")

                        Case Else
                            'si no es ninguno de los anteriores, nada
                            Me.cCondicion = ""

                    End Select

                Case "VARCHAR", "CHAR", "NVARCHAR", "NCHAR"
                    'evaluamos el campo seleccionado
                    Select Case Me.cmb_comparaciones.Text.Trim
                        'establecemos la condicion correspondiente al tipo de comparacion
                        Case "esta entre"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & "AS VARCHAR) BETWEEN '" & Me.txt_desde.Text.Trim.Replace("'", "''") & "' AND '" & Me.txt_hasta.Text.Trim.Replace("'", "''") & "'"

                        Case "no esta entre"
                            Me.cCondicion = " AND CAST(" & Me.cmb_campos.Text & "AS VARCHAR) NOT BETWEEN '" & Me.txt_desde.Text.Trim.Replace("'", "''") & "' AND '" & Me.txt_hasta.Text.Trim.Replace("'", "''") & "'"

                        Case Else
                            'si no es ninguno de los anteriores, nada
                            Me.cCondicion = ""

                    End Select

                Case "DATETIME", "SMALLDATETIME"
                    'evaluamos el campo seleccionado
                    Select Case Me.cmb_comparaciones.Text.Trim
                        'establecemos la condicion correspondiente al tipo de comparacion
                        Case "esta entre"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) BETWEEN '" & Me.txt_desde.Text.Trim.Replace("'", "''") & "' AND '" & Me.txt_hasta.Text.Trim.Replace("'", "''") & "'"

                        Case "no esta entre"
                            Me.cCondicion = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) NOT BETWEEN '" & Me.txt_desde.Text.Trim.Replace("'", "''") & "' AND '" & Me.txt_hasta.Text.Trim.Replace("'", "''") & "'"

                        Case Else
                            'si no es ninguno de los anteriores, nada
                            Me.cCondicion = ""

                    End Select
                Case Else
                    'si no es ninguno de los anteriores, nada
                    Me.cCondicion = ""
            End Select

            'ensamblamos la Consulta Auxiliar
            cConsultaAuxiliar = Me.cConsultaPrincipal & Me.cCondicion

            'llamamos a funcion de actualizacion de datos de la grilla
            If Me.bCargarGrilla(Me.cConsultaAuxiliar) Then
                'refrescamos el formulario
                Me.Refresh()
            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE LCIKC EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Exportar a Excel"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_exportar_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'llamamos a procedimiento de despliegue exportacion a excel
        Me.pExportar_A_Excel()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE DOBLE CLICK SOBRE EL CONTENIDO DE UNA CELDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_existencias_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_existencias.CellContentDoubleClick
        'variables a utilizar 
        Dim intNumeroFila As Integer = -1
        Dim strScanning As String = String.Empty

        'si la grilla esta vacia, salimos del sub
        If Me.grd_existencias.RowCount < 1 Then Exit Sub

        'obtenemos el numero de fila
        intNumeroFila = Me.grd_existencias.SelectedCells(0).RowIndex()

        'obtenemos el scanning del articulo
        strScanning = Me.grd_existencias.Rows(intNumeroFila).Cells("SCANNING").Value.ToString()

        'ensamblamos la Consulta SQL
        cSentenciaSQL = "EXEC [SP_OBTENER_DETALLES_ARTICULOS_CONTEOS] " _
                          & "@id_inventario = " & cID_Inventario _
                          & ", @scanning = N'" & strScanning & "'"

        Try
            'llamamos a funcion de Ejecucion de Consulta SQL y lo asignamos como Origen de Datos de la Grilla de Detalles
            Me.grd_detalles.DataSource = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'hacemos visible la pestana de Detalles
            Me.tab_detalles.Show()

            'pasamos a la pestana en cuestion
            Me.tab_padre.SelectedIndex = 1

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Detalles de Articulos-Conteos", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            principal.pInfo_a_Log("Error intentando Ejecutar Consulta SQL de Detalles de Articulos Conteo : " & cSentenciaSQL)
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL TAMANO DEL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_despliega_existencias_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'si el formulario no puede recibir el enfoque, salimos del sub
        If Not Me.CanFocus Then Exit Sub

        'cambiamos el tamano del tab padre
        Me.tab_padre.Height = Me.Height - (Me.tab_padre.Location.Y + 55)
        Me.tab_padre.Width = Me.Width - (Me.tab_padre.Location.X * 2 + 10)

        'luego las grillas
        Me.grd_existencias.Height = Me.tab_padre.Height - (130)
        Me.grd_existencias.Width = Me.tab_padre.Width - (Me.grd_existencias.Location.X + 10)
        Me.grd_detalles.Height = Me.tab_padre.Height - (130)
        Me.grd_detalles.Width = Me.tab_padre.Width - (Me.grd_detalles.Location.X * 2 + 10)

        'el arbol de sectores
        Me.tvw_arbol.Height = Me.tab_padre.Height - (130)

        'la posicion de los controles
        Me.cmd_aplicar_conteo.Location = New Point(Me.cmd_aplicar_conteo.Location.X, Me.tab_padre.Location.X + Me.grd_existencias.Location.X + Me.grd_existencias.Height + 62)
        Me.cmd_excel.Location = New Point(Me.cmd_excel.Location.X, Me.tab_padre.Location.X + Me.grd_existencias.Location.X + Me.grd_existencias.Height + 60)
        Me.cmd_salir.Location = New Point(Me.cmd_salir.Location.X, Me.tab_padre.Location.X + Me.grd_existencias.Location.X + Me.grd_existencias.Height + 62)

        'le contenedor de opciones de conteos
        Me.grp_conteos.Location = New Point(Me.grp_conteos.Location.X, Me.tab_padre.Location.X + Me.grd_existencias.Location.X + Me.grd_existencias.Height + 60)

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON CON LOGO EXCEL
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_excel.Click
        'llamamos a procedimiento de despliegue exportacion a excel
        Me.pExportar_A_Excel()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE EXPORTACION DE DATOS A PLANILLA EXCEL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pExportar_A_Excel()
        'refrescamos el formulario
        Me.Refresh()

        'cerramos la aplicacion excel que este abierta
        MS_EXCEL.proCerrarExcel()

        'llamamos a procedimiento de exportacion de DataGrid a Excel
        MS_EXCEL.proExportarDataGridViewAExcel(Me.grd_existencias, Me.grd_existencias.RowCount, CurDir() & "\salida.xls")

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE APLICACION DE CONTEO
    ''' </summary>
    ''' <param name="intNumeroConteo">numero de Conteo a Aplicar[1|2|3]</param>
    ''' <remarks></remarks>
    Private Sub pAplicar_Conteo(ByVal intNumeroConteo As Integer)
        'si el valor de numero de conteo no es valido, salimos del sub
        If intNumeroConteo < 1 Or intNumeroConteo > 3 Then Exit Sub

        'pedimos confirmacion del usuario
        If Not MessageBox.Show("Se Aplicará el Conteo #" & intNumeroConteo.ToString() _
                             & Chr(13) & "¿Esta Seguro de Proceder?", "Aplicar Conteo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue diferente a "SI", salimos del procedimiento
            Exit Sub

        End If

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            'ensamblamos la Sentencia SQL
            cSentenciaSQL = "EXECUTE [SP_APLICAR_CONTEO] @id_inventario= " & cID_Inventario & ", @nro_conteo= " & intNumeroConteo.ToString() & ";"

            'llamamos a funcion de Ejecucion de Sentencia SQL
            If principal.bEjecutar_SentenciaSQL(cSentenciaSQL) Then
                'si se ejecuto correctamente, volvemos a cargar la grilla
                'ensamblamos la Consulta SQL
                cSentenciaSQL = "SELECT " & Me.cListaBasicaCampos _
                                  & " FROM [VW_EXISTENCIAS] " _
                                  & " WHERE [ID_INVENTARIO]= " & cID_Inventario

                'llamamos a funcion de carga de datos a grilla
                If Me.bCargarGrilla(cSentenciaSQL) Then
                    'si se ejecuto correctamente, tomamos la Consulta SQL como consulta principal
                    Me.cConsultaPrincipal = cSentenciaSQL

                End If

                'cambiamos el valor de la variable de conteo aplicado
                principal.nNro_Conteo_Aplicado = intNumeroConteo
                bConteoaplicado = True

            Else
                'sino
                bConteoaplicado = False

            End If

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Aplicar Conteo", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            principal.pInfo_a_Log("Error intentando Aplicar Conteo : " & cSentenciaSQL)
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            bConteoaplicado = False

        End Try

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Aplicar Conteo]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aplicar_conteo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_aplicar_conteo.Click
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'si no hay ningun conteo seleccionado
        If Not Me.opt_conteo_1.Checked And Not Me.opt_conteo_2.Checked And Not Me.opt_conteo_3.Checked Then
            'mensaje de notificacion
            MessageBox.Show("Debe Seleccionar un Conteo a Aplicar!", "Aplicar Conteo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'enfoque a option de conteo 1
            Me.opt_conteo_1.Focus()

            'salimos del sub
            Exit Sub

        End If

        'evaluamos cual es la opcion de conteo seleccionada
        If Me.opt_conteo_1.Checked Then
            'llamamos a procedimiento de aplicacion de conteo
            Me.pAplicar_Conteo(1)

            'salimos del sub
            Exit Sub

        End If
        If Me.opt_conteo_2.Checked Then
            'llamamos a procedimiento de aplicacion de conteo
            Me.pAplicar_Conteo(2)

            'salimos del sub
            Exit Sub

        End If
        If Me.opt_conteo_3.Checked Then
            'llamamos a procedimiento de aplicacion de conteo
            Me.pAplicar_Conteo(3)

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Aplicar Conteo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_aplicar_conteo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'llamamos a evento click del boton [Aplicar Conteo]
        Me.cmd_aplicar_conteo_Click(sender, e)

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Ejecutar Ajustes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_ejecutar_ajustes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'si no hay un conteo aplicado
        If Not bConteoaplicado Then
            'mensaje de notificacion
            MessageBox.Show("No hay un Conteo Aplicado al Inventario!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'llamamos a procedimiento de Ejecucion de Ajustes
        Me.pEjecutar_Ajustes()

    End Sub

    ''' <summary>
    ''' EJECUTA LOS AJUSTES DE INVENTARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pEjecutar_Ajustes()
        'pedimos la confirmacion del usuario
        If Not MessageBox.Show("Esta a punto de Cambiar el Stock de Inventario. Esta Operación es Irreversible." _
                            & Chr(13) & "¿Esta seguro de Proceder con la Operación?", _
                            "Aplicar Ajustes de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue diferente a "SI", salimos del sub
            Exit Sub

        End If

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'bloqueamos el formulario
        Me.Enabled = False

        'mostramos el mensaje de estado actual
        Me.lbl_estado.Text = "Recopilando Información de Ajustes"

        'ensamblamos la Consulta SQL
        cSentenciaSQL = "EXECUTE [SP_OBTENER_DATOS_AJUSTES] @id_inventario= " & cID_Inventario

        Try
            'dimensionamos un DataTable temporal
            Dim dtbDataTableTemporal As DataTable

            'un contador de registros
            Dim intNroFila As Integer = 0

            'reseteaamos la variable de control de errores
            bHay_Error = False

            'llamamos a funcion de Ejecucion de Consulta SQL
            dtbDataTableTemporal = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'evento a LOG
            principal.pInfo_a_Log("Se Inicia Proceso de Ajuste para :" & dtbDataTableTemporal.Rows.Count.ToString() & " Registros")

            'valor de la barra de progreso al numero de filas a ajustar
            Me.pbr_estado.Maximum = dtbDataTableTemporal.Rows.Count

            'recorremos cada fila del DataTable
            For Each drwFila In dtbDataTableTemporal.Rows
                'incrementamos el contador de filas
                intNroFila = intNroFila + 1

                'mostramos el mensaje de estado actual
                Me.lbl_estado.Text = "Ajustando Articulo " & intNroFila.ToString() & "/" & dtbDataTableTemporal.Rows.Count.ToString()
                Me.pbr_estado.Value = intNroFila

                'tomamos los valores de cada campo y llamamos a la Funcion de Ejecucion de Ajustes
                If SISTEMAS.bolEjecutar_Ajuste(drwFila.Item("ID_LOCAL").ToString() _
                                               , drwFila.Item("FECHA_INVENTARIO").ToString() _
                                               , drwFila.Item("SCANNING").ToString() _
                                               , drwFila.Item("CANTIDAD_AJUSTE").ToString() _
                                               , drwFila.Item("COSTO").ToString()) Then
                    'si se ejecuto correctamente, llamamos a procedimiento de Marcado de "Registro Ajustado"
                    If Not bMarcar_Articulo_Ajustado(drwFila.Item("SCANNING").ToString()) Then
                        'si no se pudo marcar el articulo, evento a LOG
                        principal.pInfo_a_Log("A pesar de haberse Ajustado, no se pudo Marcar como tal el Articulo : " & drwFila.Item(1).ToString())

                    End If

                Else
                    'si no se Ejecuto Correctamente, evento a LOG
                    principal.pInfo_a_Log("Registro Nro : " & intNroFila.ToString())

                    'marcamos el error
                    bHay_Error = True

                    'salimos del bucle for
                    Exit For

                End If

                'refrescamos el formulario
                Me.Refresh()


            Next

            'si no hubo errores
            If Not bHay_Error Then
                'evento a LOG
                principal.pInfo_a_Log("Ajustes Realizados sin Errores")

                'mostramos el mensaje de estado actual
                Me.lbl_estado.Text = "Cerrando Inventario..."

                'llamamos a funcion de Cierre del Inventario
                If Not Me.bCerrar_Inventario() Then
                    'si no se pudo cerrar, evento a LOG
                    principal.pInfo_a_Log("No se Pudo Cerrar el Inventario debido al Error Anterior")

                Else
                    'evento a LOG
                    principal.pInfo_a_Log("Inventario Cerrado, Conteo Aplicado = " & nNro_Conteo_Aplicado.ToString())

                End If
            Else
                'evento a LOG
                principal.pInfo_a_Log("Ajustes Realizados con Errores")

            End If

            'mostramos el mensaje de estado actual
            Me.lbl_estado.Text = "Ajustes Realizados.."

            'desbloqueamos el formulario
            Me.Enabled = True

            'establecemos el cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        Catch Ex As Exception
            'en caso de error, evento a LOG
            principal.pInfo_a_Log("Error intentando Ejecutar Ajuste : " & cSentenciaSQL)
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'desbloqueamos el formulario
            Me.Enabled = True

            'establecemos el cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'mostramos el mensaje de estado actual
            Me.lbl_estado.Text = "Ajuste con Errores..."

            'mensaje de notificacion
            MessageBox.Show(Ex.Message, "Aplicar Ajustes de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    ''' <summary>
    ''' MARCA UN REGISTRO DE LA TABLA [EXISTENCIAS] COMO "AJUSTADO" Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="sScanning">Scannng del Articulo a ser Marcado</param>
    ''' <returns>Devuelve 'True' si se Marco Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bMarcar_Articulo_Ajustado(ByVal sScanning As String) As Boolean
        'ensamblamos la Sentencia SQL
        cSentenciaSQL = "EXECUTE [SP_MARCAR_ARTICULO_AJUSTADO] @id_inventario= " & cID_Inventario _
                                                                & ",@scanning= '" & sScanning & "';"

        'llamamos a funcion de Ejecucion de Sentencia SQL y devolvemos el resultado del mismo como resultado de esta funcion
        Return principal.bEjecutar_SentenciaSQL(cSentenciaSQL)

    End Function

    ''' <summary>
    ''' EJECUTA SP DE CIERRE DE INVENTARIO ACTUAL Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <returns>Devuelve 'True' si se Cerro Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bCerrar_Inventario() As Boolean
        'llamamos a funcion de Ejecucion de Consulta SQL
        If principal.bEjecutar_SentenciaSQL("EXECUTE [SP_CERRAR_INVENTARIO] @id_inventario= " & cID_Inventario) Then
            'cambiamos el valor de la variable de control de vierre de inventario
            principal.bInventario_Cerrado = True

            'devolvemos el resultado de la funcion
            Return True

        End If

    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE ARBOL DE SECTORES POSIBLES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCrear_Arbol_Sectores()
        'variables a utilizar
        Dim nodoNivel1, nodoNivel2, nodoNivel3, nodoNivel4 As TreeNode
        Dim dgrDataTableSectores As DataTable = SISTEMAS.dtbSeleccionar_Sectores(String.Empty)

        Try
            ' Agregar al TreeView los nodos Hijos que se han obtenido en el DataView.
            For Each drwFila In dgrDataTableSectores.Rows
                'obtenemos el nombre del sector
                Dim nuevoNodo As New TreeNode(drwFila.Item("NOMBRE_SECTOR"))
                nuevoNodo.Tag = drwFila.Item("NIVEL").ToString.Trim() & "|" & drwFila.Item("CODIGO").ToString.Trim()

                'evaluamos el nivel del sector
                Select Case drwFila.Item("NIVEL")
                    Case 1
                        Me.tvw_arbol.Nodes.Add(nuevoNodo)
                        nodoNivel1 = nuevoNodo
                    Case 2
                        nodoNivel1.Nodes.Add(nuevoNodo)
                        nodoNivel2 = nuevoNodo
                    Case 3
                        nodoNivel2.Nodes.Add(nuevoNodo)
                        nodoNivel3 = nuevoNodo
                    Case 4
                        nodoNivel3.Nodes.Add(nuevoNodo)
                        nodoNivel4 = nuevoNodo
                    Case 5
                        nodoNivel4.Nodes.Add(nuevoNodo)
                    Case Else

                End Select

            Next drwFila

        Catch Ex As Exception
            'en caso de error, evento a LOG
            principal.pInfo_a_Log("Error intentando crear Arbol de Sectores")
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try


    End Sub

    ''' <summary>
    ''' DESPUES DE QUE SE SELECCIONO UN NODO DEL ARBOL DE SECTORES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_sectores_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvw_arbol.AfterSelect
        Try
            'variables a utilizar
            Dim nPosicionSeparador As Integer = Me.tvw_arbol.SelectedNode.Tag.ToString.IndexOf("|")
            Dim sCodigo As String = Me.tvw_arbol.SelectedNode.Tag.ToString.Substring(nPosicionSeparador + 1)
            Dim nNivel As Integer = Int16.Parse(Me.tvw_arbol.SelectedNode.Tag.ToString.Substring(0, nPosicionSeparador))

            'evaluamos el caso del nivel sel sector seleccionado
            Select Case nNivel
                Case 1
                    'si el nivel es 1 extraemos los digitos correspondientes
                    sCodigo = sCodigo.Substring(0, 2)

                Case 2
                    'si el nivel es 2 extraemos los digitos correspondientes
                    sCodigo = sCodigo.Substring(0, 4)

                Case 3
                    'si el nivel es 3 extraemos los digitos correspondientes
                    sCodigo = sCodigo.Substring(0, 6)

                Case 4
                    'si el nivel es 8 extraemos los digitos correspondientes
                    sCodigo = sCodigo.Substring(0, 8)

                Case 5
                    'si el nivel es 5, lo pasamos como tal
                    sCodigo = sCodigo

                Case Else
                    Exit Sub

            End Select

            'ensamblamos la condicion de filtrado
            Me.cCondicion = " AND [ID_SECTOR] LIKE '" & sCodigo & "%'"

            'ensamblamos la Consulta Auxiliar
            cConsultaAuxiliar = Me.cConsultaPrincipal & Me.cCondicion

            'el campo de filtrado es [ID_SECTOR]
            If Me.cmb_campos.Text = "[ID_SECTOR]" Then
                'recorremos los controles del formulario
                For Each oControl As Control In Me.tab_resumen.Controls
                    'si el nombre del control es el mismo del control de origen de llamada al arbol
                    If oControl.Name = Me.cControlOrigen Then
                        'le pasamos el codigo del sector seleccionado
                        oControl.Text = sCodigo

                    End If
                Next

            Else
                'sino, llamamos a funcion de actualizacion de datos de la grilla
                If Me.bCargarGrilla(Me.cConsultaAuxiliar) Then
                    'refrescamos el formulario
                    Me.Refresh()
                End If

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            principal.pInfo_a_Log("Error intentando obtener Codigo de Sector Seleccionado, Filtro de Existencias")
            principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE DESPLIEGUE DE ARBOL DE SECTORES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pDesplegar_Arbol_Sectores()
        'establecemos la accion a ejecutar
        Me.bReplegarArbol = False

        'establecemos el tamano maximo del arbol
        Me.tvw_arbol.MaximumSize = New Size(210, Me.grd_existencias.Size.Height)

        'habilitamos el Timer
        Me.tmr_arbol.Enabled = True

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE REPLIEGUE DE ARBOL DE SECTORES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pReplegar_Arbol_Sectores()
        'establecemos la accion a ejecutar
        Me.bReplegarArbol = True

        'habilitamos el Timer
        Me.tmr_arbol.Enabled = True

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' CUANDO OCURRE UN EVENTO "TICK" EN EL TIMER QUE CONTROLA EL ARBOL DE SECTORES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmr_arbol_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr_arbol.Tick
        'si lo que hay que hacer es replegar el arbol
        If Me.bReplegarArbol Then
            'si el ancho es mayor a 0
            If Me.tvw_arbol.Width > 0 Then
                'decrementamos su ancho, y su alto lo dejamos en el mismo de la grilla
                Me.tvw_arbol.Size = New Size(Me.tvw_arbol.Size.Width - 30, Me.grd_existencias.Size.Height)

                'y la movemos a la izquierda para ocupar el lugar del arbol
                Me.grd_existencias.Location = New Point(Me.grd_existencias.Location.X - 30, Me.grd_existencias.Location.Y)

                'incrementamos el ancho de la grilla en la misma proporcion
                Me.grd_existencias.Width = Me.grd_existencias.Width + 30

            Else
                'sino, deshabilitamos el Timer
                Me.tmr_arbol.Enabled = False

            End If

        Else
            'sino, lo desplegamos mientras su ancho es menor o igual a 210
            If Me.tvw_arbol.Width < 210 Then
                'incrementamos el ancho de la grilla en la misma proporcion
                Me.grd_existencias.Width = Me.grd_existencias.Width - 30

                'y la movemos a la derecha para dar espacio al arbol
                Me.grd_existencias.Location = New Point(Me.grd_existencias.Location.X + 30, Me.grd_existencias.Location.Y)

                'incrementamos su ancho, y su alto lo dejamos en el mismo de la grilla
                Me.tvw_arbol.Size = New Size(Me.tvw_arbol.Size.Width + 30, Me.grd_existencias.Size.Height)

            Else
                'sino, deshabilitamos el Timer
                Me.tmr_arbol.Enabled = False

            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Desplegar Reporte"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_desplegar_reporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'desplegamos el formulario de seleccion de criterios del reporte de existencias en modo dialogo
        frm_reporte_existencias.ShowDialog()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE DOBLE CLICK EN EL CONTENIDO DE UNA CELDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_detalles_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_detalles.CellContentDoubleClick
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'variables a utilizar
        Dim nNroFila As Integer = -1

        'obtenemos el indice de la fila seleccionada
        nNroFila = Me.grd_detalles.SelectedCells.Item(0).RowIndex

        'pasamos los valores de cada celda al formulario de nmodificacion de cantidades
        frm_modificar_cantidad.lbl_scanning.Text = Me.grd_detalles.Rows(nNroFila).Cells("SCANNING").Value.ToString()
        frm_modificar_cantidad.lbl_articulo.Text = Me.grd_detalles.Rows(nNroFila).Cells("ARTICULO").Value.ToString()
        frm_modificar_cantidad.lbl_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("SOPORTE").Value.ToString()
        frm_modificar_cantidad.lbl_letra_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("LETRA").Value.ToString()
        frm_modificar_cantidad.lbl_nombre_local.Text = Me.grd_detalles.Rows(nNroFila).Cells("NOMBRE_LOCAL").Value.ToString()
        frm_modificar_cantidad.lbl_id_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_SOPORTE").Value.ToString()
        frm_modificar_cantidad.lbl_id_letra_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_LETRA_SOPORTE").Value.ToString()
        frm_modificar_cantidad.txt_conteo_1.Text = Me.grd_detalles.Rows(nNroFila).Cells("CONTEO_1").Value
        frm_modificar_cantidad.txt_conteo_2.Text = Me.grd_detalles.Rows(nNroFila).Cells("CONTEO_2").Value
        frm_modificar_cantidad.txt_conteo_3.Text = Me.grd_detalles.Rows(nNroFila).Cells("CONTEO_3").Value

        'desplegamos el formulario en modo dialogo
        frm_modificar_cantidad.ShowDialog()

        'si el resultado del dialogo es "OK"
        If frm_modificar_cantidad.DialogResult = Windows.Forms.DialogResult.OK Then
            'actualizamos los valores en las celdas de cantidades de la fila modificada
            Me.grd_detalles.Rows(nNroFila).Cells("CONTEO_1").Value = frm_modificar_cantidad.txt_conteo_1.Text
            Me.grd_detalles.Rows(nNroFila).Cells("CONTEO_2").Value = frm_modificar_cantidad.txt_conteo_2.Text
            Me.grd_detalles.Rows(nNroFila).Cells("CONTEO_3").Value = frm_modificar_cantidad.txt_conteo_3.Text

            'ensamblamos la Consulta SQL
            cSentenciaSQL = "SELECT " & Me.cListaBasicaCampos _
                              & " FROM [VW_EXISTENCIAS] " _
                              & " WHERE [ID_INVENTARIO]= " & cID_Inventario

            'llamamos a funcion de carga de datos a grilla
            If Me.bCargarGrilla(cSentenciaSQL) Then
            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'cerramos el este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO "Hasta" DE LOS VALORES BUSCADOS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_hasta_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txt_hasta.MouseClick
        'si el campo seleccionado es "[ID_SECTOR]"
        If Me.cmb_campos.Text = "[ID_SECTOR]" Then
            'si el arbol esta Replegado
            If Me.tvw_arbol.Size.Width < 20 Then
                'llamamos a procedimiento de despliegue del arbol de sectores
                Me.pDesplegar_Arbol_Sectores()

            End If

            'establecemos este cuadro como control de origen de llamada al arbol
            Me.cControlOrigen = Me.txt_hasta.Name.ToString()

            'pasamos el enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el arbol esta desplegado
            If Me.tvw_arbol.Size.Width > 20 Then
                'lo replegamos
                Me.pReplegar_Arbol_Sectores()

            End If
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO "Desde" DE LOS VALORES BUSCADOS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_desde_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txt_desde.MouseClick
        'si el campo seleccionado es "[ID_SECTOR]"
        If Me.cmb_campos.Text = "[ID_SECTOR]" Then
            'si el arbol esta Replegado
            If Me.tvw_arbol.Size.Width < 20 Then
                'llamamos a procedimiento de despliegue del arbol de sectores
                Me.pDesplegar_Arbol_Sectores()

            End If

            'establecemos este cuadro como control de origen de llamada al arbol
            Me.cControlOrigen = Me.txt_desde.Name.ToString()

            'pasamos el enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el arbol esta desplegado
            If Me.tvw_arbol.Size.Width > 20 Then
                'lo replegamos
                Me.pReplegar_Arbol_Sectores()

            End If
        End If

    End Sub

End Class