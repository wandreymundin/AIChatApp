# 🤖 AIChatApp – C# + OpenAI + Docker

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Status](https://img.shields.io/badge/status-em%20desenvolvimento-green)
![License](https://img.shields.io/badge/license-MIT-brightgreen)
![Docker](https://img.shields.io/badge/containerized-Docker-blue)

> Projeto criado para a **DEVCON 2025**: uma API leve, moderna e inteligente, desenvolvida com C#, Minimal APIs, OpenAI e Docker.

---

## ✨ Funcionalidades

- ✅ API com `.NET 8` e Minimal APIs
- ✅ Integração com OpenAI (ChatGPT)
- ✅ Organização em camadas (Domain, Application, Infrastructure, Api)
- ✅ Suporte a perfis `Development` e `Production`
- ✅ Swagger UI ativado no modo dev
- ✅ Docker e Docker Compose com profiles

---

## 🚀 Execução com Docker

### 1. Clone o repositório

```bash
git clone https://github.com/wandreymundin/aichatapp.git
cd aichatapp
```

### 2. Configure a chave da OpenAI

Edite o arquivo `AIChatApp.Api/appsettings.json`:

```json
{
  "OpenAI": {
    "ApiKey": "sua-chave-aqui"
  }
}
```

### 3. Executar com Docker Compose

#### 🔧 Ambiente de desenvolvimento (porta 5000, Swagger habilitado):

```bash
docker compose --profile dev up --build
```

➡ Acesse: [http://localhost:5000/swagger](http://localhost:5000/swagger)

#### 🚀 Ambiente de produção (porta 8080, sem Swagger):

```bash
docker compose --profile prod up --build
```

➡ Acesse: [http://localhost:8080](http://localhost:8080)

---

## 🐳 Execução manual com Docker (sem Compose)

### Build da imagem:

```bash
docker build -t aichatapp .
```

### Executar em modo desenvolvimento (Swagger habilitado):

```bash
docker run -e ASPNETCORE_ENVIRONMENT=Development -p 5000:80 aichatapp
```

➡ Acesse: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🔧 Execução local sem Docker

### Pré-requisitos:
- .NET SDK 8.0 instalado

### Comando:

```bash
cd AIChatApp.Api
dotnet run
```

➡ Acesse: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## 🔄 Exemplo de requisição

```http
POST /ask
Content-Type: application/json

{
  "question": "C# é uma boa linguagem?"
}
```

**Resposta:**

```json
{
  "answer": "Sim, C# é uma linguagem de programação popular e eficiente..."
}
```

---

## 🗂️ Estrutura do Projeto

```
AIChatApp.sln
├── AIChatApp.Api/           → Minimal API (Program.cs)
├── AIChatApp.Application/   → Lógica de orquestração (serviços)
├── AIChatApp.Infrastructure/→ Integrações externas (OpenAI)
├── AIChatApp.Domain/        → DTOs e configurações
```

---

## 📦 docker-compose.yml

```yaml
version: "3.9"

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

## 💡 Sobre a OpenAI

- Plataforma oficial: [https://platform.openai.com](https://platform.openai.com)
- API usada: `https://api.openai.com/v1/chat/completions`
- É necessário possuir uma chave de API válida e com créditos

---

## 🧠 Autor

Desenvolvido por [Wandrey Mundin](https://github.com/wandreymundin)  
Apresentado na [DEVCON 2025](https://devcon.dev.br)

---

## 📜 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).