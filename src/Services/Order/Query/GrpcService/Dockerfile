ARG ASPNET_VERSION="7.0.1"
ARG SDK_VERSION="7.0.101"
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

COPY ./src/Services/Order/Query/Application/*.csproj ./Services/Order/Query/Application/
COPY ./src/Services/Order/Query/GrpcService/*.csproj ./Services/Order/Query/GrpcService/
COPY ./src/Services/Order/Query/Infrastructure.EventBus/*.csproj ./Services/Order/Query/Infrastructure.EventBus/
COPY ./src/Services/Order/Query/Infrastructure.Projections/*.csproj ./Services/Order/Query/Infrastructure.Projections/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Order/Query/GrpcService

COPY ./src/Services/Order/Query/Application/. ./Services/Order/Query/Application/
COPY ./src/Services/Order/Query/GrpcService/. ./Services/Order/Query/GrpcService/
COPY ./src/Services/Order/Query/Infrastructure.EventBus/. ./Services/Order/Query/Infrastructure.EventBus/
COPY ./src/Services/Order/Query/Infrastructure.Projections/. ./Services/Order/Query/Infrastructure.Projections/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Order/Query/GrpcService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
