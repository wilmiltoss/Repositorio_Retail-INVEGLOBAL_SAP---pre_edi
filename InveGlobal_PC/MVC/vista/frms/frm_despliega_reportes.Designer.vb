<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_despliega_reportes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_despliega_reportes))

        'Me.crv_visualizador = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crv_visualizador
        '
        'Me.crv_visualizador.ActiveViewIndex = -1
        'Me.crv_visualizador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        'Me.crv_visualizador.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.crv_visualizador.Location = New System.Drawing.Point(0, 0)
        'Me.crv_visualizador.Name = "crv_visualizador"
        'Me.crv_visualizador.SelectionFormula = ""
        'Me.crv_visualizador.Size = New System.Drawing.Size(615, 559)
        'Me.crv_visualizador.TabIndex = 0
        'Me.crv_visualizador.ViewTimeSelectionFormula = ""
        '
        'frm_despliega_reportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 559)
        'Me.Controls.Add(Me.crv_visualizador)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(327, 274)
        Me.Name = "frm_despliega_reportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visualización de Reportes"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents crv_visualizador As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
