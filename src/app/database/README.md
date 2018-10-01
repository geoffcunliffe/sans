# Database Layer

## Overview

Provides the API for modifying all database objects through rest calls.

- Account Management (Profile, pwd, questions / answers, mfa, etc.)
- Bill Pay (add payee, make a payment to a payee)
- Account Portal (Checking / savings types, ledger, monthly statements)
- Brokerage (Basic portfolio w/ holdings, # of shares, buy, sell)

## Database Updates

Entity Framework migrations are enabled to allow database as code to modify the database as the application starts up.

dotnet ef migrations add MigrationNameHere
dotnet ef database update

## Runtime

The datalayer and API wrapper run in a Docker container to allow the application to scale up for heavy call volumes.

## To run
- Add a User Secret to the connection string.  The secrets.json should look like:
{
  "ConnectionStrings": {
    "DefaultConnection": "server=SERVERNAMEHERE;port=3306;database=SANSCreditUnion;user=USERNAMEHERE;password=PASSWORDHERE"
  }
}
