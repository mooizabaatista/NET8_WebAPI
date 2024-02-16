## Web API .NET 8

### Descrição
Este projeto é uma Web API desenvolvida em .NET 8, criada com o propósito de estudar e implementar conceitos de Web API, autenticação, autorização e uso de tokens JWT Bearer.

### Framework
.NET 8

### Pacotes Utilizados
- **Entity Framework Core**: Mapeamento objeto-relacional para acesso a dados.
- **Identity**: Gerenciamento de identidade e autenticação de usuários.
- **AutoMapper**: Mapeamento de objetos automaticamente entre modelos de dados e DTOs.
- **JwtBearer**: Autenticação baseada em tokens JWT.
- **SqlServer**: Base de dados.

### Configuração

Antes de iniciar o projeto, é necessário realizar algumas configurações:

#### 1. Atualização da ConnectionString

- Acesse o arquivo `MbStore.Infra -> Context -> AppDbContext`.
- Localize a connectionString e atualize conforme as configurações do seu ambiente.

#### 2. Atualização da Base de Dados

- Caso utilize o terminal, execute o comando:
     ```
         dotnet ef database update
     ```
     
- Caso utilize o package manager console:
    ```
        update-database
    ```
    
    ###### Ao executar o comando `update-database`, um usuário administrador será criado automaticamente, com todas as roles associadas.

**Credenciais do usuário administrador:**
- **Usuário**: admin
- **Senha**: admin

## Roles
- **Admin**: Permissões administrativas completas.
- **Owner**: Permissões específicas de proprietário.
- **User**: Permissões padrão de usuário.

## Endpoints

### Products

- **GetAll**: Retorna todos os produtos disponíveis. Qualquer usuário autorizado pode acessar.
- **GetById**: Retorna um produto específico. Qualquer usuário autorizado pode acessar.
- **Create**: Cria um novo produto. Apenas administradores e proprietários podem acessar.
- **Update**: Atualiza um produto existente. Apenas administradores e proprietários podem acessar.
- **Delete**: Remove um produto existente. Apenas administradores e proprietários podem acessar.

### Categories

- **GetAll**: Retorna todas as categorias disponíveis. Qualquer usuário autorizado pode acessar.
- **GetById**: Retorna uma categoria específica. Qualquer usuário autorizado pode acessar.
- **Create**: Cria uma nova categoria. Apenas administradores e proprietários podem acessar.
- **Update**: Atualiza uma categoria existente. Apenas administradores e proprietários podem acessar.
- **Delete**: Remove uma categoria existente. Apenas administradores e proprietários podem acessar.

### Auth

- **Register**: Registra um novo usuário no sistema. Sem restrição de acesso.
- **Login**: Autentica um usuário no sistema. Sem restrição de acesso.
- **MakeAdmin**: Concede a função de administrador a um usuário existente. Apenas administradores podem acessar.
- **MakeOwner**: Concede a função de proprietário a um usuário existente. Apenas administradores podem acessar.
- **Changepassword**: Permite que um usuário altere sua senha. Apenas administradores e proprietários podem acessar.
