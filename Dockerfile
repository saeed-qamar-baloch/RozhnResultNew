FROM mcr.microsoft.com/dotnet/aspnet:8 AS base  
WORKDIR /app  
EXPOSE 80  
EXPOSE 443  

FROM mcr.microsoft.com/dotnet/sdk:8 AS build  
WORKDIR /src  

COPY ["Result.csproj", "./"]  
RUN dotnet restore "Result.csproj"  

COPY . .

WORKDIR "/src/"  

RUN dotnet build "Result.csproj" -c Release -o /app/build  

FROM build AS publish  
RUN dotnet publish "Result.csproj" -c Release -o /app/publish  

FROM base AS final  
WORKDIR /app  
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Result.dll"]
