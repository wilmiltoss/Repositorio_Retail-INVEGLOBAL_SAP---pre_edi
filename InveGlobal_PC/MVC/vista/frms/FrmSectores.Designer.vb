<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSectores
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSectores))
        Me.Label2 = New System.Windows.Forms.Label
        Me.grd_sectores = New System.Windows.Forms.DataGridView
        Me.cmd_aceptar = New System.Windows.Forms.Button
        Me.cmd_cancelar = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel
        Me.tvw_arbol_sectores = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.chk_todos = New System.Windows.Forms.CheckBox
        CType(Me.grd_sectores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(270, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Datos de Sector/es a Inventariar"
        '
        'grd_sectores
        '
        Me.grd_sectores.AllowUserToAddRows = False
        Me.grd_sectores.AllowUserToDeleteRows = False
        Me.grd_sectores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_sectores.Location = New System.Drawing.Point(320, 75)
        Me.grd_sectores.Name = "grd_sectores"
        Me.grd_sectores.ReadOnly = True
        Me.grd_sectores.Size = New System.Drawing.Size(390, 355)
        Me.grd_sectores.TabIndex = 4
        '
        'cmd_aceptar
        '
        Me.cmd_aceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmd_aceptar.Location = New System.Drawing.Point(480, 41)
        Me.cmd_aceptar.Name = "cmd_aceptar"
        Me.cmd_aceptar.Size = New System.Drawing.Size(93, 28)
        Me.cmd_aceptar.TabIndex = 2
        Me.cmd_aceptar.Text = "&Aceptar"
        Me.cmd_aceptar.UseVisualStyleBackColor = True
        '
        'cmd_cancelar
        '
        Me.cmd_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmd_cancelar.Location = New System.Drawing.Point(579, 41)
        Me.cmd_cancelar.Name = "cmd_cancelar"
        Me.cmd_cancelar.Size = New System.Drawing.Size(93, 28)
        Me.cmd_cancelar.TabIndex = 3
        Me.cmd_cancelar.Text = "&Cancelar"
        Me.cmd_cancelar.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_estado})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 433)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(722, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_estado
        '
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(68, 17)
        Me.lbl_estado.Text = "Esperando..."
        '
        'tvw_arbol_sectores
        '
        Me.tvw_arbol_sectores.CheckBoxes = True
        Me.tvw_arbol_sectores.Location = New System.Drawing.Point(12, 75)
        Me.tvw_arbol_sectores.Name = "tvw_arbol_sectores"
        Me.tvw_arbol_sectores.Size = New System.Drawing.Size(308, 355)
        Me.tvw_arbol_sectores.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Indicar Sector/es : "
        '
        'chk_todos
        '
        Me.chk_todos.AutoSize = True
        Me.chk_todos.Location = New System.Drawing.Point(249, 57)
        Me.chk_todos.Name = "chk_todos"
        Me.chk_todos.Size = New System.Drawing.Size(56, 17)
        Me.chk_todos.TabIndex = 7
        Me.chk_todos.Text = "Todos"
        Me.chk_todos.UseVisualStyleBackColor = True
        '
        'FrmSectores
        '
        Me.AcceptButton = Me.cmd_aceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmd_cancelar
        Me.ClientSize = New System.Drawing.Size(722, 455)
        Me.ControlBox = False
        Me.Controls.Add(Me.grd_sectores)
        Me.Controls.Add(Me.tvw_arbol_sectores)
        Me.Controls.Add(Me.chk_todos)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.cmd_cancelar)
        Me.Controls.Add(Me.cmd_aceptar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSectores"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleccionar Sector a Inventariar"
        CType(Me.grd_sectores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grd_sectores As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_aceptar As System.Windows.Forms.Button
    Friend WithEvents cmd_cancelar As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tvw_arbol_sectores As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chk_todos As System.Windows.Forms.CheckBox
End Class
