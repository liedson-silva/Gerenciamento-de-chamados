using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
    /// <summary>
    /// Esta classe é como um 'contrato' que representa a tabela de Arquivos no banco de dados.
    /// É onde definimos o que cada arquivo anexado a um chamado deve ter.
    /// </summary>
    public class Arquivo
    {
        // ====================================================================
        // I. CHAVES (IDENTIFICAÇÃO E RELACIONAMENTO)
        // ====================================================================

        /// <summary>
        /// ID Único do arquivo (chave primária).
        /// </summary>
        public int IdArquivo { get; set; }

        /// <summary>
        /// Chave estrangeira que conecta este arquivo a um chamado específico.
        /// Garante que o arquivo pertence ao chamado correto.
        /// </summary>
        public int FK_IdChamado { get; set; }


        // ====================================================================
        // II. DADOS DO ARQUIVO (O QUE O SISTEMA PRECISA SABER)
        // ====================================================================

        /// <summary>
        /// O nome original do arquivo que o usuário anexou.
        /// </summary>
        public string NomeArquivo { get; set; }

        /// <summary>
        /// O tipo de arquivo (MIME Type), para saber se é 'image/png', 'application/pdf', etc.
        /// </summary>
        public string TipoArquivo { get; set; }

        /// <summary>
        /// O conteúdo real do arquivo, armazenado como um array de bytes no banco de dados.
        /// É o arquivo em si, convertido em "números" para ser salvo.
        /// </summary>
        public byte[] ArquivoBytes { get; set; }
    }
}