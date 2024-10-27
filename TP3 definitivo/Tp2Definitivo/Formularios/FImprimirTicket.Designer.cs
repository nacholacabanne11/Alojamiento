namespace Tp2Definitivo.Formularios
{
    partial class FImprimirTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FImprimirTicket));
            this.dgvTickets = new System.Windows.Forms.DataGridView();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.imprimirDocumento = new System.Drawing.Printing.PrintDocument();
            this.vistaPrevia = new System.Windows.Forms.PrintPreviewDialog();
            this.lbTicket = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.documento = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTickets
            // 
            this.dgvTickets.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTickets.Location = new System.Drawing.Point(12, 12);
            this.dgvTickets.Name = "dgvTickets";
            this.dgvTickets.Size = new System.Drawing.Size(776, 244);
            this.dgvTickets.TabIndex = 0;
            this.dgvTickets.SelectionChanged += new System.EventHandler(this.dgvTickets_SelectionChanged);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Azure;
            this.btnImprimir.Location = new System.Drawing.Point(921, 220);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(64, 36);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Azure;
            this.button1.Location = new System.Drawing.Point(861, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Vista previa";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imprimirDocumento
            // 
            this.imprimirDocumento.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.imprimirDocumento_BeginPrint);
            this.imprimirDocumento.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.imprimirDocumento_EndPrint);
            this.imprimirDocumento.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.imprimirDocumento_PrintPage);
            // 
            // vistaPrevia
            // 
            this.vistaPrevia.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.vistaPrevia.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.vistaPrevia.ClientSize = new System.Drawing.Size(400, 300);
            this.vistaPrevia.Enabled = true;
            this.vistaPrevia.Icon = ((System.Drawing.Icon)(resources.GetObject("vistaPrevia.Icon")));
            this.vistaPrevia.Name = "vistaPrevia";
            this.vistaPrevia.Visible = false;
            // 
            // lbTicket
            // 
            this.lbTicket.BackColor = System.Drawing.Color.Azure;
            this.lbTicket.FormattingEnabled = true;
            this.lbTicket.Location = new System.Drawing.Point(794, 12);
            this.lbTicket.Name = "lbTicket";
            this.lbTicket.Size = new System.Drawing.Size(191, 199);
            this.lbTicket.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Azure;
            this.pictureBox1.Location = new System.Drawing.Point(794, 220);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 36);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // FImprimirTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(997, 267);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbTicket);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.dgvTickets);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FImprimirTicket";
            this.Text = "FImprimirTicket";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTickets;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button button1;
        private System.Drawing.Printing.PrintDocument imprimirDocumento;
        private System.Windows.Forms.PrintPreviewDialog vistaPrevia;
        private System.Windows.Forms.ListBox lbTicket;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument documento;
    }
}