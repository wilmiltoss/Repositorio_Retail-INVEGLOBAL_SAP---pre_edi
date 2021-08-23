<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmListaLocales
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
        Me.pnl_inferior = New System.Windows.Forms.Panel
        Me.btn_aceptar = New System.Windows.Forms.Button
        Me.btn_cancelar = New System.Windows.Forms.Button
        Me.lst_locales = New System.Windows.Forms.ListBox
        Me.pnl_inferior.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_inferior
        '
        Me.pnl_inferior.Controls.Add(Me.btn_aceptar)
        Me.pnl_inferior.Controls.Add(Me.btn_cancelar)
        Me.pnl_inferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_inferior.Location = New System.Drawing.Point(0, 309)
        Me.pnl_inferior.Name = "pnl_inferior"
        Me.pnl_inferior.Size = New System.Drawing.Size(278, 48)
        Me.pnl_inferior.TabIndex = 0
        '
        'btn_aceptar
        '
        Me.btn_aceptar.Enabled = False
        Me.btn_aceptar.Image = Global.InveStock_PC_2.My.Resources.Resources.aceptar
        Me.btn_aceptar.Location = New System.Drawing.Point(176, 6)
        Me.btn_aceptar.Name = "btn_aceptar"
        Me.btn_aceptar.Size = New System.Drawing.Size(96, 33)
        Me.btn_aceptar.TabIndex = 1
        Me.btn_aceptar.UseVisualStyleBackColor = True
        '
        'btn_cancelar
        '
        Me.btn_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_cancelar.Image = Global.InveStock_PC_2.My.Resources.Resources.cancelar
        Me.btn_cancelar.Location = New System.Drawing.Point(12, 6)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(96, 33)
        Me.btn_cancelar.TabIndex = 0
        Me.btn_cancelar.UseVisualStyleBackColor = True
        '
        'lst_locales
        '
        Me.lst_locales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lst_locales.FormattingEnabled = True
        Me.lst_locales.Location = New System.Drawing.Point(0, 0)
        Me.lst_locales.Name = "lst_locales"
        Me.lst_locales.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lst_locales.Size = New System.Drawing.Size(278, 303)
        Me.lst_locales.TabIndex = 1
        '
        'FrmListaLocales
        '
        Me.AcceptButton = Me.btn_aceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_cancelar
        Me.ClientSize = New System.Drawing.Size(278, 357)
        Me.ControlBox = False
        Me.Controls.Add(Me.lst_locales)
        Me.Controls.Add(Me.pnl_inferior)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmListaLocales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lista de Locales"
        Me.pnl_inferior.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_inferior As System.Windows.Forms.Panel
    Friend WithEvents btn_aceptar As System.Windows.Forms.Button
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button
    Friend WithEvents lst_locales As System.Windows.Forms.ListBox
End Class
