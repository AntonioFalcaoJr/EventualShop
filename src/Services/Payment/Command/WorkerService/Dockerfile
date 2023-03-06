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

COPY ./src/Services/Payment/Command/Application/*.csproj ./Services/Payment/Command/Application/
COPY ./src/Services/Payment/Command/Domain/*.csproj ./Services/Payment/Command/Domain/
COPY ./src/Services/Payment/Command/Infrastructure.EventStore/*.csproj ./Services/Payment/Command/Infrastructure.EventStore/
COPY ./src/Services/Payment/Command/Infrastructure.MessageBus/*.csproj ./Services/Payment/Command/Infrastructure.MessageBus/
COPY ./src/Services/Payment/Command/Infrastructure.HTTP/*.csproj ./Services/Payment/Command/Infrastructure.HTTP/
COPY ./src/Services/Payment/Command/WorkerService/*.csproj ./Services/Payment/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Payment/Command/WorkerService

COPY ./src/Services/Payment/Command/Application/. ./Services/Payment/Command/Application/
COPY ./src/Services/Payment/Command/Domain/. ./Services/Payment/Command/Domain/
COPY ./src/Services/Payment/Command/Infrastructure.EventStore/. ./Services/Payment/Command/Infrastructure.EventStore/
COPY ./src/Services/Payment/Command/Infrastructure.MessageBus/. ./Services/Payment/Command/Infrastructure.MessageBus/
COPY ./src/Services/Payment/Command/Infrastructure.HTTP/. ./Services/Payment/Command/Infrastructure.HTTP/
COPY ./src/Services/Payment/Command/WorkerService/. ./Services/Payment/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Payment/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]