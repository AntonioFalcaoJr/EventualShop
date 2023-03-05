ARG ASPNET_VERSION="8.0-preview"
ARG SDK_VERSION="8.0-preview"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

EXPOSE 80

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./

WORKDIR /src

COPY ./src/Web/WebAPI/*.csproj ./Web/WebAPI/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Web/WebAPI

COPY ./src/Web/WebAPI/. ./Web/WebAPI/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Web/WebAPI
RUN dotnet build -c Release --no-restore -v m -o /app/build

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y curl sudo
RUN curl -L -O https://artifacts.elastic.co/downloads/beats/filebeat/filebeat-8.6.2-amd64.deb
RUN sudo dpkg -i filebeat-8.6.2-amd64.deb

COPY filebeat.yml /etc/filebeat/
RUN chmod go-w /etc/filebeat/filebeat.yml

CMD filebeat modules enable elasticsearch
CMD filebeat setup 
CMD service filebeat start
CMD dotnet WebAPI.dll