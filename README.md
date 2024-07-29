# Thunders.Tasks

## Índice

1. [Descrição do Projeto](#descricao-do-projeto)
2. [Estrutura do Projeto](#estrutura-do-projeto)
3. [Pré-requisitos](#pre-requisitos)
4. [Configuração do Ambiente](#configuracao-do-ambiente)
   - [Configuração do Docker](#configuracao-do-docker)
5. [Executando a Aplicação](#executando-a-aplicacao)

## Descrição do Projeto

Esta é uma API de gerenciamento de tarefas desenvolvida utilizando ASP.NET Core Web API 8.

## Estrutura do Projeto

O projeto foi organizado da seguinte forma:

Thunders.Tasks
│── src
│ ├── Thunders.Tasks.WebApi # Projeto principal da API
│ ├── Thunders.Tasks.Application # Camada de aplicação (CQRS, Handlers)
│ ├── Thunders.Tasks.Core # Camada de domínio (Entidades, Regras de Negócio)
│ ├── Thunders.Tasks.Infrastructure # Infraestrutura (EF Core, Migrations, Repositórios)
│ ├── Thunders.Tasks.Tests # Testes unitários
├── docker-compose.yml # Arquivo de configuração do Docker Compose
├── README.md # Documentação do projeto

## Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/)

## Configuração do Ambiente

### Configuração do Docker

1. **Criação do volume para o banco de dados***:

	O volume garante a persistência dos dados do SQL Server. Neste projeto, por default, a pasta encontra-se em "C:\data\sqlserver" (vide arquivo docker-compose.yml).

2. Arquivo `docker-compose.yml`:
	
	O arquivo define os serviços necessários para a aplicação, incluindo o SQL Server e a aplicação ASP.NET Core, além de um serviço para executar as migrações.
  
    ```
    version: '3.4'

    services:
      sqlserver:
        image: mcr.microsoft.com/mssql/server
        container_name: thunders-db-sqlserver
        environment:
          SA_PASSWORD: "P@ssword"
          ACCEPT_EULA: "Y"
        ports:
          - "1433:1433"
        volumes:
          - c:\data\sqlserver:/var/opt/mssql/data
      
      tasklistapi:
        image: tasklistapi
        build:
          context: .
          dockerfile: src/Thunders.Tasks.WebApi/Dockerfile
        container_name: thunders-api-container
        ports:
          - "5000:5000"
          - "5001:5001"
        environment:
          - ASPNETCORE_ENVIRONMENT=Development
          - ConnectionStrings__DefaultConnection=Server=sqlserver;Initial Catalog=thunders-db;User Id=SA;Password=P@ssword;TrustServerCertificate=true;
    ```

## Executando a Aplicação

Para executar a aplicação utilizando Docker Compose, siga os passos abaixo:

1. Construir e iniciar os containers:

    ```
    docker-compose up -d
    ```

2. Acessar a aplicação:
    
    A API estará disponível em `https://localhost:5001/`. Para acessar o Swagger, basta ir em `https://localhost:5001/swagger/index.html`.