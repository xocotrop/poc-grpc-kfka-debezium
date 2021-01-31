# Ambiente
___

## Banco de dados Sql Server

Caso não possua uma instancia com SQL Server, segue comando docker para levantar um banco de dados sql server
* docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=P@ssw0rd -e MSSQL_PID=Developer -p 1433:1433 -e MSSQL_AGENT_ENABLED=true -d mcr.microsoft.com/mssql/server:2019-latest 

Comando para subir o banco na porta default 1433 com a senha P@sswo0rd para o usuário 'SA'

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

___

```
No exemplo foi utilizado o ip: 192.168.110.241 que é o ip da maquina linux que subiu nos exemplos, altere para o seu ip
```

[Referência ao uso do debezium e kafka](https://medium.com/jundevelopers/debezium-kafka-e-net-core-9cee3ca3e0db)