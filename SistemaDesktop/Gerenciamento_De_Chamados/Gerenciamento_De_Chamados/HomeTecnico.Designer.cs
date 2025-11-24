using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    partial class HomeTecnico
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeTecnico));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_NomeUser = new System.Windows.Forms.Label();
            this.lbl_Inicio = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSair = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PctBox_Logo = new System.Windows.Forms.PictureBox();
            this.roundedPanel1 = new Gerenciamento_De_Chamados.RoundedPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.roundedPanel2 = new Gerenciamento_De_Chamados.RoundedPanel();
            this.lblResolvido = new System.Windows.Forms.Label();
            this.pnResolvido = new System.Windows.Forms.Panel();
            this.lblEmAndamento = new System.Windows.Forms.Label();
            this.lblPendente = new System.Windows.Forms.Label();
            this.pnPendente = new System.Windows.Forms.Panel();
            this.pnEmAndamento = new System.Windows.Forms.Panel();
            this.plotStatus = new ScottPlot.WinForms.FormsPlot();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.roundedPanel3 = new Gerenciamento_De_Chamados.RoundedPanel();
            this.lblIncidentes = new System.Windows.Forms.Label();
            this.pnIncidentes = new System.Windows.Forms.Panel();
            this.lblComunica = new System.Windows.Forms.Label();
            this.pnComunica = new System.Windows.Forms.Panel();
            this.lblServicos = new System.Windows.Forms.Label();
            this.pnServicos = new System.Windows.Forms.Panel();
            this.lblRede = new System.Windows.Forms.Label();
            this.pnRede = new System.Windows.Forms.Panel();
            this.lblSoftware = new System.Windows.Forms.Label();
            this.pnSoftware = new System.Windows.Forms.Panel();
            this.lblInfra = new System.Windows.Forms.Label();
            this.pnInfra = new System.Windows.Forms.Panel();
            this.lblSeguranca = new System.Windows.Forms.Label();
            this.lblHardware = new System.Windows.Forms.Label();
            this.pnHardware = new System.Windows.Forms.Panel();
            this.pnSeguranca = new System.Windows.Forms.Panel();
            this.plotCategoria = new ScottPlot.WinForms.FormsPlot();
            this.Home_Tecnico = new System.Windows.Forms.Label();
            this.btnResponder_chamado = new System.Windows.Forms.Button();
            this.btnMedio = new System.Windows.Forms.Button();
            this.btnAlto = new System.Windows.Forms.Button();
            this.btn_AbrirChamado = new System.Windows.Forms.Button();
            this.btnBaixo = new System.Windows.Forms.Button();
            this.timerSessao = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Logo)).BeginInit();
            this.roundedPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lbl_NomeUser);
            this.panel2.Controls.Add(this.lbl_Inicio);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(244, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(948, 67);
            this.panel2.TabIndex = 21;
            // 
            // lbl_NomeUser
            // 
            this.lbl_NomeUser.AutoSize = true;
            this.lbl_NomeUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NomeUser.Location = new System.Drawing.Point(155, 32);
            this.lbl_NomeUser.Name = "lbl_NomeUser";
            this.lbl_NomeUser.Size = new System.Drawing.Size(0, 20);
            this.lbl_NomeUser.TabIndex = 9;
            // 
            // lbl_Inicio
            // 
            this.lbl_Inicio.AutoSize = true;
            this.lbl_Inicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Inicio.Image = global::Gerenciamento_De_Chamados.Properties.Resources.HOME_36p;
            this.lbl_Inicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Inicio.Location = new System.Drawing.Point(35, 27);
            this.lbl_Inicio.Name = "lbl_Inicio";
            this.lbl_Inicio.Size = new System.Drawing.Size(105, 26);
            this.lbl_Inicio.TabIndex = 5;
            this.lbl_Inicio.Text = "       Início";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_51dp_000000_FILL0_wght400_GRAD0_opsz48__1_;
            this.pictureBox4.Location = new System.Drawing.Point(885, 13);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(51, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbSair);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PctBox_Logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 794);
            this.panel1.TabIndex = 20;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lbSair
            // 
            this.lbSair.AutoSize = true;
            this.lbSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbSair.Image = global::Gerenciamento_De_Chamados.Properties.Resources.move_item_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.lbSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSair.Location = new System.Drawing.Point(20, 351);
            this.lbSair.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSair.Name = "lbSair";
            this.lbSair.Size = new System.Drawing.Size(72, 24);
            this.lbSair.TabIndex = 17;
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
            this.label3.Location = new System.Drawing.Point(20, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "      FAQ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::Gerenciamento_De_Chamados.Properties.Resources.menu;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(20, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "      Meus Chamados";
            // 
            // PctBox_Logo
            // 
            this.PctBox_Logo.Image = global::Gerenciamento_De_Chamados.Properties.Resources.logo_empresa;
            this.PctBox_Logo.Location = new System.Drawing.Point(-26, 0);
            this.PctBox_Logo.Name = "PctBox_Logo";
            this.PctBox_Logo.Size = new System.Drawing.Size(295, 228);
            this.PctBox_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PctBox_Logo.TabIndex = 3;
            this.PctBox_Logo.TabStop = false;
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedPanel1.BackColor = System.Drawing.Color.White;
            this.roundedPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderWidth = 1F;
            this.roundedPanel1.Controls.Add(this.groupBox1);
            this.roundedPanel1.Controls.Add(this.groupBox2);
            this.roundedPanel1.Controls.Add(this.Home_Tecnico);
            this.roundedPanel1.Controls.Add(this.btnResponder_chamado);
            this.roundedPanel1.Controls.Add(this.btnMedio);
            this.roundedPanel1.Controls.Add(this.btnAlto);
            this.roundedPanel1.Controls.Add(this.btn_AbrirChamado);
            this.roundedPanel1.Controls.Add(this.btnBaixo);
            this.roundedPanel1.CornerRadius = 15F;
            this.roundedPanel1.Location = new System.Drawing.Point(261, 94);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(904, 690);
            this.roundedPanel1.TabIndex = 29;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.roundedPanel2);
            this.groupBox1.Controls.Add(this.plotStatus);
            this.groupBox1.Location = new System.Drawing.Point(23, 304);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(387, 375);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel2.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPanel2.BorderWidth = 1F;
            this.roundedPanel2.Controls.Add(this.lblResolvido);
            this.roundedPanel2.Controls.Add(this.pnResolvido);
            this.roundedPanel2.Controls.Add(this.lblEmAndamento);
            this.roundedPanel2.Controls.Add(this.lblPendente);
            this.roundedPanel2.Controls.Add(this.pnPendente);
            this.roundedPanel2.Controls.Add(this.pnEmAndamento);
            this.roundedPanel2.CornerRadius = 15F;
            this.roundedPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.roundedPanel2.Location = new System.Drawing.Point(249, 15);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(136, 358);
            this.roundedPanel2.TabIndex = 4;
            // 
            // lblResolvido
            // 
            this.lblResolvido.AutoSize = true;
            this.lblResolvido.Location = new System.Drawing.Point(28, 236);
            this.lblResolvido.Name = "lblResolvido";
            this.lblResolvido.Size = new System.Drawing.Size(54, 13);
            this.lblResolvido.TabIndex = 7;
            this.lblResolvido.Text = "Resolvido";
            // 
            // pnResolvido
            // 
            this.pnResolvido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnResolvido.Location = new System.Drawing.Point(4, 228);
            this.pnResolvido.Name = "pnResolvido";
            this.pnResolvido.Size = new System.Drawing.Size(18, 21);
            this.pnResolvido.TabIndex = 6;
            // 
            // lblEmAndamento
            // 
            this.lblEmAndamento.AutoSize = true;
            this.lblEmAndamento.Location = new System.Drawing.Point(28, 143);
            this.lblEmAndamento.Name = "lblEmAndamento";
            this.lblEmAndamento.Size = new System.Drawing.Size(78, 13);
            this.lblEmAndamento.TabIndex = 4;
            this.lblEmAndamento.Text = "Em andamento";
            // 
            // lblPendente
            // 
            this.lblPendente.AutoSize = true;
            this.lblPendente.Location = new System.Drawing.Point(28, 61);
            this.lblPendente.Name = "lblPendente";
            this.lblPendente.Size = new System.Drawing.Size(53, 13);
            this.lblPendente.TabIndex = 3;
            this.lblPendente.Text = "Pendente";
            // 
            // pnPendente
            // 
            this.pnPendente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnPendente.Location = new System.Drawing.Point(4, 53);
            this.pnPendente.Name = "pnPendente";
            this.pnPendente.Size = new System.Drawing.Size(18, 21);
            this.pnPendente.TabIndex = 2;
            // 
            // pnEmAndamento
            // 
            this.pnEmAndamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnEmAndamento.Location = new System.Drawing.Point(4, 135);
            this.pnEmAndamento.Name = "pnEmAndamento";
            this.pnEmAndamento.Size = new System.Drawing.Size(18, 21);
            this.pnEmAndamento.TabIndex = 0;
            // 
            // plotStatus
            // 
            this.plotStatus.DisplayScale = 0F;
            this.plotStatus.Location = new System.Drawing.Point(2, 15);
            this.plotStatus.Name = "plotStatus";
            this.plotStatus.Size = new System.Drawing.Size(253, 358);
            this.plotStatus.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.roundedPanel3);
            this.groupBox2.Controls.Add(this.plotCategoria);
            this.groupBox2.Location = new System.Drawing.Point(506, 304);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(363, 375);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            // 
            // roundedPanel3
            // 
            this.roundedPanel3.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel3.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPanel3.BorderWidth = 1F;
            this.roundedPanel3.Controls.Add(this.lblIncidentes);
            this.roundedPanel3.Controls.Add(this.pnIncidentes);
            this.roundedPanel3.Controls.Add(this.lblComunica);
            this.roundedPanel3.Controls.Add(this.pnComunica);
            this.roundedPanel3.Controls.Add(this.lblServicos);
            this.roundedPanel3.Controls.Add(this.pnServicos);
            this.roundedPanel3.Controls.Add(this.lblRede);
            this.roundedPanel3.Controls.Add(this.pnRede);
            this.roundedPanel3.Controls.Add(this.lblSoftware);
            this.roundedPanel3.Controls.Add(this.pnSoftware);
            this.roundedPanel3.Controls.Add(this.lblInfra);
            this.roundedPanel3.Controls.Add(this.pnInfra);
            this.roundedPanel3.Controls.Add(this.lblSeguranca);
            this.roundedPanel3.Controls.Add(this.lblHardware);
            this.roundedPanel3.Controls.Add(this.pnHardware);
            this.roundedPanel3.Controls.Add(this.pnSeguranca);
            this.roundedPanel3.CornerRadius = 15F;
            this.roundedPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.roundedPanel3.Location = new System.Drawing.Point(237, 15);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(124, 358);
            this.roundedPanel3.TabIndex = 4;
            // 
            // lblIncidentes
            // 
            this.lblIncidentes.AutoSize = true;
            this.lblIncidentes.Location = new System.Drawing.Point(28, 313);
            this.lblIncidentes.Name = "lblIncidentes";
            this.lblIncidentes.Size = new System.Drawing.Size(56, 13);
            this.lblIncidentes.TabIndex = 17;
            this.lblIncidentes.Text = "Incidentes";
            // 
            // pnIncidentes
            // 
            this.pnIncidentes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnIncidentes.Location = new System.Drawing.Point(4, 305);
            this.pnIncidentes.Name = "pnIncidentes";
            this.pnIncidentes.Size = new System.Drawing.Size(18, 21);
            this.pnIncidentes.TabIndex = 16;
            // 
            // lblComunica
            // 
            this.lblComunica.AutoSize = true;
            this.lblComunica.Location = new System.Drawing.Point(28, 276);
            this.lblComunica.Name = "lblComunica";
            this.lblComunica.Size = new System.Drawing.Size(72, 13);
            this.lblComunica.TabIndex = 15;
            this.lblComunica.Text = "Comunicacao";
            // 
            // pnComunica
            // 
            this.pnComunica.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnComunica.Location = new System.Drawing.Point(4, 268);
            this.pnComunica.Name = "pnComunica";
            this.pnComunica.Size = new System.Drawing.Size(18, 21);
            this.pnComunica.TabIndex = 14;
            // 
            // lblServicos
            // 
            this.lblServicos.AutoSize = true;
            this.lblServicos.Location = new System.Drawing.Point(28, 193);
            this.lblServicos.Name = "lblServicos";
            this.lblServicos.Size = new System.Drawing.Size(48, 13);
            this.lblServicos.TabIndex = 13;
            this.lblServicos.Text = "Servicos";
            // 
            // pnServicos
            // 
            this.pnServicos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnServicos.Location = new System.Drawing.Point(4, 185);
            this.pnServicos.Name = "pnServicos";
            this.pnServicos.Size = new System.Drawing.Size(18, 21);
            this.pnServicos.TabIndex = 12;
            // 
            // lblRede
            // 
            this.lblRede.AutoSize = true;
            this.lblRede.Location = new System.Drawing.Point(28, 154);
            this.lblRede.Name = "lblRede";
            this.lblRede.Size = new System.Drawing.Size(33, 13);
            this.lblRede.TabIndex = 11;
            this.lblRede.Text = "Rede";
            // 
            // pnRede
            // 
            this.pnRede.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnRede.Location = new System.Drawing.Point(4, 146);
            this.pnRede.Name = "pnRede";
            this.pnRede.Size = new System.Drawing.Size(18, 21);
            this.pnRede.TabIndex = 10;
            // 
            // lblSoftware
            // 
            this.lblSoftware.AutoSize = true;
            this.lblSoftware.Location = new System.Drawing.Point(28, 73);
            this.lblSoftware.Name = "lblSoftware";
            this.lblSoftware.Size = new System.Drawing.Size(49, 13);
            this.lblSoftware.TabIndex = 9;
            this.lblSoftware.Text = "Software";
            // 
            // pnSoftware
            // 
            this.pnSoftware.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSoftware.Location = new System.Drawing.Point(4, 65);
            this.pnSoftware.Name = "pnSoftware";
            this.pnSoftware.Size = new System.Drawing.Size(18, 21);
            this.pnSoftware.TabIndex = 8;
            // 
            // lblInfra
            // 
            this.lblInfra.AutoSize = true;
            this.lblInfra.Location = new System.Drawing.Point(28, 236);
            this.lblInfra.Name = "lblInfra";
            this.lblInfra.Size = new System.Drawing.Size(69, 13);
            this.lblInfra.TabIndex = 7;
            this.lblInfra.Text = "Infraestrutura";
            // 
            // pnInfra
            // 
            this.pnInfra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnInfra.Location = new System.Drawing.Point(4, 228);
            this.pnInfra.Name = "pnInfra";
            this.pnInfra.Size = new System.Drawing.Size(18, 21);
            this.pnInfra.TabIndex = 6;
            // 
            // lblSeguranca
            // 
            this.lblSeguranca.AutoSize = true;
            this.lblSeguranca.Location = new System.Drawing.Point(28, 115);
            this.lblSeguranca.Name = "lblSeguranca";
            this.lblSeguranca.Size = new System.Drawing.Size(59, 13);
            this.lblSeguranca.TabIndex = 4;
            this.lblSeguranca.Text = "Seguranca";
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Location = new System.Drawing.Point(28, 34);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(53, 13);
            this.lblHardware.TabIndex = 3;
            this.lblHardware.Text = "Hardware";
            // 
            // pnHardware
            // 
            this.pnHardware.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnHardware.Location = new System.Drawing.Point(4, 26);
            this.pnHardware.Name = "pnHardware";
            this.pnHardware.Size = new System.Drawing.Size(18, 21);
            this.pnHardware.TabIndex = 2;
            // 
            // pnSeguranca
            // 
            this.pnSeguranca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSeguranca.Location = new System.Drawing.Point(4, 107);
            this.pnSeguranca.Name = "pnSeguranca";
            this.pnSeguranca.Size = new System.Drawing.Size(18, 21);
            this.pnSeguranca.TabIndex = 0;
            // 
            // plotCategoria
            // 
            this.plotCategoria.DisplayScale = 0F;
            this.plotCategoria.Location = new System.Drawing.Point(5, 15);
            this.plotCategoria.Name = "plotCategoria";
            this.plotCategoria.Size = new System.Drawing.Size(218, 358);
            this.plotCategoria.TabIndex = 0;
            // 
            // Home_Tecnico
            // 
            this.Home_Tecnico.BackColor = System.Drawing.Color.Transparent;
            this.Home_Tecnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Home_Tecnico.Location = new System.Drawing.Point(29, 18);
            this.Home_Tecnico.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Home_Tecnico.Name = "Home_Tecnico";
            this.Home_Tecnico.Size = new System.Drawing.Size(494, 49);
            this.Home_Tecnico.TabIndex = 19;
            // 
            // btnResponder_chamado
            // 
            this.btnResponder_chamado.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResponder_chamado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResponder_chamado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResponder_chamado.Location = new System.Drawing.Point(22, 98);
            this.btnResponder_chamado.Margin = new System.Windows.Forms.Padding(2);
            this.btnResponder_chamado.Name = "btnResponder_chamado";
            this.btnResponder_chamado.Size = new System.Drawing.Size(183, 57);
            this.btnResponder_chamado.TabIndex = 28;
            this.btnResponder_chamado.Text = "Responder chamado";
            this.btnResponder_chamado.UseVisualStyleBackColor = true;
            this.btnResponder_chamado.Click += new System.EventHandler(this.btnResponder_chamado_Click);
            // 
            // btnMedio
            // 
            this.btnMedio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnMedio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMedio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedio.Image = global::Gerenciamento_De_Chamados.Properties.Resources.error_48dp_0000F5_FILL0_wght400_GRAD0_opsz48;
            this.btnMedio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMedio.Location = new System.Drawing.Point(312, 180);
            this.btnMedio.Margin = new System.Windows.Forms.Padding(2);
            this.btnMedio.Name = "btnMedio";
            this.btnMedio.Size = new System.Drawing.Size(255, 98);
            this.btnMedio.TabIndex = 24;
            this.btnMedio.Text = "      Chamados \r\n       Médio\r\n";
            this.btnMedio.UseVisualStyleBackColor = true;
            this.btnMedio.Click += new System.EventHandler(this.btnMedio_Click);
            // 
            // btnAlto
            // 
            this.btnAlto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAlto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlto.Image = global::Gerenciamento_De_Chamados.Properties.Resources.error_48dp_EA3323_FILL0_wght400_GRAD0_opsz48;
            this.btnAlto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlto.Location = new System.Drawing.Point(612, 180);
            this.btnAlto.Margin = new System.Windows.Forms.Padding(2);
            this.btnAlto.Name = "btnAlto";
            this.btnAlto.Size = new System.Drawing.Size(255, 98);
            this.btnAlto.TabIndex = 23;
            this.btnAlto.Text = "      Chamados\r\n        Alto";
            this.btnAlto.UseVisualStyleBackColor = true;
            this.btnAlto.Click += new System.EventHandler(this.btnAlto_Click);
            // 
            // btn_AbrirChamado
            // 
            this.btn_AbrirChamado.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_AbrirChamado.BackColor = System.Drawing.Color.White;
            this.btn_AbrirChamado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AbrirChamado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AbrirChamado.Location = new System.Drawing.Point(684, 106);
            this.btn_AbrirChamado.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AbrirChamado.Name = "btn_AbrirChamado";
            this.btn_AbrirChamado.Size = new System.Drawing.Size(183, 57);
            this.btn_AbrirChamado.TabIndex = 25;
            this.btn_AbrirChamado.Text = "Criar chamado";
            this.btn_AbrirChamado.UseVisualStyleBackColor = false;
            this.btn_AbrirChamado.Click += new System.EventHandler(this.btn_AbrirChamado_Click);
            // 
            // btnBaixo
            // 
            this.btnBaixo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBaixo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaixo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaixo.Image = global::Gerenciamento_De_Chamados.Properties.Resources.error_48dp_75FB4C_FILL0_wght400_GRAD0_opsz48;
            this.btnBaixo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaixo.Location = new System.Drawing.Point(22, 180);
            this.btnBaixo.Margin = new System.Windows.Forms.Padding(2);
            this.btnBaixo.Name = "btnBaixo";
            this.btnBaixo.Size = new System.Drawing.Size(255, 98);
            this.btnBaixo.TabIndex = 22;
            this.btnBaixo.Text = "    \r\n Chamados \r\n     Baixo\r\n\r\n";
            this.btnBaixo.UseVisualStyleBackColor = true;
            this.btnBaixo.Click += new System.EventHandler(this.btnBaixo_Click);
            // 
            // timerSessao
            // 
            this.timerSessao.Enabled = true;
            this.timerSessao.Interval = 5000;
            this.timerSessao.Tick += new System.EventHandler(this.timerSessao_Tick);
            // 
            // HomeTecnico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 794);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundedPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HomeTecnico";
            this.Text = "HomeTecnico";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HomeTecnico_Paint);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Logo)).EndInit();
            this.roundedPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Home_Tecnico;
        private System.Windows.Forms.Button btn_AbrirChamado;
        private System.Windows.Forms.Button btnMedio;
        private System.Windows.Forms.Button btnAlto;
        private System.Windows.Forms.Button btnBaixo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_NomeUser;
        private System.Windows.Forms.Label lbl_Inicio;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PctBox_Logo;
        private System.Windows.Forms.Button btnResponder_chamado;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label lbSair;
        private System.Windows.Forms.Timer timerSessao;
        private System.Windows.Forms.GroupBox groupBox1;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Label lblResolvido;
        private System.Windows.Forms.Panel pnResolvido;
        private System.Windows.Forms.Label lblEmAndamento;
        private System.Windows.Forms.Label lblPendente;
        private System.Windows.Forms.Panel pnPendente;
        private System.Windows.Forms.Panel pnEmAndamento;
        private ScottPlot.WinForms.FormsPlot plotStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private RoundedPanel roundedPanel3;
        private System.Windows.Forms.Label lblIncidentes;
        private System.Windows.Forms.Panel pnIncidentes;
        private System.Windows.Forms.Label lblComunica;
        private System.Windows.Forms.Panel pnComunica;
        private System.Windows.Forms.Label lblServicos;
        private System.Windows.Forms.Panel pnServicos;
        private System.Windows.Forms.Label lblRede;
        private System.Windows.Forms.Panel pnRede;
        private System.Windows.Forms.Label lblSoftware;
        private System.Windows.Forms.Panel pnSoftware;
        private System.Windows.Forms.Label lblInfra;
        private System.Windows.Forms.Panel pnInfra;
        private System.Windows.Forms.Label lblSeguranca;
        private System.Windows.Forms.Label lblHardware;
        private System.Windows.Forms.Panel pnHardware;
        private System.Windows.Forms.Panel pnSeguranca;
        private ScottPlot.WinForms.FormsPlot plotCategoria;
    }
}