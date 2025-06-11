// Define o namespace onde essa classe de serviço está localizada
namespace AIChatApp.Domain.DTOs;

/// <summary>
/// Representa a requisição com uma pergunta enviada para a OpenAI.
/// </summary>
public class QuestionRequest
{
    /// <summary>
    /// Pergunta que será enviada ao modelo da OpenAI.
    /// </summary>
    public string Question { get; set; }
}