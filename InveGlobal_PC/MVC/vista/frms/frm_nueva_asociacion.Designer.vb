<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_nueva_asociacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_nueva_asociacion))
        Me.lbl_uno = New System.Windows.Forms.Label
        Me.grp_varios = New System.Windows.Forms.GroupBox
        Me.grd_varios = New System.Windows.Forms.DataGridView
        Me.cmd_guardar = New System.Windows.Forms.Button
        Me.cmd_cancelar = New System.Windows.Forms.Button
        Me.grp_varios.SuspendLayout()
        CType(Me.grd_varios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_uno
        '
        Me.lbl_uno.AutoSize = True
        Me.lbl_uno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_uno.Location = New System.Drawing.Point(20, 20)
        Me.lbl_uno.Name = "lbl_uno"
        Me.lbl_uno.Size = New System.Drawing.Size(58, 16)
        Me.lbl_uno.TabIndex = 0
        Me.lbl_uno.Text = "lbl_uno"
        '
        'grp_varios
        '
        Me.grp_varios.Controls.Add(Me.grd_varios)
        Me.grp_varios.Location = New System.Drawing.Point(10, 50)
        Me.grp_varios.Name = "grp_varios"
        Me.grp_varios.Size = New System.Drawing.Size(380, 220)
        Me.grp_varios.TabIndex = 1
        Me.grp_varios.TabStop = False
        Me.grp_varios.Text = "Asociar con : "
        '
        'grd_varios
        '
        Me.grd_varios.AllowUserToAddRows = False
        Me.grd_varios.AllowUserToDeleteRows = False
        Me.grd_varios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_varios.Location = New System.Drawing.Point(10, 20)
        Me.grd_varios.Name = "grd_varios"
        Me.grd_varios.Size = New System.Drawing.Size(360, 190)
        Me.grd_varios.TabIndex = 0
        '
        'cmd_guardar
        '
        Me.cmd_guardar.Location = New System.Drawing.Point(60, 280)
        Me.cmd_guardar.Name = "cmd_guardar"
        Me.cmd_guardar.Size = New System.Drawing.Size(130, 30)
        Me.cmd_guardar.TabIndex = 2
        Me.cmd_guardar.Text = "&Guardar"
        Me.cmd_guardar.UseVisualStyleBackColor = True
        '
        'cmd_cancelar
        '
        Me.cmd_cancelar.Location = New System.Drawing.Point(220, 280)
        Me.cmd_cancelar.Name = "cmd_cancelar"
        Me.cmd_cancelar.Size = New System.Drawing.Size(130, 30)
        Me.cmd_cancelar.TabIndex = 3
        Me.cmd_cancelar.Text = "&Cancelar"
        Me.cmd_cancelar.UseVisualStyleBackColor = True
        '
        'frm_nueva_asociacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 319)
        Me.Controls.Add(Me.cmd_cancelar)
        Me.Controls.Add(Me.cmd_guardar)
        Me.Controls.Add(Me.grp_varios)
        Me.Controls.Add(Me.lbl_uno)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_nueva_asociacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frm_nueva_asociacion"
        Me.grp_varios.ResumeLayout(False)
        CType(Me.grd_varios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_uno As System.Windows.Forms.Label
    Friend WithEvents grp_varios As System.Windows.Forms.GroupBox
    Friend WithEvents grd_varios As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_guardar As System.Windows.Forms.Button
    Friend WithEvents cmd_cancelar As System.Windows.Forms.Button
End Class
