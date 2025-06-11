// Importa��o de bibliotecas necess�rias
using AIChatApp.Application.Services; // onde est�o os servi�os da aplica��o (a l�gica do neg�cio)
using AIChatApp.Domain.DTOs; // estrutura dos dados (como ser� enviado e recebido)
using AIChatApp.Domain.Options; // configura��es do sistema, como a chave da OpenAI
using AIChatApp.Infrastructure.Clients; // c�digo que se comunica com a OpenAI
using Microsoft.AspNetCore.Mvc; // funcionalidades para trabalhar com web (requisi��es, JSON etc.)

// Aqui come�a tudo.
// Voc� est� criando a aplica��o e preparando o sistema para saber como deve funcionar.
// � como montar o esqueleto da casa: onde vai porta, janela, luz, etc.
var builder = WebApplication.CreateBuilder(args);

// Sempre que algu�m pedir para falar com a OpenAI, me d� um cliente HTTP pronto.
// O HttpClient � quem faz as chamadas � internet (nesse caso, para o ChatGPT).
builder.Services.AddHttpClient<OpenAIClient>();

// Pegue as configura��es da OpenAI (como a API Key) do arquivo de configura��es (appsettings.json) e deixe dispon�vel no sistema.
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));

// Adicione o servi�o que vai processar as perguntas.
// O `.AddScoped` quer dizer que o servi�o ser� criado a cada requisi��o HTTP.
builder.Services.AddScoped<AskQuestionService>();

// Essas duas linhas habilitam o Swagger, que � aquela tela azul bonitona onde voc� consegue testar a API pelo navegador.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// O servidor da aplica��o deve escutar na porta 80.
// Isso significa que, quando algu�m digitar http://localhost, a aplica��o vai estar l� esperando.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP
});

// Aqui voc� transforma tudo que configurou antes em uma aplica��o real.
// � como terminar de montar a casa e ligar a energia el�trica.
var app = builder.Build();

// "Se estiver rodando em modo de desenvolvimento (n�o em produ��o), mostre o Swagger no navegador."
// Em produ��o, isso normalmente � desabilitado por seguran�a.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Se algu�m acessar http://localhost:porta/, responda com o texto 'API do Chat est� no ar'
// Serve como teste para saber se a API est� funcionando.
app.MapGet("/", () => Results.Ok("API do Chat est� no ar ?"));

// Essa � a parte mais importante da API.
//"Se algu�m enviar uma pergunta para o endere�o /ask, pegue essa pergunta, envie para o servi�o da OpenAI, espere a resposta e devolva essa resposta para quem perguntou.
app.MapPost("/ask", async (
    [FromBody] QuestionRequest req, //req: � o corpo do JSON enviado (ex: { "question": "C# � boa?" })
    AskQuestionService service) => //service: � o servi�o que fala com a OpenAI
{
    var response = await service.ProcessQuestionAsync(req.Question); //envia a pergunta para o ChatGPT
    return Results.Ok(new { answer = response }); //devolve a resposta ao usu�rio
});


//Tudo pronto, agora comece a escutar e responder os pedidos que chegarem.
app.Run();