Imports CdgUtiles.Util

Public Class FrmSeleccionLocal

#Region "ATRIBUTOS"


    Public Const NOMBRE_CLASE As String = "FrmSeleccionLocal"

    Private __oApControlador As ApControlador

    Private nIntentos_Fallidos As Integer = 0
    Private nTiempo_Transcurrido As Integer = 0



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

        'llamamos al procedimiento MAIN del modulo Principal
        principal.main()

        'conectamos a la BD, si no conecta
        If Not principal.bConectar_A_BD() Then
            'si no se pudo
            MessageBox.Show("No se Pudo Conectar a la Bases de Datos", "Datos de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'salimos del sub
            Exit Sub

        Else
            'llamamos a procedimiento de Carga de Combo de Locales
            Me.cargar_combo_locales()

        End If

    End Sub

#End Region


#Region "METODOS"

    ''' <summary>
    ''' CARGA LOS ITEMS DEL COMBO DE "Local a Trabajar"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cargar_combo_locales()

        Dim NOMBRE_METODO As String = NOMBRE_CLASE & ".cargar_combo_locales()"

        'intentamos recuperar los datos de los locales con inventarios
        Try
            Dim lResultado As List(Of Object) = __oApControlador.oApModelo.Locales_ADM.lGet_locales_con_inventario()

            'lista de instancias devueltas
            Dim lLocales As New List(Of LocalOTD)

            'si se ejecuto correctamente
            If lResultado(0).Equals(1) Then
                'tomamos la lista de instancias devueltas
                lLocales = CType(lResultado(1), List(Of LocalOTD))

            End If

            'le agregamos una instancia para nuevos inventarios
            Dim oLocal As New LocalOTD()
            oLocal.cDescripcion = "NUEVO INVENTARIO"
            lLocales.Insert(0, oLocal)

            'la asignamos como origen de datos del combo de locales
            cmb_locales.DataSource = lLocales

            'cmb_locales.DisplayMember = "cDescripcion"
            cmb_locales.SelectedItem = cmb_locales.Items(0)

        Catch ex As Exception
            'en caso de error, emnsaje de notificacion
            __oApControlador.notificar_error(NOMBRE_METODO & " Error: " & ex.Message, ApControlador.NOMBRE_APLICACION)

        End Try

    End Sub

    ''' <summary>
    ''' PROCEDIMIENTO DE BLOQUEO DE APLICACION
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pBloquear_Aplicacion()
        'habilitamos el Timer
        Me.Timer1.Enabled = True

        'bloqueamos todo el formulario
        Me.Enabled = False

    End Sub


#End Region


#Region "EVENTOS"


    ''' <summary>
    ''' EVENTO DEL TIMER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'si el tiempo transcurrido es igual a 2
        If nTiempo_Transcurrido = 3 Then
            'desbloqueamos el formulario
            Me.Enabled = True

            'deshabilitamos este timer
            Me.Timer1.Enabled = False

            'cerramos el formulario
            Me.Close()

        End If
        'incrementamos el contador de tiempo transcurrido
        nTiempo_Transcurrido = nTiempo_Transcurrido + 1

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Aceptar]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_aceptar_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_aceptar.Click

        'si hay campos vacios
        If Me.txt_usuario.Text.Equals(String.Empty) _
            Or Me.txt_contrasena.Text.Equals(String.Empty) Then
            'mensaje de notificacion
            MessageBox.Show("Debe Ingresar todos los Datos!", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            'pasamos el enfoque al cuadro vacio
            If Me.txt_contrasena.Text.Equals(String.Empty) Then Me.txt_contrasena.Focus()
            If Me.txt_usuario.Text.Equals(String.Empty) Then Me.txt_usuario.Focus()

            'salimos del sub
            Exit Sub

        End If

        'tomamos los datos de Id y Contrasena para formar la contrasena compuesta y encriptada
        Dim aContrasena As Byte() = Cripto.Get_SHA1(Me.txt_usuario.Text & Me.txt_contrasena.Text)

        'creamos una nueva instancia de UsuarioOTD
        Dim oUsuario As New UsuarioOTD(Long.Parse(Me.txt_usuario.Text), String.Empty, aContrasena, New NivelAccesoOTD(), True)
        oUsuario.Locales.Add(Me.cmb_locales.SelectedItem)

        'llamamos al metodo de login
        Dim lResultado As List(Of Object) = __oApControlador.lIniciar_sesion(oUsuario)

        'si se ejecuto correctamente
        If lResultado(0).Equals(1) Then

            'obtener la instancia de local seleccionado
            __oApControlador.Local_OTD = CType(Me.cmb_locales.SelectedItem, LocalOTD)

            'establecemos el Resultado del Dialogo a "OK"
            Me.DialogResult = Windows.Forms.DialogResult.OK

            'evento a LOG
            __oApControlador.log().Escribir(String.Format("Iniciada Sesion de Usuario : {0}, para Local : {1}" _
                                                      , __oApControlador.Usuario_OTD.nId, __oApControlador.Local_OTD.nId) _
                                                      )

            'cerramos este formulario
            Me.Close()

        Else
            'sino, incrementamos el contador de intentos fallidos
            nIntentos_Fallidos += 1

            'mensaje de notificacion
            __oApControlador.notificar_stop(lResultado(1).ToString(), "Inicio de Sesión")

            'evento a LOG
            __oApControlador.log().Escribir(String.Format("Intento de Inicio de Sesion #{0} No Valido : {1}" _
                                                          , nIntentos_Fallidos, __oApControlador.Usuario_OTD.nId))

            'si es el tercer intento
            If nIntentos_Fallidos = 3 Then
                'bloqueamos la aplicacion
                Me.pBloquear_Aplicacion()

            End If

            'borramos el contenido del cuadro de "Contrasena"
            Me.txt_contrasena.Text = String.Empty

            'pasamos el enfoque al cuadro de "ID de Usuario"
            Me.txt_usuario.Focus()
            Me.txt_usuario.SelectAll()

        End If

    End Sub

    ''' <summary>
    ''' CUANDO SE HACE CLICK EN [Salir]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmd_salir_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_salir.Click
        'establecemos el Resultado del Dialogo a "Cancel"
        Me.DialogResult = Windows.Forms.DialogResult.Cancel

        'cerramos el formulario
        Me.Close()

    End Sub


#End Region



End Class