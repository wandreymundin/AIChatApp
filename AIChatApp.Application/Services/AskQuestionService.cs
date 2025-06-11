using AIChatApp.Infrastructure.Clients; // Importa o namespace onde está o cliente que se comunica com a API da OpenAI

// Define o namespace onde essa classe de serviço está localizada
namespace AIChatApp.Application.Services;

/// <summary>
/// Essa classe representa um "serviço de aplicação", responsável por coordenar o envio da pergunta do usuário
/// para a IA da OpenAI e devolver a resposta.
/// </summary>
public class AskQuestionService
{
    // Declara uma variável para armazenar a instância do cliente que fala com a API da OpenAI
    private readonly OpenAIClient _openAIClient;

    /// <summary>
    /// Construtor da classe, que recebe o OpenAIClient por injeção de dependência 
    /// Isso significa que quem for usar essa classe precisa fornecer o OpenAIClient já pronto
    /// </summary>
    /// <param name="openAIClient">Objeto da OpenAIClient</param>
    public AskQuestionService(OpenAIClient openAIClient) => _openAIClient = openAIClient;

    /// <summary>
    /// Método que processa a pergunta enviada e retorna a resposta da IA.
    /// </summary>
    /// <param name="question">Texto da pergunta digitada pelo usuário</param>
    /// <returns>Texto da resposta vinda da IA</returns>
    public async Task<string> ProcessQuestionAsync(string question)
    {
        // Verifica se a pergunta está vazia ou só contém espaços em branco
        // Se estiver, lança um erro informando que não é permitido enviar pergunta vazia
        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException("A pergunta não pode estar vazia.");
        }

        // Chama o cliente da OpenAI passando a pergunta, e aguarda a resposta da IA
        // O "await" significa que o código vai esperar a resposta chegar antes de continuar
        return await _openAIClient.AskQuestion(question);
    }
}