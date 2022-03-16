ARG ASPNET_VERSION="7.0.0-preview.1-alpine3.15"
ARG SDK_VERSION="7.0.100-preview.1-alpine3.15"
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

COPY ./src/Services/Catalog/Application/*.csproj ./Services/Catalog/Application/
COPY ./src/Services/Catalog/Domain/*.csproj ./Services/Catalog/Domain/
COPY ./src/Services/Catalog/Infrastructure/*.csproj ./Services/Catalog/Infrastructure/
COPY ./src/Services/Catalog/WorkerService/*.csproj ./Services/Catalog/WorkerService/
COPY ./src/Contracts/ECommerce/*.csproj ./Contracts/ECommerce/

RUN dotnet restore -v m ./Services/Catalog/WorkerService

COPY ./src/Services/Catalog/Application/. ./Services/Catalog/Application/
COPY ./src/Services/Catalog/Domain/. ./Services/Catalog/Domain/
COPY ./src/Services/Catalog/Infrastructure/. ./Services/Catalog/Infrastructure/
COPY ./src/Services/Catalog/WorkerService/. ./Services/Catalog/WorkerService/
COPY ./src/Contracts/ECommerce/. ./Contracts/ECommerce/

WORKDIR /src/Services/Catalog/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]