# Aplicação de Gerenciamento de Vendas, Produtos e Clientes

Este projeto é uma aplicação web desenvolvida com **ASP.NET MVC** utilizando **.NET 8.0**. A aplicação permite o gerenciamento de **Vendas**, **Produtos** e **Clientes** com funcionalidades de CRUD (Criar, Ler, Atualizar e Excluir) para cada uma dessas entidades. Além disso, os dados para as tabelas de clientes, vendas e produtos são carregados através de endpoints externos.

Foi utilizado **SQL Server** como banco de dados para persistência dos dados. Para facilitar a criação das tabelas no banco, está disponível o arquivo `SQLQuery.sql`, que contém os comandos `CREATE TABLE` necessários para configurar o banco de dados.

## Funcionalidades

- **Gestão de Vendas (CRUD):** Permite criar, editar, listar e excluir vendas.
- **Gestão de Produtos (CRUD):** Permite criar, editar, listar e excluir produtos.
- **Gestão de Clientes (CRUD):** Permite criar, editar, listar e excluir clientes.
- **Carregamento de Dados:** A aplicação carrega os dados para as tabelas de **Clientes**, **Vendas** e **Produtos** a partir dos seguintes endpoints:
  - [Clientes](https://camposdealer.dev/Sites/TesteAPI/cliente)
  - [Vendas](https://camposdealer.dev/Sites/TesteAPI/venda)
  - [Produtos](https://camposdealer.dev/Sites/TesteAPI/produto)

  Esses dados são carregados no início para serem utilizados nas operações de CRUD da aplicação.

## Como Rodar o Projeto

1. Clone este repositório para sua máquina local.
2. Abra o projeto no Visual Studio.
3. Configure a string de conexão para o banco de dados no arquivo `app.settings`.
4. Execute a aplicação utilizando o IIS Express ou outro servidor local.
5. Utilize o arquivo `SQLQuery.sql` para criar as tabelas necessárias no banco de dados SQL Server.

## Endpoints de Teste

A aplicação realiza o carregamento de dados para **Clientes**, **Produtos** e **Vendas** através dos seguintes endpoints:

- **Clientes:** [https://camposdealer.dev/Sites/TesteAPI/cliente](https://camposdealer.dev/Sites/TesteAPI/cliente)
- **Vendas:** [https://camposdealer.dev/Sites/TesteAPI/venda](https://camposdealer.dev/Sites/TesteAPI/venda)
- **Produtos:** [https://camposdealer.dev/Sites/TesteAPI/produto](https://camposdealer.dev/Sites/TesteAPI/produto)
