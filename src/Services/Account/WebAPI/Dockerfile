ARG ASPNET_VERSION="6.0.0-preview.7-alpine3.13"
ARG SDK_VERSION="6.0.100-preview.7-alpine3.13"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app
EXPOSE 5000

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build
WORKDIR /src

COPY ./src/Application/*.csproj ./Application/
COPY ./src/Domain/*.csproj ./Domain/
COPY ./src/Infrastructure/*.csproj ./Infrastructure/
COPY ./src/WebAPI/*.csproj ./WebAPI/

#COPY ./NuGet.Config ./
COPY ./Directory.Build.props ./

RUN dotnet restore ./WebAPI/

COPY ./src/Application/. ./Application/
COPY ./src/Domain/. ./Domain/
COPY ./src/Infrastructure/. ./Infrastructure/
COPY ./src/WebAPI/. ./WebAPI/
    
WORKDIR /src/WebAPI
RUN dotnet build -c Release --no-restore -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]