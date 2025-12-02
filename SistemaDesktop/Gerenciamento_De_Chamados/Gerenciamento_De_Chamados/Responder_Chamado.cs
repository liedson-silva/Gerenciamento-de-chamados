using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    public partial class Responder_Chamado : Form
    {

        private readonly IChamadoRepository _chamadoRepository;
     
        private readonly IAIService _aiService;
        private DataTable chamadosTable = new DataTable();

        public Responder_Chamado()
        {
            InitializeComponent();


            _chamadoRepository = new ChamadoRepository();
            // Instancia o serviço de IA, que será usado se a triagem ainda não foi feita
            _aiService = new AIService();

            ConfigurarGrade();
            this.Load += Responder_Chamado_Load;
        }

        private void Responder_Chamado_Load(object sender, EventArgs e)
        {
            CarregarChamados();
            // LÓGICA DE VISIBILIDADE DOS BOTÕES
            string funcao = SessaoUsuario.FuncaoUsuario?.Trim();
            bool Admin = funcao?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;
            


           
            if (this.Controls.Find("btnEditarCH", true).FirstOrDefault() is Button btnEditar)
            {
                btnEditar.Visible = Admin;
            }

            
            if (this.Controls.Find("bt_Criar", true).FirstOrDefault() is Button btnCriar)
            {
                btnCriar.Visible = Admin;
            }

           


        }

        private void ConfigurarGrade()
        {
            
            dgvResponder.RowTemplate.Height = 35;
            dgvResponder.ColumnHeadersHeight = 35;
            dgvResponder.AutoGenerateColumns = false;
            dgvResponder.Columns.Clear();
           
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdChamado", DataPropertyName = "IdChamado", HeaderText = "ID", Width = 60 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Titulo", DataPropertyName = "Titulo", HeaderText = "Titulo", Width = 250 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descricao", DataPropertyName = "Descricao", HeaderText = "Descricao", Width = 450 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Prioridade", DataPropertyName = "Prioridade", HeaderText = "Prioridade", Width = 100 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "data", DataPropertyName = "data", HeaderText = "Data", Width = 100 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", DataPropertyName = "Status", HeaderText = "Status", Width = 120 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdUsuario", DataPropertyName = "IdUsuario", HeaderText = "Usuario ID", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }


        private void CarregarChamados(string filtro = "")
        {

            try
            {
                // Chama o repositório
                chamadosTable = _chamadoRepository.BuscarTodosFiltrados(filtro);
                dgvResponder.DataSource = chamadosTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar chamados: " + ex.Message);
            }
        }

       
        private async Task ResponderChamadoAsync(int idChamadoSelecionado)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Busca os dados atuais do chamado
                Chamado chamado = await _chamadoRepository.BuscarPorIdAsync(idChamadoSelecionado);

                if (chamado == null)
                {
                    MessageBox.Show("Chamado não encontrado no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool statusAtualizado = false;

                // 2. VERIFICA E EXECUTA A IA, SE NECESSÁRIO
                if (string.IsNullOrEmpty(chamado.PrioridadeSugeridaIA) || chamado.PrioridadeSugeridaIA == "Pendente" || chamado.PrioridadeSugeridaIA == "Análise Pendente")
                {
                    MessageBox.Show("Este chamado ainda não foi triado. Iniciando análise da IA... Isso pode levar alguns segundos.", "Análise da IA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                       
                        List<string> solucoesAnteriores = await _chamadoRepository.BuscarSolucoesAnterioresAsync(chamado.Categoria);

                        var (novoProblema, novaPrioridade, novaSolucao) = await _aiService.AnalisarChamado(
                            chamado.Titulo, chamado.PessoasAfetadas, chamado.OcorreuAnteriormente,
                            chamado.ImpedeTrabalho, chamado.Descricao, chamado.Categoria, solucoesAnteriores
                        );

                        if (novaPrioridade != "Não identificado" && !novaPrioridade.Contains("Erro"))
                        {
                            // Atualiza a triagem no BD
                            await _chamadoRepository.AtualizarSugestoesIAAsync(idChamadoSelecionado, novaPrioridade, novoProblema, novaSolucao);
                        }
                        else
                        {
                            MessageBox.Show($"A IA não conseguiu analisar o chamado. Detalhes: {novaSolucao}", "Erro na IA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        // Recarrega o chamado após a IA para pegar os novos dados
                        chamado = await _chamadoRepository.BuscarPorIdAsync(idChamadoSelecionado);
                    }
                    catch (Exception aiEx)
                    {
                        MessageBox.Show($"Erro ao executar a análise da IA: {aiEx.Message}", "Erro de IA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // 3. MUDA O STATUS PARA "EM ANDAMENTO" SE AINDA ESTIVER PENDENTE
                if (chamado.StatusChamado == "Pendente" || chamado.StatusChamado == "Análise Pendente")
                {
                    await _chamadoRepository.AtualizarStatusSimplesAsync(idChamadoSelecionado, "Em andamento");
                    statusAtualizado = true; 
                }

                // 4. ABRE A TELA DE ANÁLISE/RESPOSTA
                var analisechamado = new AnaliseChamado(idChamadoSelecionado);
                analisechamado.Show();
               
                

                // 5. RECARRREGAR O DATAGRID
                if (statusAtualizado || analisechamado.DialogResult == DialogResult.OK)
                {
                    CarregarChamados();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar o chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicioPanel = Color.White;
            Color corFimPanel = ColorTranslator.FromHtml("#232325");
            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                         panel1.ClientRectangle,
                         corInicioPanel,
                         corFimPanel,
                         LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);
        }

        private void Responder_Chamado_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicio = Color.White;
            Color corFim = ColorTranslator.FromHtml("#232325");

            using (LinearGradientBrush gradiente = new LinearGradientBrush(
                this.ClientRectangle, corInicio, corFim, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(gradiente, this.ClientRectangle);
            }
        }


        private async void dgvResponder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvResponder.Rows[e.RowIndex];
            object idValue = row.Cells["IdChamado"].Value;

            if (idValue != null && int.TryParse(idValue.ToString(), out int idChamadoSelecionado))
            {
                // Chama o método centralizado
                await ResponderChamadoAsync(idChamadoSelecionado);
            }
            else
            {
                MessageBox.Show("Não foi possível obter o ID do chamado selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        private async void btnResponderCH_Click(object sender, EventArgs e)
        {
            int? idChamado = ObterIdChamadoSelecionado();

            if (idChamado.HasValue)
            {
               
                await ResponderChamadoAsync(idChamado.Value);
            }
        }

        
        private void lblInicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Logo_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }


        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }

        private void bt_Criar_Click(object sender, EventArgs e)
        {
            var criarchamado = new AberturaChamados();
            criarchamado.Show();
            this.Close();
        }

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }
        private int? ObterIdChamadoSelecionado()
        {
            if (dgvResponder.CurrentRow == null || dgvResponder.CurrentRow.DataBoundItem == null)
            {

                MessageBox.Show("Por favor, selecione um chamado na lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {

                object idValue = dgvResponder.CurrentRow.Cells["IdChamado"].Value;

                // Validação robusta e conversão
                if (idValue != null && idValue != DBNull.Value && int.TryParse(idValue.ToString(), out int idChamadoSelecionado))
                {
                    return idChamadoSelecionado;
                }
                else
                {
                    // Caso a linha esteja selecionada mas o valor do ID seja inválido/nulo no dado
                    MessageBox.Show("O ID do chamado selecionado é inválido ou não foi encontrado nos dados.", "Erro de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erro interno ao ler o ID do chamado: {ex.Message}", "Erro de Leitura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnEditarCH_Click(object sender, EventArgs e)
        {
            int? idChamado = ObterIdChamadoSelecionado();

            if (idChamado.HasValue)
            {
                try
                {
                    // Reutiliza a tela de Análise/Resposta para edição
                    var analisechamado = new AnaliseChamado(idChamado.Value);
                    analisechamado.Show();

                    // Recarrega a lista para refletir possíveis mudanças de Status/Prioridade
                    CarregarChamados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir a tela de Edição/Análise: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnVisualizarCh_Click(object sender, EventArgs e)
        {
            int? idChamado = ObterIdChamadoSelecionado();

            if (idChamado.HasValue)
            {
                try
                {
                    // Abre a tela ChamadoCriado (somente leitura de detalhes)
                    var telaDetalhes = new ChamadoCriado(idChamado.Value);
                    telaDetalhes.ShowDialog();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir a tela de Visualização: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }
    }
}