using Google.Cloud.AIPlatform.V1;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Gerenciamento_De_Chamados
{
    public class AIService
    {
        private readonly PredictionServiceClient _predictionServiceClient;
        private readonly string _projectId;
        private readonly string _locationId;
        private readonly string _publisher;
        private readonly string _model;
        private readonly string _apiKey;

        public AIService()
        {
            _apiKey = ConfigurationManager.AppSettings["GEMINI_API_KEY"];

            
            _projectId = ""; 

            _locationId = "us-central1";
            _publisher = "google";
            _model = "gemini-1.0-pro"; // Modelo Padrão

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("API Key do Gemini não encontrada no App.config.");
            }
            if (string.IsNullOrEmpty(_projectId))
            {
                Console.WriteLine("AVISO: Project ID está vazio. A chamada à API Vertex AI pode falhar.");
                throw new InvalidOperationException("Project ID não configurado no AIService.cs.");
            }

            
            PredictionServiceClientBuilder builder = new PredictionServiceClientBuilder
            {
                Endpoint = $"{_locationId}-aiplatform.googleapis.com",
                
                ApiKey = _apiKey
            };
            _predictionServiceClient = builder.Build();
        }

        public async Task<(string problema, string solucao)> AnalisarChamado(string titulo, string descricao, string categoria)
        {
            if (string.IsNullOrEmpty(_projectId))
            {
                Console.WriteLine("ERRO: Project ID está vazio. Não é possível chamar EndpointName.FromProjectLocationPublisherModel.");
                return ("Erro de Configuração", "Project ID não definido"); 
            }

            EndpointName endpointName = EndpointName.FromProjectLocationPublisherModel(_projectId, _locationId, _publisher, _model);

            string prompt = $@"
                Analise o seguinte chamado de suporte técnico:
                Título: {titulo}
                Categoria: {categoria}
                Descrição: {descricao}

                Com base nessas informações, forneça:
                1. Identificação do Problema: (Descreva o problema principal em poucas palavras)
                2. Proposta de Solução: (Sugira uma solução inicial ou próximos passos)

                Responda APENAS com o texto solicitado para cada item, um em cada linha, começando EXATAMENTE com '1. Identificação do Problema:' e '2. Proposta de Solução:'.";
            var instance = new Google.Protobuf.WellKnownTypes.Value
            {
                StructValue = new Google.Protobuf.WellKnownTypes.Struct
                {
                    Fields = { { "prompt", Google.Protobuf.WellKnownTypes.Value.ForString(prompt) } }
                }
            };

            var parameters = new Google.Protobuf.WellKnownTypes.Value
            {
                StructValue = new Google.Protobuf.WellKnownTypes.Struct
                {
                    Fields =
                    {
                         { "temperature", Google.Protobuf.WellKnownTypes.Value.ForNumber(0.2) },
                         { "maxOutputTokens", Google.Protobuf.WellKnownTypes.Value.ForNumber(256) },
                         { "topP", Google.Protobuf.WellKnownTypes.Value.ForNumber(0.95) },
                         { "topK", Google.Protobuf.WellKnownTypes.Value.ForNumber(40) }
                    }
                }
            };

            PredictRequest request = new PredictRequest
            {
                EndpointAsEndpointName = endpointName,
                Instances = { instance },
                Parameters = parameters
            };

            try
            {
                PredictResponse response = await _predictionServiceClient.PredictAsync(request);

               
                string respostaCompleta = response.Predictions[0].StructValue.Fields["content"].StringValue;

                string problemaSugerido = ExtrairValor(respostaCompleta, @"1\. Identificação do Problema:\s*(.*)");
                
                string solucaoSugerida = ExtrairValor(respostaCompleta, @"2\. Proposta de Solução:\s*(.*)");

                return (problemaSugerido, solucaoSugerida);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar a API Gemini via Vertex AI: {ex.Message}");
                return ($"Erro IA: {ex.Message}", "Erro na análise");
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