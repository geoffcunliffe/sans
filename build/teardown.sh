#!/bin/bash

## Old dbs
CONTAINER_ID=$(docker ps -a | grep "mysql:latest" | awk '{print $1}')

## Clean up the db acceptance
CONTAINER_ID=$(docker ps -a | grep "creditunion-db-$1" | awk '{print $1}')

if [[ -z "$CONTAINER_ID" ]]; then
	echo "No db acceptance container found."
else
	docker stop $CONTAINER_ID || true
	docker rm $CONTAINER_ID || true
fi

CONTAINER_ID=$(docker ps -a | grep "creditunion-api-$1" | awk '{print $1}')

if [[ -z "$CONTAINER_ID" ]]; then
	echo "No api acceptance container found."
else
	docker stop $CONTAINER_ID || true
	docker rm $CONTAINER_ID || true
fi

# Extra cleanup
docker container prune -f

# Delete image if still there
docker rmi -f $2:$3 || true
