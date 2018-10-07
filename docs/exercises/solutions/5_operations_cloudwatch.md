# Exercise 5: Docker CloudWatch Logging

Configuring the docker run statement to send logs to CloudWatch requires the following command:

```
docker run --name creditunion-api-$1 -p $PORT:44300 --link creditunion-db-$1 -d --log-driver=awslogs --log-opt awslogs-region=${AWS_DEFAULT_REGION} --log-opt awslogs-group=creditunion-api --log-opt awslogs-create-group=true $2:$3
```