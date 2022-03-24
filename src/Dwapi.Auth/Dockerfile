#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
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