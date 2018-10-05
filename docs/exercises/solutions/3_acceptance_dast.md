# Exercise 3: Dynamic Security Testing Solution

Configuring the ZAP Baseline command requires the following execute shell command in the build step:

```bash
#!/bin/bash +x

# Container details for the acceptance contianer
REPOSITORY_URI="creditunion-api-acceptance"
TAG=${PIPELINE_VERSION}

# Get the container id for production
CONTAINER_ID=$(docker ps | grep $REPOSITORY_URI:$TAG | awk '{print $1}')

# Find the IP address we're going to smoke test
IP_ADDRESS=$(docker inspect $CONTAINER_ID | jq -r '.[].NetworkSettings.Networks[].IPAddress')

# Kick off the zap baseline scan
docker run --user 0 -v ${WORKSPACE}/report:/zap/wrk/:rw -t owasp/zap2docker-stable zap-baseline.py -t https://$IP_ADDRESS:44300 -r baseline.html -J baseline.json || true

# Clean up old images
docker container prune -f
```