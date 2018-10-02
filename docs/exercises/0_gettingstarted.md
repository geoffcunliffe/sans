# Exercise 0: Getting Started

To get started, let's get your environment created and configured so we can hop in and build security for the rest of the workshop.

## 1) Launch the DevSecOps Workshop CloudFormation Stack

- Sign in to the AWS Console

- In the Services menu, enter **CloudFormation** and press enter.

- Press the button to **Create a new stack** and upload the master stack from the workshop files:

    ```
    ~/devsecops-workshop/infrastructure/master.yaml
    ```

    - Enter the name of the bucket given to you by the instructor

- The stack will take a few minutes to complete.

- When the stack completes, find the following output values:

    - **Jenkins Front End** should look similar to this URL: *https://<YOUR IP ADDRESS>/jenkins*.

    - Browse to the Jenkins front end. You will see a certificate warning because your workshop image is using a self signed certificate. This is not a production system, just for demos. Go ahead and accept the certificate warning and sign in using these credentials:

        - User Id: user

        - Password: dev$3c0psForTheWin

    - **BankCodeCommitRepository** should look similar to the following:

    ```
    ssh://git-codecommit.ap-southeast-2.amazonaws.com/v1/repos/credit-union
    ```

## 2) Configure CodeCommit Repository

- Run the sshkeygen command to create a new keypair for connecting to the new codecommit repository:

    ```bash
    ssh-keygen
    ```

    - Store the key in your home directory, such as `/Users/student/.ssh/devsecops

    - Choose a passphrase, such as **dev$3c0psForTheWin**

- In Jenkins, open the **Credentials** and update the **codecommit-privatekey** item:

    - In the Private Key field, paste your private key created above

    - In the Passphrase field, enter the passphrase you used above

- In the AWS Console, open the IAM service and upload your CodeCommit public key to your user account.

    - Choose **My Security Credentials**

    - Find the **SSH keys for AWS CodeCommit** section

    - Upload the public key created above

    - Note the value of the **SSH Key ID**

## 3) Push Files To Your CodeCommit Repository

- To push code to your CodeCommit repository, configure your SSH config file to connect using the appropriate user id. The following show's a new entry in the file:

    ```bash
    Host git-codecommit.*.amazonaws.com
    User <ENTER YOUR SSH KEY ID>
    IdentityFile ~/.ssh/devsecops
    AddKeysToAgent yes
    ```

- Change your directory to the devsecops-workshop directory.

    ```bash
    cd ~/devsecops-workshop
    ```

- Add a git remote pointing to the new CodeCommit repository.

    ```bash
    git remote add aws ssh://git-codecommit.ap-southeast-2.amazonaws.com/v1/repos/credit-union 
    ```

- Push the workshop contents to the CodeCommit repository.

    ```bash
    git push aws master
    ```

- When prompted, enter your key's passphrase. You should see a response similar to the following:

    ```bash
    Counting objects: 201, done.
    Delta compression using up to 8 threads.
    Compressing objects: 100% (188/188), done.
    Writing objects: 100% (201/201), 58.14 KiB | 2.08 MiB/s, done.
    Total 201 (delta 57), reused 3 (delta 0)
    To ssh://git-codecommit.ap-southeast-2.amazonaws.com/v1/repos/credit-union
    * [new branch]      master -> master
    ```

- If you'd prefer to read these instruction files in the browser, you can browse to the AWS CodeCommit service and view these markdown files inside the CodeCommit repository.

## 4) Connect Jenkins and CodeCommit

- Back in Jenkins, open the **Bank_Build** job and configure the **Source Code Management** section's git repository to use your **SSH Key ID** and git clone url. The format should be similar to the following:

    ```bash
    ssh://<ENTER YOUR SSH KEY ID>@git-codecommit.ap-southeast-2.amazonaws.com/v1/repos/credit-union
    ```

- Run the **Bank_Build** job to test your connection to the git repository. If all is well, your workspace will contain the devsecops-workshop files.