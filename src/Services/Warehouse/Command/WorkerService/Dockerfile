ARG ASPNET_VERSION="8.0-preview"
ARG SDK_VERSION="8.0-preview"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"
ARG FILEBEAT_VERSION="8.6.2-amd64.deb"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

RUN curl -L -O https://artifacts.elastic.co/downloads/beats/filebeat/filebeat-$FILEBEAT_VERSION \
  && dpkg -i filebeat-$FILEBEAT_VERSION \
  && rm filebeat-$FILEBEAT_VERSION

ADD filebeat.yml /etc/filebeat/filebeat.yml


COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Build.props ./


WORKDIR /src

COPY ./src/Services/Warehouse/Command/Application/*.csproj ./Services/Warehouse/Command/Application/
COPY ./src/Services/Warehouse/Command/Domain/*.csproj ./Services/Warehouse/Command/Domain/
COPY ./src/Services/Warehouse/Command/Infrastructure.EventStore/*.csproj ./Services/Warehouse/Command/Infrastructure.EventStore/
COPY ./src/Services/Warehouse/Command/Infrastructure.MessageBus/*.csproj ./Services/Warehouse/Command/Infrastructure.MessageBus/
COPY ./src/Services/Warehouse/Command/WorkerService/*.csproj ./Services/Warehouse/Command/WorkerService/
COPY ./src/Contracts/*.csproj ./Contracts/

RUN dotnet restore -v m ./Services/Warehouse/Command/WorkerService

COPY ./src/Services/Warehouse/Command/Application/. ./Services/Warehouse/Command/Application/
COPY ./src/Services/Warehouse/Command/Domain/. ./Services/Warehouse/Command/Domain/
COPY ./src/Services/Warehouse/Command/Infrastructure.EventStore/. ./Services/Warehouse/Command/Infrastructure.EventStore/
COPY ./src/Services/Warehouse/Command/Infrastructure.MessageBus/. ./Services/Warehouse/Command/Infrastructure.MessageBus/
COPY ./src/Services/Warehouse/Command/WorkerService/. ./Services/Warehouse/Command/WorkerService/
COPY ./src/Contracts/. ./Contracts/

WORKDIR /src/Services/Warehouse/Command/WorkerService

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WorkerService.dll"]

CMD ["/usr/bin/filebeat", "-c", "/etc/filebeat/filebeat.yml"]
