ARG ASPNET_VERSION="7.0.1"
ARG SDK_VERSION="7.0.101"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Services/Account/Command/Application/*.csproj ./Services/Account/Command/Application/
COPY ./src/Services/Account/Command/Domain/*.csproj ./Services/Account/Command/Domain/
COPY ./src/Services/Account/Command/Infrastructure.EventStore/*.csproj ./Services/Account/Command/Infrastructure.EventStore/
COPY ./src/Services/Account/Command/Infrastructure.MessageBus/*.csproj ./Services/Account/Command/Infrastructure.MessageBus/
COPY ./src/Services/Account/Command/WorkerService/*.csproj ./Services/Account/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Account/Command/WorkerService

COPY ./src/Services/Account/Command/Application/. ./Services/Account/Command/Application/
COPY ./src/Services/Account/Command/Domain/. ./Services/Account/Command/Domain/
COPY ./src/Services/Account/Command/Infrastructure.EventStore/. ./Services/Account/Command/Infrastructure.EventStore/
COPY ./src/Services/Account/Command/Infrastructure.MessageBus/. ./Services/Account/Command/Infrastructure.MessageBus/
COPY ./src/Services/Account/Command/WorkerService/. ./Services/Account/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Account/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]