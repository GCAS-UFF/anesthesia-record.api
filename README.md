# 🏥 Ficha Anestésica - API (BFF)

API responsável pelo registro e gerenciamento de dados de anestesia, fornecendo integração e processamento de informações clínicas.

## 📖 Sobre o Projeto
Este repositório contém o **Backend For Frontend (BFF)** do aplicativo "Ficha Anestésica", garantindo que o front-end receba dados limpos, tipados e adaptados para o uso _offline-first_. 

O backend é inteiramente construído em **C# / .NET 8**, utilizando padrões robustos como Repository Pattern e CQRS (via AppServices/Commands/Queries), e se comunica nativamente com bancos de dados relacionais avançados (PostgreSQL via Supabase).

Esta API intermediária consome e converte os dados provenientes da rede central/defasada do hospital (HUAP), que atualmente conta com um mock construído em PHP (Laravel).

## 💻 Stack Tecnológico
* **Framework:** C# / ASP.NET Core 8.0 (.NET 8)
* **ORM:** Entity Framework Core
* **Banco de Dados:** PostgreSQL (Supabase)
* **Arquitetura:** DDD (Domain-Driven Design focado), CQRS, Repository Pattern
* **Integração/Http:** IHttpClientFactory para varredura e parse de sistemas legados.

## 🚀 Como Executar Localmente

### 📌 Pré-requisitos
* Ter o **.NET SDK 8.0.x** instalado.
* Observação: O repositório possui um arquivo global.json que trava a execução estritamente para ferramentas do .NET 8 (Ex: 8.0.419), a fim de manter consistência de ambiente entre os desenvolvedores.

### 🛠️ Passos
1. Clone ou baixe o repositório.
2. Navegue via terminal até a raiz do projeto (onde está o .sln ou em UFF.FichaAnestesica.Api/).
3. (Opcional) Restaure os pacotes:
   `ash
   dotnet restore
   `
4. Navegue para a pasta da API e inicie o projeto no modo HTTP:
   `ash
   cd UFF.FichaAnestesica.Api
   dotnet run --launch-profile "http"
   `
5. Acesse o **Swagger** gerado automaticamente para visualizar e testar os _endpoints_ nativos através do link:
   🔗 http://localhost:5211/swagger

> ⚠️ **Nota sobre o Mock do PHP:**  
> A rota de listagem de cirurgias diárias (GET /api/Cirurgias/hoje) bate em um serviço externo localizado em localhost:8000. Portanto, certifique-se de que a API simulada do Hospital (huap-api-mock) esteja rodando em paralelo durante seus testes.

## 📝 Cartões Entregues Recentes / Changelog
- **[FA-013 / FA-014]**: Configuração base, appsettings e serviços base com Entity Framework (Bruno Peçanha).
- **[FA-025]**: Criação do BFF (C#) com Integração à API do HUAP (PHP) via HTTP Client (Mateus).
- **[FA-026]**: Refatoração CQRS e Persistência de Cirurgias com PostgreSQL via EF Core (Mateus).
