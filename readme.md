# SimpleLoan.API

## Database
`SQLite` is used for simplicity reasons.  

### Migrations
To create migrations you need to run following commands:

> dotnet tool install --global dotnet-ef

> dotnet ef migrations add 'initial' --startup-project ./SimpleLoan.API/SimpleLoan.API.csproj --project ./SimpleLoan.Infrastructure/SimpleLoan.Infrastructure.csproj --context PaymentDbContext --output-dir Persistence/Migrations