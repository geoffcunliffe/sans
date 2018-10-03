## Executing Tests

This project uses .NET Core XUNIT for unit testing. To execute the unit tests for a given namespace, run the following commands:

```cs
dotnet xunit -namespace Sans.CreditUnion.API.Tests.FeaturesTests -xml xunit.xml
```

Build and running tests inside a docker container:

```
docker build -t api/tests:1.0 -f Dockerfile.xunit .
docker run --rm -v $(pwd)/wip:/results api/tests:1.0
```