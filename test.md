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
