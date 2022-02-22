ARG ASPNET_VERSION="6.0.1-alpine3.14"
ARG SDK_VERSION="6.0.101-alpine3.14"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

#RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build
WORKDIR /src

COPY ./src/Services/ShoppingCart/Application/*.csproj ./Application/
COPY ./src/Services/ShoppingCart/Domain/*.csproj ./Domain/
COPY ./src/Services/ShoppingCart/Infrastructure/*.csproj ./Infrastructure/
COPY ./src/Services/ShoppingCart/WorkerService/*.csproj ./WorkerService/
COPY ./src/Contracts/ECommerce/*.csproj ./Contracts/ECommerce/

COPY ./nuget.config ./
COPY ./Directory.Build.props ./

RUN dotnet restore ./WorkerService

COPY ./src/Services/ShoppingCart/Application/. ./Application/
COPY ./src/Services/ShoppingCart/Domain/. ./Domain/
COPY ./src/Services/ShoppingCart/Infrastructure/. ./Infrastructure/
COPY ./src/Services/ShoppingCart/WorkerService/. ./WorkerService/
COPY ./src/Contracts/ECommerce/. ./Contracts/ECommerce/

WORKDIR /src/WorkerService
RUN dotnet build -c Release --no-restore -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]