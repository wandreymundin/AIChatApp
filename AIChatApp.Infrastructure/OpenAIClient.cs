using AIChatApp.Domain.Options; // Importa as configurações da OpenAI que estão definidas no projeto
using Microsoft.Extensions.Options; // Permite acessar opções configuradas via appsettings.json
using Newtonsoft.Json; // Biblioteca usada para trabalhar com JSON (enviar e receber)
using System.Net; // Usado para verificar códigos de resposta HTTP (ex: 200, 404, 429)
using System.Net.Http.Headers; // Usado para configurar cabeçalhos HTTP, como autorização
using System.Text; // Usado para codificar strings como UTF-8 (padrão da internet)

// Define o namespace onde essa classe de serviço está localizada
namespace AIChatApp.Infrastructure.Clients;

/// <summary>
/// Classe responsável por falar com a OpenAI 
/// </summary>
public class OpenAIClient
{
    // Cliente HTTP que faz chamadas para a internet
    private readonly HttpClient _httpClient;

    // Configurações com a chave da OpenAI
    private readonly OpenAIOptions _options;

    /// <summary>
    /// Construtor que recebe o HttpClient e as opções da OpenAI.
    /// </summary>
    /// <param name="httpClient">httpClient</param>
    /// <param name="options">Opções da OpenAI</param>
    public OpenAIClient(HttpClient httpClient, IOptions<OpenAIOptions> options)
    {
        // Guarda a instância do HttpClient recebida
        _httpClient = httpClient;

        // Acessa o valor da configuração (pega a chave da OpenAI)
        _options = options.Value;

        // Adiciona no cabeçalho da requisição a chave de acesso à API da OpenAI
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);
    }

    /// <summary>
    /// Envia uma pergunta para a OpenAI e retorna a resposta.
    /// </summary>
    /// <param name="question">Texto da pergunta feita pelo usuário.</param>
    /// <returns>Texto com a resposta gerada pela IA.</returns>
    public async Task<string> AskQuestion(string question)
    {
        // Cria o "pacote de dados" (payload) com o modelo e a mensagem que será enviada
        // Objeto anônimo usado para criar um objeto sem precisar de uma classe específica
        var payload = new
        {
            model = "gpt-4o-mini", // Define o modelo da IA que será usado
            messages = new[]
            {
                new { role = "user", content = question } // A mensagem que o usuário enviou
            }
        };

        // Transforma o payload em um texto JSON (formato aceito pela API)
        var json = JsonConvert.SerializeObject(payload);

        // Cria o corpo da requisição com o JSON, codificado como UTF-8
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Envia uma requisição POST para a API da OpenAI com os dados
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

        // Se a resposta não for bem-sucedida (ex: erro 401, 500), lança uma exceção
        response.EnsureSuccessStatusCode();

        // Se a OpenAI responder que foram feitas requisições demais (erro 429), retorna uma mensagem de aviso
        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            return "Você excedeu o número de requisições permitidas. Por favor, tente novamente em instantes.";
        }

        // Lê o conteúdo da resposta da OpenAI em formato de texto
        var responseString = await response.Content.ReadAsStringAsync();

        // Converte o texto JSON da resposta para um objeto dinâmico
        dynamic result = JsonConvert.DeserializeObject(responseString);

        // Acessa a resposta gerada pela IA e retorna como string
        return result.choices[0].message.content.ToString();
    }
}