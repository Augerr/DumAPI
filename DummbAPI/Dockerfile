# Use the official .NET 9 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY *.csproj ./

# Set an explicit NuGet package folder inside the container
ENV NUGET_PACKAGES=/root/.nuget/packages

# Restore dependencies with a clean cache
RUN dotnet restore --disable-parallel --no-cache --force

# Copy the entire project and build
COPY . ./
RUN dotnet publish -c Release -o /out --no-restore

# Use .NET 9 runtime for production
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .

# Expose API port
EXPOSE 8080
CMD ["dotnet", "DummbAPI.dll"]
