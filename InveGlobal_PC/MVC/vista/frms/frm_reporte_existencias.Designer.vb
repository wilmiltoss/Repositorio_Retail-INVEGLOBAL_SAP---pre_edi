<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_reporte_existencias
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_reporte_existencias))
        Me.grp_campos = New System.Windows.Forms.GroupBox()
        Me.chk_ajustado = New System.Windows.Forms.CheckBox()
        Me.chk_cantidad_teorica = New System.Windows.Forms.CheckBox()
        Me.chk_cantidad_ajuste = New System.Windows.Forms.CheckBox()
        Me.chk_diferencia_1_3 = New System.Windows.Forms.CheckBox()
        Me.chk_diferencia_2_3 = New System.Windows.Forms.CheckBox()
        Me.chk_diferencia_1_2 = New System.Windows.Forms.CheckBox()
        Me.chk_conteo_3 = New System.Windows.Forms.CheckBox()
        Me.chk_conteo_2 = New System.Windows.Forms.CheckBox()
        Me.chk_conteo_1 = New System.Windows.Forms.CheckBox()
        Me.chk_costo = New System.Windows.Forms.CheckBox()
        Me.chk_scanning = New System.Windows.Forms.CheckBox()
        Me.chk_sectores = New System.Windows.Forms.CheckBox()
        Me.grp_operadores = New System.Windows.Forms.GroupBox()
        Me.cmb_ajustado = New System.Windows.Forms.ComboBox()
        Me.cmb_cantidad_teorica = New System.Windows.Forms.ComboBox()
        Me.cmb_cantidad_ajuste = New System.Windows.Forms.ComboBox()
        Me.cmb_diferencia_1_3 = New System.Windows.Forms.ComboBox()
        Me.cmb_diferencia_2_3 = New System.Windows.Forms.ComboBox()
        Me.cmb_diferencia_1_2 = New System.Windows.Forms.ComboBox()
        Me.cmb_conteo_3 = New System.Windows.Forms.ComboBox()
        Me.cmb_conteo_2 = New System.Windows.Forms.ComboBox()
        Me.cmb_conteo_1 = New System.Windows.Forms.ComboBox()
        Me.cmb_costo = New System.Windows.Forms.ComboBox()
        Me.cmb_scanning = New System.Windows.Forms.ComboBox()
        Me.cmb_sector = New System.Windows.Forms.ComboBox()
        Me.grp_valores = New System.Windows.Forms.GroupBox()
        Me.tvw_arbol = New System.Windows.Forms.TreeView()
        Me.txt_max_cantidad_teorica = New System.Windows.Forms.TextBox()
        Me.txt_min_cantidad_teorica = New System.Windows.Forms.TextBox()
        Me.lbl_y_cantidad_teorica = New System.Windows.Forms.Label()
        Me.txt_max_ajuste = New System.Windows.Forms.TextBox()
        Me.txt_max_dif_1_3 = New System.Windows.Forms.TextBox()
        Me.txt_max_dif_2_3 = New System.Windows.Forms.TextBox()
        Me.txt_max_dif_1_2 = New System.Windows.Forms.TextBox()
        Me.txt_max_conteo_3 = New System.Windows.Forms.TextBox()
        Me.txt_max_conteo_2 = New System.Windows.Forms.TextBox()
        Me.txt_max_conteo_1 = New System.Windows.Forms.TextBox()
        Me.txt_max_costo = New System.Windows.Forms.TextBox()
        Me.txt_max_scanning = New System.Windows.Forms.TextBox()
        Me.txt_min_ajuste = New System.Windows.Forms.TextBox()
        Me.txt_min_dif_1_3 = New System.Windows.Forms.TextBox()
        Me.txt_min_dif_2_3 = New System.Windows.Forms.TextBox()
        Me.txt_min_dif_1_2 = New System.Windows.Forms.TextBox()
        Me.txt_min_conteo_3 = New System.Windows.Forms.TextBox()
        Me.txt_min_conteo_2 = New System.Windows.Forms.TextBox()
        Me.txt_min_conteo_1 = New System.Windows.Forms.TextBox()
        Me.txt_min_costo = New System.Windows.Forms.TextBox()
        Me.txt_min_scanning = New System.Windows.Forms.TextBox()
        Me.lbl_y_ajuste = New System.Windows.Forms.Label()
        Me.lbl_y_dif_1_3 = New System.Windows.Forms.Label()
        Me.lbl_y_dif_2_3 = New System.Windows.Forms.Label()
        Me.lbl_y_dif_1_2 = New System.Windows.Forms.Label()
        Me.lbl_y_conteo_3 = New System.Windows.Forms.Label()
        Me.lbl_y_conteo_2 = New System.Windows.Forms.Label()
        Me.lbl_y_conteo_1 = New System.Windows.Forms.Label()
        Me.lbl_y_costo = New System.Windows.Forms.Label()
        Me.lbl_y_scanning = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tmr_arbol = New System.Windows.Forms.Timer(Me.components)
        Me.cmd_visualizar = New System.Windows.Forms.Button()
        Me.cmd_salir = New System.Windows.Forms.Button()
        Me.grp_campos.SuspendLayout()
        Me.grp_operadores.SuspendLayout()
        Me.grp_valores.SuspendLayout()
        Me.SuspendLayout()
        '
        'grp_campos
        '
        Me.grp_campos.Controls.Add(Me.chk_ajustado)
        Me.grp_campos.Controls.Add(Me.chk_cantidad_teorica)
        Me.grp_campos.Controls.Add(Me.chk_cantidad_ajuste)
        Me.grp_campos.Controls.Add(Me.chk_diferencia_1_3)
        Me.grp_campos.Controls.Add(Me.chk_diferencia_2_3)
        Me.grp_campos.Controls.Add(Me.chk_diferencia_1_2)
        Me.grp_campos.Controls.Add(Me.chk_conteo_3)
        Me.grp_campos.Controls.Add(Me.chk_conteo_2)
        Me.grp_campos.Controls.Add(Me.chk_conteo_1)
        Me.grp_campos.Controls.Add(Me.chk_costo)
        Me.grp_campos.Controls.Add(Me.chk_scanning)
        Me.grp_campos.Controls.Add(Me.chk_sectores)
        Me.grp_campos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_campos.Location = New System.Drawing.Point(12, 40)
        Me.grp_campos.Name = "grp_campos"
        Me.grp_campos.Size = New System.Drawing.Size(178, 360)
        Me.grp_campos.TabIndex = 0
        Me.grp_campos.TabStop = False
        Me.grp_campos.Text = "Campos"
        '
        'chk_ajustado
        '
        Me.chk_ajustado.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_ajustado.Location = New System.Drawing.Point(20, 300)
        Me.chk_ajustado.Name = "chk_ajustado"
        Me.chk_ajustado.Size = New System.Drawing.Size(146, 20)
        Me.chk_ajustado.TabIndex = 12
        Me.chk_ajustado.Text = "Ajustado"
        Me.chk_ajustado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_ajustado.UseVisualStyleBackColor = True
        '
        'chk_cantidad_teorica
        '
        Me.chk_cantidad_teorica.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_cantidad_teorica.Location = New System.Drawing.Point(20, 90)
        Me.chk_cantidad_teorica.Name = "chk_cantidad_teorica"
        Me.chk_cantidad_teorica.Size = New System.Drawing.Size(145, 20)
        Me.chk_cantidad_teorica.TabIndex = 3
        Me.chk_cantidad_teorica.Text = "Cant. Teórica"
        Me.chk_cantidad_teorica.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_cantidad_teorica.UseVisualStyleBackColor = True
        '
        'chk_cantidad_ajuste
        '
        Me.chk_cantidad_ajuste.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_cantidad_ajuste.Location = New System.Drawing.Point(20, 270)
        Me.chk_cantidad_ajuste.Name = "chk_cantidad_ajuste"
        Me.chk_cantidad_ajuste.Size = New System.Drawing.Size(146, 20)
        Me.chk_cantidad_ajuste.TabIndex = 11
        Me.chk_cantidad_ajuste.Text = "Cant. de Ajuste"
        Me.chk_cantidad_ajuste.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_cantidad_ajuste.UseVisualStyleBackColor = True
        '
        'chk_diferencia_1_3
        '
        Me.chk_diferencia_1_3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_diferencia_1_3.Location = New System.Drawing.Point(20, 330)
        Me.chk_diferencia_1_3.Name = "chk_diferencia_1_3"
        Me.chk_diferencia_1_3.Size = New System.Drawing.Size(50, 20)
        Me.chk_diferencia_1_3.TabIndex = 10
        Me.chk_diferencia_1_3.Text = "Diferencia C1 C3"
        Me.chk_diferencia_1_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_diferencia_1_3.UseVisualStyleBackColor = True
        Me.chk_diferencia_1_3.Visible = False
        '
        'chk_diferencia_2_3
        '
        Me.chk_diferencia_2_3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_diferencia_2_3.Location = New System.Drawing.Point(90, 330)
        Me.chk_diferencia_2_3.Name = "chk_diferencia_2_3"
        Me.chk_diferencia_2_3.Size = New System.Drawing.Size(50, 20)
        Me.chk_diferencia_2_3.TabIndex = 9
        Me.chk_diferencia_2_3.Text = "Diferencia C2 C3"
        Me.chk_diferencia_2_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_diferencia_2_3.UseVisualStyleBackColor = True
        Me.chk_diferencia_2_3.Visible = False
        '
        'chk_diferencia_1_2
        '
        Me.chk_diferencia_1_2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_diferencia_1_2.Location = New System.Drawing.Point(20, 240)
        Me.chk_diferencia_1_2.Name = "chk_diferencia_1_2"
        Me.chk_diferencia_1_2.Size = New System.Drawing.Size(146, 20)
        Me.chk_diferencia_1_2.TabIndex = 8
        Me.chk_diferencia_1_2.Text = "Diferencia C1 C2"
        Me.chk_diferencia_1_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_diferencia_1_2.UseVisualStyleBackColor = True
        '
        'chk_conteo_3
        '
        Me.chk_conteo_3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_conteo_3.Location = New System.Drawing.Point(20, 210)
        Me.chk_conteo_3.Name = "chk_conteo_3"
        Me.chk_conteo_3.Size = New System.Drawing.Size(145, 20)
        Me.chk_conteo_3.TabIndex = 7
        Me.chk_conteo_3.Text = "Conteo 3"
        Me.chk_conteo_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_conteo_3.UseVisualStyleBackColor = True
        '
        'chk_conteo_2
        '
        Me.chk_conteo_2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_conteo_2.Location = New System.Drawing.Point(20, 180)
        Me.chk_conteo_2.Name = "chk_conteo_2"
        Me.chk_conteo_2.Size = New System.Drawing.Size(146, 20)
        Me.chk_conteo_2.TabIndex = 6
        Me.chk_conteo_2.Text = "Conteo 2"
        Me.chk_conteo_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_conteo_2.UseVisualStyleBackColor = True
        '
        'chk_conteo_1
        '
        Me.chk_conteo_1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_conteo_1.Location = New System.Drawing.Point(20, 150)
        Me.chk_conteo_1.Name = "chk_conteo_1"
        Me.chk_conteo_1.Size = New System.Drawing.Size(145, 20)
        Me.chk_conteo_1.TabIndex = 5
        Me.chk_conteo_1.Text = "Conteo 1"
        Me.chk_conteo_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_conteo_1.UseVisualStyleBackColor = True
        '
        'chk_costo
        '
        Me.chk_costo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_costo.Location = New System.Drawing.Point(20, 120)
        Me.chk_costo.Name = "chk_costo"
        Me.chk_costo.Size = New System.Drawing.Size(145, 20)
        Me.chk_costo.TabIndex = 4
        Me.chk_costo.Text = "Costo"
        Me.chk_costo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_costo.UseVisualStyleBackColor = True
        '
        'chk_scanning
        '
        Me.chk_scanning.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_scanning.Location = New System.Drawing.Point(20, 60)
        Me.chk_scanning.Name = "chk_scanning"
        Me.chk_scanning.Size = New System.Drawing.Size(145, 20)
        Me.chk_scanning.TabIndex = 2
        Me.chk_scanning.Text = "Cod. Artículo"
        Me.chk_scanning.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_scanning.UseVisualStyleBackColor = True
        '
        'chk_sectores
        '
        Me.chk_sectores.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_sectores.Location = New System.Drawing.Point(20, 30)
        Me.chk_sectores.Name = "chk_sectores"
        Me.chk_sectores.Size = New System.Drawing.Size(145, 20)
        Me.chk_sectores.TabIndex = 1
        Me.chk_sectores.Text = "Sectores"
        Me.chk_sectores.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chk_sectores.UseVisualStyleBackColor = True
        '
        'grp_operadores
        '
        Me.grp_operadores.Controls.Add(Me.cmb_ajustado)
        Me.grp_operadores.Controls.Add(Me.cmb_cantidad_teorica)
        Me.grp_operadores.Controls.Add(Me.cmb_cantidad_ajuste)
        Me.grp_operadores.Controls.Add(Me.cmb_diferencia_1_3)
        Me.grp_operadores.Controls.Add(Me.cmb_diferencia_2_3)
        Me.grp_operadores.Controls.Add(Me.cmb_diferencia_1_2)
        Me.grp_operadores.Controls.Add(Me.cmb_conteo_3)
        Me.grp_operadores.Controls.Add(Me.cmb_conteo_2)
        Me.grp_operadores.Controls.Add(Me.cmb_conteo_1)
        Me.grp_operadores.Controls.Add(Me.cmb_costo)
        Me.grp_operadores.Controls.Add(Me.cmb_scanning)
        Me.grp_operadores.Controls.Add(Me.cmb_sector)
        Me.grp_operadores.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_operadores.Location = New System.Drawing.Point(200, 40)
        Me.grp_operadores.Name = "grp_operadores"
        Me.grp_operadores.Size = New System.Drawing.Size(100, 360)
        Me.grp_operadores.TabIndex = 12
        Me.grp_operadores.TabStop = False
        Me.grp_operadores.Text = "Operadores"
        '
        'cmb_ajustado
        '
        Me.cmb_ajustado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ajustado.Enabled = False
        Me.cmb_ajustado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_ajustado.FormattingEnabled = True
        Me.cmb_ajustado.Items.AddRange(New Object() {"SI", "NO"})
        Me.cmb_ajustado.Location = New System.Drawing.Point(10, 300)
        Me.cmb_ajustado.Name = "cmb_ajustado"
        Me.cmb_ajustado.Size = New System.Drawing.Size(81, 21)
        Me.cmb_ajustado.TabIndex = 24
        '
        'cmb_cantidad_teorica
        '
        Me.cmb_cantidad_teorica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_cantidad_teorica.Enabled = False
        Me.cmb_cantidad_teorica.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_cantidad_teorica.FormattingEnabled = True
        Me.cmb_cantidad_teorica.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_cantidad_teorica.Location = New System.Drawing.Point(10, 90)
        Me.cmb_cantidad_teorica.Name = "cmb_cantidad_teorica"
        Me.cmb_cantidad_teorica.Size = New System.Drawing.Size(81, 21)
        Me.cmb_cantidad_teorica.TabIndex = 15
        '
        'cmb_cantidad_ajuste
        '
        Me.cmb_cantidad_ajuste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_cantidad_ajuste.Enabled = False
        Me.cmb_cantidad_ajuste.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_cantidad_ajuste.FormattingEnabled = True
        Me.cmb_cantidad_ajuste.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_cantidad_ajuste.Location = New System.Drawing.Point(10, 270)
        Me.cmb_cantidad_ajuste.Name = "cmb_cantidad_ajuste"
        Me.cmb_cantidad_ajuste.Size = New System.Drawing.Size(81, 21)
        Me.cmb_cantidad_ajuste.TabIndex = 23
        '
        'cmb_diferencia_1_3
        '
        Me.cmb_diferencia_1_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_diferencia_1_3.Enabled = False
        Me.cmb_diferencia_1_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_diferencia_1_3.FormattingEnabled = True
        Me.cmb_diferencia_1_3.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_diferencia_1_3.Location = New System.Drawing.Point(10, 330)
        Me.cmb_diferencia_1_3.Name = "cmb_diferencia_1_3"
        Me.cmb_diferencia_1_3.Size = New System.Drawing.Size(30, 21)
        Me.cmb_diferencia_1_3.TabIndex = 22
        Me.cmb_diferencia_1_3.Visible = False
        '
        'cmb_diferencia_2_3
        '
        Me.cmb_diferencia_2_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_diferencia_2_3.Enabled = False
        Me.cmb_diferencia_2_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_diferencia_2_3.FormattingEnabled = True
        Me.cmb_diferencia_2_3.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_diferencia_2_3.Location = New System.Drawing.Point(50, 330)
        Me.cmb_diferencia_2_3.Name = "cmb_diferencia_2_3"
        Me.cmb_diferencia_2_3.Size = New System.Drawing.Size(30, 21)
        Me.cmb_diferencia_2_3.TabIndex = 21
        Me.cmb_diferencia_2_3.Visible = False
        '
        'cmb_diferencia_1_2
        '
        Me.cmb_diferencia_1_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_diferencia_1_2.Enabled = False
        Me.cmb_diferencia_1_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_diferencia_1_2.FormattingEnabled = True
        Me.cmb_diferencia_1_2.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_diferencia_1_2.Location = New System.Drawing.Point(10, 240)
        Me.cmb_diferencia_1_2.Name = "cmb_diferencia_1_2"
        Me.cmb_diferencia_1_2.Size = New System.Drawing.Size(81, 21)
        Me.cmb_diferencia_1_2.TabIndex = 20
        '
        'cmb_conteo_3
        '
        Me.cmb_conteo_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_conteo_3.Enabled = False
        Me.cmb_conteo_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_conteo_3.FormattingEnabled = True
        Me.cmb_conteo_3.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_conteo_3.Location = New System.Drawing.Point(10, 210)
        Me.cmb_conteo_3.Name = "cmb_conteo_3"
        Me.cmb_conteo_3.Size = New System.Drawing.Size(81, 21)
        Me.cmb_conteo_3.TabIndex = 19
        '
        'cmb_conteo_2
        '
        Me.cmb_conteo_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_conteo_2.Enabled = False
        Me.cmb_conteo_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_conteo_2.FormattingEnabled = True
        Me.cmb_conteo_2.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_conteo_2.Location = New System.Drawing.Point(10, 180)
        Me.cmb_conteo_2.Name = "cmb_conteo_2"
        Me.cmb_conteo_2.Size = New System.Drawing.Size(81, 21)
        Me.cmb_conteo_2.TabIndex = 18
        '
        'cmb_conteo_1
        '
        Me.cmb_conteo_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_conteo_1.Enabled = False
        Me.cmb_conteo_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_conteo_1.FormattingEnabled = True
        Me.cmb_conteo_1.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_conteo_1.Location = New System.Drawing.Point(10, 150)
        Me.cmb_conteo_1.Name = "cmb_conteo_1"
        Me.cmb_conteo_1.Size = New System.Drawing.Size(81, 21)
        Me.cmb_conteo_1.TabIndex = 17
        '
        'cmb_costo
        '
        Me.cmb_costo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_costo.Enabled = False
        Me.cmb_costo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_costo.FormattingEnabled = True
        Me.cmb_costo.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_costo.Location = New System.Drawing.Point(10, 120)
        Me.cmb_costo.Name = "cmb_costo"
        Me.cmb_costo.Size = New System.Drawing.Size(81, 21)
        Me.cmb_costo.TabIndex = 16
        '
        'cmb_scanning
        '
        Me.cmb_scanning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_scanning.Enabled = False
        Me.cmb_scanning.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_scanning.FormattingEnabled = True
        Me.cmb_scanning.Items.AddRange(New Object() {"es igual a", "no es igual a", "es mayor que", "es menor que", "es mayor o igual que", "es menor o igual que", "esta entre", "no esta entre"})
        Me.cmb_scanning.Location = New System.Drawing.Point(10, 60)
        Me.cmb_scanning.Name = "cmb_scanning"
        Me.cmb_scanning.Size = New System.Drawing.Size(81, 21)
        Me.cmb_scanning.TabIndex = 14
        '
        'cmb_sector
        '
        Me.cmb_sector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_sector.Enabled = False
        Me.cmb_sector.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_sector.FormattingEnabled = True
        Me.cmb_sector.Items.AddRange(New Object() {"es igual a", "no es igual a"})
        Me.cmb_sector.Location = New System.Drawing.Point(10, 30)
        Me.cmb_sector.Name = "cmb_sector"
        Me.cmb_sector.Size = New System.Drawing.Size(81, 21)
        Me.cmb_sector.TabIndex = 13
        '
        'grp_valores
        '
        Me.grp_valores.Controls.Add(Me.tvw_arbol)
        Me.grp_valores.Controls.Add(Me.txt_max_cantidad_teorica)
        Me.grp_valores.Controls.Add(Me.txt_min_cantidad_teorica)
        Me.grp_valores.Controls.Add(Me.lbl_y_cantidad_teorica)
        Me.grp_valores.Controls.Add(Me.txt_max_ajuste)
        Me.grp_valores.Controls.Add(Me.txt_max_dif_1_3)
        Me.grp_valores.Controls.Add(Me.txt_max_dif_2_3)
        Me.grp_valores.Controls.Add(Me.txt_max_dif_1_2)
        Me.grp_valores.Controls.Add(Me.txt_max_conteo_3)
        Me.grp_valores.Controls.Add(Me.txt_max_conteo_2)
        Me.grp_valores.Controls.Add(Me.txt_max_conteo_1)
        Me.grp_valores.Controls.Add(Me.txt_max_costo)
        Me.grp_valores.Controls.Add(Me.txt_max_scanning)
        Me.grp_valores.Controls.Add(Me.txt_min_ajuste)
        Me.grp_valores.Controls.Add(Me.txt_min_dif_1_3)
        Me.grp_valores.Controls.Add(Me.txt_min_dif_2_3)
        Me.grp_valores.Controls.Add(Me.txt_min_dif_1_2)
        Me.grp_valores.Controls.Add(Me.txt_min_conteo_3)
        Me.grp_valores.Controls.Add(Me.txt_min_conteo_2)
        Me.grp_valores.Controls.Add(Me.txt_min_conteo_1)
        Me.grp_valores.Controls.Add(Me.txt_min_costo)
        Me.grp_valores.Controls.Add(Me.txt_min_scanning)
        Me.grp_valores.Controls.Add(Me.lbl_y_ajuste)
        Me.grp_valores.Controls.Add(Me.lbl_y_dif_1_3)
        Me.grp_valores.Controls.Add(Me.lbl_y_dif_2_3)
        Me.grp_valores.Controls.Add(Me.lbl_y_dif_1_2)
        Me.grp_valores.Controls.Add(Me.lbl_y_conteo_3)
        Me.grp_valores.Controls.Add(Me.lbl_y_conteo_2)
        Me.grp_valores.Controls.Add(Me.lbl_y_conteo_1)
        Me.grp_valores.Controls.Add(Me.lbl_y_costo)
        Me.grp_valores.Controls.Add(Me.lbl_y_scanning)
        Me.grp_valores.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_valores.Location = New System.Drawing.Point(310, 40)
        Me.grp_valores.Name = "grp_valores"
        Me.grp_valores.Size = New System.Drawing.Size(240, 360)
        Me.grp_valores.TabIndex = 24
        Me.grp_valores.TabStop = False
        Me.grp_valores.Text = "Valores"
        '
        'tvw_arbol
        '
        Me.tvw_arbol.Enabled = False
        Me.tvw_arbol.Location = New System.Drawing.Point(10, 30)
        Me.tvw_arbol.Name = "tvw_arbol"
        Me.tvw_arbol.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tvw_arbol.Size = New System.Drawing.Size(220, 21)
        Me.tvw_arbol.TabIndex = 25
        '
        'txt_max_cantidad_teorica
        '
        Me.txt_max_cantidad_teorica.Enabled = False
        Me.txt_max_cantidad_teorica.Location = New System.Drawing.Point(130, 90)
        Me.txt_max_cantidad_teorica.MaxLength = 7
        Me.txt_max_cantidad_teorica.Name = "txt_max_cantidad_teorica"
        Me.txt_max_cantidad_teorica.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_cantidad_teorica.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_cantidad_teorica.TabIndex = 29
        '
        'txt_min_cantidad_teorica
        '
        Me.txt_min_cantidad_teorica.Enabled = False
        Me.txt_min_cantidad_teorica.Location = New System.Drawing.Point(10, 90)
        Me.txt_min_cantidad_teorica.MaxLength = 7
        Me.txt_min_cantidad_teorica.Name = "txt_min_cantidad_teorica"
        Me.txt_min_cantidad_teorica.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_cantidad_teorica.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_cantidad_teorica.TabIndex = 28
        '
        'lbl_y_cantidad_teorica
        '
        Me.lbl_y_cantidad_teorica.AutoSize = True
        Me.lbl_y_cantidad_teorica.Location = New System.Drawing.Point(110, 90)
        Me.lbl_y_cantidad_teorica.Name = "lbl_y_cantidad_teorica"
        Me.lbl_y_cantidad_teorica.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_cantidad_teorica.TabIndex = 53
        Me.lbl_y_cantidad_teorica.Text = "Y :"
        Me.lbl_y_cantidad_teorica.Visible = False
        '
        'txt_max_ajuste
        '
        Me.txt_max_ajuste.Enabled = False
        Me.txt_max_ajuste.Location = New System.Drawing.Point(130, 270)
        Me.txt_max_ajuste.MaxLength = 7
        Me.txt_max_ajuste.Name = "txt_max_ajuste"
        Me.txt_max_ajuste.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_ajuste.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_ajuste.TabIndex = 45
        '
        'txt_max_dif_1_3
        '
        Me.txt_max_dif_1_3.Enabled = False
        Me.txt_max_dif_1_3.Location = New System.Drawing.Point(200, 330)
        Me.txt_max_dif_1_3.MaxLength = 7
        Me.txt_max_dif_1_3.Name = "txt_max_dif_1_3"
        Me.txt_max_dif_1_3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_dif_1_3.Size = New System.Drawing.Size(30, 21)
        Me.txt_max_dif_1_3.TabIndex = 43
        Me.txt_max_dif_1_3.Visible = False
        '
        'txt_max_dif_2_3
        '
        Me.txt_max_dif_2_3.Enabled = False
        Me.txt_max_dif_2_3.Location = New System.Drawing.Point(60, 330)
        Me.txt_max_dif_2_3.MaxLength = 7
        Me.txt_max_dif_2_3.Name = "txt_max_dif_2_3"
        Me.txt_max_dif_2_3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_dif_2_3.Size = New System.Drawing.Size(30, 21)
        Me.txt_max_dif_2_3.TabIndex = 41
        Me.txt_max_dif_2_3.Visible = False
        '
        'txt_max_dif_1_2
        '
        Me.txt_max_dif_1_2.Enabled = False
        Me.txt_max_dif_1_2.Location = New System.Drawing.Point(130, 240)
        Me.txt_max_dif_1_2.MaxLength = 7
        Me.txt_max_dif_1_2.Name = "txt_max_dif_1_2"
        Me.txt_max_dif_1_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_dif_1_2.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_dif_1_2.TabIndex = 39
        '
        'txt_max_conteo_3
        '
        Me.txt_max_conteo_3.Enabled = False
        Me.txt_max_conteo_3.Location = New System.Drawing.Point(130, 210)
        Me.txt_max_conteo_3.MaxLength = 7
        Me.txt_max_conteo_3.Name = "txt_max_conteo_3"
        Me.txt_max_conteo_3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_conteo_3.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_conteo_3.TabIndex = 37
        '
        'txt_max_conteo_2
        '
        Me.txt_max_conteo_2.Enabled = False
        Me.txt_max_conteo_2.Location = New System.Drawing.Point(130, 180)
        Me.txt_max_conteo_2.MaxLength = 7
        Me.txt_max_conteo_2.Name = "txt_max_conteo_2"
        Me.txt_max_conteo_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_conteo_2.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_conteo_2.TabIndex = 35
        '
        'txt_max_conteo_1
        '
        Me.txt_max_conteo_1.Enabled = False
        Me.txt_max_conteo_1.Location = New System.Drawing.Point(130, 150)
        Me.txt_max_conteo_1.MaxLength = 7
        Me.txt_max_conteo_1.Name = "txt_max_conteo_1"
        Me.txt_max_conteo_1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_conteo_1.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_conteo_1.TabIndex = 33
        '
        'txt_max_costo
        '
        Me.txt_max_costo.Enabled = False
        Me.txt_max_costo.Location = New System.Drawing.Point(130, 120)
        Me.txt_max_costo.MaxLength = 7
        Me.txt_max_costo.Name = "txt_max_costo"
        Me.txt_max_costo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_costo.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_costo.TabIndex = 31
        '
        'txt_max_scanning
        '
        Me.txt_max_scanning.Enabled = False
        Me.txt_max_scanning.Location = New System.Drawing.Point(130, 60)
        Me.txt_max_scanning.MaxLength = 15
        Me.txt_max_scanning.Name = "txt_max_scanning"
        Me.txt_max_scanning.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_max_scanning.Size = New System.Drawing.Size(100, 21)
        Me.txt_max_scanning.TabIndex = 27
        '
        'txt_min_ajuste
        '
        Me.txt_min_ajuste.Enabled = False
        Me.txt_min_ajuste.Location = New System.Drawing.Point(10, 270)
        Me.txt_min_ajuste.MaxLength = 7
        Me.txt_min_ajuste.Name = "txt_min_ajuste"
        Me.txt_min_ajuste.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_ajuste.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_ajuste.TabIndex = 44
        '
        'txt_min_dif_1_3
        '
        Me.txt_min_dif_1_3.Enabled = False
        Me.txt_min_dif_1_3.Location = New System.Drawing.Point(150, 330)
        Me.txt_min_dif_1_3.MaxLength = 7
        Me.txt_min_dif_1_3.Name = "txt_min_dif_1_3"
        Me.txt_min_dif_1_3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_dif_1_3.Size = New System.Drawing.Size(30, 21)
        Me.txt_min_dif_1_3.TabIndex = 42
        Me.txt_min_dif_1_3.Visible = False
        '
        'txt_min_dif_2_3
        '
        Me.txt_min_dif_2_3.Enabled = False
        Me.txt_min_dif_2_3.Location = New System.Drawing.Point(10, 330)
        Me.txt_min_dif_2_3.MaxLength = 7
        Me.txt_min_dif_2_3.Name = "txt_min_dif_2_3"
        Me.txt_min_dif_2_3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_dif_2_3.Size = New System.Drawing.Size(30, 21)
        Me.txt_min_dif_2_3.TabIndex = 40
        Me.txt_min_dif_2_3.Visible = False
        '
        'txt_min_dif_1_2
        '
        Me.txt_min_dif_1_2.Enabled = False
        Me.txt_min_dif_1_2.Location = New System.Drawing.Point(10, 240)
        Me.txt_min_dif_1_2.MaxLength = 7
        Me.txt_min_dif_1_2.Name = "txt_min_dif_1_2"
        Me.txt_min_dif_1_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_dif_1_2.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_dif_1_2.TabIndex = 38
        '
        'txt_min_conteo_3
        '
        Me.txt_min_conteo_3.Enabled = False
        Me.txt_min_conteo_3.Location = New System.Drawing.Point(10, 210)
        Me.txt_min_conteo_3.MaxLength = 7
        Me.txt_min_conteo_3.Name = "txt_min_conteo_3"
        Me.txt_min_conteo_3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_conteo_3.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_conteo_3.TabIndex = 36
        '
        'txt_min_conteo_2
        '
        Me.txt_min_conteo_2.Enabled = False
        Me.txt_min_conteo_2.Location = New System.Drawing.Point(10, 180)
        Me.txt_min_conteo_2.MaxLength = 7
        Me.txt_min_conteo_2.Name = "txt_min_conteo_2"
        Me.txt_min_conteo_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_conteo_2.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_conteo_2.TabIndex = 34
        '
        'txt_min_conteo_1
        '
        Me.txt_min_conteo_1.Enabled = False
        Me.txt_min_conteo_1.Location = New System.Drawing.Point(10, 150)
        Me.txt_min_conteo_1.MaxLength = 7
        Me.txt_min_conteo_1.Name = "txt_min_conteo_1"
        Me.txt_min_conteo_1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_conteo_1.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_conteo_1.TabIndex = 32
        '
        'txt_min_costo
        '
        Me.txt_min_costo.Enabled = False
        Me.txt_min_costo.Location = New System.Drawing.Point(10, 120)
        Me.txt_min_costo.MaxLength = 7
        Me.txt_min_costo.Name = "txt_min_costo"
        Me.txt_min_costo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_costo.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_costo.TabIndex = 30
        '
        'txt_min_scanning
        '
        Me.txt_min_scanning.Enabled = False
        Me.txt_min_scanning.Location = New System.Drawing.Point(10, 60)
        Me.txt_min_scanning.MaxLength = 15
        Me.txt_min_scanning.Name = "txt_min_scanning"
        Me.txt_min_scanning.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txt_min_scanning.Size = New System.Drawing.Size(100, 21)
        Me.txt_min_scanning.TabIndex = 26
        '
        'lbl_y_ajuste
        '
        Me.lbl_y_ajuste.AutoSize = True
        Me.lbl_y_ajuste.Location = New System.Drawing.Point(110, 270)
        Me.lbl_y_ajuste.Name = "lbl_y_ajuste"
        Me.lbl_y_ajuste.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_ajuste.TabIndex = 50
        Me.lbl_y_ajuste.Text = "Y :"
        Me.lbl_y_ajuste.Visible = False
        '
        'lbl_y_dif_1_3
        '
        Me.lbl_y_dif_1_3.AutoSize = True
        Me.lbl_y_dif_1_3.Location = New System.Drawing.Point(180, 330)
        Me.lbl_y_dif_1_3.Name = "lbl_y_dif_1_3"
        Me.lbl_y_dif_1_3.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_dif_1_3.TabIndex = 49
        Me.lbl_y_dif_1_3.Text = "Y :"
        Me.lbl_y_dif_1_3.Visible = False
        '
        'lbl_y_dif_2_3
        '
        Me.lbl_y_dif_2_3.AutoSize = True
        Me.lbl_y_dif_2_3.Location = New System.Drawing.Point(40, 330)
        Me.lbl_y_dif_2_3.Name = "lbl_y_dif_2_3"
        Me.lbl_y_dif_2_3.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_dif_2_3.TabIndex = 48
        Me.lbl_y_dif_2_3.Text = "Y :"
        Me.lbl_y_dif_2_3.Visible = False
        '
        'lbl_y_dif_1_2
        '
        Me.lbl_y_dif_1_2.AutoSize = True
        Me.lbl_y_dif_1_2.Location = New System.Drawing.Point(110, 240)
        Me.lbl_y_dif_1_2.Name = "lbl_y_dif_1_2"
        Me.lbl_y_dif_1_2.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_dif_1_2.TabIndex = 47
        Me.lbl_y_dif_1_2.Text = "Y :"
        Me.lbl_y_dif_1_2.Visible = False
        '
        'lbl_y_conteo_3
        '
        Me.lbl_y_conteo_3.AutoSize = True
        Me.lbl_y_conteo_3.Location = New System.Drawing.Point(110, 210)
        Me.lbl_y_conteo_3.Name = "lbl_y_conteo_3"
        Me.lbl_y_conteo_3.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_conteo_3.TabIndex = 46
        Me.lbl_y_conteo_3.Text = "Y :"
        Me.lbl_y_conteo_3.Visible = False
        '
        'lbl_y_conteo_2
        '
        Me.lbl_y_conteo_2.AutoSize = True
        Me.lbl_y_conteo_2.Location = New System.Drawing.Point(110, 180)
        Me.lbl_y_conteo_2.Name = "lbl_y_conteo_2"
        Me.lbl_y_conteo_2.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_conteo_2.TabIndex = 45
        Me.lbl_y_conteo_2.Text = "Y :"
        Me.lbl_y_conteo_2.Visible = False
        '
        'lbl_y_conteo_1
        '
        Me.lbl_y_conteo_1.AutoSize = True
        Me.lbl_y_conteo_1.Location = New System.Drawing.Point(110, 150)
        Me.lbl_y_conteo_1.Name = "lbl_y_conteo_1"
        Me.lbl_y_conteo_1.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_conteo_1.TabIndex = 44
        Me.lbl_y_conteo_1.Text = "Y :"
        Me.lbl_y_conteo_1.Visible = False
        '
        'lbl_y_costo
        '
        Me.lbl_y_costo.AutoSize = True
        Me.lbl_y_costo.Location = New System.Drawing.Point(110, 120)
        Me.lbl_y_costo.Name = "lbl_y_costo"
        Me.lbl_y_costo.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_costo.TabIndex = 43
        Me.lbl_y_costo.Text = "Y :"
        Me.lbl_y_costo.Visible = False
        '
        'lbl_y_scanning
        '
        Me.lbl_y_scanning.AutoSize = True
        Me.lbl_y_scanning.Location = New System.Drawing.Point(110, 60)
        Me.lbl_y_scanning.Name = "lbl_y_scanning"
        Me.lbl_y_scanning.Size = New System.Drawing.Size(20, 15)
        Me.lbl_y_scanning.TabIndex = 42
        Me.lbl_y_scanning.Text = "Y :"
        Me.lbl_y_scanning.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(387, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Criterios de Filtrado del Reporte de Existencias"
        '
        'tmr_arbol
        '
        Me.tmr_arbol.Interval = 1
        '
        'cmd_visualizar
        '
        Me.cmd_visualizar.Location = New System.Drawing.Point(140, 410)
        Me.cmd_visualizar.Name = "cmd_visualizar"
        Me.cmd_visualizar.Size = New System.Drawing.Size(120, 30)
        Me.cmd_visualizar.TabIndex = 46
        Me.cmd_visualizar.Text = "&Visualizar"
        Me.cmd_visualizar.UseVisualStyleBackColor = True
        '
        'cmd_salir
        '
        Me.cmd_salir.Location = New System.Drawing.Point(280, 410)
        Me.cmd_salir.Name = "cmd_salir"
        Me.cmd_salir.Size = New System.Drawing.Size(120, 30)
        Me.cmd_salir.TabIndex = 47
        Me.cmd_salir.Text = "&Salir"
        Me.cmd_salir.UseVisualStyleBackColor = True
        '
        'frm_reporte_existencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 448)
        Me.Controls.Add(Me.cmd_salir)
        Me.Controls.Add(Me.cmd_visualizar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grp_valores)
        Me.Controls.Add(Me.grp_operadores)
        Me.Controls.Add(Me.grp_campos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_reporte_existencias"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Criterios de Visualización de Reporte"
        Me.grp_campos.ResumeLayout(False)
        Me.grp_operadores.ResumeLayout(False)
        Me.grp_valores.ResumeLayout(False)
        Me.grp_valores.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grp_campos As System.Windows.Forms.GroupBox
    Friend WithEvents grp_operadores As System.Windows.Forms.GroupBox
    Friend WithEvents grp_valores As System.Windows.Forms.GroupBox
    Friend WithEvents tvw_arbol As System.Windows.Forms.TreeView
    Friend WithEvents chk_conteo_1 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_costo As System.Windows.Forms.CheckBox
    Friend WithEvents chk_scanning As System.Windows.Forms.CheckBox
    Friend WithEvents chk_sectores As System.Windows.Forms.CheckBox
    Friend WithEvents chk_diferencia_1_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_conteo_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_conteo_2 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_diferencia_2_3 As System.Windows.Forms.CheckBox
    Friend WithEvents chk_cantidad_ajuste As System.Windows.Forms.CheckBox
    Friend WithEvents chk_diferencia_1_3 As System.Windows.Forms.CheckBox
    Friend WithEvents cmb_sector As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_scanning As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_cantidad_ajuste As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_diferencia_1_3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_diferencia_2_3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_diferencia_1_2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_conteo_3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_conteo_2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_conteo_1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_costo As System.Windows.Forms.ComboBox
    Friend WithEvents txt_min_conteo_1 As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_costo As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_scanning As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_ajuste As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_dif_1_3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_dif_2_3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_dif_1_2 As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_conteo_3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_conteo_2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_ajuste As System.Windows.Forms.Label
    Friend WithEvents lbl_y_dif_1_3 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_dif_2_3 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_dif_1_2 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_conteo_3 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_conteo_2 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_conteo_1 As System.Windows.Forms.Label
    Friend WithEvents lbl_y_costo As System.Windows.Forms.Label
    Friend WithEvents lbl_y_scanning As System.Windows.Forms.Label
    Friend WithEvents txt_max_ajuste As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_dif_1_3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_dif_2_3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_dif_1_2 As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_conteo_3 As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_conteo_2 As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_conteo_1 As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_costo As System.Windows.Forms.TextBox
    Friend WithEvents txt_max_scanning As System.Windows.Forms.TextBox
    Friend WithEvents tmr_arbol As System.Windows.Forms.Timer
    Friend WithEvents cmd_visualizar As System.Windows.Forms.Button
    Friend WithEvents cmd_salir As System.Windows.Forms.Button
    Friend WithEvents chk_cantidad_teorica As System.Windows.Forms.CheckBox
    Friend WithEvents cmb_cantidad_teorica As System.Windows.Forms.ComboBox
    Friend WithEvents txt_max_cantidad_teorica As System.Windows.Forms.TextBox
    Friend WithEvents txt_min_cantidad_teorica As System.Windows.Forms.TextBox
    Friend WithEvents lbl_y_cantidad_teorica As System.Windows.Forms.Label
    Friend WithEvents chk_ajustado As System.Windows.Forms.CheckBox
    Friend WithEvents cmb_ajustado As System.Windows.Forms.ComboBox

End Class
