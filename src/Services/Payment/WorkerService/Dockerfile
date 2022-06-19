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

COPY ./src/Services/Payment/Application/*.csproj ./Services/Payment/Application/
COPY ./src/Services/Payment/Domain/*.csproj ./Services/Payment/Domain/
COPY ./src/Services/Payment/Infrastructure.EventStore/*.csproj ./Services/Payment/Infrastructure.EventStore/
COPY ./src/Services/Payment/Infrastructure.HttpClients/*.csproj ./Services/Payment/Infrastructure.HttpClients/
COPY ./src/Services/Payment/Infrastructure.MessageBus/*.csproj ./Services/Payment/Infrastructure.MessageBus/
COPY ./src/Services/Payment/Infrastructure.Projections/*.csproj ./Services/Payment/Infrastructure.Projections/
COPY ./src/Services/Payment/WorkerService/*.csproj ./Services/Payment/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Payment/WorkerService

COPY ./src/Services/Payment/Application/. ./Services/Payment/Application/
COPY ./src/Services/Payment/Domain/. ./Services/Payment/Domain/
COPY ./src/Services/Payment/Infrastructure.EventStore/. ./Services/Payment/Infrastructure.EventStore/
COPY ./src/Services/Payment/Infrastructure.HttpClients/. ./Services/Payment/Infrastructure.HttpClients/
COPY ./src/Services/Payment/Infrastructure.MessageBus/. ./Services/Payment/Infrastructure.MessageBus/
COPY ./src/Services/Payment/Infrastructure.Projections/. ./Services/Payment/Infrastructure.Projections/
COPY ./src/Services/Payment/WorkerService/. ./Services/Payment/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Payment/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]