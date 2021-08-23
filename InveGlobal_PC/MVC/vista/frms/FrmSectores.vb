Public Class FrmSectores

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmSectores"

    Private __oApControlador As ApControlador

    Private __lSectoresSelec As List(Of SectorOTD)
    Private __lSectores As List(Of SectorOTD)
    Private __oSectorAux As SectorOTD



#End Region



#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Instancia del controlador de la Aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControlador As ApControlador)

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'tomamos la instancia del controlador
        __oApControlador = oApControlador

        'llamamos al incializador de componentes internos
        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion del resto de los componentes internos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        'inicializamos los atributos
        __oSectorAux = New SectorOTD()

        'creamos el arbol de sectores
        Dim lResultado As List(Of Object) = __lCrear_arbol_sectores()

        'si la lista de sectores a cubrir ya esta cargada, llamamos a procedimiento de marcado de nodos del arbol de sectores
        If __oApControlador.Get_datosInventario_frm().lSectores.Count > 0 Then marcar_sectores()


    End Sub



#End Region



#Region "METODOS"

    ''' <summary>
    ''' Ejecuta la creacion del arbolde sectores de productos
    ''' </summary>
    ''' <returns>Lista de Resultados [int, object]</returns>
    ''' <remarks></remarks>
    Private Function __lCrear_arbol_sectores() As List(Of Object)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__lCrear_arbol_sectores()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'variables a utilizar
        Dim nodoNivel1, nodoNivel2, nodoNivel3, nodoNivel4 As New TreeNode()

        Try
            'si el arbol NO tiene cargados los sectores
            If Me.tvw_arbol_sectores.Nodes.Count.Equals(0) Then

                'sentencia de ordenamiento de registros
                Dim cOrdenamiento As String = String.Format("ORDER BY {0}, {1}", SectoresTBL.ID.cNombre, SectoresTBL.NIVEL.cNombre)

                'llamamos al metodo de recuperacion de sectores
                lResultado = __oApControlador.oApModelo.Sectores_ADM.lGet_elementos(cOrdenamiento)

                'si se ejecuto sin problemas
                If lResultado(0).Equals(1) Then
                    'tomamos la lista devuelta
                    __lSectores = CType(lResultado(1), List(Of SectorOTD))

                    'recorremos los elementos de la fila
                    For Each oSector As SectorOTD In __lSectores
                        'creamos un nuevo nodo para el arbol
                        Dim nuevoNodo As New TreeNode(oSector.cDescripcion)
                        nuevoNodo.Tag = oSector

                        'evaluamos el nivel del sector
                        Select Case oSector.nNivel
                            Case 1
                                tvw_arbol_sectores.Nodes.Add(nuevoNodo)
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
                                'en otro caso, evento a log
                                __oApControlador.log().Escribir("Nodo de Arbol de Sectores sin Nivel : " & oSector.ToString())

                        End Select

                    Next
                End If
            End If

            'establecemos el resultado del metodo
            lResultado = New List(Of Object)(New Object() {1, "Ok"})

        Catch ex As Exception
            'en caso de error
            lResultado = New List(Of Object)(New Object() {-1, NOMBRE_METODO & " Error: " & ex.Message})

        End Try

        'devolvemos el resultado del metodo
        Return lResultado


    End Function

    ''' <summary>
    ''' Actualiza la vista de la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub post_sector_seleccionado()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".post_sector_seleccionado()"

        Cursor.Current = Cursors.WaitCursor

        Try
            'variables a utilizar
            Dim oSector As SectorOTD = CType(tvw_arbol_sectores.SelectedNode.Tag, SectorOTD)
            Dim cCodigo As String = String.Format(" WHERE {0}.{1} LIKE '{2}%'" _
                                                  , SectoresTBL.NOMBRE_TABLA, SectoresTBL.ID.cNombre, "{0}")

            'evaluamos el caso del nivel sel sector seleccionado
            Select Case oSector.nNivel
                Case 1
                    'si el nivel es 1 extraemos los digitos correspondientes
                    cCodigo = String.Format(cCodigo, oSector.cIdSector.ToString().Substring(0, 2))

                Case 2
                    'si el nivel es 2 extraemos los digitos correspondientes
                    cCodigo = String.Format(cCodigo, oSector.cIdSector.ToString().Substring(0, 4))

                Case 3
                    'si el nivel es 3 extraemos los digitos correspondientes
                    cCodigo = String.Format(cCodigo, oSector.cIdSector.ToString().Substring(0, 6))

                Case 4
                    'si el nivel es 8 extraemos los digitos correspondientes
                    cCodigo = String.Format(cCodigo, oSector.cIdSector.ToString().Substring(0, 8))

                Case 5
                    'si el nivel es 5, lo pasamos como tal
                    cCodigo = String.Format(cCodigo, oSector.cIdSector.ToString())

                Case Else
                    Exit Sub

            End Select

            'eliminamos el notenido de la grilla
            grd_sectores.DataSource = Nothing

            'llamamos al metodo de recuperacion de sectores
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.InventariosSectores_ADM.lGet_elementos(cCodigo)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'recargamos la grilla de sectores seleccionados
                grd_sectores.DataSource = Nothing
                grd_sectores.DataSource = CType(lResultado(1), List(Of SectorOTD))
                grd_sectores.Columns(SectoresTBL.DESCRIPCION.nIndice).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), NOMBRE_METODO)

            End If

        Catch Ex As Exception
            'en caso de error, notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Mostrar Sectores")

        End Try

        'mostramos la cantidad de filas de la grilla
        lbl_estado.Text = grd_sectores.RowCount.ToString() & " Sectores incluidos.."

        'refrescamos el formulario
        Refresh()

        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Marca los nodos del arbol que aparecen en la lista de sectores a cubrir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub marcar_sectores()
        'variables a utilizar
        Dim ndNodo As TreeNode

        'pasamos por todos los nodos de Nivel 1 del arbol
        For Each ndNodo In tvw_arbol_sectores.Nodes
            'recorremos la lista de sectores
            'For Each oSector As Object In FrmDatosInventario.lstListaSectores
            'si el tag del nodo es igual al item actual de la lista
            'If ndNodo.Tag = oSector.ToString() Then
            'lo marcamos
            'ndNodo.Checked = True

            'Else
            'sino, recorremos los nodos hijos del nodo actual
            'pMarcar_Nodos_Hijos(ndNodo)

            'End If
            'Next oSector
        Next ndNodo

    End Sub

    ''' <summary>
    ''' Recarga de datos en grilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub recargar_sectores(ByVal cCodigo As String)

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".recargar_sectores()"

        Try
            cCodigo = String.Format(" WHERE {0}.{1} LIKE '{2}%'" _
                                                  , SectoresTBL.NOMBRE_TABLA, SectoresTBL.ID.cNombre, cCodigo)

            'limpiamos la grilla
            grd_sectores.DataSource = Nothing

            'llamamos al metodo de recuperacion de sectores
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.InventariosSectores_ADM.lGet_elementos(cCodigo)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'recargamos la grilla de sectores seleccionados
                grd_sectores.DataSource = SISTEMAS.dtbSeleccionar_Sectores(cCodigo)
                grd_sectores.Columns("NOMBRE_SECTOR").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), NOMBRE_METODO)

            End If

        Catch Ex As Exception
            'en caso de error, notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Mostrar Sectores")

        End Try

        'mostramos la cantidad de filas de la grilla
        lbl_estado.Text = grd_sectores.RowCount.ToString() & " Sectores incluidos.."

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Carga la lista de sectores a ser inventariados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cargar_lista_sectores()
        'reseteamos la lista de sectores cubiertos por el inventario temporal
        __oApControlador.NuevoInventario_OTD.lSectores = New List(Of SectorOTD)()

        'pasamos por todos los nodos de Nivel 1 del arbol
        For Each oNodo As TreeNode In tvw_arbol_sectores.Nodes
            'si el nodo esta seleccionado
            If oNodo.Checked Then
                'obtenemos el tag del nodo actual
                __oSectorAux = CType(oNodo.Tag, SectorOTD)

                'recorremos los nodos hijos del nodo actual
                agregar_nodos_hijos(oNodo)

                'agregamos sector a la lista de Sectores Seleccionados
                __oApControlador.NuevoInventario_OTD.lSectores.Add(__oSectorAux)

            End If
        Next

    End Sub

    ''' <summary>
    ''' Recorre los hijos del nodo actual para cargarlos a la lista de sectores seleccionados si corresponde
    ''' </summary>
    ''' <param name="oNodo">Instancia de Nodo del Arbol a Recorrer</param>
    ''' <remarks></remarks>
    Private Sub agregar_nodos_hijos(ByVal oNodo As TreeNode)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".agregar_nodos_hijos()"

        Try
            'recorremos los nodos hijos del nodo parametro
            For Each oNodoHijo As TreeNode In oNodo.Nodes
                'si el nodo esta seleccionado
                If oNodoHijo.Checked Then
                    'tomamos el valor para la lista
                    __oSectorAux = CType(oNodo.Tag, SectorOTD)

                    'recorremos los nodos hijos del nodo actual
                    agregar_nodos_hijos(oNodoHijo)

                    'si el elemento aun no esta en la lista, agregamos el valor a la lista de Sectores Seleccionados
                    If __lSectoresSelec.IndexOf(__oSectorAux) < 0 Then __lSectoresSelec.Add(__oSectorAux)

                End If
            Next

        Catch Ex As Exception
            'en caso de error, notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Seleccion de Sectores")

        End Try


    End Sub

    ''' <summary>
    ''' Marca los nodos padres del Nodo parametro a estado indicado
    ''' </summary>
    ''' <param name="oNodoActual">Nodo Actual cuyo Padre sera Marcado|Desmarcado</param>
    ''' <param name="bEstado">Marca de Estado a Aplicar</param>
    ''' <remarks></remarks>
    Private Sub marcar_nodos_padres(ByRef oNodoActual As TreeNode, ByVal bEstado As Boolean)

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".marcar_nodos_padres()"

        Try
            'si el nivel del nodo actual es mayor a uno, marcamos a su nodo padre
            If CType(oNodoActual.Tag, SectorOTD).nNivel > 1 Then oNodoActual.Parent.Checked = bEstado

        Catch Ex As Exception
            'en caso de error, notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Marcar Sectores")

        End Try


    End Sub

    ''' <summary>
    ''' MARCA LOS NODOS DE NIVELES HIJOS AL NODO "oNodoActual"
    ''' </summary>
    ''' <param name="oNodoActual">Nodo al que se cambiara el Estado</param>
    ''' <remarks></remarks>
    Private Sub marcar_nodos_hijos(ByRef oNodoActual As TreeNode)
        'variables a utlizar
        Dim oNodoHijo As TreeNode

        'recorremos los nodos hijos del nodo parametro
        For Each oNodoHijo In oNodoActual.Nodes
            'recorremos la lista de sectores
            'For Each oSector As Object In FrmDatosInventario.lstListaSectores
            'si el tag del nodo es igual al item actual de la lista, lo marcamos
            'If oNodoHijo.Tag = oSector.ToString() Then
            'oNodoHijo.Checked = True
            'Else
            'sino, recorremos los nodos hijos del nodo actual
            'pMarcar_Nodos_Hijos(oNodoHijo)

            'End If

            'Next oSector
        Next oNodoHijo

    End Sub

    ''' <summary>
    ''' DESMARCA LOS NODOS HIJOS DE UN NODO DESMARCADO
    ''' </summary>
    ''' <param name="oNodoActual">Nodo cuyos Hijos deben desmarcarse</param>
    ''' <remarks></remarks>
    Private Sub desmarcar_nodos_hijos(ByRef oNodoActual As TreeNode)
        'recorremos los nodos hijos del nodo actual
        For Each oNodoHijo As TreeNode In oNodoActual.Nodes
            'si el nodo hijo esta marcado, lo desmarcamos
            If oNodoHijo.Checked Then oNodoHijo.Checked = False
        Next

    End Sub



#End Region



#Region "EVENTOS"

    ''' <summary>
    ''' CUANDO SE CAMBIA EL ESTADO DEL CHECKBOX "Todos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chk_todos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_todos.CheckedChanged

        'cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".chk_todos_CheckedChanged()"

        'limpiamos la lista de sectores
        __lSectoresSelec = New List(Of SectorOTD)

        'si el valor actual es Checked
        If chk_todos.Checked Then
            Try
                'recorremos todos los nodos de nivel 1 del arbol y los vamos marcando
                For Each ndNodo As TreeNode In tvw_arbol_sectores.Nodes
                    'checkeamos el nodo
                    ndNodo.Checked = True

                    'anadimos el tag del nodo a la lista de sectores a inventariar
                    __lSectoresSelec.Add(CType(ndNodo.Tag, SectorOTD))

                Next

                'limpiamos la grilla
                grd_sectores.DataSource = Nothing

                'recargamos la grilla de sectores seleccionados
                grd_sectores.DataSource = __lSectores
                grd_sectores.Columns(SectoresTBL.DESCRIPCION.nIndice).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

                'mostramos la cantidad de filas de la grilla
                lbl_estado.Text = grd_sectores.RowCount.ToString() & " Sectores incluidos.."

            Catch ex As Exception
                'en caso de error, evento a LOG
                __oApControlador.log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)
            End Try

        Else
            'sino, recorremos todos los nodos de nivel 1 del arbol y los vamos desmarcando
            For Each ndNodo As TreeNode In tvw_arbol_sectores.Nodes
                'checkeamos el nodo
                ndNodo.Checked = False

            Next

        End If

        'refrescamos el formulario
        Refresh()

        'cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_aceptar.Click

        'si el check de "Todos" los sectores no esta marcado
        If Not chk_todos.Checked Then
            'llamamos a procedimiento de carga de lista de sectores
            cargar_lista_sectores()

        Else
            'si lo esta, reseteamos la lista de sectores y le agregamos un marcador por defecto para incluir todos los sectores
            __oApControlador.NuevoInventario_OTD.lSectores = New List(Of SectorOTD)()
            __oApControlador.NuevoInventario_OTD.lSectores.Add(New SectorOTD(SectorOTD.TODOS.ToString(), String.Empty, SectorOTD.TODOS, SectorOTD.TODOS.ToString()))

        End If

        'si la lista de sectores a inventariar esta vacia y el check de "Todos" los sectores no esta seleccionado
        If __oApControlador.NuevoInventario_OTD.lSectores.Count.Equals(0) And Not chk_todos.Checked Then
            __oApControlador.notificar_stop("Debe Seleccionar al menos un Sector", "Sectores a Inventariar")
            'salimos del sub
            Exit Sub

            'End If
        End If

        'si la respuesta es "SI", establecemos el Resultado del Dialogo a OK
        DialogResult = Windows.Forms.DialogResult.OK

        'cerramos este formulario
        Me.Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancelar.Click
        'establecemos el Resultado del Dialogo a "Cancel"
        DialogResult = Windows.Forms.DialogResult.Cancel

        'cerramos este formulario
        Close()

    End Sub

    ''' <summary>
    ''' CUANDO SE ESTA CERRANDO EL FORMULARIO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frm_sectores_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        'si el resultado del dialogo es "Ok"
        If Me.DialogResult.Equals(DialogResult.OK) Then
            'si la lista de sectores a inventariar esta vacia y el check de "Todos" los sectores no esta seleccionado
            If __lSectoresSelec.Count.Equals(0) And Not chk_todos.Checked Then
                __oApControlador.notificar_stop("Debe Seleccionar al menos un Sector", "Sectores a Inventariar")
                'cancelamos el cierre del formulario
                e.Cancel = True
            End If
        End If

    End Sub

    ''' <summary>
    ''' DESPUES DE HABER SELECCIONADO UN SECTOR
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_sectores_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvw_arbol_sectores.AfterSelect
        'llamamos a procedimiento de actualizacion de la grilla de sectores cubiertos
        post_sector_seleccionado()

    End Sub

    ''' <summary>
    ''' CUANDO SE HA ESTABLECIDO DEL VALOR CHECKED DE UN NODO A FALSO O VERDADERO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_sectores_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvw_arbol_sectores.AfterCheck
        'si el nodo esta checkeado
        If e.Node.Checked Then
            'marcamos el nodo padre
            marcar_nodos_padres(e.Node, True)

        Else
            'sino, los desmarcamos
            marcar_nodos_padres(e.Node, False)

            'llamamos a procedimiento de desmarcado de nodos hijos
            desmarcar_nodos_hijos(e.Node)

        End If

    End Sub



#End Region


End Class


    