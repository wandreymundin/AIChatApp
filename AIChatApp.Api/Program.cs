// Importação de bibliotecas necessárias
using AIChatApp.Application.Services; // onde estão os serviços da aplicação (a lógica do negócio)
using AIChatApp.Domain.DTOs; // estrutura dos dados (como será enviado e recebido)
using AIChatApp.Domain.Options; // configurações do sistema, como a chave da OpenAI
using AIChatApp.Infrastructure.Clients; // código que se comunica com a OpenAI
using Microsoft.AspNetCore.Mvc; // funcionalidades para trabalhar com web (requisições, JSON etc.)

// Aqui começa tudo.
// Você está criando a aplicação e preparando o sistema para saber como deve funcionar.
// É como montar o esqueleto da casa: onde vai porta, janela, luz, etc.
var builder = WebApplication.CreateBuilder(args);

// Sempre que alguém pedir para falar com a OpenAI, me dê um cliente HTTP pronto.
// O HttpClient é quem faz as chamadas à internet (nesse caso, para o ChatGPT).
builder.Services.AddHttpClient<OpenAIClient>();

// Pegue as configurações da OpenAI (como a API Key) do arquivo de configurações (appsettings.json) e deixe disponível no sistema.
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));

// Adicione o serviço que vai processar as perguntas.
// O `.AddScoped` quer dizer que o serviço será criado a cada requisição HTTP.
builder.Services.AddScoped<AskQuestionService>();

// Essas duas linhas habilitam o Swagger, que é aquela tela azul bonitona onde você consegue testar a API pelo navegador.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// O servidor da aplicação deve escutar na porta 80.
// Isso significa que, quando alguém digitar http://localhost, a aplicação vai estar lá esperando.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // HTTP
});

// Aqui você transforma tudo que configurou antes em uma aplicação real.
// É como terminar de montar a casa e ligar a energia elétrica.
var app = builder.Build();

// "Se estiver rodando em modo de desenvolvimento (não em produção), mostre o Swagger no navegador."
// Em produção, isso normalmente é desabilitado por segurança.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Se alguém acessar http://localhost:porta/, responda com o texto 'API do Chat está no ar'
// Serve como teste para saber se a API está funcionando.
app.MapGet("/", () => Results.Ok("API do Chat está no ar ?"));

// Essa é a parte mais importante da API.
//"Se alguém enviar uma pergunta para o endereço /ask, pegue essa pergunta, envie para o serviço da OpenAI, espere a resposta e devolva essa resposta para quem perguntou.
app.MapPost("/ask", async (
    [FromBody] QuestionRequest req, //req: é o corpo do JSON enviado (ex: { "question": "C# é boa?" })
    AskQuestionService service) => //service: é o serviço que fala com a OpenAI
{
    var response = await service.ProcessQuestionAsync(req.Question); //envia a pergunta para o ChatGPT
    return Results.Ok(new { answer = response }); //devolve a resposta ao usuário
});


//Tudo pronto, agora comece a escutar e responder os pedidos que chegarem.
app.Run();