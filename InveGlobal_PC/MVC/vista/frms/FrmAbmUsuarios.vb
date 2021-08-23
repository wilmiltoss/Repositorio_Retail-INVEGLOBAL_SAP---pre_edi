Imports CdgPersistencia.ClasesBases
Imports CdgUtiles.Util

Public Class FrmAbmUsuarios


#Region "ATRIBUTOS"

    Public Const NOMBRE_CLASE As String = "FrmAbmUsuarios"

    Private __oApControlador As ApControlador

    Private __lNiveles As List(Of NivelAccesoOTD)

    Private __oUsuarioDesplegando As UsuarioOTD
    Private __oUsuarioEditando As UsuarioOTD
    Private __lListaLocales As List(Of LocalOTD)

    Private Enum ESTADOS_ABM
        VISUALIZANDO = 0
        AGREGANDO = 1
        MODIFICANDO = 2

    End Enum
    Private __nEstadoABM As Int16



#End Region


#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="oApControlador">Instancia del controlador de la aplicacion</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal oApControlador As ApControlador)

        'tomamos la instancia del controlador
        __oApControlador = oApControlador

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        'inicializamos el resto de los componentes de la clase
        __Inicializar_componentes()

    End Sub

    ''' <summary>
    ''' Ejecuta la inicializacion del resto de los componentes de la clase 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __Inicializar_componentes()

        'evento a LOG
        __oApControlador.log().Escribir("Se carga el Formulario : " & Me.Name)

        'llamamos a procedimiento de carga de datos a grilla
        __cargar_grilla()

        'cargamos el combo de niveles
        __cargar_combo_niveles()

        'llamamos a procedimiento de carga de datos en controles de ABM
        __mostrar_primer_elemento()

        'bloqueamos los controles
        __deshabilitar_controles_upd()

    End Sub

#End Region


#Region "METODOS"


    ''' <summary>
    ''' CARGA LOS DATOS DE USUARIOS EN LA GRILLA DE VISUALIZACION
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_grilla()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__cargar_grilla()"

        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        'cursor del mouse a "Esperar"
        Cursor.Current = Cursors.WaitCursor

        Try
            'sentencia de filtrado
            Dim cFiltroWhere As String = String.Format("WHERE {0}.{1} = 'True'" _
                                                       , UsuariosTBL.NOMBRE_TABLA _
                                                       , UsuariosTBL.MOSTRAR.cNombre _
                                                       )

            'llamamos al metodo de recuperacion de datos de usuarios
            lResultado = __oApControlador.oApModelo.Usuarios_ADM().lGet_elementos(cFiltroWhere)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la tabla devuelta
                __oApControlador.dtTablaAuxiliar = CType(lResultado(2), DataTable)

                'la asignamos como origen de datos de la grilla
                Me.grd_usuarios.DataSource = __oApControlador.dtTablaAuxiliar

            Else
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Usuarios")

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Datos de Usuarios")

        Finally
            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End Try

    End Sub

    ''' <summary>
    ''' CARGA LOS ITEMS EN EL COMBO DE "Nivel"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __cargar_combo_niveles()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__cargar_combo_niveles()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Try
            'llamamos al metodo de recuperacion de datos de usuarios
            lResultado = __oApControlador.oApModelo.NivelesAccesosADM().lGet_elementos(String.Format("WHERE {0}.{1} = 'True'" _
                                                                                       , NivelesAccesosTBL.NOMBRE_TABLA _
                                                                                       , NivelesAccesosTBL.MOSTRAR.cNombre) _
                                                                                       )

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la lista de instancias devuelta
                __lNiveles = CType(lResultado(1), List(Of NivelAccesoOTD))

                'la asignamos como origen de datos del combo de niveles
                Me.cmb_niveles.DataSource = __lNiveles
                Me.cmb_niveles.DisplayMember = "cDescripcion"

            ElseIf Not lResultado(1).ToString().Equals(ConectorBase.ERROR_NO_HAY_FILAS) Then
                'sino, mensaje de notificacion
                __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Niveles de Acceso")

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Datos de Niveles de Acceso")

        End Try

    End Sub

    ''' <summary>
    ''' CARGA LOS DATOS DEL PRIMER REGISTRO DE LA GRILLA EN LOS CONTROLES DE ABM
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __mostrar_primer_elemento()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__mostrar_primer_elemento()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No EJecutado!"})

        Cursor.Current = Cursors.WaitCursor

        Try
            'establecemos el estado inicial de operacion ABM
            __nEstadoABM = ESTADOS_ABM.VISUALIZANDO

            'si la grilla de usuarios tiene al menos un elemento
            If Me.grd_usuarios.RowCount > 0 Then
                'tomamos el identificador del primer elemento
                __oUsuarioDesplegando = New UsuarioOTD()
                __oUsuarioDesplegando.nId = Me.grd_usuarios.Rows(0).Cells.Item(UsuariosTBL.ID.cNombre).Value.ToString()

                'llamamos al metodo de recuperacion de la instancia correspondiente
                lResultado = __oApControlador.oApModelo.Usuarios_ADM().lGet_elemento(__oUsuarioDesplegando)

                'si se ejecuto sin problemas, tomamosla instancia devuelta
                If lResultado(0).Equals(1) Then
                    __oUsuarioDesplegando = CType(lResultado(1), UsuarioOTD)

                    'desplegamos los datos en los controles correspondientes
                    Me.txt_id.Text = __oUsuarioDesplegando.nId.ToString()
                    Me.txt_nombre_usuario.Text = __oUsuarioDesplegando.cDescripcion
                    Me.chk_habilitado.Checked = __oUsuarioDesplegando.Habilitado
                    Me.cmb_niveles.SelectedItem = __oUsuarioDesplegando.NivelAcceso

                    Me.txt_contrasena.Text = String.Empty
                    Me.txt_confirmar.Text = String.Empty

                    Me.lst_locales.DataSource = __oUsuarioDesplegando.Locales
                    Me.lst_locales.Refresh()

                Else
                    'sino, mensaje de notificacion
                    __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Usuarios")

                End If

            End If

        Catch Ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & Ex.Message, "Datos de Usuarios")

        End Try

        Cursor.Current = Cursors.Default

        'refrescamos el form
        Refresh()

    End Sub

    ''' <summary>
    ''' HABILITA LOS CONTROLES NECESARIOS PARA UNA ACTUALIZACION DE DATOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __habilitar_controles_upd()
        'habilitamos los controles necesarios para una actualizacion
        Me.cmd_cancelar.Enabled = True
        Me.cmd_guardar.Enabled = True
        Me.cmb_niveles.Enabled = True

        'los cuadros de texto
        If __nEstadoABM = ESTADOS_ABM.AGREGANDO Then
            Me.txt_id.ReadOnly = False
            Me.txt_nombre_usuario.ReadOnly = False
            If Me.cmb_niveles.Items.Count > 0 Then Me.cmb_niveles.SelectedIndex = 0
            Me.lst_locales.DataSource = Nothing
        Else
            Me.lst_locales.DataSource = __oUsuarioDesplegando.Locales
        End If

        Me.txt_contrasena.ReadOnly = False
        Me.txt_confirmar.ReadOnly = False

        'otros controles
        Me.chk_habilitado.Enabled = True
        Me.cmb_niveles.Enabled = True
        Me.btn_agregar_local.Enabled = True
        Me.lst_locales.Refresh()

        'deshabilitamos los controles innecesarios
        Me.cmd_agregar.Enabled = False
        Me.cmd_modificar.Enabled = False
        Me.cmd_salir.Enabled = False
        Me.grd_usuarios.Enabled = False

    End Sub

    ''' <summary>
    ''' DESHABILITA LOS CONTROLES NECESARIOS PARA UNA ACTUALIZACION DE DATOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __deshabilitar_controles_upd()
        'deshabilitamos los controles necesarios para una actualizacion
        Me.cmd_cancelar.Enabled = False
        Me.cmd_guardar.Enabled = False

        'los cuadros de texto
        Me.txt_id.ReadOnly = True
        Me.txt_nombre_usuario.ReadOnly = True
        Me.txt_contrasena.ReadOnly = True
        Me.txt_confirmar.ReadOnly = True

        Me.txt_contrasena.Text = String.Empty
        Me.txt_confirmar.Text = String.Empty

        'otros controles
        Me.cmb_niveles.Enabled = False
        Me.chk_habilitado.Enabled = False
        Me.btn_agregar_local.Enabled = False
        Me.btn_quitar_local.Enabled = False
        Me.lst_locales.Refresh()

        'habilitamos los controles necesarios
        Me.cmd_agregar.Enabled = True
        Me.cmd_modificar.Enabled = True
        Me.cmd_salir.Enabled = True
        Me.grd_usuarios.Enabled = True

    End Sub

    ''' <summary>
    ''' GRABA LOS DATOS DEL USUARIO EN LA TABLA CORRESPONDIENTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub __guardar_datos()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".__guardar_datos()"
        Dim lResultado As New List(Of Object)(New Object() {0, NOMBRE_METODO & " No Ejecutado!"})

        Cursor.Current = Cursors.WaitCursor

        Try
            'si se estan Agregando los datos de un usuario
            If __nEstadoABM = ESTADOS_ABM.AGREGANDO Then
                'tomamos el identificador del usuario
                __oUsuarioEditando.nId = Long.Parse(Me.txt_id.Text)

            End If

            'asignamos los demas valores correspondientes a la instancia de edicion
            __oUsuarioEditando.cDescripcion = Me.txt_nombre_usuario.Text.Trim().ToUpper()
            __oUsuarioEditando.NivelAcceso = Me.cmb_niveles.SelectedItem
            __oUsuarioEditando.Habilitado = Me.chk_habilitado.Checked
            __oUsuarioEditando.Locales = __oUsuarioDesplegando.Locales

            'si se esta Agregando
            If __nEstadoABM = ESTADOS_ABM.AGREGANDO Then
                'llamamos al metodo de insersion de datos del usuario
                lResultado = __oApControlador.oApModelo.Usuarios_ADM().lAgregar(__oUsuarioEditando)

            ElseIf __nEstadoABM = ESTADOS_ABM.MODIFICANDO Then
                'sino, si se esta modificando
                lResultado = __oApControlador.oApModelo.Usuarios_ADM().lActualizar(__oUsuarioEditando)

            End If

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'bloqueamos los controles
                __deshabilitar_controles_upd()

                'volvemos a cargar la grilla
                __cargar_grilla()

                'mensaje exitoso
                __oApControlador.notificar_exito("Datos de Usuario guardados correctamente!", "Datos de Usuario")

            Else
                'sino, mensaje de notificacion de error
                __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Usuario")

            End If

        Catch ex As Exception
            'en caso de error, mensaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, "Datos de Usuario")

        End Try

        Cursor.Current = Cursors.Default

    End Sub


#End Region


#Region "EVENTOS"

    ''' <summary>
    ''' Cuando se hace doble click en la cabecera de una fila
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_usuarios_RowHeaderMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd_usuarios.RowHeaderMouseDoubleClick

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".grd_usuarios_RowHeaderMouseDoubleClick()"

        'cursor del mouse a "Esperar"
        Cursor.Current = Cursors.WaitCursor

        Try
            'si la grilla de usuarios tiene al menos un elemento
            If Me.grd_usuarios.RowCount > 0 Then

                'tomamos la fila seleccionada
                With Me.grd_usuarios.SelectedRows(0)

                    'tomamos el identificador del primer elemento
                    __oUsuarioDesplegando = New UsuarioOTD()
                    __oUsuarioDesplegando.nId = .Cells.Item(UsuariosTBL.ID.cNombre).Value.ToString()

                    'llamamos al metodo de recuperacion de la instancia correspondiente
                    Dim lResultado As List(Of Object) = __oApControlador.oApModelo.Usuarios_ADM().lGet_elemento(__oUsuarioDesplegando)

                    'si se ejecuto sin problemas, tomamosla instancia devuelta
                    If lResultado(0).Equals(1) Then
                        __oUsuarioDesplegando = CType(lResultado(1), UsuarioOTD)

                        'desplegamos los datos en los controles correspondientes
                        Me.txt_id.Text = __oUsuarioDesplegando.nId.ToString()
                        Me.txt_nombre_usuario.Text = __oUsuarioDesplegando.cDescripcion
                        Me.chk_habilitado.Checked = __oUsuarioDesplegando.Habilitado
                        Me.cmb_niveles.SelectedItem = __oUsuarioDesplegando.NivelAcceso

                        Me.txt_contrasena.Text = String.Empty
                        Me.txt_confirmar.Text = String.Empty

                        Me.lst_locales.DataSource = __oUsuarioDesplegando.Locales

                    Else
                        'sino, mensaje de notificacion
                        __oApControlador.notificar_error(lResultado(1).ToString(), "Datos de Usuarios")

                    End If

                End With

            End If

        Catch Ex As Exception
            'en caso de error, evento a LOG
            __oApControlador.log().Escribir(NOMBRE_METODO & " Error: " & Ex.Message)

            MessageBox.Show("Error iniciando carga de Datos del Usuario a Editar: " & Ex.Message _
                            , NOMBRE_METODO, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End Try
    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Agregar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_agregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_agregar.Click

        'cambiamos a estado actual de operacion ABM
        __nEstadoABM = ESTADOS_ABM.AGREGANDO
        __oUsuarioEditando = New UsuarioOTD()

        'reseteamos los valores de los controles de ABM
        Me.txt_id.Text = String.Empty
        Me.txt_nombre_usuario.Text = String.Empty

        'llamamos a procedimiento de habilitacion de controles para actualizacion
        __habilitar_controles_upd()

        'enfoque a cuadro de "Id Usuario"
        Me.txt_id.Focus()
        Me.txt_id.SelectAll()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Cancelar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancelar.Click
        'limpiamos el contenido de los controles
        Me.txt_id.Text = String.Empty
        Me.txt_nombre_usuario.Text = String.Empty

        'cambiamos el estado de actualizacion de datos
        __nEstadoABM = ESTADOS_ABM.VISUALIZANDO

        'llamamos a procedimiento de Deshabilitacion de Controles
        __deshabilitar_controles_upd()

        'llamamos a procedimiento de carga de datos en controles de ABM
        __mostrar_primer_elemento()

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Modificar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_modificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_modificar.Click

        'cambiamos a estado actual de operacion ABM
        __nEstadoABM = ESTADOS_ABM.MODIFICANDO

        'clonamos la instancia visualizada
        __oUsuarioEditando = __oUsuarioDesplegando

        'llamamos a procedimiento de habilitacion de controles para actualizacion de datos
        __habilitar_controles_upd()


    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Guardar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_guardar.Click

        'si las contrasenas ingresadas NO coinciden
        If (Len(Me.txt_contrasena.Text.Trim) > 0) And Not Me.txt_contrasena.Text.Trim.Equals(Me.txt_confirmar.Text.Trim) Then
            'mensaje de notificacion
            __oApControlador.notificar_stop("Las Contraseñas Ingresadas no coinciden", "Datos de Usuario")

            'enfoque a cuadro de contrasena
            Me.txt_contrasena.Focus()
            Me.txt_contrasena.SelectAll()

        Else
            'si son iguales, evaluamos la longitud del nombre del usuario
            Select Case Me.txt_nombre_usuario.Text.Trim().Length
                Case Is < 6
                    'si tiene menos de seis caracteres, mensaje de notificacion
                    __oApControlador.notificar_stop("El nombre debe tener al menos 6 Caracteres", "Datos de Usuario")

                    'enfoque a cuadro de "Descripcion"
                    Me.txt_nombre_usuario.Focus()
                    Me.txt_nombre_usuario.SelectAll()

                    'salimos del metodo
                    Exit Sub

                Case Else
                    'si se esta insertando
                    If __nEstadoABM = ESTADOS_ABM.AGREGANDO Then
                        'sino, verificamos la contrasena
                        If Me.txt_contrasena.Text.Trim().Equals(String.Empty) Then
                            'si esta vaci, no se acepta la contrasena
                            __oApControlador.notificar_stop("Debe ingresar una Contraseña!", "Datos de Usuario")

                            'enfoque a cuadro de contrasena
                            Me.txt_contrasena.Focus()

                            'salimos del metodo
                            Exit Sub

                        Else
                            'sino, calculamos la contrasena nueva
                            __oUsuarioEditando.Contrasena = Cripto.Get_SHA1(Me.txt_id.Text.Trim() & Me.txt_contrasena.Text.Trim())

                        End If

                    ElseIf __nEstadoABM = ESTADOS_ABM.MODIFICANDO Then
                        'sino, si se esta modificando y la contrasena nueva no esta creada
                        If Me.txt_contrasena.Text.Trim().Equals(String.Empty) Then
                            'pasamos la original
                            __oUsuarioEditando.Contrasena = __oUsuarioDesplegando.Contrasena

                        Else
                            'sino, le pasamos la nueva
                            __oUsuarioEditando.Contrasena = Cripto.Get_SHA1(Me.txt_id.Text.Trim() & Me.txt_contrasena.Text.Trim())

                        End If

                    End If
            End Select

            'sino, es que todo, llamamos a procedimeinto de Guardado de Datos del Soporte
            __guardar_datos()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO EL CUADRO DE "ID Usuario" PIERDE EL ENFOQUE
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_id_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_id.LostFocus, txt_id.Leave
        'convertimos su contenido a mayusculas
        Me.txt_nombre_usuario.Text = Me.txt_nombre_usuario.Text.ToUpper

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'cerramos y liberamos el formulario
        Close()
        Dispose()

    End Sub

    ''' <summary>
    ''' Cuando de hace click en la Lista de Locales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lst_locales_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lst_locales.MouseClick
        'si hay al menos un item seleccionado, habilitamos el boton de eliminacion
        Me.btn_quitar_local.Enabled = (Me.lst_locales.SelectedItems.Count > 0 _
                                       And (__nEstadoABM = ESTADOS_ABM.MODIFICANDO _
                                            Or __nEstadoABM = ESTADOS_ABM.AGREGANDO))

    End Sub

    ''' <summary>
    ''' Cuando se hace click en el boton [+]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_agregar_local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_agregar_local.Click

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".btn_agregar_local_Click()"

        'cursor del mouse a "Esperar"
        Cursor.Current = Cursors.WaitCursor

        Try
            'si no se cargo previamente la lista de locales
            If __lListaLocales Is Nothing Then
                'recuperamos la lista de locales desde la BD
                Dim lResultado As List(Of Object) = ApModelo.Get_instancia().Locales_ADM.lGet_elementos(String.Empty, New Dictionary(Of String, Object))

                'si NO se ejecuto correctamente
                If Not lResultado(0).Equals(1) Then
                    'excepcion
                    Throw New Exception(lResultado(1))
                Else 'si fue exitoso
                    __lListaLocales = lResultado(1)
                End If

            End If

            'si se recupero la lista de locales
            If Not __lListaLocales Is Nothing And __lListaLocales.Count > 0 Then
                'tomamos la lista devuelta y la pasamos al formulario de seleccion
                Dim frmLocales As FrmListaLocales = New FrmListaLocales(__lListaLocales)

                'desplegamos el formulario en modo dialogo
                frmLocales.ShowDialog()

                'evaluamos el resultado del dialogo
                If frmLocales.DialogResult = Windows.Forms.DialogResult.OK Then
                    'tomamos la lista de locales seleccionados y los agregamos a la lista actual
                    For Each oLocal As LocalOTD In frmLocales.lLocalesSeleccionados
                        'solo si no esta ya 
                        If __oUsuarioDesplegando.Locales.IndexOf(oLocal) = -1 Then
                            __oUsuarioDesplegando.Locales.Add(oLocal)
                        End If
                    Next

                    Me.lst_locales.DataSource = Nothing
                    Me.lst_locales.DataSource = __oUsuarioDesplegando.Locales
                    Me.lst_locales.Refresh()

                End If

            End If

        Catch ex As Exception
            'en caso de error, evento a LOG
            __oApControlador.log().Escribir(NOMBRE_METODO & " Error: " & ex.Message)

            MessageBox.Show("Error iniciando carga de Lista de Locales: " & ex.Message _
                            , NOMBRE_METODO, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'cursor del mouse a "Normal"
            Cursor.Current = Cursors.Default

        End Try


    End Sub

    ''' <summary>
    ''' Cuando se hace click en el boton de eliminacion de locales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_quitar_local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_quitar_local.Click

        'si hay elementos seleccionados
        If Me.lst_locales.SelectedItems.Count > 0 Then
            'los recorremos
            For Each oLocal As LocalOTD In Me.lst_locales.SelectedItems
                'lo eliminamos de la lista de locales actuales del usuario
                __oUsuarioDesplegando.Locales.Remove(oLocal)

            Next

            'limpiamos los locales seleccionados
            Me.lst_locales.DataSource = Nothing
            Me.lst_locales.DataSource = __oUsuarioDesplegando.Locales
            Me.lst_locales.Refresh()

        End If

    End Sub


#End Region

    
End Class