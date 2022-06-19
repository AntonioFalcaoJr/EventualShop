ARG SDK_VERSION="7.0.100-preview.5-1-alpine3.16"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Web/WebAPP/*.csproj ./Web/WebAPP/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Web/WebAPP

COPY ./src/Web/WebAPP/. ./Web/WebAPP/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Web/WebAPP
RUN dotnet build -c Release --no-restore -v m -o /app/build

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM nginx:alpine AS final
COPY --from=publish /app/publish/wwwroot ./usr/share/nginx/html