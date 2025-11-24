namespace Gerenciamento_De_Chamados
{
    partial class FormRelatorioDetalhado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioDetalhado));
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lbSair = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PctBox_Inicio = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbl_Inicio = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.dgvRelatorio = new System.Windows.Forms.DataGridView();
            this.lblMconta = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Inicio)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.Transparent;
            this.panelSidebar.Controls.Add(this.lblMconta);
            this.panelSidebar.Controls.Add(this.lbSair);
            this.panelSidebar.Controls.Add(this.label3);
            this.panelSidebar.Controls.Add(this.label1);
            this.panelSidebar.Controls.Add(this.PctBox_Inicio);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.panelSidebar.Size = new System.Drawing.Size(296, 910);
            this.panelSidebar.TabIndex = 0;
            // 
            // lbSair
            // 
            this.lbSair.AutoSize = true;
            this.lbSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbSair.Image = global::Gerenciamento_De_Chamados.Properties.Resources.move_item_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.lbSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSair.Location = new System.Drawing.Point(6, 472);
            this.lbSair.Name = "lbSair";
            this.lbSair.Size = new System.Drawing.Size(92, 29);
            this.lbSair.TabIndex = 18;
            this.lbSair.Text = "      Sair";
            this.lbSair.Click += new System.EventHandler(this.lbSair_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = global::Gerenciamento_De_Chamados.Properties.Resources.Suporte;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(6, 419);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 29);
            this.label3.TabIndex = 15;
            this.label3.Text = "      FAQ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::Gerenciamento_De_Chamados.Properties.Resources.menu;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(6, 365);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 29);
            this.label1.TabIndex = 16;
            this.label1.Text = "      Meus Chamados";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // PctBox_Inicio
            // 
            this.PctBox_Inicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.PctBox_Inicio.Image = ((System.Drawing.Image)(resources.GetObject("PctBox_Inicio.Image")));
            this.PctBox_Inicio.Location = new System.Drawing.Point(0, 0);
            this.PctBox_Inicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PctBox_Inicio.Name = "PctBox_Inicio";
            this.PctBox_Inicio.Size = new System.Drawing.Size(283, 267);
            this.PctBox_Inicio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PctBox_Inicio.TabIndex = 11;
            this.PctBox_Inicio.TabStop = false;
            this.PctBox_Inicio.Click += new System.EventHandler(this.PctBox_Inicio_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportarPDF);
            this.panel2.Controls.Add(this.btnVoltar);
            this.panel2.Controls.Add(this.btnImprimir);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(296, 830);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1225, 80);
            this.panel2.TabIndex = 1;
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarPDF.Location = new System.Drawing.Point(845, 17);
            this.btnExportarPDF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(165, 59);
            this.btnExportarPDF.TabIndex = 2;
            this.btnExportarPDF.Text = "Imprimir relatorio em PDF";
            this.btnExportarPDF.UseVisualStyleBackColor = true;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.Location = new System.Drawing.Point(657, 17);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(165, 59);
            this.btnVoltar.TabIndex = 1;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Location = new System.Drawing.Point(1037, 17);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(165, 59);
            this.btnImprimir.TabIndex = 0;
            this.btnImprimir.Text = "Imprimir relatorio em CSV";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.pictureBox4);
            this.panelHeader.Controls.Add(this.lbl_Inicio);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(296, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1225, 126);
            this.panelHeader.TabIndex = 1;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_51dp_000000_FILL0_wght400_GRAD0_opsz48__1_;
            this.pictureBox4.Location = new System.Drawing.Point(1095, 49);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(68, 62);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // lbl_Inicio
            // 
            this.lbl_Inicio.AutoSize = true;
            this.lbl_Inicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Inicio.Image = global::Gerenciamento_De_Chamados.Properties.Resources.HOME_36p;
            this.lbl_Inicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Inicio.Location = new System.Drawing.Point(20, 49);
            this.lbl_Inicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Inicio.Name = "lbl_Inicio";
            this.lbl_Inicio.Size = new System.Drawing.Size(134, 36);
            this.lbl_Inicio.TabIndex = 6;
            this.lbl_Inicio.Text = "      Início";
            this.lbl_Inicio.Click += new System.EventHandler(this.lbl_Inicio_Click);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPrincipal.AutoScroll = true;
            this.panelPrincipal.Controls.Add(this.dgvRelatorio);
            this.panelPrincipal.Controls.Add(this.panel2);
            this.panelPrincipal.Controls.Add(this.panelHeader);
            this.panelPrincipal.Controls.Add(this.panelSidebar);
            this.panelPrincipal.Location = new System.Drawing.Point(7, -26);
            this.panelPrincipal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1521, 910);
            this.panelPrincipal.TabIndex = 1;
            // 
            // dgvRelatorio
            // 
            this.dgvRelatorio.AllowUserToAddRows = false;
            this.dgvRelatorio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelatorio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRelatorio.Location = new System.Drawing.Point(296, 126);
            this.dgvRelatorio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRelatorio.Name = "dgvRelatorio";
            this.dgvRelatorio.RowHeadersWidth = 51;
            this.dgvRelatorio.Size = new System.Drawing.Size(1225, 704);
            this.dgvRelatorio.TabIndex = 0;
            // 
            // lblMconta
            // 
            this.lblMconta.AutoSize = true;
            this.lblMconta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMconta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMconta.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.lblMconta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMconta.Location = new System.Drawing.Point(6, 316);
            this.lblMconta.Name = "lblMconta";
            this.lblMconta.Size = new System.Drawing.Size(183, 29);
            this.lblMconta.TabIndex = 19;
            this.lblMconta.Text = "      Minha Conta";
            this.lblMconta.Click += new System.EventHandler(this.lblMconta_Click);
            // 
            // FormRelatorioDetalhado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1563, 922);
            this.Controls.Add(this.panelPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormRelatorioDetalhado";
            this.Text = "FormRelatorio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormRelatorioDetalhado_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Inicio)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panelPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lbSair;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PctBox_Inicio;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lbl_Inicio;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.DataGridView dgvRelatorio;
        private System.Windows.Forms.Label lblMconta;
    }
}