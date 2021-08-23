Imports System.Windows.Forms

'espacio de nombres del Motor del Crystal
Imports CrystalDecisions.CrystalReports.Engine
'espacio de nombres para Datos del Crystal
Imports CrystalDecisions.Shared

Public Class frm_reporte_existencias

#Region "VARIABLES_LOCALES"
    Private cConsulta_Principal As String = String.Empty
    Private cCondiciones As String = String.Empty
    Private cCodigo_Sector As String = String.Empty
    Private cCampo_Sector As String = String.Empty
    Private dtbDataTableSectores As DataTable = Nothing
    Private bReplegar_Arbol As Boolean = False

#End Region

    ''' <summary>
    ''' CUANDO SE CARGA EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_reporte_existencias_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'evento a LOG
        'principal.pInfo_a_Log("Se carga el Formulario : " & Me.Name)

        'establecemos la Consulta Principal de acuerdo al sistema de gestion
        Me.cConsulta_Principal = "SELECT * FROM [VW_RPT_EXISTENCIAS_EME] WHERE [ID_INVENTARIO] = " & principal.cID_Inventario

        'llamamos a la funcion de obtencion de Sectores
        Me.dtbDataTableSectores = principal.dtEjecutar_ConsultaSQL("EXECUTE [SP_OBTENER_SECTORES_PARA_FILTROS] @id_inventario = " & cID_Inventario) 'SISTEMAS.dtbObtener_Sectores()

        'llamamos al procedimiento de carga del arbol de sectores
        Me.pCargar_Arbol_Sectores()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE ARBOL DE SECTORES POSIBLES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pCargar_Arbol_Sectores()
        'variables a utilizar
        Dim nodoNivel1, nodoNivel2, nodoNivel3, nodoNivel4 As TreeNode
        nodoNivel1 = Nothing
        nodoNivel2 = Nothing
        nodoNivel3 = Nothing
        nodoNivel4 = Nothing

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            ' Agregar al TreeView los nodos Hijos que se han obtenido en el DataView.
            For Each drwFila In dtbDataTableSectores.Rows
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

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' CUANDO SE HA SELECCIONADO UN NODO DEL ARBOL DE SECTORES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_sectores_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvw_arbol.AfterSelect
        'variables a utilizar
        Dim nPosicionSeparador As Integer = Me.tvw_arbol.SelectedNode.Tag.ToString.IndexOf("|")
        Dim cCodigo As String = Me.tvw_arbol.SelectedNode.Tag.ToString.Substring(nPosicionSeparador + 1)
        Dim nNivel As Integer = Int16.Parse(Me.tvw_arbol.SelectedNode.Tag.ToString.Substring(0, nPosicionSeparador))

        Try
            'evaluamos el caso del nivel sel sector seleccionado
            Select Case nNivel
                Case 1
                    'si el nivel es 1 extraemos los digitos correspondientes
                    cCodigo = cCodigo.Substring(0, 2)

                    'establecemos el campo a filtrar
                    Me.cCampo_Sector = "SECTOR"

                Case 2
                    'si el nivel es 2 extraemos los digitos correspondientes
                    cCodigo = cCodigo.Substring(0, 4)

                    'establecemos el campo a filtrar
                    Me.cCampo_Sector = "SUB_SECTOR_1"

                Case 3
                    'si el nivel es 3 extraemos los digitos correspondientes
                    cCodigo = cCodigo.Substring(0, 6)

                    'establecemos el campo a filtrar
                    Me.cCampo_Sector = "SUB_SECTOR_2"

                Case 4
                    'si el nivel es 8 extraemos los digitos correspondientes
                    cCodigo = cCodigo.Substring(0, 8)

                    'establecemos el campo a filtrar
                    Me.cCampo_Sector = "SUB_SECTOR_3"

                Case 5
                    'si el nivel es 5, lo pasamos como tal
                    cCodigo = cCodigo

                    'establecemos el campo a filtrar
                    Me.cCampo_Sector = "SUB_SECTOR_4"

                Case Else
                    Exit Sub

            End Select

            'pasamos el codigo de sector seleccionado a la variable de nivel de formulario correspondiente
            Me.cCodigo_Sector = cCodigo

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error intentando obtener Codigo de Sector Seleccionado, Filtro de Existencias")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE DOBLE CLICK EN EL ARBOL DE SECTORES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_sectores_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvw_arbol.DoubleClick
        'si el arbol esta desplegado
        If Me.tvw_arbol.Size.Height <= 25 Then
            'llamamos a procedimiento de despliegue del arbol
            Me.pDesplegar_Arbol_Sectores()

        Else
            'sino, llamamos a procedimiento de repliegue
            Me.pReplegar_Arbol_Sectores()

        End If

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE DESPLIEGUE DE ARBOL DE SECTORES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pDesplegar_Arbol_Sectores()
        'establecemos la accion a ejecutar
        Me.bReplegar_Arbol = False

        'establecemos el tamano maximo del arbol
        Me.tvw_arbol.MaximumSize = New Size(220, 290)

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
        Me.bReplegar_Arbol = True

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
        If Me.bReplegar_Arbol Then
            'si el alto es mayor a 21
            If Me.tvw_arbol.Size.Height > 21 Then
                'decrementamos su alto, y su ancho lo dejamos igual
                Me.tvw_arbol.Size = New Size(Me.tvw_arbol.Size.Width, Me.tvw_arbol.Size.Height - 30)

            Else
                'sino, deshabilitamos el Timer
                Me.tmr_arbol.Enabled = False

            End If

        Else
            'sino, lo desplegamos mientras su alto sea menor a 290
            If Me.tvw_arbol.Size.Height < 290 Then
                'incrementamos su alto, y su ancho lo dejamos igual
                Me.tvw_arbol.Size = New Size(Me.tvw_arbol.Size.Width, Me.tvw_arbol.Size.Height + 30)

            Else
                'sino, deshabilitamos el Timer
                Me.tmr_arbol.Enabled = False

            End If

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Sectores"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_sectores_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_sectores.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_sectores.Checked Then
            'habilitamos el combo de operadores y el arbol de sectores
            Me.cmb_sector.Enabled = True
            Me.tvw_arbol.Enabled = True

            'establecemos el valor del combo al primer elemento
            Me.cmb_sector.Text = Me.cmb_sector.Items(0)

        Else
            'sino, los deshabilitamos
            Me.cmb_sector.Enabled = False
            Me.tvw_arbol.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Cod. Articulo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_scanning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_scanning.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_scanning.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_scanning.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_scanning.Text = Me.cmb_scanning.Items(0)

        Else
            'sino, los deshabilitamos
            Me.cmb_scanning.Enabled = False
            Me.txt_min_scanning.Enabled = False
            Me.txt_max_scanning.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE "Cant Teorica"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_cantidad_teorica_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_cantidad_teorica.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_cantidad_teorica.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_cantidad_teorica.Enabled Then Me.txt_min_cantidad_teorica.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_cantidad_teorica.Visible Then Me.lbl_y_cantidad_teorica.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_cantidad_teorica.Enabled Then Me.txt_max_cantidad_teorica.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_cantidad_teorica.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Costos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_costo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_costo.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_costo.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_costo.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_costo.Text = Me.cmb_costo.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_costo.Enabled = False
            Me.txt_min_costo.Enabled = False
            Me.txt_max_costo.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Conteo 1"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_conteo_1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_conteo_1.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_conteo_1.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_conteo_1.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_conteo_1.Text = Me.cmb_conteo_1.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_conteo_1.Enabled = False
            Me.txt_min_conteo_1.Enabled = False
            Me.txt_max_conteo_1.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Conteo 2"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_conteo_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_conteo_2.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_conteo_2.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_conteo_2.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_conteo_2.Text = Me.cmb_conteo_2.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_conteo_2.Enabled = False
            Me.txt_min_conteo_2.Enabled = False
            Me.txt_max_conteo_2.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Conteo 3"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_conteo_3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_conteo_3.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_conteo_3.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_conteo_3.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_conteo_3.Text = Me.cmb_conteo_3.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_conteo_3.Enabled = False
            Me.txt_min_conteo_3.Enabled = False
            Me.txt_max_conteo_3.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Diferencia 1_2"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_diferencia_1_2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_diferencia_1_2.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_diferencia_1_2.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_diferencia_1_2.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_diferencia_1_2.Text = Me.cmb_diferencia_1_2.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_diferencia_1_2.Enabled = False
            Me.txt_min_dif_1_2.Enabled = False
            Me.txt_max_dif_1_2.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Diferencia 2_3"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_diferencia_2_3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_diferencia_2_3.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_diferencia_2_3.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_diferencia_2_3.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_diferencia_2_3.Text = Me.cmb_diferencia_2_3.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_diferencia_2_3.Enabled = False
            Me.txt_min_dif_2_3.Enabled = False
            Me.txt_max_dif_2_3.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Diferencia 1_3"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_diferencia_1_3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_diferencia_1_3.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_diferencia_1_3.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_diferencia_1_3.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_diferencia_1_3.Text = Me.cmb_diferencia_1_3.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_diferencia_1_3.Enabled = False
            Me.txt_min_dif_1_3.Enabled = False
            Me.txt_max_dif_1_3.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CAMBIA EL ESTADO DEL CHECKBOX DE "Cant Ajuste"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_cantidad_ajuste_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_cantidad_ajuste.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_cantidad_ajuste.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_cantidad_ajuste.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_cantidad_ajuste.Text = Me.cmb_cantidad_ajuste.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_cantidad_ajuste.Enabled = False
            Me.txt_min_ajuste.Enabled = False
            Me.txt_max_ajuste.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Sectores"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_sector_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_sector.SelectedIndexChanged
        'si el arbol de sectores no esta habilitado
        If Not Me.tvw_arbol.Enabled Then
            'lo habilitamos
            Me.tvw_arbol.Enabled = True

        End If

        'le pasamos el enfoque al arbol
        Me.tvw_arbol.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Scanning"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_scanning_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_scanning.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_scanning.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_scanning.Enabled Then Me.txt_min_scanning.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_scanning.Visible Then Me.lbl_y_scanning.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_scanning.Enabled Then Me.txt_max_scanning.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_scanning.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Costo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_costo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_costo.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_costo.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_costo.Enabled Then Me.txt_min_costo.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_costo.Visible Then Me.lbl_y_costo.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_costo.Enabled Then Me.txt_max_costo.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_costo.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Conteo 1"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_conteo_1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_conteo_1.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_conteo_1.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_1.Enabled Then Me.txt_min_conteo_1.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_conteo_1.Visible Then Me.lbl_y_conteo_1.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_conteo_1.Enabled Then Me.txt_max_conteo_1.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_conteo_1.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES  DE "Conteo 2"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_conteo_2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_conteo_2.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_conteo_2.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_2.Enabled Then Me.txt_min_conteo_2.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_conteo_2.Visible Then Me.lbl_y_conteo_2.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_conteo_2.Enabled Then Me.txt_max_conteo_2.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_conteo_2.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Conteo 3"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_conteo_3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_conteo_3.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_conteo_3.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_conteo_3.Enabled Then Me.txt_min_conteo_3.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_conteo_3.Visible Then Me.lbl_y_conteo_3.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_conteo_3.Enabled Then Me.txt_max_conteo_3.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_conteo_3.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Diferencia C1 C2"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_diferencia_1_2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_diferencia_1_2.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_diferencia_1_2.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_2.Enabled Then Me.txt_min_dif_1_2.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_dif_1_2.Visible Then Me.lbl_y_dif_1_2.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_dif_1_2.Enabled Then Me.txt_max_dif_1_2.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_dif_1_2.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECIONADO DEL COMBO DE OPERADORES DE "Diferencia C2 C3"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_diferencia_2_3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_diferencia_2_3.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_diferencia_2_3.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_2_3.Enabled Then Me.txt_min_dif_2_3.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_dif_2_3.Visible Then Me.lbl_y_dif_2_3.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_dif_2_3.Enabled Then Me.txt_max_dif_2_3.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_dif_2_3.Focus()
    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Diferencia C1 C3"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_diferencia_1_3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_diferencia_1_3.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_diferencia_1_3.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_dif_1_3.Enabled Then Me.txt_min_dif_1_3.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_dif_1_3.Visible Then Me.lbl_y_dif_1_3.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_dif_1_3.Enabled Then Me.txt_max_dif_1_3.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_dif_1_3.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ITEM SELECCIONADO DEL COMBO DE OPERADORES DE "Cant Ajuste"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmb_cantidad_ajuste_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_cantidad_ajuste.SelectedIndexChanged
        'evaluamos el operador seleccionado
        Select Case Me.cmb_cantidad_ajuste.Text
            Case "es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = False

            Case "no es igual a"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = False

            Case "es mayor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = False

            Case "es menor que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = False

            Case "es mayor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = False

            Case "es menor o igual que"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo esta habilitado, lo deshabilitamos
                If Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = False

            Case "esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = True

            Case "no esta entre"
                'si el cuadro de valor minimo no esta habilitado, lo habilitamos
                If Not Me.txt_min_ajuste.Enabled Then Me.txt_min_ajuste.Enabled = True

                'si la etiqueta "Y:" no esta visible, la visibilizamos
                If Not Me.lbl_y_ajuste.Visible Then Me.lbl_y_ajuste.Visible = True

                'si el cuadro de valor maximo no esta habilitado, lo habilitamos
                If Not Me.txt_max_ajuste.Enabled Then Me.txt_max_ajuste.Enabled = True

            Case Else
                'en otro caso, salimos del sub
                Exit Sub

        End Select

        'enfoque al cuadro de texto del valor minimo
        Me.txt_min_ajuste.Focus()

    End Sub

    ''' <summary>
    ''' CUANDO EL ARBOL DE SECTORES PIERDE EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvw_arbol.LostFocus
        'llamamos a procedimiento de repliegue del arbol
        Me.pReplegar_Arbol_Sectores()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'cerrammos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Visualizar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_visualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_visualizar.Click
        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        'llamamos al procedimiento de ensamblado de la Consulta SQL
        Me.pEnsamblar_Filtros_RPT()

        'llamamos a procedimiento de preparacion del reporte
        Me.pPreparar_Reporte()

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

        'desplegamos el formulario en modo dialogo
        frm_despliega_reportes.ShowDialog()

        'liberamos el formulario
        frm_despliega_reportes.Dispose()


    End Sub

    ''' <summary>
    ''' ENSAMBLA LA CONSULTA SQL DE ACUERDO A LOS CRITERIOS DE FILTRADO SELECCIONADOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pEnsamblar_Filtros_RPT()
        'reseteamos el contenido de la variable de condiciones
        Me.cCondiciones = String.Empty

        'establecemos la primera condicion que es el ID de Inventario
        Me.cCondiciones = " {VW_RPT_GENERAL.ID_INVENTARIO}= " & principal.cID_Inventario

        'evaluamos cada criterio de todos los posibles
        'si hay un sector especificado
        If Me.chk_sectores.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_sector.Text
                Case "es igual a"
                    Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL." & Me.cCampo_Sector & "} LIKE '" & Me.cCodigo_Sector & "*'"
                Case "no es igual a"
                    Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.SECTOR} LIKE '" & Me.cCodigo_Sector & "*')"
                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un scanning especificado
        If Me.chk_scanning.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_scanning.Text()
                Case "es igual a"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} LIKE '" & Me.txt_min_scanning.Text.Trim() & "'"

                Case "no es igual a"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} <> '" & Me.txt_min_scanning.Text.Trim() & "'"

                Case "es mayor que"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} > '" & Me.txt_min_scanning.Text.Trim() & "'"

                Case "es menor que"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} < '" & Me.txt_min_scanning.Text.Trim() & "'"

                Case "es mayor o igual que"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} >= '" & Me.txt_min_scanning.Text.Trim() & "'"

                Case "es menor o igual que"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} <= '" & Me.txt_min_scanning.Text.Trim() & "'"

                Case "esta entre"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) And Not Me.txt_max_scanning.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.SCANNING} IN '" & Me.txt_min_scanning.Text.Trim() & "' TO '" & Me.txt_max_scanning.Text.Trim() & "'"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_scanning.Text.Trim.Length.Equals(0) And Not Me.txt_max_scanning.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.SCANNING} IN '" & Me.txt_min_scanning.Text.Trim() & "' TO '" & Me.txt_max_scanning.Text.Trim() & "')"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay una cantidad_teorica especificada
        If Me.chk_cantidad_teorica.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_cantidad_teorica.Text()
                Case "es igual a"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} = " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} <> " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} > " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} < " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} >= " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} <= " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) And Not Me.txt_max_cantidad_teorica.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_TEORICA} IN " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000" & " TO " & Me.txt_max_cantidad_teorica.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_cantidad_teorica.Text.Trim.Length.Equals(0) And Not Me.txt_max_cantidad_teorica.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND ({VW_RPT_GENERAL.CANTIDAD_TEORICA} IN " & Me.txt_min_cantidad_teorica.Text.Trim() & ".000" & " TO " & Me.txt_max_cantidad_teorica.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un costo especificado
        If Me.chk_costo.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_costo.Text()
                Case "es igual a"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} = " & Me.txt_min_costo.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} <> " & Me.txt_min_costo.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} > " & Me.txt_min_costo.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} < " & Me.txt_min_costo.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} >= " & Me.txt_min_costo.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} <= " & Me.txt_min_costo.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) And Not Me.txt_max_costo.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.COSTO} IN " & Me.txt_min_costo.Text.Trim() & ".000" & " TO " & Me.txt_max_costo.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_costo.Text.Trim.Length.Equals(0) And Not Me.txt_max_costo.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND ({VW_RPT_GENERAL.COSTO} IN " & Me.txt_min_costo.Text.Trim() & ".000" & " TO " & Me.txt_max_costo.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de conteo 1 especificado
        If Me.chk_conteo_1.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_conteo_1.Text()
                Case "es igual a"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} = " & Me.txt_min_conteo_1.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} <> " & Me.txt_min_conteo_1.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} > " & Me.txt_min_conteo_1.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} < " & Me.txt_min_conteo_1.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} >= " & Me.txt_min_conteo_1.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} <= " & Me.txt_min_conteo_1.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) And Not Me.txt_max_conteo_1.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_1} IN " & Me.txt_min_conteo_1.Text.Trim() & ".000" & " TO " & Me.txt_max_conteo_1.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_conteo_1.Text.Trim.Length.Equals(0) And Not Me.txt_max_conteo_1.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.CONTEO_1} IN " & Me.txt_min_conteo_1.Text.Trim() & ".000" & " TO " & Me.txt_max_conteo_1.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de conteo 2 especificado
        If Me.chk_conteo_2.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_conteo_2.Text()
                Case "es igual a"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} = " & Me.txt_min_conteo_2.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} <> " & Me.txt_min_conteo_2.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} > " & Me.txt_min_conteo_2.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} < " & Me.txt_min_conteo_2.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} >= " & Me.txt_min_conteo_2.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} <= " & Me.txt_min_conteo_2.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) And Not Me.txt_max_conteo_2.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_2} IN " & Me.txt_min_conteo_2.Text.Trim() & ".000" & " TO " & Me.txt_max_conteo_2.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_conteo_2.Text.Trim.Length.Equals(0) And Not Me.txt_max_conteo_2.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.CONTEO_2} IN " & Me.txt_min_conteo_2.Text.Trim() & ".000" & " TO " & Me.txt_max_conteo_2.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de conteo 3 especificado
        If Me.chk_conteo_3.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_conteo_3.Text()
                Case "es igual a"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} = " & Me.txt_min_conteo_3.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} <> " & Me.txt_min_conteo_3.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} > " & Me.txt_min_conteo_3.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} < " & Me.txt_min_conteo_3.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} >= " & Me.txt_min_conteo_3.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} <= " & Me.txt_min_conteo_3.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) And Not Me.txt_max_conteo_3.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CONTEO_3} IN " & Me.txt_min_conteo_3.Text.Trim() & ".000" & " TO " & Me.txt_max_conteo_3.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_conteo_3.Text.Trim.Length.Equals(0) And Not Me.txt_max_conteo_3.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.CONTEO_3} IN " & Me.txt_min_conteo_3.Text.Trim() & ".000" & " TO " & Me.txt_max_conteo_3.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de diferencia entre conteo 1 y 2 especificado
        If Me.chk_diferencia_1_2.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_diferencia_1_2.Text()
                Case "es igual a"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} = " & Me.txt_max_dif_1_2.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} <> " & Me.txt_max_dif_1_2.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} > " & Me.txt_max_dif_1_2.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} < " & Me.txt_max_dif_1_2.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} >= " & Me.txt_min_dif_1_2.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} <= " & Me.txt_min_dif_1_2.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) And Not Me.txt_max_dif_1_2.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_2} IN " & Me.txt_min_dif_1_2.Text.Trim() & ".000" & " TO " & Me.txt_max_dif_1_2.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_dif_1_2.Text.Trim.Length.Equals(0) And Not Me.txt_max_dif_1_2.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.DIFERENCIA_1_2} IN " & Me.txt_min_dif_1_2.Text.Trim() & ".000" & " TO " & Me.txt_max_dif_1_2.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de diferencia entre conteo 2 y 3 especificado
        If Me.chk_diferencia_2_3.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_diferencia_2_3.Text()
                Case "es igual a"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} = " & Me.txt_max_dif_2_3.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} <> " & Me.txt_max_dif_2_3.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} > " & Me.txt_max_dif_2_3.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} < " & Me.txt_max_dif_2_3.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} >= " & Me.txt_min_dif_2_3.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} <= " & Me.txt_min_dif_2_3.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) And Not Me.txt_max_dif_2_3.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_2_3} IN " & Me.txt_min_dif_2_3.Text.Trim() & ".000" & " TO " & Me.txt_max_dif_2_3.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_dif_2_3.Text.Trim.Length.Equals(0) And Not Me.txt_max_dif_2_3.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.DIFERENCIA_2_3} IN " & Me.txt_min_dif_2_3.Text.Trim() & ".000" & " TO " & Me.txt_max_dif_2_3.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de diferencia entre conteo 1 y 3 especificado
        If Me.chk_diferencia_1_3.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_diferencia_1_3.Text()
                Case "es igual a"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} = " & Me.txt_max_dif_1_3.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} <> " & Me.txt_max_dif_1_3.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} > " & Me.txt_max_dif_1_3.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} < " & Me.txt_max_dif_1_3.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} >= " & Me.txt_min_dif_1_3.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} <= " & Me.txt_min_dif_1_3.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) And Not Me.txt_max_dif_1_3.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.DIFERENCIA_1_3} IN  " & Me.txt_min_dif_1_3.Text.Trim() & ".000" & " TO " & Me.txt_max_dif_1_3.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_dif_1_3.Text.Trim.Length.Equals(0) And Not Me.txt_max_dif_1_3.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.DIFERENCIA_1_3} IN  " & Me.txt_min_dif_1_3.Text.Trim() & ".000" & " TO " & Me.txt_max_dif_1_3.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si hay un valor de ajuste especificado
        If Me.chk_cantidad_ajuste.Checked Then
            'evaluamos cual es el operador a utilizar
            Select Case Me.cmb_cantidad_ajuste.Text()
                Case "es igual a"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} = " & Me.txt_min_ajuste.Text.Trim() & ".000"

                Case "no es igual a"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} <> " & Me.txt_min_ajuste.Text.Trim() & ".000"

                Case "es mayor que"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} > " & Me.txt_min_ajuste.Text.Trim() & ".000"

                Case "es menor que"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} < " & Me.txt_min_ajuste.Text.Trim() & ".000"

                Case "es mayor o igual que"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} >= " & Me.txt_min_ajuste.Text.Trim() & ".000"

                Case "es menor o igual que"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) Then Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} <= " & Me.txt_min_ajuste.Text.Trim() & ".000"

                Case "esta entre"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) And Not Me.txt_max_ajuste.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.CANTIDAD_AJUSTE} IN  " & Me.txt_min_ajuste.Text.Trim() & ".000" & " TO " & Me.txt_max_ajuste.Text.Trim() & ".000"
                    End If

                Case "no esta entre"
                    If Not Me.txt_min_ajuste.Text.Trim.Length.Equals(0) And Not Me.txt_max_ajuste.Text.Trim.Length.Equals(0) Then
                        Me.cCondiciones = Me.cCondiciones & " AND NOT({VW_RPT_GENERAL.CANTIDAD_AJUSTE} IN  " & Me.txt_min_ajuste.Text.Trim() & ".000" & " TO " & Me.txt_max_ajuste.Text.Trim() & ".000" & ")"
                    End If

                Case Else
                    'salimos del select
                    Exit Select
            End Select

        End If

        'si esta seleccionada la opcion de "Ajustado"
        If Me.chk_ajustado.Checked Then
            'si el valor seleccionado es "SI"
            If Me.cmb_ajustado.Text.ToUpper.Trim.Equals("SI") Then
                Me.cCondiciones = Me.cCondiciones & " AND {VW_RPT_GENERAL.AJUSTADO}"

            Else
                Me.cCondiciones = Me.cCondiciones & " AND NOT {VW_RPT_GENERAL.AJUSTADO}"

            End If

        End If

    End Sub

    ''' <summary>
    ''' PREPARA EL REPORTE APLICANDO LOS CRITERIOS DE SELECCION
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pPreparar_Reporte()
        '-------------------------------------------------------------------------------------------------'
        '   AQUI EMPEZAMOS A MANIPULAR LOS OBJETOS DEL REPORTE
        '-------------------------------------------------------------------------------------------------'
        'variables a utilizar
        Dim oInfoDeConexion As ConnectionInfo
        Dim oListaTablas As Tables
        Dim oTabla As Table
        Dim oInfoConnDeTabla As TableLogOnInfo
        'Dim oDefinicionDeCampo As FieldDefinition
        Dim oTextoTitulo, oTextoFiltro As TextObject

        Dim rptReporte As Object
        rptReporte = New rpt_existencias_eme()

        'instanciamos los objetos para guardar los datos de conexion
        oInfoDeConexion = New ConnectionInfo

        Try
            'asignamos los valores
            oInfoDeConexion.ServerName = principal.cServidorSQL
            oInfoDeConexion.DatabaseName = principal.cCatalogoSQL
            oInfoDeConexion.UserID = principal.cUsuario
            oInfoDeConexion.Password = principal.cContrasena

            'obtenemos el objeto de texto correspondiente al titulo
            oTextoTitulo = rptReporte.ReportDefinition.ReportObjects.Item("txt_titulo")

            'obtenemos el objeto de texto correspondiente al filtro del reporte
            oTextoFiltro = rptReporte.ReportDefinition.ReportObjects.Item("txt_filtro")

            'establecemos el titulo del reporte
            oTextoTitulo.Text = "Reporte de Existencias Inventariadas"

            'si esta seleccionado el CheckBox de "Ajustado"
            If Me.chk_ajustado.Checked Then
                'si la opcion seleccionada es "SI"
                If Me.cmb_ajustado.Text.ToUpper.Trim.Equals("SI") Then
                    'anadimos al final del titulo la opcion seleccionada correspondiente
                    oTextoTitulo.Text = oTextoTitulo.Text & " - Sólo Ajustados"

                Else
                    'anadimos al final del titulo la opcion seleccionada correspondiente
                    oTextoTitulo.Text = oTextoTitulo.Text & " - Sólo NO Ajustados"

                End If
            End If

            'pasamos el filtro del reporte
            oTextoFiltro.Text = Me.cCondiciones

            'obtenemos la coleccion de tablas del informe
            oListaTablas = rptReporte.Database.Tables

            'recorremos cada una de las tablas del informe
            For Each oTabla In oListaTablas
                'obtenemos objetos con los datos de conexion
                oInfoConnDeTabla = oTabla.LogOnInfo

                'le asignamos el objeto con los datos de conexion que hemos creado
                oInfoConnDeTabla.ConnectionInfo = oInfoDeConexion

                'aplicamos los datos de conexion a la tabla
                oTabla.ApplyLogOnInfo(oInfoConnDeTabla)

            Next

            'aplicamos el filtro al reporte
            rptReporte.RecordSelectionFormula = Me.cCondiciones

            'lo asignamos como origen de datos del visualizador
            frm_despliega_reportes.crv_visualizador.ReportSource = rptReporte

        Catch Ex As Exception
            'en caso de error, evento a LOG
            'principal.pInfo_a_Log("Error preparando el Reporte para su Despliegue en Pantalla")
            'principal.pInfo_a_Log("Detalles del Error : " & Ex.Message)

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ESTADO DEL CHECKBOX DE "Cant Teorica"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_cantidad_teorica_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_cantidad_teorica.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_cantidad_teorica.Checked Then
            'habilitamos el combo de operadores
            Me.cmb_cantidad_teorica.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_cantidad_teorica.Text = Me.cmb_cantidad_teorica.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_cantidad_teorica.Enabled = False
            Me.txt_min_cantidad_teorica.Enabled = False
            Me.txt_max_cantidad_teorica.Enabled = False

        End If
    End Sub

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ESTADO DEL CHECKBOX DE "Ajustado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_ajustado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ajustado.CheckedChanged
        'si su nuevo estado es seleccionado
        If Me.chk_ajustado.Checked Then
            'habilitamos el combo de valores
            Me.cmb_ajustado.Enabled = True

            'y establecemos el valor del mismo al primer elemento
            Me.cmb_ajustado.Text = Me.cmb_ajustado.Items(0)

        Else
            'sino, lo deshabilitamos
            Me.cmb_ajustado.Enabled = False

        End If

    End Sub

End Class
