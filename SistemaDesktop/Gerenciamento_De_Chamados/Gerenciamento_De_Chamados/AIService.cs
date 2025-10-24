using System;
using System.Configuration; 
using System.Net.Http;
using System.Text;
using System.Text.Json; 
using System.Text.RegularExpressions; 
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Gerenciamento_De_Chamados
{
    public class AIService
    {
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-preview-09-2025:generateContent";  
        private static readonly HttpClient _httpClient = new HttpClient(); // Criar HttpClient estático é mais eficiente

        public AIService()
        {
            _apiKey = ConfigurationManager.AppSettings["GEMINI_API_KEY"];
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("API Key do Gemini não encontrada no App.config.");
            }
            // Não precisamos mais do Project ID com esta API
        }

        // Assinatura ajustada para retornar apenas problema e solucao
        public async Task<(string problema, string prioridade, string solucao)> AnalisarChamado(string titulo, 
            string pessoaAfetadas, string ocorreuAnteriormente, string impedeTrabalho, string descricao, string categoria)
        {
            // Monta o prompt (igual ao anterior)
            string prompt = $@"
                Analise o seguinte chamado de suporte técnico:
                Título: {titulo}
                Categoria: {categoria}
                Descrição: {descricao}
                Pessoas Afetadas: {pessoaAfetadas}
                Ocorreu Anteriormente: {ocorreuAnteriormente}
                Impede o Trabalho: {impedeTrabalho}
                
                Regras para definir a prioridade (Aplicar estritamente):
                * Alta: (O problema afeta 'A empresa inteira' OU 'Meu setor') E (Impede o Trabalho = 'Sim'). OU (Afeta 'Somente eu' E Impede o Trabalho = 'Sim' E Ocorrência Anterior = 'Sim').
                * Média: (O problema afeta 'Meu setor' AND (Impede o Trabalho = 'Não' OU Impede o Trabalho = 'Parcialmente')). OU (Afeta 'Somente eu' AND Impede o Trabalho = 'Sim' AND Ocorrência Anterior = 'Não').
                * Baixa: (O problema afeta 'Somente eu' AND (Impede o Trabalho = 'Não' OU Impede o Trabalho = 'Parcialmente')). OU (Descrição indica pedido de informação/melhoria e não um erro funcional).

                Com base nessas informações, forneça:
                1. Identificação do Problema: (Descreva o problema principal em poucas palavras)
                2. Proposta de Solução: (Sugira uma solução inicial ou próximos passos)
                3. Prioridade Definida: [Baixa, Média ou Alta]
                "";             

                Responda APENAS com o texto solicitado para cada item, um em cada linha, começando EXATAMENTE com '1. Identificação do Problema:',
                '2. Proposta de Solução:' e '3. Prioridade Definida:'.";

            try
            {
                
                var requestBody = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = prompt } } }
                    },
                 
                    generationConfig = new
                    {
                        temperature = 0.2,
                        maxOutputTokens = 256,
                        topP = 0.95,
                        topK = 40
                    }
                };

               
                string jsonBody = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Monta a URL final com a chave
                string urlWithKey = $"{_apiUrl}?key={_apiKey}";

                // Faz a requisição POST
                // Usando SendAsync para poder definir o método explicitamente (embora PostAsync funcione)
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlWithKey))
                {
                    requestMessage.Content = content;
                    HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

                    // Lê o corpo da resposta
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Verifica se a requisição foi bem-sucedida (status code 2xx)
                    if (!response.IsSuccessStatusCode)
                    {
                        // Tenta extrair uma mensagem de erro do corpo da resposta
                        string errorDetails = responseBody;
                        try
                        {
                            using (JsonDocument errorDoc = JsonDocument.Parse(responseBody))
                            {
                                if (errorDoc.RootElement.TryGetProperty("error", out JsonElement errorElement) &&
                                    errorElement.TryGetProperty("message", out JsonElement messageElement))
                                {
                                    errorDetails = messageElement.GetString();
                                }
                            }
                        }
                        catch { /* Ignora erro ao parsear o erro */ }
                        Console.WriteLine($"Erro HTTP: {response.StatusCode} - {errorDetails}");
                        
                        return ("Erro na API", "Não identificado", $"Erro {response.StatusCode}: {errorDetails}"); 
                    }

                    // Analisa a resposta JSON para extrair o texto gerado
                    string textoGerado = "Não foi possível extrair a resposta.";
                    try
                    {
                        using (JsonDocument doc = JsonDocument.Parse(responseBody))
                        {
                            JsonElement root = doc.RootElement;
                            // Navega pela estrutura JSON da resposta da Generative Language API
                            if (root.TryGetProperty("candidates", out var candidatesElement) && candidatesElement.GetArrayLength() > 0 &&
                                candidatesElement[0].TryGetProperty("content", out var contentElement) &&
                                contentElement.TryGetProperty("parts", out var partsElement) && partsElement.GetArrayLength() > 0 &&
                                partsElement[0].TryGetProperty("text", out var textElement))
                            {
                                textoGerado = textElement.GetString();
                            }
                        }
                    }
                    catch (Exception jsonEx)
                    {
                        Console.WriteLine($"Erro ao analisar JSON da resposta: {jsonEx.Message}");
                        return ("Erro no JSON", "Não identificado", $"Resposta recebida, mas não pôde ser lida: {responseBody}"); // Ajustado para 2 valores
                    }

                    // Usa a função ExtrairValor para pegar os dados específicos do texto gerado
                    string problemaSugerido = ExtrairValor(textoGerado, @"1\. Identificação do Problema:\s*(.*)");
                    string solucaoSugerida = ExtrairValor(textoGerado, @"2\. Proposta de Solução:\s*(.*)");
                    string prioridadeSugerida = ExtrairValor(textoGerado, @"3\. Prioridade Definida:\s*(.*)");

                    return (problemaSugerido, prioridadeSugerida, solucaoSugerida);
                } // Fim do using requestMessage
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de Rede ao chamar a API Gemini: {httpEx.Message}");
                return ("Erro de Rede", "Não identificado", $"Não foi possível conectar à API: {httpEx.Message}"); // Troque por:
            }
            catch (Exception ex) // Captura outros erros inesperados
            {
                Console.WriteLine($"Erro inesperado na AIService: {ex.Message}");
                return ("Erro Inesperado", "Não identificado", ex.Message); // Ajustado para 2 valores
            }
        }

 
        private string ExtrairValor(string texto, string padraoRegex)
        {
            Match match = Regex.Match(texto, padraoRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value.Trim();
            }
            return "Não identificado";
        }
    }
}