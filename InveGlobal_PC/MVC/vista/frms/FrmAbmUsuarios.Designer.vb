<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAbmUsuarios
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAbmUsuarios))
        Me.pnl_izquierdo = New System.Windows.Forms.Panel
        Me.grd_usuarios = New System.Windows.Forms.DataGridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnl_inferior = New System.Windows.Forms.Panel
        Me.cmd_salir = New System.Windows.Forms.Button
        Me.cmd_modificar = New System.Windows.Forms.Button
        Me.cmd_guardar = New System.Windows.Forms.Button
        Me.cmd_cancelar = New System.Windows.Forms.Button
        Me.cmd_agregar = New System.Windows.Forms.Button
        Me.pnl_superior = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_id = New System.Windows.Forms.TextBox
        Me.txt_nombre_usuario = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txt_contrasena = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt_confirmar = New System.Windows.Forms.TextBox
        Me.chk_habilitado = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmb_niveles = New System.Windows.Forms.ComboBox
        Me.grp_datos = New System.Windows.Forms.GroupBox
        Me.btn_quitar_local = New System.Windows.Forms.Button
        Me.btn_agregar_local = New System.Windows.Forms.Button
        Me.lst_locales = New System.Windows.Forms.ListBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnl_izquierdo.SuspendLayout()
        CType(Me.grd_usuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_inferior.SuspendLayout()
        Me.pnl_superior.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp_datos.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_izquierdo
        '
        Me.pnl_izquierdo.Controls.Add(Me.grd_usuarios)
        Me.pnl_izquierdo.Controls.Add(Me.Label2)
        Me.pnl_izquierdo.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_izquierdo.Location = New System.Drawing.Point(400, 0)
        Me.pnl_izquierdo.Name = "pnl_izquierdo"
        Me.pnl_izquierdo.Size = New System.Drawing.Size(324, 466)
        Me.pnl_izquierdo.TabIndex = 17
        '
        'grd_usuarios
        '
        Me.grd_usuarios.AllowUserToAddRows = False
        Me.grd_usuarios.AllowUserToDeleteRows = False
        Me.grd_usuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_usuarios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd_usuarios.Location = New System.Drawing.Point(0, 16)
        Me.grd_usuarios.Name = "grd_usuarios"
        Me.grd_usuarios.ReadOnly = True
        Me.grd_usuarios.Size = New System.Drawing.Size(324, 450)
        Me.grd_usuarios.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Todos los Usuarios"
        '
        'pnl_inferior
        '
        Me.pnl_inferior.Controls.Add(Me.cmd_salir)
        Me.pnl_inferior.Controls.Add(Me.cmd_modificar)
        Me.pnl_inferior.Controls.Add(Me.cmd_guardar)
        Me.pnl_inferior.Controls.Add(Me.cmd_cancelar)
        Me.pnl_inferior.Controls.Add(Me.cmd_agregar)
        Me.pnl_inferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_inferior.Location = New System.Drawing.Point(0, 384)
        Me.pnl_inferior.Name = "pnl_inferior"
        Me.pnl_inferior.Size = New System.Drawing.Size(400, 82)
        Me.pnl_inferior.TabIndex = 18
        '
        'cmd_salir
        '
        Me.cmd_salir.Location = New System.Drawing.Point(152, 42)
        Me.cmd_salir.Name = "cmd_salir"
        Me.cmd_salir.Size = New System.Drawing.Size(120, 30)
        Me.cmd_salir.TabIndex = 16
        Me.cmd_salir.Text = "&Salir"
        Me.cmd_salir.UseVisualStyleBackColor = True
        '
        'cmd_modificar
        '
        Me.cmd_modificar.Location = New System.Drawing.Point(299, 6)
        Me.cmd_modificar.Name = "cmd_modificar"
        Me.cmd_modificar.Size = New System.Drawing.Size(90, 30)
        Me.cmd_modificar.TabIndex = 15
        Me.cmd_modificar.Text = "&Modificar"
        Me.cmd_modificar.UseVisualStyleBackColor = True
        '
        'cmd_guardar
        '
        Me.cmd_guardar.Enabled = False
        Me.cmd_guardar.Location = New System.Drawing.Point(109, 6)
        Me.cmd_guardar.Name = "cmd_guardar"
        Me.cmd_guardar.Size = New System.Drawing.Size(100, 30)
        Me.cmd_guardar.TabIndex = 13
        Me.cmd_guardar.Text = "&Guardar"
        Me.cmd_guardar.UseVisualStyleBackColor = True
        '
        'cmd_cancelar
        '
        Me.cmd_cancelar.Enabled = False
        Me.cmd_cancelar.Location = New System.Drawing.Point(209, 6)
        Me.cmd_cancelar.Name = "cmd_cancelar"
        Me.cmd_cancelar.Size = New System.Drawing.Size(90, 30)
        Me.cmd_cancelar.TabIndex = 14
        Me.cmd_cancelar.Text = "&Cancelar"
        Me.cmd_cancelar.UseVisualStyleBackColor = True
        '
        'cmd_agregar
        '
        Me.cmd_agregar.Location = New System.Drawing.Point(9, 6)
        Me.cmd_agregar.Name = "cmd_agregar"
        Me.cmd_agregar.Size = New System.Drawing.Size(100, 30)
        Me.cmd_agregar.TabIndex = 12
        Me.cmd_agregar.Text = "&Agregar"
        Me.cmd_agregar.UseVisualStyleBackColor = True
        '
        'pnl_superior
        '
        Me.pnl_superior.Controls.Add(Me.PictureBox1)
        Me.pnl_superior.Controls.Add(Me.Label1)
        Me.pnl_superior.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_superior.Location = New System.Drawing.Point(0, 0)
        Me.pnl_superior.Name = "pnl_superior"
        Me.pnl_superior.Size = New System.Drawing.Size(400, 88)
        Me.pnl_superior.TabIndex = 19
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(320, 15)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(70, 70)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(306, 20)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Mantenimiento de Datos de Usuarios"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "ID Usuario: "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Nombre : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_id
        '
        Me.txt_id.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_id.Location = New System.Drawing.Point(90, 30)
        Me.txt_id.Name = "txt_id"
        Me.txt_id.Size = New System.Drawing.Size(90, 21)
        Me.txt_id.TabIndex = 0
        '
        'txt_nombre_usuario
        '
        Me.txt_nombre_usuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_nombre_usuario.Location = New System.Drawing.Point(90, 60)
        Me.txt_nombre_usuario.MaxLength = 50
        Me.txt_nombre_usuario.Name = "txt_nombre_usuario"
        Me.txt_nombre_usuario.Size = New System.Drawing.Size(270, 21)
        Me.txt_nombre_usuario.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 20)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Contraseña : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_contrasena
        '
        Me.txt_contrasena.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_contrasena.Location = New System.Drawing.Point(90, 87)
        Me.txt_contrasena.MaxLength = 50
        Me.txt_contrasena.Name = "txt_contrasena"
        Me.txt_contrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_contrasena.Size = New System.Drawing.Size(270, 21)
        Me.txt_contrasena.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Confirmar : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_confirmar
        '
        Me.txt_confirmar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_confirmar.Location = New System.Drawing.Point(90, 114)
        Me.txt_confirmar.MaxLength = 50
        Me.txt_confirmar.Name = "txt_confirmar"
        Me.txt_confirmar.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_confirmar.Size = New System.Drawing.Size(270, 21)
        Me.txt_confirmar.TabIndex = 4
        '
        'chk_habilitado
        '
        Me.chk_habilitado.AutoSize = True
        Me.chk_habilitado.Enabled = False
        Me.chk_habilitado.Location = New System.Drawing.Point(90, 141)
        Me.chk_habilitado.Name = "chk_habilitado"
        Me.chk_habilitado.Size = New System.Drawing.Size(73, 17)
        Me.chk_habilitado.TabIndex = 5
        Me.chk_habilitado.Text = "Habilitado"
        Me.chk_habilitado.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(186, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Nivel: "
        '
        'cmb_niveles
        '
        Me.cmb_niveles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_niveles.Enabled = False
        Me.cmb_niveles.FormattingEnabled = True
        Me.cmb_niveles.Location = New System.Drawing.Point(222, 29)
        Me.cmb_niveles.Name = "cmb_niveles"
        Me.cmb_niveles.Size = New System.Drawing.Size(138, 21)
        Me.cmb_niveles.TabIndex = 1
        '
        'grp_datos
        '
        Me.grp_datos.Controls.Add(Me.btn_quitar_local)
        Me.grp_datos.Controls.Add(Me.btn_agregar_local)
        Me.grp_datos.Controls.Add(Me.lst_locales)
        Me.grp_datos.Controls.Add(Me.Label8)
        Me.grp_datos.Controls.Add(Me.cmb_niveles)
        Me.grp_datos.Controls.Add(Me.Label7)
        Me.grp_datos.Controls.Add(Me.chk_habilitado)
        Me.grp_datos.Controls.Add(Me.txt_confirmar)
        Me.grp_datos.Controls.Add(Me.Label6)
        Me.grp_datos.Controls.Add(Me.txt_contrasena)
        Me.grp_datos.Controls.Add(Me.Label5)
        Me.grp_datos.Controls.Add(Me.txt_nombre_usuario)
        Me.grp_datos.Controls.Add(Me.txt_id)
        Me.grp_datos.Controls.Add(Me.Label4)
        Me.grp_datos.Controls.Add(Me.Label3)
        Me.grp_datos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grp_datos.Location = New System.Drawing.Point(0, 88)
        Me.grp_datos.Name = "grp_datos"
        Me.grp_datos.Size = New System.Drawing.Size(400, 296)
        Me.grp_datos.TabIndex = 7
        Me.grp_datos.TabStop = False
        Me.grp_datos.Text = "Datos de Usuarios"
        '
        'btn_quitar_local
        '
        Me.btn_quitar_local.Image = Global.InveStock_PC_2.My.Resources.Resources.eliminar
        Me.btn_quitar_local.Location = New System.Drawing.Point(51, 215)
        Me.btn_quitar_local.Name = "btn_quitar_local"
        Me.btn_quitar_local.Size = New System.Drawing.Size(35, 30)
        Me.btn_quitar_local.TabIndex = 15
        Me.btn_quitar_local.UseVisualStyleBackColor = True
        '
        'btn_agregar_local
        '
        Me.btn_agregar_local.Image = Global.InveStock_PC_2.My.Resources.Resources.agregar
        Me.btn_agregar_local.Location = New System.Drawing.Point(51, 184)
        Me.btn_agregar_local.Name = "btn_agregar_local"
        Me.btn_agregar_local.Size = New System.Drawing.Size(35, 30)
        Me.btn_agregar_local.TabIndex = 14
        Me.btn_agregar_local.UseVisualStyleBackColor = True
        '
        'lst_locales
        '
        Me.lst_locales.FormattingEnabled = True
        Me.lst_locales.Location = New System.Drawing.Point(90, 165)
        Me.lst_locales.Name = "lst_locales"
        Me.lst_locales.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lst_locales.Size = New System.Drawing.Size(270, 108)
        Me.lst_locales.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 20)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Locales : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAbmUsuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(724, 466)
        Me.ControlBox = False
        Me.Controls.Add(Me.grp_datos)
        Me.Controls.Add(Me.pnl_superior)
        Me.Controls.Add(Me.pnl_inferior)
        Me.Controls.Add(Me.pnl_izquierdo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAbmUsuarios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de Usuarios"
        Me.pnl_izquierdo.ResumeLayout(False)
        Me.pnl_izquierdo.PerformLayout()
        CType(Me.grd_usuarios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_inferior.ResumeLayout(False)
        Me.pnl_superior.ResumeLayout(False)
        Me.pnl_superior.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp_datos.ResumeLayout(False)
        Me.grp_datos.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_izquierdo As System.Windows.Forms.Panel
    Friend WithEvents grd_usuarios As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnl_inferior As System.Windows.Forms.Panel
    Friend WithEvents cmd_salir As System.Windows.Forms.Button
    Friend WithEvents cmd_modificar As System.Windows.Forms.Button
    Friend WithEvents cmd_guardar As System.Windows.Forms.Button
    Friend WithEvents cmd_cancelar As System.Windows.Forms.Button
    Friend WithEvents cmd_agregar As System.Windows.Forms.Button
    Friend WithEvents pnl_superior As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_id As System.Windows.Forms.TextBox
    Friend WithEvents txt_nombre_usuario As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_contrasena As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_confirmar As System.Windows.Forms.TextBox
    Friend WithEvents chk_habilitado As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmb_niveles As System.Windows.Forms.ComboBox
    Friend WithEvents grp_datos As System.Windows.Forms.GroupBox
    Friend WithEvents lst_locales As System.Windows.Forms.ListBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btn_quitar_local As System.Windows.Forms.Button
    Friend WithEvents btn_agregar_local As System.Windows.Forms.Button
End Class
