# Exercise 3: Acceptance

## Dynamic Security Testing

Automating Dynamic Application Security Testing (DAST) is challenging in DevOps environments. The tooling is always improving; however spidering, active scanning, and reporting were not features originally designed to be invoked from a command line against a phoenix server (ephemeral app). The main challenges in this topic area include authenticating and spidering authenticated pages, testing APIs over standard GET / POST web forms, and converting those results into true / false test cases.

Your mission is to configure a ZAP Baseline scan against the Credit Union API Acceptance environment.

- In Jenkins, open the **ZAP Baseline Scan** job in the Acceptance phase.

- View the job configuration and locate the first (and only) build step that is responsible for executing the ZAP baseline scan.

- The shell commands in the build step take care of finding the acceptance container that is currently running and extracting the container's IP address from the Docker daemon.

    ```bash
    # Container details for the acceptance contianer
    REPOSITORY_URI="creditunion-api-acceptance"
    TAG=${PIPELINE_VERSION}

    # Get the container id for production
    CONTAINER_ID=$(docker ps | grep $REPOSITORY_URI:$TAG | awk '{print $1}')

    # Find the IP address we're going to smoke test
    IP_ADDRESS=$(docker inspect $CONTAINER_ID | jq -r '.[].NetworkSettings.Networks[].IPAddress')

    # Kick off the zap baseline scan
    #<INSERT DOCKER RUN COMMAND TO START ZAP HERE>
    
    # Clean up old images
    docker container prune -f
    ```

- Update the build step replacing the comment to actually run the docker command to kick off the ZAP baseline scan. Here are some details that you'll need to start the command:

    - The ZAP docker image requires the scan to be run as root (uid 0)

    - Mount host's ${WORKSPACE}/report directory to the /zap/wrk directory inside the container

    - The name of the image is owasp/zap2docker-stable

    - Execute the zap-baseline.py script in the starting directory

        - ZAP's target should be the acceptance container's endpoint: https://$IP_ADDRESS:44300

        - Export the report to a file called baseline.html

        - Export the json formatted report to a file called baseline.json

    - To ensure the step always passes (instead of failing the build), put a `|| true` at the end of the command.

    - Examples and details can be found on the [ZAP Baseline Wiki](https://github.com/zaproxy/zaproxy/wiki/ZAP-Baseline-Scan)

- If you get stuck, view the [Solution File](./solutions/3_acceptance_dast_sln.md)

- Kick off your pipeline. Did any baseline test cases fail?