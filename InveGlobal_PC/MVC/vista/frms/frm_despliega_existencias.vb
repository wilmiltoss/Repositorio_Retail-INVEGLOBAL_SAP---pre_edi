Imports System.IO

Public Class frm_despliega_existencias

#Region "VARIABLES_LOCALES"
    Private cListaBasicaCamposResumen As String = String.Empty
    Private cListaBasicaCamposUbicacion As String = String.Empty
    Private cConsultaPrincipal As String = String.Empty
    Private cConsultaPrincipalUbicaciones As String = String.Empty
    Private cConsultaTotales As String = String.Empty
    Private cConsultaAuxiliar As String = String.Empty
    Private cCondicion As String = String.Empty
    Private cTipoDatoCampo As String = String.Empty
    Private nCantidadDeFilas As Integer = 0

    Private bReplegarArbol As Boolean = False
    Private bConteoAplicado As Boolean = False

    Private cControlOrigen As String = String.Empty

    Private bActualmenteProcesando As Boolean = False

    Private dtDataTableResumen As DataTable

#End Region

#Region "CONSTANTES_SENTENCIAS_SQL"

    Private Const SP_CREAR_TABLA_VW_EXISTENCIAS As String = "EXEC [SP_CREAR_TABLA_VW_EXISTENCIAS] @id_inventario = {0}"
    Private Const CAMPOS_A_DESPLEGAR_EXISTENCIAS As String = "SELECT * FROM [VW_CAMPOS_A_DESPLEGAR_EXISTENCIAS]"
    Private Const SELECT_BASICO_DATOS As String = "SELECT {0} FROM [VW_EXISTENCIAS] WHERE [ID_INVENTARIO]= {1}"

    Private Const SP_MARCAR_NO_CONTABILIZADOS As String = "EXEC [SP_MARCAR_NO_CONTABILIZADOS] @id_inventario= {0}, @estado= {1}"
    Private Const SP_CALCULAR_PRE_AJUSTES As String = "EXEC [SP_CALCULAR_PRE_AJUSTES] @id_inventario= {0}"
    Private Const UPDATE_VW_EXISTENCIAS As String = "UPDATE [VW_EXISTENCIAS] SET [AJUSTAR]= {0}  WHERE [ID_INVENTARIO]= {1}"
    Private Const UPDATE_EXISTENCIAS As String = "UPDATE [EXISTENCIAS] SET AJUSTAR = {0} WHERE [ID_INVENTARIO]= {1} AND [SCANNING] IN (SELECT [SCANNING] FROM [VW_EXISTENCIAS] WHERE [ID_INVENTARIO]= {1} AND [AJUSTAR]= {0})"

    Private Const SP_OBTENER_DATOS_AJUSTES As String = "EXECUTE [SP_OBTENER_DATOS_AJUSTES] @id_inventario= {0}"
    Private Const SP_MARCAR_ARTICULO_PARA_AJUSTE As String = "EXEC [SP_MARCAR_ARTICULO_PARA_AJUSTE] @scanning= {0}, @valor= '{1}'"
    Private Const SP_MARCAR_ARTICULO_AJUSTADO As String = "EXECUTE [SP_MARCAR_ARTICULO_AJUSTADO] @id_inventario= {0}, @scanning= '{1}'"
    Private Const SP_MARCAR_ARTICULO_NO_AJUSTADO As String = "EXECUTE [SP_MARCAR_ARTICULO_NO_AJUSTADO] @id_inventario= {0}, @scanning= '{1}'"

#End Region


    ''' <summary>
    ''' CUANDO SE ESTA CERRANDO EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_despliega_existencias_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'si se esta procesando actualmente
        If bActualmenteProcesando Then
            'evento a LOG
            'principal.pInfo_a_Log("Se Intento Cerrar el Formulario de Ajuste mientras este se estaba Realizando")

            'cancelamos el cierre
            e.Cancel = True

            'salimos del sub
            Exit Sub

        End If

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
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        'llamamos a procedimiento de cargas de datos en grillas
        Me.pCargar_Grillas()


    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE DATOS A GRILLAS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Grillas()

        'sentencias a ejecutar
        Dim SENTENCIA_1 As String = String.Format(SP_CREAR_TABLA_VW_EXISTENCIAS, principal.cID_Inventario)

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'llamamos a la funcion de ejecucion de sentencias sql
        If Not principal.bEjecutar_SentenciaSQL(SENTENCIA_1) Then

            'si no se ejecuto, mensaje
            MessageBox.Show("No se pudo Ejecutar la Sentencia " & SENTENCIA_1, "Resumen de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            'establecemos las listas basicas de campos a desplegar para cada grilla
            Me.cListaBasicaCamposResumen = Me.cLista_Campos_Basicos(CAMPOS_A_DESPLEGAR_EXISTENCIAS)

            'ensamblamos la Consulta SQL
            cSentenciaSQL = String.Format(SELECT_BASICO_DATOS, Me.cListaBasicaCamposResumen, cID_Inventario)

            'llamamos a funcion de carga de datos a grilla de resumen
            If Me.bCargar_Grilla_Resumen(cSentenciaSQL) Then

                'si se ejecuto correctamente, tomamos la Consulta SQL como consulta principal
                Me.cConsultaPrincipal = cSentenciaSQL

                'llamamos a funcion de carga de combo de campos
                Me.pCargar_Campos()

                'llamamos a procedimiento de carga de Nodos del Arbol de Sectores
                Me.pCrear_Arbol_Sectores()

                'tamano del arbol de sectores a 0:anchoGrilla
                Me.tvw_arbol.Size = New Size(0, Me.grd_existencias.Size.Height)

            End If

            '---------------------------------------------------------------------
            '   COMENTADO PARA SALTAR EL CALCULO DE LAS DIFERENCIAS ENTRE CONTEOS
            '---------------------------------------------------------------------

            'sentencia para calcular totales de diferencias
            'Me.cConsultaTotales = "SELECT CAST(SUM([DIFERENCIA_1_2]) AS INT) DIF_12 " _
            '                                & ", CAST(SUM([DIFERENCIA_2_3]) AS INT) DIF_23 " _
            '                                & ", CAST(SUM([DIFERENCIA_1_3]) AS INT) DIF_13 " _
            '                                & " FROM [VW_EXISTENCIAS] " _
            '                                & " WHERE [ID_INVENTARIO]= " & cID_Inventario
            '---------------------------------------------------------------------

        End If

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' OBTIENE LA LISTA DE CAMPOS A DESPLEGAR EN LA GRILLA DE EXISTENCIAS Y LA DEVUELVE COMO CADENA
    ''' </summary>
    ''' <param name="cConsultaSQL">Consulta SQL a Ejecutar</param>
    ''' <returns>Devuelve una Cadena con la Lista de Campos</returns>
    ''' <remarks></remarks>
    Private Function cLista_Campos_Basicos(ByVal cConsultaSQL As String) As String
        'variables a utilizar
        Dim strCampos As String = String.Empty

        Try
            'llamamos a funcion de Ejecucion de Consulta SQL
            Dim dtDataTableCampos As DataTable = principal.dtEjecutar_ConsultaSQL(cConsultaSQL)
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
            'principal.pInfo_a_Log("Error intentando obtener la Lista de Campos a Desplegar : " & cConsultaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

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
    Private Function bCargar_Grilla_Resumen(ByVal strSentenciaSQL As String) As Boolean
        'cursor del mouse a "Esperar.."
        Cursor.Current = Cursors.WaitCursor

        'intentamos actualizar los datos de la grilla
        Try
            'llamamos a funcion de Ejecucion de Consulta SQL
            'dtDataTableAuxiliar = principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)
            dtDataTableResumen = principal.dtEjecutar_ConsultaSQL(strSentenciaSQL)

            'mostramos la cantidad de registros devueltos
            Me.lbl_estado_2.Text = dtDataTableAuxiliar.Rows.Count.ToString() & " Registros"

            'guardamos la cantidad de filas devueltas
            'nCantidadDeFilas = dtDataTableAuxiliar.Rows.Count
            nCantidadDeFilas = dtDataTableResumen.Rows.Count

            'establecemos el DataTable como origen de datos de la grilla
            'Me.grd_existencias.DataSource = dtDataTableAuxiliar
            Me.grd_existencias.DataSource = dtDataTableResumen

            'recorremos las columnas de la grilla
            For Each dclColumnaActual As DataGridViewColumn In Me.grd_existencias.Columns
                'si la columna NO ES la de [AJUSTAR]
                If Not dclColumnaActual.HeaderText.Equals("AJUSTAR") Then
                    'volvemos de solo lectura la columna actual
                    Me.grd_existencias.Columns(dclColumnaActual.Name).ReadOnly = True

                End If

            Next

            'llamamos a procedimiento de coloreado de campo de [DIFERENCIA_1_2]
            Me.pMarcar_Diferencias(Me.grd_existencias)

            '----------------------------------------------------------------
            '   COMENTADO PARA EVITAR EL CALCULO DE DIFERENCIA ENTRE CONTEOS
            '----------------------------------------------------------------
            'Try
            '    'extraemos la unica fila devuelta
            '    Dim oFila As DataRow = principal.dtEjecutar_ConsultaSQL(Me.cConsultaTotales & " " & Me.cCondicion).Rows(0)
            '
            '    'obtenemos los valores de los campos de la fila
            '    Me.lbl_1_2.Text = "C1-C2= " & oFila.Item("DIF_12").ToString()
            '    Me.lbl_2_3.Text = "C2-C3= " & oFila.Item("DIF_23").ToString()
            '    Me.lbl_1_3.Text = "C1-C3= " & oFila.Item("DIF_13").ToString()
            '
            'Catch Ex As Exception
            'en caso de error, evento a LOG
            '    'principal.pInfo_a_Log("Error Obteniendo Sumas de Diferencias : " & Me.cConsultaTotales & " " & Me.cCondicion)
            '    'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)
            '
            'End Try
            '----------------------------------------------------------------

            'mostramos la cantidad de registros
            Me.lbl_estado_2.Text = Me.grd_existencias.RowCount.ToString() & " Registros"

            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'devolvemos el resultado de la funcion
            Return True

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Actualizar Vista de Resumen de Inventario : " & strSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'mostramos el mensaje de error
            MessageBox.Show(Ex.Message, "Resumen de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'devolvemos el resultado de la funcion
            Return False

        End Try

    End Function

    ''' <summary>
    ''' CARGA LOS ITEMS DEL COMBO DE CAMPOS DE FILTRADO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Campos()
        'variables a utilizar
        Dim strCampo As String = String.Empty

        'recorremos cada campo de la lista
        For Each strCampo In principal.arlstSepararCampos(Me.cListaBasicaCamposResumen, ",")
            'lo anadimos como item del combo de "Filtrar por:"
            Me.cmb_campos.Items.Add(strCampo)

        Next

        'si al final el combo tiene al menos un item
        If Me.cmb_campos.Items.Count > 0 Then
            'anadimos un valor mas para filtrar los articulos sin conteos
            Me.cmb_campos.Items.Add("[SIN CONTEOS]")

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

            'ocultamos el panel de opciones de campo "[AJUSTAR]" y deseleccionamos los radio buttons
            Me.pnl_ajustar.Visible = False
            Me.opt_verdadero.Checked = False
            Me.opt_falso.Checked = False

            'llamamos a procedimiento de despliegue del arbol de sectores
            Me.pDesplegar_Arbol_Sectores()

        Else
            'sino, si el campo es "[AJUSTAR]"
            If Me.cmb_campos.Text = "[AJUSTAR]" Then
                'mostramos el panel con las opciones del campo
                Me.pnl_ajustar.Visible = True

                'volvemos invisible el combo de operadores y los demas controles de busqueda
                Me.cmb_comparaciones.Visible = False
                Me.txt_desde.Visible = False
                Me.txt_hasta.Visible = False
                Me.lbl_Y.Visible = False
                Me.txt_valor_buscado.Visible = False
                Me.cmd_filtrar.Visible = False

                'si el arbol esta desplegado
                If Me.tvw_arbol.Width > 1 Then
                    'llamamos a procedimiento de repliegue del arbol de sectores
                    Me.pReplegar_Arbol_Sectores()

                    'enfoque a arbol de sectores
                    Me.tvw_arbol.Focus()

                    'salimos del sub
                    Exit Sub

                End If

            Else
                'sino, ocultamos el panel de opciones de campo "[AJUSTAR]" y deseleccionamos los radio buttons
                Me.pnl_ajustar.Visible = False
                Me.opt_verdadero.Checked = False
                Me.opt_falso.Checked = False

                'si el valor del combo es "[SIN CONTEOS]"
                If Me.cmb_campos.Text.Equals("[SIN CONTEOS]") Then
                    'ensamblamos la Consulta SQL
                    cSentenciaSQL = String.Format(Me.cConsultaPrincipal & " AND SCANNING NOT IN (SELECT DISTINCT DET.SCANNING FROM DETALLES_CONTEOS DET WHERE DET.ID_INVENTARIO= {0})", cID_Inventario)

                    'cargamos la grilla
                    If Not bCargar_Grilla_Resumen(cSentenciaSQL) Then
                        'sino se cargo, evento a LOG
                        'principal.pInfo_a_Log("No se Cargo la Grilla con los Datos de SIN CONTEOS : " & cSentenciaSQL)

                    End If

                    'salimos del sub
                    Exit Sub

                End If

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
    ''' CUANDO SE PRESIONA UNA TECLA SOBRE EL CUADRO DE "Cantidad"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_buscado_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_valor_buscado.KeyDown
        'si el cuadro de valor buscado no esta vacio
        If Me.txt_valor_buscado.Text.Length > 0 Then
            'si la tecla es [Enter]
            If e.KeyCode = Keys.Enter Then
                'llamamos al procedimiento de busqueda del registro
                Me.pEvaluar_Valor_Buscado()

            End If

        Else
            'si esta vacio y tambien la grilla
            If Me.grd_existencias.RowCount = 0 Then
                'si la tecla es [Enter]
                If e.KeyCode = Keys.Enter Then
                    'llamamos al procedimiento de busqueda del registro
                    Me.pEvaluar_Valor_Buscado()

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO DE "Valor Buscado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_buscado_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txt_valor_buscado.MouseClick
        'si el campo seleccionado es "[ID_SECTOR]" y el cuadro de texto esta vacio
        If Me.cmb_campos.Text = "[ID_SECTOR]" And Me.txt_valor_buscado.Text.Equals(String.Empty) Then
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
    End Sub

    ''' <summary>
    ''' EVALUA Y EJECUTA LA BUSQUEDA DEL ARTICULO CORRESPONDIENTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pEvaluar_Valor_Buscado()
        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'si el cuadro esta vacio y no hay ningun radio button seleccionado
        If Me.txt_valor_buscado.Text.Length.Equals(0) _
            And Not Me.opt_falso.Checked And Not Me.opt_verdadero.Checked Then
            'llamamos a funcion de actualizacion de datos de la grilla
            If Me.bCargar_Grilla_Resumen(Me.cConsultaPrincipal) Then
                'refrescamos el formulario
                Me.Refresh()

            End If
        Else
            'obtenemos la condicion de busqueda
            Me.cCondicion = Me.cCondicion_Filtrado()

            'ensamblamos la Consulta Auxiliar
            cConsultaAuxiliar = Me.cConsultaPrincipal & Me.cCondicion

            'llamamos a funcion de actualizacion de datos de la grilla
            If Me.bCargar_Grilla_Resumen(Me.cConsultaAuxiliar) Then
                'refrescamos el formulario
                Me.Refresh()
            End If
        End If

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' GENERA LA CONDICION DE FILTRADO DE ACUERDO A LOS CRITERIOS SELECCIONADOS
    ''' </summary>
    ''' <returns>Devuelve la Cadena con los Criterios de Seleccion</returns>
    ''' <remarks></remarks>
    Private Function cCondicion_Filtrado() As String
        'variables auxiliares
        Dim cCondicionBusqueda As String = String.Empty

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
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " = " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                    Case "no es igual a"
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " <> " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                    Case "es mayor que"
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " > " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                    Case "es menor que"
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " < " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                    Case "es mayor o igual que"
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " >= " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                    Case "es menor o igual que"
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " <= " & Me.txt_valor_buscado.Text.Trim.Replace("'", "''").Replace(",", ".")

                    Case Else
                        'si no es ninguno de los anteriores, nada
                        cCondicionBusqueda = ""

                End Select

            Case "VARCHAR", "CHAR", "NVARCHAR", "NCHAR"
                'evaluamos el campo seleccionado
                Select Case Me.cmb_comparaciones.Text.Trim
                    'establecemos la condicion correspondiente al tipo de comparacion
                    Case "es igual a"
                        cCondicionBusqueda = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                    Case "no es igual a"
                        cCondicionBusqueda = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) NOT LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                    Case "es mayor que"
                        cCondicionBusqueda = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) > '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case "es menor que"
                        cCondicionBusqueda = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) < '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case "es mayor o igual que"
                        cCondicionBusqueda = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) >= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case "es menor o igual que"
                        cCondicionBusqueda = " AND CAST(" & Me.cmb_campos.Text & " AS VARCHAR) <= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case Else
                        'si no es ninguno de los anteriores, nada
                        Me.cCondicion = ""

                End Select

            Case "DATETIME", "SMALLDATETIME"
                'evaluamos el campo seleccionado
                Select Case Me.cmb_comparaciones.Text.Trim
                    'establecemos la condicion correspondiente al tipo de comparacion
                    Case "es igual a"
                        cCondicionBusqueda = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                    Case "no es igual a"
                        cCondicionBusqueda = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) NOT LIKE '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "%'"

                    Case "es mayor que"
                        cCondicionBusqueda = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) > '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case "es menor que"
                        cCondicionBusqueda = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) < '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case "es mayor o igual que"
                        cCondicionBusqueda = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) >= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case "es menor o igual que"
                        cCondicionBusqueda = " AND CONVERT(VARCHAR, " & Me.cmb_campos.Text & ", 103) <= '" & Me.txt_valor_buscado.Text.Trim.Replace("'", "''") & "'"

                    Case Else
                        'si no es ninguno de los anteriores, nada
                        cCondicionBusqueda = ""

                End Select

            Case "BIT"
                Select Case Me.pnl_ajustar.Visible
                    'establecemos la condicion correspondiente al tipo de comparacion
                    Case True
                        cCondicionBusqueda = " AND " & Me.cmb_campos.Text & " = " & IIf(Me.opt_verdadero.Checked, "'True'", "'False'")

                    Case Else
                        'si no es ninguno de los anteriores, nada
                        cCondicionBusqueda = ""

                End Select

            Case Else
                'si no es ninguno de los anteriores, nada
                cCondicionBusqueda = ""

        End Select

        'devolvemos la condicion de buqueda
        Return cCondicionBusqueda

    End Function

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
            If Me.bCargar_Grilla_Resumen(Me.cConsultaAuxiliar) Then
                'refrescamos el formulario
                Me.Refresh()
            End If

        End If

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

            Try
                'ocultamos las columnas de IDs
                Me.grd_detalles.Columns("ID_LOCACION").Visible = False
                Me.grd_detalles.Columns("ID_SOPORTE").Visible = False
                Me.grd_detalles.Columns("ID_LETRA_SOPORTE").Visible = False

                '-----------------MODIFICACON SOLICITADA POR CONTROL INTERNO
                Me.grd_detalles.Columns("CARA").Visible = False

            Catch Ex As Exception
                'en caso de error, nada
            End Try

            'hacemos visible la pestana de Detalles
            Me.tab_detalles.Show()

            'pasamos a la pestana en cuestion
            Me.tab_padre.SelectedIndex = 1

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Detalles de Articulos-Conteos", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error intentando Ejecutar Consulta SQL de Detalles de Articulos Conteo : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

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
        Me.chk_marcar_todos_para_ajustar.Location = New Point(Me.chk_marcar_todos_para_ajustar.Location.X, Me.tab_padre.Location.X + Me.grd_existencias.Location.X + Me.grd_existencias.Height + 60)
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
        'Me.pExportar_A_Excel()
        Me.bExportar_Archivo_Plano()

    End Sub

    ''' <summary>
    ''' CREA UN ARCHIVO PLANO A PARTIR DE LOS DATOS DE LA GRILLA
    ''' </summary>
    ''' <returns>Devuelve 'True' si se creo Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bExportar_Archivo_Plano() As Boolean
        'creamos una instancia de la clase ArchivosPlanos
        Dim oArchivoPlano As ArchivosPlanos = New ArchivosPlanos("csv")

        'exportamos la grilla a un archivo plano
        If bExportar_DataGrid_A_Archivo_Plano(oArchivoPlano) Then
            'si se exporto, lo cerrramos y lo abrimos para visualizar
            oArchivoPlano.bCerrar_Archivo()
            If oArchivoPlano.bAbrir_Archivo() Then

                bExportar_Archivo_Plano = True

            Else
                bExportar_Archivo_Plano = False

            End If

        Else
            bExportar_Archivo_Plano = False

        End If

    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE EXPORTACION DE DATOS A PLANILLA EXCEL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pExportar_A_Excel()
        'si la grilla no contiene filas, salimos del sub
        If Me.grd_existencias.RowCount < 1 Then Exit Sub

        'si la grilla contiene mas de las filas especificadas para generar el libro excel
        If Me.grd_existencias.RowCount > principal.nFilas_Para_Excel Then
            'mensaje de notificacion
            MessageBox.Show("La grilla contiene demasiadas Filas, el Limite establecido es de " & nFilas_Para_Excel.ToString() _
                                & " Filas..." & Chr(13) & "Trate es Establecer Filtros a la Vista Actual ", "Generar libro Excel", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            'salimos del sub
            Exit Sub

        End If

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
                cSentenciaSQL = "SELECT " & Me.cListaBasicaCamposResumen _
                                  & " FROM [VW_EXISTENCIAS] " _
                                  & " WHERE [ID_INVENTARIO]= " & cID_Inventario

                'llamamos a funcion de carga de datos a grilla
                If Me.bCargar_Grilla_Resumen(cSentenciaSQL) Then
                    'si se ejecuto correctamente, tomamos la Consulta SQL como consulta principal
                    Me.cConsultaPrincipal = cSentenciaSQL

                End If

                'cambiamos el valor de la variable de conteo aplicado
                principal.nNro_Conteo_Aplicado = intNumeroConteo
                bConteoAplicado = True

            Else
                'sino
                bConteoAplicado = False

            End If

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            MessageBox.Show(Ex.Message, "Aplicar Conteo", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'evento a LOG
            'principal.pInfo_a_Log("Error intentando Aplicar Conteo : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            bConteoAplicado = False

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

        'ejecutamos el procedimiento de calculo de cantidades a ajustar
        Call pCalcular_Pre_Ajustes()

    End Sub

    ''' <summary>
    ''' EJECUTA LOS AJUSTES DE INVENTARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pEjecutar_Ajustes()

        '/////////////////////////////////////////////////////////////////////////
        ' --------- BLOQUEADO PARA EVITAR AJUSTES DESDE LA APLICACION ------------
        MessageBox.Show("ESTA OPCION HA SIDO DESHABILITADA EXPLICITAMENTE POR TIC!", "Ajuste de Inventario", MessageBoxButtons.OK)
        Exit Sub
        '/////////////////////////////////////////////////////////////////////////

        'pedimos la confirmacion del usuario
        If Not MessageBox.Show("Esta a punto de Cambiar el Stock de Inventario. Esta Operación es Irreversible." _
                            & Chr(13) & "¿Esta seguro de Proceder con la Operación?", _
                            "Aplicar Ajustes de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            'si la respuesta fue diferente a "SI", salimos del sub
            Exit Sub

        End If

        'cambiamos el valor de la variable de control de estado de proceso
        bActualmenteProcesando = True

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'bloqueamos el formulario
        'Me.Enabled = False

        'mostramos el mensaje de estado actual
        Me.lbl_estado_2.Text = "Recopilando Información de Ajustes"

        'ensamblamos la Consulta SQL
        cSentenciaSQL = String.Format(SP_OBTENER_DATOS_AJUSTES, cID_Inventario)

        Try
            'bloqueamos los controles
            Me.pBloquear_Controles()

            'un contador de registros
            Dim intNroFila As Integer = 0

            'reseteaamos la variable de control de errores
            bHay_Error = False

            'dimensionamos un DataTable temporal y llamamos a funcion de Ejecucion de Consulta SQL
            Dim dtbDataTableTemporal As DataTable = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            'evento a LOG
            'principal.pInfo_a_Log("Se Inicia Proceso de Ajuste para : " & dtbDataTableTemporal.Rows.Count.ToString() & " Registros")

            'valor de la barra de progreso al numero de filas a ajustar
            Me.pbr_estado_2.Maximum = dtbDataTableTemporal.Rows.Count

            'recorremos cada fila del DataTable
            For Each drwFila In dtbDataTableTemporal.Rows
                'incrementamos el contador de filas
                intNroFila = intNroFila + 1

                If intNroFila Mod 100 = 0 Or intNroFila = Me.pbr_estado_2.Maximum Then
                    'mostramos el mensaje de estado actual
                    Me.lbl_estado_2.Text = "Ajustando Articulo " & intNroFila.ToString() & "/" & dtbDataTableTemporal.Rows.Count.ToString()
                    Me.pbr_estado_2.Value = intNroFila

                    'mostramos el porcentaje procesado
                    Me.lbl_porcentaje.Text = Math.Ceiling(Convert.ToDouble((intNroFila * 100) / Me.pbr_estado_2.Maximum)).ToString() & "%"

                    'ejecutamos eventos pendientes
                    Application.DoEvents()

                    'establecemos el cursor del mouse a "Esperar..."
                    Cursor.Current = Cursors.WaitCursor

                End If

                'tomamos los valores de cada campo y llamamos a la Funcion de Ejecucion de Ajustes
                If SISTEMAS.bolEjecutar_Ajuste(drwFila.Item("ID_LOCAL").ToString() _
                                                , drwFila.Item("FECHA_INVENTARIO").ToString() _
                                                , drwFila.Item("SCANNING").ToString() _
                                                , Format(drwFila.Item("CANTIDAD_AJUSTE"), "###0.00") _
                                                , Format(drwFila.Item("COSTO"), "###0.00") _
                                                , Format(drwFila.Item("CANTIDAD_TEORICA"), "###0.00") _
                                                ) Then
                    'si se ejecuto correctamente, llamamos a procedimiento de Marcado de "Registro Ajustado"
                    If Not bMarcar_Articulo_Ajustado(drwFila.Item("SCANNING").ToString()) Then
                        'si no se pudo marcar el articulo, evento a LOG
                        'principal.pInfo_a_Log("A pesar de haberse Ajustado, no se pudo Marcar como tal el Articulo : " & drwFila.Item(1).ToString())

                    End If

                Else
                    'si no se Ejecuto Correctamente, evento a LOG
                    'principal.pInfo_a_Log("Articulo No Ajustado : " & drwFila.Item("SCANNING").ToString())
                    'principal.pInfo_a_Log("Registro Nro : " & intNroFila.ToString())

                    'si se ejecuto correctamente, llamamos a procedimiento de Marcado de "Registro Ajustado"
                    If Not bMarcar_Articulo_No_Ajustado(drwFila.Item("SCANNING").ToString()) Then
                        'si no se pudo marcar el articulo, evento a LOG
                        'principal.pInfo_a_Log("A pesar de haberse Ajustado, no se pudo Marcar como tal el Articulo : " & drwFila.Item(1).ToString())

                    End If

                    'marcamos el error
                    bHay_Error = True

                End If

            Next drwFila

            'refrescamos el formulario
            Me.Refresh()

            'mostramos el mensaje de estado actual
            Me.lbl_estado_2.Text = "Cerrando Inventario..."

            'llamamos a funcion de Cierre del Inventario
            If Not Me.bCerrar_Inventario() Then
                'si no se pudo cerrar, evento a LOG
                'principal.pInfo_a_Log("Articulos Ajustados pero No se Pudo Cerrar el Inventario debido al Error Anterior")

            Else
                'evento a LOG
                'principal.pInfo_a_Log("Inventario Cerrado sin Errores, Conteo Aplicado = " & nNro_Conteo_Aplicado.ToString())

            End If

            'mostramos el mensaje de estado actual
            Me.lbl_estado_2.Text = "Ajustes Realizados.."

            'si no hubo errores
            If Not bHay_Error Then
                'evento a LOG
                'principal.pInfo_a_Log("Articulos Ajustados Totalmente")

                'mensaje de notificacion
                MessageBox.Show("Articulos Totalmente Ajustados!", "Ajuste de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                'evento a LOG
                'principal.pInfo_a_Log("Se encontraron Articulos No Ajustados")

                'mensaje de notificacion
                MessageBox.Show("Se Encontraron Articulos No Ajustados!!!..." _
                                    & Chr(13) & "Puede Verlos desde el Reporte", "Ajuste de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End If

            'cambiamos el valor de la variable de control de estado de proceso
            bActualmenteProcesando = False

            'establecemos el cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'desbloqueamos los controles
            Me.pDesbloquear_Controles()

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando Ejecutar Ajuste : " & cSentenciaSQL)
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'cambiamos el valor de la variable de control de estado de proceso
            bActualmenteProcesando = False

            'establecemos el cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

            'mostramos el mensaje de estado actual
            Me.lbl_estado_2.Text = "Ajuste con Errores..."

            'mensaje de notificacion
            MessageBox.Show(Ex.Message, "Aplicar Ajustes de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'desbloqueamos los controles
            Me.pDesbloquear_Controles()

        End Try

    End Sub

    ''' <summary>
    ''' MARCA UN REGISTRO DE LA TABLA [EXISTENCIAS] COMO "AJUSTADO" Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="sScanning">Scannng del Articulo a ser Marcado</param>
    ''' <returns>Devuelve 'True' si se Marco Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bMarcar_Articulo_Ajustado(ByVal sScanning As String) As Boolean

        'llamamos a funcion de Ejecucion de Sentencia SQL y devolvemos el resultado del mismo como resultado de esta funcion
        Return principal.bEjecutar_SentenciaSQL(String.Format(SP_MARCAR_ARTICULO_AJUSTADO, cID_Inventario, sScanning))

    End Function

    ''' <summary>
    ''' MARCA UN REGISTRO DE LA TABLA [EXISTENCIAS] COMO "NO AJUSTADO" Y DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="sScanning">Scannng del Articulo a ser Marcado</param>
    ''' <returns>Devuelve 'True' si se Marco Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bMarcar_Articulo_No_Ajustado(ByVal sScanning As String) As Boolean

        'llamamos a funcion de Ejecucion de Sentencia SQL y devolvemos el resultado del mismo como resultado de esta funcion
        Return principal.bEjecutar_SentenciaSQL(String.Format(SP_MARCAR_ARTICULO_NO_AJUSTADO, cID_Inventario, sScanning))

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
        nodoNivel1 = Nothing
        nodoNivel2 = Nothing
        nodoNivel3 = Nothing
        nodoNivel4 = Nothing

        Dim dgrDataTableSectores As DataTable = principal.dtEjecutar_ConsultaSQL("EXECUTE [SP_OBTENER_SECTORES_PARA_FILTROS] @id_inventario = " & cID_Inventario) 'SISTEMAS.dtbSeleccionar_Sectores(String.Empty)

        Try
            ' Agregar al TreeView los nodos Hijos que se han obtenido en el DataView.
            For Each drwFila In dgrDataTableSectores.Rows
                'obtenemos el nombre del sector
                Dim nuevoNodo As New TreeNode(drwFila.Item("NOMBRE_SECTOR"))
                nuevoNodo.Tag = drwFila.Item("NIVEL").ToString.Trim() & "|" & drwFila.Item("ID_SECTOR").ToString.Trim()

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
            'principal.pInfo_a_Log("Error intentando crear Arbol de Sectores")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

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
            Dim sCodigo As String = Me.cCodigo_Sector()

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
                If Me.bCargar_Grilla_Resumen(Me.cConsultaAuxiliar) Then
                    'refrescamos el formulario
                    Me.Refresh()
                End If

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando obtener Codigo de Sector Seleccionado, Filtro de Existencias")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' DEVUELVE EL CODIGO DE SECTOR DE ACUERDO AL NODO SELECCIONADO
    ''' </summary>
    ''' <returns>Devuelve el Codigo del Sector Seleccionado</returns>
    ''' <remarks></remarks>
    Private Function cCodigo_Sector() As String
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
                'si no es ninguno de los casos contemplados, devolvemos vacio
                Return String.Empty

        End Select

        'devolvemos el codigo
        Return sCodigo


    End Function

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

        'pasamos los valores de cada celda al formulario de modificacion de cantidades
        frm_modificar_cantidad.lbl_scanning.Text = Me.grd_detalles.Rows(nNroFila).Cells("SCANNING").Value.ToString()
        frm_modificar_cantidad.lbl_articulo.Text = Me.grd_detalles.Rows(nNroFila).Cells("ARTICULO").Value.ToString()
        frm_modificar_cantidad.lbl_locacion.Text = Me.grd_detalles.Rows(nNroFila).Cells("LOCACION").Value.ToString()
        frm_modificar_cantidad.lbl_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("SOPORTE").Value.ToString()
        frm_modificar_cantidad.lbl_nro_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("NRO_SOPORTE").Value.ToString()
        frm_modificar_cantidad.lbl_letra_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("CARA").Value.ToString()
        frm_modificar_cantidad.lbl_id_locacion.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_LOCACION").Value.ToString()
        frm_modificar_cantidad.lbl_id_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_SOPORTE").Value.ToString()
        frm_modificar_cantidad.lbl_id_letra_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_LETRA_SOPORTE").Value.ToString()
        frm_modificar_cantidad.lbl_nivel.Text = Me.grd_detalles.Rows(nNroFila).Cells("NIVEL").Value.ToString()
        frm_modificar_cantidad.lbl_metro.Text = Me.grd_detalles.Rows(nNroFila).Cells("METRO").Value.ToString()
        frm_modificar_cantidad.lbl_conteo.Text = Me.grd_detalles.Rows(nNroFila).Cells("NRO_CONTEO").Value.ToString()
        frm_modificar_cantidad.txt_conteo_1.Text = Me.grd_detalles.Rows(nNroFila).Cells("CANTIDAD").Value

        'desplegamos el formulario en modo dialogo
        frm_modificar_cantidad.ShowDialog()

        'si el resultado del dialogo es "OK"
        If frm_modificar_cantidad.DialogResult = Windows.Forms.DialogResult.OK Then
            'actualizamos los valores en las celdas de cantidades de la fila modificada
            Me.grd_detalles.Rows(nNroFila).Cells("CANTIDAD").Value = frm_modificar_cantidad.txt_conteo_1.Text

            'ensamblamos la Consulta SQL
            cSentenciaSQL = "SELECT " & Me.cListaBasicaCamposResumen _
                              & " FROM [VW_EXISTENCIAS] " _
                              & " WHERE [ID_INVENTARIO]= " & cID_Inventario

            'llamamos a funcion de carga de datos a grilla
            If Me.bCargar_Grilla_Resumen(cSentenciaSQL) Then
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

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Exportar a Excel"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_exportar_excel_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_exportar_excel_2.Click
        'llamamos a procedimiento de despliegue exportacion a excel
        Me.pExportar_A_Excel()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Aplicar Conteo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_aplicar_conteo_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_aplicar_conteo_2.Click
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
    ''' CUANDO SE HACE CLICK EN EL MENU "Salir"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_salir_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_salir_2.Click
        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Ejecutar Ajustes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks> 
    Private Sub mnu_ejecutar_ajustes_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_ejecutar_ajustes_2.Click
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'si no hay un conteo aplicado
        If Not bConteoAplicado Then
            'mensaje de notificacion
            MessageBox.Show("No hay un Ajuste Aplicado al Inventario!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'llamamos a procedimiento de Ejecucion de Ajustes
        Me.pEjecutar_Ajustes()

        'mostramos el estado del inventario
        FrmPrincipal.lbl_estado_inventario.Text = IIf(principal.bInventario_Cerrado, "Cerrado", "Abierto")

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Desplegar Reporte"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_desplegar_reporte_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_desplegar_reporte_2.Click
        'desplegamos el formulario de seleccion de criterios del reporte de existencias en modo dialogo
        frm_reporte_existencias.ShowDialog()

    End Sub

    ''' <summary>
    ''' CUANDO DE HACE CLICK EN EL MENU "Cubo Excel"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_cubo_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_cubo_excel.Click
        'si el archivo de Cubos no Existe
        If Not File.Exists(CurDir() & "\CUBOS\CUBOS.xlsx") Then
            'evento a LOG
            'principal.pInfo_a_Log("No se Encuentra el Archivo de Cubos " & CurDir() & "\CUBOS\CUBOS.xlsx")

            'mensaje de notifiacion
            MessageBox.Show("No se Encuentra el Archivo de Cubos " _
                            & Chr(13) & CurDir() & "\CUBOS\CUBOS.xlsx", "Archivo de Cubos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'salimos del procedimiento
            Exit Sub

        End If

        Try
            'abrimos el archivo Excel de Cubos
            Process.Start(CurDir() & "\CUBOS\CUBOS.xlsx")

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error Abriendo el Archiv ode Cubos : " & CurDir() & "\CUBOS\CUBOS.xlsx")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

            'mensaje de notificacion
            MessageBox.Show("Error Intentando Abrir el Archivo de Cubos" _
                                & Chr(13) & Ex.Message, "Archivo de Cubos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    ''' <summary>
    ''' BLOQUEA LOS CONTROLES DEL FORMULARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pBloquear_Controles()
        'bloqueamos los controles del Formulario
        Me.MenuStrip1.Enabled = False
        Me.tab_padre.Enabled = False

        Me.lbl_porcentaje.Text = String.Empty

    End Sub

    ''' <summary>
    ''' DESBLOQUEA LOS CONTROLES DEL FORMULARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pDesbloquear_Controles()
        'bloqueamos los controles del Formulario
        Me.MenuStrip1.Enabled = True
        Me.tab_padre.Enabled = True

        Me.lbl_porcentaje.Text = String.Empty

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE EJECUCION DE ALGORITMO DE CALCULO DE CANTIDADES 
    ''' PARA AJUSTES A APLICAR POR ARTICULO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCalcular_Pre_Ajustes()
        'variables a utilizar
        Dim dgrColumna As DataGridViewColumn
        Dim dgrFila As DataGridViewRow
        Dim nColConteo1, nColConteo2, nColConteo3, nColAjuste, nColTeorica, nColParaAjuste As Integer

        'seteamos las variables de Columnas
        nColConteo1 = -1
        nColConteo2 = -1
        nColConteo3 = -1
        nColParaAjuste = -1
        nColAjuste = -1
        nColTeorica = -1

        'cursor del mouse a 'Esperar...'
        Cursor.Current = Cursors.WaitCursor

        Try
            'recorremos las columnas de la grilla
            For Each dgrColumna In Me.grd_existencias.Columns
                'si el titulo de la columna es 'CONTEO_1'
                If dgrColumna.HeaderText.Trim.ToUpper() = "CONTEO_1" Then
                    nColConteo1 = dgrColumna.Index
                End If
                'si el titulo de la columna es 'CONTEO_2'
                If dgrColumna.HeaderText.Trim.ToUpper() = "CONTEO_2" Then
                    nColConteo2 = dgrColumna.Index
                End If
                'si el titulo de la columna es 'CONTEO_3'
                If dgrColumna.HeaderText.Trim.ToUpper() = "CONTEO_3" Then
                    nColConteo3 = dgrColumna.Index
                End If
                'si el titulo de la columna es 'AJUSTE'
                If dgrColumna.HeaderText.Trim.ToUpper() = "CANTIDAD_AJUSTE" Then
                    nColAjuste = dgrColumna.Index
                End If
                'si el titulo de la columna es 'CANTIDAD_TEORICA'
                If dgrColumna.HeaderText.Trim.ToUpper() = "CANTIDAD_TEORICA" Then
                    nColTeorica = dgrColumna.Index
                End If

            Next

            'verificamos que todas las columnas de conteos hayan sido identificadas
            If nColConteo1 > -1 And nColConteo2 > -1 And nColConteo3 > -1 And nColAjuste > -1 And nColTeorica > -1 Then
                'establecemos el valor maximo de la barra de progreso a la cantidad de filas de la grilla
                Me.pbr_estado_2.Maximum = Me.grd_existencias.RowCount()
                Me.pbr_estado_2.Value = 0

                'recorremos las filas de la grilla
                For Each dgrFila In Me.grd_existencias.Rows
                    'establecemos el valor de la barra de progreso al indice de la fila actual + 1
                    Me.pbr_estado_2.Value = dgrFila.Index + 1

                    'obtenemos el numero de columna a utilizar para el ajuste
                    nColParaAjuste = nObtener_Columna_Para_Ajuste(dgrFila.Cells(nColConteo1).Value, dgrFila.Cells(nColConteo2).Value, dgrFila.Cells(nColConteo3).Value)

                    'marcamos con un color la celda del conteo a utilizar para el ajuste
                    'y pasamos el valor de la columna del conteo a la de ajuste
                    If nColParaAjuste = 3 Then
                        dgrFila.Cells(nColConteo3).Style.BackColor = Color.LightBlue
                        dgrFila.Cells(nColAjuste).Value = dgrFila.Cells(nColConteo3).Value - dgrFila.Cells(nColTeorica).Value
                    Else
                        If nColParaAjuste = 2 Then
                            dgrFila.Cells(nColConteo2).Style.BackColor = Color.LightBlue
                            dgrFila.Cells(nColAjuste).Value = dgrFila.Cells(nColConteo2).Value - dgrFila.Cells(nColTeorica).Value

                        Else
                            dgrFila.Cells(nColConteo1).Style.BackColor = Color.LightBlue
                            dgrFila.Cells(nColAjuste).Value = dgrFila.Cells(nColConteo1).Value - dgrFila.Cells(nColTeorica).Value

                        End If
                    End If

                Next

                'ejecutamos el procedimiento del lado de la base de datos
                If Not principal.bEjecutar_SentenciaSQL(String.Format(SP_CALCULAR_PRE_AJUSTES, cID_Inventario)) Then
                    'si no se ejecuto, evento a LOG
                    'principal.pInfo_a_Log("No se ejecuto el SP de Calculo de Pre Ajustes")

                End If

                'cursor del mouse a 'Normal'
                Cursor.Current = Cursors.Default

            Else
                'sino, falta alguna de las columnas
                Dim cNombreColumnaFaltante As String = String.Empty

                'buscamos el nombre de la columna faltante
                cNombreColumnaFaltante = IIf(nColConteo1 < 0, "'CONTEO_1'", IIf(nColConteo2 < 0, "'CONTEO_2'", IIf(nColConteo3 < 0, "'CONTEO_3'", IIf(nColTeorica < 0, "CANTIDAD_TEORICA", "CANTIDAD_AJUSTE"))))

                'evento a LOG
                'principal.pInfo_a_Log("Error en Procedimiento de Pre Ajustes. No se Encontró la Columna " & cNombreColumnaFaltante)

                'cursor del mouse a 'Normal'
                Cursor.Current = Cursors.Default

                'mensaje de notificacion
                MessageBox.Show("No se Encontró la Columna " & cNombreColumnaFaltante & "!", "Ejecución de Cálculos pre Ajustes", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error en Procedimiento de Pre Ajustes.")
            'principal.pInfo_a_Log("Detalles del Error: ", Ex.Message())

            'cursor del mouse a 'Normal'
            Cursor.Current = Cursors.Default

        End Try

    End Sub

    ''' <summary>
    ''' DEVUELVE LA CANTIDAD A APLICAR COMO AJUSTE DE ENTRE LAS TRES PASADAS
    ''' </summary>
    ''' <param name="nConteo1">Cantidad del Conteo1</param>
    ''' <param name="nConteo2">Cantidad del Conteo2</param>
    ''' <param name="nConteo3">Cantidad del Conteo3</param>
    ''' <returns>Devuelve un Double con la Cantidad Seleccionada</returns>
    ''' <remarks></remarks>
    Private Function nObtener_Columna_Para_Ajuste(ByVal nConteo1 As Integer, ByVal nConteo2 As Integer, ByVal nConteo3 As Integer) As Double
        'si el Conteo 3 es diferente de cero
        If Not nConteo3.Equals(0) Then
            'retornamos 3
            Return 3

        Else
            'sino, si el Conteo 2 es diferente de cero
            If Not nConteo2.Equals(0) Then
                'retornamos 2
                Return 2

            Else
                'sino, devolvemos 1
                Return 1

            End If
        End If

    End Function

    ''' <summary>
    ''' CUANDO SE HACE CLICK SOBRE EL RADIO BUTTON "Verdadero"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub opt_verdadero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opt_verdadero.CheckedChanged
        'si esta seleccionado este control
        If Me.opt_verdadero.Checked Then
            'llamamos al procedimiento de busqueda del registro
            Me.pEvaluar_Valor_Buscado()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK SOBRE EL RADIO BUTTON "Falso"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub opt_falso_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opt_falso.CheckedChanged
        'si esta seleccionado este control
        If Me.opt_falso.Checked Then
            'llamamos al procedimiento de busqueda del registro
            Me.pEvaluar_Valor_Buscado()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ESTADO DE SELCCION DEL CHECKBOX "Marcar Todos para Ajustar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_marcar_todos_para_ajustar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_marcar_todos_para_ajustar.CheckedChanged

        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'llamamos al procedimiento de marcado-desmarcado de los registros visibles
        Me.pMarcar_Parar_Ajustar(IIf(Me.chk_marcar_todos_para_ajustar.Checked, 1, 0))

    End Sub

    ''' <summary>
    ''' MARCA TODOS LOS REGISTROS VISIBLES DE LA GRILLA PARA SER AJUSTADOS O NO
    ''' </summary>
    ''' <param name="nValor">Valor con que se marcara [1=True|0= False]</param>
    ''' <remarks></remarks>
    Private Sub pMarcar_Parar_Ajustar(ByVal nValor As String)
        'variables auxiliares
        Dim cSentenciaActualizacion As String = String.Empty

        'si el campo de seleccion es "[SIN CONTEOS]"
        If Me.cmb_campos.Text.Equals("[SIN CONTEOS]") Then
            'establecemos la sentencia de actualizacion para no contabilizados
            cSentenciaActualizacion = String.Format(SP_MARCAR_NO_CONTABILIZADOS, cID_Inventario, IIf(nValor = 1, "'True'", "'False'"))

        Else
            'sino, establecemos la sentencia de actualizacion comun
            cSentenciaActualizacion = String.Format(UPDATE_VW_EXISTENCIAS, IIf(nValor = 1, "'True'", "'False'"), cID_Inventario)

        End If

        'anadimos la condicion de filtrado a la sentencia
        cSentenciaActualizacion += " " & Me.cCondicion

        'llamamos a funcion de Ejecucion de Sentencias SQL
        If principal.bEjecutar_SentenciaSQL(cSentenciaActualizacion) Then

            'actualizamos los datos en la tabla de existencias
            principal.bEjecutar_SentenciaSQL(String.Format(UPDATE_EXISTENCIAS, IIf(nValor = 1, "'True'", "'False'"), cID_Inventario))

            'si se ejecuto la sentencia, llamamos a funcion de actualizacion de datos de la grilla
            If Me.bCargar_Grilla_Resumen(Me.cConsultaPrincipal & Me.cCondicion) Then
                'refrescamos el formulario
                Me.Refresh()

            End If

            'salimos del sub
            Exit Sub

        Else
            'si no se ejecuto la sentencia, evento a LOG
            'principal.pInfo_a_Log("No se Ejecuto la Sentencia de Marcado de Registros para Ajustes: " & cSentenciaActualizacion)

            'mensaje de notificacion
            MessageBox.Show("No se Ejecuto la Sentencia de Marcado de Registros para Ajustes", "Datos de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL VALO0R DE UNA CELDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_existencias_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_existencias.CellValueChanged
        'si la columna es del campo "[AJUSTAR]"
        If Me.grd_existencias.Columns(Me.grd_existencias.SelectedCells(0).ColumnIndex).HeaderText.Equals("AJUSTAR") Then
            'variables auxiliares
            Dim cScanning As String = String.Empty
            Dim bEstado As Boolean = Me.grd_existencias.SelectedCells(0).Value

            'obtenemos el valor del campo de "[SCANNING]"
            cScanning = Me.grd_existencias.Rows(Me.grd_existencias.SelectedCells(0).RowIndex).Cells(Me.grd_existencias.Columns("SCANNING").Index).Value

            'MessageBox.Show("Valor de la Celda es : " & Me.grd_existencias.SelectedCells(0).Value.ToString() & " y el scanning es " & cScanning)

            'llamamos a funcion de marcado del articulo
            If Me.bMarcar_Articulo_Ajuste(cScanning, bEstado) Then
            End If

        End If

    End Sub

    ''' <summary>
    ''' MARCA UN SCANNING PARA INCLUIRLO - EXCLUIRLO DEL AJUSTE FINAL, DEVUELVE UN BOOLEAN
    ''' </summary>
    ''' <param name="cScanning">Scanning del Articulo</param>
    ''' <param name="bEstado">Valor para marca [True|False]</param>
    ''' <returns>Devuelve 'True' si se Ejecuto Correctamente</returns>
    ''' <remarks></remarks>
    Private Function bMarcar_Articulo_Ajuste(ByVal cScanning As String, ByVal bEstado As Boolean) As Boolean

        'llamamos a la funcion de Ejecucion de Sentencias SQL y devolvemos el resultado de la misma
        'como resultado de esta funcion
        Return principal.bEjecutar_SentenciaSQL(String.Format(SP_MARCAR_ARTICULO_PARA_AJUSTE, cScanning, bEstado.ToString()))

    End Function

    ''' <summary>
    ''' MARCA CON COLOR LAS CELDAS DE LA COLUMNA DEL CAMPO [DIFERENCIA_1_2] QUE SEAN DIFERENTES DE CERO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pMarcar_Diferencias(ByRef oNombreGrilla As DataGridView)
        'variables auxiliares
        Dim nColumnaDiferencia12 As Integer = -1

        'capturamos el indice de la columna del campo "[DIFERENCIA_1_2]"
        nColumnaDiferencia12 = oNombreGrilla.Columns("DIFERENCIA_1_2").Index

        'si se encontro la caolumna buscada
        If nColumnaDiferencia12 >= 0 Then
            'recorremos las filas de la grilla
            For Each drwFilaActual As DataGridViewRow In oNombreGrilla.Rows
                'si el calor del campo de "[DIFERENCIA_1_2]" es diferente a cero
                If Not Val(drwFilaActual.Cells(nColumnaDiferencia12).Value).Equals(0) Then
                    'cambiamos el color de fondo de la columna de la celda
                    drwFilaActual.Cells(nColumnaDiferencia12).Style.BackColor = Color.LightPink

                End If

            Next drwFilaActual

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN LAS CABECERAS DE LAS COLUMNAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks> 
    Private Sub grd_existencias_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd_existencias.ColumnHeaderMouseClick
        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'llamamos a procedimiento de coloreado de campo de [DIFERENCIA_1_2]
        Me.pMarcar_Diferencias(Me.grd_existencias)

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU DE "Reporte Diferencias"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PDiferenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDiferenciasToolStripMenuItem.Click
        'mostramos el formulario de seleccion de criterios del reporte de diferencias
        frm_rpt_diferencias.ShowDialog()

    End Sub

    ''' <summary>
    ''' EXPORTA EL CONTENIDO DE LA GRILLA DE EXISTENCIAS A UN ARCHIVO PLANO, DEVUELVE BOOLEAN
    ''' </summary>
    ''' <param name="oArchivoPlano">Instancia del Objeto de Archivo Plano</param>
    ''' <returns>Devuelte 'True' si se exporto Correctamente</returns>
    ''' <remarks></remarks>
    Public Function bExportar_DataGrid_A_Archivo_Plano(ByRef oArchivoPlano As ArchivosPlanos) As Boolean
        'variables a utilizar
        Dim intFil As Integer = 0
        Dim intCol As Integer = 0
        Dim drwFilas As DataGridViewRow
        Dim dclCelda As DataGridViewCell
        Dim bolPrimeraFila As Boolean = True
        Dim strLineaCabecera As String = String.Empty
        Dim strLineaActual As String = String.Empty

        Dim intDivisor As Integer = IIf(grd_existencias.RowCount > 50000, 1000, 100)

        Dim SEPARADOR_CAMPOS As Char = ";"

        'Colocar el cursor de espera mientras se exportan los datos
        Cursor.Current = Cursors.WaitCursor

        Me.Refresh()

        Try
            'si la cantidad de filas es cero
            If grd_existencias.RowCount <= 0 Then
                'mensaje de notificacion
                MessageBox.Show("No hay datos en la Grilla a exportar al Archivo...", "Exportar a Archivo Plano", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                'establecemos el resultado del metodo
                bExportar_DataGrid_A_Archivo_Plano = False

            Else
                'creamos la cabecera del documento
                If Not oArchivoPlano.bEscribir_Linea("Datos de Inventario Realizado") Then Return False
                oArchivoPlano.bEscribir_Linea("ID Local : " & principal.cID_Local)
                oArchivoPlano.bEscribir_Linea("Nombre Local : " & principal.cNombre_Local)
                oArchivoPlano.bEscribir_Linea("Sistema de Gestion : " & principal.cID_Sistema_Gestion & " - " & principal.cNombre_Sistema_Gestion)
                oArchivoPlano.bEscribir_Linea("Fecha Inventario : " & principal.cFecha_Inventario)
                oArchivoPlano.bEscribir_Linea("Estado del Inventario :" & IIf(principal.bInventario_Cerrado, "Cerrado", "Abierto"))
                oArchivoPlano.bEscribir_Linea("Comentarios : " & principal.cComentario_Inventario)

                oArchivoPlano.bEscribir_Linea("")

                'establecemos el valor maximo de la barra de progreso
                Me.pbr_estado_2.Maximum = grd_existencias.RowCount

                'establecemos el estilo de la barra de progreso a "Continuo"
                Me.pbr_estado_2.Style = ProgressBarStyle.Continuous

                'recorremos las filas del DataGrid
                For Each drwFilas In grd_existencias.Rows
                    'si el nro de fila actual es multiplo de
                    If (intFil Mod (grd_existencias.RowCount / intDivisor)).Equals(0) Or (intFil >= grd_existencias.RowCount) Then
                        'mostramos el mensaje en la barra de estado
                        Me.lbl_estado_2.Text = String.Format("Exportando Fila {0} de {1}", intFil, grd_existencias.RowCount)
                        Me.pbr_estado_2.Value = intFil

                    End If

                    'reseteamos el contador de columnas
                    intCol = 0

                    'recorremos las filas del DataGrid
                    For Each dclCelda In drwFilas.Cells
                        'si es la primera fila
                        If bolPrimeraFila Then
                            'si es la primera columna
                            If intCol.Equals(0) Then
                                'concatenamos el caption de la columna a la linea a escribir
                                strLineaCabecera = grd_existencias.Columns(intCol).HeaderText

                            Else
                                'concatenamos el caption de la columna a la linea a escribir
                                strLineaCabecera = strLineaCabecera & SEPARADOR_CAMPOS & grd_existencias.Columns(intCol).HeaderText

                            End If
                        End If

                        'si es la primera columna
                        If intCol.Equals(0) Then
                            'concatenamos el valor de la celda a la linea a escribir
                            strLineaActual = dclCelda.Value.ToString

                        Else
                            'concatenamos el valor de la celda a la linea a escribir
                            strLineaActual = strLineaActual & SEPARADOR_CAMPOS & dclCelda.Value.ToString

                        End If

                        'incrementamos el contador de celdas
                        intCol = intCol + 1

                    Next

                    'si es la primera linea
                    If bolPrimeraFila Then
                        'escribimos el valor de la celda actual
                        oArchivoPlano.bEscribir_Linea(strLineaCabecera)

                        strLineaCabecera = Nothing
                        bolPrimeraFila = False

                    End If

                    'escribimos el valor de la celda actual
                    oArchivoPlano.bEscribir_Linea(strLineaActual)

                    'incrementamos el contador de filas
                    intFil += 1

                Next

                'mensaje de estado
                Me.lbl_estado_2.Text = "Archivo Generado..."

            End If

            'Restauramos el cursor
            Cursor.Current = Cursors.Default

            'establecemos el resultado del metodo
            bExportar_DataGrid_A_Archivo_Plano = True

        Catch Ex As Exception
            'en caso de error, establecemos el cursor a "Normal"
            Cursor.Current = Cursors.Default

            'establecemos el resultado del metodo
            bExportar_DataGrid_A_Archivo_Plano = False

        End Try

    End Function

End Class