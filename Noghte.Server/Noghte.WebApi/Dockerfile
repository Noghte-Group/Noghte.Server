FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk

WORKDIR /app

COPY Noghte.WebApi/Noghte.WebApi.csproj Noghte.WebApi/
COPY Noghte.Application/Noghte.Application.csproj Noghte.Application/
COPY Noghte.BuildingBlock/Noghte.BuildingBlock.csproj Noghte.BuildingBlock/
COPY Noghte.Domain/Noghte.Domain.csproj Noghte.Domain/
COPY Noghte.Infrastructure/Noghte.Infrastructure.csproj Noghte.Infrastructure/

RUN dotnet restore "Noghte.WebApi/Noghte.WebApi.csproj"
COPY . .

RUN dotnet build "Noghte.WebApi/Noghte.WebApi.csproj" -c Release -o /app/build

RUN dotnet publish "Noghte.WebApi/Noghte.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=sdk /app/publish .
ENTRYPOINT ["dotnet","Noghte.WebApi.dll"]