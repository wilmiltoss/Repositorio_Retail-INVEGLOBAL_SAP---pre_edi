<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_aso_local_soporte
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_aso_local_soporte))
        Me.tab_padre = New System.Windows.Forms.TabControl
        Me.tab_soportes = New System.Windows.Forms.TabPage
        Me.cmd_asociar_local = New System.Windows.Forms.Button
        Me.cmd_nuevo_soporte = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.grd_locales = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmb_soportes = New System.Windows.Forms.ComboBox
        Me.tab_locales = New System.Windows.Forms.TabPage
        Me.grd_soportes = New System.Windows.Forms.DataGridView
        Me.cmd_asociar_soporte = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmb_tipo_locales = New System.Windows.Forms.ComboBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mnu_archivo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_nuevo_soporte = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem
        Me.cmd_salir = New System.Windows.Forms.Button
        Me.tab_padre.SuspendLayout()
        Me.tab_soportes.SuspendLayout()
        CType(Me.grd_locales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab_locales.SuspendLayout()
        CType(Me.grd_soportes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tab_padre
        '
        Me.tab_padre.Controls.Add(Me.tab_soportes)
        Me.tab_padre.Controls.Add(Me.tab_locales)
        Me.tab_padre.Location = New System.Drawing.Point(0, 50)
        Me.tab_padre.Name = "tab_padre"
        Me.tab_padre.SelectedIndex = 0
        Me.tab_padre.Size = New System.Drawing.Size(490, 210)
        Me.tab_padre.TabIndex = 0
        '
        'tab_soportes
        '
        Me.tab_soportes.Controls.Add(Me.cmd_asociar_local)
        Me.tab_soportes.Controls.Add(Me.cmd_nuevo_soporte)
        Me.tab_soportes.Controls.Add(Me.Label2)
        Me.tab_soportes.Controls.Add(Me.grd_locales)
        Me.tab_soportes.Controls.Add(Me.Label1)
        Me.tab_soportes.Controls.Add(Me.cmb_soportes)
        Me.tab_soportes.Location = New System.Drawing.Point(4, 22)
        Me.tab_soportes.Name = "tab_soportes"
        Me.tab_soportes.Padding = New System.Windows.Forms.Padding(3)
        Me.tab_soportes.Size = New System.Drawing.Size(482, 184)
        Me.tab_soportes.TabIndex = 0
        Me.tab_soportes.Text = "Por Soporte"
        Me.tab_soportes.UseVisualStyleBackColor = True
        '
        'cmd_asociar_local
        '
        Me.cmd_asociar_local.Location = New System.Drawing.Point(30, 70)
        Me.cmd_asociar_local.Name = "cmd_asociar_local"
        Me.cmd_asociar_local.Size = New System.Drawing.Size(170, 30)
        Me.cmd_asociar_local.TabIndex = 5
        Me.cmd_asociar_local.Text = "Asociar con &Tipo Local"
        Me.cmd_asociar_local.UseVisualStyleBackColor = True
        '
        'cmd_nuevo_soporte
        '
        Me.cmd_nuevo_soporte.Location = New System.Drawing.Point(30, 110)
        Me.cmd_nuevo_soporte.Name = "cmd_nuevo_soporte"
        Me.cmd_nuevo_soporte.Size = New System.Drawing.Size(170, 30)
        Me.cmd_nuevo_soporte.TabIndex = 4
        Me.cmd_nuevo_soporte.Text = "&Nuevo Soporte"
        Me.cmd_nuevo_soporte.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tipo de Local Asociado"
        '
        'grd_locales
        '
        Me.grd_locales.AllowUserToAddRows = False
        Me.grd_locales.AllowUserToDeleteRows = False
        Me.grd_locales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_locales.Location = New System.Drawing.Point(220, 30)
        Me.grd_locales.Name = "grd_locales"
        Me.grd_locales.ReadOnly = True
        Me.grd_locales.Size = New System.Drawing.Size(250, 140)
        Me.grd_locales.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Soporte"
        '
        'cmb_soportes
        '
        Me.cmb_soportes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_soportes.FormattingEnabled = True
        Me.cmb_soportes.Location = New System.Drawing.Point(30, 30)
        Me.cmb_soportes.Name = "cmb_soportes"
        Me.cmb_soportes.Size = New System.Drawing.Size(170, 21)
        Me.cmb_soportes.TabIndex = 0
        '
        'tab_locales
        '
        Me.tab_locales.Controls.Add(Me.grd_soportes)
        Me.tab_locales.Controls.Add(Me.cmd_asociar_soporte)
        Me.tab_locales.Controls.Add(Me.Label3)
        Me.tab_locales.Controls.Add(Me.Label4)
        Me.tab_locales.Controls.Add(Me.cmb_tipo_locales)
        Me.tab_locales.Location = New System.Drawing.Point(4, 22)
        Me.tab_locales.Name = "tab_locales"
        Me.tab_locales.Padding = New System.Windows.Forms.Padding(3)
        Me.tab_locales.Size = New System.Drawing.Size(482, 184)
        Me.tab_locales.TabIndex = 1
        Me.tab_locales.Text = "Por Tipo de Local"
        Me.tab_locales.UseVisualStyleBackColor = True
        '
        'grd_soportes
        '
        Me.grd_soportes.AllowUserToAddRows = False
        Me.grd_soportes.AllowUserToDeleteRows = False
        Me.grd_soportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_soportes.Location = New System.Drawing.Point(220, 30)
        Me.grd_soportes.Name = "grd_soportes"
        Me.grd_soportes.ReadOnly = True
        Me.grd_soportes.Size = New System.Drawing.Size(250, 140)
        Me.grd_soportes.TabIndex = 12
        '
        'cmd_asociar_soporte
        '
        Me.cmd_asociar_soporte.Location = New System.Drawing.Point(30, 70)
        Me.cmd_asociar_soporte.Name = "cmd_asociar_soporte"
        Me.cmd_asociar_soporte.Size = New System.Drawing.Size(170, 30)
        Me.cmd_asociar_soporte.TabIndex = 11
        Me.cmd_asociar_soporte.Text = "Aso&ciar con Soporte"
        Me.cmd_asociar_soporte.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(220, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Soportes Asociados"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Tipo Local"
        '
        'cmb_tipo_locales
        '
        Me.cmb_tipo_locales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_tipo_locales.FormattingEnabled = True
        Me.cmb_tipo_locales.Location = New System.Drawing.Point(30, 30)
        Me.cmb_tipo_locales.Name = "cmb_tipo_locales"
        Me.cmb_tipo_locales.Size = New System.Drawing.Size(170, 21)
        Me.cmb_tipo_locales.TabIndex = 6
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_archivo})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(489, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnu_archivo
        '
        Me.mnu_archivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_nuevo_soporte, Me.mnu_salir})
        Me.mnu_archivo.Name = "mnu_archivo"
        Me.mnu_archivo.Size = New System.Drawing.Size(55, 20)
        Me.mnu_archivo.Text = "&Archivo"
        '
        'mnu_nuevo_soporte
        '
        Me.mnu_nuevo_soporte.Name = "mnu_nuevo_soporte"
        Me.mnu_nuevo_soporte.Size = New System.Drawing.Size(157, 22)
        Me.mnu_nuevo_soporte.Text = "&Nuevo Soporte"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(157, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'cmd_salir
        '
        Me.cmd_salir.Location = New System.Drawing.Point(330, 270)
        Me.cmd_salir.Name = "cmd_salir"
        Me.cmd_salir.Size = New System.Drawing.Size(126, 30)
        Me.cmd_salir.TabIndex = 6
        Me.cmd_salir.Text = "&Salir"
        Me.cmd_salir.UseVisualStyleBackColor = True
        '
        'frm_aso_local_soporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 310)
        Me.Controls.Add(Me.cmd_salir)
        Me.Controls.Add(Me.tab_padre)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_aso_local_soporte"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tipos de Locales y Soportes"
        Me.tab_padre.ResumeLayout(False)
        Me.tab_soportes.ResumeLayout(False)
        Me.tab_soportes.PerformLayout()
        CType(Me.grd_locales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab_locales.ResumeLayout(False)
        Me.tab_locales.PerformLayout()
        CType(Me.grd_soportes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tab_padre As System.Windows.Forms.TabControl
    Friend WithEvents tab_soportes As System.Windows.Forms.TabPage
    Friend WithEvents tab_locales As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_soportes As System.Windows.Forms.ComboBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnu_archivo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_nuevo_soporte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_nuevo_soporte As System.Windows.Forms.Button
    Friend WithEvents cmd_salir As System.Windows.Forms.Button
    Friend WithEvents cmd_asociar_local As System.Windows.Forms.Button
    Friend WithEvents cmd_asociar_soporte As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_tipo_locales As System.Windows.Forms.ComboBox
    Friend WithEvents grd_locales As System.Windows.Forms.DataGridView
    Friend WithEvents grd_soportes As System.Windows.Forms.DataGridView
End Class
