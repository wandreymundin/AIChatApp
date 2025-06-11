## ?? `README.md` (com badges e visual GitHub-Friendly)

```markdown
# ?? AIChatApp ? C# + OpenAI + Docker

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Status](https://img.shields.io/badge/status-em%20desenvolvimento-green)
![License](https://img.shields.io/badge/license-MIT-brightgreen)
![Docker](https://img.shields.io/badge/containerized-Docker-blue)

> Projeto criado para a **DEVCON 2025**: uma API leve, moderna e inteligente, desenvolvida com C#, Minimal APIs, OpenAI e Docker.

---

## ? Funcionalidades

- ? API com `.NET 8` e Minimal APIs
- ? Integra��o com OpenAI (ChatGPT)
- ? Organiza��o em camadas (Domain, Application, Infrastructure, Api)
- ? Suporte a perfis `Development` e `Production`
- ? Swagger UI ativado no modo dev
- ? Docker e Docker Compose com profiles

---

## ?? Execu��o com Docker

### Clone o reposit�rio:

```bash
git clone https://github.com/seu-usuario/aichatapp.git
cd aichatapp
```

### Configure a chave da OpenAI:

Edite o `appsettings.json` dentro da pasta `AIChatApp.Api`:

```json
{
  "OpenAI": {
    "ApiKey": "sua-chave-aqui"
  }
}
```

### Rode com Docker Compose:

#### Ambiente de desenvolvimento:

```bash
docker compose --profile dev up --build
```

? Acesse a API em: [http://localhost:5000/swagger](http://localhost:5000/swagger)

#### Ambiente de produ��o:

```bash
docker compose --profile prod up --build
```

? API em produ��o via `http://localhost:8080`

---

## ?? Execu��o sem Docker (manual)

```bash
cd AIChatApp.Api
dotnet run
```

? Acesse: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## ?? Exemplo de requisi��o

```http
POST /ask
Content-Type: application/json

{
  "question": "C# � uma boa linguagem?"
}
```

**Resposta:**

```json
{
  "answer": "Sim, C# � uma linguagem de programa��o popular e eficiente..."
}
```

---

## ??? Estrutura do Projeto

```
AIChatApp.sln
??? AIChatApp.Api/           ? Minimal API (Program.cs)
??? AIChatApp.Application/   ? L�gica de orquestra��o (servi�os)
??? AIChatApp.Infrastructure/? Integra��es externas (OpenAI)
??? AIChatApp.Domain/        ? DTOs e configura��es
```

---

## ?? Docker Compose

```yaml
services:
  aichatapp:
    build: .
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    profiles:
      - dev

  aichatapp-prod:
    build: .
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
    profiles:
      - prod
```

---

## ?? Sobre a OpenAI

- Plataforma oficial: [https://platform.openai.com](https://platform.openai.com)
- API usada: `https://api.openai.com/v1/chat/completions`
- � necess�rio possuir uma chave de API v�lida e com cr�ditos

---

## ?? Autor

Desenvolvido por [Wandrey Mundin](https://github.com/seu-usuario)  
Apresentado na [DEVCON 2025](https://devcon.com)

---

## ?? Licen�a

Este projeto est� licenciado sob a [MIT License](LICENSE).

```