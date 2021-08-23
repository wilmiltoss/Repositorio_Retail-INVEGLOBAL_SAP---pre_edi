<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_abm_soportes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_abm_soportes))
        Me.grp_datos = New System.Windows.Forms.GroupBox
        Me.chk_subdivisible = New System.Windows.Forms.CheckBox
        Me.txt_descripcion = New System.Windows.Forms.TextBox
        Me.txt_id = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grd_soportes = New System.Windows.Forms.DataGridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmd_agregar = New System.Windows.Forms.Button
        Me.cmd_cancelar = New System.Windows.Forms.Button
        Me.cmd_guardar = New System.Windows.Forms.Button
        Me.cmd_modificar = New System.Windows.Forms.Button
        Me.cmd_salir = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.grp_datos.SuspendLayout()
        CType(Me.grd_soportes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grp_datos
        '
        Me.grp_datos.Controls.Add(Me.chk_subdivisible)
        Me.grp_datos.Controls.Add(Me.txt_descripcion)
        Me.grp_datos.Controls.Add(Me.txt_id)
        Me.grp_datos.Controls.Add(Me.Label4)
        Me.grp_datos.Controls.Add(Me.Label3)
        Me.grp_datos.Location = New System.Drawing.Point(10, 70)
        Me.grp_datos.Name = "grp_datos"
        Me.grp_datos.Size = New System.Drawing.Size(380, 130)
        Me.grp_datos.TabIndex = 0
        Me.grp_datos.TabStop = False
        Me.grp_datos.Text = "Datos de Soporte"
        '
        'chk_subdivisible
        '
        Me.chk_subdivisible.AutoSize = True
        Me.chk_subdivisible.Enabled = False
        Me.chk_subdivisible.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_subdivisible.Location = New System.Drawing.Point(100, 90)
        Me.chk_subdivisible.Name = "chk_subdivisible"
        Me.chk_subdivisible.Size = New System.Drawing.Size(141, 19)
        Me.chk_subdivisible.TabIndex = 5
        Me.chk_subdivisible.Text = "Se puede Sub Dividir"
        Me.chk_subdivisible.UseVisualStyleBackColor = True
        '
        'txt_descripcion
        '
        Me.txt_descripcion.Enabled = False
        Me.txt_descripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_descripcion.Location = New System.Drawing.Point(90, 60)
        Me.txt_descripcion.MaxLength = 50
        Me.txt_descripcion.Name = "txt_descripcion"
        Me.txt_descripcion.Size = New System.Drawing.Size(270, 21)
        Me.txt_descripcion.TabIndex = 4
        '
        'txt_id
        '
        Me.txt_id.Enabled = False
        Me.txt_id.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_id.Location = New System.Drawing.Point(90, 30)
        Me.txt_id.Name = "txt_id"
        Me.txt_id.Size = New System.Drawing.Size(90, 21)
        Me.txt_id.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Descripcion : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "ID Soporte : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(308, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Mantenimiento de Datos de Soportes"
        '
        'grd_soportes
        '
        Me.grd_soportes.AllowUserToAddRows = False
        Me.grd_soportes.AllowUserToDeleteRows = False
        Me.grd_soportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_soportes.Location = New System.Drawing.Point(400, 30)
        Me.grd_soportes.Name = "grd_soportes"
        Me.grd_soportes.ReadOnly = True
        Me.grd_soportes.Size = New System.Drawing.Size(310, 250)
        Me.grd_soportes.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(400, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Todos los Soportes"
        '
        'cmd_agregar
        '
        Me.cmd_agregar.Location = New System.Drawing.Point(10, 210)
        Me.cmd_agregar.Name = "cmd_agregar"
        Me.cmd_agregar.Size = New System.Drawing.Size(100, 30)
        Me.cmd_agregar.TabIndex = 4
        Me.cmd_agregar.Text = "&Agregar"
        Me.cmd_agregar.UseVisualStyleBackColor = True
        '
        'cmd_cancelar
        '
        Me.cmd_cancelar.Enabled = False
        Me.cmd_cancelar.Location = New System.Drawing.Point(210, 210)
        Me.cmd_cancelar.Name = "cmd_cancelar"
        Me.cmd_cancelar.Size = New System.Drawing.Size(90, 30)
        Me.cmd_cancelar.TabIndex = 4
        Me.cmd_cancelar.Text = "&Cancelar"
        Me.cmd_cancelar.UseVisualStyleBackColor = True
        '
        'cmd_guardar
        '
        Me.cmd_guardar.Enabled = False
        Me.cmd_guardar.Location = New System.Drawing.Point(110, 210)
        Me.cmd_guardar.Name = "cmd_guardar"
        Me.cmd_guardar.Size = New System.Drawing.Size(100, 30)
        Me.cmd_guardar.TabIndex = 4
        Me.cmd_guardar.Text = "&Guardar"
        Me.cmd_guardar.UseVisualStyleBackColor = True
        '
        'cmd_modificar
        '
        Me.cmd_modificar.Location = New System.Drawing.Point(300, 210)
        Me.cmd_modificar.Name = "cmd_modificar"
        Me.cmd_modificar.Size = New System.Drawing.Size(90, 30)
        Me.cmd_modificar.TabIndex = 4
        Me.cmd_modificar.Text = "&Modificar"
        Me.cmd_modificar.UseVisualStyleBackColor = True
        '
        'cmd_salir
        '
        Me.cmd_salir.Location = New System.Drawing.Point(150, 260)
        Me.cmd_salir.Name = "cmd_salir"
        Me.cmd_salir.Size = New System.Drawing.Size(120, 30)
        Me.cmd_salir.TabIndex = 5
        Me.cmd_salir.Text = "&Salir"
        Me.cmd_salir.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '

        Me.PictureBox1.Location = New System.Drawing.Point(320, 20)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(70, 70)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'frm_abm_soportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(714, 296)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmd_salir)
        Me.Controls.Add(Me.cmd_modificar)
        Me.Controls.Add(Me.cmd_guardar)
        Me.Controls.Add(Me.cmd_cancelar)
        Me.Controls.Add(Me.cmd_agregar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grd_soportes)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grp_datos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_abm_soportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de Soportes"
        Me.grp_datos.ResumeLayout(False)
        Me.grp_datos.PerformLayout()
        CType(Me.grd_soportes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grp_datos As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grd_soportes As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_agregar As System.Windows.Forms.Button
    Friend WithEvents cmd_cancelar As System.Windows.Forms.Button
    Friend WithEvents cmd_guardar As System.Windows.Forms.Button
    Friend WithEvents cmd_modificar As System.Windows.Forms.Button
    Friend WithEvents txt_descripcion As System.Windows.Forms.TextBox
    Friend WithEvents txt_id As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chk_subdivisible As System.Windows.Forms.CheckBox
    Friend WithEvents cmd_salir As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
