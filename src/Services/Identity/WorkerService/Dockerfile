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

COPY ./src/Services/Identity/Application/*.csproj ./Services/Identity/Application/
COPY ./src/Services/Identity/Domain/*.csproj ./Services/Identity/Domain/
COPY ./src/Services/Identity/Infrastructure.EventStore/*.csproj ./Services/Identity/Infrastructure.EventStore/
COPY ./src/Services/Identity/Infrastructure.MessageBus/*.csproj ./Services/Identity/Infrastructure.MessageBus/
COPY ./src/Services/Identity/Infrastructure.Projections/*.csproj ./Services/Identity/Infrastructure.Projections/
COPY ./src/Services/Identity/WorkerService/*.csproj ./Services/Identity/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Identity/WorkerService

COPY ./src/Services/Identity/Application/. ./Services/Identity/Application/
COPY ./src/Services/Identity/Domain/. ./Services/Identity/Domain/
COPY ./src/Services/Identity/Infrastructure.EventStore/. ./Services/Identity/Infrastructure.EventStore/
COPY ./src/Services/Identity/Infrastructure.MessageBus/. ./Services/Identity/Infrastructure.MessageBus/
COPY ./src/Services/Identity/Infrastructure.Projections/. ./Services/Identity/Infrastructure.Projections/
COPY ./src/Services/Identity/WorkerService/. ./Services/Identity/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Identity/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]