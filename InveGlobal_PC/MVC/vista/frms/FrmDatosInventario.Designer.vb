<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDatosInventario
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDatosInventario))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.grp_datos_inventario = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmb_empresa = New System.Windows.Forms.ComboBox
        Me.cmd_sector = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt_comentarios = New System.Windows.Forms.TextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txt_sistema_gestion = New System.Windows.Forms.TextBox
        Me.cmb_local = New System.Windows.Forms.ComboBox
        Me.dtp_fecha_inventario = New System.Windows.Forms.DateTimePicker
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_nuevo_inventario = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_guardar_datos = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grp_datos_inventario.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(268, 308)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 9
        Me.OK_Button.Text = "&Aceptar"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Enabled = False
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 10
        Me.Cancel_Button.Text = "&Cancelar"
        '
        'grp_datos_inventario
        '
        Me.grp_datos_inventario.Controls.Add(Me.Label5)
        Me.grp_datos_inventario.Controls.Add(Me.cmb_empresa)
        Me.grp_datos_inventario.Controls.Add(Me.cmd_sector)
        Me.grp_datos_inventario.Controls.Add(Me.Label1)
        Me.grp_datos_inventario.Controls.Add(Me.txt_comentarios)
        Me.grp_datos_inventario.Controls.Add(Me.PictureBox1)
        Me.grp_datos_inventario.Controls.Add(Me.Label4)
        Me.grp_datos_inventario.Controls.Add(Me.Label3)
        Me.grp_datos_inventario.Controls.Add(Me.Label2)
        Me.grp_datos_inventario.Controls.Add(Me.txt_sistema_gestion)
        Me.grp_datos_inventario.Controls.Add(Me.cmb_local)
        Me.grp_datos_inventario.Controls.Add(Me.dtp_fecha_inventario)
        Me.grp_datos_inventario.Location = New System.Drawing.Point(12, 36)
        Me.grp_datos_inventario.Name = "grp_datos_inventario"
        Me.grp_datos_inventario.Size = New System.Drawing.Size(418, 264)
        Me.grp_datos_inventario.TabIndex = 1
        Me.grp_datos_inventario.TabStop = False
        Me.grp_datos_inventario.Text = "Datos de Inventario Actual"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(80, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Empresa : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_empresa
        '
        Me.cmb_empresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_empresa.Enabled = False
        Me.cmb_empresa.FormattingEnabled = True
        Me.cmb_empresa.Location = New System.Drawing.Point(190, 62)
        Me.cmb_empresa.Name = "cmb_empresa"
        Me.cmb_empresa.Size = New System.Drawing.Size(212, 21)
        Me.cmb_empresa.TabIndex = 3
        '
        'cmd_sector
        '
        Me.cmd_sector.Enabled = False
        Me.cmd_sector.Location = New System.Drawing.Point(190, 150)
        Me.cmd_sector.Name = "cmd_sector"
        Me.cmd_sector.Size = New System.Drawing.Size(210, 30)
        Me.cmd_sector.TabIndex = 6
        Me.cmd_sector.Text = "Seleccionar Sectores ..."
        Me.cmd_sector.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 174)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Comentarios : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txt_comentarios
        '
        Me.txt_comentarios.Enabled = False
        Me.txt_comentarios.Location = New System.Drawing.Point(20, 190)
        Me.txt_comentarios.MaxLength = 255
        Me.txt_comentarios.Multiline = True
        Me.txt_comentarios.Name = "txt_comentarios"
        Me.txt_comentarios.Size = New System.Drawing.Size(380, 60)
        Me.txt_comentarios.TabIndex = 7
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 18)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(51, 61)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(31, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(153, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Sistema de Gestion del Local : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(80, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Local a Inventariar : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(73, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Fecha de Inventario : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txt_sistema_gestion
        '
        Me.txt_sistema_gestion.Enabled = False
        Me.txt_sistema_gestion.Location = New System.Drawing.Point(190, 120)
        Me.txt_sistema_gestion.Name = "txt_sistema_gestion"
        Me.txt_sistema_gestion.Size = New System.Drawing.Size(212, 20)
        Me.txt_sistema_gestion.TabIndex = 5
        '
        'cmb_local
        '
        Me.cmb_local.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_local.Enabled = False
        Me.cmb_local.FormattingEnabled = True
        Me.cmb_local.Location = New System.Drawing.Point(190, 92)
        Me.cmb_local.Name = "cmb_local"
        Me.cmb_local.Size = New System.Drawing.Size(212, 21)
        Me.cmb_local.TabIndex = 4
        '
        'dtp_fecha_inventario
        '
        Me.dtp_fecha_inventario.Enabled = False
        Me.dtp_fecha_inventario.Location = New System.Drawing.Point(190, 30)
        Me.dtp_fecha_inventario.Name = "dtp_fecha_inventario"
        Me.dtp_fecha_inventario.Size = New System.Drawing.Size(212, 20)
        Me.dtp_fecha_inventario.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(442, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_nuevo_inventario, Me.mnu_guardar_datos, Me.mnu_salir})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.ArchivoToolStripMenuItem.Text = "&Archivo"
        '
        'mnu_nuevo_inventario
        '
        Me.mnu_nuevo_inventario.Name = "mnu_nuevo_inventario"
        Me.mnu_nuevo_inventario.Size = New System.Drawing.Size(194, 22)
        Me.mnu_nuevo_inventario.Text = "&Nuevo Inventario"
        '
        'mnu_guardar_datos
        '
        Me.mnu_guardar_datos.Enabled = False
        Me.mnu_guardar_datos.Name = "mnu_guardar_datos"
        Me.mnu_guardar_datos.Size = New System.Drawing.Size(194, 22)
        Me.mnu_guardar_datos.Text = "&Guardar Datos Nuevos"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(194, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'frm_datos_inventario
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(442, 340)
        Me.Controls.Add(Me.grp_datos_inventario)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_datos_inventario"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Datos de Inventario"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.grp_datos_inventario.ResumeLayout(False)
        Me.grp_datos_inventario.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents grp_datos_inventario As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_sistema_gestion As System.Windows.Forms.TextBox
    Friend WithEvents cmb_local As System.Windows.Forms.ComboBox
    Friend WithEvents dtp_fecha_inventario As System.Windows.Forms.DateTimePicker
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_nuevo_inventario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_guardar_datos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_comentarios As System.Windows.Forms.TextBox
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_sector As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_empresa As System.Windows.Forms.ComboBox

End Class
