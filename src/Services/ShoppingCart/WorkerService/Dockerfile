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

COPY ./src/Services/ShoppingCart/Application/*.csproj ./Services/ShoppingCart/Application/
COPY ./src/Services/ShoppingCart/Domain/*.csproj ./Services/ShoppingCart/Domain/
COPY ./src/Services/ShoppingCart/Infrastructure/*.csproj ./Services/ShoppingCart/Infrastructure/
COPY ./src/Services/ShoppingCart/WorkerService/*.csproj ./Services/ShoppingCart/WorkerService/
COPY ./src/Contracts/ECommerce/*.csproj ./Contracts/ECommerce/

RUN dotnet restore -v m ./Services/ShoppingCart/WorkerService

COPY ./src/Services/ShoppingCart/Application/. ./Services/ShoppingCart/Application/
COPY ./src/Services/ShoppingCart/Domain/. ./Services/ShoppingCart/Domain/
COPY ./src/Services/ShoppingCart/Infrastructure/. ./Services/ShoppingCart/Infrastructure/
COPY ./src/Services/ShoppingCart/WorkerService/. ./Services/ShoppingCart/WorkerService/
COPY ./src/Contracts/ECommerce/. ./Contracts/ECommerce/

WORKDIR /src/Services/ShoppingCart/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]