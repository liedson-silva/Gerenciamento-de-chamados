using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Helpers;


namespace Gerenciamento_De_Chamados
{
    public partial class ChamadoCriado : Form
    {
        private readonly int _chamadoId;


        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoCriado(int idDoChamado)
        {
            InitializeComponent();


            _chamadoRepository = new ChamadoRepository();

            this._chamadoId = idDoChamado;
            // Adiciona o manipulador de evento Load
            this.Load += ChamadoCriado_Load;
        }


        private async void ChamadoCriado_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = $"Bem vindo {SessaoUsuario.Nome}";
            else
                lbl_NomeUser.Text = "Usuário não identificado";

            // *** CORREÇÃO: CHAMA O MÉTODO DE CARREGAMENTO AQUI ***
            await CarregarDetalhesChamadoAsync();
        }


        private async Task CarregarDetalhesChamadoAsync()
        {
            try
            {
                // Busca o chamado no repositório
                Chamado chamado = await _chamadoRepository.BuscarPorIdAsync(this._chamadoId);

                if (chamado != null)
                {
                    // 2. Lógica de formatação (a mesma que você já tinha)
                    DateTime dataCriacao = chamado.DataChamado;
                    string titulo = chamado.Titulo;
                    string descricao = chamado.Descricao;
                    string categoria = chamado.Categoria;
                    string pessoasAfetadas = chamado.PessoasAfetadas;
                    string impedeTrabalho = chamado.ImpedeTrabalho;
                    string ocorreuAntes = chamado.OcorreuAnteriormente;

                    // Adicionar informações do usuário que abriu o chamado (será necessário buscar o usuário)
                    // Por enquanto, usaremos apenas SessaoUsuario.Nome

                    // Nota: Para exibir o nome do criador do chamado de forma correta (se não for o usuário logado),
                    // seria necessário fazer uma busca no IUsuarioRepository pelo ID: chamado.FK_IdUsuario

                    StringBuilder resumo = new StringBuilder();


                    string quandoFoiCriado;

                    // 1. Verifica se a data é inválida (menor que o ano 2000, por exemplo)
                    if (dataCriacao < new DateTime(2000, 1, 1))
                    {
                        quandoFoiCriado = "data indisponível";
                    }
                    else
                    {
                        // 2. Simplifica a hora atual
                        DateTime horaAtual = ObterHoraBrasilia(); // Usando o método que você definiu para garantir a hora correta
                        TimeSpan tempoPassado = horaAtual - dataCriacao;

                        if (tempoPassado.TotalMinutes < 2)
                        {
                            quandoFoiCriado = "poucos segundos atrás";
                        }
                        else if (tempoPassado.TotalHours < 1)
                        {
                            quandoFoiCriado = $"{Math.Round(tempoPassado.TotalMinutes)} minutos atrás";
                        }
                        else if (tempoPassado.TotalDays < 1)
                        {
                            quandoFoiCriado = $"{Math.Round(tempoPassado.TotalHours)} horas atrás";
                        }
                        else
                        {
                            quandoFoiCriado = $"{tempoPassado.Days} dias atrás";
                        }
                    }
                    // O nome do usuário abaixo é um placeholder, pois o objeto Chamado não contém o Nome do Criador.
                    // O ideal seria buscar o nome do usuário pelo chamado.FK_IdUsuario
                    resumo.AppendLine($"Criado em: {quandoFoiCriado}    por    {SessaoUsuario.Nome.ToUpper()}");
                    resumo.AppendLine();
                    resumo.AppendLine($"{categoria} > {titulo}");
                    resumo.AppendLine("==================================================");
                    resumo.AppendLine();
                    resumo.AppendLine("DADOS DO FORMULÁRIO");
                    resumo.AppendLine("Informações do chamado");
                    resumo.AppendLine("--------------------------------------------------");
                    resumo.AppendLine($"1. Problema está impedindo o trabalho?  {impedeTrabalho}");
                    resumo.AppendLine($"2. Quais as pessoas afetadas?  {pessoasAfetadas}");
                    resumo.AppendLine($"3. Ocorreu anteriormente?  {ocorreuAntes}");
                    resumo.AppendLine();
                    resumo.AppendLine($"4. Descrição da sua solicitação:");
                    resumo.AppendLine(descricao);
                    resumo.AppendLine();

                    txtResumoChamado.Text = resumo.ToString();
                }
                else
                {
                    txtResumoChamado.Text = $"Chamado ID {_chamadoId} não encontrado.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar detalhes do chamado: " + ex.Message);
            }
        }


        private DateTime ObterHoraBrasilia()
        {
            try
            {
                // Usando a TimeZone correta para o Brasil (a mesma do seu banco de dados, se for Azure/SQL Server)
                // Se o seu SGBD for MSSQL Server, é provável que seja 'E. South America Standard Time'
                TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);
            }
            catch
            {
                // Retorna a hora local se a timezone não for encontrada (o que pode acontecer em alguns ambientes)
                return DateTime.Now;
            }
        }

        #region Código de Estética e Navegação (Sem Alterações)

        private void gbxChamadoCriado_Paint(object sender, PaintEventArgs e)
        {
            Color corFundo = ColorTranslator.FromHtml("#E3FFE5");
            using (SolidBrush brush = new SolidBrush(corFundo))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            GroupBox box = sender as GroupBox;
            if (box != null)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    box.Text,
                    box.Font,
                    new Point(10, 1),
                    box.ForeColor
                );
            }
        }

        private void gbxTitulo_Paint(object sender, PaintEventArgs e)
        {
            Color corFundo = ColorTranslator.FromHtml("#E3FFE5");
            using (SolidBrush brush = new SolidBrush(corFundo))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            GroupBox box = sender as GroupBox;
            if (box != null)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    box.Text,
                    box.Font,
                    new Point(10, 1),
                    box.ForeColor
                );
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

        private void lbl_Inicio_Click(object sender, EventArgs e)
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

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
        }

        private void lbl_MeusCha_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }
        #endregion
    }
}