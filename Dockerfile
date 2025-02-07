# Step 1: Use an official .NET image as a base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Step 2: Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy your project file to the container and restore dependencies
COPY ["Result.csproj", "./"]
RUN dotnet restore "./Result.csproj"

# Step 3: Copy the entire project and publish the application
COPY . .
RUN dotnet publish -c Release -o /app

# Step 4: Define the final runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Result.dll"]
