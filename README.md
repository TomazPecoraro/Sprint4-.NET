# Sprint4-.NET

## Descrição da Arquitetura
A API segue a arquitetura **monolítica**, implementada com **ASP.NET Core Web API**, organizando as responsabilidades em camadas:

- **Controllers**: Gerenciam requisições HTTP e respostas.
- **Services**: Contêm a lógica de negócios.
- **Repository**: Acessa o banco de dados para CRUD.
- **Models**: Define as entidades e DTOs.
- **Database**: Configuração do Entity Framework com Oracle.

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
     https://localhost:5025/swagger
     ```

### Testes

1. **Executar Testes Automatizados:**
   - Para rodar os testes automatizados, use o comando:
     ```bash
     dotnet test
     ```

### Exemplos de Uso

#### Requisição GET para `api/usuario`

```bash
curl -X GET "https://localhost:5025/api/usuario" -H "accept: application/json"
```


## Integrantes do Grupo
Tomaz de Oliveira Pecoraro – RM98499

Rennan Ferreira da Cruz – RM99364

Jaisy Cibele Alves – RM552269

Luiz Felipe Camargo Prendin – RM552475

Gabriel Amâncio - RM97936
