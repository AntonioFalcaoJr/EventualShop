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

COPY ./src/Services/Identity/Command/Application/*.csproj ./Services/Identity/Command/Application/
COPY ./src/Services/Identity/Command/Domain/*.csproj ./Services/Identity/Command/Domain/
COPY ./src/Services/Identity/Command/Infrastructure.EventStore/*.csproj ./Services/Identity/Command/Infrastructure.EventStore/
COPY ./src/Services/Identity/Command/Infrastructure.MessageBus/*.csproj ./Services/Identity/Command/Infrastructure.MessageBus/
COPY ./src/Services/Identity/Command/WorkerService/*.csproj ./Services/Identity/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Identity/Command/WorkerService

COPY ./src/Services/Identity/Command/Application/. ./Services/Identity/Command/Application/
COPY ./src/Services/Identity/Command/Domain/. ./Services/Identity/Command/Domain/
COPY ./src/Services/Identity/Command/Infrastructure.EventStore/. ./Services/Identity/Command/Infrastructure.EventStore/
COPY ./src/Services/Identity/Command/Infrastructure.MessageBus/. ./Services/Identity/Command/Infrastructure.MessageBus/
COPY ./src/Services/Identity/Command/WorkerService/. ./Services/Identity/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Identity/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]