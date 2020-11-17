FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["UserApi/UserApi.csproj", "UserApi.csproj"]
RUN dotnet restore "UserApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "UserApi/UserApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserApi/UserApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserApi.dll"]
