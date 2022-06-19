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

COPY ./src/Services/Account/Application/*.csproj ./Services/Account/Application/
COPY ./src/Services/Account/Domain/*.csproj ./Services/Account/Domain/
COPY ./src/Services/Account/Infrastructure.EventStore/*.csproj ./Services/Account/Infrastructure.EventStore/
COPY ./src/Services/Account/Infrastructure.MessageBus/*.csproj ./Services/Account/Infrastructure.MessageBus/
COPY ./src/Services/Account/Infrastructure.Projections/*.csproj ./Services/Account/Infrastructure.Projections/
COPY ./src/Services/Account/WorkerService/*.csproj ./Services/Account/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Account/WorkerService

COPY ./src/Services/Account/Application/. ./Services/Account/Application/
COPY ./src/Services/Account/Domain/. ./Services/Account/Domain/
COPY ./src/Services/Account/Infrastructure.EventStore/. ./Services/Account/Infrastructure.EventStore/
COPY ./src/Services/Account/Infrastructure.MessageBus/. ./Services/Account/Infrastructure.MessageBus/
COPY ./src/Services/Account/Infrastructure.Projections/. ./Services/Account/Infrastructure.Projections/
COPY ./src/Services/Account/WorkerService/. ./Services/Account/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Account/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]