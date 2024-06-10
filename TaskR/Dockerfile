FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
#EXPOSE 443

#ENV ASPNETCORE_ENVIRONMENT=Development # Add this line.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaskR.csproj", "./"]
RUN dotnet restore "TaskR.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TaskR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskR.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskR.dll"]
