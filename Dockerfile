# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the entire solution
COPY . .

# Restore dependencies for the entire solution
RUN dotnet restore "SmartAttendance.csproj"

# Build the main project and its project references
RUN dotnet build "SmartAttendance.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "SmartAttendance.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install Azure CLI and other necessary tools
RUN apt-get update && apt-get install -y \
    curl \
    apt-transport-https \
    ca-certificates \
    lsb-release \
    && curl -sL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor | tee /etc/apt/trusted.gpg.d/microsoft.gpg > /dev/null \
    && AZ_REPO=$(lsb_release -cs) \
    && echo "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ $AZ_REPO main" | tee /etc/apt/sources.list.d/azure-cli.list \
    && apt-get update \
    && apt-get install -y azure-cli \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Expose ports
EXPOSE 8080
EXPOSE 443

# Copy the published output
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Set the entry point
ENTRYPOINT ["dotnet", "SmartAttendance.dll"]
