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

COPY ./src/Services/Identity/Query/Application/*.csproj ./Services/Identity/Query/Application/
COPY ./src/Services/Identity/Query/GrpcService/*.csproj ./Services/Identity/Query/GrpcService/
COPY ./src/Services/Identity/Query/Infrastructure.Authentication/*.csproj ./Services/Identity/Query/Infrastructure.Authentication/
COPY ./src/Services/Identity/Query/Infrastructure.EventBus/*.csproj ./Services/Identity/Query/Infrastructure.EventBus/
COPY ./src/Services/Identity/Query/Infrastructure.Projections/*.csproj ./Services/Identity/Query/Infrastructure.Projections/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Identity/Query/GrpcService

COPY ./src/Services/Identity/Query/Application/. ./Services/Identity/Query/Application/
COPY ./src/Services/Identity/Query/GrpcService/. ./Services/Identity/Query/GrpcService/
COPY ./src/Services/Identity/Query/Infrastructure.Authentication/. ./Services/Identity/Query/Infrastructure.Authentication/
COPY ./src/Services/Identity/Query/Infrastructure.EventBus/. ./Services/Identity/Query/Infrastructure.EventBus/
COPY ./src/Services/Identity/Query/Infrastructure.Projections/. ./Services/Identity/Query/Infrastructure.Projections/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Identity/Query/GrpcService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
