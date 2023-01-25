ARG ASPNET_VERSION="7.0.2"
ARG SDK_VERSION="7.0.102"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Services/Communication/Command/Application/*.csproj ./Services/Communication/Command/Application/
COPY ./src/Services/Communication/Command/Domain/*.csproj ./Services/Communication/Command/Domain/
COPY ./src/Services/Communication/Command/Infrastructure.EventStore/*.csproj ./Services/Communication/Command/Infrastructure.EventStore/
COPY ./src/Services/Communication/Command/Infrastructure.MessageBus/*.csproj ./Services/Communication/Command/Infrastructure.MessageBus/
COPY ./src/Services/Communication/Command/Infrastructure.SMTP/*.csproj ./Services/Communication/Command/Infrastructure.SMTP/
COPY ./src/Services/Communication/Command/WorkerService/*.csproj ./Services/Communication/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Communication/Command/WorkerService

COPY ./src/Services/Communication/Command/Application/. ./Services/Communication/Command/Application/
COPY ./src/Services/Communication/Command/Domain/. ./Services/Communication/Command/Domain/
COPY ./src/Services/Communication/Command/Infrastructure.EventStore/. ./Services/Communication/Command/Infrastructure.EventStore/
COPY ./src/Services/Communication/Command/Infrastructure.MessageBus/. ./Services/Communication/Command/Infrastructure.MessageBus/
COPY ./src/Services/Communication/Command/Infrastructure.SMTP/. ./Services/Communication/Command/Infrastructure.SMTP/
COPY ./src/Services/Communication/Command/WorkerService/. ./Services/Communication/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Communication/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]