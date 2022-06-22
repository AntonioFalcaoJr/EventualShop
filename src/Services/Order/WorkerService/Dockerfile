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

COPY ./src/Services/Order/Application/*.csproj ./Services/Order/Application/
COPY ./src/Services/Order/Domain/*.csproj ./Services/Order/Domain/
COPY ./src/Services/Order/Infrastructure.EventStore/*.csproj ./Services/Order/Infrastructure.EventStore/
COPY ./src/Services/Order/Infrastructure.MessageBus/*.csproj ./Services/Order/Infrastructure.MessageBus/
COPY ./src/Services/Order/Infrastructure.Projections/*.csproj ./Services/Order/Infrastructure.Projections/
COPY ./src/Services/Order/WorkerService/*.csproj ./Services/Order/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Order/WorkerService

COPY ./src/Services/Order/Application/. ./Services/Order/Application/
COPY ./src/Services/Order/Domain/. ./Services/Order/Domain/
COPY ./src/Services/Order/Infrastructure.EventStore/. ./Services/Order/Infrastructure.EventStore/
COPY ./src/Services/Order/Infrastructure.MessageBus/. ./Services/Order/Infrastructure.MessageBus/
COPY ./src/Services/Order/Infrastructure.Projections/. ./Services/Order/Infrastructure.Projections/
COPY ./src/Services/Order/WorkerService/. ./Services/Order/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Order/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]