using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    partial class HomeFuncionario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeFuncionario));
            this.roundedPanel1 = new Gerenciamento_De_Chamados.RoundedPanel();
            this.btn_AbrirChamado = new System.Windows.Forms.Button();
            this.btnChamadosEmAndamento = new System.Windows.Forms.Button();
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
            this.lbl_NomeUser = new System.Windows.Forms.Label();
            this.btnChamadosPendentes = new System.Windows.Forms.Button();
            this.btnChamadosResolvidos = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbl_Inicio = new System.Windows.Forms.Label();
            this.lbSair = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMeusChamados = new System.Windows.Forms.Label();
            this.PctBox_Logo = new System.Windows.Forms.PictureBox();
            this.roundedPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedPanel1.BackColor = System.Drawing.Color.White;
            this.roundedPanel1.BorderColor = System.Drawing.Color.Gray;
            this.roundedPanel1.BorderWidth = 1F;
            this.roundedPanel1.Controls.Add(this.btn_AbrirChamado);
            this.roundedPanel1.Controls.Add(this.btnChamadosEmAndamento);
            this.roundedPanel1.Controls.Add(this.groupBox1);
            this.roundedPanel1.Controls.Add(this.groupBox2);
            this.roundedPanel1.Controls.Add(this.lbl_NomeUser);
            this.roundedPanel1.Controls.Add(this.btnChamadosPendentes);
            this.roundedPanel1.Controls.Add(this.btnChamadosResolvidos);
            this.roundedPanel1.CornerRadius = 15F;
            this.roundedPanel1.Location = new System.Drawing.Point(332, 104);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(1267, 848);
            this.roundedPanel1.TabIndex = 19;
            // 
            // btn_AbrirChamado
            // 
            this.btn_AbrirChamado.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_AbrirChamado.BackColor = System.Drawing.Color.White;
            this.btn_AbrirChamado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AbrirChamado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AbrirChamado.Location = new System.Drawing.Point(960, 64);
            this.btn_AbrirChamado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AbrirChamado.Name = "btn_AbrirChamado";
            this.btn_AbrirChamado.Size = new System.Drawing.Size(244, 57);
            this.btn_AbrirChamado.TabIndex = 16;
            this.btn_AbrirChamado.Text = "Abrir chamado";
            this.btn_AbrirChamado.UseVisualStyleBackColor = false;
            this.btn_AbrirChamado.Click += new System.EventHandler(this.btn_AbrirChamado_Click);
            // 
            // btnChamadosEmAndamento
            // 
            this.btnChamadosEmAndamento.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChamadosEmAndamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChamadosEmAndamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamadosEmAndamento.Image = global::Gerenciamento_De_Chamados.Properties.Resources.hourglass_empty_46dp_000000_FILL0_wght400_GRAD0_opsz48;
            this.btnChamadosEmAndamento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChamadosEmAndamento.Location = new System.Drawing.Point(441, 170);
            this.btnChamadosEmAndamento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChamadosEmAndamento.Name = "btnChamadosEmAndamento";
            this.btnChamadosEmAndamento.Size = new System.Drawing.Size(340, 121);
            this.btnChamadosEmAndamento.TabIndex = 15;
            this.btnChamadosEmAndamento.Text = "      Chamados em \r\n      andamento \r\n";
            this.btnChamadosEmAndamento.UseVisualStyleBackColor = true;
            this.btnChamadosEmAndamento.Click += new System.EventHandler(this.btnChamadosEmAndamento_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.roundedPanel2);
            this.groupBox1.Controls.Add(this.plotStatus);
            this.groupBox1.Location = new System.Drawing.Point(23, 326);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(516, 462);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
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
            this.roundedPanel2.Location = new System.Drawing.Point(332, 17);
            this.roundedPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(181, 443);
            this.roundedPanel2.TabIndex = 4;
            // 
            // lblResolvido
            // 
            this.lblResolvido.AutoSize = true;
            this.lblResolvido.Location = new System.Drawing.Point(37, 290);
            this.lblResolvido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResolvido.Name = "lblResolvido";
            this.lblResolvido.Size = new System.Drawing.Size(69, 16);
            this.lblResolvido.TabIndex = 7;
            this.lblResolvido.Text = "Resolvido";
            // 
            // pnResolvido
            // 
            this.pnResolvido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnResolvido.Location = new System.Drawing.Point(5, 281);
            this.pnResolvido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnResolvido.Name = "pnResolvido";
            this.pnResolvido.Size = new System.Drawing.Size(23, 25);
            this.pnResolvido.TabIndex = 6;
            // 
            // lblEmAndamento
            // 
            this.lblEmAndamento.AutoSize = true;
            this.lblEmAndamento.Location = new System.Drawing.Point(37, 176);
            this.lblEmAndamento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmAndamento.Name = "lblEmAndamento";
            this.lblEmAndamento.Size = new System.Drawing.Size(98, 16);
            this.lblEmAndamento.TabIndex = 4;
            this.lblEmAndamento.Text = "Em andamento";
            // 
            // lblPendente
            // 
            this.lblPendente.AutoSize = true;
            this.lblPendente.Location = new System.Drawing.Point(37, 75);
            this.lblPendente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPendente.Name = "lblPendente";
            this.lblPendente.Size = new System.Drawing.Size(65, 16);
            this.lblPendente.TabIndex = 3;
            this.lblPendente.Text = "Pendente";
            // 
            // pnPendente
            // 
            this.pnPendente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnPendente.Location = new System.Drawing.Point(5, 65);
            this.pnPendente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnPendente.Name = "pnPendente";
            this.pnPendente.Size = new System.Drawing.Size(23, 25);
            this.pnPendente.TabIndex = 2;
            // 
            // pnEmAndamento
            // 
            this.pnEmAndamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnEmAndamento.Location = new System.Drawing.Point(5, 166);
            this.pnEmAndamento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnEmAndamento.Name = "pnEmAndamento";
            this.pnEmAndamento.Size = new System.Drawing.Size(23, 25);
            this.pnEmAndamento.TabIndex = 0;
            // 
            // plotStatus
            // 
            this.plotStatus.DisplayScale = 0F;
            this.plotStatus.Location = new System.Drawing.Point(3, 18);
            this.plotStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotStatus.Name = "plotStatus";
            this.plotStatus.Size = new System.Drawing.Size(337, 441);
            this.plotStatus.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.roundedPanel3);
            this.groupBox2.Controls.Add(this.plotCategoria);
            this.groupBox2.Location = new System.Drawing.Point(667, 326);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(484, 462);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
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
            this.roundedPanel3.Location = new System.Drawing.Point(316, 17);
            this.roundedPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(165, 443);
            this.roundedPanel3.TabIndex = 4;
            // 
            // lblIncidentes
            // 
            this.lblIncidentes.AutoSize = true;
            this.lblIncidentes.Location = new System.Drawing.Point(37, 385);
            this.lblIncidentes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncidentes.Name = "lblIncidentes";
            this.lblIncidentes.Size = new System.Drawing.Size(68, 16);
            this.lblIncidentes.TabIndex = 17;
            this.lblIncidentes.Text = "Incidentes";
            // 
            // pnIncidentes
            // 
            this.pnIncidentes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnIncidentes.Location = new System.Drawing.Point(5, 375);
            this.pnIncidentes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnIncidentes.Name = "pnIncidentes";
            this.pnIncidentes.Size = new System.Drawing.Size(23, 25);
            this.pnIncidentes.TabIndex = 16;
            // 
            // lblComunica
            // 
            this.lblComunica.AutoSize = true;
            this.lblComunica.Location = new System.Drawing.Point(37, 340);
            this.lblComunica.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComunica.Name = "lblComunica";
            this.lblComunica.Size = new System.Drawing.Size(90, 16);
            this.lblComunica.TabIndex = 15;
            this.lblComunica.Text = "Comunicacao";
            // 
            // pnComunica
            // 
            this.pnComunica.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnComunica.Location = new System.Drawing.Point(5, 330);
            this.pnComunica.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnComunica.Name = "pnComunica";
            this.pnComunica.Size = new System.Drawing.Size(23, 25);
            this.pnComunica.TabIndex = 14;
            // 
            // lblServicos
            // 
            this.lblServicos.AutoSize = true;
            this.lblServicos.Location = new System.Drawing.Point(37, 238);
            this.lblServicos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServicos.Name = "lblServicos";
            this.lblServicos.Size = new System.Drawing.Size(60, 16);
            this.lblServicos.TabIndex = 13;
            this.lblServicos.Text = "Servicos";
            // 
            // pnServicos
            // 
            this.pnServicos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnServicos.Location = new System.Drawing.Point(5, 228);
            this.pnServicos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnServicos.Name = "pnServicos";
            this.pnServicos.Size = new System.Drawing.Size(23, 25);
            this.pnServicos.TabIndex = 12;
            // 
            // lblRede
            // 
            this.lblRede.AutoSize = true;
            this.lblRede.Location = new System.Drawing.Point(37, 190);
            this.lblRede.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRede.Name = "lblRede";
            this.lblRede.Size = new System.Drawing.Size(41, 16);
            this.lblRede.TabIndex = 11;
            this.lblRede.Text = "Rede";
            // 
            // pnRede
            // 
            this.pnRede.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnRede.Location = new System.Drawing.Point(5, 180);
            this.pnRede.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnRede.Name = "pnRede";
            this.pnRede.Size = new System.Drawing.Size(23, 25);
            this.pnRede.TabIndex = 10;
            // 
            // lblSoftware
            // 
            this.lblSoftware.AutoSize = true;
            this.lblSoftware.Location = new System.Drawing.Point(37, 90);
            this.lblSoftware.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoftware.Name = "lblSoftware";
            this.lblSoftware.Size = new System.Drawing.Size(59, 16);
            this.lblSoftware.TabIndex = 9;
            this.lblSoftware.Text = "Software";
            // 
            // pnSoftware
            // 
            this.pnSoftware.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSoftware.Location = new System.Drawing.Point(5, 80);
            this.pnSoftware.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnSoftware.Name = "pnSoftware";
            this.pnSoftware.Size = new System.Drawing.Size(23, 25);
            this.pnSoftware.TabIndex = 8;
            // 
            // lblInfra
            // 
            this.lblInfra.AutoSize = true;
            this.lblInfra.Location = new System.Drawing.Point(37, 290);
            this.lblInfra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfra.Name = "lblInfra";
            this.lblInfra.Size = new System.Drawing.Size(83, 16);
            this.lblInfra.TabIndex = 7;
            this.lblInfra.Text = "Infraestrutura";
            // 
            // pnInfra
            // 
            this.pnInfra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnInfra.Location = new System.Drawing.Point(5, 281);
            this.pnInfra.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnInfra.Name = "pnInfra";
            this.pnInfra.Size = new System.Drawing.Size(23, 25);
            this.pnInfra.TabIndex = 6;
            // 
            // lblSeguranca
            // 
            this.lblSeguranca.AutoSize = true;
            this.lblSeguranca.Location = new System.Drawing.Point(37, 142);
            this.lblSeguranca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeguranca.Name = "lblSeguranca";
            this.lblSeguranca.Size = new System.Drawing.Size(73, 16);
            this.lblSeguranca.TabIndex = 4;
            this.lblSeguranca.Text = "Seguranca";
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Location = new System.Drawing.Point(37, 42);
            this.lblHardware.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(66, 16);
            this.lblHardware.TabIndex = 3;
            this.lblHardware.Text = "Hardware";
            // 
            // pnHardware
            // 
            this.pnHardware.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnHardware.Location = new System.Drawing.Point(5, 32);
            this.pnHardware.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnHardware.Name = "pnHardware";
            this.pnHardware.Size = new System.Drawing.Size(23, 25);
            this.pnHardware.TabIndex = 2;
            // 
            // pnSeguranca
            // 
            this.pnSeguranca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSeguranca.Location = new System.Drawing.Point(5, 132);
            this.pnSeguranca.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnSeguranca.Name = "pnSeguranca";
            this.pnSeguranca.Size = new System.Drawing.Size(23, 25);
            this.pnSeguranca.TabIndex = 0;
            // 
            // plotCategoria
            // 
            this.plotCategoria.DisplayScale = 0F;
            this.plotCategoria.Location = new System.Drawing.Point(7, 18);
            this.plotCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotCategoria.Name = "plotCategoria";
            this.plotCategoria.Size = new System.Drawing.Size(291, 441);
            this.plotCategoria.TabIndex = 0;
            // 
            // lbl_NomeUser
            // 
            this.lbl_NomeUser.AutoSize = true;
            this.lbl_NomeUser.BackColor = System.Drawing.Color.Transparent;
            this.lbl_NomeUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NomeUser.Location = new System.Drawing.Point(37, 18);
            this.lbl_NomeUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NomeUser.Name = "lbl_NomeUser";
            this.lbl_NomeUser.Size = new System.Drawing.Size(267, 39);
            this.lbl_NomeUser.TabIndex = 9;
            this.lbl_NomeUser.Text = "                         ";
            // 
            // btnChamadosPendentes
            // 
            this.btnChamadosPendentes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChamadosPendentes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChamadosPendentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamadosPendentes.Image = global::Gerenciamento_De_Chamados.Properties.Resources.folder_46dp_EAC452_FILL0_wght400_GRAD0_opsz48;
            this.btnChamadosPendentes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChamadosPendentes.Location = new System.Drawing.Point(23, 170);
            this.btnChamadosPendentes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChamadosPendentes.Name = "btnChamadosPendentes";
            this.btnChamadosPendentes.Size = new System.Drawing.Size(340, 121);
            this.btnChamadosPendentes.TabIndex = 13;
            this.btnChamadosPendentes.Text = "     Chamados \r\n     pendentes";
            this.btnChamadosPendentes.UseVisualStyleBackColor = true;
            this.btnChamadosPendentes.Click += new System.EventHandler(this.btnChamadosPendentes_Click);
            // 
            // btnChamadosResolvidos
            // 
            this.btnChamadosResolvidos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChamadosResolvidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChamadosResolvidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamadosResolvidos.Image = global::Gerenciamento_De_Chamados.Properties.Resources.check_circle_46dp_9DC384_FILL0_wght400_GRAD0_opsz48;
            this.btnChamadosResolvidos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChamadosResolvidos.Location = new System.Drawing.Point(864, 170);
            this.btnChamadosResolvidos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChamadosResolvidos.Name = "btnChamadosResolvidos";
            this.btnChamadosResolvidos.Size = new System.Drawing.Size(340, 121);
            this.btnChamadosResolvidos.TabIndex = 14;
            this.btnChamadosResolvidos.Text = "      Chamados\r\n       solucionados";
            this.btnChamadosResolvidos.UseVisualStyleBackColor = true;
            this.btnChamadosResolvidos.Click += new System.EventHandler(this.btnChamadosResolvidos_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.lbl_Inicio);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(308, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1335, 82);
            this.panel2.TabIndex = 11;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_51dp_000000_FILL0_wght400_GRAD0_opsz48__1_;
            this.pictureBox4.Location = new System.Drawing.Point(1251, 16);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(68, 62);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // lbl_Inicio
            // 
            this.lbl_Inicio.AutoSize = true;
            this.lbl_Inicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Inicio.Image = global::Gerenciamento_De_Chamados.Properties.Resources.HOME_36p;
            this.lbl_Inicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Inicio.Location = new System.Drawing.Point(32, 30);
            this.lbl_Inicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Inicio.Name = "lbl_Inicio";
            this.lbl_Inicio.Size = new System.Drawing.Size(127, 31);
            this.lbl_Inicio.TabIndex = 5;
            this.lbl_Inicio.Text = "       Início";
            // 
            // lbSair
            // 
            this.lbSair.AutoSize = true;
            this.lbSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSair.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbSair.Image = global::Gerenciamento_De_Chamados.Properties.Resources.move_item_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.lbSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSair.Location = new System.Drawing.Point(27, 441);
            this.lbSair.Name = "lbSair";
            this.lbSair.Size = new System.Drawing.Size(92, 29);
            this.lbSair.TabIndex = 17;
            this.lbSair.Text = "      Sair";
            this.lbSair.Click += new System.EventHandler(this.lbSair_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbSair);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnMeusChamados);
            this.panel1.Controls.Add(this.PctBox_Logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 977);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = global::Gerenciamento_De_Chamados.Properties.Resources.Suporte;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(27, 384);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 29);
            this.label3.TabIndex = 13;
            this.label3.Text = "      FAQ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnMeusChamados
            // 
            this.btnMeusChamados.AutoSize = true;
            this.btnMeusChamados.BackColor = System.Drawing.Color.Transparent;
            this.btnMeusChamados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMeusChamados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeusChamados.Image = global::Gerenciamento_De_Chamados.Properties.Resources.menu;
            this.btnMeusChamados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMeusChamados.Location = new System.Drawing.Point(27, 321);
            this.btnMeusChamados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMeusChamados.Name = "btnMeusChamados";
            this.btnMeusChamados.Size = new System.Drawing.Size(230, 29);
            this.btnMeusChamados.TabIndex = 14;
            this.btnMeusChamados.Text = "      Meus Chamados";
            this.btnMeusChamados.Click += new System.EventHandler(this.btnMeusChamados_Click);
            // 
            // PctBox_Logo
            // 
            this.PctBox_Logo.Image = global::Gerenciamento_De_Chamados.Properties.Resources.Imagem_do_WhatsApp_de_2025_09_09_à_s__21_56_18_5730b37d___Editado;
            this.PctBox_Logo.Location = new System.Drawing.Point(-45, 0);
            this.PctBox_Logo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PctBox_Logo.Name = "PctBox_Logo";
            this.PctBox_Logo.Size = new System.Drawing.Size(393, 318);
            this.PctBox_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PctBox_Logo.TabIndex = 3;
            this.PctBox_Logo.TabStop = false;
            // 
            // HomeFuncionario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1643, 977);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundedPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "HomeFuncionario";
            this.Text = "HomeUsuario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HomeFuncionario_Paint);
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Button btnChamadosPendentes;
        private System.Windows.Forms.Button btnChamadosResolvidos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_AbrirChamado;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lbl_Inicio;
        private System.Windows.Forms.Label lbSair;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label btnMeusChamados;
        private System.Windows.Forms.PictureBox PctBox_Logo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnChamadosEmAndamento;
        private System.Windows.Forms.Label lbl_NomeUser;
        private ScottPlot.WinForms.FormsPlot plotStatus;
        private ScottPlot.WinForms.FormsPlot plotCategoria;
        private RoundedPanel roundedPanel3;
        private System.Windows.Forms.Label lblInfra;
        private System.Windows.Forms.Panel pnInfra;
        private System.Windows.Forms.Label lblSeguranca;
        private System.Windows.Forms.Label lblHardware;
        private System.Windows.Forms.Panel pnHardware;
        private System.Windows.Forms.Panel pnSeguranca;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Label lblResolvido;
        private System.Windows.Forms.Panel pnResolvido;
        private System.Windows.Forms.Label lblEmAndamento;
        private System.Windows.Forms.Label lblPendente;
        private System.Windows.Forms.Panel pnPendente;
        private System.Windows.Forms.Panel pnEmAndamento;
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
    }
}