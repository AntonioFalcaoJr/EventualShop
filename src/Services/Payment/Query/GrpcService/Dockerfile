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

COPY ./src/Services/Payment/Query/Application/*.csproj ./Services/Payment/Query/Application/
COPY ./src/Services/Payment/Query/GrpcService/*.csproj ./Services/Payment/Query/GrpcService/
COPY ./src/Services/Payment/Query/Infrastructure.EventBus/*.csproj ./Services/Payment/Query/Infrastructure.EventBus/
COPY ./src/Services/Payment/Query/Infrastructure.Projections/*.csproj ./Services/Payment/Query/Infrastructure.Projections/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Payment/Query/GrpcService

COPY ./src/Services/Payment/Query/Application/. ./Services/Payment/Query/Application/
COPY ./src/Services/Payment/Query/GrpcService/. ./Services/Payment/Query/GrpcService/
COPY ./src/Services/Payment/Query/Infrastructure.EventBus/. ./Services/Payment/Query/Infrastructure.EventBus/
COPY ./src/Services/Payment/Query/Infrastructure.Projections/. ./Services/Payment/Query/Infrastructure.Projections/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Payment/Query/GrpcService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcService.dll"]
