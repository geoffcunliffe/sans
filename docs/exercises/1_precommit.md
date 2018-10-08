# Exercise 1: Pre-Commit

## IDE Security Plugins

Find and install a code editor that supports your code editor. Some examples are DevSkim, SonarLint, and Puma Scan for the code that exists in this repository.

- Visit the [DevSkim](https://github.com/Microsoft/DevSkim) project and install the plugin for your editor (supports VS Code, Sublime, Visual Studio). If applicable, otherwise, skip to the next part.

- Open the **~/tests/app/SecurityTests/HighRiskCodeTest.cs** file. Do you see a DevSkim warning anywhere?

- In the code editor, go to the Settings and follow the steps below to suppress the warning:

    - Find the DevSkim Ingore Files List option

    - Update the setting to ignore code in the **tests/app/SecurityTests/HighRiskCodeTest.cs** file

- Did the warning go away?

## Pre-Commit Hooks

Install and configure the git-secrets pre-commit hook and scan the repository for vulnerabilities.

- Visit the [git-secrets](https://github.com/awslabs/git-secrets) repository for install instructions for your OS.

- Change your directory to the workshop directory.

    ```bash
    cd ~/workshop
    ```

- Install git-secrets and register the AWS provider for your repository

    ```bash
    git secrets --install
    git secrets --register-aws
    ```

- Scan the repository for any problems that will be caught on the next commit. Do you see any findings that are concerning?

    ```
    git secrets --scan
    ```

- At this point, you have two options:

    - Remove the dead code from the Dockerfile

    - Create a suppression file (.gitallowed) in the root of the repository and add the allowed patterns to the file.

- Run the scan again and ensure no results have been found.

- You can commit and push your code changes to the AWS repository if you'd like double check everything is working.

    ```bash
    git push aws master
    ```