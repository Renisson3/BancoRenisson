# Desafio Warren

## Índice

[toc]

Esse projeto tem como objetivo criar um sistema capaz de criar contas correntes e realizar operações de deposito, saque e pagamentos.

Além disso, possibilitar a visualização das movimentações feitas nas contas correntes e calcula automaticamente o jurus diário sobre o valor presente na conta corrente.



Para executar o projeto, será necessário instalar os seguintes programas:

- [.Net Core 3.1: Necessário para executar o projeto](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Visual Studio Code: Para desenvolvimento do projeto ](https://code.visualstudio.com/download)
- [MySQL Community Server: Ferramenta de banco de dados](https://dev.mysql.com/downloads/mysql/)
- [Git: Para realizar o controle de versão do projeto](https://git-scm.com/downloads)



## Desenvolvimento

Para iniciar o desenvolvimento, é necessário clonar o projeto do Azure Repos num diretório de sua preferência:

```bash
cd "diretório de sua preferência"
git clone https://github.com/Renisson3/BancoRenisson.git
```



## Construção

Para executar o projeto com o .Net Core, executar o comando abaixo:

```bash
$ cd BancoRenisson.App
$ dotnet restore
$ dotnet build
$ dotnet run
```

O comando irá compilar o projeto e realizar a sua execução.

**OBS:** *Antes de realizar a primeira execução garanta que o **MySQL** esteja instalado, pois ao executar a primeira vez o projeto o mesmo tenta executar uma migração que cria as tabelas necessárias para execução do projeto.*



## Deploy e Publicação

Por se tratar de um projeto construído com .Net Core você pode realizar o deploy e publicação em um servidor com qualquer sistema operacional.



## Features

Algumas das funcionalidades e detalhes do projeto podem ser encontrados nesta sessão e estão listados abaixo:



### Criar Conta Corrente

A aplicação permite criar contas correntes apenas utilizando o nome do titular e clicando no botão **Abrir Conta Corrente**.



### Depósitos

A aplicação permite realizar depósitos em contas correntes apenas utilizando o número da conta, o valor do deposito e clicando no botão **Depositar**.



### Saques

A aplicação permite realizar saques em contas correntes apenas utilizando o número da conta, o valor do saque e clicando no botão **Sacar**.



### Pagamentos

A aplicação permite realizar pagamentos em contas correntes apenas utilizando o número da conta, o valor do pagamento, descrição do pagamento e clicando no botão **Pagar**.



### Cálculo de juros

Utilizando um JOB a aplicação calcula o juros automaticamente uma vez por dia com o valor de base de calculo de 0,5%.



## Configuração

Nessa seção é possível encontrar como realizar configurações na execução e ambiente do projeto.

### Appsettings

É possível realizar algumas configurações para executar o projeto no ambiente desejado. 

No projeto **BancoRenisson.App** é possível encontrar um arquivo chamado `appsettings.json`. Esse arquivo permite flexibilizar o ambiente a qual a aplicação deverá ser executado. Por exemplo temos o seguinte arquivos:

- `appsettings.Development.json` para o ambiente de desenvolvimento;

Nesse arquivo também é possível configurar conexão com o banco de dados e configuração dos jobs.



### JOB (Cálculo de juros)

Para o calculo do juros foi criado um JOB que é executado uma vez por dia, mas vale resaltar que para trocar o seu tempo de execução é possivel alterar no arquivo `appsettings.Development.json`  as propriedades:

```json
"JobScheduleDiary": {
    "TimeToUpdate": 5,
    "StartHour": 1,
    "EndHour": 23
  }
```



## Testes

A aplicação conta com um projeto de testes unitários de domínio e integração chamado de **BancoRenisson.DomainTest**.

No projeto de teste **BancoRenisson.DomainTest** é possível encontrar classes que contem testes de cadastro e das operaçoes de deposito, saque e pagamentos. 

