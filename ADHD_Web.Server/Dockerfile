# Use ASP.NET base image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use SDK and Node.js setup for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs

# Prepare the build environment and restore .NET and npm packages
FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore .NET project
COPY ["ADHD_Web.Server/ADHD_Web.Server.csproj", "ADHD_Web.Server/"]
RUN dotnet restore "./ADHD_Web.Server/ADHD_Web.Server.csproj"

# Copy the entire client-side directory to ensure all necessary files are available
COPY ["adhd_web.client/", "/src/adhd_web.client/"]

WORKDIR "/src/adhd_web.client"

# Install client-side dependencies
RUN npm install

# Build the client-side application
RUN npm run build

# Return to the server-side application and build it
WORKDIR "/src/ADHD_Web.Server"
COPY . .
RUN dotnet build "./ADHD_Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the server-side application
FROM build AS publish
RUN dotnet publish "./ADHD_Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Setup the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ADHD_Web.Server.dll"]
