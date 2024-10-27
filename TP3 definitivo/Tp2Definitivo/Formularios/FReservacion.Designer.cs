namespace Tp2Definitivo
{
    partial class FReservacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FReservacion));
            this.cbDni = new System.Windows.Forms.ComboBox();
            this.cbNro = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Ingreso = new System.Windows.Forms.MonthCalendar();
            this.Egreso = new System.Windows.Forms.MonthCalendar();
            this.btnReservar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbCliente = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imagen1 = new System.Windows.Forms.PictureBox();
            this.imagen3 = new System.Windows.Forms.PictureBox();
            this.imagen2 = new System.Windows.Forms.PictureBox();
            this.imagen4 = new System.Windows.Forms.PictureBox();
            this.lbProp = new System.Windows.Forms.ListBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numPasajeros = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.rbHotel = new System.Windows.Forms.RadioButton();
            this.rbCasa = new System.Windows.Forms.RadioButton();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cbPasajeros = new System.Windows.Forms.CheckBox();
            this.cbDisponibilidad = new System.Windows.Forms.CheckBox();
            this.cbLugar = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagen1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen4)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPasajeros)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDni
            // 
            this.cbDni.BackColor = System.Drawing.Color.Azure;
            this.cbDni.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDni.FormattingEnabled = true;
            this.cbDni.Location = new System.Drawing.Point(96, 20);
            this.cbDni.Name = "cbDni";
            this.cbDni.Size = new System.Drawing.Size(104, 21);
            this.cbDni.TabIndex = 0;
            this.cbDni.SelectedIndexChanged += new System.EventHandler(this.cbDni_SelectedIndexChanged);
            // 
            // cbNro
            // 
            this.cbNro.BackColor = System.Drawing.Color.Azure;
            this.cbNro.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNro.FormattingEnabled = true;
            this.cbNro.Location = new System.Drawing.Point(96, 20);
            this.cbNro.Name = "cbNro";
            this.cbNro.Size = new System.Drawing.Size(104, 21);
            this.cbNro.TabIndex = 1;
            this.cbNro.SelectedIndexChanged += new System.EventHandler(this.cbNro_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightBlue;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DNI:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightBlue;
            this.label2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Propiedad:";
            // 
            // Ingreso
            // 
            this.Ingreso.BackColor = System.Drawing.Color.Azure;
            this.Ingreso.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ingreso.Location = new System.Drawing.Point(12, 30);
            this.Ingreso.Name = "Ingreso";
            this.Ingreso.TabIndex = 5;
            // 
            // Egreso
            // 
            this.Egreso.BackColor = System.Drawing.Color.Azure;
            this.Egreso.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Egreso.Location = new System.Drawing.Point(12, 214);
            this.Egreso.Name = "Egreso";
            this.Egreso.TabIndex = 6;
            // 
            // btnReservar
            // 
            this.btnReservar.BackColor = System.Drawing.Color.Azure;
            this.btnReservar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnReservar.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnReservar.Location = new System.Drawing.Point(12, 365);
            this.btnReservar.Name = "btnReservar";
            this.btnReservar.Size = new System.Drawing.Size(75, 31);
            this.btnReservar.TabIndex = 7;
            this.btnReservar.Text = "Reservar";
            this.btnReservar.UseVisualStyleBackColor = false;
            this.btnReservar.Click += new System.EventHandler(this.btnReservar_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Azure;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(147, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LightBlue;
            this.label4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fecha Ingreso";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightBlue;
            this.label5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(69, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fecha Egreso";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbCliente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbDni);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 87);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cliente";
            // 
            // lbCliente
            // 
            this.lbCliente.AutoSize = true;
            this.lbCliente.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCliente.Location = new System.Drawing.Point(6, 59);
            this.lbCliente.Name = "lbCliente";
            this.lbCliente.Size = new System.Drawing.Size(61, 13);
            this.lbCliente.TabIndex = 3;
            this.lbCliente.Text = "lbCliente";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imagen1);
            this.groupBox2.Controls.Add(this.imagen3);
            this.groupBox2.Controls.Add(this.imagen2);
            this.groupBox2.Controls.Add(this.imagen4);
            this.groupBox2.Controls.Add(this.lbProp);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbNro);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(228, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 384);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Propiedad";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // imagen1
            // 
            this.imagen1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imagen1.Image = ((System.Drawing.Image)(resources.GetObject("imagen1.Image")));
            this.imagen1.Location = new System.Drawing.Point(219, 18);
            this.imagen1.Margin = new System.Windows.Forms.Padding(2);
            this.imagen1.Name = "imagen1";
            this.imagen1.Size = new System.Drawing.Size(73, 76);
            this.imagen1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagen1.TabIndex = 16;
            this.imagen1.TabStop = false;
            // 
            // imagen3
            // 
            this.imagen3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imagen3.Image = ((System.Drawing.Image)(resources.GetObject("imagen3.Image")));
            this.imagen3.Location = new System.Drawing.Point(219, 201);
            this.imagen3.Margin = new System.Windows.Forms.Padding(2);
            this.imagen3.Name = "imagen3";
            this.imagen3.Size = new System.Drawing.Size(73, 76);
            this.imagen3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagen3.TabIndex = 17;
            this.imagen3.TabStop = false;
            // 
            // imagen2
            // 
            this.imagen2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imagen2.Image = ((System.Drawing.Image)(resources.GetObject("imagen2.Image")));
            this.imagen2.Location = new System.Drawing.Point(219, 111);
            this.imagen2.Margin = new System.Windows.Forms.Padding(2);
            this.imagen2.Name = "imagen2";
            this.imagen2.Size = new System.Drawing.Size(73, 76);
            this.imagen2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagen2.TabIndex = 18;
            this.imagen2.TabStop = false;
            // 
            // imagen4
            // 
            this.imagen4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imagen4.Image = ((System.Drawing.Image)(resources.GetObject("imagen4.Image")));
            this.imagen4.Location = new System.Drawing.Point(219, 290);
            this.imagen4.Margin = new System.Windows.Forms.Padding(2);
            this.imagen4.Name = "imagen4";
            this.imagen4.Size = new System.Drawing.Size(73, 76);
            this.imagen4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagen4.TabIndex = 19;
            this.imagen4.TabStop = false;
            // 
            // lbProp
            // 
            this.lbProp.BackColor = System.Drawing.Color.Azure;
            this.lbProp.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProp.FormattingEnabled = true;
            this.lbProp.ItemHeight = 14;
            this.lbProp.Location = new System.Drawing.Point(9, 78);
            this.lbProp.Name = "lbProp";
            this.lbProp.Size = new System.Drawing.Size(192, 284);
            this.lbProp.TabIndex = 15;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.Azure;
            this.btnFiltrar.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnFiltrar.Location = new System.Drawing.Point(67, 226);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(63, 21);
            this.btnFiltrar.TabIndex = 14;
            this.btnFiltrar.Text = "Buscar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Ingreso);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.Egreso);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox3.Location = new System.Drawing.Point(538, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(211, 384);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Fecha";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.numPasajeros);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.dtpHasta);
            this.groupBox4.Controls.Add(this.rbHotel);
            this.groupBox4.Controls.Add(this.rbCasa);
            this.groupBox4.Controls.Add(this.dtpDesde);
            this.groupBox4.Controls.Add(this.cbPasajeros);
            this.groupBox4.Controls.Add(this.cbDisponibilidad);
            this.groupBox4.Controls.Add(this.cbLugar);
            this.groupBox4.Controls.Add(this.btnFiltrar);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox4.Location = new System.Drawing.Point(12, 105);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 253);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filtro";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Pasajeros:";
            // 
            // numPasajeros
            // 
            this.numPasajeros.BackColor = System.Drawing.Color.Azure;
            this.numPasajeros.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPasajeros.Location = new System.Drawing.Point(96, 82);
            this.numPasajeros.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numPasajeros.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPasajeros.Name = "numPasajeros";
            this.numPasajeros.Size = new System.Drawing.Size(94, 20);
            this.numPasajeros.TabIndex = 22;
            this.numPasajeros.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Desde:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Location = new System.Drawing.Point(6, 196);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(195, 20);
            this.dtpHasta.TabIndex = 20;
            // 
            // rbHotel
            // 
            this.rbHotel.AutoSize = true;
            this.rbHotel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHotel.Location = new System.Drawing.Point(67, 18);
            this.rbHotel.Name = "rbHotel";
            this.rbHotel.Size = new System.Drawing.Size(55, 17);
            this.rbHotel.TabIndex = 19;
            this.rbHotel.TabStop = true;
            this.rbHotel.Text = "Hotel";
            this.rbHotel.UseVisualStyleBackColor = true;
            // 
            // rbCasa
            // 
            this.rbCasa.AutoSize = true;
            this.rbCasa.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCasa.Location = new System.Drawing.Point(141, 18);
            this.rbCasa.Name = "rbCasa";
            this.rbCasa.Size = new System.Drawing.Size(49, 17);
            this.rbCasa.TabIndex = 18;
            this.rbCasa.TabStop = true;
            this.rbCasa.Text = "Casa";
            this.rbCasa.UseVisualStyleBackColor = true;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Location = new System.Drawing.Point(6, 157);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(195, 20);
            this.dtpDesde.TabIndex = 16;
            // 
            // cbPasajeros
            // 
            this.cbPasajeros.AutoSize = true;
            this.cbPasajeros.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPasajeros.Location = new System.Drawing.Point(6, 55);
            this.cbPasajeros.Name = "cbPasajeros";
            this.cbPasajeros.Size = new System.Drawing.Size(80, 17);
            this.cbPasajeros.TabIndex = 17;
            this.cbPasajeros.Text = "Pasajeros";
            this.cbPasajeros.UseVisualStyleBackColor = true;
            this.cbPasajeros.CheckedChanged += new System.EventHandler(this.cbPasajeros_CheckedChanged);
            // 
            // cbDisponibilidad
            // 
            this.cbDisponibilidad.AutoSize = true;
            this.cbDisponibilidad.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDisponibilidad.Location = new System.Drawing.Point(6, 121);
            this.cbDisponibilidad.Name = "cbDisponibilidad";
            this.cbDisponibilidad.Size = new System.Drawing.Size(110, 17);
            this.cbDisponibilidad.TabIndex = 16;
            this.cbDisponibilidad.Text = "Disponibilidad";
            this.cbDisponibilidad.UseVisualStyleBackColor = true;
            this.cbDisponibilidad.CheckedChanged += new System.EventHandler(this.cbDisponibilidad_CheckedChanged);
            // 
            // cbLugar
            // 
            this.cbLugar.AutoSize = true;
            this.cbLugar.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLugar.Location = new System.Drawing.Point(6, 18);
            this.cbLugar.Name = "cbLugar";
            this.cbLugar.Size = new System.Drawing.Size(56, 17);
            this.cbLugar.TabIndex = 15;
            this.cbLugar.Text = "Lugar";
            this.cbLugar.UseVisualStyleBackColor = true;
            this.cbLugar.CheckedChanged += new System.EventHandler(this.cbLugar_CheckedChanged);
            // 
            // FReservacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(759, 403);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReservar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FReservacion";
            this.Text = "Reservaciones";
            this.Load += new System.EventHandler(this.Reservaciones_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagen1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen4)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPasajeros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbDni;
        public System.Windows.Forms.ComboBox cbNro;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.MonthCalendar Ingreso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btnReservar;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbCliente;
        public System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.ListBox lbProp;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbPasajeros;
        private System.Windows.Forms.CheckBox cbDisponibilidad;
        private System.Windows.Forms.CheckBox cbLugar;
        public System.Windows.Forms.MonthCalendar Egreso;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numPasajeros;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.RadioButton rbHotel;
        private System.Windows.Forms.RadioButton rbCasa;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        public System.Windows.Forms.PictureBox imagen1;
        public System.Windows.Forms.PictureBox imagen3;
        public System.Windows.Forms.PictureBox imagen2;
        public System.Windows.Forms.PictureBox imagen4;
    }
}