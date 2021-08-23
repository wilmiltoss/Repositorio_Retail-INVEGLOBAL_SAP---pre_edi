<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCargaManual
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCargaManual))
        Me.grp_ubicacion = New System.Windows.Forms.GroupBox
        Me.num_conteo = New System.Windows.Forms.NumericUpDown
        Me.Label10 = New System.Windows.Forms.Label
        Me.num_nro_soporte = New System.Windows.Forms.NumericUpDown
        Me.cmb_letras = New System.Windows.Forms.ComboBox
        Me.cmb_soportes = New System.Windows.Forms.ComboBox
        Me.num_metro = New System.Windows.Forms.NumericUpDown
        Me.num_nivel = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmb_locaciones = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grp_datos = New System.Windows.Forms.GroupBox
        Me.cmd_cancelar = New System.Windows.Forms.Button
        Me.lbl_valor_actual = New System.Windows.Forms.Label
        Me.cmd_guardar = New System.Windows.Forms.Button
        Me.txt_detalle = New System.Windows.Forms.TextBox
        Me.txt_descripcion = New System.Windows.Forms.TextBox
        Me.txt_cantidad = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txt_scanning = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.grd_cargas = New System.Windows.Forms.DataGridView
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel
        Me.grp_comandos = New System.Windows.Forms.GroupBox
        Me.cmd_salir = New System.Windows.Forms.Button
        Me.cmd_limpiar = New System.Windows.Forms.Button
        Me.cmd_validar = New System.Windows.Forms.Button
        Me.grp_ubicacion.SuspendLayout()
        CType(Me.num_conteo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_nro_soporte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_metro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_nivel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_datos.SuspendLayout()
        CType(Me.grd_cargas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.grp_comandos.SuspendLayout()
        Me.SuspendLayout()
        '
        'grp_ubicacion
        '
        Me.grp_ubicacion.Controls.Add(Me.num_conteo)
        Me.grp_ubicacion.Controls.Add(Me.Label10)
        Me.grp_ubicacion.Controls.Add(Me.num_nro_soporte)
        Me.grp_ubicacion.Controls.Add(Me.cmb_letras)
        Me.grp_ubicacion.Controls.Add(Me.cmb_soportes)
        Me.grp_ubicacion.Controls.Add(Me.num_metro)
        Me.grp_ubicacion.Controls.Add(Me.num_nivel)
        Me.grp_ubicacion.Controls.Add(Me.Label5)
        Me.grp_ubicacion.Controls.Add(Me.Label4)
        Me.grp_ubicacion.Controls.Add(Me.Label3)
        Me.grp_ubicacion.Controls.Add(Me.Label2)
        Me.grp_ubicacion.Controls.Add(Me.cmb_locaciones)
        Me.grp_ubicacion.Controls.Add(Me.Label1)
        Me.grp_ubicacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_ubicacion.Location = New System.Drawing.Point(10, 10)
        Me.grp_ubicacion.Name = "grp_ubicacion"
        Me.grp_ubicacion.Size = New System.Drawing.Size(780, 150)
        Me.grp_ubicacion.TabIndex = 0
        Me.grp_ubicacion.TabStop = False
        Me.grp_ubicacion.Text = "Localización a Contabilizar"
        '
        'num_conteo
        '
        Me.num_conteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_conteo.Location = New System.Drawing.Point(120, 45)
        Me.num_conteo.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.num_conteo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_conteo.Name = "num_conteo"
        Me.num_conteo.Size = New System.Drawing.Size(48, 35)
        Me.num_conteo.TabIndex = 2
        Me.num_conteo.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(29, 53)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 25)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Conteo : "
        '
        'num_nro_soporte
        '
        Me.num_nro_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_nro_soporte.Location = New System.Drawing.Point(340, 90)
        Me.num_nro_soporte.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_nro_soporte.Name = "num_nro_soporte"
        Me.num_nro_soporte.Size = New System.Drawing.Size(70, 35)
        Me.num_nro_soporte.TabIndex = 4
        Me.num_nro_soporte.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmb_letras
        '
        Me.cmb_letras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_letras.Enabled = False
        Me.cmb_letras.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_letras.FormattingEnabled = True
        Me.cmb_letras.Location = New System.Drawing.Point(729, 43)
        Me.cmb_letras.Name = "cmb_letras"
        Me.cmb_letras.Size = New System.Drawing.Size(30, 37)
        Me.cmb_letras.TabIndex = 4
        Me.cmb_letras.Visible = False
        '
        'cmb_soportes
        '
        Me.cmb_soportes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_soportes.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_soportes.FormattingEnabled = True
        Me.cmb_soportes.Location = New System.Drawing.Point(120, 90)
        Me.cmb_soportes.Name = "cmb_soportes"
        Me.cmb_soportes.Size = New System.Drawing.Size(220, 37)
        Me.cmb_soportes.TabIndex = 3
        '
        'num_metro
        '
        Me.num_metro.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_metro.Location = New System.Drawing.Point(518, 90)
        Me.num_metro.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.num_metro.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_metro.Name = "num_metro"
        Me.num_metro.Size = New System.Drawing.Size(80, 35)
        Me.num_metro.TabIndex = 6
        Me.num_metro.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'num_nivel
        '
        Me.num_nivel.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_nivel.Location = New System.Drawing.Point(679, 88)
        Me.num_nivel.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.num_nivel.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_nivel.Name = "num_nivel"
        Me.num_nivel.Size = New System.Drawing.Size(80, 35)
        Me.num_nivel.TabIndex = 5
        Me.num_nivel.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(438, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 25)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Metro : "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(609, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 25)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Nivel : "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Enabled = False
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(659, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 25)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Cara : "
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Soporte : "
        '
        'cmb_locaciones
        '
        Me.cmb_locaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_locaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_locaciones.FormattingEnabled = True
        Me.cmb_locaciones.Location = New System.Drawing.Point(300, 43)
        Me.cmb_locaciones.Name = "cmb_locaciones"
        Me.cmb_locaciones.Size = New System.Drawing.Size(298, 37)
        Me.cmb_locaciones.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(190, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Locación : "
        '
        'grp_datos
        '
        Me.grp_datos.Controls.Add(Me.cmd_cancelar)
        Me.grp_datos.Controls.Add(Me.lbl_valor_actual)
        Me.grp_datos.Controls.Add(Me.cmd_guardar)
        Me.grp_datos.Controls.Add(Me.txt_detalle)
        Me.grp_datos.Controls.Add(Me.txt_descripcion)
        Me.grp_datos.Controls.Add(Me.txt_cantidad)
        Me.grp_datos.Controls.Add(Me.Label9)
        Me.grp_datos.Controls.Add(Me.Label8)
        Me.grp_datos.Controls.Add(Me.Label7)
        Me.grp_datos.Controls.Add(Me.txt_scanning)
        Me.grp_datos.Controls.Add(Me.Label6)
        Me.grp_datos.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_datos.Location = New System.Drawing.Point(10, 170)
        Me.grp_datos.Name = "grp_datos"
        Me.grp_datos.Size = New System.Drawing.Size(780, 210)
        Me.grp_datos.TabIndex = 7
        Me.grp_datos.TabStop = False
        Me.grp_datos.Text = "Datos de Articulo"
        '
        'cmd_cancelar
        '
        Me.cmd_cancelar.Enabled = False
        Me.cmd_cancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_cancelar.Location = New System.Drawing.Point(590, 140)
        Me.cmd_cancelar.Name = "cmd_cancelar"
        Me.cmd_cancelar.Size = New System.Drawing.Size(180, 50)
        Me.cmd_cancelar.TabIndex = 14
        Me.cmd_cancelar.Text = "&Cancelar"
        Me.cmd_cancelar.UseVisualStyleBackColor = True
        '
        'lbl_valor_actual
        '
        Me.lbl_valor_actual.AutoSize = True
        Me.lbl_valor_actual.Location = New System.Drawing.Point(630, 30)
        Me.lbl_valor_actual.Name = "lbl_valor_actual"
        Me.lbl_valor_actual.Size = New System.Drawing.Size(138, 24)
        Me.lbl_valor_actual.TabIndex = 13
        Me.lbl_valor_actual.Text = "lbl_valor_actual"
        Me.lbl_valor_actual.Visible = False
        '
        'cmd_guardar
        '
        Me.cmd_guardar.Enabled = False
        Me.cmd_guardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_guardar.Location = New System.Drawing.Point(410, 140)
        Me.cmd_guardar.Name = "cmd_guardar"
        Me.cmd_guardar.Size = New System.Drawing.Size(180, 50)
        Me.cmd_guardar.TabIndex = 12
        Me.cmd_guardar.Text = "&Guardar"
        Me.cmd_guardar.UseVisualStyleBackColor = True
        '
        'txt_detalle
        '
        Me.txt_detalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_detalle.Location = New System.Drawing.Point(180, 100)
        Me.txt_detalle.MaxLength = 200
        Me.txt_detalle.Name = "txt_detalle"
        Me.txt_detalle.ReadOnly = True
        Me.txt_detalle.Size = New System.Drawing.Size(590, 35)
        Me.txt_detalle.TabIndex = 10
        '
        'txt_descripcion
        '
        Me.txt_descripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_descripcion.Location = New System.Drawing.Point(180, 60)
        Me.txt_descripcion.MaxLength = 200
        Me.txt_descripcion.Name = "txt_descripcion"
        Me.txt_descripcion.ReadOnly = True
        Me.txt_descripcion.Size = New System.Drawing.Size(590, 35)
        Me.txt_descripcion.TabIndex = 9
        '
        'txt_cantidad
        '
        Me.txt_cantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_cantidad.Location = New System.Drawing.Point(180, 140)
        Me.txt_cantidad.MaxLength = 6
        Me.txt_cantidad.Name = "txt_cantidad"
        Me.txt_cantidad.Size = New System.Drawing.Size(220, 44)
        Me.txt_cantidad.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(30, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(150, 40)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Detalle :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(30, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 40)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Cantidad :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 40)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Descripción :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txt_scanning
        '
        Me.txt_scanning.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_scanning.Location = New System.Drawing.Point(180, 20)
        Me.txt_scanning.MaxLength = 15
        Me.txt_scanning.Name = "txt_scanning"
        Me.txt_scanning.Size = New System.Drawing.Size(240, 35)
        Me.txt_scanning.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(40, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 40)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Scanning :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'grd_cargas
        '
        Me.grd_cargas.AllowUserToAddRows = False
        Me.grd_cargas.AllowUserToDeleteRows = False
        Me.grd_cargas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.grd_cargas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_cargas.Location = New System.Drawing.Point(10, 390)
        Me.grd_cargas.Name = "grd_cargas"
        Me.grd_cargas.ReadOnly = True
        Me.grd_cargas.Size = New System.Drawing.Size(1040, 240)
        Me.grd_cargas.TabIndex = 15
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_estado})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 631)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1058, 28)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_estado
        '
        Me.lbl_estado.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(102, 23)
        Me.lbl_estado.Text = "0 Registros"
        '
        'grp_comandos
        '
        Me.grp_comandos.Controls.Add(Me.cmd_salir)
        Me.grp_comandos.Controls.Add(Me.cmd_limpiar)
        Me.grp_comandos.Controls.Add(Me.cmd_validar)
        Me.grp_comandos.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_comandos.Location = New System.Drawing.Point(800, 10)
        Me.grp_comandos.Name = "grp_comandos"
        Me.grp_comandos.Size = New System.Drawing.Size(250, 370)
        Me.grp_comandos.TabIndex = 16
        Me.grp_comandos.TabStop = False
        Me.grp_comandos.Text = "Comandos"
        '
        'cmd_salir
        '
        Me.cmd_salir.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_salir.Location = New System.Drawing.Point(10, 290)
        Me.cmd_salir.Name = "cmd_salir"
        Me.cmd_salir.Size = New System.Drawing.Size(230, 60)
        Me.cmd_salir.TabIndex = 19
        Me.cmd_salir.Text = "&Salir"
        Me.cmd_salir.UseVisualStyleBackColor = True
        '
        'cmd_limpiar
        '
        Me.cmd_limpiar.Enabled = False
        Me.cmd_limpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_limpiar.Location = New System.Drawing.Point(10, 100)
        Me.cmd_limpiar.Name = "cmd_limpiar"
        Me.cmd_limpiar.Size = New System.Drawing.Size(230, 60)
        Me.cmd_limpiar.TabIndex = 18
        Me.cmd_limpiar.Text = "&Limpiar Conteo"
        Me.cmd_limpiar.UseVisualStyleBackColor = True
        '
        'cmd_validar
        '
        Me.cmd_validar.Enabled = False
        Me.cmd_validar.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_validar.Location = New System.Drawing.Point(10, 40)
        Me.cmd_validar.Name = "cmd_validar"
        Me.cmd_validar.Size = New System.Drawing.Size(230, 60)
        Me.cmd_validar.TabIndex = 17
        Me.cmd_validar.Text = "&Validar Conteo"
        Me.cmd_validar.UseVisualStyleBackColor = True
        '
        'FrmCargaManual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.grp_comandos)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.grd_cargas)
        Me.Controls.Add(Me.grp_datos)
        Me.Controls.Add(Me.grp_ubicacion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(656, 411)
        Me.Name = "FrmCargaManual"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Carga Manual de Conteos"
        Me.grp_ubicacion.ResumeLayout(False)
        Me.grp_ubicacion.PerformLayout()
        CType(Me.num_conteo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_nro_soporte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_metro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_nivel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_datos.ResumeLayout(False)
        Me.grp_datos.PerformLayout()
        CType(Me.grd_cargas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.grp_comandos.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grp_ubicacion As System.Windows.Forms.GroupBox
    Friend WithEvents cmb_letras As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_soportes As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_locaciones As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grp_datos As System.Windows.Forms.GroupBox
    Friend WithEvents txt_scanning As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_cantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_descripcion As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grd_cargas As System.Windows.Forms.DataGridView
    Friend WithEvents num_metro As System.Windows.Forms.NumericUpDown
    Friend WithEvents num_nivel As System.Windows.Forms.NumericUpDown
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents grp_comandos As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_salir As System.Windows.Forms.Button
    Friend WithEvents cmd_limpiar As System.Windows.Forms.Button
    Friend WithEvents cmd_validar As System.Windows.Forms.Button
    Friend WithEvents txt_detalle As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmd_guardar As System.Windows.Forms.Button
    Friend WithEvents lbl_valor_actual As System.Windows.Forms.Label
    Friend WithEvents cmd_cancelar As System.Windows.Forms.Button
    Friend WithEvents num_nro_soporte As System.Windows.Forms.NumericUpDown
    Friend WithEvents num_conteo As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
