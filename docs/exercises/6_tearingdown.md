# Workshop Teardown

Make sure you do the following before you forget about the cloud resources you are using:

- In the AWS Console, browse to the **Elastic Container Service**

    - Browse into the **creditunion-api** repository

    - Select the checkbox next to **Image tags** to select ALL of the images created during the workshop

    - Press the **Delete** button

- In the AWS Console, browse to the **CloudFormation** service

    - Check the box next to the **DevSecOps-Workshop**

    - Use the Actions > Delete Stack menu item to remove the stack from your account

    - Wait for the stack deletion to complete

- In the AWS Console, browser to the CloudWatch service

    - Select the Dashboards link, open your dashboard, and use the Actions > Delete Dashboard link to remove the graph

    - Select the Logs link and delete the **creditunion-api** log group