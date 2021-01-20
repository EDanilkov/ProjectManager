FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

RUN /bin/bash -c 'ln -sf /usr/share/zoneinfo/Europe/Minsk /etc/localtime; \
dpkg-reconfigure -f noninteractive tzdata'

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["IdentityApi/IdentityApi.csproj", "IdentityApi.csproj"]
RUN dotnet restore "IdentityApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "IdentityApi/IdentityApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IdentityApi/IdentityApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityApi.dll"]

