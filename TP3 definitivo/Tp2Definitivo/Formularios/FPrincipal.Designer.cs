namespace Tp2Definitivo
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ltbxClientes = new System.Windows.Forms.ListBox();
            this.ltbxPropiedades = new System.Windows.Forms.ListBox();
            this.btnAgregarCasa = new System.Windows.Forms.Button();
            this.btnAgregarHotel = new System.Windows.Forms.Button();
            this.btnAgregarCliente = new System.Windows.Forms.Button();
            this.btnAgregarReserva = new System.Windows.Forms.Button();
            this.btnEditarPropiedad = new System.Windows.Forms.Button();
            this.btnEliminarPropiedad = new System.Windows.Forms.Button();
            this.btnModificarReserva = new System.Windows.Forms.Button();
            this.btnEliminarReserva = new System.Windows.Forms.Button();
            this.btnVerCalendario = new System.Windows.Forms.Button();
            this.btnVerTicket = new System.Windows.Forms.Button();
            this.dgvPropiedades = new System.Windows.Forms.DataGridView();
            this.Nro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Propiedad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dirección = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Servicios = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lbServicios = new System.Windows.Forms.ListBox();
            this.dgvReservas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropiedades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).BeginInit();
            this.SuspendLayout();
            // 
            // ltbxClientes
            // 
            this.ltbxClientes.FormattingEnabled = true;
            this.ltbxClientes.Location = new System.Drawing.Point(306, 429);
            this.ltbxClientes.Margin = new System.Windows.Forms.Padding(2);
            this.ltbxClientes.Name = "ltbxClientes";
            this.ltbxClientes.Size = new System.Drawing.Size(206, 95);
            this.ltbxClientes.TabIndex = 1;
            // 
            // ltbxPropiedades
            // 
            this.ltbxPropiedades.FormattingEnabled = true;
            this.ltbxPropiedades.Location = new System.Drawing.Point(532, 429);
            this.ltbxPropiedades.Margin = new System.Windows.Forms.Padding(2);
            this.ltbxPropiedades.Name = "ltbxPropiedades";
            this.ltbxPropiedades.Size = new System.Drawing.Size(212, 95);
            this.ltbxPropiedades.TabIndex = 2;
            // 
            // btnAgregarCasa
            // 
            this.btnAgregarCasa.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCasa.Location = new System.Drawing.Point(12, 12);
            this.btnAgregarCasa.Name = "btnAgregarCasa";
            this.btnAgregarCasa.Size = new System.Drawing.Size(75, 36);
            this.btnAgregarCasa.TabIndex = 3;
            this.btnAgregarCasa.Text = "Agregar Casa";
            this.btnAgregarCasa.UseVisualStyleBackColor = true;
            this.btnAgregarCasa.Click += new System.EventHandler(this.btnAgregarCasa_Click);
            // 
            // btnAgregarHotel
            // 
            this.btnAgregarHotel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarHotel.Location = new System.Drawing.Point(12, 54);
            this.btnAgregarHotel.Name = "btnAgregarHotel";
            this.btnAgregarHotel.Size = new System.Drawing.Size(75, 36);
            this.btnAgregarHotel.TabIndex = 4;
            this.btnAgregarHotel.Text = "Agregar Hotel";
            this.btnAgregarHotel.UseVisualStyleBackColor = true;
            this.btnAgregarHotel.Click += new System.EventHandler(this.btnAgregarHotel_Click);
            // 
            // btnAgregarCliente
            // 
            this.btnAgregarCliente.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCliente.Location = new System.Drawing.Point(12, 214);
            this.btnAgregarCliente.Name = "btnAgregarCliente";
            this.btnAgregarCliente.Size = new System.Drawing.Size(75, 36);
            this.btnAgregarCliente.TabIndex = 5;
            this.btnAgregarCliente.Text = "Agregar Cliente";
            this.btnAgregarCliente.UseVisualStyleBackColor = true;
            this.btnAgregarCliente.Click += new System.EventHandler(this.btnAgregarCliente_Click);
            // 
            // btnAgregarReserva
            // 
            this.btnAgregarReserva.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarReserva.Location = new System.Drawing.Point(12, 291);
            this.btnAgregarReserva.Name = "btnAgregarReserva";
            this.btnAgregarReserva.Size = new System.Drawing.Size(75, 36);
            this.btnAgregarReserva.TabIndex = 6;
            this.btnAgregarReserva.Text = "Agregar Reserva";
            this.btnAgregarReserva.UseVisualStyleBackColor = true;
            this.btnAgregarReserva.Click += new System.EventHandler(this.btnAgregarReserva_Click);
            // 
            // btnEditarPropiedad
            // 
            this.btnEditarPropiedad.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarPropiedad.Location = new System.Drawing.Point(12, 113);
            this.btnEditarPropiedad.Name = "btnEditarPropiedad";
            this.btnEditarPropiedad.Size = new System.Drawing.Size(75, 36);
            this.btnEditarPropiedad.TabIndex = 7;
            this.btnEditarPropiedad.Text = "Ver/Editar Propiedad";
            this.btnEditarPropiedad.UseVisualStyleBackColor = true;
            this.btnEditarPropiedad.Click += new System.EventHandler(this.btnEditarPropiedad_Click);
            // 
            // btnEliminarPropiedad
            // 
            this.btnEliminarPropiedad.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarPropiedad.Location = new System.Drawing.Point(12, 155);
            this.btnEliminarPropiedad.Name = "btnEliminarPropiedad";
            this.btnEliminarPropiedad.Size = new System.Drawing.Size(75, 36);
            this.btnEliminarPropiedad.TabIndex = 8;
            this.btnEliminarPropiedad.Text = "Eliminar Propiedad";
            this.btnEliminarPropiedad.UseVisualStyleBackColor = true;
            this.btnEliminarPropiedad.Click += new System.EventHandler(this.btnEliminarPropiedad_Click);
            // 
            // btnModificarReserva
            // 
            this.btnModificarReserva.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarReserva.Location = new System.Drawing.Point(12, 333);
            this.btnModificarReserva.Name = "btnModificarReserva";
            this.btnModificarReserva.Size = new System.Drawing.Size(75, 36);
            this.btnModificarReserva.TabIndex = 9;
            this.btnModificarReserva.Text = "Modificar Reserva";
            this.btnModificarReserva.UseVisualStyleBackColor = true;
            this.btnModificarReserva.Click += new System.EventHandler(this.btnModificarReserva_Click);
            // 
            // btnEliminarReserva
            // 
            this.btnEliminarReserva.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarReserva.Location = new System.Drawing.Point(12, 375);
            this.btnEliminarReserva.Name = "btnEliminarReserva";
            this.btnEliminarReserva.Size = new System.Drawing.Size(75, 36);
            this.btnEliminarReserva.TabIndex = 10;
            this.btnEliminarReserva.Text = "Eliminar Reserva";
            this.btnEliminarReserva.UseVisualStyleBackColor = true;
            this.btnEliminarReserva.Click += new System.EventHandler(this.btnEliminarReserva_Click);
            // 
            // btnVerCalendario
            // 
            this.btnVerCalendario.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerCalendario.Location = new System.Drawing.Point(12, 485);
            this.btnVerCalendario.Name = "btnVerCalendario";
            this.btnVerCalendario.Size = new System.Drawing.Size(75, 36);
            this.btnVerCalendario.TabIndex = 11;
            this.btnVerCalendario.Text = "Ver Calendario";
            this.btnVerCalendario.UseVisualStyleBackColor = true;
            this.btnVerCalendario.Click += new System.EventHandler(this.btnVerCalendario_Click);
            // 
            // btnVerTicket
            // 
            this.btnVerTicket.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerTicket.Location = new System.Drawing.Point(12, 443);
            this.btnVerTicket.Name = "btnVerTicket";
            this.btnVerTicket.Size = new System.Drawing.Size(75, 36);
            this.btnVerTicket.TabIndex = 12;
            this.btnVerTicket.Text = "Ver Ticket";
            this.btnVerTicket.UseVisualStyleBackColor = true;
            this.btnVerTicket.Click += new System.EventHandler(this.btnVerTicket_Click);
            // 
            // dgvPropiedades
            // 
            this.dgvPropiedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropiedades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nro,
            this.Propiedad,
            this.Dirección,
            this.Precio,
            this.Servicios});
            this.dgvPropiedades.Location = new System.Drawing.Point(93, 28);
            this.dgvPropiedades.Name = "dgvPropiedades";
            this.dgvPropiedades.Size = new System.Drawing.Size(651, 179);
            this.dgvPropiedades.TabIndex = 13;
            // 
            // Nro
            // 
            this.Nro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nro.HeaderText = "Nro";
            this.Nro.Name = "Nro";
            // 
            // Propiedad
            // 
            this.Propiedad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Propiedad.HeaderText = "Propiedad";
            this.Propiedad.Name = "Propiedad";
            // 
            // Dirección
            // 
            this.Dirección.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Dirección.HeaderText = "Dirección";
            this.Dirección.Name = "Dirección";
            // 
            // Precio
            // 
            this.Precio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Precio.HeaderText = "Precio por día";
            this.Precio.Name = "Precio";
            // 
            // Servicios
            // 
            this.Servicios.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Servicios.HeaderText = "Servicios";
            this.Servicios.Name = "Servicios";
            this.Servicios.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Servicios.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // lbServicios
            // 
            this.lbServicios.FormattingEnabled = true;
            this.lbServicios.Location = new System.Drawing.Point(93, 429);
            this.lbServicios.Margin = new System.Windows.Forms.Padding(2);
            this.lbServicios.Name = "lbServicios";
            this.lbServicios.Size = new System.Drawing.Size(197, 95);
            this.lbServicios.TabIndex = 14;
            // 
            // dgvReservas
            // 
            this.dgvReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservas.Location = new System.Drawing.Point(93, 232);
            this.dgvReservas.Name = "dgvReservas";
            this.dgvReservas.Size = new System.Drawing.Size(651, 179);
            this.dgvReservas.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Propiedades:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Reservas:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(756, 546);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvReservas);
            this.Controls.Add(this.lbServicios);
            this.Controls.Add(this.dgvPropiedades);
            this.Controls.Add(this.btnVerTicket);
            this.Controls.Add(this.btnVerCalendario);
            this.Controls.Add(this.btnEliminarReserva);
            this.Controls.Add(this.btnModificarReserva);
            this.Controls.Add(this.btnEliminarPropiedad);
            this.Controls.Add(this.btnEditarPropiedad);
            this.Controls.Add(this.btnAgregarReserva);
            this.Controls.Add(this.btnAgregarCliente);
            this.Controls.Add(this.btnAgregarHotel);
            this.Controls.Add(this.btnAgregarCasa);
            this.Controls.Add(this.ltbxPropiedades);
            this.Controls.Add(this.ltbxClientes);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropiedades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox ltbxClientes;
        private System.Windows.Forms.ListBox ltbxPropiedades;
        private System.Windows.Forms.Button btnAgregarCasa;
        private System.Windows.Forms.Button btnAgregarHotel;
        private System.Windows.Forms.Button btnAgregarCliente;
        private System.Windows.Forms.Button btnAgregarReserva;
        private System.Windows.Forms.Button btnEditarPropiedad;
        private System.Windows.Forms.Button btnEliminarPropiedad;
        private System.Windows.Forms.Button btnModificarReserva;
        private System.Windows.Forms.Button btnEliminarReserva;
        private System.Windows.Forms.Button btnVerCalendario;
        private System.Windows.Forms.Button btnVerTicket;
        private System.Windows.Forms.DataGridView dgvPropiedades;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dirección;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewComboBoxColumn Servicios;
        private System.Windows.Forms.ListBox lbServicios;
        private System.Windows.Forms.DataGridView dgvReservas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

