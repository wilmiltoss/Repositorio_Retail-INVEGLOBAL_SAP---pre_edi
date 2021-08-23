<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmModificarConteo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmModificarConteo))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.grp_datos = New System.Windows.Forms.GroupBox
        Me.lbl_nro_soporte = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lbl_locacion = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lbl_conteo = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lbl_metro = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lbl_nivel = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txt_conteo_1 = New System.Windows.Forms.TextBox
        Me.lbl_letra_soporte = New System.Windows.Forms.Label
        Me.lbl_soporte = New System.Windows.Forms.Label
        Me.lbl_articulo = New System.Windows.Forms.Label
        Me.lbl_scanning = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbl_id_soporte = New System.Windows.Forms.Label
        Me.lbl_id_letra_soporte = New System.Windows.Forms.Label
        Me.lbl_id_locacion = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grp_datos.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(220, 304)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 5
        Me.OK_Button.Text = "Aceptar"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 6
        Me.Cancel_Button.Text = "Cancelar"
        '
        'grp_datos
        '
        Me.grp_datos.Controls.Add(Me.lbl_nro_soporte)
        Me.grp_datos.Controls.Add(Me.Label11)
        Me.grp_datos.Controls.Add(Me.lbl_locacion)
        Me.grp_datos.Controls.Add(Me.Label7)
        Me.grp_datos.Controls.Add(Me.lbl_conteo)
        Me.grp_datos.Controls.Add(Me.Label10)
        Me.grp_datos.Controls.Add(Me.lbl_metro)
        Me.grp_datos.Controls.Add(Me.Label9)
        Me.grp_datos.Controls.Add(Me.lbl_nivel)
        Me.grp_datos.Controls.Add(Me.Label8)
        Me.grp_datos.Controls.Add(Me.txt_conteo_1)
        Me.grp_datos.Controls.Add(Me.lbl_letra_soporte)
        Me.grp_datos.Controls.Add(Me.lbl_soporte)
        Me.grp_datos.Controls.Add(Me.lbl_articulo)
        Me.grp_datos.Controls.Add(Me.lbl_scanning)
        Me.grp_datos.Controls.Add(Me.Label6)
        Me.grp_datos.Controls.Add(Me.Label5)
        Me.grp_datos.Controls.Add(Me.Label4)
        Me.grp_datos.Controls.Add(Me.Label3)
        Me.grp_datos.Controls.Add(Me.Label2)
        Me.grp_datos.Location = New System.Drawing.Point(10, 10)
        Me.grp_datos.Name = "grp_datos"
        Me.grp_datos.Size = New System.Drawing.Size(400, 290)
        Me.grp_datos.TabIndex = 1
        Me.grp_datos.TabStop = False
        Me.grp_datos.Text = "Detalles del Registro"
        '
        'lbl_nro_soporte
        '
        Me.lbl_nro_soporte.AutoSize = True
        Me.lbl_nro_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nro_soporte.Location = New System.Drawing.Point(110, 170)
        Me.lbl_nro_soporte.Name = "lbl_nro_soporte"
        Me.lbl_nro_soporte.Size = New System.Drawing.Size(12, 16)
        Me.lbl_nro_soporte.TabIndex = 22
        Me.lbl_nro_soporte.Text = "|"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 170)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 20)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Nro Soporte : "
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_locacion
        '
        Me.lbl_locacion.AutoSize = True
        Me.lbl_locacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_locacion.Location = New System.Drawing.Point(110, 110)
        Me.lbl_locacion.Name = "lbl_locacion"
        Me.lbl_locacion.Size = New System.Drawing.Size(12, 16)
        Me.lbl_locacion.TabIndex = 20
        Me.lbl_locacion.Text = "|"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(20, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 20)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Locación : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_conteo
        '
        Me.lbl_conteo.AutoSize = True
        Me.lbl_conteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_conteo.Location = New System.Drawing.Point(110, 20)
        Me.lbl_conteo.Name = "lbl_conteo"
        Me.lbl_conteo.Size = New System.Drawing.Size(12, 16)
        Me.lbl_conteo.TabIndex = 18
        Me.lbl_conteo.Text = "|"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 20)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Nº Conteo : "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_metro
        '
        Me.lbl_metro.AutoSize = True
        Me.lbl_metro.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_metro.Location = New System.Drawing.Point(110, 230)
        Me.lbl_metro.Name = "lbl_metro"
        Me.lbl_metro.Size = New System.Drawing.Size(12, 16)
        Me.lbl_metro.TabIndex = 16
        Me.lbl_metro.Text = "|"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(20, 230)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 20)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Metro : "
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_nivel
        '
        Me.lbl_nivel.AutoSize = True
        Me.lbl_nivel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nivel.Location = New System.Drawing.Point(110, 200)
        Me.lbl_nivel.Name = "lbl_nivel"
        Me.lbl_nivel.Size = New System.Drawing.Size(12, 16)
        Me.lbl_nivel.TabIndex = 14
        Me.lbl_nivel.Text = "|"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(20, 200)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 20)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Nivel : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_conteo_1
        '
        Me.txt_conteo_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_conteo_1.Location = New System.Drawing.Point(110, 260)
        Me.txt_conteo_1.MaxLength = 20
        Me.txt_conteo_1.Name = "txt_conteo_1"
        Me.txt_conteo_1.Size = New System.Drawing.Size(150, 21)
        Me.txt_conteo_1.TabIndex = 2
        Me.txt_conteo_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_letra_soporte
        '
        Me.lbl_letra_soporte.AutoSize = True
        Me.lbl_letra_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_letra_soporte.Location = New System.Drawing.Point(380, 10)
        Me.lbl_letra_soporte.Name = "lbl_letra_soporte"
        Me.lbl_letra_soporte.Size = New System.Drawing.Size(12, 16)
        Me.lbl_letra_soporte.TabIndex = 12
        Me.lbl_letra_soporte.Text = "|"
        Me.lbl_letra_soporte.Visible = False
        '
        'lbl_soporte
        '
        Me.lbl_soporte.AutoSize = True
        Me.lbl_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_soporte.Location = New System.Drawing.Point(110, 140)
        Me.lbl_soporte.Name = "lbl_soporte"
        Me.lbl_soporte.Size = New System.Drawing.Size(12, 16)
        Me.lbl_soporte.TabIndex = 11
        Me.lbl_soporte.Text = "|"
        '
        'lbl_articulo
        '
        Me.lbl_articulo.AutoSize = True
        Me.lbl_articulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_articulo.Location = New System.Drawing.Point(110, 80)
        Me.lbl_articulo.Name = "lbl_articulo"
        Me.lbl_articulo.Size = New System.Drawing.Size(12, 16)
        Me.lbl_articulo.TabIndex = 10
        Me.lbl_articulo.Text = "|"
        '
        'lbl_scanning
        '
        Me.lbl_scanning.AutoSize = True
        Me.lbl_scanning.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_scanning.Location = New System.Drawing.Point(110, 50)
        Me.lbl_scanning.Name = "lbl_scanning"
        Me.lbl_scanning.Size = New System.Drawing.Size(12, 16)
        Me.lbl_scanning.TabIndex = 9
        Me.lbl_scanning.Text = "|"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 260)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Cantidad : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(290, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Letra Soporte : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Soporte : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Articulo : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Scanning : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_id_soporte
        '
        Me.lbl_id_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_id_soporte.Location = New System.Drawing.Point(70, 300)
        Me.lbl_id_soporte.Name = "lbl_id_soporte"
        Me.lbl_id_soporte.Size = New System.Drawing.Size(70, 20)
        Me.lbl_id_soporte.TabIndex = 13
        Me.lbl_id_soporte.Text = "id_soporte"
        Me.lbl_id_soporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_id_soporte.Visible = False
        '
        'lbl_id_letra_soporte
        '
        Me.lbl_id_letra_soporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_id_letra_soporte.Location = New System.Drawing.Point(100, 310)
        Me.lbl_id_letra_soporte.Name = "lbl_id_letra_soporte"
        Me.lbl_id_letra_soporte.Size = New System.Drawing.Size(100, 20)
        Me.lbl_id_letra_soporte.TabIndex = 14
        Me.lbl_id_letra_soporte.Text = "id_letra_soporte"
        Me.lbl_id_letra_soporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_id_letra_soporte.Visible = False
        '
        'lbl_id_locacion
        '
        Me.lbl_id_locacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_id_locacion.Location = New System.Drawing.Point(30, 310)
        Me.lbl_id_locacion.Name = "lbl_id_locacion"
        Me.lbl_id_locacion.Size = New System.Drawing.Size(70, 20)
        Me.lbl_id_locacion.TabIndex = 15
        Me.lbl_id_locacion.Text = "id_locacion"
        Me.lbl_id_locacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_id_locacion.Visible = False
        '
        'frm_modificar_cantidad
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(422, 340)
        Me.Controls.Add(Me.lbl_id_locacion)
        Me.Controls.Add(Me.lbl_id_letra_soporte)
        Me.Controls.Add(Me.lbl_id_soporte)
        Me.Controls.Add(Me.grp_datos)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_modificar_cantidad"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Modificar Cantidad de Artículo"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.grp_datos.ResumeLayout(False)
        Me.grp_datos.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents grp_datos As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_scanning As System.Windows.Forms.Label
    Friend WithEvents lbl_letra_soporte As System.Windows.Forms.Label
    Friend WithEvents lbl_soporte As System.Windows.Forms.Label
    Friend WithEvents lbl_articulo As System.Windows.Forms.Label
    Friend WithEvents txt_conteo_1 As System.Windows.Forms.TextBox
    Friend WithEvents lbl_id_soporte As System.Windows.Forms.Label
    Friend WithEvents lbl_id_letra_soporte As System.Windows.Forms.Label
    Friend WithEvents lbl_metro As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lbl_nivel As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lbl_conteo As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lbl_id_locacion As System.Windows.Forms.Label
    Friend WithEvents lbl_locacion As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbl_nro_soporte As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label

End Class
