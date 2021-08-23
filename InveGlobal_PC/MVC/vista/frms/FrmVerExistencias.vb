Imports System.IO

Public Class FrmVerExistencias

#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmVerExistencias"

    Private __oApControlador As ApControlador

    Private __dtTablaAuxiliar As DataTable
    Private __lSectores As List(Of SectorOTD)
    Private __lExistencias As List(Of ExistenciaVwOTD) 'para EME
    Private __lExistencias_sap As List(Of ExistenciaVwOTD_SAP) 'para SAP


    Private __dtDetalles As DataTable

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


#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef oApControlador As ApControlador)

        'tomamos la instancia del controlador
        __oApControlador = oApControlador

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'inicializamos el resto de los componentes
        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion del resto de los componentes de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        __dtTablaAuxiliar = New DataTable()

        'cargamos las grillas con sus datos
        __cargar_grillas()


    End Sub



#End Region


#Region "METODOS"

    'cargar grillas de acuerdo al sistema (bifurcacion SAP y EME)
    Private Sub __cargar_grillas()

        If __oApControlador.Local_OTD.Sistema_OTD.nId = 18 Then
            __cargar_grillas_sap()
        Else
            __cargar_grillas_eme()
        End If



    End Sub


    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE DATOS A GRILLAS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_grillas_eme()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__cargar_grillas_eme()"

        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            'ensablamos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(" WHERE {0}.{1} = {2}" _
                                                       , ExistenciasVwTBL.NOMBRE_TABLA _
                                                       , ExistenciasVwTBL.ID_INVENTARIO.cNombre _
                                                       , __oApControlador.Inventario_OTD.nId
                                                        )

            'llamamos al metodo de recuperacion de registros de la vista de existencias
            lResultado = __oApControlador.oApModelo.ExistenciasVwADM().lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la lista y la tabla devuelta
                __lExistencias = CType(lResultado(1), List(Of ExistenciaVwOTD)) 'cambio 1
                __oApControlador.dtTablaAuxiliar = CType(lResultado(2), DataTable)

                'asignamos el origen de datos de la grilla
                Me.grd_existencias.DataSource = __oApControlador.dtTablaAuxiliar

                'mostramos la cantidad de registros
                Me.lbl_estado_2.Text = Me.grd_existencias.RowCount.ToString() & " Registros"

                'llamamos a funcion de carga de combo de campos
                __cargar_combo_campos()
                __resaltar_diferencias()

                'llamamos a procedimiento de carga de Nodos del Arbol de Sectores
                __crear_arbol_sectores()

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), NOMBRE_METODO)

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Resumen de Existencias")

        End Try

        'refrescamos el form
        Refresh()

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub


    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE DATOS A GRILLAS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_grillas_sap()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__cargar_grillas_sap()"

        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            'ensablamos la sentencia de filtrado de registros
            Dim cFiltroWhere As String = String.Format(" WHERE {0}.{1} = {2}" _
                                                       , ExistenciasVwTBL_SAP.NOMBRE_TABLA _
                                                       , ExistenciasVwTBL_SAP.ID_INVENTARIO.cNombre _
                                                       , __oApControlador.Inventario_OTD.nId
                                                        )

            'llamamos al metodo de recuperacion de registros de la vista de existencias
            lResultado = __oApControlador.oApModelo.ExistenciasVwADM_SAP().lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la lista y la tabla devuelta
                __lExistencias_sap = CType(lResultado(1), List(Of ExistenciaVwOTD_SAP)) 'cambio 1
                __oApControlador.dtTablaAuxiliar = CType(lResultado(2), DataTable)

                'asignamos el origen de datos de la grilla
                Me.grd_existencias.DataSource = __oApControlador.dtTablaAuxiliar

                'mostramos la cantidad de registros
                Me.lbl_estado_2.Text = Me.grd_existencias.RowCount.ToString() & " Registros"

                'llamamos a funcion de carga de combo de campos
                __cargar_combo_campos()
                __resaltar_diferencias()

                'llamamos a procedimiento de carga de Nodos del Arbol de Sectores
                __crear_arbol_sectores()

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), NOMBRE_METODO)

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Resumen de Existencias")

        End Try

        'refrescamos el form
        Refresh()

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub


    ''' <summary>
    ''' CARGA LOS ITEMS DEL COMBO DE CAMPOS DE FILTRADO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_combo_campos()

        'limpiamos los items anteriores, por si acaso
        Me.cmb_campos.Items.Clear()

        'recorremos cada campo de la lista
        For Each cl As DataColumn In __oApControlador.dtTablaAuxiliar.Columns
            'lo anadimos como item del combo de "Filtrar por:"
            Me.cmb_campos.Items.Add(cl.ColumnName)

        Next

        'tambien cargamos el combo de comparadores
        Me.cmb_comparaciones.DataSource = ApControlador.aComparadores

        'mostramos el primer elemento de cada combo
        Me.cmb_campos.SelectedIndex = 0
        Me.cmb_comparaciones.SelectedIndex = 0



    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE CARGA DE ARBOL DE SECTORES POSIBLES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __crear_arbol_sectores()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__crear_arbol_sectores()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'variables a utilizar
        Dim nodoNivel1, nodoNivel2, nodoNivel3, nodoNivel4 As New TreeNode()

        Try
            'si el arbol NO tiene cargados los sectores
            If Me.tvw_arbol.Nodes.Count.Equals(0) Then

                'sentencia de ordenamiento de registros
                Dim cOrdenamiento As String = String.Format(" ORDER BY {0}, {1}", SectoresTBL.ID.cNombre, SectoresTBL.NIVEL.cNombre)

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
                                tvw_arbol.Nodes.Add(nuevoNodo)
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
                Else
                    'sino, mensaje de notificacion
                    __oApControlador.notificar_error(lResultado(1).ToString(), NOMBRE_METODO)
                End If

            End If

        Catch ex As Exception
            'en caso de error, notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Creando árbol sectores")

        Finally
            'tamano del arbol de sectores a 0:anchoGrilla
            Me.tvw_arbol.Size = New Size(0, Me.tvw_arbol.Size.Height)

        End Try

    End Sub

    ''' <summary>
    ''' EVALUA Y EJECUTA LA BUSQUEDA DEL ARTICULO CORRESPONDIENTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __filtrar_registros()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__filtrar_registros()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'establecemos el cursor del mouse a "Esperar..."
        Cursor.Current = Cursors.WaitCursor

        Try
            'resetamos el origen de datos de la grilla
            Me.grd_existencias.DataSource = Nothing

            'si el cuadro esta vacio y no hay ningun radio button seleccionado
            If Me.txt_valor_buscado.Text.Length.Equals(0) _
                And Not Me.opt_falso.Checked And Not Me.opt_verdadero.Checked _
                And Me.txt_desde.Text.Trim().Length.Equals(0) Then
                'asignamos como origen de datos la tabla original
                Me.grd_existencias.DataSource = __oApControlador.dtTablaAuxiliar

            Else
                'sino, clonamos la tabla original
                __dtTablaAuxiliar = __oApControlador.dtTablaAuxiliar.Clone()

                'recorremos las filas devueltas
                For Each dr As DataRow In __oApControlador.dtTablaAuxiliar.Select(__cGet_condicion_filtrado())
                    'pasamos la fila a la tabla auxiliar local
                    __dtTablaAuxiliar.ImportRow(dr)
                Next

                'asignamos el origen de datos de la grila
                Me.grd_existencias.DataSource = __dtTablaAuxiliar

            End If

        Catch ex As Exception
            'en caso de error, notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Filtrado de Registros")

        End Try

        'mostramos la cantidad de registros
        Me.lbl_estado_2.Text = Me.grd_existencias.RowCount.ToString() & " Registros"

        'refrescamos el formulario
        Refresh()

        'establecemos el cursor del mouse a "Normal"
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' GENERA LA CONDICION DE FILTRADO DE ACUERDO A LOS CRITERIOS SELECCIONADOS
    ''' </summary>
    ''' <returns>Devuelve la Cadena con los Criterios de Seleccion</returns>
    ''' <remarks></remarks>
    Private Function __cGet_condicion_filtrado() As String

        'variables auxiliares
        Dim cCondicionBusqueda As String = String.Empty
        Dim cValor0 As String = String.Empty
        Dim cValor1 As String = String.Empty

        'evaluamos el tipo de dato del campo seleccionado
        Select Case CType(__oApControlador.dtTablaAuxiliar.Columns(Me.cmb_campos.SelectedItem), DataColumn).DataType.Name
            'si es numerico
            Case "Double", "Decimal", "Integer", "Int16", "Int32", "Int64"
                cValor0 = Me.txt_valor_buscado.Text.Trim.Replace("'", "").Replace(",", ".")

                'evaluamos el campo seleccionado
                Select Case Me.cmb_comparaciones.Text.Trim
                    'establecemos la condicion correspondiente al tipo de comparacion
                    Case ApControlador.aComparadores(ApControlador.OPERADORES.IGUAL)
                        cCondicionBusqueda = Me.cmb_campos.Text & " = " & cValor0
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.NO_IGUAL)
                        cCondicionBusqueda = Me.cmb_campos.Text & " <> " & cValor0
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.MAYOR_QUE)
                        cCondicionBusqueda = Me.cmb_campos.Text & " > " & cValor0
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.MENOR_QUE)
                        cCondicionBusqueda = Me.cmb_campos.Text & " < " & cValor0
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.ENTRE)
                        cValor0 = Me.txt_desde.Text.Trim.Replace("'", "").Replace(",", ".")
                        cValor1 = Me.txt_hasta.Text.Trim.Replace("'", "").Replace(",", ".")
                        cCondicionBusqueda = String.Format("{0} >= {1} AND {0} <= {2} ", Me.cmb_campos.Text, cValor0, cValor1)

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.NO_ENTRE)
                        cValor0 = Me.txt_desde.Text.Trim.Replace("'", "").Replace(",", ".")
                        cValor1 = Me.txt_hasta.Text.Trim.Replace("'", "").Replace(",", ".")
                        cCondicionBusqueda = String.Format("{0} < {1} AND {0} > {2} ", Me.cmb_campos.Text, cValor0, cValor1)

                End Select

            Case "String"
                cValor0 = Me.txt_valor_buscado.Text.Trim.Replace("'", "")

                'evaluamos el campo seleccionado
                Select Case Me.cmb_comparaciones.Text.Trim
                    'establecemos la condicion correspondiente al tipo de comparacion
                    Case ApControlador.aComparadores(ApControlador.OPERADORES.IGUAL)
                        cCondicionBusqueda = Me.cmb_campos.Text & " LIKE '" & cValor0 & "%'"
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.NO_IGUAL)
                        cCondicionBusqueda = Me.cmb_campos.Text & " NOT LIKE '" & cValor0 & "%'"
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.MAYOR_QUE)
                        cCondicionBusqueda = Me.cmb_campos.Text & " > '" & cValor0 & "'"
                        Me.txt_desde.Text = String.Empty

                    Case ApControlador.aComparadores(ApControlador.OPERADORES.MENOR_QUE)
                        cCondicionBusqueda = Me.cmb_campos.Text & " < '" & cValor0 & "'"
                        Me.txt_desde.Text = String.Empty

                End Select

            Case "Boolean"
                Select Case Me.pnl_ajustar.Visible
                    'establecemos la condicion correspondiente al tipo de comparacion
                    Case True
                        cCondicionBusqueda = Me.cmb_campos.Text & " = " & IIf(Me.opt_verdadero.Checked, "'True'", "'False'")
                        Me.txt_desde.Text = String.Empty

                End Select

        End Select

        'devolvemos la condicion de buqueda
        Return cCondicionBusqueda

    End Function

    ''' <summary>
    ''' PROCEDIMIENTO DE APLICACION DE CONTEO
    ''' </summary>
    ''' <param name="intNumeroConteo">numero de Conteo a Aplicar[1|2|3]</param>
    ''' <remarks></remarks>
    Private Sub __aplicar_conteos(ByVal intNumeroConteo As Integer)
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
            ''cSentenciaSQL = "EXECUTE [SP_APLICAR_CONTEO] @id_inventario= " & cID_Inventario & ", @nro_conteo= " & intNumeroConteo.ToString() & ";"

            bConteoAplicado = False

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
    ''' DEVUELVE EL CODIGO DE SECTOR DE ACUERDO AL NODO SELECCIONADO
    ''' </summary>
    ''' <returns>Devuelve el Codigo del Sector Seleccionado</returns>
    ''' <remarks></remarks>
    Private Function __cGet_id_sector() As String

        'variables a utilizar
        Dim sCodigo As String = CType(Me.tvw_arbol.SelectedNode.Tag, SectorOTD).cIdSector

        'evaluamos el caso del nivel sel sector seleccionado
        Select Case CType(Me.tvw_arbol.SelectedNode.Tag, SectorOTD).nNivel
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
    Private Sub __desplegar_arbol_sectores()

        If Not Me.CanFocus Then Exit Sub

        'establecemos la accion a ejecutar
        Me.bReplegarArbol = False

        'establecemos el tamano maximo del arbol
        Me.tvw_arbol.MaximumSize = New Size(210, Me.tvw_arbol.Size.Height)

        'habilitamos el Timer
        Me.tmr_arbol.Enabled = True

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE REPLIEGUE DE ARBOL DE SECTORES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __replegar_arbol_sectores()

        If Not Me.CanFocus Then Exit Sub

        'establecemos la accion a ejecutar
        Me.bReplegarArbol = True

        'habilitamos el Timer
        Me.tmr_arbol.Enabled = True

        'refrescamos el formulario
        Me.Refresh()

    End Sub

    ''' <summary>
    ''' BLOQUEA LOS CONTROLES DEL FORMULARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __bloquear_controles()
        'bloqueamos los controles del Formulario
        Me.MenuStrip1.Enabled = False
        Me.tab_padre.Enabled = False

        Me.lbl_porcentaje.Text = String.Empty

    End Sub

    ''' <summary>
    ''' DESBLOQUEA LOS CONTROLES DEL FORMULARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __desbloquear_controles()
        'bloqueamos los controles del Formulario
        Me.MenuStrip1.Enabled = True
        Me.tab_padre.Enabled = True

        Me.lbl_porcentaje.Text = String.Empty

    End Sub

    ''' <summary>
    ''' MARCA CON COLOR LAS CELDAS DE LA COLUMNA DEL CAMPO [DIFERENCIA_1_2] QUE SEAN DIFERENTES DE CERO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __resaltar_diferencias()

        Cursor.Current = Cursors.WaitCursor

        'si se encontro la columna buscada
        If Me.grd_existencias.Columns.Contains(VwDetallesConteosTBL.DIFERENCIA_1_2.cNombre) Then
            Dim nIdx As Integer = Me.grd_existencias.Columns(VwDetallesConteosTBL.DIFERENCIA_1_2.cNombre).Index

            'recorremos las filas de la grilla
            For Each dr As DataGridViewRow In Me.grd_existencias.Rows
                If Not Val(dr.Cells(nIdx).Value).Equals(0) Then
                    'cambiamos el color de fondo de la columna de la celda
                    dr.Cells(nIdx).Style.BackColor = Color.Red
                End If
            Next dr
        End If

        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Exporta a un archivo CSV los datos de Existencias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __exportar_datos_a_csv()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__exportar_datos_a_csv()"

        'variables auxiliares
        Dim cNombreArchivo As String = String.Empty
        Dim cDirectorio As String = String.Empty

        Cursor.Current = Cursors.WaitCursor

        Try
            'seteamos el cuadro de dialogo
            Me.SaveFileDialog1.Filter = "Archivos CSV(*.csv)|*.csv"
            Me.SaveFileDialog1.InitialDirectory = "C:\"

            'mostramos el cuadro de dialogo para guardar el archivo a generar
            Me.SaveFileDialog1.ShowDialog()

            'si se elijio nombre de archivo
            If Not Me.SaveFileDialog1.FileName.Equals(String.Empty) Then

                'instanciamos un escritor
                Dim oStream As New StreamWriter(Me.SaveFileDialog1.FileName)

                Try
                    'escribimos la cabecera del documento
                    oStream.WriteLine("DATOS DE INVENTARIO REALIZADO")
                    oStream.WriteLine("ID Local :" & __oApControlador.Inventario_OTD.Local_OTD.nIdEnSistema.ToString())
                    oStream.WriteLine("Nombre Local : " & __oApControlador.Inventario_OTD.Local_OTD.cDescripcion)
                    oStream.WriteLine("Sistema de Gestion : " & __oApControlador.Inventario_OTD.Local_OTD.Sistema_OTD.cDescripcion)
                    oStream.WriteLine("Fecha Inventario : " & String.Format("{0:dd/MM/yyyy}", __oApControlador.Inventario_OTD.Local_OTD.Sistema_OTD.cDescripcion))

                    oStream.WriteLine("Estado del Inventario : " & __oApControlador.Inventario_OTD.bCerrado.ToString())
                    oStream.WriteLine("Comentarios: " & __oApControlador.Inventario_OTD.cDescripcion)

                    'un espacio
                    oStream.WriteLine(String.Empty)

                    'obtenemos la lista de titulos en representacion CSV y lo derivamos al archivo
                    If __oApControlador.Local_OTD.Sistema_OTD.nId = 18 Then


                        oStream.WriteLine(__lExistencias_sap(0).cGet_titulos_csv(__oApControlador.cSeparadorListas))

                        'reseteamos la barra de progreso local
                        Me.pbr_estado.Maximum = __lExistencias_sap.Count
                        Me.pbr_estado.Value = 0

                        'recorremos los elementos de la lista de Existencias 
                        For Each oExistencia_sap As ExistenciaVwOTD_SAP In __lExistencias_sap
                            'obtenemos su representacion CSV y lo derivamos al archivo
                            oStream.WriteLine(oExistencia_sap.cGet_csv(__oApControlador.cSeparadorListas))

                            'incrementamos el valor de la barra de progreso
                            Me.pbr_estado.Value += 1
                            Application.DoEvents()

                        Next

                    Else
                        oStream.WriteLine(__lExistencias(0).cGet_titulos_csv(__oApControlador.cSeparadorListas))

                        'reseteamos la barra de progreso local
                        Me.pbr_estado.Maximum = __lExistencias.Count
                        Me.pbr_estado.Value = 0

                        'recorremos los elementos de la lista de Existencias 
                        For Each oExistencia As ExistenciaVwOTD In __lExistencias
                            'obtenemos su representacion CSV y lo derivamos al archivo
                            oStream.WriteLine(oExistencia.cGet_csv(__oApControlador.cSeparadorListas))

                            'incrementamos el valor de la barra de progreso
                            Me.pbr_estado.Value += 1
                            Application.DoEvents()

                        Next
                    End If

                    'mensaje de notificacion
                    __oApControlador.notificar_exito("Archivo generado correctamente!", "Exportar datos a CSV")

                Catch ex As Exception
                    'en caso de error, mensaje de notificacion
                    __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Exportar datos a CSV")

                Finally
                    'liberamos el escritor
                    If Not oStream Is Nothing Then oStream.Close()

                End Try
            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Exportar datos a CSV")

        End Try

        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' Exporta los datos de resumen del inventario al archivo IBM Basto para alimentar 
    ''' a la foto en el EmeRetail
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __exportar_ibm_basto()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__exportar_ibm_basto()"

        'variables auxiliares
        Dim cNombreArchivo As String = String.Empty
        Dim cDirectorio As String = String.Empty

        Cursor.Current = Cursors.WaitCursor

        Try
            'seteamos el cuadro de dialogo
            Me.SaveFileDialog1.Filter = "Archivos TXT(*.txt)|*.txt"
            Me.SaveFileDialog1.InitialDirectory = "C:\"
            Me.SaveFileDialog1.FileName = "Vasto"

            'mostramos el cuadro de dialogo para guardar el archivo a generar
            Me.SaveFileDialog1.ShowDialog()

            'si se elijio nombre de archivo
            If Not Me.SaveFileDialog1.FileName.Equals(String.Empty) Then

                'instanciamos un escritor
                Dim oStream As New StreamWriter(Me.SaveFileDialog1.FileName)

                '###############################################################################################################

                Try
                    'validar si es sistema sap o eme
                    If __oApControlador.Local_OTD.Sistema_OTD.nId = 18 Then
                        'reseteamos la barra de progreso local
                        Me.pbr_estado.Maximum = __lExistencias_sap.Count
                        Me.pbr_estado.Value = 0

                        'recorremos los elementos de la lista de Existencias
                        For Each oExistencia_sap As ExistenciaVwOTD_SAP In __lExistencias_sap 'cambio 3

                            'obtenemos su representacion CSV y lo derivamos al archivo 
                            oStream.WriteLine(oExistencia_sap.cGet_string_ibm_vasto_sap())

                            'incrementamos el valor de la barra de progreso
                            Me.pbr_estado.Value += 1
                            Application.DoEvents()

                        Next

                    Else
                        'reseteamos la barra de progreso local
                        Me.pbr_estado.Maximum = __lExistencias.Count
                        Me.pbr_estado.Value = 0
                        'recorremos los elementos de la lista de Existencias
                        For Each oExistencia As ExistenciaVwOTD In __lExistencias 'cambio 4

                            'obtenemos su representacion CSV y lo derivamos al archivo 
                            oStream.WriteLine(oExistencia.cGet_string_ibm_vasto())

                            'incrementamos el valor de la barra de progreso
                            Me.pbr_estado.Value += 1
                            Application.DoEvents()

                        Next

                    End If

                    '###############################################################################################################
                    'mensaje de notificacion
                    __oApControlador.notificar_exito("Archivo generado correctamente!", "Exportar datos a IBM Basto")

                Catch ex As Exception
                    'en caso de error, mensaje de notificacion
                    __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Exportar datos a IBM Basto")

                Finally
                    'liberamos el escritor
                    If Not oStream Is Nothing Then oStream.Close()

                End Try
            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Exportar datos a IBM Basto")

        End Try

        Cursor.Current = Cursors.Default

    End Sub



#End Region


#Region "EVENTOS"

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
        'MS_EXCEL.proCerrarExcel()

        Me.Dispose()

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
        If Me.cmb_campos.SelectedItem = ExistenciasVwTBL.NOMBRE_SECTOR.cNombre Then
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
            __desplegar_arbol_sectores()

            'enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el campo es uno de los BOOLEANOs
            If Me.cmb_campos.Text = ExistenciasVwTBL.AJUSTAR.cNombre _
                Or Me.cmb_campos.Text = ExistenciasVwTBL.AJUSTADO.cNombre _
                Or Me.cmb_campos.Text = ExistenciasVwTBL.PESABLE.cNombre Then

                'mostramos el panel con las opciones del campo
                Me.pnl_ajustar.Visible = True

                'volvemos invisible el combo de operadores y los demas controles de busqueda
                Me.cmb_comparaciones.Visible = False
                Me.txt_desde.Visible = False
                Me.txt_hasta.Visible = False
                Me.lbl_Y.Visible = False
                Me.txt_valor_buscado.Visible = False
                Me.cmd_filtrar.Visible = False

            Else
                'sino, ocultamos el panel de opciones de campo BOOLEANO y deseleccionamos los radio buttons
                Me.pnl_ajustar.Visible = False
                Me.opt_verdadero.Checked = False
                Me.opt_falso.Checked = False

                'volvemos visible el combo de operadores
                Me.cmb_comparaciones.Visible = True

            End If

            'si el arbol esta desplegado, llamamos a procedimiento de repliegue del arbol de sectores
            If Me.tvw_arbol.Width > 20 Then __replegar_arbol_sectores()

            'le pasamos el enfoque al combo de comparadores
            Me.cmb_comparaciones.Focus()

        End If



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
            Case ApControlador.aComparadores(ApControlador.OPERADORES.IGUAL) _
                , ApControlador.aComparadores(ApControlador.OPERADORES.NO_IGUAL) _
                , ApControlador.aComparadores(ApControlador.OPERADORES.MAYOR_QUE) _
                , ApControlador.aComparadores(ApControlador.OPERADORES.MENOR_QUE)
                'mostramos el cuadro de texto de valor buscado
                Me.txt_valor_buscado.Visible = True

                'le pasamos el enfoque
                Me.txt_valor_buscado.Focus()

            Case ApControlador.aComparadores(ApControlador.OPERADORES.ENTRE) _
                , ApControlador.aComparadores(ApControlador.OPERADORES.NO_ENTRE) _
                'mostramos los cuadros de texto DESDE y HASTA y la etiqueta "Y:", tambien el boton de busqueda
                Me.txt_desde.Visible = True
                Me.lbl_Y.Visible = True
                Me.txt_hasta.Visible = True
                Me.cmd_filtrar.Visible = True

                'pasamos el enfoque al cuadro DESDE
                Me.txt_desde.Focus()

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
            'si la tecla es [Enter], llamamos al procedimiento de busqueda del registro
            If e.KeyCode = Keys.Enter Then __filtrar_registros()
        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO DE "Valor Buscado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_valor_buscado_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'si el campo seleccionado es "[ID_SECTOR]" y el cuadro de texto esta vacio
        If Me.cmb_campos.Text = ExistenciasVwTBL.ID_SECTOR.cNombre And Me.txt_valor_buscado.Text.Equals(String.Empty) Then
            'llamamos a procedimiento de despliegue del arbol de sectores
            __desplegar_arbol_sectores()

            'establecemos este cuadro como control de origen de llamada al arbol
            Me.cControlOrigen = Me.txt_valor_buscado.Name.ToString()

            'pasamos el enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el arbol esta desplegado, lo replegamos
            If Me.tvw_arbol.Size.Width > 20 Then __replegar_arbol_sectores()
        End If

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
            'si el arbol de sectores esta desplegado, lo replegamos
            If Me.tvw_arbol.Size.Width > 20 Then __replegar_arbol_sectores()

            'llamamos al metodo de filtrado de registros
            __filtrar_registros()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE DOBLE CLICK SOBRE EL CONTENIDO DE UNA CELDA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_existencias_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_existencias.CellDoubleClick

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".Sub grd_existencias_CellContentDoubleClick()"

        'variables a utilizar 
        Dim intNumeroFila As Integer = -1
        Dim strScanning As String = String.Empty

        'si la grilla esta vacia, salimos del sub
        If Me.grd_existencias.RowCount < 1 Then Exit Sub

        Try
            'obtenemos el numero de fila
            intNumeroFila = Me.grd_existencias.SelectedCells(0).RowIndex()

            'obtenemos el scanning del articulo
            strScanning = Me.grd_existencias.Rows(intNumeroFila).Cells(ExistenciasVwTBL.SCANNING.nIndice).Value.ToString()

            'ensamblamos la Consulta SQL
            cSentenciaSQL = "EXEC [SP_OBTENER_DETALLES_ARTICULOS_CONTEOS] @id_inventario = {0}, @scanning = N'{1}' "
            cSentenciaSQL = String.Format(cSentenciaSQL, __oApControlador.Inventario_OTD.nId, strScanning)


            'reseteamos el origen de datos de la grilla de detalles
            Me.grd_detalles.DataSource = Nothing

            'llamamos a funcion de Ejecucion de Consulta SQL y lo asignamos como Origen de Datos de la Grilla de Detalles
            'Me.grd_detalles.DataSource = principal.dtEjecutar_ConsultaSQL(cSentenciaSQL)

            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.Get_conector().lEjecutar_consulta(cSentenciaSQL)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                __dtDetalles = CType(lResultado(1), DataTable)

                With Me.grd_detalles
                    'la asignamos como origen de datos de la grilla de detalles
                    .DataSource = __dtDetalles

                    'ocultamos las columnas de IDs
                    If .Columns.Contains("ID_LOCACION") Then .Columns("ID_LOCACION").Visible = False
                    If .Columns.Contains("ID_SOPORTE") Then .Columns("ID_SOPORTE").Visible = False
                    If .Columns.Contains("ID_LETRA_SOPORTE") Then .Columns("ID_LETRA_SOPORTE").Visible = False
                    If .Columns.Contains("CARA") Then .Columns("CARA").Visible = False

                End With

                'hacemos visible la pestana de Detalles
                Me.tab_detalles.Show()

                'pasamos a la pestana en cuestion
                Me.tab_padre.SelectedIndex = 1

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_stop(lResultado(1).ToString(), "Detalles de Conteos")

            End If


        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_stop(NOMBRE_METODO & " Error: " & ex.Message, "Detalles de Conteos")

        End Try

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON CON LOGO EXCEL
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_excel.Click
        'llamamos a procedimiento de despliegue exportacion a excel
        __exportar_datos_a_csv()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL BOTON [Aplicar Conteo]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aplicar_conteo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'si el inventario ya esta cerrado
        If bInventario_Cerrado Then
            'mensaje de notificacion
            MessageBox.Show("El Inventario ya esta Cerrado!", "Estado del Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'salimos del sub
            Exit Sub

        End If

        'ejecutamos el procedimiento de calculo de cantidades a ajustar
        'Call pCalcular_Pre_Ajustes()

    End Sub

    ''' <summary>
    ''' DESPUES DE QUE SE SELECCIONO UN NODO DEL ARBOL DE SECTORES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tvw_arbol_sectores_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvw_arbol.AfterSelect

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".tvw_arbol_sectores_AfterSelect()"

        Cursor.Current = Cursors.WaitCursor

        Try
            
            'formamos la sentencia de filtrado
            Dim cFiltro As String = String.Format("{0} LIKE '{1}%'", ExistenciasVwTBL.ID_SECTOR.cNombre, __cGet_id_sector())

            'reseteamos el origen de datos de la grilla
            Me.grd_existencias.DataSource = Nothing

            'clonamos la tabla original
            __dtTablaAuxiliar = __oApControlador.dtTablaAuxiliar.Clone

            'recorremos las filas devueltas
            For Each dr As DataRow In __oApControlador.dtTablaAuxiliar.Select(cFiltro)
                'la pasamos la tabla auxiliar local
                __dtTablaAuxiliar.ImportRow(dr)
            Next

            'establecemos el origen de datos
            Me.grd_existencias.DataSource = __dtTablaAuxiliar

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Filtrado por Sectores")

        End Try

        'refrescamos el formulario
        Me.Refresh()

        Cursor.Current = Cursors.Default

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
            If Me.tvw_arbol.Width > Me.tvw_arbol.MinimumSize.Width Then
                'decrementamos su ancho, y su alto lo dejamos en el mismo de la grilla
                Me.tvw_arbol.Size = New Size(Me.tvw_arbol.Size.Width - 30, Me.tvw_arbol.Size.Height)
            Else
                'sino, deshabilitamos el Timer
                Me.tmr_arbol.Enabled = False

            End If

        Else
            'sino, lo desplegamos mientras su ancho es menor o igual a 210
            If Me.tvw_arbol.Width < Me.tvw_arbol.MaximumSize.Width Then
                'incrementamos su ancho, y su alto lo dejamos en el mismo de la grilla
                Me.tvw_arbol.Size = New Size(Me.tvw_arbol.Size.Width + 30, Me.tvw_arbol.Size.Height)

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
    Private Sub grd_detalles_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd_detalles.CellDoubleClick
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
        With __oApControlador.Get_mod_conteo_frm()
            .lbl_scanning.Text = Me.grd_detalles.Rows(nNroFila).Cells("SCANNING").Value.ToString()
            .lbl_articulo.Text = Me.grd_detalles.Rows(nNroFila).Cells("ARTICULO").Value.ToString()
            .lbl_locacion.Text = Me.grd_detalles.Rows(nNroFila).Cells("LOCACION").Value.ToString()
            .lbl_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("SOPORTE").Value.ToString()
            .lbl_nro_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("NRO_SOPORTE").Value.ToString()
            .lbl_letra_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("CARA").Value.ToString()
            .lbl_id_locacion.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_LOCACION").Value.ToString()
            .lbl_id_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_SOPORTE").Value.ToString()
            .lbl_id_letra_soporte.Text = Me.grd_detalles.Rows(nNroFila).Cells("ID_LETRA_SOPORTE").Value.ToString()
            .lbl_nivel.Text = Me.grd_detalles.Rows(nNroFila).Cells("NIVEL").Value.ToString()
            .lbl_metro.Text = Me.grd_detalles.Rows(nNroFila).Cells("METRO").Value.ToString()
            .lbl_conteo.Text = Me.grd_detalles.Rows(nNroFila).Cells("NRO_CONTEO").Value.ToString()
            .txt_conteo_1.Text = Me.grd_detalles.Rows(nNroFila).Cells("CANTIDAD").Value

            'desplegamos el formulario en modo dialogo
            .ShowDialog()

            'si el resultado del dialogo es "OK"
            If .DialogResult = Windows.Forms.DialogResult.OK Then
                'actualizamos los valores en las celdas de cantidades de la fila modificada
                Me.grd_detalles.Rows(nNroFila).Cells("CANTIDAD").Value = .txt_conteo_1.Text

                __cargar_grillas()

            End If

        End With
        

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO "Hasta" DE LOS VALORES BUSCADOS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_hasta_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'si el campo seleccionado es "[ID_SECTOR]"
        If Me.cmb_campos.Text = ExistenciasVwTBL.ID_SECTOR.cNombre Then
            'si el arbol esta Replegado, llamamos a procedimiento de despliegue del arbol de sectores
            If Me.tvw_arbol.Size.Width < 20 Then __desplegar_arbol_sectores()

            'establecemos este cuadro como control de origen de llamada al arbol
            Me.cControlOrigen = Me.txt_hasta.Name.ToString()

            'pasamos el enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el arbol esta desplegado, lo replegamos
            If Me.tvw_arbol.Size.Width > 20 Then __replegar_arbol_sectores()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL CUADRO "Desde" DE LOS VALORES BUSCADOS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_desde_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'si el campo seleccionado es "[ID_SECTOR]"
        If Me.cmb_campos.SelectedItem = ExistenciasVwTBL.ID_SECTOR.cNombre Then
            'si el arbol esta Replegado, llamamos a procedimiento de despliegue del arbol de sectores
            If Me.tvw_arbol.Size.Width < 20 Then __desplegar_arbol_sectores()

            'establecemos este cuadro como control de origen de llamada al arbol
            Me.cControlOrigen = Me.txt_desde.Name.ToString()

            'pasamos el enfoque al arbol de sectores
            Me.tvw_arbol.Focus()

        Else
            'sino, si el arbol esta desplegado, lo replegamos
            If Me.tvw_arbol.Size.Width > 20 Then __replegar_arbol_sectores()

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
        __exportar_datos_a_csv()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU "Aplicar Conteo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_aplicar_conteo_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    ''' CUANDO SE HACE CLICK EN EL MENU "Desplegar Reporte"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_desplegar_reporte_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_desplegar_reporte_2.Click
        'desplegamos el formulario de seleccion de criterios del reporte de existencias en modo dialogo
        'frm_reporte_existencias.ShowDialog()

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
    ''' CUANDO SE HACE CLICK SOBRE EL RADIO BUTTON "Verdadero"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub opt_verdadero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opt_verdadero.Click
        'si esta seleccionado este control
        If Me.opt_verdadero.Checked Then
            'llamamos al procedimiento de busqueda del registro
            __filtrar_registros()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK SOBRE EL RADIO BUTTON "Falso"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub opt_falso_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opt_falso.Click
        'si esta seleccionado este control
        If Me.opt_falso.Checked Then
            'llamamos al procedimiento de busqueda del registro
            __filtrar_registros()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN LAS CABECERAS DE LAS COLUMNAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks> 
    Private Sub grd_existencias_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd_existencias.ColumnHeaderMouseClick
        'llamamos a procedimiento de coloreado de campo de [DIFERENCIA_1_2]
        __resaltar_diferencias()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN EL MENU DE "Reporte Diferencias"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PDiferenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'mostramos el formulario de seleccion de criterios del reporte de diferencias
        'frm_rpt_diferencias.ShowDialog()

    End Sub

    ''' <summary>
    ''' Cuando se hace lick en el menu "Exportar IBM Basto"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mnu_ibm_basto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_ibm_basto.Click

        'llamamos al metodo de generacion del archivo
        __exportar_ibm_basto()

    End Sub

#End Region


    
    
End Class