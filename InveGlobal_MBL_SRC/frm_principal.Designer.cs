namespace InveStockMBL
{
    partial class frm_principal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnu_archivo = new System.Windows.Forms.MenuItem();
            this.mnu_limpiar = new System.Windows.Forms.MenuItem();
            this.mnu_salir = new System.Windows.Forms.MenuItem();
            this.mnu_lecturas = new System.Windows.Forms.MenuItem();
            this.mnu_generar_archivo = new System.Windows.Forms.MenuItem();
            this.mnu_cuenta_lecturas = new System.Windows.Forms.MenuItem();
            this.tab_padre = new System.Windows.Forms.TabControl();
            this.tab_ubicacion = new System.Windows.Forms.TabPage();
            this.num_conteo = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_locaciones = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmd_hecho = new System.Windows.Forms.Button();
            this.num_nro_soporte = new System.Windows.Forms.NumericUpDown();
            this.cmb_letras = new System.Windows.Forms.ComboBox();
            this.num_metro = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.num_nivel = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_soportes = new System.Windows.Forms.ComboBox();
            this.tab_lecturas = new System.Windows.Forms.TabPage();
            this.pnl_mensajes = new System.Windows.Forms.Panel();
            this.lbl_mensaje = new System.Windows.Forms.Label();
            this.lbl_metro_nivel = new System.Windows.Forms.Label();
            this.lbl_locacion = new System.Windows.Forms.Label();
            this.lbl_conteo = new System.Windows.Forms.Label();
            this.lbl_soporte = new System.Windows.Forms.Label();
            this.cmd_buscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_scanning = new System.Windows.Forms.TextBox();
            this.tab_stock = new System.Windows.Forms.TabPage();
            this.lbl_sector = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmd_grabar = new System.Windows.Forms.Button();
            this.cmd_negativo = new System.Windows.Forms.Button();
            this.lbl_cantidad_anterior = new System.Windows.Forms.Label();
            this.lbl_pesable = new System.Windows.Forms.Label();
            this.txt_cantidad = new System.Windows.Forms.TextBox();
            this.lbl_costo = new System.Windows.Forms.Label();
            this.lbl_detalle = new System.Windows.Forms.Label();
            this.lbl_descripcion = new System.Windows.Forms.Label();
            this.lbl_scanning = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tab_padre.SuspendLayout();
            this.tab_ubicacion.SuspendLayout();
            this.tab_lecturas.SuspendLayout();
            this.pnl_mensajes.SuspendLayout();
            this.tab_stock.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnu_archivo);
            this.mainMenu1.MenuItems.Add(this.mnu_lecturas);
            this.mainMenu1.MenuItems.Add(this.mnu_cuenta_lecturas);
            // 
            // mnu_archivo
            // 
            this.mnu_archivo.MenuItems.Add(this.mnu_limpiar);
            this.mnu_archivo.MenuItems.Add(this.mnu_salir);
            this.mnu_archivo.Text = "Archivo";
            // 
            // mnu_limpiar
            // 
            this.mnu_limpiar.Text = "Limpiar Datos";
            this.mnu_limpiar.Click += new System.EventHandler(this.mnu_limpiar_Click);
            // 
            // mnu_salir
            // 
            this.mnu_salir.Text = "Salir";
            this.mnu_salir.Click += new System.EventHandler(this.mnu_salir_Click);
            // 
            // mnu_lecturas
            // 
            this.mnu_lecturas.MenuItems.Add(this.mnu_generar_archivo);
            this.mnu_lecturas.Text = "Lecturas";
            // 
            // mnu_generar_archivo
            // 
            this.mnu_generar_archivo.Text = "Generar Archivo";
            this.mnu_generar_archivo.Click += new System.EventHandler(this.mnu_generar_archivo_Click);
            // 
            // mnu_cuenta_lecturas
            // 
            this.mnu_cuenta_lecturas.Text = "0";
            // 
            // tab_padre
            // 
            this.tab_padre.Controls.Add(this.tab_ubicacion);
            this.tab_padre.Controls.Add(this.tab_lecturas);
            this.tab_padre.Controls.Add(this.tab_stock);
            this.tab_padre.Location = new System.Drawing.Point(0, 39);
            this.tab_padre.Name = "tab_padre";
            this.tab_padre.SelectedIndex = 0;
            this.tab_padre.Size = new System.Drawing.Size(238, 239);
            this.tab_padre.TabIndex = 10;
            // 
            // tab_ubicacion
            // 
            this.tab_ubicacion.Controls.Add(this.num_conteo);
            this.tab_ubicacion.Controls.Add(this.label12);
            this.tab_ubicacion.Controls.Add(this.cmb_locaciones);
            this.tab_ubicacion.Controls.Add(this.label11);
            this.tab_ubicacion.Controls.Add(this.cmd_hecho);
            this.tab_ubicacion.Controls.Add(this.num_nro_soporte);
            this.tab_ubicacion.Controls.Add(this.cmb_letras);
            this.tab_ubicacion.Controls.Add(this.num_metro);
            this.tab_ubicacion.Controls.Add(this.label10);
            this.tab_ubicacion.Controls.Add(this.num_nivel);
            this.tab_ubicacion.Controls.Add(this.label9);
            this.tab_ubicacion.Controls.Add(this.lbl_version);
            this.tab_ubicacion.Controls.Add(this.label2);
            this.tab_ubicacion.Controls.Add(this.cmb_soportes);
            this.tab_ubicacion.Location = new System.Drawing.Point(4, 25);
            this.tab_ubicacion.Name = "tab_ubicacion";
            this.tab_ubicacion.Size = new System.Drawing.Size(230, 210);
            this.tab_ubicacion.Text = "Ubicacion";
            // 
            // num_conteo
            // 
            this.num_conteo.Location = new System.Drawing.Point(3, 25);
            this.num_conteo.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.num_conteo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_conteo.Name = "num_conteo";
            this.num_conteo.Size = new System.Drawing.Size(53, 24);
            this.num_conteo.TabIndex = 3;
            this.num_conteo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_conteo.GotFocus += new System.EventHandler(this.num_conteo_GotFocus);
            this.num_conteo.LostFocus += new System.EventHandler(this.num_conteo_LostFocus);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(2, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 23);
            this.label12.Text = "Conteo :";
            // 
            // cmb_locaciones
            // 
            this.cmb_locaciones.Location = new System.Drawing.Point(58, 25);
            this.cmb_locaciones.Name = "cmb_locaciones";
            this.cmb_locaciones.Size = new System.Drawing.Size(166, 23);
            this.cmb_locaciones.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(59, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 15);
            this.label11.Text = "Locación : ";
            // 
            // cmd_hecho
            // 
            this.cmd_hecho.Location = new System.Drawing.Point(15, 125);
            this.cmd_hecho.Name = "cmd_hecho";
            this.cmd_hecho.Size = new System.Drawing.Size(194, 52);
            this.cmd_hecho.TabIndex = 8;
            this.cmd_hecho.Text = "Hecho";
            this.cmd_hecho.Click += new System.EventHandler(this.cmd_hecho_Click);
            // 
            // num_nro_soporte
            // 
            this.num_nro_soporte.Location = new System.Drawing.Point(171, 66);
            this.num_nro_soporte.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_nro_soporte.Name = "num_nro_soporte";
            this.num_nro_soporte.Size = new System.Drawing.Size(53, 24);
            this.num_nro_soporte.TabIndex = 5;
            this.num_nro_soporte.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_nro_soporte.ValueChanged += new System.EventHandler(this.num_nro_soporte_ValueChanged);
            // 
            // cmb_letras
            // 
            this.cmb_letras.Location = new System.Drawing.Point(197, 193);
            this.cmb_letras.Name = "cmb_letras";
            this.cmb_letras.Size = new System.Drawing.Size(24, 23);
            this.cmb_letras.TabIndex = 5;
            this.cmb_letras.Visible = false;
            this.cmb_letras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_letras_KeyDown);
            // 
            // num_metro
            // 
            this.num_metro.Location = new System.Drawing.Point(45, 95);
            this.num_metro.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.num_metro.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_metro.Name = "num_metro";
            this.num_metro.Size = new System.Drawing.Size(56, 24);
            this.num_metro.TabIndex = 6;
            this.num_metro.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_metro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.num_metro_KeyDown);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 23);
            this.label10.Text = "Metro :";
            // 
            // num_nivel
            // 
            this.num_nivel.Location = new System.Drawing.Point(153, 96);
            this.num_nivel.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.num_nivel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_nivel.Name = "num_nivel";
            this.num_nivel.Size = new System.Drawing.Size(56, 24);
            this.num_nivel.TabIndex = 7;
            this.num_nivel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_nivel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.num_nivel_KeyDown);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(108, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 23);
            this.label9.Text = "Nivel :";
            // 
            // lbl_version
            // 
            this.lbl_version.Location = new System.Drawing.Point(15, 180);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(194, 23);
            this.lbl_version.Text = "Version";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.Text = "Tipo Soporte : ";
            // 
            // cmb_soportes
            // 
            this.cmb_soportes.Location = new System.Drawing.Point(3, 66);
            this.cmb_soportes.Name = "cmb_soportes";
            this.cmb_soportes.Size = new System.Drawing.Size(166, 23);
            this.cmb_soportes.TabIndex = 4;
            this.cmb_soportes.SelectedIndexChanged += new System.EventHandler(this.cmb_soportes_SelectedIndexChanged);
            this.cmb_soportes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_soportes_KeyDown);
            // 
            // tab_lecturas
            // 
            this.tab_lecturas.Controls.Add(this.pnl_mensajes);
            this.tab_lecturas.Controls.Add(this.lbl_metro_nivel);
            this.tab_lecturas.Controls.Add(this.lbl_locacion);
            this.tab_lecturas.Controls.Add(this.lbl_conteo);
            this.tab_lecturas.Controls.Add(this.lbl_soporte);
            this.tab_lecturas.Controls.Add(this.cmd_buscar);
            this.tab_lecturas.Controls.Add(this.label1);
            this.tab_lecturas.Controls.Add(this.txt_scanning);
            this.tab_lecturas.Location = new System.Drawing.Point(4, 25);
            this.tab_lecturas.Name = "tab_lecturas";
            this.tab_lecturas.Size = new System.Drawing.Size(230, 210);
            this.tab_lecturas.Text = "Lecturas";
            // 
            // pnl_mensajes
            // 
            this.pnl_mensajes.Controls.Add(this.lbl_mensaje);
            this.pnl_mensajes.Location = new System.Drawing.Point(9, 190);
            this.pnl_mensajes.Name = "pnl_mensajes";
            this.pnl_mensajes.Size = new System.Drawing.Size(198, 174);
            this.pnl_mensajes.Visible = false;
            // 
            // lbl_mensaje
            // 
            this.lbl_mensaje.Location = new System.Drawing.Point(12, 30);
            this.lbl_mensaje.Name = "lbl_mensaje";
            this.lbl_mensaje.Size = new System.Drawing.Size(206, 32);
            this.lbl_mensaje.Text = "|";
            // 
            // lbl_metro_nivel
            // 
            this.lbl_metro_nivel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_metro_nivel.Location = new System.Drawing.Point(9, 70);
            this.lbl_metro_nivel.Name = "lbl_metro_nivel";
            this.lbl_metro_nivel.Size = new System.Drawing.Size(208, 20);
            this.lbl_metro_nivel.Text = "|";
            // 
            // lbl_locacion
            // 
            this.lbl_locacion.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_locacion.Location = new System.Drawing.Point(9, 30);
            this.lbl_locacion.Name = "lbl_locacion";
            this.lbl_locacion.Size = new System.Drawing.Size(208, 20);
            this.lbl_locacion.Text = "|";
            // 
            // lbl_conteo
            // 
            this.lbl_conteo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_conteo.Location = new System.Drawing.Point(9, 10);
            this.lbl_conteo.Name = "lbl_conteo";
            this.lbl_conteo.Size = new System.Drawing.Size(208, 20);
            this.lbl_conteo.Text = "|";
            // 
            // lbl_soporte
            // 
            this.lbl_soporte.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_soporte.Location = new System.Drawing.Point(9, 50);
            this.lbl_soporte.Name = "lbl_soporte";
            this.lbl_soporte.Size = new System.Drawing.Size(208, 20);
            this.lbl_soporte.Text = "|";
            // 
            // cmd_buscar
            // 
            this.cmd_buscar.Enabled = false;
            this.cmd_buscar.Location = new System.Drawing.Point(9, 161);
            this.cmd_buscar.Name = "cmd_buscar";
            this.cmd_buscar.Size = new System.Drawing.Size(208, 43);
            this.cmd_buscar.TabIndex = 1;
            this.cmd_buscar.Text = "Buscar";
            this.cmd_buscar.Click += new System.EventHandler(this.cmd_buscar_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 19);
            this.label1.Text = "Scanning";
            // 
            // txt_scanning
            // 
            this.txt_scanning.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txt_scanning.Location = new System.Drawing.Point(9, 120);
            this.txt_scanning.MaxLength = 15;
            this.txt_scanning.Name = "txt_scanning";
            this.txt_scanning.Size = new System.Drawing.Size(208, 26);
            this.txt_scanning.TabIndex = 0;
            this.txt_scanning.TextChanged += new System.EventHandler(this.txt_scanning_TextChanged);
            this.txt_scanning.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_scanning_KeyDown);
            // 
            // tab_stock
            // 
            this.tab_stock.Controls.Add(this.lbl_sector);
            this.tab_stock.Controls.Add(this.label13);
            this.tab_stock.Controls.Add(this.cmd_grabar);
            this.tab_stock.Controls.Add(this.cmd_negativo);
            this.tab_stock.Controls.Add(this.lbl_cantidad_anterior);
            this.tab_stock.Controls.Add(this.lbl_pesable);
            this.tab_stock.Controls.Add(this.txt_cantidad);
            this.tab_stock.Controls.Add(this.lbl_costo);
            this.tab_stock.Controls.Add(this.lbl_detalle);
            this.tab_stock.Controls.Add(this.lbl_descripcion);
            this.tab_stock.Controls.Add(this.lbl_scanning);
            this.tab_stock.Controls.Add(this.label8);
            this.tab_stock.Controls.Add(this.label7);
            this.tab_stock.Controls.Add(this.label6);
            this.tab_stock.Controls.Add(this.label5);
            this.tab_stock.Controls.Add(this.label4);
            this.tab_stock.Location = new System.Drawing.Point(4, 25);
            this.tab_stock.Name = "tab_stock";
            this.tab_stock.Size = new System.Drawing.Size(230, 210);
            this.tab_stock.Text = "Stock";
            // 
            // lbl_sector
            // 
            this.lbl_sector.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_sector.Location = new System.Drawing.Point(50, 89);
            this.lbl_sector.Name = "lbl_sector";
            this.lbl_sector.Size = new System.Drawing.Size(137, 20);
            this.lbl_sector.Text = "|";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(1, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 20);
            this.label13.Text = "Sector :";
            // 
            // cmd_grabar
            // 
            this.cmd_grabar.Enabled = false;
            this.cmd_grabar.Location = new System.Drawing.Point(40, 173);
            this.cmd_grabar.Name = "cmd_grabar";
            this.cmd_grabar.Size = new System.Drawing.Size(181, 32);
            this.cmd_grabar.TabIndex = 1;
            this.cmd_grabar.Text = "Grabar";
            this.cmd_grabar.Click += new System.EventHandler(this.cmd_grabar_Click);
            // 
            // cmd_negativo
            // 
            this.cmd_negativo.Location = new System.Drawing.Point(6, 173);
            this.cmd_negativo.Name = "cmd_negativo";
            this.cmd_negativo.Size = new System.Drawing.Size(33, 32);
            this.cmd_negativo.TabIndex = 11;
            this.cmd_negativo.Text = "-1";
            this.cmd_negativo.Click += new System.EventHandler(this.cmd_negativo_Click);
            // 
            // lbl_cantidad_anterior
            // 
            this.lbl_cantidad_anterior.Location = new System.Drawing.Point(111, 208);
            this.lbl_cantidad_anterior.Name = "lbl_cantidad_anterior";
            this.lbl_cantidad_anterior.Size = new System.Drawing.Size(102, 17);
            this.lbl_cantidad_anterior.Text = "lbl_cantidad_anterior";
            this.lbl_cantidad_anterior.Visible = false;
            // 
            // lbl_pesable
            // 
            this.lbl_pesable.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_pesable.Location = new System.Drawing.Point(3, 205);
            this.lbl_pesable.Name = "lbl_pesable";
            this.lbl_pesable.Size = new System.Drawing.Size(91, 20);
            this.lbl_pesable.Text = "lbl_pesable";
            this.lbl_pesable.Visible = false;
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txt_cantidad.Location = new System.Drawing.Point(79, 132);
            this.txt_cantidad.MaxLength = 6;
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(134, 29);
            this.txt_cantidad.TabIndex = 0;
            this.txt_cantidad.Text = "1";
            this.txt_cantidad.TextChanged += new System.EventHandler(this.txt_cantidad_TextChanged);
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // lbl_costo
            // 
            this.lbl_costo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_costo.Location = new System.Drawing.Point(76, 225);
            this.lbl_costo.Name = "lbl_costo";
            this.lbl_costo.Size = new System.Drawing.Size(137, 20);
            this.lbl_costo.Text = "|";
            this.lbl_costo.Visible = false;
            // 
            // lbl_detalle
            // 
            this.lbl_detalle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_detalle.Location = new System.Drawing.Point(50, 69);
            this.lbl_detalle.Name = "lbl_detalle";
            this.lbl_detalle.Size = new System.Drawing.Size(137, 20);
            this.lbl_detalle.Text = "|";
            // 
            // lbl_descripcion
            // 
            this.lbl_descripcion.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_descripcion.Location = new System.Drawing.Point(6, 45);
            this.lbl_descripcion.Name = "lbl_descripcion";
            this.lbl_descripcion.Size = new System.Drawing.Size(215, 19);
            this.lbl_descripcion.Text = "|";
            // 
            // lbl_scanning
            // 
            this.lbl_scanning.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_scanning.Location = new System.Drawing.Point(61, 4);
            this.lbl_scanning.Name = "lbl_scanning";
            this.lbl_scanning.Size = new System.Drawing.Size(139, 21);
            this.lbl_scanning.Text = "|";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(13, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 20);
            this.label8.Text = "Cantidad :";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(29, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 20);
            this.label7.Text = "Precio :";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(-1, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.Text = "Detalle :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(-1, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.Text = "Descripcion :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 21);
            this.label4.Text = "Scanning :";
            // 
            // frm_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 278);
            this.Controls.Add(this.tab_padre);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "frm_principal";
            this.Text = "InveGlobal";
            this.Load += new System.EventHandler(this.frm_principal_Load);
            this.tab_padre.ResumeLayout(false);
            this.tab_ubicacion.ResumeLayout(false);
            this.tab_lecturas.ResumeLayout(false);
            this.pnl_mensajes.ResumeLayout(false);
            this.tab_stock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnu_archivo;
        private System.Windows.Forms.MenuItem mnu_limpiar;
        private System.Windows.Forms.MenuItem mnu_salir;
        private System.Windows.Forms.TabControl tab_padre;
        private System.Windows.Forms.TabPage tab_ubicacion;
        private System.Windows.Forms.Button cmd_hecho;
        private System.Windows.Forms.ComboBox cmb_letras;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_soportes;
        private System.Windows.Forms.Button cmd_buscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_scanning;
        private System.Windows.Forms.TabPage tab_stock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_costo;
        private System.Windows.Forms.Label lbl_detalle;
        private System.Windows.Forms.Label lbl_descripcion;
        private System.Windows.Forms.Label lbl_scanning;
        private System.Windows.Forms.TextBox txt_cantidad;
        private System.Windows.Forms.Button cmd_grabar;
        private System.Windows.Forms.TabPage tab_lecturas;
        private System.Windows.Forms.MenuItem mnu_lecturas;
        private System.Windows.Forms.MenuItem mnu_generar_archivo;
        private System.Windows.Forms.Panel pnl_mensajes;
        private System.Windows.Forms.Label lbl_mensaje;
        private System.Windows.Forms.Label lbl_soporte;
        private System.Windows.Forms.NumericUpDown num_nivel;
        private System.Windows.Forms.NumericUpDown num_metro;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_pesable;
        private System.Windows.Forms.NumericUpDown num_nro_soporte;
        private System.Windows.Forms.Label lbl_cantidad_anterior;
        private System.Windows.Forms.ComboBox cmb_locaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmd_negativo;
        private System.Windows.Forms.NumericUpDown num_conteo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MenuItem mnu_cuenta_lecturas;
        private System.Windows.Forms.Label lbl_sector;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbl_metro_nivel;
        private System.Windows.Forms.Label lbl_locacion;
        private System.Windows.Forms.Label lbl_conteo;
    }
}

