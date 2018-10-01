# Exercise 0: Getting Started

To complete your environment configuration and move forward with the workshop, you need to do the following:

## 1) Push Files To Your Personal Repository

The git repository you just cloned will not be available after the workshop ends. To ensure you have access to the files afterwards, start by pushing the contents of this repository to your GitHub or Gitlab account.

To do this, run the following commands in your local repository:

```bash
<add git commands>
```

## 2) Launch Jenkins Instance

### Jenkins Image

```
cd /opt/bitnami
mkdir .gradle
chown -R tomcat:tomcat .gradle
usermod -a -G docker bitnami
usermod -a -G docker tomcat
/opt/bitnami/ctlscript.sh restart
```
**Mac / Linux**

```bash
cd ~/workshop/devsecops-workshop/docker/jenkins
docker build -t devsecops/zap:latest .
```

**ZAP Image**

```bash
docker pull owasp/zap2docker-stable
```

**Windows Powershell**

```ps

```

```ps

```

## 3) Launch Jenkins

To spin up the Jenkins container, run the following commands:

```bash

docker run -d -p 8080:8080 -u $(id -g) -v /var/run/docker.sock:/var/run/docker.sock:z -v $(pwd)/jenkins_home:/var/jenkins_home devsecops/jenkins:latest
```

```ps
Insert powershell commands here
```

Open a browser and navigate to [http://localhost:8080/](http://localhost:8080/)

Login creds:
**UID**: devsecops
**PWD**: devsecops