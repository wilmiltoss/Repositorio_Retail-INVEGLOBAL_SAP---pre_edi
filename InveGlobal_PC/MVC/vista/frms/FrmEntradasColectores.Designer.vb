<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEntradasColectores
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEntradasColectores))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.mnu_lecturas = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_tomar_datos = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_descartar_datos = New System.Windows.Forms.ToolStripMenuItem
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem
        Me.grd_lecturas = New System.Windows.Forms.DataGridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmb_campos = New System.Windows.Forms.ComboBox
        Me.txt_valor = New System.Windows.Forms.TextBox
        Me.chkModoPrueba = New System.Windows.Forms.CheckBox
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.grd_lecturas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_estado})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 510)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(690, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_estado
        '
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(0, 17)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(196, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Lecturas Realizadas"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_lecturas})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(690, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnu_lecturas
        '
        Me.mnu_lecturas.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_tomar_datos, Me.mnu_descartar_datos, Me.mnu_salir})
        Me.mnu_lecturas.Name = "mnu_lecturas"
        Me.mnu_lecturas.Size = New System.Drawing.Size(63, 20)
        Me.mnu_lecturas.Text = "&Lecturas"
        '
        'mnu_tomar_datos
        '
        Me.mnu_tomar_datos.Name = "mnu_tomar_datos"
        Me.mnu_tomar_datos.Size = New System.Drawing.Size(156, 22)
        Me.mnu_tomar_datos.Text = "&Tomar Datos"
        '
        'mnu_descartar_datos
        '
        Me.mnu_descartar_datos.Name = "mnu_descartar_datos"
        Me.mnu_descartar_datos.Size = New System.Drawing.Size(156, 22)
        Me.mnu_descartar_datos.Text = "&Descartar Datos"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(156, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'grd_lecturas
        '
        Me.grd_lecturas.AllowUserToAddRows = False
        Me.grd_lecturas.AllowUserToDeleteRows = False
        Me.grd_lecturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_lecturas.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grd_lecturas.Location = New System.Drawing.Point(0, 76)
        Me.grd_lecturas.Name = "grd_lecturas"
        Me.grd_lecturas.ReadOnly = True
        Me.grd_lecturas.Size = New System.Drawing.Size(690, 434)
        Me.grd_lecturas.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(77, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Buscar :"
        '
        'cmb_campos
        '
        Me.cmb_campos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_campos.FormattingEnabled = True
        Me.cmb_campos.Location = New System.Drawing.Point(129, 52)
        Me.cmb_campos.Name = "cmb_campos"
        Me.cmb_campos.Size = New System.Drawing.Size(165, 21)
        Me.cmb_campos.TabIndex = 5
        '
        'txt_valor
        '
        Me.txt_valor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_valor.Location = New System.Drawing.Point(300, 52)
        Me.txt_valor.MaxLength = 20
        Me.txt_valor.Name = "txt_valor"
        Me.txt_valor.Size = New System.Drawing.Size(135, 21)
        Me.txt_valor.TabIndex = 6
        '
        'chkModoPrueba
        '
        Me.chkModoPrueba.AutoSize = True
        Me.chkModoPrueba.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkModoPrueba.Location = New System.Drawing.Point(534, 54)
        Me.chkModoPrueba.Name = "chkModoPrueba"
        Me.chkModoPrueba.Size = New System.Drawing.Size(144, 20)
        Me.chkModoPrueba.TabIndex = 7
        Me.chkModoPrueba.Text = "Datos de Prueba"
        Me.chkModoPrueba.UseVisualStyleBackColor = True
        '
        'FrmEntradasColectores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 532)
        Me.Controls.Add(Me.chkModoPrueba)
        Me.Controls.Add(Me.txt_valor)
        Me.Controls.Add(Me.cmb_campos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grd_lecturas)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(698, 560)
        Me.Name = "FrmEntradasColectores"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Entrada Lecturas Realizadas"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.grd_lecturas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnu_lecturas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_tomar_datos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_descartar_datos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grd_lecturas As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_campos As System.Windows.Forms.ComboBox
    Friend WithEvents txt_valor As System.Windows.Forms.TextBox
    Friend WithEvents chkModoPrueba As System.Windows.Forms.CheckBox
End Class
