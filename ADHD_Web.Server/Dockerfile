FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ADHD_Web.Server/ADHD_Web.Server.csproj", "ADHD_Web.Server/"]
COPY ["adhd_web.client/adhd_web.client.esproj", "adhd_web.client/"]
RUN dotnet restore "./ADHD_Web.Server/ADHD_Web.Server.csproj"
COPY . .
WORKDIR "/src/ADHD_Web.Server"
RUN dotnet build "./ADHD_Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ADHD_Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ADHD_Web.Server.dll"]
