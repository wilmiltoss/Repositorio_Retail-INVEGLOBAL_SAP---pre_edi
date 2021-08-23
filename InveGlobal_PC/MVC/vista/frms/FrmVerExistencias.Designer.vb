<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVerExistencias
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVerExistencias))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lbl_estado_2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbr_estado_2 = New System.Windows.Forms.ToolStripProgressBar()
        Me.lbl_porcentaje = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lbl_estado = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbr_estado = New System.Windows.Forms.ToolStripProgressBar()
        Me.mnu_archivo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_exportar_excel = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_aplicar_conteo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_salir = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ajustes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ejecutar_ajustes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_reporte = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_desplegar_reporte = New System.Windows.Forms.ToolStripMenuItem()
        Me.tab_padre = New System.Windows.Forms.TabControl()
        Me.tab_resumen = New System.Windows.Forms.TabPage()
        Me.grd_existencias = New System.Windows.Forms.DataGridView()
        Me.tvw_arbol = New System.Windows.Forms.TreeView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnl_ajustar = New System.Windows.Forms.Panel()
        Me.opt_falso = New System.Windows.Forms.RadioButton()
        Me.opt_verdadero = New System.Windows.Forms.RadioButton()
        Me.cmd_excel = New System.Windows.Forms.Button()
        Me.txt_desde = New System.Windows.Forms.TextBox()
        Me.txt_hasta = New System.Windows.Forms.TextBox()
        Me.cmd_filtrar = New System.Windows.Forms.Button()
        Me.cmb_comparaciones = New System.Windows.Forms.ComboBox()
        Me.txt_valor_buscado = New System.Windows.Forms.TextBox()
        Me.cmb_campos = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_Y = New System.Windows.Forms.Label()
        Me.tab_detalles = New System.Windows.Forms.TabPage()
        Me.grd_detalles = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tmr_arbol = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnu_archivo_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_exportar_excel_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_ibm_basto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_salir_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_desplegar_reporte_2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu_cubo_excel = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.StatusStrip1.SuspendLayout()
        Me.tab_padre.SuspendLayout()
        Me.tab_resumen.SuspendLayout()
        CType(Me.grd_existencias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnl_ajustar.SuspendLayout()
        Me.tab_detalles.SuspendLayout()
        CType(Me.grd_detalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_estado_2, Me.pbr_estado_2, Me.lbl_porcentaje})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 568)
        Me.StatusStrip1.MinimumSize = New System.Drawing.Size(922, 22)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(922, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_estado_2
        '
        Me.lbl_estado_2.Name = "lbl_estado_2"
        Me.lbl_estado_2.Size = New System.Drawing.Size(0, 17)
        '
        'pbr_estado_2
        '
        Me.pbr_estado_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pbr_estado_2.Name = "pbr_estado_2"
        Me.pbr_estado_2.Size = New System.Drawing.Size(100, 16)
        Me.pbr_estado_2.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'lbl_porcentaje
        '
        Me.lbl_porcentaje.Name = "lbl_porcentaje"
        Me.lbl_porcentaje.Size = New System.Drawing.Size(0, 17)
        '
        'lbl_estado
        '
        Me.lbl_estado.Name = "lbl_estado"
        Me.lbl_estado.Size = New System.Drawing.Size(23, 23)
        '
        'pbr_estado
        '
        Me.pbr_estado.Name = "pbr_estado"
        Me.pbr_estado.Size = New System.Drawing.Size(100, 15)
        '
        'mnu_archivo
        '
        Me.mnu_archivo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_exportar_excel, Me.mnu_aplicar_conteo, Me.mnu_salir})
        Me.mnu_archivo.Name = "mnu_archivo"
        Me.mnu_archivo.Size = New System.Drawing.Size(55, 20)
        Me.mnu_archivo.Text = "&Archivo"
        '
        'mnu_exportar_excel
        '
        Me.mnu_exportar_excel.Name = "mnu_exportar_excel"
        Me.mnu_exportar_excel.Size = New System.Drawing.Size(155, 22)
        Me.mnu_exportar_excel.Text = "&Exportar a Excel"
        '
        'mnu_aplicar_conteo
        '
        Me.mnu_aplicar_conteo.Name = "mnu_aplicar_conteo"
        Me.mnu_aplicar_conteo.Size = New System.Drawing.Size(155, 22)
        Me.mnu_aplicar_conteo.Text = "Aplicar &Conteo"
        '
        'mnu_salir
        '
        Me.mnu_salir.Name = "mnu_salir"
        Me.mnu_salir.Size = New System.Drawing.Size(155, 22)
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
        Me.mnu_ejecutar_ajustes.Size = New System.Drawing.Size(157, 22)
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
        Me.mnu_desplegar_reporte.Size = New System.Drawing.Size(170, 22)
        Me.mnu_desplegar_reporte.Text = "Desple&gar Reporte"
        '
        'tab_padre
        '
        Me.tab_padre.Controls.Add(Me.tab_resumen)
        Me.tab_padre.Controls.Add(Me.tab_detalles)
        Me.tab_padre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tab_padre.Location = New System.Drawing.Point(0, 24)
        Me.tab_padre.Name = "tab_padre"
        Me.tab_padre.SelectedIndex = 0
        Me.tab_padre.Size = New System.Drawing.Size(922, 544)
        Me.tab_padre.TabIndex = 1
        '
        'tab_resumen
        '
        Me.tab_resumen.Controls.Add(Me.grd_existencias)
        Me.tab_resumen.Controls.Add(Me.tvw_arbol)
        Me.tab_resumen.Controls.Add(Me.Panel2)
        Me.tab_resumen.Location = New System.Drawing.Point(4, 22)
        Me.tab_resumen.Name = "tab_resumen"
        Me.tab_resumen.Padding = New System.Windows.Forms.Padding(3)
        Me.tab_resumen.Size = New System.Drawing.Size(914, 518)
        Me.tab_resumen.TabIndex = 0
        Me.tab_resumen.Text = "Resumen Inventario"
        Me.tab_resumen.UseVisualStyleBackColor = True
        '
        'grd_existencias
        '
        Me.grd_existencias.AllowUserToAddRows = False
        Me.grd_existencias.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd_existencias.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grd_existencias.DefaultCellStyle = DataGridViewCellStyle2
        Me.grd_existencias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd_existencias.Location = New System.Drawing.Point(213, 76)
        Me.grd_existencias.Name = "grd_existencias"
        Me.grd_existencias.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd_existencias.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grd_existencias.Size = New System.Drawing.Size(698, 439)
        Me.grd_existencias.TabIndex = 8
        '
        'tvw_arbol
        '
        Me.tvw_arbol.Dock = System.Windows.Forms.DockStyle.Left
        Me.tvw_arbol.Location = New System.Drawing.Point(3, 76)
        Me.tvw_arbol.MaximumSize = New System.Drawing.Size(210, 4)
        Me.tvw_arbol.MinimumSize = New System.Drawing.Size(4, 512)
        Me.tvw_arbol.Name = "tvw_arbol"
        Me.tvw_arbol.Size = New System.Drawing.Size(210, 512)
        Me.tvw_arbol.TabIndex = 15
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnl_ajustar)
        Me.Panel2.Controls.Add(Me.cmd_excel)
        Me.Panel2.Controls.Add(Me.txt_desde)
        Me.Panel2.Controls.Add(Me.txt_hasta)
        Me.Panel2.Controls.Add(Me.cmd_filtrar)
        Me.Panel2.Controls.Add(Me.cmb_comparaciones)
        Me.Panel2.Controls.Add(Me.txt_valor_buscado)
        Me.Panel2.Controls.Add(Me.cmb_campos)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.lbl_Y)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(908, 73)
        Me.Panel2.TabIndex = 21
        '
        'pnl_ajustar
        '
        Me.pnl_ajustar.Controls.Add(Me.opt_falso)
        Me.pnl_ajustar.Controls.Add(Me.opt_verdadero)
        Me.pnl_ajustar.Location = New System.Drawing.Point(298, 9)
        Me.pnl_ajustar.Name = "pnl_ajustar"
        Me.pnl_ajustar.Size = New System.Drawing.Size(150, 30)
        Me.pnl_ajustar.TabIndex = 28
        Me.pnl_ajustar.Visible = False
        '
        'opt_falso
        '
        Me.opt_falso.AutoSize = True
        Me.opt_falso.Location = New System.Drawing.Point(90, 10)
        Me.opt_falso.Name = "opt_falso"
        Me.opt_falso.Size = New System.Drawing.Size(50, 17)
        Me.opt_falso.TabIndex = 1
        Me.opt_falso.TabStop = True
        Me.opt_falso.Text = "Falso"
        Me.opt_falso.UseVisualStyleBackColor = True
        '
        'opt_verdadero
        '
        Me.opt_verdadero.AutoSize = True
        Me.opt_verdadero.Location = New System.Drawing.Point(10, 10)
        Me.opt_verdadero.Name = "opt_verdadero"
        Me.opt_verdadero.Size = New System.Drawing.Size(74, 17)
        Me.opt_verdadero.TabIndex = 0
        Me.opt_verdadero.TabStop = True
        Me.opt_verdadero.Text = "Verdadero"
        Me.opt_verdadero.UseVisualStyleBackColor = True
        '
        'cmd_excel
        '
        Me.cmd_excel.Image = CType(resources.GetObject("cmd_excel.Image"), System.Drawing.Image)
        Me.cmd_excel.Location = New System.Drawing.Point(3, 31)
        Me.cmd_excel.Name = "cmd_excel"
        Me.cmd_excel.Size = New System.Drawing.Size(36, 36)
        Me.cmd_excel.TabIndex = 17
        Me.cmd_excel.UseVisualStyleBackColor = True
        '
        'txt_desde
        '
        Me.txt_desde.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_desde.Location = New System.Drawing.Point(241, 37)
        Me.txt_desde.MaxLength = 40
        Me.txt_desde.Name = "txt_desde"
        Me.txt_desde.Size = New System.Drawing.Size(107, 21)
        Me.txt_desde.TabIndex = 22
        Me.txt_desde.Visible = False
        '
        'txt_hasta
        '
        Me.txt_hasta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_hasta.Location = New System.Drawing.Point(379, 37)
        Me.txt_hasta.MaxLength = 40
        Me.txt_hasta.Name = "txt_hasta"
        Me.txt_hasta.Size = New System.Drawing.Size(107, 21)
        Me.txt_hasta.TabIndex = 23
        Me.txt_hasta.Visible = False
        '
        'cmd_filtrar
        '
        Me.cmd_filtrar.Image = CType(resources.GetObject("cmd_filtrar.Image"), System.Drawing.Image)
        Me.cmd_filtrar.Location = New System.Drawing.Point(491, 27)
        Me.cmd_filtrar.Name = "cmd_filtrar"
        Me.cmd_filtrar.Size = New System.Drawing.Size(36, 36)
        Me.cmd_filtrar.TabIndex = 26
        Me.cmd_filtrar.UseVisualStyleBackColor = True
        Me.cmd_filtrar.Visible = False
        '
        'cmb_comparaciones
        '
        Me.cmb_comparaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_comparaciones.FormattingEnabled = True
        Me.cmb_comparaciones.Location = New System.Drawing.Point(136, 37)
        Me.cmb_comparaciones.Name = "cmb_comparaciones"
        Me.cmb_comparaciones.Size = New System.Drawing.Size(99, 21)
        Me.cmb_comparaciones.TabIndex = 21
        Me.cmb_comparaciones.Visible = False
        '
        'txt_valor_buscado
        '
        Me.txt_valor_buscado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_valor_buscado.Location = New System.Drawing.Point(241, 37)
        Me.txt_valor_buscado.MaxLength = 40
        Me.txt_valor_buscado.Name = "txt_valor_buscado"
        Me.txt_valor_buscado.Size = New System.Drawing.Size(244, 21)
        Me.txt_valor_buscado.TabIndex = 24
        Me.txt_valor_buscado.Visible = False
        '
        'cmb_campos
        '
        Me.cmb_campos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_campos.FormattingEnabled = True
        Me.cmb_campos.Location = New System.Drawing.Point(136, 13)
        Me.cmb_campos.Name = "cmb_campos"
        Me.cmb_campos.Size = New System.Drawing.Size(157, 21)
        Me.cmb_campos.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(70, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Filtrar por : "
        '
        'lbl_Y
        '
        Me.lbl_Y.AutoSize = True
        Me.lbl_Y.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Y.Location = New System.Drawing.Point(352, 40)
        Me.lbl_Y.Name = "lbl_Y"
        Me.lbl_Y.Size = New System.Drawing.Size(23, 15)
        Me.lbl_Y.TabIndex = 27
        Me.lbl_Y.Text = "Y : "
        Me.lbl_Y.Visible = False
        '
        'tab_detalles
        '
        Me.tab_detalles.Controls.Add(Me.grd_detalles)
        Me.tab_detalles.Controls.Add(Me.Label2)
        Me.tab_detalles.Location = New System.Drawing.Point(4, 22)
        Me.tab_detalles.Name = "tab_detalles"
        Me.tab_detalles.Padding = New System.Windows.Forms.Padding(3)
        Me.tab_detalles.Size = New System.Drawing.Size(914, 518)
        Me.tab_detalles.TabIndex = 1
        Me.tab_detalles.Text = "Detalles de Conteos"
        Me.tab_detalles.UseVisualStyleBackColor = True
        '
        'grd_detalles
        '
        Me.grd_detalles.AllowUserToAddRows = False
        Me.grd_detalles.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd_detalles.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grd_detalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grd_detalles.DefaultCellStyle = DataGridViewCellStyle5
        Me.grd_detalles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd_detalles.Location = New System.Drawing.Point(3, 28)
        Me.grd_detalles.Name = "grd_detalles"
        Me.grd_detalles.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd_detalles.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grd_detalles.Size = New System.Drawing.Size(908, 487)
        Me.grd_detalles.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(345, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Detalles de Conteos de Articulo"
        '
        'tmr_arbol
        '
        Me.tmr_arbol.Interval = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_archivo_2, Me.ReportesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(922, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnu_archivo_2
        '
        Me.mnu_archivo_2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_exportar_excel_2, Me.mnu_ibm_basto, Me.mnu_salir_2})
        Me.mnu_archivo_2.Name = "mnu_archivo_2"
        Me.mnu_archivo_2.Size = New System.Drawing.Size(60, 20)
        Me.mnu_archivo_2.Text = "&Archivo"
        '
        'mnu_exportar_excel_2
        '
        Me.mnu_exportar_excel_2.Name = "mnu_exportar_excel_2"
        Me.mnu_exportar_excel_2.Size = New System.Drawing.Size(180, 22)
        Me.mnu_exportar_excel_2.Text = "&Exportar a Excel"
        '
        'mnu_ibm_basto
        '
        Me.mnu_ibm_basto.Name = "mnu_ibm_basto"
        Me.mnu_ibm_basto.Size = New System.Drawing.Size(180, 22)
        Me.mnu_ibm_basto.Text = "Exportar IBM Vasto"
        '
        'mnu_salir_2
        '
        Me.mnu_salir_2.Name = "mnu_salir_2"
        Me.mnu_salir_2.Size = New System.Drawing.Size(180, 22)
        Me.mnu_salir_2.Text = "&Salir"
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_desplegar_reporte_2, Me.mnu_cubo_excel})
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.ReportesToolStripMenuItem.Text = "&Reportes"
        '
        'mnu_desplegar_reporte_2
        '
        Me.mnu_desplegar_reporte_2.Name = "mnu_desplegar_reporte_2"
        Me.mnu_desplegar_reporte_2.Size = New System.Drawing.Size(170, 22)
        Me.mnu_desplegar_reporte_2.Text = "&Desplegar Reporte"
        '
        'mnu_cubo_excel
        '
        Me.mnu_cubo_excel.Name = "mnu_cubo_excel"
        Me.mnu_cubo_excel.Size = New System.Drawing.Size(170, 22)
        Me.mnu_cubo_excel.Text = "Cubo &Excel"
        '
        'FrmVerExistencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 590)
        Me.ControlBox = False
        Me.Controls.Add(Me.tab_padre)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(910, 600)
        Me.Name = "FrmVerExistencias"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resumen de Inventario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.tab_padre.ResumeLayout(False)
        Me.tab_resumen.ResumeLayout(False)
        CType(Me.grd_existencias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_ajustar.ResumeLayout(False)
        Me.pnl_ajustar.PerformLayout()
        Me.tab_detalles.ResumeLayout(False)
        Me.tab_detalles.PerformLayout()
        CType(Me.grd_detalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_estado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tab_padre As System.Windows.Forms.TabControl
    Friend WithEvents tab_resumen As System.Windows.Forms.TabPage
    Friend WithEvents grd_existencias As System.Windows.Forms.DataGridView
    Friend WithEvents tab_detalles As System.Windows.Forms.TabPage
    Friend WithEvents mnu_archivo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_exportar_excel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnu_archivo_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_exportar_excel_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_salir_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_estado_2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbr_estado_2 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_desplegar_reporte_2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_cubo_excel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbl_porcentaje As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmd_excel As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnl_ajustar As System.Windows.Forms.Panel
    Friend WithEvents opt_falso As System.Windows.Forms.RadioButton
    Friend WithEvents opt_verdadero As System.Windows.Forms.RadioButton
    Friend WithEvents txt_desde As System.Windows.Forms.TextBox
    Friend WithEvents txt_hasta As System.Windows.Forms.TextBox
    Friend WithEvents cmd_filtrar As System.Windows.Forms.Button
    Friend WithEvents cmb_comparaciones As System.Windows.Forms.ComboBox
    Friend WithEvents txt_valor_buscado As System.Windows.Forms.TextBox
    Friend WithEvents cmb_campos As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_Y As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnu_ibm_basto As System.Windows.Forms.ToolStripMenuItem
End Class
