using Gerenciamento_De_Chamados.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Interface para o Repositório de Chamados.
    /// Define todas as operações CRUD e de consulta necessárias para gerenciar os tickets de suporte,
    /// incluindo a busca de dados para os gráficos e relatórios.
    /// </summary>
    public interface IChamadoRepository
    {
        // ====================================================================
        // I. CRIAÇÃO E BUSCA POR ID
        // ====================================================================

        /// <summary>
        /// Adiciona um novo chamado ao banco de dados.
        /// </summary>
        /// <param name="chamado">O objeto Chamado a ser salvo.</param>
        /// <returns>O ID (protocolo) do chamado inserido.</returns>
        Task<int> AdicionarAsync(Chamado chamado);

        /// <summary>
        /// Busca um chamado específico pelo seu ID (protocolo).
        /// </summary>
        /// <param name="id">O ID do chamado a ser buscado.</param>
        /// <returns>O objeto Chamado preenchido.</returns>
        Task<Chamado> BuscarPorIdAsync(int id);

        /// <summary>
        /// Busca as soluções aplicadas anteriormente para chamados da mesma categoria.
        /// Este é um dado crucial para a inteligência artificial (IA).
        /// </summary>
        /// <param name="categoria">Categoria do chamado.</param>
        /// <returns>Lista de soluções antigas (strings).</returns>
        Task<List<string>> BuscarSolucoesAnterioresAsync(string categoria);

        // ====================================================================
        // II. ATUALIZAÇÃO (UPDATE)
        // ====================================================================

        /// <summary>
        /// Atualiza APENAS os campos de sugestão da IA (Prioridade, Problema, Solução).
        /// </summary>
        Task AtualizarSugestoesIAAsync(int id, string prioridade, string problema, string solucao);

        /// <summary>
        /// Atualiza o Status, Prioridade e Categoria. Usado na tela de Resolução.
        /// Este método é transacional e requer conexão e transação ativas.
        /// </summary>
        Task AtualizarStatusAsync(int idChamado, string novoStatus, string novaPrioridade, string novaCategoria, SqlConnection conn, SqlTransaction trans);

        /// <summary>
        /// Atualiza o Status e as sugestões de IA no contexto da análise.
        /// Este método é transacional e requer conexão e transação ativas.
        /// </summary>
        Task AtualizarAnaliseAsync(Chamado chamado, SqlConnection conn, SqlTransaction trans);

        /// <summary>
        /// Atualiza o status do chamado de forma simples, fora de uma transação.
        /// </summary>
        Task AtualizarStatusSimplesAsync(int idChamado, string novoStatus);

        // ====================================================================
        // III. CONSULTAS DE LISTAGEM E FILTRO
        // ====================================================================

        /// <summary>
        /// Busca todos os chamados no banco de dados, permitindo filtro.
        /// Usado na visão de Administradores/Técnicos (Grid Principal).
        /// </summary>
        DataTable BuscarTodosFiltrados(string filtro);

        /// <summary>
        /// Busca e filtra os chamados criados por um usuário específico.
        /// Usado na tela do Funcionário ('Meus Chamados').
        /// </summary>
        Task<DataTable> BuscarMeusChamadosFiltrados(int idUsuario, string status, string filtroPesquisa);

        /// <summary>
        /// Busca chamados por uma Prioridade específica e filtra por pesquisa textual.
        /// Usado na tela de Responder Chamado do Técnico.
        /// </summary>
        Task<DataTable> BuscarPorPrioridadeEFiltrarAsync(string prioridade, string filtroPesquisa);


        // ====================================================================
        // IV. CONSULTAS PARA GRÁFICOS (RELATÓRIOS)
        // ====================================================================

        /// <summary>
        /// Conta os chamados agrupados por Status. Usado para o gráfico do Funcionário.
        /// </summary>
        Task<List<ChartDataPoint>> ContarPorStatusAsync(int idUsuario);

        /// <summary>
        /// Conta os chamados agrupados por Categoria. Usado para o gráfico do Funcionário.
        /// </summary>
        Task<List<ChartDataPoint>> ContarPorCategoriaAsync(int idUsuario);

        /// <summary>
        /// Conta os chamados agrupados por Status (Visão Geral - Admin/Técnico).
        /// </summary>
        Task<List<ChartDataPoint>> ContarPorStatusGeralAsync();

        /// <summary>
        /// Conta os chamados agrupados por Categoria (Visão Geral - Admin/Técnico).
        /// </summary>
        Task<List<ChartDataPoint>> ContarPorCategoriaGeralAsync();
    }
}