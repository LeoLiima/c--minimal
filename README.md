# API de Gerenciamento de Monitores e Horários

> API RESTful para gerenciar o cadastro de monitores acadêmicos e seus respectivos horários de atendimento.

<br>

## 📝 Descrição

Este projeto é uma API desenvolvida em C# com .NET, utilizando a abordagem de **Minimal APIs**. O objetivo é fornecer endpoints para realizar operações de CRUD (Create, Read, Update, Delete) para monitores e seus horários. A aplicação foi estruturada para ser leve e eficiente, ideal para microsserviços e aplicações web modernas.

Foi uma oportunidade prática para aplicar conceitos como injeção de dependência, acesso a dados com Entity Framework Core e a criação de uma API funcional e bem definida.

<br>

## ✨ Tecnologias e Conceitos Aplicados

Esta seção detalha as principais tecnologias e padrões de projeto utilizados.

*   **C# e .NET**: Linguagem e plataforma principal para toda a construção da lógica de negócio e da API.
*   **Minimal APIs (.NET)**: A estrutura da API foi toda construída utilizando o paradigma de Minimal APIs. Isso resultou em um código mais limpo e direto ao ponto, com menos boilerplate, ideal para focar na lógica dos endpoints. Todo o roteamento e manipulação de requisições (POST, GET, PUT, DELETE) foram feitos de forma explícita e concisa no arquivo `Program.cs`.
*   **Entity Framework Core**: Utilizado como ORM (Object-Relational Mapper) para fazer a ponte entre o código C# e o banco de dados SQL Server. Ele foi responsável por mapear as classes `Moni` e `Horario` para tabelas no banco.
*   **SQL Server**: Banco de dados relacional escolhido para a persistência dos dados. A conexão foi configurada no `Program.cs` através da string de conexão.
*   **DTO (Data Transfer Objects)**: O padrão DTO foi implementado (`MonitorDTO`, `HorarioDTO`) para desacoplar o modelo de domínio do modelo da API. Isso garante que apenas os dados necessários sejam expostos nos endpoints, prevenindo exposição de dados sensíveis e over-posting.
*   **EF Core Migrations**: Para gerenciar a evolução do esquema do banco de dados, as Migrations do EF Core foram utilizadas. Isso permite que o banco de dados seja criado e atualizado de forma versionada e automática a partir dos modelos definidos no código.
*   **Enums (Enumerações)**: O `Enum` de dias da semana (`DiaDaSemana`) foi utilizado para garantir consistência e evitar erros de digitação ao registrar os horários, representando os dias da semana de forma mais segura e legível no código.
*   **Injeção de Dependência**: O `APIDbContext` foi registrado nos serviços do .NET e injetado diretamente nos endpoints, uma prática que facilita os testes e o gerenciamento do ciclo de vida dos objetos.

<br>

⚙️ Endpoints Disponíveis
POST /monitor: Cria um novo monitor.
<br>

POST /horarios: Adiciona um novo horário para um monitor.
<br>

GET /monitores/listar: Lista todos os monitores cadastrados.
<br>

GET /monitores/{apelido}: Busca um monitor específico pelo apelido e lista seus horários.
<br>

PUT /monitores/{id}: Atualiza os dados de um monitor.
<br>

PUT /horario/{id}: Atualiza um horário existente.
<br>

DELETE /monitor/{id}: Deleta um monitor (se não houver horários associados).
<br>

DELETE /horarios/{id}: Deleta um horário específico.
<br>

👨‍💻 Autor

