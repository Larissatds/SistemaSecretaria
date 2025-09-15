# Sistema de Secretaria Acadêmica

Este projeto é uma **API RESTful** desenvolvida como parte de um desafio técnico.  
O objetivo é fornecer um sistema administrativo para uma secretaria, permitindo **gerenciar alunos, turmas e matrículas**.

---

## Tecnologias Utilizadas

- **Framework:** .NET 6
- **Banco de Dados:** SQL Server
- **ORM:** Entity Framework Cor
- **Autenticação:** JWT
- **Arquitetura:** Clean Architecture (Domain, Application, Infrastructure, API)

---

## Recursos do Sistema

O administrador terá acesso a três entidades principais:

### Aluno
- Campos: **NomeCompleto, DataNascimento, CPF, Email, Senha**
- Operações:
  - Cadastrar
  - Listar (com paginação e filtro)
  - Editar
  - Excluir

### Turma
- Campos: **Nome, Descricao**
- Operações:
  - Cadastrar
  - Listar
  - Editar
  - Excluir

### Matrícula
- Campos: **IdAluno, IdTurma, Status**
- Operações:
  - Cadastrar um aluno em uma turma
  - Listar os alunos matriculados em uma turma (com paginação e filtro)

---

## Como Executar o Projeto
### Pré-requisitos
- Visual Studio 2022
- .NET 6 SDK
- SQL Server
- Postman ou similar para testar os endpoints

### Configuração do Banco
O banco de dados precisa ser criado com o nome `SISTEMA_SECRETARIA` no servidor `(localdb)\\MSSQLLocalDB`, depois basta executar o script **`dump.sql`** localizado na raiz do projeto.
> Importante:  
Se o banco não for criado com esse nome e nesse servidor, será necessário alterar a **connection string** no arquivo `appsettings.json`.

Esse arquivo contém:
- Estrutura das tabelas (Alunos, Turmas, Matriculas)
- Chaves primárias e estrangeiras
- Registros iniciais para teste

### Executar a API
- Pressionar F5 no teclado ou se preferir executar o comando:
  ```bash
  dotnet run --project SistemaSecretaria.API
  ```

---

## Autenticação
Todas as rotas (exceto login) exigem autenticação JWT.
1. **Login**
  - Endpoint: POST /api/auth/login
  - Body:
  ```json
  {
    "email": "admin@secretaria.com",
    "senha": "Admin123!"
  }
  ```
  - Retorna o token JWT

2. **Requisições Autenticadas**
   - Enviar o token no cabeçalho:
     ```makefile
      Authorization: Bearer <seu_token>
     ```

---

## Endpoints Principais
### Alunos
- **POST /api/aluno**
  - Body (JSON):
  ```json
  {
    "NomeCompleto": "",
    "DataNascimento": "yyyy-MM-DD",
    "CPF": "",
    "Email": "",
    "Senha": ""
  }
  ```
- **GET /api/aluno**
  - Body (JSON):
  ```json
  {
    "NumeroPagina": 1,
    "TamanhoPagina": 10,
    "Nome": ""
  }
  ```
- **PUT /api/aluno**
   - Body (JSON):
  ```json
  {
    "IdAluno": 0,
    "NomeCompleto": "",
    "DataNascimento": "yyyy-MM-DD",
    "CPF": "",
    "Email": "",
    "Senha": ""
  }
  ```
- **DELETE /api/aluno/{id}**

### Turmas
- **POST /api/turma**
  - Body (JSON):
  ```json
  {
    "Nome": "",
    "Descricao": ""
  }
  ```
- **GET /api/turma**
  - Body (JSON):
  ```json
  {
    "numeroPagina": 1,
    "tamanhoPagina": 10,
  }
  ```
- **PUT /api/turma**
   - Body (JSON):
  ```json
  {
    "IdTurma": 0,
    "Nome": "",
    "Descricao": ""
  }
  ```
- **DELETE /api/turma/{id}**

### Matrículas
- **POST /api/matricula**
  - Body (JSON):
  ```json
  {
    "IdAluno": 0,
    "IdTurma": 0,
    "Status": ""
  }
  ```
- **GET /api/matricula**
  - Body (JSON):
  ```json
  {
    "IdTurma": 0,
    "NumeroPagina": 1,
    "TamanhoPagina": 10,
  }
  ```
