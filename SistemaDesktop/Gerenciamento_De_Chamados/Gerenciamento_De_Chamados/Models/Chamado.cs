using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
    /// <summary>
    /// Esta é a classe principal que representa um 'Chamado' (ticket) no nosso sistema.
    /// Ela define todos os campos que coletamos, salvamos e processamos.
    /// </summary>
    public class Chamado
    {
        // ====================================================================
        // I. CHAVES E DADOS BASE (IDENTIFICAÇÃO E FLUXO)
        // ====================================================================

        /// <summary>
        /// ID Único do chamado (chave primária). É o nosso número de Protocolo.
        /// </summary>
        public int IdChamado { get; set; }

        /// <summary>
        /// Chave estrangeira que conecta o chamado ao usuário que o abriu.
        /// </summary>
        public int FK_IdUsuario { get; set; }

        /// <summary>
        /// O status atual do chamado (Pendente, Em Andamento, Resolvido).
        /// </summary>
        public string StatusChamado { get; set; }

        /// <summary>
        /// Data e hora em que o chamado foi aberto no sistema.
        /// </summary>
        public DateTime DataChamado { get; set; }


        // ====================================================================
        // II. DADOS COLETADOS DO USUÁRIO
        // ====================================================================

        /// <summary>
        /// Título curto do problema (Ex: "Internet lenta").
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição detalhada do problema reportado pelo usuário.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Categoria ou tipo de problema (Ex: "Software", "Hardware", "Rede").
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Prioridade que o sistema e a IA define para o chamado (Baixa, Média, Alta).
        /// </summary>
        public string PrioridadeChamado { get; set; } 

        /// <summary>
        /// Pergunta: Quantas pessoas estão afetadas pelo problema?
        /// (Usado como dado de entrada para a IA).
        /// </summary>
        public string PessoasAfetadas { get; set; }

        /// <summary>
        /// Pergunta: O problema impede o trabalho do usuário ou da equipe? (Somente eu/meu setor/minha empresa).
        /// (Usado como dado de entrada para a IA).
        /// </summary>
        public string ImpedeTrabalho { get; set; }

        /// <summary>
        /// Pergunta: O problema ocorreu anteriormente? (Sim/Não).
        /// (Usado como dado de entrada para a IA).
        /// </summary>
        public string OcorreuAnteriormente { get; set; }


        // ====================================================================
        // III. DADOS GERADOS PELA INTELIGÊNCIA ARTIFICIAL (IA)
        // ====================================================================

        /// <summary>
        /// A prioridade que a IA sugere para o chamado (Baixa, Média, Alta).
        /// Usamos isso para garantir um atendimento justo e rápido.
        /// </summary>
        public string PrioridadeSugeridaIA { get; set; }

        /// <summary>
        /// O resumo do problema identificado pela IA com base na descrição do usuário.
        /// </summary>
        public string ProblemaSugeridoIA { get; set; }

        /// <summary>
        /// A sugestão de solução que a IA oferece ao técnico, baseada no histórico.
        /// </summary>
        public string SolucaoSugeridaIA { get; set; }
    }
}