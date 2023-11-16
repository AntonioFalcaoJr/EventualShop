ARG ASPNET_VERSION="8.0-preview"
ARG SDK_VERSION="8.0-preview"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Services/Catalog/Command/Application/*.csproj ./Services/Catalog/Command/Application/
COPY ./src/Services/Catalog/Command/Domain/*.csproj ./Services/Catalog/Command/Domain/
COPY ./src/Services/Catalog/Command/Infrastructure.EventStore/*.csproj ./Services/Catalog/Command/Infrastructure.EventStore/
COPY ./src/Services/Catalog/Command/Infrastructure.MessageBus/*.csproj ./Services/Catalog/Command/Infrastructure.MessageBus/
COPY ./src/Services/Catalog/Command/WorkerService/*.csproj ./Services/Catalog/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Catalog/Command/WorkerService

COPY ./src/Services/Catalog/Command/Application/. ./Services/Catalog/Command/Application/
COPY ./src/Services/Catalog/Command/Domain/. ./Services/Catalog/Command/Domain/
COPY ./src/Services/Catalog/Command/Infrastructure.EventStore/. ./Services/Catalog/Command/Infrastructure.EventStore/
COPY ./src/Services/Catalog/Command/Infrastructure.MessageBus/. ./Services/Catalog/Command/Infrastructure.MessageBus/
COPY ./src/Services/Catalog/Command/WorkerService/. ./Services/Catalog/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Catalog/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]