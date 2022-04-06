FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Web/WebAPP/WebAPP.csproj", "WebAPP/"]
RUN dotnet restore "src/Web/WebAPP/WebAPP.csproj"
COPY . .
WORKDIR "/src/WebAPP"
RUN dotnet build "WebAPP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPP.dll"]
