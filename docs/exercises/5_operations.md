# Exercise 5: Operations

## Continuous Monitoring

Security in monitoring and active defense all starts with the DATA! Your mission is to enable CloudWatch monitoring of the API application. From ther

- Open the /build/launch.sh script and find the docker run command for the API container. It should look similar to the following:

    ```bash
    docker run --name creditunion-api-$1 -p 44300:44300 --link creditunion-db-$1 -d $2:$3
    ```

- Update the docker run command to enable the docker CloudWatch logs driver and write logging data into CloudWatch:

    ```
    docker run --name creditunion-api-$1 -p 44300:44300 --link creditunion-db-$1 -d --log-driver=awslogs --log-opt awslogs-region=${AWS_DEFAULT_REGION} --log-opt awslogs-group=creditunion-api --log-opt awslogs-create-group=true $2:$3
    ```

- Commit and push the logging patch to AWS.

    ```bash
    git add *
    git commit -m "Fixed unit test for security bug"
    git push aws master
    ```

- In Jenkins, start your build pipeline again to deploying the logging update.

- After the deployment completes, browse to the CloudWatch service in the AWS Web Console.

- View the log data in the **creditunion-api** log group

- Use the Swagger interface to login to the API:

    - Browse to your EC2 instance's IP address 

        ```
        https://YOUR_IP_ADDRESS:44300/swagger
        ```
    
    - Invoke the Authentication endpoint (/api/Authentication) using the following credentials:

        - **jsmith@gmail.com**

        - **Password123!** 

## Creating a CloudWatch Metric

- In the AWS Web Console, refresh the CloudWatch data. You should see some new log data similar to the following:

    ```
     Request starting HTTP/1.1 POST https://54.252.172.79:44300/api/Authentication application/json 63
    [40m[32minfo[39m[22m[49m: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[1]
    Route matched with {action = "Post", controller = "Authentication"}. Executing action Sans.CreditUnion.API.Features.Authentication.AuthenticationController.Post (Sans.CreditUnion.API)
    [40m[32minfo[39m[22m[49m: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[1]
    Executing action method Sans.CreditUnion.API.Features.Authentication.AuthenticationController.Post (Sans.CreditUnion.API) with arguments (Sans.CreditUnion.API.Features.Authentication.Models.AuthenticateRequest) - Validation state: Valid
    [40m[32minfo[39m[22m[49m: Microsoft.EntityFrameworkCore.Infrastructure[10403]
    Executed action method Sans.CreditUnion.API.Features.Authentication.AuthenticationController.Post (Sans.CreditUnion.API), returned result Microsoft.AspNetCore.Mvc.ObjectResult in 173.1781ms.
    [40m[32minfo[39m[22m[49m: Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor[1]
    Executing ObjectResult, writing value of type 'Sans.CreditUnion.API.Features.Authentication.Models.AuthenticateResult'.
    [40m[32minfo[39m[22m[49m: Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker[2]
    Executed action Sans.CreditUnion.API.Features.Authentication.AuthenticationController.Post (Sans.CreditUnion.API) in 230.7644ms
    [40m[32minfo[39m[22m[49m: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
    Request finished in 374.3266ms 200 application/json; charset=utf-8
    ```

- View the Log Groups list again and click on the **Metric Filters** row for the **creditunion-api** log group that reads **0 filters**.

- Press the button to **Add Metric Filter**

- Enter a filter pattern to locate requests the authentication controller's post method.

    ```
    Sans.CreditUnion.API.Features.Authentication.AuthenticationController.Post
    ```

- Press the **Assign Metric** button.

- Enter the following values on the **Create Metric Filter and Assign a Metric** screen:

    - Filter Name: CreditUnion-API-AuthenticationController

    - Metric Namespace: creditunion/api/authentication

    - Metric Name: authentication-attempt

    - Metric Value: 1

    - Default Value: 0

- Press the **Create Filter** button.

- Back in your web browser, submit a few more requests to the authentication endpoint to generate some data. Valid and invalid attempts are OK.

## CloudWatch Dashboard

- In the AWS Web Console, create a new CloudWatch dashboard using the new metric.

    - Press the **Dashboards** link in the left navigation menu

    - Press the **Create Dashboard** button and enter a name

    - Add a new line graph to the dashboard

    - Select the **creditunion/api/authentication** namespaces **authentication-attempt** metric.

    - Explore the Graphed Metrics tab and change the Statistic and Period values to see how that changes the dashboard.

## Exploring Further

- Create a CloudWatch alarm if the authentication value exceeds a threshold

- Write a serverless function to automate