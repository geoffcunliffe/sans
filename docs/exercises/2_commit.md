# Exercise 2: Commit

Plenty of verification needs to be done in the commit phase to secure DevOps build artifacts. Let's take a look at some of the security controls.

## Unit Testing

Unit testing is critical for DevSecOps and making up for false negatives that occur with automated scanners. You'll notice in your Credit Union build pipeline that there is a **Unit Testing** step in the build phase. 

- Kick off your pipeline and observe that there are currently 9 passing test cases.

- To view the test cases, open the various classes located in the **tests/app/FeaturesTests** directory:

    - One example found in the **~/tests/app/FeaturesTests/AccountTests/AccountControllerGet.cs** class has the following:

    ```cs
    [Fact]
    public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
    {
        // Act
        var result = await Client.SendAccountGetRequestAsync();

        // Assert
        result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }
    ```

    - Notice that this test case attempts to request account details without an authenticated user. This is a perfect security test that is built into the standard unit tests.

- Your task is to add a new unit test that looks for changes to high risk code.

    - View the Unit Test job configure in Jenkins and find the command that executes the unit test methods in the application.

    - Open the **~/tests/app/SecurityTests/HighRiskCodeTest.cs** class that you saw flagged earlier by the DevSkim code checker.

    - Why is this unit test not current being invoked by the build pipeline?

        > HINT: Search the codebase for the *xunit* command

    - Fix the bug in the code to allow ALL unit tests to run and push the code changes to aws.

        ```bash
        git add *
        git commit -m "Fixed unit test for security bug"
        git push aws master
        ```

- In Jenkins, start the pipeline again and let the unit test task run.

- Notice the new High Risk Code test case is failing now? Generate the current SHA1 hash value for the **AuthentiationController.cs** class:

    ```bash
    openssl sha1 src/app/api/Features/Authentication/AuthenticationController.cs
    ```

- In the **HighRiskCodeTest.cs** class, update the checksum for the AuthenticationController.cs test case.

- Fix the bug in the code to allow ALL unit tests to run and push the code changes to aws.

    ```bash
    git add *
    git commit -m "Updated checksum for AuthenticationController.cs"
    git push aws master
    ```

- Kick off the Jenkins pipeline again to see if the new test case passes.

## Container Security



## Dependency Management