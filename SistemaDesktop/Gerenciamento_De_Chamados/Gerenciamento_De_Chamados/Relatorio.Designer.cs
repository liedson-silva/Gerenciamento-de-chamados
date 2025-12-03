using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    partial class Relatorio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Relatorio));
            this.btnGerarRelatorio = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotalChamados = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalResolvidos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalPendentes = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbSair = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbl_Inicio = new System.Windows.Forms.Label();
            this.dtpAte = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.formPlotVisaoGeral = new ScottPlot.WinForms.FormsPlot();
            this.pnLegenda = new Gerenciamento_De_Chamados.RoundedPanel();
            this.lblResolvido = new System.Windows.Forms.Label();
            this.lblEmAndamento = new System.Windows.Forms.Label();
            this.lblPendente = new System.Windows.Forms.Label();
            this.pnPendente = new System.Windows.Forms.Panel();
            this.pnResolvido = new System.Windows.Forms.Panel();
            this.pnEmAndamento = new System.Windows.Forms.Panel();
            this.dtpDe = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.fpPrioridades = new ScottPlot.WinForms.FormsPlot();
            this.roundedPanel1 = new Gerenciamento_De_Chamados.RoundedPanel();
            this.lblBaixa = new System.Windows.Forms.Label();
            this.pnBaixa = new System.Windows.Forms.Panel();
            this.lblMedia = new System.Windows.Forms.Label();
            this.lblAlta = new System.Windows.Forms.Label();
            this.pnAlta = new System.Windows.Forms.Panel();
            this.pnMedia = new System.Windows.Forms.Panel();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.roundedPanel2 = new Gerenciamento_De_Chamados.RoundedPanel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnLegenda.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.roundedPanel1.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGerarRelatorio
            // 
            this.btnGerarRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarRelatorio.Location = new System.Drawing.Point(382, 80);
            this.btnGerarRelatorio.Name = "btnGerarRelatorio";
            this.btnGerarRelatorio.Size = new System.Drawing.Size(123, 38);
            this.btnGerarRelatorio.TabIndex = 3;
            this.btnGerarRelatorio.Text = "Gerar Relatorio";
            this.btnGerarRelatorio.UseVisualStyleBackColor = true;
            this.btnGerarRelatorio.Click += new System.EventHandler(this.btnGerarRelatorio_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.19014F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.38456F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.4253F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(764, 298);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(414, 75);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTotalChamados);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 69);
            this.panel2.TabIndex = 0;
            // 
            // lblTotalChamados
            // 
            this.lblTotalChamados.AutoSize = true;
            this.lblTotalChamados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalChamados.Location = new System.Drawing.Point(33, 39);
            this.lblTotalChamados.Name = "lblTotalChamados";
            this.lblTotalChamados.Size = new System.Drawing.Size(51, 16);
            this.lblTotalChamados.TabIndex = 1;
            this.lblTotalChamados.Text = "           ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Total Abertos";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblTotalResolvidos);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(123, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(140, 69);
            this.panel3.TabIndex = 1;
            // 
            // lblTotalResolvidos
            // 
            this.lblTotalResolvidos.AutoSize = true;
            this.lblTotalResolvidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalResolvidos.Location = new System.Drawing.Point(49, 40);
            this.lblTotalResolvidos.Name = "lblTotalResolvidos";
            this.lblTotalResolvidos.Size = new System.Drawing.Size(43, 16);
            this.lblTotalResolvidos.TabIndex = 2;
            this.lblTotalResolvidos.Text = "         ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Total Resolvidos";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblTotalPendentes);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(269, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(142, 69);
            this.panel4.TabIndex = 2;
            // 
            // lblTotalPendentes
            // 
            this.lblTotalPendentes.AutoSize = true;
            this.lblTotalPendentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPendentes.Location = new System.Drawing.Point(52, 38);
            this.lblTotalPendentes.Name = "lblTotalPendentes";
            this.lblTotalPendentes.Size = new System.Drawing.Size(51, 16);
            this.lblTotalPendentes.TabIndex = 3;
            this.lblTotalPendentes.Text = "           ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total Pendentes";
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.Transparent;
            this.panelSidebar.Controls.Add(this.pictureBox1);
            this.panelSidebar.Controls.Add(this.lbSair);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelSidebar.Size = new System.Drawing.Size(231, 817);
            this.panelSidebar.TabIndex = 0;
            this.panelSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSidebar_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Gerenciamento_De_Chamados.Properties.Resources.logo_empresa;
            this.pictureBox1.Location = new System.Drawing.Point(-46, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(309, 228);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // lbSair
            // 
            this.lbSair.AutoSize = true;
            this.lbSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbSair.Image = global::Gerenciamento_De_Chamados.Properties.Resources.move_item_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.lbSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSair.Location = new System.Drawing.Point(10, 388);
            this.lbSair.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSair.Name = "lbSair";
            this.lbSair.Size = new System.Drawing.Size(72, 24);
            this.lbSair.TabIndex = 18;
            this.lbSair.Text = "      Sair";
            this.lbSair.Click += new System.EventHandler(this.lbSair_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.pictureBox4);
            this.panelHeader.Controls.Add(this.lbl_Inicio);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(231, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1099, 92);
            this.panelHeader.TabIndex = 1;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_51dp_000000_FILL0_wght400_GRAD0_opsz48__1_;
            this.pictureBox4.Location = new System.Drawing.Point(1006, 11);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(51, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // lbl_Inicio
            // 
            this.lbl_Inicio.AutoSize = true;
            this.lbl_Inicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Inicio.Image = global::Gerenciamento_De_Chamados.Properties.Resources.home_40dp_000000_FILL1_wght400_GRAD0_opsz40;
            this.lbl_Inicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Inicio.Location = new System.Drawing.Point(28, 32);
            this.lbl_Inicio.Name = "lbl_Inicio";
            this.lbl_Inicio.Size = new System.Drawing.Size(106, 29);
            this.lbl_Inicio.TabIndex = 6;
            this.lbl_Inicio.Text = "      Início";
            this.lbl_Inicio.Click += new System.EventHandler(this.lbl_Inicio_Click);
            // 
            // dtpAte
            // 
            this.dtpAte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpAte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAte.Location = new System.Drawing.Point(237, 86);
            this.dtpAte.Name = "dtpAte";
            this.dtpAte.Size = new System.Drawing.Size(104, 26);
            this.dtpAte.TabIndex = 12;
            this.dtpAte.Value = new System.DateTime(2025, 10, 10, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(184, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Até:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "De:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(325, 298);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.83761F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(361, 468);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.formPlotVisaoGeral);
            this.panel5.Controls.Add(this.pnLegenda);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.panel5.Size = new System.Drawing.Size(355, 462);
            this.panel5.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(49, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(239, 24);
            this.label11.TabIndex = 2;
            this.label11.Text = "Visão Geral dos Chamados";
            // 
            // formPlotVisaoGeral
            // 
            this.formPlotVisaoGeral.DisplayScale = 0F;
            this.formPlotVisaoGeral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formPlotVisaoGeral.Location = new System.Drawing.Point(10, 10);
            this.formPlotVisaoGeral.Name = "formPlotVisaoGeral";
            this.formPlotVisaoGeral.Size = new System.Drawing.Size(197, 440);
            this.formPlotVisaoGeral.TabIndex = 0;
            // 
            // pnLegenda
            // 
            this.pnLegenda.BackColor = System.Drawing.Color.Transparent;
            this.pnLegenda.BorderColor = System.Drawing.Color.Transparent;
            this.pnLegenda.BorderWidth = 1F;
            this.pnLegenda.Controls.Add(this.lblResolvido);
            this.pnLegenda.Controls.Add(this.lblEmAndamento);
            this.pnLegenda.Controls.Add(this.lblPendente);
            this.pnLegenda.Controls.Add(this.pnPendente);
            this.pnLegenda.Controls.Add(this.pnResolvido);
            this.pnLegenda.Controls.Add(this.pnEmAndamento);
            this.pnLegenda.CornerRadius = 15F;
            this.pnLegenda.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnLegenda.Location = new System.Drawing.Point(207, 10);
            this.pnLegenda.Name = "pnLegenda";
            this.pnLegenda.Size = new System.Drawing.Size(136, 440);
            this.pnLegenda.TabIndex = 1;
            // 
            // lblResolvido
            // 
            this.lblResolvido.AutoSize = true;
            this.lblResolvido.Location = new System.Drawing.Point(27, 343);
            this.lblResolvido.Name = "lblResolvido";
            this.lblResolvido.Size = new System.Drawing.Size(54, 13);
            this.lblResolvido.TabIndex = 5;
            this.lblResolvido.Text = "Resolvido";
            // 
            // lblEmAndamento
            // 
            this.lblEmAndamento.AutoSize = true;
            this.lblEmAndamento.Location = new System.Drawing.Point(24, 205);
            this.lblEmAndamento.Name = "lblEmAndamento";
            this.lblEmAndamento.Size = new System.Drawing.Size(79, 13);
            this.lblEmAndamento.TabIndex = 4;
            this.lblEmAndamento.Text = "Em Andamento";
            // 
            // lblPendente
            // 
            this.lblPendente.AutoSize = true;
            this.lblPendente.Location = new System.Drawing.Point(28, 65);
            this.lblPendente.Name = "lblPendente";
            this.lblPendente.Size = new System.Drawing.Size(53, 13);
            this.lblPendente.TabIndex = 3;
            this.lblPendente.Text = "Pendente";
            // 
            // pnPendente
            // 
            this.pnPendente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnPendente.Location = new System.Drawing.Point(4, 57);
            this.pnPendente.Name = "pnPendente";
            this.pnPendente.Size = new System.Drawing.Size(18, 21);
            this.pnPendente.TabIndex = 2;
            // 
            // pnResolvido
            // 
            this.pnResolvido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnResolvido.Location = new System.Drawing.Point(3, 335);
            this.pnResolvido.Name = "pnResolvido";
            this.pnResolvido.Size = new System.Drawing.Size(18, 21);
            this.pnResolvido.TabIndex = 1;
            // 
            // pnEmAndamento
            // 
            this.pnEmAndamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnEmAndamento.Location = new System.Drawing.Point(0, 197);
            this.pnEmAndamento.Name = "pnEmAndamento";
            this.pnEmAndamento.Size = new System.Drawing.Size(18, 21);
            this.pnEmAndamento.TabIndex = 0;
            // 
            // dtpDe
            // 
            this.dtpDe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDe.Location = new System.Drawing.Point(65, 86);
            this.dtpDe.Name = "dtpDe";
            this.dtpDe.Size = new System.Drawing.Size(102, 26);
            this.dtpDe.TabIndex = 9;
            this.dtpDe.Value = new System.DateTime(2025, 10, 10, 0, 0, 0, 0);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(764, 388);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.7027F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(414, 370);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.panel7.Size = new System.Drawing.Size(408, 364);
            this.panel7.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.fpPrioridades);
            this.panel6.Controls.Add(this.roundedPanel1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(10, 10);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.panel6.Size = new System.Drawing.Size(386, 342);
            this.panel6.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(230, 24);
            this.label10.TabIndex = 4;
            this.label10.Text = "Distribuição por prioridade";
            // 
            // fpPrioridades
            // 
            this.fpPrioridades.DisplayScale = 0F;
            this.fpPrioridades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPrioridades.Location = new System.Drawing.Point(10, 10);
            this.fpPrioridades.Name = "fpPrioridades";
            this.fpPrioridades.Size = new System.Drawing.Size(230, 322);
            this.fpPrioridades.TabIndex = 2;
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderWidth = 1F;
            this.roundedPanel1.Controls.Add(this.lblBaixa);
            this.roundedPanel1.Controls.Add(this.pnBaixa);
            this.roundedPanel1.Controls.Add(this.lblMedia);
            this.roundedPanel1.Controls.Add(this.lblAlta);
            this.roundedPanel1.Controls.Add(this.pnAlta);
            this.roundedPanel1.Controls.Add(this.pnMedia);
            this.roundedPanel1.CornerRadius = 15F;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.roundedPanel1.Location = new System.Drawing.Point(240, 10);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(136, 322);
            this.roundedPanel1.TabIndex = 3;
            // 
            // lblBaixa
            // 
            this.lblBaixa.AutoSize = true;
            this.lblBaixa.Location = new System.Drawing.Point(28, 236);
            this.lblBaixa.Name = "lblBaixa";
            this.lblBaixa.Size = new System.Drawing.Size(33, 13);
            this.lblBaixa.TabIndex = 7;
            this.lblBaixa.Text = "Baixa";
            // 
            // pnBaixa
            // 
            this.pnBaixa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnBaixa.Location = new System.Drawing.Point(4, 228);
            this.pnBaixa.Name = "pnBaixa";
            this.pnBaixa.Size = new System.Drawing.Size(18, 21);
            this.pnBaixa.TabIndex = 6;
            // 
            // lblMedia
            // 
            this.lblMedia.AutoSize = true;
            this.lblMedia.Location = new System.Drawing.Point(28, 143);
            this.lblMedia.Name = "lblMedia";
            this.lblMedia.Size = new System.Drawing.Size(36, 13);
            this.lblMedia.TabIndex = 4;
            this.lblMedia.Text = "Media";
            // 
            // lblAlta
            // 
            this.lblAlta.AutoSize = true;
            this.lblAlta.Location = new System.Drawing.Point(28, 61);
            this.lblAlta.Name = "lblAlta";
            this.lblAlta.Size = new System.Drawing.Size(25, 13);
            this.lblAlta.TabIndex = 3;
            this.lblAlta.Text = "Alta";
            // 
            // pnAlta
            // 
            this.pnAlta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnAlta.Location = new System.Drawing.Point(4, 53);
            this.pnAlta.Name = "pnAlta";
            this.pnAlta.Size = new System.Drawing.Size(18, 21);
            this.pnAlta.TabIndex = 2;
            // 
            // pnMedia
            // 
            this.pnMedia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMedia.Location = new System.Drawing.Point(4, 135);
            this.pnMedia.Name = "pnMedia";
            this.pnMedia.Size = new System.Drawing.Size(18, 21);
            this.pnMedia.TabIndex = 0;
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.AutoScroll = true;
            this.panelPrincipal.Controls.Add(this.tableLayoutPanel3);
            this.panelPrincipal.Controls.Add(this.tableLayoutPanel2);
            this.panelPrincipal.Controls.Add(this.panelHeader);
            this.panelPrincipal.Controls.Add(this.panelSidebar);
            this.panelPrincipal.Controls.Add(this.tableLayoutPanel1);
            this.panelPrincipal.Controls.Add(this.roundedPanel2);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1330, 817);
            this.panelPrincipal.TabIndex = 0;
            this.panelPrincipal.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPrincipal_Paint);
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedPanel2.BackColor = System.Drawing.Color.White;
            this.roundedPanel2.BorderColor = System.Drawing.Color.White;
            this.roundedPanel2.BorderWidth = 1F;
            this.roundedPanel2.Controls.Add(this.btnVoltar);
            this.roundedPanel2.Controls.Add(this.label2);
            this.roundedPanel2.Controls.Add(this.dtpDe);
            this.roundedPanel2.Controls.Add(this.dtpAte);
            this.roundedPanel2.Controls.Add(this.label9);
            this.roundedPanel2.Controls.Add(this.label8);
            this.roundedPanel2.Controls.Add(this.btnGerarRelatorio);
            this.roundedPanel2.CornerRadius = 15F;
            this.roundedPanel2.Location = new System.Drawing.Point(248, 110);
            this.roundedPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(1046, 682);
            this.roundedPanel2.TabIndex = 13;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.Location = new System.Drawing.Point(936, 624);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(104, 43);
            this.btnVoltar.TabIndex = 13;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Relatorio";
            // 
            // Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1330, 817);
            this.Controls.Add(this.panelPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Relatorio_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnLegenda.ResumeLayout(false);
            this.pnLegenda.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            this.panelPrincipal.ResumeLayout(false);
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGerarRelatorio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalChamados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalResolvidos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalPendentes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lbSair;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lbl_Inicio;
        private System.Windows.Forms.DateTimePicker dtpAte;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private ScottPlot.WinForms.FormsPlot formPlotVisaoGeral;
        private RoundedPanel pnLegenda;
        private System.Windows.Forms.Label lblResolvido;
        private System.Windows.Forms.Label lblEmAndamento;
        private System.Windows.Forms.Label lblPendente;
        private System.Windows.Forms.Panel pnPendente;
        private System.Windows.Forms.Panel pnResolvido;
        private System.Windows.Forms.Panel pnEmAndamento;
        private System.Windows.Forms.DateTimePicker dtpDe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label10;
        private ScottPlot.WinForms.FormsPlot fpPrioridades;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label lblBaixa;
        private System.Windows.Forms.Panel pnBaixa;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Label lblAlta;
        private System.Windows.Forms.Panel pnAlta;
        private System.Windows.Forms.Panel pnMedia;
        private System.Windows.Forms.Panel panelPrincipal;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnVoltar;
    }
}