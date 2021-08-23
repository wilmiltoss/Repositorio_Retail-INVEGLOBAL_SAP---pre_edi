<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_despliega_existencias
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_despliega_existencias))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel
        Me.pbr_estado = New System.Windows.Forms.ToolStripProgressBar
        Me.tab_padre = New System.Windows.Forms.TabControl
        Me.tab_resumen = New System.Windows.Forms.TabPage
        Me.cmd_salir = New System.Windows.Forms.Button
        Me.tvw_arbol = New System.Windows.Forms.TreeView
        Me.cmd_aplicar_conteo = New System.Windows.Forms.Button
        Me.cmd_excel = New System.Windows.Forms.Button
        Me.txt_desde = New System.Windows.Forms.TextBox
        Me.txt_hasta = New System.Windows.Forms.TextBox
        Me.cmd_filtrar = New System.Windows.Forms.Button
        Me.cmb_comparaciones = New System.Windows.Forms.ComboBox
        Me.txt_valor_buscado = New System.Windows.Forms.TextBox
        Me.cmb_campos = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grp_conteos = New System.Windows.Forms.GroupBox
        Me.opt_conteo_3 = New System.Windows.Forms.RadioButton
        Me.opt_conteo_2 = New System.Windows.Forms.RadioButton
        Me.opt_conteo_1 = New System.Windows.Forms.RadioButton
        Me.grd_existencias = New System.Windows.Forms.DataGridView
        Me.lbl_Y = New System.Windows.Forms.Label
        Me.tab_detalles = New System.Windows.Forms.TabPage
        Me.Label2 = New System.Windows.Forms.Label
        Me.grd_detalles = New System.Windows.Forms.DataGridView
        Me.tmr_arbol = New System.Windows.Forms.Timer(Me.components)
        Me.bar_menu = New System.Windows.Forms.MenuStrip
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        'Me.mnu_exportar_a_excel = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.tab_padre.SuspendLayout()
        Me.tab_resumen.SuspendLayout()
        Me.grp_conteos.SuspendLayout()
        CType(Me.grd_existencias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab_detalles.SuspendLayout()
        CType(Me.grd_detalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bar_menu.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 564)
        Me.StatusStrip1.MinimumSize = New System.Drawing.Size(922, 22)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(922, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_estado
        '
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(70, 17)
        Me.lbl_estado.Text = "Esperando..."
        '
        'pbr_estado
        '
        Me.pbr_estado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pbr_estado.Name = "pbr_estado"
        Me.pbr_estado.Size = New System.Drawing.Size(100, 16)
        Me.pbr_estado.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.mnu_archivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_exportar_excel, Me.mnu_aplicar_conteo, Me.mnu_salir})
        Me.mnu_archivo.Name = "mnu_archivo"
        Me.mnu_archivo.Size = New System.Drawing.Size(55, 20)
        Me.mnu_archivo.Text = "&Archivo"
        '
        'mnu_exportar_excel
        '
        Me.mnu_exportar_excel.Name = "mnu_exportar_excel"
        Me.mnu_exportar_excel.Size = New System.Drawing.Size(164, 22)
        Me.mnu_exportar_excel.Text = "&Exportar a Excel"
        '
        'mnu_aplicar_conteo
        '
        Me.mnu_aplicar_conteo.Name = "mnu_aplicar_conteo"
        Me.mnu_aplicar_conteo.Size = New System.Drawing.Size(164, 22)
        Me.mnu_aplicar_conteo.Text = "Aplicar &Conteo"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(164, 22)
        Me.mnu_salir.Text = "&Salir"
        '
        'mnu_ajustes
        '
        Me.mnu_ajustes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_ejecutar_ajustes})
        Me.mnu_ajustes.Name = "mnu_ajustes"
        Me.mnu_ajustes.Size = New System.Drawing.Size(55, 20)
        Me.mnu_ajustes.Text = "Aj&ustes"
        '
        'mnu_ejecutar_ajustes
        '
        Me.mnu_ejecutar_ajustes.Name = "mnu_ejecutar_ajustes"
        Me.mnu_ejecutar_ajustes.Size = New System.Drawing.Size(164, 22)
        Me.mnu_ejecutar_ajustes.Text = "Ejecu&tar Ajustes"
        '
        'mnu_reporte
        '
        Me.mnu_reporte.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_desplegar_reporte})
        Me.mnu_reporte.Name = "mnu_reporte"
        Me.mnu_reporte.Size = New System.Drawing.Size(63, 20)
        Me.mnu_reporte.Text = "&Reportes"
        '
        'mnu_desplegar_reporte
        '
        Me.mnu_desplegar_reporte.Name = "mnu_desplegar_reporte"
        Me.mnu_desplegar_reporte.Size = New System.Drawing.Size(175, 22)
        Me.mnu_desplegar_reporte.Text = "Desple&gar Reporte"
        '
        'tab_padre
        '
        Me.tab_padre.Controls.Add(Me.tab_resumen)
        Me.tab_padre.Controls.Add(Me.tab_detalles)
        Me.tab_padre.Location = New System.Drawing.Point(0, 43)
        Me.tab_padre.Name = "tab_padre"
        Me.tab_padre.SelectedIndex = 0
        Me.tab_padre.Size = New System.Drawing.Size(922, 518)
        Me.tab_padre.TabIndex = 1
        '
        'tab_resumen
        '
        Me.tab_resumen.Controls.Add(Me.cmd_salir)
        Me.tab_resumen.Controls.Add(Me.tvw_arbol)
        Me.tab_resumen.Controls.Add(Me.cmd_aplicar_conteo)
        Me.tab_resumen.Controls.Add(Me.cmd_excel)
        Me.tab_resumen.Controls.Add(Me.txt_desde)
        Me.tab_resumen.Controls.Add(Me.txt_hasta)
        Me.tab_resumen.Controls.Add(Me.cmd_filtrar)
        Me.tab_resumen.Controls.Add(Me.cmb_comparaciones)
        Me.tab_resumen.Controls.Add(Me.txt_valor_buscado)
        Me.tab_resumen.Controls.Add(Me.cmb_campos)
        Me.tab_resumen.Controls.Add(Me.Label1)
        Me.tab_resumen.Controls.Add(Me.grp_conteos)
        Me.tab_resumen.Controls.Add(Me.grd_existencias)
        Me.tab_resumen.Controls.Add(Me.lbl_Y)
        Me.tab_resumen.Location = New System.Drawing.Point(4, 22)
        Me.tab_resumen.Name = "tab_resumen"
        Me.tab_resumen.Padding = New System.Windows.Forms.Padding(3)
        Me.tab_resumen.Size = New System.Drawing.Size(914, 492)
        Me.tab_resumen.TabIndex = 0
        Me.tab_resumen.Text = "Resumen Inventario"
        Me.tab_resumen.UseVisualStyleBackColor = True
        '
        'cmd_salir
        '
        Me.cmd_salir.Location = New System.Drawing.Point(710, 450)
        Me.cmd_salir.Name = "cmd_salir"
        Me.cmd_salir.Size = New System.Drawing.Size(130, 30)
        Me.cmd_salir.TabIndex = 16
        Me.cmd_salir.Text = "&Salir"
        Me.cmd_salir.UseVisualStyleBackColor = True
        '
        'tvw_arbol
        '
        Me.tvw_arbol.Location = New System.Drawing.Point(8, 65)
        Me.tvw_arbol.Name = "tvw_arbol"
        Me.tvw_arbol.Size = New System.Drawing.Size(1, 384)
        Me.tvw_arbol.TabIndex = 15
        '
        'cmd_aplicar_conteo
        '
        Me.cmd_aplicar_conteo.Location = New System.Drawing.Point(571, 450)
        Me.cmd_aplicar_conteo.Name = "cmd_aplicar_conteo"
        Me.cmd_aplicar_conteo.Size = New System.Drawing.Size(126, 30)
        Me.cmd_aplicar_conteo.TabIndex = 14
        Me.cmd_aplicar_conteo.Text = "Aplicar &Conteo"
        Me.cmd_aplicar_conteo.UseVisualStyleBackColor = True
        '
        'cmd_excel
        '
        Me.cmd_excel.Image = Global.InveStock_PC_2.My.Resources.Resources.excel
        Me.cmd_excel.Location = New System.Drawing.Point(494, 450)
        Me.cmd_excel.Name = "cmd_excel"
        Me.cmd_excel.Size = New System.Drawing.Size(36, 36)
        Me.cmd_excel.TabIndex = 13
        Me.cmd_excel.UseVisualStyleBackColor = True
        '
        'txt_desde
        '
        Me.txt_desde.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_desde.Location = New System.Drawing.Point(302, 30)
        Me.txt_desde.MaxLength = 40
        Me.txt_desde.Name = "txt_desde"
        Me.txt_desde.Size = New System.Drawing.Size(107, 21)
        Me.txt_desde.TabIndex = 4
        Me.txt_desde.Visible = False
        '
        'txt_hasta
        '
        Me.txt_hasta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_hasta.Location = New System.Drawing.Point(440, 30)
        Me.txt_hasta.MaxLength = 40
        Me.txt_hasta.Name = "txt_hasta"
        Me.txt_hasta.Size = New System.Drawing.Size(107, 21)
        Me.txt_hasta.TabIndex = 5
        Me.txt_hasta.Visible = False
        '
        'cmd_filtrar
        '
        Me.cmd_filtrar.Image = Global.InveStock_PC_2.My.Resources.Resources.buscar
        Me.cmd_filtrar.Location = New System.Drawing.Point(549, 12)
        Me.cmd_filtrar.Name = "cmd_filtrar"
        Me.cmd_filtrar.Size = New System.Drawing.Size(41, 40)
        Me.cmd_filtrar.TabIndex = 7
        Me.cmd_filtrar.UseVisualStyleBackColor = True
        Me.cmd_filtrar.Visible = False
        '
        'cmb_comparaciones
        '
        Me.cmb_comparaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_comparaciones.FormattingEnabled = True
        Me.cmb_comparaciones.Items.AddRange(New Object() {"es igual a", "no es igual a", "esta entre", "no esta entre", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que"})
        Me.cmb_comparaciones.Location = New System.Drawing.Point(197, 30)
        Me.cmb_comparaciones.Name = "cmb_comparaciones"
        Me.cmb_comparaciones.Size = New System.Drawing.Size(99, 21)
        Me.cmb_comparaciones.TabIndex = 3
        Me.cmb_comparaciones.Visible = False
        '
        'txt_valor_buscado
        '
        Me.txt_valor_buscado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_valor_buscado.Location = New System.Drawing.Point(302, 30)
        Me.txt_valor_buscado.MaxLength = 40
        Me.txt_valor_buscado.Name = "txt_valor_buscado"
        Me.txt_valor_buscado.Size = New System.Drawing.Size(244, 21)
        Me.txt_valor_buscado.TabIndex = 6
        Me.txt_valor_buscado.Visible = False
        '
        'cmb_campos
        '
        Me.cmb_campos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_campos.FormattingEnabled = True
        Me.cmb_campos.Location = New System.Drawing.Point(197, 6)
        Me.cmb_campos.Name = "cmb_campos"
        Me.cmb_campos.Size = New System.Drawing.Size(157, 21)
        Me.cmb_campos.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(131, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Filtrar por : "
        '
        'grp_conteos
        '
        Me.grp_conteos.Controls.Add(Me.opt_conteo_3)
        Me.grp_conteos.Controls.Add(Me.opt_conteo_2)
        Me.grp_conteos.Controls.Add(Me.opt_conteo_1)
        Me.grp_conteos.Location = New System.Drawing.Point(118, 452)
        Me.grp_conteos.Name = "grp_conteos"
        Me.grp_conteos.Size = New System.Drawing.Size(323, 34)
        Me.grp_conteos.TabIndex = 9
        Me.grp_conteos.TabStop = False
        Me.grp_conteos.Text = "Conteo a Validar"
        '
        'opt_conteo_3
        '
        Me.opt_conteo_3.AutoSize = True
        Me.opt_conteo_3.Location = New System.Drawing.Point(242, 12)
        Me.opt_conteo_3.Name = "opt_conteo_3"
        Me.opt_conteo_3.Size = New System.Drawing.Size(68, 17)
        Me.opt_conteo_3.TabIndex = 12
        Me.opt_conteo_3.Text = "Conteo &3"
        Me.opt_conteo_3.UseVisualStyleBackColor = True
        '
        'opt_conteo_2
        '
        Me.opt_conteo_2.AutoSize = True
        Me.opt_conteo_2.Location = New System.Drawing.Point(168, 12)
        Me.opt_conteo_2.Name = "opt_conteo_2"
        Me.opt_conteo_2.Size = New System.Drawing.Size(68, 17)
        Me.opt_conteo_2.TabIndex = 11
        Me.opt_conteo_2.Text = "Conteo &2"
        Me.opt_conteo_2.UseVisualStyleBackColor = True
        '
        'opt_conteo_1
        '
        Me.opt_conteo_1.AutoSize = True
        Me.opt_conteo_1.Location = New System.Drawing.Point(94, 12)
        Me.opt_conteo_1.Name = "opt_conteo_1"
        Me.opt_conteo_1.Size = New System.Drawing.Size(68, 17)
        Me.opt_conteo_1.TabIndex = 10
        Me.opt_conteo_1.Text = "Conteo &1"
        Me.opt_conteo_1.UseVisualStyleBackColor = True
        '
        'grd_existencias
        '
        Me.grd_existencias.AllowUserToAddRows = False
        Me.grd_existencias.AllowUserToDeleteRows = False
        Me.grd_existencias.Location = New System.Drawing.Point(8, 64)
        Me.grd_existencias.Name = "grd_existencias"
        Me.grd_existencias.ReadOnly = True
        Me.grd_existencias.Size = New System.Drawing.Size(903, 384)
        Me.grd_existencias.TabIndex = 8
        '
        'lbl_Y
        '
        Me.lbl_Y.AutoSize = True
        Me.lbl_Y.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Y.Location = New System.Drawing.Point(413, 33)
        Me.lbl_Y.Name = "lbl_Y"
        Me.lbl_Y.Size = New System.Drawing.Size(23, 15)
        Me.lbl_Y.TabIndex = 12
        Me.lbl_Y.Text = "Y : "
        Me.lbl_Y.Visible = False
        '
        'tab_detalles
        '
        Me.tab_detalles.Controls.Add(Me.Label2)
        Me.tab_detalles.Controls.Add(Me.grd_detalles)
        Me.tab_detalles.Location = New System.Drawing.Point(4, 22)
        Me.tab_detalles.Name = "tab_detalles"
        Me.tab_detalles.Padding = New System.Windows.Forms.Padding(3)
        Me.tab_detalles.Size = New System.Drawing.Size(914, 492)
        Me.tab_detalles.TabIndex = 1
        Me.tab_detalles.Text = "Detalles de Conteos"
        Me.tab_detalles.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(345, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Detalles de Conteos de Articulo"
        '
        'grd_detalles
        '
        Me.grd_detalles.AllowUserToAddRows = False
        Me.grd_detalles.AllowUserToDeleteRows = False
        Me.grd_detalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd_detalles.Location = New System.Drawing.Point(8, 64)
        Me.grd_detalles.Name = "grd_detalles"
        Me.grd_detalles.ReadOnly = True
        Me.grd_detalles.Size = New System.Drawing.Size(903, 384)
        Me.grd_detalles.TabIndex = 16
        '
        'tmr_arbol
        '
        Me.tmr_arbol.Interval = 1
        '
        'bar_menu
        '
        Me.bar_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem5})
        Me.bar_menu.Location = New System.Drawing.Point(0, 0)
        Me.bar_menu.Name = "bar_menu"
        Me.bar_menu.Size = New System.Drawing.Size(922, 24)
        Me.bar_menu.TabIndex = 2
        Me.bar_menu.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_exportar_a_excel, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(55, 20)
        Me.ToolStripMenuItem1.Text = "&Archivo"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(164, 22)
        Me.ToolStripMenuItem3.Text = "Aplicar &Conteo"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(164, 22)
        Me.ToolStripMenuItem4.Text = "&Salir"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem6})
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(55, 20)
        Me.ToolStripMenuItem5.Text = "Aj&ustes"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(164, 22)
        Me.ToolStripMenuItem6.Text = "Ejecu&tar Ajustes"
        '
        'frm_despliega_existencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 586)
        Me.Controls.Add(Me.bar_menu)
        Me.Controls.Add(Me.tab_padre)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(910, 600)
        Me.Name = "frm_despliega_existencias"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resumen de Inventario"
        Me.tab_padre.ResumeLayout(False)
        Me.tab_resumen.ResumeLayout(False)
        Me.tab_resumen.PerformLayout()
        Me.grp_conteos.ResumeLayout(False)
        Me.grp_conteos.PerformLayout()
        CType(Me.grd_existencias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab_detalles.ResumeLayout(False)
        Me.tab_detalles.PerformLayout()
        CType(Me.grd_detalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bar_menu.ResumeLayout(False)
        Me.bar_menu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tab_padre As System.Windows.Forms.TabControl
    Friend WithEvents tab_resumen As System.Windows.Forms.TabPage
    Friend WithEvents grp_conteos As System.Windows.Forms.GroupBox
    Friend WithEvents opt_conteo_1 As System.Windows.Forms.RadioButton
    Friend WithEvents grd_existencias As System.Windows.Forms.DataGridView
    Friend WithEvents tab_detalles As System.Windows.Forms.TabPage
    Friend WithEvents opt_conteo_3 As System.Windows.Forms.RadioButton
    Friend WithEvents opt_conteo_2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_campos As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_comparaciones As System.Windows.Forms.ComboBox
    Friend WithEvents txt_valor_buscado As System.Windows.Forms.TextBox
    Friend WithEvents cmd_filtrar As System.Windows.Forms.Button
    Friend WithEvents lbl_Y As System.Windows.Forms.Label
    Friend WithEvents txt_hasta As System.Windows.Forms.TextBox
    Friend WithEvents txt_desde As System.Windows.Forms.TextBox
    Friend WithEvents mnu_archivo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_exportar_excel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_excel As System.Windows.Forms.Button
    Friend WithEvents cmd_aplicar_conteo As System.Windows.Forms.Button
    Friend WithEvents grd_detalles As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mnu_aplicar_conteo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ajustes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ejecutar_ajustes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbr_estado As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tvw_arbol As System.Windows.Forms.TreeView
    Friend WithEvents tmr_arbol As System.Windows.Forms.Timer
    Friend WithEvents mnu_reporte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_desplegar_reporte As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_salir As System.Windows.Forms.Button
    Friend WithEvents bar_menu As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_exportar_a_excel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
End Class
