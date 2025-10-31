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

        public static void BotaoHomeAdmin(Form formAtual)
        {
            var homeAdmin = new HomeAdmin();
            homeAdmin.Show();
            formAtual.Hide();
        }

        public static void BotaoHomeFuncionario(Form formAtual)
        {
            var homeFuncionario = new HomeFuncionario();
            homeFuncionario.Show();
            formAtual.Hide();
        }

        public static void BotaoHomeTecnico(Form formAtual)
        {
            var homeTecnico = new HomeTecnico();
            homeTecnico.Show();
            formAtual.Hide();
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
                    var loginForm = new Login();
                    loginForm.Show();
                    formAtual.Hide();
                    break;
            }
        }


        public static void Sair(Form formAtual)
        {
            if (MessageBox.Show("Você realmente deseja sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var telaLogin = new Login();
                telaLogin.Show();
                formAtual.Hide();
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