#!/bin/bash

#Spin up temp mysql container
docker pull mysql:latest
docker run --name creditunion-db-$1 -e MYSQL_ROOT_PASSWORD='dev$3c0psForTheWin' -e MYSQL_DATABASE='CreditUnion' -e MYSQL_USER='credituser' -e MYSQL_PASSWORD='dev$3c0psForTheWin' -d mysql:latest

CONTAINER_ID=$(docker ps | grep "creditunion-db-$1" | awk '{print $1}')
IP_ADDRESS=$(docker inspect $CONTAINER_ID | jq -r '.[].NetworkSettings.Networks[].IPAddress')

# UPDATE APPSETTINGS JSON WITH IP ADDRESS TO CONNECT TO MYSQL
cd src/app/
sed -i -e "s#Server=localhost;#Server=$IP_ADDRESS;#g" api/appsettings.json

# Build the testing container
docker build --tag $2:$3 .

# Race condition it appears before SQL is ready to roll
sleep 30

# Run web api container
if [[ "acceptance" == "$1" ]]; then
    PORT=44301
else
    PORT=44300
fi
docker run --name creditunion-api-$1 -p $PORT:44300 --link creditunion-db-$1 -d $2:$3