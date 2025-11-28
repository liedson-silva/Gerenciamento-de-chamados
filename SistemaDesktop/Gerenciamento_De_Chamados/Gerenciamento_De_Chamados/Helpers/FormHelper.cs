using System.Linq;
using System.Windows.Forms;
using Gerenciamento_De_Chamados; // Para acessar os Formulários HomeAdmin, HomeFuncionario, Login, etc.

namespace Gerenciamento_De_Chamados.Helpers
{

    public static class FormHelper
    {

        public static void ExibirUsuarioEmLabel(Label labelDestino)
        {
            labelDestino.Text = !string.IsNullOrEmpty(SessaoUsuario.Nome)
                ? SessaoUsuario.Nome
                : "Usuário não identificado";
        }

        private static void AbrirOuReativarForm<T>(Form formAtual) where T : Form, new()
        {
            // 1. Tenta encontrar uma instância existente do formulário no Application.OpenForms
            T formExistente = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (formExistente != null)
            {
                // 2. Se achou, mostra e traz para frente
                formExistente.Show();
                if (formExistente.WindowState == FormWindowState.Minimized)
                {
                    formExistente.WindowState = FormWindowState.Normal;
                }
                formExistente.BringToFront();
            }
            else
            {
                // 3. Se não achou, cria e mostra uma nova (situação menos comum para "Voltar")
                var novoForm = new T();
                novoForm.Show();
            }


            // 4. Fecha o formulário atual (FAQ)
            if (formAtual.GetType() != typeof(T))
            {
                formAtual.Close();
            }
        }

        public static void BotaoVoltar<TFormAnterior>(Form formAtual) where TFormAnterior : Form, new()
        {
            AbrirOuReativarForm<TFormAnterior>(formAtual);
        }
        public static void BotaoHomeAdmin(Form formAtual)
        {
            var homeAdmin = new HomeAdmin();
            homeAdmin.Show();
            formAtual.Close(); 
        }

        
        public static void BotaoHomeFuncionario(Form formAtual)
        {
            var homeFuncionario = new HomeFuncionario();
            homeFuncionario.Show();
            formAtual.Close();
        }

        
        public static void BotaoHomeTecnico(Form formAtual)
        {
            var homeTecnico = new HomeTecnico();
            homeTecnico.Show();
            formAtual.Close(); // Usa Close() para fechar a tela atual definitivamente
        }

        public static void BotaoHome(Form formAtual)
        {
            string funcao = SessaoUsuario.FuncaoUsuario?.ToLower() ?? "";

            switch (funcao)
            {
                case "admin":
                case "administrador":
                    BotaoHomeAdmin(formAtual);
                    break;
                case "funcionario":
                    BotaoHomeFuncionario(formAtual);
                    break;
                case "tecnico":
                    BotaoHomeTecnico(formAtual);
                    break;
                default:
                    // Se der erro de perfil ou função não definida, joga pro login
                    var loginForm = new Login();
                    loginForm.Show();
                    formAtual.Close(); // Usa Close() para garantir que a pilha do Main seja limpa corretamente
                    break;
            }
        }


        public static void Sair(Form formAtual, Timer timerSessao = null)
        {

            if (timerSessao != null)
            {
                timerSessao.Stop();
            }

            // CORREÇÃO: Usar um MessageBox sem usar o FormHelper (não é recomendável)
            if (MessageBox.Show("Você realmente deseja sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                SessaoUsuario.EncerrarSessao();


                var telaLogin = new Login();
                telaLogin.Show();


                formAtual.Close();
            }
            else
            {

                if (timerSessao != null)
                {
                    timerSessao.Start();
                }
            }
        }


        public static void FAQ(Form formAtual)
        {
            var telaFaq = new Faq();
            telaFaq.Show();
            formAtual.Hide();
        }
    }
}