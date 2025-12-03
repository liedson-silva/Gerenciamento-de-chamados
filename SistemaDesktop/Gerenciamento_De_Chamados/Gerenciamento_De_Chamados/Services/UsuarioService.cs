using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Validacao;
using System;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    /// <summary>
    /// 
    /// Responsável por todas as regras e validações do cadastro de Usuário
    /// antes que os dados sejam enviados ao Repositório para persistência.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        // Dependência do repositório (abstração)
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// 
        /// O serviço recebe uma instância do IUsuarioRepository, garantindo 
        /// que ele use a camada de dados correta e que possa ser facilmente testado.
        /// </summary>
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Adiciona um novo usuário ao sistema, aplicando 
        /// todas as regras de negócio e validações antes de salvar no banco.
        /// </summary>
        /// <param name="novoUsuario">O objeto Usuario a ser salvo.</param>
        public async Task AdicionarUsuarioAsync(Usuario novoUsuario)
        {
           
            // Limpa caracteres especiais do CPF e RG antes de validar
            string cpfLimpo = (novoUsuario.CPF ?? "")
            .Replace(".", "").Replace("-", "").Trim();

            string rgLimpo = (novoUsuario.RG ?? "")
            .Replace(".", "").Replace("-", "").Trim();

            // Atualiza o objeto com os valores limpos (importante para o Repositório salvar)
            novoUsuario.CPF = cpfLimpo;
            novoUsuario.RG = rgLimpo;

            // 1. Validar os dados de entrada usando a nossa classe ValidadorUsuario
            // Se qualquer validação falhar, uma exceção é lançada,
            // e o fluxo é interrompido antes de acessar o banco de dados.
            if (!ValidadorUsuario.IsNomeValido(novoUsuario.Nome))
                throw new Exception("O campo 'Nome' é obrigatório.");

            if (!ValidadorUsuario.IsEmailValido(novoUsuario.Email))
                throw new Exception("O formato do 'Email' é inválido.");

            if (!ValidadorUsuario.IsCPFValido(novoUsuario.CPF))
                throw new Exception("O formato do 'CPF' é inválido.");

            if (!ValidadorUsuario.IsNomeValido(novoUsuario.Login))
                throw new Exception("O campo 'Login' é obrigatório.");

            if (!ValidadorUsuario.IsSenhaForte(novoUsuario.Senha))
                throw new Exception("A 'Senha' é inválida. Requisitos: 8+ chars, 1 maiúscula, 1 número.");

            // 2. Chamar o Repositório para salvar
            // Se tudo estiver OK, delegamos a responsabilidade de criptografar a senha (no Repositório) e salvar no BD.
            await _usuarioRepository.AdicionarAsync(novoUsuario);
        }
    }
}