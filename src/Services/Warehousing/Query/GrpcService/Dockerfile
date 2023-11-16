ARG ASPNET_VERSION="8.0-preview"
ARG SDK_VERSION="8.0-preview"
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

COPY ./src/Services/Warehouse/Query/Application/*.csproj ./Services/Warehouse/Query/Application/
COPY ./src/Services/Warehouse/Query/GrpcService/*.csproj ./Services/Warehouse/Query/GrpcService/
COPY ./src/Services/Warehouse/Query/Infrastructure.EventBus/*.csproj ./Services/Warehouse/Query/Infrastructure.EventBus/
COPY ./src/Services/Warehouse/Query/Infrastructure.Projections/*.csproj ./Services/Warehouse/Query/Infrastructure.Projections/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Warehouse/Query/GrpcService

COPY ./src/Services/Warehouse/Query/Application/. ./Services/Warehouse/Query/Application/
COPY ./src/Services/Warehouse/Query/GrpcService/. ./Services/Warehouse/Query/GrpcService/
COPY ./src/Services/Warehouse/Query/Infrastructure.EventBus/. ./Services/Warehouse/Query/Infrastructure.EventBus/
COPY ./src/Services/Warehouse/Query/Infrastructure.Projections/. ./Services/Warehouse/Query/Infrastructure.Projections/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Warehouse/Query/GrpcService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
