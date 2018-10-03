#!/bin/bash

#Spin up temp mysql container
docker pull mysql:latest
docker run --name creditunion-db-acceptance -e MYSQL_ROOT_PASSWORD='dev$3c0psForTheWin' -e MYSQL_DATABASE='CreditUnion' -e MYSQL_USER='credituser' -e MYSQL_PASSWORD='dev$3c0psForTheWin' -d mysql:latest

CONTAINER_ID=$(docker ps | grep "creditunion-db-acceptance" | awk '{print $1}')
IP_ADDRESS=$(docker inspect $CONTAINER_ID | jq -r '.[].NetworkSettings.Networks[].IPAddress')

# UPDATE APPSETTINGS JSON WITH IP ADDRESS TO CONNECT TO MYSQL
cd src/app/
sed -i -e "s#Server=localhost;#Server=$IP_ADDRESS;#g" api/appsettings.json

# Build the testing container
docker build --tag $1:$2 .

# Run web api container
docker run --name creditunion-api-acceptance -p 44300:44300 --link creditunion-db-acceptance $1:$2