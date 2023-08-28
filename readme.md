# SimpleLoan

## API
NET7 MinimalAPI / FastEndpoints

Dev
> dotnet dev-certs https --trust
> dotnet build ./SimpleLoan.API/SimpleLoan.sln

Test
> dotnet test ./SimpleLoan.API/SimpleLoan.UnitTests/SimpleLoan.UnitTestscsproj

Run
> dotnet run --project ./SimpleLoan.API/SimpleLoan.API/SimpleLoan.API.csproj
> https://localhost:7078/swagger/index.html

## UI
SvelteKit

> cd ./SimpleLoan.UI
> npm install
> npm run dev -- --open

## Database
`SQLite` is used for simplicity reasons.  

### Migrations
To create migrations you need to run following commands:

> dotnet tool install --global dotnet-ef

> dotnet ef migrations add 'initial' --startup-project ./SimpleLoan.API/SimpleLoan.API.csproj --project ./SimpleLoan.Infrastructure/SimpleLoan.Infrastructure.csproj --context PaymentDbContext --output-dir Persistence/Migrations