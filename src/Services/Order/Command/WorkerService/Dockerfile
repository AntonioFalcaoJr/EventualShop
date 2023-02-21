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

COPY ./src/Services/Order/Command/Application/*.csproj ./Services/Order/Command/Application/
COPY ./src/Services/Order/Command/Domain/*.csproj ./Services/Order/Command/Domain/
COPY ./src/Services/Order/Command/Infrastructure.EventStore/*.csproj ./Services/Order/Command/Infrastructure.EventStore/
COPY ./src/Services/Order/Command/Infrastructure.MessageBus/*.csproj ./Services/Order/Command/Infrastructure.MessageBus/
COPY ./src/Services/Order/Command/WorkerService/*.csproj ./Services/Order/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Order/Command/WorkerService

COPY ./src/Services/Order/Command/Application/. ./Services/Order/Command/Application/
COPY ./src/Services/Order/Command/Domain/. ./Services/Order/Command/Domain/
COPY ./src/Services/Order/Command/Infrastructure.EventStore/. ./Services/Order/Command/Infrastructure.EventStore/
COPY ./src/Services/Order/Command/Infrastructure.MessageBus/. ./Services/Order/Command/Infrastructure.MessageBus/
COPY ./src/Services/Order/Command/WorkerService/. ./Services/Order/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Order/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]