ARG ASPNET_VERSION="7.0.2"
ARG SDK_VERSION="7.0.102"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Services/Catalog/Query/Application/*.csproj ./Services/Catalog/Query/Application/
COPY ./src/Services/Catalog/Query/GrpcService/*.csproj ./Services/Catalog/Query/GrpcService/
COPY ./src/Services/Catalog/Query/Infrastructure.EventBus/*.csproj ./Services/Catalog/Query/Infrastructure.EventBus/
COPY ./src/Services/Catalog/Query/Infrastructure.Projections/*.csproj ./Services/Catalog/Query/Infrastructure.Projections/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Catalog/Query/GrpcService

COPY ./src/Services/Catalog/Query/Application/. ./Services/Catalog/Query/Application/
COPY ./src/Services/Catalog/Query/GrpcService/. ./Services/Catalog/Query/GrpcService/
COPY ./src/Services/Catalog/Query/Infrastructure.EventBus/. ./Services/Catalog/Query/Infrastructure.EventBus/
COPY ./src/Services/Catalog/Query/Infrastructure.Projections/. ./Services/Catalog/Query/Infrastructure.Projections/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Catalog/Query/GrpcService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
