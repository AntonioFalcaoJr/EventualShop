ARG ASPNET_VERSION="7.0.0-preview.5-alpine3.16"
ARG SDK_VERSION="7.0.100-preview.5-1-alpine3.16"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Services/Catalog/Application/*.csproj ./Services/Catalog/Application/
COPY ./src/Services/Catalog/Domain/*.csproj ./Services/Catalog/Domain/
COPY ./src/Services/Catalog/Infrastructure.EventStore/*.csproj ./Services/Catalog/Infrastructure.EventStore/
COPY ./src/Services/Catalog/Infrastructure.MessageBus/*.csproj ./Services/Catalog/Infrastructure.MessageBus/
COPY ./src/Services/Catalog/Infrastructure.Projections/*.csproj ./Services/Catalog/Infrastructure.Projections/
COPY ./src/Services/Catalog/WorkerService/*.csproj ./Services/Catalog/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Catalog/WorkerService

COPY ./src/Services/Catalog/Application/. ./Services/Catalog/Application/
COPY ./src/Services/Catalog/Domain/. ./Services/Catalog/Domain/
COPY ./src/Services/Catalog/Infrastructure.EventStore/. ./Services/Catalog/Infrastructure.EventStore/
COPY ./src/Services/Catalog/Infrastructure.MessageBus/. ./Services/Catalog/Infrastructure.MessageBus/
COPY ./src/Services/Catalog/Infrastructure.Projections/. ./Services/Catalog/Infrastructure.Projections/
COPY ./src/Services/Catalog/WorkerService/. ./Services/Catalog/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Catalog/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]