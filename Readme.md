# Debezium connector for MongoDB

Debezium’s MongoDB connector tracks a MongoDB replica set or a MongoDB sharded cluster for document changes in databases and collections, recording those changes as events in Kafka topics. The connector automatically handles the addition or removal of shards in a sharded cluster, changes in membership of each replica set, elections within each replica set, and awaiting the resolution of communications problems.

## How to start

You can run docker-compose with `register.sh` file.The file use `docker-compose-mongodb.yaml` and `register-mongodb.json` files.

```sh
cd debezium-mongodb-starter/
./register.sh
```

### Insert document to MongoDB and Test Scenario

You can run commands with `docker-compose-mongodb.yaml` for insert sample document to mongodb container:

```sh
cd debezium-mongodb-starter/

docker-compose -f docker-compose-mongodb.yaml exec mongodb bash -c 'mongo -u debezium -p dbz --authenticationDatabase admin inventory'
```

After that, you can execute insert query with Immediate CLI.

```sh
db.customers.insert([
    { _id : NumberLong("1006"), first_name : 'Kenan', last_name : 'Başdemir', email : 'test@', unique_id : UUID() }
]);
```

### Shut down the cluster

```sh
docker-compose -f docker-compose-mongodb.yaml down
```

---

## How to monitor Kafka

You can access from <http://localhost:9000/> web address to web interface.

> I used kafdrop for monitoring tool (see: github.com/obsidiandynamics/kafdrop)

---

## How to consume Debezium

You can run sample .Net Core Consumer Project.

```sh
cd KafkaConsumerWorker/
dotnet run
```

To see examples of consumers written in various languages, refer to the specific language sections. For additional examples, including usage of Confluent Cloud, refer to [Code Examples for Apache Kafka®.](https://docs.confluent.io/platform/current/tutorials/examples/clients/docs/clients-all-examples.html#clients-all-examples)

---

## Port Table

Container | Ports            | Description / Link |
----------|------------------|-------------|
zookeeper | 2181, 2888, 3888 | zookeeper.apache.org/
kafka     | 9092, 9094       | kafka.apache.org/
mongodb   | 27017            | docs.mongodb.com/
connect   | 8083             | kafka.apache.org/documentation.html#connect
kafdrop   | 9000             | github.com/obsidiandynamics/kafdrop#readme

---

## References / Links

debezium.io/documentation/reference/connectors/mongodb.html

docs.confluent.io/platform/current/tutorials/examples/clients/docs/clients-all-examples.html#clients-all-examples

## License

[MIT](https://choosealicense.com/licenses/mit/)


## Authors

- [@kenanbasdemir](https://www.github.com/kenanbasdemir)

