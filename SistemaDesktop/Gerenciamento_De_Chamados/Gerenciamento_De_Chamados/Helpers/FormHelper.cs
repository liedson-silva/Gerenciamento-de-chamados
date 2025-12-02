using System.Linq;
using System.Windows.Forms;
using Gerenciamento_De_Chamados; 

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
            
            T formExistente = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (formExistente != null)
            {
               
                formExistente.Show();
                if (formExistente.WindowState == FormWindowState.Minimized)
                {
                    formExistente.WindowState = FormWindowState.Normal;
                }
                formExistente.BringToFront();
            }
            else
            {
                
                var novoForm = new T();
                novoForm.Show();
            }


            if (formAtual.GetType() != typeof(T))
            {
                formAtual.Close();
            }
        }

        private static void NavegarParaHome<T>(Form formAtual) where T : Form, new()
        {

            T homeForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (homeForm == null)
            {

                homeForm = new T();
                homeForm.Show();
            }
            else
            {

                homeForm.Show();
                if (homeForm.WindowState == FormWindowState.Minimized)
                {
                    homeForm.WindowState = FormWindowState.Normal;
                }
                homeForm.BringToFront();
            }


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
            NavegarParaHome<HomeAdmin>(formAtual);
        }

        public static void BotaoHomeFuncionario(Form formAtual)
        {
            NavegarParaHome<HomeFuncionario>(formAtual);
        }

        public static void BotaoHomeTecnico(Form formAtual)
        {
            NavegarParaHome<HomeTecnico>(formAtual);
        }

        // Função de roteamento principal para o botão "Início"
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
                    // Se a sessão for inválida, volta para o Login
                    var loginForm = new Login();
                    loginForm.Show();
                    formAtual.Close(); // Fecha o form atual
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