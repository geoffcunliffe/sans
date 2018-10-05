# Exercise 4: Linux Hardening Guidelines Solution

Configuring the InSpec Linux baseline container scan requires the following command:

```bash
#!/bin/bash +x

# Container details for the acceptance contianer
REPOSITORY_URI="creditunion-api-acceptance"
TAG=${PIPELINE_VERSION}

# Get the container id for production
CONTAINER_ID=$(docker ps | grep $REPOSITORY_URI:$TAG | awk '{print $1}')

inspec exec https://github.com/dev-sec/linux-baseline -t docker://$CONTAINER_ID --reporter json:inspec.json html:inspec.html junit:inspec.xml || true
```