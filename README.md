# Kafka Testing
This project demonstrates how to set up Apache Kafka on Windows and how to use C# code in terms of generating and consuming messages.

# Environment

- Windows 10
- Visual Studio 2019
- Install Docker Desktop from https://hub.docker.com/?overlay=onboarding
- Once Docker Desktop is installed, open two Powershell console windows.

- In PS console 1, run the following:

```sh
docker network create kafka-net --driver bridge
docker run --name zookeeper-server -p 2181:2181 --network kafka-net -e ALLOW_ANONYMOUS_LOGIN=yes bitnami/zookeeper:latest
```

- In PS console 2, run the following:
```sh
docker run --name kafka-server1 --network kafka-net -e ALLOW_PLAINTEXT_LISTENER=yes -e KAFKA_CFG_ZOOKEEPER_CONNECT=zookeeper-server:2181 -e KAFKA_CFG_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 -p 9092:9092 bitnami/kafka:latest
```

> You should now have ZooKeeper and the Kafka broker up and running as per https://itnext.io/how-to-install-kafka-using-docker-a2b7c746cbdc

# Code



# Sources
- https://kafka.apache.org/quickstart
- https://nielsberglund.com/2019/06/18/confluent-platform--kafka-for-a-.net-developer-on-windows/
- http://www.kafkatool.com/features.html