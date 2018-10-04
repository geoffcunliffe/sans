FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore ./api/Sans.CreditUnion.API.csproj

# Build
RUN dotnet publish -c Release -o /app/www ./api/Sans.CreditUnion.API.csproj

# Copy runtime cert
COPY ./api/SansCredit.pfx /app/www

# OLD CODE - REMOVE ME LATER
# AWS_ACCESS_KEY_ID=AKIAJOZ2BJ2FOYJGLPYQ
# AWS_SECRET_ACCESS_KEY=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

# Build runtime image
FROM microsoft/dotnet:2.1-sdk
WORKDIR /www
COPY --from=build-env /app/www .
ENTRYPOINT ["dotnet", "Sans.CreditUnion.API.dll"]