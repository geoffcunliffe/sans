#!/bin/bash

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

docker rmi -f $2:$3 || true