## Executing Tests

This project uses .NET Core XUNIT for unit testing. To execute the unit tests for a given namespace, run the following commands:

```cs
dotnet xunit -namespace Sans.CreditUnion.API.Tests.FeaturesTests -xml ~/Downloads/api-xunit.xml
```