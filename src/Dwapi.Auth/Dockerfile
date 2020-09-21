#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Dwapi.Auth/Dwapi.Auth.csproj", "src/Dwapi.Auth/"]
RUN dotnet restore "src/Dwapi.Auth/Dwapi.Auth.csproj"
COPY . .
WORKDIR "/src/src/Dwapi.Auth"
RUN dotnet build "Dwapi.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dwapi.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dwapi.Auth.dll"]
