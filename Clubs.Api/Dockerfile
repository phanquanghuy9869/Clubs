#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Clubs.Api/Clubs.Api.csproj", "Clubs.Api/"]
COPY ["Clubs.Domain/Clubs.Domain.csproj", "Clubs.Domain/"]
COPY ["Clubs.Infrastructure/Clubs.Infrastructure.csproj", "Clubs.Infrastructure/"]
RUN dotnet restore "Clubs.Api/Clubs.Api.csproj"
COPY . .
WORKDIR "/src/Clubs.Api"
RUN dotnet build "Clubs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clubs.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clubs.Api.dll"]
