# Exercise 4: Production

## Security Configuration & Server Hardening

InSpec is a powerful security configuration compliance and auditing tool from Chef. Combined with the open source InSpec hardening profiles put together by the DevSec team, DevSecOps teams have the capability to perform fast compliance scanning before moving to production.

## Linux Hardening Guidelines

Your mission is to scan the Credit Union container against the Linux hardening guidelines and report the results as jUnit test results.

- In Jenkins, open the **InSepc - Credit Union Container** task.

- View the job configuration and locate the first (and only) build step that is responsible for executing the InSpec scan.

- The shell commands in the build step take care of finding the acceptance container that is currently running and extracting the container's IP address from the Docker daemon.

    ```bash
    #!/bin/bash +x

    # Container details for the acceptance container
    REPOSITORY_URI="creditunion-api-acceptance"
    TAG=${PIPELINE_VERSION}

    # Get the container id for production
    CONTAINER_ID=$(docker ps | grep $REPOSITORY_URI:$TAG | awk '{print $1}')

    # <ENTER INSPEC COMMAND TO SCAN THE LINUX CONTAINER>
    ```

- Update the build step replacing the comment to actually run the InSpec command using the DevSec Linux Baseline profile. Here are some details that you'll need to start the command:

    - Use the DevSec profile located at https://github.com/dev-sec/linux-baseline

    - Set the target switch to the docker://$CONTAINER_ID value

    - Set the reporter switch to the following:

        - json:inspec.json

        - html:inspec.html

        - junit:inspec.xml

    - To ensure the step always passes (instead of failing the build), put a `|| true` at the end of the command.

    - Examples and details can be found on the [DevSec Linux Baseline Wiki](https://github.com/dev-sec/linux-baseline)

- If you get stuck, view the [Solution File](./solutions/4_production_inspec_container.md)

- How many failures occur?

## CIS Docker Benchmark Guidelines

You mission is to scan the actual Dockerhost against the CIS Docker Benchmark guidelines and report the results as jUnit test results.

- In Jenkins, open the **InSpec - Docker CIS Benchmark** task.

- View the job configuration and locate the first (and only) build step that is responsible for executing the InSpec scan.

- The shell commands in the build step are much more simple that the previous step because we don't need to look up the container id. This step is scanning the actual host machine and the job is already configured to run against the master agent.

    ```bash
    # <ENTER INSPEC COMMAND TO SCAN THE DOCKER HOST>
    ```

- Update the build step replacing the comment to actually run the InSpec command to kick off the InSpec using the DevSec CIS Docker benchmark profile. Here are some details that you'll need to start the command:

    - Use the DevSec profile located at https://github.com/dev-sec/cis-docker-benchmark

    - Set the reporter switch to the following:

        - json:inspec.json

        - html:inspec.html

        - junit:inspec.xml

    - To ensure the step always passes (instead of failing the build), put a `|| true` at the end of the command.

    - Examples and details can be found on the [DevSec Linux Baseline Wiki](https://github.com/dev-sec/linux-baseline)

- If you get stuck, view the [Solution File](./solutions/4_production_inspec_docker.md)

- How many failures occur?