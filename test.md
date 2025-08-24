Database Test: 
psql -h localhost -p 5432 -U demo -d ordersdb
\l
\c ordersdb
\dt
SELECT * FROM "Orders";
\d "Orders"



Application URL: http://orders-api-demo-test.apps.cluster-94bnb.94bnb.sandbox1543.opentlc.com/api/orders

Create a new order (POST)
curl -X POST \
  -H "Content-Type: application/json" \
  -d '{"name": "Order 3"}' \
  http://orders-api-demo-test.apps.cluster-94bnb.94bnb.sandbox1543.opentlc.com/api/orders


List All Orders:
curl -X GET http://orders-api-demo-test.apps.cluster-94bnb.94bnb.sandbox1543.opentlc.com/api/orders

Update an order (PUT)
curl -X PUT \
  -H "Content-Type: application/json" \
  -d '{"id": 1, "name": "Updated Order 1"}' \
  http://orders-api-demo-test.apps.cluster-94bnb.94bnb.sandbox1543.opentlc.com/api/orders/1

Delete an order (DELETE)
curl -X DELETE http://orders-api-demo-test.apps.cluster-94bnb.94bnb.sandbox1543.opentlc.com/api/orders/2



### 1. Prepare PostgreSQL for Debezium 
vi /var/lib/pgsql/data/userdata/postgresql.conf
  wal_level = logical
  max_wal_senders = 10
  max_replication_slots = 10

psql -h localhost -p 5432 -U demo -d ordersdb
SHOW wal_level;
SHOW max_wal_senders;
SHOW max_replication_slots;

### Create a Replication User
psql -U postgres
CREATE USER debezium WITH PASSWORD 'debeziumpass' REPLICATION;
GRANT CONNECT ON DATABASE ordersdb TO debezium;
GRANT USAGE ON SCHEMA public TO debezium;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO debezium;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO debezium;
ALTER ROLE demo WITH REPLICATION;
CREATE PUBLICATION orders_pub FOR TABLE public."Orders";
CREATE PUBLICATION orders_pub FOR ALL TABLES;
ALTER ROLE demo WITH REPLICATION;


create connector

Test in kafka pod:

./bin/kafka-topics.sh --bootstrap-server my-cluster-kafka-bootstrap:9092 --list           


Workaround:
psql -U postgres -d ordersdb
DROP PUBLICATION IF EXISTS orders_pub;
CREATE PUBLICATION orders_pub FOR TABLE public."Orders";
GRANT SELECT ON public."Orders" TO demo;
ALTER ROLE demo WITH REPLICATION;

\dt public.*