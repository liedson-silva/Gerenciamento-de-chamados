namespace Gerenciamento_De_Chamados
{
    partial class VisualizarChamado
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
            this.dgvChamados = new System.Windows.Forms.DataGridView();
            this.idChamadoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKIdUsuarioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tituloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataChamadoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoriaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prioridadeChamadoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusChamadoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chamadoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._DbaFatal_SystemDataSet1 = new Gerenciamento_De_Chamados._DbaFatal_SystemDataSet1();
            this.txtPesquisarChamados = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._DbaFatal_SystemDataSet = new Gerenciamento_De_Chamados._DbaFatal_SystemDataSet();
            this.dbaFatalSystemDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chamadoTableAdapter = new Gerenciamento_De_Chamados._DbaFatal_SystemDataSet1TableAdapters.ChamadoTableAdapter();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chamadoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._DbaFatal_SystemDataSet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._DbaFatal_SystemDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbaFatalSystemDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvChamados
            // 
            this.dgvChamados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChamados.AutoGenerateColumns = false;
            this.dgvChamados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChamados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChamados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idChamadoDataGridViewTextBoxColumn,
            this.fKIdUsuarioDataGridViewTextBoxColumn,
            this.tituloDataGridViewTextBoxColumn,
            this.dataChamadoDataGridViewTextBoxColumn,
            this.categoriaDataGridViewTextBoxColumn,
            this.descricaoDataGridViewTextBoxColumn,
            this.prioridadeChamadoDataGridViewTextBoxColumn,
            this.statusChamadoDataGridViewTextBoxColumn});
            this.dgvChamados.DataSource = this.chamadoBindingSource;
            this.dgvChamados.Location = new System.Drawing.Point(317, 431);
            this.dgvChamados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvChamados.Name = "dgvChamados";
            this.dgvChamados.RowHeadersWidth = 51;
            this.dgvChamados.Size = new System.Drawing.Size(1284, 321);
            this.dgvChamados.TabIndex = 17;
            // 
            // idChamadoDataGridViewTextBoxColumn
            // 
            this.idChamadoDataGridViewTextBoxColumn.DataPropertyName = "IdChamado";
            this.idChamadoDataGridViewTextBoxColumn.HeaderText = "IdChamado";
            this.idChamadoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idChamadoDataGridViewTextBoxColumn.Name = "idChamadoDataGridViewTextBoxColumn";
            this.idChamadoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fKIdUsuarioDataGridViewTextBoxColumn
            // 
            this.fKIdUsuarioDataGridViewTextBoxColumn.DataPropertyName = "FK_IdUsuario";
            this.fKIdUsuarioDataGridViewTextBoxColumn.HeaderText = "Usuario";
            this.fKIdUsuarioDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fKIdUsuarioDataGridViewTextBoxColumn.Name = "fKIdUsuarioDataGridViewTextBoxColumn";
            // 
            // tituloDataGridViewTextBoxColumn
            // 
            this.tituloDataGridViewTextBoxColumn.DataPropertyName = "Titulo";
            this.tituloDataGridViewTextBoxColumn.HeaderText = "Titulo";
            this.tituloDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tituloDataGridViewTextBoxColumn.Name = "tituloDataGridViewTextBoxColumn";
            // 
            // dataChamadoDataGridViewTextBoxColumn
            // 
            this.dataChamadoDataGridViewTextBoxColumn.DataPropertyName = "DataChamado";
            this.dataChamadoDataGridViewTextBoxColumn.HeaderText = "Data";
            this.dataChamadoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dataChamadoDataGridViewTextBoxColumn.Name = "dataChamadoDataGridViewTextBoxColumn";
            // 
            // categoriaDataGridViewTextBoxColumn
            // 
            this.categoriaDataGridViewTextBoxColumn.DataPropertyName = "Categoria";
            this.categoriaDataGridViewTextBoxColumn.HeaderText = "Categoria";
            this.categoriaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.categoriaDataGridViewTextBoxColumn.Name = "categoriaDataGridViewTextBoxColumn";
            // 
            // descricaoDataGridViewTextBoxColumn
            // 
            this.descricaoDataGridViewTextBoxColumn.DataPropertyName = "Descricao";
            this.descricaoDataGridViewTextBoxColumn.HeaderText = "Descricao";
            this.descricaoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.descricaoDataGridViewTextBoxColumn.Name = "descricaoDataGridViewTextBoxColumn";
            // 
            // prioridadeChamadoDataGridViewTextBoxColumn
            // 
            this.prioridadeChamadoDataGridViewTextBoxColumn.DataPropertyName = "PrioridadeChamado";
            this.prioridadeChamadoDataGridViewTextBoxColumn.HeaderText = "Prioridade";
            this.prioridadeChamadoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prioridadeChamadoDataGridViewTextBoxColumn.Name = "prioridadeChamadoDataGridViewTextBoxColumn";
            // 
            // statusChamadoDataGridViewTextBoxColumn
            // 
            this.statusChamadoDataGridViewTextBoxColumn.DataPropertyName = "StatusChamado";
            this.statusChamadoDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusChamadoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusChamadoDataGridViewTextBoxColumn.Name = "statusChamadoDataGridViewTextBoxColumn";
            // 
            // chamadoBindingSource
            // 
            this.chamadoBindingSource.DataMember = "Chamado";
            this.chamadoBindingSource.DataSource = this._DbaFatal_SystemDataSet1;
            // 
            // _DbaFatal_SystemDataSet1
            // 
            this._DbaFatal_SystemDataSet1.DataSetName = "_DbaFatal_SystemDataSet1";
            this._DbaFatal_SystemDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtPesquisarChamados
            // 
            this.txtPesquisarChamados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisarChamados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisarChamados.Location = new System.Drawing.Point(463, 290);
            this.txtPesquisarChamados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPesquisarChamados.Multiline = true;
            this.txtPesquisarChamados.Name = "txtPesquisarChamados";
            this.txtPesquisarChamados.Size = new System.Drawing.Size(1033, 42);
            this.txtPesquisarChamados.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(315, 290);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Pesquisar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(308, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(362, 46);
            this.label2.TabIndex = 14;
            this.label2.Text = "Chamados Criados";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(305, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1326, 103);
            this.panel2.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Image = global::Gerenciamento_De_Chamados.Properties.Resources.HOME__2_;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(77, 42);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 31);
            this.label9.TabIndex = 5;
            this.label9.Text = "      Início";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 958);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = global::Gerenciamento_De_Chamados.Properties.Resources.contact_support_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(64, 384);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "      FAQ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::Gerenciamento_De_Chamados.Properties.Resources.menu_24dp_000000_FILL0_wght400_GRAD0_opsz24__1_;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(64, 303);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "      Meus Chamados";
            // 
            // _DbaFatal_SystemDataSet
            // 
            this._DbaFatal_SystemDataSet.DataSetName = "_DbaFatal_SystemDataSet";
            this._DbaFatal_SystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dbaFatalSystemDataSetBindingSource
            // 
            this.dbaFatalSystemDataSetBindingSource.DataSource = this._DbaFatal_SystemDataSet;
            this.dbaFatalSystemDataSetBindingSource.Position = 0;
            // 
            // chamadoTableAdapter
            // 
            this.chamadoTableAdapter.ClearBeforeFill = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = global::Gerenciamento_De_Chamados.Properties.Resources.account_circle_51dp_000000_FILL0_wght400_GRAD0_opsz48__1_;
            this.pictureBox4.Location = new System.Drawing.Point(1242, 16);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(68, 62);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Gerenciamento_De_Chamados.Properties.Resources.Imagem_do_WhatsApp_de_2025_09_09_à_s__21_56_18_5730b37d___Editado;
            this.pictureBox1.Location = new System.Drawing.Point(-47, -63);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(393, 298);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // VisualizarChamado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1631, 958);
            this.Controls.Add(this.dgvChamados);
            this.Controls.Add(this.txtPesquisarChamados);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "VisualizarChamado";
            this.Text = "VisualizarChamado";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VisualizarChamado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chamadoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._DbaFatal_SystemDataSet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._DbaFatal_SystemDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbaFatalSystemDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvChamados;
        private System.Windows.Forms.BindingSource dbaFatalSystemDataSetBindingSource;
        private _DbaFatal_SystemDataSet _DbaFatal_SystemDataSet;
        private System.Windows.Forms.TextBox txtPesquisarChamados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private _DbaFatal_SystemDataSet1 _DbaFatal_SystemDataSet1;
        private System.Windows.Forms.BindingSource chamadoBindingSource;
        private _DbaFatal_SystemDataSet1TableAdapters.ChamadoTableAdapter chamadoTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idChamadoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fKIdUsuarioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tituloDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataChamadoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoriaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prioridadeChamadoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusChamadoDataGridViewTextBoxColumn;
    }
}