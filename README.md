# Ficha Anest�sica - API (BFF)

API respons�vel pelo registro e gerenciamento de dados de anestesia, fornecendo integra��o e processamento de informa��es cl�nicas.

## ?? Sobre o Projeto
Este reposit�rio cont�m o **Backend For Frontend (BFF)** do aplicativo "Ficha Anest�sica", garantindo que o front-end receba dados limpos, tipados e adaptados para o uso _offline-first_. 

O backend � inteiramente constru�do em **C# / .NET 8**, utilizando padr�es robustos como Repository Pattern e CQRS (via AppServices/Commands/Queries), e se comunica nativamente com bancos de dados relacionais avan�ados (PostgreSQL via Supabase).

Esta API intermedi�ria consome e converte os dados provenientes da rede central/defasada do hospital (HUAP), que atualmente conta com um mock constru�do em PHP (Laravel).

## ??? Stack Tecnol�gico
* **Framework:** C# / ASP.NET Core 8.0 (.NET 8)
* **ORM:** Entity Framework Core
* **Banco de Dados:** PostgreSQL (Supabase)
* **Arquitetura:** DDD (Domain-Driven Design focado), CQRS, Repository Pattern
* **Integra��o/Http:** IHttpClientFactory para varredura e parse de sistemas legados.

## ?? Como Executar Localmente

### Pr�-requisitos
* Ter o **.NET SDK 8.0.x** instalado.
* Observa��o: O reposit�rio possui um arquivo global.json que trava a execu��o estritamente para ferramentas do .NET 8 (Ex: 8.0.419), a fim de manter consist�ncia de ambiente entre os desenvolvedores.

### Passos
1. Clone ou baixe o reposit�rio.
2. Navegue via terminal at� a raiz do projeto (onde est� o .sln ou em UFF.FichaAnestesica.Api/).
3. (Opcional) Restaure os pacotes:
   dotnet restore
4. Navegue para a pasta da API e inicie o projeto no modo HTTP:
   cd UFF.FichaAnestesica.Api
   dotnet run --launch-profile "http"
5. Acesse o **Swagger** gerado automaticamente para visualizar e testar os _endpoints_ nativos atrav�s do link:
   ?? http://localhost:5211/swagger

> **Nota sobre o Mock do PHP:**  
> A rota de listagem de cirurgias di�rias (GET /api/Cirurgias/hoje) bate em um servi�o externo localizado em localhost:8000. Portanto, certifique-se de que a API simulada do Hospital (huap-api-mock) esteja rodando em paralelo durante seus testes.

## ?? Cart�es Entregues Recentes / Changelog
- **[FA-013 / FA-014]**: Configura��o base, appsettings e servi�os base com Entity Framework (Bruno Pe�anha).
- **[FA-025]**: Criação do BFF (C#) com Integração à API do HUAP (PHP) via HTTP Client (Mateus).
- **[FA-026]**: Refatoração CQRS e Persistência de Cirurgias com PostgreSQL via EF Core (Mateus).
