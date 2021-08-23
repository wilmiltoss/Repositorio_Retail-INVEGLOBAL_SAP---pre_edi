<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrincipal
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrincipal))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnu_archivo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_datos_inventario = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_configuracion_inventario = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_obtener_maestro = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_actualizar_teoricos = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_carga_manual = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_detalles_x_ubicaciones = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_resumen_inventario = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_cerrar_inventario = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_colectores = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_maestro_a_colectores = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_actualizar_maestro_colectora = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_actualizar_tablas_parametros = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnu_descargar_lecturas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_opciones = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_soportes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_aso_local_soportes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_usuarios = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_modo_transmision = New System.Windows.Forms.ToolStripComboBox()
        Me.mnu_ayuda = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_info_actual = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_reportar_problema = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_version = New System.Windows.Forms.ToolStripMenuItem()
        Me.grp_datos_actuales = New System.Windows.Forms.GroupBox()
        Me.lbl_estado_inventario = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_empresa = New System.Windows.Forms.Label()
        Me.cmd_sectores = New System.Windows.Forms.Button()
        Me.lbl_nro_conteo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lbl_fecha = New System.Windows.Forms.Label()
        Me.lbl_sistema = New System.Windows.Forms.Label()
        Me.lbl_local = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lbl_mensaje = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbr_progreso = New System.Windows.Forms.ToolStripProgressBar()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        Me.grp_datos_actuales.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_archivo, Me.mnu_colectores, Me.mnu_opciones, Me.mnu_ayuda, Me.mnu_version})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(731, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnu_archivo
        '
        Me.mnu_archivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_datos_inventario, Me.mnu_configuracion_inventario, Me.mnu_obtener_maestro, Me.ToolStripSeparator5, Me.mnu_actualizar_teoricos, Me.ToolStripSeparator2, Me.mnu_carga_manual, Me.mnu_detalles_x_ubicaciones, Me.mnu_resumen_inventario, Me.mnu_cerrar_inventario, Me.ToolStripSeparator1, Me.mnu_salir})
        Me.mnu_archivo.Name = "mnu_archivo"
        Me.mnu_archivo.Size = New System.Drawing.Size(60, 20)
        Me.mnu_archivo.Text = "&Archivo"
        '
        'mnu_datos_inventario
        '
        Me.mnu_datos_inventario.Name = "mnu_datos_inventario"
        Me.mnu_datos_inventario.Size = New System.Drawing.Size(225, 22)
        Me.mnu_datos_inventario.Text = "Datos de &Inventario"
        '
        'mnu_configuracion_inventario
        '
        Me.mnu_configuracion_inventario.Name = "mnu_configuracion_inventario"
        Me.mnu_configuracion_inventario.Size = New System.Drawing.Size(225, 22)
        Me.mnu_configuracion_inventario.Text = "Con&figuracion del Inventario"
        '
        'mnu_obtener_maestro
        '
        Me.mnu_obtener_maestro.Name = "mnu_obtener_maestro"
        Me.mnu_obtener_maestro.Size = New System.Drawing.Size(225, 22)
        Me.mnu_obtener_maestro.Text = "Obtener &Maestro"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(222, 6)
        '
        'mnu_actualizar_teoricos
        '
        Me.mnu_actualizar_teoricos.Name = "mnu_actualizar_teoricos"
        Me.mnu_actualizar_teoricos.Size = New System.Drawing.Size(225, 22)
        Me.mnu_actualizar_teoricos.Text = "Actualizar &Teóricos"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(222, 6)
        '
        'mnu_carga_manual
        '
        Me.mnu_carga_manual.Name = "mnu_carga_manual"
        Me.mnu_carga_manual.Size = New System.Drawing.Size(225, 22)
        Me.mnu_carga_manual.Text = "Car&ga Manual"
        '
        'mnu_detalles_x_ubicaciones
        '
        Me.mnu_detalles_x_ubicaciones.Name = "mnu_detalles_x_ubicaciones"
        Me.mnu_detalles_x_ubicaciones.Size = New System.Drawing.Size(225, 22)
        Me.mnu_detalles_x_ubicaciones.Text = "Detalles x &Ubicaciones"
        '
        'mnu_resumen_inventario
        '
        Me.mnu_resumen_inventario.Name = "mnu_resumen_inventario"
        Me.mnu_resumen_inventario.Size = New System.Drawing.Size(225, 22)
        Me.mnu_resumen_inventario.Text = "&Resumen de Inventario"
        '
        'mnu_cerrar_inventario
        '
        Me.mnu_cerrar_inventario.Name = "mnu_cerrar_inventario"
        Me.mnu_cerrar_inventario.Size = New System.Drawing.Size(225, 22)
        Me.mnu_cerrar_inventario.Text = "&Cerrar Inventario"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(222, 6)
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(225, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'mnu_colectores
        '
        Me.mnu_colectores.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_maestro_a_colectores, Me.ToolStripSeparator4, Me.mnu_actualizar_maestro_colectora, Me.mnu_actualizar_tablas_parametros, Me.ToolStripSeparator3, Me.mnu_descargar_lecturas})
        Me.mnu_colectores.Name = "mnu_colectores"
        Me.mnu_colectores.Size = New System.Drawing.Size(75, 20)
        Me.mnu_colectores.Text = "&Colectores"
        '
        'mnu_maestro_a_colectores
        '
        Me.mnu_maestro_a_colectores.Name = "mnu_maestro_a_colectores"
        Me.mnu_maestro_a_colectores.Size = New System.Drawing.Size(247, 22)
        Me.mnu_maestro_a_colectores.Text = "&Transferir Maestro (PC -> PDA)"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(244, 6)
        '
        'mnu_actualizar_maestro_colectora
        '
        Me.mnu_actualizar_maestro_colectora.Name = "mnu_actualizar_maestro_colectora"
        Me.mnu_actualizar_maestro_colectora.Size = New System.Drawing.Size(247, 22)
        Me.mnu_actualizar_maestro_colectora.Text = "Actuali&zar Maestro de Colectoras"
        '
        'mnu_actualizar_tablas_parametros
        '
        Me.mnu_actualizar_tablas_parametros.Name = "mnu_actualizar_tablas_parametros"
        Me.mnu_actualizar_tablas_parametros.Size = New System.Drawing.Size(247, 22)
        Me.mnu_actualizar_tablas_parametros.Text = "Actualizar Tablas de &Parametros"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(244, 6)
        '
        'mnu_descargar_lecturas
        '
        Me.mnu_descargar_lecturas.Name = "mnu_descargar_lecturas"
        Me.mnu_descargar_lecturas.Size = New System.Drawing.Size(247, 22)
        Me.mnu_descargar_lecturas.Text = "&Descargar Lecturas (PDA -> PC)"
        '
        'mnu_opciones
        '
        Me.mnu_opciones.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_soportes, Me.mnu_aso_local_soportes, Me.mnu_usuarios, Me.mnu_modo_transmision})
        Me.mnu_opciones.Name = "mnu_opciones"
        Me.mnu_opciones.Size = New System.Drawing.Size(69, 20)
        Me.mnu_opciones.Text = "&Opciones"
        '
        'mnu_soportes
        '
        Me.mnu_soportes.Name = "mnu_soportes"
        Me.mnu_soportes.Size = New System.Drawing.Size(242, 22)
        Me.mnu_soportes.Text = "Datos de &Soportes"
        '
        'mnu_aso_local_soportes
        '
        Me.mnu_aso_local_soportes.Name = "mnu_aso_local_soportes"
        Me.mnu_aso_local_soportes.Size = New System.Drawing.Size(242, 22)
        Me.mnu_aso_local_soportes.Text = "Asociación &Tipo Local - Soporte"
        '
        'mnu_usuarios
        '
        Me.mnu_usuarios.Name = "mnu_usuarios"
        Me.mnu_usuarios.Size = New System.Drawing.Size(242, 22)
        Me.mnu_usuarios.Text = "&Usuarios"
        '
        'mnu_modo_transmision
        '
        Me.mnu_modo_transmision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mnu_modo_transmision.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.mnu_modo_transmision.Items.AddRange(New Object() {"Trans. USB", "Trans. WI-FI"})
        Me.mnu_modo_transmision.MaxDropDownItems = 3
        Me.mnu_modo_transmision.Name = "mnu_modo_transmision"
        Me.mnu_modo_transmision.Size = New System.Drawing.Size(121, 23)
        Me.mnu_modo_transmision.ToolTipText = "Modo de transmición de archivos a colectores"
        '
        'mnu_ayuda
        '
        Me.mnu_ayuda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_info_actual, Me.mnu_reportar_problema})
        Me.mnu_ayuda.Name = "mnu_ayuda"
        Me.mnu_ayuda.Size = New System.Drawing.Size(53, 20)
        Me.mnu_ayuda.Text = "A&yuda"
        '
        'mnu_info_actual
        '
        Me.mnu_info_actual.Name = "mnu_info_actual"
        Me.mnu_info_actual.Size = New System.Drawing.Size(190, 22)
        Me.mnu_info_actual.Text = "&Info Actual"
        '
        'mnu_reportar_problema
        '
        Me.mnu_reportar_problema.Name = "mnu_reportar_problema"
        Me.mnu_reportar_problema.Size = New System.Drawing.Size(190, 22)
        Me.mnu_reportar_problema.Text = "Reportar un &Problema"
        '
        'mnu_version
        '
        Me.mnu_version.Name = "mnu_version"
        Me.mnu_version.Size = New System.Drawing.Size(60, 20)
        Me.mnu_version.Text = "&Versión "
        '
        'grp_datos_actuales
        '
        Me.grp_datos_actuales.Controls.Add(Me.lbl_estado_inventario)
        Me.grp_datos_actuales.Controls.Add(Me.Label3)
        Me.grp_datos_actuales.Controls.Add(Me.lbl_empresa)
        Me.grp_datos_actuales.Controls.Add(Me.cmd_sectores)
        Me.grp_datos_actuales.Controls.Add(Me.lbl_nro_conteo)
        Me.grp_datos_actuales.Controls.Add(Me.Label1)
        Me.grp_datos_actuales.Controls.Add(Me.PictureBox1)
        Me.grp_datos_actuales.Controls.Add(Me.lbl_fecha)
        Me.grp_datos_actuales.Controls.Add(Me.lbl_sistema)
        Me.grp_datos_actuales.Controls.Add(Me.lbl_local)
        Me.grp_datos_actuales.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_datos_actuales.Location = New System.Drawing.Point(5, 28)
        Me.grp_datos_actuales.Name = "grp_datos_actuales"
        Me.grp_datos_actuales.Size = New System.Drawing.Size(723, 127)
        Me.grp_datos_actuales.TabIndex = 1
        Me.grp_datos_actuales.TabStop = False
        Me.grp_datos_actuales.Text = "Datos actuales"
        '
        'lbl_estado_inventario
        '
        Me.lbl_estado_inventario.AutoSize = True
        Me.lbl_estado_inventario.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_estado_inventario.ForeColor = System.Drawing.Color.Red
        Me.lbl_estado_inventario.Location = New System.Drawing.Point(128, 84)
        Me.lbl_estado_inventario.Name = "lbl_estado_inventario"
        Me.lbl_estado_inventario.Size = New System.Drawing.Size(27, 29)
        Me.lbl_estado_inventario.TabIndex = 13
        Me.lbl_estado_inventario.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Estado Actual : "
        '
        'lbl_empresa
        '
        Me.lbl_empresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_empresa.Location = New System.Drawing.Point(385, 18)
        Me.lbl_empresa.Name = "lbl_empresa"
        Me.lbl_empresa.Size = New System.Drawing.Size(320, 19)
        Me.lbl_empresa.TabIndex = 11
        Me.lbl_empresa.Text = "Empresa :"
        Me.lbl_empresa.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'cmd_sectores
        '
        Me.cmd_sectores.Location = New System.Drawing.Point(560, 40)
        Me.cmd_sectores.Name = "cmd_sectores"
        Me.cmd_sectores.Size = New System.Drawing.Size(150, 30)
        Me.cmd_sectores.TabIndex = 10
        Me.cmd_sectores.Text = "Sectores C&ubiertos"
        Me.cmd_sectores.UseVisualStyleBackColor = True
        '
        'lbl_nro_conteo
        '
        Me.lbl_nro_conteo.AutoSize = True
        Me.lbl_nro_conteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nro_conteo.ForeColor = System.Drawing.Color.Red
        Me.lbl_nro_conteo.Location = New System.Drawing.Point(618, 94)
        Me.lbl_nro_conteo.Name = "lbl_nro_conteo"
        Me.lbl_nro_conteo.Size = New System.Drawing.Size(27, 29)
        Me.lbl_nro_conteo.TabIndex = 9
        Me.lbl_nro_conteo.Text = "1"
        Me.lbl_nro_conteo.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(496, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Conteo Actual : "
        Me.Label1.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(651, 74)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(54, 50)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'lbl_fecha
        '
        Me.lbl_fecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_fecha.Location = New System.Drawing.Point(316, 48)
        Me.lbl_fecha.Name = "lbl_fecha"
        Me.lbl_fecha.Size = New System.Drawing.Size(238, 16)
        Me.lbl_fecha.TabIndex = 2
        Me.lbl_fecha.Text = "Fecha de Inventario :"
        Me.lbl_fecha.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lbl_sistema
        '
        Me.lbl_sistema.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_sistema.Location = New System.Drawing.Point(6, 48)
        Me.lbl_sistema.Name = "lbl_sistema"
        Me.lbl_sistema.Size = New System.Drawing.Size(278, 16)
        Me.lbl_sistema.TabIndex = 1
        Me.lbl_sistema.Text = "Sistema :"
        Me.lbl_sistema.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lbl_local
        '
        Me.lbl_local.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_local.Location = New System.Drawing.Point(7, 16)
        Me.lbl_local.Name = "lbl_local"
        Me.lbl_local.Size = New System.Drawing.Size(383, 19)
        Me.lbl_local.TabIndex = 0
        Me.lbl_local.Text = "Local :"
        Me.lbl_local.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_mensaje, Me.lbl_estado, Me.pbr_progreso})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 158)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(731, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_mensaje
        '
        Me.lbl_mensaje.Name = "lbl_mensaje"
        Me.lbl_mensaje.Size = New System.Drawing.Size(71, 17)
        Me.lbl_mensaje.Text = "Esperando..."
        '
        'lbl_estado
        '
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(30, 17)
        Me.lbl_estado.Text = "0 / 0"
        '
        'pbr_progreso
        '
        Me.pbr_progreso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pbr_progreso.Name = "pbr_progreso"
        Me.pbr_progreso.Size = New System.Drawing.Size(200, 16)
        Me.pbr_progreso.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(707, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 27)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 8
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(613, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(88, 27)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'FrmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 180)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.grp_datos_actuales)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(739, 208)
        Me.Name = "FrmPrincipal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "..::InveGlobal PC"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.grp_datos_actuales.ResumeLayout(False)
        Me.grp_datos_actuales.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnu_archivo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ayuda As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_reportar_problema As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_datos_inventario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_obtener_maestro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grp_datos_actuales As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_fecha As System.Windows.Forms.Label
    Friend WithEvents lbl_sistema As System.Windows.Forms.Label
    Friend WithEvents lbl_local As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_mensaje As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbr_progreso As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnu_opciones As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_soportes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_colectores As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_maestro_a_colectores As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_descargar_lecturas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_nro_conteo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents mnu_resumen_inventario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_sectores As System.Windows.Forms.Button
    Friend WithEvents mnu_aso_local_soportes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_carga_manual As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lbl_empresa As System.Windows.Forms.Label
    Friend WithEvents mnu_configuracion_inventario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_estado_inventario As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mnu_detalles_x_ubicaciones As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_info_actual As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_actualizar_tablas_parametros As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_usuarios As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_actualizar_maestro_colectora As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_version As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnu_actualizar_teoricos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_cerrar_inventario As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnu_modo_transmision As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
End Class
