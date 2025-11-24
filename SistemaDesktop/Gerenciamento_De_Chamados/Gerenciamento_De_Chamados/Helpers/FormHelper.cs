using System.Linq;
using System.Windows.Forms;

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
            // 1. Procura na lista de janelas abertas do Windows se já tem uma do tipo T
            T formExistente = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (formExistente != null)
            {
                // 2. Se achou, traz ela para frente e restaura se estiver minimizada
                formExistente.Show();
                if (formExistente.WindowState == FormWindowState.Minimized)
                {
                    formExistente.WindowState = FormWindowState.Normal;
                }
                formExistente.BringToFront();
            }
            else
            {
                // 3. Se não achou (foi fechada antes), cria uma nova
                var novoForm = new T();
                novoForm.Show();
            }

            // 4. FECHA a janela atual (Feature) em vez de esconder.
            // A verificação (GetType != typeof(T)) impede de fechar a Home se clicar no botão nela mesma.
            if (formAtual.GetType() != typeof(T))
            {
                formAtual.Close();
            }
        }

        public static void BotaoHomeAdmin(Form formAtual)
        {
            AbrirOuReativarForm<HomeAdmin>(formAtual);
        }

        public static void BotaoHomeFuncionario(Form formAtual)
        {
            AbrirOuReativarForm<HomeFuncionario>(formAtual);
        }

        public static void BotaoHomeTecnico(Form formAtual)
        {
            AbrirOuReativarForm<HomeTecnico>(formAtual);
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
                    // Se der erro de perfil, joga pro login e fecha a atual
                    var loginForm = new Login();
                    loginForm.Show();
                    formAtual.Close();
                    break;
            }
        }


        public static void Sair(Form formAtual, Timer timerSessao = null)
        {

            if (timerSessao != null)
            {
                timerSessao.Stop();
            }

          
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