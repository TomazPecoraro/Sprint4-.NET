# Sprint4-.NET

## Descrição da Arquitetura
A API segue a arquitetura **monolítica**, implementada com **ASP.NET Core Web API**, organizando as responsabilidades em camadas:

- **API**: Contém a lógica de controle da API, incluindo controladores e configurações.
- **Controllers**: Gerenciam requisições HTTP e respostas.
- **Services**:  Contém a lógica de negócios, dividida em subpastas por funcionalidade.
- **Repository**: Implementa o padrão de repositório para acesso a dados.
- **Models**: Define as entidades e DTOs.
- **Database**: Contém a configuração do contexto do banco de dados Oracle.
- **Tets**: Testes Unitários ultilizando Xunit e Moq
- **Firebase**: Implementação do Firebase
- **ML**: Previsão de Impressões em Campanhas


## Design Patterns Utilizados
1. **Repository Pattern**: Encapsula o acesso ao banco de dados.
2. **Dependency Injection**: Reduz acoplamento e facilita testes.
3. **Singleton Pattern**: Garante uma única instância para serviços.

## Instruções para Rodar a API

### Pré-requisitos

Antes de rodar a API, certifique-se de ter os seguintes itens instalados:

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [Oracle Database](https://www.oracle.com/database/)
- [Postman](https://www.postman.com/downloads/) (para testar os endpoints)

### Configuração do Banco de Dados

1. **Configure o Banco de Dados Oracle:**
   - Siga as instruções fornecidas pela Oracle para configurar o banco de dados localmente.

2. **Atualize a String de Conexão:**
   - Abra o arquivo `appsettings.json` localizado na raiz do projeto.
   - Atualize o valor da chave `OracleFIAP` em `ConnectionStrings` com as informações de conexão do seu banco de dados Oracle. Exemplo:
     ```json
     "ConnectionStrings": {
       "OracleFIAP": "Data Source=seu_host:1521/seu_serviço;User ID=seu_usuario;Password=sua_senha;"
     }
     ```

### Instalação

1. **Clone o Repositório:**
   - Clone o repositório para o seu ambiente local usando o seguinte comando:
     ```bash
     git clone https://github.com/TomazPecoraro/Sprint4-.NET.git
     ```
   - Navegue para o diretório do projeto:
     ```bash
     cd Sprint4-.NET
     ```

2. **Restaure as Dependências:**
   - Execute o comando para restaurar as dependências do projeto:
     ```bash
     dotnet restore
     ```

### Execução

1. **Rodar a API:**
   - Use o seguinte comando para rodar a API localmente:
     ```bash
     dotnet run --project Sprint4.API/Sprint4.API.csproj
     ```
   - A API estará disponível em `https://localhost:5025` por padrão.

2. **Acessar a Documentação da API:**
   - A documentação Swagger pode ser acessada navegando para:
     ```markdown
     http://localhost:5025/swagger/index.html
     ```

### Testes

1. **Executar Testes Automatizados:**
   - Para rodar os testes automatizados, use o comando:
     ```bash
     dotnet test
     ```

### Exemplos de Uso

- Testes de Integração das Entidades Anuncio, Campanha e Usuario
- Teste da Camada Service de cada Classe
- Utilização do Swagger para teste de todos os EndPoint e Operações CRUD

#### Requisição GET de todas a entidades

```bash
http://localhost:5025/swagger/index.html
```

## Práticas de Clean Code

O projeto segue as prática de Clean Code e SOLID, sendo bem especifico e bem estruturado. Facilitando a organizando e a escalabilidade da soluçÃo

Entendido! Vou simplificar a explicação das funções de ML:

## Funções de Machine Learning (ML)

As funções de Machine Learning no projeto **Sprint4-.NET** foram desenvolvidas para facilitar a previsão de impressões nas campanhas, ajudando a melhorar o desempenho geral.

### 1. Previsão de Impressões

- **Objetivo**: Estimar o número de impressões de uma campanha com base em dados anteriores.
- **Como Funciona**: O modelo usa dados históricos para prever a quantidade de impressões esperadas, considerando variáveis como orçamento e público.

### 2. Simplicidade e Integração

- **Objetivo**: Permitir o uso fácil das previsões de impressões diretamente na API.
- **Como Funciona**: A API disponibiliza um endpoint onde o usuário pode enviar os dados da campanha e receber a previsão de impressões, sem complexidades adicionais.
  ```markdown
     http://localhost:5025/swagger/predict
     ```

### 3. Atualização com Novos Dados

- **Objetivo**: Melhorar as previsões ao longo do tempo.
- **Como Funciona**: À medida que mais dados de campanhas são coletados, eles podem ser usados para atualizar o modelo e tornar as previsões mais precisas.

Esse modelo de ML é simples e direto, mas fornece uma visão útil para planejar campanhas com base em previsões.

## Integrantes do Grupo
Tomaz de Oliveira Pecoraro – RM98499

Rennan Ferreira da Cruz – RM99364

Jaisy Cibele Alves – RM552269

Luiz Felipe Camargo Prendin – RM552475

Gabriel Amâncio - RM97936
