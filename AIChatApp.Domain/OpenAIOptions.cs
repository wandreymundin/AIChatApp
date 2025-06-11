// Define o namespace onde essa classe de serviço está localizada
namespace AIChatApp.Domain.Options; 

/// <summary>
/// Representa as configurações da OpenAI no appsettings.json.
/// </summary>
public class OpenAIOptions
{
    /// <summary>
    /// Chave de API da OpenAI.
    /// </summary>
    public string ApiKey { get; set; }
}