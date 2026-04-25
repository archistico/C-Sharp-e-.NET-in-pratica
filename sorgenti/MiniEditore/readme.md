dotnet clean
dotnet restore
dotnet build
dotnet test

dotnet run --project src/App.Api --urls http://localhost:5001
dotnet run --project src/App.Web --urls http://localhost:5002

http://localhost:5001/
http://localhost:5001/health/live
http://localhost:5001/api/catalogo/libri