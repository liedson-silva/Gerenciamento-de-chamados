using System.Linq;
using System.Windows.Forms;
using Gerenciamento_De_Chamados;

namespace Gerenciamento_De_Chamados.Helpers
{

    public static class FormHelper
    {

        // ====================================================================
        // I. MÉTODOS DE UTILIDADE PÚBLICA (COISAS QUE FAZEMOS EM TODA TELA)
        // ====================================================================

        /// <summary>
        /// Minha função de 'etiqueta' que mostra o nome do usuário logado na tela.
        /// Se não tiver ninguém logado, mostra "Usuário não identificado".
        /// </summary>
        public static void ExibirUsuarioEmLabel(Label labelDestino)
        {
            labelDestino.Text = !string.IsNullOrEmpty(SessaoUsuario.Nome)
                ? SessaoUsuario.Nome
                : "Usuário não identificado";
        }

        /// <summary>
        /// Minha função para abrir a tela de FAQ (Perguntas Frequentes).
        /// Abro a tela do FAQ e escondo a tela atual para não ficar um monte de tela aberta.
        /// </summary>
        public static void FAQ(Form formAtual)
        {
            var telaFaq = new Faq();
            telaFaq.Show();
            formAtual.Hide();
        }

        // ====================================================================
        // II. LÓGICA DE NAVEGAÇÃO PRIVADA (OS MECANISMOS POR TRÁS DOS BOTÕES)
        // ====================================================================

        /// <summary>
        /// Método secreto que reutiliza uma tela se ela já estiver aberta.
        /// Se a tela que quero voltar existe, eu a trago para a frente. Se não, eu crio uma nova.
        /// É o motor do 'Botão Voltar'.
        /// </summary>
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

        /// <summary>
        /// Esta é a lógica específica para ir para a tela 'Home'.
        /// Ela é parecida com a de voltar, mas garante que a Home seja a tela principal.
        /// </summary>
        private static void NavegarParaHome<T>(Form formAtual) where T : Form, new()
        {
            // Tenta encontrar uma Home já aberta
            T homeForm = Application.OpenForms.OfType<T>().FirstOrDefault();

            if (homeForm == null)
            {
                // Se não achou, cria uma nova Home
                homeForm = new T();
                homeForm.Show();
            }
            else
            {
                // Se achou, traz a Home antiga de volta
                homeForm.Show();
                if (homeForm.WindowState == FormWindowState.Minimized)
                {
                    homeForm.WindowState = FormWindowState.Normal;
                }
                homeForm.BringToFront();
            }

            // Fecha a tela de onde estou vindo, para não acumular janelas abertas.
            if (formAtual.GetType() != typeof(T))
            {
                formAtual.Close();
            }

        }

        // ====================================================================
        // III. MÉTODOS DE AÇÃO DE NAVEGAÇÃO (OS CLIQUES DOS BOTÕES)
        // ====================================================================

        /// <summary>
        /// Ação de ir para a Home de Administrador (usa a lógica de navegação).
        /// </summary>
        public static void BotaoHomeAdmin(Form formAtual)
        {
            NavegarParaHome<HomeAdmin>(formAtual);
        }

        /// <summary>
        /// Ação de ir para a Home de Funcionário (usa a lógica de navegação).
        /// </summary>
        public static void BotaoHomeFuncionario(Form formAtual)
        {
            NavegarParaHome<HomeFuncionario>(formAtual);
        }

        /// <summary>
        /// Ação de ir para a Home de Técnico (usa a lógica de navegação).
        /// </summary>
        public static void BotaoHomeTecnico(Form formAtual)
        {
            NavegarParaHome<HomeTecnico>(formAtual);
        }

        /// <summary>
        /// Este é o ROTEADOR principal! Ele decide para qual Home o usuário vai.
        /// Ele pega a 'Função' do usuário logado e direciona para a tela certa (Admin, Funcionario ou Tecnico).
        /// </summary>
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

        /// <summary>
        /// Ação do Botão Voltar. Ele reativa a tela anterior (usando a lógica de AbrirOuReativar).
        /// </summary>
        public static void BotaoVoltar<TFormAnterior>(Form formAtual) where TFormAnterior : Form, new()
        {
            AbrirOuReativarForm<TFormAnterior>(formAtual);
        }

        /// <summary>
        /// Ação de Sair do sistema. Ele encerra a sessão, mostra uma mensagem de confirmação
        /// e abre a tela de Login novamente.
        /// </summary>
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
    }
}