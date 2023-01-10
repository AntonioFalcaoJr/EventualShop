ARG ASPNET_VERSION="7.0.0"
ARG SDK_VERSION="7.0.100"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Services/ShoppingCart/Command/Application/*.csproj ./Services/ShoppingCart/Command/Application/
COPY ./src/Services/ShoppingCart/Command/Domain/*.csproj ./Services/ShoppingCart/Command/Domain/
COPY ./src/Services/ShoppingCart/Command/Infrastructure.EventStore/*.csproj ./Services/ShoppingCart/Command/Infrastructure.EventStore/
COPY ./src/Services/ShoppingCart/Command/Infrastructure.MessageBus/*.csproj ./Services/ShoppingCart/Command/Infrastructure.MessageBus/
COPY ./src/Services/ShoppingCart/Command/WorkerService/*.csproj ./Services/ShoppingCart/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/ShoppingCart/Command/WorkerService

COPY ./src/Services/ShoppingCart/Command/Application/. ./Services/ShoppingCart/Command/Application/
COPY ./src/Services/ShoppingCart/Command/Domain/. ./Services/ShoppingCart/Command/Domain/
COPY ./src/Services/ShoppingCart/Command/Infrastructure.EventStore/. ./Services/ShoppingCart/Command/Infrastructure.EventStore/
COPY ./src/Services/ShoppingCart/Command/Infrastructure.MessageBus/. ./Services/ShoppingCart/Command/Infrastructure.MessageBus/
COPY ./src/Services/ShoppingCart/Command/WorkerService/. ./Services/ShoppingCart/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/ShoppingCart/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]