<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_opciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_opciones))
        Me.grp_opciones = New System.Windows.Forms.GroupBox
        Me.chk_pesables = New System.Windows.Forms.CheckBox
        Me.chk_decimales = New System.Windows.Forms.CheckBox
        Me.txt_cantidad_maxima_conteo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grd_comandos = New System.Windows.Forms.GroupBox
        Me.cmd_aceptar = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.grp_opciones.SuspendLayout()
        Me.grd_comandos.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grp_opciones
        '
        Me.grp_opciones.Controls.Add(Me.chk_pesables)
        Me.grp_opciones.Controls.Add(Me.chk_decimales)
        Me.grp_opciones.Controls.Add(Me.txt_cantidad_maxima_conteo)
        Me.grp_opciones.Controls.Add(Me.Label1)
        Me.grp_opciones.Location = New System.Drawing.Point(10, 40)
        Me.grp_opciones.Name = "grp_opciones"
        Me.grp_opciones.Size = New System.Drawing.Size(240, 160)
        Me.grp_opciones.TabIndex = 0
        Me.grp_opciones.TabStop = False
        Me.grp_opciones.Text = "Opciones de Inventario"
        '
        'chk_pesables
        '
        Me.chk_pesables.AutoSize = True
        Me.chk_pesables.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_pesables.Location = New System.Drawing.Point(30, 130)
        Me.chk_pesables.Name = "chk_pesables"
        Me.chk_pesables.Size = New System.Drawing.Size(147, 19)
        Me.chk_pesables.TabIndex = 3
        Me.chk_pesables.Text = "Pesables Incluidos"
        Me.chk_pesables.UseVisualStyleBackColor = True
        '
        'chk_decimales
        '
        Me.chk_decimales.AutoSize = True
        Me.chk_decimales.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_decimales.Location = New System.Drawing.Point(30, 100)
        Me.chk_decimales.Name = "chk_decimales"
        Me.chk_decimales.Size = New System.Drawing.Size(195, 19)
        Me.chk_decimales.TabIndex = 2
        Me.chk_decimales.Text = "Cantidades con decimales"
        Me.chk_decimales.UseVisualStyleBackColor = True
        '
        'txt_cantidad_maxima_conteo
        '
        Me.txt_cantidad_maxima_conteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_cantidad_maxima_conteo.Location = New System.Drawing.Point(20, 60)
        Me.txt_cantidad_maxima_conteo.MaxLength = 8
        Me.txt_cantidad_maxima_conteo.Name = "txt_cantidad_maxima_conteo"
        Me.txt_cantidad_maxima_conteo.Size = New System.Drawing.Size(200, 22)
        Me.txt_cantidad_maxima_conteo.TabIndex = 1
        Me.txt_cantidad_maxima_conteo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 40)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cantidad Máxima Ingresable por Articulo en Existencia: "
        '
        'grd_comandos
        '
        Me.grd_comandos.Controls.Add(Me.cmd_aceptar)
        Me.grd_comandos.Controls.Add(Me.PictureBox1)
        Me.grd_comandos.Location = New System.Drawing.Point(260, 40)
        Me.grd_comandos.Name = "grd_comandos"
        Me.grd_comandos.Size = New System.Drawing.Size(180, 160)
        Me.grd_comandos.TabIndex = 1
        Me.grd_comandos.TabStop = False
        Me.grd_comandos.Text = "Comandos"
        '
        'cmd_aceptar
        '
        Me.cmd_aceptar.Location = New System.Drawing.Point(10, 40)
        Me.cmd_aceptar.Name = "cmd_aceptar"
        Me.cmd_aceptar.Size = New System.Drawing.Size(160, 40)
        Me.cmd_aceptar.TabIndex = 3
        Me.cmd_aceptar.Text = "&Aceptar"
        Me.cmd_aceptar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '

        Me.PictureBox1.Location = New System.Drawing.Point(70, 100)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(339, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Opciones de Configuración del Inventario"
        '
        'frm_opciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 211)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grd_comandos)
        Me.Controls.Add(Me.grp_opciones)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_opciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Opciones de Inventario"
        Me.grp_opciones.ResumeLayout(False)
        Me.grp_opciones.PerformLayout()
        Me.grd_comandos.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grp_opciones As System.Windows.Forms.GroupBox
    Friend WithEvents chk_pesables As System.Windows.Forms.CheckBox
    Friend WithEvents chk_decimales As System.Windows.Forms.CheckBox
    Friend WithEvents txt_cantidad_maxima_conteo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grd_comandos As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmd_aceptar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
