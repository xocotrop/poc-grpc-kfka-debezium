# Ambiente

## Banco de dados Sql Server

Caso não possua uma instancia com SQL Server, segue comando docker para levantar um banco de dados sql server
* docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=P@ssw0rd -e MSSQL_PID=Developer -p 1433:1433 -e MSSQL_AGENT_ENABLED=true -d mcr.microsoft.com/mssql/server:2019-latest 

Comando para subir o banco na porta default 1433 com a senha P@sswo0rd para o usuário 'SA'

### Criação do banco e tabela

```
-- 1- Criação do database
CREATE DATABASE Teste

Use Teste
GO

-- 2- Criação da tabela
CREATE TABLE Person (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(200) NOT NULL,
    Phone VARCHAR(11)
)
```

### Habilitar CDC no banco de dados
```
-- Habilitar CDC
EXEC sys.sp_cdc_enable_db
GO
```

### Configurar o CDC para a tabela
```
-- Criar o CDC para a tabela Person
EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name   = N'Person',
@role_name     = N'Admin',
@supports_net_changes = 1
GO
```

## Outros serviços com docker compose

Dentro da pasta docker-compose tem um arquivo docker-compose.yml para lenvatar os outros serviços que são eles:

* Zookeeper - porta 2181
* Kafka - porta 9092
* debezium - porta 8083

Acesse pelo terminal onde está localizad o arquivo docker-compose.yml e execute o comando para subir os containers

* docker-compose up

### Zookeeper

O Zookeeper é necessário para rodar o kafka. O kafka utiliza ele para sincronizar as informações entre os clusters

[Link de Referência](https://medium.com/trainingcenter/apache-kafka-codifica%C3%A7%C3%A3o-na-pratica-9c6a4142a08f#:~:text=Resumidamente%2C%20o%20Apache%20Zookeeper%20%C3%A9,as%20configura%C3%A7%C3%B5es%20entre%20diferentes%20clusters.)

### Kafka

Serviço utilizado para enviar mensagens para os tópicos e também ler deles

[O que é o kafka](https://blog.geekhunter.com.br/apache-kafka/)

### Debezium

Serviço responsável por monitorar as mudanças (CRUD) do SQL e enviar para o kafka através do CDC do banco de dados (Change Data Capture)


## Configuração do debezium
### Rest para configuração do debezium

```
curl --location --request POST 'http://localhost:8083/connectors' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "sqlserver-person-connector1",
    "config": {
        "connector.class" : "io.debezium.connector.sqlserver.SqlServerConnector",
        "database.hostname": "192.168.110.241",
        "database.port": "1433",
        "database.user": "sa",
        "database.password": "P@ssw0rd",
        "database.dbname": "Teste",
        "database.server.name": "dbserver1",
        "table.whitelist": "dbo.Person",
        "database.history.kafka.bootstrap.servers": "192.168.110.241:9092",
        "database.history.kafka.topic": "dbhistory.person"
    }
}'
```
* name - alias para a configuração
* connector.class - conector do sql server
* database.hostname - ip do banco de dados
* database.port - porta do BD
* database.password - senha do BD
* database.dbname - Banco de dados
* database.server.name - alias
* table.whitelist - tabela a monitorar
* database.history.kafka.bootstrap.servers - endereço do kafka
* database.history.kafka.topic - topico do kafka para enviar as mensagens

### Rest para remover configuração do debezium
```
curl --location --request DELETE 'http://localhost:8083/connectors/sqlserver-person-connector1' \
--data-raw ''
```

* após connectors/ é o mesmo que foi utilizado em **name** para criar

### Rest para listar os conectores

```
curl --location --request GET 'http://localhost:8083/connectors'
```

### Rest para ver a configuração específica
```
curl --location --request GET 'http://localhost:8083/connectors/sqlserver-person-connector1' \
--data-raw ''
```
* após connectors/ é o mesmo utilizado em **name**

### Rest para ver o status de uma configuração
```
curl --location --request GET 'http://localhost:8083/connectors/sqlserver-person-connector1/status' \
--data-raw ''
```
___

```
No exemplo foi utilizado o ip: 192.168.110.241 que é o ip da maquina linux que subiu nos exemplos, altere para o seu ip
```

[Referência ao uso do debezium e kafka](https://medium.com/jundevelopers/debezium-kafka-e-net-core-9cee3ca3e0db)