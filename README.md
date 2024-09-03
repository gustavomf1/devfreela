# DevFreela

**DevFreela** é uma aplicação Web API desenvolvida como parte da minha formação em ASP.NET Core. O projeto tem como objetivo fornecer uma plataforma de gerenciamento de freelancers, onde é possível gerenciar usuários, projetos e habilidades (skills) por meio de operações CRUD (Create, Read, Update, Delete).

## Funcionalidades

A aplicação DevFreela oferece as seguintes funcionalidades:

### 1. CRUD de Usuários (Users)
- **Criação de usuários**: Permite a criação de novos usuários na plataforma.
- **Leitura de usuários**: Recupera informações de usuários cadastrados.
- **Atualização de usuários**: Permite a edição de informações dos usuários existentes.
- **Exclusão de usuários**: Remove usuários da plataforma.

### 2. CRUD de Projetos (Projects)
- **Criação de projetos**: Permite a criação de novos projetos para os freelancers.
- **Leitura de projetos**: Recupera informações sobre projetos existentes.
- **Atualização de projetos**: Permite a edição de projetos cadastrados.
- **Exclusão de projetos**: Remove projetos da plataforma.

### 3. CRUD de Habilidades (Skills)
- **Criação de habilidades**: Permite o cadastro de novas habilidades que podem ser associadas a usuários.
- **Leitura de habilidades**: Recupera a lista de habilidades disponíveis.
- **Atualização de habilidades**: Permite a edição de habilidades cadastradas.
- **Exclusão de habilidades**: Remove habilidades da plataforma.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework utilizado para construir a Web API.
- **Entity Framework Core**: Utilizado para o mapeamento objeto-relacional (ORM) e manipulação do banco de dados.
- **SQL Server**: Banco de dados utilizado para armazenar as informações da aplicação.

## Como Executar o Projeto

1. **Clonar o Repositório**

   Clone o repositório para o seu ambiente local:

   ```bash
   git clone https://github.com/seu-usuario/devfreela.git
   
2. **Configurar o Banco de Dados**

   Configure a string de conexão para o banco de dados SQL Server no arquivo `appsettings.json`.

3. **Aplicar Migrações**

   Execute o comando abaixo para aplicar as migrações e criar o banco de dados:

  ```bash
  dotnet ef database update
```

4. **Executar a Aplicação**
   
    Execute a aplicação usando o comando:
  ```bash
  dotnet run
