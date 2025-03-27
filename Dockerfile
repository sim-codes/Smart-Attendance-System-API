# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.sln .
COPY SmartAttendance/*.csproj ./SmartAttendance/
RUN dotnet restore

# Copy the rest of the application code and build the application
COPY SmartAttendance/ ./SmartAttendance/
WORKDIR /app/SmartAttendance
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/SmartAttendance/out ./
ENTRYPOINT ["dotnet", "SmartAttendance.dll"]

