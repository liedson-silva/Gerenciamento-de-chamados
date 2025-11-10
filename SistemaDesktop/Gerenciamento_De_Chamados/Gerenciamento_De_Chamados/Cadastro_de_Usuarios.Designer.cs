namespace Gerenciamento_De_Chamados
{
    partial class Cadastro_de_Usuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cadastro_de_Usuarios));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSair = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PctBox_Inicio = new System.Windows.Forms.PictureBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblCadastroSenha = new System.Windows.Forms.Label();
            this.txtCadastroEmail = new System.Windows.Forms.TextBox();
            this.txtCadastroSenha = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnCadastroAdd = new System.Windows.Forms.Button();
            this.btnCadastroCancel = new System.Windows.Forms.Button();
            this.PctBox_Logo = new System.Windows.Forms.Panel();
            this.lbl_NomeUser = new System.Windows.Forms.Label();
            this.lbl_Inicio = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblCPF = new System.Windows.Forms.Label();
            this.lblDN = new System.Windows.Forms.Label();
            this.lblSexo = new System.Windows.Forms.Label();
            this.lblSetor = new System.Windows.Forms.Label();
            this.lblFuncao = new System.Windows.Forms.Label();
            this.dtpCadDN = new System.Windows.Forms.DateTimePicker();
            this.comboBoxCadastroSexo = new System.Windows.Forms.ComboBox();
            this.lblCadastroUsuario = new System.Windows.Forms.Label();
            this.txtCadastroLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblRG = new System.Windows.Forms.Label();
            this.cBoxCadSetor = new System.Windows.Forms.ComboBox();
            this.cbxCadastroFuncao = new System.Windows.Forms.ComboBox();
            this.panelFormularioCriar = new Gerenciamento_De_Chamados.RoundedPanel();
            this.txtCadastroRG = new System.Windows.Forms.MaskedTextBox();
            this.txtCadastroCpf = new System.Windows.Forms.MaskedTextBox();
            this.txtCadastroNome = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Inicio)).BeginInit();
            this.PctBox_Logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panelFormularioCriar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbSair);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PctBox_Inicio);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 977);
            this.panel1.TabIndex = 5;
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
            this.lbSair.Location = new System.Drawing.Point(23, 427);
            this.lbSair.Name = "lbSair";
            this.lbSair.Size = new System.Drawing.Size(92, 29);
            this.lbSair.TabIndex = 17;
            this.lbSair.Text = "      Sair";
            this.lbSair.Click += new System.EventHandler(this.lbSair_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = global::Gerenciamento_De_Chamados.Properties.Resources.contact_support_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(23, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "      FAQ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::Gerenciamento_De_Chamados.Properties.Resources.menu_24dp_000000_FILL0_wght400_GRAD0_opsz24__1_;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(23, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "      Meus Chamados";
            // 
            // PctBox_Inicio
            // 
            this.PctBox_Inicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PctBox_Inicio.Image = ((System.Drawing.Image)(resources.GetObject("PctBox_Inicio.Image")));
            this.PctBox_Inicio.Location = new System.Drawing.Point(-35, 0);
            this.PctBox_Inicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PctBox_Inicio.Name = "PctBox_Inicio";
            this.PctBox_Inicio.Size = new System.Drawing.Size(387, 294);
            this.PctBox_Inicio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PctBox_Inicio.TabIndex = 10;
            this.PctBox_Inicio.TabStop = false;
            this.PctBox_Inicio.Click += new System.EventHandler(this.PctBox_Inicio_Click);
            // 
            // lblNome
            // 
            this.lblNome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(4, 95);
            this.lblNome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(101, 31);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome: ";
            // 
            // lblCadastroSenha
            // 
            this.lblCadastroSenha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblCadastroSenha.AutoSize = true;
            this.lblCadastroSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCadastroSenha.Location = new System.Drawing.Point(556, 576);
            this.lblCadastroSenha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCadastroSenha.Name = "lblCadastroSenha";
            this.lblCadastroSenha.Size = new System.Drawing.Size(117, 36);
            this.lblCadastroSenha.TabIndex = 3;
            this.lblCadastroSenha.Text = "Senha: ";
            // 
            // txtCadastroEmail
            // 
            this.txtCadastroEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtCadastroEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastroEmail.Location = new System.Drawing.Point(563, 377);
            this.txtCadastroEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtCadastroEmail.Name = "txtCadastroEmail";
            this.txtCadastroEmail.Size = new System.Drawing.Size(457, 41);
            this.txtCadastroEmail.TabIndex = 5;
            // 
            // txtCadastroSenha
            // 
            this.txtCadastroSenha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtCadastroSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastroSenha.Location = new System.Drawing.Point(563, 615);
            this.txtCadastroSenha.Margin = new System.Windows.Forms.Padding(4);
            this.txtCadastroSenha.Name = "txtCadastroSenha";
            this.txtCadastroSenha.Size = new System.Drawing.Size(457, 41);
            this.txtCadastroSenha.TabIndex = 6;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(556, 322);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(96, 36);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email:";
            // 
            // btnCadastroAdd
            // 
            this.btnCadastroAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadastroAdd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCadastroAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastroAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastroAdd.Location = new System.Drawing.Point(847, 746);
            this.btnCadastroAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnCadastroAdd.Name = "btnCadastroAdd";
            this.btnCadastroAdd.Size = new System.Drawing.Size(173, 69);
            this.btnCadastroAdd.TabIndex = 8;
            this.btnCadastroAdd.Text = "Adicionar novo usuario";
            this.btnCadastroAdd.UseVisualStyleBackColor = false;
            this.btnCadastroAdd.Click += new System.EventHandler(this.btnCadastroAdd_Click);
            // 
            // btnCadastroCancel
            // 
            this.btnCadastroCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCadastroCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastroCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastroCancel.Location = new System.Drawing.Point(616, 746);
            this.btnCadastroCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCadastroCancel.Name = "btnCadastroCancel";
            this.btnCadastroCancel.Size = new System.Drawing.Size(173, 69);
            this.btnCadastroCancel.TabIndex = 9;
            this.btnCadastroCancel.Text = "Cancelar";
            this.btnCadastroCancel.UseVisualStyleBackColor = true;
            // 
            // PctBox_Logo
            // 
            this.PctBox_Logo.BackColor = System.Drawing.Color.White;
            this.PctBox_Logo.Controls.Add(this.lbl_NomeUser);
            this.PctBox_Logo.Controls.Add(this.lbl_Inicio);
            this.PctBox_Logo.Controls.Add(this.pictureBox4);
            this.PctBox_Logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PctBox_Logo.Location = new System.Drawing.Point(325, 0);
            this.PctBox_Logo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PctBox_Logo.Name = "PctBox_Logo";
            this.PctBox_Logo.Size = new System.Drawing.Size(1096, 82);
            this.PctBox_Logo.TabIndex = 10;
            // 
            // lbl_NomeUser
            // 
            this.lbl_NomeUser.AutoSize = true;
            this.lbl_NomeUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NomeUser.Location = new System.Drawing.Point(919, 28);
            this.lbl_NomeUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NomeUser.Name = "lbl_NomeUser";
            this.lbl_NomeUser.Size = new System.Drawing.Size(0, 25);
            this.lbl_NomeUser.TabIndex = 7;
            // 
            // lbl_Inicio
            // 
            this.lbl_Inicio.AutoSize = true;
            this.lbl_Inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Inicio.Image = global::Gerenciamento_De_Chamados.Properties.Resources.HOME_36p;
            this.lbl_Inicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Inicio.Location = new System.Drawing.Point(49, 28);
            this.lbl_Inicio.Name = "lbl_Inicio";
            this.lbl_Inicio.Size = new System.Drawing.Size(120, 31);
            this.lbl_Inicio.TabIndex = 2;
            this.lbl_Inicio.Text = "      Início";
            this.lbl_Inicio.Click += new System.EventHandler(this.lbl_Inicio_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_51dp_000000_FILL0_wght400_GRAD0_opsz48__1_;
            this.pictureBox4.Location = new System.Drawing.Point(1035, 12);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(51, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // lblCPF
            // 
            this.lblCPF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblCPF.AutoSize = true;
            this.lblCPF.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPF.Location = new System.Drawing.Point(8, 217);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(77, 31);
            this.lblCPF.TabIndex = 11;
            this.lblCPF.Text = "CPF:";
            // 
            // lblDN
            // 
            this.lblDN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblDN.AutoSize = true;
            this.lblDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDN.Location = new System.Drawing.Point(8, 449);
            this.lblDN.Name = "lblDN";
            this.lblDN.Size = new System.Drawing.Size(263, 31);
            this.lblDN.TabIndex = 13;
            this.lblDN.Text = "Data de nascimento:";
            // 
            // lblSexo
            // 
            this.lblSexo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblSexo.AutoSize = true;
            this.lblSexo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSexo.Location = new System.Drawing.Point(8, 581);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(83, 31);
            this.lblSexo.TabIndex = 14;
            this.lblSexo.Text = "Sexo:";
            // 
            // lblSetor
            // 
            this.lblSetor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblSetor.AutoSize = true;
            this.lblSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetor.Location = new System.Drawing.Point(556, 95);
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Size = new System.Drawing.Size(87, 31);
            this.lblSetor.TabIndex = 15;
            this.lblSetor.Text = "Setor:";
            // 
            // lblFuncao
            // 
            this.lblFuncao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblFuncao.AutoSize = true;
            this.lblFuncao.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFuncao.Location = new System.Drawing.Point(556, 217);
            this.lblFuncao.Name = "lblFuncao";
            this.lblFuncao.Size = new System.Drawing.Size(113, 31);
            this.lblFuncao.TabIndex = 16;
            this.lblFuncao.Text = "Função:";
            // 
            // dtpCadDN
            // 
            this.dtpCadDN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dtpCadDN.CustomFormat = "dd/MM/yyyy";
            this.dtpCadDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCadDN.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCadDN.Location = new System.Drawing.Point(15, 495);
            this.dtpCadDN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpCadDN.Name = "dtpCadDN";
            this.dtpCadDN.Size = new System.Drawing.Size(465, 41);
            this.dtpCadDN.TabIndex = 17;
            this.dtpCadDN.Tag = "";
            this.dtpCadDN.Value = new System.DateTime(1900, 1, 1, 11, 45, 0, 0);
            // 
            // comboBoxCadastroSexo
            // 
            this.comboBoxCadastroSexo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.comboBoxCadastroSexo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCadastroSexo.FormattingEnabled = true;
            this.comboBoxCadastroSexo.Items.AddRange(new object[] {
            "Feminino",
            "Masculino",
            "Outros"});
            this.comboBoxCadastroSexo.Location = new System.Drawing.Point(15, 615);
            this.comboBoxCadastroSexo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCadastroSexo.Name = "comboBoxCadastroSexo";
            this.comboBoxCadastroSexo.Size = new System.Drawing.Size(465, 44);
            this.comboBoxCadastroSexo.TabIndex = 19;
            this.comboBoxCadastroSexo.Text = "Selecione";
            // 
            // lblCadastroUsuario
            // 
            this.lblCadastroUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblCadastroUsuario.AutoSize = true;
            this.lblCadastroUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCadastroUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCadastroUsuario.Location = new System.Drawing.Point(5, 31);
            this.lblCadastroUsuario.Name = "lblCadastroUsuario";
            this.lblCadastroUsuario.Size = new System.Drawing.Size(379, 46);
            this.lblCadastroUsuario.TabIndex = 22;
            this.lblCadastroUsuario.Text = "Cadastro de usuário";
            // 
            // txtCadastroLogin
            // 
            this.txtCadastroLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtCadastroLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastroLogin.Location = new System.Drawing.Point(563, 495);
            this.txtCadastroLogin.Margin = new System.Windows.Forms.Padding(4);
            this.txtCadastroLogin.Name = "txtCadastroLogin";
            this.txtCadastroLogin.Size = new System.Drawing.Size(457, 41);
            this.txtCadastroLogin.TabIndex = 24;
            // 
            // lblLogin
            // 
            this.lblLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(556, 446);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(106, 36);
            this.lblLogin.TabIndex = 23;
            this.lblLogin.Text = "Login: ";
            // 
            // lblRG
            // 
            this.lblRG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblRG.AutoSize = true;
            this.lblRG.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRG.Location = new System.Drawing.Point(8, 322);
            this.lblRG.Name = "lblRG";
            this.lblRG.Size = new System.Drawing.Size(63, 31);
            this.lblRG.TabIndex = 25;
            this.lblRG.Text = "RG:";
            // 
            // cBoxCadSetor
            // 
            this.cBoxCadSetor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cBoxCadSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxCadSetor.FormattingEnabled = true;
            this.cBoxCadSetor.Items.AddRange(new object[] {
            "RH",
            "Financeiro",
            "Administrativo",
            "Operador"});
            this.cBoxCadSetor.Location = new System.Drawing.Point(563, 148);
            this.cBoxCadSetor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBoxCadSetor.Name = "cBoxCadSetor";
            this.cBoxCadSetor.Size = new System.Drawing.Size(457, 44);
            this.cBoxCadSetor.TabIndex = 27;
            this.cBoxCadSetor.Text = "Selecione";
            // 
            // cbxCadastroFuncao
            // 
            this.cbxCadastroFuncao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cbxCadastroFuncao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCadastroFuncao.FormattingEnabled = true;
            this.cbxCadastroFuncao.Items.AddRange(new object[] {
            "Admin",
            "Equipe De TI",
            "Supervisor",
            "Tecnico",
            "Operador",
            "Terceirizado",
            "Outros.."});
            this.cbxCadastroFuncao.Location = new System.Drawing.Point(563, 251);
            this.cbxCadastroFuncao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxCadastroFuncao.Name = "cbxCadastroFuncao";
            this.cbxCadastroFuncao.Size = new System.Drawing.Size(457, 44);
            this.cbxCadastroFuncao.TabIndex = 29;
            this.cbxCadastroFuncao.Text = "Selecione";
            // 
            // panelFormularioCriar
            // 
            this.panelFormularioCriar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFormularioCriar.BackColor = System.Drawing.Color.White;
            this.panelFormularioCriar.BorderColor = System.Drawing.Color.White;
            this.panelFormularioCriar.BorderWidth = 1F;
            this.panelFormularioCriar.Controls.Add(this.txtCadastroRG);
            this.panelFormularioCriar.Controls.Add(this.txtCadastroCpf);
            this.panelFormularioCriar.Controls.Add(this.btnCadastroCancel);
            this.panelFormularioCriar.Controls.Add(this.btnCadastroAdd);
            this.panelFormularioCriar.Controls.Add(this.lblDN);
            this.panelFormularioCriar.Controls.Add(this.lblCadastroSenha);
            this.panelFormularioCriar.Controls.Add(this.lblNome);
            this.panelFormularioCriar.Controls.Add(this.lblSexo);
            this.panelFormularioCriar.Controls.Add(this.lblCadastroUsuario);
            this.panelFormularioCriar.Controls.Add(this.lblRG);
            this.panelFormularioCriar.Controls.Add(this.cbxCadastroFuncao);
            this.panelFormularioCriar.Controls.Add(this.cBoxCadSetor);
            this.panelFormularioCriar.Controls.Add(this.txtCadastroLogin);
            this.panelFormularioCriar.Controls.Add(this.lblLogin);
            this.panelFormularioCriar.Controls.Add(this.comboBoxCadastroSexo);
            this.panelFormularioCriar.Controls.Add(this.dtpCadDN);
            this.panelFormularioCriar.Controls.Add(this.lblFuncao);
            this.panelFormularioCriar.Controls.Add(this.lblSetor);
            this.panelFormularioCriar.Controls.Add(this.lblCPF);
            this.panelFormularioCriar.Controls.Add(this.txtCadastroNome);
            this.panelFormularioCriar.Controls.Add(this.txtCadastroEmail);
            this.panelFormularioCriar.Controls.Add(this.lblEmail);
            this.panelFormularioCriar.Controls.Add(this.txtCadastroSenha);
            this.panelFormularioCriar.CornerRadius = 15F;
            this.panelFormularioCriar.Location = new System.Drawing.Point(349, 110);
            this.panelFormularioCriar.Margin = new System.Windows.Forms.Padding(4);
            this.panelFormularioCriar.Name = "panelFormularioCriar";
            this.panelFormularioCriar.Size = new System.Drawing.Size(1056, 830);
            this.panelFormularioCriar.TabIndex = 30;
            // 
            // txtCadastroRG
            // 
            this.txtCadastroRG.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCadastroRG.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastroRG.Location = new System.Drawing.Point(15, 377);
            this.txtCadastroRG.Margin = new System.Windows.Forms.Padding(4);
            this.txtCadastroRG.Mask = "00.000.000-0";
            this.txtCadastroRG.Name = "txtCadastroRG";
            this.txtCadastroRG.Size = new System.Drawing.Size(463, 34);
            this.txtCadastroRG.TabIndex = 32;
            // 
            // txtCadastroCpf
            // 
            this.txtCadastroCpf.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCadastroCpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastroCpf.Location = new System.Drawing.Point(15, 261);
            this.txtCadastroCpf.Margin = new System.Windows.Forms.Padding(4);
            this.txtCadastroCpf.Mask = "000.000.000-00";
            this.txtCadastroCpf.Name = "txtCadastroCpf";
            this.txtCadastroCpf.Size = new System.Drawing.Size(463, 34);
            this.txtCadastroCpf.TabIndex = 31;
            // 
            // txtCadastroNome
            // 
            this.txtCadastroNome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtCadastroNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastroNome.Location = new System.Drawing.Point(15, 150);
            this.txtCadastroNome.Margin = new System.Windows.Forms.Padding(4);
            this.txtCadastroNome.Name = "txtCadastroNome";
            this.txtCadastroNome.Size = new System.Drawing.Size(457, 41);
            this.txtCadastroNome.TabIndex = 4;
            // 
            // Cadastro_de_Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1421, 977);
            this.Controls.Add(this.panelFormularioCriar);
            this.Controls.Add(this.PctBox_Logo);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Cadastro_de_Usuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro_de_Usuarios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Cadastro_de_Usuarios_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PctBox_Inicio)).EndInit();
            this.PctBox_Logo.ResumeLayout(false);
            this.PctBox_Logo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panelFormularioCriar.ResumeLayout(false);
            this.panelFormularioCriar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PctBox_Inicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblCadastroSenha;
        private System.Windows.Forms.TextBox txtCadastroEmail;
        private System.Windows.Forms.TextBox txtCadastroSenha;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnCadastroAdd;
        private System.Windows.Forms.Button btnCadastroCancel;
        private System.Windows.Forms.Panel PctBox_Logo;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.Label lblDN;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.Label lblFuncao;
        private System.Windows.Forms.DateTimePicker dtpCadDN;
        private System.Windows.Forms.ComboBox comboBoxCadastroSexo;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblCadastroUsuario;
        private System.Windows.Forms.Label lbl_Inicio;
        private System.Windows.Forms.TextBox txtCadastroLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblRG;
        private System.Windows.Forms.ComboBox cBoxCadSetor;
        private System.Windows.Forms.Label lbl_NomeUser;
        private System.Windows.Forms.ComboBox cbxCadastroFuncao;
        private System.Windows.Forms.Label lbSair;
        private RoundedPanel panelFormularioCriar;
        private System.Windows.Forms.MaskedTextBox txtCadastroCpf;
        private System.Windows.Forms.TextBox txtCadastroNome;
        private System.Windows.Forms.MaskedTextBox txtCadastroRG;
    }
}