using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Validacao;
using System;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    // Adicione "public" aqui
    public class UsuarioService : IUsuarioService
    {
        // Dependência do repositório (abstração)
        private readonly IUsuarioRepository _usuarioRepository;

        // Injeção de Dependência via construtor
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Método principal da lógica de negócio
        public async Task AdicionarUsuarioAsync(Usuario novoUsuario)
        {
            string cpfLimpo = (novoUsuario.CPF ?? "")
            .Replace(".", "").Replace("-", "").Trim();

            string rgLimpo = (novoUsuario.RG ?? "")
            .Replace(".", "").Replace("-", "").Trim();

            // 1. Validar os dados de entrada usando nosso validador
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
            await _usuarioRepository.AdicionarAsync(novoUsuario);
        }
    }
}