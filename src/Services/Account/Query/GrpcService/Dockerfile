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

COPY ./src/Services/Account/Query/Application/*.csproj ./Services/Account/Query/Application/
COPY ./src/Services/Account/Query/GrpcService/*.csproj ./Services/Account/Query/GrpcService/
COPY ./src/Services/Account/Query/Infrastructure.EventBus/*.csproj ./Services/Account/Query/Infrastructure.EventBus/
COPY ./src/Services/Account/Query/Infrastructure.Projections/*.csproj ./Services/Account/Query/Infrastructure.Projections/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Account/Query/GrpcService

COPY ./src/Services/Account/Query/Application/. ./Services/Account/Query/Application/
COPY ./src/Services/Account/Query/GrpcService/. ./Services/Account/Query/GrpcService/
COPY ./src/Services/Account/Query/Infrastructure.EventBus/. ./Services/Account/Query/Infrastructure.EventBus/
COPY ./src/Services/Account/Query/Infrastructure.Projections/. ./Services/Account/Query/Infrastructure.Projections/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Account/Query/GrpcService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
