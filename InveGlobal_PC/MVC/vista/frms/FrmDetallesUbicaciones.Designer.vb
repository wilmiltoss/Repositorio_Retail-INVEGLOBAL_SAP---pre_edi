<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDetallesUbicaciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDetallesUbicaciones))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mnu_archivo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_reportes = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_planilla_diferencias = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_exportar_csv = New System.Windows.Forms.ToolStripMenuItem
        Me.grd_detalles_ubicaciones = New System.Windows.Forms.DataGridView
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel
        Me.svf_guardar_archivo = New System.Windows.Forms.SaveFileDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.num_nro_soporte = New System.Windows.Forms.NumericUpDown
        Me.cmd_buscar = New System.Windows.Forms.Button
        Me.cmb_soportes = New System.Windows.Forms.ComboBox
        Me.cmb_locaciones = New System.Windows.Forms.ComboBox
        Me.cmd_excel = New System.Windows.Forms.Button
        Me.num_nivel = New System.Windows.Forms.NumericUpDown
        Me.num_metro = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.chk_modo_prueba = New System.Windows.Forms.CheckBox
        Me.MenuStrip1.SuspendLayout()
        CType(Me.grd_detalles_ubicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.num_nro_soporte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_nivel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_metro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_archivo, Me.mnu_reportes})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(885, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnu_archivo
        '
        Me.mnu_archivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_salir})
        Me.mnu_archivo.Name = "mnu_archivo"
        Me.mnu_archivo.Size = New System.Drawing.Size(60, 20)
        Me.mnu_archivo.Text = "&Archivo"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(96, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'mnu_reportes
        '
        Me.mnu_reportes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_planilla_diferencias, Me.mnu_exportar_csv})
        Me.mnu_reportes.Name = "mnu_reportes"
        Me.mnu_reportes.Size = New System.Drawing.Size(65, 20)
        Me.mnu_reportes.Text = "&Reportes"
        '
        'mnu_planilla_diferencias
        '
        Me.mnu_planilla_diferencias.Name = "mnu_planilla_diferencias"
        Me.mnu_planilla_diferencias.Size = New System.Drawing.Size(189, 22)
        Me.mnu_planilla_diferencias.Text = "&Planilla de Diferencias"
        '
        'mnu_exportar_csv
        '
        Me.mnu_exportar_csv.Name = "mnu_exportar_csv"
        Me.mnu_exportar_csv.Size = New System.Drawing.Size(189, 22)
        Me.mnu_exportar_csv.Text = "&Exportar a CSV"
        '
        'grd_detalles_ubicaciones
        '
        Me.grd_detalles_ubicaciones.AllowUserToAddRows = False
        Me.grd_detalles_ubicaciones.AllowUserToDeleteRows = False
        Me.grd_detalles_ubicaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_detalles_ubicaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd_detalles_ubicaciones.Location = New System.Drawing.Point(0, 127)
        Me.grd_detalles_ubicaciones.Name = "grd_detalles_ubicaciones"
        Me.grd_detalles_ubicaciones.ReadOnly = True
        Me.grd_detalles_ubicaciones.Size = New System.Drawing.Size(885, 380)
        Me.grd_detalles_ubicaciones.TabIndex = 1
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_estado})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 507)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(885, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_estado
        '
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(64, 17)
        Me.lbl_estado.Text = "0 Registros"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chk_modo_prueba)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.num_metro)
        Me.Panel1.Controls.Add(Me.num_nivel)
        Me.Panel1.Controls.Add(Me.num_nro_soporte)
        Me.Panel1.Controls.Add(Me.cmd_buscar)
        Me.Panel1.Controls.Add(Me.cmb_soportes)
        Me.Panel1.Controls.Add(Me.cmb_locaciones)
        Me.Panel1.Controls.Add(Me.cmd_excel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(885, 103)
        Me.Panel1.TabIndex = 8
        '
        'num_nro_soporte
        '
        Me.num_nro_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_nro_soporte.Location = New System.Drawing.Point(452, 17)
        Me.num_nro_soporte.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_nro_soporte.Name = "num_nro_soporte"
        Me.num_nro_soporte.Size = New System.Drawing.Size(45, 22)
        Me.num_nro_soporte.TabIndex = 11
        Me.num_nro_soporte.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmd_buscar
        '
        Me.cmd_buscar.Image = CType(resources.GetObject("cmd_buscar.Image"), System.Drawing.Image)
        Me.cmd_buscar.Location = New System.Drawing.Point(521, 35)
        Me.cmd_buscar.Name = "cmd_buscar"
        Me.cmd_buscar.Size = New System.Drawing.Size(38, 40)
        Me.cmd_buscar.TabIndex = 12
        Me.cmd_buscar.UseVisualStyleBackColor = True
        '
        'cmb_soportes
        '
        Me.cmb_soportes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_soportes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_soportes.FormattingEnabled = True
        Me.cmb_soportes.Location = New System.Drawing.Point(87, 44)
        Me.cmb_soportes.Name = "cmb_soportes"
        Me.cmb_soportes.Size = New System.Drawing.Size(246, 24)
        Me.cmb_soportes.TabIndex = 10
        '
        'cmb_locaciones
        '
        Me.cmb_locaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_locaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_locaciones.FormattingEnabled = True
        Me.cmb_locaciones.Location = New System.Drawing.Point(87, 14)
        Me.cmb_locaciones.Name = "cmb_locaciones"
        Me.cmb_locaciones.Size = New System.Drawing.Size(247, 24)
        Me.cmb_locaciones.TabIndex = 9
        '
        'cmd_excel
        '
        Me.cmd_excel.Image = CType(resources.GetObject("cmd_excel.Image"), System.Drawing.Image)
        Me.cmd_excel.Location = New System.Drawing.Point(578, 35)
        Me.cmd_excel.Name = "cmd_excel"
        Me.cmd_excel.Size = New System.Drawing.Size(40, 40)
        Me.cmd_excel.TabIndex = 8
        Me.cmd_excel.UseVisualStyleBackColor = True
        '
        'num_nivel
        '
        Me.num_nivel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_nivel.Location = New System.Drawing.Point(452, 72)
        Me.num_nivel.Name = "num_nivel"
        Me.num_nivel.Size = New System.Drawing.Size(45, 22)
        Me.num_nivel.TabIndex = 13
        '
        'num_metro
        '
        Me.num_metro.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.num_metro.Location = New System.Drawing.Point(452, 44)
        Me.num_metro.Name = "num_metro"
        Me.num_metro.Size = New System.Drawing.Size(45, 22)
        Me.num_metro.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Locación :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 16)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Soporte :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(356, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 16)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Nro. Soporte :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(356, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Metro :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(356, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 16)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Nivel :"
        '
        'chk_modo_prueba
        '
        Me.chk_modo_prueba.AutoSize = True
        Me.chk_modo_prueba.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_modo_prueba.Location = New System.Drawing.Point(87, 76)
        Me.chk_modo_prueba.Name = "chk_modo_prueba"
        Me.chk_modo_prueba.Size = New System.Drawing.Size(109, 20)
        Me.chk_modo_prueba.TabIndex = 20
        Me.chk_modo_prueba.Text = "Sólo Pruebas"
        Me.chk_modo_prueba.UseVisualStyleBackColor = True
        '
        'FrmDetallesUbicaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(885, 529)
        Me.ControlBox = False
        Me.Controls.Add(Me.grd_detalles_ubicaciones)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(844, 435)
        Me.Name = "FrmDetallesUbicaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detalles por Ubicaciones"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.grd_detalles_ubicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.num_nro_soporte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_nivel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_metro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnu_archivo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_reportes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grd_detalles_ubicaciones As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnu_planilla_diferencias As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents svf_guardar_archivo As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnu_exportar_csv As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents num_nro_soporte As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmd_buscar As System.Windows.Forms.Button
    Friend WithEvents cmb_soportes As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_locaciones As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_excel As System.Windows.Forms.Button
    Friend WithEvents num_metro As System.Windows.Forms.NumericUpDown
    Friend WithEvents num_nivel As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chk_modo_prueba As System.Windows.Forms.CheckBox
End Class
