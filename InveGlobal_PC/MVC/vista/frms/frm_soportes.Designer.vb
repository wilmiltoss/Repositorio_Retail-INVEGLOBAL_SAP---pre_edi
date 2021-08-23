<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_soportes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_soportes))
        Me.grp_soportes_seleccionados = New System.Windows.Forms.GroupBox
        Me.lbl_cantidad_soportes = New System.Windows.Forms.Label
        Me.grd_soportes = New System.Windows.Forms.DataGridView
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem
        Me.grp_soportes_seleccionados.SuspendLayout()
        CType(Me.grd_soportes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grp_soportes_seleccionados
        '
        Me.grp_soportes_seleccionados.Controls.Add(Me.lbl_cantidad_soportes)
        Me.grp_soportes_seleccionados.Controls.Add(Me.grd_soportes)
        Me.grp_soportes_seleccionados.Location = New System.Drawing.Point(10, 40)
        Me.grp_soportes_seleccionados.Name = "grp_soportes_seleccionados"
        Me.grp_soportes_seleccionados.Size = New System.Drawing.Size(420, 470)
        Me.grp_soportes_seleccionados.TabIndex = 1
        Me.grp_soportes_seleccionados.TabStop = False
        Me.grp_soportes_seleccionados.Text = "Soportes del Inventario"
        '
        'lbl_cantidad_soportes
        '
        Me.lbl_cantidad_soportes.AutoSize = True
        Me.lbl_cantidad_soportes.Location = New System.Drawing.Point(20, 30)
        Me.lbl_cantidad_soportes.Name = "lbl_cantidad_soportes"
        Me.lbl_cantidad_soportes.Size = New System.Drawing.Size(103, 13)
        Me.lbl_cantidad_soportes.TabIndex = 1
        Me.lbl_cantidad_soportes.Text = "0 Soportes Incluidos"
        '
        'grd_soportes
        '
        Me.grd_soportes.AllowUserToAddRows = False
        Me.grd_soportes.AllowUserToDeleteRows = False
        Me.grd_soportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_soportes.Location = New System.Drawing.Point(20, 50)
        Me.grd_soportes.Name = "grd_soportes"
        Me.grd_soportes.ReadOnly = True
        Me.grd_soportes.Size = New System.Drawing.Size(380, 392)
        Me.grd_soportes.TabIndex = 7
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(442, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_salir})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.ArchivoToolStripMenuItem.Text = "&Archivo"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(152, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'frm_soportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 524)
        Me.Controls.Add(Me.grp_soportes_seleccionados)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_soportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Definición de Soportes"
        Me.grp_soportes_seleccionados.ResumeLayout(False)
        Me.grp_soportes_seleccionados.PerformLayout()
        CType(Me.grd_soportes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grp_soportes_seleccionados As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_cantidad_soportes As System.Windows.Forms.Label
    Friend WithEvents grd_soportes As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
End Class
