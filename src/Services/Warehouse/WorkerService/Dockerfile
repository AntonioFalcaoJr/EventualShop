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

COPY ./src/Services/Warehouse/Application/*.csproj ./Services/Warehouse/Application/
COPY ./src/Services/Warehouse/Domain/*.csproj ./Services/Warehouse/Domain/
COPY ./src/Services/Warehouse/Infrastructure.EventStore/*.csproj ./Services/Warehouse/Infrastructure.EventStore/
COPY ./src/Services/Warehouse/Infrastructure.MessageBus/*.csproj ./Services/Warehouse/Infrastructure.MessageBus/
COPY ./src/Services/Warehouse/Infrastructure.Projections/*.csproj ./Services/Warehouse/Infrastructure.Projections/
COPY ./src/Services/Warehouse/WorkerService/*.csproj ./Services/Warehouse/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Warehouse/WorkerService

COPY ./src/Services/Warehouse/Application/. ./Services/Warehouse/Application/
COPY ./src/Services/Warehouse/Domain/. ./Services/Warehouse/Domain/
COPY ./src/Services/Warehouse/Infrastructure.EventStore/. ./Services/Warehouse/Infrastructure.EventStore/
COPY ./src/Services/Warehouse/Infrastructure.MessageBus/. ./Services/Warehouse/Infrastructure.MessageBus/
COPY ./src/Services/Warehouse/Infrastructure.Projections/. ./Services/Warehouse/Infrastructure.Projections/
COPY ./src/Services/Warehouse/WorkerService/. ./Services/Warehouse/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Warehouse/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]